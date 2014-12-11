using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.MISServices
{
    public class CompanyService : ICompanyService
    {
        #region Private

        private readonly ICompanyRepository companyRepository;
        private readonly ISystemUserRepository systemUserRepository;
        private readonly IRaveReviewRepository raveReviewRepository;
        private readonly ICompanyCMYKColorRepository companyCmykColorRepository;
        private readonly ICompanyTerritoryRepository companyTerritoryRepository;
        /// <summary>
        /// Save Company
        /// </summary>
        private Company SaveNewCompany(Company company)
        {
            companyRepository.Add(company);
            companyRepository.SaveChanges();
            return company;
        }

        private Company UpdateRaveReviewsOfUpdatingCompany(Company company)
        {
            var companyDbVersion = companyRepository.Find(company.CompanyId);
            #region Sub Stock Categories Items
            //Add  rave reviews
            if (company.RaveReviews != null)
            {
                foreach (var item in company.RaveReviews)
                {
                    if (companyDbVersion.RaveReviews.All(x => x.ReviewId != item.ReviewId) || item.ReviewId == 0)
                    {
                        item.CompanyId = company.CompanyId;
                        companyDbVersion.RaveReviews.Add(item);
                    }
                }
            }
            //find missing items

            List<RaveReview> missingRaveReviews = new List<RaveReview>();
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (RaveReview dbversionRaveReviews in companyDbVersion.RaveReviews)
            {
                if (company.RaveReviews != null && company.RaveReviews.All(x => x.ReviewId != dbversionRaveReviews.ReviewId))
                {
                    missingRaveReviews.Add(dbversionRaveReviews);
                }
            }

            //remove missing items
            foreach (RaveReview missingRaveReview in missingRaveReviews)
            {

                RaveReview dbVersionMissingItem = companyDbVersion.RaveReviews.First(x => x.ReviewId == missingRaveReview.ReviewId);
                if (dbVersionMissingItem.ReviewId > 0)
                {
                    companyDbVersion.RaveReviews.Remove(dbVersionMissingItem);
                    raveReviewRepository.Delete(dbVersionMissingItem);
                }
            }
            if (company.RaveReviews != null)
            {
                //updating stock sub categories
                foreach (var raveReviewItem in company.RaveReviews)
                {
                    raveReviewRepository.Update(raveReviewItem);
                }
            }
            #endregion
            return company;
        }

        private Company UpdateCmykColorsOfUpdatingCompany(Company company)
        {
            var companyDbVersion = companyRepository.Find(company.CompanyId);
            #region CMYK Colors Items
            //Add  CMYK Colors
            if (company.CompanyCMYKColors != null)
            {
                foreach (var item in company.CompanyCMYKColors)
                {
                    if (companyDbVersion.CompanyCMYKColors.All(x => x.ColorId != item.ColorId && x.CompanyId != item.CompanyId))
                    {
                        item.CompanyId = company.CompanyId;
                        companyDbVersion.CompanyCMYKColors.Add(item);
                    }
                }
            }
            //find missing items

            List<CompanyCMYKColor> missingCompanyCMYKColors = new List<CompanyCMYKColor>();
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (CompanyCMYKColor dbversionCompanyCMYKColors in companyDbVersion.CompanyCMYKColors)
            {
                if (company.CompanyCMYKColors != null && company.CompanyCMYKColors.All(x => x.ColorId != dbversionCompanyCMYKColors.ColorId && x.CompanyId != dbversionCompanyCMYKColors.ColorId))
                {
                    missingCompanyCMYKColors.Add(dbversionCompanyCMYKColors);
                }
            }

            //remove missing items
            foreach (CompanyCMYKColor missingCompanyCMYKColor in missingCompanyCMYKColors)
            {

                CompanyCMYKColor dbVersionMissingItem = companyDbVersion.CompanyCMYKColors.First(x => x.ColorId == missingCompanyCMYKColor.ColorId && x.CompanyId == missingCompanyCMYKColor.CompanyId);
                //if (dbVersionMissingItem.ColorId > 0)
                //{
                companyDbVersion.CompanyCMYKColors.Remove(dbVersionMissingItem);
                companyCmykColorRepository.Delete(dbVersionMissingItem);
                //}
            }
            if (company.CompanyCMYKColors != null)
            {
                //updating Company CMYK Colors
                foreach (var companyCMYKColorsItem in company.CompanyCMYKColors)
                {
                    companyCmykColorRepository.Update(companyCMYKColorsItem);
                }
            }
            #endregion
            return company;
        }

        private void UpdateCompanyTerritoryOfUpdatingCompany(CompanySavingModel companySavingModel)
        {
            //Add New Company Territories
            foreach (var companyTerritory in companySavingModel.NewAddedCompanyTerritories)
            {
                companyTerritory.CompanyId = companySavingModel.Company.CompanyId;
                companyTerritoryRepository.Add(companyTerritory);
            }
            //Update Company Territories
            foreach (var companyTerritory in companySavingModel.EdittedCompanyTerritories)
            {
                companyTerritoryRepository.Update(companyTerritory);
            }
            //Delete Company Territories
            foreach (var companyTerritory in companySavingModel.DeletedCompanyTerritories)
            {
                companyTerritoryRepository.Delete(companyTerritory);
            }
        }

        /// <summary>
        /// Update Company
        /// </summary>
        private Company UpdateCompany(CompanySavingModel companySavingModel, Company companyDbVersion)
        {
            var companyToBeUpdated = UpdateRaveReviewsOfUpdatingCompany(companySavingModel.Company);
            companyToBeUpdated = UpdateCmykColorsOfUpdatingCompany(companyToBeUpdated);
            BannersUpdate(companySavingModel.Company, companyDbVersion);
            UpdateCompanyTerritoryOfUpdatingCompany(companySavingModel);
            companyRepository.Update(companyToBeUpdated);
            companyRepository.SaveChanges();
            return companySavingModel.Company;
        }

        /// <summary>
        /// Update Company Banners and Banner Set
        /// </summary>
        private void BannersUpdate(Company company, Company companyDbVersion)
        {
            if (company.CompanyBannerSets != null)
            {
                foreach (var bannerItem in company.CompanyBannerSets)
                {

                    //Company Banner Set New Add
                    if (bannerItem.CompanySetId < 0)
                    {
                        bannerItem.CompanySetId = 0;
                        bannerItem.CompanyId = company.CompanyId;
                        bannerItem.OrganisationId = companyRepository.OrganisationId;

                        if (bannerItem.CompanyBanners != null)
                        {
                            foreach (var item in bannerItem.CompanyBanners)
                            {
                                //Company Banner new Added
                                if (item.CompanyBannerId < 0)
                                {
                                    item.CompanyBannerId = 0;
                                    item.CompanySetId = 0;
                                }
                            }
                        }
                        companyDbVersion.CompanyBannerSets.Add(bannerItem);
                    }

                    //if (bannerItem.CompanyBanners != null && bannerItem.CompanySetId > 0)
                    //{
                    //    foreach (var item in bannerItem.CompanyBanners)
                    //    {
                    //        //Company Banner new Added
                    //        if (item.CompanyBannerId < 0)
                    //        {
                    //            item.CompanyBannerId = 0;
                    //            item.CompanySetId = 0;
                    //        }
                    //    }
                    //}

                }
            }
        }
        #endregion

        #region Constructor

        public CompanyService(ICompanyRepository companyRepository, ISystemUserRepository systemUserRepository, IRaveReviewRepository raveReviewRepository, ICompanyCMYKColorRepository companyCmykColorRepository, ICompanyTerritoryRepository companyTerritoryRepository)
        {
            this.companyRepository = companyRepository;
            this.systemUserRepository = systemUserRepository;
            this.raveReviewRepository = raveReviewRepository;
            this.companyCmykColorRepository = companyCmykColorRepository;
            this.companyTerritoryRepository = companyTerritoryRepository;
        }
        #endregion

        #region Public

        public CompanyResponse GetAllCompaniesOfOrganisation(CompanyRequestModel request)
        {
            return companyRepository.SearchCompanies(request);
        }
        public CompanyTerritoryResponse SearchCompanyTerritories(CompanyTerritoryRequestModel request)
        {
            return companyTerritoryRepository.GetCompanyTerritory(request);
        }
        public Company GetCompanyById(int companyId)
        {
            return companyRepository.GetCompanyById(companyId);
        }

        public CompanyBaseResponse GetBaseData()
        {
            return new CompanyBaseResponse
                   {
                       SystemUsers = systemUserRepository.GetAll()
                   };
        }
        public void SaveFile(string filePath, long companyId)
        {
            Company company = companyRepository.GetCompanyById(companyId);
            if (company.Image != null)
            {
                if (File.Exists(company.Image))
                {
                    //If already organisation logo is save,it delete it 
                    File.Delete(company.Image);
                }
            }
            company.Image = filePath;
            companyRepository.SaveChanges();
        }

        public Company SaveCompany(CompanySavingModel companyModel)
        {
            Company companyDbVersion = companyRepository.Find(companyModel.Company.CompanyId);
            if (companyDbVersion == null)
            {
                SaveNewCompany(companyModel.Company);
            }
            else
            {
                UpdateCompany(companyModel, companyDbVersion);
            }
            return null;
        }

        public long GetOrganisationId()
        {
            return companyRepository.OrganisationId;
        }

        #endregion
    }
}
