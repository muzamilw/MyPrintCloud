
using System.Collections.Generic;
using System.Linq;
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
         ICompanyBannerRepository companyBannerRepository, IProductCategoryRepository productCategoryRepository, ICmsPageRepository cmspageRepository,IPageCategoryRepository pageCategoryRepository)
        {
            this._companyRepository = companyRepository;
            this._widgetRepository = widgetRepository;
            this._companyBannerRepository = companyBannerRepository;
            this._productCategoryRepository = productCategoryRepository;
            this._cmsPageRepositary = cmspageRepository;
            this._pageCategoryRepositary = pageCategoryRepository;
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
            return new MyCompanyDomainBaseReponse
            {
                Company = _companyRepository.GetCompanyById(companyId),
                CmsSkinPageWidgets = _widgetRepository.GetDomainWidgetsById(companyId),
                Banners = _companyBannerRepository.GetCompanyBannersById(companyId),
                cmsPages = _cmsPageRepositary.GetSecondaryPages(companyId),
                PageCategories =  _pageCategoryRepositary.GetCmsSecondaryPageCategories(),

            };
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

        public List<CmsPage> GetSecondaryPages(long companyId)
        {
            return _cmsPageRepositary.GetSecondaryPages(companyId);
                
        }


        public List<PageCategory> GetSecondaryPageCategories()
        {
            return _pageCategoryRepositary.GetCmsSecondaryPageCategories();

        }

        #endregion
    }
}
