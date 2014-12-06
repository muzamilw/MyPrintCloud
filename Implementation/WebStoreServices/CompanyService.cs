
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.WebStoreServices
{
    public class CompanyService : ICompanyService   
    {
        #region Private

        /// <summary>
        /// Private members
        /// </summary>
        public readonly ICompanyRepository _companyRepository;
        public readonly ICompanyContactRepository _companyContactRepository;
        private readonly ICmsSkinPageWidgetRepository _widgetRepository;
        private readonly ICompanyBannerRepository _companyBannerRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly ICmsPageRepository _cmsPageRepositary;
        private readonly IPageCategoryRepository _pageCategoryRepositary;
        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public CompanyService(ICompanyRepository companyRepository, ICmsSkinPageWidgetRepository widgetRepository,
         ICompanyBannerRepository companyBannerRepository, IProductCategoryRepository productCategoryRepository, ICmsPageRepository cmspageRepository,
            IPageCategoryRepository pageCategoryRepository, ICompanyContactRepository companyContactRepository)
        {
            this._companyRepository = companyRepository;
            this._widgetRepository = widgetRepository;
            this._companyBannerRepository = companyBannerRepository;
            this._productCategoryRepository = productCategoryRepository;
            this._cmsPageRepositary = cmspageRepository;
            this._pageCategoryRepositary = pageCategoryRepository;
            this._companyContactRepository = companyContactRepository;
        }

        #endregion


        #region Public
        /// <summary>
        /// Resolves the Company/Store by the domain provided
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>

        public MyCompanyDomainBaseReponse GetBaseData(long companyId)
        {
            string CacheKeyName = "CompanyBaseResponse";
             ObjectCache cache = MemoryCache.Default;

             MyCompanyDomainBaseReponse responseObject = cache.Get(CacheKeyName) as MyCompanyDomainBaseReponse;

            if (responseObject == null)
            {
                List<CmsPage> AllPages = _cmsPageRepositary.GetSecondaryPages(companyId); 


                responseObject = new MyCompanyDomainBaseReponse();
                responseObject.Company = _companyRepository.GetCompanyById(companyId);
                responseObject.CmsSkinPageWidgets = _widgetRepository.GetDomainWidgetsById(companyId);
                responseObject.Banners = _companyBannerRepository.GetCompanyBannersById(companyId);
                responseObject.SystemPages = AllPages.Where(s => s.CompanyId != null).ToList();
                responseObject.SecondaryPages = AllPages.Where(s => s.CompanyId == companyId).ToList();
                responseObject.PageCategories = _pageCategoryRepositary.GetCmsSecondaryPageCategories();

                CacheItemPolicy policy = null;
                CacheEntryRemovedCallback callback = null;

                policy = new CacheItemPolicy();
                policy.Priority = CacheItemPriority.NotRemovable;
                policy.SlidingExpiration =
                    TimeSpan.FromMinutes(5);
                policy.RemovedCallback = callback;
                cache.Set(CacheKeyName, responseObject, policy);
                return responseObject;
            }
            else
            {
                return responseObject;
            }
        } 
        public long GetCompanyIdByDomain(string domain)
        {
            return _companyRepository.GetCompanyIdByDomain(domain);
        }
       
        public List<ProductCategory> GetCompanyParentCategoriesById(long companyId)
        {
            return _productCategoryRepository.GetParentCategoriesByTerritory(companyId);
        }

        public CompanyResponse GetAllCompaniesOfOrganisation(CompanyRequestModel request)
        {
            return _companyRepository.SearchCompanies(request);
        }

        public CompanyContact GetContactUser(string email, string password)
        {
            return _companyContactRepository.GetContactUser(email, password);
        }
        public CompanyContact GetContactByFirstName(string FName)
        {
            return _companyContactRepository.GetContactByFirstName(FName);
        }

        public CompanyContact GetContactByEmail(string Email)
        {
            return _companyContactRepository.GetContactByEmail(Email);
        }

        public int CreateContact(CompanyContact Contact)
        {
            return _companyContactRepository.CreateContact(Contact);
        }

        #endregion
    }
}
