
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Models.Common;
using System.Globalization;
using MPC.Common;
using WebSupergoo.ABCpdf8;
using System.IO;
using System.Configuration;
using MPC.Webstore.Common;

namespace MPC.Implementation.WebStoreServices
{
    public class CompanyService : ICompanyService
    {
        #region Private

        /// <summary>
        /// Private members
        /// </summary>
        /// 
        private readonly IListingRepository _listingRepository;
        private readonly IAssetItemsRepository _AssetItemsRepository;
        public readonly ICompanyRepository _CompanyRepository;
        public readonly ICompanyContactRepository _CompanyContactRepository;
        private readonly ISystemUserRepository _SystemUserRepository;
        private readonly ICmsSkinPageWidgetRepository _widgetRepository;
        private readonly ICompanyBannerRepository _companyBannerRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly ICmsPageRepository _cmsPageRepositary;
        private readonly IPageCategoryRepository _pageCategoryRepositary;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IGlobalLanguageRepository _globalLanguageRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IOrganisationRepository _organisationRepository;
        private readonly ISystemUserRepository _systemUserRepository;
        private readonly ICampaignRepository _campaignRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IMarkupRepository _markupRepository;
        private readonly ICompanyTerritoryRepository _CompanyTerritoryRepository;
        private readonly IStateRepository _StateRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IFavoriteDesignRepository _favoriteRepository;
        private readonly INewsLetterSubscriberRepository _newsLetterSubscriberRepository;
        private readonly IRaveReviewRepository _raveReviewRepository;
        private readonly IOrderRepository _orderrepository;
        private readonly ICompanyVoucherRedeemRepository _companyVoucherReedemRepository;
        private readonly IRegistrationQuestionRepository _questionRepository;
        private readonly ICompanyContactRoleRepository _companycontactRoleRepo;
        private readonly IScopeVariableRepository _IScopeVariableRepository;
        private readonly ICompanyDomainRepository _companyDomainRepository;
        private readonly IListingBulletPointsRepository _listingBulletPontRepository;
        private readonly IAssetsRepository _AssestsRepository;
        private readonly IFolderRepository _FolderRepository;
        private readonly ITemplateRepository _templaterepository;
        private readonly IReportRepository _ReportRepository;
        private string pageTitle = string.Empty;
        private string MetaKeywords = string.Empty;
        private string MetaDEsc = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public CompanyService(ICompanyRepository companyRepository, ICmsSkinPageWidgetRepository widgetRepository,
         ICompanyBannerRepository companyBannerRepository, IProductCategoryRepository productCategoryRepository, ICmsPageRepository cmspageRepository,
            IPageCategoryRepository pageCategoryRepository, ICompanyContactRepository companyContactRepository, ICurrencyRepository currencyRepository
            , IGlobalLanguageRepository globalLanguageRepository, IOrganisationRepository organisationRepository, ISystemUserRepository systemUserRepository, IItemRepository itemRepository, IAddressRepository addressRepository, IMarkupRepository markuprepository
            , ICountryRepository countryRepository, IStateRepository stateRepository, IFavoriteDesignRepository favoriteRepository, IStateRepository StateRepository, ICompanyTerritoryRepository CompanyTerritoryRepository
            , INewsLetterSubscriberRepository newsLetterSubscriberRepository, IRaveReviewRepository raveReviewRepository, IOrderRepository _orderrepository
            , ICompanyVoucherRedeemRepository companyVoucherReedemRepository, IRegistrationQuestionRepository _questionRepository,
            ICompanyContactRoleRepository _companycontactRoleRepo, ISystemUserRepository _SystemUserRepository, IScopeVariableRepository IScopeVariableRepository
            , ICompanyDomainRepository companyDomainRepository, IListingRepository _listingRepository, IListingBulletPointsRepository _listingBulletPontRepository, IAssetsRepository _AssestsRepository, IFolderRepository _FolderRepository, IAssetItemsRepository _AssetItemsRepository, ITemplateRepository _templaterepository, IReportRepository _ReportRepository)
        {
            this._listingRepository = _listingRepository;
            this._CompanyRepository = companyRepository;
            this._questionRepository = _questionRepository;
            this._widgetRepository = widgetRepository;
            this._companyBannerRepository = companyBannerRepository;
            this._productCategoryRepository = productCategoryRepository;
            this._cmsPageRepositary = cmspageRepository;
            this._pageCategoryRepositary = pageCategoryRepository;
            this._CompanyContactRepository = companyContactRepository;
            this._currencyRepository = currencyRepository;
            this._globalLanguageRepository = globalLanguageRepository;
            this._organisationRepository = organisationRepository;
            this._systemUserRepository = systemUserRepository;
            this._itemRepository = itemRepository;
            this._markupRepository = markuprepository;
            this._addressRepository = addressRepository;
            this._CompanyTerritoryRepository = CompanyTerritoryRepository;
            this._StateRepository = StateRepository;
            this._countryRepository = countryRepository;
            this._favoriteRepository = favoriteRepository;
            this._newsLetterSubscriberRepository = newsLetterSubscriberRepository;
            this._raveReviewRepository = raveReviewRepository;
            this._orderrepository = _orderrepository;
            this._companyVoucherReedemRepository = companyVoucherReedemRepository;
            this._companycontactRoleRepo = _companycontactRoleRepo;
            this._SystemUserRepository = _SystemUserRepository;
            this._IScopeVariableRepository = IScopeVariableRepository;
            this._companyDomainRepository = companyDomainRepository;
            this._listingBulletPontRepository = _listingBulletPontRepository;
            this._AssestsRepository = _AssestsRepository;
            this._FolderRepository = _FolderRepository;
            this._AssetItemsRepository = _AssetItemsRepository;
            this._templaterepository = _templaterepository;
            this._ReportRepository = _ReportRepository;
        }

        #endregion

        #region Public
        /// <summary>
        /// Resolves the Company/Stores by the companyid and organizationid
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>

