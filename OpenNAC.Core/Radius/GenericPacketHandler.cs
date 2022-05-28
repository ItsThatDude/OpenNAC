using Flexinets.Radius.Core;
using Microsoft.Extensions.Logging;
using OpenNAC.Core.Policies;
using System.Collections.Generic;

namespace OpenNAC.Core.Radius
{
    public class GenericPacketHandler : PacketHandlerBase
    {
        public GenericPacketHandler(RadiusClient source, IEnumerable<AccessPolicy> accessPolicies, ILogger logger)
            : base(source, accessPolicies, logger) { }

        public override IRadiusPacket HandleAccessPacket(RadiusRequestContext context)
        {
            return context.Response;
        }
    }
}
