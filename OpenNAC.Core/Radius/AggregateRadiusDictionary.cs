using Flexinets.Radius.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenNAC.Core.Radius
{
    public class AggregateRadiusDictionary : IRadiusDictionary
    {
        internal Dictionary<byte, DictionaryAttribute> Attributes { get; set; } = new Dictionary<byte, DictionaryAttribute>();
        internal List<DictionaryVendorAttribute> VendorSpecificAttributes { get; set; } = new List<DictionaryVendorAttribute>();
        internal Dictionary<string, DictionaryAttribute> AttributeNames { get; set; } = new Dictionary<string, DictionaryAttribute>();

        public AggregateRadiusDictionary(IEnumerable<RadiusDictionary> sourceDictionaries)
        {
            foreach(var dictionary in sourceDictionaries)
            {
                foreach (var kvp in dictionary.GetAttributes())
                {
                    if(!Attributes.ContainsKey(kvp.Key))
                        Attributes.Add(kvp.Key, kvp.Value);
                }

                foreach (var kvp in dictionary.GetAttributeNames())
                {
                    if(!AttributeNames.ContainsKey(kvp.Key))
                        AttributeNames.Add(kvp.Key, kvp.Value);
                }

                foreach(var vsa in dictionary.GetVendorSpecificAttributes())
                {
                    if (!VendorSpecificAttributes.Any(i => i.VendorId == vsa.VendorId && i.Code == vsa.Code))
                        VendorSpecificAttributes.Add(vsa);
                }
            }
        }

        public DictionaryAttribute GetAttribute(byte code)
        {
            throw new NotImplementedException();
        }

        public DictionaryAttribute GetAttribute(string name)
        {
            throw new NotImplementedException();
        }

        public DictionaryVendorAttribute GetVendorAttribute(uint vendorId, byte vendorCode)
        {
            throw new NotImplementedException();
        }
    }
}
