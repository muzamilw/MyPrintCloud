using System.Collections.Generic;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.MISServices
{
    /// <summary>
    /// My Organization Service Interface
    /// </summary>
    public interface IMyOrganizationService
    {
        /// <summary>
        /// Load My Organization Base data
        /// </summary>
        MyOrganizationBaseResponse GetBaseData();

        /// <summary>
        /// Find Company Site Detail By Company Site ID
        /// </summary>
        CompanySites FindDetailById(int companySiteId);

        /// <summary>
        /// Add/Update Company Sites
        /// </summary>
        int SaveCompanySite(CompanySites companySites);

        IList<int> GetOrganizationIds(int request);
    }
}
