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
using MPC.Models.ResponseModels;
using System.Web.UI;
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
        private readonly ITemplateService _templateService;
        public OrderConfirmationController(IOrderService OrderService, IWebstoreClaimsHelperService myClaimHelper, ICompanyService myCompanyService, IItemService ItemService
            , ICampaignService myCampaignService, IUserManagerService userManagerService, ICampaignService campaignService
            , ITemplateService templateService)
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
            this._templateService = templateService;
        }
        // GET: OrderConfirmation
        public ActionResult Index(string OrderId)
        {
            ShoppingCart shopCart = LoadOrderDetail(OrderId);
            if (shopCart == null)
            {
                ControllerContext.HttpContext.Response.RedirectToRoute("ShopCart");
                return null;
            }
            else 
            {
                return View("PartialViews/OrderConfirmation", shopCart);

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
            try
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
            catch (Exception ex) 
            {
                throw ex;
                return null;
            }
            

        }

        private ShoppingCart PlaceOrder(int modOverride, long OrderId)
        {
            string ItemTypeFourHtml = string.Empty;
            string URl = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority;
            List<Item> GetAllItems = _ItemService.GetItemsByOrderID(OrderId);
            GetAllItems = GetAllItems.Where(i => i.IsOrderedItem == true && i.ProductType == 4).ToList();
            CampaignEmailParams cep = new CampaignEmailParams();
            if (GetAllItems != null)
            {
                cep.AssetId = 1;
              
                StringWriter stringWriter = new StringWriter();


                using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
                {
                    string clearboth = "clearBoth";
                    string FloatLeft = "float_left";
                    string FloatRight = "float_right";
                    string fullWidth = "Width100Percent";
                    string halfwidth = "width50p";
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, fullWidth); //Main Div
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);//Main Div



                    writer.AddAttribute(HtmlTextWriterAttribute.Class, fullWidth);//AssetsDiv
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);

                    foreach (var item in GetAllItems)
                    {

                        Asset ParentAsset = _myCompanyService.GetAsset(Convert.ToInt64(item.RefItemId));

                        if(ParentAsset != null)
                        {

                            List<AssetItem> AssetItems = _myCompanyService.GetAssetItemsByAssetID(ParentAsset.AssetId);

                            writer.AddAttribute(HtmlTextWriterAttribute.Class, fullWidth);//AssetNameDiv
                            writer.RenderBeginTag(HtmlTextWriterTag.Div);
                            writer.Write(ParentAsset.AssetName);
                            writer.RenderEndTag();
                            foreach (var AssetItem in AssetItems)
                            {
                                writer.AddAttribute(HtmlTextWriterAttribute.Class, fullWidth);//AssetDetailsDiv
                                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                                writer.AddAttribute(HtmlTextWriterAttribute.Class, halfwidth);//AssetNameDiv

                                writer.AddAttribute(HtmlTextWriterAttribute.Class, halfwidth);//AssetDownloadDiv
                                writer.AddAttribute(HtmlTextWriterAttribute.Class, FloatLeft);
                                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                                writer.AddAttribute(HtmlTextWriterAttribute.Class, FloatLeft);
                                writer.AddAttribute(HtmlTextWriterAttribute.Href, URl + "/" + AssetItem.FileUrl);
                                writer.RenderBeginTag(HtmlTextWriterTag.A);

                                string[] Tokens = (AssetItem.FileUrl).Split('/');


                                string[] TokenComma = Tokens[5].Split('.');


                                writer.Write("Download " + TokenComma[1].ToUpper() + "");

                                writer.RenderEndTag();

                                writer.RenderEndTag();
                            }
                            writer.RenderBeginTag(HtmlTextWriterTag.Br);
                            writer.RenderEndTag();//

                            writer.AddAttribute(HtmlTextWriterAttribute.Class, clearboth);
                            writer.RenderBeginTag(HtmlTextWriterTag.Div);
                            //Clearoth
                            writer.RenderEndTag();

                            writer.RenderEndTag();
                        }


                    }
                    writer.RenderEndTag();//AssetsDiv

                    writer.RenderEndTag();//Main Div
                }

                ItemTypeFourHtml = stringWriter.ToString();
                ItemTypeFourHtml = "Please click on the links below to download your uploaded Asset(s) Item(s): <br />" + ItemTypeFourHtml;
            }
            
            ShoppingCart shopCart = null;
         
            MyCompanyDomainBaseReponse baseResponse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);


            bool result = false;

            PaymentGateway oPaymentGateWay = _ItemService.GetPaymentGatewayRecord(UserCookieManager.WBStoreId);

            CompanyContact user = _myCompanyService.GetContactByID(_myClaimHelper.loginContactID()); //LoginUser;

           
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
                            UserCookieManager.WEBOrderId = 0;




                            string AttachmentPath = _templateService.OrderConfirmationPDF(OrderId, UserCookieManager.WBStoreId);
                            List<string> AttachmentList = new List<string>();
                            AttachmentList.Add(AttachmentPath);

                            // string contains table 

                            SystemUser EmailOFSM = _userManagerService.GetSalesManagerDataByID(baseResponse.Company.SalesAndOrderManagerId1.Value);
                            if (ItemTypeFourHtml != null && ItemTypeFourHtml != string.Empty)
                            {
                                _myCampaignService.emailBodyGenerator(OnlineOrderCampaign, cep, user, (StoreMode)UserCookieManager.WEBStoreMode, Convert.ToInt32(baseResponse.Organisation.OrganisationId), "", HTMLOfShopReceipt, "", EmailOFSM.Email, "", "", AttachmentList, "", null, "", "", "", "", "", 0, "", 0, ItemTypeFourHtml);
                            }
                            else
                            {
                                _myCampaignService.emailBodyGenerator(OnlineOrderCampaign, cep, user, (StoreMode)UserCookieManager.WEBStoreMode, Convert.ToInt32(baseResponse.Organisation.OrganisationId), "", HTMLOfShopReceipt, "", EmailOFSM.Email, "", "", AttachmentList);
                            }
                            _campaignService.SendEmailToSalesManager((int)Events.NewOrderToSalesManager, _myClaimHelper.loginContactID(), _myClaimHelper.loginContactCompanyID(), OrderId, UserCookieManager.WEBOrganisationID, 0, StoreMode.Retail, UserCookieManager.WBStoreId, EmailOFSM);

                                 

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
                            ViewBag.Message = Utils.GetKeyValueFromResourceFile("ltrlpaymentgnotset", UserCookieManager.WBStoreId, "Payment Gateway is not set.")
