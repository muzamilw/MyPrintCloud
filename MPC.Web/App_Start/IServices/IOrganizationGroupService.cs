using Cares.Models.DomainModels;
using Cares.Models.RequestModels;
using Cares.Models.ResponseModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Organization Group Service interface
    /// </summary>
    public interface IOrganizationGroupService
    {
        /// <summary>
        /// Search Organization Group 
        /// </summary>
        OrgGroupResponse SerchOrgGroup(OrgGroupSearchRequest request);

        /// <summary>
        /// Delete Organization Group 
        /// </summary>
        void DeleteOrgGroup(long orgGroupId);

        /// <summary>
        /// Add / Update Organization Group 
        /// </summary>
        OrgGroup AddUpdateOrgGroup(OrgGroup requestOrgGroup);
    }
}
