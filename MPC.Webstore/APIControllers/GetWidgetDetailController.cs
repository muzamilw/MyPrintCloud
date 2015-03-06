using System.IO;
using System.Web;
using System.Web.Http;

namespace MPC.Webstore.APIControllers
{
    /// <summary>
    /// Get Widget Detail API Controller
    /// </summary>
    public class GetWidgetDetailController : ApiController
    {
        #region Public
        /// <summary>
        /// Get Widget Detail By Control Name
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get([FromUri]string widgetControlName)
        {
            if (File.Exists(HttpContext.Current.Server.MapPath("~/Views/Shared/PartialViews/" + widgetControlName + ".cshtml")))
            {
                return
                    File.ReadAllText(
                        HttpContext.Current.Server.MapPath("~/Views/Shared/PartialViews/" + widgetControlName +
                                                           ".cshtml"));
            }
            return "<div></div>";
        }

        #endregion
    }
}