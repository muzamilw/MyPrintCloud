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

        public bool isVoucherAppliedOnThisProductCategory(long VoucherId, long ItemId)
        {
            List<long?> productCategoryIds = db.ProductCategoryItems.Where(v => v.ItemId == ItemId).Select(c => c.CategoryId).ToList();
            if (productCategoryIds != null && productCategoryIds.Count() > 0)
            {
                List<ProductCategoryVoucher> catVouchers = db.ProductCategoryVouchers.Where(v => productCategoryIds.Contains(v.ProductCategoryId) && v.VoucherId == VoucherId).ToList();
                if (catVouchers != null && catVouchers.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public List<long?> GetItemIdsListByCategoryId(long VoucherId, List<int?> ItemIds, List<long?> filteredItemIds)
        {
            if (filteredItemIds == null)
            {
                filteredItemIds = new List<long?>();
            }
            
            foreach (int? i in ItemIds)
            {
                List<long?> productCategoryIds = db.ProductCategoryItems.Where(v => v.ItemId == (int)i).Select(c => c.CategoryId).ToList();
                if (productCategoryIds != null && productCategoryIds.Count() > 0)
                {
                    List<ProductCategoryVoucher> catVouchers = db.ProductCategoryVouchers.Where(v => productCategoryIds.Contains(v.ProductCategoryId) && v.VoucherId == VoucherId).ToList();
                    if (catVouchers != null && catVouchers.Count() > 0)
                    {
                        if (!filteredItemIds.Contains(i))
                        {
                            filteredItemIds.Add((long)i);
                        }
                    }
                }
            }
            return filteredItemIds;
        }
        #endregion
    }
}
