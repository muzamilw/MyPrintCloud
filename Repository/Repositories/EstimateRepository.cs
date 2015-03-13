using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.MIS.Areas.Api.Models;
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
                         { OrderByColumn.CreationDate, c => c.CreationDate },
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
        /// Get Orders For Specified Search
        /// </summary>
        public GetOrdersResponse GetOrders(GetOrdersRequest request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            bool isStatusSpecified = request.Status == 0;//if true get all then get by status
            Expression<Func<Estimate, bool>> query =
                item =>
                    ((string.IsNullOrEmpty(request.SearchString) || (item.Company != null && item.Company.Name.Contains(request.SearchString))) &&
                    (item.isEstimate.HasValue && !item.isEstimate.Value) && ((!isStatusSpecified && item.StatusId == request.Status || isStatusSpecified)) &&
                    item.OrganisationId == OrganisationId);

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

            return new GetOrdersResponse { Orders = items, TotalCount = DbSet.Count(query) };
        }

        /// <summary>
        /// Gives count of new orders by given number of last dats
        /// </summary>
        public int GetNewOrdersCount(int noOfLastDays, long companyId)
        {
           DateTime currenteDate  = DateTime.UtcNow.Date.AddDays(-noOfLastDays);
          return  DbSet.Count(estimate => estimate.isEstimate == false && companyId==estimate.CompanyId  && estimate.CreationDate >= currenteDate);
        }


        /// <summary>
        /// Get Order Statuses Response
        /// </summary>
        public OrderStatusesResponse GetOrderStatusesCount()
        {
            return new OrderStatusesResponse
            {
                PendingOrdersCount = DbSet.Count(order =>order.OrganisationId==OrganisationId && order.StatusId == (short)OrderStatusEnum.PendingOrder && order.isEstimate == false),
                InProductionOrdersCount = DbSet.Count(order => order.OrganisationId == OrganisationId && order.StatusId == (short)OrderStatusEnum.InProduction && order.isEstimate == false),
                CompletedOrdersCount = DbSet.Count(order => order.OrganisationId == OrganisationId && order.StatusId == (short)OrderStatusEnum.CompletedOrders && order.isEstimate == false),
                UnConfirmedOrdersCount = DbSet.Count(estimate => estimate.OrganisationId == OrganisationId && estimate.isEstimate == true),
                TotalEarnings = DbSet.Where(order => order.OrganisationId == OrganisationId).Sum(estimate =>estimate.Estimate_Total ),
                CurrentMonthOdersCount = DbSet.Count(order => order.OrganisationId == OrganisationId && order.Order_Date.HasValue && 
                    order.isEstimate == false && DateTime.Now.Month == order.Order_Date.Value.Month)
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
                    item.CompanyId == request.CompanyId;
        

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

        #endregion
    }
}
