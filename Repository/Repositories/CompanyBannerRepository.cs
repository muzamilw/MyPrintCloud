using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Models.DomainModels;
using MPC.Interfaces.Repository;
using MPC.Repository.BaseRepository;
using System.Data.Entity;
using System;
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
                return db.CompanyBanners;
            }
        }
     
        public List<CompanyBanner> GetCompanyBannersById(long companysetId)
        {
            try
            {
                if (companysetId > 0)
                {
                    var companyBanners = from banner in db.CompanyBanners
                        join companyBannerSet in db.CompanyBannerSets on banner.CompanySetId equals
                            companyBannerSet.CompanySetId
                        where companyBannerSet.CompanySetId == companysetId
                        //&& companyBannerSet.OrganisationId == organisationId
                        select banner;

                    return companyBanners.ToList();
                }
                else
                {
                    return null;
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
         
        }

        


    }
}
