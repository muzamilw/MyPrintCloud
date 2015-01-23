
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using System;
using MPC.Models.Common;
using System.Globalization;

namespace MPC.Implementation.WebStoreServices
{
    public class CompanyService : ICompanyService
    {
        #region Private

        /// <summary>
        /// Private members
        /// </summary>
        public readonly ICompanyRepository _CompanyRepository;
        public readonly ICompanyContactRepository _CompanyContactRepository;

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
            , ICountryRepository countryRepository, IStateRepository stateRepository, IFavoriteDesignRepository favoriteRepository, IStateRepository StateRepository, ICompanyTerritoryRepository CompanyTerritoryRepository)
        {
            this._CompanyRepository = companyRepository;
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


            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;
            CacheItemPolicy policy = null;

            MyCompanyDomainBaseReponse responseObject = cache.Get(CacheKeyName) as MyCompanyDomainBaseReponse;

            policy = new CacheItemPolicy();
            policy.Priority = CacheItemPriority.NotRemovable;
            policy.SlidingExpiration =
                TimeSpan.FromMinutes(5);
            policy.RemovedCallback = null;

            Dictionary<long, MyCompanyDomainBaseReponse> stores = cache.Get(CacheKeyName) as Dictionary<long, MyCompanyDomainBaseReponse>;

            if (stores == null)
            {
                stores = new Dictionary<long, MyCompanyDomainBaseReponse>();


                List<CmsPageModel> AllPages = _cmsPageRepositary.GetSystemPagesAndSecondaryPages(companyId);

                Company oCompany = GetCompanyByCompanyID(companyId);

                CacheEntryRemovedCallback callback = null;

                MyCompanyDomainBaseReponse oStore = new MyCompanyDomainBaseReponse();
                oStore.Company = oCompany;
                oStore.Organisation = _organisationRepository.GetOrganizatiobByID(Convert.ToInt64(oCompany.OrganisationId));
                oStore.CmsSkinPageWidgets = _widgetRepository.GetDomainWidgetsById(oCompany.CompanyId);
                oStore.Banners = _companyBannerRepository.GetCompanyBannersById(oCompany.CompanyId);
                oStore.SystemPages = AllPages.Where(s => s.CompanyId == null).ToList();
                oStore.SecondaryPages = AllPages.Where(s => s.CompanyId == oCompany.CompanyId).ToList();
                oStore.PageCategories = _pageCategoryRepositary.GetCmsSecondaryPageCategories();
                oStore.Currency = _currencyRepository.GetCurrencyCodeById(Convert.ToInt64(oCompany.OrganisationId));
                oStore.ResourceFile = _globalLanguageRepository.GetResourceFileByOrganisationId(Convert.ToInt64(oCompany.OrganisationId));
                stores.Add(oCompany.CompanyId, oStore);


                cache.Set(CacheKeyName, stores, policy);
                return stores[oCompany.CompanyId];
            }
            else // there are some stores already in cache.
            {

                if (!stores.ContainsKey(companyId))
                {
                    Company oCompany = GetCompanyByCompanyID(companyId);

                    List<CmsPageModel> AllPages = _cmsPageRepositary.GetSystemPagesAndSecondaryPages(oCompany.CompanyId);

                    MyCompanyDomainBaseReponse oStore = new MyCompanyDomainBaseReponse();


                    oStore.Company = oCompany;
                    oStore.CmsSkinPageWidgets = _widgetRepository.GetDomainWidgetsById(oCompany.CompanyId);
                    oStore.Banners = _companyBannerRepository.GetCompanyBannersById(oCompany.CompanyId);
                    oStore.SystemPages = AllPages.Where(s => s.CompanyId == null).ToList();
                    oStore.SecondaryPages = AllPages.Where(s => s.CompanyId == oCompany.CompanyId).ToList();
                    oStore.PageCategories = _pageCategoryRepositary.GetCmsSecondaryPageCategories();
                    oStore.Currency = _currencyRepository.GetCurrencyCodeById(Convert.ToInt64(oCompany.OrganisationId));

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

        public void GetStoreFromCache(long companyId, bool clearcache)
        {


            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;
            CacheItemPolicy policy = null;

            MyCompanyDomainBaseReponse responseObject = cache.Get(CacheKeyName) as MyCompanyDomainBaseReponse;

            policy = new CacheItemPolicy();
            policy.Priority = CacheItemPriority.NotRemovable;
            policy.SlidingExpiration =
                TimeSpan.FromMinutes(5);
            policy.RemovedCallback = null;

            Dictionary<long, MyCompanyDomainBaseReponse> stores = cache.Get(CacheKeyName) as Dictionary<long, MyCompanyDomainBaseReponse>;
            responseObject = null;
            stores = null;
            if (stores == null)
            {
                stores = new Dictionary<long, MyCompanyDomainBaseReponse>();


                List<CmsPageModel> AllPages = _cmsPageRepositary.GetSystemPagesAndSecondaryPages(companyId);

                Company oCompany = GetCompanyByCompanyID(companyId);

                CacheEntryRemovedCallback callback = null;

                MyCompanyDomainBaseReponse oStore = new MyCompanyDomainBaseReponse();
                oStore.Company = oCompany;
                oStore.Organisation = _organisationRepository.GetOrganizatiobByID((int)oCompany.OrganisationId);
                oStore.CmsSkinPageWidgets = _widgetRepository.GetDomainWidgetsById(oCompany.CompanyId);
                oStore.Banners = _companyBannerRepository.GetCompanyBannersById(oCompany.CompanyId);
                oStore.SystemPages = AllPages.Where(s => s.CompanyId == null).ToList();
                oStore.SecondaryPages = AllPages.Where(s => s.CompanyId == oCompany.CompanyId).ToList();
                oStore.PageCategories = _pageCategoryRepositary.GetCmsSecondaryPageCategories();
                oStore.Currency = _currencyRepository.GetCurrencyCodeById(Convert.ToInt64(oCompany.OrganisationId));
                oStore.ResourceFile = _globalLanguageRepository.GetResourceFileByOrganisationId(Convert.ToInt64(oCompany.OrganisationId));
                stores.Add(oCompany.CompanyId, oStore);
                cache.Set(CacheKeyName, stores, policy);
            }
        }

        public long GetStoreIdFromDomain(string domain)
        {
            return _CompanyRepository.GetStoreIdFromDomain(domain);
        }

        public List<ProductCategory> GetCompanyParentCategoriesById(long companyId, long OrganisationId)
        {
            return _productCategoryRepository.GetParentCategoriesByStoreId(companyId, OrganisationId);
        }

        public CompanyResponse GetAllCompaniesOfOrganisation(CompanyRequestModel request)
        {
            return _CompanyRepository.SearchCompanies(request);
        }

        public CompanyContact GetUserByEmailAndPassword(string email, string password)
        {
            return _CompanyContactRepository.GetContactUser(email, password);
        }
        public CompanyContact GetContactByFirstName(string FName)
        {
            return _CompanyContactRepository.GetContactByFirstName(FName);
        }

        public CompanyContact GetContactByEmail(string Email)
        {
            return _CompanyContactRepository.GetContactByEmail(Email);
        }

        public long CreateContact(CompanyContact Contact, string Name, long OrganizationID, int CustomerType, string TwitterScreanName, long SaleAndOrderManagerID, long StoreID)
        {
            return _CompanyContactRepository.CreateContact(Contact, Name, OrganizationID, CustomerType, TwitterScreanName, SaleAndOrderManagerID, StoreID);
        }



        public Company GetCompanyByCompanyID(Int64 CompanyID)
        {
            return _CompanyRepository.GetStoreById(CompanyID);
        }

        public CompanyContact GetContactByID(Int64 ContactID)
        {
            return _CompanyContactRepository.GetContactByID(ContactID);
        }

        public List<Address> GetAddressesByTerritoryID(Int64 TerritoryID)
        {
            return _addressRepository.GetAddressesByTerritoryID(TerritoryID);
        }
        public CompanyContact CreateCorporateContact(int CustomerId, CompanyContact regContact, string TwitterScreenName)
        {

            return _CompanyContactRepository.CreateCorporateContact(CustomerId, regContact, TwitterScreenName);
        }

        public string GetUiCulture(long organisationId)
        {
            return _globalLanguageRepository.GetLanguageCodeById(organisationId);

        }

        public CompanyContact GetContactByEmailAndMode(string Email, int Type, int customerID)
        {
            return _CompanyContactRepository.GetContactByEmailAndMode(Email, Type, customerID);
        }


        public string GeneratePasswordHash(string plainText)
        {
            return _CompanyContactRepository.GeneratePasswordHash(plainText);
        }

        public void UpdateUserPassword(int userId, string pass)
        {
            _CompanyContactRepository.UpdateUserPassword(userId, pass);
        }
        public SystemUser GetSystemUserById(long SystemUserId)
        {
            return _systemUserRepository.GetSalesManagerById(SystemUserId);
        }


        public bool AddMsgToTblQueue(string Toemail, string CC, string ToName, string msgbody, string fromName, string fromEmail, string smtpUserName, string ServerPass, string ServerName, string subject, List<string> AttachmentList, int CampaignReportID)
        {
            return _campaignRepository.AddMsgToTblQueue(Toemail, CC, ToName, msgbody, fromName, fromEmail, smtpUserName, ServerPass, ServerName, subject, AttachmentList, CampaignReportID);

        }


        public List<ProductCategory> GetAllParentCorporateCatalog(int customerId)
        {
            return _productCategoryRepository.GetAllParentCorporateCatalog(customerId);
        }

        public List<ProductCategory> GetAllParentCorporateCatalogByTerritory(int customerId, int ContactId)
        {
            return _productCategoryRepository.GetAllParentCorporateCatalogByTerritory(customerId, ContactId);
        }

        public List<ProductCategory> GetStoreParentCategories(long companyId, long OrganisationId)
        {
            return _productCategoryRepository.GetParentCategoriesByStoreId(companyId, OrganisationId);
        }
        public List<ProductCategory> GetAllCategories(long companyId)
        {
            return _productCategoryRepository.GetAllCategoriesByStoreId(companyId);
        }

        public CompanyContact GetCorporateUserByEmailAndPassword(string email, string password, long companyId)
        {
            return _CompanyContactRepository.GetCorporateUser(email, password, companyId);
        }

        public ProductCategory GetCategoryById(int categoryId)
        {
            return _productCategoryRepository.GetCategoryById(categoryId);
        }

        public List<ProductCategory> GetChildCategories(int categoryId)
        {
            return _productCategoryRepository.GetChildCategories(categoryId);
        }

        public List<ProductCategory> GetAllChildCorporateCatalogByTerritory(int customerId, int ContactId, int ParentCatId)
        {
            return _productCategoryRepository.GetAllChildCorporateCatalogByTerritory(customerId, ContactId, ParentCatId);

        }


        public string[] CreatePageMetaTags(string MetaTitle, string metaDesc, string metaKeyword, StoreMode mode, string StoreName, Address address = null)
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

            return new[] { pageTitle, MetaKeywords, MetaDEsc };
        }

        public Address GetDefaultAddressByStoreID(Int64 StoreID)
        {
            return _addressRepository.GetDefaultAddressByStoreID(StoreID);
        }

        public List<GetCategoryProduct> GetRetailOrCorpPublishedProducts(int ProductCategoryID)
        {
            return _itemRepository.GetRetailOrCorpPublishedProducts(ProductCategoryID);
        }

        public ItemStockOption GetFirstStockOptByItemID(int ItemId, int CompanyId)
        {
            return _itemRepository.GetFirstStockOptByItemID(ItemId, CompanyId);
        }

        public List<ItemPriceMatrix> GetPriceMatrixByItemID(int ItemId)
        {
            return _itemRepository.GetPriceMatrixByItemID(ItemId);
        }

        public string FormatDecimalValueToTwoDecimal(string valueToFormat)
        {
            if (!string.IsNullOrEmpty(valueToFormat))
            {
                return string.Format("{0:n}", Math.Round(Convert.ToDouble(valueToFormat, CultureInfo.CurrentCulture), 2));
            }
            else
            {
                return "";
            }

        }
        public double CalculateVATOnPrice(double ActualPrice, double TaxValue)
        {
            double Price = ActualPrice + ((ActualPrice * TaxValue) / 100);
            return Price;
        }

        public double CalculateDiscount(double price, double discountPrecentage)
        {
            return (price - (price * (discountPrecentage / 100)));
        }

        public long CreateCustomer(string name, bool isEmailSubScription, bool isNewsLetterSubscription, CompanyTypes customerType, string RegWithTwitter, long OrganisationId, CompanyContact regContact = null)
        {
            return _CompanyRepository.CreateCustomer(name, isEmailSubScription, isNewsLetterSubscription, customerType, RegWithTwitter, OrganisationId, regContact);
        }

        public Organisation getOrganisatonByID(int OID)
        {
            return _organisationRepository.GetOrganizatiobByID(OID);

        }
        public string GetContactMobile(long CID)
        {
            return _CompanyContactRepository.GetContactMobile(CID);
        }

        public CmsPage getPageByID(long PageID)
        {
            return _cmsPageRepositary.getPageByID(PageID);
        }

        public bool canContactPlaceOrder(long contactID, out bool hasWebAccess)
        {
            return _CompanyContactRepository.canContactPlaceOrder(contactID, out hasWebAccess);
        }

        public string GetCountryNameById(long CountryId)
        {
            return _countryRepository.GetCountryNameById(CountryId);
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
        public CompanyContact GetRetailUser(string email, string password)
        {
            return _CompanyContactRepository.GetRetailUser(email, password);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>

        public long GetContactIdByCompanyId(long CompanyId)
        {
            return _CompanyContactRepository.GetContactIdByCustomrID(CompanyId);
        }
        /// <summary>
        /// get contact list by role and company id
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="Role"></param>
        /// <returns></returns>
        public long GetContactIdByRole(long CompanyID, int Role)
        {
            return _CompanyContactRepository.GetContactIdByRole(CompanyID, Role);
        }
        #endregion
    }


}
