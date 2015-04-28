using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// SectionCostCentreDetail Repository
    /// </summary>
    public class SectionCostCentreDetailRepository : BaseRepository<SectionCostCentreDetail>, ISectionCostCentreDetailRepository
    {
        #region private
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SectionCostCentreDetailRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<SectionCostCentreDetail> DbSet
        {
            get
            {
                return db.SectionCostCentreDetails;
            }
        }

        #endregion

        #region public

        /// <summary>
        /// Find Section Cost Centre by id
        /// </summary>
        public SectionCostCentreDetail Find(int id)
        {
            return base.Find(id);
        }

        #endregion

    }
}
