
using System.Collections.Generic;
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
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

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public CompanyService(ICompanyRepository companyRepository, ICmsSkinPageWidgetRepository widgetRepository,
         ICompanyBannerRepository companyBannerRepository, IProductCategoryRepository productCategoryRepository)
        {
            this._companyRepository = companyRepository;
            this._widgetRepository = widgetRepository;
            this._companyBannerRepository = companyBannerRepository;
            this._productCategoryRepository = productCategoryRepository;
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
                Banners = _companyBannerRepository.GetCompanyBannersById(companyId)
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
        #endregion
    }
}
