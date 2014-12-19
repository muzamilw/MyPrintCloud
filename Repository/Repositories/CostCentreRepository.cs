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
    /// CostCentre Repository
    /// </summary>
    public class CostCentreRepository : BaseRepository<CostCentre>, ICostCentreRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CostCentreRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<CostCentre> DbSet
        {
            get
            {
                return db.CostCentres;
            }
        }

        #endregion

        #region public

        /// <summary>
        /// Get All Cost Centres that are not system defined
        /// </summary>
        public IEnumerable<CostCentre> GetAllNonSystemCostCentres()
        {
            return DbSet.Where(costcentre => costcentre.OrganisationId == OrganisationId && costcentre.Type != 1);
        }

        #endregion

        
    }
}
