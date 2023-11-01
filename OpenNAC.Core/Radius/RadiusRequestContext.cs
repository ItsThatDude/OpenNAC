using OpenNAC.Core.Authentication;
using OpenNAC.Core.Endpoints;
using System.Collections.Generic;
using System.Net;

namespace OpenNAC.Core.Radius
{
    public class RadiusRequestContext
    {
        public IPAddress SourceAddress { get; private set; }
        public RadiusClient RequestDevice { get; private set; }
        public Endpoint RequestEndpoint { get; private set; }

        public IRadiusPacket Request { get; private set; }
        public IRadiusPacket Response { get; set; }

        public List<AuthenticationResult> Authentication { get; internal set; } = new List<AuthenticationResult>();

        public RadiusRequestContext(IPAddress sourceAddress, IRadiusPacket request, IRadiusPacket response, RadiusClient requestDevice = null, Endpoint requestEndpoint = null)
        {
            SourceAddress = sourceAddress;
            RequestDevice = requestDevice;
            RequestEndpoint = requestEndpoint;
            Request = request;
            Response = response;
        }
    }
}
