using System.Collections.Generic;

namespace OpenNAC.Core.Authentication
{
    public enum AuthenticationOutcome
    {
        FAIL = 0,
        SUCCESS = 1,
        CHALLENGE = 2
    }

    public class AuthenticationResult
    {
        public string Source { get; set; }

        public AuthenticationOutcome Outcome { get; set; }

        public IDictionary<string, string> Context { get; set; }

        public AuthenticationResult(string source, AuthenticationOutcome outcome, IDictionary<string, string> context = null)
        {
            Source = source;
            Outcome = outcome;
            Context = context ?? new Dictionary<string,string>();
        }
    }
}