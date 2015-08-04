using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Repository.Repositories
{
    public class ProductCategoryVoucherRepository : BaseRepository<ProductCategoryVoucher>, IProductCategoryVoucherRepository
    {
         #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ProductCategoryVoucherRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ProductCategoryVoucher> DbSet
        {
            get
            {
                return db.ProductCategoryVouchers;
            }
        }

        #endregion
    }
}
