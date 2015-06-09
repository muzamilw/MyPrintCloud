using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;


namespace MPC.Implementation.MISServices
{
    public class PurchaseService : IPurchaseService
    {
        #region Private

        private readonly IPurchaseRepository purchaseRepository;
        private readonly ISectionFlagRepository sectionFlagRepository;
        private readonly ISystemUserRepository systemUserRepository;

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public PurchaseService(IPurchaseRepository purchaseRepository, ISectionFlagRepository sectionFlagRepository, ISystemUserRepository systemUserRepository)
        {
            this.purchaseRepository = purchaseRepository;
            this.sectionFlagRepository = sectionFlagRepository;
            this.systemUserRepository = systemUserRepository;
        }

        #endregion
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
            return new PurchaseBaseResponse
            {
                SectionFlags = sectionFlagRepository.GetSectionFlagBySectionId((int)SectionEnum.Order),
                SystemUsers = systemUserRepository.GetAll(),
            };
        }
    }
}
