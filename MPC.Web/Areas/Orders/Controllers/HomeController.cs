using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.Models.Common;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Orders.Controllers
{
    /// <summary>
    /// Orders Home Controller
    /// </summary>
    [SiteAuthorize(MisRoles = new[] { SecurityRoles.Admin }, AccessRights = new[] { SecurityAccessRight.CanViewOrder })]
    public class HomeController : Controller
    {
        #region Private

        private readonly IOrderService orderService;


        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public HomeController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        #endregion

        // GET: Orders/Home
        [SiteAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrder })]
        public ActionResult Index(int? id, int? itemId)
        {
            ViewBag.CallingMethod = (string)TempData["CallingMethod"] != "" ? TempData["CallingMethod"] : "0";
            ViewBag.OrderId = id ?? 0;
            ViewBag.ItemId = itemId ?? 0;
            return View();
        }

        public ActionResult PurchaseOrders()
        {

            return View();

        }
        public ActionResult PurchaseOrdersPO()
        {

            return View();

        }
        public ActionResult EstimatesList(int? id)
        {
            ViewBag.OrderId = id ?? 0;
            return View();
        }

        public ActionResult PendingOrders()
        {
            TempData["CallingMethod"] = "4";
            return RedirectToAction("Index");
        }
        public ActionResult ConfirmedStarts()
        {
            TempData["CallingMethod"] = "5";
            return RedirectToAction("Index");
        }
        public ActionResult InProduction()
        {
            TempData["CallingMethod"] = "6";
            return RedirectToAction("Index");
        }
        public ActionResult ReadyForShipping()
        {
            TempData["CallingMethod"] = "7";
            return RedirectToAction("Index");
        }
        public ActionResult Invoiced()
        {
            TempData["CallingMethod"] = "8";
            return RedirectToAction("Index");
        }
        public ActionResult CancelledOrders()
        {
            TempData["CallingMethod"] = "9";
            return RedirectToAction("Index");
        }

        public ActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FileDownload()
        {
            string fileType = ".jpg";
            string fileName = "";
            string filePath = orderService.DownloadAttachment(Request.Form["item"] != null ? Convert.ToInt64(Request.Form["item"]) : 0, out fileName, out fileType);
            string contentType;

            if (fileType == ".pdf")
            {
                contentType = "application/pdf";
            }

            else if (fileType == ".docx")
            {
                contentType = "application/docx";
            }
            else if (fileType == ".doc")
            {
                contentType = "application/docx";
            }
            else if (fileType == ".xlsx")
            {
                contentType = "application/vnd.ms-excel";
            }
            else if (fileType == ".xls")
            {
                contentType = "application/vnd.ms-excel";
            }
            else if (fileType == ".rtf")
            {
                contentType = "application/rtf";
            }
            else if (fileType == ".png")
            {
                contentType = "image/png";
            }
            else if (fileType == ".jpg")
            {
                contentType = "image/jpeg";
            }
            else if (fileType == ".gif")
            {
                contentType = "image/gif";
            }
            else if (fileType == ".txt")
            {
                contentType = "text/plain";
            }
            else
            {
                contentType = "image/jpeg";

            }
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    return File(filePath, contentType, fileName + fileType);
                }
                RedirectToAction("Error");
            }
            catch (FileNotFoundException ex)
            {
                RedirectToAction("Error");
                
            }
            return RedirectToAction("Error");

        }
    }
}