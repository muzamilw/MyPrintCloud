
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

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public CompanyService(ICompanyRepository companyRepository, ICmsSkinPageWidgetRepository widgetRepository,
         ICompanyBannerRepository companyBannerRepository, IProductCategoryRepository productCategoryRepository, ICmsPageRepository cmspageRepository,
            IPageCategoryRepository pageCategoryRepository, ICompanyContactRepository companyContactRepository, ICurrencyRepository currencyRepository
            , IGlobalLanguageRepository globalLanguageRepository, IOrganisationRepository organisationRepository)
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
            Company oCompany = GetCompanyByCompanyID(companyId);

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

                List<CmsPage> AllPages = _cmsPageRepositary.GetSecondaryPages(companyId); 

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

                stores.Add(oCompany.CompanyId, oStore);




                cache.Set(CacheKeyName, stores, policy);
                return stores[oCompany.CompanyId];
            }
            else // there are some stores already in cache.
            {

                if (!stores.ContainsKey(oCompany.CompanyId))
                {
                    List<CmsPage> AllPages = _cmsPageRepositary.GetSecondaryPages(oCompany.CompanyId);

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
                    return stores[oCompany.CompanyId];
                }
            }
        }

        public long GetStoreIdFromDomain(string domain)
        {
            return _CompanyRepository.GetStoreIdFromDomain(domain);
        }

        public List<ProductCategory> GetCompanyParentCategoriesById(long companyId)
        {
            return _productCategoryRepository.GetParentCategoriesByTerritory(companyId);
        }

        public CompanyResponse GetAllCompaniesOfOrganisation(CompanyRequestModel request)
        {
            return _CompanyRepository.SearchCompanies(request);
        }

        public CompanyContact GetContactUser(string email, string password)
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

        public Int64 CreateContact(CompanyContact Contact, string Name, int OrganizationID, int CustomerType, string TwitterScreanName)
        {
            return _CompanyContactRepository.CreateContact(Contact, Name, OrganizationID, CustomerType, TwitterScreanName);
        }

       

        public Company GetCompanyByCompanyID(Int64 CompanyID)
        {
            return _CompanyRepository.GetCompanyById(CompanyID).Company;
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
        
            return _CompanyContactRepository.CreateCorporateContact(CustomerId, regContact,TwitterScreenName);
        }

        public string GetUiCulture(long organisationId)
        {
            return _globalLanguageRepository.GetLanguageCodeById(organisationId);

        }

        #endregion
    }
}
