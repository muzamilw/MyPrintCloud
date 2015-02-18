using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.MIS.Areas.Api.Models;

namespace MPC.Implementation.MISServices
{
    /// <summary>
    /// Dashboard Service 
    /// </summary>
    public class DashboardService : IDashboardService
    {
        #region Private

        private readonly IEstimateRepository estimateRepository;
        private readonly ICompanyRepository companyRepository;
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DashboardService(IEstimateRepository estimateRepository, ICompanyRepository companyRepository)
        {
            this.estimateRepository = estimateRepository;
            this.companyRepository = companyRepository;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Order Statuses Response
        /// </summary>
        public OrderStatusesResponse GetOrderStatusesCount()
        {
            OrderStatusesResponse response = estimateRepository.GetOrderStatusesCount();
            response.LiveStoresCount = companyRepository.LiveStoresCountForDashboard();
            return response;
        }

        #endregion
    }
}
