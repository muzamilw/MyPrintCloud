using System.Web.Mvc;
using MPC.Interfaces.Data;
using MPC.Models.Common;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Stores.Controllers
{
    //[SiteAuthorize(MisRoles = new[] { SecurityRoles.Admin }, AccessRights = new[] { SecurityAccessRight.CanViewSecurity })]
    
    public class StoresController : Controller
    {
        // GET: Stores/Stores
        //[SiteAuthorize(AccessRights = new[] { SecurityAccessRight.CanVeiwPaperSheet })]
        public ActionResult Index()
        {
            return View();
        }
    }
}