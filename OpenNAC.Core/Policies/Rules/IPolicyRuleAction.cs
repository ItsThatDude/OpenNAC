using OpenNAC.Core.Radius;

namespace OpenNAC.Core.Policies.Rules
{
    public interface IPolicyRuleAction
    {
        RadiusRequestContext Execute(RadiusRequestContext context);
    }
}