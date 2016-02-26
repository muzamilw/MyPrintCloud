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
using Stripe;

namespace MPC.Webstore.Controllers
{

    public class StripeGatewayController : Controller
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
        // GET: NabSubmit
        public StripeGatewayController(IPaymentGatewayService PaymentGatewayService, IOrderService OrderService, ICompanyService _CompanyService,
            ICampaignService _campaignService, IUserManagerService _usermanagerService,
            IPrePaymentService _IPrePaymentService,
            INABTransactionService _INABTransactionService,
            IWebstoreClaimsHelperService myClaimHelper, ITemplateService templateService)
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
                    Company Store = StoreBaseResopnse.Company;
                    PaymentGateway oGateWay = null;
                    if (Store.isPaymentRequired == true)
                    {
                        oGateWay = _PaymentGatewayService.GetPaymentGatewayRecord(Store.CompanyId);
                    }

                    StripeViewModel model = new StripeViewModel();

                    model.PublishableKey = oGateWay.BusinessEmail;
                    model.OrderId = OrderID;
                    Initialize(model);
                    model.OrderTotal = Utils.FormatDecimalValueToTwoDecimal(CustomerOrder.Estimate_Total);
                    model.Currency = StoreBaseResopnse.Currency;
                    return View("PartialViews/StripeGateway", model);
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
        public ActionResult Index(StripeViewModel model)
        {


            //Initialize(model);
            long StoreId = _OrderService.GetStoreIdByOrderId(model.OrderId);
          
                MyCompanyDomainBaseReponse StoreBaseResopnse = _CompanyService.GetStoreCachedObject(StoreId);

                Company Store = StoreBaseResopnse.Company;

                PaymentGateway oGateWay = null;
                if (Store.isPaymentRequired == true)
                {
                    oGateWay = _PaymentGatewayService.GetPaymentGatewayRecord(Store.CompanyId);
                }


                if (oGateWay == null)
                {
                    intializeModel(MPC.Webstore.Controllers.NabSubmitController.ErrorSummary.InvalidPaymentGateway, model.OrderId, model);
                    return View("PartialViews/StripeGateway", model);
                }
                else
                {

                    var chargeService = new StripeChargeService(oGateWay.IdentityToken);
               

                


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


                        if (model.stripeToken != string.Empty)
                        {
                            try
                            {

                             var myCharge = new StripeChargeCreateOptions();
                             myCharge.Amount = Convert.ToInt32(CustomerOrder.Estimate_Total);
                                 myCharge.Currency = _CompanyService.GetCurrencyCodeById(Convert.ToInt64(StoreBaseResopnse.Organisation.CurrencyId));

                                 myCharge.Source = new StripeSourceOptions() { TokenId = model.stripeToken };
                                 myCharge.Capture = true;
                        
                                 myCharge.Description = "Order from store";

                    StripeCharge stripeCharge = chargeService.Create(myCharge);
                               

                                if ( stripeCharge.Paid == true)
                                {

                                }


                                string orderNumber = CustomerOrder.Order_Code + "_" + DateTime.Now.Day + DateTime.Now.Minute + DateTime.Now.Second;

                                string responseFromServer = "stripe token" + model.stripeToken + " tran id=" + stripeCharge.Id + " receipt id=" + stripeCharge.ReceiptNumber;
                                _INABTransactionService.NabTransactionSaveRequest(Convert.ToInt32(model.OrderId), responseFromServer);


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
                                Campaign OnlineOrderCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.OnlineOrder, Store.OrganisationId ?? 0, Store.CompanyId);
                                cep.SalesManagerContactID = Convert.ToInt32(CustomerOrder.ContactId);
                                cep.StoreId = Store.CompanyId;
                                cep.AddressId = Convert.ToInt32(CustomerOrder.CompanyId);
                                long ManagerID = _CompanyService.GetContactIdByRole(_myClaimHelper.loginContactCompanyID(), (int)Roles.Manager);
                                cep.CorporateManagerID = ManagerID;
                                if (CustomerCompany.IsCustomer == (int)CustomerTypes.Customers) ///Retail Mode
                                {
                                    _campaignService.emailBodyGenerator(OnlineOrderCampaign, cep, CustomrContact, StoreMode.Retail, Convert.ToInt32(Store.OrganisationId), "", "", "", EmailOFSM.Email, "", "", AttachmentList);
                                    _campaignService.SendEmailToSalesManager((int)Events.NewQuoteToSalesManager, (int)CustomerOrder.ContactId, (int)CustomerOrder.CompanyId, model.OrderId, Store.OrganisationId ?? 0, 0, StoreMode.Retail, Store.CompanyId, EmailOFSM);
                                }
                                else
                                {
                                    _campaignService.emailBodyGenerator(OnlineOrderCampaign, cep, CustomrContact, StoreMode.Corp, Convert.ToInt32(Store.OrganisationId), "", "", "", EmailOFSM.Email, "", "", AttachmentList);
                                    _campaignService.SendEmailToSalesManager((int)Events.NewOrderToSalesManager, Convert.ToInt32(CustomerOrder.ContactId), Convert.ToInt32(CustomerOrder.CompanyId), model.OrderId, Store.OrganisationId ?? 0, Convert.ToInt32(ManagerID), StoreMode.Corp, Store.CompanyId, EmailOFSM);

                                }



                                _IPrePaymentService.CreatePrePaymentStripe(PaymentMethods.PayWay, model.OrderId, Convert.ToInt32(customerID), stripeCharge.ReceiptNumber, stripeCharge.Id, CustomerOrder.Estimate_Total ?? 0);
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
                                    var result = _OrderService.UpdateOrderAndCartStatus(model.OrderId, OrderStatus.PendingOrder, StoreMode.Retail, StoreBaseResopnse.Organisation, StockManagerIds, UserCookieManager.WBStoreId);
                                }
                                else
                                {
                                    var result = _OrderService.UpdateOrderAndCartStatus(model.OrderId, OrderStatus.PendingOrder, StoreMode.Corp, StoreBaseResopnse.Organisation, StockManagerIds, UserCookieManager.WBStoreId);
                                }

                                Response.Redirect("/Receipt/" + model.OrderId);
                                return null;
                            }
                            catch (Exception ex)
                            {
                                ViewBag.ErrorMessage = "Error : " + ex.ToString();

                                string virtualFolderPth = System.Web.HttpContext.Current.Server.MapPath("~/mpc_content/Exception/ErrorLog.txt");

                                using (StreamWriter writer = new StreamWriter(virtualFolderPth, true))
                                {
                                    writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                                       "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                                }

                              
                               
                            }



                        }
                        else
                        {
                            ViewBag.ErrorMessage = "missing token from stripe";
                        }




                    }
                    else
                    {
                        ViewBag.ErrorMessage = "no gateway selected";
                    }

                   
                }
                return View("PartialViews/StripeGateway", model);
            }
        
        
        private StripeViewModel intializeModel(MPC.Webstore.Controllers.NabSubmitController.ErrorSummary? Message, int OrderID, StripeViewModel model)
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

        private void Initialize(StripeViewModel model)
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