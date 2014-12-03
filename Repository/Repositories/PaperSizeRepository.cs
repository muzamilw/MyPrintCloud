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
    /// Paper Size Repository
    /// </summary>
    public class PaperSizeRepository : BaseRepository<PaperSize>, IPaperSizeRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PaperSizeRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<PaperSize> DbSet
        {
            get
            {
                return db.PaperSizes;
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// Get All Paper Size
        /// </summary>
        public override IEnumerable<PaperSize> GetAll()
        {
            return DbSet.ToList();
        }
        #endregion
    }
}
