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
    /// PipeLine Product Repository
    /// </summary>
    public class PipeLineProductRepository : BaseRepository<PipeLineProduct>, IPipeLineProductRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PipeLineProductRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<PipeLineProduct> DbSet
        {
            get
            {
                return db.PipeLineProducts;
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// Get All 
        /// </summary>
        public override IEnumerable<PipeLineProduct> GetAll()
        {
            return DbSet.ToList();
        }
        #endregion
    }
}
