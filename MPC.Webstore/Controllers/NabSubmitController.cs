using MPC.Webstore.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MPC.Models.DomainModels;
using MPC.Models.Common;
using System.Text.RegularExpressions;
using MPC.Webstore.Common;
using MPC.Interfaces.WebStoreServices;
using System.Net;
using System.IO;
using System.Text;
using System.Xml;
using MPC.Models.ResponseModels;
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
        public ActionResult Index(int OrderID)
        {

            //switch (Message)
            //{
            //    case ErrorSummary.InvalidCardTypeNumber:
            //        {
            //            ViewBag.ErrorMessage = "Sorry, your credit Card Type and Number is not valid";
            //            break;
            //        }
            //    case ErrorSummary.InvalidCardType:
            //        {
            //            ViewBag.ErrorMessage = "Sorry, Please Select Valid Card Type.";
            //            break;
            //        }
            //    case ErrorSummary.InvalidCardNumber:
            //        {
            //            ViewBag.ErrorMessage = "Sorry, Please Enter a Valid Card Number.";
            //            break;
            //        }
            //    case ErrorSummary.InvalidPaymentGateway:
            //        {
            //            ViewBag.ErrorMessage = "Payment Gateway is not set.";
            //            break;
            //        }
            //    case ErrorSummary.InvalidOrder:
            //        {
            //            ViewBag.ErrorMessage = "Invalid Order";
            //            break;
            //        }
            //    case ErrorSummary.statusResponseMessage:
            //        {
            //            ViewBag.ErrorMessage = "statusResponseMessage.";
            //            break;
            //        }
            //    case ErrorSummary.Error:
            //        {
            //            ViewBag.ErrorMessage = "Error occurred while processing.";
            //            break;
            //        }
            //    default:
            //        {
            //            ViewBag.ErrorMessage = "null";
            //            break;
            //        }



            //}

            NABViewModel model = new NABViewModel();
            model.OrderId = OrderID;
            List<DropdownCardType> sourceOfType = new List<DropdownCardType>();
            sourceOfType.Add(new DropdownCardType
            {
                value = 0,
                Text = "Select credit card type"
            });
            sourceOfType.Add(new DropdownCardType
            {
                value = 1,
                Text = "Visa"
            });
            sourceOfType.Add(new DropdownCardType
            {
                value = 2,
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
        public ActionResult Index(NABViewModel model)
        {

            
            //string CacheKeyName = "CompanyBaseResponse";
            //ObjectCache cache = MemoryCache.Default;

            //MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
            MyCompanyDomainBaseReponse StoreBaseResopnse = _CompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            int typeid = _CompanyService.GetCardTypeIdFromNumber(model.CardNumber);
            bool result = _CompanyService.IsValidNumber(model.CardNumber);

            if (model.SelectedCardType != typeid && result == false)
            {
                return View("PartialViews/NabSubmit", intializeModel(ErrorSummary.InvalidCardTypeNumber, model.OrderId));
                
            }
            else if (model.SelectedCardType != typeid)
            {
                return View("PartialViews/NabSubmit", intializeModel(ErrorSummary.InvalidCardType, model.OrderId));
                

            }
            else if (result == false)
            {
                return View("PartialViews/NabSubmit", intializeModel(ErrorSummary.InvalidCardNumber, model.OrderId));
                
            }
            else
            {
                PaymentGateway oGateWay = null;
                if (StoreBaseResopnse.Company.isPaymentRequired == true)
                {
                    oGateWay = _PaymentGatewayService.GetPaymentByMethodId(StoreBaseResopnse.Company.CompanyId, (int)PaymentMethods.NAB);
                }


                if (oGateWay == null)
                {
                    return View("PartialViews/NabSubmit", intializeModel(ErrorSummary.InvalidPaymentGateway, model.OrderId));
                    

                }
                else
                {

                    if (model.OrderId > 0)
                    {

                        Estimate modelOrder = _OrderService.GetOrderByID(model.OrderId);
                        ShoppingCart shopCart = _OrderService.GetShopCartOrderAndDetails(model.OrderId, OrderStatus.ShoppingCart);


                        string orderValue = Math.Round(Convert.ToDouble(modelOrder.Estimate_Total ?? 0), 2).ToString();
                        string xmlFormate = OrderXmlData(model.OrderId, model.CardNumber, model.SelectedDate, model.SelectedYear.Substring(2), orderValue, modelOrder.Order_Code, oGateWay.BusinessEmail, oGateWay.IdentityToken);
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




                            NabTransactionid = _INABTransactionService.NabTransactionSaveRequest(Convert.ToInt32(model.OrderId), xmlFormate);
                            byte[] byteArray = Encoding.UTF8.GetBytes(xmlFormate);

                            request.ContentType = "text/xml; encoding='utf-8'";

                            request.ContentLength = byteArray.Length;
                            
                            Stream dataStream = request.GetRequestStream( );

                            dataStream.Write(byteArray, 0, byteArray.Length);




                            dataStream.Close();



                            response = request.GetResponse();


                            dataStream = response.GetResponseStream();
                         //   dataStream.CanRead = true;
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
                                        int? customerID = null;
                                        StoreMode modeOfStore = StoreMode.Retail;

                                        if (modelOrder != null)
                                            customerID = Convert.ToInt32(modelOrder.ContactId);

                                        // order code and order creation date
                                        CampaignEmailParams cep = new CampaignEmailParams();
                                        string HTMLOfShopReceipt = null;
                                        cep.OrganisationId = UserCookieManager.WEBOrganisationID;
                                        cep.ContactId = modelOrder.ContactId ?? 0; //SessionParameters.ContactID;
                                        cep.CompanyId = modelOrder.CompanyId;

                                        cep.EstimateId = model.OrderId; //PageParameters.OrderID;

                                        Company CustomerCompany = _CompanyService.GetCustomer(Convert.ToInt32(modelOrder.CompanyId));
                                        CompanyContact CustomrContact = _CompanyService.GetContactById(Convert.ToInt32(modelOrder.ContactId));
                                        _OrderService.SetOrderCreationDateAndCode(model.OrderId, UserCookieManager.WEBOrganisationID);

                                        if (CustomerCompany.IsCustomer == (int)CustomerTypes.Corporate)
                                        {
                                            modeOfStore = StoreMode.Corp;
                                            HTMLOfShopReceipt = _campaignService.GetPinkCardsShopReceiptPage(model.OrderId, CustomrContact.ContactId);
                                            // corp
                                        }
                                        else
                                        {
                                            HTMLOfShopReceipt = _campaignService.GetPinkCardsShopReceiptPage(model.OrderId, 0);
                                            // retail
                                        }


                                        Campaign OnlineOrderCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.OnlineOrder, UserCookieManager.WEBOrganisationID, UserCookieManager.WBStoreId);
                                        cep.SalesManagerContactID = Convert.ToInt32(modelOrder.ContactId);
                                        SystemUser EmailOFSM =  _usermanagerService.GetSalesManagerDataByID(CustomerCompany.SalesAndOrderManagerId1.Value);
                                        cep.StoreId = UserCookieManager.WBStoreId;
                                        cep.AddressId = modelOrder.CompanyId;
                                        if (CustomerCompany.IsCustomer == (int)CustomerTypes.Corporate)
                                        {

                                            long ManagerID = _CompanyService.GetContactIdByRole(modelOrder.CompanyId, (int)Roles.Manager);
                                            cep.CorporateManagerID = ManagerID;
                                            _campaignService.SendEmailToSalesManager((int)Events.NewOrderToSalesManager, Convert.ToInt32(modelOrder.ContactId), Convert.ToInt32(modelOrder.CompanyId), model.OrderId, UserCookieManager.WEBOrganisationID, Convert.ToInt32(ManagerID), StoreMode.Corp, UserCookieManager.WBStoreId, EmailOFSM);
                                        }
                                        else
                                        {

                                            _campaignService.SendEmailToSalesManager((int)Events.NewOrderToSalesManager, (int)modelOrder.ContactId, (int)modelOrder.CompanyId, model.OrderId, UserCookieManager.WEBOrganisationID, 0, StoreMode.Retail, UserCookieManager.WBStoreId, EmailOFSM);
                                        }

                                        //in case of retail <<SalesManagerEmail>> variable should be resolved by organization's sales manager
                                        // thats why after getting the sales manager records ew are sending his email as a parameter in email body genetor
                                        _campaignService.emailBodyGenerator(OnlineOrderCampaign, cep, CustomrContact, StoreMode.Retail, Convert.ToInt32(CustomerCompany.OrganisationId), "", HTMLOfShopReceipt, "", EmailOFSM.Email);
                                        _IPrePaymentService.CreatePrePayment(PaymentMethods.NAB, model.OrderId, Convert.ToInt32(customerID), 0, transactionID, Convert.ToDouble(orderValue), modeOfStore, ResponseStatusCode + " " + statusResponseMessage);

                                        return View("PartialViews/Receipt", OrderDetailModel(model.OrderId.ToString())); //Redirect(Url.Action("Index", "Receipt", new { OrderId = model.OrderId.ToString() }));


                                    }
                                    else
                                    {
                                        return View("PartialViews/NabSubmit", intializeModel(ErrorSummary.InvalidOrder, model.OrderId));
                                        


                                    }
                                }
                                else
                                {
                                    return View("PartialViews/NabSubmit", intializeModel(ErrorSummary.statusResponseMessage, model.OrderId));
                                    
                                }
                            }
                            else
                            {
                                return View("PartialViews/NabSubmit", intializeModel(ErrorSummary.Error, model.OrderId));
                                

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
                        return View("PartialViews/NabSubmit", intializeModel(ErrorSummary.InvalidOrder, model.OrderId));
                        

                    }
                }

            }

        }
        private OrderDetail OrderDetailModel(string OrderId)
        {
            //string CacheKeyName = "CompanyBaseResponse";
            //ObjectCache cache = MemoryCache.Default;

            //MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];

            MyCompanyDomainBaseReponse StoreBaseResopnse = _CompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);


            if (StoreBaseResopnse.Company.ShowPrices ?? true)
            {
                ViewBag.IsShowPrices = true;
                //do nothing because pricing are already visible.
            }
            else
            {
                ViewBag.IsShowPrices = false;
                //  cntRightPricing1.Visible = false;
            }
            if (!string.IsNullOrEmpty(StoreBaseResopnse.Currency))
            {
                ViewBag.Currency = StoreBaseResopnse.Currency;
            }
            else
            {
                ViewBag.Currency = "";
            }

            ViewBag.TaxLabel = StoreBaseResopnse.Company.TaxLabel;
            OrderDetail order = _OrderService.GetOrderReceipt(Convert.ToInt64(OrderId));

            ViewBag.Organisation = StoreBaseResopnse.Organisation;
            if (StoreBaseResopnse.Organisation.Country != null)
            {
                ViewBag.OrganisationCountryName = StoreBaseResopnse.Organisation.Country.CountryName;
            }
            else
            {
                ViewBag.OrganisationCountryName = "";
            }
            if (StoreBaseResopnse.Organisation.State != null)
            {
                ViewBag.OrganisationStateName = StoreBaseResopnse.Organisation.State.StateName;
            }
            else
            {
                ViewBag.OrganisationStateName = "";
            }
            return order;
        }
        private NABViewModel intializeModel(ErrorSummary? Message, int  OrderID)
        {
            switch (Message)
            {
                case ErrorSummary.InvalidCardTypeNumber:
                    {
                        ViewBag.ErrorMessage = Utils.GetKeyValueFromResourceFile("ltrlsorryinvalid", UserCookieManager.WBStoreId, "Sorry, your credit Card Type and Number is not valid");
                        break;
                    }
                case ErrorSummary.InvalidCardType:
                    {
                        ViewBag.ErrorMessage =
Utils.GetKeyValueFromResourceFile("lrlselectvalidcardtype", UserCookieManager.WBStoreId, "Sorry, Please Select Valid Card Type.")
;
                        break;
                    }
                case ErrorSummary.InvalidCardNumber:
                    {
                        ViewBag.ErrorMessage = Utils.GetKeyValueFromResourceFile("ltrlentervalidcardnumber", UserCookieManager.WBStoreId, "Sorry, Please Enter a Valid Card Number.");
                        break;
                    }
                case ErrorSummary.InvalidPaymentGateway:
                    {
                        ViewBag.ErrorMessage = Utils.GetKeyValueFromResourceFile("ltrlpaymentgateway", UserCookieManager.WBStoreId, "Sorry,Payment Gateway is not set");
                        break;
                    }
                case ErrorSummary.InvalidOrder:
                    {
                        ViewBag.ErrorMessage = Utils.GetKeyValueFromResourceFile("ltrlinvalidorder", UserCookieManager.WBStoreId, "Invalid Order");
                        break;
                    }
                case ErrorSummary.statusResponseMessage:
                    {
                        ViewBag.ErrorMessage = Utils.GetKeyValueFromResourceFile("ltrlstatusrespmessage", UserCookieManager.WBStoreId, "status Response Message.");
                        break;
                    }
                case ErrorSummary.Error:
                    {
                        ViewBag.ErrorMessage = Utils.GetKeyValueFromResourceFile("ltrleror", UserCookieManager.WBStoreId, "Error occurred while processing.");
                        break;
                    }
                default:
                    {
                        ViewBag.ErrorMessage = Utils.GetKeyValueFromResourceFile("ltrlnull", UserCookieManager.WBStoreId, "null");

                        break;
                    }



            }

            NABViewModel model = new NABViewModel();
            model.OrderId = OrderID;
            List<DropdownCardType> sourceOfType = new List<DropdownCardType>();
            sourceOfType.Add(new DropdownCardType
            {
                value = 0,
                Text = "Select credit card type"
            });
            sourceOfType.Add(new DropdownCardType
            {
                value = 1,
                Text = "Visa"
            });
            sourceOfType.Add(new DropdownCardType
            {
                value = 2,
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
            return model;
        }
        private string OrderXmlDataTest()
        {
            return @"<?xml version=”1.0” encoding=”UTF-8”?>
<NABTransactMessage>
<MessageInfo>
<messageID>8af793f9af34bea0cf40f5fb5c630c</messageID>
<messageTimestamp>20041803161306527000+660</messageTimestamp>
<timeoutValue>60</timeoutValue>
<apiVersion>xml-4.2</apiVersion>
</MessageInfo>
<MerchantInfo>
<merchantID>XYZ0010</merchantID>
<password>abcd1234</password>
</MerchantInfo>
<RequestType>Payment</RequestType>
<Payment>
<TxnList count=”1”>
<Txn ID=”1”>
<txnType>0</txnType>
<txnSource>0</txnSource>
<txnChannel>0</txnChannel>
<amount>1000</amount>
<purchaseOrderNo>test</purchaseOrderNo>
<CreditCardInfo>
<cardNumber>4444333322221111</cardNumber>
<expiryDate>08/14</expiryDate>
<recurringFlag>no</recurringFlag>
</CreditCardInfo>
</Txn>
</TxnList>
</Payment>
</NABTransactMessage>";
        }
        private string OrderXmlData(long orderID, string ccNumber, string ccDate, string ccYear, string OrderAmount,
            string PONumber, string marchantID, string Password)
        {
            //string CacheKeyName = "CompanyBaseResponse";
            //ObjectCache cache = MemoryCache.Default;
            //MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
            MyCompanyDomainBaseReponse StoreBaseResopnse = _CompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

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

       
       


        public enum ErrorSummary
        {
            InvalidCardTypeNumber,
            InvalidCardType,
            InvalidCardNumber,
            InvalidPaymentGateway,
            InvalidOrder,
            statusResponseMessage,
            Error,
            NoError
        }
    }
}