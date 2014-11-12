using Cares.Models.DomainModels;
using Cares.Models.RequestModels;
using Cares.Models.ResponseModels;
using CommonTypes = Cares.Models.CommonTypes;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Business Partner Service Interface
    /// </summary>
    public interface IBusinessPartnerService
    {
        /// <summary>
        /// Get all business partneres
        /// </summary>
        BusinessPartnerSearchResponse LoadAllBusinessPartners(BusinessPartnerSearchRequest businessPartnerSearchRequest);
        /// <summary>
        /// Delete businsess partner
        /// </summary>
        void DeleteBusinessPartner(BusinessPartner businessPartner);
        /// <summary>
        /// Add business partner
        /// </summary>
        bool AddBusinessPartner(BusinessPartner businessPartner);
        /// <summary>
        /// Update business partner
        /// </summary>
        bool UpdateBusinessPartner(BusinessPartner businessPartner);

        /// <summary>
        /// Get For Rental Agreement
        /// </summary>
        BusinessPartner GetForRentalAgreement(GetBusinessPartnerRequest request);

        /// <summary>
        /// Get business partnere by Id
        /// </summary>
        BusinessPartner FindBusinessPartnerById(long id);      

        /// <summary>
        /// Get business partnere by License No
        /// </summary>
        BusinessPartner GetByLicenseNo(string licenseNo);

        /// <summary>
        /// Get business partnere by Nic No
        /// </summary>
        BusinessPartner GetByNicNo(string nicNo);

        /// <summary>
        /// Get business partnere by Passport No
        /// </summary>
        BusinessPartner GetByPassportNo(string passportNo);

        /// <summary>
        /// Get business partnere by Phone No
        /// </summary>
        BusinessPartner GetByPhoneNo(string phoneNo, CommonTypes.PhoneType phoneType);

    }
}
