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
        public ActionResult Index(AccountModel model)
        {

            return PartialView("PartialViews/Login", model);
        }

        [HttpPost]
        public ActionResult Login(AccountModel model)
        {

            if (ModelState.IsValid)
            {

            }
            else
            {
                ViewBag.Message = "Invalid email or password.";
            }
            
            return View("PartialViews/Login", model);
            //return View("Login", model);
           // return View("PartialViews/Login");
            // return RedirectToAction("Index", "Home", model);
            //return Redirect("/Login");
        }
    }
}