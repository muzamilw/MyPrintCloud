using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.ModelMappers;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;


namespace MPC.Implementation.MISServices
{
    public class PurchaseService : IPurchaseService
    {
        #region Private

        private readonly IPurchaseRepository purchaseRepository;
        private readonly IPurchaseDetailRepository purchaseDetailRepository;
        private readonly ISectionFlagRepository sectionFlagRepository;
        private readonly ISystemUserRepository systemUserRepository;
        private readonly IOrganisationRepository organisationRepository;
        private readonly IPrefixRepository prefixRepository;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public PurchaseService(IPurchaseRepository purchaseRepository, ISectionFlagRepository sectionFlagRepository, ISystemUserRepository systemUserRepository,
            IOrganisationRepository organisationRepository, IPrefixRepository prefixRepository, IPurchaseDetailRepository purchaseDetailRepository)
        {
            this.purchaseRepository = purchaseRepository;
            this.sectionFlagRepository = sectionFlagRepository;
            this.systemUserRepository = systemUserRepository;
            this.organisationRepository = organisationRepository;
            this.prefixRepository = prefixRepository;
            this.purchaseDetailRepository = purchaseDetailRepository;
        }

        #endregion

        #region Public
        public PurchaseResponseModel GetPurchases(PurchaseRequestModel requestModel)
        {
            return purchaseRepository.GetPurchases(requestModel);
        }

        public PurchaseResponseModel GetPurchaseOrders(PurchaseOrderSearchRequestModel request)
        {
            return purchaseRepository.GetPurchaseOrders(request);
        }

        /// <summary>
        /// base Data for Purchase
        /// </summary>
        public PurchaseBaseResponse GetBaseData()
        {
            Organisation organisation = organisationRepository.GetOrganizatiobByID();
            return new PurchaseBaseResponse
            {
                SectionFlags = sectionFlagRepository.GetSectionFlagBySectionId((int)SectionEnum.Order),
                SystemUsers = systemUserRepository.GetAll(),
                CurrencySymbol = organisation != null ? (organisation.Currency != null ? organisation.Currency.CurrencySymbol : string.Empty) : string.Empty
            };
        }

        /// <summary>
        /// Save Purchase
        /// </summary>
        public Purchase SavePurchase(Purchase purchase)
        {
            // Get Purchase if exists else create new
            Purchase purchaseTarget = GetById(purchase.PurchaseId) ?? CreatePurchase();
            // Update Order
            purchase.UpdateTo(purchaseTarget, new PurchaseMapperActions
            {
                CreatePurchaseDetail = CreatePurchaseDetail,
                DeletePurchaseDetail = DeletePurchaseDetail,
            });

            // Save Changes
            purchaseRepository.SaveChanges();
            return GetById(purchaseTarget.PurchaseId);
        }

        /// <summary>
        /// 
        /// </summary>
        public Purchase GetPurchaseById(int purchaseId)
        {
            return purchaseRepository.Find(purchaseId);
        }

        /// <summary>
        /// Creates New Purchase Detail
        /// </summary>
        private PurchaseDetail CreatePurchaseDetail()
        {
            PurchaseDetail itemTarget = purchaseDetailRepository.Create();
            purchaseDetailRepository.Add(itemTarget);
            return itemTarget;
        }

        /// <summary>
        /// Delete Purchase Detail
        /// </summary>
        private void DeletePurchaseDetail(PurchaseDetail purchaseDetail)
        {
            purchaseDetailRepository.Delete(purchaseDetail);
        }
        /// <summary>
        /// Get By Id
        /// </summary>
        public Purchase GetById(long purchaseId)
        {
            return purchaseRepository.Find(purchaseId);
        }

        /// <summary>
        /// Creates New Purchse and assigns new generated code
        /// </summary>
        private Purchase CreatePurchase()
        {
            string code = prefixRepository.GetNextPurchaseCodePrefix();
            Purchase itemTarget = purchaseRepository.Create();
            purchaseRepository.Add(itemTarget);
            itemTarget.Code = code;
            return itemTarget;
        }

        /// <summary>
        /// Delete Purchase Order
        /// </summary>
        public void DeletePurchaseOrder(int purchaseId)
        {
            purchaseRepository.Delete(purchaseRepository.Find(purchaseId));
            purchaseRepository.SaveChanges();
        }


        //public bool GeneratePO(long OrderID, string ServerPath,int ContactID,int ContactCompanyID)
        //{
        //    ObjectContext.usp_GeneratePurchaseOrders(OrderID, CreatedBy, TaxID);
        //    POEmail(ServerPath, OrderID, ContactID, ContactCompanyID);
        //    return true;
        //}
        #endregion
    }
}
