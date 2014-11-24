
using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    public class CmsPageRepository : BaseRepository<CmsPage>, ICmsPageRepository
    {
        public CmsPageRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<CmsPage> DbSet
        {
            get
            {
                return db.CmsPage;
            }
        }

    }
}
