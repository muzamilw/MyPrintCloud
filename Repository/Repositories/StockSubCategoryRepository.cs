using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    public class StockSubCategoryRepository : BaseRepository<StockSubCategory>, IStockSubCategoryRepository
    {
        #region Constructor

        public StockSubCategoryRepository(IUnityContainer container) : base(container)
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
    }
}
