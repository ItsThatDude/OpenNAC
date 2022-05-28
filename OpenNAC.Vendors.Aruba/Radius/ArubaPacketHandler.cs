using Flexinets.Radius.Core;
using Microsoft.Extensions.Logging;
using OpenNAC.Core.Policies;
using OpenNAC.Core.Radius;
using System.Collections.Generic;

namespace OpenNAC.Vendors.Aruba.Radius
{
    public class ArubaPacketHandler : PacketHandlerBase
    {
        public ArubaPacketHandler(RadiusClient source, IEnumerable<AccessPolicy> accessPolicies, ILogger logger)
            : base(source, accessPolicies, logger)
        {
        }

        public override IRadiusPacket HandleAccessPacket(RadiusRequestContext context)
        {
            return context.Response;
        }
    }
}
