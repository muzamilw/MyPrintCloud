
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

    }
}
