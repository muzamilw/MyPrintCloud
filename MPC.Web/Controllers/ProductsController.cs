using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Interfaces.MISServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;

namespace MPC.MIS.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IMyOrganizationService _organizationService;
        public ProductsController(IMyOrganizationService organizationService)
        {
            this._organizationService = organizationService;
            
        }
        // GET: /Product/

        public PartialViewResult ProductWizardView()
        {
            return PartialView("_PartialAddProductWizard");
        }
        public ActionResult ProductsList()
        {
            return View();

        }
        public ActionResult ProductDetail()
        {

            return View();

        }

        public ActionResult ProductCategories()
        {

            return View();
        }


        public ActionResult ProductCategoryDetail()
        {

            return View();
        }
        

    }
}
