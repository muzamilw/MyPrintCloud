using MPC.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MPC.Webstore.Areas.WebstoreApi.Controllers
{
    public class RealEstateSMController : ApiController
    {
        public RealEstateSMController()
        { 
        
        }

       

        //private void UpdateListingData(int listingId)
        //{
        //    try
        //    {


        //        Listing updateListing = new Listing();
        //        updateListing.UnitNum = txtUnitNumber.Text;
        //        updateListing.StreetNum = txtStreetNumber.Text;
        //        updateListing.PropertyType = txtPropertyType.Text;
        //        updateListing.PropertyCategory = txtCategory.Text;
        //        updateListing.Street = txtStreetName.Text;
        //        updateListing.Construction = txtConstruction.Text;
        //        updateListing.Features = txtStyle.Text;
        //        updateListing.Suburb = txtSuburb.Text;
        //        updateListing.LandArea = txtLandSize.Text;
        //        updateListing.BuildingAreaSqm = txtBuildingSize.Text;
        //        updateListing.State = txtState.Text;
        //        updateListing.Postcode = txtPostcode.Text;
        //        updateListing.PropertyCondition = txtCondition.Text;
        //        updateListing.Aspect = txtViewAspect.Text;
        //        updateListing.ListingID = txtListingId.Text;
        //        updateListing.BedRooms = txtBedrooms.Text;
        //        updateListing.BathRooms = txtBathrooms.Text;
        //        updateListing.LoungeRooms = txtLoungeRooms.Text;
        //        updateListing.Toilets = txtToilets.Text;
        //        updateListing.CounsilRates = txtCouncil.Text;
        //        updateListing.StrataAdmin = txtStartaAdmin.Text; ////
        //        updateListing.StrataSinking = txtStarta.Text;
        //        updateListing.WaterRates = txtWater.Text;
        //        updateListing.LandTax = txtLand.Text; ////
        //        // txtStartaAdmin.Text = txtStartaAdmin.Text;// Convert.ToString(updateListing.StrataAdmin);
        //        updateListing.OtherOutgoings = txtOther.Text;
        //        updateListing.Studies = txtStudies.Text;
        //        updateListing.Pools = txtPools.Text;
        //        updateListing.Garages = txtGarages.Text;
        //        updateListing.CarSpaces = txtCarSpaces.Text;

        //        updateListing.ListingType = txtListingType.Text;
        //        updateListing.DisplayPrice = txtPrice.Text;
        //        updateListing.SearchPrice = txtPriceGuid.Text;

        //        updateListing.WebLink = txtWebsite.Text;
        //        updateListing.AvailableDate = txtInspectionDate.Text;
        //        updateListing.InspectionEndTime = txtInspectionEndTime.Text;
        //        updateListing.InspectionStartTime = txtInspectionStartTime.Text;
        //        updateListing.AuctionStartTime = txtAuctionStartTime.Text;
        //        updateListing.AuctionEndTime = txtAuctionEndTime.Text;

        //        updateListing.AuctionDate = txtAuctionDate.Text;
        //        updateListing.AuctionVenue = txtLocation.Text;
        //        updateListing.BroucherDescription = txtbrochuresDesc.Text;
        //        updateListing.BroucherMainHeading = txtbrochuresMainHeading.Text;
        //        updateListing.BroucherSummery = txtbrochuresSummery.Text;
        //        updateListing.SignBoardDescription = txtSignboardDesc.Text;
        //        updateListing.SignBoardMainHeading = txtSignboardMainHead.Text;
        //        updateListing.SignBoardSummery = txtSignboardSummery.Text;
        //        updateListing.PressDescription = txtPressDescription.Text;
        //        updateListing.PressMainHeading = txtPressMainHead.Text;
        //        updateListing.PressSummery = txtPressSummery.Text;
        //        updateListing.InstallationInstructio = txtInstalationInst.Text;
        //        tbl_Listing listing = propertyManager.GetListingByListingID(propertyId); //Listing
        //        UpdateListing(updateListing, listing);

        //        //List<tbl_ListingAgent> listingAgents = propertyManager.GetListingAgentsByListingID(propertyId); //Listing Agents

        //        // for agents
        //        List<tbl_contacts> updateAgentList = new List<tbl_contacts>();
        //        foreach (RepeaterItem rptItem in rptAgents.Items)
        //        {

        //            if (rptItem.ItemType == ListItemType.Item || rptItem.ItemType == ListItemType.AlternatingItem)
        //            {
        //                tbl_contacts contact = new tbl_contacts();
        //                contact.FirstName = (rptItem.FindControl("txtAgent1Name") as TextBox).Text;
        //                contact.Mobile = (rptItem.FindControl("txtAgent1Mobile") as TextBox).Text;
        //                contact.HomeTel1 = (rptItem.FindControl("txtAgent1Telephone") as TextBox).Text;
        //                contact.Email = (rptItem.FindControl("txtAgent1Email") as TextBox).Text;
        //                updateAgentList.Add(contact);
        //            }
        //        }

        //        PropertyManager.UpdateAgent(updateAgentList);
        //        if (!string.IsNullOrEmpty(hfAgentList.Value))
        //        {
        //            ListAgentMode agentsList = JsonConvert.DeserializeObject<ListAgentMode>(hfAgentList.Value);
        //            if (agentsList != null)
        //            {
        //                PropertyManager.AddAgent(agentsList, SessionParameters.ContactCompany.ContactCompanyID);
        //            }
        //        }

        //        // for bullet points

        //        List<tbl_ListingBulletPoints> updatePointList = new List<tbl_ListingBulletPoints>();
        //        foreach (RepeaterItem rptItem in rptBulletPoints.Items)
        //        {

        //            if (rptItem.ItemType == ListItemType.Item || rptItem.ItemType == ListItemType.AlternatingItem)
        //            {
        //                tbl_ListingBulletPoints oPoint = new tbl_ListingBulletPoints();
        //                oPoint.BulletPoint = (rptItem.FindControl("txtBuletPoints") as TextBox).Text;
        //                oPoint.BulletPointId = Convert.ToInt32((rptItem.FindControl("lblPointId") as Label).Text);
        //                updatePointList.Add(oPoint);
        //            }
        //        }
        //        PropertyManager.UpdateBulletPoints(updatePointList);
        //        if (!string.IsNullOrEmpty(hfBPointsList.Value))
        //        {
        //            ListPointsModel bulletList = JsonConvert.DeserializeObject<ListPointsModel>(hfBPointsList.Value);
        //            if (bulletList != null)
        //            {
        //                PropertyManager.AddBulletPoint(bulletList, listing.ListingID);
        //            }
        //        }
        //        hfNewlyAddedBPoints.Value = "";
        //        //if (!string.IsNullOrEmpty(txtBulletPoint.Text))
        //        //{
        //        //    PropertyManager.AddBulletPoint(txtBulletPoint.Text, listing.ListingID);
        //        //}
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //private void GotoDesigner(int itemID, int productCategoryID, List<TemplateVariable> lstPageControls, int propertyId)
        //{
        //    Dictionary<string, string> parameterNameValueList = new Dictionary<string, string>();
        //    int ItemID = 0;
        //    int TemplateID = 0;
        //    tbl_contacts objContact = updateContact();

        //    if (ProcessOrder(propertyId))
        //    {
        //        if (SessionParameters.StoreMode == StoreMode.Corp)
        //        {
        //            tbl_items items = (new ProductManager()).CloneItem(itemID, 0, 0, SessionParameters.ProductSelection.OrderID, SessionParameters.CustomerID, 0, TemplateID, 0, null, true, false, false, SessionParameters.ContactCompany, objContact, null, lstPageControls);
        //            ItemID = items.ItemID;
        //            TemplateID = items.TemplateID.Value;
        //        }
        //    }
        //    else
        //    {
        //        throw new Exception("order processing error");
        //    }

        //    parameterNameValueList.Add(ParameterName.CATEGORY_ID, productCategoryID.ToString());
        //    parameterNameValueList.Add(ParameterName.ITEM_ID, ItemID.ToString());
        //    parameterNameValueList.Add(ParameterName.TEMPLATE_DESIGN_ID, TemplateID.ToString());

        //    if (IsUserLoggedIn())
        //    {
        //        parameterNameValueList.Add(ParameterName.CUSTOMER_ID, SessionParameters.CustomerID.ToString());
        //        parameterNameValueList.Add(ParameterName.CONTACT_ID, SessionParameters.ContactID.ToString());
        //    }
        //    else
        //    {
        //        if (SessionParameters.ProductSelection != null)
        //        {
        //            if (SessionParameters.ProductSelection.CustomerID > 0)
        //            {
        //                int ContctID = ContactManager.GetContactIdByCustomrID(SessionParameters.ProductSelection.CustomerID);
        //                parameterNameValueList.Add(ParameterName.CUSTOMER_ID, SessionParameters.ProductSelection.CustomerID.ToString());
        //                parameterNameValueList.Add("ContactID", ContctID.ToString());
        //            }
        //        }
        //    }

        //    string username = Utils.specialCharactersEncoder(objContact.FirstName + " " + objContact.LastName);
        //    parameterNameValueList.Add(ParameterName.IsEmbedded, "true");
        //    parameterNameValueList.Add(ParameterName.IsCalledFrom, "4");
        //    parameterNameValueList.Add(ParameterName.designOwner, username);
        //    parameterNameValueList.Add(ParameterName.PROPERTY_ID, propertyId.ToString());
        //    Response.Redirect(Utils.BuilQueryString("TempDesigner.aspx", parameterNameValueList), false);

        //}
        //public bool ProcessOrder(int listingID)
        //{
        //    if (SessionParameters.BrokerContactCompany != null)
        //    {
        //        return ProcessPublicUserOrderLocal(string.Empty, SessionParameters.BrokerContactCompany.ContactCompanyID, listingID);
        //    }
        //    else
        //    {
        //        return ProcessPublicUserOrderLocal(string.Empty, 0, listingID);
        //    }
        //}

        //public bool ProcessPublicUserOrderLocal(string orderTitle, int brokerid, int listingID)
        //{
        //    int customerID = 0;
        //    long orderID = 0;

        //    if (!IsUserLoggedIn())
        //    {
        //        if (!CheckCustomerCookie())
        //        {
        //            customerID = CreateCustomer();
        //            int CID = ContactManager.GetContactIdByCustomrID(customerID);
        //            orderID = OrderManager.CreateNewRealEstateOrder(customerID, CID, brokerid, listingID, orderTitle); //Here Ofcourse for new Customer There shall not be an order exists so we need to create one
        //        }
        //        else
        //        {
        //            customerID = this.GetCustomerIDFromCookie(); //dummy customer
        //            tbl_contactcompanies tblCustomer = CustomerManager.GetCustomer(customerID);
        //            if (tblCustomer == null)
        //                customerID = this.CreateCustomer();

        //            int CID = ContactManager.GetContactIdByCustomrID(customerID);
        //            orderID = OrderManager.GetOrderID(customerID, CID, brokerid, orderTitle);
        //        }
        //    }
        //    else
        //    {
        //        //user is Loggged in
        //        //Then get customer

        //        // Sajid Ali
        //        // When user is logged in then we have the contact id why to get order by customer id.
        //        orderID = GetRealEstateOrderID(SessionParameters.CustomerID, SessionParameters.ContactID, brokerid, listingID, orderTitle);
        //        customerID = SessionParameters.CustomerID;
        //    }

        //    //Finally set the order id in session
        //    if (SessionParameters.ProductSelection == null)
        //    {
        //        SessionParameters.ProductSelection = new Model.ProductSelection();
        //    }

        //    SessionParameters.ProductSelection.CustomerID = customerID;
        //    SessionParameters.ProductSelection.OrderID = orderID;

        //    return true;
        //}
        //public long GetRealEstateOrderID(int customerID, int contactId, int brokerID, int listingID, string orderTitle)
        //{
        //    long orderID = 0;
        //    tbl_estimates tblOrder = OrderManager.GetOrderByContactID(contactId, Web2Print.BLL.OrderManager.OrderStatus.ShoppingCart);

        //    if (tblOrder == null)
        //        orderID = OrderManager.CreateNewRealEstateOrder(customerID, contactId, brokerID, listingID, orderTitle);
        //    else
        //    {
        //        using (MPCEntities context = new MPCEntities())
        //        {
        //            tbl_estimates UpdatedOrder = context.tbl_estimates.Where(i => i.EstimateID == tblOrder.EstimateID).FirstOrDefault();
        //            UpdatedOrder.ListingID = listingID;
        //            context.SaveChanges();
        //            orderID = tblOrder.EstimateID;
        //        }
        //    }
        //    tblOrder = null;

        //    return orderID;
        //}

        //public tbl_contacts updateContact()
        //{
        //    tbl_contacts objContact = null;
        //    int sid = 0;
        //    if (hfSPType.Value == "default")
        //    {
        //        sid = SessionParameters.CustomerContact.ContactID;
        //    }
        //    else
        //    {
        //        sid = Convert.ToInt32(dropBoxSelectUser.SelectedValue);
        //    }
        //    using (MPCEntities db = new MPCEntities())
        //    {
        //        objContact = db.tbl_contacts.Where(g => g.ContactID == sid).SingleOrDefault();
        //        if (objContact != null)
        //        {
        //            // quick text 
        //            objContact.quickFullName = txtQname.Value;
        //            objContact.quickTitle = txtQTitle.Value;
        //            objContact.quickCompanyName = txtQCompany.Value;
        //            objContact.quickAddress1 = txtAddress.Value;
        //            objContact.quickPhone = txtQtel.Value;
        //            objContact.quickFax = txtQFax.Value;
        //            objContact.quickEmail = txtQEmail.Value;
        //            objContact.quickWebsite = txtQWebsite.Value;
        //            objContact.quickCompMessage = txtQMessage.Value;
        //            //contact
        //            objContact.FirstName = txtFName.Value;
        //            objContact.MiddleName = txtMname.Value;
        //            objContact.LastName = txtLname.Value;
        //            objContact.Title = txtTitle.Value;
        //            objContact.HomeTel1 = txtHomeTel1.Value;
        //            objContact.HomeTel2 = txtHomeTel2.Value;
        //            objContact.HomeExtension1 = txtHomeExt1.Value;
        //            objContact.HomeExtension2 = txtHomeExt2.Value;
        //            objContact.Mobile = txtMobile.Value;
        //            objContact.Pager = txtPager.Value;
        //            objContact.FAX = txtFax.Value;
        //            objContact.JobTitle = txtJtitle.Value;
        //            objContact.LinkedinURL = txtLinkedInURL.Value;
        //            objContact.SkypeID = txtSkypeID.Value;
        //            objContact.TwitterURL = txtTwitterURL.Value;
        //            objContact.FacebookURL = txtFbID.Value;
        //            objContact.URL = txtURL.Value;
        //            objContact.POBoxAddress = txtPOBox.Value;
        //            objContact.CorporateUnit = txtCunit.Value;
        //            objContact.OfficeTradingName = txtOfcTrading.Value;
        //            objContact.ContractorName = txtContractorName.Value;
        //            objContact.BPayCRN = txtBpay.Value;
        //            objContact.ABN = txtABN.Value;
        //            objContact.ACN = txtACN.Value;
        //            objContact.AdditionalField1 = txtAf1.Value;
        //            objContact.AdditionalField2 = txtAf2.Value;
        //            objContact.AdditionalField3 = txtAf3.Value;
        //            objContact.AdditionalField4 = txtAf4.Value;
        //            objContact.AdditionalField5 = txtAf5.Value;

        //            db.SaveChanges();
        //        }
        //    }
        //    return objContact;
        //}
        //private int UpdateAgent(int agentId, ListingAgents updatedAgent)
        //{
        //    int updatedAgentId = 0;
        //    try
        //    {

        //        using (MPCEntities context = new MPCEntities())
        //        {
        //            var agent = context.tbl_ListingAgent.Where(item => item.AgentID == agentId).FirstOrDefault();

        //            if (agent != null)
        //            {
        //                agent.Name = updatedAgent.Name;
        //                agent.Phone = updatedAgent.Phone;
        //                agent.Mobile = updatedAgent.Mobile;
        //                agent.Email = updatedAgent.Email;

        //                if (context.SaveChanges() > 0)
        //                {
        //                    updatedAgentId = agent.AgentID;
        //                }
        //            }
        //        }

        //        return updatedAgentId;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //private int UpdateListing(Listing propertyListing, tbl_Listing tblListing)
        //{
        //    int updatedListing = 0;
        //    try
        //    {
        //        string strForParse = string.Empty;

        //        using (MPCEntities context = new MPCEntities())
        //        {
        //            var listing = context.tbl_Listing.Where(item => item.ListingID == tblListing.ListingID).FirstOrDefault();

        //            if (listing != null)
        //            {
        //                //  listing.ClientListingID = propertyListing.ListingID;
        //                listing.WebID = propertyListing.WebID;
        //                listing.WebLink = propertyListing.WebLink;
        //                listing.AddressDisplay = propertyListing.AddressDisplay;
        //                listing.StreetAddress = propertyListing.StreetAddress;
        //                listing.LevelNumber = (String.IsNullOrEmpty(propertyListing.LevelNum)) ? 0 : Convert.ToInt32(propertyListing.LevelNum);
        //                listing.LotNumber = (String.IsNullOrEmpty(propertyListing.LotNum)) ? 0 : Convert.ToInt32(propertyListing.LotNum);
        //                listing.UnitNumber = (String.IsNullOrEmpty(propertyListing.UnitNum)) ? 0 : Convert.ToInt32(propertyListing.UnitNum);
        //                listing.StreetNumber = (String.IsNullOrEmpty(propertyListing.StreetNum)) ? 0 : Convert.ToInt32(propertyListing.StreetNum);
        //                listing.Street = propertyListing.Street;
        //                listing.Suburb = propertyListing.Suburb;
        //                listing.State = propertyListing.State;
        //                listing.PostCode = propertyListing.Postcode;
        //                listing.PropertyName = propertyListing.PropertyName;
        //                listing.PropertyType = propertyListing.PropertyType;
        //                listing.PropertyCategory = propertyListing.PropertyCategory;

        //                //if (!String.IsNullOrEmpty(propertyListing.ListingDate))
        //                //    listing.ListingDate = DateTime.Parse(propertyListing.ListingDate, culture, System.Globalization.DateTimeStyles.AssumeLocal);

        //                //if (!String.IsNullOrEmpty(propertyListing.ListingExpiryDate))
        //                //    listing.ListingExpiryDate = DateTime.Parse(propertyListing.ListingExpiryDate, culture, System.Globalization.DateTimeStyles.AssumeLocal);

        //                listing.ListingStatus = propertyListing.ListingStatus;
        //                listing.ListingMethod = propertyListing.ListingMethod;
        //                listing.ListingAuthority = propertyListing.ListingAuthority;
        //                listing.InspectionTypye = propertyListing.InspectionType;
        //                listing.ListingType = propertyListing.ListingType;
        //                if (!string.IsNullOrEmpty(propertyListing.WaterRates))
        //                {
        //                    listing.WaterRates = Convert.ToDouble(propertyListing.WaterRates);
        //                }
        //                else
        //                {
        //                    listing.WaterRates = null;
        //                }

        //                if (!string.IsNullOrEmpty(propertyListing.StrataAdmin))
        //                {
        //                    listing.StrataAdmin = Convert.ToDouble(propertyListing.StrataAdmin);
        //                }
        //                else
        //                {
        //                    listing.StrataAdmin = null;
        //                }

        //                if (!string.IsNullOrEmpty(propertyListing.StrataSinking))
        //                {
        //                    listing.StrataSinking = Convert.ToDouble(propertyListing.StrataSinking);
        //                }
        //                else
        //                {
        //                    listing.StrataSinking = null;
        //                }

        //                listing.AutionVenue = propertyListing.AuctionVenue;
        //                listing.AuctionDate = propertyListing.AuctionDate; //DateTime.ParseExact(propertyListing.AuctionDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

        //                //listing.AutionVenue = propertyListing.AuctionVenue;

        //                //if (!String.IsNullOrEmpty(propertyListing.EOIClosingDate))
        //                //    listing.EOIClosingDate = DateTime.Parse(propertyListing.EOIClosingDate, culture, System.Globalization.DateTimeStyles.AssumeLocal);

        //                if (!string.IsNullOrEmpty(propertyListing.DisplayPrice))
        //                {
        //                    strForParse = propertyListing.DisplayPrice;
        //                    string result = Regex.Replace(strForParse, @"[^\d]", "");
        //                    if (!result.Equals(string.Empty))
        //                        listing.DisplayPrice = Convert.ToDouble(result);
        //                }
        //                else
        //                {
        //                    listing.DisplayPrice = null;
        //                }
        //                if (!string.IsNullOrEmpty(propertyListing.SearchPrice))
        //                {
        //                    strForParse = propertyListing.SearchPrice;
        //                    string result = Regex.Replace(strForParse, @"[^\d]", "");
        //                    if (!result.Equals(string.Empty))
        //                        listing.SearchPrice = Convert.ToDouble(result);
        //                }
        //                else
        //                {
        //                    listing.SearchPrice = null;
        //                }

        //                listing.RendPeriod = (String.IsNullOrEmpty(propertyListing.RentPeriod)) ? 0 : Convert.ToInt32(propertyListing.RentPeriod);


        //                listing.AvailableDate = propertyListing.AvailableDate; //DateTime.ParseExact(propertyListing.AvailableDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

        //                listing.InspectionEndTime = propertyListing.InspectionEndTime;


        //                listing.InspectionStartTime = propertyListing.InspectionStartTime;


        //                listing.AuctionEndTime = propertyListing.AuctionEndTime;


        //                listing.AuctionStartTime = propertyListing.AuctionStartTime;

        //                //if (!String.IsNullOrEmpty(propertyListing.SoldDate))
        //                //    listing.SoldDate = DateTime.Parse(propertyListing.SoldDate, culture, System.Globalization.DateTimeStyles.AssumeLocal);

        //                if (propertyListing.SoldPrice != null)
        //                {
        //                    strForParse = propertyListing.SoldPrice;
        //                    string result = Regex.Replace(strForParse, @"[^\d]", "");
        //                    if (!result.Equals(string.Empty))
        //                        listing.SoldPrice = Convert.ToDouble(result);
        //                }
        //                else
        //                {
        //                    listing.SoldPrice = null;
        //                }

        //                if (propertyListing.SoldPriceConfidential != null)
        //                {
        //                    string confid = propertyListing.SoldPriceConfidential;
        //                    if (confid.Equals("Yes"))
        //                    {
        //                        listing.IsSoldPriceConfidential = true;
        //                    }
        //                    else if (confid.Equals("No"))
        //                    {
        //                        listing.IsSoldPriceConfidential = false;
        //                    }
        //                }
        //                else
        //                {
        //                    listing.IsSoldPriceConfidential = null;
        //                }
        //                listing.MainHeadLine = propertyListing.MainHeadline;
        //                listing.MainDescription = propertyListing.MainDescription;
        //                listing.BedRooms = (String.IsNullOrEmpty(propertyListing.BedRooms)) ? 0 : Convert.ToInt32(propertyListing.BedRooms);
        //                listing.BathRooms = (String.IsNullOrEmpty(propertyListing.BathRooms)) ? 0 : Convert.ToInt32(propertyListing.BathRooms);
        //                listing.LoungeRooms = (String.IsNullOrEmpty(propertyListing.LoungeRooms)) ? 0 : Convert.ToInt32(propertyListing.LoungeRooms);
        //                listing.Toilets = (String.IsNullOrEmpty(propertyListing.Toilets)) ? 0 : Convert.ToInt32(propertyListing.Toilets);
        //                listing.Studies = (String.IsNullOrEmpty(propertyListing.Studies)) ? 0 : Convert.ToInt32(propertyListing.Studies);
        //                listing.Pools = (String.IsNullOrEmpty(propertyListing.Pools)) ? 0 : Convert.ToInt32(propertyListing.Pools);
        //                listing.Garages = (String.IsNullOrEmpty(propertyListing.Garages)) ? 0 : Convert.ToInt32(propertyListing.Garages);
        //                listing.Carports = (String.IsNullOrEmpty(propertyListing.Carports)) ? 0 : Convert.ToInt32(propertyListing.Carports);
        //                listing.CarSpaces = (String.IsNullOrEmpty(propertyListing.CarSpaces)) ? 0 : Convert.ToInt32(propertyListing.CarSpaces);
        //                listing.TotalParking = (String.IsNullOrEmpty(propertyListing.TotalParking)) ? 0 : Convert.ToInt32(propertyListing.TotalParking);

        //                if (propertyListing.LandArea != null)
        //                {
        //                    strForParse = propertyListing.LandArea;
        //                    string result = Regex.Replace(strForParse, @"[^\d]", "");
        //                    if (!result.Equals(string.Empty))
        //                    {
        //                        listing.LandArea = Convert.ToDouble(result);
        //                    }
        //                    else
        //                    {
        //                        listing.LandArea = null;
        //                    }

        //                }
        //                else
        //                {
        //                    listing.LandArea = null;
        //                }
        //                listing.LandAreaUnit = propertyListing.LandAreaUnit;
        //                listing.BuildingAreaSqm = (String.IsNullOrEmpty(propertyListing.BuildingAreaSqm)) ? 0 : Convert.ToInt32(propertyListing.BuildingAreaSqm);
        //                listing.ExternalAreaSqm = (String.IsNullOrEmpty(propertyListing.ExternalAreaSqm)) ? 0 : Convert.ToInt32(propertyListing.ExternalAreaSqm);
        //                listing.FrontageM = (String.IsNullOrEmpty(propertyListing.FrontageM)) ? 0 : Convert.ToInt32(propertyListing.FrontageM);
        //                listing.Aspect = propertyListing.Aspect;
        //                listing.YearBuilt = propertyListing.YearBuilt;
        //                listing.YearRenovated = propertyListing.YearRenovated;
        //                listing.Construction = propertyListing.Construction;
        //                listing.PropertyCondition = propertyListing.PropertyCondition;

        //                if (propertyListing.EnergyRating != null)
        //                {
        //                    strForParse = propertyListing.EnergyRating;
        //                    string result = Regex.Replace(strForParse, @"[^\d]", "");
        //                    if (!result.Equals(string.Empty))
        //                    {
        //                        listing.EnergyRating = Convert.ToDouble(result);
        //                    }
        //                    else
        //                    {
        //                        listing.EnergyRating = null;
        //                    }

        //                }
        //                else
        //                {
        //                    listing.EnergyRating = null;
        //                }


        //                listing.Features = propertyListing.Features;

        //                if (propertyListing.LandTax != null)
        //                {
        //                    strForParse = propertyListing.LandTax;
        //                    string result = Regex.Replace(strForParse, @"[^\d]", "");
        //                    if (!result.Equals(string.Empty))
        //                    {
        //                        listing.LandTax = Convert.ToDouble(result);
        //                    }
        //                    else
        //                    {
        //                        listing.LandTax = null;
        //                    }

        //                }
        //                else
        //                {
        //                    listing.LandTax = null;
        //                }
        //                if (propertyListing.CounsilRates != null)
        //                {
        //                    strForParse = propertyListing.CounsilRates;
        //                    string result = Regex.Replace(strForParse, @"[^\d]", "");
        //                    if (!result.Equals(string.Empty))
        //                    {
        //                        listing.CounsilRates = Convert.ToDouble(result);
        //                    }
        //                    else
        //                    {
        //                        listing.CounsilRates = null;
        //                    }

        //                }
        //                else
        //                {
        //                    listing.CounsilRates = null;
        //                }

        //                if (propertyListing.StrataAdmin != null)
        //                {
        //                    strForParse = propertyListing.StrataAdmin;
        //                    string result = Regex.Replace(strForParse, @"[^\d]", "");
        //                    if (!result.Equals(string.Empty))
        //                    {
        //                        listing.StrataAdmin = Convert.ToDouble(result);
        //                    }
        //                    else
        //                    {
        //                        listing.StrataAdmin = null;
        //                    }
        //                }
        //                else
        //                {
        //                    listing.StrataAdmin = null;
        //                }

        //                if (propertyListing.StrataSinking != null)
        //                {
        //                    strForParse = propertyListing.StrataSinking;
        //                    string result = Regex.Replace(strForParse, @"[^\d]", "");
        //                    if (!result.Equals(string.Empty))
        //                    {
        //                        listing.StrataSinking = Convert.ToDouble(result);
        //                    }
        //                    else
        //                    {
        //                        listing.StrataSinking = null;
        //                    }

        //                }
        //                else
        //                {
        //                    listing.StrataSinking = null;
        //                }

        //                if (propertyListing.OtherOutgoings != null)
        //                {
        //                    strForParse = propertyListing.OtherOutgoings;
        //                    string result = Regex.Replace(strForParse, @"[^\d]", "");
        //                    if (!result.Equals(string.Empty))
        //                    {
        //                        listing.OtherOutgoings = Convert.ToDouble(result);
        //                    }
        //                    else
        //                    {
        //                        listing.OtherOutgoings = null;
        //                    }
        //                }
        //                else
        //                {
        //                    listing.OtherOutgoings = null;
        //                }

        //                if (propertyListing.TotalOutgoings != null)
        //                {
        //                    strForParse = propertyListing.TotalOutgoings;
        //                    string result = Regex.Replace(strForParse, @"[^\d]", "");
        //                    if (!result.Equals(string.Empty))
        //                    {
        //                        listing.TotalOutgoings = Convert.ToDouble(result);
        //                    }
        //                    else
        //                    {
        //                        listing.TotalOutgoings = null;
        //                    }
        //                }
        //                else
        //                {
        //                    listing.TotalOutgoings = null;
        //                }

        //                listing.LegalDescription = propertyListing.LegalDescription;
        //                listing.LegalLot = propertyListing.LegalLot;
        //                listing.LegalDP = propertyListing.LegalDP;
        //                listing.LegalVol = propertyListing.LegalVol;
        //                listing.LegalFolio = propertyListing.LegalFolio;
        //                listing.Zoning = propertyListing.Zoning;
        //                //    listing.ContactCompanyID = (String.IsNullOrEmpty(propertyListing.ContactCompanyID)) ? 0 : Convert.ToInt32(propertyListing.ContactCompanyID);
        //                listing.BrochureDescription = propertyListing.BroucherDescription;
        //                listing.BrochureMainHeadLine = propertyListing.BroucherMainHeading;
        //                listing.BrochureSummary = propertyListing.BroucherSummery;
        //                listing.SignBoardDescription = propertyListing.SignBoardDescription;
        //                listing.SignBoardInstallInstruction = propertyListing.InstallationInstructio;
        //                listing.SignBoardMainHeadLine = propertyListing.SignBoardMainHeading;
        //                listing.SignBoardSummary = propertyListing.SignBoardSummery;
        //                listing.AdvertsDescription = propertyListing.PressDescription;
        //                listing.AdvertsMainHeadLine = propertyListing.PressMainHeading;
        //                listing.AdvertsSummary = propertyListing.PressSummery;
        //                if (context.SaveChanges() > 0)
        //                {
        //                    updatedListing = listing.ListingID;

        //                }
        //            }
        //        }

        //        return updatedListing;
        //    }
        //    catch (Exception ex)
        //    {
        //        updatedListing = 0;
        //        //return updatedListing;
        //        throw ex;
        //    }
        //}


    }
}
