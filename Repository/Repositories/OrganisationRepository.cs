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
    public class OrganisationRepository : BaseRepository<Organisation>, IOrganisationRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public OrganisationRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Organisation> DbSet
        {
            get
            {
                return db.Organisations;
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// Get All Company Sites for User Domain Key
        /// </summary>
        public override IEnumerable<Organisation> GetAll()
        {
            return DbSet.Where(cs => cs.OrganisationId == OrganisationId).ToList();
        }

        /// <summary>
        /// Get Organisation By ID
        /// </summary>
        public Organisation GetOrganizatiobByID()
        {
            return DbSet.FirstOrDefault(cs => cs.OrganisationId == OrganisationId);
        }

        public Organisation GetOrganizatiobByID(long organisationId)
        {
            return DbSet.FirstOrDefault(cs => cs.OrganisationId == organisationId);
        }

        public Organisation GetOrganizatiobByOrganisationID(long organisationId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            return db.Organisations.Where(o => o.OrganisationId == organisationId).FirstOrDefault();
        }
        #endregion
    }
}
