using System.Web.Mvc;

namespace MPC.MIS.App_Start
{
    /// <summary>
    /// Custom Razor View Engine
    /// Looks for only cshtml pages
    /// </summary>
    public class CustomRazorViewEngine : RazorViewEngine
    {
        public CustomRazorViewEngine()
        {
            AreaViewLocationFormats = new [] {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };

            AreaMasterLocationFormats = new [] {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };

            AreaPartialViewLocationFormats = new [] {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml",
                "~/Areas/{2}/Views/{1}/{0}.ascx"
            };


            ViewLocationFormats = new [] {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };

            PartialViewLocationFormats = new [] {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };

            MasterLocationFormats = new [] {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };
        }
    }
}