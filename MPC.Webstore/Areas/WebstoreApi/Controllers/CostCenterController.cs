using MPC.Interfaces.Common;
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Runtime.Caching;
using System.Web;
using System.Web.Http;

namespace MPC.Webstore.Areas.WebstoreApi.Controllers
{
    public class CostCenterController : ApiController
    {
        #region Private

        private readonly ICostCentreService _CostCentreService;

        private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;
        private readonly IItemService _ItemService;
        private readonly ICompanyService _companyService;
        private readonly ICampaignService _campaignService;
        private readonly IUserManagerService _usermanagerService;
        private readonly ICompanyContactRepository _companyContact;
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        private readonly IOrderService _orderService;
        public CostCenterController(ICostCentreService CostCentreService, IItemService ItemService, IOrderService _orderService, ICompanyService companyService, IWebstoreClaimsHelperService _webstoreAuthorizationChecker, ICampaignService _campaignService, IUserManagerService _usermanagerService, ICompanyContactRepository _companyContact)
        {
            this._CostCentreService = CostCentreService;
            this._ItemService = ItemService;
            this._orderService = _orderService;
            this._companyService = companyService;
            this._webstoreAuthorizationChecker = _webstoreAuthorizationChecker;
            this._campaignService = _campaignService;
            this._usermanagerService = _usermanagerService;
            this._companyContact = _companyContact;
        }

