using OpenNAC.Core.Radius;

namespace OpenNAC.Core.Policies.Rules
{
    public abstract class PolicyRuleAction : IPolicyRuleAction
    {
        public abstract RadiusRequestContext Execute(RadiusRequestContext context);
    }
}
