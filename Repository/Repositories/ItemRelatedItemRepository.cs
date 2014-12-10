using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// ItemRelatedItem Repository
    /// </summary>
    public class ItemRelatedItemRepository : BaseRepository<ItemRelatedItem>, IItemRelatedItemRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemRelatedItemRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ItemRelatedItem> DbSet
        {
            get
            {
                return db.ItemRelatedItems;
            }
        }

        #endregion

        #region public
        
        /// <summary>
        /// Find Item Related Item
        /// </summary>
        public ItemRelatedItem Find(int id)
        {
            return DbSet.Find(id);
        }

        #endregion
    }
}
