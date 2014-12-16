using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index(string name,string id)
        {
            return View("PartialViews/Category");
        }
    }
}