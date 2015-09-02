
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Models.DomainModels;
using MPC.Interfaces.Repository;
using MPC.Repository.BaseRepository;
using System.Data.Entity;
using System.Collections.Generic;
using System;


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

        public List<string> GetCompanyBannersByCompanyId(long companyId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            List<string> bannersList = new List<string>();
            try
            {
                var banners = DbSet.Include(c => c.CompanyBanners)
                    .Where(c => c.CompanyId == companyId)
                    .Select(c => new
                    {
                        BannersList = c.CompanyBanners.ToList()
                    }).ToList();
                foreach (var banner in banners)
                {
                    banner.BannersList.ForEach(b => bannersList.Add(b.ImageURL));
                }
                return bannersList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
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
