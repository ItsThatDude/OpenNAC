using OpenNAC.Core.Authentication;
using System.Collections.Generic;

namespace OpenNAC.Core.Policies
{
    public interface IAccessPolicy
    {
        int Priority { get; }
        bool Enabled { get; }
        bool EnableAccounting { get; }
        CollectionMatchRule ConditionMatchPolicy { get; set; }
        IEnumerable<PolicyCondition> Conditions { get; }

        IEnumerable<AuthenticationSource> AuthenticationSources { get; }

        CollectionMatchRule RuleMatchPolicy { get; set; }
        IEnumerable<PolicyRule> Rules { get; }
    }
}