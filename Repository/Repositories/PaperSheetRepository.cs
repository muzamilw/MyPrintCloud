using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    public class PaperSheetRepository: BaseRepository<PaperSize>, IPaperSheetRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PaperSheetRepository(IUnityContainer container)
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

        #endregion
    }
}
