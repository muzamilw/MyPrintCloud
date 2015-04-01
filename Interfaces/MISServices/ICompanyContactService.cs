using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.MISServices
{
    public interface ICompanyContactService
    {
        /// <summary>
        /// Save
        /// </summary>
        CompanyContact Save(CompanyContact companyContact);
        /// <summary>
        /// Delete
        /// </summary>
        bool Delete(long companyContactId);

        /// <summary>
        /// Get Company Contacts
        /// </summary>
        CompanyContactResponse SearchCompanyContacts(CompanyContactRequestModel request);

        /// <summary>
        /// Deletion for Crm
        /// </summary>
        bool DeleteContactForCrm(long companyContactId);

        /// <summary>
        /// Get Base Data
        /// </summary>
        CompanyBaseResponse GetBaseData();

        /// <summary>
        /// Get Contact Detail
        /// </summary>
        CompanyBaseResponse GetContactDetail(short companyId);
        /// <summary>
        /// Get Addresses and Territories Of "Company Contact's company"
        /// </summary>
        CrmContactResponse SearchAddressesAndTerritories(CompanyContactRequestModel request);
    }
}
