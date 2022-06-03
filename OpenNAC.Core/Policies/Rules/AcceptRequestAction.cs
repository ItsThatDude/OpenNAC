using OpenNAC.Core.Radius;

namespace OpenNAC.Core.Policies.Rules
{
    public class AcceptRequestAction : PolicyRuleAction
    {
        public override RadiusRequestContext Execute(RadiusRequestContext context)
        {
            context.Response = context.Response.CreateResponsePacket(Flexinets.Radius.Core.PacketCode.AccessAccept);

            return context;
        }
    }
}
