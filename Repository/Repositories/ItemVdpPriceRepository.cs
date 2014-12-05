using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// ItemVdpPrice Repository
    /// </summary>
    public class ItemVdpPriceRepository : BaseRepository<ItemVdpPrice>, IItemVdpPriceRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemVdpPriceRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ItemVdpPrice> DbSet
        {
            get
            {
                return db.ItemVdpPrices;
            }
        }

        #endregion

        #region public
        #endregion
    }
}
