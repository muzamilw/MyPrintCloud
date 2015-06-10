using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Net.Mime;
using System.Web;
using Castle.Core.Internal;
using Microsoft.IdentityModel.SecurityTokenService;
using MPC.ExceptionHandling;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.ModelMappers;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using Ionic.Zip;
using System.IO;
using MPC.Repository.Repositories;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Net.Http.Headers;
using MPC.Common;
using Newtonsoft.Json.Linq;


namespace MPC.Implementation.MISServices
{
    public class CompanyService : ICompanyService
    {

        #region Private

        #region Repositories

        private readonly ICompanyRepository companyRepository;
        private readonly IEstimateRepository estimateRepository;
        private readonly ISystemUserRepository systemUserRepository;
        private readonly IRaveReviewRepository raveReviewRepository;
        private readonly ICompanyCMYKColorRepository companyCmykColorRepository;
        private readonly ITemplateColorStylesRepository templateColorStylesRepository;
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
        private readonly IGetItemsListViewRepository itemsListViewRepository;
        private readonly IItemRepository itemRepository;
        private readonly IItemStockOptionRepository itemStockOptionRepository;
        private readonly IPrefixRepository prefixRepository;
        private readonly IMarkupRepository markupRepository;
        private readonly IItemVdpPriceRepository itemVdpPriceRepository;
        private readonly IItemVideoRepository itemVideoRepository;
        private readonly IItemRelatedItemRepository itemRelatedItemRepository;
        private readonly ITemplatePageRepository templatePageRepository;
        private readonly ITemplateRepository templateRepository;
        private readonly IItemAddOnCostCentreRepository itemAddOnCostCentreRepository;
        private readonly ICostCentreRepository costCentreRepository;
        private readonly IStockItemRepository stockItemRepository;
        private readonly IItemPriceMatrixRepository itemPriceMatrixRepository;
        private readonly IItemStateTaxRepository itemStateTaxRepository;
        private readonly ICountryRepository countryRepository;
        private readonly IStateRepository stateRepository;
        private readonly ISectionFlagRepository sectionFlagRepository;
        private readonly IItemProductDetailRepository itemProductDetailRepository;
        private readonly ICompanyDomainRepository companyDomainRepository;
        private readonly ICostCentreMatrixRepository costCentreMatrixRepositry;
        private readonly ICostCentreQuestionRepository CostCentreQuestionRepository;
        private readonly IStockCategoryRepository StockCategoryRepository;
        private readonly IPaperSizeRepository PaperSizeRepository;
        private readonly IMachineRepository MachineRepository;
        private readonly IPhraseFieldRepository PhraseFieldRepository;
        private readonly IReportRepository ReportRepository;
        private readonly IFieldVariableRepository fieldVariableRepository;
        private readonly IVariableOptionRepository variableOptionRepository;
        private readonly IScopeVariableRepository scopeVariableRepository;
        private readonly ISmartFormRepository smartFormRepository;
        private readonly ISmartFormDetailRepository smartFormDetailRepository;
        private readonly IMediaLibraryRepository mediaLibraryRepository;
        private readonly ICompanyCostCenterRepository companyCostCenterRepository;
        private readonly ICmsTagReporistory cmsTagReporistory;
        private readonly ICompanyBannerSetRepository bannerSetRepository;
        private readonly MPC.Interfaces.WebStoreServices.ITemplateService templateService;
        private readonly ICampaignRepository campaignRepository;
        private readonly ITemplateFontsRepository templatefonts;

        #endregion

