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
    /// Cms Tag Reporistory
    /// </summary>
    public class CmsTagReporistory : BaseRepository<CmsTag>, ICmsTagReporistory
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CmsTagReporistory(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<CmsTag> DbSet
        {
            get
            {
                return db.CmsTags;
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// Get CMS Tag For CMS Page (Load Default keywords)
        /// </summary>
        public IEnumerable<CmsTag> GetAllForCmsPage()
        {
            return DbSet.Where(ct => ct.IsDisplay == true).ToList();
        }

        
        #endregion
    }
}
