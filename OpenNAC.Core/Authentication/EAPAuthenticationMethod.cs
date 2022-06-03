using OpenNAC.Core.Radius;

namespace OpenNAC.Core.Authentication
{
    public class EAPAuthenticationMethod : AuthenticationMethod
    {
        public EAPAuthenticationMethod() {}

        public override bool IsDetected(RadiusRequestContext context)
        {
            return context.Request.Attributes.ContainsKey("EAP-Message") && context.Request.Attributes.ContainsKey("Message-Authenticator");
        }
    }
}
