using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Item Price Matrix Repository
    /// </summary>
    public class ItemPriceMatrixRepository : BaseRepository<ItemPriceMatrix>, IItemPriceMatrixRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemPriceMatrixRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ItemPriceMatrix> DbSet
        {
            get
            {
                return db.ItemPriceMatrices;
            }
        }

        #endregion

        #region public
        #endregion

        /// <summary>
        /// Get For Item By Section Flag
        /// </summary>
        public IEnumerable<ItemPriceMatrix> GetForItemBySectionFlag(long sectionFlagId, long itemId)
        {
            return DbSet.Where(itemPrice => itemPrice.FlagId == sectionFlagId && itemPrice.ItemId == itemId && itemPrice.SupplierId == null);
        }

        //public  List<ItemPriceMatrix> GetRetailProductsPriceMatrix() // Customer ID , Broker Product List
        //{
            
        //    {
        //       // db.Configuration.LazyLoadingEnabled = false;

        //        var qry = from prices in db.ItemPriceMatrices
        //                  join i in db.Items on prices.ItemId equals i.ItemId
        //                  join cat in db.ProductCategories on i.productc equals cat.ProductCategoryId
        //                  where cat.isArchived == false && i.IsPublished == true && prices.SupplierID == null && prices.ContactCompanyID == null && ((i.isQtyRanged == true && prices.QtyRangeFrom > 0) || (i.isQtyRanged == false && prices.Quantity > 0))

        //                  select prices;

        //        return qry.ToList();

        //    }
        //}
    }
}