        #endregion
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage ExecuteCostCentre(string CostCentreId, string ClonedItemId, string OrderedQuantity, string CallMode, QuestionAndInputQueues Queues)
        {
            if ((CallMode == "UpdateAllCostCentreOnQuantityChange" && Queues != null) || CallMode != "UpdateAllCostCentreOnQuantityChange")
            {
                AppDomain _AppDomain = null;

                try
                {
                    string CacheKeyName = "CompanyBaseResponse";
                    ObjectCache cache = MemoryCache.Default;

// ReSharper disable SuggestUseVarKeywordEvident
                    Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse> companyBaseResponse =
// ReSharper restore SuggestUseVarKeywordEvident
                        (cache.Get(CacheKeyName) as
                            Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>);
                    string orgName = string.Empty;
                    if (companyBaseResponse != null)
                    {
                        MPC.Models.ResponseModels.MyCompanyDomainBaseReponse myCompanyBaseResponseFromCache =
                        companyBaseResponse[UserCookieManager.WBStoreId];    
                        if (myCompanyBaseResponseFromCache != null && myCompanyBaseResponseFromCache.Organisation != null)
                        {
                            orgName = myCompanyBaseResponseFromCache.Organisation.OrganisationName;
                        }
                    }
                    else
                    {
                        Organisation organisation = _CostCentreService.GetOrganisation(Convert.ToInt64(CostCentreId));
                        if (organisation != null)
                        {
                            orgName = organisation.OrganisationName;
                        }
                    }
                    string OrganizationName = orgName;
                    OrganizationName = Utils.specialCharactersEncoderCostCentre(OrganizationName);

                    AppDomainSetup _AppDomainSetup = new AppDomainSetup();


                    object _oLocalObject;
                    ICostCentreLoader _oRemoteObject;

                    object[] _CostCentreParamsArray = new object[12];

                    _AppDomainSetup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
                    _AppDomainSetup.PrivateBinPath = Path.GetDirectoryName((new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath);


                    _AppDomain = AppDomain.CreateDomain("CostCentresDomain", null, _AppDomainSetup);
                    //Me._AppDomain.InitializeLifetimeService()

                    List<CostCentreQueueItem> CostCentreQueue = new List<CostCentreQueueItem>();




                    //Me._CostCentreLaoderFactory = CType(Me._AppDomain.CreateInstance(Common.g_GlobalData.AppSettings.ApplicationStartupPath + "\Infinity.Model.dll", "Infinity.Model.CostCentres.CostCentreLoaderFactory").Unwrap(), Model.CostCentres.CostCentreLoaderFactory)
                    CostCentreLoaderFactory _CostCentreLaoderFactory = (CostCentreLoaderFactory)_AppDomain.CreateInstance("MPC.Interfaces", "MPC.Interfaces.WebStoreServices.CostCentreLoaderFactory").Unwrap();
                    _CostCentreLaoderFactory.InitializeLifetimeService();
                  
                    CostCentre  oCostCentre = _CostCentreService.GetCostCentreByID(Convert.ToInt64(CostCentreId));

                    List<CostcentreInstruction> oInstList = new List<CostcentreInstruction>();
                    List<CostcentreWorkInstructionsChoice> oInsChoicesList = null;
                    foreach (CostcentreInstruction obj in oCostCentre.CostcentreInstructions)
                    {
                        CostcentreInstruction oObject = new CostcentreInstruction();
                        oObject.CostCentreId = obj.CostCentreId;
                        oObject.Instruction =obj.Instruction;
                        oObject.InstructionId = obj.InstructionId;
                        oInsChoicesList = new List<CostcentreWorkInstructionsChoice>();
                        foreach (CostcentreWorkInstructionsChoice wI in obj.CostcentreWorkInstructionsChoices)
                        {
                            CostcentreWorkInstructionsChoice oChoicObject = new CostcentreWorkInstructionsChoice();
                            oChoicObject.Choice = wI.Choice;
                            oChoicObject.Id = wI.Id;
                            oChoicObject.InstructionId = wI.InstructionId;
                            oInsChoicesList.Add(oChoicObject);
                        }
                        oObject.CostcentreWorkInstructionsChoices = oInsChoicesList;
                        oInstList.Add(oObject);// = oCostCentre.CostcentreInstructions.ToArray();
                    }
                    CostCentreQueue.Add(new CostCentreQueueItem(oCostCentre.CostCentreId, oCostCentre.Name, 1, oCostCentre.CodeFileName, oInstList.ToArray(), oCostCentre.SetupSpoilage, oCostCentre.RunningSpoilage));

                    

                    if (CallMode == "New")
                    {
                        if (Queues != null)
                        {
                            _CostCentreParamsArray[1] = CostCentreExecutionMode.ExecuteMode;
                            // if queue contains item of other cost centre then this condition will filter the items of current cost centre

                            if (Queues.QuestionQueues != null)
                            {
                                _CostCentreParamsArray[2] = Queues.QuestionQueues.Where(c => c.CostCentreID == oCostCentre.CostCentreId).ToList(); ;
                            }
                            else 
                            {
                                _CostCentreParamsArray[2] = Queues.QuestionQueues;
                            }
                            
                         
                            if (Queues.InputQueues != null) 
                            {
                                _CostCentreParamsArray[7] = Queues.InputQueues.Where(c => c.CostCentreID == oCostCentre.CostCentreId).ToList();
                            }
                            else // else assign null
                            {
                                _CostCentreParamsArray[7] = Queues.InputQueues;
                            }
                            
                        }
                        else
                        {
                            _CostCentreParamsArray[1] = CostCentreExecutionMode.PromptMode;
                            _CostCentreParamsArray[2] = new List<QuestionQueueItem>();
                            _CostCentreParamsArray[7] = new InputQueue();
                        }
                    }

                    if (CallMode == "addAnother")
                    {

                        _CostCentreParamsArray[1] = CostCentreExecutionMode.PromptMode;
                        _CostCentreParamsArray[2] = Queues.QuestionQueues;
                        if (Queues.InputQueues != null)
                        {
                            InputQueue inputQueueObj = new InputQueue();
                            List<InputQueueItem> Items = Queues.InputQueues.Where(c => c.CostCentreID == oCostCentre.CostCentreId).ToList();
                            foreach (InputQueueItem obj in Items)
                            {
                                inputQueueObj.addItem(obj.ID, obj.VisualQuestion, obj.CostCentreID, obj.ItemType, obj.ItemInputType, obj.VisualQuestion, obj.Value, obj.Qty1Answer);
                            }
                            _CostCentreParamsArray[7] = inputQueueObj;
                        }
                        else
                        {
                            _CostCentreParamsArray[7] = new InputQueue();
                        }
                    }


                    //_CostCentreParamsArray(0) = Common.g_GlobalData;
                    //GlobalData

                    //this mode will load the questionqueue

                    //QuestionQueue / Execution Queue
                    _CostCentreParamsArray[3] = CostCentreQueue;
                    // check if cc has wk ins


                    //CostCentreQueue
                    _CostCentreParamsArray[4] = 1;

                    _CostCentreParamsArray[5] = 1;
                    //MultipleQuantities

                    //CurrentQuantity
                    _CostCentreParamsArray[6] = new List<StockQueueItem>();
                    //StockQueue
                 
                    //InputQueue

                    if (!string.IsNullOrEmpty(ClonedItemId))
                    {
                        if (OrderedQuantity == "null" || OrderedQuantity == null)
                        {
                            // get first item section
                            _CostCentreParamsArray[8] = _ItemService.GetItemFirstSectionByItemId(Convert.ToInt64(ClonedItemId));
                        }
                        else
                        {
                            // update quantity in item section and return
                            _CostCentreParamsArray[8] = _ItemService.UpdateItemFirstSectionByItemId(Convert.ToInt64(ClonedItemId), Convert.ToInt32(OrderedQuantity));

                        }    
                    }

                    _CostCentreParamsArray[9] = 1;

                    // connection string
                    _CostCentreParamsArray[10] =  "Persist Security Info=False;Integrated Security=false;Initial Catalog=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringDBName"] + ";server=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringServerName"] + "; user id=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringUserName"] + "; password=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringPasswordName"] + ";";
                  


                   


