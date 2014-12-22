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
            return DbSet.ToList(); ;
        }
        public StockCategoryResponse SearchStockCategory(StockCategoryRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            bool isStringSpecified = !string.IsNullOrEmpty(request.SearchString);
            bool isCategoryIdSpecified = request.StockCategoryId != 0;
            Expression<Func<StockCategory, bool>> query =
                s =>
                    (isStringSpecified && (s.Name.Contains(request.SearchString)) ||
                                                                     !isStringSpecified) &&
                                                                     ((isCategoryIdSpecified && s.CategoryId.Equals(request.StockCategoryId)) || !isCategoryIdSpecified);

            int rowCount = DbSet.Count(query);
            IEnumerable<StockCategory> stockCategories = request.IsAsc
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
            return new StockCategoryResponse
                   {
                       RowCount = rowCount,
                       StockCategories = stockCategories
                   };
        }
        #endregion
    }
}
