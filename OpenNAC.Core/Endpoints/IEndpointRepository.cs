using System.Collections.Generic;

namespace OpenNAC.Core.Endpoints
{
    public interface IEndpointRepository
    {
        public Endpoint Get(string macAddress);

        public IEnumerable<Endpoint> GetAll();
    }
}
