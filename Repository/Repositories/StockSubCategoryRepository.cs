using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    public class StockSubCategoryRepository : BaseRepository<StockSubCategory>, IStockSubCategoryRepository
    {
        #region Constructor

        public StockSubCategoryRepository(IUnityContainer container)
            : base(container)
        {
        }

        protected override IDbSet<StockSubCategory> DbSet
        {
            get
            {
                return db.StockSubCategories;
            }
        }
        #endregion

        /// <summary>
        /// Get All Stock Sub Category
        /// </summary>
        public override IEnumerable<StockSubCategory> GetAll()
        {
            return DbSet.ToList();
        }

         /// <summary>
        /// Get list of Stock Sub Category for Stock Category Ids
        /// </summary>
        public IEnumerable<StockSubCategory> GetStockSubCategoriesByStockCategoryIds(List<long> categoryIds)
        {
            return DbSet.Where(subCategory => categoryIds.Contains(subCategory.CategoryId)).ToList();
        }
    }
}
