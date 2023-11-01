using Microsoft.Extensions.Logging;
using OpenNAC.Core.Endpoints;
using OpenNAC.Core.Policies;
using OpenNAC.Core.Radius;

namespace OpenNAC.Vendors.Aruba.Radius
{
    public class ArubaPacketHandler : PacketHandlerBase
    {
        public ArubaPacketHandler(ILogger<ArubaPacketHandler> logger,
            IAccessPolicyRepository accessPolicyRepository,
            IRadiusClientRepository radiusClientRepository,
            IEndpointRepository endpointRepository)
            : base(logger, accessPolicyRepository, radiusClientRepository, endpointRepository) { }

        public override IRadiusPacket HandleAccessRequest(RadiusRequestContext context)
        {
            var calledStationId = context.Request.GetAttribute<string>("Called-Station-ID");
            var callingStationId = context.Request.GetAttribute<string>("Calling-Station-ID");

            Logger.LogInformation("Called Station Id: {0}; Calling Station Id: {1}", calledStationId, callingStationId);

            return context.Response;
        }

        public override IRadiusPacket HandleAccountingRequest(RadiusRequestContext context)
        {
            return context.Response;
        }
    }
}
