using OpenNAC.Core.Radius;

namespace OpenNAC.Core.Authentication
{
    public class CHAPAuthenticationMethod : AuthenticationMethod
    {
        public CHAPAuthenticationMethod() {}

        public override bool IsDetected(RadiusRequestContext context)
        {
            return context.Request.Attributes.ContainsKey("User-Name") && context.Request.Attributes.ContainsKey("CHAP-Password");
        }
    }
}