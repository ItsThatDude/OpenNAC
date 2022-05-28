using OpenNAC.Core.Radius;

namespace OpenNAC.Core.Authentication
{
    public abstract class AuthenticationSource : IAuthenticationSource
    {
        public abstract AuthenticationResult Authenticate(RadiusRequestContext context);
    }
}
