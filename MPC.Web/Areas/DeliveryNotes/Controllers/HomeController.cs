using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.Models.Common;
using MPC.WebBase.Mvc;


namespace MPC.MIS.Areas.DeliveryNotes.Controllers
{
    public class HomeController : Controller
    {
        // GET: DeliveryNotes/Home
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult DeliveryNote(int? id)
        {
            ViewBag.DeliveryNoteId = id ?? 0;
            return View();
        
        }
    }
}