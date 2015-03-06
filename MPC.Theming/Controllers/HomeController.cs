using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace MPC.Theming.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
                return View();
            
        }
    }
}
