using MPC.Webstore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Models.DomainModels;
using MPC.Models.Common;
using System.Text.RegularExpressions;
using MPC.Webstore.Common;
using MPC.Interfaces.WebStoreServices;
using System.Runtime.Caching;
namespace MPC.Webstore.Controllers
{
    public class NabSubmitController : Controller
    {
        private readonly IPaymentGatewayService _PaymentGatewayService;
        private readonly IOrderService _OrderService;
        // GET: NabSubmit
        public NabSubmitController(IPaymentGatewayService PaymentGatewayService, IOrderService OrderService)
        {
            this._PaymentGatewayService = PaymentGatewayService;
            this._OrderService = OrderService;
        }
        public ActionResult Index()
        {

            NABViewModel model = new NABViewModel();
            List<DropdownType> sourceOfType = new List<DropdownType>();
            sourceOfType.Add(new DropdownType
            {
                value = "Select credit card type",
                Text = "Select credit card type"
            });
            sourceOfType.Add(new DropdownType
            {
                value = "Visa",
                Text = "Visa"
            });
            sourceOfType.Add(new DropdownType
            {
                value = "Master",
                Text = "Master"
            });

            model.ListCardType = new SelectList(sourceOfType, "value", "Text", 0);
            List<DropdownType> sourceOfDate = new List<DropdownType>();
            for (int i = 1; i <= 12; i++)
            {
                sourceOfDate.Add(new DropdownType
                {
                   // value = i,
                    value = i <= 9 ? "0" + i : i.ToString(),
                   Text = i <= 9 ? "0" + i : i.ToString()
                });
            }
            model.ListDate = new SelectList(sourceOfDate, "value", "Text");
            List<DropdownType> sourceOfYear = new List<DropdownType>();
            for (int i = 1; i <= 15; i++)
            {
                if (i == 1)
                {
                   // sourceOfYear.Add(new DropdownType() { value = i, Text = DateTime.Now.Year.ToString() });
                    sourceOfYear.Add(new DropdownType() { value = DateTime.Now.Year.ToString(), Text = DateTime.Now.Year.ToString() });
                }
                else
                {
                    sourceOfYear.Add(new DropdownType() { value = DateTime.Now.AddYears(i - 1).Year.ToString(), Text = DateTime.Now.AddYears(i - 1).Year.ToString() });
                }
            }
            model.ListYear = new SelectList(sourceOfYear, "value", "Text");
            return View("PartialViews/NabSubmit", model);
        }

        [HttpPost]
        //public ActionResult Index(NABViewModel model, int OrderID)
        //{

        //    string CacheKeyName = "CompanyBaseResponse";
        //    ObjectCache cache = MemoryCache.Default;

        //    MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.StoreId];
          
        //    int typeid = GetCardTypeIdFromNumber(model.CardNumber);
        //    bool result = IsValidNumber(model.CardNumber);

        //    if (model.SelectedCardType != typeid && result == false)
        //    {
        //        //ErrorMEsSummry.Visible = true;
        //        //ErrorMEsSummry.Text = "Sorry, your credit card type and number is not valid.";
        //        //errorMesgCnt.Visible = true;
        //    }
        //    else if (model.SelectedCardType != typeid)
        //    {
        //        //ErrorMEsSummry.Visible = true;
        //        //ErrorMEsSummry.Text = "Sorry, Please select valid card type.";
        //        //errorMesgCnt.Visible = true;
        //    }
        //    else if (result == false)
        //    {
        //        //ErrorMEsSummry.Visible = true;
        //        //ErrorMEsSummry.Text = "Sorry, Please enter a valid card number.";
        //        //errorMesgCnt.Visible = true;
        //    }
        //    else
        //    {
        //        PaymentGateway oGateWay = null;

