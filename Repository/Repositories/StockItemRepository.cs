using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Stock Item Repository
    /// </summary>
    public class StockItemRepository : BaseRepository<StockItem>, IStockItemRepository
    {
        #region privte
        /// <summary>
        /// Company Orderby clause
        /// </summary>
        private readonly Dictionary<InventoryByColumn, Func<StockItem, object>> stockItemOrderByClause = new Dictionary<InventoryByColumn, Func<StockItem, object>>
                    {
                         {InventoryByColumn.Name, c => c.ItemName}
                    };
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public StockItemRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<StockItem> DbSet
        {
            get
            {
                return db.StockItems;
            }
        }

        #endregion

        #region public
        /// <summary>
        /// Get All StockI tem User Domain Key
        /// </summary>
        public override IEnumerable<StockItem> GetAll()
        {
            return DbSet.ToList();
        }

        /// <summary>
        /// Search Company
        /// </summary>
        public InventorySearchResponse GetStockItems(InventorySearchRequestModel request)
        {
            int fromRow = (request.PageNo) * request.PageSize;
            int toRow = request.PageSize;
            Expression<Func<StockItem, bool>> query =
                stockItem =>
                    (string.IsNullOrEmpty(request.SearchString) || (stockItem.ItemName.Contains(request.SearchString)) ||
                     (stockItem.AlternateName.Contains(request.SearchString))) && (
                         (!request.CategoryId.HasValue || request.CategoryId == stockItem.CategoryId) &&
                         (!request.SubCategoryId.HasValue || request.SubCategoryId == stockItem.SubCategoryId)) && stockItem.OrganisationId == OrganisationId;

            IEnumerable<StockItem> stockItems = request.IsAsc
               ? DbSet.Where(query)
                   .OrderBy(stockItemOrderByClause[request.InventoryOrderBy])
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList()
               : DbSet.Where(query)
                   .OrderByDescending(stockItemOrderByClause[request.InventoryOrderBy])
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList();
            return new InventorySearchResponse { StockItems = stockItems, TotalCount = DbSet.Count(query) };
        }

        /// <summary>
        /// Get Items For Product
        /// </summary>
        public InventorySearchResponse GetStockItemsForProduct(StockItemRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            Expression<Func<StockItem, bool>> query =
                stockItem =>
                    (string.IsNullOrEmpty(request.SearchString) || stockItem.ItemName.Contains(request.SearchString)) && 
                    stockItem.OrganisationId == OrganisationId;

            IEnumerable<StockItem> stockItems = request.IsAsc
               ? DbSet.Where(query)
                   .OrderBy(stockItemOrderByClause[request.StockItemOrderBy])
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList()
               : DbSet.Where(query)
                   .OrderByDescending(stockItemOrderByClause[request.StockItemOrderBy])
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList();

            return new InventorySearchResponse { StockItems = stockItems, TotalCount = DbSet.Count(query) };
        }

        #endregion
    }
}
