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
    /// Get Items List View Repository
    /// </summary>
    public class GetItemsListViewRepository : BaseRepository<GetItemsListView>, IGetItemsListViewRepository
    {
        #region privte

        /// <summary>
        /// Item Orderby clause
        /// </summary>
        private readonly Dictionary<ItemByColumn, Func<GetItemsListView, object>> stockItemOrderByClause =
            new Dictionary<ItemByColumn, Func<GetItemsListView, object>>
                    {
                         { ItemByColumn.Name, c => c.ProductName },
                         { ItemByColumn.Code, c => c.ProductCode }
                    };
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public GetItemsListViewRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<GetItemsListView> DbSet
        {
            get
            {
                return db.GetItemsListViews;
            }
        }

        #endregion

        #region public

        /// <summary>
        /// Get All Items for Current Organisation
        /// </summary>
        public override IEnumerable<GetItemsListView> GetAll()
        {
            return DbSet.Where(item => item.OrganisationId == OrganisationId).ToList();
        }

        /// <summary>
        /// Get Items For Specified Search
        /// </summary>
        public ItemListViewSearchResponse GetItems(ItemSearchRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;

            Expression<Func<GetItemsListView, bool>> query =
                item =>
                    ((string.IsNullOrEmpty(request.SearchString) || item.ProductName.Contains(request.SearchString)) &&
                    (!request.CompanyId.HasValue || item.CompanyId == request.CompanyId) &&
                    item.OrganisationId == OrganisationId && !item.IsArchived.HasValue);

            IEnumerable<GetItemsListView> items = request.IsAsc
               ? DbSet.Where(query)
                   .OrderBy(stockItemOrderByClause[request.ItemOrderBy])
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList()
               : DbSet.Where(query)
                   .OrderByDescending(stockItemOrderByClause[request.ItemOrderBy])
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList();

            return new ItemListViewSearchResponse { Items = items, TotalCount = DbSet.Count(query) };
        }
        /// <summary>
        /// Get Items For Company/Store
        /// </summary>
        public ItemListViewSearchResponse GetItemsForCompany(CompanyProductSearchRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;

            Expression<Func<GetItemsListView, bool>> query =
                item =>
                    ((string.IsNullOrEmpty(request.SearchString) || item.ProductName.Contains(request.SearchString)) &&
                    (item.CompanyId == request.CompanyId) &&
                    item.OrganisationId == OrganisationId);

            IEnumerable<GetItemsListView> items = request.IsAsc
               ? DbSet.Where(query)
                   .OrderBy(stockItemOrderByClause[request.ItemOrderBy])
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList()
               : DbSet.Where(query)
                   .OrderByDescending(stockItemOrderByClause[request.ItemOrderBy])
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList();

            return new ItemListViewSearchResponse { Items = items, TotalCount = DbSet.Count(query) };
        }

        #endregion
    }
}
