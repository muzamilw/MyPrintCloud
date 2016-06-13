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
using System.Collections;
using Qvalent.PayWay;

namespace MPC.Webstore.Controllers
{

    public class PayWayController : Controller
    {
        private readonly IPaymentGatewayService _PaymentGatewayService;
        private readonly IOrderService _OrderService;
        private readonly ICompanyService _CompanyService;
        private readonly ICampaignService _campaignService;
        private readonly IUserManagerService _usermanagerService;
        private readonly IPrePaymentService _IPrePaymentService;
        private readonly INABTransactionService _INABTransactionService;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        private readonly ITemplateService _templateService;
        private readonly MPC.Interfaces.MISServices.IOrderService _MISOrderService;
        private readonly IItemService _ItemService;
        // GET: NabSubmit
        public PayWayController(IPaymentGatewayService PaymentGatewayService, IOrderService OrderService, ICompanyService _CompanyService,
            ICampaignService _campaignService, IUserManagerService _usermanagerService,
            IPrePaymentService _IPrePaymentService,
            INABTransactionService _INABTransactionService,
            IWebstoreClaimsHelperService myClaimHelper, ITemplateService templateService
            , MPC.Interfaces.MISServices.IOrderService MISOrderService
            , IItemService ItemService)
        {
            this._PaymentGatewayService = PaymentGatewayService;
            this._OrderService = OrderService;
            this._CompanyService = _CompanyService;
            this._campaignService = _campaignService;
            this._usermanagerService = _usermanagerService;
            this._IPrePaymentService = _IPrePaymentService;
            this._INABTransactionService = _INABTransactionService;
            this._myClaimHelper = myClaimHelper;
            this._templateService = templateService;
            this._MISOrderService = MISOrderService;
            this._ItemService = ItemService;
        }
        // GET: PayWay
        public ActionResult Index(int OrderID)
        {
            Estimate CustomerOrder = null;
            if (OrderID > 0)
            {
                CustomerOrder = _OrderService.GetOrderByID(OrderID);
                if (CustomerOrder != null && CustomerOrder.StatusId == (int)OrderStatus.ShoppingCart)
                {
                    MyCompanyDomainBaseReponse StoreBaseResopnse = _CompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

                    NABViewModel model = new NABViewModel();
                    model.OrderId = OrderID;
                    Initialize(model);
                    model.OrderTotal = Utils.FormatDecimalValueToTwoDecimal(CustomerOrder.Estimate_Total);
                    model.Currency = StoreBaseResopnse.Currency;
                    return View("PartialViews/PayWay", model);
                }
                else
                {
                    Response.Redirect("/ShopCart/");
                    return null;
                }
            }
            else
            {
                Response.Redirect("/ShopCart/");
                return null;
            }

        }

