using System.Net;

namespace OpenNAC.Core.Net
{
    public interface IUdpClientFactory
    {
        IUdpClient CreateClient(IPEndPoint localEndpoint);
    }
}
