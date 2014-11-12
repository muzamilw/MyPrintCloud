using System;
using System.Web.Mvc;
using MPC.Interfaces.IServices;

namespace MPC.WebApi.Controllers
{
    
    public class HomeController : Controller
    {
        //private readonly IWebApiAuthenticationService authenticationService;

        public HomeController(IWebApiAuthenticationService authenticationService)
        {
            
        }

        public ActionResult Index()
        {

            //var item = authenticationService.IsValidWebApiUser("asf", "asfasf");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}