using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.Repositories;

namespace MPC.Implementation.MISServices
{
    public class InquiryService : IInquiryService
    {
        #region Private
        private readonly IEstimateInquiryRepository estimateInquiryRepository;
        private readonly IOrganisationRepository organisationRepository;

        #endregion
        #region Constructor
        public InquiryService(IOrganisationRepository organisationRepository, IEstimateInquiryRepository estimateInquiryRepository)
        {
            this.organisationRepository = organisationRepository;
            this.estimateInquiryRepository = estimateInquiryRepository;
        }

        #endregion

        #region Public
        /// <summary>
        /// Get All Orders
        /// </summary>
        public GetInquiryResponse GetAll(GetInquiryRequest request)
        {
            return estimateInquiryRepository.GetInquiries(request);
        }
        /// <summary>
        /// Add Inquiry
        /// </summary>
        /// <param name="inquiry"></param>
        /// <returns></returns>
        public Inquiry Add(Inquiry inquiry)
        {
            inquiry.OrganisationId = organisationRepository.OrganisationId;
            estimateInquiryRepository.Add(inquiry);
            estimateInquiryRepository.SaveChanges();
            return inquiry;
        }

        /// <summary>
        /// Update Inquiry
        /// </summary>
        public Inquiry Update(Inquiry inquiry)
        {
            inquiry.OrganisationId = organisationRepository.OrganisationId;
            estimateInquiryRepository.Update(inquiry);
            estimateInquiryRepository.SaveChanges();
            return inquiry;
        }
        /// <summary>
        /// Delete Inquiry
        /// </summary>
        /// <param name="inquiryId"></param>
        /// <returns></returns>
        public bool Delete(int inquiryId)
        {
            Inquiry paperSize = GetInquiryById(inquiryId);
            //todo delete work
            estimateInquiryRepository.SaveChanges();
            return true;
        }

        public Inquiry GetInquiryById(int id)
        {
            return estimateInquiryRepository.Find(id);
        }
        #endregion
    }
}
