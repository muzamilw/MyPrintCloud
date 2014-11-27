using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Models.DomainModels;
using MPC.Interfaces.Repository;
using MPC.Repository.BaseRepository;
using System.Data.Entity;


namespace MPC.Repository.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(IUnityContainer container)
            : base(container)
        {
        }

        protected override IDbSet<Company> DbSet
        {
            get
            {
                return db.Company;
            }
        }

        public override IEnumerable<Company> GetAll()
        {
            return DbSet.Where(c => c.OrganisationId == UserDomainKey).ToList();
        }

        public long GetCompanyIdByDomain(string domain)
        {
            //var companyDomain = from c in db.Company.Include("CmsSkinPageWidgetParams").Include("Widget")
            //                    join cc in db.CompanyDomains on c.CompanyId equals cc.CompanyId  
            //                    where cc.Domain.Contains(domain) && c.OrganisationId == UserDomainKey 
            //                    select c;
            var companyDomain = db.CompanyDomains.Where(d => d.Domain.Contains(domain)).ToList();
            if (companyDomain.FirstOrDefault() != null)
            {
                return companyDomain.FirstOrDefault().CompanyId;
            }
            else
            {
                return 0;
            }
        }

        public Company GetCompanyById(long companyId)
        {
            return db.Company.Where(c => c.CompanyId == companyId && c.OrganisationId == UserDomainKey).Single();
        }
    }
}