                    _oLocalObject = _CostCentreLaoderFactory.Create(HttpContext.Current.Server.MapPath("/") + "\\ccAssembly\\" + OrganizationName + "UserCostCentres.dll", "UserCostCentres." + oCostCentre.CodeFileName, null);
                    _oRemoteObject = (ICostCentreLoader)_oLocalObject;

                    CostCentrePriceResult oResult = null;

                    if (CallMode == "Modify")
                    {
                        _CostCentreParamsArray[1] = CostCentreExecutionMode.PromptMode;
                        if (Queues.QuestionQueues != null)
                        {
                            _CostCentreParamsArray[2] = Queues.QuestionQueues.Where(c => c.CostCentreID == oCostCentre.CostCentreId).ToList();
                        }
                        else 
                        {
                            _CostCentreParamsArray[2] = new List<QuestionQueueItem>();
                        }

                        if (Queues.InputQueues != null)
                        {
                            InputQueue inputQueueObj = new InputQueue();
                            List<InputQueueItem> Items = Queues.InputQueues.Where(c => c.CostCentreID == oCostCentre.CostCentreId).ToList();
                            foreach (InputQueueItem obj in Items)
                            {
                                inputQueueObj.addItem(obj.ID, obj.VisualQuestion, obj.CostCentreID, obj.ItemType, obj.ItemInputType, obj.VisualQuestion, obj.Value, obj.Qty1Answer);
                            }
                            _CostCentreParamsArray[7] = inputQueueObj;
                        }
                        else
                        {
                            _CostCentreParamsArray[7] = new InputQueue();
                        }
                        
                        
                    }
                    else
                    {
                        if (CallMode == "UpdateAllCostCentreOnQuantityChange") 
                        { 
                            _CostCentreParamsArray[1] = CostCentreExecutionMode.ExecuteMode;
                            _CostCentreParamsArray[2] = Queues.QuestionQueues.ToList();
                            if (Queues.InputQueues != null)
                            {
                                InputQueue inputQueueObj = new InputQueue();
                                List<InputQueueItem> Items = Queues.InputQueues.ToList();
                                foreach (InputQueueItem obj in Items)
                                {
                                    inputQueueObj.addItem(obj.ID, obj.VisualQuestion, obj.CostCentreID, obj.ItemType, obj.ItemInputType, obj.VisualQuestion, obj.Value, obj.Qty1Answer);
                                }
                                _CostCentreParamsArray[7] = inputQueueObj.Items;
                            }
                            else
                            {
                                _CostCentreParamsArray[7] = new InputQueue();
                            }
                        }
                        oResult = _oRemoteObject.returnPrice(ref _CostCentreParamsArray);

                    }

