using Flexinets.Radius.Core;

namespace OpenNAC.Core.Authentication
{
    public abstract class AuthenticationSource : IAuthenticationSource
    {
        public abstract bool Authenticate(IRadiusPacket packet);
    }
}
