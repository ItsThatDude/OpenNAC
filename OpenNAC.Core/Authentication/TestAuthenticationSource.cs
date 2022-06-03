using OpenNAC.Core.Radius;
using System;
using System.Collections.Generic;

namespace OpenNAC.Core.Authentication
{
    public class TestAuthenticationSource : AuthenticationSource
    {
        public override AuthenticationResult Authenticate(RadiusRequestContext context)
        {
            var username = context.Request.GetAttribute<string>("User-Name");
            var password = context.Request.GetAttribute<string>("User-Password");
            var chapPassword = context.Request.GetAttribute<byte[]>("CHAP-Password");

            if (chapPassword != null)
                return new AuthenticationResult(nameof(TestAuthenticationSource), AuthenticationOutcome.CHALLENGE, new Dictionary<string, string>(new[]
                {
                    new KeyValuePair<string,string>("State", "")
                }));

            if (context.Request.GetAttribute<string>("User-Name") == "user@example.com" && context.Request.GetAttribute<string>("User-Password") == "1234")
            {
                return new AuthenticationResult(nameof(TestAuthenticationSource), AuthenticationOutcome.SUCCESS);
            }

            return new AuthenticationResult(nameof(TestAuthenticationSource), AuthenticationOutcome.FAIL);
        }
    }
}
