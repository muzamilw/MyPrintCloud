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
        /// Load My Organization Base data for Regional Settings
        /// </summary>
        MyOrganizationBaseResponse GetRegionalSettingBaseData();
        /// <summary>
        /// Get Organisation Detail 
        /// </summary>
        Organisation GetOrganisationDetail();

        /// <summary>
        /// Add/Update Organization
        /// </summary>
        MyOrganizationSaveResponse SaveOrganization(Organisation organisation);

        /// <summary>
        /// Save File Path In Db against organization ID
        /// </summary>
        void SaveFile(string filePath);

        IList<int> GetOrganizationIds(int request);

        /// <summary>
        /// Save File Path to 
        /// </summary>
        void SaveFilePath(string path);

        List<LanguageEditor> ReadResourceFileByLanguageId(long organisationId, long lanuageId);

        bool DeleteOrganisation(long OrganisationID);

        IEnumerable<Markup> GetMarkups();
        void UpdateOrganisationLicensing(long organisationId, int storesCount, bool isTrial, int misOrdersCount, int webStoreOrdersCount);

    }
}
