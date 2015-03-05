using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// ItemImage Repository
    /// </summary>
    public class ItemImageRepository : BaseRepository<ItemImage>, IItemImageRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemImageRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ItemImage> DbSet
        {
            get
            {
                return db.ItemImages;
            }
        }

        #endregion

        #region public
        
        /// <summary>
        /// Find Item Image by Id
        /// </summary>
        public ItemImage Find(int id)
        {
            return base.Find(id);
        }

        #endregion
    }
}
