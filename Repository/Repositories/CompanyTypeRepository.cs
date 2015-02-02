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
    ///Company Type Repository
    /// </summary>
    public class CompanyTypeRepository : BaseRepository<CompanyType>, ICompanyTypeRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyTypeRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<CompanyType> DbSet
        {
            get
            {
                return db.CompanyTypes;
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// Get All Company Types
        /// </summary>
        public override IEnumerable<CompanyType> GetAll()
        {
            return DbSet.ToList();
        }

        /// <summary>
        /// Get All Company Types For Campaign
        /// </summary>
        public IEnumerable<CompanyType> GetAllForCampaign()
        {
            return DbSet.Where(c => c.TypeId != 53).ToList();
        }
        #endregion
    }
}
