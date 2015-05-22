using System;
using System.Web.Mvc;
using MPC.Interfaces.MISServices;

namespace MPC.MIS.Areas.Production.Controllers
{
    /// <summary>
    /// Production Board Controller
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILiveJobsService liveJobsService;
        public HomeController(ILiveJobsService liveJobsService)
        {
            this.liveJobsService = liveJobsService;
        }
        // GET: Production/Home
        public ActionResult Index()
        {
            Boolean istrial = false;
            int count = 0;
            ViewBag.Istrial = istrial;
            ViewBag.TrialCount = count;
            return View();
        }

        // GET: Production/ItemJobStatus
        public ActionResult ItemJobStatus()
        {
            return View();
        }

        // GET: Production/LiveJobs
        public ActionResult LiveJobs()
        {
            return View();
        }

        [HttpPost]
        public FileResult Test()
        {
            var stream = liveJobsService.DownloadArtwork();
            stream.Position = 0;
            return File(stream, "application/zip", "download.zip");

        }

        public ActionResult ItemLateStart()
        {
            ViewBag.IsLateScreen = "Late";
            return View();
        }
      
       
    }
}
