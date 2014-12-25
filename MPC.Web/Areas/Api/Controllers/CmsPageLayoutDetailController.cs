using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Cms Page Layout Detail Api Controller
    /// </summary>
    public class CmsPageLayoutDetailController : ApiController
    {
        #region Private

        private readonly ICompanyService companyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public CmsPageLayoutDetailController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        #endregion

        #region Public
        /// <summary>
        /// Get Company By Id
        /// </summary>
        /// <returns></returns>
        public List<CmsSkinPageWidget> Get([FromUri]int pageId, long companyId)
        {
            return companyService.GetCmsPageWidgetByPageId(pageId, companyId).Select(w => w.CreateFrom()).ToList();

        }
        #endregion
    }
}