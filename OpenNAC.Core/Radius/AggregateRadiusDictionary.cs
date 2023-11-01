using System.Collections.Generic;
using System.Linq;

namespace OpenNAC.Core.Radius
{
    public class AggregateRadiusDictionary : IRadiusDictionary
    {
        internal Dictionary<byte, DictionaryAttribute> Attributes { get; set; } = new Dictionary<byte, DictionaryAttribute>();
        internal List<DictionaryVendorAttribute> VendorSpecificAttributes { get; set; } = new List<DictionaryVendorAttribute>();
        internal Dictionary<string, DictionaryAttribute> AttributeNames { get; set; } = new Dictionary<string, DictionaryAttribute>();

        public AggregateRadiusDictionary() { }
        public AggregateRadiusDictionary(IEnumerable<RadiusDictionary> sourceDictionaries)
        {
            foreach(var dictionary in sourceDictionaries)
            {
                AddDictionary(dictionary);
            }
        }

        public void AddDictionary(RadiusDictionary dictionary)
        {
            foreach (var kvp in dictionary.GetAttributes())
            {
                if (!Attributes.ContainsKey(kvp.Key))
                    Attributes.Add(kvp.Key, kvp.Value);
            }

            foreach (var kvp in dictionary.GetAttributeNames())
            {
                if (!AttributeNames.ContainsKey(kvp.Key))
                    AttributeNames.Add(kvp.Key, kvp.Value);
            }

            foreach (var vsa in dictionary.GetVendorSpecificAttributes())
            {
                if (!VendorSpecificAttributes.Any(i => i.VendorId == vsa.VendorId && i.Code == vsa.Code))
                    VendorSpecificAttributes.Add(vsa);
            }
        }

        public IReadOnlyDictionary<byte, DictionaryAttribute> GetAttributes() => Attributes;
        public IReadOnlyDictionary<string, DictionaryAttribute> GetAttributeNames() => AttributeNames;
        public IEnumerable<DictionaryVendorAttribute> GetVendorSpecificAttributes() => VendorSpecificAttributes;

        public DictionaryVendorAttribute GetVendorAttribute(uint vendorId, byte vendorCode)
        {
            return VendorSpecificAttributes.FirstOrDefault(o => o.VendorId == vendorId && o.VendorCode == vendorCode);
        }

        public DictionaryAttribute GetAttribute(byte typecode)
        {
            return Attributes[typecode];
        }

        public DictionaryAttribute GetAttribute(string name)
        {
            AttributeNames.TryGetValue(name, out var attributeType);
            return attributeType;
        }
    }
}
