using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System.Collections.Generic;
using System.Linq;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// ItemVideo Repository
    /// </summary>
    public class ItemVideoRepository : BaseRepository<ItemVideo>, IItemVideoRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemVideoRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ItemVideo> DbSet
        {
            get
            {
                return db.ItemVideos;
            }
        }

        #endregion

        #region public

        public List<ItemVideo> GetProductVideos(long ItemID)
        {
            return db.ItemVideos.Where(i => i.ItemId == ItemID).ToList();
        }

        #endregion
    }
}
