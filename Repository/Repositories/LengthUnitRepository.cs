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
    /// Length Unit Repository 
    /// </summary>
    public class LengthUnitRepository : BaseRepository<LengthUnit>, ILengthUnitRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public LengthUnitRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<LengthUnit> DbSet
        {
            get
            {
                return db.LengthUnits;
            }
        }

        #endregion

        #region Public
        /// <summary>
        /// Get All Length Units
        /// </summary>
        public override IEnumerable<LengthUnit> GetAll()
        {
            return DbSet.ToList();
        }
        #endregion
    }
}
