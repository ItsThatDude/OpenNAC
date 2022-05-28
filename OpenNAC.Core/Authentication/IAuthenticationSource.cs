using OpenNAC.Core.Radius;

namespace OpenNAC.Core.Authentication
{
    public interface IAuthenticationSource
    {
        AuthenticationResult Authenticate(RadiusRequestContext context);
    }
}