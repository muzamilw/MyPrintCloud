using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Delivery Note Detail Repository
    /// </summary>
    public class DeliveryNoteDetailRepository : BaseRepository<DeliveryNoteDetail>, IDeliveryNoteDetailRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public DeliveryNoteDetailRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<DeliveryNoteDetail> DbSet
        {
            get
            {
                return db.DeliveryNoteDetails;
            }
        }

        #endregion
    }
}
