using Flexinets.Radius.Core;
using Microsoft.Extensions.Logging;
using OpenNAC.Core.Authentication;
using OpenNAC.Core.Endpoints;
using OpenNAC.Core.Policies;
using System;
using System.Linq;
using System.Net;

namespace OpenNAC.Core.Radius
{
    public abstract class PacketHandlerBase : IPacketHandler
    {
        protected readonly ILogger Logger;
        protected readonly IAccessPolicyRepository AccessPolicies;
        protected readonly IRadiusClientRepository RadiusClients;
        protected readonly IEndpointRepository Endpoints;

        public PacketHandlerBase(ILogger<PacketHandlerBase> logger,
            IAccessPolicyRepository accessPolicyRepository,
            IRadiusClientRepository radiusClientRepository,
            IEndpointRepository endpointRepository)
        {
            Logger = logger;
            AccessPolicies = accessPolicyRepository;
            RadiusClients = radiusClientRepository;
            Endpoints = endpointRepository;
        }

        public IRadiusPacket HandlePacket(IPAddress sourceAddress, IRadiusPacket packet)
        {
            var radiusClient = RadiusClients.Get(sourceAddress);

            if(radiusClient == null)
            {
                Logger.LogError("Radius Client is not recognized");
                throw new InvalidOperationException("Radius Client is not recognized.");
            }

            var attributesLogString = string.Empty;
            foreach(var attr in packet.Attributes)
            {
                attributesLogString += "\t" + attr.Key + " = " + string.Join(", ", attr.Value.Select(v => v.ToString())) + Environment.NewLine;
            }

            Logger.LogInformation("Received RADIUS Packet from {0} - Code: {1}\r\n" +
                "\tAttributes:\r\n{2}", sourceAddress, packet.Code, attributesLogString);

            var macAddress = packet.GetAttribute<string>("Calling-Station-Id");
            var requestEndpoint = Endpoints.Get(macAddress);

            if(requestEndpoint == null)
            {
                requestEndpoint = new Endpoint(macAddress, macAddress);
            }

            var context = new RadiusRequestContext(sourceAddress, packet, packet.CreateResponsePacket(PacketCode.AccessReject), radiusClient, requestEndpoint);

            var matchingPolicies = AccessPolicies.GetAll().Where(x => x.Enabled == true &&
                x.ConditionMatchPolicy == CollectionMatchRule.MATCH_ALL ?
                    x.Conditions.All(x => x.IsSatisfied(context)) : x.Conditions.Any(x => x.IsSatisfied(context)))
                        .OrderBy(x => x.Priority);

            if (matchingPolicies.Count() == 0)
            {
                Logger.LogWarning("There are no matching policies for this RADIUS Request.");
                if (packet.Code == PacketCode.AccessRequest)
                    return packet.CreateResponsePacket(PacketCode.AccessReject);
                else
                    return null;
            }

            foreach (var policy in matchingPolicies)
            {
                Logger.LogInformation("Evaluating Policy {0} for remote client {1}", policy.Name, sourceAddress);

                switch (packet.Code)
                {
                    case PacketCode.AccountingRequest:
                        if (policy.EnableAccounting)
                        {
                            var acctStatusType = packet.GetAttribute<AcctStatusType>("Acct-Status-Type");

                            switch (acctStatusType)
                            {
                                case AcctStatusType.Start:
                                    break;
                                case AcctStatusType.Stop:
                                    break;
                                case AcctStatusType.InterimUpdate:
                                    break;
                            }

                            return HandleAccountingRequest(context);
                        }
                        break;
                    case PacketCode.AccessRequest:

                        if (policy.AuthenticationSources.Count() > 0) {
                            var authenticationResults = policy.AuthenticationSources.Select(authSource => authSource.Authenticate(context));

                            if (authenticationResults.Any(a => a.Outcome == AuthenticationOutcome.CHALLENGE))
                            {
                                var pkt = packet.CreateResponsePacket(PacketCode.AccessChallenge);
                                pkt.AddAttribute("State", "test");
                                return pkt;
                            }

                            if (!authenticationResults.Any(a => a.Outcome == AuthenticationOutcome.SUCCESS))
                                return packet.CreateResponsePacket(PacketCode.AccessReject);

                            context.Authentication.AddRange(authenticationResults);
                        }

                        foreach(var rule in policy.Rules.Where(r => r.IsSatisfied(context)))
                        {
                            Logger.LogInformation("Applying Rule {0}", context.Response.Code);
                            var output = rule.ApplyRule(context);
                            Logger.LogInformation("PacketCode {0}", output.Response.Code);
                        }

                        return HandleAccessRequest(context) ?? packet.CreateResponsePacket(PacketCode.AccessReject);
                    default:
                        throw new InvalidOperationException(string.Format("Unhandled Radius Packet received. ({0})", packet.Code));
                }
            }

            throw new Exception("No matching policies could fulfil the RADIUS Request.");
        }

        public abstract IRadiusPacket HandleAccessRequest(RadiusRequestContext context);
        public abstract IRadiusPacket HandleAccountingRequest(RadiusRequestContext context);


        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
        }
    }
}
