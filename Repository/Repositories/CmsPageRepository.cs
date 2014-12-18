
using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;
using System.Collections.Generic;
using System.Linq;
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
                return db.CmsPages;
            }
        }

        public List<CmsPage> GetSecondaryPages(long CompanyId)
        {

            return db.CmsPages.Where(p => (p.CompanyId == CompanyId || p.CompanyId == null) && p.isEnabled == true).ToList();

        }

        /// <summary>
        /// Get CMS Pages
        /// </summary>
        public SecondaryPageResponse GetCMSPages(SecondaryPageRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            IEnumerable<CmsPage> CmsPages =
            DbSet.Where(x => x.CompanyId == request.CompanyId)
                    .Skip(fromRow)
                .Take(toRow)
                .ToList();

            return new SecondaryPageResponse
            {
                RowCount = CmsPages.Count(),
                CmsPages = CmsPages
            };
        }


    }
}