        //        if (UserCookieManager.StoreMode == (int)StoreMode.Retail)
        //        {
        //            oGateWay = _PaymentGatewayService.GetPaymentGatewayRecord();
        //        }
        //        else if (UserCookieManager.StoreMode == (int) StoreMode.Corp && StoreBaseResopnse.Company.isBrokerCanAcceptPaymentOnline == true)
        //        {
        //            oGateWay = _PaymentGatewayService.GetPaymentGatewayRecord(StoreBaseResopnse.Company.CompanyId);
        //        }
        //        else if (UserCookieManager.StoreMode == (int)StoreMode.Corp && StoreBaseResopnse.Company.isBrokerCanAcceptPaymentOnline == false)
        //        {
        //            oGateWay = _PaymentGatewayService.GetPaymentGatewayRecord();
        //        }

        //        if (oGateWay == null)
        //        {
        //            //ErrorMEsSummry.Visible = true;
        //            //errorMesgCnt.Visible = true;
        //            //ErrorMEsSummry.Text = "Payment Gateway is not set.";
        //        }
        //        else
        //        {

        //            if (OrderID > 0)
        //            {
                        
        //                Estimate modelOrder = _OrderService.GetOrderByID(OrderID);
        //                ShoppingCart shopCart = _OrderService.GetShopCartOrderAndDetails(OrderID, OrderStatus.ShoppingCart);
                       
        //                //ErrorMEsSummry.Visible = false;
        //                //errorMesgCnt.Visible = false;
                        
        //                string orderValue = Math.Round(Convert.ToDouble(modelOrder.Estimate_Total?? 0), 2).ToString();
        //                string xmlFormate = OrderXmlData(OrderID, model.CardNumber, model.SelectedDate, model.SelectedYear.Substring(2), orderValue, modelOrder.OrderCode, oGateWay.BusinessEmail, oGateWay.IdentityToken);
        //                WebRequest request = null;
        //                WebResponse response = null;

        //                string ResponseStatusCode = "515";
        //                string statusResponseMessage = "";
        //                try
        //                {

        //                    long NabTransactionid = 0;

        //                    string apiURL = ConfigurationSettings.AppSettings["NABAPIURL"];

        //                    request = WebRequest.Create(apiURL);
        //                    // Set the Method property of the request to POST.
        //                    request.Method = "POST";




        //                    NabTransactionid = PaymentsManager.NabTransactionSaveRequest(Convert.ToInt32(modelOrder.OrderID), xmlFormate);
        //                    byte[] byteArray = Encoding.UTF8.GetBytes(xmlFormate);

        //                    request.ContentType = "text/xml; encoding='utf-8'";

        //                    request.ContentLength = byteArray.Length;

        //                    Stream dataStream = request.GetRequestStream();

        //                    dataStream.Write(byteArray, 0, byteArray.Length);




        //                    dataStream.Close();



        //                    response = request.GetResponse();


        //                    dataStream = response.GetResponseStream();

        //                    StreamReader reader = new StreamReader(dataStream);

        //                    string responseFromServer = reader.ReadToEnd();
        //                    PaymentsManager.NabTransactionUpdateRequest(NabTransactionid, responseFromServer);
        //                    if (!string.IsNullOrEmpty(responseFromServer))
        //                    {
        //                        var xmlDoc2 = new XmlDocument();
        //                        xmlDoc2.LoadXml(responseFromServer);

        //                        XmlNodeList statusNode = xmlDoc2.GetElementsByTagName("responseCode");

        //                        if (statusNode[0] != null)
        //                        {
        //                            ResponseStatusCode = statusNode[0].InnerText;
        //                        }
        //                        else
        //                        {
        //                            statusNode = xmlDoc2.GetElementsByTagName("statusCode");
        //                            if (statusNode[0] != null)
        //                            {
        //                                ResponseStatusCode = statusNode[0].InnerText;
        //                            }
        //                        }
        //                        XmlNodeList descNode = xmlDoc2.GetElementsByTagName("responseText");
        //                        if (descNode[0] != null)
        //                        {
        //                            statusResponseMessage = descNode[0].InnerText;
        //                        }
        //                        else
        //                        {
        //                            XmlNodeList descTransNode = xmlDoc2.GetElementsByTagName("statusDescription");
        //                            if (descTransNode[0] != null)
        //                            {
        //                                statusResponseMessage = statusResponseMessage + descTransNode[0].InnerText;
        //                            }
        //                        }
        //                        reader.Close();
        //                        dataStream.Close();
        //                        response.Close();

