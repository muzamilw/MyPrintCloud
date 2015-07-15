using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using MPC.Webstore.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Webstore.ModelMappers;
using System.Runtime.Caching;
using System.Net;
using System.IO;
using WebSupergoo.ABCpdf8;
using System.Configuration;
namespace MPC.Webstore.Controllers
{
    public class OrderConfirmationController : Controller
    {
        private readonly IOrderService _OrderService;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        private readonly IItemService _ItemService;
        private readonly ICompanyService _myCompanyService;
        private readonly ICampaignService _myCampaignService;
        private readonly IUserManagerService _userManagerService;
        private readonly ICampaignService _campaignService;
        public OrderConfirmationController(IOrderService OrderService, IWebstoreClaimsHelperService myClaimHelper, ICompanyService myCompanyService, IItemService ItemService
            , ICampaignService myCampaignService, IUserManagerService userManagerService, ICampaignService campaignService)
        {
            if (OrderService == null)
            {
                throw new ArgumentNullException("OrderService");
            }
            this._OrderService = OrderService;
            this._myClaimHelper = myClaimHelper;
            this._myCompanyService = myCompanyService;
            this._ItemService = ItemService;
            this._myCampaignService = myCampaignService;
            this._userManagerService = userManagerService;
            this._campaignService = campaignService;
        }
        // GET: OrderConfirmation
        public ActionResult Index(string OrderId)
        {
            ShoppingCart shopCart = LoadOrderDetail(OrderId);
            return View("PartialViews/OrderConfirmation", shopCart);

        }

        /// <summary>
        /// 1 type : place order, Submit for Apprval, Pay by personal credit card
        /// 2 type : direct deposite
        /// 
        /// </summary>
        /// <param name="btnDirectDeposit"></param>
        /// <param name="btnPlaceOrderUsingCreditCard"></param>
        /// <param name="btnPlaceOrder"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string buttonType, string OrderId)
        {
            ShoppingCart shopCart = null;
            if (buttonType == "1")
            {
                shopCart = PlaceOrder(1, Convert.ToInt64(OrderId));
           
            }
            else
            {
                if (_myClaimHelper.loginContactRoleID() == (int)Roles.Adminstrator || _myClaimHelper.loginContactRoleID() == (int)Roles.Manager)
                {
                    shopCart = PlaceOrder(3, Convert.ToInt64(OrderId));
                }
                else
                {
                    shopCart = PlaceOrder(2, Convert.ToInt64(OrderId));
                }
            }
            if (shopCart != null)
            {
                return View("PartialViews/OrderConfirmation", shopCart);
            }
            else
            {
                return null;
            }

        }

        private ShoppingCart PlaceOrder(int modOverride, long OrderId)
        {
            ShoppingCart shopCart = null;
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;
            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse baseResponse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];


            bool result = false;

            PaymentGateway oPaymentGateWay = _ItemService.GetPaymentGatewayRecord(UserCookieManager.WBStoreId);

            CompanyContact user = _myCompanyService.GetContactByID(_myClaimHelper.loginContactID()); //LoginUser;

