using OpenNAC.Core.Radius;

namespace OpenNAC.Core.Authentication
{
    public abstract class AuthenticationMethod : IAuthenticationMethod
    {
        public abstract bool IsDetected(RadiusRequestContext context);
    }
}
