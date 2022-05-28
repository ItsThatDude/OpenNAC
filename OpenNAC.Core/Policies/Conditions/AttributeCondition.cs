using OpenNAC.Core.Radius;

namespace OpenNAC.Core.Policies.Conditions
{
    public class AttributeCondition : PolicyCondition
    {
        public string Attribute { get; private set; }
        public Comparator Comparator { get; private set; }
        public string Value { get; private set; }

        public AttributeCondition(string attribute, Comparator comparator, string value)
        {
            Attribute = attribute;
            Comparator = comparator;
            Value = value;
        }

        public override bool IsSatisfied(RadiusRequestContext context)
        {
            var attribute = context.Request.GetAttribute<string>(Attribute);

            switch(Comparator)
            {
                case Comparator.EQUAL_TO:
                    return attribute == Value;
                case Comparator.CONTAINS:
                    return attribute.Contains(Value);
                default:
                    return false;
            }
        }
    }
}
