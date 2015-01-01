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
    }
}
