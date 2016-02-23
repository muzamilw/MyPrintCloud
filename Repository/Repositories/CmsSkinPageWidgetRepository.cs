using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Models.DomainModels;
using MPC.Interfaces.Repository;
using MPC.Repository.BaseRepository;
using System.Data.Entity;

namespace MPC.Repository.Repositories
{

    public class CmsSkinPageWidgetRepository : BaseRepository<CmsSkinPageWidget>, ICmsSkinPageWidgetRepository
    {
        public CmsSkinPageWidgetRepository(IUnityContainer container)
            : base(container)
        {
        }

        protected override IDbSet<CmsSkinPageWidget> DbSet
        {
            get
            {
                return db.PageWidgets;
            }
        }

        public override IEnumerable<CmsSkinPageWidget> GetAll()
        {
            return DbSet.Where(c => c.OrganisationId == OrganisationId).ToList();
        }

        /// <summary>
        /// Get By Page Id
        /// </summary>
        public IEnumerable<CmsSkinPageWidget> GetByPageId(long pageId, long companyId)
        {
            return DbSet.Where(c => c.PageId == pageId && c.CompanyId == companyId && c.OrganisationId == OrganisationId)
                .OrderBy(c => c.Sequence).ToList();
        }


        public List<CmsSkinPageWidget> GetDomainWidgetsById(long companyId)
        {

            var widgets = (from result in db.PageWidgets.Include("CmsSkinPageWidgetParams").Include("Widget")
                           select result).Where(g => g.CompanyId == companyId).OrderBy(c => c.Sequence).ToList();

            return widgets.ToList();
        }

        public List<CmsSkinPageWidget> GetDomainWidgetsById2(long companyId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var widgets = (from result in db.PageWidgets.Include("CmsSkinPageWidgetParams")
                           select result).Where(g => g.CompanyId == companyId).OrderBy(c => c.Sequence).ToList();

            return widgets.ToList();
        }

        public bool IsCustomWidgetUsed(long widgetId)
        {
            return DbSet.ToList().Any(a => a.WidgetId == widgetId);
        }
    }
}
