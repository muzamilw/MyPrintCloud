using MPC.Interfaces.WebStoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class ManageAssetsController : Controller
    {
        // GET: ManageAssets
        private readonly ICompanyService _myCompanyService;

        public ManageAssetsController(ICompanyService _myCompanyService)
        {
            this._myCompanyService = _myCompanyService;
        }
        public ActionResult Index()
        {

            return View("PartialViews/ManageAssets");
        }
        [HttpPost]
        public ActionResult Index()
        {

            return View("PartialViews/ManageAssets");
        }
    }
}