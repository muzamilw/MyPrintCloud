using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class RealEstateItemController : Controller
    {
        public class RealEstateListing
        {
            public string ListingImageURL { get; set; }
            public Listing ListingProperty { get; set; }
        }

        #region Private

        private readonly IListingService _myListingService;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public RealEstateItemController(IListingService myListingService)
        {
            if (myListingService == null)
            {
                throw new ArgumentNullException("myListingService");
            }

            this._myListingService = myListingService;
        }

        #endregion

        // GET: RealEstateItem
        public ActionResult Index()
        {
            List<Listing> model = _myListingService.GetRealEstateProperties();
            //ViewData["Listings"] = model;

            List<RealEstateListing> lstListings = new List<RealEstateListing>();

            foreach (var item in model)
            {
                string imagePath = _myListingService.GetImageURLByListingId(item.ListingId);
                RealEstateListing objREL = new RealEstateListing();
                objREL.ListingImageURL = imagePath;
                objREL.ListingProperty = item;

                lstListings.Add(objREL);
            }

            ViewData["Listings"] = lstListings;

            return View("PartialViews/RealEstateItem");

            //List<string> lstImages = new List<string>();
            //lstImages.Add("http://bourkes.agentboxcrm.com.au/lt-1-1P1899-0217057437.jpg");
            //lstImages.Add("http://bigdrumassociates.com/wp-content/uploads/2014/01/Real-Estate.jpg");
            //lstImages.Add("http://torontocaribbean.com/wp-content/uploads/2014/02/house-for-sale.jpg"); 
            //lstImages.Add("http://bourkes.agentboxcrm.com.au/lt-1-1P1899-0217057437.jpg");
            //lstImages.Add("http://bigdrumassociates.com/wp-content/uploads/2014/01/Real-Estate.jpg");
            //lstImages.Add("http://torontocaribbean.com/wp-content/uploads/2014/02/house-for-sale.jpg"); 
            //lstImages.Add("http://bourkes.agentboxcrm.com.au/lt-1-1P1899-0217057437.jpg");
            //lstImages.Add("http://bigdrumassociates.com/wp-content/uploads/2014/01/Real-Estate.jpg");
            //lstImages.Add("http://torontocaribbean.com/wp-content/uploads/2014/02/house-for-sale.jpg");
            //ViewBag.ImageSource = lstImages;
            //return View("PartialViews/RealEstateItem");
        }
    }
}