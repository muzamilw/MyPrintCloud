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
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;

            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
           // MyCompanyDomainBaseResponse baseResponseCompany = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();
          
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
                    return View("PartialViews/OrderConfirmation", shopCart);
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
            if (buttonType == "1")
            {
                PlaceOrder(1, Convert.ToInt64(OrderId));
            }
            else 
            {
                if (_myClaimHelper.loginContactRoleID() == (int)Roles.Adminstrator || _myClaimHelper.loginContactRoleID() == (int)Roles.Manager)
                {
                    PlaceOrder(3, Convert.ToInt64(OrderId));
                }
                else
                {
                    PlaceOrder(2, Convert.ToInt64(OrderId));
                }
            }
            
            
            return null;
        }

        private void PlaceOrder(int modOverride, long OrderId)
        {
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
                            // Only for demo mode.
                            result = _OrderService.UpdateOrderAndCartStatus(OrderId, OrderStatus.PendingOrder, StoreMode.Retail);
                            Estimate updatedOrder = _OrderService.GetOrderByID(OrderId);

                            string AttachmentPath = "";//emailmgr.OrderConfirmationPDF(OrderId, 0, 0);
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
                            //MessgeToDisply.Visible = true;
                            //MessgeToDisply.Style.Add("border", "1px solid red");
                            //MessgeToDisply.Style.Add("font-size", "20px");
                            //MessgeToDisply.Style.Add("font-weight", "bold");
                            //MessgeToDisply.Style.Add("text-align", "left");
                            //MessgeToDisply.Style.Add("color", "red");
                            //MessgeToDisply.Style.Add("padding", "20px");
                            //ltrlMessge.Text = "Error occurred while processing order.";
                            //////LogError(ex);
                        }
                        if (result)
                        {
                            Response.Redirect("/Receipt/" + OrderId);

                        }
                    }
                    else // online payments enabled
                    {

                        if (oPaymentGateWay == null)
                        {
                            //MessgeToDisply.Visible = true;
                            //MessgeToDisply.Style.Add("border", "1px solid red");
                            //MessgeToDisply.Style.Add("font-size", "20px");
                            //MessgeToDisply.Style.Add("font-weight", "bold");
                            //MessgeToDisply.Style.Add("text-align", "left");
                            //MessgeToDisply.Style.Add("color", "red");
                            //MessgeToDisply.Style.Add("padding", "20px");
                            //ltrlMessge.Text = "Payment Gatway is not set. Please contact your admin.";
                        }
                        else
                        {
                          
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
                                            Response.Redirect("payments/NabSubmit/" + OrderId);
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
                           
                        }
                    }
                }
                else if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                {
                    cep.StoreId = UserCookieManager.WBStoreId;

                    cep.AddressId = UserCookieManager.WBStoreId;
                    SystemUser EmailOFSM = _userManagerService.GetSalesManagerDataByID(baseResponse.Company.SalesAndOrderManagerId1.Value);
                    cep.SystemUserId = EmailOFSM.SystemUserId;
                    if (((user.ContactRoleId == Convert.ToInt32(Roles.Adminstrator) || user.ContactRoleId == Convert.ToInt32(Roles.Manager)) && ((user.IsPayByPersonalCreditCard ?? false) == false)) || (modOverride == 3) || (user.ContactRoleId == Convert.ToInt32(Roles.User) && user.canUserPlaceOrderWithoutApproval == true && modOverride == 2) || (user.ContactRoleId == Convert.ToInt32(Roles.User) && user.canUserPlaceOrderWithoutApproval == true && user.IsPayByPersonalCreditCard == false)) // Corporate user that can approve the orders
                    {
                        try
                        {
                            result = _OrderService.UpdateOrderAndCartStatus(OrderId, OrderStatus.PendingOrder, StoreMode.Corp);

                            long ManagerID = _myCompanyService.GetContactIdByRole(_myClaimHelper.loginContactCompanyID(), (int)Roles.Manager); //ContactManager.GetBrokerByRole(SessionParameters.BrokerContactCompany.ContactCompanyID, Convert.ToInt32(Roles.Adminstrator));
                            cep.CorporateManagerID = ManagerID;
                            string AttachmentPath = "";//emailmgr.OrderConfirmationPDF(OrderId, 0, SessionParameters.CustomerContact.ContactID);
                            List<string> AttachmentList = new List<string>();
                            AttachmentList.Add(AttachmentPath);
                            //_myCampaignService.emailBodyGenerator(OnlineOrderCampaign, baseResponseOrganisation, cep, user, StoreMode.Corp, "", HTMLOfShopReceipt, "", EmailOFSM.Email, "", "", AttachmentList);
                           // emailmgr.SendEmailToSalesManager((int)EmailEvents.NewOrderToSalesManager, SessionParameters.ContactID, SessionParameters.CustomerID, 0, OrderId, SessionParameters.CompanySite, 0, ManagerID, StoreMode.Corp);
                           UserCookieManager.WEBOrderId = 0;
                        }
                        catch (Exception ex)
                        {
                            //MessgeToDisply.Visible = true;
                            //MessgeToDisply.Style.Add("border", "1px solid red");
                            //MessgeToDisply.Style.Add("font-size", "20px");
                            //MessgeToDisply.Style.Add("font-weight", "bold");
                            //MessgeToDisply.Style.Add("text-align", "left");
                            //MessgeToDisply.Style.Add("color", "red");
                            //MessgeToDisply.Style.Add("padding", "20px");
                            //ltrlMessge.Text = "Error occurred while processing order.";
                            //LogError(ex);
                        }

                        Response.Redirect("/Receipt/" + OrderId);


                    }
                    else if (((user.IsPayByPersonalCreditCard ?? false) == false) || (modOverride == 2)) //user.IsPayByPersonalCreditCard ?? false) == false || CanShowPrices == false -- this condition is changed) Corporate user that can't pay and he is not an approver
                    {
                        // and prices are hidden
                        try
                        {
                            result = _OrderService.UpdateOrderAndCartStatus(OrderId, OrderStatus.PendingCorporateApprovel, StoreMode.Corp);

                            long ManagerID = _myCompanyService.GetContactIdByRole(_myClaimHelper.loginContactCompanyID(), (int)Roles.Manager);
                            cep.CorporateManagerID = ManagerID;
                            string AttachmentPath = "";//emailmgr.OrderConfirmationPDF(OrderId, 0, SessionParameters.CustomerContact.ContactID);
                            List<string> AttachmentList = new List<string>();
                            AttachmentList.Add(AttachmentPath);
                            //_myCampaignService.emailBodyGenerator(OnlineOrderCampaign, baseResponseOrganisation.Organisation, cep, user, StoreMode.Corp, "", HTMLOfShopReceipt, "", EmailOFSM.Email, "", "", AttachmentList);
                            //emailmgr.EmailsToCorpUser(OrderId, SessionParameters.ContactID, StoreMode.Corp, Convert.ToInt32(SessionParameters.CustomerContact.TerritoryID));
                            UserCookieManager.WEBOrderId = 0;
                        }
                        catch (Exception ex)
                        {
                            //MessgeToDisply.Visible = true;
                            //MessgeToDisply.Style.Add("border", "1px solid red");
                            //MessgeToDisply.Style.Add("font-size", "20px");
                            //MessgeToDisply.Style.Add("font-weight", "bold");
                            //MessgeToDisply.Style.Add("text-align", "left");
                            //MessgeToDisply.Style.Add("color", "red");
                            //MessgeToDisply.Style.Add("padding", "20px");
                            //ltrlMessge.Text = "Error occurred while processing order.";
                            //LogError(ex);
                        }

                        Response.Redirect("/Receipt/" + OrderId);


                    }
                    else
                    {
                       
                        if (oPaymentGateWay == null)
                        {
                            //MessgeToDisply.Visible = true;
                            //MessgeToDisply.Style.Add("border", "1px solid red");
                            //MessgeToDisply.Style.Add("font-size", "20px");
                            //MessgeToDisply.Style.Add("font-weight", "bold");
                            //MessgeToDisply.Style.Add("text-align", "left");
                            //MessgeToDisply.Style.Add("color", "red");
                            //MessgeToDisply.Style.Add("padding", "20px");
                            //ltrlMessge.Text = "Payment Gatway is not set. Please contact your admin.";
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

                        }
                    }
                }
            }
            else
            {
                Response.Redirect("/");
            }
        }

        public string GetReceiptPage(long OrderId)
        {
            try
            {
                string URl = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority + "/Receipt?OrderId=" + OrderId;
                WebClient myClient = new WebClient();
                Stream response = myClient.OpenRead(URl);
                StreamReader streamreader = new StreamReader(response);
                string pageHtml = streamreader.ReadToEnd();
                return pageHtml;
            }
            catch (Exception ex)
            {

                // LoggingManager.LogBLLException(e);
                return null;
            }
        }
        //public string OrderConfirmationPDF(int OrderId)
        //{
        //    try
        //    {
        //        string URl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/Receipt?OrderId=" + OrderId;
        //        // string html = GetShopReceiptPage(OrderId, BrokerID, CorpID);
        //        ////Stream stream = GenerateStreamFromString(html)



        //        string FileName = OrderId + "_OrderReceipt.pdf";
        //        string FilePath = HttpContext.Current.Server.MapPath("~/mpc_content/Assets/" + FileName);
        //        string AttachmentPath = "/mpc_content/Assets/" + FileName;
        //        using (Doc theDoc = new Doc())
        //        {
        //            theDoc.HtmlOptions.Engine = EngineType.Gecko;
        //            //  theDoc.FontSize = 22;
        //            int objid = theDoc.AddImageUrl(URl);


        //            while (true)
        //            {
        //                theDoc.FrameRect();
        //                if (!theDoc.Chainable(objid))
        //                    break;
        //                theDoc.Page = theDoc.AddPage();
        //                objid = theDoc.AddImageToChain(objid);
        //            }


        //            theDoc.Save(FilePath);
        //            theDoc.Clear();
        //        }
        //        if (File.Exists(FilePath))
        //            return AttachmentPath;
        //        else
        //            return null;
        //    }
        //    catch (Exception e)
        //    {
        //        LoggingManager.LogBLLException(e);
        //        return null;
        //    }
        //}
    }
}