using Microsoft.AspNetCore.HttpOverrides;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace OpenNAC.Core.Radius
{
    public class InMemoryRadiusClientRepository : IRadiusClientRepository
    {
        private List<RadiusClient> _clients = new List<RadiusClient>();

        public InMemoryRadiusClientRepository() { }

        public InMemoryRadiusClientRepository(IEnumerable<RadiusClient> clients)
        {
            _clients.AddRange(clients);
        }

        public void Add(RadiusClient client)
        {
            _clients.Add(client);
        }

        public void Remove(RadiusClient client)
        {
            _clients.Remove(client);
        }

        public RadiusClient Get(IPAddress ipAddress)
        {
            return _clients.FirstOrDefault(x => x.IPAddress.Equals(ipAddress));
        }

        public IEnumerable<RadiusClient> GetAll()
        {
            return _clients;
        }
    }
}
