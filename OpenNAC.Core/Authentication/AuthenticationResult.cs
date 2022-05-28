using System.Collections.Generic;

namespace OpenNAC.Core.Authentication
{
    public class AuthenticationResult
    {
        public string Source { get; set; }

        public bool Result { get; set; }

        public IDictionary<string, string> Context { get; set; }

        public AuthenticationResult(string source, bool result, IDictionary<string, string> context = null)
        {
            Source = source;
            Result = result;
            Context = context ?? new Dictionary<string,string>();
        }
    }
}