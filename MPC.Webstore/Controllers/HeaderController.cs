using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Models.DomainModels;

namespace MPC.Webstore.Controllers
{
    public class HeaderController : Controller
    {

        // GET: News
        public ActionResult Index()
        {
            var model = Session["store"] as Company;

            return PartialView("PartialViews/Header", model);
        }
    }
}