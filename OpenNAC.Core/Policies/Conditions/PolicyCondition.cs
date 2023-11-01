using OpenNAC.Core.Radius;

namespace OpenNAC.Core.Policies
{
    public abstract class PolicyCondition : IPolicyCondition
    {
        public abstract bool IsSatisfied(RadiusRequestContext context);
    }
}