using System.Collections.Generic;
using System.Linq;

namespace OpenNAC.Core.Policies
{
    public class InMemoryAccessPolicyRepository : IAccessPolicyRepository
    {
        private List<AccessPolicy> _accessPolicies = new List<AccessPolicy>();

        public InMemoryAccessPolicyRepository() { }

        public InMemoryAccessPolicyRepository(IEnumerable<AccessPolicy> policy)
        {
            _accessPolicies.AddRange(policy);
        }

        public void Add(AccessPolicy policy)
        {
            _accessPolicies.Add(policy);
        }

        public void Remove(AccessPolicy policy)
        {
            _accessPolicies.Remove(policy);
        }

        public IAccessPolicy Get(string name)
        {
            return _accessPolicies.FirstOrDefault(x => x.Name == name);
        }

        public IEnumerable<IAccessPolicy> GetAll()
        {
            return _accessPolicies;
        }
    }
}
