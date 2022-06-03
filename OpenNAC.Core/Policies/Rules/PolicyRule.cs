using Flexinets.Radius.Core;
using OpenNAC.Core.Policies.Rules;
using OpenNAC.Core.Radius;
using System;

namespace OpenNAC.Core.Policies
{
    public class PolicyRule
    {
        public string AttributeName { get; private set; }
        public Comparator Comparator { get; private set; }

        private Func<RadiusRequestContext, string> _computeAttributeValue;

        public PolicyRuleAction[] Actions { get; private set; }

        public PolicyRule(string attributeName, Comparator comparator, Func<RadiusRequestContext, string> value, PolicyRuleAction[] actions)
        {
            AttributeName = attributeName;
            Comparator = comparator;
            _computeAttributeValue = value;
            Actions = actions;
        }

        public PolicyRule(string attributeName, Comparator comparator, string value, PolicyRuleAction[] actions)
        {
            AttributeName = attributeName;
            Comparator = comparator;
            _computeAttributeValue = (context) => value;
            Actions = actions;
        }

        public RadiusRequestContext ApplyRule(RadiusRequestContext context)
        {
            foreach(var action in Actions)
            {
                context = action.Execute(context);
            }

            return context;
        }

        public bool IsSatisfied(RadiusRequestContext context)
        {
            return context.Request.GetAttribute<string>(AttributeName) == _computeAttributeValue(context);
        }
    }
}