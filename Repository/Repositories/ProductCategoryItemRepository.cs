using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Product Category Item Repository
    /// </summary>
    public class ProductCategoryItemRepository : BaseRepository<ProductCategoryItem>, IProductCategoryItemRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ProductCategoryItemRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ProductCategoryItem> DbSet
        {
            get
            {
                return db.ProductCategoryItems;
            }
        }

        #endregion

        #region public
        
        #endregion
    }
}
