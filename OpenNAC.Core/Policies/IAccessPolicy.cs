using OpenNAC.Core.Authentication;
using System.Collections.Generic;

namespace OpenNAC.Core.Policies
{
    public interface IAccessPolicy
    {
        CollectionMatchPolicy ConditionMatchPolicy { get; set; }
        IEnumerable<PolicyCondition> Conditions { get; }

        IEnumerable<AuthenticationSource> AuthenticationSources { get; }

        CollectionMatchPolicy RuleMatchPolicy { get; set; }
        IEnumerable<PolicyRule> Rules { get; }
    }
}