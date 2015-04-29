using MPC.Interfaces.MISServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GrapeCity.ActiveReports;


namespace MPC.MIS.Areas.Reports.Controllers
{
    public class HomeController : Controller
    {
        private readonly IReportService IReportService;
        public HomeController(IReportService IReportService)
        {
            this.IReportService = IReportService;
        }
        public ActionResult Index()
        {



            return View("Viewer");

        }

        public ActionResult Viewer()
        {

           
            
            return View();
        }
        

        // GET: Common/Report
        public ActionResult GetReport()
        {

           SectionReport report= IReportService.GetReport(103);
          
           ViewBag.Report = report;

           return PartialView("WebViewer");
          // return PartialView("test");
        }
    }
}