;
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
                                        Response.Redirect("/ANZSubmit/" + OrderId);
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
                                case 8:
                                    {
                                        Response.Redirect("/PayWay/" + OrderId);
                                        break;

                                    }
                                case 9:
                                    {
                                        Response.Redirect("/StripeGateway/" + OrderId);
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
                            UserCookieManager.WEBOrderId = 0;
                            long ManagerID = _myCompanyService.GetContactIdByRole(_myClaimHelper.loginContactCompanyID(), (int)Roles.Manager); //ContactManager.GetBrokerByRole(SessionParameters.BrokerContactCompany.ContactCompanyID, Convert.ToInt32(Roles.Adminstrator));
                            cep.CorporateManagerID = ManagerID;
                            string AttachmentPath = _templateService.OrderConfirmationPDF(OrderId, UserCookieManager.WBStoreId);
                            List<string> AttachmentList = new List<string>();
                            AttachmentList.Add(AttachmentPath);
                            if (ItemTypeFourHtml != null && ItemTypeFourHtml != string.Empty)
                            {
                                _myCampaignService.emailBodyGenerator(OnlineOrderCampaign, cep, user, (StoreMode)UserCookieManager.WEBStoreMode, Convert.ToInt32(baseResponse.Organisation.OrganisationId), "", HTMLOfShopReceipt, "", EmailOFSM.Email, "", "", AttachmentList,"",null,"","","","","",0,"",0,ItemTypeFourHtml);
                            }
                            else
                            {
                                _myCampaignService.emailBodyGenerator(OnlineOrderCampaign, cep, user, (StoreMode)UserCookieManager.WEBStoreMode, Convert.ToInt32(baseResponse.Organisation.OrganisationId), "", HTMLOfShopReceipt, "", EmailOFSM.Email, "", "", AttachmentList);
                            }
                            _campaignService.SendEmailToSalesManager((int)Events.NewOrderToSalesManager, _myClaimHelper.loginContactID(), _myClaimHelper.loginContactCompanyID(), OrderId, UserCookieManager.WEBOrganisationID, (int)ManagerID, StoreMode.Retail, UserCookieManager.WBStoreId, EmailOFSM);
                          
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
                            UserCookieManager.WEBOrderId = 0;
                            long ManagerID = _myCompanyService.GetContactIdByRole(_myClaimHelper.loginContactCompanyID(), (int)Roles.Manager);
                            cep.CorporateManagerID = ManagerID;
                            string AttachmentPath = _templateService.OrderConfirmationPDF(OrderId, UserCookieManager.WBStoreId);
                            List<string> AttachmentList = new List<string>();
                            AttachmentList.Add(AttachmentPath);
                            _myCampaignService.emailBodyGenerator(OnlineOrderCampaign, cep, user, (StoreMode)UserCookieManager.WEBStoreMode, Convert.ToInt32(baseResponse.Organisation.OrganisationId), "", HTMLOfShopReceipt, "", EmailOFSM.Email, "", "", AttachmentList);

                            _campaignService.EmailsToCorpUser(OrderId, _myClaimHelper.loginContactID(), StoreMode.Corp, _myClaimHelper.loginContactTerritoryID(), baseResponse.Organisation, UserCookieManager.WBStoreId, EmailOFSM.Email);
                            
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
                                        Response.Redirect("/SignupPaypal/" + OrderId);
                                        break;
                                    }

                                case 2:
                                    {
                                        Response.Redirect("payments/paymentAuthorizeNet/" + OrderId);
                                        break;
                                    }
                                case 3:
                                    {
                                        Response.Redirect("/ANZSubmit/" + OrderId);
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
                                case 8:
                                    {
                                        Response.Redirect("/PayWay/" + OrderId);
                                        break;

                                    }
                                     case 9:
                                    {
                                        Response.Redirect("/StripeGateway/" + OrderId);
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


        private ShoppingCart LoadOrderDetail(string OrderId)
        {
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

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

                    ViewBag.IsShowPrices = _myCompanyService.ShowPricesOnStore(UserCookieManager.WEBStoreMode, StoreBaseResopnse.Company.ShowPrices ?? false, _myClaimHelper.loginContactID(), UserCookieManager.ShowPriceOnWebstore);

                    ViewBag.Currency = StoreBaseResopnse.Currency;
                    ViewBag.TaxLabel = StoreBaseResopnse.Company.TaxLabel;
                    StoreBaseResopnse = null;
                    return shopCart;
                }
                else
                {
                  
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}