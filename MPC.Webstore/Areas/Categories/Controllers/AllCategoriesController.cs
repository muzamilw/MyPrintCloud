using MPC.Webstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Areas.Categories.Controllers
{
    public class AllCategoriesController : Controller
    {


        // GET: Categories/AllCategories
        public ActionResult Index()
        {
            ExternalLoginListViewModel model = new ExternalLoginListViewModel();
            return View(model);
        }
    }
}