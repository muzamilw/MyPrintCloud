using System.Web.Mvc;
using MPC.Interfaces.Data;
using MPC.Models.Common;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Invoices.Controllers
{
    public class HomeController : Controller
    {
        // GET: Invoices/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}