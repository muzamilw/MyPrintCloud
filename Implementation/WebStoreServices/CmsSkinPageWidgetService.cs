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
// ReSharper disable InconsistentNaming
        public readonly ICmsSkinPageWidgetRepository widgetRepository;
// ReSharper restore InconsistentNaming
     

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

       
        #endregion
    }
}
