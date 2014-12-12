using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Repository.Repositories
{
    public class GlobalLanguageRepository : BaseRepository<GlobalLanguage>, IGlobalLanguageRepository
    {
        public GlobalLanguageRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<GlobalLanguage> DbSet
        {
            get
            {
                 return db.GlobalLanguages;
            }
        }

        public string GetLanguageCodeById(long organisationId)
        {
            Organisation organisation = db.Organisations.Where(o => o.OrganisationId == organisationId).Single();
            if (organisation != null)
            {
                return
                    db.GlobalLanguages.Where(c => c.LanguageId == organisation.LanguageId)
                        .Select(n => n.uiCulture)
                        .FirstOrDefault();
            }
            else
            {
                return "";
            }
        }
    }
}
