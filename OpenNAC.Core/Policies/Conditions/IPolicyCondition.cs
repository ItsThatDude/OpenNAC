using OpenNAC.Core.Radius;

namespace OpenNAC.Core.Policies
{
    public interface IPolicyCondition
    {
        bool IsSatisfied(RadiusRequestContext context);
    }
}