using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// ItemA Repository
    /// </summary>
    public class ItemAddonCostCentreRepository : BaseRepository<ItemAddonCostCentre>, IItemAddOnCostCentreRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemAddonCostCentreRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ItemAddonCostCentre> DbSet
        {
            get
            {
                return db.ItemAddonCostCentres;
            }
        }

        #endregion

        #region public
        #endregion
    }
}
