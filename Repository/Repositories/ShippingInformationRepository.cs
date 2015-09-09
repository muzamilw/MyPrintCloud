using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Shipping Information Repository
    /// </summary>
    public class ShippingInformationRepository : BaseRepository<ShippingInformation>, IShippingInformationRepository
    {
        #region private
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ShippingInformationRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ShippingInformation> DbSet
        {
            get
            {
                return db.ShippingInformations;
            }
        }

        #endregion

        #region public

        /// <summary>
        /// Find By Id
        /// </summary>
        public ShippingInformation Find(int id)
        {
            return base.Find(id);
        }

        #endregion
        
    }
}
