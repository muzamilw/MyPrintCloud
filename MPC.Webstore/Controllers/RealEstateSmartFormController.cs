using MPC.Interfaces.WebStoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Models.DomainModels;

namespace MPC.Webstore.Controllers
{
    public class RealEstateSmartFormController : Controller
    {
        #region Private

        private readonly IListingService _myListingService;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public RealEstateSmartFormController(IListingService myListingService)
        {
            if (myListingService == null)
            {
                throw new ArgumentNullException("myListingService");
            }

            this._myListingService = myListingService;
        }

        #endregion

        // GET: RealEstateSmartForm
        public ActionResult Index(long listingId, long itemId)
        {
            Listing listing = _myListingService.GetListingByListingId(listingId);
            List<ListingOFI> listingOFIs = _myListingService.GetListingOFIsByListingId(listingId);
            List<ListingImage> listingImages = _myListingService.GetListingImagesByListingId(listingId);
            List<ListingFloorPlan> listingFloorPlans = _myListingService.GetListingFloorPlansByListingId(listingId);
            List<ListingLink> listingLinks = _myListingService.GetListingLinksByListingId(listingId);
            List<ListingAgent> listingAgents = _myListingService.GetListingAgentsByListingId(listingId);
            List<ListingConjunctionAgent> listingConAgents = _myListingService.GetListingConjunctionalAgentsByListingId(listingId);
            List<ListingVendor> listingVendors = _myListingService.GetListingVendorsByListingId(listingId);

            List<FieldVariable> lstFieldVariables = _myListingService.GeyFieldVariablesByItemID(itemId);

            return View("PartialViews/RealEstateSmartForm");
        }
    }
}