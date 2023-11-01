using OpenNAC.Core.Radius;

namespace OpenNAC.Core.Authentication.EAP
{
    public class EAPAuthenticationMethod : AuthenticationMethod
    {
        internal const string ATTR_EAP_Message = "EAP-Message";
        internal const string ATTR_Message_Authenticator = "Message-Authenticator";
        internal const string ATTR_State = "State";
        internal const string ATTR_Username = "User-Name";

        public EAPAuthenticationMethod() { }

        public override bool IsDetected(RadiusRequestContext context)
        {
            return context.Request.Attributes.ContainsKey(ATTR_EAP_Message) && context.Request.Attributes.ContainsKey(ATTR_Message_Authenticator);
        }
    }
}
