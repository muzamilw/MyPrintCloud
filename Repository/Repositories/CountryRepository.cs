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
    /// Country Repository
    /// </summary>
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CountryRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Country> DbSet
        {
            get
            {
                return db.Countries;
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// Get All Countries for Current Organisation
        /// </summary>
        public override IEnumerable<Country> GetAll()
        {
            return DbSet.ToList();
        }

        #endregion
    }
}
