using MPC.Common;
using MPC.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using Newtonsoft.Json;
using MPC.Webstore.Common;
using MPC.Interfaces.Repository;
namespace MPC.Webstore.Areas.WebstoreApi.Controllers
{
    public class RealEstateSMController : ApiController
    {

        private readonly IListingService _listingService;
        private readonly  ICompanyService _mycompanyservice;
        private readonly IListingBulletPointsRepository _listingBulletPointRepository;
        public RealEstateSMController(IListingService _listingService, ICompanyService _mycompanyservice, IListingBulletPointsRepository _listingBulletPointRepository)
        {
            this._listingService = _listingService;
            this._mycompanyservice = _mycompanyservice;
            this._listingBulletPointRepository = _listingBulletPointRepository;
        }



        public void UpdateListingData(MPC.Models.DomainModels.Listing Listing, List<CompanyContact> AgentsList, List<ListingBulletPoint> BulletPoints, string hfAgentList, string hfBPointsList)
        {
            try
            {

                MPC.Models.DomainModels.Listing updateListing = new MPC.Models.DomainModels.Listing();
                updateListing.UnitNumber = Listing.UnitNumber;
                updateListing.StreetNumber = Listing.StreetNumber;
                updateListing.PropertyType = Listing.PropertyType;
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
                MPC.Models.DomainModels.Listing listing = _mycompanyservice.GetListingByListingID(Convert.ToInt32(Listing.ListingId));//Listing

                long UpdatedLisTingID = _mycompanyservice.UpdateListing(updateListing, listing);

                //List<tbl_ListingAgent> listingAgents = propertyManager.GetListingAgentsByListingID(propertyId); //Listing Agents

                // for agents
                List<CompanyContact> updateAgentList = new List<CompanyContact>();
                foreach (var Item in AgentsList)
                {
                    CompanyContact contact = new CompanyContact();
                    contact.FirstName = Item.FileName;
                    contact.Mobile = Item.Mobile;
                    contact.HomeTel1 = Item.HomeTel1;
                    contact.Email = Item.Email;
                    updateAgentList.Add(contact);
                }

                _mycompanyservice.UpdateAgent(updateAgentList);

                if (!string.IsNullOrEmpty(hfAgentList))
                {
                    ListAgentMode agentsList = JsonConvert.DeserializeObject<ListAgentMode>(hfAgentList);
                    if (agentsList != null)
                    {
                        _mycompanyservice.AddAgent(agentsList, UserCookieManager.WBStoreId);
                    }
                }

                // for bullet points

                List<ListingBulletPoint> updatePointList = new List<ListingBulletPoint>();
                foreach (var Item in BulletPoints)
                {
                        ListingBulletPoint oPoint = new ListingBulletPoint();
                        oPoint.BulletPoint = Item.BulletPoint;
                        oPoint.BulletPointId = Item.BulletPointId;
                        updatePointList.Add(oPoint);
                }

                _mycompanyservice.UpdateBulletPoints(updatePointList);

                if (!string.IsNullOrEmpty(hfBPointsList))
                {
                    ListPointsModel bulletList = JsonConvert.DeserializeObject<ListPointsModel>(hfBPointsList);
                    if (bulletList != null)
                    {
                   //     _mycompanyservice.AddBulletPoint(bulletList, UpdatedLisTingID);
                    }
                }
                //hfNewlyAddedBPoints.Value ="";
                //if (!string.IsNullOrEmpty(txtBulletPoint.Text))
                //{
                //    PropertyManager.AddBulletPoint(txtBulletPoint.Text, listing.ListingID);
                //}
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public void UpdateAgent(CompanyContact AgentContact)
        {
            _mycompanyservice.UpdateSignleAgent(AgentContact);
        }
        [HttpPost]
        public void AddSingleAjent(CompanyContact AgentContact)
        {
            _mycompanyservice.AddSingleAgent( AgentContact);
        
        }
        [HttpPost]
        public void DeleteListingImage(long ListingImageId,string ImageUrl)
        {
            _mycompanyservice.DeleteListingImage(ListingImageId);
        }
        [HttpPost]
        public void DeleteAjent(long ContactID)
        {
            _mycompanyservice.DeleteAjent(ContactID);
        }
        [HttpPost]
        public void AddSingleBulletPoint(ListingBulletPoint BulletPoint)
        {
            _mycompanyservice.AddSingleBulletPoint(BulletPoint);
        }
        [HttpPost]
        public void UpdateSingleBulletPoint(ListingBulletPoint BulletPoint)
        {
            _mycompanyservice.UpdateSingleBulletPoint(BulletPoint);
        }
        [HttpPost]
        public void DeleteBulletPoint(long BulletPointId, long ListingId)
        {
            _mycompanyservice.DeleteBulletPoint(BulletPointId, ListingId);
        }
        [HttpPost]

        public void DeleteListing(long ListingId)
        {
          bool result= _mycompanyservice.DeleteLisitngData(ListingId);
        }
        //private void ProcessVariablesAndGoToDesigner(int listingId, long itemId, long productCatId)
        //{
        //    try
        //    {
        //        List<MPC.Models.Common.TemplateVariable> lstPageControls = new List<MPC.Models.Common.TemplateVariable>();

        //        //foreach (Control control in pnlSmartForm.Controls)
        //        //{
        //        //    if (control.GetType() == typeof(TextBox))
        //        //    {
        //        //        TextBox currentTextBox = (TextBox)control;
        //        //        string controlClasses = currentTextBox.CssClass;

        //        //        if (!string.IsNullOrEmpty(controlClasses))
        //        //        {
        //        //            if (controlClasses.Contains(' '))
        //        //            {
        //        //                string[] txtClasses = controlClasses.Split(' ');

        //        //                foreach (string cssclass in txtClasses)
        //        //                {
        //        //                    if (cssclass.Contains("{{"))
        //        //                    {
        //        //                        TemplateVariable tempVar = new TemplateVariable(cssclass, currentTextBox.Text);
        //        //                        lstPageControls.Add(tempVar);
        //        //                    }
        //        //                }
        //        //            }
        //        //            else
        //        //            {
        //        //                if (controlClasses.Contains("{{"))
        //        //                {
        //        //                    TemplateVariable tempVar = new TemplateVariable(controlClasses, currentTextBox.Text);
        //        //                    lstPageControls.Add(tempVar);
        //        //                }
        //        //            }
        //        //        }
        //        //    }
        //        //}
        //        //fix multiple agent bug
        //        List<CompanyContact> updatedAgentList = _mycompanyservice.GetUsersByCompanyId(UserCookieManager.WBStoreId);
              
        //        if (updatedAgentList.Count > 5)
        //            updatedAgentList = updatedAgentList.Take(5).ToList();
        //        int count = 1;
        //        foreach (CompanyContact agent in updatedAgentList)
        //        {
        //            if (count != 1)
        //            {
        //                MPC.Models.Common.TemplateVariable tempVar = new MPC.Models.Common.TemplateVariable("{{Agent" + count + "Name}}", agent.FirstName); lstPageControls.Add(tempVar);
        //                MPC.Models.Common.TemplateVariable tempVar2 = new MPC.Models.Common.TemplateVariable("{{Agent" + count + "Phone1}}", agent.HomeTel1); lstPageControls.Add(tempVar2);
        //                MPC.Models.Common.TemplateVariable tempVar3 = new MPC.Models.Common.TemplateVariable("{{Agent" + count + "Mobile}}", agent.Mobile); lstPageControls.Add(tempVar3);
        //                MPC.Models.Common.TemplateVariable tempVar4 = new MPC.Models.Common.TemplateVariable("{{Agent" + count + "Email}}", agent.Email); lstPageControls.Add(tempVar4);

        //            }
        //            else
        //            {
        //                MPC.Models.Common.TemplateVariable tempVar = new MPC.Models.Common.TemplateVariable("{{AgentName}}", agent.FirstName);
        //                lstPageControls.Add(tempVar);
        //                MPC.Models.Common.TemplateVariable tempVar2 = new MPC.Models.Common.TemplateVariable("{{AgentPhone1}}", agent.HomeTel1);
        //                lstPageControls.Add(tempVar2);
        //                MPC.Models.Common.TemplateVariable tempVar3 = new MPC.Models.Common.TemplateVariable("{{AgentMobile}}", agent.Mobile);
        //                lstPageControls.Add(tempVar3);
        //                MPC.Models.Common.TemplateVariable tempVar4 = new MPC.Models.Common.TemplateVariable("{{AgentEmail}}", agent.Email);
        //                lstPageControls.Add(tempVar4);
        //            }
        //            count++;

        //        }
        //        // get bullet points 
                
                
        //        List<ListingBulletPoint> bulletList = _mycompanyservice.GetAllListingBulletPoints(listingId);
        //        if (bulletList != null && bulletList.Count > 6)
        //            bulletList = bulletList.Take(6).ToList();
        //        count = 1;
        //        foreach (ListingBulletPoint points in bulletList)
        //        {
        //            if (count != 1)
        //            {
        //                MPC.Models.Common.TemplateVariable tempVar = new MPC.Models.Common.TemplateVariable("{{BulletPoint" + count + "}}", points.BulletPoint);
        //                lstPageControls.Add(tempVar);

        //            }
        //            else
        //            {
        //                MPC.Models.Common.TemplateVariable tempVar = new MPC.Models.Common.TemplateVariable("{{BulletPoint1}}", points.BulletPoint);
        //                lstPageControls.Add(tempVar);

        //            }
        //            count++;

        //        }
        //        //get images 
        //        Dictionary<string, string> lstImagesAndUrls = new Dictionary<string, string>();
               
        //        List <ListingImage> listingImages = _mycompanyservice.GetAllListingImages(listingId);
        //        if (listingImages != null && listingImages.Count > 20)
        //            listingImages = listingImages.Take(20).ToList();
        //        count = 1;
        //        foreach (var item in listingImages)
        //        {
        //            string imageControlName = "{{ListingImage" + count + "}}";
        //            MPC.Models.Common.TemplateVariable imgTempVar = new MPC.Models.Common.TemplateVariable(imageControlName, item.ImageURL);
        //            lstPageControls.Add(imgTempVar);
        //            count += 1;
        //        }
               
             

        //        lstPageControls = oManger.GetAllVariablesUsedInTemplate(lstPageControls, itemId, Convert.ToInt32(SessionParameters.ContactID.ToString()), propertyId);
        //        GotoDesigner(itemId, productCatId, lstPageControls, propertyId);
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

     

    }
    
}
