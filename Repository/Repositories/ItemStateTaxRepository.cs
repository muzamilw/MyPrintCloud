using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Item State Tax Repository
    /// </summary>
    public class ItemStateTaxRepository : BaseRepository<ItemStateTax>, IItemStateTaxRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemStateTaxRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ItemStateTax> DbSet
        {
            get
            {
                return db.ItemStateTaxes;
            }
        }

        #endregion

        #region public
        #endregion
    }
}
