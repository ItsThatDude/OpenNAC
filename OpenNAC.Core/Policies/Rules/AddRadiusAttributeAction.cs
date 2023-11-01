using OpenNAC.Core.Radius;
using System;

namespace OpenNAC.Core.Policies.Rules
{
    public class AddRadiusAttributeAction : PolicyRuleAction
    {
        public string AttributeName { get; private set; }

        private Func<RadiusRequestContext, string> _computeAttributeValue;

        public AddRadiusAttributeAction(string attributeName, Func<RadiusRequestContext, string> value)
        {
            AttributeName = attributeName;
            _computeAttributeValue = value;
        }

        public AddRadiusAttributeAction(string attributeName, string attributeValue)
            : this(attributeName, (RadiusRequestContext) => { return attributeValue; }) { }

        public override RadiusRequestContext Execute(RadiusRequestContext context)
        {
            context.Response.AddAttribute(AttributeName, _computeAttributeValue(context));

            return context;
        }
    }
}
