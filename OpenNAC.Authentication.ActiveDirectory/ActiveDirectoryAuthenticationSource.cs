using OpenNAC.Core.Authentication;
using OpenNAC.Core.Radius;
using System;

namespace OpenNAC.Authentication.ActiveDirectory
{
    public class ActiveDirectoryAuthenticationSource : AuthenticationSource
    {
        public override AuthenticationResult Authenticate(RadiusRequestContext context)
        {
            throw new NotImplementedException();
        }
    }
}
