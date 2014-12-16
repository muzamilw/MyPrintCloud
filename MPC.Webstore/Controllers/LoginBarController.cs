using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Webstore.Common;

namespace MPC.Webstore.Controllers
{
    public class LoginBarController : Controller
    {
        // GET: LoginBar
        public ActionResult Index()
        {
            return PartialView("PartialViews/LoginBar");
        }

        public ActionResult LogOut()
        {

            ControllerContext.HttpContext.Response.Redirect(Url.Action("Index", "Home", null, protocol: Request.Url.Scheme));
            return null;
        }
    }
}