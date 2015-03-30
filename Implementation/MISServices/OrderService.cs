using System;
using System.Collections.Generic;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.MISServices
{
    /// <summary>
    /// Order Service
    /// </summary>
    public class OrderService : IOrderService
    {
        #region Private
        private readonly IStockCategoryRepository stockCategoryRepository;
        private readonly IEstimateRepository estimateRepository;
        private readonly ISectionFlagRepository sectionFlagRepository;
        private readonly ICompanyContactRepository companyContactRepository;
        private readonly IAddressRepository addressRepository;
        private readonly ISystemUserRepository systemUserRepository;
        private readonly IPipeLineSourceRepository pipeLineSourceRepository;
        private readonly IMarkupRepository _markupRepository;
        private readonly IPaymentMethodRepository paymentMethodRepository;
        private readonly IOrganisationRepository organisationRepository;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public OrderService(IEstimateRepository estimateRepository, ISectionFlagRepository sectionFlagRepository, ICompanyContactRepository companyContactRepository,
            IAddressRepository addressRepository, ISystemUserRepository systemUserRepository, IPipeLineSourceRepository pipeLineSourceRepository, IMarkupRepository markupRepository,
            IPaymentMethodRepository paymentMethodRepository, IOrganisationRepository organisationRepository, IStockCategoryRepository stockCategoryRepository)
        {
            if (estimateRepository == null)
            {
                throw new ArgumentNullException("estimateRepository");
            }
            if (companyContactRepository == null)
            {
                throw new ArgumentNullException("companyContactRepository");
            }
            if (addressRepository == null)
            {
                throw new ArgumentNullException("addressRepository");
            }
            if (systemUserRepository == null)
            {
                throw new ArgumentNullException("systemUserRepository");
            }
            if (pipeLineSourceRepository == null)
            {
                throw new ArgumentNullException("pipeLineSourceRepository");
            }
            if (paymentMethodRepository == null)
            {
                throw new ArgumentNullException("paymentMethodRepository");
            }
            if (organisationRepository == null)
            {
                throw new ArgumentNullException("organisationRepository");
            }
            
            this.estimateRepository = estimateRepository;
            this.sectionFlagRepository = sectionFlagRepository;
            this.companyContactRepository = companyContactRepository;
            this.addressRepository = addressRepository;
            this.systemUserRepository = systemUserRepository;
            this.pipeLineSourceRepository = pipeLineSourceRepository;
            _markupRepository = markupRepository;
            this.paymentMethodRepository = paymentMethodRepository;
            this.organisationRepository = organisationRepository;
            this.stockCategoryRepository = stockCategoryRepository;
        }

        #endregion
        #region Public
        
        /// <summary>
        /// Get All Orders
        /// </summary>
        public GetOrdersResponse GetAll(GetOrdersRequest request)
        {
            return estimateRepository.GetOrders(request);
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        public Estimate GetById(long orderId)
        {
            return estimateRepository.Find(orderId);
        }

        /// <summary>
        /// Delete Order
        /// </summary>
        public void DeleteOrder(long orderId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Save Order
        /// </summary>
        public Estimate SaveOrder(Estimate estimate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get base data for order
        /// </summary>
        public OrderBaseResponse GetBaseData()
        {
            IEnumerable<StockCategory> stocks = stockCategoryRepository.GetAll();
            return new OrderBaseResponse
                   {
                       SectionFlags = sectionFlagRepository.GetSectionFlagBySectionId((int)SectionEnum.Order),
                       SystemUsers = systemUserRepository.GetAll(),
                       PipeLineSources = pipeLineSourceRepository.GetAll(),
                       PaymentMethods = paymentMethodRepository.GetAll(),
                       Markups= _markupRepository.GetAll(),
                       Organisation = organisationRepository.Find(organisationRepository.OrganisationId),
                       StockCategories = stocks
                   };
        }

        /// <summary>
        /// Get Base Data For Company
        /// </summary>
        public OrderBaseResponseForCompany GetBaseDataForCompany(long companyId)
        {
            return new OrderBaseResponseForCompany
                {
                    CompanyContacts = companyContactRepository.GetCompanyContactsByCompanyId(companyId),
                    CompanyAddresses = addressRepository.GetAddressByCompanyID(companyId)
                };
        }
        /// <summary>
        /// Get Order Statuses Count For Menu Items
        /// </summary>
        /// <returns></returns>
        public OrderMenuCount GetOrderScreenMenuItemCount()
        {
            return estimateRepository.GetOrderStatusesCountForMenuItems();
        }

        #endregion
    }
}
