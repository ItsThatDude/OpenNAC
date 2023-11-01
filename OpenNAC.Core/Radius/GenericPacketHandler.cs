using Microsoft.Extensions.Logging;
using OpenNAC.Core.Endpoints;
using OpenNAC.Core.Policies;

namespace OpenNAC.Core.Radius
{
    public class GenericPacketHandler : PacketHandlerBase
    {
        public GenericPacketHandler(ILogger<GenericPacketHandler> logger,
            IAccessPolicyRepository accessPolicyRepository,
            IRadiusClientRepository radiusClientRepository,
            IEndpointRepository endpointRepository)
            : base(logger, accessPolicyRepository, radiusClientRepository, endpointRepository) { }

        public override IRadiusPacket HandleAccessRequest(RadiusRequestContext context)
        {
            return context.Response;
        }

        public override IRadiusPacket HandleAccountingRequest(RadiusRequestContext context)
        {
            return context.Response;
        }
    }
}