        //                        if (ResponseStatusCode == "00" || ResponseStatusCode == "08")
        //                        {

        //                            if (modelOrder.StatusID == 3)
        //                            {

        //                                XmlNodeList TransNode = xmlDoc2.GetElementsByTagName("txnID");
        //                                string transactionID = "";
        //                                if (TransNode[0] != null)
        //                                {
        //                                    transactionID = TransNode[0].InnerText;
        //                                }


        //                                BLL.EmailManager emailMgr = new EmailManager();
        //                                CompanySiteManager CSM = new CompanySiteManager();
        //                                tbl_company_sites Serversettingss = CompanySiteManager.GetCompanySite();
        //                                int? customerID = null;
        //                                StoreMode modeOfStore = StoreMode.Retail;

        //                                if (modelOrder != null)
        //                                    customerID = modelOrder.CustomerID;

        //                                // order code and order creation date
        //                                CampaignEmailParams cep = new CampaignEmailParams();
        //                                string HTMLOfShopReceipt = null;
        //                                cep.CompanySiteID = 1;
        //                                cep.ContactID = modelOrder.ContactUserID ?? 0; //SessionParameters.ContactID;
        //                                cep.ContactCompanyID = modelOrder.CustomerID ?? 0;
        //                                //SessionParameters.CustomerID;
        //                                cep.EstimateID = PageParameters.OrderID; //PageParameters.OrderID;
        //                                tbl_contactcompanies BrokerCompany = null;
        //                                tbl_contactcompanies CustomerCompany =
        //                                    CustomerManager.GetCustomer(Convert.ToInt32(modelOrder.CustomerID));
        //                                tbl_contacts CustomrContact =
        //                                    ContactManager.GetContactById(Convert.ToInt32(modelOrder.ContactUserID));
        //                                (new BLL.OrderManager()).SetOrderCreationDateAndCode(PageParameters.OrderID);

        //                                if (modelOrder.BrokerID != null)
        //                                {
        //                                    modeOfStore = StoreMode.Broker;
        //                                    HTMLOfShopReceipt =
        //                                        (new BLL.EmailManager()).GetPinkCardsShopReceiptPage(
        //                                            PageParameters.OrderID, modelOrder.BrokerID ?? 0, 0);
        //                                }
        //                                else if (CustomerCompany.IsCustomer == (int)CustomerTypes.Corporate)
        //                                {
        //                                    modeOfStore = StoreMode.Corp;
        //                                    HTMLOfShopReceipt =
        //                                        (new BLL.EmailManager()).GetPinkCardsShopReceiptPage(
        //                                            PageParameters.OrderID, 0, 1); // corp
        //                                }
        //                                else
        //                                {
        //                                    HTMLOfShopReceipt =
        //                                        (new BLL.EmailManager()).GetPinkCardsShopReceiptPage(
        //                                            PageParameters.OrderID, 0, 0); // retail
        //                                }


