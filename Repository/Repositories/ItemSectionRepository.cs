using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Item Section Repository
    /// </summary>
    public class ItemSectionRepository : BaseRepository<ItemSection>, IItemSectionRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemSectionRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ItemSection> DbSet
        {
            get
            {
                return db.ItemSections;
            }
        }

        #endregion

        #region public

        #endregion

        
    }
}
