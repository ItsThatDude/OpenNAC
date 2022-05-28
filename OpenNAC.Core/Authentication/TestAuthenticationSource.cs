using OpenNAC.Core.Radius;

namespace OpenNAC.Core.Authentication
{
    public class TestAuthenticationSource : AuthenticationSource
    {
        public override AuthenticationResult Authenticate(RadiusRequestContext context)
        {
            bool result = false;

            if (context.Request.GetAttribute<string>("User-Name") == "user@example.com" && context.Request.GetAttribute<string>("User-Password") == "1234")
            {
                result = true;
            }

            return new AuthenticationResult(nameof(TestAuthenticationSource), result);
        }
    }
}
