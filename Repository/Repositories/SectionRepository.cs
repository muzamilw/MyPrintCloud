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
    /// Section Repository
    /// </summary>
    public class SectionRepository : BaseRepository<Section>, ISectionRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SectionRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Section> DbSet
        {
            get
            {
                return db.Sections;
            }
        }

        #endregion

        #region Public
        /// <summary>
        /// Get All Sections
        /// </summary>
        public override IEnumerable<Section> GetAll()
        {
            return DbSet.ToList();
        }
        #endregion
    }
}
