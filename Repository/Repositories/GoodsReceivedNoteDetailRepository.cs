using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Goods Received Note Detail Repository
    /// </summary>
    public class GoodsReceivedNoteDetailRepository : BaseRepository<GoodsReceivedNoteDetail>, IGoodsReceivedNoteDetailRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public GoodsReceivedNoteDetailRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<GoodsReceivedNoteDetail> DbSet
        {
            get
            {
                return db.GoodsReceivedNoteDetails;
            }
        }

        #endregion
    }
}
