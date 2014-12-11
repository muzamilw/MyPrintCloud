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
    /// Color Pallete Repository
    /// </summary>
    public class ColorPalleteRepository : BaseRepository<ColorPallete>, IColorPalleteRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ColorPalleteRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ColorPallete> DbSet
        {
            get
            {
                return db.ColorPalletes;
            }
        }

        #endregion

        #region Public
        /// <summary>
        /// Get All ColorPallete
        /// </summary>
        public override IEnumerable<ColorPallete> GetAll()
        {
            return DbSet.ToList();
        }
        #endregion
    }
}
