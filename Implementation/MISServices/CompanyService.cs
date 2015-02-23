using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Net.Mime;
using System.Web;
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
using Newtonsoft.Json;
using System.Web.UI.WebControls;

namespace MPC.Implementation.MISServices
{
    public class CompanyService : ICompanyService
    {

        #region Private

        #region Repositories

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
        private readonly IGetItemsListViewRepository itemsListViewRepository;
        private readonly IItemRepository itemRepository;
        private readonly IItemStockOptionRepository itemStockOptionRepository;
        private readonly IPrefixRepository prefixRepository;
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
        private readonly ICompanyContactVariableRepository companyContactVariableRepository;
        #endregion

        /// <summary>
        /// Save Company
        /// </summary>
        private Company SaveNewCompany(CompanySavingModel companySaving)
        {
            //companySaving.Company.CmsPages = companySaving.NewAddedCmsPages;
            companyRepository.Add(companySaving.Company);
            companyRepository.SaveChanges();
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
            if (companyDbVersion.CompanyCMYKColors != null)
            {


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
                    companyDbVersion.CompanyCostCentres.Remove(dbVersionMissingItem);
                }
            }
            else if (company.CompanyCostCentres == null && companyDbVersion.CompanyCostCentres != null && companyDbVersion.CompanyCostCentres.Count > 0)
            {
                List<CompanyCostCentre> lisRemoveAllItemsList = companyDbVersion.CompanyCostCentres.ToList();
                foreach (CompanyCostCentre missingCompanyCostCentre in lisRemoveAllItemsList)
                {
                    CompanyCostCentre dbVersionMissingItem = companyDbVersion.CompanyCostCentres.First(x => x.CostCentreId == missingCompanyCostCentre.CostCentreId && x.CompanyId == missingCompanyCostCentre.CompanyId);
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
            //if (productCategory.ThumbNailBytes != null)
            //{
            //    string base64 = productCategory.ThumbNailBytes.Substring(productCategory.ThumbNailBytes.IndexOf(',') + 1);
            //    base64 = base64.Trim('\0');
            //    productCategory.ThumbNailFileBytes = Convert.FromBase64String(base64);
            //}
            //if (productCategory.ImageBytes != null)
            //{
            //    string base64Image = productCategory.ImageBytes.Substring(productCategory.ImageBytes.IndexOf(',') + 1);
            //    base64Image = base64Image.Trim('\0');
            //    productCategory.ImageFileBytes = Convert.FromBase64String(base64Image);
            //}
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
                    if (companyContacts.CompanyContactVariables != null)
                    {
                        foreach (var companyContactVariable in companyContacts.CompanyContactVariables)
                        {
                            FieldVariable fieldVariable = companySavingModel.Company.FieldVariables.FirstOrDefault(
                                f => f.FakeIdVariableId == companyContactVariable.FakeVariableId);
                            if (fieldVariable != null)
                                companyContactVariable.VariableId = fieldVariable.VariableId;
                        }
                    }

                    companyContacts.OrganisationId = companyContactRepository.OrganisationId;

                    companyDbVersion.CompanyContacts.Add(companyContacts);
                }
            }
            //Edit
            if (companySavingModel.EdittedCompanyContacts != null)
            {
                foreach (var companyContact in companySavingModel.EdittedCompanyContacts)
                {
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
            var productCategories = new List<ProductCategory>();
            companySavingModel.Company.OrganisationId = companyRepository.OrganisationId;
            var companyToBeUpdated = UpdateRaveReviewsOfUpdatingCompany(companySavingModel.Company);
            companyToBeUpdated = UpdatePaymentGatewaysOfUpdatingCompany(companyToBeUpdated);
            companyToBeUpdated = UpdateCmykColorsOfUpdatingCompany(companyToBeUpdated, companyDbVersion);
            companyToBeUpdated = UpdateCompanyCostCentersOfUpdatingCompany(companyToBeUpdated, companyDbVersion);
            companyToBeUpdated = UpdateCompanyDomain(companyToBeUpdated);

            UpdateTerritories(companySavingModel, companyDbVersion);
            UpdateAddresses(companySavingModel, companyDbVersion);
            UpdateCompanyContacts(companySavingModel, companyDbVersion);
            //UpdateCompanyTerritoryOfUpdatingCompany(companySavingModel);
            //UpdateAddressOfUpdatingCompany(companySavingModel);
            //UpdateCompanyContactOfUpdatingCompany(companySavingModel);
            UpdateProductCategoriesOfUpdatingCompany(companySavingModel, productCategories);

            UpdateSecondaryPagesCompany(companySavingModel, companyDbVersion);
            UpdateCampaigns(companySavingModel.Company.Campaigns, companyDbVersion);
            UpdateCmsSkinPageWidget(companySavingModel.CmsPageWithWidgetList, companyDbVersion);
            UpdateColorPallete(companySavingModel.Company, companyDbVersion);
            if (companyToBeUpdated.ImageBytes != null)
            {
                companySavingModel.Company.Image = SaveCompanyProfileImage(companySavingModel.Company);
            }
            companyRepository.Update(companyToBeUpdated);
            companyRepository.Update(companySavingModel.Company);
            UpdateCmsOffers(companySavingModel.Company, companyDbVersion);
            UpdateMediaLibrary(companySavingModel.Company, companyDbVersion);
            BannersUpdate(companySavingModel.Company, companyDbVersion);
            companyRepository.SaveChanges();
            //Update products
            UpdateProductsOfUpdatingCompany(companySavingModel);
            //Save Files
            companyToBeUpdated.ProductCategories = productCategories;
            //SaveFilesOfProductCategories(companyToBeUpdated);
            SaveSpriteImage(companySavingModel.Company);
            SaveCompanyCss(companySavingModel.Company);
            UpdateMediaLibraryFilePath(companySavingModel.Company, companyDbVersion);

            UpdateContactProfileImage(companySavingModel, companyDbVersion);
            SaveCompanyBannerImages(companySavingModel.Company);
            SaveStoreBackgroundImage(companySavingModel.Company, companyDbVersion);
            UpdateSecondaryPageImagePath(companySavingModel, companyDbVersion);
            UpdateCampaignImages(companySavingModel.Company.Campaigns, companyDbVersion);
            companyRepository.SaveChanges();
            return companySavingModel.Company;
        }

        public void UpdateCampaignImages(IEnumerable<Campaign> campaigns, Company companyDbVersion)
        {
            if (campaigns != null)
            {
                foreach (var campaign in campaigns)
                {
                    if (campaign.CampaignImages != null)
                    {
                        foreach (var campaignImage in campaign.CampaignImages)
                        {
                            campaignImage.ImagePath = SaveCampaignImage(campaignImage, companyDbVersion.CompanyId);

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

                string savePath = directoryPath + "\\" + campaignImage.CampaignImageId + ".png";
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
                    if (item.FilePath == string.Empty)
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
                companyRepository.SaveChanges();
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
            if (company.StoreBackgroundFile != null)
            {
                string base64 = company.StoreBackgroundFile.Substring(company.StoreBackgroundFile.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);

                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string savePath = directoryPath + "\\background.png";
                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                }

                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                companyDbVersion.StoreBackgroundImage = savePath;
            }
        }

        /// <summary>
        /// Update Secondary Page Images Path
        /// </summary>
        private void UpdateSecondaryPageImagePath(CompanySavingModel companySavingModel, Company companyDbVersion)
        {
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
        private void SaveFilesOfProductCategories(Company company)
        {
            //// Update Organisation MISLogoStreamId
            //Organisation organisation = organisationRepository.Find(organisationRepository.OrganisationId);
            ////IEnumerable<ProductCategory> productCategories = productCategoryRepository.GetAllCategoriesByStoreId(company.CompanyId);
            //if (organisation == null)
            //{
            //    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture,
            //        LanguageResources.MyOrganisationService_OrganisationNotFound,
            //        organisationRepository.OrganisationId));
            //}
            //string pathLocator = "\\Organisation" + organisation.OrganisationId;
            //if (
            //    string.IsNullOrEmpty(productCategoryFileTableViewRepository.GetNewPathLocator(pathLocator,
            //        FileTableCaption.Category)))
            //{
            //    CategoryFileTableView categoryFile = productCategoryFileTableViewRepository.Create();
            //    productCategoryFileTableViewRepository.Add(categoryFile);
            //    categoryFile.Name = "Organisation" + organisation.OrganisationId;
            //    categoryFile.UncPath = pathLocator;
            //    categoryFile.IsDirectory = true;
            //    categoryFile.FileTableName = FileTableCaption.Category;
            //    // Save to File Table
            //    productCategoryFileTableViewRepository.SaveChanges();
            //}
            //if (company.ProductCategories != null)
            //{
            //    Dictionary<long, List<CategoryFileTableView>> categoryFileTableViews = new Dictionary<long, List<CategoryFileTableView>>();
            //    foreach (var productCategory in company.ProductCategories)
            //    {

            //        categoryFileTableViews[productCategory.ProductCategoryId] = new List<CategoryFileTableView>();
            //        if (!string.IsNullOrEmpty(productCategory.ThumbNailBytes))
            //        {
            //            // Add File
            //            CategoryFileTableView categoryFileTableView = productCategoryFileTableViewRepository.Create();

            //            categoryFileTableView.Name = productCategory.ThumbNailFileName + "_" + productCategory.ProductCategoryId + "_Thumbnail";
            //            categoryFileTableView.FileStream = productCategory.ThumbNailFileBytes;
            //            categoryFileTableView.FileTableName = FileTableCaption.Category;
            //            categoryFileTableView.UncPath = pathLocator;
            //            productCategoryFileTableViewRepository.Add(categoryFileTableView);

            //            categoryFileTableViews[productCategory.ProductCategoryId].Add(categoryFileTableView);
            //        }
            //        if (!string.IsNullOrEmpty(productCategory.ImageBytes))
            //        {
            //            // Add File
            //            CategoryFileTableView categoryFileTableView = productCategoryFileTableViewRepository.Create();

            //            categoryFileTableView.Name = productCategory.ImageFileName + "_" + productCategory.ProductCategoryId + "_Image";
            //            categoryFileTableView.FileStream = productCategory.ImageFileBytes;
            //            categoryFileTableView.FileTableName = FileTableCaption.Category;
            //            categoryFileTableView.UncPath = pathLocator;
            //            productCategoryFileTableViewRepository.Add(categoryFileTableView);

            //            categoryFileTableViews[productCategory.ProductCategoryId].Add(categoryFileTableView);
            //        }

            //    }
            //    productCategoryFileTableViewRepository.SaveChanges();

            //    foreach (var categoryFileTableView in categoryFileTableViews)
            //    {
            //        ProductCategory category =
            //            company.ProductCategories.FirstOrDefault(p => p.ProductCategoryId == categoryFileTableView.Key);
            //        if (category != null)
            //        {
            //            CategoryFileTableView view = categoryFileTableView.Value.FirstOrDefault(c => c.Name == category.ThumbNailFileName + "_" + category.ProductCategoryId + "_Thumbnail");
            //            if (view != null)
            //            {
            //                category.ThumbnailStreamId = view.StreamId;
            //            }
            //            CategoryFileTableView view2 = categoryFileTableView.Value.FirstOrDefault(c => c.Name == category.ImageFileName + "_" + category.ProductCategoryId + "_Image");
            //            if (view2 != null)
            //            {
            //                category.ImageStreamId = view2.StreamId;
            //            }
            //        }
            //    }

            //    productCategoryFileTableViewRepository.SaveChanges();
            //}
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
                        cmsSkinPageWidgetRepository.SaveChanges();
                    }
                }
                #endregion
            }




        }
        /// <summary>
        /// Update Campaigns
        /// </summary>
        private void UpdateCampaigns(IEnumerable<Campaign> campaigns, Company companyDbVersion)
        {
            #region update Campaign

            if (campaigns != null)
            {
                if (companyDbVersion.Campaigns == null)
                {
                    companyDbVersion.Campaigns = new Collection<Campaign>();
                }
                foreach (var campaign in campaigns)
                {
                    #region Campaign
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
                    }

                    #endregion

                    #region Campain Image

                    if (campaign.CampaignImages != null)
                    {
                        foreach (var campaignImage in campaign.CampaignImages)
                        {
                            Campaign campaignDbItem =
                            companyDbVersion.Campaigns.FirstOrDefault(c => c.CampaignId == campaign.CampaignId);
                            if (campaignImage.CampaignImageId == 0 && campaignDbItem != null)
                            {
                                campaignImage.CampaignId = campaignDbItem.CampaignId;
                                campaignDbItem.CampaignImages.Add(campaignImage);
                            }
                        }
                    }
                    #endregion
                }
            }

            #endregion

            #region Delete Campaigns

            //find missing items
            List<Campaign> missingCampaignListItems = new List<Campaign>();
            if (companyDbVersion.Campaigns != null)
            {


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
                                        bannerDbVersion.ImageURL = item.ImageURL;
                                    }
                                }
                            }
                        }
                    }

                }
            }//End Add/Edit 
            #endregion

            #region Delete Banners

            if (companyDbVersion.CompanyBannerSets != null)
            {


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
            }
            #endregion


        }

        /// <summary>
        /// Save Company Banner Images
        /// </summary>
        private void SaveCompanyBannerImages(Company company)
        {
            if (company.CompanyBannerSets != null)
            {
                foreach (var item in company.CompanyBannerSets)
                {
                    if (item.CompanyBanners != null)
                        foreach (var banner in item.CompanyBanners)
                        {
                            foreach (var media in company.MediaLibraries)
                            {
                                if (media.FakeId == banner.ImageURL)
                                {
                                    banner.ImageURL = media.FilePath;
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
                string savePath = directoryPath + "\\" + companyContact.ContactId + "_profile" + ".png";
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
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
            fieldVariableRepository.SaveChanges();



            if (companyId > 0 && fieldVariable.Scope.HasValue && fieldVariable.Scope == (int)FieldVariableScopeType.Contact)
            {
                IEnumerable<CompanyContact> companyContacts =
                    companyContactRepository.GetCompanyContactsByCompanyId(companyId);
                if (companyContacts != null)
                {
                    foreach (var contact in companyContacts)
                    {
                        CompanyContactVariable contactVariable = new CompanyContactVariable();

                        contactVariable.ContactId = contact.ContactId;
                        contactVariable.VariableId = fieldVariable.VariableId;
                        contactVariable.Value = fieldVariable.DefaultValue;
                        companyContactVariableRepository.Add(contactVariable);
                    }
                    companyContactVariableRepository.SaveChanges();
                }
            }

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
                        variableOptionRepository.SaveChanges();
                    }
                }
                #endregion

                fieldVariableRepository.SaveChanges();
            }

            return fieldVariable.VariableId;
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
            ICompanyContactVariableRepository companyContactVariableRepository)
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
            this.companyContactVariableRepository = companyContactVariableRepository;

        }
        #endregion

        #region Public

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
            return companyRepository.GetCompanyById(companyId);
        }

        public CompanyBaseResponse GetBaseData(long storeId)
        {
            FieldVariableRequestModel request = new FieldVariableRequestModel();
            request.CompanyId = storeId;

            return new CompanyBaseResponse
                   {
                       SystemUsers = systemUserRepository.GetAll(),
                       CompanyTerritories = companyTerritoryRepository.GetAllCompanyTerritories(storeId),
                       CompanyContactRoles = companyContactRoleRepository.GetAll(),
                       PageCategories = pageCategoryRepository.GetCmsSecondaryPageCategories(),
                       RegistrationQuestions = registrationQuestionRepository.GetAll(),
                       Addresses = addressRepository.GetAllAddressByStoreId(storeId),
                       PaymentMethods = paymentMethodRepository.GetAll().ToList(),
                       EmailEvents = emailEventRepository.GetAll(),
                       Widgets = widgetRepository.GetAll(),
                       CostCentres = costCentreRepository.GetAllCompanyCentersByOrganisationId().ToList(),//GetAllCompanyCentersByCompanyId
                       States = stateRepository.GetAll(),
                       Countries = countryRepository.GetAll(),
                       FieldVariableResponse = fieldVariableRepository.GetFieldVariable(request)
                   };
        }
        public CompanyBaseResponse GetBaseDataForNewCompany()
        {
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
                Countries = countryRepository.GetAll()
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
                return SaveNewCompany(companyModel);
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
        public IEnumerable<CompanyContactVariable> GetContactVariableByContactId(long contactId)
        {
            return companyContactVariableRepository.GetContactVariableByContactId(contactId);
        }

        /// <summary>
        /// Get Field Varibale By Company ID
        /// </summary>
        public IEnumerable<FieldVariable> GetFieldVariableByCompanyId(long companyId)
        {
            return fieldVariableRepository.GetFieldVariableByCompanyId(companyId);
        }
        #endregion

        #region ExportOrganisation

        public void ExportOrganisation(long OrganisationID)
        {
            try
            {
                #region OrganisationEntities
                string DPath = string.Empty;
                ExportOrganisation ObjExportOrg = new Models.Common.ExportOrganisation();
                Organisation organisation = new Organisation();
                List<CostCentre> costCentre = new List<CostCentre>();
                List<CostCentreMatrixDetail> costCentreMatrixDetail = new List<CostCentreMatrixDetail>();
                List<CostCentreAnswer> CostCentreAnswers = new List<CostCentreAnswer>();
                List<StockCategory> StockCategories = new List<StockCategory>();
                List<StockSubCategory> StockSubCategories = new List<StockSubCategory>();
                List<StockItem> StockItems = new List<StockItem>();
                List<CostCenterChoice> CostCenterChoice = new List<CostCenterChoice>();
                List<PhraseField> PhraseField = new List<PhraseField>();
                List<Report> Reports = new List<Report>();
                List<ItemSection> ItemSections = new List<ItemSection>();
                List<SectionCostcentre> SectionCostCentre = new List<SectionCostcentre>();
                List<SectionCostCentreResource> SectionCostCentreResources = new List<SectionCostCentreResource>();


                // get organisation to export
                organisation = organisationRepository.GetOrganizatiobByOrganisationID(OrganisationID);
                ObjExportOrg.Organisation = organisation;

                // for paper size add organisationid in papersize
                ObjExportOrg.PaperSizes = PaperSizeRepository.GetPaperByOrganisation(OrganisationID);

                // get costcentres based on organisationid 
                costCentre = costCentreRepository.GetCostCentersByOrganisationID(OrganisationID, out CostCenterChoice);
                ObjExportOrg.CostCentre = costCentre;

                ObjExportOrg.CostCentreQuestion = CostCentreQuestionRepository.GetCostCentreQuestionsByOID(OrganisationID, out CostCentreAnswers);

                // for cost centre answers
                ObjExportOrg.CostCentreAnswer = CostCentreAnswers;


                // workinstructions based on costcentreid
                List<CostcentreInstruction> lstInstruction = new List<CostcentreInstruction>();
                if (costCentre != null && costCentre.Count > 0)
                {
                    foreach (var cost in costCentre)
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
                if (costCentre != null && costCentre.Count > 0)
                {
                    foreach (var cost in costCentre)
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
                if (costCentre != null && costCentre.Count > 0)
                {
                    foreach (var cost in costCentre)
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

                // cost centre choices is missing
                ObjExportOrg.CostCenterChoice = CostCenterChoice;

                // get stockcategories based on organisation ID
                StockCategories = StockCategoryRepository.GetStockCategoriesByOrganisationID(OrganisationID);
                ObjExportOrg.StockCategory = StockCategories;


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
                            ObjExportOrg.StockSubCategory = lstSubCategory;
                        }
                    }
                }


                // get stockitems based on organisationID
                StockItems = stockItemRepository.GetStockItemsByOrganisationID(OrganisationID);
                ObjExportOrg.StockItem = StockItems;


                // set stock sale and price]
                List<StockCostAndPrice> lstSCP = new List<StockCostAndPrice>();
                if (StockItems != null)
                {
                    if (StockItems.Count > 0)
                    {
                        foreach (var stock in StockItems)
                        {

                            if (stock.StockCostAndPrices != null)
                            {
                                if (stock.StockCostAndPrices.Count > 0)
                                {
                                    foreach (var costP in stock.StockCostAndPrices)
                                    {
                                        lstSCP.Add(costP);
                                    }
                                    ObjExportOrg.StockCostAndPrice = lstSCP;
                                }
                            }
                        }
                    }
                }

                // Delivery carriers structure is not defined yet


                // reports 
                ObjExportOrg.Reports = ReportRepository.GetReportsByOrganisationID(OrganisationID);

                ObjExportOrg.ReportNote = ReportRepository.GetReportNotesByOrganisationID(OrganisationID);

                // get prefixes based on organisationID
                ObjExportOrg.Prefixes = prefixRepository.GetPrefixesByOrganisationID(OrganisationID);

                // get machines by organisation id
                ObjExportOrg.Machines = MachineRepository.GetMachinesByOrganisationID(OrganisationID);

                // get lookupmethods by organisationid
                ObjExportOrg.LookupMethods = MachineRepository.getLookupmethodsbyOrganisationID(OrganisationID);


                // Phrases of organisation
                PhraseField = PhraseFieldRepository.GetPhraseFieldsByOrganisationID(OrganisationID);
                ObjExportOrg.PhraseField = PhraseField;

                // organisationID in phrase fields
                List<Phrase> lstPhrase = new List<Phrase>();
                if (PhraseField != null)
                {
                    foreach (var phrase in PhraseField)
                    {

                        if (phrase.Phrases != null && phrase.Phrases.Count > 0)
                        {
                            foreach (var p in phrase.Phrases)
                            {
                                lstPhrase.Add(p);

                            }
                            ObjExportOrg.Phrases = lstPhrase;
                        }
                    }
                }



                // section flags of organisation
                ObjExportOrg.SectionFlags = sectionFlagRepository.GetSectionFlagsByOrganisationID(OrganisationID);


                // Comapny Entities

                // Set CompanyData
                long CompanyID = 2165; // Later it will be changes

                // Company Company = new Models.DomainModels.Company();
                // Company = companyRepository.GetStoreByStoreId(CompanyID);

                // set Company Domain
                //if (Company.CompanyDomains != null)
                //{
                //    ObjExportOrg.CompanyDomain = Company.CompanyDomains.Take(2).ToList();
                //}

                //// set systemusers
                //ObjExportOrg.SystemUser = systemUserRepository.GetSystemUSersByOrganisationID(OrganisationID);

                //// set cms offers
                //if(Company.CmsOffers != null)
                //{
                //    ObjExportOrg.CmsOffer = Company.CmsOffers.Take(2).ToList();
                //}

                //// set media libraries
                //if(Company.MediaLibraries != null)
                //{
                //    ObjExportOrg.MediaLibrary = Company.MediaLibraries.Take(2).ToList();
                //}

                //List<CompanyBanner> Lstbanner = new List<CompanyBanner>();
                //// company banners
                //if(Company.CompanyBannerSets != null)
                //{
                //  List<CompanyBannerSet>  CompanyBannerSet = Company.CompanyBannerSets.ToList();
                //  ObjExportOrg.CompanyBannerSet = CompanyBannerSet;
                //  if(CompanyBannerSet != null && CompanyBannerSet.Count > 0)
                //  {
                //      foreach(var banner in CompanyBannerSet)
                //      {
                //          if(banner.CompanyBanners != null)
                //          {
                //              if(banner.CompanyBanners.Count > 0)
                //              {
                //                  foreach(var bann in banner.CompanyBanners)
                //                  {
                //                      Lstbanner.Add(bann);
                //                  }
                //              }
                //          }
                //      }

                //  }
                //  ObjExportOrg.CompanyBanner = Lstbanner.Take(2).ToList();
                //}

                //// Secondary Pages
                //if(Company.CmsPages != null)
                //{
                //    ObjExportOrg.SecondaryPages = Company.CmsPages.ToList();
                //}

                //// Rave Reviews
                //if(Company.RaveReviews != null)
                //{
                //    ObjExportOrg.RaveReview = Company.RaveReviews.Take(2).ToList();
                //}

                //// CompanyTerritories

                //if(Company.CompanyTerritories != null)
                //{
                //    ObjExportOrg.CompanyTerritory = Company.CompanyTerritories.Take(2).ToList();
                //}

                //// Addresses

                //if(Company.Addresses != null)
                //{
                //    ObjExportOrg.Address = Company.Addresses.Take(2).ToList();
                //}

                //// contacts

                //if(Company.CompanyContacts != null)
                //{
                //    ObjExportOrg.CompanyContact = Company.CompanyContacts.Take(2).ToList();
                //}

                //// product Categories
                //if (Company.ProductCategories != null)
                //{
                //    ObjExportOrg.ProductCategory = Company.ProductCategories.Where(s => s.isPublished == true && s.isArchived == false).Take(2).ToList();
                //}

                //// items
                //if(Company.Items != null)
                //{
                //    List<Item> items = Company.Items.Where(i => i.IsPublished == true && i.IsArchived == false).ToList();
                //    items = items.Take(2).ToList();

                //    ObjExportOrg.Items = items;

                //    if(items != null)
                //    {
                //        if(items.Count > 0)
                //        {
                //            foreach(var item in items)
                //            {
                //                // itemSections
                //                if(item.ItemSections != null)
                //                {

                //                    if(item.ItemSections != null && item.ItemSections.Count > 0)
                //                    {
                //                        // add item sections
                //                        foreach(var sec in item.ItemSections)
                //                        {
                //                            if(sec.SectionCostcentres != null)
                //                            {
                //                                if(sec.SectionCostcentres.Count > 0)
                //                                {
                //                                    // add section Costcentre
                //                                    foreach(var ss in sec.SectionCostcentres)
                //                                    {
                //                                        if(ss.SectionCostCentreResources != null)
                //                                        {
                //                                            if(ss.SectionCostCentreResources.Count > 0)
                //                                            {
                //                                                foreach(var res in ss.SectionCostCentreResources)
                //                                                {
                //                                                    SectionCostCentreResources.Add(res);
                //                                                }

                //                                            }
                //                                        }

                //                                        SectionCostCentre.Add(ss);
                //                                    }
                //                                }
                //                            }
                //                            ItemSections.Add(sec);
                //                        }
                //                    }
                //                }


                //            }
                //        }
                //        ObjExportOrg.ItemSection = ItemSections;
                //        ObjExportOrg.SectionCostcentre = SectionCostCentre;
                //        ObjExportOrg.SectionCostCentreResource = SectionCostCentreResources;
                //    }


                //}


                //// add companyid in campaigns

                //// payment gateways
                //if(Company.PaymentGateways != null)
                //{

                //    ObjExportOrg.PaymentGateway = Company.PaymentGateways.Take(2).ToList();
                //}



                //// cms skin page widgets
                //if(Company.CmsSkinPageWidgets != null)
                //{
                //    ObjExportOrg.CmsSkinPageWidget = Company.CmsSkinPageWidgets.Take(2).ToList();
                //}


                //// company cost centre
                //if (Company.CompanyCostCentres != null)
                //{
                //    ObjExportOrg.CompanyCostCentre = Company.CompanyCostCentres.Take(2).ToList();
                //}

                //if(Company.CompanyCMYKColors != null)
                //{
                //    ObjExportOrg.CompanyCMYKColor = Company.CompanyCMYKColors.Take(2).ToList();
                //}

                // export json file

                ObjExportOrg = companyRepository.ExportCompany(ObjExportOrg, CompanyID);
                string Json = JsonConvert.SerializeObject(ObjExportOrg, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

                string sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/Json.txt";
                System.IO.File.WriteAllText(sPath, Json);


                #endregion


                #region ExportFiles


                using (ZipFile zip = new ZipFile())
                {
                    if (File.Exists(sPath))
                    {
                        ZipEntry r = zip.AddFile(sPath, "");
                        r.Comment = "Json File for an organisation";
                    }


                    //export language file for an organisation

                    // Add all files in directory
                    string FolderPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Resources/" + OrganisationID;
                    DPath = "/MPC_Content/Resources/" + OrganisationID;
                    if (Directory.Exists(FolderPath))
                    {
                        foreach (string item in System.IO.Directory.GetFiles(FolderPath))
                        {

                            ZipEntry r = zip.AddFile(item, DPath);
                            r.Comment = "Language File for an organisation";

                        }
                    }

                    // export MIS logo in Organisation
                    if (organisation != null)
                    {

                        if (organisation.MISLogo != null)
                        {
                            string FilePath = HttpContext.Current.Server.MapPath(organisation.MISLogo);
                            DPath = "/Organisations/" + OrganisationID;
                            if (File.Exists(FilePath))
                            {
                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                r.Comment = "MIS Logo for an organisation";

                            }
                        }


                    }

                    // export cost centre images
                    if (costCentre != null && costCentre.Count > 0)
                    {
                        foreach (var objCost in costCentre)
                        {
                            if (objCost.ThumbnailImageURL != null)
                            {

                                string FilePath = HttpContext.Current.Server.MapPath(objCost.ThumbnailImageURL);
                                DPath = "/CostCentres/" + OrganisationID;
                                if (File.Exists(FilePath))
                                {
                                    ZipEntry r = zip.AddFile(FilePath, DPath);
                                    r.Comment = "Thumbnail image for cost centre";

                                }
                            }
                            if (objCost.MainImageURL != null)
                            {
                                string FilePath = HttpContext.Current.Server.MapPath(objCost.MainImageURL);
                                DPath = "/CostCentres/" + OrganisationID;
                                if (File.Exists(FilePath))
                                {
                                    ZipEntry r = zip.AddFile(FilePath, DPath);
                                    r.Comment = "Main image for cost centre";

                                }
                            }
                        }
                    }

                    //// export report banner
                    if (ObjExportOrg.ReportNote != null && ObjExportOrg.ReportNote.Count > 0)
                    {

                        foreach (var report in ObjExportOrg.ReportNote)
                        {
                            if (report.ReportBanner != null)
                            {
                                //string FilePath = HttpContext.Current.Server.MapPath(report.ReportBanner);
                                //DPath = "/Media/" + OrganisationID + "/" + CompanyID;
                                //if (File.Exists(FilePath))
                                //{
                                //    ZipEntry r = zip.AddFile(FilePath, DPath);
                                //    r.Comment = "Media Files for Store";

                                //}
                            }
                        }
                    }
                    //// export company Logo
                    if (ObjExportOrg.Company != null)
                    {
                        if (ObjExportOrg.Company.Image != null)
                        {
                            string FilePath = HttpContext.Current.Server.MapPath(ObjExportOrg.Company.Image);
                            DPath = "/Assets/" + OrganisationID + "/" + CompanyID;
                            if (File.Exists(FilePath))
                            {
                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                r.Comment = "Company Logo for Store";

                            }
                        }
                        // export company background image

                        if (ObjExportOrg.Company.StoreBackgroundImage != null)
                        {
                            string FilePath = HttpContext.Current.Server.MapPath(ObjExportOrg.Company.StoreBackgroundImage);
                            DPath = "/Assets/" + OrganisationID + "/" + CompanyID;
                            if (File.Exists(FilePath))
                            {
                                ZipEntry r = zip.AddFile(FilePath, DPath);
                                r.Comment = "Background image for Store";

                            }
                        }

                        // export media

                        if (ObjExportOrg.Company.MediaLibraries != null)
                        {
                            if (ObjExportOrg.Company.MediaLibraries.Count > 0)
                            {
                                foreach (var media in ObjExportOrg.Company.MediaLibraries)
                                {
                                    if (media.FilePath != null)
                                    {
                                        string FilePath = HttpContext.Current.Server.MapPath(media.FilePath);
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
                        // export company banners
                        //if (ObjExportOrg.Company.CompanyBannerSets != null)
                        //{
                        //    List<CompanyBannerSet> CompanyBannerSet = ObjExportOrg.Company.CompanyBannerSets.ToList();

                        //    if (CompanyBannerSet != null && CompanyBannerSet.Count > 0)
                        //    {
                        //        foreach (var banner in CompanyBannerSet)
                        //        {
                        //            if (banner.CompanyBanners != null)
                        //            {
                        //                if (banner.CompanyBanners.Count > 0)
                        //                {
                        //                    foreach (var bann in banner.CompanyBanners)
                        //                    {
                        //                        if (bann.ImageURL != null)
                        //                        {
                        //                            string FilePath = HttpContext.Current.Server.MapPath(bann.ImageURL);
                        //                            if (File.Exists(FilePath))
                        //                            {
                        //                                ZipEntry r = zip.AddFile(FilePath, bann.ImageURL);
                        //                                r.Comment = "Banners for Store";

                        //                            }
                        //                        }
                        //                    }
                        //                }

                        //            }
                        //        }
                        //    }


                        //}
                        if (ObjExportOrg.Company.ProductCategories != null)
                        {
                            foreach (var cat in ObjExportOrg.Company.ProductCategories)
                            {
                                if (cat.ImagePath != null)
                                {
                                    string FilePath = HttpContext.Current.Server.MapPath(cat.ImagePath);
                                    DPath = "/Categories/" + OrganisationID + "/" + CompanyID;
                                    if (File.Exists(FilePath))
                                    {
                                        ZipEntry r = zip.AddFile(FilePath, DPath);
                                        r.Comment = "Category Image Path for Store";

                                    }
                                }

                                if (cat.ThumbnailPath != null)
                                {
                                    string FilePath = HttpContext.Current.Server.MapPath(cat.ThumbnailPath);
                                    DPath = "/Categories/" + OrganisationID + "/" + CompanyID;

                                    if (File.Exists(FilePath))
                                    {
                                        ZipEntry r = zip.AddFile(FilePath, DPath);
                                        r.Comment = "Category Thumbnail Path for Store";

                                    }
                                }

                            }

                        }
                        if (ObjExportOrg.Company.Items != null)
                        {
                            if (ObjExportOrg.Company.Items.Count > 0)
                            {
                                foreach (var item in ObjExportOrg.Company.Items)
                                {
                                    if (item.ImagePath != null)
                                    {
                                        string FilePath = HttpContext.Current.Server.MapPath(item.ImagePath);
                                        DPath = "/Products/" + OrganisationID + "/" + CompanyID;
                                        if (File.Exists(FilePath))
                                        {
                                            ZipEntry r = zip.AddFile(FilePath, DPath);
                                            r.Comment = "Items Image Path for Store";

                                        }
                                    }

                                    if (item.ThumbnailPath != null)
                                    {
                                        string FilePath = HttpContext.Current.Server.MapPath(item.ThumbnailPath);
                                        DPath = "/Products/" + OrganisationID + "/" + CompanyID;
                                        if (File.Exists(FilePath))
                                        {
                                            ZipEntry r = zip.AddFile(FilePath, DPath);
                                            r.Comment = "Items Thumbnail Path for Store";

                                        }
                                    }

                                    if (item.GridImage != null)
                                    {
                                        string FilePath = HttpContext.Current.Server.MapPath(item.GridImage);
                                        DPath = "/Products/" + OrganisationID + "/" + CompanyID;
                                        if (File.Exists(FilePath))
                                        {
                                            ZipEntry r = zip.AddFile(FilePath, DPath);
                                            r.Comment = "Items Grid image for Store";

                                        }
                                    }
                                    if (item.File1 != null)
                                    {
                                        string FilePath = HttpContext.Current.Server.MapPath(item.File1);
                                        DPath = "/Products/" + OrganisationID + "/" + CompanyID;
                                        if (File.Exists(FilePath))
                                        {
                                            ZipEntry r = zip.AddFile(FilePath, DPath);
                                            r.Comment = "Items image for Store";

                                        }
                                    }
                                    if (item.File2 != null)
                                    {
                                        string FilePath = HttpContext.Current.Server.MapPath(item.File2);
                                        DPath = "/Products/" + OrganisationID + "/" + CompanyID;
                                        if (File.Exists(FilePath))
                                        {
                                            ZipEntry r = zip.AddFile(FilePath, DPath);
                                            r.Comment = "Items image for Store";

                                        }
                                    }
                                    if (item.File3 != null)
                                    {
                                        string FilePath = HttpContext.Current.Server.MapPath(item.File3);
                                        DPath = "/Products/" + OrganisationID + "/" + CompanyID;
                                        if (File.Exists(FilePath))
                                        {
                                            ZipEntry r = zip.AddFile(FilePath, DPath);
                                            r.Comment = "Items image for Store";

                                        }
                                    }
                                    if (item.File4 != null)
                                    {
                                        string FilePath = HttpContext.Current.Server.MapPath(item.File4);
                                        DPath = "/Products/" + OrganisationID + "/" + CompanyID;
                                        if (File.Exists(FilePath))
                                        {
                                            ZipEntry r = zip.AddFile(FilePath, DPath);
                                            r.Comment = "Items image for Store";

                                        }
                                    }
                                    if (item.File5 != null)
                                    {
                                        string FilePath = HttpContext.Current.Server.MapPath(item.File5);
                                        DPath = "/Products/" + OrganisationID + "/" + CompanyID;
                                        if (File.Exists(FilePath))
                                        {
                                            ZipEntry r = zip.AddFile(FilePath, DPath);
                                            r.Comment = "Items image for Store";

                                        }
                                    }
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


                    zip.Comment = "This zip archive was created to export complete organisation";
                    string sDirectory = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations";
                    string name = "ExportedZip" + OrganisationID;
                    string sZipFileName = string.Empty;
                    if (Path.HasExtension(name))
                        sZipFileName = name;
                    else
                        sZipFileName = name + ".zip";
                    if (System.IO.Directory.Exists(sDirectory))
                    {
                        zip.Save(sDirectory + "\\" + sZipFileName);
                    }
                }


                #endregion



            }
            catch (Exception ex)
            {
                throw new MPCException(ex.ToString(), OrganisationID);
            }

        }

        #endregion

        #region ImportOrganisation

        public void ImportOrganisation()
        {
            try
            {
                long OrganisationID = 0;
                string extractPath = System.Web.Hosting.HostingEnvironment.MapPath("/MPC_Content/Organisations/ImportOrgansation");
                string ReadPath = System.Web.Hosting.HostingEnvironment.MapPath("/MPC_Content/Organisations/ExportedZip2.zip");

                //string zipToUnpack = "C1P3SML.zip";
                //string unpackDirectory = "Extracted Files";
                using (ZipFile zip1 = ZipFile.Read(ReadPath))
                {
                    // here, we extract every entry, but we could extract conditionally
                    // based on entry name, size, date, checkbox status, etc.  
                    foreach (ZipEntry e in zip1)
                    {
                        e.Extract(extractPath, ExtractExistingFileAction.OverwriteSilently);
                        string JsonFilePath = System.Web.Hosting.HostingEnvironment.MapPath("/MPC_Content/Organisations/ImportOrgansation/Json.txt");
                        if (File.Exists(JsonFilePath))
                        {
                            string json = System.IO.File.ReadAllText(JsonFilePath);

                            ExportOrganisation objExpOrg = JsonConvert.DeserializeObject<ExportOrganisation>(json);
                            //List<TemplateObjects> lstTemplatesObjects = JsonConvert.DeserializeObject<List<TemplateObjects>>(res);
                            //if(objExpOrg.Organisation != null)
                            //{
                            organisationRepository.InsertOrganisation(objExpOrg.Organisation, objExpOrg);
                            //}
                            //if(objExpOrg.PaperSizes != null && objExpOrg.PaperSizes.Count > 0)
                            //{

                            //}

                        }



                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
        #endregion
    }
}

