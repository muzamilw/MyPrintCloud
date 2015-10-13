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
using Scope = System.IdentityModel.Scope;
using System.Text;


namespace MPC.Implementation.MISServices
{
    public class CompanyService : ICompanyService
    {

        #region Private

        #region Repositories

        private readonly ICompanyRepository companyRepository;
       
        private readonly ICampaignImageRepository campaignImageRepository;
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
        private readonly ICmsSkinPageWidgetParamRepository cmsSkinPageWidgetParamRepository;
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
        private readonly IStagingImportCompanyContactAddressRepository stagingImportCompanyContactRepository;
        private readonly ICostCentersService CostCentreService;
        private readonly IDiscountVoucherRepository discountVoucherRepository;
        private readonly ITemplateVariableRepository templateVariableRepository;
        private readonly IActivityRepository activityRepository;
        private readonly IProductCategoryVoucherRepository productcategoryvoucherRepository;
        private readonly ItemsVoucherRepository itemsVoucherRepository;
        private readonly ICMSOfferRepository cmsofferRepository;
        private readonly IReportNoteRepository reportNoteRepository;
        private readonly IVariableExtensionRespository variableExtensionRespository;
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
           // SaveCompanyCss(companySavingModel.Company);
            UpdateMediaLibraryFilePath(companySavingModel.Company, companyDbVersion);
            UpdateContactProfileImage(companySavingModel, companyDbVersion);

            SaveCompanyBannerImages(companySavingModel.Company, companyDbVersion);
            SaveStoreBackgroundImage(companySavingModel.Company, companyDbVersion);
            //UpdateSecondaryPageImagePath(companySavingModel, companyDbVersion);
            UpdateCampaignImages(companySavingModel, companyDbVersion);


            //UpdateSmartFormVariableIds(companySavingModel.Company.SmartForms, companyDbVersion);

            UpdateScopeVariables(companySavingModel);
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

        public void UpdateCompanyCss(string sCustomCss, long oCompanyId)
        {
            Company companyDbVersion = companyRepository.Find(oCompanyId);
            companyDbVersion.CustomCSS = sCustomCss;
            companyRepository.Update(companyDbVersion);
            
            string directoryPath =
                HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" +
                                                   oCompanyId);
            
            if (directoryPath != null && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string savePath = directoryPath + "\\site.css";
            File.WriteAllText(savePath, sCustomCss);
            companyRepository.SaveChanges();
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

            if (fieldVariable.VariableExtensions != null)
            {
                foreach (var vExtension in fieldVariable.VariableExtensions)
                {
                    vExtension.OrganisationId = (int)fieldVariableRepository.OrganisationId;
                }
            }

            fieldVariableRepository.Add(fieldVariable);
            List<ScopeVariable> scopeVariables = new List<ScopeVariable>();
            fieldVariable.ScopeVariables = scopeVariables;
            UpdateScopeVariables(fieldVariable);

            fieldVariableRepository.SaveChanges();
            return fieldVariable.VariableId;
        }

        private void UpdateScopeVariables(FieldVariable fieldVariable)
        {
            long companyId = (long)(fieldVariable.CompanyId ?? 0);
            if (fieldVariable.ScopeVariables == null)
            {
                fieldVariable.ScopeVariables = new List<ScopeVariable>();
            }
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
                        scopeVariable.VariableId = fieldVariable.VariableId;
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
                        scopeVariable.VariableId = fieldVariable.VariableId;
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
                        scopeVariable.VariableId = fieldVariable.VariableId;
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
                scopeVariable.VariableId = fieldVariable.VariableId;
                scopeVariable.Value = fieldVariable.DefaultValue;
                scopeVariableRepository.Add(scopeVariable);
                fieldVariable.ScopeVariables.Add(scopeVariable);
            }
        }

        private void DeleteScopeVariables(FieldVariable fieldVariable)
        {
            if (fieldVariable.ScopeVariables != null)
            {
                List<ScopeVariable> scopeVariablesDeleteList = new List<ScopeVariable>();
                foreach (var sVar in fieldVariable.ScopeVariables)
                {
                    scopeVariablesDeleteList.Add(sVar);
                }

                foreach (var sVar in scopeVariablesDeleteList)
                {
                    fieldVariable.ScopeVariables.Remove(sVar);
                    scopeVariableRepository.Delete(sVar);
                }
            }
        }


