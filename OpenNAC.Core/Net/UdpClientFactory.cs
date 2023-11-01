using System.Net;

namespace OpenNAC.Core.Net
{
    public class UdpClientFactory : IUdpClientFactory
    {
        public IUdpClient CreateClient(IPEndPoint localEndpoint)
        {
            return new UdpClientWrapper(localEndpoint);
        }
    }
}
