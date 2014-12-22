﻿using System.Collections.Generic;
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

      
        public List<CmsSkinPageWidget> GetDomainWidgetsById(long companyId)
        {
            var widgets = (from result in db.PageWidgets.Include("CmsSkinPageWidgetParams").Include("Widget")
                           select result).Where(g => g.CompanyId == companyId).OrderBy(c => c.Sequence).ToList();

            return widgets.ToList();
        }
    }
}
