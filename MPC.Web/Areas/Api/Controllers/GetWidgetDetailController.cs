using System;
using System.IO;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Get Widget Detail API Controller
    /// </summary>
    public class GetWidgetDetailController : ApiController
    {
        #region Private

        private readonly ICompanyService companyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public GetWidgetDetailController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        #endregion

        #region Public
        /// <summary>
        /// Get Company By Id
        /// </summary>
        /// <returns></returns>
        public string Get([FromUri]string widgetControlName)
        {
            //switch (widgetControlName)
            //{
            //    case "SavedDesignsWidget.ascx":
            //        return File.ReadAllText(HttpContext.Current.Server.MapPath("~/Areas/Stores/Views/Shared/_HomeWidget.cshtml"));
            //    case "LoginBar.ascx":
            //        return File.ReadAllText(HttpContext.Current.Server.MapPath("~/Areas/Stores/Views/Shared/_AboutUs.cshtml"));
            //    case "HomePageBannerS4.ascx":
            //        return File.ReadAllText(HttpContext.Current.Server.MapPath("~/Areas/Stores/Views/Shared/_AboutUs.cshtml"));
            //}

            return File.ReadAllText(HttpContext.Current.Server.MapPath("~/Areas/Stores/Views/Shared/_Default.cshtml"));
            //return string.Empty;
        }
        #endregion
    }
}