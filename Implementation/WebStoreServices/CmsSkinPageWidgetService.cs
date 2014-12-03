using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;

namespace MPC.Implementation.WebStoreServices
{
    public class CmsSkinPageWidgetService : ICmsSkinPageWidgetService
    {
         #region Private

        /// <summary>
        /// Private members
        /// </summary>
        public readonly ICmsSkinPageWidgetRepository widgetRepository;
     

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public CmsSkinPageWidgetService(ICmsSkinPageWidgetRepository widgetRepository)
        {
            this.widgetRepository = widgetRepository;
         
        }

        #endregion


        #region Public
        /// <summary>
        /// Resolves the Company/Store widgets 
        /// </summary>
        /// <returns></returns>
        public List<CmsSkinPageWidget> GetDomainWidgetsById(long companyId)
        {
            return widgetRepository.GetDomainWidgetsById(companyId);
        }
        //public List<CmsPage> GetSecondaryPages(long companyId)
        //{
        //    return _cmsPageRepositary.GetSecondaryPages(companyId);

        //}


        //public List<PageCategory> GetSecondaryPageCategories()
        //{
        //    return _pageCategoryRepositary.GetCmsSecondaryPageCategories();

        //}
       
        #endregion
    }
}