        //                                tbl_campaigns OnlineOrderCampaign =
        //                                    (new BLL.EmailManager()).GetCampaignRecordByEmailEvent(
        //                                        (int)BLL.Events.OnlineOrder);
        //                                if (modelOrder.BrokerID != null)
        //                                {
        //                                    cep.ContactCompanyID =
        //                                        SessionParameters.BrokerContactCompany.ContactCompanyID;
        //                                    BrokerCompany = CustomerManager.GetCustomer(modelOrder.BrokerID ?? 0);
        //                                    cep.BrokerID = modelOrder.BrokerID ?? 0;
        //                                    int AdminIDOfBroker =
        //                                        ContactManager.GetBrokerByRole(BrokerCompany.ContactCompanyID,
        //                                            Convert.ToInt32(Roles.Adminstrator));
        //                                    cep.BrokerContactID = AdminIDOfBroker;
        //                                    cep.SalesManagerContactID = AdminIDOfBroker;
        //                                    cep.StoreID = modelOrder.BrokerID ?? 0;
        //                                    cep.AddressID = modelOrder.BrokerID ?? 0;
        //                                    emailMgr.emailBodyGenerator(OnlineOrderCampaign, Serversettingss, cep,
        //                                        CustomrContact, StoreMode.Broker, "", HTMLOfShopReceipt, "", "", "", "",
        //                                        null, "", null, "", BrokerCompany.Name);
        //                                    emailMgr.SendEmailToSalesManager((int)EmailEvents.NewOrderToSalesManager,
        //                                        Convert.ToInt32(modelOrder.ContactUserID),
        //                                        Convert.ToInt32(modelOrder.CustomerID), modelOrder.BrokerID ?? 0,
        //                                        PageParameters.OrderID, Serversettingss, AdminIDOfBroker, 0,
        //                                        StoreMode.Broker, BrokerCompany.Name);
        //                                }
        //                                else
        //                                {
        //                                    cep.SalesManagerContactID = Convert.ToInt32(modelOrder.ContactUserID);

        //                                    if (CustomerCompany.IsCustomer == (int)CustomerTypes.Corporate)
        //                                    {
        //                                        cep.StoreID = Convert.ToInt32(modelOrder.CustomerID);
        //                                        cep.AddressID = Convert.ToInt32(modelOrder.CustomerID);
        //                                        int ManagerID =
        //                                            ContactManager.GetBrokerByRole(
        //                                                Convert.ToInt32(modelOrder.CustomerID),
        //                                                (int)Roles.Manager);
        //                                        //ContactManager.GetBrokerByRole(SessionParameters.BrokerContactCompany.ContactCompanyID, Convert.ToInt32(Roles.Adminstrator));
        //                                        cep.CorporateManagerID = ManagerID;
        //                                        emailMgr.SendEmailToSalesManager(
        //                                            (int)EmailEvents.NewOrderToSalesManager,
        //                                            Convert.ToInt32(modelOrder.ContactUserID),
        //                                            Convert.ToInt32(modelOrder.CustomerID), 0, PageParameters.OrderID,
        //                                            Serversettingss, 0, ManagerID, StoreMode.Corp);
        //                                    }
        //                                    else
        //                                    {
        //                                        cep.AddressID = Convert.ToInt32(modelOrder.CustomerID);
        //                                        cep.StoreID = Serversettingss.CompanySiteID;
        //                                        emailMgr.SendEmailToSalesManager(
        //                                            (int)EmailEvents.NewOrderToSalesManager,
        //                                            Convert.ToInt32(modelOrder.ContactUserID),
        //                                            Convert.ToInt32(modelOrder.CustomerID), 0, PageParameters.OrderID,
        //                                            Serversettingss, 0, 0, StoreMode.Retail);
        //                                    }
        //                                    UsersManager usermgr = new UsersManager();
        //                                    //in case of retail <<SalesManagerEmail>> variable should be resolved by organization's sales manager
        //                                    // thats why after getting the sales manager records ew are sending his email as a parameter in email body genetor
        //                                    tbl_systemusers EmailOFSM =
        //                                        usermgr.GetSalesManagerDataByID(
        //                                            Convert.ToInt32(Serversettingss.SalesManagerID));
        //                                    emailMgr.emailBodyGenerator(OnlineOrderCampaign, Serversettingss, cep,
        //                                        CustomrContact, StoreMode.Retail, "", HTMLOfShopReceipt, "",
        //                                        EmailOFSM.Email);

        //                                }
        //                                BLL.PaymentsManager payManager = new BLL.PaymentsManager();
        //                                payManager.CreatePrePayment(Model.PaymentMethods.NAB, PageParameters.OrderID,
        //                                    customerID, 0, transactionID, Convert.ToDouble(orderValue),
        //                                    modeOfStore, ResponseStatusCode + " " + statusResponseMessage);