            CampaignEmailParams cep = new CampaignEmailParams();
            //    PageManager pageMgr = new PageManager();
            string HTMLOfShopReceipt = null;
            cep.ContactId = _myClaimHelper.loginContactID();
            cep.CompanyId = _myClaimHelper.loginContactCompanyID();
            cep.SalesManagerContactID = _myClaimHelper.loginContactID();
            cep.OrganisationId = UserCookieManager.WEBOrganisationID;
            cep.EstimateId = Convert.ToInt32(OrderId);
            cep.ItemId = _ItemService.GetFirstItemIdByOrderId(OrderId);
            Campaign OnlineOrderCampaign = _myCampaignService.GetCampaignRecordByEmailEvent((int)Events.OnlineOrder, baseResponse.Company.OrganisationId ?? 0, UserCookieManager.WBStoreId);
            List<Guid> StockManagerIds = new List<Guid>();
            if(baseResponse.Company.StockNotificationManagerId1 != null)
            {
                StockManagerIds.Add((Guid)baseResponse.Company.StockNotificationManagerId1);
            }
            if(baseResponse.Company.StockNotificationManagerId2 != null)
            {
                StockManagerIds.Add((Guid)baseResponse.Company.StockNotificationManagerId2);
            }
            if (user != null)
            {

                if (UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)
                {
                    cep.StoreId = UserCookieManager.WBStoreId;
                    cep.AddressId = UserCookieManager.WBStoreId;
                    if (baseResponse.Company.isPaymentRequired == false || baseResponse.Company.isPaymentRequired == null)
                    {
                        try
                        {
                            result = _OrderService.UpdateOrderAndCartStatus(OrderId, OrderStatus.PendingOrder, StoreMode.Retail, baseResponse.Organisation, StockManagerIds, UserCookieManager.WBStoreId);
                            Estimate updatedOrder = _OrderService.GetOrderByID(OrderId);

                            string AttachmentPath = OrderConfirmationPDF(OrderId, UserCookieManager.WBStoreId);
                            List<string> AttachmentList = new List<string>();
                            AttachmentList.Add(AttachmentPath);
                            SystemUser EmailOFSM = _userManagerService.GetSalesManagerDataByID(baseResponse.Company.SalesAndOrderManagerId1.Value);
                            // HTMLOfShopReceipt = GetReceiptPage(OrderId);
                            _myCampaignService.emailBodyGenerator(OnlineOrderCampaign, cep, user, (StoreMode)UserCookieManager.WEBStoreMode, Convert.ToInt32(baseResponse.Organisation.OrganisationId), "", HTMLOfShopReceipt, "", EmailOFSM.Email, "", "", AttachmentList);
                            _campaignService.SendEmailToSalesManager((int)Events.NewOrderToSalesManager, _myClaimHelper.loginContactID(), _myClaimHelper.loginContactCompanyID(), OrderId, UserCookieManager.WEBOrganisationID, 0, StoreMode.Retail, UserCookieManager.WBStoreId, EmailOFSM);
                            UserCookieManager.WEBOrderId = 0;

                            // For demo mode as enter the pre payment with the known parameters
                            PrePayment tblPrePayment = new PrePayment()
                            {
                                Amount = updatedOrder.Estimate_Total,
                                CustomerId = Convert.ToInt32(_myClaimHelper.loginContactCompanyID()),
                                OrderId = OrderId,
                                PaymentDate = DateTime.Now,
                                PaymentMethodId = 99, //(int)PaymentMethod.Cash,
                                ReferenceCode = updatedOrder.CustomerPO
                            };

                            _ItemService.AddPrePayment(tblPrePayment);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                            return null;
                        }
                        if (result)
                        {
                            Response.Redirect("/Receipt/" + OrderId.ToString());
                            return null;
                        }
                    }
                    else // online payments enabled
                    {

                        if (oPaymentGateWay == null)
                        {
                            shopCart = LoadOrderDetail(OrderId.ToString());
                            ViewBag.Message = "Payment Gateway is not set.";
                            return shopCart;
                        }
                        else
                        {

                            switch (oPaymentGateWay.PaymentMethodId)
                            {
                                case 1: //PayPal
                                    {
                                        Response.Redirect("/SignupPaypal/" + OrderId);
                                        break;
                                    }

                                case 2:
                                    {
                                        Response.Redirect("/payments/paymentAuthorizeNet/" + OrderId);
                                        break;
                                    }
                                case 3:
                                    {
                                        Response.Redirect("/payments/ANZSubmit/" + OrderId);
                                        break;
                                    }
                                case 4:
                                    {
                                        Response.Redirect("/paymentAuthorizeNet/" + OrderId);
                                        break;
                                    }
                                case 5:
                                    {
                                        Response.Redirect("/payments/stGeorgeSubmit/" + OrderId);
                                        break;
                                    }
                                case 6:
                                    {
                                        Response.Redirect("/payments/NabSubmit/" + OrderId);
                                        break;
                                    }
                                case 7:
                                    {
                                        Response.Redirect("/payments/PayJunctionSubmit/" + OrderId);
                                        break;

                                    }
                                default:
                                    break;
                            }
                            return null;
                        }
                    }
                }
                else if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                {
                    cep.StoreId = UserCookieManager.WBStoreId;

                    cep.AddressId = UserCookieManager.WBStoreId;
                    SystemUser EmailOFSM = _userManagerService.GetSalesManagerDataByID(baseResponse.Company.SalesAndOrderManagerId1.Value);
                    cep.SystemUserId = EmailOFSM.SystemUserId;
                   
                    if (((user.ContactRoleId == Convert.ToInt32(Roles.Adminstrator) || user.ContactRoleId == Convert.ToInt32(Roles.Manager)) && ((user.IsPayByPersonalCreditCard ?? false) == false)) || (modOverride == 3) || (user.ContactRoleId == Convert.ToInt32(Roles.User) && user.canUserPlaceOrderWithoutApproval == true && modOverride == 2) || (user.ContactRoleId == Convert.ToInt32(Roles.User) && user.canUserPlaceOrderWithoutApproval == true && (user.IsPayByPersonalCreditCard == false || user.IsPayByPersonalCreditCard == null))) // Corporate user that can approve the orders
                    {
                        try
                        {
                            result = _OrderService.UpdateOrderAndCartStatus(OrderId, OrderStatus.PendingOrder, StoreMode.Corp, baseResponse.Organisation, StockManagerIds, UserCookieManager.WBStoreId);

                            long ManagerID = _myCompanyService.GetContactIdByRole(_myClaimHelper.loginContactCompanyID(), (int)Roles.Manager); //ContactManager.GetBrokerByRole(SessionParameters.BrokerContactCompany.ContactCompanyID, Convert.ToInt32(Roles.Adminstrator));
                            cep.CorporateManagerID = ManagerID;
                            string AttachmentPath = OrderConfirmationPDF(OrderId, UserCookieManager.WBStoreId);
                            List<string> AttachmentList = new List<string>();
                            AttachmentList.Add(AttachmentPath);
                            _myCampaignService.emailBodyGenerator(OnlineOrderCampaign, cep, user, (StoreMode)UserCookieManager.WEBStoreMode, Convert.ToInt32(baseResponse.Organisation.OrganisationId), "", HTMLOfShopReceipt, "", EmailOFSM.Email, "", "", AttachmentList);
                            _campaignService.SendEmailToSalesManager((int)Events.NewOrderToSalesManager, _myClaimHelper.loginContactID(), _myClaimHelper.loginContactCompanyID(), OrderId, UserCookieManager.WEBOrganisationID, (int)ManagerID, StoreMode.Retail, UserCookieManager.WBStoreId, EmailOFSM);
                            UserCookieManager.WEBOrderId = 0;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        Response.Redirect("/Receipt/" + OrderId);
                        return null;

                    }
                    else if (((user.IsPayByPersonalCreditCard ?? false) == false) || (modOverride == 2)) //user.IsPayByPersonalCreditCard ?? false) == false || CanShowPrices == false -- this condition is changed) Corporate user that can't pay and he is not an approver
                    {
                        // and prices are hidden
                        try
                        {
                            result = _OrderService.UpdateOrderAndCartStatus(OrderId, OrderStatus.PendingCorporateApprovel, StoreMode.Corp, baseResponse.Organisation, StockManagerIds, UserCookieManager.WBStoreId);

                            long ManagerID = _myCompanyService.GetContactIdByRole(_myClaimHelper.loginContactCompanyID(), (int)Roles.Manager);
                            cep.CorporateManagerID = ManagerID;
                            string AttachmentPath = OrderConfirmationPDF(OrderId, UserCookieManager.WBStoreId);
                            List<string> AttachmentList = new List<string>();
                            AttachmentList.Add(AttachmentPath);
                            _myCampaignService.emailBodyGenerator(OnlineOrderCampaign, cep, user, (StoreMode)UserCookieManager.WEBStoreMode, Convert.ToInt32(baseResponse.Organisation.OrganisationId), "", HTMLOfShopReceipt, "", EmailOFSM.Email, "", "", AttachmentList);
                            _campaignService.EmailsToCorpUser(OrderId, _myClaimHelper.loginContactID(), StoreMode.Corp, _myClaimHelper.loginContactTerritoryID(), baseResponse.Organisation, UserCookieManager.WBStoreId, EmailOFSM.Email);
                            UserCookieManager.WEBOrderId = 0;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        Response.Redirect("/Receipt/" + OrderId);
                        return null;

                    }
                    else
                    {

                        if (oPaymentGateWay == null)
                        {
                            shopCart = LoadOrderDetail(OrderId.ToString());
                            ViewBag.Message = "Payment Gateway is not set.";
                            return shopCart;
                        }
                        else
                        {
                            //selection of payment gateways happening here
                            switch (oPaymentGateWay.PaymentMethodId)
                            {
                                case 1: //PayPal
                                    {
                                        Response.Redirect("SignupPaypal/" + OrderId);
                                        break;
                                    }

                                case 2:
                                    {
                                        Response.Redirect("payments/paymentAuthorizeNet/" + OrderId);
                                        break;
                                    }
                                case 3:
                                    {
                                        Response.Redirect("payments/ANZSubmit/" + OrderId);
                                        break;
                                    }
                                case 4:
                                    {
                                        Response.Redirect("paymentAuthorizeNet/" + OrderId);
                                        break;
                                    }
                                case 5:
                                    {
                                        Response.Redirect("payments/stGeorgeSubmit/" + OrderId);
                                        break;
                                    }
                                case 6:
                                    {
                                        Response.Redirect("/NabSubmit/" + OrderId);

                                        break;
                                    }
                                case 7:
                                    {
                                        Response.Redirect("payments/PayJunctionSubmit/" + OrderId);
                                        break;

                                    }
                                default:
                                    break;
                            }
                            return null;
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("/");
                return null;
            }
            return shopCart;
        }


        public string OrderConfirmationPDF(long OrderId, long StoreId)
        {
            //try
            //{
            //    string URl = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority + "/ReceiptPlain?OrderId=" + OrderId + "&StoreId=" + StoreId + "&IsPrintReceipt=0";

            //    string FileName = OrderId + "_OrderReceipt.pdf";
            //    string FilePath = System.Web.HttpContext.Current.Server.MapPath("~/mpc_content/EmailAttachments/" + FileName);
            //    string AttachmentPath = "/mpc_content/EmailAttachments/" + FileName;
            //    using (Doc theDoc = new Doc())
            //    {
            //        string AddGeckoKey = ConfigurationManager.AppSettings["AddEngineTypeGecko"];
            //        if (AddGeckoKey == "1")
            //        {
            //            theDoc.HtmlOptions.Engine = EngineType.Gecko;
            //        }
                   
            //        theDoc.FontSize = 22;
            //        int objid = theDoc.AddImageUrl(URl);


            //        while (true)
            //        {
            //            theDoc.FrameRect();
            //            if (!theDoc.Chainable(objid))
            //                break;
            //            theDoc.Page = theDoc.AddPage();
            //            objid = theDoc.AddImageToChain(objid);
            //        }
            //        string physicalFolderPath = System.Web.HttpContext.Current.Server.MapPath("~/mpc_content/EmailAttachments/");
            //        if (!Directory.Exists(physicalFolderPath))
            //            Directory.CreateDirectory(physicalFolderPath);
            //        theDoc.Save(FilePath);
            //        theDoc.Clear();
            //    }
            //    if (System.IO.File.Exists(FilePath))
            //        return AttachmentPath;
            //    else
            //        return null;
            //}
            //catch (Exception e)
            //{
              
            //    //   LoggingManager.LogBLLException(e);
            //   // string FilePath = System.Web.HttpContext.Current.Server.MapPath("~/mpc_content/EmailAttachments/exe.txt" );
            //   // System.IO.File.WriteAllText(FilePath, e.InnerException.ToString() + "\n" + e.StackTrace.ToString());
            //  throw e;
            //  return null;
            //}
            return null;
        }

        private ShoppingCart LoadOrderDetail(string OrderId)
        {
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;

            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];

            long OrderID = Convert.ToInt64(OrderId);
            if (OrderID > 0)
            {
                ShoppingCart shopCart = _OrderService.GetShopCartOrderAndDetails(OrderID, OrderStatus.ShoppingCart);
                if (shopCart != null)
                {
                    long CID = _myClaimHelper.loginContactID();
                    CompanyContact oContact = _myCompanyService.GetContactByID(CID);
                    ViewBag.LoginUser = oContact;
                    if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                    {


                        ViewData["OrderAddresses"] = _myCompanyService.GetContactCompanyAddressesList(shopCart.BillingAddressID, shopCart.ShippingAddressID, oContact.AddressId);
                    }
                    else
                    {
                        ViewData["OrderAddresses"] = _myCompanyService.GetContactCompanyAddressesList(shopCart.BillingAddressID, shopCart.ShippingAddressID, 0);

                    }

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

                    ViewBag.Currency = StoreBaseResopnse.Currency;
                    ViewBag.TaxLabel = StoreBaseResopnse.Company.TaxLabel;
                    StoreBaseResopnse = null;
                    return shopCart;
                }
                else
                {
                    Response.Redirect("/");
                    return null;
                }
            }
            else
            {
                Response.Redirect("/");
                return null;
            }
        }
    }
}