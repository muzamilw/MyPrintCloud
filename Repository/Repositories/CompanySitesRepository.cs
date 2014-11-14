using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Company Sites Repository
    /// </summary>
    public class CompanySitesRepository : BaseRepository<CompanySites>, ICompanySitesRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CompanySitesRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<CompanySites> DbSet
        {
            get
            {
                return db.CompanySites;
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// Get All Company Sites for User Domain Key
        /// </summary>
        public override IEnumerable<CompanySites> GetAll()
        {
            return DbSet.ToList();
        }


        #endregion
    }
}
