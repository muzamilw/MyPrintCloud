using System.Collections;
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
    /// Media Library Repository
    /// </summary>
    public class MediaLibraryRepository : BaseRepository<MediaLibrary>, IMediaLibraryRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MediaLibraryRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<MediaLibrary> DbSet
        {
            get
            {
                return db.MediaLibraries;
            }
        }


        /// <summary>
        /// Get Media Libraries By Company Id
        /// </summary>
        public IEnumerable<MediaLibrary> GetMediaLibrariesByCompanyId(long companyId)
        {
            return DbSet.Where(ml => ml.CompanyId == companyId).ToList();
        }
        #endregion
    }
}
