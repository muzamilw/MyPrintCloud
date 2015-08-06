using MPC.Models.Common;
using MPC.Webstore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class BlackAndWhiteHeaderMenuController : Controller
    {
        // GET: BlackAndWhiteHeaderMenu
        public ActionResult Index()
        {
            ViewBag.loggedInUser = "Logged in " + UserCookieManager.WEBContactFirstName + UserCookieManager.WEBContactLastName;
            
            return View();
        }
        [HttpPost]
        public ActionResult LogOut()
        {
            System.Web.HttpContext.Current.Response.Cookies["ShowPrice"].Expires = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Response.Cookies["CanEditProfile"].Expires = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Response.Cookies["WEBLastName"].Expires = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Response.Cookies["WEBFirstName"].Expires = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Response.Cookies["WEBOrderId"].Expires = DateTime.Now.AddDays(-1);
            UserCookieManager.isRegisterClaims = 2;

            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
            {
                Response.Redirect("/Login");
            }
            else
            {
                Response.Redirect("/");
            }
            //Response.Redirect("/"); 
            return null;

        }
    }
}