                    if ((Queues != null && CallMode != "Modify" && CallMode != "addAnother" && oResult != null) || (Queues != null && CallMode == "Update"))
                    {


                        JsonSerializerSettings jSettings = new Newtonsoft.Json.JsonSerializerSettings();
                        GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = jSettings;

                        double actualPrice = oResult.TotalPrice;

                        if (actualPrice < oCostCentre.MinimumCost && oCostCentre.MinimumCost != 0)
                        {
                            actualPrice = oCostCentre.MinimumCost ?? 0;
                        }

                        return Request.CreateResponse(HttpStatusCode.OK, actualPrice);
                    }
                    else
                    {
                        JsonSerializerSettings jSettings = new Newtonsoft.Json.JsonSerializerSettings();
                        GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = jSettings;

                        return Request.CreateResponse(HttpStatusCode.OK, _CostCentreParamsArray);
                    }



                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    AppDomain.Unload(_AppDomain);
                }

            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "");
            }

        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetData(long orderID)
        {
            double GrandTotal = 0;
            double Subtotal = 0;
            double vat = 0;
            Order order = _orderService.GetOrderAndDetails(orderID);
            Address BillingAddress = _orderService.GetBillingAddress(order.BillingAddressID);
            Address ShippingAddress = _orderService.GetdeliveryAddress(order.DeliveryAddressID);
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;
            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
            CalculateProductDescription(order, out GrandTotal, out Subtotal, out vat);
            JasonResponseObject obj = new JasonResponseObject();
            obj.order = order;
            obj.SubTotal = @Utils.FormatDecimalValueToTwoDecimal(Subtotal.ToString(), StoreBaseResopnse.Currency);
            obj.GrossTotal = @Utils.FormatDecimalValueToTwoDecimal(GrandTotal.ToString(), StoreBaseResopnse.Currency);
            obj.VAT = @Utils.FormatDecimalValueToTwoDecimal(vat.ToString(), StoreBaseResopnse.Currency);
            obj.DeliveryCostCharges = @Utils.FormatDecimalValueToTwoDecimal(order.DeliveryCost.ToString(), StoreBaseResopnse.Currency);
            obj.billingAddress = BillingAddress;
            obj.shippingAddress = ShippingAddress;
            if (BillingAddress.CountryId != null && BillingAddress.CountryId > 0)
            {
                obj.BillingCountry = _companyService.GetCountryNameById(BillingAddress.CountryId ?? 0);
            }
            else
            {
                obj.BillingCountry = string.Empty;
            }
            if (BillingAddress.StateId != null && BillingAddress.StateId > 0)
            {
                obj.BillingState = _companyService.GetStateNameById(BillingAddress.StateId ?? 0);
            }
            else
            {
                obj.BillingState = string.Empty;
            }

            if (ShippingAddress.CountryId != null && ShippingAddress.CountryId > 0)
            {
                obj.ShippingCountry = _companyService.GetCountryNameById(ShippingAddress.CountryId ?? 0);
            }
            else
            {
                obj.ShippingCountry = string.Empty;
            }

            if (ShippingAddress.StateId != null && ShippingAddress.StateId > 0)
            {
                obj.ShippingState = _companyService.GetStateNameById(ShippingAddress.StateId ?? 0);
            }
            else
            {
                obj.ShippingState = string.Empty;
            }
            obj.CurrencySymbol = StoreBaseResopnse.Currency;
            obj.OrderDateValue = FormatDateValue(order.OrderDate);
            obj.DeliveryDateValue = FormatDateValue(order.DeliveryDate);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, obj, formatter);
        }


        private void CalculateProductDescription(Order order, out double GrandTotal, out double Subtotal, out double vat)
        {

            double Delevery = 0;
            double DeliveryTaxValue = 0;
            double TotalVat = 0;
            double calculate = 0;
            Subtotal = 0;
            vat = 0;
            GrandTotal = 0;

            {
                //List<tbl_items> items = context.tbl_items.Where(i => i.EstimateID == OrderID).ToList();

                foreach (var item in order.OrderDetails.CartItemsList)
                {

                    if (item.ItemType == (int)ItemTypes.Delivery)
                    {
                        Delevery = Convert.ToDouble(item.Qty1NetTotal);
                        DeliveryTaxValue = Convert.ToDouble(item.Qty1GrossTotal - item.Qty1NetTotal);
                    }
                    else
                    {

                        Subtotal = Subtotal + Convert.ToDouble(item.Qty1NetTotal);
                        TotalVat = Convert.ToDouble(item.Qty1GrossTotal) - Convert.ToDouble(item.Qty1NetTotal);
                        calculate = calculate + TotalVat;
                    }

                }

                GrandTotal = Subtotal + calculate + DeliveryTaxValue + Delevery;
                vat = calculate;

                // ViewBag.GrandTotal = GrandTotal;
                // ViewBag.SubTotal = Subtotal;
                // ViewBag.Vat = calculate;
            }

        }
        public string FormatDateValue(DateTime? dateTimeValue, string formatString = null)
        {
            const string defaultFormat = "MMMM d, yyyy";

            if (dateTimeValue.HasValue)
                return dateTimeValue.Value.ToString(string.IsNullOrWhiteSpace(formatString) ? defaultFormat : formatString);
            else
                return string.Empty;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage WidgetJson(string StoreId)
        {
            List<CmsSkinPageWidget> oStoreWidgets = _companyService.GetStoreWidgets(Convert.ToInt64(StoreId));
            var objSer = JsonConvert.SerializeObject(oStoreWidgets, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return Request.CreateResponse(HttpStatusCode.OK, objSer);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage FillAddresses(long AddressId)
        {
            JsonAddressClass obj = new JsonAddressClass();
            Address Address = _companyService.GetAddressByID(AddressId);
            obj.Address = Address;
            obj.StateId = Address.StateId ?? 0;
            obj.CountryId = Address.CountryId ?? 0;
            obj.CompanyID = Address.CompanyId ?? 0;
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, obj, formatter);
        }

        [HttpPost]
        public void UploadImage(string FirstName, string LastName, string Email, string JobTitle, string HomeTel1, string Mobile, string FAX, string CompanyName, string quickWebsite, string POBoxAddress, string CorporateUnit, string OfficeTradingName, string ContractorName, string BPayCRN, string ABN, string ACN, string AdditionalField1, string AdditionalField2, string AdditionalField3, string AdditionalField4, string AdditionalField5, bool IsEmailSubscription, bool IsNewsLetterSubscription)
        {
            // if (HttpContext.Current.Request.Files.AllKeys.Any())
            // {
            //Get the uploaded image from the Files collection
            try
            {
                var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                //}

                bool result = false;
                CompanyContact UpdateContact = new CompanyContact();
                UpdateContact.FirstName = FirstName;
                UpdateContact.LastName = LastName;
                UpdateContact.Email = Email;
                UpdateContact.JobTitle = JobTitle;
                UpdateContact.HomeTel1 = HomeTel1;
                UpdateContact.Mobile = Mobile;
                UpdateContact.FAX = FAX;
                UpdateContact.quickWebsite = quickWebsite;
                UpdateContact.image = UpdateImage(httpPostedFile);
                UpdateContact.ContactId = _webstoreAuthorizationChecker.loginContactID();
                UpdateContact.IsEmailSubscription = IsEmailSubscription;
                UpdateContact.IsNewsLetterSubscription = IsNewsLetterSubscription;
                if (UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)
                {
                    Company Company = _companyService.GetCompanyByCompanyID(_webstoreAuthorizationChecker.loginContactCompanyID());
                    if (Company != null)
                    {
                        Company.Name = CompanyName;
                        Company.CompanyId = _webstoreAuthorizationChecker.loginContactCompanyID();
                        result = _companyService.UpdateCompanyName(Company);
                    }
                    result = _companyService.UpdateCompanyContactForRetail(UpdateContact);
                }
                else
                {
                    UpdateContact.POBoxAddress = POBoxAddress;
                    UpdateContact.CorporateUnit = CorporateUnit;
                    UpdateContact.OfficeTradingName = OfficeTradingName;
                    UpdateContact.ContractorName = ContractorName;
                    UpdateContact.BPayCRN = BPayCRN;
                    UpdateContact.ABN = ABN;
                    UpdateContact.ACN = ACN;
                    UpdateContact.AdditionalField1 = AdditionalField1;
                    UpdateContact.AdditionalField2 = AdditionalField2;
                    UpdateContact.AdditionalField3 = AdditionalField3;
                    UpdateContact.AdditionalField4 = AdditionalField4;
                    UpdateContact.AdditionalField5 = AdditionalField5;
                    UpdateContact.ContactId = _webstoreAuthorizationChecker.loginContactID();
                    result = _companyService.UpdateCompanyContactForCorporate(UpdateContact);
                }
                if (result)
                {

                }
                else
                {

                }
                UserCookieManager.WEBContactFirstName = FirstName;

                UserCookieManager.WEBContactLastName = LastName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string UpdateImage(HttpPostedFile Request)
        {
            string ImagePath = string.Empty;
            CompanyContact contact = _companyService.GetContactByID(_webstoreAuthorizationChecker.loginContactID());
            if (Request != null)
            {
                string folderPath = "/mpc_content/Assets" + "/" + UserCookieManager.WEBOrganisationID + "/" + UserCookieManager.WBStoreId + "/Contacts/" + contact.ContactId + "";
                string virtualFolderPth = string.Empty;

                // virtualFolderPth = @Server.MapPath(folderPath);
                //  virtualFolderPth = Request.MapPath(folderPath);
                virtualFolderPth = HttpContext.Current.Server.MapPath(folderPath);
                /// virtualFolderPth = System.Web.Http.HttpServer.
                if (!System.IO.Directory.Exists(virtualFolderPth))
                {
                    System.IO.Directory.CreateDirectory(virtualFolderPth);
                }
                if (contact.image != null || contact.image != "")
                {
                    RemovePreviousFile(contact.image);
                }
                var fileName = Path.GetFileName(Request.FileName);
                Request.SaveAs(virtualFolderPth + "/" + fileName);
                ImagePath = folderPath + "/" + fileName;
            }
            else
            {
                ImagePath = contact.image;
            }

            return ImagePath;
        }

        private void RemovePreviousFile(string previousFileToremove)
        {
            if (!string.IsNullOrEmpty(previousFileToremove))
            {
                string ServerPath = HttpContext.Current.Server.MapPath(previousFileToremove);
                if (System.IO.File.Exists(ServerPath))
                {
                    DeleteFile(ServerPath);
                }
            }
        }

        public void DeleteFile(string completePath)
        {
            try
            {
                if (System.IO.File.Exists(completePath))
                {
                    System.IO.File.Delete(completePath);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        public HttpPostedFile ImageFile()
        {
            HttpPostedFile file = null;
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection
                var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                if (httpPostedFile != null)
                {
                    file = httpPostedFile;
                }

            }
            return file;
        }
        [HttpPost]
        public void UpdateDataRequestQuote(string FirstName, string LastName, string Email, string Mobile, string Title, string InquiryItemTitle1, string InquiryItemNotes1, string InquiryItemDeliveryDate1, string InquiryItemTitle2, string InquiryItemNotes2, string InquiryItemDeliveryDate2, string InquiryItemTitle3, string InquiryItemNotes3, string InquiryItemDeliveryDate3, string hfNoOfRec)
        {
            int count = HttpContext.Current.Request.Files.Count;
            int Contentlength = HttpContext.Current.Request.ContentLength;
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;

            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
            Inquiry NewInqury = new Inquiry();

            NewInqury.Title = Title;

            if (_webstoreAuthorizationChecker.loginContactID() > 0)
            {
                NewInqury.ContactId = _webstoreAuthorizationChecker.loginContactID();
                NewInqury.CompanyId = (int)_webstoreAuthorizationChecker.loginContactCompanyID();

            }
            else
            {
                if (_companyContact.GetContactByEmailID(Email) != null)
                {
                    return;
                }
                CompanyContact Contact = new CompanyContact();
                Contact.FirstName = FirstName;
                Contact.LastName = LastName;
                Contact.Email = Email;
                Contact.Mobile = Mobile;
                Contact.Password = "password";
                Campaign RegistrationCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.Registration, UserCookieManager.WEBOrganisationID, UserCookieManager.WBStoreId);


                long Customer = _companyService.CreateCustomer(FirstName, false, false, CompanyTypes.SalesCustomer, string.Empty, 0, StoreBaseResopnse.Company.CompanyId, Contact);

                if (Customer > 0)
                {

                    MPC.Models.DomainModels.Company loginUserCompany = _companyService.GetCompanyByCompanyID(UserCookieManager.WEBOrganisationID);
                    CompanyContact UserContact = _companyService.GetContactByID(_webstoreAuthorizationChecker.loginContactID());
                    CampaignEmailParams cep = new CampaignEmailParams();

                    Campaign RegistrationCampaignn = _campaignService.GetCampaignRecordByEmailEvent((int)Events.RequestAQuote, UserCookieManager.WEBOrganisationID, UserCookieManager.WBStoreId);
                    cep.ContactId = NewInqury.ContactId;

                    cep.OrganisationId = 1;
                    cep.AddressId = (int)NewInqury.CompanyId;
                    cep.SalesManagerContactID = _webstoreAuthorizationChecker.loginContactID();
                    cep.StoreId = UserCookieManager.WBStoreId;

                    SystemUser EmailOFSM = _usermanagerService.GetSalesManagerDataByID(loginUserCompany.SalesAndOrderManagerId1.Value);

                    if (UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)
                    {
                        _campaignService.SendEmailToSalesManager((int)Events.NewQuoteToSalesManager, (int)NewInqury.ContactId, (int)NewInqury.CompanyId, 0, UserCookieManager.WEBOrganisationID, 0, StoreMode.Retail, UserCookieManager.WBStoreId, EmailOFSM);

                    }
                    else
                    {
                        _campaignService.SendEmailToSalesManager((int)Events.NewQuoteToSalesManager, (int)NewInqury.ContactId, (int)NewInqury.CompanyId, 0, UserCookieManager.WEBOrganisationID, 0, StoreMode.Corp, UserCookieManager.WBStoreId, EmailOFSM);

                    }

                    _campaignService.emailBodyGenerator(RegistrationCampaignn, cep, UserContact, StoreMode.Retail, (int)loginUserCompany.OrganisationId, "", "", "", EmailOFSM.Email, "", "", null, "");

                }
            }
            NewInqury.CreatedDate = DateTime.Now;
            NewInqury.IsDirectInquiry = false;
            NewInqury.FlagId = null;
            NewInqury.SourceId = 30;
            int iMaxFileSize = 2097152;
            long result = _ItemService.AddInquiryAndItems(NewInqury, FillItems(InquiryItemDeliveryDate1, InquiryItemDeliveryDate2, InquiryItemDeliveryDate3, InquiryItemTitle1, InquiryItemNotes1, InquiryItemTitle2, InquiryItemNotes2, InquiryItemTitle3, InquiryItemNotes3, Convert.ToInt32(hfNoOfRec)));
            long InquiryId = result;

            if (Request != null)
            {
                if (HttpContext.Current.Request.ContentLength < iMaxFileSize)
                {
                    FillAttachments(result);
                }
            }
            if (result > 0)
            {

                MPC.Models.DomainModels.Company loginUserCompany = _companyService.GetCompanyByCompanyID(_webstoreAuthorizationChecker.loginContactCompanyID());
                CompanyContact UserContact = _companyService.GetContactByID(_webstoreAuthorizationChecker.loginContactID());
                CampaignEmailParams cep = new CampaignEmailParams();

                Campaign RegistrationCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.RequestAQuote, UserCookieManager.WEBOrganisationID, UserCookieManager.WBStoreId);
                cep.ContactId = NewInqury.ContactId;

                cep.OrganisationId = 1;
                cep.AddressId = (int)NewInqury.CompanyId;
                cep.SalesManagerContactID = _webstoreAuthorizationChecker.loginContactID();
                cep.StoreId = UserCookieManager.WBStoreId;
                cep.CompanyId = UserCookieManager.WBStoreId;
                Company GetCompany = _companyService.GetCompanyByCompanyID(UserCookieManager.WBStoreId);
              
                SystemUser EmailOFSM = _usermanagerService.GetSalesManagerDataByID(StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value);

                if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                {

                    long MID = _companyContact.GetContactIdByRole(_webstoreAuthorizationChecker.loginContactCompanyID(), (int)Roles.Manager);
                    cep.CorporateManagerID = MID;
                    int ManagerID = (int)MID;

                    _campaignService.SendEmailToSalesManager((int)Events.NewQuoteToSalesManager, (int)NewInqury.ContactId, (int)NewInqury.CompanyId, 0, UserCookieManager.WEBOrganisationID, ManagerID, StoreMode.Corp, UserCookieManager.WBStoreId, EmailOFSM);
                }
                else
                {

                    _campaignService.SendEmailToSalesManager((int)Events.NewQuoteToSalesManager, (int)NewInqury.ContactId, (int)NewInqury.CompanyId, 0, UserCookieManager.WEBOrganisationID, 0, StoreMode.Retail, UserCookieManager.WBStoreId, EmailOFSM);

                }

                _campaignService.emailBodyGenerator(RegistrationCampaign, cep, UserContact, StoreMode.Retail, (int)loginUserCompany.OrganisationId, "", "", "", EmailOFSM.Email, "", "", null, "");
            }


        }

        private void FillAttachments(long inquiryID)
        {
            if (HttpContext.Current.Request != null)
            {
                List<InquiryAttachment> listOfAttachment = new List<InquiryAttachment>();
                string folderPath = "/mpc_content/Attachments/" + "/" + UserCookieManager.WEBOrganisationID + "/" + UserCookieManager.WBStoreId + "/" + inquiryID + "";
                string virtualFolderPth = string.Empty;

                virtualFolderPth = HttpContext.Current.Server.MapPath(folderPath);
                if (!System.IO.Directory.Exists(virtualFolderPth))
                    System.IO.Directory.CreateDirectory(virtualFolderPth);

                for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                {
                    //HttpPostedFile postedFile = HttpContext.Current.Request.Files[i];
                    HttpPostedFile postedFile = HttpContext.Current.Request.Files["UploadedFile" + i];

                    string fileName = string.Format("{0}{1}", Guid.NewGuid().ToString(), Path.GetFileName(postedFile.FileName));

                    InquiryAttachment inquiryAttachment = new InquiryAttachment();
                    inquiryAttachment.OrignalFileName = Path.GetFileName(postedFile.FileName);
                    inquiryAttachment.Extension = Path.GetExtension(postedFile.FileName);
                    inquiryAttachment.AttachmentPath = "/" + folderPath + fileName;
                    inquiryAttachment.InquiryId = Convert.ToInt32(inquiryID);
                    listOfAttachment.Add(inquiryAttachment);
                    //Request.SaveAs(virtualFolderPth + fileName);
                    // HttpContext.Current.Request.SaveAs(virtualFolderPth + fileName);
                    string filevirtualpath = virtualFolderPth + "/" + fileName;
                    postedFile.SaveAs(virtualFolderPth + "/" + fileName);
                }
                _ItemService.AddInquiryAttachments(listOfAttachment);
            }

        }
        private Inquiry AddInquiry(Prefix prefix)
        {
            // Get order prefix and update the order next number
            //  tbl_prefixes prefix = PrefixManager.GetDefaultPrefix(context);
            Inquiry inquiry = new Inquiry();

            inquiry.InquiryCode = prefix.EnquiryPrefix + "-001-" + prefix.EnquiryNext.ToString();
            prefix.EnquiryNext = prefix.EnquiryNext + 1;

            return inquiry;
        }
        private List<InquiryItem> FillItems(string InquiryItemDeliveryDate1, string InquiryItemDeliveryDate2, string InquiryItemDeliveryDate3, string InquiryItemTitle1, string InquiryItemNotes1, string InquiryItemTitle2, string InquiryItemNotes2, string InquiryItemTitle3, string InquiryItemNotes3, int hfNoOfRec)
        {
            List<InquiryItem> listOfInquiries = new List<InquiryItem>();
            DateTime requideddate = DateTime.Now;
            if (hfNoOfRec > 0)
            {
                int numOfrec = Convert.ToInt32(hfNoOfRec);
                if (numOfrec > 3)
                {
                    numOfrec = 3;
                }
                if (numOfrec == 1)
                {
                    InquiryItem item1 = new InquiryItem();

                    item1.Title = InquiryItemTitle1;
                    item1.Notes = InquiryItemNotes1;

                    item1.DeliveryDate = Convert.ToDateTime(InquiryItemDeliveryDate1, CultureInfo.InvariantCulture);

                    requideddate = item1.DeliveryDate;

                    listOfInquiries.Add(item1);
                }

                if (numOfrec == 2)
                {
                    InquiryItem item1 = new InquiryItem();

                    item1.Title = InquiryItemTitle1;
                    item1.Notes = InquiryItemNotes1;

                    item1.DeliveryDate = Convert.ToDateTime(InquiryItemDeliveryDate1, CultureInfo.InvariantCulture);
                    listOfInquiries.Add(item1);

                    InquiryItem item2 = new InquiryItem();

                    item2.Title = InquiryItemTitle2;
                    item2.Notes = InquiryItemNotes2;
                    item2.DeliveryDate = Convert.ToDateTime(InquiryItemDeliveryDate2, CultureInfo.InvariantCulture);

                    if (requideddate > item2.DeliveryDate)
                        requideddate = item2.DeliveryDate;

                    listOfInquiries.Add(item2);
                }

                if (numOfrec == 3)
                {
                    InquiryItem item1 = new InquiryItem();

                    item1.Title = InquiryItemTitle1;
                    item1.Notes = InquiryItemNotes1;

                    item1.DeliveryDate = Convert.ToDateTime(InquiryItemDeliveryDate1, CultureInfo.InvariantCulture);
                    listOfInquiries.Add(item1);

                    InquiryItem item2 = new InquiryItem();

                    item2.Title = InquiryItemTitle2;
                    item2.Notes = InquiryItemNotes2;
                    item2.DeliveryDate = Convert.ToDateTime(InquiryItemDeliveryDate2, CultureInfo.InvariantCulture);

                    listOfInquiries.Add(item2);


                    InquiryItem item3 = new InquiryItem();

                    item3.Title = InquiryItemTitle3;
                    item3.Notes = InquiryItemNotes3;
                    item3.DeliveryDate = Convert.ToDateTime(InquiryItemDeliveryDate3, CultureInfo.InvariantCulture);

                    if (requideddate > item3.DeliveryDate)
                        requideddate = item3.DeliveryDate;

                    listOfInquiries.Add(item3);
                }

            }
            return listOfInquiries;
        }

        //public ActionResult Index(RequestQuote Model, HttpPostedFileBase uploadFile, string hfNoOfRec)
        //{

        //    return View("PartialViews/RequestQuote", Model);
        //}

        [HttpPost]
        public void DeleteArtworkAttachment(long AttachmentId)
        {
            _ItemService.DeleteItemAttachment(AttachmentId);
        }
        public class JasonResponseObject
        {
            public Order order;
            public Address billingAddress;
            public Address shippingAddress;
            public string GrossTotal;
            public string SubTotal;
            public string VAT;
            public string DeliveryCostCharges;
            public string CurrencySymbol;
            public string OrderDateValue;
            public string DeliveryDateValue;
            public string BillingCountry;
            public string BillingState;
            public string ShippingCountry;
            public string ShippingState;
        }

        public class JsonAddressClass
        {
            public Address Address;
            public long StateId;
            public long CountryId;
            public long CompanyID;
        }

       
    }
}