        private void AddScopeVariables(FieldVariable fieldVariable, FieldVariable fieldVariableDbVersion)
        {
            if (fieldVariableDbVersion.ScopeVariables == null)
            {
                fieldVariableDbVersion.ScopeVariables = new List<ScopeVariable>();
            }

            List<ScopeVariable> scopeVariables = new List<ScopeVariable>();
            foreach (var sVar in fieldVariable.ScopeVariables)
            {
                fieldVariableDbVersion.ScopeVariables.Add(sVar);
                scopeVariables.Add(sVar);
            }
            // Reset the list
            scopeVariables.ForEach(sc => fieldVariable.ScopeVariables.Remove(sc));
        }
        /// <summary>
        /// Update Field Variable
        /// </summary>
        private long UpdateFieldVariable(FieldVariable fieldVariable)
        {
            FieldVariable fieldVariableDbVersion = fieldVariableRepository.Find(fieldVariable.VariableId);
            if (fieldVariableDbVersion != null)
            {
                if (fieldVariable.Scope != fieldVariableDbVersion.Scope)
                {
                    DeleteScopeVariables(fieldVariableDbVersion);
                    UpdateScopeVariables(fieldVariable);
                    AddScopeVariables(fieldVariable, fieldVariableDbVersion);
                }
                fieldVariableDbVersion.InputMask = fieldVariable.InputMask;
                fieldVariableDbVersion.VariableName = fieldVariable.VariableName;
                fieldVariableDbVersion.DefaultValue = fieldVariable.DefaultValue;
                fieldVariableDbVersion.Scope = fieldVariable.Scope;
                fieldVariableDbVersion.VariableTag = fieldVariable.VariableTag;
                fieldVariableDbVersion.VariableTitle = fieldVariable.VariableTitle;
                fieldVariableDbVersion.VariableType = fieldVariable.VariableType;
                fieldVariableDbVersion.WaterMark = fieldVariable.WaterMark;
                if (fieldVariableDbVersion.IsSystem != true)
                {
                    fieldVariableDbVersion.OrganisationId = fieldVariableRepository.OrganisationId;
                }
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
                if (fieldVariable.VariableExtensions != null && fieldVariableDbVersion.VariableExtensions != null)
                {

                    foreach (var item in fieldVariable.VariableExtensions)
                    {
                        VariableExtension variableExtensionDb = fieldVariableDbVersion.VariableExtensions.FirstOrDefault(vx => vx.Id == item.Id);
                        if (variableExtensionDb != null)
                        {
                            variableExtensionDb.CompanyId = item.CompanyId;
                            variableExtensionDb.VariablePrefix = item.VariablePrefix;
                            variableExtensionDb.VariablePostfix = item.VariablePostfix;
                            variableExtensionDb.CollapsePrefix = item.CollapsePrefix;
                            variableExtensionDb.CollapsePostfix = item.CollapsePostfix;
                            variableExtensionDb.OrganisationId = (int)fieldVariableRepository.OrganisationId;
                        }
                        else
                        {
                            item.OrganisationId = (int)fieldVariableRepository.OrganisationId;
                            fieldVariableDbVersion.VariableExtensions.Add(item);
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
                    smartFormDbVersion.SmartFormDetails.Remove(missingItem);
                    smartFormDetailRepository.Delete(missingItem);
                }
            }

            #endregion

            smartFormRepository.SaveChanges();
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
                        cmsSkinPageWidgetRepository.Delete(cmsSkinPageWidgetItem);
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
            ITemplateColorStylesRepository templateColorStylesRepository, IStagingImportCompanyContactAddressRepository stagingImportCompanyContactRepository,
            ICostCentersService CostCentreService, IDiscountVoucherRepository discountVoucherRepository, ICampaignImageRepository campaignImageRepository, ICmsSkinPageWidgetParamRepository cmsSkinPageWidgetParamRepository, ITemplateVariableRepository templateVariableRepository,
            IActivityRepository activityRepository, IProductCategoryVoucherRepository productcategoryvoucherRepository, ItemsVoucherRepository itemsVoucherRepository, ICMSOfferRepository cmsofferRepository, IReportNoteRepository reportNoteRepository, IVariableExtensionRespository variableExtensionRespository)
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
            this.stagingImportCompanyContactRepository = stagingImportCompanyContactRepository;
            this.CostCentreService = CostCentreService;
            this.discountVoucherRepository = discountVoucherRepository;
            this.campaignImageRepository = campaignImageRepository;
            this.cmsSkinPageWidgetParamRepository = cmsSkinPageWidgetParamRepository;
            this.templateVariableRepository = templateVariableRepository;
            this.activityRepository = activityRepository;
            this.productcategoryvoucherRepository = productcategoryvoucherRepository;
            this.itemsVoucherRepository = itemsVoucherRepository;
            this.cmsofferRepository = cmsofferRepository;
            this.reportNoteRepository = reportNoteRepository;
            this.variableExtensionRespository = variableExtensionRespository;


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
                List<CmsPage> cmsPages = cmsPageRepository.GetCmsPagesByOrganisationForBanners(mediaLibraryDbVersion.CompanyId);
                // List<CompanyBanner> companyBanners = companyBannerRepository.GetAll().ToList();
                
                //IEnumerable<CmsPage> cmsPages = cmsPageRepository.GetAll();
                CmsPage cmsPage = cmsPages.FirstOrDefault(cp => cp.PageBanner == mediaLibraryDbVersion.FilePath);
                if (cmsPage != null)
                {
                    throw new MPCException(string.Format(CultureInfo.InvariantCulture, "File is used in CMS page."), companyRepository.OrganisationId);
                }

                //IEnumerable<CompanyBanner> companyBanners = companyBannerRepository.GetAll();
                List<string> companyBanners = bannerSetRepository.GetCompanyBannersByCompanyId(mediaLibraryDbVersion.CompanyId);
                //CompanyBanner companyBanner = companyBanners.FirstOrDefault(cp => cp.ImageURL == mediaLibraryDbVersion.FilePath);
                var companyBanner = companyBanners.Contains(mediaLibraryDbVersion.FilePath);
                if (companyBanner == true)
                {
                    throw new MPCException(string.Format(CultureInfo.InvariantCulture, "File is used in Banner."), companyRepository.OrganisationId);
                }

                mediaLibraryRepository.Delete(mediaLibraryDbVersion);
                string currFile = HttpContext.Current.Server.MapPath("~/" + mediaLibraryDbVersion.FilePath);
                if (File.Exists(currFile))
                    File.Delete(currFile);
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
        /// Delete CRM Company Permanently
        /// </summary>
        public void DeleteCrmCompanyPermanently(long companyId)
        {
            Company company = companyRepository.Find(companyId);

            if (company == null)
            {
                throw new MPCException(string.Format(CultureInfo.InvariantCulture, "Company with id {0} not found", companyId), companyRepository.OrganisationId);
            }

            companyRepository.DeleteCrmCompanyBySP(companyId);
        }

        /// <summary>
        /// Get Items For Widgets
        /// </summary>
        public List<Item> GetItemsForWidgets()
        {
            return itemRepository.GetItemsForWidgets();
        }
        public List<Item> GetItemsForWidgetsByStoreId(long storeId)
        {
            return itemRepository.GetItemsForWidgetsByStoreId(storeId);
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
            DiscountVoucherRequestModel discountVoucherRequestModelRequest = new DiscountVoucherRequestModel();
            request.CompanyId = storeId;
            smartFormRequest.CompanyId = storeId;
            discountVoucherRequestModelRequest.CompanyId = storeId;

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
                Widgets = widgetRepository.GetAll().OrderBy(o => o.WidgetControlName),
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
        /// Get System Variables
        /// </summary>
        public FieldVariableResponse GetSystemVariables(FieldVariableRequestModel request)
        {
            return fieldVariableRepository.GetSystemFieldVariable(request);
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
                Companies = companyRepository.GetAllRetailStores()//Get Retail Stores in crm base data
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
            if (!CanSaveStore(companyModel.Company))
            {
                companyModel.Company.IsClickReached = true;
                return companyModel.Company;
            }
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

        private bool CanSaveStore(Company comp)
        {
            string allowedOfflineClicks = ConfigurationManager.AppSettings["OfflineStoreClicks"];
            var org = organisationRepository.GetOrganizatiobByID();
            int liveStoresCount = org.LiveStoresCount ?? 0;
            if (comp.IsCustomer == 3 || comp.IsCustomer == 4)
            {
                if (liveStoresCount <= 0)
                {
                    if ((org.OfflineStoreClicks ?? 0) >= Convert.ToInt32(allowedOfflineClicks))
                    {
                        return false;
                    }
                    else
                    {
                        org.OfflineStoreClicks = (org.OfflineStoreClicks ?? 0) + 1;
                        organisationRepository.Update(org);
                        organisationRepository.SaveChanges();
                        return true;
                    }
                }
            }

            return true;
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

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string url = "WebstoreApi/StoreCache/Get?id=" + companyId;
                var response = client.GetAsync(url);
                if (!response.Result.IsSuccessStatusCode)
                {
                    //throw new MPCException("Failed to clear store cache", companyRepository.OrganisationId);
                }
            }

        }

        private void DeleteMediaFiles(long companyId)
        {
            IEnumerable<MediaLibrary> mediaLibraries = mediaLibraryRepository.GetMediaLibrariesByCompanyId(companyId);
            List<CmsPage> cmsPages =
                cmsPageRepository.GetCmsPagesByOrganisationForBanners(companyId);
           // List<CompanyBanner> companyBanners = companyBannerRepository.GetAll().ToList();
            List<string> companyBanners = bannerSetRepository.GetCompanyBannersByCompanyId(companyId);

            List<MediaLibrary> mediaLibrariesForDelete = new List<MediaLibrary>();
            foreach (var media in mediaLibraries)
            {

                //CmsPage cmsPage = cmsPages.FirstOrDefault(cp => cp.PageBanner == media.FilePath);
                //CompanyBanner companyBanner = companyBanners.FirstOrDefault(cp => cp.ImageURL == media.FilePath);
                var cmsPage = cmsPages.Where(c => c.PageBanner == media.FilePath).Select(c => c.PageBanner).FirstOrDefault();
                //var companyBanner = companyBanners.Where(c => c.ImageURL == media.FilePath).Select(c => c.ImageURL).FirstOrDefault();
                var companyBanner = companyBanners.Contains(media.FilePath);
                if (cmsPage == null && companyBanner == true)
                {
                    mediaLibrariesForDelete.Add(media);
                }
            }
            
            foreach (var item in mediaLibrariesForDelete)
            {
                mediaLibraryRepository.Delete(item);
                string currFile = HttpContext.Current.Server.MapPath("~/" + item.FilePath);
                if (File.Exists(currFile))
                    File.Delete(currFile);
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
        /// <summary>
        /// Save Imported Company Contacts
        /// </summary>
        /// <param name="stagingImportCompanyContact"></param>
        /// <returns></returns>
        public bool SaveImportedCompanyContact(IEnumerable<StagingImportCompanyContactAddress> stagingImportCompanyContact)
        {
            //Calling Stored Procedure to delete all records in staging company contact table
            stagingImportCompanyContactRepository.RunProcedureToDeleteAllStagingCompanyContact();

            foreach (var companyContact in stagingImportCompanyContact)
            {
                companyContact.OrganisationId = stagingImportCompanyContactRepository.OrganisationId;
                stagingImportCompanyContactRepository.Add(companyContact);
            }
            stagingImportCompanyContactRepository.SaveChanges();
            stagingImportCompanyContactRepository.RunProcedure(stagingImportCompanyContactRepository.OrganisationId,
                stagingImportCompanyContact.FirstOrDefault().CompanyId);

            return true;
        }

        public bool SaveCRMImportedCompanyContact(IEnumerable<StagingImportCompanyContactAddress> stagingImportCompanyContact)
        {
            //Calling Stored Procedure to delete all records in staging company contact table
            stagingImportCompanyContactRepository.RunProcedureToDeleteAllStagingCompanyContact();

            foreach (var companyContact in stagingImportCompanyContact)
            {
                companyContact.OrganisationId = stagingImportCompanyContactRepository.OrganisationId;
                stagingImportCompanyContactRepository.Add(companyContact);
            }
            stagingImportCompanyContactRepository.SaveChanges();
            stagingImportCompanyContactRepository.RunCRMProcedure();

            return true;
        }

        /// <summary>
        /// Add/Update Discount Voucher
        /// </summary>
        public DiscountVoucher SaveDiscountVoucher(DiscountVoucher discountVoucher)
        {
            if (discountVoucher.DiscountVoucherId == 0)
            {
                return AddDiscountVoucher(discountVoucher);
            }
            return UpdateDiscountVoucher(discountVoucher);
        }

        /// <summary>
        /// Get Discount Voucher By Id
        /// </summary>
        public DiscountVoucher GetDiscountVoucherById(long discountVoucherId)
        {
            return discountVoucherRepository.GetDiscountVoucherByVoucherId(discountVoucherId);
        }
        private DiscountVoucher AddDiscountVoucher(DiscountVoucher discountVoucher)
        {
            DiscountVoucher DV = discountVoucherRepository.CreateDiscountVoucher(discountVoucher);
            return DV;
        }

        private DiscountVoucher UpdateDiscountVoucher(DiscountVoucher discountVoucher)
        {
       
            DiscountVoucher voucher = discountVoucherRepository.UpdateVoucher(discountVoucher);
            return voucher;
        }

        public List<LiveStoreDetails> GetLiveStoresJason()
        {
            string stores = string.Empty;
            List<Company> livestores = companyRepository.GetLiveStoresList();
            List<LiveStoreDetails> storeDetails = new List<LiveStoreDetails>();
            foreach (var company in livestores)
            {
                var address = company.Addresses.FirstOrDefault();
                string domainName = string.Empty;
                //mpc/store/Ooo2112
                if (company.CompanyDomains.Count() > 1)
                {
                    var odomain = company.CompanyDomains.Where(c => !c.Domain.Contains("/store/" + company.WebAccessCode)).FirstOrDefault();
                    domainName = odomain != null
                        ? odomain.Domain
                        : company.CompanyDomains.FirstOrDefault() != null
                            ? company.CompanyDomains.FirstOrDefault().Domain ?? ""
                            : "";
                    
                }
                else
                {
                   var odomain = company.CompanyDomains.FirstOrDefault();
                    domainName = odomain.Domain != null ? odomain.Domain : string.Empty;
                }
                
                storeDetails.Add(new LiveStoreDetails
                {
                    OrganisationId = company.OrganisationId?? 0, 
                    StoreId = company.CompanyId,
                    StoreCode  = company.WebAccessCode,
                    StoreName = company.Name,
                    StoreType = company.IsCustomer,
                    LogoUrl = company.Image,
                    Address1 = address != null ? address.Address1 : string.Empty,
                    Address2 = address != null ? address.Address2: string.Empty,
                    AddressName = address != null ? address.AddressName : string.Empty,
                    City = address != null ? address.City : string.Empty,
                    Country = address != null ? address.Country != null ? address.Country.CountryName: string.Empty : string.Empty,
                    State = address != null ? address.State != null? address.State.StateName: string.Empty : string.Empty,
                    DefaultDomain = domainName,
                    GeoLatitude = address != null ? address.GeoLatitude : string.Empty,
                    GeoLongitude = address != null ? address.GeoLongitude : string.Empty
                });
            }


            return storeDetails;
        }

        public string GetCompanyCss(long companyId)
        {
            string defaultCss = string.Empty;
            string defaultCssPath =
                HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" +
                                                   companyId + "/site.css");

            if (File.Exists(defaultCssPath))
            {
                defaultCss = File.ReadAllText(HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + companyId + "/site.css"));
            }
            return defaultCss;
        }


        public RealEstateVariableIconsListViewResponse GetCompanyVariableIcons(CompanyVariableIconRequestModel request)
        {
            return companyRepository.GetCompanyVariableIcons(request);
        }

        public void DeleteCompanyVariableIcon(long iconId)
        {
            companyRepository.DeleteCompanyVariableIcon(iconId);
        }


        public void SaveCompanyVariableIcon(CompanyVariableIconRequestModel request)
        {
            companyRepository.SaveCompanyVariableIcon(request);
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

                   // CostCentre objCostCentre = costCentreRepository.GetFirstCostCentreByOrganisationId(OrganisationId);
                  //  CostCentreService.CostCentreDLL(objCostCentre, OrganisationId);
                    CostCentreService.ReCompileAllCostCentres(OrganisationId);

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

        #region ImportStore

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


        #region CopyStore

        /// <summary>
        /// Clone Product
        /// </summary>
        public Company CloneStore(long companyId)
        {
            try
            {
                // company id after clonning
                long NewCompanyId = 0;

                // Find Company - Throws Exception if not exist
                Company source = companyRepository.GetCompanyByCompanyID(companyId);


                // Create New Instance

                Company target = CreateNewCompany();

                // Clone
                NewCompanyId = CloneCompany(source, target);


                // insert product categories and items
               // companyRepository.CopyProductByStore(NewCompanyId, companyId);

          


                // insert discount voucher
                CloneDiscountVouchers(companyId, NewCompanyId);

                // insert template fonts
                CloneTemplateFonts(companyId, NewCompanyId);

                CloneReportBanners(companyId, NewCompanyId);
                // update data
                Company objCompany = companyRepository.LoadCompanyWithItems(NewCompanyId);

                companyRepository.SetTerritoryIdAddress(objCompany,source.CompanyId);
                companyRepository.InsertProductCategories(objCompany,source.CompanyId);
                companyRepository.InsertItem(objCompany,source.CompanyId);

                // insert reports
                if (objCompany != null)
                {
                    string SetName = source.CompanyBannerSets.Where(c => c.CompanySetId == source.ActiveBannerSetId).Select(c => c.SetName).FirstOrDefault();
                    SetValuesAfterClone(objCompany, SetName,source.CompanyId);

                    // copy variable extension of system variables
                    companyRepository.SaveSystemVariableExtension(companyId, objCompany.CompanyId);
                    companyRepository.InsertProductCategoryItems(objCompany, source);
                    // copy All files or images
                    CopyCompanyFiles(objCompany, companyId);
                }



                // Load Item Full
                // target = itemRepository.GetItemWithDetails(target.ItemId);

                // Get Updated Minimum Price
                //target.MinPrice = itemRepository.GetMinimumProductValue(target.ItemId);

                // convert template length to system unit 
                //  ConvertTemplateLengthToSystemUnit(target);

                // Return Product
                companyRepository.SaveChanges();
                return objCompany;
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
            
        }

        /// <summary>
        /// Creates New Item and assigns new generated code
        /// </summary>
        private Company CreateNewCompany()
        {

            Company companyTarget = companyRepository.Create();
            companyRepository.Add(companyTarget);
            companyTarget.OrganisationId = companyRepository.OrganisationId;
            return companyTarget;
        }

        /// <summary>
        /// Creates Copy of Product
        /// </summary>
        private long CloneCompany(Company source, Company target)
        {
            try
            {
                // Clone Item
                source.Clone(target);

                // Clone Company Domains
                CloneCompanyDomain(source, target);

                // clone Media Library
                CloneMediaLibrary(source, target);


                // Clone company banners sets and its banner
                CloneCompanyBannerSet(source, target);

                // Clone cms pages
                CloneCMSPages(source, target);

                

                // clone payment gateways
                ClonePaymentGateways(source, target);

                // Clone Rave Reviews
                CloneRaveReviews(source, target);

                // Clone Company Territory
                CloneCompanyTerritory(source, target);

                // Clone Addresses
                CloneAddresses(source, target);

                // Clone company contacts
               CloneCompanyContacts(source, target);

                // Clone campaignEmails
                CloneCampaigns(source, target);

                // Clone Pcompany cost centre
                CloneCompanyCostCentre(source, target);

                // clone template color style
                CloneTemplateColorStyles(source, target);

                // Clone company cmyk colors
                CloneCompanyCMYKColor(source, target);


                // Clone field variables
                CloneFieldVariables(source, target);

                // Clone smart forms and its details
                CloneSmartFom(source, target);

                // clone cms offers
                CloneCMSOffer(source,target);

                // Clone activities
                CloneActivities(source, target);


                companyRepository.SaveChanges();

                return target.CompanyId;
               

               
             



            }
            catch(Exception ex)
            {
                throw ex;
            }
          
        }




        /// <summary>
        /// Copy Company Domains
        /// </summary>
        public void CloneCompanyDomain(Company source, Company target)
        {
            string subdomain = HttpContext.Current.Request.Url.Host.ToString();
            CompanyDomain domain = companyDomainRepository.Create();
            domain.Domain = subdomain;
            domain.CompanyId = target.CompanyId;
            companyDomainRepository.Add(domain);


        }



        /// <summary>
        /// Copy company banners
        /// </summary>
        private void CloneCompanyBannerSet(Company source, Company target)
        {
            if (source.CompanyBannerSets == null)
            {
                return;
            }

            // Initialize List
            if (target.CompanyBannerSets == null)
            {
                target.CompanyBannerSets = new List<CompanyBannerSet>();
            }

            foreach (CompanyBannerSet companyBannerSet in source.CompanyBannerSets)
            {
               

                CompanyBannerSet targetCompanyBannerSet = bannerSetRepository.Create();
                bannerSetRepository.Add(targetCompanyBannerSet);
                targetCompanyBannerSet.CompanyId = targetCompanyBannerSet.CompanyId;
                target.CompanyBannerSets.Add(targetCompanyBannerSet);
                companyBannerSet.Clone(targetCompanyBannerSet);

                // Clone CompanyBanners
                if (companyBannerSet.CompanyBanners == null)
                {
                    continue;
                }

                // Copy CompanyBanners
                CloneCompanyBanners(companyBannerSet, targetCompanyBannerSet);
            }
        }
        /// <summary>
        /// Creates Copy of company  Banners
        /// </summary>
        private void CloneCompanyBanners(CompanyBannerSet companyBannerSets, CompanyBannerSet targetcompanyBannerSets)
        {
            if (targetcompanyBannerSets.CompanyBanners == null)
            {
                targetcompanyBannerSets.CompanyBanners = new List<CompanyBanner>();
            }

            foreach (CompanyBanner objcompanyBanners in companyBannerSets.CompanyBanners.ToList())
            {
                CompanyBanner targetCompanyBanner = companyBannerRepository.Create();
                companyBannerRepository.Add(targetCompanyBanner);
                targetCompanyBanner.CompanySetId = targetcompanyBannerSets.CompanySetId;
                targetcompanyBannerSets.CompanyBanners.Add(targetCompanyBanner);
                objcompanyBanners.Clone(targetCompanyBanner);
            }
        }

        /// <summary>
        /// Copy cms pages
        /// </summary>
        private void CloneCMSPages(Company source, Company target)
        {
            if (source.CmsPages == null)
            {
                return;
            }

            // Initialize List
            if (target.CmsPages == null)
            {
                target.CmsPages = new List<CmsPage>();
            }

            foreach (CmsPage cmsPage in source.CmsPages)
            {
                CmsPage targetCMSPage = cmsPageRepository.Create();
                cmsPageRepository.Add(targetCMSPage);
                targetCMSPage.CompanyId = target.CompanyId;
                target.CmsPages.Add(targetCMSPage);
                cmsPage.Clone(targetCMSPage);

                // Clone CompanyBanners
                if (cmsPage.CmsSkinPageWidgets == null)
                {
                    continue;
                }

                // Copy CompanyBanners
                CloneCMSSkinPageWidgets(cmsPage, targetCMSPage,target);

            }
        }
        /// <summary>
        /// Creates Copy of company  Banners
        /// </summary>
        private void CloneCMSSkinPageWidgets(CmsPage cmspage, CmsPage targetcmspage,Company targetCompany)
        {
            if (targetcmspage.CmsSkinPageWidgets == null)
            {
                targetcmspage.CmsSkinPageWidgets = new List<CmsSkinPageWidget>();
            }

            foreach (CmsSkinPageWidget objcmsSkinPageWidget in cmspage.CmsSkinPageWidgets.ToList())
            {
                CmsSkinPageWidget targetCMSSkinPageWidget = cmsSkinPageWidgetRepository.Create();
                cmsSkinPageWidgetRepository.Add(targetCMSSkinPageWidget);
                targetCMSSkinPageWidget.PageId = targetcmspage.PageId;
                targetCMSSkinPageWidget.OrganisationId = cmsSkinPageWidgetRepository.OrganisationId;
                targetCMSSkinPageWidget.CompanyId = targetCompany.CompanyId;
                targetcmspage.CmsSkinPageWidgets.Add(targetCMSSkinPageWidget);
                objcmsSkinPageWidget.Clone(targetCMSSkinPageWidget);

                // Clone params
                if (objcmsSkinPageWidget.CmsSkinPageWidgetParams == null)
                {
                    continue;
                }

                // Copy params
                CloneCMSSkinPageWidgetsParams(objcmsSkinPageWidget, targetCMSSkinPageWidget);

            }
        }
        private void CloneCMSSkinPageWidgetsParams(CmsSkinPageWidget cmsskinPageWidget, CmsSkinPageWidget targetcmsskinPageWidget)
        {
            if (targetcmsskinPageWidget.CmsSkinPageWidgetParams == null)
            {
                targetcmsskinPageWidget.CmsSkinPageWidgetParams = new List<CmsSkinPageWidgetParam>();
            }      
            foreach (CmsSkinPageWidgetParam objcmsSkinPageWidgetParams in cmsskinPageWidget.CmsSkinPageWidgetParams.ToList())
            {
                CmsSkinPageWidgetParam targetCMSSkinPageWidgetParam = cmsSkinPageWidgetParamRepository.Create();
                cmsSkinPageWidgetParamRepository.Add(targetCMSSkinPageWidgetParam);

                targetCMSSkinPageWidgetParam.PageWidgetId = targetcmsskinPageWidget.PageWidgetId;

                targetcmsskinPageWidget.CmsSkinPageWidgetParams.Add(targetCMSSkinPageWidgetParam);
                objcmsSkinPageWidgetParams.Clone(targetCMSSkinPageWidgetParam);

             

            }
        }
        /// <summary>
        /// Copy  payment gateways
        /// </summary>
        private void ClonePaymentGateways(Company source, Company target)
        {
            if (source.PaymentGateways == null)
            {
                return;
            }

            // Initialize List
            if (target.PaymentGateways == null)
            {
                target.PaymentGateways = new List<PaymentGateway>();
            }

            foreach (PaymentGateway paymentGateways in source.PaymentGateways)
            {
                PaymentGateway targetpaymentGateway = paymentGatewayRepository.Create();
                paymentGatewayRepository.Add(targetpaymentGateway);
                targetpaymentGateway.CompanyId = target.CompanyId;
                target.PaymentGateways.Add(targetpaymentGateway);
                paymentGateways.Clone(targetpaymentGateway);
            }
        }


        /// <summary>
        /// Copy rave reviews
        /// </summary>
        private void CloneRaveReviews(Company source, Company target)
        {
            if (source.RaveReviews == null)
            {
                return;
            }

            // Initialize List
            if (target.RaveReviews == null)
            {
                target.RaveReviews = new List<RaveReview>();
            }

            foreach (RaveReview raveReview in source.RaveReviews)
            {
                RaveReview targetRaveReviews = raveReviewRepository.Create();
                raveReviewRepository.Add(targetRaveReviews);
                targetRaveReviews.CompanyId = target.CompanyId;
                target.RaveReviews.Add(targetRaveReviews);
                raveReview.Clone(targetRaveReviews);
            }
        }


        /// <summary>
        /// Copy company Territory
        /// </summary>
        private void CloneCompanyTerritory(Company source, Company target)
        {
            if (source.CompanyTerritories == null)
            {
                return;
            }

            // Initialize List
            if (target.CompanyTerritories == null)
            {
                target.CompanyTerritories = new List<CompanyTerritory>();
            }

            foreach (CompanyTerritory companyTerritory in source.CompanyTerritories)
            {
                CompanyTerritory targetCompanyTerritory = companyTerritoryRepository.Create();
                companyTerritoryRepository.Add(targetCompanyTerritory);
                targetCompanyTerritory.CompanyId = target.CompanyId;
                targetCompanyTerritory.Addresses = null;
                target.CompanyTerritories.Add(targetCompanyTerritory);
                companyTerritory.Clone(targetCompanyTerritory);
            }
        }

        /// <summary>
        /// Copy ADDRESSES
        /// </summary>
        private void CloneAddresses(Company source, Company target)
        {
            if (source.Addresses == null)
            {
                return;
            }

            // Initialize List
            if (target.Addresses == null)
            {
                target.Addresses = new List<Address>();
            }

            foreach (Address addresses in source.Addresses)
            {

                //string OldTerritoryName = addresses.CompanyTerritory.TerritoryName;
                //string oldTerritoryCode = addresses.CompanyTerritory.TerritoryCode;

                //CompanyTerritory NewTerrObj = target.CompanyTerritories.Where(c => c.TerritoryName == OldTerritoryName && c.TerritoryCode == oldTerritoryCode).FirstOrDefault();


                Address targetAddress = addressRepository.Create();
                addressRepository.Add(targetAddress);
                targetAddress.CompanyId = target.CompanyId;
                targetAddress.Tel2 = Convert.ToString(addresses.AddressId);
               // targetAddress.TerritoryId = NewTerrObj != null ? NewTerrObj.TerritoryId : 0;
                target.Addresses.Add(targetAddress);
                addresses.Clone(targetAddress);
            }
           
        }

        /// <summary>
        /// Copy companyContacts
        /// </summary>
        private void CloneCompanyContacts(Company source, Company target)
        {
            if (source.CompanyContacts == null)
            {
                return;
            }

            // Initialize List
            if (target.CompanyContacts == null)
            {
                target.CompanyContacts = new List<CompanyContact>();
            }

            foreach (CompanyContact contacts in source.CompanyContacts)
            {

                string OldTerritoryName = contacts.CompanyTerritory.TerritoryName;
                string oldTerritoryCode = contacts.CompanyTerritory.TerritoryCode;

                string OldAddressName = contacts.Address.AddressName;

                string OldShippingAddressName = contacts.Address.AddressName;

               // CompanyTerritory NewTerrObj = target.CompanyTerritories.Where(c => c.TerritoryName == OldTerritoryName && c.TerritoryCode == oldTerritoryCode).FirstOrDefault();

                Address NewAddressObj = target.Addresses.Where(c => c.AddressName == OldAddressName).FirstOrDefault();

                Address NewShipingAdd = target.Addresses.Where(c => c.AddressName == OldShippingAddressName).FirstOrDefault();

                CompanyContact targetCompanyContact = companyContactRepository.Create();
                companyContactRepository.Add(targetCompanyContact);
                targetCompanyContact.CompanyId = target.CompanyId;
               // targetCompanyContact.TerritoryId = NewTerrObj != null ? NewTerrObj.TerritoryId : 0;
               
                
                if (NewAddressObj != null)
                {
                    targetCompanyContact.Address = NewAddressObj;
                    targetCompanyContact.AddressId = NewAddressObj.AddressId;
                }
                if (NewShipingAdd != null)
                {
                    targetCompanyContact.ShippingAddress = NewShipingAdd;
                    targetCompanyContact.ShippingAddressId = NewShipingAdd.AddressId;
                }
                targetCompanyContact.quickAddress3 = Convert.ToString(contacts.ContactId);
                target.CompanyContacts.Add(targetCompanyContact);
                contacts.Clone(targetCompanyContact);
            }
        }

        /// <summary>
        /// Copy campaigns
        /// </summary>
        private void CloneCampaigns(Company source, Company target)
        {
            if (source.Campaigns == null)
            {
                return;
            }

            // Initialize List
            if (target.Campaigns == null)
            {
                target.Campaigns = new List<Campaign>();
            }

            foreach (Campaign campaigns in source.Campaigns)
            {
                Campaign targetCampaigns = campaignRepository.Create();
                campaignRepository.Add(targetCampaigns);
                targetCampaigns.CompanyId = target.CompanyId;
                target.Campaigns.Add(targetCampaigns);
                campaigns.Clone(targetCampaigns);


                // Clone campaign images
                if (campaigns.CampaignImages == null)
                {
                    continue;
                }

                // Copy Campaign Images
                CloneCampaignImages(campaigns, targetCampaigns);
            }
        }

        /// <summary>
        /// Creates Copy of company  Banners
        /// </summary>
        public void CloneCampaignImages(Campaign campaigns, Campaign targetcampaigns)
        {
            if (targetcampaigns.CampaignImages == null)
            {
                targetcampaigns.CampaignImages = new List<CampaignImage>();
            }

            foreach (CampaignImage objcampaignImages in campaigns.CampaignImages.ToList())
            {
                CampaignImage targetCampaignImage = campaignImageRepository.Create();
                campaignImageRepository.Add(targetCampaignImage);
                targetCampaignImage.CampaignId = targetcampaigns.CampaignId;
                targetcampaigns.CampaignImages.Add(targetCampaignImage);
                targetCampaignImage.Clone(targetCampaignImage);
            }
        }

        
        /// <summary>
        /// Copy company cost centre
        private void CloneCompanyCostCentre(Company source, Company target)
        {
            if (source.CompanyCostCentres == null)
            {
                return;
            }

            // Initialize List
            if (target.CompanyCostCentres == null)
            {
                target.CompanyCostCentres = new List<CompanyCostCentre>();
            }

            foreach (CompanyCostCentre companyCostCentre in source.CompanyCostCentres)
            {
                CompanyCostCentre targetCompanyCostCentre = companyCostCenterRepository.Create();
                companyCostCenterRepository.Add(targetCompanyCostCentre);
                targetCompanyCostCentre.CompanyId = target.CompanyId;
                target.CompanyCostCentres.Add(targetCompanyCostCentre);
                companyCostCentre.Clone(targetCompanyCostCentre);
            }
        }


        /// <summary>
        /// Copy cmyk color
        /// 
        /// </summary>
        private void CloneCompanyCMYKColor(Company source, Company target)
        {
            if (source.CompanyCMYKColors == null)
            {
                return;
            }

            // Initialize List
            if (target.CompanyCMYKColors == null)
            {
                target.CompanyCMYKColors = new List<CompanyCMYKColor>();
            }

            foreach (CompanyCMYKColor companyCMYKColor in source.CompanyCMYKColors)
            {
                CompanyCMYKColor targetCompanyCMYKColor = companyCmykColorRepository.Create();
                companyCmykColorRepository.Add(targetCompanyCMYKColor);
                targetCompanyCMYKColor.CompanyId = target.CompanyId;
                target.CompanyCMYKColors.Add(targetCompanyCMYKColor);
                companyCMYKColor.Clone(targetCompanyCMYKColor);
            }
        }

          /// <summary>
        /// Copy smart form or its details
        /// 
        /// </summary>
        private void CloneSmartFom(Company source, Company target)
        {
            if (source.SmartForms == null)
            {
                return;
            }

            // Initialize List
            if (target.SmartForms == null)
            {
                target.SmartForms = new List<SmartForm>();
            }

            foreach (SmartForm companySmartForm in source.SmartForms)
            {
                SmartForm targetSmartForm = smartFormRepository.Create();
                smartFormRepository.Add(targetSmartForm);
                targetSmartForm.CompanyId = target.CompanyId;
                target.SmartForms.Add(targetSmartForm);
                companySmartForm.Clone(targetSmartForm);

                // Clone smart form details
                if (companySmartForm.SmartFormDetails == null)
                {
                    continue;
                }

                // Clone smart form details
                CloneSmartFormDetails(companySmartForm, targetSmartForm,target);
            }


         
        }

        /// <summary>
        /// Creates Copy of company  Banners
        /// </summary>
        public void CloneSmartFormDetails(SmartForm smartForm, SmartForm targetsmartForm,Company targetCompany)
        {
            if (targetsmartForm.SmartFormDetails == null)
            {
                targetsmartForm.SmartFormDetails = new List<SmartFormDetail>();
            }

            foreach (SmartFormDetail objsmartFormDetails in smartForm.SmartFormDetails.ToList())
            {
                SmartFormDetail targetsmartFormDetail = smartFormDetailRepository.Create();
                smartFormDetailRepository.Add(targetsmartFormDetail);
                targetsmartFormDetail.SmartFormId = targetsmartForm.SmartFormId;

                //string oldVariableName = objsmartFormDetails.FieldVariable != null ? objsmartFormDetails.FieldVariable.VariableName : "";

                //FieldVariable objNewFieldVariable = targetCompany.FieldVariables.Where(c => c.VariableName == oldVariableName).FirstOrDefault();
               
                //if(objNewFieldVariable != null)
                //{
                //    targetsmartFormDetail.FieldVariable = objNewFieldVariable;
                //    targetsmartFormDetail.VariableId = objNewFieldVariable != null ? objNewFieldVariable.VariableId : 0;

                //}
                

                targetsmartForm.SmartFormDetails.Add(targetsmartFormDetail);
                objsmartFormDetails.Clone(targetsmartFormDetail);
            }
        }

        /// <summary>
        /// Copy smart form or its details
        /// 
        /// </summary>
        private void CloneFieldVariables(Company source, Company target)
        {
            if (source.FieldVariables == null)
            {
                return;
            }

            // Initialize List
            if (target.FieldVariables == null)
            {
                target.FieldVariables = new List<FieldVariable>();
            }

            foreach (FieldVariable companyFielVariables in source.FieldVariables)
            {
                FieldVariable targetfieldVariables = fieldVariableRepository.Create();
                fieldVariableRepository.Add(targetfieldVariables);
                targetfieldVariables.CompanyId = target.CompanyId;
               
                target.FieldVariables.Add(targetfieldVariables);
                companyFielVariables.Clone(targetfieldVariables);

                // Clone variable options
                if (companyFielVariables.VariableOptions == null)
                {
                    continue;
                }

                // Clone smart form details
                CloneVariableOption(companyFielVariables, targetfieldVariables);




                // Clone scope variable
                if (companyFielVariables.ScopeVariables == null)
                {
                    continue;
                }

                // Clone scope variable
                CloneScopeVariables(companyFielVariables, targetfieldVariables);


                // Clone template Variable
                if (companyFielVariables.TemplateVariables == null)
                {
                    continue;
                }

                // Clone scope variable
                CloneTemplateVariables(companyFielVariables, targetfieldVariables);

                // Clone variable extension
                if (companyFielVariables.VariableExtensions == null)
                {
                    continue;
                }

                // Clone variable extension
                CloneVariableExtension(companyFielVariables, targetfieldVariables);



            }



        }


        public void CloneVariableOption(FieldVariable fieldVariables, FieldVariable targetfieldVariables)
        {
            if (targetfieldVariables.VariableOptions == null)
            {
                targetfieldVariables.VariableOptions = new List<VariableOption>();
            }

            foreach (VariableOption objvariableOptions in fieldVariables.VariableOptions.ToList())
            {
                VariableOption targetvariableOption = variableOptionRepository.Create();
                variableOptionRepository.Add(targetvariableOption);
                targetvariableOption.VariableId = targetfieldVariables.VariableId;
                targetfieldVariables.VariableOptions.Add(targetvariableOption);
                objvariableOptions.Clone(targetvariableOption);
            }
        }



        public void CloneScopeVariables(FieldVariable fieldVariables, FieldVariable targetfieldVariables)
        {
            if (targetfieldVariables.ScopeVariables == null)
            {
                targetfieldVariables.ScopeVariables = new List<ScopeVariable>();
            }

            foreach (ScopeVariable objScopeVariable in fieldVariables.ScopeVariables.ToList())
            {
                ScopeVariable targetScopeVariable = scopeVariableRepository.Create();
                scopeVariableRepository.Add(targetScopeVariable);
                targetScopeVariable.VariableId = targetfieldVariables.VariableId;
                targetfieldVariables.ScopeVariables.Add(targetScopeVariable);
                objScopeVariable.Clone(targetScopeVariable);
            }
        }

        public void CloneTemplateVariables(FieldVariable fieldVariables, FieldVariable targetfieldVariables)
        {
            if (targetfieldVariables.TemplateVariables == null)
            {
                targetfieldVariables.TemplateVariables = new List<MPC.Models.DomainModels.TemplateVariable>();
            }

            foreach (MPC.Models.DomainModels.TemplateVariable objtemplateVariable in fieldVariables.TemplateVariables.ToList())
            {
                MPC.Models.DomainModels.TemplateVariable targetTemplateVariable = templateVariableRepository.Create();
                templateVariableRepository.Add(targetTemplateVariable);
                targetTemplateVariable.VariableId = targetfieldVariables.VariableId;
                targetfieldVariables.TemplateVariables.Add(targetTemplateVariable);
                objtemplateVariable.Clone(targetTemplateVariable);
            }
        }

        public void CloneVariableExtension(FieldVariable fieldVariables, FieldVariable targetfieldVariables)
        {
            if (targetfieldVariables.VariableExtensions == null)
            {
                targetfieldVariables.VariableExtensions = new List<MPC.Models.DomainModels.VariableExtension>();
            }

            foreach (MPC.Models.DomainModels.VariableExtension objVariableExtension in fieldVariables.VariableExtensions.ToList())
            {
                MPC.Models.DomainModels.VariableExtension targetVariableExtension =  variableExtensionRespository.Create();
                variableExtensionRespository.Add(targetVariableExtension);
                targetVariableExtension.FieldVariableId = targetfieldVariables.VariableId;
                targetfieldVariables.VariableExtensions.Add(targetVariableExtension);
                objVariableExtension.Clone(targetVariableExtension);
            }
        }

        /// <summary>
        /// Copy color palletes
        /// </summary>
        private void CloneActivities(Company source, Company target)
        {
            if (source.Activities == null)
            {
                return;
            }
          
            // Initialize List
            if (target.Activities == null)
            {
                target.Activities = new List<Activity>();
            }

            foreach (Activity companyActivities in source.Activities)
            {

                Activity targetActivity = activityRepository.Create();
                activityRepository.Add(targetActivity);
                targetActivity.CompanyId = target.CompanyId;
                target.Activities.Add(targetActivity);
                companyActivities.Clone(targetActivity);



            }



        }

        /// <summary>
        /// Copy template color styles
        /// </summary>
        private void CloneTemplateColorStyles(Company source, Company target)
        {
            if (source.TemplateColorStyles == null)
            {
                return;
            }

            // Initialize List
            if (target.TemplateColorStyles == null)
            {
                target.TemplateColorStyles = new List<TemplateColorStyle>();
            }

            foreach (TemplateColorStyle templateColorStyles in source.TemplateColorStyles)
            {

                TemplateColorStyle targetColorStyle = templateColorStylesRepository.Create();
                templateColorStylesRepository.Add(targetColorStyle);
                targetColorStyle.CustomerId = target.CompanyId;
                target.TemplateColorStyles.Add(targetColorStyle);
                templateColorStyles.Clone(targetColorStyle);



            }



        }

        /// <summary>
        /// Copy cms offer
        /// </summary>
        private void CloneCMSOffer(Company source, Company target)
        {
            if (source.CmsOffers == null)
            {
                return;
            }

            // Initialize List
            if (target.CmsOffers == null)
            {
                target.CmsOffers = new List<CmsOffer>();
            }

            foreach (CmsOffer cmsOffer in source.CmsOffers)
            {
                CmsOffer targetCMSOffer = cmsofferRepository.Create();
                cmsofferRepository.Add(targetCMSOffer);
                targetCMSOffer.CompanyId = target.CompanyId;
                target.CmsOffers.Add(targetCMSOffer);
                cmsOffer.Clone(targetCMSOffer);

              

            }
        }
        /// <summary>
        /// Copy media library
        /// </summary>
        private void CloneMediaLibrary(Company source, Company target)
        {
            if (source.MediaLibraries == null)
            {
                return;
            }

            // Initialize List
            if (target.MediaLibraries == null)
            {
                target.MediaLibraries = new List<MediaLibrary>();
            }

            foreach (MediaLibrary mediaLibrary in source.MediaLibraries)
            {
                MediaLibrary targetMediaLibrary = mediaLibraryRepository.Create();
                mediaLibraryRepository.Add(targetMediaLibrary);
                targetMediaLibrary.CompanyId = target.CompanyId;
                target.MediaLibraries.Add(targetMediaLibrary);
                mediaLibrary.Clone(targetMediaLibrary);



            }
        }

        // clone discount vouchers
        public void CloneDiscountVouchers(long OldCompanyid,long NewCompanyId)
        {
            List<DiscountVoucher> discountVouchers = discountVoucherRepository.getDiscountVouchersByCompanyId(OldCompanyid);

            if(discountVouchers != null && discountVouchers.Count > 0)
            {
                foreach(var voucher in discountVouchers)
                {
                    DiscountVoucher targetDiscountVoucher = discountVoucherRepository.Create();
                    targetDiscountVoucher = voucher;
                    targetDiscountVoucher.CompanyId = NewCompanyId;
                    Guid g;
                    // Create and display the value of two GUIDs.
                    g = Guid.NewGuid();


                    targetDiscountVoucher.VoucherCode = g.ToString();
                    discountVoucherRepository.Add(targetDiscountVoucher);
                    
                    if(voucher.ProductCategoryVouchers != null && voucher.ProductCategoryVouchers.Count > 0)
                    {
                        foreach(var pcv in voucher.ProductCategoryVouchers)
                        {
                            ProductCategoryVoucher objPCV = productcategoryvoucherRepository.Create();
                            objPCV = pcv;
                            objPCV.DiscountVoucher = targetDiscountVoucher;
                            objPCV.VoucherId = targetDiscountVoucher.DiscountVoucherId;

                            productcategoryvoucherRepository.Add(objPCV);

                        }
                    }

                    if (voucher.ItemsVouchers != null && voucher.ItemsVouchers.Count > 0)
                    {
                        foreach (var iv in voucher.ItemsVouchers)
                        {
                            ItemsVoucher objIV = itemsVoucherRepository.Create();
                            objIV = iv;
                            objIV.DiscountVoucher = targetDiscountVoucher;
                            objIV.VoucherId = targetDiscountVoucher.DiscountVoucherId;

                            itemsVoucherRepository.Add(objIV);

                        }
                    }

                }
            }


        }

        // clone discount vouchers
        public void CloneTemplateFonts(long OldCompanyid, long NewCompanyId)
        {
            List<TemplateFont> fonts = templatefonts.getTemplateFontsByCompanyID(OldCompanyid);

            if (fonts != null && fonts.Count > 0)
            {
                foreach (var font in fonts)
                {
                    TemplateFont templateFont = templatefonts.Create();
                    templateFont = font;
                    templateFont.CustomerId = NewCompanyId;
                    templatefonts.Add(templateFont);
                }
            }


        }

        // clone report banners
        public void CloneReportBanners(long OldCompanyid, long NewCompanyId)
        {

            List<ReportNote> reportNotes = reportNoteRepository.GetReportNotesByCompanyId(OldCompanyid);

            if (reportNotes != null && reportNotes.Count > 0)
            {
                foreach (var note in reportNotes)
                {
                    ReportNote reportNote = reportNoteRepository.Create();
                    reportNote = note;
                    reportNote.CompanyId = NewCompanyId;
                    if(!string.IsNullOrEmpty(note.ReportBanner))
                    {
                      
                        // update reportbanner path
                        string name = Path.GetFileName(note.ReportBanner);
                        string BannerPath = "MPC_Content/Reports/Banners/" + companyRepository.OrganisationId + "/" + NewCompanyId + "/" + name;
                      


                        string DestinationBannerPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Reports/Banners/" + companyRepository.OrganisationId + "/" + NewCompanyId + "/" + name);

                        string DestinationBannerDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Reports/Banners/" + companyRepository.OrganisationId + "/" + NewCompanyId);
                        string BannerSourcePath = HttpContext.Current.Server.MapPath("~/" + note.ReportBanner);
                        if (!System.IO.Directory.Exists(DestinationBannerDirectory))
                        {
                            Directory.CreateDirectory(DestinationBannerDirectory);
                            if (Directory.Exists(DestinationBannerDirectory))
                            {
                                if (File.Exists(BannerSourcePath))
                                {
                                    if (!File.Exists(DestinationBannerPath))
                                        File.Copy(BannerSourcePath, DestinationBannerPath);
                                }


                            }

                        }
                        else
                        {
                            if (File.Exists(BannerSourcePath))
                            {
                                if (!File.Exists(DestinationBannerPath))
                                    File.Copy(BannerSourcePath, DestinationBannerPath);

                            }
                        }


                        reportNote.ReportBanner = BannerPath;
                    }


                    reportNoteRepository.Add(reportNote);

                    // 
                }
            }


        }

        public void SetValuesAfterClone(Company company,string OldSelectedSetName,long OldcompanyId)
        {
            // set active banner set id in company

            company.ActiveBannerSetId = company.CompanyBannerSets.Where(c => c.SetName == OldSelectedSetName).Select(c => c.CompanySetId).FirstOrDefault();

            // set pickupaddress in company
            if(company.PickupAddressId > 0)
            {
                long NewId = company.Addresses.Where(c => c.Tel2 == Convert.ToString(company.PickupAddressId)).Select(c => c.AddressId).FirstOrDefault();

                company.PickupAddressId = NewId;

               
            }
           
            // set ids of contact , address and store in scopevariables
          
            IEnumerable<CompanyTerritory> companyTerritory = companyTerritoryRepository.GetAllCompanyTerritories(OldcompanyId);
            if(company.FieldVariables != null && company.FieldVariables.Count > 0)
            {
                foreach(var fv in company.FieldVariables)
                {
                    if(fv.ScopeVariables != null && fv.ScopeVariables.Count > 0)
                    {
                        foreach(var sv in fv.ScopeVariables)
                        {
                            // store
                            if(sv.Scope == 1)
                            {
                                sv.Id = company.CompanyId;
                            }
                            // contact
                            if(sv.Scope == 2)
                            {
                                long NewId = company.CompanyContacts.Where(c => c.quickAddress3 == Convert.ToString(sv.Id)).Select(c => c.ContactId).FirstOrDefault();
                                sv.Id = NewId;
                            }
                            // addresses
                            if (sv.Scope == 3)
                            {
                                long NewId = company.Addresses.Where(c => c.Tel2 == Convert.ToString(sv.Id)).Select(c => c.AddressId).FirstOrDefault();
                                sv.Id = NewId;
                            }
                            // territory
                            if (sv.Scope == 4)
                            {
                                string TerritoryName = companyTerritory.Where(c => c.TerritoryId == sv.Id).Select(c => c.TerritoryName).FirstOrDefault();
                                if(!string.IsNullOrEmpty(TerritoryName))
                                {
                                    long nEWiD = company.CompanyTerritories.Where(c => c.TerritoryName == TerritoryName).Select(c => c.TerritoryId).FirstOrDefault();
                                    sv.Id = nEWiD;
                                }
                              
                            }
                        }
                    }
                    if(fv.VariableExtensions != null && fv.VariableExtensions.Count > 0)
                    {
                        foreach (var ve in fv.VariableExtensions)
                        {
                            ve.CompanyId = (int)company.CompanyId;
                        }
                    }
                }
            }
            // set itemid in cmsoffer
            if (company.CmsOffers != null && company.CmsOffers.Count > 0)
            {
                foreach (var offer in company.CmsOffers)
                {
                    offer.ItemId = company.Items.Where(c => c.Tax3 == offer.ItemId).Select(c => (int)c.ItemId).FirstOrDefault();
                }
            }

            // set parent category id in productcategories
            if (company.ProductCategories != null && company.ProductCategories.Count > 0)
            {
                foreach (var item in company.ProductCategories)
                {
                    if (item.ParentCategoryId > 0) // 11859
                    {


                        //  string scat = item.Description2;
                        var pCat = company.ProductCategories.Where(g => g.ContentType.Contains(item.ParentCategoryId.Value.ToString())).FirstOrDefault();
                        if (pCat != null)
                        {
                            item.ParentCategoryId = Convert.ToInt32(pCat.ProductCategoryId);
                          
                        }
                    }

                    //if (item.ProductCategoryItems != null && item.ProductCategoryItems.Count > 0)
                    //{
                    //    foreach (var pci in item.ProductCategoryItems)
                    //    {
                    //        if (company.Items != null && company.Items.Count > 0)
                    //        {
                    //            long PID = company.Items.Where(c => c.Tax3 == pci.ItemId).Select(x => x.ItemId).FirstOrDefault();
                    //            if (PID > 0)
                    //            {
                    //                pci.ItemId = PID;
                    //            }
                    //            else
                    //            {
                    //                // PID = stockitems.Select(s => s.StockItemId).FirstOrDefault();
                    //                pci.ItemId = null;


                    //            }
                    //        }

                    //    }
                    //}
                 

                  
                }

               

               
               

            }




            // copy templates in items
            
            if(company.Items != null && company.Items.Count > 0)
            {
                foreach(var item in company.Items)
                {
                    if (item.TemplateId.HasValue)
                    {
                        long templateId = templateService.CopyTemplate(item.TemplateId.Value, 0, string.Empty, item.OrganisationId.HasValue ?
                            item.OrganisationId.Value : itemRepository.OrganisationId);

                        item.TemplateId = templateId;
                    }

                   
                }
            }

          
        }
        
        public void CopyCompanyFiles(Company ObjCompany,long OldCompanyID)
        {
            List<string> DestinationsPath = new List<string>();
           
            // new CompanyId
            long oCID = ObjCompany.CompanyId;

           
            // company logo
            string CompanyPathOld = string.Empty;
            string CompanylogoPathNew = string.Empty;
            if (ObjCompany.Image != null)
            {
                CompanyPathOld = Path.GetFileName(ObjCompany.Image);

                CompanylogoPathNew = CompanyPathOld.Replace(OldCompanyID + "_", ObjCompany.CompanyId + "_");

                string DestinationCompanyLogoFilePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + ObjCompany.CompanyId + "/" + CompanylogoPathNew);
                DestinationsPath.Add(DestinationCompanyLogoFilePath);
                string DestinationCompanyLogoDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + ObjCompany.CompanyId);
                string CompanyLogoSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + OldCompanyID + "/" + CompanyPathOld);
                if (!System.IO.Directory.Exists(DestinationCompanyLogoDirectory))
                {
                    Directory.CreateDirectory(DestinationCompanyLogoDirectory);
                    if (Directory.Exists(DestinationCompanyLogoDirectory))
                    {
                        if (File.Exists(CompanyLogoSourcePath))
                        {
                            if (!File.Exists(DestinationCompanyLogoFilePath))
                                File.Copy(CompanyLogoSourcePath, DestinationCompanyLogoFilePath);
                        }


                    }


                }
                else
                {
                    if (File.Exists(CompanyLogoSourcePath))
                    {
                        if (!File.Exists(DestinationCompanyLogoFilePath))
                            File.Copy(CompanyLogoSourcePath, DestinationCompanyLogoFilePath);
                    }
                }
                ObjCompany.Image = "MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + ObjCompany.CompanyId + "/" + CompanylogoPathNew;
            }


            // copy store background image
            if (ObjCompany.StoreBackgroundImage != null)
            {
                CompanyPathOld = Path.GetFileName(ObjCompany.StoreBackgroundImage);

                CompanylogoPathNew = CompanyPathOld.Replace(OldCompanyID + "_", ObjCompany.CompanyId + "_");

                string DestinationCompanyBackgroundFilePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID + "/" + CompanylogoPathNew);
                DestinationsPath.Add(DestinationCompanyBackgroundFilePath);
                string DestinationCompanyBackgroundDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID);
                string CompanyLogoSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + OldCompanyID + "/" + CompanyPathOld);
                if (!System.IO.Directory.Exists(DestinationCompanyBackgroundDirectory))
                {
                    Directory.CreateDirectory(DestinationCompanyBackgroundDirectory);
                    if (Directory.Exists(DestinationCompanyBackgroundDirectory))
                    {
                        if (File.Exists(CompanyLogoSourcePath))
                        {
                            if (!File.Exists(DestinationCompanyBackgroundFilePath))
                                File.Copy(CompanyLogoSourcePath, DestinationCompanyBackgroundFilePath);
                        }


                    }


                }
                else
                {
                    if (File.Exists(CompanyLogoSourcePath))
                    {
                        if (!File.Exists(DestinationCompanyBackgroundFilePath))
                            File.Copy(CompanyLogoSourcePath, DestinationCompanyBackgroundFilePath);
                    }
                }
                ObjCompany.StoreBackgroundImage = "MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID + "/" + CompanylogoPathNew;
            }

