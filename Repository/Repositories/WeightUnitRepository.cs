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
    /// Weight Unit Repository
    /// </summary>
    public class WeightUnitRepository : BaseRepository<WeightUnit>, IWeightUnitRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public WeightUnitRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<WeightUnit> DbSet
        {
            get
            {
                return db.WeightUnits;
            }
        }

        #endregion

        #region Public
        /// <summary>
        /// Get All Weight Units
        /// </summary>
        public override IEnumerable<WeightUnit> GetAll()
        {
            return DbSet.ToList();
        }
        #endregion
    }
}
