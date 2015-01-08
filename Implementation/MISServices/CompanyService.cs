using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.Repositories;

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
        private readonly ICompanyBannerRepository companyBannerRepository;
        private readonly IAddressRepository addressRepository;
        private readonly ICompanyContactRepository companyContactRepository;
        private readonly ICompanyContactRoleRepository companyContactRoleRepository;
        private readonly IRegistrationQuestionRepository registrationQuestionRepository;
        private readonly ICmsPageRepository cmsPageRepository;
        private readonly IPageCategoryRepository pageCategoryRepository;
        private readonly IPaymentMethodRepository paymentMethodRepository;
        private readonly IEmailEventRepository emailEventRepository;
        private readonly IPaymentGatewayRepository paymentGatewayRepository;
        private readonly IWidgetRepository widgetRepository;
        private readonly ICmsSkinPageWidgetRepository cmsSkinPageWidgetRepository;
        private readonly IProductCategoryRepository productCategoryRepository;
        private readonly IOrganisationRepository organisationRepository;
        private readonly IOrganisationFileTableViewRepository mpcFileTableViewRepository;
        private readonly IProductCategoryFileTableViewRepository productCategoryFileTableViewRepository;
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
            #region Rave Reviews
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
        private Company UpdatePaymentGatewaysOfUpdatingCompany(Company company)
        {
            var companyDbVersion = companyRepository.Find(company.CompanyId);
            #region Payment Gateways
            //Add  Payment Gateways
            if (company.PaymentGateways != null)
            {
                foreach (var item in company.PaymentGateways)
                {
                    if (companyDbVersion.PaymentGateways.All(x => x.PaymentGatewayId != item.PaymentGatewayId) || item.PaymentGatewayId == 0)
                    {
                        item.CompanyId = company.CompanyId;
                        companyDbVersion.PaymentGateways.Add(item);
                    }
                }
            }
            //find missing items

            List<PaymentGateway> missingPaymentGateways = new List<PaymentGateway>();
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (PaymentGateway dbversionPaymentGateways in companyDbVersion.PaymentGateways)
            {
                if (company.PaymentGateways != null && company.PaymentGateways.All(x => x.PaymentGatewayId != dbversionPaymentGateways.PaymentGatewayId))
                {
                    missingPaymentGateways.Add(dbversionPaymentGateways);
                }
            }

            //remove missing items
            foreach (PaymentGateway missingPaymentGateway in missingPaymentGateways)
            {

                PaymentGateway dbVersionMissingItem = companyDbVersion.PaymentGateways.First(x => x.PaymentGatewayId == missingPaymentGateway.PaymentGatewayId);
                if (dbVersionMissingItem.PaymentGatewayId > 0)
                {
                    companyDbVersion.PaymentGateways.Remove(dbVersionMissingItem);
                    paymentGatewayRepository.Delete(dbVersionMissingItem);
                }
            }
            if (company.PaymentGateways != null)
            {
                //updating payment Gateway
                foreach (var paymentGateway in company.PaymentGateways)
                {
                    paymentGatewayRepository.Update(paymentGateway);
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
            if (companySavingModel.NewAddedCompanyTerritories != null)
            {
                foreach (var companyTerritory in companySavingModel.NewAddedCompanyTerritories)
                {
                    companyTerritory.CompanyId = companySavingModel.Company.CompanyId;
                    companyTerritoryRepository.Add(companyTerritory);
                }
            }
            if (companySavingModel.EdittedCompanyTerritories != null)
            {
                //Update Company Territories
                foreach (var companyTerritory in companySavingModel.EdittedCompanyTerritories)
                {
                    companyTerritoryRepository.Update(companyTerritory);
                }
            }
            if (companySavingModel.DeletedCompanyTerritories != null)
            {
                //Delete Company Territories
                foreach (var companyTerritory in companySavingModel.DeletedCompanyTerritories)
                {
                    var companyTerritoryToDelete = companyTerritoryRepository.Find(companyTerritory.TerritoryId);
                    companyTerritoryRepository.Delete(companyTerritoryToDelete);
                }
            }
        }
        private void UpdateAddressOfUpdatingCompany(CompanySavingModel companySavingModel)
        {
            if (companySavingModel.NewAddedAddresses != null)
            {
                //Add New Addresses
                foreach (var address in companySavingModel.NewAddedAddresses)
                {
                    address.CompanyId = companySavingModel.Company.CompanyId;
                    addressRepository.Add(address);
                }
            }
            if (companySavingModel.EdittedAddresses != null)
                //Update addresses
                foreach (var address in companySavingModel.EdittedAddresses)
                {
                    addressRepository.Update(address);
                }
            if (companySavingModel.DeletedAddresses != null)
                //Delete Addresses
                foreach (var address in companySavingModel.DeletedAddresses)
                {
                    var addressToDelete = addressRepository.Find(address.AddressId);
                    addressRepository.Delete(addressToDelete);
                }
        }
        private void SaveProductCategoryThumbNailImage(ProductCategory productCategory)
        {
            if (productCategory.ThumbNailBytes != null)
            {
                string base64 = productCategory.ThumbNailBytes.Substring(productCategory.ThumbNailBytes.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                productCategory.ThumbNailFileBytes = Convert.FromBase64String(base64);
            }
            if (productCategory.ImageBytes != null)
            {
                string base64Image = productCategory.ImageBytes.Substring(productCategory.ImageBytes.IndexOf(',') + 1);
                base64Image = base64Image.Trim('\0');
                productCategory.ImageFileBytes = Convert.FromBase64String(base64Image);
            }
        }
        private void UpdateProductCategoriesOfUpdatingCompany(CompanySavingModel companySavingModel, List<ProductCategory> productCategories)
        {
            if (companySavingModel.NewProductCategories != null)
            {
                //Add New Product Category
                foreach (var productCategory in companySavingModel.NewProductCategories)
                {
                    productCategory.CompanyId = companySavingModel.Company.CompanyId;
                    SaveProductCategoryThumbNailImage(productCategory);
                    productCategoryRepository.Add(productCategory);
                    //companyToBeUpdated.ProductCategories.Add(productCategory);
                    productCategories.Add(productCategory);

                }
            }
            if (companySavingModel.EdittedProductCategories != null)
                //Update Product Categories
                foreach (var productCategory in companySavingModel.EdittedProductCategories)
                {
                    productCategoryRepository.Update(productCategory);
                    SaveProductCategoryThumbNailImage(productCategory);
                    //companyToBeUpdated.ProductCategories.Update(productCategory);
                    productCategories.Add(productCategory);
                }
            if (companySavingModel.DeletedProductCategories != null)
                //Delete Product Categories
                foreach (var productCategory in companySavingModel.DeletedProductCategories)
                {
                    var productCategoryToDelete = productCategoryRepository.Find(productCategory.ProductCategoryId);
                    productCategoryRepository.Delete(productCategoryToDelete);
                    //companyToBeUpdated.ProductCategories.Delete(productCategory);
                }
        }

        private void UpdateCompanyContactOfUpdatingCompany(CompanySavingModel companySavingModel)
        {
            if (companySavingModel.NewAddedCompanyContacts != null)
            {
                //Add New companyContacts
                foreach (var companyContact in companySavingModel.NewAddedCompanyContacts)
                {
                    //address.CompanyId = companySavingModel.Company.CompanyId;
                    companyContact.image = SaveCompanyContactProfileImage(companyContact);
                    companyContactRepository.Add(companyContact);
                }
            }
            if (companySavingModel.EdittedCompanyContacts != null)
            {
                //Update companyContacts
                foreach (var companyContact in companySavingModel.EdittedCompanyContacts)
                {
                    if (File.Exists(companyContact.image))
                    {
                        //If already image exist
                        File.Delete(companyContact.image);
                    }
                    companyContact.image = SaveCompanyContactProfileImage(companyContact);
                    companyContactRepository.Update(companyContact);
                }
            }
            if (companySavingModel.DeletedCompanyContacts != null)
            {
                //Delete companyContacts
                foreach (var companyContact in companySavingModel.DeletedCompanyContacts)
                {
                    var companyContactTodelete = companyContactRepository.Find(companyContact.ContactId);
                    companyContactRepository.Delete(companyContactTodelete);
                }
            }
        }

        /// <summary>
        /// Update Company
        /// </summary>
        private Company UpdateCompany(CompanySavingModel companySavingModel, Company companyDbVersion)
        {
            var productCategories = new List<ProductCategory>();
            companySavingModel.Company.OrganisationId = companyRepository.OrganisationId;
            var companyToBeUpdated = UpdateRaveReviewsOfUpdatingCompany(companySavingModel.Company);
            companyToBeUpdated = UpdatePaymentGatewaysOfUpdatingCompany(companyToBeUpdated);
            companyToBeUpdated = UpdateCmykColorsOfUpdatingCompany(companyToBeUpdated);
            BannersUpdate(companySavingModel.Company, companyDbVersion);
            UpdateCompanyTerritoryOfUpdatingCompany(companySavingModel);
            UpdateAddressOfUpdatingCompany(companySavingModel);
            UpdateProductCategoriesOfUpdatingCompany(companySavingModel, productCategories);
            UpdateCompanyContactOfUpdatingCompany(companySavingModel);
            UpdateSecondaryPagesCompany(companySavingModel, companyDbVersion);
            UpdateCampaigns(companySavingModel.Company.Campaigns, companyDbVersion);
            UpdateCmsSkinPageWidget(companySavingModel.CmsPageWithWidgetList, companyDbVersion);
            companyRepository.Update(companyToBeUpdated);
            companyRepository.SaveChanges();

            //Save Files
            companyToBeUpdated.ProductCategories = productCategories;
            SaveFilesOfProductCategories(companyToBeUpdated);

            return companySavingModel.Company;
        }

        private void SaveFilesOfProductCategories(Company company)
        {
            // Update Organisation MISLogoStreamId
            Organisation organisation = organisationRepository.Find(organisationRepository.OrganisationId);
            //IEnumerable<ProductCategory> productCategories = productCategoryRepository.GetAllCategoriesByStoreId(company.CompanyId);
            if (organisation == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture,
                    LanguageResources.MyOrganisationService_OrganisationNotFound,
                    organisationRepository.OrganisationId));
            }
            string pathLocator = "\\Organisation" + organisation.OrganisationId;
            if (
                string.IsNullOrEmpty(productCategoryFileTableViewRepository.GetNewPathLocator(pathLocator,
                    FileTableCaption.Category)))
            {
                CategoryFileTableView categoryFile = productCategoryFileTableViewRepository.Create();
                productCategoryFileTableViewRepository.Add(categoryFile);
                categoryFile.Name = "Organisation" + organisation.OrganisationId;
                categoryFile.UncPath = pathLocator;
                categoryFile.IsDirectory = true;
                categoryFile.FileTableName = FileTableCaption.Category;
                // Save to File Table
                productCategoryFileTableViewRepository.SaveChanges();
            }
            if (company.ProductCategories != null)
            {
                Dictionary<long, List<CategoryFileTableView>> categoryFileTableViews = new Dictionary<long, List<CategoryFileTableView>>();
                foreach (var productCategory in company.ProductCategories)
                {

                    categoryFileTableViews[productCategory.ProductCategoryId] = new List<CategoryFileTableView>();
                    if (!string.IsNullOrEmpty(productCategory.ThumbNailBytes))
                    {
                        // Add File
                        CategoryFileTableView categoryFileTableView = productCategoryFileTableViewRepository.Create();

                        categoryFileTableView.Name = productCategory.ThumbNailFileName + "_" + productCategory.ProductCategoryId + "_Thumbnail";
                        categoryFileTableView.FileStream = productCategory.ThumbNailFileBytes;
                        categoryFileTableView.FileTableName = FileTableCaption.Category;
                        categoryFileTableView.UncPath = pathLocator;
                        productCategoryFileTableViewRepository.Add(categoryFileTableView);

                        categoryFileTableViews[productCategory.ProductCategoryId].Add(categoryFileTableView);
                    }
                    if (!string.IsNullOrEmpty(productCategory.ImageBytes))
                    {
                        // Add File
                        CategoryFileTableView categoryFileTableView = productCategoryFileTableViewRepository.Create();

                        categoryFileTableView.Name = productCategory.ImageFileName + "_" + productCategory.ProductCategoryId + "_Image";
                        categoryFileTableView.FileStream = productCategory.ImageFileBytes;
                        categoryFileTableView.FileTableName = FileTableCaption.Category;
                        categoryFileTableView.UncPath = pathLocator;
                        productCategoryFileTableViewRepository.Add(categoryFileTableView);

                        categoryFileTableViews[productCategory.ProductCategoryId].Add(categoryFileTableView);
                    }

                }
                productCategoryFileTableViewRepository.SaveChanges();

                foreach (var categoryFileTableView in categoryFileTableViews)
                {
                    ProductCategory category =
                        company.ProductCategories.FirstOrDefault(p => p.ProductCategoryId == categoryFileTableView.Key);
                    if (category != null)
                    {
                        CategoryFileTableView view = categoryFileTableView.Value.FirstOrDefault(c => c.Name == category.ThumbNailFileName + "_" + category.ProductCategoryId + "_Thumbnail");
                        if (view != null)
                        {
                            category.ThumbnailStreamId = view.StreamId;
                        }
                        CategoryFileTableView view2 = categoryFileTableView.Value.FirstOrDefault(c => c.Name == category.ImageFileName + "_" + category.ProductCategoryId + "_Image");
                        if (view2 != null)
                        {
                            category.ImageStreamId = view2.StreamId;
                        }
                    }
                }

                productCategoryFileTableViewRepository.SaveChanges();
            }
        }

        /// <summary>
        /// Update CMS Skin Page Widget
        /// </summary>
        private void UpdateCmsSkinPageWidget(IEnumerable<CmsPageWithWidgetList> cmsPageWithWidgetList, Company companyDbVersion)
        {

            if (cmsPageWithWidgetList != null)
            {
                #region Add/Edit
                foreach (var item in cmsPageWithWidgetList)
                {
                    if (item.CmsSkinPageWidgets != null)
                    {
                        foreach (var cmsSkinPageWidget in item.CmsSkinPageWidgets)
                        {
                            CmsSkinPageWidget skinPageWidgetDbVsersion = companyDbVersion.CmsSkinPageWidgets.FirstOrDefault(p => p.PageWidgetId == cmsSkinPageWidget.PageWidgetId);
                            if (skinPageWidgetDbVsersion != null && skinPageWidgetDbVsersion.PageWidgetId > 0)
                            {
                                skinPageWidgetDbVsersion.Sequence = cmsSkinPageWidget.Sequence;
                                CmsSkinPageWidgetParam skinPageWidgetParam =
                                    cmsSkinPageWidget.CmsSkinPageWidgetParams != null
                                        ? cmsSkinPageWidget.CmsSkinPageWidgetParams.FirstOrDefault(
                                            p => p.PageWidgetId == cmsSkinPageWidget.PageWidgetId)
                                        : null;

                                if (skinPageWidgetParam != null)
                                {
                                    foreach (var dbVersionParam in skinPageWidgetDbVsersion.CmsSkinPageWidgetParams)
                                    {
                                        dbVersionParam.ParamValue = skinPageWidgetParam.ParamValue;
                                    }
                                }
                            }
                            else
                            {
                                //New widget Added
                                cmsSkinPageWidget.OrganisationId = companyRepository.OrganisationId;
                                companyDbVersion.CmsSkinPageWidgets.Add(cmsSkinPageWidget);
                            }
                        }
                    }
                }
                #endregion

                #region Delete
                //find missing items
                List<CmsSkinPageWidget> missingCmsSkinPageWidgetListItems = new List<CmsSkinPageWidget>();
                //find missing items
                foreach (var dbversionCmsSkinPageWidgetItem in companyDbVersion.CmsSkinPageWidgets)
                {
                    CmsPageWithWidgetList cmsPageWithWidgetListItem = cmsPageWithWidgetList.FirstOrDefault(pw => pw.PageId == dbversionCmsSkinPageWidgetItem.PageId);
                    if (cmsPageWithWidgetListItem != null && cmsPageWithWidgetListItem.CmsSkinPageWidgets != null && cmsPageWithWidgetListItem.CmsSkinPageWidgets.All(w => w.PageWidgetId != dbversionCmsSkinPageWidgetItem.PageWidgetId))
                    {
                        missingCmsSkinPageWidgetListItems.Add(dbversionCmsSkinPageWidgetItem);
                    }
                    //In case user delete all Widgets items from client side then it delete all items from db
                    if (cmsPageWithWidgetListItem != null && cmsPageWithWidgetListItem.CmsSkinPageWidgets == null)
                    {
                        missingCmsSkinPageWidgetListItems.Add(dbversionCmsSkinPageWidgetItem);
                    }
                }
                //remove missing items
                foreach (CmsSkinPageWidget missingCmsSkinPageWidgetItem in missingCmsSkinPageWidgetListItems)
                {

                    CmsSkinPageWidget dbVersionMissingItem = companyDbVersion.CmsSkinPageWidgets != null ? companyDbVersion.CmsSkinPageWidgets.FirstOrDefault(
                        w => w.PageWidgetId == missingCmsSkinPageWidgetItem.PageWidgetId) : null;
                    if (dbVersionMissingItem != null && dbVersionMissingItem.PageWidgetId > 0)
                    {
                        cmsSkinPageWidgetRepository.Delete(dbVersionMissingItem);
                        cmsSkinPageWidgetRepository.SaveChanges();
                    }
                }
                #endregion
            }




        }
        /// <summary>
        /// 
        /// </summary>
        private void UpdateCampaigns(IEnumerable<Campaign> campaigns, Company companyDbVersion)
        {
            #region update Campaign

            if (campaigns != null)
            {
                foreach (var campaign in campaigns)
                {
                    //New Added
                    if (campaign.CampaignId == 0)
                    {
                        campaign.CompanyId = companyDbVersion.CompanyId;
                        companyDbVersion.Campaigns.Add(campaign);
                    }
                    else
                    {
                        Campaign campaignDbItem =
                            companyDbVersion.Campaigns.FirstOrDefault(c => c.CampaignId == campaign.CampaignId);
                        if (campaignDbItem != null)
                        {
                            if (campaign.CampaignName != campaignDbItem.CampaignName ||
                                campaign.EmailEvent != campaignDbItem.EmailEvent
                                || campaign.IsEnabled != campaignDbItem.IsEnabled ||
                                campaign.SendEmailAfterDays != campaignDbItem.SendEmailAfterDays
                                || campaign.StartDateTime != campaignDbItem.StartDateTime)
                            {
                                campaignDbItem.CampaignName = campaign.CampaignName;
                                campaignDbItem.EmailEvent = campaign.EmailEvent;
                                campaignDbItem.IsEnabled = campaign.IsEnabled;
                                campaignDbItem.StartDateTime = campaign.StartDateTime;
                                campaignDbItem.SendEmailAfterDays = campaign.SendEmailAfterDays;
                                campaignDbItem.CampaignType = campaign.CampaignType;
                            }
                        }
                    }
                }
            }

            #endregion

            #region Delete Campaigns

            //find missing items
            List<Campaign> missingCampaignListItems = new List<Campaign>();
            foreach (var dbversionCampaignItem in companyDbVersion.Campaigns)
            {
                if (campaigns != null && campaigns.All(x => x.CampaignId != dbversionCampaignItem.CampaignId))
                {
                    missingCampaignListItems.Add(dbversionCampaignItem);
                }
                //In case user delete all Campaigns
                if (campaigns == null)
                {
                    missingCampaignListItems.Add(dbversionCampaignItem);
                }
            }
            //remove missing items
            foreach (Campaign missingCampaignItem in missingCampaignListItems)
            {
                companyDbVersion.Campaigns.Remove(missingCampaignItem);
            }
            #endregion
        }

        //Update Secondary Pages
        private void UpdateSecondaryPagesCompany(CompanySavingModel companySavingModel, Company companyDbVersion)
        {
            IEnumerable<PageCategory> pageCategoriesDbVersion = pageCategoryRepository.GetAll();
            if (companySavingModel.NewAddedCmsPages != null)
            {
                foreach (var item in companySavingModel.NewAddedCmsPages)
                {
                    item.PageId = 0;
                    item.CompanyId = companySavingModel.Company.CompanyId;
                    item.OrganisationId = companyRepository.OrganisationId;
                    item.PageBanner = SaveCmsPageImage(item);
                    companyDbVersion.CmsPages.Add(item);
                }
            }
            //Edited List
            if (companySavingModel.EditCmsPages != null)
            {
                foreach (var item in companySavingModel.EditCmsPages)
                {
                    foreach (var dbItem in companyDbVersion.CmsPages)
                    {
                        if (item.PageId == dbItem.PageId)
                        {
                            dbItem.CategoryId = item.CategoryId;
                            dbItem.Meta_AuthorContent = item.Meta_AuthorContent;
                            dbItem.Meta_CategoryContent = item.Meta_CategoryContent;
                            dbItem.Meta_DescriptionContent = item.Meta_DescriptionContent;
                            dbItem.Meta_LanguageContent = item.Meta_LanguageContent;
                            dbItem.Meta_RevisitAfterContent = item.Meta_RevisitAfterContent;
                            dbItem.Meta_RobotsContent = item.Meta_RobotsContent;
                            dbItem.Meta_Title = item.Meta_Title;
                            dbItem.PageHTML = item.PageHTML;
                            dbItem.PageKeywords = item.PageKeywords;
                            dbItem.PageTitle = item.PageTitle;
                            if (File.Exists(dbItem.PageBanner))
                            {
                                //If already image exist
                                File.Delete(dbItem.PageBanner);
                            }
                            dbItem.PageBanner = SaveCmsPageImage(item);
                        }
                    }
                }
            }
            //Delete List
            if (companySavingModel.DeletedCmsPages != null)
            {
                foreach (var item in companySavingModel.DeletedCmsPages)
                {
                    cmsPageRepository.Delete(cmsPageRepository.Find(item.PageId));
                    cmsPageRepository.SaveChanges();
                }
            }
            companyRepository.SaveChanges();

            //Update Page Category List Items
            if (companySavingModel.PageCategories != null)
            {
                foreach (var item in companySavingModel.PageCategories)
                {
                    foreach (var dbItem in pageCategoriesDbVersion)
                    {
                        if (item.CategoryId == dbItem.CategoryId)
                        {
                            dbItem.CategoryName = item.CategoryName;
                        }
                    }
                }
                pageCategoryRepository.SaveChanges();
            }
        }

        /// <summary>
        /// Update Company Banners and Banner Set
        /// </summary>
        private void BannersUpdate(Company company, Company companyDbVersion)
        {
            #region Update Banners
            if (company.CompanyBannerSets != null)
            {
                foreach (var bannerItem in company.CompanyBannerSets)
                {
                    //Company Banner Set New Added and company banner also added under this banner set
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
                                    item.CreateDate = DateTime.Now;
                                }
                            }
                        }
                        companyDbVersion.CompanyBannerSets.Add(bannerItem);
                    }

                    if (bannerItem.CompanyBanners != null && bannerItem.CompanySetId > 0)
                    {
                        CompanyBannerSet bannerSetDbVersion =
                                  companyDbVersion.CompanyBannerSets.FirstOrDefault(
                                      x => x.CompanySetId == bannerItem.CompanySetId);

                        foreach (var item in bannerItem.CompanyBanners)
                        {
                            //Company Banner new Added under existing banner set
                            if (item.CompanyBannerId < 0)
                            {
                                item.CompanyBannerId = 0;
                                item.CompanySetId = 0;
                                item.CreateDate = DateTime.Now;
                                if (bannerSetDbVersion != null) bannerSetDbVersion.CompanyBanners.Add(item);
                            }
                            else
                            {    //Updated company banner
                                if (bannerSetDbVersion != null)
                                {
                                    CompanyBanner bannerDbVersion = bannerSetDbVersion.CompanyBanners.FirstOrDefault(
                                        x => x.CompanyBannerId == item.CompanyBannerId);
                                    if (bannerDbVersion != null)
                                    {

                                        bannerDbVersion.Heading = item.Heading;
                                        bannerDbVersion.ButtonURL = item.ButtonURL;
                                        bannerDbVersion.ItemURL = item.ItemURL;
                                        bannerDbVersion.Description = item.Description;
                                        bannerDbVersion.CompanySetId = item.CompanySetId;
                                    }
                                }
                            }
                        }
                    }

                }
                companyRepository.SaveChanges();
            }//End Add/Edit 
            #endregion

            #region Delete Banners
            foreach (var bannerSetDbVersion in companyDbVersion.CompanyBannerSets)
            {

                //find missing items
                List<CompanyBanner> missingCompanyBannerListItems = new List<CompanyBanner>();
                foreach (var dbversionCompanyBannerItem in bannerSetDbVersion.CompanyBanners)
                {
                    CompanyBannerSet bannerSetItem = company.CompanyBannerSets != null ? company.CompanyBannerSets.FirstOrDefault(x => x.CompanySetId == dbversionCompanyBannerItem.CompanySetId) : null;
                    if (bannerSetItem != null && bannerSetItem.CompanyBanners != null && bannerSetItem.CompanyBanners.All(x => x.CompanyBannerId != dbversionCompanyBannerItem.CompanyBannerId))
                    {
                        missingCompanyBannerListItems.Add(dbversionCompanyBannerItem);
                    }
                    //In case user delete all Stock Cost And Price items from client side then it delete all items from db
                    if (bannerSetItem == null || bannerSetItem.CompanyBanners == null)
                    {
                        missingCompanyBannerListItems.Add(dbversionCompanyBannerItem);
                    }
                }
                //remove missing items
                foreach (CompanyBanner missingCompanyBannerItem in missingCompanyBannerListItems)
                {
                    CompanyBanner dbVersionMissingItem = bannerSetDbVersion.CompanyBanners.First(x => x.CompanyBannerId == missingCompanyBannerItem.CompanyBannerId);
                    if (dbVersionMissingItem.CompanyBannerId > 0)
                    {
                        companyBannerRepository.Delete(dbVersionMissingItem);
                        companyBannerRepository.SaveChanges();
                    }
                }
            }
            #endregion

            if (company.CompanyBannerSets != null)
                SaveCompanyBannerImages(company.CompanyBannerSets);
        }
        /// <summary>
        /// Save Images
        /// </summary>
        private void SaveCompanyBannerImages(IEnumerable<CompanyBannerSet> companyBannerSets)
        {
            List<CompanyBanner> companyBannersList = companyBannerRepository.GetAll().ToList();

            foreach (var item in companyBannerSets)
            {
                if (item.CompanyBanners != null)
                    foreach (var img in item.CompanyBanners)
                    {
                        if (img.Bytes != null)
                        {
                            string base64 = img.Bytes.Substring(img.Bytes.IndexOf(',') + 1);
                            base64 = base64.Trim('\0');
                            byte[] data = Convert.FromBase64String(base64);

                            string directoryPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Resources/CompanyBanners");
                            if (directoryPath != null && !Directory.Exists(directoryPath))
                            {
                                Directory.CreateDirectory(directoryPath);
                            }
                            string savePath = directoryPath + "\\" + img.CompanyBannerId + "_" + img.FileName;
                            File.WriteAllBytes(savePath, data);

                            CompanyBanner companyBanner = companyBannersList.FirstOrDefault(x => x.CompanyBannerId == img.CompanyBannerId);
                            if (companyBanner != null)
                            {
                                companyBanner.ImageURL = savePath;
                            }
                        }
                    }
            }

            companyBannerRepository.SaveChanges();

        }

        /// <summary>
        /// Save Images for CMS Page
        /// </summary>
        private string SaveCmsPageImage(CmsPage cmsPage)
        {
            string base64 = cmsPage.Bytes.Substring(cmsPage.Bytes.IndexOf(',') + 1);
            base64 = base64.Trim('\0');
            byte[] data = Convert.FromBase64String(base64);

            string directoryPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Resources/CMSPages");
            if (directoryPath != null && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            Guid newGuid = Guid.NewGuid();
            string savePath = directoryPath + "\\" + newGuid + "_" + cmsPage.FileName;
            File.WriteAllBytes(savePath, data);
            return savePath;

        }
        /// <summary>
        /// Save Images for Company Contact Profile Image
        /// </summary>
        private string SaveCompanyContactProfileImage(CompanyContact companyContact)
        {
            string base64 = companyContact.image.Substring(companyContact.image.IndexOf(',') + 1);
            base64 = base64.Trim('\0');
            byte[] data = Convert.FromBase64String(base64);

            string directoryPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Resources/CompanyContactProfileImages");
            if (directoryPath != null && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            Guid newGuid = Guid.NewGuid();
            string savePath = directoryPath + "\\" + newGuid + "_" + companyContact.FileName;
            File.WriteAllBytes(savePath, data);
            return savePath;

        }
        #endregion

        #region Constructor

        public CompanyService(ICompanyRepository companyRepository, ISystemUserRepository systemUserRepository, IRaveReviewRepository raveReviewRepository,
            ICompanyCMYKColorRepository companyCmykColorRepository, ICompanyTerritoryRepository companyTerritoryRepository, IAddressRepository addressRepository,
            ICompanyContactRoleRepository companyContactRoleRepository, IRegistrationQuestionRepository registrationQuestionRepository
            , ICompanyBannerRepository companyBannerRepository, ICompanyContactRepository companyContactRepository, ICmsPageRepository cmsPageRepository,
             IPageCategoryRepository pageCategoryRepository, IEmailEventRepository emailEventRepository, IPaymentMethodRepository paymentMethodRepository,
            IPaymentGatewayRepository paymentGatewayRepository, IWidgetRepository widgetRepository, ICmsSkinPageWidgetRepository cmsSkinPageWidgetRepository, IProductCategoryRepository productCategoryRepository,
            IOrganisationRepository organisationRepository, IOrganisationFileTableViewRepository mpcFileTableViewRepository, IProductCategoryFileTableViewRepository productCategoryFileTableViewRepository)
        {
            this.companyRepository = companyRepository;
            this.systemUserRepository = systemUserRepository;
            this.raveReviewRepository = raveReviewRepository;
            this.companyCmykColorRepository = companyCmykColorRepository;
            this.companyTerritoryRepository = companyTerritoryRepository;
            this.companyBannerRepository = companyBannerRepository;
            this.addressRepository = addressRepository;
            this.companyContactRepository = companyContactRepository;
            this.companyContactRoleRepository = companyContactRoleRepository;
            this.registrationQuestionRepository = registrationQuestionRepository;
            this.cmsPageRepository = cmsPageRepository;
            this.pageCategoryRepository = pageCategoryRepository;
            this.paymentMethodRepository = paymentMethodRepository;
            this.emailEventRepository = emailEventRepository;
            this.paymentGatewayRepository = paymentGatewayRepository;
            this.widgetRepository = widgetRepository;
            this.cmsSkinPageWidgetRepository = cmsSkinPageWidgetRepository;
            this.productCategoryRepository = productCategoryRepository;
            this.organisationRepository = organisationRepository;
            this.mpcFileTableViewRepository = mpcFileTableViewRepository;
            this.productCategoryFileTableViewRepository = productCategoryFileTableViewRepository;
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
        public PaymentGatewayResponse SearchPaymentGateways(PaymentGatewayRequestModel request)
        {
            return paymentGatewayRepository.GetPaymentGateways(request);
        }
        public AddressResponse SearchAddresses(AddressRequestModel request)
        {
            return addressRepository.GetAddress(request);
        }
        public CompanyContactResponse SearchCompanyContacts(CompanyContactRequestModel request)
        {
            return companyContactRepository.GetCompanyContacts(request);
        }

        /// <summary>
        /// Get CMS Pages
        /// </summary>
        public SecondaryPageResponse GetCMSPages(SecondaryPageRequestModel request)
        {
            return cmsPageRepository.GetCMSPages(request);
        }

        /// <summary>
        /// Get Cms Page By Id
        /// </summary>
        public CmsPage GetCmsPageById(long pageId)
        {
            return cmsPageRepository.Find(pageId);
        }
        public CompanyResponse GetCompanyById(long companyId)
        {
            return companyRepository.GetCompanyById(companyId);
        }

        public CompanyBaseResponse GetBaseData(long storeId)
        {
            return new CompanyBaseResponse
                   {
                       SystemUsers = systemUserRepository.GetAll(),
                       CompanyTerritories = companyTerritoryRepository.GetAllCompanyTerritories(storeId),
                       CompanyContactRoles = companyContactRoleRepository.GetAll(),
                       PageCategories = pageCategoryRepository.GetCmsSecondaryPageCategories(),
                       RegistrationQuestions = registrationQuestionRepository.GetAll(),
                       Addresses = addressRepository.GetAllDefaultAddressByStoreID(storeId),
                       PaymentMethods = paymentMethodRepository.GetAll().ToList(),
                       EmailEvents = emailEventRepository.GetAll(),
                       Widgets = widgetRepository.GetAll(),
                   };
        }
        public void SaveFile(string filePath, long companyId)
        {
            Company company = companyRepository.GetCompanyById(companyId).Company;
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
                return SaveNewCompany(companyModel.Company);
            }
            else
            {
                return UpdateCompany(companyModel, companyDbVersion);
            }
        }

        public long GetOrganisationId()
        {
            return companyRepository.OrganisationId;
        }

        public void UpdateAddressesOnCompanyContactUpdation(CompanySavingModel model)
        {
            var allCompanyAddresses = addressRepository.GetAllDefaultAddressByStoreID(model.Company.CompanyId);
            foreach (var companyContact in model.EdittedCompanyContacts)
            {
                if (companyContact.ShippingAddressId != null)
                {
                    foreach (var address in allCompanyAddresses)
                    {
                        if (address.TerritoryId == companyContact.TerritoryId)
                        {
                            address.IsDefaultShippingAddress = false;
                        }
                    }
                }
                var addressToUpdate = addressRepository.Find(companyContact.AddressId);
                addressToUpdate.IsDefaultShippingAddress = true;
            }
            foreach (var companyContact in model.NewAddedCompanyContacts)
            {
                if (companyContact.ShippingAddressId != null)
                {
                    foreach (var address in allCompanyAddresses)
                    {
                        if (address.TerritoryId == companyContact.TerritoryId)
                        {
                            address.IsDefaultShippingAddress = false;
                        }
                    }
                }
                var addressToUpdate = addressRepository.Find(companyContact.AddressId);
                addressToUpdate.IsDefaultShippingAddress = true;
            }
        }

        /// <summary>
        /// Get Cms Page Widget By Page Id
        /// </summary>
        public IEnumerable<CmsSkinPageWidget> GetCmsPageWidgetByPageId(long pageId, long companyId)
        {
            return cmsSkinPageWidgetRepository.GetByPageId(pageId, companyId);
        }
        #endregion
    }
}
