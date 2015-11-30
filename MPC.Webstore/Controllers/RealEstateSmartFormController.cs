using MPC.Interfaces.WebStoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Models.DomainModels;
using System.Runtime.Caching;
using MPC.Webstore.Common;
using MPC.Models.ResponseModels;
using MPC.Models.Common;

namespace MPC.Webstore.Controllers
{
    public class RealEstateSmartFormController : Controller
    {

        public class SectionControls
        {
            private List<MPC.Models.Common.TemplateVariable> _controls;
            public string SectionName { get; set; }
            public List<MPC.Models.Common.TemplateVariable> Controls
            {
                get
                {
                    if (_controls == null)
                    {
                        _controls = new List<MPC.Models.Common.TemplateVariable>();

                    }

                    return _controls;
                }
                set
                {
                    _controls = value;
                }
            }
        }

        #region Private

        private readonly IListingService _myListingService;
        private readonly IWebstoreClaimsHelperService _webstoreclaimHelper;
        private readonly ICompanyService _myCompanyService;
        private readonly IItemService _ItemService;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public RealEstateSmartFormController(IListingService myListingService, IWebstoreClaimsHelperService webstoreClaimHelper
            , ICompanyService myCompanyService, IItemService _ItemService)
        {

            if (webstoreClaimHelper == null)
            {
                throw new ArgumentNullException("webstoreClaimHelper");
            }
        
            if (myListingService == null)
            {
                throw new ArgumentNullException("myListingService");
            }

            this._myListingService = myListingService;
            this._webstoreclaimHelper = webstoreClaimHelper;
            this._myCompanyService = myCompanyService;
            this._ItemService = _ItemService;
        }

        #endregion

        // GET: RealEstateSmartForm
        public ActionResult Index(string id, string PropertyId)
        {
            long PropID =Convert.ToInt64(PropertyId);
            
            Listing Listing = _myListingService.GetListingByListingID(Convert.ToInt32(PropertyId));
            GetCategoryProduct currentItem = new GetCategoryProduct();
            currentItem = _ItemService.GetPublishedProductByItemID(Convert.ToInt32(id));
            List<CompanyContact> listingAgents = _myCompanyService.GetUsersByCompanyId(UserCookieManager.WBStoreId); // propertyManager.GetListingAgentsByListingID(propertyId); //Listing Agents
            ViewBag.Listings = Listing;
            ViewBag.ListingAgents = listingAgents;
            ViewBag.CurrentItem = currentItem;

            ViewBag.ListingImages = GetAllListingImages(PropID);
          //  List<ListingBulletPoints> listingBulletPoint = PropertyManager.GetListingBulletPoints(listing.ListingID); // propertyManager.GetListingAgentsByListingID(propertyId); //Listing Agents
            return PartialView("PartialViews/RealEstateSmartForm");
        }

