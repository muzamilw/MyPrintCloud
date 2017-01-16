using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;
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
        public long? GetCategoryId(long ItemId)
        {

            return db.ProductCategoryItems.Where(i => i.ItemId == ItemId).Select(c => c.CategoryId).FirstOrDefault();


        }

        
        #endregion
    }
}
