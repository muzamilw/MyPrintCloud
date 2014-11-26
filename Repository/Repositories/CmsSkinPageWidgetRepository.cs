using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
            return DbSet.Where(c => c.OrganisationId == UserDomainKey).ToList();
        }

      
        public List<CmsSkinPageWidget> GetDomainWidgetsById(long companyId)
        {
            var widgets = (from result in db.PageWidgets.Include("CmsSkinPageWidgetParams").Include("Widget")
                           select result).Where(g => g.PageId == 1 && g.CompanyId == companyId).OrderBy(c => c.Sequence).ToList();

            return widgets.ToList();
        }
    }
}