        public MyCompanyDomainBaseReponse GetStoreFromCache(long companyId)
        {
            try
            {
                string CacheKeyName = "CompanyBaseResponse";
                ObjectCache cache = MemoryCache.Default;
                CacheItemPolicy policy = null;

                MyCompanyDomainBaseReponse responseObject = cache.Get(CacheKeyName) as MyCompanyDomainBaseReponse;

                policy = new CacheItemPolicy();
                policy.Priority = CacheItemPriority.NotRemovable;
               
                //policy.SlidingExpiration =
                //    TimeSpan.FromMinutes(5);
                policy.RemovedCallback = null;

                Dictionary<long, MyCompanyDomainBaseReponse> stores = cache.Get(CacheKeyName) as Dictionary<long, MyCompanyDomainBaseReponse>;

                if (stores == null)
                {
                    stores = new Dictionary<long, MyCompanyDomainBaseReponse>();


                    List<CmsPageModel> AllPages = _cmsPageRepositary.GetSystemPagesAndSecondaryPages(companyId);

                    Company oCompany = GetCompanyByCompanyID(companyId);
                    if (oCompany != null)
                    {
                        CacheEntryRemovedCallback callback = null;
                        MyCompanyDomainBaseReponse oStore = new MyCompanyDomainBaseReponse();
                        oStore.Company = oCompany;
                        oStore.Organisation = _organisationRepository.GetOrganizatiobByID(Convert.ToInt64(oCompany.OrganisationId));
                        oStore.CmsSkinPageWidgets = _widgetRepository.GetDomainWidgetsById(oCompany.CompanyId);
                        oStore.Banners = _companyBannerRepository.GetCompanyBannersById(Convert.ToInt64(oCompany.ActiveBannerSetId));
                        oStore.SystemPages = AllPages.Where(s => s.isUserDefined == false).ToList();
                        oStore.SecondaryPages = AllPages.Where(s => s.isUserDefined == true).ToList();
                        oStore.PageCategories = _pageCategoryRepositary.GetCmsSecondaryPageCategories();
                        oStore.Currency = _currencyRepository.GetCurrencySymbolById(Convert.ToInt64(oStore.Organisation.CurrencyId));
                        oStore.ResourceFile = _globalLanguageRepository.GetResourceFileByOrganisationId(Convert.ToInt64(oCompany.OrganisationId));
                        oStore.StoreDetaultAddress = GetDefaultAddressByStoreID(companyId);
                        stores.Add(oCompany.CompanyId, oStore);



                        cache.Set(CacheKeyName, stores, policy);
                        return stores[oCompany.CompanyId];
                    }
                    else 
                    {
                        return null;
                    }

                }
                else // there are some stores already in cache.
                {

                    if (!stores.ContainsKey(companyId))
                    {
                        Company oCompany = GetCompanyByCompanyID(companyId);

                        List<CmsPageModel> AllPages = _cmsPageRepositary.GetSystemPagesAndSecondaryPages(oCompany.CompanyId);

                        MyCompanyDomainBaseReponse oStore = new MyCompanyDomainBaseReponse();


                        oStore.Company = oCompany;
                        oStore.Organisation = _organisationRepository.GetOrganizatiobByID(Convert.ToInt64(oCompany.OrganisationId));
                        oStore.CmsSkinPageWidgets = _widgetRepository.GetDomainWidgetsById(oCompany.CompanyId);
                        oStore.Banners = _companyBannerRepository.GetCompanyBannersById(Convert.ToInt64(oCompany.ActiveBannerSetId));
                        oStore.SystemPages = AllPages.Where(s => s.isUserDefined == false).ToList();
                        oStore.SecondaryPages = AllPages.Where(s => s.isUserDefined == true).ToList();
                        oStore.PageCategories = _pageCategoryRepositary.GetCmsSecondaryPageCategories();
                        oStore.Currency = _currencyRepository.GetCurrencySymbolById(Convert.ToInt64(oCompany.OrganisationId));
                        oStore.ResourceFile = _globalLanguageRepository.GetResourceFileByOrganisationId(Convert.ToInt64(oCompany.OrganisationId));
                        oStore.StoreDetaultAddress = GetDefaultAddressByStoreID(companyId);
                        stores.Add(oCompany.CompanyId, oStore);
                        cache.Set(CacheKeyName, stores, policy);
                        return stores[oCompany.CompanyId];
                    }
                    else
                    {
                        return stores[companyId];
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void GetStoreFromCache(long companyId, bool clearcache)
        {

            try
            {
                string CacheKeyName = "CompanyBaseResponse";
                ObjectCache cache = MemoryCache.Default;
                CacheItemPolicy policy = null;

                // MyCompanyDomainBaseReponse responseObject = cache.Get(CacheKeyName) as MyCompanyDomainBaseReponse;

                policy = new CacheItemPolicy();
                policy.Priority = CacheItemPriority.NotRemovable;
                //policy.SlidingExpiration =
                //    TimeSpan.FromMinutes(5);
                policy.RemovedCallback = null;

                Dictionary<long, MyCompanyDomainBaseReponse> stores = cache.Get(CacheKeyName) as Dictionary<long, MyCompanyDomainBaseReponse>;
                if (stores.ContainsKey(companyId))
                {
                    stores.Remove(companyId);
                    stores = new Dictionary<long, MyCompanyDomainBaseReponse>();


                    List<CmsPageModel> AllPages = _cmsPageRepositary.GetSystemPagesAndSecondaryPages(companyId);

                    Company oCompany = GetCompanyByCompanyID(companyId);

                    CacheEntryRemovedCallback callback = null;

                    MyCompanyDomainBaseReponse oStore = new MyCompanyDomainBaseReponse();
                    oStore.Company = oCompany;
                    oStore.Organisation = _organisationRepository.GetOrganizatiobByID(Convert.ToInt64(oCompany.OrganisationId));
                    oStore.CmsSkinPageWidgets = _widgetRepository.GetDomainWidgetsById(oCompany.CompanyId);
                    oStore.Banners = _companyBannerRepository.GetCompanyBannersById(Convert.ToInt64(oCompany.ActiveBannerSetId));
                    oStore.SystemPages = AllPages.Where(s => s.isUserDefined == false).ToList();
                    oStore.SecondaryPages = AllPages.Where(s => s.isUserDefined == true).ToList();
                    oStore.PageCategories = _pageCategoryRepositary.GetCmsSecondaryPageCategories();
                    oStore.Currency = _currencyRepository.GetCurrencySymbolById(Convert.ToInt64(oStore.Organisation.CurrencyId));
                    oStore.ResourceFile = _globalLanguageRepository.GetResourceFileByOrganisationId(Convert.ToInt64(oCompany.OrganisationId));
                    oStore.StoreDetaultAddress = GetDefaultAddressByStoreID(companyId);
                    stores.Add(oCompany.CompanyId, oStore);
                    cache.Set(CacheKeyName, stores, policy);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public long GetStoreIdFromDomain(string domain)
        {
            try
            {
                return _CompanyRepository.GetStoreIdFromDomain(domain);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductCategory> GetCompanyParentCategoriesById(long companyId, long OrganisationId)
        {
            try
            {
                return _productCategoryRepository.GetParentCategoriesByStoreId(companyId, OrganisationId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public CompanyResponse GetAllCompaniesOfOrganisation(CompanyRequestModel request)
        {
            try
            {
                return _CompanyRepository.SearchCompanies(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public CompanyContact GetUserByEmailAndPassword(string email, string password)
        {
            try
            {
                return _CompanyContactRepository.GetContactUser(email, password);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public CompanyContact GetContactByFirstName(string FName, long StoreId, long OrganisationId, int WebStoreMode, string providerKey)
        {
            try
            {
                return _CompanyContactRepository.GetContactByFirstName(FName, StoreId, OrganisationId, WebStoreMode, providerKey);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public CompanyContact GetContactByEmail(string Email, long OrganisationID, long StoreId)
        {
            try
            {
                return _CompanyContactRepository.GetContactByEmail(Email, OrganisationID, StoreId);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public long CreateContact(CompanyContact Contact, string Name, long OrganizationID, int CustomerType, string TwitterScreanName, long SaleAndOrderManagerID, long StoreID)
        {
            try
            {
                return _CompanyContactRepository.CreateContact(Contact, Name, OrganizationID, CustomerType, TwitterScreanName, SaleAndOrderManagerID, StoreID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public Company GetCompanyByCompanyID(Int64 CompanyID)
        {
            try
            {
                return _CompanyRepository.GetStoreById(CompanyID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public Company GetStoreReceiptPage(long companyId)
        {
            try
            {
                return _CompanyRepository.GetStoreReceiptPage(companyId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CompanyContact GetContactByID(Int64 ContactID)
        {
            try
            {
                return _CompanyContactRepository.GetContactByID(ContactID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        
        public CompanyContact CreateCorporateContact(long CustomerId, CompanyContact regContact, string TwitterScreenName, long OrganisationId)
        {
            try
            {
                return _CompanyContactRepository.CreateCorporateContact(CustomerId, regContact, TwitterScreenName, OrganisationId, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string GetUiCulture(long organisationId)
        {
            try
            {
                return _globalLanguageRepository.GetLanguageCodeById(organisationId);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public CompanyContact GetContactByEmailAndMode(string Email, int Type, long customerID)
        {
            try
            {
                return _CompanyContactRepository.GetContactByEmailAndMode(Email, Type, customerID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public string GeneratePasswordHash(string plainText)
        {
            try
            {
                return _CompanyContactRepository.GeneratePasswordHash(plainText);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void UpdateUserPassword(int userId, string pass)
        {
            try
            {
                _CompanyContactRepository.UpdateUserPassword(userId, pass);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public SystemUser GetSystemUserById(Guid SystemUserId)
        {
            try
            {
                return _systemUserRepository.GetUserrById(SystemUserId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool AddMsgToTblQueue(string Toemail, string CC, string ToName, string msgbody, string fromName, string fromEmail, string smtpUserName, string ServerPass, string ServerName, string subject, List<string> AttachmentList, int CampaignReportID)
        {
            try
            {
                return _campaignRepository.AddMsgToTblQueue(Toemail, CC, ToName, msgbody, fromName, fromEmail, smtpUserName, ServerPass, ServerName, subject, AttachmentList, CampaignReportID);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public List<ProductCategory> GetAllParentCorporateCatalog(int customerId)
        {
            try
            {
                return _productCategoryRepository.GetAllParentCorporateCatalog(customerId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductCategory> GetAllParentCorporateCatalogByTerritory(int customerId, int ContactId)
        {
            try
            {
                return _productCategoryRepository.GetAllParentCorporateCatalogByTerritory(customerId, ContactId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductCategory> GetStoreParentCategories(long companyId, long OrganisationId)
        {
            try
            {
                return _productCategoryRepository.GetParentCategoriesByStoreId(companyId, OrganisationId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

         }
        public List<ProductCategory> GetAllCategories(long companyId, long OrganisationId)
        {
            try
            {
                return _productCategoryRepository.GetAllCategoriesByStoreId(companyId, OrganisationId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public CompanyContact GetCorporateUserByEmailAndPassword(string email, string password, long companyId,long OrganisationId)
        {
            try
            {
                return _CompanyContactRepository.GetCorporateUser(email, password, companyId,OrganisationId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ProductCategory GetCategoryById(long categoryId)
        {
            try
            {
                return _productCategoryRepository.GetCategoryById(categoryId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<ProductCategory> GetChildCategories(long categoryId, long CompanyId)
        {
            try
            {
                return _productCategoryRepository.GetChildCategories(categoryId, CompanyId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<ProductCategory> GetAllChildCorporateCatalogByTerritory(long customerId, long ContactId, long ParentCatId)
        {
            try
            {
                return _productCategoryRepository.GetAllChildCorporateCatalogByTerritory(customerId, ContactId, ParentCatId);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public string[] CreatePageMetaTags(string MetaTitle, string metaDesc, string metaKeyword, string StoreName, Address address = null)
        {
            try
            {
                this.pageTitle = "";
                this.MetaKeywords = "";
                this.MetaDEsc = "";
                if (address != null)
                {
                    this.pageTitle = MetaTitle + " - " + StoreName + ", " + address.City + ", " + address.State;
                    this.MetaKeywords = metaKeyword + ", " + address.City + ", " + address.State + ", " + address.Country + "," + address.PostCode;

                    if (!string.IsNullOrEmpty(metaDesc))
                    {
                        if (metaDesc.Length > 156)
                        {
                            this.MetaDEsc = metaDesc.Substring(0, 156) + " - " + StoreName + ", " + address.City + ", " + address.State;
                        }
                        else
                        {
                            this.MetaDEsc = metaDesc + " - " + StoreName + ", " + address.City + ", " + address.State;
                        }
                    }
                }
                else
                {
                    this.pageTitle = MetaTitle;
                    this.MetaKeywords = metaKeyword;
                    if (!string.IsNullOrEmpty(metaDesc))
                    {
                        if (metaDesc.Length > 156)
                        {
                            this.MetaDEsc = metaDesc.Substring(0, 156);
                        }
                        else
                        {
                            this.MetaDEsc = metaDesc + " - " + StoreName;
                        }
                    }
                }

                return new[] { pageTitle, MetaKeywords, MetaDEsc };
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Address GetDefaultAddressByStoreID(Int64 StoreID)
        {
            try
            {
                return _addressRepository.GetDefaultAddressByStoreID(StoreID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<GetCategoryProduct> GetRetailOrCorpPublishedProducts(long ProductCategoryID)
        {
            try
            {
                return _itemRepository.GetRetailOrCorpPublishedProducts(ProductCategoryID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ItemStockOption GetFirstStockOptByItemID(long ItemId, long CompanyId)
        {
            try
            {
                return _itemRepository.GetFirstStockOptByItemID(ItemId, CompanyId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<ItemPriceMatrix> GetPriceMatrixByItemID(int ItemId)
        {
            try
            {
                return _itemRepository.GetPriceMatrixByItemID(ItemId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //public string FormatDecimalValueToTwoDecimal(string valueToFormat)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(valueToFormat))
        //        {
        //            return string.Format("{0:n}", Math.Round(Convert.ToDouble(valueToFormat, CultureInfo.CurrentCulture), 2));
        //        }
        //        else
        //        {
        //            return "";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        public double CalculateVATOnPrice(double ActualPrice, double TaxValue)
        {
            try
            {
                double Price = ActualPrice + ((ActualPrice * TaxValue) / 100);
                return Price;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public double CalculateDiscount(double price, double discountPrecentage)
        {
            try
            {
                return (price - (price * (discountPrecentage / 100)));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public long CreateCustomer(string name, bool isEmailSubScription, bool isNewsLetterSubscription, CompanyTypes customerType, string RegWithTwitter, long OrganisationId, long StoreId, CompanyContact regContact = null)
        {
            try
            {
                return _CompanyRepository.CreateCustomer(name, isEmailSubScription, isNewsLetterSubscription, customerType, RegWithTwitter, OrganisationId, StoreId, regContact);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Organisation GetOrganisatonById(long OrganisationId)
        {
            try
            {
                return _organisationRepository.GetOrganizatiobByID(OrganisationId);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public string GetContactMobile(long CID)
        {
            try
            {
                return _CompanyContactRepository.GetContactMobile(CID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public CmsPage getPageByID(long PageID)
        {
            try
            {
                return _cmsPageRepositary.getPageByID(PageID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool canContactPlaceOrder(long contactID, out bool hasWebAccess)
        {
            try
            {
                return _CompanyContactRepository.canContactPlaceOrder(contactID, out hasWebAccess);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public string GetCountryNameById(long CountryId)
        {
            try
            {
                return _countryRepository.GetCountryNameById(CountryId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        /// <summary>
        /// Gets the count of users register against a company by its id
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public int GetContactCountByCompanyId(long CompanyId)
        {
            try
            {
                return _CompanyContactRepository.GetContactCountByCompanyId(CompanyId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets favorite design count Of a login user to display on dashboard
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        public int GetFavDesignCountByContactId(long contactId)
        {
            try
            {
                return _favoriteRepository.GetFavDesignCountByContactId(contactId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets the contact orders count by Status
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public int GetOrdersCountByStatus(long contactId, OrderStatus statusId)
        {
            try
            {
                return _CompanyContactRepository.GetOrdersCountByStatus(contactId, statusId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Gets pending approval orders count
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="isApprover"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public int GetPendingOrdersCountByTerritory(long companyId, OrderStatus statusId, int TerritoryID)
        {
            try
            {
                return _CompanyContactRepository.GetPendingOrdersCountByTerritory(companyId, statusId, TerritoryID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets all pending approval orders count for corporate customers
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="isApprover"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public int GetAllPendingOrders(long CompanyId, OrderStatus statusId)
        {

            try
            {
                return _CompanyContactRepository.GetAllPendingOrders(CompanyId, statusId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get all orders count placed against a company
        /// </summary>
        /// <param name="CCID"></param>
        /// <returns></returns>
        public int GetAllOrdersCount(long CompanyId)
        {
            try
            {

                return _CompanyContactRepository.GetAllOrdersCount(CompanyId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets login user orders count which are placed and not archieved
        /// </summary>
        /// <param name="CID"></param>
        /// <param name="CCID"></param>
        /// <returns></returns>
        public int AllOrders(long contactID, long CompanyID)
        {
            try
            {

                return _CompanyContactRepository.AllOrders(contactID, CompanyID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get retail user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public CompanyContact GetRetailUser(string email, string password, long OrganisationId, long StoreId)
        {
            return _CompanyContactRepository.GetRetailUser(email, password, OrganisationId, StoreId);
        }
        public Address GetAddressByID(long AddressID)
        {
            try
            {
                return _addressRepository.GetAddressByID(AddressID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CompanyContact GetCorporateAdmin(long contactCompanyId)
        {
            try
            {
                return _CompanyContactRepository.GetCorporateAdmin(contactCompanyId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Address> GetAddressByCompanyID(long companyID)
        {
            try
            {
                return _addressRepository.GetAddressByCompanyID(companyID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CompanyTerritory GetTerritoryById(long territoryId)
        {
            try
            {
                return _CompanyTerritoryRepository.GetTerritoryById(territoryId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Address> GetAdressesByContactID(long contactID)
        {
            try
            {
                return _addressRepository.GetAdressesByContactID(contactID);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public List<Address> GetBillingAndShippingAddresses(long TerritoryID)
        {
            try
            {
                return _addressRepository.GetBillingAndShippingAddresses(TerritoryID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Address> GetContactCompanyAddressesList(long customerID)
        {
            try
            {
                return _addressRepository.GetContactCompanyAddressesList(customerID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public State GetStateFromStateID(long StateID)
        {
            try
            {
                return _StateRepository.GetStateFromStateID(StateID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetStateNameById(long StateId)
        {
            try
            {
                return _StateRepository.GetStateNameById(StateId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public long GetContactTerritoryID(long CID)
        {
            try
            {
                return _CompanyContactRepository.GetContactTerritoryID(CID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Address> GetContactCompanyAddressesList(long BillingAddressId, long ShippingAddressid, long PickUpAddressId)
        {
            try
            {
                return _addressRepository.GetContactCompanyAddressesList(BillingAddressId, ShippingAddressid, PickUpAddressId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public long GetContactAddressID(long cID)
        {
            try
            {
                return _CompanyContactRepository.GetContactAddressID(cID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetStateCodeById(long stateId)
        {
            try
            {
                return _StateRepository.GetStateCodeById(stateId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>

        public long GetContactIdByCompanyId(long CompanyId)
        {
            try
            {
                return _CompanyContactRepository.GetContactIdByCustomrID(CompanyId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public string GetCountryCodeById(long countryId)
        {
            try
            {
                return _countryRepository.GetCountryCodeById(countryId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// get contact list by role and company id
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="Role"></param>
        /// <returns></returns>
        public long GetContactIdByRole(long CompanyID, int Role)
        {
            try
            {
                return _CompanyContactRepository.GetContactIdByRole(CompanyID, Role);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public string SystemWeight(long OrganisationID)
        {
            try
            {
                return _CompanyRepository.SystemWeight(OrganisationID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string SystemLength(long OrganisationID)
        {
            try
            {
                return _CompanyRepository.SystemLength(OrganisationID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CompanyTerritory GetCcompanyByTerritoryID(Int64 ContactId)
        {
            try
            {
                return _CompanyContactRepository.GetCcompanyByTerritoryID(ContactId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateCompanyOrderingPolicy(Company Instance)
        {

            _CompanyRepository.Update(Instance);
            _CompanyRepository.SaveChanges();
        }

        public void UpdateContactCompany(CompanyContact Instance)
        {
            _CompanyContactRepository.Update(Instance);
            _CompanyContactRepository.SaveChanges();
        }

        public bool UpdateCompanyContactForRetail(CompanyContact Instance)
        {
            try
            {
                return _CompanyContactRepository.UpdateCompanyContactForRetail(Instance);
            }
            catch (Exception ex)
            {
                throw ex;
            }




        }
        public bool UpdateCompanyContactForCorporate(CompanyContact Instance)
        {
            try
            {
                return _CompanyContactRepository.UpdateCompanyContactForCorporate(Instance);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        #endregion

        public bool UpdateCompanyName(Company Instance)
        {
            try
            {
                return _CompanyRepository.UpdateCompanyName(Instance);
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }
        public CompanyContact GetContactById(int contactId)
        {
            return _CompanyContactRepository.GetContactById(contactId);
        }



        public Company GetCustomer(int CompanyId)
        {
            return _CompanyRepository.GetCustomer(CompanyId);
        }

        public bool VerifyHashSha1(string plainText, string compareWithSalt)
        {
            try
            {
                return HashingManager.VerifyHashSha1(plainText, compareWithSalt);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string GetPasswordByContactID(long ContactID)
        {
            try
            {
                return _CompanyContactRepository.GetPasswordByContactID(ContactID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool SaveResetPassword(long ContactID, string Password)
        {
            try
            {
                return _CompanyContactRepository.SaveResetPassword(ContactID, Password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CmsSkinPageWidget> GetStoreWidgets(long CompanyId)
        {
            try
            {
                return _widgetRepository.GetDomainWidgetsById2(CompanyId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Address> GetAddressesListByContactCompanyID(long contactCompanyId)
        {
            return _addressRepository.GetAddressesListByContactCompanyID(contactCompanyId);
        }
        public List<Address> GetsearchedAddress(long CompanyId, String searchtxt)
        {
            return _addressRepository.GetsearchedAddress(CompanyId, searchtxt);
        }

        public bool UpdateBillingShippingAdd(Address Model)
        {
            return _addressRepository.UpdateBillingShippingAdd(Model);

        }
        public bool AddressNameExist(Address address)
        {
            return _addressRepository.AddressNameExist(address);
        }
        public bool AddAddBillingShippingAdd(Address Address)
        {
            return _addressRepository.AddAddBillingShippingAdd(Address);
        }
        public void ResetDefaultShippingAddress(Address address)
        {
            _addressRepository.ResetDefaultShippingAddress(address);

        }
        public List<State> GetCountryStates(long CountryId)
        {
            return _addressRepository.GetCountryStates(CountryId);

        }
        public Country GetCountryByCountryID(long CountryID)
        {
            return _addressRepository.GetCountryByCountryID(CountryID);
        }
        public State GetStateByStateID(long StateID)
        {
            return _addressRepository.GetStateByStateID(StateID);
        }
        public List<Country> GetAllCountries()
        {
            return _addressRepository.GetAllCountries();
        }
        public List<State> GetAllStates()
        {
            return _addressRepository.GetAllStates();
        }
        public CompanyContact GetContactByEmailID(string Email)
        {
            return _CompanyContactRepository.GetContactByEmailID(Email);
        }

        public NewsLetterSubscriber GetSubscriber(string email, long CompanyId)
        {
            try
            {
                return _newsLetterSubscriberRepository.GetSubscriber(email, CompanyId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int AddSubscriber(NewsLetterSubscriber subsriber)
        {
            try
            {
                return _newsLetterSubscriberRepository.AddSubscriber(subsriber);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool UpdateSubscriber(string subscriptionCode, SubscriberStatus status)
        {
            try
            {
                return _newsLetterSubscriberRepository.UpdateSubscriber(subscriptionCode, status);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public RaveReview GetRaveReview()
        {
            try
            {
                return _raveReviewRepository.GetRaveReview();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<Order> GetPendingApprovelOrdersList(long contactUserID, bool isApprover, long companyId)
        {
            return _orderrepository.GetPendingApprovelOrdersList(contactUserID, isApprover, companyId);
        }
        public CompanyContact GetOrCreateContact(Company company, string ContactEmail, string ContactFirstName, string ContactLastName, string CompanyWebAccessCode)
        {
            bool isValid = false;

            CompanyContact ContactRecord = null;

            if(company.IsCustomer == (int)StoreMode.Corp)
            {
                ContactRecord = _CompanyContactRepository.GetCorporateContactForAutoLogin(ContactEmail, Convert.ToInt64(company.OrganisationId), company.CompanyId);
                if(company.isAllowRegistrationFromWeb == true)
                {
                    if (ContactRecord == null) // contact already exists...
                    {
                        ContactRecord = new CompanyContact();
                        ContactRecord.FirstName = ContactFirstName;
                        ContactRecord.LastName = ContactLastName;
                        ContactRecord.Email = ContactEmail;
                        ContactRecord.OrganisationId = company.OrganisationId;
                        ContactRecord.Notes = "Temporary Password = guest";
                        ContactRecord.Password = "guest";

                        ContactRecord = _CompanyContactRepository.CreateCorporateContact(company.CompanyId, ContactRecord, "", Convert.ToInt64(company.OrganisationId), true);


                    }
                }
                
            }
            else if (company.IsCustomer == (int)StoreMode.Retail)
            {
                ContactRecord = _CompanyContactRepository.GetContactByEmail(ContactEmail, Convert.ToInt64(company.OrganisationId), company.CompanyId);
                if (ContactRecord == null) // contact already exists...
                {
                    ContactRecord = new CompanyContact();
                    ContactRecord.FirstName = ContactFirstName;
                    ContactRecord.LastName = ContactLastName;
                    ContactRecord.Email = ContactEmail;
                    ContactRecord.OrganisationId = company.OrganisationId;
                    ContactRecord.Notes = "Temporary Password = guest";
                    ContactRecord.Password = "guest";

                    long CompanyID = _CompanyRepository.CreateCustomer(ContactFirstName, true, true, CompanyTypes.SalesCustomer, "", Convert.ToInt64(company.OrganisationId), company.CompanyId, ContactRecord);
                    ContactRecord = _CompanyContactRepository.GetContactsByCompanyId(CompanyID).FirstOrDefault();
                    

                }
            }
            
            
            return ContactRecord;
        }
        public long ApproveOrRejectOrder(long orderID, long loggedInContactID, OrderStatus orderStatus, Guid OrdermangerID,string RejectionReason, string BrokerPO = "")
        {
            return _orderrepository.ApproveOrRejectOrder(orderID, loggedInContactID, orderStatus, OrdermangerID, RejectionReason);
        }

        /// <summary>
        /// Check web access code exists
        /// </summary>
        /// <param name="subscriptionCode"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public Company isValidWebAccessCode(string WebAccessCode, long OrganisationId)
        {
            return _CompanyRepository.isValidWebAccessCode(WebAccessCode, OrganisationId);
        }

        public CompanyContact GetCorporateContactForAutoLogin(string emailAddress, long organistionId, long companyId)
        {
            return _CompanyContactRepository.GetCorporateContactForAutoLogin(emailAddress, organistionId, companyId);
        }

        public List<ProductCategory> GetAllRetailPublishedCat()
        {
            return _productCategoryRepository.GetAllRetailPublishedCat();
        }
        public List<ProductCategory> GetAllCategories()
        {
           return _productCategoryRepository.GetAllCategories();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        public string GetCurrencyCodeById(long currencyId)
        {
            return _currencyRepository.GetCurrencyCodeById(currencyId);
        }
        public List<CompanyContact> GetCompanyAdminByCompanyId(long CompanyId)
        {
            return _CompanyContactRepository.GetCompanyAdminByCompanyId(CompanyId);
        }
        public CompanyContact GetCorporateContactByEmail(string Email, long OID, long StoreId)
        {
            return _CompanyContactRepository.GetCorporateContactByEmail(Email, OID, StoreId);
        }

        public string OrderConfirmationPDF(long OrderId, long StoreId)
        {
            Doc theDoc = new Doc();
            try
            {
               

                string URl = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority + "/ReceiptPlain?OrderId=" + OrderId + "&StoreId=" + StoreId + "&IsPrintReceipt=0";

                string FileName = OrderId + "_OrderReceipt.pdf";
                string FilePath = System.Web.HttpContext.Current.Server.MapPath("~/mpc_content/EmailAttachments/" + FileName);
                string AttachmentPath = "/mpc_content/EmailAttachments/" + FileName;
                
                    string AddGeckoKey = ConfigurationManager.AppSettings["AddEngineTypeGecko"];
                    if (AddGeckoKey == "1")
                    {
                        theDoc.HtmlOptions.Engine = EngineType.Gecko;
                    }

                    theDoc.FontSize = 22;
                    int objid = theDoc.AddImageUrl(URl);


                    while (true)
                    {
                        theDoc.FrameRect();
                        if (!theDoc.Chainable(objid))
                            break;
                        theDoc.Page = theDoc.AddPage();
                        objid = theDoc.AddImageToChain(objid);
                    }
                    string physicalFolderPath = System.Web.HttpContext.Current.Server.MapPath("~/mpc_content/EmailAttachments/");
                    if (!Directory.Exists(physicalFolderPath))
                        Directory.CreateDirectory(physicalFolderPath);
                    theDoc.Save(FilePath);
                    theDoc.Clear();
               
                if (System.IO.File.Exists(FilePath))
                    return AttachmentPath;
                else
                    return null;
            }
            catch (Exception e)
            {
                theDoc.Clear();
                string virtualFolderPth = System.Web.HttpContext.Current.Server.MapPath("~/mpc_content/Exception/ErrorLog.txt");

                using (StreamWriter writer = new StreamWriter(virtualFolderPth, true))
                {
                    writer.WriteLine("Message :" + e.Message + "<br/>" + Environment.NewLine + "StackTrace :" + e.StackTrace +
                       "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                }
                throw e;
                return null;
            }
        }

        public int GetSavedDesignCountByContactId(long ContactID)
        {
            return _itemRepository.GetSavedDesignCountByContactId(ContactID);
        }
        public double? GetOrderTotalById(long OrderId)
        {
            return _orderrepository.GetOrderTotalById(OrderId);
            }
        public bool IsVoucherUsedByCustomer(long contactId, long companyId, long DiscountVoucherId)
        {
            try
            {
                return _companyVoucherReedemRepository.IsVoucherUsedByCustomer(contactId, companyId, DiscountVoucherId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void AddReedem(long contactId, long companyId, long DiscountVoucherId)
        {
            try
            {
                _companyVoucherReedemRepository.AddReedem(contactId, companyId, DiscountVoucherId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public MyCompanyDomainBaseReponse GetStoreCachedObject(long StoreId)
        {
            MyCompanyDomainBaseReponse StoreCachedData = null;

            ObjectCache cache = MemoryCache.Default;

            Dictionary<long, MyCompanyDomainBaseReponse> cachedObject = (cache.Get("CompanyBaseResponse")) as Dictionary<long, MyCompanyDomainBaseReponse>;

            if (cachedObject == null)
            {
                if (StoreId > 0)
                {
                    StoreCachedData = GetStoreFromCache(StoreId);

                }
                else
                {
                    //TempData["ErrorMessage"] = "Your session is expired. Please re-enter your domain URL.";
                    //RedirectToAction("Error");
                }
            }
            else
            {
                // if company not found in cache then rebuild the cache
                if (!cachedObject.ContainsKey(StoreId))
                {
                    StoreCachedData = GetStoreFromCache(StoreId);
                }
                else
                {
                    StoreCachedData = cachedObject.Where(i => i.Key == StoreId).FirstOrDefault().Value;
                }
            }
            return StoreCachedData;
        }
        public IEnumerable<CompanyTerritory> GetAllCompanyTerritories(long companyId)
        {
           return  _CompanyTerritoryRepository.GetAllCompanyTerritories(companyId);
        
        }
        public IEnumerable<RegistrationQuestion> GetAllQuestions()
        {
            return _questionRepository.GetAll();
        }
        public List<CompanyContactRole> GetContactRolesExceptAdmin(int AdminRole)
        {
            return _companycontactRoleRepo.GetContactRolesExceptAdmin(AdminRole);
        }
        public List<CompanyContactRole> GetAllContactRoles()
        {
            return _companycontactRoleRepo.GetAllContactRoles();
        }
        public List<CompanyContact> GetSearched_Contacts(long contactCompanyId, String searchtxt, long territoryID)
        {
            return _CompanyContactRepository.GetSearched_Contacts(contactCompanyId, searchtxt, territoryID);
        }
        public List<CompanyContact> GetContactsByTerritory(long contactCompanyId, long territoryID)
        {
            return _CompanyContactRepository.GetContactsByTerritory(contactCompanyId, territoryID);
        }
        public List<ProductItem> GetAllRetailDisplayProductsQuickCalc(long CompanyID)
        {
            return _itemRepository.GetAllRetailDisplayProductsQuickCalc(CompanyID);
        }
        public List<ItemPriceMatrix> GetRetailProductsPriceMatrix(long CompanyID)
        {
            return _itemRepository.GetRetailProductsPriceMatrix(CompanyID);
        }
        public RegistrationQuestion GetSecretQuestionByID(int QuestionID)
        {
            return _questionRepository.GetSecretQuestionByID(QuestionID);
        }
        public CompanyContactRole GetRoleByID(int RoleID)
        {
            return _companycontactRoleRepo.GetRoleByID(RoleID);
        }
        public void UpdateDataSystemUser(CompanyContact Contact)
        {
            _CompanyContactRepository.UpdateDataSystemUser(Contact);
        }
        public void AddDataSystemUser(CompanyContact Contact)
        {
            _CompanyContactRepository.AddDataSystemUser(Contact);
            AddScopeVariables(Contact.ContactId, Contact.CompanyId);
        }
        public List<Address> GetAddressesByTerritoryID(Int64 TerritoryID)
        {
            try
            {
                return _addressRepository.GetAddressesByTerritoryID(TerritoryID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ShowPricesOnStore(int storeModeFromCookie, bool PriceFlagOfStore, long loginContactId, bool PriceFlagFromCookie)
        {
            if (PriceFlagOfStore == true)
            {
                if (storeModeFromCookie == (int)StoreMode.Corp)
                {
                    if (loginContactId > 0)
                    {
                        if (PriceFlagFromCookie == true)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (storeModeFromCookie == (int)StoreMode.Corp)
                {
                    if (loginContactId > 0)
                    {
                        if (PriceFlagFromCookie == true)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        public string GetCurrencySymbolById(long currencyId)
        {
            return _currencyRepository.GetCurrencySymbolById(currencyId);
        }

        public long OrganisationThroughSystemUserEmail(string Email)
        {
            return _SystemUserRepository.OrganisationThroughSystemUserEmail(Email);
        }

        public void DeleteItems(List<Item> ItemList)
        {
            foreach (var item in ItemList)
            {
                _itemRepository.Delete(item);
            }
            _itemRepository.SaveChanges();
        }

        public void AddScopeVariables(long ContactId, long StoreId) 
        {
            _IScopeVariableRepository.AddScopeVariables(ContactId, StoreId);
        }
        public long GetOrganisationIdByRequestUrl(string Url)
        {
            CompanyDomain domain = _companyDomainRepository.GetDomainByUrl(Url);
            if(domain != null)
            {
                return _CompanyRepository.GetOrganisationIdByCompanyId(domain.CompanyId);
            }
            return 0;
        }
        public CompanyContact GetContactBySocialNameAndEmail(string FName, long StoreId, long OrganisationId, int WebStoreMode, string Email)
        {
            try
            {
                return _CompanyContactRepository.GetContactBySocialNameAndEmail(FName, StoreId, OrganisationId, WebStoreMode, Email);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// if url is e.g crystalmediagroup.myprintcloud.com and in domain table there is no record with crystalmediagroup.myprintcloud.com then it will compare the 
        /// string and get the first record from domain table e.g. crystalmediagroup.myprintcloud.com/store/retail and crystalmediagroup.myprintcloud.com/store/corporate then it will return the first domain storeid
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public long GetFirstStoreByRequestUrl(string Url)
        {
            CompanyDomain domain = _companyDomainRepository.GetDomainByUrl(Url);
            if (domain != null)
            {
                return domain.CompanyId;
            }
            return 0;
        }

        public List<CompanyContact> GetUsersByCompanyId(long CompanyId)
        {
            return _CompanyContactRepository.GetUsersByCompanyId(CompanyId);
              
        }
        public List<CompanyContact> GetCorporateUserOnly( long companyId, long OrganisationId)
        {
            return _CompanyContactRepository.GetCorporateUserOnly(companyId, OrganisationId);
        }
        public MPC.Models.DomainModels.Listing GetListingByListingID(int propertyId)
        {
            return _listingRepository.GetListingByListingID(propertyId);
        }
        public long UpdateListing(MPC.Models.DomainModels.Listing propertyListing, MPC.Models.DomainModels.Listing tblListing)
        {
            return _listingRepository.UpdateListing(propertyListing, tblListing);
        }
        public void UpdateAgent(List<CompanyContact> model)
        {
             _CompanyContactRepository.UpdateAgent(model);
        }
        public void AddAgent(ListAgentMode model, long ContactCompanyId)
        {
            _CompanyContactRepository.AddAgent(model, ContactCompanyId);
        }
        public void UpdateBulletPoints(List<ListingBulletPoint> BulletPoints, long ListingId)
        {
            _listingBulletPontRepository.UpdateBulletPoints(BulletPoints, ListingId);
        }
        public void AddBulletPoint(List<ListingBulletPoint> model, long listingId)
        {
            _listingBulletPontRepository.AddBulletPoint(model, listingId);
        }
        public void UpdateSignleAgent(CompanyContact Agent)
        {
            _CompanyContactRepository.UpdateSignleAgent(Agent);
        }
        public void AddSingleAgent( CompanyContact NewAgent)
        {
            _CompanyContactRepository.AddSingleAgent( NewAgent);
        }
        public List<ListingImage> GetAllListingImages(long ListingID)
        {
            return _listingRepository.GetAllListingImages(ListingID);
        }
        public void ListingImage(ListingImage NewImage)
        {
            _listingRepository.ListingImage(NewImage);
        }
        public void DeleteListingImage(long listingImageID)
        {
            _listingRepository.DeleteListingImage(listingImageID);
        }
        public void DeleteAjent(long ContactID)
        {
            _CompanyContactRepository.DeleteAjent(ContactID);
        }
        public void AddSingleBulletPoint(ListingBulletPoint BullentPoint)
        {
            _listingBulletPontRepository.AddSingleBulletPoint(BullentPoint);
        }
        public void UpdateSingleBulletPoint(ListingBulletPoint BullentPoint)
        {
            _listingBulletPontRepository.UpdateSingleBulletPoint(BullentPoint);
        }
        public List<ListingBulletPoint> GetAllListingBulletPoints(long ListingID)
        {
           return _listingBulletPontRepository.GetAllListingBulletPoints(ListingID);
        }
        public void DeleteBulletPoint(long BulletPointId, long ListingId)
        {
            _listingBulletPontRepository.DeleteBulletPoint(BulletPointId, ListingId);
        }
        public long AddNewListing(MPC.Models.DomainModels.Listing propertyListing)
        {
          return  _listingRepository.AddNewListing(propertyListing);
        }
        public bool DeleteLisitngData(long ListingId)
        {
            return _listingRepository.DeleteLisitngData(ListingId);
        }
        public void AddlistingImages(long ListingId, List<ListingImage> Images)
        {
            _listingRepository.AddlistingImages(ListingId, Images);
        }
        public CompanyContact GetContactOnUserNamePass(long OrganisationId, string Email, string password)
        {
            return _CompanyContactRepository.GetContactOnUserNamePass(OrganisationId, Email, password);
        }

        public CompanyDomain GetDomainByCompanyId(long CompanyId)
        {
            return _companyDomainRepository.GetDomainByCompanyId(CompanyId);
        }
        public List<Asset> GetAssetsByCompanyID(long CompanyID)
        {
            return _AssestsRepository.GetAssetsByCompanyID(CompanyID);
        
        }
        public List<Folder> GetFoldersByCompanyId(long CompanyID, long OrganisationID)
        {
            return _FolderRepository.GetFoldersByCompanyId(CompanyID, OrganisationID);
        }
        public void DeleteAsset(long AssetID)
        {
            _AssestsRepository.DeleteAsset(AssetID);
        }
        public void UpdateAsset(Asset UpdatedAsset)
        {
            _AssestsRepository.UpdateAsset(UpdatedAsset);
        }
        public List<Folder> GetChildFolders(long ParentFolderId)
        {
            return _FolderRepository.GetChildFolders(ParentFolderId);
        }
        public long AddFolder(Folder NewFolder)
        {
            return _FolderRepository.AddFolder(NewFolder);
        }
       public bool UpdateImage(Folder folder)
        {
            return _FolderRepository.UpdateImage(folder);
        }
       public Folder GetFolderByFolderId(long FolderID)
       {
           return _FolderRepository.GetFolderByFolderId(FolderID);
       }
       public List<TreeViewNodeVM> GetTreeVeiwList(long CompanyId, long OrganisationId)
       {
           return _FolderRepository.GetTreeVeiwList(CompanyId, OrganisationId);
       }
       public List<Asset> GetAssetsByCompanyIDAndFolderID(long CompanyID, long FolderId)
       {
           return _AssestsRepository.GetAssetsByCompanyIDAndFolderID(CompanyID, FolderId);
       }
       public List<Folder> GetAllFolders(long CompanyID, long OrganisationID)
       {
           return _FolderRepository.GetAllFolders(CompanyID, OrganisationID);
       }
       public long AddAsset(Asset Asset)
       {
           return _AssestsRepository.AddAsset(Asset);
       }
       public void UpdateAssetImage(Asset Asset)
       {
            _AssestsRepository.UpdateAssetImage(Asset);
       }
       public bool AddAssetItems(List<AssetItem> AssetItemsList)
       {
           return _AssetItemsRepository.AddAssetItems(AssetItemsList);
          
       }
       public Asset GetAsset(long AssetId)
       {
           return _AssestsRepository.GetAsset(AssetId);
       }
       public void UpdateFolder(Folder Ufolder)
       {
           _FolderRepository.UpdateFolder(Ufolder);
       }
       public void DeleteFolder(long folderID)
       {
           _FolderRepository.DeleteFolder(folderID);
       }
       public bool UpdateTemplatePdfDimensions(Template Template)
       {
           return _templaterepository.UpdateTemplatePdfDimensions(Template);
       }
       public List<AssetItem> GetAssetItemsByAssetID(long AssetID)
       {
           return _AssetItemsRepository.GetAssetItemsByAssetID(AssetID);
       }
       public void RemoveAssetItem(long AssetID)
       {
           _AssetItemsRepository.RemoveAssetItem(AssetID);
       }
       public void RemoveAssetItems(List<AssetItem> RemoveAssetItemsIDs)
       {
           _AssetItemsRepository.RemoveAssetItems(RemoveAssetItemsIDs);
       }
       public string AssetItemFilePath(long AssetItemId)
       {
           return _AssetItemsRepository.AssetItemFilePath(AssetItemId);
       }
       public double GetTemplateCuttingMargin(long ProductId)
       {
           return _templaterepository.GetTemplateCuttingMargin(ProductId);
       }
       public ProductCategory GetlCategorieByName(long OrganisationId, long CompanyId, string CategoryName)
       {
           return _productCategoryRepository.GetlCategorieByName(OrganisationId, CompanyId, CategoryName);
       }
       public bool IsValidNumber(string cardNum)
       {
           try
           {
               return CreditCardManager.IsValidNumber(cardNum);
           }
           catch (Exception ex)
           {
               throw ex;
           }

       }
       public int GetCardTypeIdFromNumber(string cardNum)
       {
           try
           {
               return CreditCardManager.GetCardTypeIdFromNumber(cardNum);
           }
           catch (Exception ex)
           {
               throw ex;
           }

       }

       public long GetReportIdByName(string ReportName)
       {
           return _ReportRepository.GetReportIdByName(ReportName);
       
       }
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD

       public bool UpdateOderStatus(Estimate Estimate)
       {
           return _orderrepository.UpdateOderStatus(Estimate);
       }
       public bool UpdateItemsStatus(long EstimateId)
       {

           return _itemRepository.UpdateItemsStatus(EstimateId);
       }

       public bool UpdateOrderAndItemsForRejectOrder(long OrderId, long CartOrderId)
       {
           return _orderrepository.UpdateOrderAndItemsForRejectOrder(OrderId, CartOrderId);
       
=======
       public long GetStoreIdByCustomerId(long CustomerId) 
       {
           return _CompanyRepository.GetStoreIdByCustomerId(CustomerId);
>>>>>>> a185c3b17e3c940bafdf4c45fe8c13df2571d5b4
=======
       public long GetStoreIdByCustomerId(long CustomerId) 
       {
           return _CompanyRepository.GetStoreIdByCustomerId(CustomerId);
>>>>>>> a185c3b17e3c940bafdf4c45fe8c13df2571d5b4
=======
       public long GetStoreIdByCustomerId(long CustomerId) 
       {
           return _CompanyRepository.GetStoreIdByCustomerId(CustomerId);
>>>>>>> a185c3b17e3c940bafdf4c45fe8c13df2571d5b4
=======
       public long GetStoreIdByCustomerId(long CustomerId) 
       {
           return _CompanyRepository.GetStoreIdByCustomerId(CustomerId);
>>>>>>> a185c3b17e3c940bafdf4c45fe8c13df2571d5b4
       }
    }
}
