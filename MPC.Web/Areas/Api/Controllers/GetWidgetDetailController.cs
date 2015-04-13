using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.Models.Common;
using MPC.WebBase.Mvc;
using Newtonsoft.Json;

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
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public string Get([FromUri]string widgetControlName)
        {
            string mySiteUrl = HttpContext.Current.Request.Url.Host;
            //  string webStoreWidgetsPath = ConfigurationManager.AppSettings["WebStoreWidgetsPath"];

            //using (var client = new HttpClient())
            //{


            //    client.BaseAddress = new Uri("http://" + mySiteUrl);
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //    string url = "/APIControllers/GetWidgetDetail/GET?widgetControlName=\"" + widgetControlName + "\"";
            //    string responsestr = "";
            //    var response = client.GetAsync(url);
            //    if (response.Result.IsSuccessStatusCode)
            //    {
            //        responsestr = response.Result.Content.ReadAsStringAsync().Result;

            //    }

            //}
            return File.ReadAllText(HttpContext.Current.Server.MapPath("~/Areas/Stores/Views/Shared/_Default.cshtml"));

        }

        #endregion
    }
}