        //                                Response.Redirect("../Receipt.aspx?OrderID=" + PageParameters.OrderID.ToString());
        //                            }
        //                            else
        //                            {
        //                                ErrorMEsSummry.Visible = true;
        //                                errorMesgCnt.Visible = true;
        //                                ErrorMEsSummry.Text = "invalid Order.";
        //                            }
        //                        }
        //                        else
        //                        {
        //                            ErrorMEsSummry.Visible = true;
        //                            errorMesgCnt.Visible = true;
        //                            ErrorMEsSummry.Text = statusResponseMessage;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        ErrorMEsSummry.Visible = true;
        //                        errorMesgCnt.Visible = true;
        //                        ErrorMEsSummry.Text = "Error occurred while processing.";
        //                    }
        //                }
        //                catch
        //                {
        //                    throw;
        //                }
        //                finally
        //                {
        //                    if (request != null) request.GetRequestStream().Close();
        //                    if (response != null) response.GetResponseStream().Close();
        //                }
        //            }
        //            else
        //            {
        //                ErrorMEsSummry.Visible = true;
        //                errorMesgCnt.Visible = true;
        //                ErrorMEsSummry.Text = "Invalid Order number.";
        //            }
        //        }

        //    }
        //    return view();
        //}


        //public bool IsValidNumber(string cardNum)
        //{
        //    string cardRegex = "^(?:(?<Visa>4\\d{3})|(?<MasterCard>5[1-5]\\d{2})|(?<Discover>6011)|(?<DinersClub>(?:3[68]\\d{2})|(?:30[0-5]\\d))|(?<Amex>3[47]\\d{2}))([ -]?)(?(DinersClub)(?:\\d{6}\\1\\d{4})|(?(Amex)(?:\\d{6}\\1\\d{5})|(?:\\d{4}\\1\\d{4}\\1\\d{4})))$";

        //    Regex cardTest = new Regex(cardRegex);

        //    //Determine the card type based on the number
        //    CreditCardTypeType? cardType = GetCardTypeFromNumber(cardNum);

        //    //Call the base version of IsValidNumber and pass the 
        //    //number and card type
        //    if (IsValidNumber(cardNum, cardType))
        //        return true;
        //    else
        //        return false;
        //}
        public int GetCardTypeIdFromNumber(string cardNum)
        {
            string cardRegex = "^(?:(?<Visa>4\\d{3})|(?<MasterCard>5[1-5]\\d{2})|(?<Discover>6011)|(?<DinersClub>(?:3[68]\\d{2})|(?:30[0-5]\\d))|(?<Amex>3[47]\\d{2}))([ -]?)(?(DinersClub)(?:\\d{6}\\1\\d{4})|(?(Amex)(?:\\d{6}\\1\\d{5})|(?:\\d{4}\\1\\d{4}\\1\\d{4})))$";

            //Create new instance of Regex comparer with our
            //credit card regex pattern
            Regex cardTest = new Regex(cardRegex);

            //Compare the supplied card number with the regex
            //pattern and get reference regex named groups
            GroupCollection gc = cardTest.Match(cardNum).Groups;

            //Compare each card type to the named groups to 
            //determine which card type the number matches

            if (gc[CreditCardTypeType.MasterCard.ToString()].Success)
            {
                return Convert.ToInt32(CreditCardTypeType.MasterCard);
            }
            else if (gc[CreditCardTypeType.Visa.ToString()].Success)
            {
                return Convert.ToInt32(CreditCardTypeType.Visa);
            }
            else if (gc[CreditCardTypeType.DinersClub.ToString()].Success)
            {
                return Convert.ToInt32(CreditCardTypeType.DinersClub);
            }
            else if (gc[CreditCardTypeType.Amex.ToString()].Success)
            {
                return Convert.ToInt32(CreditCardTypeType.Amex);
            }
            else
            {
                //Card type is not supported by our system, return null
                //(You can modify this code to support more (or less)
                // card types as it pertains to your application)
                return 0;
            }
        }
    
    }
}