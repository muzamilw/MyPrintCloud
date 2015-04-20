using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// SectionCostCentre Repository
    /// </summary>
    public class SectionCostCentreRepository : BaseRepository<SectionCostcentre>, ISectionCostCentreRepository
    {
        #region private
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SectionCostCentreRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<SectionCostcentre> DbSet
        {
            get
            {
                return db.SectionCostcentres;
            }
        }

        #endregion

        #region public

        /// <summary>
        /// Find Section Cost Centre by id
        /// </summary>
        public SectionCostcentre Find(int id)
        {
            return base.Find(id);
        }

        #endregion

    }
}
