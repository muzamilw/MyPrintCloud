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
    /// Paper Basis Area Repository
    /// </summary>
    public class PaperBasisAreaRepository: BaseRepository<PaperBasisArea>, IPaperBasisAreaRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PaperBasisAreaRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<PaperBasisArea> DbSet
        {
            get
            {
                return db.PaperBasisAreas;
            }
        }

        #endregion

        #region Public
        /// <summary>
        /// Get All Paper Basis Areas
        /// </summary>
        public override IEnumerable<PaperBasisArea> GetAll()
        {
            return DbSet.ToList();
        }
        #endregion
    }
}

