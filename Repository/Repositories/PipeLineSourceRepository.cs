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
    /// Pipe Line Source Repository
    /// </summary>
    public class PipeLineSourceRepository : BaseRepository<PipeLineSource>, IPipeLineSourceRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PipeLineSourceRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<PipeLineSource> DbSet
        {
            get
            {
                return db.PipeLineSources;
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// Get All 
        /// </summary>
        public override IEnumerable<PipeLineSource> GetAll()
        {
            return DbSet.ToList();
        }
        #endregion
    }
}
