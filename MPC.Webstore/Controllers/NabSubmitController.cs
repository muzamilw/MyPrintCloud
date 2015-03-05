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
using System.Net;
using System.IO;
using System.Text;
using System.Xml;
namespace MPC.Webstore.Controllers
{
    public class NabSubmitController : Controller
    {
        private readonly IPaymentGatewayService _PaymentGatewayService;
        private readonly IOrderService _OrderService;
        private readonly ICompanyService _CompanyService;
        private readonly ICampaignService _campaignService;
        private readonly IUserManagerService _usermanagerService;
        private readonly IPrePaymentService _IPrePaymentService;
        private readonly INABTransactionService _INABTransactionService;

        // GET: NabSubmit
        public NabSubmitController(IPaymentGatewayService PaymentGatewayService, IOrderService OrderService, ICompanyService _CompanyService, ICampaignService _campaignService, IUserManagerService _usermanagerService, IPrePaymentService _IPrePaymentService, INABTransactionService _INABTransactionService)
        {
            this._PaymentGatewayService = PaymentGatewayService;
            this._OrderService = OrderService;
            this._CompanyService = _CompanyService;
            this._campaignService = _campaignService;
            this._usermanagerService = _usermanagerService;
            this._IPrePaymentService = _IPrePaymentService;
            this._INABTransactionService = _INABTransactionService;
        }
        public ActionResult Index()
        {

            NABViewModel model = new NABViewModel();
            List<DropdownType> sourceOfType = new List<DropdownType>();
            sourceOfType.Add(new DropdownType
            {
                value = "0",
                Text = "Select credit card type"
            });
            sourceOfType.Add(new DropdownType
            {
                value = "1",
                Text = "Visa"
            });
            sourceOfType.Add(new DropdownType
            {
                value = "2",
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
        public ActionResult Index(NABViewModel model, int OrderID)
        {

            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;

            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.StoreId];

            int typeid = GetCardTypeIdFromNumber(model.CardNumber);
            bool result = IsValidNumber(model.CardNumber);

            if (model.SelectedCardType != typeid && result == false)
            {
                //ErrorMEsSummry.Visible = true;
                //ErrorMEsSummry.Text = "Sorry, your credit card type and number is not valid.";
                //errorMesgCnt.Visible = true;
            }
            else if (model.SelectedCardType != typeid)
            {
                //ErrorMEsSummry.Visible = true;
                //ErrorMEsSummry.Text = "Sorry, Please select valid card type.";
                //errorMesgCnt.Visible = true;
            }
            else if (result == false)
            {
                //ErrorMEsSummry.Visible = true;
                //ErrorMEsSummry.Text = "Sorry, Please enter a valid card number.";
                //errorMesgCnt.Visible = true;
            }
            else
            {
                PaymentGateway oGateWay = null;

                if (UserCookieManager.StoreMode == (int)StoreMode.Retail)
                {
                    oGateWay = _PaymentGatewayService.GetPaymentGatewayRecord();
                }
                else if (UserCookieManager.StoreMode == (int)StoreMode.Corp && StoreBaseResopnse.Company.isAcceptPaymentOnline == true)
                {
                    oGateWay = _PaymentGatewayService.GetPaymentGatewayRecord(StoreBaseResopnse.Company.CompanyId);
                }
                else if (UserCookieManager.StoreMode == (int)StoreMode.Corp && StoreBaseResopnse.Company.isAcceptPaymentOnline == false)
                {
                    oGateWay = _PaymentGatewayService.GetPaymentGatewayRecord();
                }

                if (oGateWay == null)
                {
                    //ErrorMEsSummry.Visible = true;
                    //errorMesgCnt.Visible = true;
                    //ErrorMEsSummry.Text = "Payment Gateway is not set.";
                }
                else
                {

                    if (OrderID > 0)
                    {

                        Estimate modelOrder = _OrderService.GetOrderByID(OrderID);
                        ShoppingCart shopCart = _OrderService.GetShopCartOrderAndDetails(OrderID, OrderStatus.ShoppingCart);

                        //ErrorMEsSummry.Visible = false;
                        //errorMesgCnt.Visible = false;

                        string orderValue = Math.Round(Convert.ToDouble(modelOrder.Estimate_Total ?? 0), 2).ToString();
                        string xmlFormate = OrderXmlData(OrderID, model.CardNumber, model.SelectedDate, model.SelectedYear.Substring(2), orderValue, modelOrder.Order_Code, oGateWay.BusinessEmail, oGateWay.IdentityToken);
                        WebRequest request = null;
                        WebResponse response = null;

                        string ResponseStatusCode = "515";
                        string statusResponseMessage = "";
                        try
                        {

                            long NabTransactionid = 0;
                            string apiURL = "";

                            if (oGateWay.UseSandbox.HasValue && oGateWay.UseSandbox.Value)
                            {
                                apiURL = oGateWay.TestApiUrl;
                            }
                            else
                            {
                                apiURL = oGateWay.LiveApiUrl;
                            }


                            request = WebRequest.Create(apiURL);
                            // Set the Method property of the request to POST.
                            request.Method = "POST";




                            NabTransactionid = _INABTransactionService.NabTransactionSaveRequest(Convert.ToInt32(OrderID), xmlFormate);
                            byte[] byteArray = Encoding.UTF8.GetBytes(xmlFormate);

                            request.ContentType = "text/xml; encoding='utf-8'";

                            request.ContentLength = byteArray.Length;

                            Stream dataStream = request.GetRequestStream();

                            dataStream.Write(byteArray, 0, byteArray.Length);




                            dataStream.Close();



                            response = request.GetResponse();


                            dataStream = response.GetResponseStream();

                            StreamReader reader = new StreamReader(dataStream);

                            string responseFromServer = reader.ReadToEnd();
                            _INABTransactionService.NabTransactionUpdateRequest(NabTransactionid, responseFromServer);
                            if (!string.IsNullOrEmpty(responseFromServer))
                            {
                                var xmlDoc2 = new XmlDocument();
                                xmlDoc2.LoadXml(responseFromServer);

                                XmlNodeList statusNode = xmlDoc2.GetElementsByTagName("responseCode");

                                if (statusNode[0] != null)
                                {
                                    ResponseStatusCode = statusNode[0].InnerText;
                                }
                                else
                                {
                                    statusNode = xmlDoc2.GetElementsByTagName("statusCode");
                                    if (statusNode[0] != null)
                                    {
                                        ResponseStatusCode = statusNode[0].InnerText;
                                    }
                                }
                                XmlNodeList descNode = xmlDoc2.GetElementsByTagName("responseText");
                                if (descNode[0] != null)
                                {
                                    statusResponseMessage = descNode[0].InnerText;
                                }
                                else
                                {
                                    XmlNodeList descTransNode = xmlDoc2.GetElementsByTagName("statusDescription");
                                    if (descTransNode[0] != null)
                                    {
                                        statusResponseMessage = statusResponseMessage + descTransNode[0].InnerText;
                                    }
                                }
                                reader.Close();
                                dataStream.Close();
                                response.Close();

                                if (ResponseStatusCode == "00" || ResponseStatusCode == "08")
                                {

                                    if (modelOrder.StatusId == 3)
                                    {

                                        XmlNodeList TransNode = xmlDoc2.GetElementsByTagName("txnID");
                                        string transactionID = "";
                                        if (TransNode[0] != null)
                                        {
                                            transactionID = TransNode[0].InnerText;
                                        }


                                        // BLL.EmailManager emailMgr = new EmailManager();
                                        // CompanySiteManager CSM = new CompanySiteManager();
                                        //  tbl_company_sites Serversettingss = CompanySiteManager.GetCompanySite();
                                        int? customerID = null;
                                        StoreMode modeOfStore = StoreMode.Retail;

                                        if (modelOrder != null)
                                            customerID = Convert.ToInt32(modelOrder.ContactId);

                                        // order code and order creation date
                                        CampaignEmailParams cep = new CampaignEmailParams();
                                        string HTMLOfShopReceipt = null;
                                        cep.CompanySiteID = 1;
                                        cep.ContactId = modelOrder.ContactId ?? 0; //SessionParameters.ContactID;
                                        cep.CompanyId = modelOrder.CompanyId;
                                        //SessionParameters.CustomerID;
                                        cep.EstimateID = OrderID; //PageParameters.OrderID;
                                        //Company BrokerCompany = null;
                                        Company CustomerCompany = _CompanyService.GetCustomer(Convert.ToInt32(modelOrder.CompanyId));
                                        CompanyContact CustomrContact = _CompanyService.GetContactById(Convert.ToInt32(modelOrder.ContactId));
                                        _OrderService.SetOrderCreationDateAndCode(OrderID);

                                        if (CustomerCompany.IsCustomer == (int)CustomerTypes.Corporate)
                                        {
                                            modeOfStore = StoreMode.Corp;
                                            HTMLOfShopReceipt = _campaignService.GetPinkCardsShopReceiptPage(OrderID, CustomrContact.ContactId);
                                            // corp
                                        }
                                        else
                                        {
                                            HTMLOfShopReceipt = _campaignService.GetPinkCardsShopReceiptPage(OrderID, 0);
                                            ; // retail
                                        }


                                        Campaign OnlineOrderCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.OnlineOrder);
                                        cep.SalesManagerContactID = Convert.ToInt32(modelOrder.ContactId);
                                        SystemUser EmailOFSM = _usermanagerService.GetSalesManagerDataByID(Convert.ToInt32(CustomerCompany.SalesAndOrderManagerId1));
                                        cep.StoreID = Convert.ToInt32(modelOrder.CompanyId);
                                        cep.AddressID = Convert.ToInt32(modelOrder.CompanyId);
                                        if (CustomerCompany.IsCustomer == (int)CustomerTypes.Corporate)
                                        {

                                            long ManagerID = _CompanyService.GetContactIdByRole(modelOrder.CompanyId, (int)Roles.Manager);
                                            cep.CorporateManagerID = ManagerID;
                                            _campaignService.SendEmailToSalesManager((int)Events.NewOrderToSalesManager, Convert.ToInt32(modelOrder.ContactId), Convert.ToInt32(modelOrder.CompanyId), 0, OrderID, 0, Convert.ToInt32(ManagerID), StoreMode.Corp, CustomerCompany, EmailOFSM);
                                        }
                                        else
                                        {

                                            _campaignService.SendEmailToSalesManager((int)Events.NewQuoteToSalesManager, (int)modelOrder.ContactId, (int)modelOrder.CompanyId, 0, OrderID, 0, 0, StoreMode.Retail, CustomerCompany, EmailOFSM);
                                        }

                                        //in case of retail <<SalesManagerEmail>> variable should be resolved by organization's sales manager
                                        // thats why after getting the sales manager records ew are sending his email as a parameter in email body genetor
                                        _campaignService.emailBodyGenerator(OnlineOrderCampaign, cep, CustomrContact, StoreMode.Retail, Convert.ToInt32(CustomerCompany.OrganisationId), "", HTMLOfShopReceipt, "", EmailOFSM.Email);
                                        _IPrePaymentService.CreatePrePayment(PaymentMethods.NAB, OrderID, Convert.ToInt32(customerID), 0, transactionID, Convert.ToDouble(orderValue), modeOfStore, ResponseStatusCode + " " + statusResponseMessage);



                                        Response.Redirect("../Receipt.aspx?OrderID=" + OrderID.ToString());
                                    }
                                    else
                                    {
                                        //ErrorMEsSummry.Visible = true;
                                        //errorMesgCnt.Visible = true;
                                        //ErrorMEsSummry.Text = "invalid Order.";
                                    }
                                }
                                else
                                {
                                    //ErrorMEsSummry.Visible = true;
                                    //errorMesgCnt.Visible = true;
                                    //ErrorMEsSummry.Text = statusResponseMessage;
                                }
                            }
                            else
                            {
                                //ErrorMEsSummry.Visible = true;
                                //errorMesgCnt.Visible = true;
                                //ErrorMEsSummry.Text = "Error occurred while processing.";
                            }
                        }
                        catch
                        {
                            throw;
                        }
                        finally
                        {
                            if (request != null) request.GetRequestStream().Close();
                            if (response != null) response.GetResponseStream().Close();
                        }
                    }
                    else
                    {
                        //ErrorMEsSummry.Visible = true;
                        //errorMesgCnt.Visible = true;
                        //ErrorMEsSummry.Text = "Invalid Order number.";
                    }
                }

            }
            return View("Index");
        }

        private string OrderXmlData(int orderID, string ccNumber, string ccDate, string ccYear, string OrderAmount,
            string PONumber, string marchantID, string Password)
        {
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;
            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.StoreId];

            int indexOdDecimal = OrderAmount.IndexOf(".");
            if (indexOdDecimal >= 0)
            {
                OrderAmount = OrderAmount.Replace(".", "").Trim();
            }
            else
            {
                OrderAmount = OrderAmount + "00";
            }
            // ABC0001 changeit
            string xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                            <NABTransactMessage>
                            <MessageInfo>
                            <messageID>#messageID</messageID>
                            <messageTimestamp>#messageTimeStamp</messageTimestamp>
                            <timeoutValue>60</timeoutValue>
                            <apiVersion>xml-4.2</apiVersion>
                            </MessageInfo>
                            <MerchantInfo>
                            <merchantID>#merchantID</merchantID>
                            <password>#merchantPassword</password>
                            </MerchantInfo>
                            <RequestType>Payment</RequestType>
                            <Payment>
                            <TxnList count=""1"">
                            <Txn ID=""1"">
                            <txnType>0</txnType>
                            <txnSource>23</txnSource>
                            <amount>#OrderAmount</amount>
                            <currency>#CurrencyNAB</currency>
                            <purchaseOrderNo>#PONumber</purchaseOrderNo>
                            <CreditCardInfo>
                            <cardNumber>#cardNumber</cardNumber>
                            <expiryDate>#ExpiryDate</expiryDate>
                            </CreditCardInfo>
                            </Txn>
                            </TxnList>
                            </Payment>
                            </NABTransactMessage>";


            xml = xml.Replace("#messageID", orderID.ToString());
            xml = xml.Replace("#cardNumber", ccNumber);
            xml = xml.Replace("#OrderAmount", OrderAmount);
            xml = xml.Replace("#PONumber", PONumber);
            xml = xml.Replace("#merchantID", marchantID);
            xml = xml.Replace("#merchantPassword", Password);
            if (StoreBaseResopnse.Currency != null)
            {
                xml = xml.Replace("#CurrencyNAB", StoreBaseResopnse.Currency);
            }
            else
            {
                xml = xml.Replace("#CurrencyNAB", "AUD");
            }

            xml = xml.Replace("#ExpiryDate", ccDate + "/" + ccYear);

            xml = xml.Replace("#messageTimeStamp",
                DateTime.Now.Year + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Hour + DateTime.Now.Minute +
                DateTime.Now.Second + DateTime.Now.Millisecond + "000+600");

            xml = xml.Replace("\r\n", "");
            return xml;
        }

