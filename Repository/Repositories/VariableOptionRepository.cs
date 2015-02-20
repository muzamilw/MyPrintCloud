using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Variable Option Repository
    /// </summary>
    public class VariableOptionRepository : BaseRepository<VariableOption>, IVariableOptionRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public VariableOptionRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<VariableOption> DbSet
        {
            get
            {
                return db.VariableOptions;
            }
        }

        #endregion

        #region Public
        #endregion
    }
}
