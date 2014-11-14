using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Web.Controllers
{
    public class HomeController : Controller
    {
        //public ActionResult Index()
        //{
        //    ViewData["Heading"] = "Setting";
        //    return View();
        //}
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Redirect()
        {
           
           // Uri url = new Uri("http://site.myprintcloud.com/dashboard");
            Uri url = new Uri("http://localhost:56090/Home/LoginFull");
            if (Request.Url == url)
                return RedirectToRoute("Dashboard");
            else
                return RedirectToRoute("Login");
        }
        public ActionResult LoginFull()
        {
            return View();

            /*
             * Get UserToken from Query String [User is already on its own site]
             * Make Api Call to verify Security Token and it will return User Details, Roles Details DomainKey 
             * Use User Details to create Authentication Ticket 
             * Create Claims Principals for (Role, User, Menus, Menu Rights) 
             * Redirect to SITE_BASED controller of Dashboard [/Dashboard/Index]
             */ 
        }

       
    }
}
