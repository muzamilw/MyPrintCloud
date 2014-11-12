using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Web.Controllers
{
    public class ProductsController : Controller
    {
        //
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

    }
}
