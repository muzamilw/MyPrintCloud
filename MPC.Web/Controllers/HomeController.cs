using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.MIS.Controllers
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
            /*
             * Entry Point For MIS
             * Get User Id, DomainKey From Url
             * Call WebStore Service to Authenticate User
             * On Call back, if user is authenticated then add Claims
             */

            var domainKey = Request.QueryString["DomainKey"];
            var userId = Request.QueryString["UserId"];

            // Authenticate User For this Site


            return View();
        }

       
    }
}
