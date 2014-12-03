using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Webstore.Models;

namespace MPC.Webstore.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {

            return PartialView("PartialViews/Login");
        }

        [HttpPost]
        public ActionResult Login(AccountModel model)
        {
            if (ModelState.IsValid)
            {
                
            }
            return Content("Invalid");
        }
        //public ActionResult Login(string email, string password)
        //{

        //    if (ModelState.IsValid)
        //    {

        //    }
        //    else
        //    {
               
        //    }
        //   //return  "Invalid email or password.";
        //   //return Content("no");
        //    return View("PartialViews/Login");
        //    //return View("PartialViews/Login", "no");
        //   // return View("PartialViews/Login");
        //    // return RedirectToAction("Index", "Home", model);
        //   // return Redirect("/Login");
        //}
    }
}