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
using MPC.Repository.Repositories;

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
        private readonly IOrganisationFileTableViewRepository mpcFileTableViewRepository;
        private readonly IProductCategoryFileTableViewRepository productCategoryFileTableViewRepository;
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
                    companyTerritoryRepository.Delete(territory);
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
                    addressRepository.Delete(address);
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
                    companyContactRepository.Delete(companyContact);
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
            companyRepository.SaveChanges();
            return companySavingModel.Company;
        }

        private void UpdateContactProfileImage(CompanySavingModel companySavingModel, Company companyDbVersion)
        {
            if (companySavingModel.NewAddedCompanyContacts != null)
            {
                //Add New companyContacts
                foreach (var companyContact in companySavingModel.NewAddedCompanyContacts)
                {
                    companyContact.image = SaveCompanyContactProfileImage(companyContact);
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

            string directoryPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + company.CompanyId);
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
        /// 
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
                string savePath = directoryPath + "\\Contact_" + companyContact.ContactId + ".png";
                File.WriteAllBytes(savePath, data);
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

                return savePath;
            }
            return null;
        }

        #endregion

        #region Constructor

        public CompanyService(ICompanyRepository companyRepository, ISystemUserRepository systemUserRepository, IRaveReviewRepository raveReviewRepository,
            ICompanyCMYKColorRepository companyCmykColorRepository, ICompanyTerritoryRepository companyTerritoryRepository, IAddressRepository addressRepository,
            ICompanyContactRoleRepository companyContactRoleRepository, IRegistrationQuestionRepository registrationQuestionRepository
            , ICompanyBannerRepository companyBannerRepository, ICompanyContactRepository companyContactRepository, ICmsPageRepository cmsPageRepository,
             IPageCategoryRepository pageCategoryRepository, IEmailEventRepository emailEventRepository, IPaymentMethodRepository paymentMethodRepository,
            IPaymentGatewayRepository paymentGatewayRepository, IWidgetRepository widgetRepository, ICmsSkinPageWidgetRepository cmsSkinPageWidgetRepository, IProductCategoryRepository productCategoryRepository,
            IOrganisationRepository organisationRepository, IOrganisationFileTableViewRepository mpcFileTableViewRepository, IProductCategoryFileTableViewRepository productCategoryFileTableViewRepository,
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
            ICompanyDomainRepository companyDomainRepository)
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
                       CostCentres = costCentreRepository.GetAllCompanyCentersByOrganisationId().ToList()//GetAllCompanyCentersByCompanyId
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
        #endregion
    }
}
