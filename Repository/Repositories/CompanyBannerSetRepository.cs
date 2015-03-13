
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Models.DomainModels;
using MPC.Interfaces.Repository;
using MPC.Repository.BaseRepository;
using System.Data.Entity;
using System.Collections.Generic;


namespace MPC.Repository.Repositories
{
    public class CompanyBannerSetRepository: BaseRepository<CompanyBannerSet>, ICompanyBannerSetRepository
    {
        public CompanyBannerSetRepository(IUnityContainer container)
            : base(container)
        {
        }

        protected override IDbSet<CompanyBannerSet> DbSet
        {
            get
            {
                return db.CompanyBannerSets;
            }
        }

        public override IEnumerable<CompanyBannerSet> GetAll()
        {
            return DbSet.Where(c => c.OrganisationId == OrganisationId).ToList();
        }

        /// <summary>
        /// Get Active Banner Set for Company
        /// </summary>
        public CompanyBannerSet GetActiveBannerSetForCompany(long companyId)
        {
            var query = from company in db.Companies
                join companyBannerSet in DbSet on company.ActiveBannerSetId equals companyBannerSet.CompanySetId
                where company.CompanyId == companyId
                select companyBannerSet;

            return query.FirstOrDefault();
        }
    }
}
