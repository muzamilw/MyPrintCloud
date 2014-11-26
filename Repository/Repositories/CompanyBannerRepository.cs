
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Models.DomainModels;
using MPC.Interfaces.Repository;
using MPC.Repository.BaseRepository;
using System.Data.Entity;
namespace MPC.Repository.Repositories
{
    public class CompanyBannerRepository: BaseRepository<CompanyBanner>, ICompanyBannerRepository
    {
        public CompanyBannerRepository(IUnityContainer container)
            : base(container)
        {
        }

        protected override IDbSet<CompanyBanner> DbSet
        {
            get
            {
                return db.CompanyBanner;
            }
        }
     
        public List<CompanyBanner> GetCompanyBannersById(long companyId)
        {
            var companyBanners = from banner in db.CompanyBanner
                                 join companyBannerSet in db.CompanyBannerSets on banner.CompanySetId equals companyBannerSet.CompanySetId
                                 where companyBannerSet.CompanyId == companyId //&& companyBannerSet.OrganisationId == organisationId
                                 select banner;

            return companyBanners.ToList();
        }
    }
}
