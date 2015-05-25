using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using Castle.Core.Internal;
using GrapeCity.ActiveReports.PageReportModel;
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
        public FileResult LiveJobsDownload(long? item0, long? item1, long? item2, long? item3, long? item4)
        {


            var s = Request.Form.AllKeys;
            var stream = liveJobsService.DownloadArtwork(AddSelectedItemToList(item0, item1, item2, item3, item4));
            stream.Position = 0;
            return File(stream, "application/zip", "download.zip");

        }

        private List<long?> AddSelectedItemToList(long? item0, long? item1, long? item2, long? item3, long? item4)
        {
            List<long?> list = new List<long?>();
            if (item0 != null)
            {
                list.Add(item0);
            }
            if (item1 != null)
            {
                list.Add(item1);
            }
            if (item2 != null)
            {
                list.Add(item2);
            }
            if (item3 != null)
            {
                list.Add(item3);
            }
            if (item4 != null)
            {
                list.Add(item4);
            }
            return list;
        }
    }
}