        private bool CheckDuplicateExistenceOfCompanyDomains(CompanySavingModel companySaving)
        {
            var allDomains = companyDomainRepository.GetAll();
            var itemMatched = false;
            foreach (var domain in allDomains)
            {
                if (companySaving.Company.CompanyDomains != null)
                {
                    foreach (var domainForSaving in companySaving.Company.CompanyDomains)
                    {
                        if (domainForSaving.CompanyDomainId == 0)
                        {
                            if (domainForSaving.Domain == domain.Domain)
                            {
                                throw new MPCException("There Exist Another Domain Name Instance in system for:" + domainForSaving.Domain, organisationRepository.OrganisationId);
                                //return false;
                            }
                        }
                    }
                }
            }
            return true;
            //var commonItem = companySaving.Company.CompanyDomains..Intersect(allCompanyDomains);
            //if (commonItem.Any())
            //{
            //    return false;
            //}

        }
        /// <summary>
        /// Save Company
        /// </summary>
        private Company SaveNewCompany(CompanySavingModel companySaving)
        {
            //companySaving.Company.CmsPages = companySaving.NewAddedCmsPages;
            if (companySaving.Company.SmartForms != null)
            {
                foreach (var smartForm in companySaving.Company.SmartForms)
                {
                    smartForm.OrganisationId = companyRepository.OrganisationId;
                }
            }
            companySaving.Company.OrganisationId = companyRepository.OrganisationId;
            companyRepository.Add(companySaving.Company);
            companyRepository.SaveChanges(); // TODO: Remove it from here
            var companyId = companySaving.Company.CompanyId;
            UpdateCompany(companySaving, companySaving.Company);
            return companySaving.Company;
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
                        item.OrganisationId = companyRepository.OrganisationId;
                        item.CompanyId = company.CompanyId;
                        companyDbVersion.RaveReviews.Add(item);
                    }
                }
            }
            //find missing items

            List<RaveReview> missingRaveReviews = new List<RaveReview>();
            // ReSharper disable once LoopCanBeConvertedToQuery
            if (companyDbVersion.RaveReviews != null)
            {
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
            if (companyDbVersion.PaymentGateways != null)
            {
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
        private Company UpdateCmykColorsOfUpdatingCompany(Company company, Company companyDbVersion)
        {
            //var companyDbVersion = companyRepository.Find(company.CompanyId);
            #region CMYK Colors Items
            //Add  CMYK Colors
            if (company.TemplateColorStyles != null)
            {
                foreach (var item in company.TemplateColorStyles)
                {
                    if (companyDbVersion.TemplateColorStyles.All(x => x.PelleteId != item.PelleteId && x.CustomerId != item.CustomerId))
                    {
                        item.CustomerId = company.CompanyId;
                        companyDbVersion.TemplateColorStyles.Add(item);
                    }
                }
            }
            //find missing items

            List<TemplateColorStyle> missingTemplateColorStyles = new List<TemplateColorStyle>();
            // ReSharper disable once LoopCanBeConvertedToQuery
            if (companyDbVersion.TemplateColorStyles != null)
            {


                foreach (TemplateColorStyle dbversionTemplateColorStyles in companyDbVersion.TemplateColorStyles)
                {
                    if (company.TemplateColorStyles != null && company.TemplateColorStyles.All(x => x.PelleteId != dbversionTemplateColorStyles.PelleteId &&
                        x.CustomerId != dbversionTemplateColorStyles.CustomerId))
                    {
                        missingTemplateColorStyles.Add(dbversionTemplateColorStyles);
                    }
                }

                //remove missing items
                foreach (TemplateColorStyle missingTemplateColorStyle in missingTemplateColorStyles)
                {

                    TemplateColorStyle dbVersionMissingItem = companyDbVersion.TemplateColorStyles.First(x => x.PelleteId == missingTemplateColorStyle.PelleteId &&
                        x.CustomerId == missingTemplateColorStyle.CustomerId);
                    //if (dbVersionMissingItem.PelleteId > 0)
                    //{
                    companyDbVersion.TemplateColorStyles.Remove(dbVersionMissingItem);
                    templateColorStylesRepository.Delete(dbVersionMissingItem);
                    //}
                }
            }
            if (company.TemplateColorStyles != null)
            {
                //updating Company CMYK Colors
                foreach (var TemplateColorStylesItem in company.TemplateColorStyles)
                {
                    templateColorStylesRepository.Update(TemplateColorStylesItem);
                }
            }
            #endregion
            return company;
        }
        private Company UpdateCompanyCostCentersOfUpdatingCompany(Company company, Company companyDbVersion)
        {

            #region Company Cost Centers
            //Add  Company Cost Centers
            if (company.CompanyCostCentres != null)
            {
                List<CompanyCostCentre> newlist = company.CompanyCostCentres.Where(
                    c => companyDbVersion.CompanyCostCentres.All(cc => cc.CostCentreId != c.CostCentreId)).ToList();
                //List<CompanyCostCentre> missingItemsList = companyDbVersion.CompanyCostCentres.Where(
                //    c => company.CompanyCostCentres.All(cc => cc.CostCentreId != c.CostCentreId)).ToList();
                foreach (var item in newlist)
                {
                    item.CompanyId = company.CompanyId;
                    item.OrganisationId = companyRepository.OrganisationId;
                    companyDbVersion.CompanyCostCentres.Add(item);
                }
            }
            if (company.CompanyCostCentres != null)
            {
                List<CompanyCostCentre> missingItemsList = companyDbVersion.CompanyCostCentres.Where(
                    c => company.CompanyCostCentres.All(cc => cc.CostCentreId != c.CostCentreId)).ToList();
                //remove missing items
                foreach (CompanyCostCentre missingCompanyCostCentre in missingItemsList)
                {
                    CompanyCostCentre dbVersionMissingItem = companyDbVersion.CompanyCostCentres.First(x => x.CostCentreId == missingCompanyCostCentre.CostCentreId && x.CompanyId == missingCompanyCostCentre.CompanyId);
                    companyCostCenterRepository.Delete(dbVersionMissingItem);
                    companyDbVersion.CompanyCostCentres.Remove(dbVersionMissingItem);
                    //company.CompanyCostCentres.Remove(dbVersionMissingItem);

                }
            }
            else if (company.CompanyCostCentres == null && companyDbVersion.CompanyCostCentres != null && companyDbVersion.CompanyCostCentres.Count > 0)
            {
                List<CompanyCostCentre> lisRemoveAllItemsList = companyDbVersion.CompanyCostCentres.ToList();
                foreach (CompanyCostCentre missingCompanyCostCentre in lisRemoveAllItemsList)
                {
                    CompanyCostCentre dbVersionMissingItem = companyDbVersion.CompanyCostCentres.First(x => x.CostCentreId == missingCompanyCostCentre.CostCentreId && x.CompanyId == missingCompanyCostCentre.CompanyId);
                    companyCostCenterRepository.Delete(dbVersionMissingItem);
                    companyDbVersion.CompanyCostCentres.Remove(dbVersionMissingItem);
                }
            }

            #endregion
            return company;
        }
        private Company UpdateCompanyDomain(Company company)
        {
            var companyDbVersion = companyRepository.Find(company.CompanyId);
            #region Company Domain
            //Add Company Domain
            if (company.CompanyDomains != null)
            {
                foreach (var item in company.CompanyDomains)
                {
                    if (companyDbVersion.CompanyDomains.All(x => x.CompanyDomainId != item.CompanyDomainId && x.CompanyId != item.CompanyId))
                    {
                        item.CompanyId = company.CompanyId;
                        companyDbVersion.CompanyDomains.Add(item);
                    }
                }
            }
            //find missing items

            List<CompanyDomain> missingCompanyDomains = new List<CompanyDomain>();
            // ReSharper disable once LoopCanBeConvertedToQuery
            if (companyDbVersion.CompanyDomains != null)
            {


                foreach (CompanyDomain dbversionCompanyDomain in companyDbVersion.CompanyDomains)
                {
                    if (company.CompanyDomains != null && company.CompanyDomains.All(x => x.CompanyDomainId != dbversionCompanyDomain.CompanyDomainId && x.CompanyId != dbversionCompanyDomain.CompanyDomainId))
                    {
                        missingCompanyDomains.Add(dbversionCompanyDomain);
                    }
                }

                //remove missing items
                foreach (CompanyDomain missingCompanyDomain in missingCompanyDomains)
                {

                    CompanyDomain dbVersionMissingItem = companyDbVersion.CompanyDomains.First(x => x.CompanyDomainId == missingCompanyDomain.CompanyDomainId && x.CompanyId == missingCompanyDomain.CompanyId);

                    companyDbVersion.CompanyDomains.Remove(dbVersionMissingItem);
                    companyDomainRepository.Delete(dbVersionMissingItem);

                }
            }
            if (company.CompanyDomains != null)
            {
                //updating Company Domains
                foreach (var companyDomain in company.CompanyDomains)
                {
                    companyDomainRepository.Update(companyDomain);
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
                    address.OrganisationId = addressRepository.OrganisationId;
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
            addressRepository.SaveChanges();
        }
        private void SaveProductCategoryThumbNailImage(ProductCategory productCategory)
        {
            var thumbNailFileBytes = new byte[] { };
            var imageFileBytes = new byte[] { };
            if (!string.IsNullOrEmpty(productCategory.ThumbNailBytes))
            {
                string base64 = productCategory.ThumbNailBytes.Substring(productCategory.ThumbNailBytes.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                thumbNailFileBytes = Convert.FromBase64String(base64);
            }
            if (!string.IsNullOrEmpty(productCategory.ImageBytes))
            {
                string base64Image = productCategory.ImageBytes.Substring(productCategory.ImageBytes.IndexOf(',') + 1);
                base64Image = base64Image.Trim('\0');
                imageFileBytes = Convert.FromBase64String(base64Image);
            }

            string directoryPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + productCategory.ProductCategoryId + "_" + StringHelper.SimplifyString(productCategory.CategoryName) + "/ProductCategories");
            if (directoryPath != null && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string savePath = directoryPath + "\\" + productCategory.ProductCategoryId + "_Thumbnail.png";
            if ((!string.IsNullOrEmpty(productCategory.ThumbnailPath)) && File.Exists(HttpContext.Current.Server.MapPath("~/" + productCategory.ThumbnailPath)))
            {
                File.Delete(productCategory.ThumbnailPath);
            }
            File.WriteAllBytes(savePath, thumbNailFileBytes);
            int indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
            productCategory.ThumbnailPath = savePath.Substring(indexOf, savePath.Length - indexOf);

            savePath = directoryPath + "\\" + productCategory.ProductCategoryId + "_Banner.png";
            if ((!string.IsNullOrEmpty(productCategory.ImagePath)) && File.Exists(HttpContext.Current.Server.MapPath("~/" + productCategory.ImagePath)))
            {
                File.Delete(productCategory.ImagePath);
            }
            File.WriteAllBytes(savePath, imageFileBytes);
            indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
            productCategory.ImagePath = savePath.Substring(indexOf, savePath.Length - indexOf);
        }

        #region Product
        /// <summary>
        /// Get By Id
        /// </summary>
        public Item GetById(long id)
        {
            if (id <= 0)
            {
                return null;
            }

            Item item = itemRepository.Find(id);

            if (item == null)
            {
                throw new MPCException(string.Format(CultureInfo.InvariantCulture, LanguageResources.ItemService_ItemNotFound, id), itemRepository.OrganisationId);
            }

            return item;
        }
        /// <summary>
        /// Create Item Vdp Price
        /// </summary>
        private ItemVdpPrice CreateItemVdpPrice()
        {
            ItemVdpPrice line = itemVdpPriceRepository.Create();
            itemVdpPriceRepository.Add(line);
            return line;
        }

        /// <summary>
        /// Delete Item Vdp Price
        /// </summary>
        private void DeleteItemVdpPrice(ItemVdpPrice line)
        {
            itemVdpPriceRepository.Delete(line);
        }

        /// <summary>
        /// Create Item Video
        /// </summary>
        private ItemVideo CreateItemVideo()
        {
            ItemVideo video = itemVideoRepository.Create();
            itemVideoRepository.Add(video);
            return video;
        }

        /// <summary>
        /// Delete Item Video
        /// </summary>
        private void DeleteItemVideo(ItemVideo video)
        {
            itemVideoRepository.Delete(video);
        }

        /// <summary>
        /// Create Item RelatedItem
        /// </summary>
        private ItemRelatedItem CreateItemRelatedItem()
        {
            ItemRelatedItem relatedItem = itemRelatedItemRepository.Create();
            itemRelatedItemRepository.Add(relatedItem);
            return relatedItem;
        }

        /// <summary>
        /// Delete Item RelatedItem
        /// </summary>
        private void DeleteItemRelatedItem(ItemRelatedItem relatedItem)
        {
            itemRelatedItemRepository.Delete(relatedItem);
        }

        /// <summary>
        /// Create Template
        /// </summary>
        private Template CreateTemplate()
        {
            Template template = templateRepository.Create();
            templateRepository.Add(template);
            return template;
        }

        /// <summary>
        /// Create Template Page
        /// </summary>
        private TemplatePage CreateTemplatePage()
        {
            TemplatePage relatedItem = templatePageRepository.Create();
            templatePageRepository.Add(relatedItem);
            return relatedItem;
        }

        /// <summary>
        /// Delete Template Page
        /// </summary>
        private void DeleteTemplatePage(TemplatePage relatedItem)
        {
            templatePageRepository.Delete(relatedItem);
        }

        /// <summary>
        /// Create Item Stock Option
        /// </summary>
        private ItemStockOption CreateItemStockOption()
        {
            ItemStockOption line = itemStockOptionRepository.Create();
            itemStockOptionRepository.Add(line);
            return line;
        }

        /// <summary>
        /// Delete Item Stock Option
        /// </summary>
        private void DeleteItemStockOption(ItemStockOption line)
        {
            itemStockOptionRepository.Delete(line);
        }

        /// <summary>
        /// Create Item Addon Cost Centre
        /// </summary>
        private ItemAddonCostCentre CreateItemAddonCostCentre()
        {
            ItemAddonCostCentre line = itemAddOnCostCentreRepository.Create();
            itemAddOnCostCentreRepository.Add(line);
            return line;
        }

        /// <summary>
        /// Delete Item Addon Cost Centre
        /// </summary>
        private void DeleteItemAddonCostCentre(ItemAddonCostCentre line)
        {
            itemAddOnCostCentreRepository.Delete(line);
        }

        /// <summary>
        /// Create Item State Tax
        /// </summary>
        private ItemStateTax CreateItemStateTax()
        {
            ItemStateTax line = itemStateTaxRepository.Create();
            itemStateTaxRepository.Add(line);
            return line;
        }

        /// <summary>
        /// Delete Item State Tax
        /// </summary>
        private void DeleteItemStateTax(ItemStateTax line)
        {
            itemStateTaxRepository.Delete(line);
        }

        /// <summary>
        /// Create Item Price Matrix
        /// </summary>
        private ItemPriceMatrix CreateItemPriceMatrix()
        {
            ItemPriceMatrix line = itemPriceMatrixRepository.Create();
            itemPriceMatrixRepository.Add(line);
            return line;
        }

        /// <summary>
        /// Create Item Product Detail
        /// </summary>
        private ItemProductDetail CreateItemProductDetail()
        {
            ItemProductDetail line = itemProductDetailRepository.Create();
            itemProductDetailRepository.Add(line);
            return line;
        }
        public Item SaveProduct(Item item)
        {
            // Get Db Version
            Item itemTarget = GetById(item.ItemId);

            // If New then Add, Update If Existing
            if (itemTarget == null)
            {
                // Gets Next Item Code and Increments it by 1
                string itemCode = prefixRepository.GetNextItemCodePrefix();
                itemTarget = itemRepository.Create();
                itemRepository.Add(itemTarget);
                itemTarget.ItemCreationDateTime = DateTime.Now;
                itemTarget.ItemCode = itemCode;
                itemTarget.CompanyId = item.CompanyId;
                itemTarget.OrganisationId = itemRepository.OrganisationId;
            }

            // Update
            item.UpdateTo(itemTarget, new ItemMapperActions
            {
                CreateItemVdpPrice = CreateItemVdpPrice,
                DeleteItemVdpPrice = DeleteItemVdpPrice,
                CreateItemVideo = CreateItemVideo,
                DeleteItemVideo = DeleteItemVideo,
                CreateItemRelatedItem = CreateItemRelatedItem,
                DeleteItemRelatedItem = DeleteItemRelatedItem,
                CreateTemplatePage = CreateTemplatePage,
                DeleteTemplatePage = DeleteTemplatePage,
                CreateTemplate = CreateTemplate,
                CreateItemStockOption = CreateItemStockOption,
                DeleteItemStockOption = DeleteItemStockOption,
                CreateItemAddonCostCentre = CreateItemAddonCostCentre,
                DeleteItemAddonCostCentre = DeleteItemAddonCostCentre,
                CreateItemStateTax = CreateItemStateTax,
                DeleteItemStateTax = DeleteItemStateTax,
                CreateItemPriceMatrix = CreateItemPriceMatrix,
                CreateItemProductDetail = CreateItemProductDetail
            });

            // Save Changes
            itemRepository.SaveChanges();

            // Load Properties if Any
            itemTarget = itemRepository.Find(itemTarget.ItemId);

            // Get Updated Minimum Price
            itemTarget.MinPrice = itemRepository.GetMinimumProductValue(itemTarget.ItemId);

            // Return Item
            return itemTarget;
        }
        #endregion
        private void UpdateProductsOfUpdatingCompany(CompanySavingModel companySavingModel)
        {
            if (companySavingModel.NewAddedProducts != null)
            {
                //Add New Products
                foreach (var product in companySavingModel.NewAddedProducts)
                {
                    product.CompanyId = companySavingModel.Company.CompanyId;
                    product.OrganisationId = itemRepository.OrganisationId;
                    var productSaved = SaveProduct(product);
                    //itemRepository.Add(product);
                    product.TemplateId = productSaved.TemplateId;
                    product.ItemId = productSaved.ItemId;
                    itemRepository.SaveChanges();
                    SaveStoreProductImage(product);
                }
            }
            if (companySavingModel.EdittedProducts != null)
                //Update Products
                foreach (var product in companySavingModel.EdittedProducts)
                {
                    product.CompanyId = companySavingModel.Company.CompanyId;
                    product.OrganisationId = itemRepository.OrganisationId;

                    var productSaved = SaveProduct(product);
                    //var prevProduct = itemRepository.Find(product.ItemId);
                    //updatePreviousImages(prevProduct, product);
                    product.TemplateId = productSaved.TemplateId;
                    SaveStoreProductImage(product);
                    itemRepository.Update(product);
                    itemRepository.SaveChanges();
                }
            if (companySavingModel.Deletedproducts != null)
                //Delete Products
                foreach (var product in companySavingModel.Deletedproducts)
                {
                    SaveProduct(product);

                    //var productToDelete = itemRepository.Find(product.ItemId);
                    //itemRepository.Delete(productToDelete);
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
                    productCategory.OrganisationId = productCategoryRepository.OrganisationId;
                    //SaveProductCategoryThumbNailImage(productCategory);
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
                    //SaveProductCategoryThumbNailImage(productCategory);
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
                    companyContact.CompanyId = companySavingModel.Company.CompanyId;
                    companyContact.OrganisationId = companyContactRepository.OrganisationId;
                    //companyContact.image = SaveCompanyContactProfileImage(companyContact);
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
                    companyContact.CompanyId = companySavingModel.Company.CompanyId;
                    //companyContact.image = SaveCompanyContactProfileImage(companyContact);
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
            companyContactRepository.SaveChanges();
        }
        //Territory
        private void UpdateTerritories(CompanySavingModel companySavingModel, Company companyDbVersion)
        {
            //Add
            if (companySavingModel.NewAddedCompanyTerritories != null)
            {
                if (companyDbVersion.CompanyTerritories == null)
                {
                    companyDbVersion.CompanyTerritories = new Collection<CompanyTerritory>();
                }
                foreach (var territory in companySavingModel.NewAddedCompanyTerritories)
                {
                    companyDbVersion.CompanyTerritories.Add(territory);
                }
            }
            //Edit
            if (companySavingModel.EdittedCompanyTerritories != null)
            {
                foreach (var territory in companySavingModel.EdittedCompanyTerritories)
                {
                    companyTerritoryRepository.Update(territory);
                }
            }
            //Delete
            if (companySavingModel.DeletedCompanyTerritories != null)
            {
                foreach (var territory in companySavingModel.DeletedCompanyTerritories)
                {
                    companyDbVersion.CompanyTerritories.Remove(territory);
                    var territoryToDelete = companyTerritoryRepository.Find(territory.TerritoryId);
                    companyTerritoryRepository.Delete(territoryToDelete);
                }
            }
        }
        //Address
        private void UpdateAddresses(CompanySavingModel companySavingModel, Company companyDbVersion)
        {
            //Add
            if (companySavingModel.NewAddedAddresses != null)
            {
                if (companyDbVersion.Addresses == null)
                {
                    companyDbVersion.Addresses = new Collection<Address>();
                }
                foreach (var address in companySavingModel.NewAddedAddresses)
                {
                    address.OrganisationId = addressRepository.OrganisationId;
                    companyDbVersion.Addresses.Add(address);
                }
            }
            //Edit
            if (companySavingModel.EdittedAddresses != null)
            {
                foreach (var address in companySavingModel.EdittedAddresses)
                {
                    addressRepository.Update(address);
                }
            }
            //Delete
            if (companySavingModel.DeletedAddresses != null)
            {
                foreach (var address in companySavingModel.DeletedAddresses)
                {
                    companyDbVersion.Addresses.Remove(address);
                    var addressToDelete = addressRepository.Find(address.AddressId);
                    addressRepository.Delete(addressToDelete);
                }
            }
        }
        //Company Contact
        private void UpdateCompanyContacts(CompanySavingModel companySavingModel, Company companyDbVersion)
        {
            //Add
            if (companySavingModel.NewAddedCompanyContacts != null)
            {
                if (companyDbVersion.CompanyContacts == null)
                {
                    companyDbVersion.CompanyContacts = new Collection<CompanyContact>();
                }
                foreach (var companyContacts in companySavingModel.NewAddedCompanyContacts)
                {
                    companyContacts.OrganisationId = companyContactRepository.OrganisationId;
                    companyContacts.Password = HashingManager.ComputeHashSHA1(companyContacts.Password);
                    companyDbVersion.CompanyContacts.Add(companyContacts);
                }
            }
            //Edit
            if (companySavingModel.EdittedCompanyContacts != null)
            {
                foreach (var companyContact in companySavingModel.EdittedCompanyContacts)
                {
                    companyContact.Password = HashingManager.ComputeHashSHA1(companyContact.Password);
                    companyContactRepository.Update(companyContact);
                }
            }
            //Delete
            if (companySavingModel.DeletedCompanyContacts != null)
            {
                foreach (var companyContact in companySavingModel.DeletedCompanyContacts)
                {
                    companyDbVersion.CompanyContacts.Remove(companyContact);
                    var companyContactToDelete = companyContactRepository.Find(companyContact.ContactId);
                    companyContactRepository.Delete(companyContactToDelete);
                }
            }
        }
        /// <summary>
        /// Update Company
        /// </summary>
        private Company UpdateCompany(CompanySavingModel companySavingModel, Company companyDbVersion)
        {
            companySavingModel.Company.OrganisationId = companyRepository.OrganisationId;
            companyDbVersion.OrganisationId = companyRepository.OrganisationId;
            UpdateStoreWorkflowImage(companySavingModel, companyDbVersion); // under work
            UpdateStoreMapImage(companySavingModel, companyDbVersion); // under work
            List<CompanyDomain> companyDomainsDbVersion = companyDbVersion.CompanyDomains != null ? companyDbVersion.CompanyDomains.ToList() : null;
            var companyToBeUpdated = UpdateRaveReviewsOfUpdatingCompany(companySavingModel.Company);
            companyToBeUpdated = UpdatePaymentGatewaysOfUpdatingCompany(companyToBeUpdated);
            companyToBeUpdated = UpdateCmykColorsOfUpdatingCompany(companyToBeUpdated, companyDbVersion);
            companyToBeUpdated = UpdateCompanyCostCentersOfUpdatingCompany(companyToBeUpdated, companyDbVersion);
            companyToBeUpdated = UpdateCompanyDomain(companyToBeUpdated);


            UpdateTerritories(companySavingModel, companyDbVersion);
            UpdateAddresses(companySavingModel, companyDbVersion);
            UpdateCompanyContacts(companySavingModel, companyDbVersion);
            //UpdateSecondaryPagesCompany(companySavingModel, companyDbVersion);
            UpdateCampaigns(companySavingModel, companyDbVersion);
            UpdateCmsSkinPageWidget(companySavingModel.CmsPageWithWidgetList, companyDbVersion);
            if (companyToBeUpdated.ImageBytes != null)
            {
                companySavingModel.Company.Image = SaveCompanyProfileImage(companySavingModel.Company);
            }
            else
            {
                companySavingModel.Company.Image = companyDbVersion.Image;
            }
            companyRepository.Update(companyToBeUpdated);
            companyRepository.Update(companySavingModel.Company);
            UpdateCmsOffers(companySavingModel.Company, companyDbVersion);
            UpdateMediaLibrary(companySavingModel.Company, companyDbVersion);
            BannersUpdate(companySavingModel.Company, companyDbVersion);
            companyRepository.SaveChanges();
            //Save Files
            SaveSpriteImage(companySavingModel.Company);
            SaveCompanyCss(companySavingModel.Company);
            UpdateMediaLibraryFilePath(companySavingModel.Company, companyDbVersion);
            UpdateContactProfileImage(companySavingModel, companyDbVersion);

            SaveCompanyBannerImages(companySavingModel.Company, companyDbVersion);
            SaveStoreBackgroundImage(companySavingModel.Company, companyDbVersion);
            //UpdateSecondaryPageImagePath(companySavingModel, companyDbVersion);
            UpdateCampaignImages(companySavingModel, companyDbVersion);


            //UpdateSmartFormVariableIds(companySavingModel.Company.SmartForms, companyDbVersion);

            //UpdateScopeVariables(companySavingModel);
            if (companySavingModel.Company.ActiveBannerSetId < 0)
            {
                CompanyBannerSet companyBannerSet =
                    companySavingModel.Company.CompanyBannerSets.FirstOrDefault(
                        cbs => cbs.FakeId == companySavingModel.Company.ActiveBannerSetId);
                if (companyBannerSet != null)
                {
                    companyDbVersion.ActiveBannerSetId = companyBannerSet.CompanySetId;
                }
            }
            companyRepository.SaveChanges();
            //Call Service to add or remove the IIS Bindings for Store Domains
            updateDomainsInIIS(companyDbVersion.CompanyDomains, companyDomainsDbVersion);

            // Get List of Skins 
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string url = "WebstoreApi/StoreCache/Get?id=" + companyDbVersion.CompanyId;
                var response = client.GetAsync(url);
                if (!response.Result.IsSuccessStatusCode)
                {
                    //throw new MPCException("Failed to clear store cache", companyRepository.OrganisationId);
                }
            }
            return companySavingModel.Company;
        }


        /// <summary>
        /// Update Scope Variables
        /// </summary>
        private void UpdateScopeVariables(CompanySavingModel companySavingModel)
        {
            if (companySavingModel.Company.CompanyContacts != null)
            {
                foreach (CompanyContact companyContact in companySavingModel.Company.CompanyContacts)
                {
                    if (companyContact.ScopeVariables != null)
                    {
                        foreach (ScopeVariable scopeVariable in companyContact.ScopeVariables)
                        {
                            if (scopeVariable.ScopeVariableId == 0)
                            {
                                FieldVariable fieldVariable = companySavingModel.Company.FieldVariables.FirstOrDefault(
                               f => f.FakeIdVariableId == scopeVariable.FakeVariableId);
                                if (fieldVariable != null)
                                {
                                    scopeVariable.VariableId = fieldVariable.VariableId;
                                }

                                scopeVariable.Id = companyContact.ContactId;
                                scopeVariableRepository.Add(scopeVariable);
                            }
                        }
                    }
                }

            }

            if (companySavingModel.Company.CompanyTerritories != null)
            {
                foreach (CompanyTerritory companyTerritory in companySavingModel.Company.CompanyTerritories)
                {
                    if (companyTerritory.ScopeVariables != null)
                    {
                        foreach (ScopeVariable scopeVariable in companyTerritory.ScopeVariables)
                        {
                            if (scopeVariable.ScopeVariableId == 0)
                            {
                                FieldVariable fieldVariable = companySavingModel.Company.FieldVariables.FirstOrDefault(
                               f => f.FakeIdVariableId == scopeVariable.FakeVariableId);
                                if (fieldVariable != null)
                                {
                                    scopeVariable.VariableId = fieldVariable.VariableId;
                                }

                                scopeVariable.Id = companyTerritory.TerritoryId;
                                scopeVariable.Scope = (int)FieldVariableScopeType.Territory;
                                scopeVariableRepository.Add(scopeVariable);
                            }
                        }
                    }
                }

            }

            //Address Scope variables
            if (companySavingModel.Company.Addresses != null)
            {
                foreach (Address address in companySavingModel.Company.Addresses)
                {
                    if (address.ScopeVariables != null)
                    {
                        foreach (ScopeVariable scopeVariable in address.ScopeVariables)
                        {
                            if (scopeVariable.ScopeVariableId == 0)
                            {
                                FieldVariable fieldVariable = companySavingModel.Company.FieldVariables.FirstOrDefault(
                               f => f.FakeIdVariableId == scopeVariable.FakeVariableId);
                                if (fieldVariable != null)
                                {
                                    scopeVariable.VariableId = fieldVariable.VariableId;
                                }

                                scopeVariable.Id = address.AddressId;
                                scopeVariable.Scope = (int)FieldVariableScopeType.Address;
                                scopeVariableRepository.Add(scopeVariable);
                            }
                        }
                    }
                }
            }

            //Store Scope Variables
            if (companySavingModel.Company.ScopeVariables != null)
            {
                IEnumerable<ScopeVariable> scopeVariables = scopeVariableRepository.GetContactVariableByContactId(companySavingModel.Company.CompanyId, (int)FieldVariableScopeType.Store);

                foreach (ScopeVariable scopeVariable in companySavingModel.Company.ScopeVariables)
                {
                    if (scopeVariable.ScopeVariableId == 0)
                    {
                        FieldVariable fieldVariable = companySavingModel.Company.FieldVariables.FirstOrDefault(
                       f => f.FakeIdVariableId == scopeVariable.FakeVariableId);
                        if (fieldVariable != null)
                        {
                            scopeVariable.VariableId = fieldVariable.VariableId;
                        }

                        scopeVariable.Id = companySavingModel.Company.CompanyId;
                        scopeVariable.Scope = (int)FieldVariableScopeType.Store;
                        scopeVariableRepository.Add(scopeVariable);
                    }
                    else
                    {
                        ScopeVariable scopeVariableDbItem = scopeVariables.FirstOrDefault(scv => scv.ScopeVariableId == scopeVariable.ScopeVariableId);
                        if (scopeVariableDbItem != null)
                        {
                            scopeVariableDbItem.Value = scopeVariable.Value;
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Update Smart Form variable Ids
        /// </summary>
        private void UpdateSmartFormVariableIds(IEnumerable<SmartForm> smartForms, Company companyDbVersion)
        {
            if (companyDbVersion.SmartForms != null && companyDbVersion.FieldVariables != null)
            {
                foreach (var smartForm in companyDbVersion.SmartForms)
                {
                    if (smartForm.SmartFormDetails != null)
                        foreach (var smartFormDetail in smartForm.SmartFormDetails)
                        {
                            if (smartFormDetail.FakeVariableId != null)
                            {
                                FieldVariable fieldVariable = companyDbVersion.FieldVariables.FirstOrDefault(
                                fv => fv.FakeIdVariableId == smartFormDetail.FakeVariableId);
                                if (fieldVariable != null)
                                    smartFormDetail.VariableId = fieldVariable.VariableId;
                            }

                        }

                }
            }
        }
        // ReSharper disable once InconsistentNaming
        private void updateDomainsInIIS(IEnumerable<CompanyDomain> companySavedDomains, IEnumerable<CompanyDomain> companyDbVersion)
        {
            //var companyDbVersion = companySavedDomains;
            #region Company Domain
            #region Add Company Domain
            if (companySavedDomains != null)
            {
                foreach (var item in companySavedDomains)
                {
                    if (companyDbVersion.All(x => x.CompanyDomainId != item.CompanyDomainId))
                    {
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["AddDomainPath"]);
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            string mySiteUrl = HttpContext.Current.Request.Url.Host;
                            string url = "AddDomain?siteName=" + mySiteUrl + "&domainName=" + item.Domain + "&isRemoving=" + false;
                            string responsestr = "";
                            var response = client.GetAsync(url);
                            if (response.Result.IsSuccessStatusCode)
                            {
                            }
                        }
                    }
                }
            }
            #endregion Add Company Domain
            //find missing items

            List<CompanyDomain> missingCompanyDomains = new List<CompanyDomain>();
            // ReSharper disable once LoopCanBeConvertedToQuery
            if (companyDbVersion != null)
            {
                foreach (CompanyDomain dbversionCompanyDomain in companyDbVersion)
                {
                    if (companySavedDomains != null && companySavedDomains.All(x => x.CompanyDomainId != dbversionCompanyDomain.CompanyDomainId))// && x.CompanyId != dbversionCompanyDomain.CompanyDomainId
                    {
                        missingCompanyDomains.Add(dbversionCompanyDomain);
                    }
                }

                //remove missing items
                foreach (CompanyDomain missingCompanyDomain in missingCompanyDomains)
                {

                    CompanyDomain dbVersionMissingItem = companyDbVersion.First(x => x.CompanyDomainId == missingCompanyDomain.CompanyDomainId);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["AddDomainPath"]);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        string mySiteUrl = HttpContext.Current.Request.Url.Host;
                        string url = "AddDomain?siteName=" + mySiteUrl + "&domainName=" + dbVersionMissingItem.Domain + "&isRemoving=" + true;
                        string responsestr = "";
                        var response = client.GetAsync(url);
                        if (response.Result.IsSuccessStatusCode)
                        {
                        }
                    }
                }
            }
            #endregion

        }
        public void UpdateCampaignImages(CompanySavingModel companySavingModel, Company companyDbVersion)
        {
            if (companyDbVersion.Campaigns != null)
            {
                foreach (var campaign in companyDbVersion.Campaigns)
                {
                    if (campaign.CampaignImages != null)
                    {
                        foreach (var campaignImage in campaign.CampaignImages)
                        {
                            if (campaignImage.ImageByteSource != null)
                            {
                                campaignImage.ImagePath = SaveCampaignImage(campaignImage, companyDbVersion.CompanyId);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Save Campaign Image
        /// </summary>
        private string SaveCampaignImage(CampaignImage campaignImage, long companyId)
        {
            if (campaignImage.ImageByteSource != null)
            {
                string base64 = campaignImage.ImageByteSource.Substring(campaignImage.ImageByteSource.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);
                string directoryPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + companyId + "/CampaignImages");
                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string savePath = directoryPath + "\\" + campaignImage.CampaignImageId + "_Campaign.png";
                if (!File.Exists(savePath))
                {
                    File.WriteAllBytes(savePath, data);
                }
                int indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                return savePath;
            }
            return null;
        }
        private void UpdateContactProfileImage(CompanySavingModel companySavingModel, Company companyDbVersion)
        {
            //if (companySavingModel.NewAddedCompanyContacts != null)
            if (companyDbVersion.CompanyContacts != null)
            {
                //Add New companyContacts
                //foreach (var companyContact in companySavingModel.NewAddedCompanyContacts)
                foreach (var companyContact in companyDbVersion.CompanyContacts)
                {
                    string path = SaveCompanyContactProfileImage(companyContact);
                    if (path != null)
                        companyContact.image = SaveCompanyContactProfileImage(companyContact);
                }
            }
            if (companySavingModel.EdittedCompanyContacts != null)
            {
                //Update companyContacts
                //foreach (var companyContact in companySavingModel.EdittedCompanyContacts)        
                //foreach (var companyContact in companyDbVersion.CompanyContacts)
                //{
                //    if (File.Exists(companyContact.image))
                //    {
                //        //If already image exist
                //        File.Delete(companyContact.image);
                //    }
                //    companyContact.image = SaveCompanyContactProfileImage(companyContact, companyDbVersion);
                //}
            }
        }
        private void UpdateStoreWorkflowImage(CompanySavingModel companySavingModel, Company companyDbVersion)
        {
            if (companySavingModel.Company.isTextWatermark == false)
            {
                string path = SaveStoreWorkflowImage(companySavingModel);
                if (path != null)
                {
                    companyDbVersion.WatermarkText = path;
                }
                else
                {
                    companySavingModel.Company.WatermarkText = companyDbVersion.WatermarkText;
                }
            }
        }
        private void UpdateStoreMapImage(CompanySavingModel companySavingModel, Company companyDbVersion)
        {
            if (companySavingModel.Company.isShowGoogleMap == 3)
            {
                string path = SaveMapImage(companySavingModel);
                if (path != null)
                {
                    companyDbVersion.MapImageUrl = path;
                }
                else
                {
                    companySavingModel.Company.MapImageUrl = companyDbVersion.MapImageUrl;
                }
            }
        }

        /// <summary>
        /// Update Media Library File Path
        /// </summary>
        private void UpdateMediaLibraryFilePath(Company company, Company companyDbVersion)
        {
            if (company.MediaLibraries != null)
            {
                string directoryPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Media/" + companyRepository.OrganisationId + "/" + company.CompanyId);

                foreach (var item in company.MediaLibraries)
                {
                    if (!item.FakeId.IsNullOrEmpty())
                    {
                        if (item.FileSource != null)
                        {
                            string base64 = item.FileSource.Substring(item.FileSource.IndexOf(',') + 1);
                            base64 = base64.Trim('\0');
                            byte[] data = Convert.FromBase64String(base64);

                            if (directoryPath != null && !Directory.Exists(directoryPath))
                            {
                                Directory.CreateDirectory(directoryPath);
                            }
                            string savePath = directoryPath + "\\" + item.MediaId + "_" + item.FileName;
                            if (!File.Exists(savePath))
                            {
                                File.WriteAllBytes(savePath, data);
                                MediaLibrary mediaLibraryDbVersion =
                                    companyDbVersion.MediaLibraries.FirstOrDefault(m => m.MediaId == item.MediaId);
                                if (mediaLibraryDbVersion != null)
                                {
                                    int indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                                    savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                                    mediaLibraryDbVersion.FilePath = savePath;
                                }
                            }
                        }
                    }
                }
                //  companyRepository.SaveChanges();
            }
        }

        /// <summary>
        /// Update Media Library
        /// </summary>
        private void UpdateMediaLibrary(Company company, Company companyDbVersion)
        {
            if (company.MediaLibraries != null)
            {
                foreach (var item in company.MediaLibraries)
                {
                    //New Added Files
                    if (item.MediaId < 0)
                    {
                        companyDbVersion.MediaLibraries.Add(item);
                    }
                }
            }
        }

        /// <summary>
        /// Save/Update Company/Store CSS
        /// </summary>
        private void SaveCompanyCss(Company company)
        {
            string directoryPath =
                HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" +
                                                   company.CompanyId);
            if (directoryPath != null && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string savePath = directoryPath + "\\site.css";
            File.WriteAllText(savePath, company.CustomCSS);
        }

        /// <summary>
        /// Save Sprite Image
        /// </summary>
        private void SaveSpriteImage(Company company)
        {
            string directoryPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + company.CompanyId);

            //Save user Defined Sprite File
            if (company.UserDefinedSpriteSource != null)
            {
                string base64 = company.UserDefinedSpriteSource.Substring(company.UserDefinedSpriteSource.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);

                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string savePath = directoryPath + "\\sprite.png";

                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                }
                File.WriteAllBytes(savePath, data);
            }
        }

        /// <summary>
        /// Update Cms Offer
        /// </summary>
        private void UpdateCmsOffers(Company company, Company companyDbVersion)
        {
            #region Update Cms Offer

            if (company.CmsOffers != null)
            {
                foreach (var cmsOffer in company.CmsOffers)
                {
                    if (companyDbVersion.CmsOffers == null)
                    {
                        List<CmsOffer> cmsOffers = new List<CmsOffer>();
                        companyDbVersion.CmsOffers = cmsOffers;

                    }
                    //Update
                    if (cmsOffer.OfferId > 0)
                    {
                        CmsOffer cmsOfferDbVesrion = companyDbVersion.CmsOffers.FirstOrDefault(c => c.OfferId == cmsOffer.OfferId);
                        if (cmsOfferDbVesrion != null)
                        {
                            cmsOfferDbVesrion.SortOrder = cmsOffer.SortOrder;
                            cmsOfferDbVesrion.ItemName = cmsOffer.ItemName;
                        }
                    }
                    else
                    {
                        //New Added
                        cmsOffer.CompanyId = company.CompanyId;
                        companyDbVersion.CmsOffers.Add(cmsOffer);
                    }
                }
            }
            #endregion

            #region Delete Cms Offer
            List<CmsOffer> missingCmsOffers = new List<CmsOffer>();
            if (companyDbVersion.CmsOffers != null)
            {
                foreach (var offerDbItem in companyDbVersion.CmsOffers)
                {
                    if (company.CmsOffers != null && company.CmsOffers.All(c => c.OfferId != offerDbItem.OfferId))
                    {
                        missingCmsOffers.Add(offerDbItem);
                    }
                    else if (company.CmsOffers == null)
                    {
                        missingCmsOffers.Add(offerDbItem);
                    }
                }

                foreach (var misiingItem in missingCmsOffers)
                {
                    companyDbVersion.CmsOffers.Remove(misiingItem);
                }
            }

            #endregion
        }

        /// <summary>
        /// Save Store Background Image
        /// </summary>
        private void SaveStoreBackgroundImage(Company company, Company companyDbVersion)
        {


            string directoryPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + companyDbVersion.CompanyId);
            string savePath = directoryPath + "\\background.png";
            if (company.StoreBackgroundFile != null)
            {
                string base64 = company.StoreBackgroundFile.Substring(company.StoreBackgroundFile.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);

                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                }

                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                companyDbVersion.StoreBackgroundImage = savePath;
            }
            else
            {
                if (HttpContext.Current.Server.MapPath("~/" + company.StoreBackgroundImage) != savePath)
                {
                    string mediaFilePath = HttpContext.Current.Server.MapPath("~/" + company.StoreBackgroundImage);
                    if (File.Exists(savePath))
                    {
                        File.Delete(savePath);
                    }
                    if (File.Exists(mediaFilePath))
                    {
                        File.Copy(mediaFilePath, savePath, true);
                        int indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                        savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                        companyDbVersion.StoreBackgroundImage = savePath;
                    }
                }

            }
        }

        /// <summary>
        /// Update Secondary Page Images Path
        /// </summary>
        private void UpdateSecondaryPageImagePath(CompanySavingModel companySavingModel, Company companyDbVersion)
        {
            // Return if no Media is Selected
            if (companySavingModel == null || companySavingModel.Company == null || companySavingModel.Company.MediaLibraries == null)
            {
                return;
            }

            if (companyDbVersion.CmsPages != null)
            {
                foreach (var itemDbVersion in companyDbVersion.CmsPages)
                {
                    foreach (var media in companySavingModel.Company.MediaLibraries)
                    {
                        if (media.FakeId == itemDbVersion.PageBanner)
                        {
                            itemDbVersion.PageBanner = media.FilePath;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Update Color Pallete
        /// </summary>
        private void UpdateColorPallete(Company company, Company companyDbVersion)
        {
            if (company.ColorPalletes != null)
            {

                foreach (var colorPalleteItem in company.ColorPalletes)
                {
                    if (companyDbVersion.ColorPalletes == null)
                    {
                        List<ColorPallete> colorPalletes = new List<ColorPallete>();
                        companyDbVersion.ColorPalletes = colorPalletes;

                    }
                    if (colorPalleteItem.PalleteId == 0)
                    {
                        companyDbVersion.ColorPalletes.Add(colorPalleteItem);
                    }
                    else
                    {
                        ColorPallete colorPallete =
                            companyDbVersion.ColorPalletes.FirstOrDefault(c => c.PalleteId == colorPalleteItem.PalleteId);
                        if (colorPallete != null)
                        {
                            colorPallete.Color1 = colorPalleteItem.Color1;
                            colorPallete.Color2 = colorPalleteItem.Color2;
                            colorPallete.Color3 = colorPalleteItem.Color3;
                            colorPallete.Color4 = colorPalleteItem.Color4;
                            colorPallete.Color5 = colorPalleteItem.Color5;
                            colorPallete.Color6 = colorPalleteItem.Color6;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Update CMS Skin Page Widget
        /// </summary>
        private void UpdateCmsSkinPageWidget(IEnumerable<CmsPageWithWidgetList> cmsPageWithWidgetList, Company companyDbVersion)
        {

            if (cmsPageWithWidgetList != null)
            {
                if (companyDbVersion.CmsSkinPageWidgets == null)
                {
                    companyDbVersion.CmsSkinPageWidgets = new Collection<CmsSkinPageWidget>();
                }
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
                        //cmsSkinPageWidgetRepository.SaveChanges();
                    }
                }
                #endregion
            }




        }
        /// <summary>
        /// Update Campaigns
        /// </summary>
        private void UpdateCampaigns(CompanySavingModel companySavingModel, Company companyDbVersion)
        {

            #region New Campaigns

            if (companySavingModel.NewAddedCampaigns != null)
            {
                if (companyDbVersion.Campaigns == null)
                {
                    companyDbVersion.Campaigns = new Collection<Campaign>();
                }
                foreach (Campaign campaign in companySavingModel.NewAddedCampaigns)
                {
                    //New Added
                    if (campaign.CampaignId == 0)
                    {
                        campaign.CompanyId = companyDbVersion.CompanyId;
                        campaign.OrganisationId = companyRepository.OrganisationId;
                        companyDbVersion.Campaigns.Add(campaign);
                    }
                }
            }
            #endregion

            #region update Campaign

            if (companySavingModel.EdittedCampaigns != null)
            {

                foreach (var campaign in companySavingModel.EdittedCampaigns)
                {
                    #region Campaign


                    Campaign campaignDbItem =
                        companyDbVersion.Campaigns.FirstOrDefault(c => c.CampaignId == campaign.CampaignId);
                    if (campaignDbItem != null)
                    {
                        campaignDbItem.CampaignName = campaign.CampaignName;
                        campaignDbItem.Description = campaign.Description;
                        campaignDbItem.CampaignType = campaign.CampaignType;
                        campaignDbItem.IsEnabled = campaign.IsEnabled;
                        campaignDbItem.IncludeCustomers = campaign.IncludeCustomers;
                        campaignDbItem.IncludeSuppliers = campaign.IncludeSuppliers;
                        campaignDbItem.IncludeProspects = campaign.IncludeProspects;
                        campaignDbItem.IncludeNewsLetterSubscribers = campaign.IncludeNewsLetterSubscribers;
                        campaignDbItem.IncludeFlag = campaign.IncludeFlag;
                        campaignDbItem.FlagIDs = campaign.FlagIDs;
                        campaignDbItem.CustomerTypeIDs = campaign.CustomerTypeIDs;
                        campaignDbItem.GroupIDs = campaign.GroupIDs;
                        campaignDbItem.SubjectA = campaign.SubjectA;
                        campaignDbItem.HTMLMessageA = campaign.HTMLMessageA;
                        campaignDbItem.StartDateTime = campaign.StartDateTime;
                        campaignDbItem.FromAddress = campaign.FromAddress;
                        campaignDbItem.ReturnPathAddress = campaign.ReturnPathAddress;
                        campaignDbItem.ReplyToAddress = campaign.ReplyToAddress;
                        campaignDbItem.EmailLogFileAddress2 = campaign.EmailLogFileAddress2;
                        campaignDbItem.EmailEvent = campaign.EmailEvent;
                        campaignDbItem.SendEmailAfterDays = campaign.SendEmailAfterDays;
                        campaignDbItem.FromName = campaign.FromName;
                        campaignDbItem.IncludeType = campaign.IncludeType;
                        campaignDbItem.IncludeCorporateCustomers = campaign.IncludeCorporateCustomers;
                        campaignDbItem.EnableLogFiles = campaign.EnableLogFiles;
                        campaignDbItem.EmailLogFileAddress3 = campaign.EmailLogFileAddress3;
                    }


                    #endregion

                    #region Campain Image

                    if (campaign.CampaignImages != null)
                    {
                        foreach (var campaignImage in campaign.CampaignImages)
                        {
                            Campaign campaignDbItem1 =
                            companyDbVersion.Campaigns.FirstOrDefault(c => c.CampaignId == campaign.CampaignId);
                            if (campaignImage.CampaignImageId == 0 && campaignDbItem1 != null)
                            {
                                campaignImage.CampaignId = campaignDbItem1.CampaignId;
                                campaignDbItem1.CampaignImages.Add(campaignImage);
                            }
                        }
                    }
                    #endregion
                }
            }

            #endregion

            #region Delete Campaigns
            {
                if (companySavingModel.DeletedCampaigns != null)
                {
                    foreach (Campaign campaignItem in companySavingModel.DeletedCampaigns)
                    {
                        Campaign missingCampaignItem =
                          companyDbVersion.Campaigns.FirstOrDefault(c => c.CampaignId == campaignItem.CampaignId);
                        if (missingCampaignItem != null)
                        {
                            companyDbVersion.Campaigns.Remove(missingCampaignItem);
                        }
                    }
                }

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
                    if (companyDbVersion.CmsPages == null)
                    {
                        List<CmsPage> cmsPages = new List<CmsPage>();
                        companyDbVersion.CmsPages = cmsPages;
                    }
                    item.PageId = 0;
                    item.CompanyId = companySavingModel.Company.CompanyId;
                    item.OrganisationId = companyRepository.OrganisationId;
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
                            dbItem.isEnabled = item.isEnabled;
                            dbItem.PageKeywords = item.PageKeywords;
                            dbItem.PageTitle = item.PageTitle;
                            dbItem.PageBanner = item.PageBanner;
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
                    //cmsPageRepository.SaveChanges();
                }
            }
            // companyRepository.SaveChanges();

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
                //pageCategoryRepository.SaveChanges();
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
                    if (companyDbVersion.CompanyBannerSets == null)
                    {
                        companyDbVersion.CompanyBannerSets = new Collection<CompanyBannerSet>();
                    }
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
                            {
                                CompanyBanner bannerDbVersionItem = null;
                                if (companyDbVersion.CompanyBannerSets != null)
                                {
                                    foreach (var bannerSetItem in companyDbVersion.CompanyBannerSets)
                                    {
                                        if (bannerSetItem.CompanyBanners != null && bannerDbVersionItem == null)
                                        {
                                            bannerDbVersionItem = bannerSetItem.CompanyBanners.FirstOrDefault(
                                        x => x.CompanyBannerId == item.CompanyBannerId);
                                        }
                                    }
                                }
                                if (bannerDbVersionItem != null)
                                {

                                    bannerDbVersionItem.Heading = item.Heading;
                                    bannerDbVersionItem.ButtonURL = item.ButtonURL;
                                    bannerDbVersionItem.ItemURL = item.ItemURL;
                                    bannerDbVersionItem.CompanySetId = item.CompanySetId;
                                    bannerDbVersionItem.Description = item.Description;
                                    bannerDbVersionItem.CompanySetId = item.CompanySetId;
                                    bannerDbVersionItem.ImageURL = item.ImageURL;
                                    bannerDbVersionItem = null;
                                }

                            }
                        }
                    }

                }
            }//End Add/Edit 
            #endregion




        }

        /// <summary>
        /// Save Company Banner Images
        /// </summary>
        private void SaveCompanyBannerImages(Company company, Company companyDbVersion)
        {
            if (company.CompanyBannerSets != null)
            {
                foreach (var item in company.CompanyBannerSets)
                {
                    if (item.CompanyBanners != null && company.MediaLibraries != null)
                        foreach (var banner in item.CompanyBanners)
                        {
                            foreach (var media in company.MediaLibraries)
                            {
                                if (media.FakeId != null && media.FakeId == banner.ImageURL)
                                {
                                    CompanyBannerSet companyBannerSetDbVersion = companyDbVersion.CompanyBannerSets.FirstOrDefault(
                                        cbs => cbs.CompanySetId == item.CompanySetId);
                                    CompanyBanner companyBannerDbVersion = companyBannerSetDbVersion != null
                                         ? companyBannerSetDbVersion.CompanyBanners.FirstOrDefault(
                                             b => b.CompanyBannerId == banner.CompanyBannerId)
                                         : null;
                                    if (companyBannerDbVersion != null)
                                        companyBannerDbVersion.ImageURL = media.FilePath;
                                }
                            }
                        }
                }
            }
        }

        private void SaveStoreProductImage(Item item)
        {
            string directoryPath =
                    System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/Stores/Organisation" +
                                                                  itemRepository.OrganisationId + "/Company" +
                                                                  item.CompanyId + "/Products/Product" + item.ItemId);

            #region ThumbNail

            if (item.ThumbnailImage != null)
            {
                if (item.ThumbnailPath != null)
                {
                    File.Delete(item.ThumbnailPath);
                }
                string base64 = item.ThumbnailImage.Substring(item.ThumbnailImage.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);

                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string savePath = directoryPath + "\\" + item.ProductCode + "_" + item.ItemId + "_" +
                                  item.ThumbnailImageName;
                File.WriteAllBytes(savePath, data);

                item.ThumbnailPath = savePath;

            }

            #endregion

            #region Grid Image

            if (item.GridImageBytes != null)
            {
                if (item.GridImage != null)
                {
                    File.Delete(item.GridImage);
                }
                string base64 = item.GridImageBytes.Substring(item.GridImageBytes.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);

                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string savePath = directoryPath + "\\" + item.ProductCode + "_" + item.ItemId + "_" +
                                  item.GridImageSourceName;
                File.WriteAllBytes(savePath, data);

                item.GridImage = savePath;
            }

            #endregion

            #region Image

            if (item.ImagePathImage != null)
            {
                if (item.ImagePath != null)
                {
                    File.Delete(item.ImagePath);
                }
                string base64 = item.ImagePathImage.Substring(item.ImagePathImage.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);

                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string savePath = directoryPath + "\\" + item.ProductCode + "_" + item.ItemId + "_" +
                                  item.ImagePathImageName;
                File.WriteAllBytes(savePath, data);

                item.ImagePath = savePath;
            }

            #endregion

            #region File1

            if (item.File1Byte != null)
            {
                if (item.File1 != null)
                {
                    File.Delete(item.File1);
                }
                string base64 = item.File1Byte.Substring(item.File1Byte.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);

                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string savePath = directoryPath + "\\" + item.ProductCode + "_" + item.ItemId + "_" +
                                  item.File1Name;
                File.WriteAllBytes(savePath, data);

                item.File1 = savePath;
            }

            #endregion
            #region File2

            if (item.File2Byte != null)
            {
                if (item.File2 != null)
                {
                    File.Delete(item.File2);
                }
                string base64 = item.File2Byte.Substring(item.File2Byte.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);

                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string savePath = directoryPath + "\\" + item.ProductCode + "_" + item.ItemId + "_" +
                                  item.File2Name;
                File.WriteAllBytes(savePath, data);

                item.File2 = savePath;
            }
            #endregion
            #region File3

            if (item.File3Byte != null)
            {
                if (item.File3 != null)
                {
                    File.Delete(item.File3);
                }
                string base64 = item.File3Byte.Substring(item.File3Byte.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);

                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string savePath = directoryPath + "\\" + item.ProductCode + "_" + item.ItemId + "_" +
                                  item.File3Name;
                File.WriteAllBytes(savePath, data);

                item.File3 = savePath;
            }
            #endregion
            #region File4

            if (item.File4Byte != null)
            {
                if (item.File4 != null)
                {
                    File.Delete(item.File4);
                }
                string base64 = item.File4Byte.Substring(item.File4Byte.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);

                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string savePath = directoryPath + "\\" + item.ProductCode + "_" + item.ItemId + "_" +
                                  item.File4Name;
                File.WriteAllBytes(savePath, data);

                item.File4 = savePath;
            }
            #endregion
            #region File5

            if (item.File5Byte != null)
            {
                if (item.File5 != null)
                {
                    File.Delete(item.File5);
                }
                string base64 = item.File5Byte.Substring(item.File5Byte.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);

                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string savePath = directoryPath + "\\" + item.ProductCode + "_" + item.ItemId + "_" +
                                  item.File5Name;
                File.WriteAllBytes(savePath, data);

                item.File5 = savePath;
            }
            #endregion
            #region Item Stock Option File
            #region Image

            if (item.ItemStockOptions != null)
            {
                foreach (var stockOption in item.ItemStockOptions)
                {
                    //Search If there is previously some file is safe against it then delete it
                    if (stockOption.ImageURL != null)
                    {
                        File.Delete(stockOption.ImageURL);
                    }
                    if (stockOption.FileSource != null)
                    {
                        string base64 = stockOption.FileSource.Substring(stockOption.FileSource.IndexOf(',') + 1);
                        base64 = base64.Trim('\0');
                        byte[] data = Convert.FromBase64String(base64);

                        string mpcContentPath = ConfigurationManager.AppSettings["MPC_Content"];
                        HttpServerUtility server = HttpContext.Current.Server;
                        // i.e:-  /Stores/Organisation1/Company903/StockCategories/StockCategory1
                        string mapPath = server.MapPath(mpcContentPath + "/Stores/Organisation" + itemRepository.OrganisationId + "/Company" + item.CompanyId + "/StockCategories" + stockOption.StockId);

                        // Create directory if not there
                        if (!Directory.Exists(mapPath))
                        {
                            Directory.CreateDirectory(mapPath);
                        }
                        // First Time Upload
                        string imageurl = mapPath + "\\" + stockOption.FileName + "_" +
                                          stockOption.ItemStockOptionId + stockOption.OptionSequence;
                        File.WriteAllBytes(imageurl, data);

                        stockOption.ImageURL = imageurl;
                        itemStockOptionRepository.Update(stockOption);
                    }
                }
                itemStockOptionRepository.SaveChanges();

            }

            #endregion
            #endregion



            itemRepository.Update(item);
            itemRepository.SaveChanges();
        }


        /// <summary>
        /// Save Images for CMS Page(Secondary Page Images)
        /// </summary>
        private string SaveCmsPageImage(CmsPage cmsPage, long companyId)
        {
            if (cmsPage.Bytes != null)
            {
                string base64 = cmsPage.Bytes.Substring(cmsPage.Bytes.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);

                //string directoryPath = HttpContext.Current.Server.MapPath("~/Resources/CMSPages");
                string directoryPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Organisations/Organisation" + companyRepository.OrganisationId + "/Store" + companyId);

                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string savePath = directoryPath + "\\" + "CmsPage" + cmsPage.PageId + "_" + cmsPage.FileName + ".jpeg";
                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                }
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                return savePath;
            }
            return null;
        }
        /// <summary>
        /// Save Images for Company Contact Profile Image
        /// </summary>
        private string SaveCompanyContactProfileImage(CompanyContact companyContact)
        {
            if (companyContact.ContactProfileImage != null)
            {
                string base64 = companyContact.ContactProfileImage.Substring(companyContact.ContactProfileImage.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);

                string directoryPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + companyContact.CompanyId + "/Contacts");

                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string savePath =
                    directoryPath + "\\" +
                    companyContact.ContactId + "_" + StringHelper.SimplifyString(companyContact.FirstName) + "_profile.png";
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                return savePath;
            }
            return null;
        }

        /// <summary>
        /// Save Images for Company Contact Profile Image
        /// </summary>
        private string SaveStoreWorkflowImage(CompanySavingModel companyContact)
        {
            if (companyContact.Company.StoreWorkflowImage != null)
            {
                string directoryPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + companyContact.Company.CompanyId + "/Contacts");

                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                else
                {
                    DirectoryInfo dir = new DirectoryInfo(directoryPath);
                    foreach (FileInfo fi in dir.GetFiles())
                    {
                        fi.IsReadOnly = false;
                        fi.Delete();
                    }
                }

                string savePath = directoryPath + "\\" + companyContact.Company.CompanyId + "_" + DateTime.Now.Second + "_Watermark.png";
                File.WriteAllBytes(savePath, companyContact.Company.StoreWorkFlowFileSourceBytes);
                int indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                companyContact.Company.WatermarkText = savePath;
                return savePath;
            }
            return null;
        }
        /// <summary>
        /// Save Images for Company Contact Profile Image
        /// </summary>
        private string SaveMapImage(CompanySavingModel companyContact)
        {
            if (companyContact.Company.MapImageUrl != null)
            {
                string directoryPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + companyContact.Company.CompanyId + "/MapImage");

                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                else
                {
                    DirectoryInfo dir = new DirectoryInfo(directoryPath);
                    foreach (FileInfo fi in dir.GetFiles())
                    {
                        fi.IsReadOnly = false;
                        fi.Delete();
                    }
                }

                string savePath = directoryPath + "\\" + companyContact.Company.CompanyId + "_MapImage_" + DateTime.Now.Second + ".png";
                File.WriteAllBytes(savePath, companyContact.Company.MapImageUrlSourceBytes);
                int indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                companyContact.Company.MapImageUrl = savePath;
                return savePath;
            }
            return null;
        }
        private string SaveCompanyProfileImage(Company company)
        {
            if (company.ImageBytes != null)
            {
                string base64 = company.ImageBytes.Substring(company.ImageBytes.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);

                string directoryPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + company.CompanyId);

                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string savePath = directoryPath + "\\logo.png";
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                return savePath;
            }
            return null;
        }

        /// <summary>
        /// Add Field Variable
        /// </summary>
        private long AddFieldVariable(FieldVariable fieldVariable)
        {
            fieldVariable.OrganisationId = fieldVariableRepository.OrganisationId;
            long companyId = (long)(fieldVariable.CompanyId ?? 0);
            fieldVariableRepository.Add(fieldVariable);
            List<ScopeVariable> scopeVariables = new List<ScopeVariable>();
            fieldVariable.ScopeVariables = scopeVariables;

            //In case of scope type of variable field is contact
            if (companyId > 0 && fieldVariable.Scope.HasValue && fieldVariable.Scope == (int)FieldVariableScopeType.Contact)
            {
                IEnumerable<CompanyContact> companyContacts =
                    companyContactRepository.GetCompanyContactsByCompanyId(companyId);
                if (companyContacts != null)
                {
                    foreach (var contact in companyContacts)
                    {
                        ScopeVariable scopeVariable = new ScopeVariable();

                        scopeVariable.ScopeVariableId = 0;
                        scopeVariable.Scope = fieldVariable.Scope;
                        scopeVariable.Id = contact.ContactId;
                        scopeVariable.Value = fieldVariable.DefaultValue;
                        scopeVariableRepository.Add(scopeVariable);
                        fieldVariable.ScopeVariables.Add(scopeVariable);
                    }
                }
            }

        //In case of scope type of variable field is address
            else if (companyId > 0 && fieldVariable.Scope.HasValue && fieldVariable.Scope == (int)FieldVariableScopeType.Address)
            {
                IEnumerable<Address> addresses =
                    addressRepository.GetAddressByCompanyID(companyId);
                if (addresses != null)
                {
                    foreach (var address in addresses)
                    {
                        ScopeVariable scopeVariable = new ScopeVariable();

                        scopeVariable.ScopeVariableId = 0;
                        scopeVariable.Scope = fieldVariable.Scope;
                        scopeVariable.Id = address.AddressId;
                        scopeVariable.Value = fieldVariable.DefaultValue;
                        scopeVariableRepository.Add(scopeVariable);
                        fieldVariable.ScopeVariables.Add(scopeVariable);
                    }
                }
            }
            //In case of scope type of variable field is Company Territory
            else if (companyId > 0 && fieldVariable.Scope.HasValue && fieldVariable.Scope == (int)FieldVariableScopeType.Territory)
            {
                IEnumerable<CompanyTerritory> companyTerritories =
                    companyTerritoryRepository.GetAllCompanyTerritories(companyId);
                if (companyTerritories != null)
                {
                    foreach (var territory in companyTerritories)
                    {
                        ScopeVariable scopeVariable = new ScopeVariable();

                        scopeVariable.ScopeVariableId = 0;
                        scopeVariable.Scope = fieldVariable.Scope;
                        scopeVariable.Id = territory.TerritoryId;
                        scopeVariable.Value = fieldVariable.DefaultValue;
                        scopeVariableRepository.Add(scopeVariable);
                        fieldVariable.ScopeVariables.Add(scopeVariable);
                    }
                }
            }
            //In case of scope type of variable field is Store
            else if (companyId > 0 && fieldVariable.Scope.HasValue && fieldVariable.Scope == (int)FieldVariableScopeType.Store)
            {
                ScopeVariable scopeVariable = new ScopeVariable();
                scopeVariable.ScopeVariableId = 0;
                scopeVariable.Scope = fieldVariable.Scope;
                scopeVariable.Id = companyId;
                scopeVariable.Value = fieldVariable.DefaultValue;
                scopeVariableRepository.Add(scopeVariable);
                fieldVariable.ScopeVariables.Add(scopeVariable);
            }
            fieldVariableRepository.SaveChanges();
            return fieldVariable.VariableId;
        }

        /// <summary>
        /// Update Field Variable
        /// </summary>
        private long UpdateFieldVariable(FieldVariable fieldVariable)
        {
            FieldVariable fieldVariableDbVersion = fieldVariableRepository.Find(fieldVariable.VariableId);
            if (fieldVariableDbVersion != null)
            {
                fieldVariableDbVersion.InputMask = fieldVariable.InputMask;
                fieldVariableDbVersion.VariableName = fieldVariable.VariableName;
                fieldVariableDbVersion.DefaultValue = fieldVariable.DefaultValue;
                fieldVariableDbVersion.Scope = fieldVariable.Scope;
                fieldVariableDbVersion.VariableTag = fieldVariable.VariableTag;
                fieldVariableDbVersion.VariableTitle = fieldVariable.VariableTitle;
                fieldVariableDbVersion.VariableType = fieldVariable.VariableType;
                fieldVariableDbVersion.WaterMark = fieldVariable.WaterMark;
                fieldVariableDbVersion.OrganisationId = fieldVariableRepository.OrganisationId;
                if (fieldVariable.VariableOptions != null)
                {
                    foreach (var item in fieldVariable.VariableOptions)
                    {
                        //New Added
                        if (item.VariableOptionId == 0 || item.VariableOptionId < 0)
                        {
                            item.VariableOptionId = 0;
                            fieldVariableDbVersion.VariableOptions.Add(item);
                        }
                        else
                        {
                            //Update variable options
                            VariableOption variableOption =
                                fieldVariableDbVersion.VariableOptions.FirstOrDefault(
                                    vo => vo.VariableOptionId == item.VariableOptionId);
                            if (variableOption != null)
                            {
                                variableOption.Value = item.Value;
                            }
                        }
                    }
                }

                #region Delete
                //find missing items
                List<VariableOption> missingVariableOptionListItems = new List<VariableOption>();
                foreach (VariableOption dbversionVariableOptionItem in fieldVariableDbVersion.VariableOptions)
                {
                    if (fieldVariable.VariableOptions != null && fieldVariable.VariableOptions.All(x => x.VariableOptionId != dbversionVariableOptionItem.VariableOptionId))
                    {
                        missingVariableOptionListItems.Add(dbversionVariableOptionItem);
                    }
                    //In case user delete all variable Options
                    if (fieldVariable.VariableOptions == null)
                    {
                        missingVariableOptionListItems.Add(dbversionVariableOptionItem);
                    }
                }


                //remove missing items
                foreach (VariableOption missingVariableOptionItem in missingVariableOptionListItems)
                {
                    VariableOption dbVersionMissingItem = fieldVariableDbVersion.VariableOptions.First(x => x.VariableOptionId == missingVariableOptionItem.VariableOptionId);
                    if (dbVersionMissingItem.VariableOptionId > 0)
                    {
                        variableOptionRepository.Delete(dbVersionMissingItem);
                    }
                }
                #endregion

                fieldVariableRepository.SaveChanges();
            }

            return fieldVariable.VariableId;
        }

        /// <summary>
        /// Updaten Smart Form
        /// </summary>
        private long UpdateSmartForm(SmartForm smartForm, SmartForm smartFormDbVersion)
        {
            #region Update Smart Form
            smartFormDbVersion.Name = smartForm.Name;
            smartFormDbVersion.Heading = smartForm.Heading;
            smartFormDbVersion.OrganisationId = smartFormRepository.OrganisationId;
            if (smartForm.SmartFormDetails != null)
            {
                foreach (SmartFormDetail smartFormDetail in smartForm.SmartFormDetails)
                {
                    if (smartFormDetail.SmartFormDetailId == 0)
                    {
                        if (smartFormDbVersion.SmartFormDetails == null)
                        {
                            smartFormDbVersion.SmartFormDetails = new List<SmartFormDetail>();
                        }
                        smartFormDbVersion.SmartFormDetails.Add(smartFormDetail);
                    }
                    else if (smartFormDetail.SmartFormDetailId > 0 && smartFormDetail.ObjectType == (int)SmartFormDetailFieldType.GroupCaption)
                    {
                        SmartFormDetail smartFormDetailDbVersion = smartFormDbVersion.SmartFormDetails.FirstOrDefault(
                           sf => sf.SmartFormDetailId == smartFormDetail.SmartFormDetailId);

                        if (smartFormDetailDbVersion != null)
                        {
                            smartFormDetailDbVersion.CaptionValue = smartFormDetail.CaptionValue;
                            smartFormDetailDbVersion.SortOrder = smartFormDetail.SortOrder;
                        }
                    }
                    else
                    {
                        SmartFormDetail smartFormDetailDbVersion = smartFormDbVersion.SmartFormDetails.FirstOrDefault(
                           sf => sf.SmartFormDetailId == smartFormDetail.SmartFormDetailId);
                        if (smartFormDetailDbVersion != null)
                        {
                            smartFormDetailDbVersion.SortOrder = smartFormDetail.SortOrder;
                            smartFormDetailDbVersion.IsRequired = smartFormDetail.IsRequired;
                        }
                    }
                }
            }
            smartFormRepository.SaveChanges();
            #endregion

            #region Delete SmartForm Detail
            //missing Items
            List<SmartFormDetail> missingSmartFormDetails = new List<SmartFormDetail>();
            if (smartFormDbVersion.SmartFormDetails != null)
            {
                foreach (var smartFormDetailDbItem in smartFormDbVersion.SmartFormDetails)
                {
                    if (smartForm.SmartFormDetails != null && smartForm.SmartFormDetails.All(c => c.SmartFormDetailId != smartFormDetailDbItem.SmartFormDetailId))
                    {
                        missingSmartFormDetails.Add(smartFormDetailDbItem);
                    }
                    else if (smartForm.SmartFormDetails == null)
                    {
                        missingSmartFormDetails.Add(smartFormDetailDbItem);
                    }
                }

                foreach (var missingItem in missingSmartFormDetails)
                {
                    smartFormDetailRepository.Delete(missingItem);
                }
                smartFormDetailRepository.SaveChanges();
            }

            #endregion

            return smartForm.SmartFormId;
        }

        /// <summary>
        /// Add Smart Form
        /// </summary>
        private long AddSmartForm(SmartForm smartForm)
        {
            smartForm.OrganisationId = smartFormRepository.OrganisationId;
            smartFormRepository.Add(smartForm);
            smartFormRepository.SaveChanges();
            return smartForm.SmartFormId;
        }

        /// <summary>
        /// Apply Theme Sprite Image
        /// </summary>
        private void ApplyThemeSpriteImage(string themeName, long companyId)
        {
            string themeSpriteImagePath =
                HttpContext.Current.Server.MapPath("~/MPC_Content/Themes/" + themeName + "/sprite.png");
            string directoryPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + companyId + "/sprite.png");
            if (File.Exists(directoryPath))
            {
                File.Delete(directoryPath);
            }
            if (File.Exists(themeSpriteImagePath))
            {
                File.Copy(themeSpriteImagePath, directoryPath, true);
            }

        }

        /// <summary>
        /// Apply Theme Css
        /// </summary>
        private void ApplyThemeCss(string themeName, long companyId)
        {
            string path =
                 HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId +
                                                    "/" + companyId + "/site.css");
            // Open the file to read from. 
            if (File.Exists(HttpContext.Current.Server.MapPath("~/MPC_Content/Themes/" + themeName + "/site.css")))
            {
                string css =
                  File.ReadAllText(HttpContext.Current.Server.MapPath("~/MPC_Content/Themes/" + themeName + "/site.css"));
                File.WriteAllText(path, css);
            }
        }

        private void ApplyThemeWidgets(string themeName, long companyId)
        {
            try
            {
                string widgetFilePath =
                 HttpContext.Current.Server.MapPath("~/MPC_Content/Themes/" + themeName + "/widgets.txt");
                if (File.Exists(widgetFilePath))
                {
                    List<WidgetForTheme> widgetForThemes = new List<WidgetForTheme>();
                    using (StreamReader r = new StreamReader(widgetFilePath))
                    {
                        string json = r.ReadToEnd();
                        widgetForThemes = JsonConvert.DeserializeObject<List<WidgetForTheme>>(json);
                    }

                    UpdateCmsPageWidgetFromApplyTheme(widgetForThemes, companyId);
                }


            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        private void UpdateCmsPageWidgetFromApplyTheme(List<WidgetForTheme> widgetForThemes, long companyId)
        {
            List<CmsPage> cmsPagesDbVersion = cmsPageRepository.GetCmsPagesByCompanyId(companyId);


            var pageGroups = from page in widgetForThemes
                             group page by page.PageName
                                 into pageGroup
                                 select pageGroup;

            foreach (var group in pageGroups)
            {
                CmsPage cmsPageDbVersionItem =
                    cmsPagesDbVersion.FirstOrDefault(p => p.PageName.ToLower() == group.Key.ToLower());
                if (cmsPageDbVersionItem != null)
                {
                    //Remove Old Widget From Page
                    List<CmsSkinPageWidget> cmsSkinPageWidgetRemoveItems = new List<CmsSkinPageWidget>();
                    foreach (CmsSkinPageWidget cmsSkinPageWidget in cmsPageDbVersionItem.CmsSkinPageWidgets)
                    {
                        cmsSkinPageWidgetRemoveItems.Add(cmsSkinPageWidget);
                    }
                    foreach (CmsSkinPageWidget cmsSkinPageWidgetItem in cmsSkinPageWidgetRemoveItems)
                    {
                        cmsPageDbVersionItem.CmsSkinPageWidgets.Remove(cmsSkinPageWidgetItem);
                    }
                    //Add new Widget of applied theme to Page
                    foreach (WidgetForTheme widgetTheme in group)
                    {
                        if (widgetTheme.WidgetId != null)
                        {
                            CmsSkinPageWidget widget = new CmsSkinPageWidget();
                            widget.WidgetId = widgetTheme.WidgetId;
                            widget.SkinId = widgetTheme.SkinId;
                            widget.Sequence = widgetTheme.Sequence;
                            widget.CompanyId = companyId;
                            widget.OrganisationId = companyRepository.OrganisationId;
                            widget.PageId = cmsPageDbVersionItem.PageId;
                            if (widgetTheme.ParamValue != null)
                            {
                                CmsSkinPageWidgetParam cmsSkinPageWidgetParam = new CmsSkinPageWidgetParam();
                                cmsSkinPageWidgetParam.PageWidgetId = widget.WidgetId;
                                cmsSkinPageWidgetParam.ParamValue = widgetTheme.ParamValue;
                                if (widget.CmsSkinPageWidgetParams == null)
                                {
                                    List<CmsSkinPageWidgetParam> pageWidgetParams = new List<CmsSkinPageWidgetParam>();
                                    widget.CmsSkinPageWidgetParams = pageWidgetParams;
                                }
                                widget.CmsSkinPageWidgetParams.Add(cmsSkinPageWidgetParam);
                            }
                            if (cmsPageDbVersionItem.CmsSkinPageWidgets == null)
                            {
                                List<CmsSkinPageWidget> cmsSkinPageWidgets = new List<CmsSkinPageWidget>();
                                cmsPageDbVersionItem.CmsSkinPageWidgets = cmsSkinPageWidgets;
                            }
                            cmsPageDbVersionItem.CmsSkinPageWidgets.Add(widget);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Update Media Library File Path
        /// </summary>
        private void AddThemeBanners(int themeId, string themeName, long companyId)
        {
            string target = HttpContext.Current.Server.MapPath("~/MPC_Content/Media/" + companyRepository.OrganisationId + "/" + companyId);
            string source =
                HttpContext.Current.Server.MapPath("~/MPC_Content/Themes/" + themeName + "/banners");
            CopyThemeBanners(themeId, source, target, companyId);

        }

        /// <summary>
        /// Copy Banners From 
        /// </summary>
        public void CopyThemeBanners(int themeId, string sourceDirectory, string targetDirectory, long companyId)
        {
            DirectoryInfo source = new DirectoryInfo(sourceDirectory);
            DirectoryInfo target = new DirectoryInfo(targetDirectory);
            // Check if the target directory exists; if not, create it.
            if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
            }

            List<MediaLibrary> mediaLibraries = new List<MediaLibrary>();

            // Get Active Banner Set
            CompanyBannerSet companyBannerSet = bannerSetRepository.GetActiveBannerSetForCompany(companyId);

            // Check If Exists
            if (companyBannerSet != null)
            {
                if (companyBannerSet.CompanyBanners == null)
                {
                    companyBannerSet.CompanyBanners = new List<CompanyBanner>();
                }

                // Remove Existing Banners
                List<CompanyBanner> companyBanners = companyBannerSet.CompanyBanners.ToList();
                companyBanners.ForEach(cb =>
                {
                    companyBannerSet.CompanyBanners.Remove(cb);
                    companyBannerRepository.Delete(cb);
                });
            }

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                MediaLibrary mediaLibrary = new MediaLibrary();
                mediaLibrary.MediaId = 0;
                mediaLibrary.FileName = fi.Name;
                mediaLibrary.FileType = "image/jpeg";
                mediaLibrary.CompanyId = companyId;
                mediaLibrary.FilePath = "temp";
                mediaLibraries.Add(mediaLibrary);
                mediaLibraryRepository.Add(mediaLibrary);

                if (companyBannerSet != null)
                {
                    // Add New Banners from Theme to Active Set
                    CompanyBanner banner = new CompanyBanner
                    {
                        CompanySetId = companyBannerSet.CompanySetId,
                        Heading = "Banner"
                    };

                    companyBannerSet.CompanyBanners.Add(banner);
                    companyBannerRepository.Add(banner);
                }
            }

            mediaLibraryRepository.SaveChanges();
            int i = 0;
            foreach (FileInfo fi in source.GetFiles())
            {
                MediaLibrary mediaLibrary = mediaLibraries[i];

                mediaLibrary.FilePath = "/MPC_Content/Media/" + companyRepository.OrganisationId + "/" + companyId + "/" + mediaLibrary.MediaId + "_" + fi.Name;
                fi.CopyTo(Path.Combine(target.FullName, mediaLibrary.MediaId + "_" + fi.Name), true);

                // Update Banner Image Path
                if (companyBannerSet != null && companyBannerSet.CompanyBanners != null && companyBannerSet.CompanyBanners.Count > 0)
                {
                    // Add New Banners from Theme to Active Set
                    CompanyBanner banner = companyBannerSet.CompanyBanners.ToList()[i];
                    banner.ImageURL = mediaLibrary.FilePath;
                }

                i = (i + 1);
            }
            Company company = companyRepository.Find(companyId);
            if (company != null)
            {
                company.CurrentThemeId = themeId;
            }

            mediaLibraryRepository.SaveChanges();
        }

        /// <summary>
        /// Apply Theme Fonts
        /// </summary>
        private void ApplyThemeFonts(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo source = new DirectoryInfo(sourceDirectory);
            DirectoryInfo target = new DirectoryInfo(targetDirectory);
            // Check if the target directory exists; if not, create it.
            if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
            }

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }
        }
        #endregion

        #region Constructor

        public CompanyService(ICompanyRepository companyRepository, ISystemUserRepository systemUserRepository, IRaveReviewRepository raveReviewRepository,
            ICompanyCMYKColorRepository companyCmykColorRepository, ICompanyTerritoryRepository companyTerritoryRepository, IAddressRepository addressRepository,
            ICompanyContactRoleRepository companyContactRoleRepository, IRegistrationQuestionRepository registrationQuestionRepository
            , ICompanyBannerRepository companyBannerRepository, ICompanyContactRepository companyContactRepository, ICmsPageRepository cmsPageRepository,
             IPageCategoryRepository pageCategoryRepository, IEmailEventRepository emailEventRepository, IPaymentMethodRepository paymentMethodRepository,
            IPaymentGatewayRepository paymentGatewayRepository, IWidgetRepository widgetRepository, ICmsSkinPageWidgetRepository cmsSkinPageWidgetRepository, IProductCategoryRepository productCategoryRepository,
            IOrganisationRepository organisationRepository,
            IItemRepository itemRepository, IGetItemsListViewRepository itemsListViewRepository, IItemStockOptionRepository itemStockOptionRepository,
            IPrefixRepository prefixRepository, IItemVdpPriceRepository itemVdpPriceRepository,
            IItemVideoRepository itemVideoRepository,
        IItemRelatedItemRepository itemRelatedItemRepository,
        ITemplatePageRepository templatePageRepository,
        ITemplateRepository templateRepository,
        IItemAddOnCostCentreRepository itemAddOnCostCentreRepository,
        ICostCentreRepository costCentreRepository,
        IStockItemRepository stockItemRepository,
        IItemPriceMatrixRepository itemPriceMatrixRepository,
        IItemStateTaxRepository itemStateTaxRepository,
        ICountryRepository countryRepository,
        IStateRepository stateRepository,
        ISectionFlagRepository sectionFlagRepository,
        IItemProductDetailRepository itemProductDetailRepository,
            ICompanyDomainRepository companyDomainRepository, ICostCentreMatrixRepository costCentreMatrixRepositry, ICostCentreQuestionRepository CostCentreQuestionRepository,
            IStockCategoryRepository StockCategoryRepository, IPaperSizeRepository PaperSizeRepository, IMachineRepository MachineRepository, IPhraseFieldRepository PhraseFieldRepository,
            IReportRepository ReportRepository, IFieldVariableRepository fieldVariableRepository, IVariableOptionRepository variableOptionRepository,
            IScopeVariableRepository scopeVariableRepository, ISmartFormRepository smartFormRepository, ISmartFormDetailRepository smartFormDetailRepository,
            IEstimateRepository estimateRepository, IMediaLibraryRepository mediaLibraryRepository, ICompanyCostCenterRepository companyCostCenterRepository,
            ICmsTagReporistory cmsTagReporistory, ICompanyBannerSetRepository bannerSetRepository, ICampaignRepository campaignRepository,
            MPC.Interfaces.WebStoreServices.ITemplateService templateService, ITemplateFontsRepository templateFontRepository, IMarkupRepository markupRepository,
            ITemplateColorStylesRepository templateColorStylesRepository)
        {
            if (bannerSetRepository == null)
            {
                throw new ArgumentNullException("bannerSetRepository");
            }
            if (templateColorStylesRepository == null)
            {
                throw new ArgumentNullException("templateColorStylesRepository");
            }
            this.companyRepository = companyRepository;
            this.campaignRepository = campaignRepository;
            this.smartFormRepository = smartFormRepository;
            this.cmsTagReporistory = cmsTagReporistory;
            this.bannerSetRepository = bannerSetRepository;
            this.mediaLibraryRepository = mediaLibraryRepository;
            this.companyCostCenterRepository = companyCostCenterRepository;
            this.smartFormDetailRepository = smartFormDetailRepository;
            this.estimateRepository = estimateRepository;
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
            this.itemRepository = itemRepository;
            this.itemsListViewRepository = itemsListViewRepository;
            this.itemStockOptionRepository = itemStockOptionRepository;
            this.prefixRepository = prefixRepository;
            this.itemVdpPriceRepository = itemVdpPriceRepository;

            this.templatePageRepository = templatePageRepository;
            this.itemVideoRepository = itemVideoRepository;
            this.itemRelatedItemRepository = itemRelatedItemRepository;
            this.templateRepository = templateRepository;
            this.itemAddOnCostCentreRepository = itemAddOnCostCentreRepository;
            this.costCentreRepository = costCentreRepository;
            this.stockItemRepository = stockItemRepository;
            this.itemPriceMatrixRepository = itemPriceMatrixRepository;
            this.itemStateTaxRepository = itemStateTaxRepository;
            this.countryRepository = countryRepository;
            this.stateRepository = stateRepository;
            this.sectionFlagRepository = sectionFlagRepository;
            this.itemProductDetailRepository = itemProductDetailRepository;
            this.companyDomainRepository = companyDomainRepository;
            this.costCentreMatrixRepositry = costCentreMatrixRepositry;
            this.CostCentreQuestionRepository = CostCentreQuestionRepository;
            this.StockCategoryRepository = StockCategoryRepository;
            this.PaperSizeRepository = PaperSizeRepository;
            this.MachineRepository = MachineRepository;
            this.PhraseFieldRepository = PhraseFieldRepository;
            this.ReportRepository = ReportRepository;
            this.fieldVariableRepository = fieldVariableRepository;
            this.variableOptionRepository = variableOptionRepository;
            this.scopeVariableRepository = scopeVariableRepository;
            this.templateService = templateService;
            this.templatefonts = templateFontRepository;
            this.markupRepository = markupRepository;
            this.templateColorStylesRepository = templateColorStylesRepository;

        }
        #endregion

        #region Public

        /// <summary>
        /// Delete Media Library Item By Id
        /// </summary>
        public void DeleteMedia(long mediaId)
        {
            MediaLibrary mediaLibraryDbVersion = mediaLibraryRepository.Find(mediaId);
            if (mediaLibraryDbVersion != null)
            {
                IEnumerable<CmsPage> cmsPages = cmsPageRepository.GetAll();
                CmsPage cmsPage = cmsPages.FirstOrDefault(cp => cp.PageBanner == mediaLibraryDbVersion.FilePath);
                if (cmsPage != null)
                {
                    throw new MPCException(string.Format(CultureInfo.InvariantCulture, "File is used in CMS page."), companyRepository.OrganisationId);
                }
                IEnumerable<CompanyBanner> companyBanners = companyBannerRepository.GetAll();
                CompanyBanner companyBanner = companyBanners.FirstOrDefault(cp => cp.ImageURL == mediaLibraryDbVersion.FilePath);
                if (companyBanner != null)
                {
                    throw new MPCException(string.Format(CultureInfo.InvariantCulture, "File is used in Banner."), companyRepository.OrganisationId);
                }

                mediaLibraryRepository.Delete(mediaLibraryDbVersion);
                mediaLibraryRepository.SaveChanges();
            }
        }
        /// <summary>
        /// Delete Company Permanently
        /// </summary>
        public void DeleteCompanyPermanently(long companyId)
        {
            Company company = companyRepository.Find(companyId);

            if (company == null)
            {
                throw new MPCException(string.Format(CultureInfo.InvariantCulture, "Company with id {0} not found", companyId), companyRepository.OrganisationId);
            }

            companyRepository.DeleteStoryBySP(companyId);
        }

        /// <summary>
        /// Get Items For Widgets
        /// </summary>
        public List<Item> GetItemsForWidgets()
        {
            return itemRepository.GetItemsForWidgets();
        }

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
            CompanyResponse response = companyRepository.GetCompanyById(companyId);
            int userCount = 0;
            int newOrdersCount = 0;
            if (response.Company != null && response.Company.StoreId != null)
                userCount = companyRepository.UserCount(response.Company.StoreId, 5);
            newOrdersCount = estimateRepository.GetNewOrdersCount(5, companyId);
            response.NewOrdersCount = newOrdersCount;
            response.NewUsersCount = userCount;
            return response;
        }
        public CompanyResponse GetCompanyByIdForCrm(long companyId)
        {
            CompanyResponse response = companyRepository.GetCompanyByIdForCrm(companyId);
            int userCount = 0;
            int newOrdersCount = 0;
            if (response.Company != null && response.Company.StoreId != null)
                userCount = companyRepository.UserCount(response.Company.StoreId, 5);
            newOrdersCount = estimateRepository.GetNewOrdersCount(5, companyId);
            response.NewOrdersCount = newOrdersCount;
            response.NewUsersCount = userCount;
            return response;
        }


        public CompanyBaseResponse GetBaseData(long storeId)
        {
            FieldVariableRequestModel request = new FieldVariableRequestModel();
            SmartFormRequestModel smartFormRequest = new SmartFormRequestModel();
            request.CompanyId = storeId;
            smartFormRequest.CompanyId = storeId;

            return new CompanyBaseResponse
            {
                //SystemUsers = systemUserRepository.GetAll(),
                CompanyTerritories = companyTerritoryRepository.GetAllCompanyTerritories(storeId),
                //CompanyContactRoles = companyContactRoleRepository.GetAll(),
                // PageCategories = pageCategoryRepository.GetCmsSecondaryPageCategories(),
                // RegistrationQuestions = registrationQuestionRepository.GetAll(),
                Addresses = addressRepository.GetAllAddressByStoreId(storeId),
                // PaymentMethods = paymentMethodRepository.GetAll().ToList(),
                //EmailEvents = emailEventRepository.GetAll(),
                //Widgets = widgetRepository.GetAll(),
                // CostCentres = costCentreRepository.GetAllCompanyCentersByOrganisationId().ToList(),//GetAllCompanyCentersByCompanyId
                // States = stateRepository.GetAll(),
                // Countries = countryRepository.GetAll(),
                FieldVariableResponse = fieldVariableRepository.GetFieldVariable(request),
                SmartFormResponse = smartFormRepository.GetSmartForms(smartFormRequest),
                FieldVariablesForSmartForm = fieldVariableRepository.GetFieldVariablesForSmartForm(storeId),
                CmsPages = cmsPageRepository.GetCmsPagesForOrders(storeId),
                PriceFlags = sectionFlagRepository.GetSectionFlagBySectionId((long)SectionEnum.CustomerPriceMatrix)
            };
        }

        /// <summary>
        /// Get Smart Forms
        /// </summary>
        public SmartFormResponse GetSmartForms(SmartFormRequestModel request)
        {
            return smartFormRepository.GetSmartForms(request);
        }
        public CompanyBaseResponse GetBaseDataForNewCompany()
        {
            var organisation = organisationRepository.Find(fieldVariableRepository.OrganisationId);
            return new CompanyBaseResponse
            {
                SystemUsers = systemUserRepository.GetAll(),
                CompanyContactRoles = companyContactRoleRepository.GetAll(),
                PageCategories = pageCategoryRepository.GetCmsSecondaryPageCategories(),
                RegistrationQuestions = registrationQuestionRepository.GetAll(),
                PaymentMethods = paymentMethodRepository.GetAll().ToList(),
                EmailEvents = emailEventRepository.GetAll(),
                Widgets = widgetRepository.GetAll(),
                States = stateRepository.GetAll(),
                Countries = countryRepository.GetAll(),
                SectionFlags = sectionFlagRepository.GetSectionFlagBySectionId((long)SectionEnum.CRM),
                CostCentres = costCentreRepository.GetAllDeliveryCostCentersForStore(),
                SystemVariablesForSmartForms = fieldVariableRepository.GetSystemVariables(),
                PriceFlags = sectionFlagRepository.GetSectionFlagBySectionId((long)SectionEnum.CustomerPriceMatrix),
                OrganisationId = fieldVariableRepository.OrganisationId,
                Currency = (organisation != null && organisation.Currency != null) ? organisation.Currency.CurrencySymbol :
                string.Empty,
            };

        }
        /// <summary>
        /// Base Data for Crm Screen (prospect/customer and suppliers)
        /// </summary>
        /// <returns></returns>
        public CrmBaseResponse GetBaseDataForCrm()
        {
            return new CrmBaseResponse
            {
                SystemUsers = systemUserRepository.GetAll(),
                CompanyContactRoles = companyContactRoleRepository.GetAll(),
                RegistrationQuestions = registrationQuestionRepository.GetAll(),
                States = stateRepository.GetAll(),
                Countries = countryRepository.GetAll(),
                SectionFlags = sectionFlagRepository.GetSectionFlagBySectionId((long)SectionEnum.CRM),
                Companies = companyRepository.GetAllRetailAndCorporateStores()
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

            if (CheckDuplicateExistenceOfCompanyDomains(companyModel))
            {
                if (companyDbVersion == null)
                {
                    return SaveNewCompany(companyModel);
                }
                return UpdateCompany(companyModel, companyDbVersion);
            }

            return null;
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

        /// <summary>
        /// Load Items, based on search filters
        /// </summary>
        public ItemListViewSearchResponse GetItems(CompanyProductSearchRequestModel request)
        {
            return itemsListViewRepository.GetItemsForCompany(request);
        }

        public Company DeleteCompany(long companyId)
        {
            var companyResponse = companyRepository.GetCompanyById(companyId);
            companyResponse.Company.isArchived = true;
            companyRepository.Update(companyResponse.Company);
            companyRepository.SaveChanges();
            return companyResponse.Company;
        }

        /// <summary>
        /// Delete Field Variable
        /// </summary>
        public void DeleteFieldVariable(long variableId)
        {
            FieldVariable fieldVariable = fieldVariableRepository.Find(variableId);
            if (fieldVariable != null)
            {
                if (fieldVariable.TemplateVariables.Any())
                {
                    throw new MPCException("It cannot be deleted because it is used in Template.", fieldVariableRepository.OrganisationId);
                }

                fieldVariableRepository.Delete(fieldVariable);
                fieldVariableRepository.SaveChanges();
            }
        }

        /// <summary>
        /// Save Field Variable
        /// </summary>
        public long SaveFieldVariable(FieldVariable fieldVariable)
        {
            //Check for Duplicate Name and Variable Tag
            long companyId = (long)(fieldVariable.CompanyId ?? 0);
            string dublicateErrorMsg =
                fieldVariableRepository.IsFiedlVariableNameOrTagDuplicate(fieldVariable.VariableName,
                    fieldVariable.VariableTag, companyId, fieldVariable.VariableId);
            if (dublicateErrorMsg != null)
            {
                throw new MPCException(dublicateErrorMsg, fieldVariableRepository.OrganisationId);
            }


            if (fieldVariable.VariableId == 0)
            {
                return AddFieldVariable(fieldVariable);
            }
            else
            {
                return UpdateFieldVariable(fieldVariable);
            }
        }


        /// <summary>
        /// Get Field Variables
        /// </summary>
        public FieldVariableResponse GetFieldVariables(FieldVariableRequestModel request)
        {
            return fieldVariableRepository.GetFieldVariable(request);
        }

        /// <summary>
        /// Get Field Variable Detail
        /// </summary>
        public FieldVariable GetFieldVariableDetail(long fieldId)
        {
            return fieldVariableRepository.Find(fieldId);
        }

        /// <summary>
        /// Get Company Contact Varibale By Contact ID
        /// </summary>
        public IEnumerable<ScopeVariable> GetContactVariableByContactId(long contactId, int scope)
        {
            return scopeVariableRepository.GetContactVariableByContactId(contactId, scope);
        }

        /// <summary>
        /// Get Field Varibale By Company ID By Sope Type
        /// </summary>
        public IEnumerable<FieldVariable> GetFieldVariableByCompanyIdAndScope(long companyId, int scope)
        {
            return fieldVariableRepository.GetFieldVariableByCompanyIdAndScope(companyId, scope);
        }

        /// <summary>
        /// Save Smart Form
        /// </summary>
        public long SaveSmartForm(SmartForm smartForm)
        {
            SmartForm smartFormDbVersion = smartFormRepository.Find(smartForm.SmartFormId);
            if (smartFormDbVersion != null)
            {
                return UpdateSmartForm(smartForm, smartFormDbVersion);
            }
            else
            {
                return AddSmartForm(smartForm);
            }
        }

        /// <summary>
        /// Get Smart Form Detail By Smart Form Id
        /// </summary>
        public IEnumerable<SmartFormDetail> GetSmartFormDetailBySmartFormId(long smartFormId)
        {
            return smartFormDetailRepository.GetSmartFormDetailsBySmartFormId(smartFormId);
        }

        /// <summary>
        /// Delete Company Banner
        /// </summary>
        public void DeleteCompanyBanner(long companyBannerId)
        {
            CompanyBanner companyBanner = companyBannerRepository.Find(companyBannerId);
            companyBannerRepository.Delete(companyBanner);
            companyBannerRepository.SaveChanges();
        }

        /// <summary>
        /// Apply Theme
        /// </summary>
        public void ApplyTheme(int themeId, string themeName, long companyId)
        {
            DeleteMediaFiles(companyId);
            string directoryPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + companyId);
            if (directoryPath != null && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            ApplyThemeCss(themeName, companyId);
            ApplyThemeSpriteImage(themeName, companyId);
            ApplyThemeWidgets(themeName, companyId);
            AddThemeBanners(themeId, themeName, companyId);
            string target = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + companyId + "/fonts");
            string source =
                HttpContext.Current.Server.MapPath("~/MPC_Content/Themes/" + themeName + "/fonts");
            ApplyThemeFonts(source, target);

        }

        private void DeleteMediaFiles(long companyId)
        {
            IEnumerable<MediaLibrary> mediaLibraries = mediaLibraryRepository.GetMediaLibrariesByCompanyId(companyId);
            IEnumerable<CmsPage> cmsPages = cmsPageRepository.GetAll();
            IEnumerable<CompanyBanner> companyBanners = companyBannerRepository.GetAll();

            List<MediaLibrary> mediaLibrariesForDelete = new List<MediaLibrary>();
            foreach (var media in mediaLibraries)
            {

                CmsPage cmsPage = cmsPages.FirstOrDefault(cp => cp.PageBanner == media.FilePath);
                CompanyBanner companyBanner = companyBanners.FirstOrDefault(cp => cp.ImageURL == media.FilePath);
                if (cmsPage == null && companyBanner == null)
                {
                    mediaLibrariesForDelete.Add(media);
                }
            }
            foreach (var item in mediaLibrariesForDelete)
            {
                mediaLibraryRepository.Delete(item);
            }
            mediaLibraryRepository.SaveChanges();
        }

        /// <summary>
        /// Get Campaign Detail By Campaign ID
        /// </summary>
        public Campaign GetCampaignById(long campaignId)
        {
            return campaignRepository.Find(campaignId);
        }

        /// <summary>
        /// Get Cms Tags For Cms Page Load Default page keywords
        /// </summary>
        public string GetCmsTagForCmsPage()
        {
            IEnumerable<CmsTag> cmsTags = cmsTagReporistory.GetAllForCmsPage();

            string defaultPageKeyWords = null;
            if (cmsTags != null)
            {
                foreach (var item in cmsTags)
                {

                    defaultPageKeyWords = defaultPageKeyWords != null ? (defaultPageKeyWords + " , " + item.TagName) : item.TagName;

                }
            }
            return defaultPageKeyWords ?? string.Empty;
        }


        /// <summary>
        /// Save Cms Page
        /// </summary>
        public long SaveSecondaryPage(CmsPage cmsPage)
        {
            if (cmsPage.PageId > 0)
            {
                CmsPage cmsPageDbVersion = cmsPageRepository.Find(cmsPage.PageId);
                if (cmsPageDbVersion != null)
                {
                    cmsPageRepository.Update(UpdateCmsPage(cmsPage, cmsPageDbVersion));
                }
            }
            else
            {
                cmsPageRepository.Add(cmsPage);
            }
            cmsPageRepository.SaveChanges();
            return cmsPage.PageId;
        }


        /// <summary>
        /// Delete Secondary Page
        /// </summary>
        public void DeleteSecondaryPage(long pageId)
        {
            cmsPageRepository.Delete(cmsPageRepository.Find(pageId));
        }

        private CmsPage UpdateCmsPage(CmsPage source, CmsPage target)
        {
            target.isUserDefined = source.isUserDefined;
            target.CategoryId = source.CategoryId;
            target.Meta_AuthorContent = source.Meta_AuthorContent;
            target.Meta_CategoryContent = source.Meta_CategoryContent;
            target.Meta_DescriptionContent = source.Meta_DescriptionContent;
            target.Meta_LanguageContent = source.Meta_LanguageContent;
            target.Meta_RevisitAfterContent = source.Meta_RevisitAfterContent;
            target.Meta_RobotsContent = source.Meta_RobotsContent;
            target.Meta_Title = source.Meta_Title;
            target.PageHTML = source.PageHTML;
            target.PageKeywords = source.PageKeywords;
            target.PageTitle = source.PageTitle;
            // target.FileName = source.FileName,
            //target.Bytes = source.Bytes,
            target.PageBanner = source.PageBanner;
            target.isEnabled = source.isEnabled;
            target.CompanyId = source.CompanyId;
            return target;
        }
        #endregion

        #region ExportOrganisation
        public void ExportOrganisationRoutine1(long OrganisationID, ExportSets sets)
        {
            string DPath = string.Empty;

            ExportOrganisation ObjExportOrg = new Models.Common.ExportOrganisation();
            //  Organisation organisation = new Organisation();
            //  List<CostCentre> costCentre = new List<CostCentre>();
            List<CostCentreMatrixDetail> costCentreMatrixDetail = new List<CostCentreMatrixDetail>();
            List<CostCentreAnswer> CostCentreAnswers = new List<CostCentreAnswer>();

            List<CostCenterChoice> CostCenterChoice = new List<CostCenterChoice>();


            // get organisation to export
            // organisation = ;
            ObjExportOrg.Organisation = organisationRepository.GetOrganizatiobByOrganisationID(OrganisationID);

            // for paper size add organisationid in papersize
            ObjExportOrg.PaperSizes = PaperSizeRepository.GetPaperByOrganisation(OrganisationID);

            // get costcentres based on organisationid 
            //costCentre =
            ObjExportOrg.CostCentre = costCentreRepository.GetCostCentersByOrganisationID(OrganisationID, out CostCenterChoice);

            ObjExportOrg.CostCentreQuestion = CostCentreQuestionRepository.GetCostCentreQuestionsByOID(OrganisationID, out CostCentreAnswers);

            // for cost centre answers
            ObjExportOrg.CostCentreAnswer = CostCentreAnswers;


            // workinstructions based on costcentreid
            List<CostcentreInstruction> lstInstruction = new List<CostcentreInstruction>();
            if (ObjExportOrg.CostCentre != null && ObjExportOrg.CostCentre.Count > 0)
            {
                foreach (var cost in ObjExportOrg.CostCentre)
                {
                    // get instrction for each cost centre
                    if (cost.CostcentreInstructions != null)
                    {
                        List<CostcentreInstruction> instructions = cost.CostcentreInstructions.ToList();
                        if (instructions != null && instructions.Count > 0)
                        {
                            foreach (var ins in instructions)
                            {
                                lstInstruction.Add(ins);

                            }
                        }
                        ObjExportOrg.CostcentreInstruction = lstInstruction;
                    }

                }

            }

            // get cost centre matrix based on organisationID
            ObjExportOrg.CostCentreMatrix = costCentreMatrixRepositry.GetMatrixByOrganisationID(OrganisationID, out costCentreMatrixDetail);

            // cost centre matrix detail for each cost centre
            ObjExportOrg.CostCentreMatrixDetail = costCentreMatrixDetail;

            List<CostcentreResource> CCR = new List<CostcentreResource>();
            // set cost centre resources
            if (ObjExportOrg.CostCentre != null && ObjExportOrg.CostCentre.Count > 0)
            {
                foreach (var cost in ObjExportOrg.CostCentre)
                {
                    // get CostcentreResources for each cost centre
                    if (cost.CostcentreResources != null)
                    {
                        List<CostcentreResource> resources = cost.CostcentreResources.ToList();
                        if (resources != null && resources.Count > 0)
                        {
                            foreach (var res in resources)
                            {
                                CCR.Add(res);
                            }
                            ObjExportOrg.CostcentreResource = CCR;
                        }

                    }

                }

            }

            // set costcentre work instructions choices based on cost centre work instructions
            List<CostcentreWorkInstructionsChoice> lstCostcentreWorkInstructionsChoice = new List<CostcentreWorkInstructionsChoice>();
            if (ObjExportOrg.CostCentre != null && ObjExportOrg.CostCentre.Count > 0)
            {
                foreach (var cost in ObjExportOrg.CostCentre)
                {
                    // get instrction for each cost centre
                    if (cost.CostcentreInstructions != null)
                    {
                        List<CostcentreInstruction> instructions = cost.CostcentreInstructions.ToList();
                        if (instructions != null && instructions.Count > 0)
                        {
                            foreach (var ins in instructions)
                            {
                                if (ins.CostcentreWorkInstructionsChoices != null)
                                {
                                    List<CostcentreWorkInstructionsChoice> choices = ins.CostcentreWorkInstructionsChoices.ToList();

                                    if (choices != null && choices.Count > 0)
                                    {
                                        foreach (var choice in choices)
                                        {
                                            lstCostcentreWorkInstructionsChoice.Add(choice);
                                        }
                                    }
                                    ObjExportOrg.CostcentreWorkInstructionsChoice = lstCostcentreWorkInstructionsChoice;
                                }

                            }
                        }

                    }

                }

            }

            // cost centre choices 
            ObjExportOrg.CostCenterChoice = CostCenterChoice;

            ObjExportOrg.CostCentreType = costCentreRepository.GetCostCentreTypeByOrganisationID(OrganisationID);

            ObjExportOrg.SuppliersList = companyRepository.GetSupplierByOrganisationid(OrganisationID);

            //ObjExportOrg.DefaultTemplateFonts = templatefonts.getTemplateFonts();

            string Json = JsonConvert.SerializeObject(ObjExportOrg, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            // export json file
            string sOrgPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/OrganisationJson1.txt";
            System.IO.File.WriteAllText(sOrgPath, Json);

            Json = string.Empty;
            sets.ExportOrganisationSet1 = ObjExportOrg;

            ObjExportOrg = null;
            GC.Collect();



        }


        public void ExportOrganisationRoutine2(long OrganisationID, ExportSets sets)
        {


            List<StockCategory> StockCategories = new List<StockCategory>();
            List<StockSubCategory> StockSubCategories = new List<StockSubCategory>();


            ExportOrganisation exOrg = new Models.Common.ExportOrganisation();

            // get stockcategories based on organisation ID

            StockCategories = StockCategoryRepository.GetStockCategoriesByOrganisationID(OrganisationID);
            exOrg.StockCategory = StockCategories;


            List<StockSubCategory> lstSubCategory = new List<StockSubCategory>();
            // set stock subcategories of stock categories
            if (StockCategories != null && StockCategories.Count > 0)
            {
                foreach (var stock in StockCategories)
                {
                    if (stock.StockSubCategories != null && stock.StockSubCategories.Count > 0)
                    {
                        foreach (var stockSubCat in stock.StockSubCategories)
                        {

                            lstSubCategory.Add(stockSubCat);
                        }
                        exOrg.StockSubCategory = lstSubCategory;
                    }
                }
            }



            // Delivery carriers structure is not defined yet


            // reports 
            exOrg.Reports = ReportRepository.GetReportsByOrganisationID(OrganisationID);

            exOrg.ReportNote = ReportRepository.GetReportNotesByOrganisationID(OrganisationID);


            string Json2 = JsonConvert.SerializeObject(exOrg, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            // export json file
            string sOrgPath2 = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/OrganisationJson2.txt";
            System.IO.File.WriteAllText(sOrgPath2, Json2);

            Json2 = string.Empty;

            StockCategories = null;
            StockSubCategories = null;
            sets.ExportOrganisationSet2 = exOrg;
            exOrg = null;
            GC.Collect();

        }

        public void ExportOrganisationRoutine3(long OrganisationID, ExportSets sets)
        {
            ExportOrganisation exOrg2 = new Models.Common.ExportOrganisation();

            List<PhraseField> PhraseField = new List<PhraseField>();
            // get prefixes based on organisationID
            exOrg2.Prefixes = prefixRepository.GetPrefixesByOrganisationID(OrganisationID);

            exOrg2.Markups = markupRepository.GetMarkupsByOrganisationId(OrganisationID);
            // get machines by organisation id
            exOrg2.Machines = MachineRepository.GetMachinesByOrganisationID(OrganisationID);

            // get lookupmethods by organisationid
            exOrg2.LookupMethods = MachineRepository.getLookupmethodsbyOrganisationID(OrganisationID);


            // Phrases of organisation
            exOrg2.PhraseField = PhraseFieldRepository.GetPhraseFieldsByOrganisationID(OrganisationID);


            // organisationID in phrase fields
            List<Phrase> lstPhrase = new List<Phrase>();
            if (exOrg2.PhraseField != null)
            {
                foreach (var phrase in exOrg2.PhraseField)
                {

                    if (phrase.Phrases != null && phrase.Phrases.Count > 0)
                    {
                        foreach (var p in phrase.Phrases)
                        {
                            lstPhrase.Add(p);

                        }
                        exOrg2.Phrases = lstPhrase;
                    }
                }
            }


            // section flags of organisation
            exOrg2.SectionFlags = sectionFlagRepository.GetSectionFlagsByOrganisationID(OrganisationID);

            // organisation = null;
            //  costCentre = null;


            PhraseField = null;

            string Json3 = JsonConvert.SerializeObject(exOrg2, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            // export json file
            string sOrgPath3 = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/OrganisationJson3.txt";
            System.IO.File.WriteAllText(sOrgPath3, Json3);
            // Comapny Entities
            Json3 = String.Empty;
            GC.Collect();
            sets.ExportOrganisationSet3 = exOrg2;
            exOrg2 = null;


        }

        public void ExportOrganisationRoutine4(long OrganisationID, ExportSets sets)
        {
            ExportOrganisation exOrg = new Models.Common.ExportOrganisation();



            // get stockitems based on organisationID
            exOrg.StockItem = stockItemRepository.GetStockItemsByOrganisationID(OrganisationID);



            string Json4 = JsonConvert.SerializeObject(exOrg, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            // export json file
            string sOrgPath4 = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/OrganisationJson4.txt";
            System.IO.File.WriteAllText(sOrgPath4, Json4);

            Json4 = string.Empty;

            sets.ExportOrganisationSet4 = exOrg;
            exOrg = null;
            GC.Collect();

        }
        public bool ExportOrganisation(long OrganisationID, string RetailName, string RetailNameWOP, string CorporateName, string CorporateNameWOP)
        {
            try
            {
                #region OrganisationEntities
                string DPath = string.Empty;
                ExportOrganisation ObjExportOrg = new Models.Common.ExportOrganisation();
                ExportSets objSets = new ExportSets();

                ExportOrganisationRoutine1(OrganisationID, objSets);

                ExportOrganisationRoutine2(OrganisationID, objSets);

                ExportOrganisationRoutine3(OrganisationID, objSets);

                ExportOrganisationRoutine4(OrganisationID, objSets);

                // Set CompanyData

                ExportSets ObjExportCorporate = new Models.Common.ExportSets();
                long CompanyID = 0;

                if (CorporateName != "''")
                {
                    // export corporate store with products
                    CompanyID = companyRepository.GetCompanyByName(OrganisationID, CorporateName);


                    if (CompanyID > 0)
                    {
                        ObjExportCorporate = companyRepository.ExportCorporateCompany(CompanyID);

                    }
                }

                ExportSets ObjExportRetail = new Models.Common.ExportSets();
                long RetailCompanyID = 0;
                // export retail store with Products
                if (RetailName != "''")
                {
                    RetailCompanyID = companyRepository.GetCompanyByName(OrganisationID, RetailName);

                    if (RetailCompanyID > 0)
                    {

                        ObjExportRetail = ExportRetailStore(RetailCompanyID, OrganisationID);
                    }

                }

                ExportSets ObjExportCorporateWOProducts = new Models.Common.ExportSets();
                long CompanyWOP = 0;
                // export corporate store without products
                if (CorporateNameWOP != "''")
                {
                    CompanyWOP = companyRepository.GetCompanyByName(OrganisationID, CorporateNameWOP);

                    if (CompanyWOP > 0 && CompanyWOP != null)
                    {
                        ObjExportCorporateWOProducts = companyRepository.ExportCorporateCompanyWithoutProducts(CompanyWOP);

                    }
                }


                ExportSets ObjExportRetailWOProducts = new Models.Common.ExportSets();
                long RetailCompanyWOP = 0;
                // export retail store without products
                if (RetailNameWOP != "''")
                {
                    RetailCompanyWOP = companyRepository.GetCompanyByName(OrganisationID, RetailNameWOP);


                    if (RetailCompanyWOP > 0 && RetailCompanyWOP != null)
                    {

                        ObjExportRetailWOProducts = ExportRetailStoreWithoutProducts(RetailCompanyWOP, OrganisationID);
                    }
                }


                #endregion


                #region ExportFiles

                CopyFiles(objSets, ObjExportCorporate, ObjExportRetail, DPath, OrganisationID, CompanyID, RetailCompanyID, CompanyWOP, RetailCompanyWOP, ObjExportCorporateWOProducts, ObjExportRetailWOProducts);


                #endregion

                return true;

            }
            catch (Exception ex)
            {

                throw new MPCException(ex.ToString(), OrganisationID);

            }

        }

        public void CopyFiles(ExportSets ExportSets, ExportSets ObjExportCorporateSet, ExportSets ObjExportRetailSet, string DPath, long OrganisationID, long CompanyID, long RetailCompanyID, long CompanyIDWOP, long RetailCompanyIDWOP, ExportSets ObjExportCorporateWOProducts, ExportSets ObjExportRetailWOProducts)
        {
            try
            {
                List<string> JsonFiles = new List<string>();
                using (ZipFile zip = new ZipFile())
                {
                    string sOrgPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/OrganisationJson1.txt";
                    if (File.Exists(sOrgPath))
                    {
                        ZipEntry r = zip.AddFile(sOrgPath, "");
                        r.Comment = "Json File for an organisation";
                    }
                    string sOrgPath2 = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/OrganisationJson2.txt";
                    if (File.Exists(sOrgPath2))
                    {
                        ZipEntry r = zip.AddFile(sOrgPath2, "");
                        r.Comment = "Json File for an organisation";
                    }
                    string sOrgPath3 = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/OrganisationJson3.txt";
                    if (File.Exists(sOrgPath3))
                    {
                        ZipEntry r = zip.AddFile(sOrgPath3, "");
                        r.Comment = "Json File for an organisation";
                    }
                    string sOrgPath4 = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/OrganisationJson4.txt";
                    if (File.Exists(sOrgPath4))
                    {
                        ZipEntry r = zip.AddFile(sOrgPath4, "");
                        r.Comment = "Json File for an organisation";
                    }
                    string sCorpPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/CorporateJson1.txt";
                    if (File.Exists(sCorpPath))
                    {
                        ZipEntry r = zip.AddFile(sCorpPath, "");
                        r.Comment = "Json File for a corporate Company";
                    }
                    string sCorpPath2 = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/CorporateJson2.txt";
                    if (File.Exists(sCorpPath2))
                    {
                        ZipEntry r = zip.AddFile(sCorpPath2, "");
                        r.Comment = "Json File for a corporate Company";
                    }

                    //string sCorpPath3 = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/CorporateSecondaryPages.txt";
                    //if (File.Exists(sCorpPath3))
                    //{
                    //    ZipEntry r = zip.AddFile(sCorpPath3, "");
                    //    r.Comment = "Json File for a corporate Company";
                    //}
                    string sCorpPath4 = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/CorporateProductCategories.txt";
                    if (File.Exists(sCorpPath4))
                    {
                        ZipEntry r = zip.AddFile(sCorpPath4, "");
                        r.Comment = "Json File for a corporate Company";
                    }
                    string sRetailPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/RetailJson1.txt";
                    if (File.Exists(sRetailPath))
                    {
                        ZipEntry r = zip.AddFile(sRetailPath, "");
                        r.Comment = "Json File for a retail Company";
                    }
                    string sRetailPath2 = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/RetailJson2.txt";
                    if (File.Exists(sRetailPath2))
                    {
                        ZipEntry r = zip.AddFile(sRetailPath2, "");
                        r.Comment = "Json File for retail Company";
                    }
                    //string sRetailPath3 = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/RetailSecondaryPages.txt";
                    //if (File.Exists(sRetailPath3))
                    //{
                    //    ZipEntry r = zip.AddFile(sRetailPath3, "");
                    //    r.Comment = "Json File for a retail Company";
                    //}
                    string sRetailPath4 = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/RetailProductCategories.txt";
                    if (File.Exists(sRetailPath4))
                    {
                        ZipEntry r = zip.AddFile(sRetailPath4, "");
                        r.Comment = "Json File for retail Company";
                    }
                    // for retail store without products
                    string sRetailPathWOP = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/RetailJson1WOP.txt";
                    if (File.Exists(sRetailPathWOP))
                    {
                        ZipEntry r = zip.AddFile(sRetailPathWOP, "");
                        r.Comment = "Json File for a retail Company";
                    }
                    string sRetailPath2WOP = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/RetailJson2WOP.txt";
                    if (File.Exists(sRetailPath2WOP))
                    {
                        ZipEntry r = zip.AddFile(sRetailPath2WOP, "");
                        r.Comment = "Json File for retail Company";
                    }
                    //string sRetailPath3WOP = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/RetailSecondaryPagesWOP.txt";
                    //if (File.Exists(sRetailPath3WOP))
                    //{
                    //    ZipEntry r = zip.AddFile(sRetailPath3WOP, "");
                    //    r.Comment = "Json File for a retail Company";
                    //}
                    string sRetailPath4WOP = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/RetailProductCategoriesWOP.txt";
                    if (File.Exists(sRetailPath4WOP))
                    {
                        ZipEntry r = zip.AddFile(sRetailPath4WOP, "");
                        r.Comment = "Json File for retail Company";
                    }

                    // for Corporate store without products
                    string sCorpPathWOP = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/CorporateJson1WOP.txt";
                    if (File.Exists(sCorpPathWOP))
                    {
                        ZipEntry r = zip.AddFile(sCorpPathWOP, "");
                        r.Comment = "Json File for a retail Company";
                    }
                    string sCorpPath2WOP = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/CorporateJson2WOP.txt";
                    if (File.Exists(sCorpPath2WOP))
                    {
                        ZipEntry r = zip.AddFile(sCorpPath2WOP, "");
                        r.Comment = "Json File for retail Company";
                    }
                    //string sCorpPath3WOP = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/CorporateSecondaryPagesWOP.txt";
                    //if (File.Exists(sCorpPath3WOP))
                    //{
                    //    ZipEntry r = zip.AddFile(sCorpPath3WOP, "");
                    //    r.Comment = "Json File for a retail Company";
                    //}
                    string sCorpPath4WOP = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/CorporateProductCategoriesWOP.txt";
                    if (File.Exists(sCorpPath4WOP))
                    {
                        ZipEntry r = zip.AddFile(sCorpPath4WOP, "");
                        r.Comment = "Json File for retail Company";
                    }
                    JsonFiles.Add(sOrgPath);
                    JsonFiles.Add(sOrgPath2);
                    JsonFiles.Add(sOrgPath3);
                    JsonFiles.Add(sOrgPath4);
                    JsonFiles.Add(sCorpPath);
                    JsonFiles.Add(sCorpPath2);
                    // JsonFiles.Add(sCorpPath3);
                    JsonFiles.Add(sCorpPath4);
                    JsonFiles.Add(sRetailPath);
                    JsonFiles.Add(sRetailPath2);
                    //  JsonFiles.Add(sRetailPath3);
                    JsonFiles.Add(sRetailPath4);
                    JsonFiles.Add(sRetailPathWOP);
                    JsonFiles.Add(sRetailPath2WOP);
                    // JsonFiles.Add(sRetailPath3WOP);
                    JsonFiles.Add(sRetailPath4WOP);
                    JsonFiles.Add(sCorpPathWOP);
                    JsonFiles.Add(sCorpPath2WOP);
                    // JsonFiles.Add(sCorpPath3WOP);
                    JsonFiles.Add(sCorpPath4WOP);
                    //string sRetailPath5 = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/RetailJson5.txt";
                    //if (File.Exists(sRetailPath5))
                    //{
                    //    ZipEntry r = zip.AddFile(sRetailPath5, "");
                    //    r.Comment = "Json File for a corporate Company";
                    //}



                    //export language file for an organisation

                    // Add all files in directory
                    string FolderPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Resources/" + OrganisationID;

                    if (Directory.Exists(FolderPath))
                    {

                        foreach (string newPath in Directory.GetFiles(FolderPath, "*.*", SearchOption.AllDirectories))
                        {
                            string Lname = Path.GetFileName(newPath);


                            string directoty = Path.GetDirectoryName(newPath);
                            string[] stringSeparators = new string[] { "MPC_Content" };
                            if (!string.IsNullOrEmpty(directoty))
                            {
                                string[] result = directoty.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

                                string FolderName = result[1];
                                if (!string.IsNullOrEmpty(FolderName))
                                {
                                    string[] folder = FolderName.Split('\\');
                                    directoty = "/Resources/" + OrganisationID + "/" + folder[3];

                                    ZipEntry r = zip.AddFile(newPath, directoty);
                                    r.Comment = "Language File for an organisation";
                                }


                            }

                        }
                    }


                    // export MIS logo in Organisation
                    ExportOrganisation ObjExportOrg = new ExportOrganisation();
                    ObjExportOrg = ExportSets.ExportOrganisationSet1;

                    if (ObjExportOrg.Organisation != null)
                    {

                        if (ObjExportOrg.Organisation.MISLogo != null)
                        {
                            string FilePath = HttpContext.Current.Server.MapPath("~/" + ObjExportOrg.Organisation.MISLogo);
                            DPath = "/Organisations/" + OrganisationID;
                            if (File.Exists(FilePath))
                            {
                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                r.Comment = "MIS Logo for an organisation";

                            }
                        }

                        if (ObjExportOrg.Organisation.WebsiteLogo != null)
                        {
                            string FilePath = HttpContext.Current.Server.MapPath("~/" + ObjExportOrg.Organisation.WebsiteLogo);
                            DPath = "/Organisations/" + OrganisationID;
                            if (File.Exists(FilePath))
                            {
                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                r.Comment = "MIS Logo for an organisation";

                            }
                        }
                    }

                    if (ObjExportOrg.SuppliersList != null && ObjExportOrg.SuppliersList.Count > 0)
                    {
                        foreach (var supList in ObjExportOrg.SuppliersList)
                        {
                            if (supList != null)
                            {
                                string FilePath = HttpContext.Current.Server.MapPath("~/" + supList.Image);
                                DPath = "/Assets/" + OrganisationID + "/" + supList.CompanyId;
                                if (File.Exists(FilePath))
                                {
                                    ZipEntry r = zip.AddFile(FilePath, DPath);
                                    r.Comment = "Company Logo for Store";

                                }
                            }


                        }
                    }
                    // export cost centre images
                    if (ObjExportOrg.CostCentre != null && ObjExportOrg.CostCentre.Count > 0)
                    {
                        foreach (var objCost in ObjExportOrg.CostCentre)
                        {
                            if (objCost.ThumbnailImageURL != null)
                            {

                                string FilePath = HttpContext.Current.Server.MapPath("~/" + objCost.ThumbnailImageURL);
                                DPath = "/CostCentres/" + OrganisationID + "/" + objCost.CostCentreId;
                                if (File.Exists(FilePath))
                                {
                                    ZipEntry r = zip.AddFile(FilePath, DPath);
                                    r.Comment = "Thumbnail image for cost centre";

                                }
                            }
                            if (objCost.MainImageURL != null)
                            {
                                string FilePath = HttpContext.Current.Server.MapPath("~/" + objCost.MainImageURL);
                                DPath = "/CostCentres/" + OrganisationID + "/" + objCost.CostCentreId;
                                if (File.Exists(FilePath))
                                {
                                    ZipEntry r = zip.AddFile(FilePath, DPath);
                                    r.Comment = "Main image for cost centre";

                                }
                            }
                        }
                    }

                    ObjExportOrg = null;

                    ExportOrganisation ObExportOrg2 = new ExportOrganisation();
                    ObExportOrg2 = ExportSets.ExportOrganisationSet2;
                    //// export report banner
                    if (ObExportOrg2.ReportNote != null && ObExportOrg2.ReportNote.Count > 0)
                    {

                        foreach (var report in ObExportOrg2.ReportNote)
                        {
                            if (report.ReportBanner != null)
                            {
                                string FilePath = HttpContext.Current.Server.MapPath("~/" + report.ReportBanner);
                                DPath = "/Media/" + OrganisationID;
                                if (File.Exists(FilePath))
                                {
                                    ZipEntry r = zip.AddFile(FilePath, DPath);
                                    r.Comment = "Media Files for Store";

                                }
                            }
                        }
                    }
                    ObExportOrg2 = null;
                    // export fonts
                    List<TemplateFont> lsttemplateFonts = templatefonts.getTemplateFonts();
                    if (lsttemplateFonts != null && lsttemplateFonts.Count > 0)
                    {
                        foreach (var font in lsttemplateFonts)
                        {

                            if (string.IsNullOrEmpty(font.FontPath))
                            {


                                string F1 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID + "/WebFonts/" + font.FontFile + ".eot");

                                string F2 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID + "/WebFonts/" + font.FontFile + ".ttf");

                                string F3 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID + "/WebFonts/" + font.FontFile + ".woff");

                                DPath = "Designer/Organisation" + OrganisationID + "/WebFonts";



                                if (File.Exists(F1))
                                {
                                    ZipEntry r = zip.AddFile(F1, DPath);
                                    r.Comment = "template font";
                                }

                                if (File.Exists(F2))
                                {
                                    ZipEntry r = zip.AddFile(F2, DPath);
                                    r.Comment = "template font";
                                }

                                if (File.Exists(F3))
                                {
                                    ZipEntry r = zip.AddFile(F3, DPath);
                                    r.Comment = "template font";
                                }

                            }

                        }
                    }

                    // export corporate company Flow
                    #region export corporate files
                    ExportOrganisation ObjExportCorp = new Models.Common.ExportOrganisation();
                    ObjExportCorp = ObjExportCorporateSet.ExportStore1;
                    if (ObjExportCorp != null)
                    {
                        if (ObjExportCorp.Company != null)
                        {
                            //// export company Logo
                            if (ObjExportCorp.Company.Image != null)
                            {
                                string FilePath = HttpContext.Current.Server.MapPath("~/" + ObjExportCorp.Company.Image);
                                DPath = "/Assets/" + OrganisationID + "/" + CompanyID;
                                if (File.Exists(FilePath))
                                {
                                    ZipEntry r = zip.AddFile(FilePath, DPath);
                                    r.Comment = "Company Logo for Store";

                                }
                            }

                            if (ObjExportCorp.Company.StoreBackgroundImage != null)
                            {
                                string FilePath = HttpContext.Current.Server.MapPath("~/" + ObjExportCorp.Company.StoreBackgroundImage);
                                DPath = "/Assets/" + OrganisationID + "/" + CompanyID;
                                if (File.Exists(FilePath))
                                {
                                    ZipEntry r = zip.AddFile(FilePath, DPath);
                                    r.Comment = "Background image for Store";

                                }
                            }
                            // export media

                            if (ObjExportCorp.Company.MediaLibraries != null)
                            {
                                if (ObjExportCorp.Company.MediaLibraries.Count > 0)
                                {
                                    foreach (var media in ObjExportCorp.Company.MediaLibraries)
                                    {
                                        if (media.FilePath != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + media.FilePath);
                                            DPath = "/Media/" + OrganisationID + "/" + CompanyID;
                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Media Files for Store";

                                            }
                                        }
                                    }
                                }
                            }
                            List<ProductCategory> categories = new List<ProductCategory>();
                            categories = ObjExportCorporateSet.ExportStore2;

                            if (categories != null)
                            {
                                foreach (var cat in categories)
                                {
                                    if (cat.ImagePath != null)
                                    {
                                        string FilePath = HttpContext.Current.Server.MapPath("~/" + cat.ImagePath);
                                        DPath = "/Assets/" + OrganisationID + "/" + CompanyID + "/ProductCategories";
                                        if (File.Exists(FilePath))
                                        {
                                            ZipEntry r = zip.AddFile(FilePath, DPath);
                                            r.Comment = "Category Image Path for Store";

                                        }
                                    }

                                    if (cat.ThumbnailPath != null)
                                    {
                                        string FilePath = HttpContext.Current.Server.MapPath("~/" + cat.ThumbnailPath);
                                        DPath = "/Assets/" + OrganisationID + "/" + CompanyID + "/ProductCategories";

                                        if (File.Exists(FilePath))
                                        {
                                            ZipEntry r = zip.AddFile(FilePath, DPath);
                                            r.Comment = "Category Thumbnail Path for Store";

                                        }
                                    }

                                }

                            }
                            List<Item> exItemsCorp = new List<Item>();
                            exItemsCorp = ObjExportCorporateSet.ExportStore3;
                            if (exItemsCorp != null)
                            {
                                if (exItemsCorp.Count > 0)
                                {
                                    foreach (var item in exItemsCorp)
                                    {
                                        if (item.ImagePath != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + item.ImagePath);
                                            DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Items Image Path for Store";

                                            }
                                        }

                                        if (item.ThumbnailPath != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + item.ThumbnailPath);
                                            DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Items Thumbnail Path for Store";

                                            }
                                        }

                                        if (item.GridImage != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + item.GridImage);
                                            DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Items Grid image for Store";

                                            }
                                        }
                                        if (item.File1 != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + item.File1);
                                            DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Items image for Store";

                                            }
                                        }
                                        if (item.File2 != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + item.File2);
                                            DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Items image for Store";

                                            }
                                        }
                                        if (item.File3 != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + item.File3);
                                            DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Items image for Store";

                                            }
                                        }
                                        if (item.File4 != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + item.File4);
                                            DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Items image for Store";

                                            }
                                        }
                                        if (item.File5 != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + item.File5);
                                            DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Items image for Store";

                                            }
                                        }

                                        if (item.ItemImages != null && item.ItemImages.Count > 0)
                                        {
                                            foreach (var img in item.ItemImages)
                                            {
                                                if (!string.IsNullOrEmpty(img.ImageURL))
                                                {
                                                    string FilePath = HttpContext.Current.Server.MapPath("~/" + img.ImageURL);
                                                    DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                                    if (File.Exists(FilePath))
                                                    {
                                                        ZipEntry r = zip.AddFile(FilePath, DPath);
                                                        r.Comment = "Items image for Store";

                                                    }
                                                }

                                            }
                                        }
                                        if (item.TemplateId != null && item.TemplateId > 0)
                                        {
                                            if (item.DesignerCategoryId == 0 || item.DesignerCategoryId == null)
                                            {
                                                if (item.Template.TemplateBackgroundImages != null && item.Template.TemplateBackgroundImages.Count > 0)
                                                {
                                                    foreach (var tempbcI in item.Template.TemplateBackgroundImages)
                                                    {



                                                        if (!string.IsNullOrEmpty(tempbcI.ImageName))
                                                        {

                                                            string FilePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID + "/Templates/" + tempbcI.ImageName);


                                                            if (tempbcI.ImageName.Contains("UserImgs/"))
                                                            {

                                                                DPath = "/Designer/Organisation" + OrganisationID + "/Templates/UserImgs/" + ObjExportCorp.Company.CompanyId;
                                                            }
                                                            else
                                                            {
                                                                DPath = "/Designer/Organisation" + OrganisationID + "/Templates/" + tempbcI.ProductId;
                                                            }


                                                            if (File.Exists(FilePath))
                                                            {
                                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                                r.Comment = "Items image for Store";

                                                            }

                                                            string Filename = tempbcI.ImageName;
                                                            string OldPath = Path.GetFileNameWithoutExtension(tempbcI.ImageName);

                                                            string newPath = OldPath + "_thumb";

                                                            Filename = Filename.Replace(OldPath, newPath);

                                                            string oPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID + "/Templates/" + Filename);
                                                            if (tempbcI.ImageName.Contains("UserImgs/"))
                                                            {

                                                                DPath = "/Designer/Organisation" + OrganisationID + "/Templates/UserImgs/" + ObjExportCorp.Company.CompanyId;
                                                            }
                                                            else
                                                            {
                                                                DPath = "/Designer/Organisation" + OrganisationID + "/Templates/" + tempbcI.ProductId;
                                                            }
                                                            if (File.Exists(oPath))
                                                            {
                                                                ZipEntry r = zip.AddFile(oPath, DPath);
                                                                r.Comment = "Items image for Store";
                                                            }

                                                        }

                                                    }
                                                }


                                                if (item.Template.TemplatePages != null && item.Template.TemplatePages.Count > 0)
                                                {
                                                    foreach (var tempPage in item.Template.TemplatePages)
                                                    {
                                                        if (!string.IsNullOrEmpty(tempPage.BackgroundFileName))
                                                        {
                                                            string TemplatePagesFile = "/MPC_Content/Designer/Organisation" + OrganisationID + "/Templates/" + tempPage.BackgroundFileName;
                                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + TemplatePagesFile);
                                                            DPath = "/Designer/Organisation" + OrganisationID + "/Templates/" + tempPage.ProductId;
                                                            if (File.Exists(FilePath))
                                                            {
                                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                                r.Comment = "Items image for Store";

                                                            }

                                                        }

                                                        string fileName = "templatImgBk" + tempPage.PageNo + ".jpg";
                                                        string oPath = "/MPC_Content/Designer/Organisation" + OrganisationID + "/Templates/" + tempPage.ProductId + "/" + fileName;
                                                        string FilePaths = HttpContext.Current.Server.MapPath("~/" + oPath);
                                                        DPath = "/Designer/Organisation" + OrganisationID + "/Templates/" + tempPage.ProductId;
                                                        if (File.Exists(FilePaths))
                                                        {
                                                            ZipEntry r = zip.AddFile(FilePaths, DPath);
                                                            r.Comment = "Items image for Store";
                                                        }

                                                    }
                                                }

                                            }
                                            //if(ObjExportRetail.TemplateBackgroundImage)

                                        }
                                    }

                                }
                            }
                            // copy template fonts of customer
                            if (ObjExportCorp.TemplateFonts != null && ObjExportCorp.TemplateFonts.Count > 0)
                            {
                                foreach (var font in ObjExportCorp.TemplateFonts)
                                {
                                    if (!string.IsNullOrEmpty(font.FontPath))
                                    {

                                        if (!string.IsNullOrEmpty(font.FontPath))
                                        {


                                            string F1 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + font.FontPath + "/" + font.FontFile + ".eot");

                                            string F2 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + font.FontPath + "/" + font.FontFile + ".ttf");

                                            string F3 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + font.FontPath + "/" + font.FontFile + ".woff");

                                            DPath = "Designer/" + font.FontPath + "/" + font.FontFile + ".eot";

                                            string Dpath2 = "Designer/" + font.FontPath + "/" + font.FontFile + ".ttf";

                                            string DPath3 = "Designer/" + font.FontPath + "/" + font.FontFile + ".woff";

                                            if (File.Exists(F1))
                                            {
                                                ZipEntry r = zip.AddFile(F1, DPath);
                                                r.Comment = "template font";
                                            }

                                            if (File.Exists(F2))
                                            {
                                                ZipEntry r = zip.AddFile(F2, Dpath2);
                                                r.Comment = "template font";
                                            }

                                            if (File.Exists(F3))
                                            {
                                                ZipEntry r = zip.AddFile(F3, DPath3);
                                                r.Comment = "template font";
                                            }


                                        }
                                    }
                                }
                            }

                            if (ObjExportCorp.Company.CompanyContacts != null && ObjExportCorp.Company.CompanyContacts.Count > 0)
                            {
                                foreach (var contact in ObjExportCorp.Company.CompanyContacts)
                                {
                                    if (!string.IsNullOrEmpty(contact.image))
                                    {
                                        string ContactImage = HttpContext.Current.Server.MapPath("~/" + contact.image);
                                        string ContactDirectory = "/Assets/" + OrganisationID + "/" + CompanyID + "/Contacts/" + contact.ContactId;
                                        if (File.Exists(ContactImage))
                                        {
                                            ZipEntry r = zip.AddFile(ContactImage, ContactDirectory);
                                            r.Comment = "Contact images for Store";

                                        }
                                    }
                                }

                            }


                            string CSSPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Assets/" + OrganisationID + "/" + CompanyID + "/Site.css";
                            string pCSSDirectory = "/Assets/" + OrganisationID + "/" + CompanyID;
                            if (File.Exists(CSSPath))
                            {
                                ZipEntry r = zip.AddFile(CSSPath, pCSSDirectory);
                                r.Comment = "CSS for Store";

                            }
                            string SpritePath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Assets/" + OrganisationID + "/" + CompanyID + "/sprite.png";
                            string pDirectory = "/Assets/" + OrganisationID + "/" + CompanyID;

                            if (File.Exists(SpritePath))
                            {
                                ZipEntry r = zip.AddFile(SpritePath, pDirectory);
                                r.Comment = "Sprite for Store";

                            }

                        }
                    }
                    #endregion

                    #region export retail store
                    // export files of retail store

                    ExportOrganisation ObjExportRetail = new Models.Common.ExportOrganisation();
                    ObjExportRetail = ObjExportRetailSet.ExportRetailStore1;
                    if (ObjExportRetail != null)
                    {
                        if (ObjExportRetail.RetailCompany != null)
                        {
                            if (ObjExportRetail.RetailCompany.Image != null)
                            {
                                string FilePath = HttpContext.Current.Server.MapPath("~/" + ObjExportRetail.RetailCompany.Image);
                                DPath = "/Assets/" + OrganisationID + "/" + RetailCompanyID;
                                if (File.Exists(FilePath))
                                {
                                    ZipEntry r = zip.AddFile(FilePath, DPath);
                                    r.Comment = "Company Logo for Store";

                                }
                            }
                            if (ObjExportRetail.RetailCompany.StoreBackgroundImage != null)
                            {
                                string FilePath = HttpContext.Current.Server.MapPath("~/" + ObjExportRetail.RetailCompany.StoreBackgroundImage);
                                DPath = "/Assets/" + OrganisationID + "/" + RetailCompanyID;
                                if (File.Exists(FilePath))
                                {
                                    ZipEntry r = zip.AddFile(FilePath, DPath);
                                    r.Comment = "Background image for Store";

                                }
                            }

                            // export media

                            if (ObjExportRetail.RetailCompany.MediaLibraries != null)
                            {
                                if (ObjExportRetail.RetailCompany.MediaLibraries.Count > 0)
                                {
                                    foreach (var media in ObjExportRetail.RetailCompany.MediaLibraries)
                                    {
                                        if (media.FilePath != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + media.FilePath);
                                            DPath = "/Media/" + OrganisationID + "/" + RetailCompanyID;
                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Media Files for Store";

                                            }
                                        }
                                    }
                                }
                            }

                            List<ProductCategory> categories = new List<ProductCategory>();
                            categories = ObjExportRetailSet.ExportRetailStore2;

                            if (categories != null)
                            {
                                foreach (var cat in categories)
                                {
                                    if (cat.ImagePath != null)
                                    {
                                        string FilePath = HttpContext.Current.Server.MapPath("~/" + cat.ImagePath);
                                        DPath = "/Assets/" + OrganisationID + "/" + RetailCompanyID + "/ProductCategories";
                                        if (File.Exists(FilePath))
                                        {
                                            ZipEntry r = zip.AddFile(FilePath, DPath);
                                            r.Comment = "Category Image Path for Store";

                                        }
                                    }

                                    if (cat.ThumbnailPath != null)
                                    {
                                        string FilePath = HttpContext.Current.Server.MapPath("~/" + cat.ThumbnailPath);
                                        DPath = "/Assets/" + OrganisationID + "/" + RetailCompanyID + "/ProductCategories";

                                        if (File.Exists(FilePath))
                                        {
                                            ZipEntry r = zip.AddFile(FilePath, DPath);
                                            r.Comment = "Category Thumbnail Path for Store";

                                        }
                                    }

                                }

                            }

                            List<Item> exItems = new List<Item>();
                            exItems = ObjExportRetailSet.ExportRetailStore3;
                            if (exItems != null)
                            {
                                if (exItems.Count > 0)
                                {
                                    foreach (var item in exItems)
                                    {
                                        if (item.ImagePath != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + item.ImagePath);
                                            DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Items Image Path for Store";

                                            }
                                        }

                                        if (item.ThumbnailPath != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + item.ThumbnailPath);
                                            DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Items Thumbnail Path for Store";

                                            }
                                        }

                                        if (item.GridImage != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + item.GridImage);
                                            DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Items Grid image for Store";

                                            }
                                        }
                                        if (item.File1 != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + item.File1);
                                            DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Items image for Store";

                                            }
                                        }
                                        if (item.File2 != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + item.File2);
                                            DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Items image for Store";

                                            }
                                        }
                                        if (item.File3 != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + item.File3);
                                            DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Items image for Store";

                                            }
                                        }
                                        if (item.File4 != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + item.File4);
                                            DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Items image for Store";

                                            }
                                        }
                                        if (item.File5 != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + item.File5);
                                            DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Items image for Store";

                                            }
                                        }

                                        if (item.ItemImages != null && item.ItemImages.Count > 0)
                                        {
                                            foreach (var img in item.ItemImages)
                                            {
                                                if (!string.IsNullOrEmpty(img.ImageURL))
                                                {
                                                    string FilePath = HttpContext.Current.Server.MapPath("~/" + img.ImageURL);
                                                    DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                                    if (File.Exists(FilePath))
                                                    {
                                                        ZipEntry r = zip.AddFile(FilePath, DPath);
                                                        r.Comment = "Items image for Store";

                                                    }
                                                }

                                            }
                                        }
                                        if (item.TemplateId != null && item.TemplateId > 0)
                                        {
                                            if (item.DesignerCategoryId == 0 || item.DesignerCategoryId == null)
                                            {
                                                if (item.Template != null)
                                                {
                                                    //if (item.Template.TemplateFonts != null && item.Template.TemplateFonts.Count > 0)
                                                    //{
                                                    //    foreach (var tempFont in item.Template.TemplateFonts)
                                                    //    {
                                                    //        if (!string.IsNullOrEmpty(tempFont.FontPath))
                                                    //        {
                                                    //            string F1 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID + "/WebFonts/" + tempFont.FontPath + "/" + tempFont.FontFile + ".eot");

                                                    //            string F2 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID + "/WebFonts/" + tempFont.FontPath + "/" + tempFont.FontFile + ".ttf");

                                                    //            string F3 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID + "/WebFonts/" + tempFont.FontPath + "/" + tempFont.FontFile + ".woff");

                                                    //            DPath = "Designer/Organisation" + OrganisationID + "/WebFonts/" + tempFont.FontPath + "/" + tempFont.FontFile + ".eot";

                                                    //            string Dpath2 = "Designer/Organisation" + OrganisationID + "/WebFonts/" + tempFont.FontPath + "/" + tempFont.FontFile + ".ttf";

                                                    //            string DPath3 = "Designer/Organisation" + OrganisationID + "/WebFonts/" + tempFont.FontPath + "/" + tempFont.FontFile + ".woff";

                                                    //            if (File.Exists(F1))
                                                    //            {
                                                    //                ZipEntry r = zip.AddFile(F1, DPath);
                                                    //                r.Comment = "template font";
                                                    //            }

                                                    //            if (File.Exists(F2))
                                                    //            {
                                                    //                ZipEntry r = zip.AddFile(F2, Dpath2);
                                                    //                r.Comment = "template font";
                                                    //            }

                                                    //            if (File.Exists(F3))
                                                    //            {
                                                    //                ZipEntry r = zip.AddFile(F3, DPath3);
                                                    //                r.Comment = "template font";
                                                    //            }


                                                    //        }
                                                    //        else
                                                    //        {

                                                    //            string F1 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID + "/WebFonts/" + tempFont.FontFile + ".eot");

                                                    //            string F2 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID + "/WebFonts/" + tempFont.FontFile + ".ttf");

                                                    //            string F3 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID + "/WebFonts/" + tempFont.FontFile + ".woff");

                                                    //            DPath = "Designer/Organisation" + OrganisationID + "/WebFonts/" + tempFont.FontFile + ".eot";

                                                    //            string Dpath2 = "Designer/Organisation" + OrganisationID + "/WebFonts/" + tempFont.FontFile + ".ttf";

                                                    //            string DPath3 = "Designer/Organisation" + OrganisationID + "/WebFonts/" + tempFont.FontFile + ".woff";

                                                    //            if (File.Exists(F1))
                                                    //            {
                                                    //                ZipEntry r = zip.AddFile(F1, DPath);
                                                    //                r.Comment = "template font";
                                                    //            }

                                                    //            if (File.Exists(F2))
                                                    //            {
                                                    //                ZipEntry r = zip.AddFile(F2, Dpath2);
                                                    //                r.Comment = "template font";
                                                    //            }

                                                    //            if (File.Exists(F3))
                                                    //            {
                                                    //                ZipEntry r = zip.AddFile(F3, DPath3);
                                                    //                r.Comment = "template font";
                                                    //            }
                                                    //        }

                                                    //    }


                                                    //}
                                                    if (item.Template.TemplateBackgroundImages != null && item.Template.TemplateBackgroundImages.Count > 0)
                                                    {
                                                        foreach (var tempbcI in item.Template.TemplateBackgroundImages)
                                                        {



                                                            if (!string.IsNullOrEmpty(tempbcI.ImageName))
                                                            {

                                                                string FilePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Templates/" + tempbcI.ImageName);


                                                                if (tempbcI.ImageName.Contains("UserImgs/"))
                                                                {

                                                                    DPath = "/Designer/Organisation" + OrganisationID + "/Templates/UserImgs/" + ObjExportRetail.RetailCompany.CompanyId;
                                                                }
                                                                else
                                                                {
                                                                    DPath = "/Designer/Organisation" + OrganisationID + "/Templates/" + tempbcI.ProductId;
                                                                }

                                                                if (File.Exists(FilePath))
                                                                {
                                                                    ZipEntry r = zip.AddFile(FilePath, DPath);
                                                                    r.Comment = "Items image for Store";

                                                                }

                                                            }

                                                        }
                                                    }
                                                    if (item.Template.TemplatePages != null && item.Template.TemplatePages.Count > 0)
                                                    {
                                                        foreach (var tempPage in item.Template.TemplatePages)
                                                        {
                                                            if (!string.IsNullOrEmpty(tempPage.BackgroundFileName))
                                                            {
                                                                string TemplatePagesFile = "/MPC_Content/Designer/Organisation" + OrganisationID + "/Templates/" + tempPage.BackgroundFileName;
                                                                string FilePath = HttpContext.Current.Server.MapPath("~/" + TemplatePagesFile);
                                                                DPath = "/Designer/Organisation" + OrganisationID + "/Templates/" + tempPage.ProductId;
                                                                if (File.Exists(FilePath))
                                                                {
                                                                    ZipEntry r = zip.AddFile(FilePath, DPath);
                                                                    r.Comment = "Items image for Store";

                                                                }

                                                            }
                                                            string fileName = "templatImgBk" + tempPage.PageNo + ".jpg";
                                                            string oPath = "/MPC_Content/Designer/Organisation" + OrganisationID + "/Templates/" + tempPage.ProductId + "/" + fileName;
                                                            string FilePaths = HttpContext.Current.Server.MapPath("~/" + oPath);
                                                            DPath = "/Designer/Organisation" + OrganisationID + "/Templates/" + tempPage.ProductId;
                                                            if (File.Exists(FilePaths))
                                                            {
                                                                ZipEntry r = zip.AddFile(FilePaths, DPath);
                                                                r.Comment = "Items image for Store";
                                                            }
                                                        }
                                                    }


                                                }

                                            }

                                        }
                                    }

                                }
                            }
                            if (ObjExportRetail.RetailTemplateFonts != null && ObjExportRetail.RetailTemplateFonts.Count > 0)
                            {
                                foreach (var font in ObjExportRetail.RetailTemplateFonts)
                                {
                                    if (!string.IsNullOrEmpty(font.FontPath))
                                    {
                                        if (!string.IsNullOrEmpty(font.FontPath))
                                        {


                                            string F1 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + font.FontPath + "/" + font.FontFile + ".eot");

                                            string F2 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + font.FontPath + "/" + font.FontFile + ".ttf");

                                            string F3 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + font.FontPath + "/" + font.FontFile + ".woff");

                                            DPath = "Designer/" + font.FontPath + "/" + font.FontFile + ".eot";

                                            string Dpath2 = "Designer/" + font.FontPath + "/" + font.FontFile + ".ttf";

                                            string DPath3 = "Designer/" + font.FontPath + "/" + font.FontFile + ".woff";

                                            if (File.Exists(F1))
                                            {
                                                ZipEntry r = zip.AddFile(F1, DPath);
                                                r.Comment = "template font";
                                            }

                                            if (File.Exists(F2))
                                            {
                                                ZipEntry r = zip.AddFile(F2, Dpath2);
                                                r.Comment = "template font";
                                            }

                                            if (File.Exists(F3))
                                            {
                                                ZipEntry r = zip.AddFile(F3, DPath3);
                                                r.Comment = "template font";
                                            }


                                        }
                                    }
                                }
                            }

                            //ExportOrganisation RetailexOrg = new ExportOrganisation();
                            //RetailexOrg = ObjExportRetailSet.ExportRetailStore2;
                            if (ObjExportRetail.RetailCompanyContact != null && ObjExportRetail.RetailCompanyContact.Count > 0)
                            {
                                foreach (var contact in ObjExportRetail.RetailCompanyContact)
                                {
                                    if (!string.IsNullOrEmpty(contact.image))
                                    {
                                        string ContactImage = HttpContext.Current.Server.MapPath("~/" + contact.image);
                                        string ContactDirectory = "/Assets/" + OrganisationID + "/" + RetailCompanyID + "/Contacts/" + contact.ContactId;
                                        if (File.Exists(ContactImage))
                                        {
                                            ZipEntry r = zip.AddFile(ContactImage, ContactDirectory);
                                            r.Comment = "Contact images for Store";

                                        }
                                    }
                                }

                            }
                            string CSSPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Assets/" + OrganisationID + "/" + RetailCompanyID + "/Site.css";
                            string pCSSDirectory = "/Assets/" + OrganisationID + "/" + RetailCompanyID;
                            if (File.Exists(CSSPath))
                            {
                                ZipEntry r = zip.AddFile(CSSPath, pCSSDirectory);
                                r.Comment = "CSS for Store";

                            }
                            string SpritePath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Assets/" + OrganisationID + "/" + RetailCompanyID + "/sprite.png";
                            string pDirectory = "/Assets/" + OrganisationID + "/" + RetailCompanyID;

                            if (File.Exists(SpritePath))
                            {
                                ZipEntry r = zip.AddFile(SpritePath, pDirectory);
                                r.Comment = "Sprite for Store";

                            }

                        }
                    }
                    #endregion

                    #region corporate store without products
                    // export corporate store without products
                    ExportOrganisation ObjExportCorpWOP = new Models.Common.ExportOrganisation();
                    ObjExportCorpWOP = ObjExportCorporateWOProducts.ExportStore1WOP;
                    if (ObjExportCorpWOP != null)
                    {
                        if (ObjExportCorpWOP.Company != null)
                        {
                            //// export company Logo
                            if (ObjExportCorpWOP.Company.Image != null)
                            {
                                string FilePath = HttpContext.Current.Server.MapPath("~/" + ObjExportCorpWOP.Company.Image);
                                DPath = "/Assets/" + OrganisationID + "/" + CompanyIDWOP;
                                if (File.Exists(FilePath))
                                {
                                    ZipEntry r = zip.AddFile(FilePath, DPath);
                                    r.Comment = "Company Logo for Store";

                                }
                            }

                            if (ObjExportCorpWOP.Company.StoreBackgroundImage != null)
                            {
                                string FilePath = HttpContext.Current.Server.MapPath("~/" + ObjExportCorpWOP.Company.StoreBackgroundImage);
                                DPath = "/Assets/" + OrganisationID + "/" + CompanyIDWOP;
                                if (File.Exists(FilePath))
                                {
                                    ZipEntry r = zip.AddFile(FilePath, DPath);
                                    r.Comment = "Background image for Store";

                                }
                            }
                            // export media

                            if (ObjExportCorpWOP.Company.MediaLibraries != null)
                            {
                                if (ObjExportCorpWOP.Company.MediaLibraries.Count > 0)
                                {
                                    foreach (var media in ObjExportCorpWOP.Company.MediaLibraries)
                                    {
                                        if (media.FilePath != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + media.FilePath);
                                            DPath = "/Media/" + OrganisationID + "/" + CompanyIDWOP;
                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Media Files for Store";

                                            }
                                        }
                                    }
                                }
                            }
                            List<ProductCategory> categories = new List<ProductCategory>();
                            categories = ObjExportCorporateWOProducts.ExportStore2WOP;

                            if (categories != null)
                            {
                                foreach (var cat in categories)
                                {
                                    if (cat.ImagePath != null)
                                    {
                                        string FilePath = HttpContext.Current.Server.MapPath("~/" + cat.ImagePath);
                                        DPath = "/Assets/" + OrganisationID + "/" + CompanyIDWOP + "/ProductCategories";
                                        if (File.Exists(FilePath))
                                        {
                                            ZipEntry r = zip.AddFile(FilePath, DPath);
                                            r.Comment = "Category Image Path for Store";

                                        }
                                    }

                                    if (cat.ThumbnailPath != null)
                                    {
                                        string FilePath = HttpContext.Current.Server.MapPath("~/" + cat.ThumbnailPath);
                                        DPath = "/Assets/" + OrganisationID + "/" + CompanyIDWOP + "/ProductCategories";

                                        if (File.Exists(FilePath))
                                        {
                                            ZipEntry r = zip.AddFile(FilePath, DPath);
                                            r.Comment = "Category Thumbnail Path for Store";

                                        }
                                    }

                                }

                            }
                            List<Item> exItemsCorp = new List<Item>();
                            exItemsCorp = ObjExportCorporateWOProducts.ExportStore3WOP;
                            if (exItemsCorp != null)
                            {
                                if (exItemsCorp.Count > 0)
                                {
                                    foreach (var item in exItemsCorp)
                                    {
                                        if (item.ImagePath != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + item.ImagePath);
                                            DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Items Image Path for Store";

                                            }
                                        }

                                        if (item.ThumbnailPath != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + item.ThumbnailPath);
                                            DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Items Thumbnail Path for Store";

                                            }
                                        }

                                        if (item.GridImage != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + item.GridImage);
                                            DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Items Grid image for Store";

                                            }
                                        }
                                        if (item.File1 != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + item.File1);
                                            DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Items image for Store";

                                            }
                                        }
                                        if (item.File2 != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + item.File2);
                                            DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Items image for Store";

                                            }
                                        }
                                        if (item.File3 != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + item.File3);
                                            DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Items image for Store";

                                            }
                                        }
                                        if (item.File4 != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + item.File4);
                                            DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Items image for Store";

                                            }
                                        }
                                        if (item.File5 != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + item.File5);
                                            DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Items image for Store";

                                            }
                                        }
                                        if (item.ItemImages != null && item.ItemImages.Count > 0)
                                        {
                                            foreach (var img in item.ItemImages)
                                            {
                                                if (!string.IsNullOrEmpty(img.ImageURL))
                                                {
                                                    string FilePath = HttpContext.Current.Server.MapPath("~/" + img.ImageURL);
                                                    DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                                    if (File.Exists(FilePath))
                                                    {
                                                        ZipEntry r = zip.AddFile(FilePath, DPath);
                                                        r.Comment = "Items image for Store";

                                                    }
                                                }

                                            }
                                        }
                                        if (item.TemplateId != null || item.TemplateId > 0)
                                        {
                                            if (item.DesignerCategoryId == 0 && item.DesignerCategoryId == null)
                                            {
                                                //if (item.Template.TemplateFonts != null && item.Template.TemplateFonts.Count > 0)
                                                //{
                                                //    foreach (var tempFont in item.Template.TemplateFonts)
                                                //    {
                                                //        if (!string.IsNullOrEmpty(tempFont.FontPath))
                                                //        {
                                                //            string F1 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID + "/WebFonts/" + tempFont.FontPath + "/" + tempFont.FontFile + ".eot");

                                                //            string F2 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID + "/WebFonts/" + tempFont.FontPath + "/" + tempFont.FontFile + ".ttf");

                                                //            string F3 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID + "/WebFonts/" + tempFont.FontPath + "/" + tempFont.FontFile + ".woff");

                                                //            DPath = "Designer/Organisation" + OrganisationID + "/WebFonts/" + tempFont.FontPath + "/" + tempFont.FontFile + ".eot";

                                                //            string Dpath2 = "Designer/Organisation" + OrganisationID + "/WebFonts/" + tempFont.FontPath + "/" + tempFont.FontFile + ".ttf";

                                                //            string DPath3 = "Designer/Organisation" + OrganisationID + "/WebFonts/" + tempFont.FontPath + "/" + tempFont.FontFile + ".woff";

                                                //            if (File.Exists(F1))
                                                //            {
                                                //                ZipEntry r = zip.AddFile(F1, DPath);
                                                //                r.Comment = "template font";
                                                //            }

                                                //            if (File.Exists(F2))
                                                //            {
                                                //                ZipEntry r = zip.AddFile(F2, Dpath2);
                                                //                r.Comment = "template font";
                                                //            }

                                                //            if (File.Exists(F3))
                                                //            {
                                                //                ZipEntry r = zip.AddFile(F3, DPath3);
                                                //                r.Comment = "template font";
                                                //            }


                                                //        }
                                                //        else
                                                //        {

                                                //            string F1 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID + "/WebFonts/" + tempFont.FontFile + ".eot");

                                                //            string F2 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID + "/WebFonts/" + tempFont.FontFile + ".ttf");

                                                //            string F3 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID + "/WebFonts/" + tempFont.FontFile + ".woff");

                                                //            DPath = "Designer/Organisation" + OrganisationID + "/WebFonts/" + tempFont.FontFile + ".eot";

                                                //            string Dpath2 = "Designer/Organisation" + OrganisationID + "/WebFonts/" + tempFont.FontFile + ".ttf";

                                                //            string DPath3 = "Designer/Organisation" + OrganisationID + "/WebFonts/" + tempFont.FontFile + ".woff";

                                                //            if (File.Exists(F1))
                                                //            {
                                                //                ZipEntry r = zip.AddFile(F1, DPath);
                                                //                r.Comment = "template font";
                                                //            }

                                                //            if (File.Exists(F2))
                                                //            {
                                                //                ZipEntry r = zip.AddFile(F2, Dpath2);
                                                //                r.Comment = "template font";
                                                //            }

                                                //            if (File.Exists(F3))
                                                //            {
                                                //                ZipEntry r = zip.AddFile(F3, DPath3);
                                                //                r.Comment = "template font";
                                                //            }
                                                //        }

                                                //    }

                                                //}
                                                if (item.Template.TemplateBackgroundImages != null && item.Template.TemplateBackgroundImages.Count > 0)
                                                {
                                                    foreach (var tempbcI in item.Template.TemplateBackgroundImages)
                                                    {



                                                        if (!string.IsNullOrEmpty(tempbcI.ImageName))
                                                        {

                                                            string FilePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Templates/" + tempbcI.ImageName);


                                                            if (tempbcI.ImageName.Contains("UserImgs/"))
                                                            {

                                                                DPath = "/Designer/Organisation" + OrganisationID + "/Templates/UserImgs/" + ObjExportCorpWOP.Company.CompanyId;
                                                            }
                                                            else
                                                            {
                                                                DPath = "/Designer/Organisation" + OrganisationID + "/Templates/" + tempbcI.ProductId;
                                                            }

                                                            if (File.Exists(FilePath))
                                                            {
                                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                                r.Comment = "Items image for Store";

                                                            }

                                                        }

                                                    }
                                                }
                                                if (item.Template.TemplatePages != null && item.Template.TemplatePages.Count > 0)
                                                {
                                                    foreach (var tempPage in item.Template.TemplatePages)
                                                    {
                                                        if (!string.IsNullOrEmpty(tempPage.BackgroundFileName))
                                                        {
                                                            string TemplatePagesFile = "/MPC_Content/Designer/Organisation" + OrganisationID + "/Templates/" + tempPage.BackgroundFileName;
                                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + TemplatePagesFile);
                                                            DPath = "/Designer/Organisation" + OrganisationID + "/Templates/" + tempPage.ProductId;
                                                            if (File.Exists(FilePath))
                                                            {
                                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                                r.Comment = "Items image for Store";

                                                            }

                                                        }
                                                        string fileName = "templatImgBk" + tempPage.PageNo + ".jpg";
                                                        string oPath = "/MPC_Content/Designer/Organisation" + OrganisationID + "/Templates/" + tempPage.ProductId + "/" + fileName;
                                                        string FilePaths = HttpContext.Current.Server.MapPath("~/" + oPath);
                                                        DPath = "/Designer/Organisation" + OrganisationID + "/Templates/" + tempPage.ProductId;
                                                        if (File.Exists(FilePaths))
                                                        {
                                                            ZipEntry r = zip.AddFile(FilePaths, DPath);
                                                            r.Comment = "Items image for Store";
                                                        }

                                                    }
                                                }

                                            }
                                            //if(ObjExportRetail.TemplateBackgroundImage)

                                        }
                                    }

                                }
                            }
                            if (ObjExportCorpWOP.TemplateFonts != null && ObjExportCorpWOP.TemplateFonts.Count > 0)
                            {
                                foreach (var font in ObjExportCorpWOP.TemplateFonts)
                                {
                                    if (!string.IsNullOrEmpty(font.FontPath))
                                    {
                                        if (!string.IsNullOrEmpty(font.FontPath))
                                        {


                                            string F1 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + font.FontPath + "/" + font.FontFile + ".eot");

                                            string F2 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + font.FontPath + "/" + font.FontFile + ".ttf");

                                            string F3 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + font.FontPath + "/" + font.FontFile + ".woff");

                                            DPath = "Designer/" + font.FontPath + "/" + font.FontFile + ".eot";

                                            string Dpath2 = "Designer/" + font.FontPath + "/" + font.FontFile + ".ttf";

                                            string DPath3 = "Designer/" + font.FontPath + "/" + font.FontFile + ".woff";

                                            if (File.Exists(F1))
                                            {
                                                ZipEntry r = zip.AddFile(F1, DPath);
                                                r.Comment = "template font";
                                            }

                                            if (File.Exists(F2))
                                            {
                                                ZipEntry r = zip.AddFile(F2, Dpath2);
                                                r.Comment = "template font";
                                            }

                                            if (File.Exists(F3))
                                            {
                                                ZipEntry r = zip.AddFile(F3, DPath3);
                                                r.Comment = "template font";
                                            }


                                        }
                                    }
                                }
                            }
                            if (ObjExportCorpWOP.Company.CompanyContacts != null && ObjExportCorpWOP.Company.CompanyContacts.Count > 0)
                            {
                                foreach (var contact in ObjExportCorpWOP.Company.CompanyContacts)
                                {
                                    if (!string.IsNullOrEmpty(contact.image))
                                    {
                                        string ContactImage = HttpContext.Current.Server.MapPath("~/" + contact.image);
                                        string ContactDirectory = "/Assets/" + OrganisationID + "/" + CompanyIDWOP + "/Contacts/" + contact.ContactId;
                                        if (File.Exists(ContactImage))
                                        {
                                            ZipEntry r = zip.AddFile(ContactImage, ContactDirectory);
                                            r.Comment = "Contact images for Store";

                                        }
                                    }
                                }

                            }

                            string CSSPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Assets/" + OrganisationID + "/" + CompanyIDWOP + "/Site.css";
                            string pCSSDirectory = "/Assets/" + OrganisationID + "/" + CompanyIDWOP;
                            if (File.Exists(CSSPath))
                            {
                                ZipEntry r = zip.AddFile(CSSPath, pCSSDirectory);
                                r.Comment = "CSS for Store";

                            }
                            string SpritePath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Assets/" + OrganisationID + "/" + CompanyIDWOP + "/sprite.png";
                            string pDirectory = "/Assets/" + OrganisationID + "/" + CompanyIDWOP;

                            if (File.Exists(SpritePath))
                            {
                                ZipEntry r = zip.AddFile(SpritePath, pDirectory);
                                r.Comment = "Sprite for Store";

                            }

                        }
                    }

                    #endregion

                    #region retail store without products

                    ExportOrganisation ObjExportRetailWOP = new Models.Common.ExportOrganisation();
                    if (ObjExportRetailWOProducts != null)
                    {
                        ObjExportRetailWOP = ObjExportRetailWOProducts.ExportRetailStore1WOP;

                        if (ObjExportRetailWOP != null)
                        {
                            if (ObjExportRetailWOP.RetailCompany != null)
                            {
                                if (ObjExportRetailWOP.RetailCompany.Image != null)
                                {
                                    string FilePath = HttpContext.Current.Server.MapPath("~/" + ObjExportRetailWOP.RetailCompany.Image);
                                    DPath = "/Assets/" + OrganisationID + "/" + RetailCompanyIDWOP;
                                    if (File.Exists(FilePath))
                                    {
                                        ZipEntry r = zip.AddFile(FilePath, DPath);
                                        r.Comment = "Company Logo for Store";

                                    }
                                }
                                if (ObjExportCorpWOP.Company.StoreBackgroundImage != null)
                                {
                                    string FilePath = HttpContext.Current.Server.MapPath("~/" + ObjExportRetailWOP.RetailCompany.StoreBackgroundImage);
                                    DPath = "/Assets/" + OrganisationID + "/" + RetailCompanyIDWOP;
                                    if (File.Exists(FilePath))
                                    {
                                        ZipEntry r = zip.AddFile(FilePath, DPath);
                                        r.Comment = "Background image for Store";

                                    }
                                }

                                // export media

                                if (ObjExportRetailWOP.RetailCompany.MediaLibraries != null)
                                {
                                    if (ObjExportRetailWOP.RetailCompany.MediaLibraries.Count > 0)
                                    {
                                        foreach (var media in ObjExportRetailWOP.RetailCompany.MediaLibraries)
                                        {
                                            if (media.FilePath != null)
                                            {
                                                string FilePath = HttpContext.Current.Server.MapPath("~/" + media.FilePath);
                                                DPath = "/Media/" + OrganisationID + "/" + RetailCompanyIDWOP;
                                                if (File.Exists(FilePath))
                                                {
                                                    ZipEntry r = zip.AddFile(FilePath, DPath);
                                                    r.Comment = "Media Files for Store";

                                                }
                                            }
                                        }
                                    }
                                }

                                List<ProductCategory> categories = new List<ProductCategory>();
                                categories = ObjExportRetailWOProducts.ExportRetailStore2WOP;

                                if (categories != null)
                                {
                                    foreach (var cat in categories)
                                    {
                                        if (cat.ImagePath != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + cat.ImagePath);
                                            DPath = "/Assets/" + OrganisationID + "/" + RetailCompanyIDWOP + "/ProductCategories";
                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Category Image Path for Store";

                                            }
                                        }

                                        if (cat.ThumbnailPath != null)
                                        {
                                            string FilePath = HttpContext.Current.Server.MapPath("~/" + cat.ThumbnailPath);
                                            DPath = "/Assets/" + OrganisationID + "/" + RetailCompanyIDWOP + "/ProductCategories";

                                            if (File.Exists(FilePath))
                                            {
                                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                                r.Comment = "Category Thumbnail Path for Store";

                                            }
                                        }

                                    }

                                }

                                List<Item> exItems = new List<Item>();
                                exItems = ObjExportRetailWOProducts.ExportRetailStore3WOP;
                                if (exItems != null)
                                {
                                    if (exItems.Count > 0)
                                    {
                                        foreach (var item in exItems)
                                        {
                                            if (item.ImagePath != null)
                                            {
                                                string FilePath = HttpContext.Current.Server.MapPath("~/" + item.ImagePath);
                                                DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                                if (File.Exists(FilePath))
                                                {
                                                    ZipEntry r = zip.AddFile(FilePath, DPath);
                                                    r.Comment = "Items Image Path for Store";

                                                }
                                            }

                                            if (item.ThumbnailPath != null)
                                            {
                                                string FilePath = HttpContext.Current.Server.MapPath("~/" + item.ThumbnailPath);
                                                DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                                if (File.Exists(FilePath))
                                                {
                                                    ZipEntry r = zip.AddFile(FilePath, DPath);
                                                    r.Comment = "Items Thumbnail Path for Store";

                                                }
                                            }

                                            if (item.GridImage != null)
                                            {
                                                string FilePath = HttpContext.Current.Server.MapPath("~/" + item.GridImage);
                                                DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                                if (File.Exists(FilePath))
                                                {
                                                    ZipEntry r = zip.AddFile(FilePath, DPath);
                                                    r.Comment = "Items Grid image for Store";

                                                }
                                            }
                                            if (item.File1 != null)
                                            {
                                                string FilePath = HttpContext.Current.Server.MapPath("~/" + item.File1);
                                                DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                                if (File.Exists(FilePath))
                                                {
                                                    ZipEntry r = zip.AddFile(FilePath, DPath);
                                                    r.Comment = "Items image for Store";

                                                }
                                            }
                                            if (item.File2 != null)
                                            {
                                                string FilePath = HttpContext.Current.Server.MapPath("~/" + item.File2);
                                                DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                                if (File.Exists(FilePath))
                                                {
                                                    ZipEntry r = zip.AddFile(FilePath, DPath);
                                                    r.Comment = "Items image for Store";

                                                }
                                            }
                                            if (item.File3 != null)
                                            {
                                                string FilePath = HttpContext.Current.Server.MapPath("~/" + item.File3);
                                                DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                                if (File.Exists(FilePath))
                                                {
                                                    ZipEntry r = zip.AddFile(FilePath, DPath);
                                                    r.Comment = "Items image for Store";

                                                }
                                            }
                                            if (item.File4 != null)
                                            {
                                                string FilePath = HttpContext.Current.Server.MapPath("~/" + item.File4);
                                                DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                                if (File.Exists(FilePath))
                                                {
                                                    ZipEntry r = zip.AddFile(FilePath, DPath);
                                                    r.Comment = "Items image for Store";

                                                }
                                            }
                                            if (item.File5 != null)
                                            {
                                                string FilePath = HttpContext.Current.Server.MapPath("~/" + item.File5);
                                                DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                                if (File.Exists(FilePath))
                                                {
                                                    ZipEntry r = zip.AddFile(FilePath, DPath);
                                                    r.Comment = "Items image for Store";

                                                }
                                            }

                                            if (item.ItemImages != null && item.ItemImages.Count > 0)
                                            {
                                                foreach (var img in item.ItemImages)
                                                {
                                                    if (!string.IsNullOrEmpty(img.ImageURL))
                                                    {
                                                        string FilePath = HttpContext.Current.Server.MapPath("~/" + img.ImageURL);
                                                        DPath = "/Products/" + OrganisationID + "/" + item.ItemId;
                                                        if (File.Exists(FilePath))
                                                        {
                                                            ZipEntry r = zip.AddFile(FilePath, DPath);
                                                            r.Comment = "Items image for Store";

                                                        }
                                                    }

                                                }
                                            }
                                            if (item.TemplateId != null && item.TemplateId > 0)
                                            {
                                                if (item.DesignerCategoryId == 0 || item.DesignerCategoryId == null)
                                                {
                                                    if (item.Template != null)
                                                    {


                                                        if (item.Template.TemplateBackgroundImages != null && item.Template.TemplateBackgroundImages.Count > 0)
                                                        {
                                                            foreach (var tempbcI in item.Template.TemplateBackgroundImages)
                                                            {



                                                                if (!string.IsNullOrEmpty(tempbcI.ImageName))
                                                                {

                                                                    string FilePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Templates/" + tempbcI.ImageName);


                                                                    if (tempbcI.ImageName.Contains("UserImgs/"))
                                                                    {

                                                                        DPath = "/Designer/Organisation" + OrganisationID + "/Templates/UserImgs/" + ObjExportRetailWOP.RetailCompany.CompanyId;
                                                                    }
                                                                    else
                                                                    {
                                                                        DPath = "/Designer/Organisation" + OrganisationID + "/Templates/" + tempbcI.ProductId;
                                                                    }

                                                                    if (File.Exists(FilePath))
                                                                    {
                                                                        ZipEntry r = zip.AddFile(FilePath, DPath);
                                                                        r.Comment = "Items image for Store";

                                                                    }

                                                                }

                                                            }
                                                        }
                                                        if (item.Template.TemplatePages != null && item.Template.TemplatePages.Count > 0)
                                                        {
                                                            foreach (var tempPage in item.Template.TemplatePages)
                                                            {
                                                                if (!string.IsNullOrEmpty(tempPage.BackgroundFileName))
                                                                {
                                                                    string TemplatePagesFile = "/MPC_Content/Designer/Organisation" + OrganisationID + "/Templates/" + tempPage.BackgroundFileName;
                                                                    string FilePath = HttpContext.Current.Server.MapPath("~/" + TemplatePagesFile);
                                                                    DPath = "/Designer/Organisation" + OrganisationID + "/Templates/" + tempPage.ProductId;
                                                                    if (File.Exists(FilePath))
                                                                    {
                                                                        ZipEntry r = zip.AddFile(FilePath, DPath);
                                                                        r.Comment = "Items image for Store";

                                                                    }

                                                                }
                                                                string fileName = "templatImgBk" + tempPage.PageNo + ".jpg";
                                                                string oPath = "/MPC_Content/Designer/Organisation" + OrganisationID + "/Templates/" + tempPage.ProductId + "/" + fileName;
                                                                string FilePaths = HttpContext.Current.Server.MapPath("~/" + oPath);
                                                                DPath = "/Designer/Organisation" + OrganisationID + "/Templates/" + tempPage.ProductId;
                                                                if (File.Exists(FilePaths))
                                                                {
                                                                    ZipEntry r = zip.AddFile(FilePaths, DPath);
                                                                    r.Comment = "Items image for Store";
                                                                }
                                                            }
                                                        }


                                                    }

                                                }

                                            }
                                        }

                                    }
                                }
                                if (ObjExportRetailWOP.RetailTemplateFonts != null && ObjExportRetailWOP.RetailTemplateFonts.Count > 0)
                                {
                                    foreach (var font in ObjExportRetailWOP.RetailTemplateFonts)
                                    {
                                        if (!string.IsNullOrEmpty(font.FontPath))
                                        {


                                            string F1 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + font.FontPath + "/" + font.FontFile + ".eot");

                                            string F2 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + font.FontPath + "/" + font.FontFile + ".ttf");

                                            string F3 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + font.FontPath + "/" + font.FontFile + ".woff");

                                            DPath = "Designer/" + font.FontPath + "/" + font.FontFile + ".eot";

                                            string Dpath2 = "Designer/" + font.FontPath + "/" + font.FontFile + ".ttf";

                                            string DPath3 = "Designer/" + font.FontPath + "/" + font.FontFile + ".woff";

                                            if (File.Exists(F1))
                                            {
                                                ZipEntry r = zip.AddFile(F1, DPath);
                                                r.Comment = "template font";
                                            }

                                            if (File.Exists(F2))
                                            {
                                                ZipEntry r = zip.AddFile(F2, Dpath2);
                                                r.Comment = "template font";
                                            }

                                            if (File.Exists(F3))
                                            {
                                                ZipEntry r = zip.AddFile(F3, DPath3);
                                                r.Comment = "template font";
                                            }


                                        }

                                    }
                                }
                                //ExportOrganisation RetailexOrg = new ExportOrganisation();
                                //RetailexOrg = ObjExportRetailSet.ExportRetailStore2;
                                if (ObjExportRetailWOP.RetailCompanyContact != null && ObjExportRetailWOP.RetailCompanyContact.Count > 0)
                                {
                                    foreach (var contact in ObjExportRetailWOP.RetailCompanyContact)
                                    {
                                        if (!string.IsNullOrEmpty(contact.image))
                                        {
                                            string ContactImage = HttpContext.Current.Server.MapPath("~/" + contact.image);
                                            string ContactDirectory = "/Assets/" + OrganisationID + "/" + RetailCompanyIDWOP + "/Contacts/" + contact.ContactId;
                                            if (File.Exists(ContactImage))
                                            {
                                                ZipEntry r = zip.AddFile(ContactImage, ContactDirectory);
                                                r.Comment = "Contact images for Store";

                                            }
                                        }
                                    }

                                }
                                string CSSPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Assets/" + OrganisationID + "/" + RetailCompanyIDWOP + "/Site.css";
                                string pCSSDirectory = "/Assets/" + OrganisationID + "/" + RetailCompanyIDWOP;
                                if (File.Exists(CSSPath))
                                {
                                    ZipEntry r = zip.AddFile(CSSPath, pCSSDirectory);
                                    r.Comment = "CSS for Store";

                                }
                                string SpritePath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Assets/" + OrganisationID + "/" + RetailCompanyIDWOP + "/sprite.png";
                                string pDirectory = "/Assets/" + OrganisationID + "/" + RetailCompanyIDWOP;

                                if (File.Exists(SpritePath))
                                {
                                    ZipEntry r = zip.AddFile(SpritePath, pDirectory);
                                    r.Comment = "Sprite for Store";

                                }

                            }
                        }
                    }

                    #endregion

                    // export zip
                    zip.Comment = "This zip archive was created to export complete organisation";
                    string sDirectory = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/DefaulStorePackage";
                    string name = "DefaultStores";
                    string sZipFileName = string.Empty;
                    if (Path.HasExtension(name))
                        sZipFileName = name;
                    else
                        sZipFileName = name + ".zip";
                    if (Directory.Exists(sDirectory))
                    {
                        zip.Save(sDirectory + "\\" + sZipFileName);
                    }
                    else
                    {
                        Directory.CreateDirectory(sDirectory);
                        zip.Save(sDirectory + "\\" + sZipFileName);
                    }
                    if (JsonFiles != null && JsonFiles.Count > 0)
                    {
                        foreach (var file in JsonFiles)
                        {
                            if (!string.IsNullOrEmpty(file))
                            {
                                if (File.Exists(file))
                                {
                                    File.Delete(file);
                                }

                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;

            }



        }

        public ExportSets ExportRetailStore(long RetailCompanyID, long OrganisationID)
        {
            try
            {
                ExportSets ObjExportRetail = new Models.Common.ExportSets();

                // export retail store
                ObjExportRetail = companyRepository.ExportRetailCompany(RetailCompanyID);

                //  GC.Collect();
                //string JsonRetail = JsonConvert.SerializeObject(ObjExportRetail, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                //// export json file
                //string sRetailPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/RetailJson.txt";
                //System.IO.File.WriteAllText(sRetailPath, JsonRetail);

                return ObjExportRetail;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExportSets ExportRetailStoreWithoutProducts(long RetailCompanyID, long OrganisationID)
        {
            try
            {
                ExportSets ObjExportRetail = new Models.Common.ExportSets();

                // export retail store
                ObjExportRetail = companyRepository.ExportRetailCompanyWithoutProducts(RetailCompanyID);

                //  GC.Collect();
                //string JsonRetail = JsonConvert.SerializeObject(ObjExportRetail, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                //// export json file
                //string sRetailPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/RetailJson.txt";
                //System.IO.File.WriteAllText(sRetailPath, JsonRetail);

                return ObjExportRetail;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ImportOrganisation

        public bool ImportOrganisation(long OrganisationId, string SubDomain, bool isCorpStore)
        {

            string timelog = "";
            DateTime st = DateTime.Now;
            DateTime end = DateTime.Now;

            try
            {

                timelog = "Process start " + DateTime.Now.ToLongTimeString();
                ExportSets exportSets = new ExportSets();
                ExportOrganisation objExpOrg = new Models.Common.ExportOrganisation();
                ExportOrganisation objExpCorp = new Models.Common.ExportOrganisation();
                ExportOrganisation objExpRetail = new Models.Common.ExportOrganisation();
                string extractPath = System.Web.Hosting.HostingEnvironment.MapPath("/MPC_Content/Artworks/ImportOrganisation");
                // string ReadPath = System.Web.Hosting.HostingEnvironment.MapPath("/MPC_Content/Organisations/ExportedZip20.zip");
                string ZipPath = System.Web.Hosting.HostingEnvironment.MapPath("/MPC_Content/DefaulStorePackage/DefaultStores.zip");
                if (File.Exists(ZipPath))
                {
                    //string zipToUnpack = "C1P3SML.zip";
                    //string unpackDirectory = "Extracted Files";
                    timelog += "Extract Start " + DateTime.Now.ToLongTimeString() + Environment.NewLine;
                    st = DateTime.Now;

                    using (ZipFile zip1 = ZipFile.Read(ZipPath))
                    {
                        // here, we extract every entry
                        foreach (ZipEntry e in zip1)
                        {
                            e.Extract(extractPath, ExtractExistingFileAction.OverwriteSilently);
                        }
                    }
                    end = DateTime.Now;
                    timelog += "Extract complete " + DateTime.Now.ToLongTimeString() + " Total Seconds " + end.Subtract(end).TotalSeconds.ToString() + Environment.NewLine;

                    // deserialize organisation json file
                    string JsonFilePath = System.Web.Hosting.HostingEnvironment.MapPath("/MPC_Content/Artworks/ImportOrganisation/OrganisationJson1.txt");
                    if (File.Exists(JsonFilePath))
                    {
                        string json = System.IO.File.ReadAllText(JsonFilePath);

                        exportSets.ExportOrganisationSet1 = JsonConvert.DeserializeObject<ExportOrganisation>(json);

                        json = string.Empty;
                    }
                    string JsonFilePath2 = System.Web.Hosting.HostingEnvironment.MapPath("/MPC_Content/Artworks/ImportOrganisation/OrganisationJson2.txt");
                    if (File.Exists(JsonFilePath2))
                    {
                        string json = System.IO.File.ReadAllText(JsonFilePath2);

                        exportSets.ExportOrganisationSet2 = JsonConvert.DeserializeObject<ExportOrganisation>(json);

                        json = string.Empty;
                    }
                    string JsonFilePath3 = System.Web.Hosting.HostingEnvironment.MapPath("/MPC_Content/Artworks/ImportOrganisation/OrganisationJson3.txt");
                    if (File.Exists(JsonFilePath3))
                    {
                        string json = System.IO.File.ReadAllText(JsonFilePath3);

                        exportSets.ExportOrganisationSet3 = JsonConvert.DeserializeObject<ExportOrganisation>(json);

                        json = string.Empty;
                    }
                    string JsonFilePath4 = System.Web.Hosting.HostingEnvironment.MapPath("/MPC_Content/Artworks/ImportOrganisation/OrganisationJson4.txt");
                    if (File.Exists(JsonFilePath4))
                    {
                        string json = System.IO.File.ReadAllText(JsonFilePath4);

                        exportSets.ExportOrganisationSet4 = JsonConvert.DeserializeObject<ExportOrganisation>(json);

                        json = string.Empty;
                    }


                    // deserialize retail json file
                    string JsonRetailFilePath = System.Web.Hosting.HostingEnvironment.MapPath("/MPC_Content/Artworks/ImportOrganisation/RetailJson1.txt");
                    if (File.Exists(JsonRetailFilePath))
                    {
                        string json = System.IO.File.ReadAllText(JsonRetailFilePath);

                        exportSets.ExportRetailStore1 = JsonConvert.DeserializeObject<ExportOrganisation>(json);

                        json = string.Empty;
                    }
                    string JsonRetailFilePath2 = System.Web.Hosting.HostingEnvironment.MapPath("/MPC_Content/Artworks/ImportOrganisation/RetailJson2.txt");
                    if (File.Exists(JsonRetailFilePath2))
                    {
                        string json = System.IO.File.ReadAllText(JsonRetailFilePath2);

                        exportSets.ExportRetailStore3 = JsonConvert.DeserializeObject<List<Item>>(json);

                        json = string.Empty;
                    }

                    string ProdCatRetailFilePath = System.Web.Hosting.HostingEnvironment.MapPath("/MPC_Content/Artworks/ImportOrganisation/RetailProductCategories.txt");
                    if (File.Exists(ProdCatRetailFilePath))
                    {
                        string json = System.IO.File.ReadAllText(ProdCatRetailFilePath);

                        exportSets.ExportRetailStore2 = JsonConvert.DeserializeObject<List<ProductCategory>>(json);

                        json = string.Empty;
                    }
                    //string SecRetailFilePath2 = System.Web.Hosting.HostingEnvironment.MapPath("/MPC_Content/Artworks/ImportOrganisation/RetailSecondaryPages.txt");
                    //if (File.Exists(SecRetailFilePath2))
                    //{
                    //    string json = System.IO.File.ReadAllText(SecRetailFilePath2);

                    //    exportSets.ExportRetailStore4 = JsonConvert.DeserializeObject<List<CmsPage>>(json);

                    //    json = string.Empty;
                    //}
                    // deserialize corpoate json file
                    string JsonCorpFilePath = System.Web.Hosting.HostingEnvironment.MapPath("/MPC_Content/Artworks/ImportOrganisation/CorporateJson1.txt");
                    if (File.Exists(JsonCorpFilePath))
                    {
                        string json = System.IO.File.ReadAllText(JsonCorpFilePath);

                        exportSets.ExportStore1 = JsonConvert.DeserializeObject<ExportOrganisation>(json);

                        json = string.Empty;
                    }
                    string JsonCorpFilePath2 = System.Web.Hosting.HostingEnvironment.MapPath("/MPC_Content/Artworks/ImportOrganisation/CorporateJson2.txt");
                    if (File.Exists(JsonCorpFilePath2))
                    {
                        string json = System.IO.File.ReadAllText(JsonCorpFilePath2);

                        exportSets.ExportStore3 = JsonConvert.DeserializeObject<List<Item>>(json);

                        json = string.Empty;
                    }

                    string JsonCorpFilePath3 = System.Web.Hosting.HostingEnvironment.MapPath("/MPC_Content/Artworks/ImportOrganisation/CorporateProductCategories.txt");
                    if (File.Exists(JsonCorpFilePath3))
                    {
                        string json = System.IO.File.ReadAllText(JsonCorpFilePath3);

                        exportSets.ExportStore2 = JsonConvert.DeserializeObject<List<ProductCategory>>(json);

                        json = string.Empty;
                    }
                    //string JsonCorpFilePath4 = System.Web.Hosting.HostingEnvironment.MapPath("/MPC_Content/Artworks/ImportOrganisation/CorporateSecondaryPages.txt");
                    //if (File.Exists(JsonCorpFilePath4))
                    //{
                    //    string json = System.IO.File.ReadAllText(JsonCorpFilePath4);

                    //    exportSets.ExportStore4 = JsonConvert.DeserializeObject<List<CmsPage>>(json);

                    //    json = string.Empty;
                    //}

                    end = DateTime.Now;
                    timelog += "Deserialization complete " + DateTime.Now.ToLongTimeString() + " Total Seconds " + end.Subtract(st).TotalSeconds.ToString() + Environment.NewLine;
                    st = DateTime.Now;

                    timelog = organisationRepository.InsertOrganisation(OrganisationId, objExpCorp, objExpRetail, isCorpStore, exportSets, SubDomain, timelog);


                    string StoreName = ConfigurationManager.AppSettings["RetailStoreName"];
                    end = DateTime.Now;
                    timelog += "import org " + DateTime.Now.ToLongTimeString() + " Total Seconds " + end.Subtract(st).TotalSeconds.ToString() + Environment.NewLine;
                    st = DateTime.Now;

                    ImportStore(OrganisationId, StoreName, SubDomain);
                    end = DateTime.Now;
                    timelog += "import 2nd store Complete" + DateTime.Now.ToLongTimeString() + " Total Seconds " + end.Subtract(st).TotalSeconds.ToString() + Environment.NewLine;
                    string StoreNamewop = ConfigurationManager.AppSettings["RetailStoreNameWOP"];
                    ImportStore(OrganisationId, StoreNamewop, SubDomain);

                    st = DateTime.Now;
                    end = DateTime.Now;
                    timelog += "import 3rd store Complete" + DateTime.Now.ToLongTimeString() + " Total Seconds " + end.Subtract(st).TotalSeconds.ToString() + Environment.NewLine;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;

            }

        }
        #endregion

        #region delete store

        public void DeleteStore(long CID, long OrganisationID)
        {
            try
            {
                // delete files


                // delete company files
                // delete assets 




                Company company = companyRepository.GetCompanyByCompanyID(CID);

                companyRepository.DeleteStoryBySP(CID);

                if (company != null)
                {


                    // delete company items
                    if (company.Items != null)
                    {
                        if (company.Items.Count > 0)
                        {
                            foreach (var item in company.Items)
                            {
                                // delete item things
                                string SourceItemFiles = HttpContext.Current.Server.MapPath("/MPC_Content/products/" + OrganisationID + "/" + item.ItemId);

                                if (Directory.Exists(SourceItemFiles))
                                {
                                    Directory.Delete(SourceItemFiles, true);
                                }


                                if (item.TemplateId != null && item.TemplateId > 0)
                                {
                                    if (item.DesignerCategoryId == 0 && item.DesignerCategoryId == null)
                                    {
                                        if (item.Template != null)
                                        {
                                            templateService.DeleteTemplateFiles(item.ItemId, OrganisationID);


                                        }

                                    }

                                }
                            }

                        }
                    }

                    // delete ordered items
                    if (company.Estimates != null && company.Estimates.Count > 0)
                    {
                        foreach (var estimate in company.Estimates)
                        {
                            if (estimate.Items != null && estimate.Items.Count > 0)
                            {
                                foreach (var item in estimate.Items)
                                {
                                    string SourceItemFiles = HttpContext.Current.Server.MapPath("/MPC_Content/products/" + OrganisationID + "/" + item.ItemId);

                                    if (Directory.Exists(SourceItemFiles))
                                    {
                                        Directory.Delete(SourceItemFiles, true);
                                    }


                                    if (item.TemplateId != null && item.TemplateId > 0)
                                    {
                                        if (item.DesignerCategoryId == 0 && item.DesignerCategoryId == null)
                                        {
                                            if (item.Template != null)
                                            {
                                                templateService.DeleteTemplateFiles(item.ItemId, OrganisationID);


                                            }

                                        }

                                    }
                                }
                            }
                        }
                    }

                    string SourceDelFiles = HttpContext.Current.Server.MapPath("/MPC_Content/assets/" + OrganisationID + "/" + CID);

                    if (Directory.Exists(SourceDelFiles))
                    {
                        Directory.Delete(SourceDelFiles, true);
                    }

                    // delete media
                    string SourceMediaFiles = HttpContext.Current.Server.MapPath("/MPC_Content/Media/" + OrganisationID + "/" + CID);

                    if (Directory.Exists(SourceMediaFiles))
                    {
                        Directory.Delete(SourceMediaFiles, true);
                    }



                }

            }
            catch (Exception ex)
            {

                throw ex;

            }

        }
        #endregion

        #region CopyStore

        public bool ImportStore(long OrganisationId, string StoreName, string SubDomain)
        {
            try
            {
                string status = string.Empty;
                ExportSets exportSets = new ExportSets();
                ExportOrganisation objExpCorp = new Models.Common.ExportOrganisation();
                ExportOrganisation objExpRetail = new Models.Common.ExportOrganisation();
                ExportOrganisation objExpCorpWOP = new Models.Common.ExportOrganisation();
                ExportOrganisation objExpRetailWOP = new Models.Common.ExportOrganisation();
                string extractPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/Artworks/ImportStore");

                string ZipPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/DefaulStorePackage/DefaultStores.zip");


                if (File.Exists(ZipPath))
                {

                    //string zipToUnpack = "C1P3SML.zip";
                    //string unpackDirectory = "Extracted Files";
                    using (ZipFile zip1 = ZipFile.Read(ZipPath))
                    {
                        // here, we extract every entry
                        foreach (ZipEntry e in zip1)
                        {
                            e.Extract(extractPath, ExtractExistingFileAction.OverwriteSilently);
                        }
                    }

                    // deserialize organisation json file for old org id
                    string JsonFilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/Artworks/ImportStore/OrganisationJson1.txt");
                    if (File.Exists(JsonFilePath))
                    {
                        string json = System.IO.File.ReadAllText(JsonFilePath);

                        exportSets.ExportOrganisationSet1 = JsonConvert.DeserializeObject<ExportOrganisation>(json);

                        json = string.Empty;
                    }

                    // deserialize retail json file
                    string JsonRetailFilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/Artworks/ImportStore/RetailJson1.txt");
                    if (File.Exists(JsonRetailFilePath))
                    {
                        string json = System.IO.File.ReadAllText(JsonRetailFilePath);

                        exportSets.ExportRetailStore1 = JsonConvert.DeserializeObject<ExportOrganisation>(json);

                        json = string.Empty;
                    }
                    string JsonRetailFilePath2 = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/Artworks/ImportStore/RetailJson2.txt");
                    if (File.Exists(JsonRetailFilePath2))
                    {
                        string json = System.IO.File.ReadAllText(JsonRetailFilePath2);

                        exportSets.ExportRetailStore3 = JsonConvert.DeserializeObject<List<Item>>(json);

                        json = string.Empty;
                    }

                    string ProdCatRetailFilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/Artworks/ImportStore/RetailProductCategories.txt");
                    if (File.Exists(ProdCatRetailFilePath))
                    {
                        string json = System.IO.File.ReadAllText(ProdCatRetailFilePath);

                        exportSets.ExportRetailStore2 = JsonConvert.DeserializeObject<List<ProductCategory>>(json);

                        json = string.Empty;
                    }
                    //string SecRetailFilePath2 = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/Artworks/ImportStore/RetailSecondaryPages.txt");
                    //if (File.Exists(SecRetailFilePath2))
                    //{
                    //    string json = System.IO.File.ReadAllText(SecRetailFilePath2);

                    //    exportSets.ExportRetailStore4 = JsonConvert.DeserializeObject<List<CmsPage>>(json);

                    //    json = string.Empty;
                    //}
                    // deserialize corpoate json file
                    string JsonCorpFilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/Artworks/ImportStore/CorporateJson1.txt");
                    if (File.Exists(JsonCorpFilePath))
                    {
                        string json = System.IO.File.ReadAllText(JsonCorpFilePath);

                        exportSets.ExportStore1 = JsonConvert.DeserializeObject<ExportOrganisation>(json);

                        json = string.Empty;
                    }
                    string JsonCorpFilePath2 = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/Artworks/ImportStore/CorporateJson2.txt");
                    if (File.Exists(JsonCorpFilePath2))
                    {
                        string json = System.IO.File.ReadAllText(JsonCorpFilePath2);

                        exportSets.ExportStore3 = JsonConvert.DeserializeObject<List<Item>>(json);

                        json = string.Empty;
                    }

                    string JsonCorpFilePath3 = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/Artworks/ImportStore/CorporateProductCategories.txt");
                    if (File.Exists(JsonCorpFilePath3))
                    {
                        string json = System.IO.File.ReadAllText(JsonCorpFilePath3);

                        exportSets.ExportStore2 = JsonConvert.DeserializeObject<List<ProductCategory>>(json);

                        json = string.Empty;
                    }
                    //string JsonCorpFilePath4 = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/Artworks/ImportStore/CorporateSecondaryPages.txt");
                    //if (File.Exists(JsonCorpFilePath4))
                    //{
                    //    string json = System.IO.File.ReadAllText(JsonCorpFilePath4);

                    //    exportSets.ExportStore4 = JsonConvert.DeserializeObject<List<CmsPage>>(json);

                    //    json = string.Empty;
                    //}
                    // deserialize corpoate json file WOP
                    string JsonCorpFilePathWOP = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/Artworks/ImportStore/CorporateJson1WOP.txt");
                    if (File.Exists(JsonCorpFilePathWOP))
                    {
                        string json = System.IO.File.ReadAllText(JsonCorpFilePathWOP);

                        exportSets.ExportStore1WOP = JsonConvert.DeserializeObject<ExportOrganisation>(json);

                        json = string.Empty;
                    }
                    string JsonCorpFilePath2WOP = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/Artworks/ImportStore/CorporateJson2WOP.txt");
                    if (File.Exists(JsonCorpFilePath2WOP))
                    {
                        string json = System.IO.File.ReadAllText(JsonCorpFilePath2WOP);

                        exportSets.ExportStore3WOP = JsonConvert.DeserializeObject<List<Item>>(json);

                        json = string.Empty;
                    }

                    string JsonCorpFilePath3WOP = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/Artworks/ImportStore/CorporateProductCategoriesWOP.txt");
                    if (File.Exists(JsonCorpFilePath3WOP))
                    {
                        string json = System.IO.File.ReadAllText(JsonCorpFilePath3WOP);

                        exportSets.ExportStore2WOP = JsonConvert.DeserializeObject<List<ProductCategory>>(json);

                        json = string.Empty;
                    }
                    //string JsonCorpFilePath4WOP = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/Artworks/ImportStore/CorporateSecondaryPagesWOP.txt");
                    //if (File.Exists(JsonCorpFilePath4WOP))
                    //{
                    //    string json = System.IO.File.ReadAllText(JsonCorpFilePath4WOP);

                    //    exportSets.ExportStore4WOP = JsonConvert.DeserializeObject<List<CmsPage>>(json);

                    //    json = string.Empty;
                    //}
                    // deserialize retail without product
                    // deserialize retail json file
                    string JsonRetailFilePathWOP = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/Artworks/ImportStore/RetailJson1WOP.txt");
                    if (File.Exists(JsonRetailFilePathWOP))
                    {
                        string json = System.IO.File.ReadAllText(JsonRetailFilePathWOP);

                        exportSets.ExportRetailStore1WOP = JsonConvert.DeserializeObject<ExportOrganisation>(json);

                        json = string.Empty;
                    }
                    string JsonRetailFilePath2WOP = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/Artworks/ImportStore/RetailJson2WOP.txt");
                    if (File.Exists(JsonRetailFilePath2WOP))
                    {
                        string json = System.IO.File.ReadAllText(JsonRetailFilePath2WOP);

                        exportSets.ExportRetailStore3WOP = JsonConvert.DeserializeObject<List<Item>>(json);

                        json = string.Empty;
                    }

                    string ProdCatRetailFilePathWOP = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/Artworks/ImportStore/RetailProductCategoriesWOP.txt");
                    if (File.Exists(ProdCatRetailFilePathWOP))
                    {
                        string json = System.IO.File.ReadAllText(ProdCatRetailFilePathWOP);

                        exportSets.ExportRetailStore2WOP = JsonConvert.DeserializeObject<List<ProductCategory>>(json);

                        json = string.Empty;
                    }
                    //string SecRetailFilePath2WOP = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/Artworks/ImportStore/RetailSecondaryPagesWOP.txt");
                    //if (File.Exists(SecRetailFilePath2WOP))
                    //{
                    //    string json = System.IO.File.ReadAllText(SecRetailFilePath2WOP);

                    //    exportSets.ExportRetailStore4WOP = JsonConvert.DeserializeObject<List<CmsPage>>(json);

                    //    json = string.Empty;
                    //}
                    status += "deserializationDone";
                    status += companyRepository.InsertStore(OrganisationId, objExpCorp, objExpRetail, objExpCorpWOP, objExpRetailWOP, StoreName, exportSets, SubDomain, status);

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}

