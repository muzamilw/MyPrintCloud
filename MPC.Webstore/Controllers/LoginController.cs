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
          //  return PartialView("PartialViews/Login", model);
           return RedirectToAction("Index", "Home");
            //return View("/Index", "/Home"); //return Redirect("/Login");
        }
    }
}