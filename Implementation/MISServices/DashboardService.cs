using System.Collections.Generic;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

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
        public OrderStatusesResponse GetOrderStatusesCount(DashboardRequestModel request)
        {
            OrderStatusesResponse response = estimateRepository.GetOrderStatusesCount();
            response.LiveStoresCount = companyRepository.LiveStoresCountForDashboard();
            response.Estimates = estimateRepository.GetEstimatesForDashboard(request);
            return response;
        }

        /// <summary>
        /// Get Total Earnings For Dashboard
        /// </summary>
        public IEnumerable<usp_TotalEarnings_Result> GetTotalEarningsForDashboard()
        {
            return estimateRepository.GetTotalEarningsForDashboard();
        }
        #endregion

    }
}
