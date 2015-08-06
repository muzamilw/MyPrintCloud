using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Estimate Repository
    /// </summary>
    public class EstimateRepository : BaseRepository<Estimate>, IEstimateRepository
    {
        #region privte

        /// <summary>
        /// Item Orderby clause
        /// </summary>
        private readonly Dictionary<OrderByColumn, Func<Estimate, object>> orderByClause =
            new Dictionary<OrderByColumn, Func<Estimate, object>>
                    {
                         { OrderByColumn.CompanyName, c => c.Company != null ? c.Company.Name : string.Empty },
                         { OrderByColumn.CreationDate, c => c.Order_Date },
                         { OrderByColumn.SectionFlag, c => c.SectionFlagId },
                         { OrderByColumn.OrderCode, c => c.Order_Code }
                    };
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public EstimateRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Estimate> DbSet
        {
            get
            {
                return db.Estimates;
            }
        }

        #endregion

        #region public

        /// <summary>
        /// Get All Items for Current Organisation
        /// </summary>
        public override IEnumerable<Estimate> GetAll()
        {
            return DbSet.Where(item => item.OrganisationId == OrganisationId).ToList();
        }

        /// <summary>
        /// Get Total Earnings for a specific duration
        /// </summary>
        public IEnumerable<usp_TotalEarnings_Result> GetTotalEarnings(DateTime fromDate, DateTime toDate)
        {
            
            return db.usp_TotalEarnings(fromDate, toDate, OrganisationId).ToList();
        }

        /// <summary>
        /// Get Orders For Specified Search
        /// </summary>
        public GetOrdersResponse GetOrders(GetOrdersRequest request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            bool isStatusSpecified = request.Status == 0;//if true get all then get by status
            bool filterFlagSpecified = request.FilterFlag == 0;
            //Order Type Filter , 2-> all, 0 -> Direct  Order, 1 -> Online Order
            bool orderTypeFilterSpecified = request.OrderTypeFilter == 2;
            Expression<Func<Estimate, bool>> query =
                item =>
                    ((
                    string.IsNullOrEmpty(request.SearchString) ||
                    ((item.Company != null && item.Company.Name.Contains(request.SearchString)) || (item.Order_Code.Contains(request.SearchString)) ||
                    (item.Estimate_Name.Contains(request.SearchString)) || (item.Items.Any(product => product.ProductName.Contains(request.SearchString)))
                    )) &&
                    (item.isEstimate.HasValue && !item.isEstimate.Value) && ((!isStatusSpecified && item.StatusId == request.Status || isStatusSpecified)) &&
                    ((!filterFlagSpecified && item.SectionFlagId == request.FilterFlag || filterFlagSpecified)) &&
                    ((!orderTypeFilterSpecified && item.isDirectSale == (request.OrderTypeFilter == 0) || orderTypeFilterSpecified)) &&
                    item.OrganisationId == OrganisationId &&
                    (item.StatusId != (int)OrderStatus.ShoppingCart && item.StatusId != (int)OrderStatus.PendingCorporateApprovel));

            IEnumerable<Estimate> items = DbSet.Where(query)
                   .OrderByDescending(orderByClause[OrderByColumn.CreationDate])
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList();

            return new GetOrdersResponse { Orders = items, TotalCount = DbSet.Count(query) };
        }

        /// <summary>
        /// Get Orders For Estimates List View
        /// </summary>
        public GetOrdersResponse GetOrdersForEstimates(GetOrdersRequest request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            bool isStatusSpecified = request.Status == 0;//if true get all then get by status
            bool filterFlagSpecified = request.FilterFlag == 0;
            //Order Type Filter , 2-> all, 0 -> Direct  Order, 1 -> Online Order
            bool orderTypeFilterSpecified = request.OrderTypeFilter == 2;
            Expression<Func<Estimate, bool>> query =
                item =>
                    ((string.IsNullOrEmpty(request.SearchString) || (item.Company != null && item.Company.Name.Contains(request.SearchString))) &&
                    (item.isEstimate.HasValue && item.isEstimate.Value) && ((!isStatusSpecified && item.StatusId == request.Status || isStatusSpecified)) &&
                    ((!filterFlagSpecified && item.SectionFlagId == request.FilterFlag || filterFlagSpecified)) &&
                    item.OrganisationId == OrganisationId);

            IEnumerable<Estimate> items = DbSet.Where(query)
                   .OrderByDescending(orderByClause[OrderByColumn.CreationDate])
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList();

            return new GetOrdersResponse { Orders = items, TotalCount = DbSet.Count(query) };
        }

        /// <summary>
        /// Gives count of new orders by given number of last dats
        /// </summary>
        public int GetNewOrdersCount(int noOfLastDays, long companyId)
        {
           
            DateTime currenteDate = DateTime.UtcNow.Date.AddDays(-noOfLastDays);
            return DbSet.Count(estimate => estimate.isEstimate == false && companyId == estimate.CompanyId && estimate.CreationDate >= currenteDate);
        }


        /// <summary>
        /// Get Order Statuses Response
        /// </summary>
        public OrderStatusesResponse GetOrderStatusesCount()
        {
            return new OrderStatusesResponse
            {
                PendingOrdersCount = DbSet.Count(order => order.OrganisationId == OrganisationId && order.StatusId == (short)OrderStatusEnum.PendingOrder && order.isEstimate == false),
                InProductionOrdersCount = DbSet.Count(order => order.OrganisationId == OrganisationId && order.StatusId == (short)OrderStatusEnum.InProduction && order.isEstimate == false),
                CompletedOrdersCount = DbSet.Count(order => order.OrganisationId == OrganisationId && order.StatusId == (short)OrderStatusEnum.Completed_NotShipped && order.isEstimate == false),
                UnConfirmedOrdersCount = DbSet.Count(estimate => estimate.OrganisationId == OrganisationId && estimate.isEstimate == true),
                TotalEarnings = DbSet.Where(order => order.OrganisationId == OrganisationId).Sum(estimate => estimate.Estimate_Total),
                CurrentMonthOdersCount = DbSet.Count(order => order.OrganisationId == OrganisationId && order.Order_Date.HasValue &&
                (order.isEstimate.Value == false || !order.isEstimate.HasValue) &&
                (order.StatusId != (int)OrderStatus.ShoppingCart && order.StatusId != (int)OrderStatus.PendingCorporateApprovel) &&
                    DateTime.Now.Month == order.Order_Date.Value.Month)
            };
        }

        /// <summary>
        ///Get Order Statuses Count For Menu Items
        /// </summary>
        public OrderMenuCount GetOrderStatusesCountForMenuItems()
        {
            return new OrderMenuCount
            {
                AllOrdersCount = DbSet.Count(order => order.OrganisationId == OrganisationId && order.isEstimate == false),
                PendingOrders = DbSet.Count(order => order.OrganisationId == OrganisationId && order.StatusId == (short)OrderStatusEnum.PendingOrder && order.isEstimate == false),
                ConfirmedStarts = DbSet.Count(order => order.OrganisationId == OrganisationId && order.StatusId == (short)OrderStatusEnum.ConfirmedOrder && order.isEstimate == false),
                InProduction = DbSet.Count(order => order.OrganisationId == OrganisationId && order.StatusId == (short)OrderStatusEnum.InProduction && order.isEstimate == false),
                ReadyForShipping = DbSet.Count(order => order.OrganisationId == OrganisationId && order.StatusId == (short)OrderStatusEnum.Completed_NotShipped && order.isEstimate == false),
                Invoiced = DbSet.Count(order => order.OrganisationId == OrganisationId && order.StatusId == (short)OrderStatusEnum.CompletedAndShipped_Invoiced && order.isEstimate == false),
                CancelledOrders = DbSet.Count(order => order.OrganisationId == OrganisationId && order.StatusId == (short)OrderStatusEnum.CancelledOrder && order.isEstimate == false),
            };
        }


        /// <summary>
        /// Gets list of Orders for company edit tab
        /// </summary>
        public OrdersForCrmResponse GetOrdersForCrm(GetOrdersRequest request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;

            Expression<Func<Estimate, bool>> query =
                item =>
                    (item.isEstimate.HasValue && !item.isEstimate.Value) &&
                    item.CompanyId == request.CompanyId && item.StatusId != (int)OrderStatus.ShoppingCart;


            IEnumerable<Estimate> items = request.IsAsc
               ? DbSet.Where(query)
                   .OrderBy(orderByClause[request.ItemOrderBy])
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList()
               : DbSet.Where(query)
                   .OrderByDescending(orderByClause[request.ItemOrderBy])
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList();
            return new OrdersForCrmResponse
            {
                RowCount = DbSet.Count(query),
                Orders = items
            };
        }

        public IEnumerable<Estimate> GetEstimatesForDashboard(DashboardRequestModel request)
        {
            Expression<Func<Estimate, bool>> query =
                item =>
                    (string.IsNullOrEmpty(request.SearchString) ||
                    (item.Company != null && item.Company.Name.Contains(request.SearchString)) ||
                    (item.Order_Code.Contains(request.SearchString)))
                    &&
                    (item.isEstimate.HasValue && !item.isEstimate.Value) &&
                    (item.StatusId != (int)OrderStatus.ShoppingCart && item.StatusId != (int)OrderStatus.PendingCorporateApprovel) &&
                    item.OrganisationId == OrganisationId;

            IEnumerable<Estimate> items = DbSet.Where(query).OrderByDescending(x => x.EstimateId).Take(5).ToList()
                .ToList();

            return items;
        }

        public Estimate GetEstimateWithCompanyByOrderID(long OrderID)
        {
            try
            {
                return db.Estimates.Include("Company").Where(g => g.EstimateId == OrderID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long GetEstimateIdOfInquiry(long inquiryId)
        {
            var firstOrDefault = DbSet.FirstOrDefault(x => x.EnquiryId == inquiryId);
            if (firstOrDefault != null)
                return firstOrDefault.EstimateId;
            return 0;
        }

        /// <summary>
        /// Get Total Earnings For Dashboard
        /// </summary>
        public IEnumerable<usp_TotalEarnings_Result> GetTotalEarningsForDashboard()
        {
            try
            {
                var now = DateTime.Now;
                return db.usp_TotalEarnings(new DateTime(now.Year, 01, 01), new DateTime(now.Year, 12, 31), OrganisationId);



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DashBoardChartsResponse GetChartsForDashboard()
        {
            var now = DateTime.Now;
            return new DashBoardChartsResponse
            {
                 
                TotalEarningResult = db.usp_TotalEarnings(new DateTime(now.Year, 01, 01), new DateTime(now.Year, 12, 31), OrganisationId),
                RegisteredUserByStores = db.usp_ChartRegisteredUserByStores(OrganisationId),
                TopPerformingStores = db.usp_ChartTopPerformingStores(OrganisationId),
                MonthlyOrdersCount = db.usp_ChartMonthlyOrdersCount(OrganisationId),
                EstimateToOrderConversion = db.usp_ChartEstimateToOrderConversion(OrganisationId),
                EstimateToOrderConversionCount = db.usp_ChartEstimateToOrderConversionCount(OrganisationId),
                Top10PerformingCustomers = db.usp_ChartTop10PerfomingCustomers(OrganisationId)
            
               
            };
        }


        #endregion
    }
}
