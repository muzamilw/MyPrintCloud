using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class RealEstateItemController : Controller
    {
        // GET: RealEstateItem
        public ActionResult Index()
        {
            List<string> lstImages = new List<string>();
            lstImages.Add("http://bourkes.agentboxcrm.com.au/lt-1-1P1899-0217057437.jpg");
            lstImages.Add("http://bigdrumassociates.com/wp-content/uploads/2014/01/Real-Estate.jpg");
            lstImages.Add("http://torontocaribbean.com/wp-content/uploads/2014/02/house-for-sale.jpg");

            ViewBag.ImageSource = lstImages;


            return View("PartialViews/RealEstateItem");
        }
    }
}