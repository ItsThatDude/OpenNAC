using System.Net;

namespace OpenNAC.Core.Radius
{
    public class RadiusClient
    {
        public IPAddress IPAddress { get; set; }
        public string Name { get; set; }

        public string PacketHandler { get; set; }
        public string SharedSecret { get; set; }
    }
}
