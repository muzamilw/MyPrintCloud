using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class LoginBarController : Controller
    {
        // GET: LoginBar
        public ActionResult Index()
        {
            return PartialView("PartialViews/LoginBar");
        }
    }
}