        public bool IsValidNumber(string cardNum)
        {
            string cardRegex = "^(?:(?<Visa>4\\d{3})|(?<MasterCard>5[1-5]\\d{2})|(?<Discover>6011)|(?<DinersClub>(?:3[68]\\d{2})|(?:30[0-5]\\d))|(?<Amex>3[47]\\d{2}))([ -]?)(?(DinersClub)(?:\\d{6}\\1\\d{4})|(?(Amex)(?:\\d{6}\\1\\d{5})|(?:\\d{4}\\1\\d{4}\\1\\d{4})))$";

            Regex cardTest = new Regex(cardRegex);

            //Determine the card type based on the number
            CreditCardTypeType? cardType = GetCardTypeFromNumber(cardNum);

            //Call the base version of IsValidNumber and pass the 
            //number and card type
            if (IsValidNumber(cardNum, cardType))
                return true;
            else
                return false;
        }
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
        public static CreditCardTypeType? GetCardTypeFromNumber(string cardNum)
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
                return CreditCardTypeType.MasterCard;
            }
            else if (gc[CreditCardTypeType.Visa.ToString()].Success)
            {
                return CreditCardTypeType.Visa;
            }
            else if (gc[CreditCardTypeType.DinersClub.ToString()].Success)
            {
                return CreditCardTypeType.DinersClub;
            }
            else if (gc[CreditCardTypeType.Amex.ToString()].Success)
            {
                return CreditCardTypeType.Amex;
            }
            else
            {
                //Card type is not supported by our system, return null
                //(You can modify this code to support more (or less)
                // card types as it pertains to your application)
                return null;
            }
        }
        public static bool IsValidNumber(string cardNum, CreditCardTypeType? cardType)
        {
            string cardRegex = "^(?:(?<Visa>4\\d{3})|(?<MasterCard>5[1-5]\\d{2})|(?<Discover>6011)|(?<DinersClub>(?:3[68]\\d{2})|(?:30[0-5]\\d))|(?<Amex>3[47]\\d{2}))([ -]?)(?(DinersClub)(?:\\d{6}\\1\\d{4})|(?(Amex)(?:\\d{6}\\1\\d{5})|(?:\\d{4}\\1\\d{4}\\1\\d{4})))$";

            //Create new instance of Regex comparer with our 
            //credit card regex pattern
            Regex cardTest = new Regex(cardRegex);

            //Make sure the supplied number matches the supplied
            //card type
            if (cardTest.Match(cardNum).Groups[cardType.ToString()].Success)
            {
                //If the card type matches the number, then run it
                //through Luhn's test to make sure the number appears correct
                if (PassesLuhnTest(cardNum))
                    return true;
                else
                    //The card fails Luhn's test
                    return false;
            }
            else
                //The card number does not match the card type
                return false;
        }
        public static bool PassesLuhnTest(string cardNumber)
        {
            //Clean the card number- remove dashes and spaces
            cardNumber = cardNumber.Replace("-", "").Replace(" ", "");

            //Convert card number into digits array
            int[] digits = new int[cardNumber.Length];
            for (int len = 0; len < cardNumber.Length; len++)
            {
                digits[len] = Int32.Parse(cardNumber.Substring(len, 1));
            }

            //Luhn Algorithm
            //Adapted from code availabe on Wikipedia at
            //http://en.wikipedia.org/wiki/Luhn_algorithm
            int sum = 0;
            bool alt = false;
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                int curDigit = digits[i];
                if (alt)
                {
                    curDigit *= 2;
                    if (curDigit > 9)
                    {
                        curDigit -= 9;
                    }
                }
                sum += curDigit;
                alt = !alt;
            }

            //If Mod 10 equals 0, the number is good and this will return true
            return sum % 10 == 0;
        }
    }
}