            // copy company contacts image
            if (ObjCompany.CompanyContacts != null && ObjCompany.CompanyContacts.Count > 0)
            {
                foreach (var contact in ObjCompany.CompanyContacts)
                {
                    string OldContactImage = string.Empty;
                    string NewContactImage = string.Empty;
                    string OldContactID = string.Empty;
                    if (contact.image != null)
                    {
                        string name = Path.GetFileName(contact.image);
                        string[] SplitMain = name.Split('_');
                        if (SplitMain[0] != string.Empty)
                        {
                            OldContactID = SplitMain[0];

                        }

                        OldContactImage = Path.GetFileName(contact.image);
                        NewContactImage = OldContactImage.Replace(OldContactID + "_", contact.ContactId + "_");

                        string DestinationContactFilesPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID + "/Contacts/" + "/" + NewContactImage);
                        DestinationsPath.Add(DestinationContactFilesPath);
                        string DestinationContactFilesDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID + "/Contacts");
                        string ContactFilesSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + OldCompanyID + "/Contacts/" + OldContactImage);
                        if (!System.IO.Directory.Exists(DestinationContactFilesDirectory))
                        {
                            Directory.CreateDirectory(DestinationContactFilesDirectory);
                            if (Directory.Exists(DestinationContactFilesDirectory))
                            {
                                if (File.Exists(ContactFilesSourcePath))
                                {
                                    if (!File.Exists(DestinationContactFilesPath))
                                        File.Copy(ContactFilesSourcePath, DestinationContactFilesPath);
                                }


                            }



                        }
                        else
                        {
                            if (File.Exists(ContactFilesSourcePath))
                            {
                                if (!File.Exists(DestinationContactFilesPath))
                                    File.Copy(ContactFilesSourcePath, DestinationContactFilesPath);
                            }

                        }
                        contact.image = "/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID + "/Contacts/" + NewContactImage;
                    }
                }
            }
            Dictionary<string, string> dictionaryMediaIds = new Dictionary<string, string>();
            // copy Media libraries
            if (ObjCompany.MediaLibraries != null && ObjCompany.MediaLibraries.Count > 0)
            {
                foreach (var media in ObjCompany.MediaLibraries)
                {
                    string OldMediaFilePath = string.Empty;
                    string NewMediaFilePath = string.Empty;
                    string OldMediaID = string.Empty;
                    string NewMediaID = string.Empty;
                    if (media.FilePath != null)
                    {
                        string name = Path.GetFileName(media.FilePath);
                        string[] SplitMain = name.Split('_');
                        if (SplitMain[0] != string.Empty)
                        {
                            OldMediaID = SplitMain[0];

                        }



                        if (media.MediaId > 0)
                            NewMediaID = Convert.ToString(media.MediaId);

                        dictionaryMediaIds.Add(OldMediaID, NewMediaID);

                        // DestinationsPath.Add(OldMediaID, NewMediaID);

                        OldMediaFilePath = Path.GetFileName(media.FilePath);
                        NewMediaFilePath = OldMediaFilePath.Replace(OldMediaID + "_", media.MediaId + "_");

                        string DestinationMediaFilesPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Media/" + companyRepository.OrganisationId + "/" + oCID + "/" + NewMediaFilePath);
                        DestinationsPath.Add(DestinationMediaFilesPath);
                        string DestinationMediaFilesDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Media/" + companyRepository.OrganisationId + "/" + oCID);
                        string MediaFilesSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Media/" + companyRepository.OrganisationId + "/" + OldCompanyID + "/" + OldMediaFilePath);
                        if (!System.IO.Directory.Exists(DestinationMediaFilesDirectory))
                        {
                            Directory.CreateDirectory(DestinationMediaFilesDirectory);
                            if (Directory.Exists(DestinationMediaFilesDirectory))
                            {
                                if (File.Exists(MediaFilesSourcePath))
                                {
                                    if (!File.Exists(DestinationMediaFilesPath))
                                        File.Copy(MediaFilesSourcePath, DestinationMediaFilesPath);
                                }


                            }



                        }
                        else
                        {
                            if (File.Exists(MediaFilesSourcePath))
                            {
                                if (!File.Exists(DestinationMediaFilesPath))
                                    File.Copy(MediaFilesSourcePath, DestinationMediaFilesPath);
                            }

                        }
                        media.FilePath = "MPC_Content/Media/" + companyRepository.OrganisationId + "/" + oCID + "/" + NewMediaFilePath;
                    }

                }
            }

            // copy compay banner and banner set
            if (ObjCompany.CompanyBannerSets != null && ObjCompany.CompanyBannerSets.Count > 0)
            {
                foreach (var sets in ObjCompany.CompanyBannerSets)
                {


                    if (sets.CompanyBanners != null && sets.CompanyBanners.Count > 0)
                    {
                        foreach (var bann in sets.CompanyBanners)
                        {
                            if (!string.IsNullOrEmpty(bann.ImageURL))
                            {
                                string OldMediaID = string.Empty;
                                string newMediaID = string.Empty;
                                string name = Path.GetFileName(bann.ImageURL);
                                string[] SplitMain = name.Split('_');
                                
                                if (SplitMain != null)
                                {
                                    if (SplitMain[0] != string.Empty)
                                    {
                                        OldMediaID = SplitMain[0];

                                    }
                                }
                             
                                if (dictionaryMediaIds != null && dictionaryMediaIds.Count > 0)
                                {
                                    var dec = dictionaryMediaIds.Where(s => s.Key == OldMediaID).Select(s => s.Value).FirstOrDefault();
                                    if (dec != null)
                                    {
                                        newMediaID = dec.ToString();
                                    }
                                }


                                string NewBannerPath = name.Replace(OldMediaID + "_", newMediaID + "_");

                                bann.ImageURL = "/MPC_Content/Media/" + companyRepository.OrganisationId + "/" + oCID + "/" + NewBannerPath;
                            }
                        }
                    }

                }
            }
            if (ObjCompany.CmsPages != null && ObjCompany.CmsPages.Count > 0)
            {
                foreach (var pages in ObjCompany.CmsPages)
                {
                    if (!string.IsNullOrEmpty(pages.PageBanner))
                    {
                        string OldMediaID = string.Empty;
                        string newMediaID = string.Empty;
                        string name = Path.GetFileName(pages.PageBanner);

                        string[] SplitMain = name.Split('_');

                        if (SplitMain != null)
                        {
                            if (SplitMain[0] != string.Empty)
                            {
                                OldMediaID = SplitMain[0];

                            }
                        }

                        if (dictionaryMediaIds != null && dictionaryMediaIds.Count > 0)
                        {
                            var dec = dictionaryMediaIds.Where(s => s.Key == OldMediaID).Select(s => s.Value).FirstOrDefault();
                            if (dec != null)
                            {
                                newMediaID = dec.ToString();
                            }
                        }


                        string newCMSPageName = name.Replace(OldMediaID + "_", newMediaID + "_");


                        pages.PageBanner = "/MPC_Content/Media/" + companyRepository.OrganisationId + "/" + oCID + "/" + newCMSPageName;
                    }

                }
            }
            if (ObjCompany.ProductCategories != null && ObjCompany.ProductCategories.Count > 0)
            {
                foreach (var prodCat in ObjCompany.ProductCategories)
                {
                    string ProdCatID = string.Empty;
                    string CatName = string.Empty;

                    if (!string.IsNullOrEmpty(prodCat.ThumbnailPath))
                    {
                        string OldThumbnailPath = string.Empty;
                        string NewThumbnailPath = string.Empty;

                        string name = Path.GetFileName(prodCat.ThumbnailPath);
                        string[] SplitMain = name.Split('_');
                        if (SplitMain[1] != string.Empty)
                        {
                            ProdCatID = SplitMain[1];

                        }

                        OldThumbnailPath = Path.GetFileName(prodCat.ThumbnailPath);
                        NewThumbnailPath = OldThumbnailPath.Replace(ProdCatID + "_", prodCat.ProductCategoryId + "_");



                        string DestinationThumbPathCat = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID + "/ProductCategories/" + NewThumbnailPath);
                        DestinationsPath.Add(DestinationThumbPathCat);
                        string DestinationThumbDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID + "/ProductCategories");
                        string ThumbSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + OldCompanyID + "/ProductCategories/" + OldThumbnailPath);
                        if (!System.IO.Directory.Exists(DestinationThumbDirectory))
                        {
                            Directory.CreateDirectory(DestinationThumbDirectory);
                            if (Directory.Exists(DestinationThumbDirectory))
                            {
                                if (File.Exists(ThumbSourcePath))
                                {
                                    if (!File.Exists(DestinationThumbPathCat))
                                        File.Copy(ThumbSourcePath, DestinationThumbPathCat);
                                }


                            }

                        }
                        else
                        {
                            if (File.Exists(ThumbSourcePath))
                            {
                                if (!File.Exists(DestinationThumbPathCat))
                                    File.Copy(ThumbSourcePath, DestinationThumbPathCat);
                            }

                        }
                        prodCat.ThumbnailPath = "MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID + "/ProductCategories/" + NewThumbnailPath;
                    }

                    if (!string.IsNullOrEmpty(prodCat.ImagePath))
                    {
                        string OldImagePath = string.Empty;
                        string NewImagePath = string.Empty;

                        string name = Path.GetFileName(prodCat.ImagePath);
                        string[] SplitMain = name.Split('_');
                        if (SplitMain[1] != string.Empty)
                        {
                            ProdCatID = SplitMain[1];

                        }

                        OldImagePath = Path.GetFileName(prodCat.ImagePath);
                        NewImagePath = OldImagePath.Replace(ProdCatID + "_", prodCat.ProductCategoryId + "_");

                        string DestinationImagePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID + "/ProductCategories/" + NewImagePath);
                        DestinationsPath.Add(DestinationImagePath);
                        string DestinationImageDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID + "/ProductCategories");
                        string ImageSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + OldCompanyID + "/ProductCategories/" + OldImagePath);

                        if (!System.IO.Directory.Exists(DestinationImageDirectory))
                        {
                            Directory.CreateDirectory(DestinationImageDirectory);
                            if (Directory.Exists(DestinationImageDirectory))
                            {
                                if (File.Exists(ImageSourcePath))
                                {
                                    if (!File.Exists(DestinationImagePath))
                                        File.Copy(ImageSourcePath, DestinationImagePath);
                                }


                            }

                        }
                        else
                        {
                            if (File.Exists(ImageSourcePath))
                            {
                                if (!File.Exists(DestinationImagePath))
                                    File.Copy(ImageSourcePath, DestinationImagePath);
                            }

                        }
                        prodCat.ImagePath = "MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID + "/ProductCategories/" + NewImagePath;
                    }


                }
            }
            
            // copy item images

            if (ObjCompany.Items != null && ObjCompany.Items.Count > 0)
            {
                string ItemID = string.Empty;
                string ItemName = string.Empty;
                foreach (var item in ObjCompany.Items)
                {
                    // thumbnail images
                    if (!string.IsNullOrEmpty(item.ThumbnailPath))
                    {
                        string OldThumbnailPath = string.Empty;
                        string NewThumbnailPath = string.Empty;

                        string name = Path.GetFileName(item.ThumbnailPath);
                        string[] SplitMain = name.Split('_');
                        if (SplitMain != null)
                        {
                            if (SplitMain[1] != string.Empty)
                            {
                                ItemID = SplitMain[1];

                            }
                            int i = 0;
                            // string s = "108";
                            bool result = int.TryParse(ItemID, out i);
                            if (!result)
                            {
                                ItemID = SplitMain[0];
                            }
                        }
                        OldThumbnailPath = Path.GetFileName(item.ThumbnailPath);
                        NewThumbnailPath = OldThumbnailPath.Replace(ItemID + "_", item.ItemId + "_");


                        string DestinationThumbnailPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewThumbnailPath);
                        DestinationsPath.Add(DestinationThumbnailPath);
                        string DestinationThumbnailDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId);
                        string ThumbnailSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + ItemID + "/" + OldThumbnailPath);
                        if (!System.IO.Directory.Exists(DestinationThumbnailDirectory))
                        {
                            Directory.CreateDirectory(DestinationThumbnailDirectory);
                            if (Directory.Exists(DestinationThumbnailDirectory))
                            {
                                if (File.Exists(ThumbnailSourcePath))
                                {
                                    if (!File.Exists(DestinationThumbnailPath))
                                        File.Copy(ThumbnailSourcePath, DestinationThumbnailPath);
                                }


                            }

                        }
                        else
                        {
                            if (File.Exists(ThumbnailSourcePath))
                            {
                                if (!File.Exists(DestinationThumbnailPath))
                                    File.Copy(ThumbnailSourcePath, DestinationThumbnailPath);
                            }

                        }
                        item.ThumbnailPath = "MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewThumbnailPath;
                    }

                    // main image
                    if (!string.IsNullOrEmpty(item.ImagePath))
                    {

                        string OldImagePath = string.Empty;
                        string NewImagePath = string.Empty;


                        string name = Path.GetFileName(item.ImagePath);
                        string[] SplitMain = name.Split('_');
                        if (SplitMain != null)
                        {
                            if (SplitMain[1] != string.Empty)
                            {
                                ItemID = SplitMain[1];

                            }
                            int i = 0;
                            // string s = "108";
                            bool result = int.TryParse(ItemID, out i);
                            if (!result)
                            {
                                ItemID = SplitMain[0];
                            }
                        }

                        OldImagePath = Path.GetFileName(item.ImagePath);
                        NewImagePath = OldImagePath.Replace(ItemID + "_", item.ItemId + "_");


                        string DestinationImagePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewImagePath);
                        DestinationsPath.Add(DestinationImagePath);
                        string DestinationImageDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId);
                        string ImageSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + ItemID + "/" + OldImagePath);
                        if (!System.IO.Directory.Exists(DestinationImageDirectory))
                        {
                            Directory.CreateDirectory(DestinationImageDirectory);
                            if (Directory.Exists(DestinationImageDirectory))
                            {
                                if (File.Exists(ImageSourcePath))
                                {
                                    if (!File.Exists(DestinationImagePath))
                                        File.Copy(ImageSourcePath, DestinationImagePath);
                                }


                            }

                        }
                        else
                        {
                            if (File.Exists(ImageSourcePath))
                            {
                                if (!File.Exists(DestinationImagePath))
                                    File.Copy(ImageSourcePath, DestinationImagePath);
                            }

                        }
                        item.ImagePath = "MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewImagePath;
                    }

                    // Gird image
                    if (!string.IsNullOrEmpty(item.GridImage))
                    {
                        string OldGridPath = string.Empty;
                        string NewGridPath = string.Empty;

                        string name = Path.GetFileName(item.GridImage);
                        string[] SplitMain = name.Split('_');
                        if (SplitMain[0] != string.Empty)
                        {
                            ItemID = SplitMain[0];

                        }
                        int i = 0;
                        // string s = "108";
                        bool result = int.TryParse(ItemID, out i);
                        if (!result)
                        {
                            ItemID = SplitMain[1];
                        }

                        OldGridPath = Path.GetFileName(item.GridImage);
                        NewGridPath = OldGridPath.Replace(ItemID + "_", item.ItemId + "_");

                        string DestinationGridPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewGridPath);
                        DestinationsPath.Add(DestinationGridPath);
                        string DestinationGridDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId);
                        string GridSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + ItemID + "/" + OldGridPath);
                        if (!System.IO.Directory.Exists(DestinationGridDirectory))
                        {
                            Directory.CreateDirectory(DestinationGridDirectory);
                            if (Directory.Exists(DestinationGridDirectory))
                            {
                                if (File.Exists(GridSourcePath))
                                {
                                    if (!File.Exists(DestinationGridPath))
                                        File.Copy(GridSourcePath, DestinationGridPath);
                                }


                            }

                        }
                        else
                        {
                            if (File.Exists(GridSourcePath))
                            {
                                if (!File.Exists(DestinationGridPath))
                                    File.Copy(GridSourcePath, DestinationGridPath);

                            }
                        }
                        item.GridImage = "MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewGridPath;
                    }

                    // file 1
                    if (!string.IsNullOrEmpty(item.File1))
                    {
                        string OldF1Path = string.Empty;
                        string NewF1Path = string.Empty;

                        string name = Path.GetFileName(item.File1);
                        string[] SplitMain = name.Split('_');
                        if (SplitMain[0] != string.Empty)
                        {
                            ItemID = SplitMain[0];

                        }

                        OldF1Path = Path.GetFileName(item.File1);
                        NewF1Path = OldF1Path.Replace(ItemID + "_", item.ItemId + "_");

                         string DestinationFile1Path = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewF1Path);
                        DestinationsPath.Add(DestinationFile1Path);
                        string DestinationFile1Directory = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId);
                        string File1SourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + ItemID + "/" + OldF1Path);
                        if (!System.IO.Directory.Exists(DestinationFile1Directory))
                        {
                            Directory.CreateDirectory(DestinationFile1Directory);
                            if (Directory.Exists(DestinationFile1Directory))
                            {
                                if (File.Exists(File1SourcePath))
                                {
                                    if (!File.Exists(DestinationFile1Path))
                                        File.Copy(File1SourcePath, DestinationFile1Path);
                                }


                            }

                        }
                        else
                        {
                            if (File.Exists(File1SourcePath))
                            {
                                if (!File.Exists(DestinationFile1Path))
                                    File.Copy(File1SourcePath, DestinationFile1Path);
                            }

                        }
                        item.File1 = "MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewF1Path;

                    }

                    // file 2
                    if (!string.IsNullOrEmpty(item.File2))
                    {
                        string OldF2Path = string.Empty;
                        string NewF2Path = string.Empty;

                        string name = Path.GetFileName(item.File2);

                        string[] SplitMain = name.Split('_');
                        if (SplitMain[0] != string.Empty)
                        {
                            ItemID = SplitMain[0];

                        }

                        OldF2Path = Path.GetFileName(item.File2);
                        NewF2Path = OldF2Path.Replace(ItemID + "_", item.ItemId + "_");

                        string DestinationFile2Path = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewF2Path);
                        DestinationsPath.Add(DestinationFile2Path);
                        string DestinationFile2Directory = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId);
                        string File2SourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + ItemID + "/" + OldF2Path);
                        if (!System.IO.Directory.Exists(DestinationFile2Directory))
                        {
                            Directory.CreateDirectory(DestinationFile2Directory);
                            if (Directory.Exists(DestinationFile2Directory))
                            {
                                if (File.Exists(File2SourcePath))
                                {
                                    if (!File.Exists(DestinationFile2Path))
                                        File.Copy(File2SourcePath, DestinationFile2Path);
                                }


                            }

                        }
                        else
                        {
                            if (File.Exists(File2SourcePath))
                            {
                                if (!File.Exists(DestinationFile2Path))
                                    File.Copy(File2SourcePath, DestinationFile2Path);
                            }

                        }
                        item.File2 = "MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewF2Path;
                    }

                    // file 3
                    if (!string.IsNullOrEmpty(item.File3))
                    {
                        string OldF3Path = string.Empty;
                        string NewF3Path = string.Empty;

                        string name = Path.GetFileName(item.File3);
                        string[] SplitMain = name.Split('_');
                        if (SplitMain[0] != string.Empty)
                        {
                            ItemID = SplitMain[0];

                        }

                        OldF3Path = Path.GetFileName(item.File3);
                        NewF3Path = OldF3Path.Replace(ItemID + "_", item.ItemId + "_");

                        string DestinationFil3Path = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewF3Path);
                        DestinationsPath.Add(DestinationFil3Path);
                        string DestinationFile3Directory = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId);
                        string File3SourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + ItemID + "/" + OldF3Path);
                        if (!System.IO.Directory.Exists(DestinationFile3Directory))
                        {
                            Directory.CreateDirectory(DestinationFile3Directory);
                            if (Directory.Exists(DestinationFile3Directory))
                            {
                                if (File.Exists(File3SourcePath))
                                {
                                    if (!File.Exists(DestinationFil3Path))
                                        File.Copy(File3SourcePath, DestinationFil3Path);
                                }


                            }

                        }
                        else
                        {
                            if (File.Exists(File3SourcePath))
                            {
                                if (!File.Exists(DestinationFil3Path))
                                    File.Copy(File3SourcePath, DestinationFil3Path);
                            }

                        }
                        item.File3 = "MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewF3Path;
                    }

                    // file 4
                    if (!string.IsNullOrEmpty(item.File4))
                    {
                        string OldF4Path = string.Empty;
                        string NewF4Path = string.Empty;

                        string name = Path.GetFileName(item.File4);
                        string[] SplitMain = name.Split('_');
                        if (SplitMain[0] != string.Empty)
                        {
                            ItemID = SplitMain[0];

                        }

                        OldF4Path = Path.GetFileName(item.File4);
                        NewF4Path = OldF4Path.Replace(ItemID + "_", item.ItemId + "_");

                        string DestinationFile4Path = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewF4Path);
                        DestinationsPath.Add(DestinationFile4Path);
                        string DestinationFile4Directory = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId);
                        string File4SourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + ItemID + "/" + OldF4Path);
                        if (!System.IO.Directory.Exists(DestinationFile4Directory))
                        {
                            Directory.CreateDirectory(DestinationFile4Directory);
                            if (Directory.Exists(DestinationFile4Directory))
                            {
                                if (File.Exists(File4SourcePath))
                                {
                                    if (!File.Exists(DestinationFile4Path))
                                        File.Copy(File4SourcePath, DestinationFile4Path);
                                }


                            }

                        }
                        else
                        {
                            if (File.Exists(File4SourcePath))
                            {
                                if (!File.Exists(DestinationFile4Path))
                                    File.Copy(File4SourcePath, DestinationFile4Path);
                            }

                        }
                        item.File4 = "MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewF4Path;
                    }

                    // file 5
                    if (!string.IsNullOrEmpty(item.File5))
                    {
                        string OldF5Path = string.Empty;
                        string NewF5Path = string.Empty;

                        string name = Path.GetFileName(item.File5);
                        string[] SplitMain = name.Split('_');
                        if (SplitMain[0] != string.Empty)
                        {
                            ItemID = SplitMain[0];

                        }

                        OldF5Path = Path.GetFileName(item.File5);
                        NewF5Path = OldF5Path.Replace(ItemID + "_", item.ItemId + "_");

                        string DestinationFile5Path = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewF5Path);
                        DestinationsPath.Add(DestinationFile5Path);
                        string DestinationFile5Directory = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId);
                        string File5SourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + ItemID + "/" + OldF5Path);
                        if (!System.IO.Directory.Exists(DestinationFile5Directory))
                        {
                            Directory.CreateDirectory(DestinationFile5Directory);
                            if (Directory.Exists(DestinationFile5Directory))
                            {
                                if (File.Exists(File5SourcePath))
                                {
                                    if (!File.Exists(DestinationFile5Path))
                                        File.Copy(File5SourcePath, DestinationFile5Path);
                                }


                            }

                        }
                        else
                        {
                            if (File.Exists(File5SourcePath))
                            {
                                if (!File.Exists(DestinationFile5Path))
                                    File.Copy(File5SourcePath, DestinationFile5Path);
                            }

                        }
                        item.File5 = "MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewF5Path;
                    }
                    if (item.ItemImages != null && item.ItemImages.Count > 0)
                    {
                        foreach (var img in item.ItemImages)
                        {
                            if (!string.IsNullOrEmpty(img.ImageURL))
                            {
                                string OldImagePath = string.Empty;
                                string NewImagePath = string.Empty;

                                string name = Path.GetFileName(img.ImageURL);

                                string DestinationItemImagePath = HttpContext.Current.Server.MapPath("/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + name);
                                DestinationsPath.Add(DestinationItemImagePath);
                                string DestinationItemImageDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId);
                                string ItemImageSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + ItemID + "/" + name);
                                if (!System.IO.Directory.Exists(DestinationItemImageDirectory))
                                {
                                    Directory.CreateDirectory(DestinationItemImageDirectory);
                                    if (Directory.Exists(DestinationItemImageDirectory))
                                    {
                                        if (File.Exists(ItemImageSourcePath))
                                        {
                                            if (!File.Exists(DestinationItemImagePath))
                                                File.Copy(ItemImageSourcePath, DestinationItemImagePath);
                                        }


                                    }

                                }
                                else
                                {
                                    if (File.Exists(ItemImageSourcePath))
                                    {
                                        if (!File.Exists(DestinationItemImagePath))
                                            File.Copy(ItemImageSourcePath, DestinationItemImagePath);
                                    }

                                }
                                img.ImageURL = "MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + name;
                                // item.ThumbnailPath = "MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId + "/" + NewThumbnailPath;
                            }
                        }
                    }
                    //if (item.TemplateId != null && item.TemplateId > 0)
                    //{
                    //    if (item.DesignerCategoryId == 0 || item.DesignerCategoryId == null)
                    //    {
                    //        if (item.Template != null)
                    //        {

                    //            // template background images
                    //            if (item.Template.TemplateBackgroundImages != null && item.Template.TemplateBackgroundImages.Count > 0)
                    //            {
                    //                foreach (var tempImg in item.Template.TemplateBackgroundImages)
                    //                {
                    //                    if (!string.IsNullOrEmpty(tempImg.ImageName))
                    //                    {
                    //                        if (tempImg.ImageName.Contains("UserImgs/"))
                    //                        {
                    //                            string name = tempImg.ImageName;

                    //                            string ImageName = Path.GetFileName(tempImg.ImageName);

                    //                            string NewPath = "UserImgs/" + oCID + "/" + ImageName;

                    //                            string[] tempID = tempImg.ImageName.Split('/');

                    //                            string OldTempID = tempID[1];

                    //                            string DestinationTempBackGroundImages = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + NewOrgID + "/Templates/" + NewPath);
                    //                            DestinationsPath.Add(DestinationTempBackGroundImages);
                    //                            string DestinationTempBackgroundDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + NewOrgID + "/Templates/UserImgs/" + oCID);
                    //                            string FileBackGroundSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Designer/Organisation" + oldOrgID + "/Templates/UserImgs/" + OldCompanyID + "/" + ImageName);
                    //                            if (!System.IO.Directory.Exists(DestinationTempBackgroundDirectory))
                    //                            {
                    //                                Directory.CreateDirectory(DestinationTempBackgroundDirectory);
                    //                                if (Directory.Exists(DestinationTempBackgroundDirectory))
                    //                                {
                    //                                    if (File.Exists(FileBackGroundSourcePath))
                    //                                    {
                    //                                        if (!File.Exists(DestinationTempBackGroundImages))
                    //                                            File.Copy(FileBackGroundSourcePath, DestinationTempBackGroundImages);
                    //                                    }


                    //                                }
                    //                            }
                    //                            else
                    //                            {
                    //                                if (File.Exists(FileBackGroundSourcePath))
                    //                                {
                    //                                    if (!File.Exists(DestinationTempBackGroundImages))
                    //                                        File.Copy(FileBackGroundSourcePath, DestinationTempBackGroundImages);
                    //                                }

                    //                            }
                    //                            tempImg.ImageName = NewPath;
                    //                        }
                    //                        else
                    //                        {
                    //                            string name = tempImg.ImageName;

                    //                            string ImageName = Path.GetFileName(tempImg.ImageName);

                    //                            string NewPath = tempImg.ProductId + "/" + ImageName;

                    //                            string[] tempID = tempImg.ImageName.Split('/');

                    //                            string OldTempID = tempID[0];


                    //                            string DestinationTempBackGroundImages = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + NewOrgID + "/Templates/" + NewPath);
                    //                            DestinationsPath.Add(DestinationTempBackGroundImages);
                    //                            string DestinationTempBackgroundDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + NewOrgID + "/Templates/" + tempImg.ProductId);
                    //                            string FileBackGroundSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Designer/Organisation" + oldOrgID + "/Templates/" + OldTempID + "/" + ImageName);
                    //                            if (!System.IO.Directory.Exists(DestinationTempBackgroundDirectory))
                    //                            {
                    //                                Directory.CreateDirectory(DestinationTempBackgroundDirectory);
                    //                                if (Directory.Exists(DestinationTempBackgroundDirectory))
                    //                                {
                    //                                    if (File.Exists(FileBackGroundSourcePath))
                    //                                    {
                    //                                        if (!File.Exists(DestinationTempBackGroundImages))
                    //                                            File.Copy(FileBackGroundSourcePath, DestinationTempBackGroundImages);
                    //                                    }


                    //                                }
                    //                            }
                    //                            else
                    //                            {
                    //                                if (File.Exists(FileBackGroundSourcePath))
                    //                                {
                    //                                    if (!File.Exists(DestinationTempBackGroundImages))
                    //                                        File.Copy(FileBackGroundSourcePath, DestinationTempBackGroundImages);
                    //                                }

                    //                            }
                    //                            tempImg.ImageName = NewPath;
                    //                        }



                    //                    }

                    //                }
                    //            }
                    //            if (item.Template.TemplatePages != null && item.Template.TemplatePages.Count > 0)
                    //            {
                    //                foreach (var tempPage in item.Template.TemplatePages)
                    //                {
                    //                    if (!string.IsNullOrEmpty(tempPage.BackgroundFileName))
                    //                    {
                    //                        string name = tempPage.BackgroundFileName;

                    //                        string FileName = Path.GetFileName(tempPage.BackgroundFileName);

                    //                        string NewPath = tempPage.ProductId + "/" + FileName;

                    //                        string[] tempID = tempPage.BackgroundFileName.Split('/');

                    //                        string OldTempID = tempID[0];


                    //                        string DestinationTempBackGroundImages = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + NewOrgID + "/Templates/" + NewPath);
                    //                        DestinationsPath.Add(DestinationTempBackGroundImages);
                    //                        string DestinationTempBackgroundDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + NewOrgID + "/Templates/" + tempPage.ProductId);
                    //                        string FileBackGroundSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Designer/Organisation" + oldOrgID + "/Templates/" + OldTempID + "/" + FileName);
                    //                        if (!System.IO.Directory.Exists(DestinationTempBackgroundDirectory))
                    //                        {
                    //                            Directory.CreateDirectory(DestinationTempBackgroundDirectory);
                    //                            if (Directory.Exists(DestinationTempBackgroundDirectory))
                    //                            {
                    //                                if (File.Exists(FileBackGroundSourcePath))
                    //                                {
                    //                                    if (!File.Exists(DestinationTempBackGroundImages))
                    //                                        File.Copy(FileBackGroundSourcePath, DestinationTempBackGroundImages);
                    //                                }


                    //                            }

                    //                        }
                    //                        else
                    //                        {
                    //                            if (File.Exists(FileBackGroundSourcePath))
                    //                            {
                    //                                if (!File.Exists(DestinationTempBackGroundImages))
                    //                                    File.Copy(FileBackGroundSourcePath, DestinationTempBackGroundImages);
                    //                            }

                    //                        }
                    //                        tempPage.BackgroundFileName = NewPath;
                    //                    }
                    //                    string fileName = "templatImgBk" + tempPage.PageNo + ".jpg";
                    //                    string sPath = "/MPC_Content/Designer/Organisation" + NewOrgID + "/Templates/" + tempPage.ProductId + "/" + fileName;
                    //                    string FilePaths = HttpContext.Current.Server.MapPath("~/" + sPath);


                    //                    string DestinationDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + NewOrgID + "/Templates/" + tempPage.ProductId);
                    //                    string SourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Designer/Organisation" + NewOrgID + "/Templates/" + tempPage.ProductId + "/" + fileName);
                    //                    string DestinationPath = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + NewOrgID + "/Templates/" + tempPage.ProductId + "/" + fileName);
                    //                    if (!System.IO.Directory.Exists(DestinationDirectory))
                    //                    {
                    //                        Directory.CreateDirectory(DestinationDirectory);
                    //                        if (Directory.Exists(DestinationDirectory))
                    //                        {
                    //                            if (File.Exists(SourcePath))
                    //                            {
                    //                                if (!File.Exists(DestinationPath))
                    //                                    File.Copy(SourcePath, DestinationPath);
                    //                            }


                    //                        }

                    //                    }
                    //                    else
                    //                    {
                    //                        if (File.Exists(SourcePath))
                    //                        {
                    //                            if (!File.Exists(DestinationPath))
                    //                                File.Copy(SourcePath, DestinationPath);
                    //                        }

                    //                    }


                    //                }
                    //            }



                    //        }

                    //    }

                    //}

                }
            }

            List<TemplateFont> otemplatefonts = templatefonts.getTemplateFontsByCompanyID(ObjCompany.CompanyId);
            if (otemplatefonts != null && otemplatefonts.Count > 0)
            {
                foreach (var fonts in otemplatefonts)
                {
                    string DestinationFontDirectory = string.Empty;
                    string companyoid = string.Empty;
                    string FontSourcePath = string.Empty;
                    string FontSourcePath1 = string.Empty;
                    string FontSourcePath2 = string.Empty;
                    string NewFilePath = string.Empty;
                    string DestinationFont1 = string.Empty;

                     string DestinationFont2 = string.Empty;

                        string DestinationFont3 = string.Empty;
                    if (!string.IsNullOrEmpty(fonts.FontPath))
                    {

                        string NewPath = "Organisation" + companyRepository.OrganisationId + "/WebFonts/" + fonts.CustomerId;


                        DestinationFont1 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + NewPath + fonts.FontFile + ".eot");

                        DestinationFont2 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + NewPath + fonts.FontFile + ".ttf");

                        DestinationFont3 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + NewPath + fonts.FontFile + ".woff");

                        DestinationFontDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + NewPath);

                        FontSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + companyRepository.OrganisationId + "/WebFonts/" + fonts.FontPath + fonts.FontFile + ".eot");

                        FontSourcePath1 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + companyRepository.OrganisationId + "/WebFonts/" + fonts.FontPath + fonts.FontFile + ".ttf");

                        FontSourcePath2 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + companyRepository.OrganisationId + "/WebFonts/" + fonts.FontPath + fonts.FontFile + ".woff");

                        if (!System.IO.Directory.Exists(DestinationFontDirectory))
                        {
                            Directory.CreateDirectory(DestinationFontDirectory);
                            if (Directory.Exists(DestinationFontDirectory))
                            {
                                if (File.Exists(FontSourcePath))
                                {
                                    if (!File.Exists(DestinationFont1))
                                        File.Copy(FontSourcePath, DestinationFont1);
                                }

                                if (File.Exists(FontSourcePath1))
                                {
                                    if (!File.Exists(DestinationFont2))
                                        File.Copy(FontSourcePath1, DestinationFont2);

                                }

                                if (File.Exists(FontSourcePath2))
                                {
                                    if (!File.Exists(DestinationFont3))
                                        File.Copy(FontSourcePath2, DestinationFont3);

                                }

                            }

                        }
                        else
                        {
                            if (File.Exists(FontSourcePath))
                            {
                                if (!File.Exists(DestinationFont1))
                                    File.Copy(FontSourcePath, DestinationFont1);
                            }

                            if (File.Exists(FontSourcePath1))
                            {
                                if (!File.Exists(DestinationFont2))
                                    File.Copy(FontSourcePath1, DestinationFont2);

                            }

                            if (File.Exists(FontSourcePath2))
                            {
                                if (!File.Exists(DestinationFont3))
                                    File.Copy(FontSourcePath2, DestinationFont3);

                            }

                        }
                        fonts.FontPath = NewPath;
                    }
                

                    DestinationsPath.Add(DestinationFont1);
                    DestinationsPath.Add(DestinationFont2);
                    DestinationsPath.Add(DestinationFont3);



                }
            }
            // site.css
            string DestinationSiteFile = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID + "/Site.css");
            DestinationsPath.Add(DestinationSiteFile);
            string DestinationSiteFileDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID);
            string SourceSiteFile = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + OldCompanyID + "/Site.css");
            if (!System.IO.Directory.Exists(DestinationSiteFileDirectory))
            {
                Directory.CreateDirectory(DestinationSiteFileDirectory);
                if (Directory.Exists(DestinationSiteFileDirectory))
                {
                    if (File.Exists(SourceSiteFile))
                    {
                        if (!File.Exists(DestinationSiteFile))
                            File.Copy(SourceSiteFile, DestinationSiteFile);
                    }


                }


            }
            else
            {
                if (File.Exists(SourceSiteFile))
                {
                    if (!File.Exists(DestinationSiteFile))
                        File.Copy(SourceSiteFile, DestinationSiteFile);
                }

            }

            // sprite.png
            string DestinationSpriteFile = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID + "/Sprite.png");
            DestinationsPath.Add(DestinationSpriteFile);
            string DestinationSpriteDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID);
            string SourceSpriteFile = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + OldCompanyID + "/Sprite.png");
            if (!System.IO.Directory.Exists(DestinationSpriteDirectory))
            {
                Directory.CreateDirectory(DestinationSpriteDirectory);
                if (Directory.Exists(DestinationSpriteDirectory))
                {
                    if (File.Exists(SourceSiteFile))
                    {
                        if (!File.Exists(DestinationSpriteFile))
                            File.Copy(SourceSpriteFile, DestinationSpriteFile);
                    }

                }
                else
                {
                    if (File.Exists(SourceSpriteFile))
                    {
                        if (!File.Exists(DestinationSpriteFile))
                            File.Copy(SourceSpriteFile, DestinationSpriteFile);
                    }

                }


            }
            else
            {
                if (File.Exists(SourceSpriteFile))
                {
                    if (!File.Exists(DestinationSpriteFile))
                        File.Copy(SourceSpriteFile, DestinationSpriteFile);
                }
            }        
        }
        #endregion

        



    }
}

