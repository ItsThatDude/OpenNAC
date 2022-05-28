using Flexinets.Radius.Core;
using OpenNAC.Core.Endpoints;

namespace OpenNAC.Core.Radius
{
    public class RadiusRequestContext
    {
        public RadiusClient RequestDevice { get; private set; }
        public Endpoint RequestEndpoint { get; private set; }

        public IRadiusPacket Request { get; private set; }
        public IRadiusPacket Response { get; set; }

        public RadiusRequestContext(IRadiusPacket request, IRadiusPacket response, RadiusClient requestDevice = null, Endpoint requestEndpoint = null)
        {
            RequestDevice = requestDevice;
            RequestEndpoint = requestEndpoint;
            Request = request;
            Response = response;
        }
    }
}
