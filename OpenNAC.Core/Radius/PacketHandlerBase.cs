using Flexinets.Radius.Core;
using Microsoft.Extensions.Logging;
using OpenNAC.Core.Policies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace OpenNAC.Core.Radius
{
    public abstract class PacketHandlerBase : IPacketHandler
    {
        private readonly RadiusClient _source;
        private readonly IEnumerable<AccessPolicy> _accessPolicies;

        protected readonly ILogger Logger;

        public PacketHandlerBase(RadiusClient source, IEnumerable<AccessPolicy> accessPolicies, ILogger logger)
        {
            _source = source;
            _accessPolicies = accessPolicies;
            Logger = logger;
        }

        public IRadiusPacket HandlePacket(IPAddress sourceAddress, IRadiusPacket packet)
        {
            var attributesLogString = string.Empty;
            foreach(var attr in packet.Attributes)
            {
                attributesLogString += "\t" + attr.Key + " = " + string.Join(", ", attr.Value.Select(v => v.ToString())) + Environment.NewLine;
            }

            Logger.LogInformation("Received RADIUS Packet from {0} ({1}) - Code: {2}\r\n" +
                "\tAttributes:\r\n{3}", _source.Name, _source.IPAddress, packet.Code, attributesLogString);

            var matchingPolicies = _accessPolicies.Where(x => x.Enabled == true &&
                x.ConditionMatchPolicy == CollectionMatchPolicy.MATCH_ALL ?
                    x.Conditions.All(x => x.IsSatisfied()) : x.Conditions.Any(x => x.IsSatisfied()))
                        .OrderBy(x => x.Priority);

            if (matchingPolicies.Count() == 0)
            {
                Logger.LogWarning("There are no matching policies for this RADIUS Request.");
                if (packet.Code == PacketCode.AccessRequest)
                    return packet.CreateResponsePacket(PacketCode.AccessReject);
                else
                    return null;
            }

            var macAddress = packet.GetAttribute<string>("Calling-Station-Id");

            foreach (var policy in matchingPolicies)
            {
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

                            return packet.CreateResponsePacket(PacketCode.AccountingResponse);
                        }
                        break;
                    case PacketCode.AccessRequest:
                        IRadiusPacket responsePacket = packet.CreateResponsePacket(PacketCode.AccessReject);

                        var authenticationResults = policy.AuthenticationSources.Select(authSource => authSource.Authenticate(packet));

                        if (packet.GetAttribute<string>("User-Name") == "user@example.com" && packet.GetAttribute<string>("User-Password") == "1234")
                        {
                            responsePacket = packet.CreateResponsePacket(PacketCode.AccessAccept);
                            responsePacket.AddAttribute("Acct-Interim-Interval", 60);
                        }

                        return HandleAccessPacket(new RadiusRequestContext(packet, responsePacket, _source));
                    default:
                        throw new InvalidOperationException(string.Format("Unhandled Radius Packet received. ({0})", packet.Code));
                }
            }

            return null;
        }

        public abstract IRadiusPacket HandleAccessPacket(RadiusRequestContext context);


        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
        }
    }
}
