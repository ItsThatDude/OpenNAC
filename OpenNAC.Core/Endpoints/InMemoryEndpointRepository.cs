using System.Collections.Generic;
using System.Linq;

namespace OpenNAC.Core.Endpoints
{
    public class InMemoryEndpointRepository : IEndpointRepository
    {
        private List<Endpoint> _endpoints = new List<Endpoint>();

        public InMemoryEndpointRepository() { }

        public InMemoryEndpointRepository(IEnumerable<Endpoint> endpoint)
        {
            _endpoints.AddRange(endpoint);
        }

        public void Add(Endpoint endpoint)
        {
            _endpoints.Add(endpoint);
        }

        public void Remove(Endpoint endpoint)
        {
            _endpoints.Remove(endpoint);
        }

        public Endpoint Get(string name)
        {
            return _endpoints.FirstOrDefault(x => x.Name == name);
        }

        public IEnumerable<Endpoint> GetAll()
        {
            return _endpoints;
        }
    }
}
