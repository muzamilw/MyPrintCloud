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
using Newtonsoft.Json;

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
        public ActionResult Index( string PropertyId)
        {
          //  string id
           
            long PropID =Convert.ToInt64(PropertyId);
            if (PropID > 0)
            {
                ViewBag.isNewListing = 0;

                Listing Listing = _myListingService.GetListingByListingID(Convert.ToInt32(PropertyId));
        
                ViewBag.Listings = Listing;
                ViewBag.ListingBulletPoints = _myCompanyService.GetAllListingBulletPoints(PropID);
                ViewBag.ListingImages = GetAllListingImages(PropID);
                ViewBag.ListingID = PropID;
                return PartialView("PartialViews/RealEstateSmartForm", Listing);
            }
            else
            {
                ViewBag.isNewListing = 1;
                Listing Listing = null;
                long cloneListingId = 0;
                bool isCreateNewLiting = false;
                if (TempData["CloneListingId"] != null)
                {
                    cloneListingId = Convert.ToInt32(TempData["CloneListingId"]);
                    Listing = _myListingService.GetListingByListingID(Convert.ToInt32(cloneListingId));
                    if(Listing == null)
                    {
                        isCreateNewLiting = true;
                    }
                    else
                    {
                         isCreateNewLiting = false;
                         TempData.Keep("CloneListingId");
                    }
                  
                }
                else 
                {
                    isCreateNewLiting = true;
                
                }
                if(isCreateNewLiting)
                {
                     Listing newListingObj = new Listing();
                    
                    cloneListingId = _myCompanyService.AddNewListing(newListingObj);
                    TempData["CloneListingId"] = cloneListingId;
                    Listing = _myListingService.GetListingByListingID(Convert.ToInt32(cloneListingId));
                }
                
                ViewBag.ListingBulletPoints = _myCompanyService.GetAllListingBulletPoints(cloneListingId);
                ViewBag.ListingImages = GetAllListingImages(cloneListingId);
                ViewBag.ListingID = PropID;
                return PartialView("PartialViews/RealEstateSmartForm", Listing);
           
            }
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

        [HttpPost]
        public void Index(MPC.Models.DomainModels.Listing Listing, string ListingId)
        {
            try
            {
                TempData["CloneListingId"] = null;
                List<ListingBulletPoint> BulletList = new List<ListingBulletPoint>();
                List<ListingBulletPoint> UpdatedBulletsList = new List<ListingBulletPoint>();
                List<ListingImage> DeleteImagesList = new List<ListingImage>();
                var ll = Request.Form["BulletList"];
                var UpDatedBullets = Request.Form["BulletListUpdated"];

                if (UpDatedBullets != null && UpDatedBullets != string.Empty)
                {
                    string[] words = UpDatedBullets.Split('/');
                    foreach (var i in words)
                    {
                        ListingBulletPoint model = new ListingBulletPoint();
                        if (i != string.Empty)
                        {
                            model.BulletPoint = i;
                            UpdatedBulletsList.Add(model);
                        }
                    }

                }

                if (ll != null && ll != string.Empty)
                {
                    
                    string[] words = ll.Split('/');
                    foreach (var i in words)
                    {
                        ListingBulletPoint model = new ListingBulletPoint();
                        if (i != string.Empty)
                        {
                            model.BulletPoint = i;
                            BulletList.Add(model);
                        }
                    }
                    
                }

             

                var Img = Request.Form["DeleteImagesList"];

                    if(Img!=null&&Img!=string.Empty)
                    {

                        
                        string[] words = Img.Split('&');
                        foreach (var i in words)
                        {

                            if (i != string.Empty)
                            {
                                ListingImage model = new ListingImage();
                                string[] imgword = i.Split('/');
                              //  if (imgword[0] == string.Empty)
                              //  {
                                    model.ListingImageId = Convert.ToInt64(imgword[0]);
                                    model.ImageURL = '/'+imgword[1] + '/' + imgword[2] + '/' + imgword[3]+'/'+imgword[4] + '/'+ imgword[5];
                                    DeleteImagesList.Add(model);
                               // }
                              //  else
                              //  {
                                  
                               // }
                               
                            }
                        }
                       
                    }
             //   List<ListingBulletPoint> BulletList = JsonConvert.DeserializeObject<List<ListingBulletPoint>>(Request.Form.Get("BulletList"));



             //   List<ListingImage> DeleteImagesList = JsonConvert.DeserializeObject<List<ListingImage>>(Request.Form.Get("DeleteImagesList"));


                    
              //  if (!ListingId.Equals(String.Empty))
             //   {
                    int LisId = Convert.ToInt32(ListingId);
                    MPC.Models.DomainModels.Listing updateListing = new MPC.Models.DomainModels.Listing();
                    updateListing.UnitNumber = Listing.UnitNumber;
                    updateListing.StreetNumber = Listing.StreetNumber;
                    updateListing.PropertyName = Listing.PropertyName;
                    updateListing.PropertyType = Listing.PropertyType;
                    updateListing.ClientListingId = Listing.ClientListingId;
                    updateListing.PropertyCategory = Listing.PropertyCategory;
                    updateListing.Street = Listing.Street;
                    updateListing.Construction = Listing.Construction;
                    updateListing.Features = Listing.Features;
                    updateListing.Suburb = Listing.Suburb;
                    updateListing.LandArea = Listing.LandArea;
                    updateListing.BuildingAreaSqm = Listing.BuildingAreaSqm;
                    updateListing.State = Listing.State;
                    updateListing.PostCode = Listing.PostCode;
                    updateListing.PropertyCondition = Listing.PropertyCondition;
                    updateListing.Aspect = Listing.Aspect;
                    updateListing.ListingId = Listing.ListingId;
                    updateListing.BedRooms = Listing.BedRooms;
                    updateListing.BathRooms = Listing.BathRooms;
                    updateListing.LoungeRooms = Listing.LoungeRooms;
                    updateListing.Toilets = Listing.Toilets;
                    updateListing.CounsilRates = Listing.CounsilRates;
                    updateListing.StrataAdmin = Listing.StrataAdmin; ////
                    updateListing.StrataSinking = Listing.StrataSinking;
                    updateListing.WaterRates = Listing.WaterRates;
                    updateListing.LandTax = Listing.LandTax; ////
                    //txtStartaAdmin.Text = txtStartaAdmin.Text;// Convert.ToString(updateListing.StrataAdmin);
                    updateListing.OtherOutgoings = Listing.OtherOutgoings;
                    updateListing.Studies = Listing.Studies;
                    updateListing.Pools = Listing.Pools;
                    updateListing.Garages = Listing.Garages;
                    updateListing.CarSpaces = Listing.CarSpaces;
                    updateListing.ListingType = Listing.ListingType;
                    updateListing.DisplayPrice = Listing.DisplayPrice;
                    updateListing.SearchPrice = Listing.SearchPrice;
                    updateListing.WebLink = Listing.WebLink;
                    updateListing.AvailableDate = Listing.AvailableDate;
                    updateListing.InspectionTimeFrom2 = Listing.InspectionTimeFrom2;
                    updateListing.InspectionTimeFrom1 = Listing.InspectionTimeFrom1;
                    updateListing.AuctionTime = Listing.AuctionTime;
                    updateListing.AuctionEndTime = Listing.AuctionEndTime;
                    updateListing.AuctionDate = Listing.AuctionDate;
                    updateListing.AutionVenue = Listing.AutionVenue;
                    updateListing.BrochureDescription = Listing.BrochureDescription;
                    updateListing.BrochureMainHeadLine = Listing.BrochureMainHeadLine;
                    updateListing.BrochureSummary = Listing.BrochureSummary;
                    updateListing.SignBoardDescription = Listing.SignBoardDescription;
                    updateListing.SignBoardMainHeadLine = Listing.SignBoardMainHeadLine;
                    updateListing.SignBoardSummary = Listing.SignBoardSummary;
                    updateListing.AdvertsDescription = Listing.AdvertsDescription;
                    updateListing.AdvertsMainHeadLine = Listing.AdvertsMainHeadLine;
                    updateListing.AdvertsSummary = Listing.AdvertsSummary;
                    updateListing.SignBoardInstallInstruction = Listing.SignBoardInstallInstruction;
                   
                    updateListing.CompanyId = UserCookieManager.WBStoreId;
                    MPC.Models.DomainModels.Listing listing = _myCompanyService.GetListingByListingID(LisId);//Listing
                    if (DeleteImagesList != null)
                    {
                        foreach (var image in DeleteImagesList)
                        {
                            _myCompanyService.DeleteListingImage(image.ListingImageId);
                             RemovePreviousFile(image.ImageURL);
                        }
                    
                    }
                    long UpdatedLisTingID = _myCompanyService.UpdateListing(updateListing, listing);
                  
                    if (UpdatedBulletsList != null && UpdatedBulletsList.Count > 0)
                    {
                        _myCompanyService.UpdateBulletPoints(UpdatedBulletsList, UpdatedLisTingID);
                    }
                    if (BulletList != null && BulletList.Count > 0)
                    {
                        _myCompanyService.AddBulletPoint(BulletList, UpdatedLisTingID);
                    }
             //   /    if (UpdatedLisTingID > 0)
                  //  {
               //         // return RedirectToAction("Index", "RealEstateListingGrid");
                        Response.Redirect("/RealEstateListingGrid");
                 //   }
                
               // else
                //{
                //    MPC.Models.DomainModels.Listing updateListing = new MPC.Models.DomainModels.Listing();
                //    updateListing.UnitNumber = Listing.UnitNumber;
                //    updateListing.StreetNumber = Listing.StreetNumber;
                //    updateListing.PropertyType = Listing.PropertyType;
                //    updateListing.PropertyCategory = Listing.PropertyCategory;
                //    updateListing.Street = Listing.Street;
                //    updateListing.Construction = Listing.Construction;
                //    updateListing.Features = Listing.Features;
                //    updateListing.Suburb = Listing.Suburb;
                //    updateListing.LandArea = Listing.LandArea;
                //    updateListing.BuildingAreaSqm = Listing.BuildingAreaSqm;
                //    updateListing.State = Listing.State;
                //    updateListing.PostCode = Listing.PostCode;
                //    updateListing.PropertyCondition = Listing.PropertyCondition;
                //    updateListing.Aspect = Listing.Aspect;
                //    updateListing.ListingId = Listing.ListingId;
                //    updateListing.BedRooms = Listing.BedRooms;
                //    updateListing.BathRooms = Listing.BathRooms;
                //    updateListing.LoungeRooms = Listing.LoungeRooms;
                //    updateListing.Toilets = Listing.Toilets;
                //    updateListing.CounsilRates = Listing.CounsilRates;
                //    updateListing.StrataAdmin = Listing.StrataAdmin; ////
                //    updateListing.StrataSinking = Listing.StrataSinking;
                //    updateListing.WaterRates = Listing.WaterRates;
                //    updateListing.LandTax = Listing.LandTax; ////
                //    //txtStartaAdmin.Text = txtStartaAdmin.Text;// Convert.ToString(updateListing.StrataAdmin);
                //    updateListing.OtherOutgoings = Listing.OtherOutgoings;
                //    updateListing.Studies = Listing.Studies;
                //    updateListing.Pools = Listing.Pools;
                //    updateListing.Garages = Listing.Garages;
                //    updateListing.CarSpaces = Listing.CarSpaces;
                //    updateListing.ListingType = Listing.ListingType;
                //    updateListing.DisplayPrice = Listing.DisplayPrice;
                //    updateListing.SearchPrice = Listing.SearchPrice;
                //    updateListing.WebLink = Listing.WebLink;
                //    updateListing.AvailableDate = Listing.AvailableDate;
                //    updateListing.InspectionTimeFrom2 = Listing.InspectionTimeFrom2;
                //    updateListing.InspectionTimeFrom1 = Listing.InspectionTimeFrom1;
                //    updateListing.AuctionTime = Listing.AuctionTime;
                //    updateListing.AuctionEndTime = Listing.AuctionEndTime;
                //    updateListing.AuctionDate = Listing.AuctionDate;
                //    updateListing.AutionVenue = Listing.AutionVenue;
                //    updateListing.BrochureDescription = Listing.BrochureDescription;
                //    updateListing.BrochureMainHeadLine = Listing.BrochureMainHeadLine;
                //    updateListing.BrochureSummary = Listing.BrochureSummary;
                //    updateListing.SignBoardDescription = Listing.SignBoardDescription;
                //    updateListing.SignBoardMainHeadLine = Listing.SignBoardMainHeadLine;
                //    updateListing.SignBoardSummary = Listing.SignBoardSummary;
                //    updateListing.AdvertsDescription = Listing.AdvertsDescription;
                //    updateListing.AdvertsMainHeadLine = Listing.AdvertsMainHeadLine;
                //    updateListing.AdvertsSummary = Listing.AdvertsSummary;
                //    updateListing.SignBoardInstallInstruction = Listing.SignBoardInstallInstruction;
                //    updateListing.CompanyId = UserCookieManager.WBStoreId;
                //    long Result = _myCompanyService.AddNewListing(updateListing);
                //    if (BulletList != null && BulletList.Count > 0)
                //    {
                //        _myCompanyService.AddBulletPoint(BulletList, Result);
                //    }

                //}
                
                
                //AddNewListing
                    
            }
                

            catch (Exception)
            {

                throw;
            }
        }

        private void RemovePreviousFile(string previousFileToremove)
        {
            TempData["CloneListingId"] = null;
            if (!string.IsNullOrEmpty(previousFileToremove))
            {
                string ServerPath = HttpContext.Server.MapPath(previousFileToremove);
                if (System.IO.File.Exists(ServerPath))
                {
                    Utils.DeleteFile(ServerPath);
                }
            }
        }
    }
}