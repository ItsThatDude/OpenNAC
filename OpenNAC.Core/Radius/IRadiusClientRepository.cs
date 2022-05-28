using System.Collections.Generic;
using System.Net;

namespace OpenNAC.Core.Radius
{
    public interface IRadiusClientRepository
    {
        public RadiusClient Get(IPAddress ipAddress);

        public IEnumerable<RadiusClient> GetAll();
    }
}
