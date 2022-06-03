using OpenNAC.Core.Radius;

namespace OpenNAC.Core.Authentication
{
    public interface IAuthenticationMethod
    {
        bool IsDetected(RadiusRequestContext context);
    }
}
