using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;

namespace MPC.Webstore.Controllers
{
    public class HomeController : Controller
    {
         #region Private

        private readonly ICmsSkinPageWidgetService _widgetService;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public HomeController(ICmsSkinPageWidgetService widgetService)
        {
            if (widgetService == null)
            {
                throw new ArgumentNullException("widgetService");
            }
            this._widgetService = widgetService;
        }

        #endregion

        
        public ActionResult Index()
        {
           
            var model = Session["store"] as Company;
            if (model == null)
            {

            }
            else
            {
                var widgets = _widgetService.GetDomainWidgetsById(model.CompanyId, model.OrganisationId ?? 0);
                ViewBag.Company = model;
                ViewBag.widgets = widgets;
                ViewBag.ContentFile = "/Content/Site.css";
            }
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
    }
}