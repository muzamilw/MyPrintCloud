using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Models.DomainModels;
using MPC.Interfaces.Repository;
using MPC.Repository.BaseRepository;
using System.Data.Entity;


namespace MPC.Repository.Repositories
{
    public class CompanyDomainRepository : BaseRepository<CompanyDomain>, ICompanyDomainRepository
    {
        public CompanyDomainRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<CompanyDomain> DbSet
        {
            get
            {
                return db.CompanyDomains;
            }
        }


        #region Public

        public override IEnumerable<CompanyDomain> GetAll()
        {
            return DbSet.ToList();
        }

        public CompanyDomain GetDomainByUrl(string Url)
        {

            return db.CompanyDomains.Where(d => d.Domain.Contains(Url)).FirstOrDefault();
        }
        public CompanyDomain GetDomainByCompanyId(long Companyid)
        {

            return db.CompanyDomains.Where(d => d.CompanyId == Companyid).FirstOrDefault();
        }
        #endregion


    }
}
