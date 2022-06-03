using OpenNAC.Core.Authentication;
using System.Collections.Generic;

namespace OpenNAC.Core.Policies
{
    public class AccessPolicy : IAccessPolicy
    {
        public string Name { get; set; }

        public int Priority { get; set; }

        public bool Enabled { get; set; }

        public bool EnableAccounting { get; set; }

        public CollectionMatchRule ConditionMatchPolicy { get; set; }
        private List<PolicyCondition> _conditions = new List<PolicyCondition>();
        public IEnumerable<PolicyCondition> Conditions => _conditions;

        private List<AuthenticationMethod> _authenticationMethods = new List<AuthenticationMethod>();
        public IEnumerable<AuthenticationMethod> AuthenticationMethods => _authenticationMethods;

        private List<AuthenticationSource> _authenticationSources = new List<AuthenticationSource>();
        public IEnumerable<AuthenticationSource> AuthenticationSources => _authenticationSources;

        public CollectionMatchRule RuleMatchPolicy { get; set; }
        private List<PolicyRule> _rules = new List<PolicyRule>();
        public IEnumerable<PolicyRule> Rules => _rules;

        protected AccessPolicy() { }

        public AccessPolicy(string name, int priority)
        {
            Name = name;
            Priority = priority;
        }

        public void AddCondition(PolicyCondition condition)
        {
            _conditions.Add(condition);
        }

        public void RemoveCondition(PolicyCondition condition)
        {
            _conditions.Remove(condition);
        }

        public void AddAuthenticationMethod(AuthenticationMethod authMethod)
        {
            _authenticationMethods.Add(authMethod);
        }

        public void RemoveAuthenticationMethod(AuthenticationMethod authMethod)
        {
            _authenticationMethods.Remove(authMethod);
        }

        public void AddAuthenticationSource(AuthenticationSource authSource)
        {
            _authenticationSources.Add(authSource);
        }

        public void RemoveAuthenticationSource(AuthenticationSource authSource)
        {
            _authenticationSources.Remove(authSource);
        }

        public void AddRule(PolicyRule rule)
        {
            _rules.Add(rule);
        }

        public void RemoveRule(PolicyRule rule)
        {
            _rules.Remove(rule);
        }
    }
}
