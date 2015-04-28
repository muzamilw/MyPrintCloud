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
    /// InkPlateSide Repository
    /// </summary>
    public class InkPlateSideRepository : BaseRepository<InkPlateSide>, IInkPlateSideRepository
    {
        #region privte

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public InkPlateSideRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<InkPlateSide> DbSet
        {
            get
            {
                return db.InkPlateSides;
            }
        }

        #endregion

        #region public

        public override IEnumerable<InkPlateSide> GetAll()
        {
            return DbSet.OrderBy(ink => ink.InkTitle).ToList();
        }

        /// <summary>
        /// Find Ink Plate Side
        /// </summary>
        public InkPlateSide Find(int id)
        {
            return base.Find(id);
        }
        
        #endregion
        
    }
}
