using System.Collections.Generic;

namespace OpenNAC.Core.Policies
{
    public interface IAccessPolicyRepository
    {
        IAccessPolicy Get(string name);

        IEnumerable<IAccessPolicy> GetAll();
    }
}