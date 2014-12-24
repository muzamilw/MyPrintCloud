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

        //protected string RenderPartialViewToString(string viewName, object model)
        //{
        //    if (string.IsNullOrEmpty(viewName))
        //        viewName = ControllerContext.RouteData.GetRequiredString("action");

        //    ViewData.Model = model;

        //    using (StringWriter sw = new StringWriter())
        //    {
        //        ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(Configuration.ControllerContext, viewName);
        //        ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
        //        viewResult.View.Render(viewContext, sw);

        //        return sw.GetStringBuilder().ToString();
        //    }
        //}
        #endregion
    }
}