using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class RealEstateImageSliderController : Controller
    {
        // GET: RealEstateImageSlider
        public ActionResult Index(List<string> lstImages)
        {
            return View("PartialViews/RealEstateImageSlider", lstImages);
        }
    }
}