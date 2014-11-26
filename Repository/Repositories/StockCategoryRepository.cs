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
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    public class StockCategoryRepository : BaseRepository<StockCategory>, IStockCategoryRepository
    {
        #region Private
        private readonly Dictionary<StockCategoryByColumn, Func<StockCategory, object>> stockCategoryOrderByClause = new Dictionary<StockCategoryByColumn, Func<StockCategory, object>>
                    {
                        {StockCategoryByColumn.Code, d => d.Code},
                        {StockCategoryByColumn.Name, c => c.Name},
                        {StockCategoryByColumn.Description, d => d.Description}
                    };
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public StockCategoryRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<StockCategory> DbSet
        {
            get
            {
                return db.StockCategories;
            }
        }

        #endregion
        #region Public

        /// <summary>
        /// Get All Stock Category
        /// </summary>
        public override IEnumerable<StockCategory> GetAll()
        {
            return DbSet.ToList();
        }
        public IEnumerable<StockCategory> SearchStockCategory(StockCategoryRequestModel request, out int rowCount)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            Expression<Func<StockCategory, bool>> query =
                paperSize =>
                    (string.IsNullOrEmpty(request.SearchString));

            rowCount = DbSet.Count(query);
            return request.IsAsc
                ? DbSet.Where(query)
                    .OrderBy(stockCategoryOrderByClause[request.StockCategoryOrderBy])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet.Where(query)
                    .OrderByDescending(stockCategoryOrderByClause[request.StockCategoryOrderBy])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();
        }
        #endregion
    }
}
