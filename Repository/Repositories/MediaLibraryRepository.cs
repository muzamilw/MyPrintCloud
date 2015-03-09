using System.Data.Entity;
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

        #endregion
    }
}
