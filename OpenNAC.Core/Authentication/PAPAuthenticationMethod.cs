using OpenNAC.Core.Radius;

namespace OpenNAC.Core.Authentication
{
    public class PAPAuthenticationMethod : AuthenticationMethod
    {
        public PAPAuthenticationMethod() {}

        public override bool IsDetected(RadiusRequestContext context)
        {
            return context.Request.Attributes.ContainsKey("User-Name") && context.Request.Attributes.ContainsKey("User-Password");
        }
    }
}