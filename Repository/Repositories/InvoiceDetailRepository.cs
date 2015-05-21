using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Invoice Detail Repository
    /// </summary>
    public class InvoiceDetailRepository : BaseRepository<InvoiceDetail>, IInvoiceDetailRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public InvoiceDetailRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<InvoiceDetail> DbSet
        {
            get
            {
                return db.InvoiceDetails;
            }
        }

        #endregion
    }
}