        private List<ListingImage> GetAllListingImages(long ListingId)
        {
            return _myCompanyService.GetAllListingImages(ListingId);
        
        }
        private void LoadSmartForm()
        {
            //hfCompanyID.Value = SessionParameters.ContactCompany.ContactCompanyID.ToString();
            //hfContactID.Value = SessionParameters.CustomerContact.ContactID.ToString();
            //hfPrintCropMarks.Value = SessionParameters.tbl_cmsDefaultSettings.isAddCropMarks.ToString();
            CompanyContact Contact = _myCompanyService.GetContactByID(_webstoreclaimHelper.loginContactID());

            if (Contact.ContactRoleId == (int)Roles.Adminstrator)
            {
               
               // hfCorpType.Value = "admin";
                loadUserProfiles(Roles.Adminstrator);
            }
            else if (Contact.ContactRoleId == (int)Roles.Manager)
            {
               // hfCorpType.Value = "manager";
                loadUserProfiles(Roles.Manager);
            }
            else if (Contact.ContactRoleId == (int)Roles.User)
            {
               // hfCorpType.Value = "user";
                loadUserProfiles(Roles.User);
            }

            //int itemId = 0;
            //itemId = Convert.ToInt32(Request.QueryString["ItemID"]);

            //currentItem = ProductManager.GetPublishedProductByItemID(itemId);

            //tbl_Listing listing = propertyManager.GetListingByListingID(propertyId); //Listing

            //List<tbl_contacts> listingAgents = ContactManager.GetUsersByCompanyId(SessionParameters.ContactCompany.ContactCompanyID); // propertyManager.GetListingAgentsByListingID(propertyId); //Listing Agents
           


            //List<tbl_ListingImage> listingImages = propertyManager.GetListingImagesByListingID(propertyId); //Listing Images
            //List<tbl_ListingOFID> listingOFIDs = propertyManager.GetListingOFIDsByListingID(propertyId); //Listing OFIDs
            //List<tbl_ListingFloorPlan> listingFloorPlans = propertyManager.GetListingFloorPlansByListingID(propertyId); //Listing Floorplans
            //List<tbl_ListingLink> listingLinks = propertyManager.GetListingLinksByListingID(propertyId); //Listing Links
            //List<tbl_ListingConjunctionAgent> listingConjuctionAgents = propertyManager.GetListingConjunctionAgentsByListingID(propertyId); //Listing ConjunctionAgents
            //List<tbl_ListingVendor> listingVendors = propertyManager.GetListingVendorsByListingID(propertyId); //Listing Vendors


            //txtUnitNumber.Text = Convert.ToString(listing.UnitNumber);
            //txtStreetNumber.Text = Convert.ToString(listing.StreetNumber);
            //txtPropertyType.Text = listing.PropertyType;
            //txtCategory.Text = listing.PropertyCategory;
            //txtStreetName.Text = listing.Street;
            //txtConstruction.Text = listing.Construction;
            //txtStyle.Text = listing.Features;
            //txtSuburb.Text = listing.Suburb;
            //txtLandSize.Text = Convert.ToString(listing.LandArea);
            //txtBuildingSize.Text = Convert.ToString(listing.BuildingAreaSqm);
            //txtState.Text = listing.State;
            //txtPostcode.Text = listing.PostCode;
            //txtCondition.Text = listing.PropertyCondition;
            //txtViewAspect.Text = listing.Aspect;
            //txtListingId.Text = Convert.ToString(listing.ClientListingID);
            //txtBedrooms.Text = Convert.ToString(listing.BedRooms);
            //txtBathrooms.Text = Convert.ToString(listing.BathRooms);
            //txtLoungeRooms.Text = Convert.ToString(listing.LoungeRooms);
            //txtToilets.Text = Convert.ToString(listing.Toilets);
            //txtCouncil.Text = Convert.ToString(listing.CounsilRates);
            //txtStarta.Text = Convert.ToString(listing.StrataSinking); ////
            //txtWater.Text = Convert.ToString(listing.WaterRates);
            //txtLand.Text = Convert.ToString(listing.LandTax); ////
            //txtStartaAdmin.Text = Convert.ToString(listing.StrataAdmin);
            //txtOther.Text = Convert.ToString(listing.OtherOutgoings);
            //txtStudies.Text = Convert.ToString(listing.Studies);
            //txtPools.Text = Convert.ToString(listing.Pools);
            //txtGarages.Text = Convert.ToString(listing.Garages);
            //txtCarSpaces.Text = Convert.ToString(listing.CarSpaces);

            //txtListingType.Text = listing.ListingType;
            //txtPrice.Text = Convert.ToString(listing.DisplayPrice);
            //txtPriceGuid.Text = Convert.ToString(listing.SearchPrice); ////
            //txtWebsite.Text = listing.WebLink;
            //if (listing.AvailableDate != null)
            //{
            //    txtInspectionDate.Text = listing.AvailableDate;
            //    //).Date.Day + "/" + Convert.ToDateTime(listing.AvailableDate).Date.Month + "/" + Convert.ToDateTime(listing.AvailableDate).Date.Year;
            //}
            //if (listing.AuctionDate != null)
            //{
            //    txtAuctionDate.Text = listing.AuctionDate;//).Date.Day + "/" + Convert.ToDateTime(listing.AuctionDate).Date.Month + "/" + Convert.ToDateTime(listing.AuctionDate).Date.Year; 
            //}

            //txtInspectionEndTime.Text = Convert.ToString(listing.InspectionEndTime);
            //txtInspectionStartTime.Text = Convert.ToString(listing.InspectionStartTime);

            //txtAuctionStartTime.Text = Convert.ToString(listing.AuctionStartTime);
            //txtAuctionEndTime.Text = Convert.ToString(listing.AuctionEndTime);
            //txtLocation.Text = listing.AutionVenue;
            //txtbrochuresDesc.Text = listing.BrochureDescription;
            //txtbrochuresMainHeading.Text = listing.BrochureMainHeadLine;
            //txtbrochuresSummery.Text = listing.BrochureSummary;
            //txtSignboardDesc.Text = listing.SignBoardDescription;
            //txtSignboardMainHead.Text = listing.SignBoardMainHeadLine;
            //txtSignboardSummery.Text = listing.SignBoardSummary;
            //txtPressDescription.Text = listing.AdvertsDescription;
            //txtPressMainHead.Text = listing.AdvertsMainHeadLine;
            //txtPressSummery.Text = listing.AdvertsSummary;
            //txtInstalationInst.Text = listing.SignBoardInstallInstruction;
            //if (listingAgents != null && listingAgents.Count > 0)
            //{
            //    hfAgentsCount.Value = listingAgents.Count.ToString();
            //    // get all contacts
            //    rptAgents.DataSource = listingAgents;
            //    rptAgents.DataBind();
            //}

            //if (listingBulletPoint != null && listingBulletPoint.Count > 0)
            //{
            //    // get all bullet points
            //    rptBulletPoints.DataSource = listingBulletPoint;
            //    rptBulletPoints.DataBind();
            //}
        }

        private void loadUserProfiles(Roles role)
        {
            if (role == Roles.Adminstrator)
            {
                //corp Admin
                List<CompanyContact> objContacts = _myCompanyService.GetCorporateUserOnly(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
                //foreach (var val in objContacts)
                //{
                //    dropBoxSelectUser.Items.Add(new ListItem(val.FirstName + " " + val.LastName, val.ContactID.ToString()));
                //}
                //dropBoxSelectUser.SelectedValue = SessionParameters.CustomerContact.ContactID.ToString();

            }
            else if (role == Roles.Manager)
            {

                List<CompanyContact> objContacts = _myCompanyService.GetContactsByTerritory(UserCookieManager.WBStoreId, _webstoreclaimHelper.loginContactTerritoryID());
                //foreach (var val in objContacts)
                //{
                //    dropBoxSelectUser.Items.Add(new ListItem(val.FirstName + " " + val.LastName, val.ContactID.ToString()));
                //}
                //dropBoxSelectUser.SelectedValue = SessionParameters.CustomerContact.ContactID.ToString();
            }
            else if (role == Roles.User)
            {
                //dropDownContainer.Visible = false;
                //profileContainer.Style.Add("display", "block");
                // corp user
            }
        }
        
    }
}