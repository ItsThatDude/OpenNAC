using System.Collections.Generic;
using System.Net;

namespace OpenNAC.Core.Endpoints
{
    public class Endpoint
    {
        public string Name { get; set; }

        public string MACAddress { get; set; }

        public IPAddress LastKnownIPAddress { get; set; }

        public IDictionary<string, string> Attributes { get; set; }

        protected Endpoint() { }

        public Endpoint(string name, string macAddress)
        {
            Name = name;
            MACAddress = macAddress;

            Attributes = new Dictionary<string, string>();
        }
    }
}
