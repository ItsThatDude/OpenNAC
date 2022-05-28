using Flexinets.Radius.Core;
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

        public override IRadiusPacket HandleAccessPacket(RadiusRequestContext context)
        {
            return context.Response;
        }
    }
}
