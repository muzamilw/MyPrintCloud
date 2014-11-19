using System.Collections.Generic;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.IServices
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
        /// Find Organisation Detail By Organisation ID
        /// </summary>
        Organisation FindDetailById(int companySiteId);

        /// <summary>
        /// Add/Update Company Sites
        /// </summary>
        long SaveCompanySite(Organisation organisation);

        IList<int> GetOrganizationIds(int request);
    }
}
