using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Purchase Detail Repository
    /// </summary>
    public class PurchaseDetailRepository : BaseRepository<PurchaseDetail>, IPurchaseDetailRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PurchaseDetailRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<PurchaseDetail> DbSet
        {
            get
            {
                return db.PurchaseDetails;
            }
        }

        #endregion

    }
}
