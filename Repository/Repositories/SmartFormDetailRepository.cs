using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Smart Form Detail Repository
    /// </summary>
    public class SmartFormDetailRepository : BaseRepository<SmartFormDetail>, ISmartFormDetailRepository
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SmartFormDetailRepository(IUnityContainer container)
            : base(container)
        {

        }
        protected override IDbSet<SmartFormDetail> DbSet
        {
            get
            {
                return db.SmartFormDetails;
            }
        }

        #endregion

        #region public
        #endregion
    }
}
