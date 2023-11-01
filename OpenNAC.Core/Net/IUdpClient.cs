using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using System;

namespace OpenNAC.Core.Net
{
    public interface IUdpClient : IDisposable
    {
        Socket Client { get; }

        void Close();
        void Send(byte[] content, int length, IPEndPoint recipient);
        Task<int> SendAsync(byte[] content, int length, IPEndPoint remoteEndpoint);
        Task<UdpReceiveResult> ReceiveAsync();
    }
}