        [HttpPost]
        public ActionResult Index(NABViewModel model)
        {

            int typeid = _CompanyService.GetCardTypeIdFromNumber(model.CardNumber);
            bool result = _CompanyService.IsValidNumber(model.CardNumber);


            
            if (model.SelectedCardType != typeid && result == false)
            {
                intializeModel(MPC.Webstore.Controllers.NabSubmitController.ErrorSummary.InvalidCardTypeNumber, model.OrderId, model);
                return View("PartialViews/PayWay", model);

            }
            else if (model.SelectedCardType != typeid)
            {
                intializeModel(MPC.Webstore.Controllers.NabSubmitController.ErrorSummary.InvalidCardTypeNumber, model.OrderId, model);
                return View("PartialViews/PayWay", model);
            }
            else if (result == false)
            {
                intializeModel(MPC.Webstore.Controllers.NabSubmitController.ErrorSummary.InvalidCardTypeNumber, model.OrderId, model);
                return View("PartialViews/PayWay", model);
            }
            else
            {
                Initialize(model);
                long StoreId = _OrderService.GetStoreIdByOrderId(model.OrderId);
                if (StoreId > 0)
                {
                    MyCompanyDomainBaseReponse StoreBaseResopnse = _CompanyService.GetStoreCachedObject(StoreId);

                    Company Store = StoreBaseResopnse.Company;

                    PaymentGateway oGateWay = null;
                    if (Store.isPaymentRequired == true)
                    {
                        oGateWay = _PaymentGatewayService.GetPaymentByMethodId(Store.CompanyId, (int)PaymentMethods.PayWay);
                    }


                    if (oGateWay == null)
                    {
                        intializeModel(MPC.Webstore.Controllers.NabSubmitController.ErrorSummary.InvalidPaymentGateway, model.OrderId, model);
                        return View("PartialViews/PayWay", model);
                    }
                    else
                    {
                        Estimate CustomerOrder = null;
                        if (model.OrderId > 0)
                        {
                            CustomerOrder = _OrderService.GetOrderByID(model.OrderId);
                        }
                        if (CustomerOrder != null)
                        {
                            string CERT_FILE = "D:/ccapi.q0";
                            string LOG_DIR = "D:/logDirectory";

                            String initParams =
                           @"certificateFile=" + CERT_FILE + "&" +
                           @"logDirectory=" + LOG_DIR;
                            PayWayAPI payWayAPI = new PayWayAPI();
                            payWayAPI.Initialise(initParams);


                            //----------------------------------------------------------------------------
                            // SET CONNECTION DEFAULTS
                            //----------------------------------------------------------------------------
                            string orderECI = "SSL";
                            string orderType = "capture";

                            string card_currency = _CompanyService.GetCurrencyCodeById(Convert.ToInt64(StoreBaseResopnse.Organisation.CurrencyId));
                            string orderAmountCents =
                                Convert.ToString(Convert.ToUInt64(Math.Round(
                                    Convert.ToDouble(CustomerOrder.Estimate_Total) * 100)));

                            string customerUsername = oGateWay.IdentityToken;
                            string customerPassword = oGateWay.SecureHash;
                            string customerMerchant = oGateWay.BusinessEmail;

                            // Note: you must supply a unique order number for each transaction request.
                            // We recommend that you store each transaction request in your database and
                            // that the order number is the primary key for the transaction record in that
                            // database.

                            string orderNumber = CustomerOrder.Order_Code + "_" + DateTime.Now.Day + DateTime.Now.Minute + DateTime.Now.Second;
                            string cardExpYear = model.SelectedYear.Substring(2, model.SelectedYear.Length - 2);
                            //----------------------------------------------------------------------------
                            //INITIALISE CONNECTION VARIABLES
                            //----------------------------------------------------------------------------
                            Hashtable requestParameters = new Hashtable();
                            requestParameters.Add("customer.username", customerUsername);
                            requestParameters.Add("customer.password", customerPassword);
                            requestParameters.Add("customer.merchant", customerMerchant);
                            requestParameters.Add("order.type", orderType);
                            requestParameters.Add("card.PAN", model.CardNumber);
                            requestParameters.Add("card.CVN", model.CVVNumber);
                            requestParameters.Add("card.expiryYear", cardExpYear);
                            requestParameters.Add("card.expiryMonth", model.SelectedDate);
                            requestParameters.Add("order.amount", orderAmountCents);
                            requestParameters.Add("customer.orderNumber", orderNumber);
                            requestParameters.Add("card.currency", card_currency);
                            requestParameters.Add("order.ECI", orderECI);

                            string requestText = payWayAPI.FormatRequestParameters(requestParameters);

                            string responseText = payWayAPI.ProcessCreditCard(requestText);

                            // Break the response string into its component parameters
                            IDictionary responseParameters = payWayAPI.ParseResponseParameters(responseText);

                            // Get the required parameters from the response
                            string summaryCode = (string)responseParameters["response.summaryCode"];
                            string responseCode = (string)responseParameters["response.responseCode"];
                            string description = (string)responseParameters["response.text"];
                            string receiptNo = (string)responseParameters["response.receiptNo"];
                            string settlementDate = (string)responseParameters["response.settlementDate"];
                            string creditGroup = (string)responseParameters["response.creditGroup"];
                            string previousTxn = (string)responseParameters["response.previousTxn"];
                            string cardSchemeName = (string)responseParameters["response.cardSchemeName"];

                            if (responseCode == "QH")
                            {
                                ViewBag.ErrorMessage = "Incorrect Customer Username or Password";
                            }
                            else if (responseCode == "QK")
                            {
                                ViewBag.ErrorMessage = "Unknown Customer Merchant";
                            }
                            else if (responseCode == "QJ")
                            {
                                ViewBag.ErrorMessage = "Invalid Customer Certificate";
                            }
                            else if (responseCode == "QU")
                            {
                                ViewBag.ErrorMessage = "Unknown Customer IP Address";
                            }
                            else if (responseCode == "QI")
                            {
                                ViewBag.ErrorMessage = "Transaction Incomplete";
                            }
                            else
                            {
                                long PayWayreceiptNo = 0;
                                if (!string.IsNullOrEmpty(summaryCode))
                                {
                                    string responseFromServer = "responseCode=" + responseCode + " ,summaryCode=" + summaryCode + " ,description=" + description + " ,receiptNo=" + receiptNo + " ,settlementDate=" + settlementDate + " ,creditGroup=" + creditGroup + " ,previousTxn=" + previousTxn + " ,cardSchemeName=" + cardSchemeName;
                                    _INABTransactionService.NabTransactionSaveRequest(Convert.ToInt32(model.OrderId), responseFromServer);
                                    if (summaryCode == "0")
                                    {
                                        try
                                        {

                                            int? customerID = null;
                                            StoreMode modeOfStore = StoreMode.Retail;


                                            customerID = Convert.ToInt32(CustomerOrder.ContactId);

                                            // order code and order creation date
                                            CampaignEmailParams cep = new CampaignEmailParams();
                                            string AttachmentPath = null;
                                            cep.OrganisationId = Store.OrganisationId ?? 0;
                                            cep.ContactId = CustomerOrder.ContactId ?? 0; //SessionParameters.ContactID;
                                            cep.CompanyId = CustomerOrder.CompanyId;

                                            cep.EstimateId = model.OrderId; //PageParameters.OrderID;

                                            Company CustomerCompany = _CompanyService.GetCustomer(Convert.ToInt32(CustomerOrder.CompanyId));
                                            CompanyContact CustomrContact = _CompanyService.GetContactById(Convert.ToInt32(CustomerOrder.ContactId));
                                            _OrderService.SetOrderCreationDateAndCode(model.OrderId, UserCookieManager.WEBOrganisationID);
                                            SystemUser EmailOFSM = _usermanagerService.GetSalesManagerDataByID(Store.SalesAndOrderManagerId1.Value);

                                            if (CustomerCompany.IsCustomer == (int)CustomerTypes.Corporate)
                                            {
                                                modeOfStore = StoreMode.Corp;
                                                AttachmentPath = _templateService.OrderConfirmationPDF(model.OrderId, StoreId);
                                            }
                                            else
                                            {
                                                AttachmentPath = _templateService.OrderConfirmationPDF(model.OrderId, StoreId);
                                            }
                                            List<string> AttachmentList = new List<string>();
                                            AttachmentList.Add(AttachmentPath);
                                            if (_ItemService.HasDigitalItem(CustomerOrder.EstimateId))
                                            {
                                                string HiResArtworkDownloadLink = _MISOrderService.GenerateDigitalItemsArtwork(CustomerOrder.EstimateId, Store.OrganisationId ?? 0);
                                                AttachmentList.Add(HiResArtworkDownloadLink);
                                            }
                         
                                            Campaign OnlineOrderCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.OnlineOrder, Store.OrganisationId ?? 0, Store.CompanyId);
                                            cep.SalesManagerContactID = Convert.ToInt32(CustomerOrder.ContactId);
                                            cep.StoreId = Store.CompanyId;
                                            cep.AddressId = Convert.ToInt32(CustomerOrder.CompanyId);
                                            long ManagerID = _CompanyService.GetContactIdByRole(_myClaimHelper.loginContactCompanyID(), (int)Roles.Manager);
                                            cep.CorporateManagerID = ManagerID;
                                            if (CustomerCompany.IsCustomer == (int)CustomerTypes.Customers) ///Retail Mode
                                            {
                                                _campaignService.emailBodyGenerator(OnlineOrderCampaign, cep, CustomrContact, StoreMode.Retail, Convert.ToInt32(Store.OrganisationId), "", "", "", EmailOFSM.Email, "", "", AttachmentList);
                                                _campaignService.SendEmailToSalesManager((int)Events.NewOrderToSalesManager, (int)CustomerOrder.ContactId, (int)CustomerOrder.CompanyId, model.OrderId, Store.OrganisationId ?? 0, 0, StoreMode.Retail, Store.CompanyId, EmailOFSM);
                                            }
                                            else
                                            {
                                                _campaignService.emailBodyGenerator(OnlineOrderCampaign, cep, CustomrContact, StoreMode.Corp, Convert.ToInt32(Store.OrganisationId), "", "", "", EmailOFSM.Email, "", "", AttachmentList);
                                                _campaignService.SendEmailToSalesManager((int)Events.NewOrderToSalesManager, Convert.ToInt32(CustomerOrder.ContactId), Convert.ToInt32(CustomerOrder.CompanyId), model.OrderId, Store.OrganisationId ?? 0, Convert.ToInt32(ManagerID), StoreMode.Corp, Store.CompanyId, EmailOFSM);

                                            }

                                            if (!string.IsNullOrEmpty(receiptNo))
                                            {
                                                PayWayreceiptNo = Convert.ToInt64(receiptNo);
                                            }
                                            if (string.IsNullOrEmpty(previousTxn))
                                            {
                                                previousTxn = "";
                                            }
                                            _IPrePaymentService.CreatePrePaymentPayWay(PaymentMethods.PayWay, model.OrderId, Convert.ToInt32(customerID), PayWayreceiptNo, previousTxn, CustomerOrder.Estimate_Total ?? 0);
                                            List<Guid> StockManagerIds = new List<Guid>();
                                            if (Store.StockNotificationManagerId1 != null)
                                            {
                                                StockManagerIds.Add((Guid)Store.StockNotificationManagerId1);
                                            }
                                            if (Store.StockNotificationManagerId2 != null)
                                            {
                                                StockManagerIds.Add((Guid)Store.StockNotificationManagerId2);
                                            }
                                            if (CustomerCompany.IsCustomer == (int)CustomerTypes.Customers) ///Retail Mode
                                            {
                                                result = _OrderService.UpdateOrderAndCartStatus(model.OrderId, OrderStatus.PendingOrder, StoreMode.Retail, StoreBaseResopnse.Organisation, StockManagerIds, UserCookieManager.WBStoreId);
                                            }
                                            else
                                            {
                                                result = _OrderService.UpdateOrderAndCartStatus(model.OrderId, OrderStatus.PendingOrder, StoreMode.Corp, StoreBaseResopnse.Organisation, StockManagerIds, UserCookieManager.WBStoreId);
                                            }

                                            Response.Redirect("/Receipt/" + model.OrderId);
                                            return null;
                                        }
                                        catch (Exception ex)
                                        {
                                            string virtualFolderPth = System.Web.HttpContext.Current.Server.MapPath("~/mpc_content/Exception/ErrorLog.txt");

                                            using (StreamWriter writer = new StreamWriter(virtualFolderPth, true))
                                            {
                                                writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                                                   "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                                                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                                            }
                                            return null;
                                        }
                                    }
                                    else if (summaryCode == "1")
                                    {
                                        ViewBag.ErrorMessage = "Sorry, the transaction is declined.(" + summaryCode + "). " + description;
                                    }
                                    else if (summaryCode == "2")
                                    {
                                        ViewBag.ErrorMessage = "Sorry, the transaction is declined.(" + summaryCode + "). " + description;
                                    }
                                    else if (summaryCode == "3")
                                    {
                                        ViewBag.ErrorMessage = "Sorry, the transaction is rejected.(" + summaryCode + "). " + description;
                                    }
                                }
                                else
                                {
                                    ViewBag.ErrorMessage = description;
                                }
                            }
                        }
                    }
                }


                return View("PartialViews/PayWay", model);
            }
        }
        private NABViewModel intializeModel(MPC.Webstore.Controllers.NabSubmitController.ErrorSummary? Message, int OrderID, NABViewModel model)
        {

            switch (Message)
            {
                case MPC.Webstore.Controllers.NabSubmitController.ErrorSummary.InvalidCardTypeNumber:
                    {
                        ViewBag.ErrorMessage = Utils.GetKeyValueFromResourceFile("ltrlsorryinvalid", UserCookieManager.WBStoreId, "Sorry, your credit Card Type and Number is not valid");
                        break;
                    }
                case MPC.Webstore.Controllers.NabSubmitController.ErrorSummary.InvalidCardType:
                    {
                        ViewBag.ErrorMessage = Utils.GetKeyValueFromResourceFile("lrlselectvalidcardtype", UserCookieManager.WBStoreId, "Sorry, Please Select Valid Card Type.");
                        break;
                    }
                case MPC.Webstore.Controllers.NabSubmitController.ErrorSummary.InvalidCardNumber:
                    {
                        ViewBag.ErrorMessage = Utils.GetKeyValueFromResourceFile("ltrlentervalidcardnumber", UserCookieManager.WBStoreId, "Sorry, Please Enter a Valid Card Number.");
                        break;
                    }
                case MPC.Webstore.Controllers.NabSubmitController.ErrorSummary.InvalidPaymentGateway:
                    {
                        ViewBag.ErrorMessage = Utils.GetKeyValueFromResourceFile("ltrlpaymentgateway", UserCookieManager.WBStoreId, "Sorry,Payment Gateway is not set");
                        break;
                    }
                case MPC.Webstore.Controllers.NabSubmitController.ErrorSummary.InvalidOrder:
                    {
                        ViewBag.ErrorMessage = Utils.GetKeyValueFromResourceFile("ltrlinvalidorder", UserCookieManager.WBStoreId, "Invalid Order");
                        break;
                    }
                case MPC.Webstore.Controllers.NabSubmitController.ErrorSummary.statusResponseMessage:
                    {
                        ViewBag.ErrorMessage = Utils.GetKeyValueFromResourceFile("ltrlstatusrespmessage", UserCookieManager.WBStoreId, "status Response Message.");
                        break;
                    }
                case MPC.Webstore.Controllers.NabSubmitController.ErrorSummary.Error:
                    {
                        ViewBag.ErrorMessage = Utils.GetKeyValueFromResourceFile("ltrleror", UserCookieManager.WBStoreId, "Error occurred while processing.");
                        break;
                    }
                case MPC.Webstore.Controllers.NabSubmitController.ErrorSummary.NoError:
                    {
                        ViewBag.ErrorMessage = "";
                        break;
                    }
                default:
                    {
                        ViewBag.ErrorMessage = Utils.GetKeyValueFromResourceFile("ltrlnull", UserCookieManager.WBStoreId, "null");

                        break;
                    }



            }
            model.OrderId = OrderID;

            Initialize(model);
            return model;
        }

        private void Initialize(NABViewModel model)
        {

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
            //sourceOfType.Add(new DropdownCardType
            //{
            //    value = 3,
            //    Text = "Diners"
            //});
            sourceOfType.Add(new DropdownCardType
            {
                value = 4,
                Text = "Amex"
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
        }

    }
}