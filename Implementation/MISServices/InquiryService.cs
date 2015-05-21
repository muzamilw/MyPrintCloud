using System;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.ModelMappers;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.Repositories;

namespace MPC.Implementation.MISServices
{
    public class InquiryService : IInquiryService
    {
        #region Private
        private readonly IEstimateInquiryRepository estimateInquiryRepository;
        private readonly IInquiryItemRepository inquiryItemRepository;
        private readonly IOrganisationRepository organisationRepository;
        private readonly IPrefixRepository prefixRepository;
        private readonly IEstimateRepository estimateRepository;

        #endregion
        #region Constructor
        public InquiryService(IOrganisationRepository organisationRepository, IEstimateInquiryRepository estimateInquiryRepository, IPrefixRepository prefixRepository, IInquiryItemRepository inquiryItemRepository, IEstimateRepository estimateRepository)
        {
            this.organisationRepository = organisationRepository;
            this.estimateInquiryRepository = estimateInquiryRepository;
            this.prefixRepository = prefixRepository;
            this.inquiryItemRepository = inquiryItemRepository;
            this.estimateRepository = estimateRepository;
        }
        private Inquiry CreateNewInquiry()
        {
            string inquiryCode = prefixRepository.GetNextInquiryCodePrefix();
            Inquiry itemTarget = estimateInquiryRepository.Create();
            estimateInquiryRepository.Add(itemTarget);
            itemTarget.CreatedDate = itemTarget.CreatedDate = DateTime.Now;
            itemTarget.InquiryCode = inquiryCode;
            itemTarget.OrganisationId = estimateInquiryRepository.OrganisationId;
            return itemTarget;
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
            inquiry.InquiryCode = prefixRepository.GetNextInquiryCodePrefix();
            estimateInquiryRepository.Add(inquiry);
            estimateInquiryRepository.SaveChanges();
            return inquiry;
        }

        /// <summary>
        /// Update Inquiry
        /// </summary>
        public Inquiry Update(Inquiry recievedInquiry)
        {
            Inquiry inquiry = GetInquiryById(recievedInquiry.InquiryId) ?? CreateNewInquiry();
            // Update Inquiry
            recievedInquiry.UpdateTo(inquiry, new InquiryMapperActions
            {
                CreateInquiryItem = CreateNewInquiryItem,
                DeleteInquiryItem = DeleteInquiryItem
            });
            // Save Changes
            estimateInquiryRepository.SaveChanges();
            return inquiry;


        }
        /// <summary>
        /// Create New Inquiry Item
        /// </summary>
        /// <returns></returns>
        private InquiryItem CreateNewInquiryItem()
        {
            InquiryItem itemTarget = new InquiryItem();
            inquiryItemRepository.Add(itemTarget);
            return itemTarget;
        }
        /// <summary>
        /// Delete Inquiry Item
        /// </summary>
        private void DeleteInquiryItem(InquiryItem inquiryItem)
        {
            inquiryItemRepository.Delete(inquiryItem);
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
            var estimateId = estimateRepository.GetEstimateIdOfInquiry(id);
            Inquiry inquiry= estimateInquiryRepository.Find(id);
            inquiry.EstimateId = estimateId;
            return inquiry;
        }

        public void ProgressInquiryToEstimate(long inquiryId)
        {
            Inquiry inquiry = estimateInquiryRepository.Find(inquiryId);
            if (inquiry != null)
            {
                inquiry.Status = 26;
            }
            estimateInquiryRepository.Update(inquiry);
            estimateInquiryRepository.SaveChanges();
            
        }
        #endregion
    }
}
