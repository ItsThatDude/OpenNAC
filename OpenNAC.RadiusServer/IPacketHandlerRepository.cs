using OpenNAC.Core.Radius;
using System.Net;

namespace OpenNAC.RadiusServer
{
    public interface IPacketHandlerRepository
    {
        bool TryGetHandler(IPAddress remoteAddress, out (IPacketHandler packetHandler, string sharedSecret) handler);
    }
}
