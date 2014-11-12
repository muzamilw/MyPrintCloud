using Cares.Models.DomainModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Business Partner Individual Service Interface
    /// </summary>
    public interface IBusinessPartnerIndividualService
    {        
        /// <summary>
        /// Delete businsess partner individual
        /// </summary>
        /// <param name="businessPartner"></param>
        void DeleteBusinessPartnerIndividual(BusinessPartnerIndividual businessPartner);
        /// <summary>
        /// Get business partnere by business partner Id
        /// </summary>
        BusinessPartnerIndividual FindBusinessPartnerIndividualById(long id);
    }
}
