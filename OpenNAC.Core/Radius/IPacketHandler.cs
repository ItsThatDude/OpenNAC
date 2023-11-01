using System;
using System.Net;

namespace OpenNAC.Core.Radius
{
    public interface IPacketHandler : IDisposable
    {
        IRadiusPacket HandlePacket(IPAddress sourceAddress, IRadiusPacket packet);
    }
}
