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
        /// Find Organisation Detail By Organisation ID
        /// </summary>
        Organisation FindDetailById(long organisationId);

        /// <summary>
        /// Add/Update Organization
        /// </summary>
        MyOrganizationSaveResponse SaveOrganization(Organisation organisation);

        /// <summary>
        /// Save File Path In Db against organization ID
        /// </summary>
        void SaveFile(string filePath);

        IList<int> GetOrganizationIds(int request);
    }
}
