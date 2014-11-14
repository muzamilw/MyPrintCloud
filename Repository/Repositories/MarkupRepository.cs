using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Markup Repository
    /// </summary>
    public class MarkupRepository : BaseRepository<MarkUp>, IMarkupRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MarkupRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<MarkUp> DbSet
        {
            get
            {
                return db.MarkUps;
            }
        }

        #endregion

        #region Public
        #endregion
    }
}
