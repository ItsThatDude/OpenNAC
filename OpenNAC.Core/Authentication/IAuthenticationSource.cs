using Flexinets.Radius.Core;

namespace OpenNAC.Core.Authentication
{
    public interface IAuthenticationSource
    {
        bool Authenticate(IRadiusPacket packet);
    }
}