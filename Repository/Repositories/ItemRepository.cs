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
    /// Item Repository
    /// </summary>
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        #region privte

        /// <summary>
        /// Item Orderby clause
        /// </summary>
        private readonly Dictionary<ItemByColumn, Func<Item, object>> stockItemOrderByClause = 
            new Dictionary<ItemByColumn, Func<Item, object>>
                    {
                         { ItemByColumn.Name, c => c.ProductName },
                         { ItemByColumn.Code, c => c.ProductCode }
                    };
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Item> DbSet
        {
            get
            {
                return db.Items;
            }
        }

        #endregion

        #region public

        /// <summary>
        /// Get All Items for Current Organisation
        /// </summary>
        public override IEnumerable<Item> GetAll()
        {
            return DbSet.Where(item => item.OrganisationId == OrganisationId).ToList();
        }

        /// <summary>
        /// Get Items For Specified Search
        /// </summary>
        public ItemSearchResponse GetItems(ItemSearchRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;

            Expression<Func<Item, bool>> query =
                item =>
                    ((string.IsNullOrEmpty(request.SearchString) || item.ProductName.Contains(request.SearchString)) &&
                    item.OrganisationId == OrganisationId);

            IEnumerable<Item> items = request.IsAsc
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

            return new ItemSearchResponse { Items = items, TotalCount = DbSet.Count(query) };
        }

        #endregion
    }
}
