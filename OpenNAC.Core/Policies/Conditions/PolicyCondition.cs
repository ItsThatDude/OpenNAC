using OpenNAC.Core.Radius;

namespace OpenNAC.Core.Policies
{
    public abstract class PolicyCondition
    {
        public abstract bool IsSatisfied(RadiusRequestContext context);
    }
}