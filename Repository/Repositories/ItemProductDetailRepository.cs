using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// ItemProductDetail Repository
    /// </summary>
    public class ItemProductDetailRepository : BaseRepository<ItemProductDetail>, IItemProductDetailRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemProductDetailRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ItemProductDetail> DbSet
        {
            get
            {
                return db.ItemProductDetails;
            }
        }

        #endregion

        #region public

        /// <summary>
        /// Find Item Product Detail
        /// </summary>
        public ItemProductDetail Find(int id)
        {
            return DbSet.Find(id);
        }

        #endregion

        
    }
}
