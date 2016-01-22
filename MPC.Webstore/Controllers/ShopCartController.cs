using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Webstore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Models.DomainModels;
using MPC.Webstore.ResponseModels;
using MPC.Webstore.ModelMappers;
using MPC.Webstore.ViewModels;
using System.Runtime.Caching;
using MPC.Models.ResponseModels;
namespace MPC.Webstore.Controllers
{
    public class ShopCartController : Controller
    {
        private readonly IOrderService _OrderService;
        private readonly ITemplateService _TemplateService;
        private readonly IItemService _ItemService;
        private readonly ICompanyService _myCompanyService;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        RelatedItemViewModel RIviewModel = new RelatedItemViewModel();

        private int NumberOfRecords = 0;
        public ShopCartController(IOrderService OrderService, IWebstoreClaimsHelperService myClaimHelper, ICompanyService myCompanyService, IItemService ItemService, ITemplateService TemplateService)
        {
            if (OrderService == null)
            {
                throw new ArgumentNullException("OrderService");
            }
            this._OrderService = OrderService;
            this._myClaimHelper = myClaimHelper;
            this._myCompanyService = myCompanyService;
            this._ItemService = ItemService;
            this._TemplateService = TemplateService;
        }

        // GET: ShopCart
        public ActionResult Index(string Orderid)
        {

            string optionalOrderId = Request.QueryString["OrderId"];
            string PayPalMessage = Request.QueryString["Message"];
            string ANZError = Request.QueryString["Error"];
            string ANZErrorMes = Request.QueryString["ErrorMessage"];
            string voucherAppliedMesg = Request.QueryString["VCId"];
            long OrderId = 0;
            ShoppingCart shopCart = null;
          
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);
            bool TypeFourItemStatus = _ItemService.typeFourItemsStatus(Convert.ToInt64(OrderId));
            ViewBag.TypeFourItemStatus = TypeFourItemStatus;
            if (string.IsNullOrEmpty(optionalOrderId)) // check if parameter have order id
            {
                if (UserCookieManager.WEBOrderId == 0) // cookie contains order id
                {
                    if (_myClaimHelper.loginContactID() > 0) // is user logged in
                    {
                        OrderId = _OrderService.GetOrderIdByContactId(_myClaimHelper.loginContactID(), _myClaimHelper.loginContactCompanyID());
                        UserCookieManager.WEBOrderId = OrderId;
                    }
                    else if (UserCookieManager.TemporaryCompanyId > 0 && UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)
                    {
                        OrderId = _OrderService.GetOrderIdByCompanyId(UserCookieManager.TemporaryCompanyId, OrderStatus.ShoppingCart);
                        UserCookieManager.WEBOrderId = OrderId;
                    }
                }
                else
                {
                    OrderId = UserCookieManager.WEBOrderId;
                    if (UserCookieManager.WEBOrderId > 0 && _myClaimHelper.loginContactID() > 0 && _myClaimHelper.loginContactCompanyID() > 0)
                    {
                        if (_OrderService.IsRealCustomerOrder(UserCookieManager.WEBOrderId, _myClaimHelper.loginContactID(), _myClaimHelper.loginContactCompanyID()) == true)
                        {
                            OrderId = UserCookieManager.WEBOrderId;
                        }
                        else
                        {
                            OrderId = 0;
                            UserCookieManager.WEBOrderId = 0;
                        }
                    }

                }

            }
            else
            {
                OrderId = Convert.ToInt64(optionalOrderId);
            }

            if (OrderId > 0)
            {
                List<CostCentre> deliveryCostCentersList = null;

                ViewBag.Currency = StoreBaseResopnse.Currency;

                if (StoreBaseResopnse.Company.IsDisplayDiscountVoucherCode == true)
                {
                    ViewBag.DisableCouponCode = 0;
                    if (string.IsNullOrEmpty(Request.QueryString["VCId"]))
                    {
                        long FreeShippingVoucherId = 0;
                        long DisVId = _ItemService.ApplyStoreDefaultDiscountRateOnCartItems(OrderId, UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID, Convert.ToDouble(StoreBaseResopnse.Company.TaxRate), ref FreeShippingVoucherId);
                        if (DisVId > 0) // if coupon is applies then this condition will apply voucher on the cart item recently added and no voucher applied
                        {
                            string VErrorMesg = "";
                            DiscountVoucher voucher = _ItemService.GetDiscountVoucherById(DisVId);
                            if (voucher != null)
                            {
                                _ItemService.ApplyDiscountOnCartProducts(voucher, OrderId, Convert.ToDouble(StoreBaseResopnse.Company.TaxRate), ref FreeShippingVoucherId, ref VErrorMesg);
                                VErrorMesg = "";
                            }
                        }

                        if (FreeShippingVoucherId > 0)
                        {
                            ApplyVoucherOnDeliveryItem(OrderId, FreeShippingVoucherId, Convert.ToDouble(StoreBaseResopnse.Company.TaxRate));
                        }
                        else
                        {
                            FreeShippingVoucherId = _ItemService.IsStoreHaveFreeShippingDiscountVoucher(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID, OrderId);
                            if (FreeShippingVoucherId == 0)
                            {
                                UserCookieManager.FreeShippingVoucherId = 0;
                                _ItemService.RollBackDiscountedItems(OrderId, Convert.ToDouble(StoreBaseResopnse.Company.TaxRate), UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID, true, _myClaimHelper.loginContactID(), _myClaimHelper.loginContactCompanyID());
                            }
                            else
                            {
                                ApplyVoucherOnDeliveryItem(OrderId, FreeShippingVoucherId, Convert.ToDouble(StoreBaseResopnse.Company.TaxRate));
                            }
                        }
                    }
                }
                else
                {
                    ViewBag.DisableCouponCode = 1;
                    _ItemService.RollBackDiscountedItems(OrderId, Convert.ToDouble(StoreBaseResopnse.Company.TaxRate), UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID, false, _myClaimHelper.loginContactID(), _myClaimHelper.loginContactCompanyID());
                    _ItemService.RollBackDiscountedItems(OrderId, Convert.ToDouble(StoreBaseResopnse.Company.TaxRate), UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID, true, _myClaimHelper.loginContactID(), _myClaimHelper.loginContactCompanyID());
                }

                shopCart = LoadShoppingCart(OrderId);

                if (shopCart != null)
                {
                    BindGridView(shopCart, StoreBaseResopnse, StoreBaseResopnse.Company.ShowPrices ?? false, OrderId);
                    if (!string.IsNullOrEmpty(voucherAppliedMesg) || (shopCart.DiscountVoucherID != null && shopCart.DiscountVoucherID > 0))
                    {
                        ViewBag.VoucherApplied = true;
                    }
                    else
                    {
                        ViewBag.VoucherApplied = null;
                    }
                }
                ViewBag.OrderID = OrderId;

                if (StoreBaseResopnse.Company.TaxRate != null)
                    ViewBag.TaxRate = StoreBaseResopnse.Company.TaxRate;
                else
                    ViewBag.TaxRate = "N/A";

                // start from here 


                // no Redeem Voucher options AT ALL for corporate customers

                ViewBag.IsShowPrices = _myCompanyService.ShowPricesOnStore(UserCookieManager.WEBStoreMode, StoreBaseResopnse.Company.ShowPrices ?? false, _myClaimHelper.loginContactID(), UserCookieManager.ShowPriceOnWebstore);

                if (UserCookieManager.WEBStoreMode != (int)StoreMode.Corp)
                    SetLastItemTemplateMatchingSets(shopCart, StoreBaseResopnse);

                if (StoreBaseResopnse.Company.isIncludeVAT.Value == false)
                {
                    ViewBag.isIncludeVAT = false;
                }
                else
                {
                    ViewBag.isIncludeVAT = true;
                }
            }
            if (!string.IsNullOrEmpty(PayPalMessage))
            {
                ViewBag.CancelPaymentMessage = Utils.GetKeyValueFromResourceFile("ltrlpaymentprocesscac", UserCookieManager.WBStoreId, "The Order payment processing has been cancelled");
            }
            else
            {
                ViewBag.CancelPaymentMessage = null;
            }
            if (!string.IsNullOrEmpty(ANZError))
            {
                ViewBag.ANZError = ANZError;
            }
            else
            {
                ViewBag.ANZError = null;
            }
            if (!string.IsNullOrEmpty(ANZErrorMes))
            {
                ViewBag.ANZErrorMes = ANZErrorMes;
            }
            else
            {
                ViewBag.ANZErrorMes = null;
            }
            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp && UserCookieManager.CanPlaceOrder == false)
            {
                ViewBag.CanPlaceOrder = 0;
            }
            else 
            {
                ViewBag.CanPlaceOrder = 1;
            }
            StoreBaseResopnse = null;
            return View("PartialViews/ShopCart", shopCart);
        }
        [HttpPost]
        public ActionResult Index()
        {

            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);
            string IsCallFrom = Request.Form["hfIsCallFrom"];
            string OrderID = Request.Form["hfOrderID"].ToString();
            string ItemID = Request.Form["hfItemID"].ToString();
            ShoppingCart shopCart = null;
            if (IsCallFrom == "Checkout")// to redirect add select page if login successfull
            {
                    bool result = false;

                    long sOrderID = 0;
                    string voucherCode = string.Empty;
                    int deliverCostCenterID = 0;
                    double deliveryCost = 0;
                    string deliveryCompletionTime = string.Empty;
                    int DeliveryTime = 0;//Standard is seven
                    bool hasWebAccess;
                    bool IsPlaceOrder = _myCompanyService.canContactPlaceOrder(_myClaimHelper.loginContactID(), out hasWebAccess);
                    double grandOrderTotal = 0;
                    string DeliveryName = null;

                    if (!string.IsNullOrEmpty(OrderID))
                    {
                        sOrderID = Convert.ToInt32(OrderID);
                    }

                    if (_myClaimHelper.isUserLoggedIn())
                    {
                        string total = Request.Form["hfGrandTotal"].ToString();
                        if (!string.IsNullOrEmpty(total))
                        {
                            grandOrderTotal = Convert.ToDouble(total);
                        }



                        deliveryCompletionTime = Request.Form["numberOfDaysAddedTodelivery"];

                        if (!string.IsNullOrEmpty(deliveryCompletionTime))
                        {
                            DeliveryTime = Convert.ToInt32(deliveryCompletionTime);
                        }

                        // if cart has items with product type other than 4 then do not make any change and redirect to address select page
                        //if cart has only asset item then get login user record update billing and shipping address in estimate and redriret to order confirmation

                        CompanyContact LoginContact=_myCompanyService.GetContactByID(_myClaimHelper.loginContactID());
                        if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                        {
                            if (_ItemService.typeFourItemsStatus(Convert.ToInt64(OrderID)) == false)
                            {
                               result = _OrderService.UpdateOrderWithDetails(sOrderID, _myClaimHelper.loginContactID(), grandOrderTotal, DeliveryTime, StoreMode.Corp,null);
                            }
                            else
                            {
                                result = _OrderService.UpdateOrderWithDetails(sOrderID, _myClaimHelper.loginContactID(), grandOrderTotal, DeliveryTime, StoreMode.Corp, LoginContact);
                            }
                        }
                        else if (UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)
                        {
                            if (_ItemService.typeFourItemsStatus(Convert.ToInt64(OrderID)) == false)
                            {
                            result = _OrderService.UpdateOrderWithDetails(sOrderID, _myClaimHelper.loginContactID(), grandOrderTotal, DeliveryTime, StoreMode.Retail,LoginContact);
                            }
                            else
                            {
                                result = _OrderService.UpdateOrderWithDetails(sOrderID, _myClaimHelper.loginContactID(), grandOrderTotal, DeliveryTime, StoreMode.Retail, LoginContact);
                            }
                            
                        }


                        if (result)
                        {
                            StoreBaseResopnse = null;
                            if (_ItemService.typeFourItemsStatus(Convert.ToInt64(OrderID)) == false)
                            {
                                Response.Redirect("/ShopCartAddressSelect/" + sOrderID);
                                return null;
                            }
                            else
                            {
                                Response.Redirect("/OrderConfirmation/" + sOrderID);
                                return null;
                            }
                        }
                        else
                        {

                            return null;
                        }

                    }
                    else
                    {
                        if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                        {
                            ValidateOrderForCorporateLogin(sOrderID, IsPlaceOrder, StoreBaseResopnse, hasWebAccess); // rediret user to the corp login page.
                        }

                        StoreBaseResopnse = null;
                        Response.Redirect("/Login");
                        return null;
                    }
                }
                else
                {
                    if (IsCallFrom == "RemoveVoucherCode")
                    {
                        long FreeShippingVoucherId = 0;
                        _ItemService.RollBackDiscountedItems(Convert.ToInt64(OrderID), Convert.ToDouble(StoreBaseResopnse.Company.TaxRate), UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID, false, _myClaimHelper.loginContactID(), _myClaimHelper.loginContactCompanyID());
                        _ItemService.ApplyStoreDefaultDiscountRateOnCartItems(Convert.ToInt64(OrderID), UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID, Convert.ToDouble(StoreBaseResopnse.Company.TaxRate), ref FreeShippingVoucherId);
                        if (FreeShippingVoucherId > 0)
                        {
                            UserCookieManager.FreeShippingVoucherId = FreeShippingVoucherId;
                        }
                        else
                        {
                            FreeShippingVoucherId = _ItemService.IsStoreHaveFreeShippingDiscountVoucher(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID, Convert.ToInt64(OrderID));
                            if (FreeShippingVoucherId == 0)
                            {
                                UserCookieManager.FreeShippingVoucherId = 0;
                                _ItemService.RollBackDiscountedItems(Convert.ToInt64(OrderID), Convert.ToDouble(StoreBaseResopnse.Company.TaxRate), UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID, true, _myClaimHelper.loginContactID(), _myClaimHelper.loginContactCompanyID());
                            }
                            else
                            {
                                UserCookieManager.FreeShippingVoucherId = FreeShippingVoucherId;
                                ApplyVoucherOnDeliveryItem(Convert.ToInt64(OrderID), FreeShippingVoucherId, Convert.ToDouble(StoreBaseResopnse.Company.TaxRate));
                            }
                        }
                    }
                    else if (IsCallFrom == "RemoveDeliveryVoucherCode")
                    {
                        _ItemService.RollBackDiscountedItems(Convert.ToInt64(OrderID), Convert.ToDouble(StoreBaseResopnse.Company.TaxRate), UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID, true, _myClaimHelper.loginContactID(), _myClaimHelper.loginContactCompanyID());
                        Estimate order = _OrderService.GetOrderByID(Convert.ToInt64(OrderID));
                        order.DiscountVoucherID = null;
                        order.VoucherDiscountRate = null;
                        _OrderService.SaveOrUpdateOrder();
                        UserCookieManager.FreeShippingVoucherId = 0;

                    }
                    else
                    {
                        CopyProduct(Convert.ToInt64(ItemID), Convert.ToInt64(OrderID));
                    }

                    if (StoreBaseResopnse.Company.IsDisplayDiscountVoucherCode == true)
                    {
                        ViewBag.DisableCouponCode = 0;
                    }
                    else
                    {
                        ViewBag.DisableCouponCode = 1;
                    }
                    if (StoreBaseResopnse.Company.ShowPrices ?? true)
                    {
                        ViewBag.IsShowPrices = true;

                    }
                    else
                    {
                        ViewBag.IsShowPrices = false;

                    }

                    if (StoreBaseResopnse.Company.isIncludeVAT.Value == false)
                    {
                        ViewBag.isIncludeVAT = false;
                    }
                    else
                    {
                        ViewBag.isIncludeVAT = true;
                    }
                    if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp && UserCookieManager.CanPlaceOrder == false)
                    {
                        ViewBag.CanPlaceOrder = 0;
                    }
                    else
                    {
                        ViewBag.CanPlaceOrder = 1;
                    }
                    shopCart = LoadShoppingCart(Convert.ToInt64(OrderID));

                    BindGridView(shopCart, StoreBaseResopnse, StoreBaseResopnse.Company.ShowPrices ?? false, Convert.ToInt64(OrderID));
                    if (UserCookieManager.WEBStoreMode != (int)StoreMode.Corp)
                        SetLastItemTemplateMatchingSets(shopCart, StoreBaseResopnse);

                    ViewBag.OrderID = OrderID;
                    ViewBag.Currency = StoreBaseResopnse.Currency;
                    StoreBaseResopnse = null;
                
                    return View("PartialViews/ShopCart", shopCart);
                
                
            }
        }
        private ShoppingCart LoadShoppingCart(long orderID)
        {
            ShoppingCart shopCart = _OrderService.GetShopCartOrderAndDetails(orderID, OrderStatus.ShoppingCart);
            if (shopCart != null)
            {
                ViewData["selectedItemsAddonsList"] = shopCart.ItemsSelectedAddonsList;

            }

            return shopCart;
        }

        private void ApplyVoucherOnDeliveryItem(long orderID, long voucherId, double storeTaxRate)
        {
            DiscountVoucher voucher = _ItemService.GetDiscountVoucherById(voucherId);
            if (voucher != null)
            {
                _ItemService.ApplyDiscountOnDeliveryItemAlreadyAddedToCart(voucher, orderID, storeTaxRate);
                UserCookieManager.FreeShippingVoucherId = voucherId;
            }
            else
            {
                UserCookieManager.FreeShippingVoucherId = 0;
            }

        }

        public void ValidateOrderForCorporateLogin(long orderID, bool isPlaceOrder, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse baseResponse, bool isWebAccess)
        {
            long CustomerID = 0;
            bool result = _OrderService.ValidateOrderForCorporateLogin(orderID, isPlaceOrder, baseResponse.Company.IsCustomer, isWebAccess, out CustomerID);
            if (result)
            {
                RedirectToAction("Login");
            }
            else
            {
                // nothing
            }

        }




        private void BindGridView(ShoppingCart shopCart, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse baseResponse, bool IsShowPrices, long OrderId)
        {
            List<ProductItem> itemsList = null;

            if (shopCart != null)
            {
                shopCart.TaxLabel = baseResponse.Company.TaxLabel + ":";
                itemsList = shopCart.CartItemsList;

                if (itemsList != null && itemsList.Count > 0)
                {
                    BindGriViewWithProductItemList(itemsList, baseResponse, IsShowPrices, OrderId);
                    return;
                }
                else
                {
                   
                    Estimate order = _OrderService.GetOrderByID(OrderId);
                    if (order != null)
                    {
                        order.DiscountVoucherID = null;
                        order.VoucherDiscountRate = null;
                        _OrderService.SaveOrUpdateOrder();
                    }
                }
            }
        }


        private void BindGriViewWithProductItemList(List<ProductItem> itemsList, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse baseResponse, bool IsShowPrices, long OrderId)
        {
            if (itemsList != null && itemsList.Count > 0)
            {
                NumberOfRecords = itemsList.Count;
            }
            else
            {
                NumberOfRecords = 0;
                Estimate order = _OrderService.GetOrderByID(OrderId);
                if(order != null)
                {
                    order.DiscountVoucherID = null;
                    order.VoucherDiscountRate = null;
                    _OrderService.SaveOrUpdateOrder();
                }
            }

            ViewBag.NumberOfRecords = NumberOfRecords;
          
            ViewData["ProductItemList"] = itemsList;

        }


        public void CopyProduct(long ItemID, long OrderID)
        {
            Item newCloneditem = null;

            newCloneditem = _ItemService.CloneItem(ItemID, 0, OrderID, UserCookieManager.WBStoreId, 0, 0, null, false, true, _myClaimHelper.loginContactID(), UserCookieManager.WEBOrganisationID, UserCookieManager.WBStoreId);

            Estimate objOrder = _OrderService.GetOrderByID(OrderID);

            _ItemService.CopyAttachments(ItemID, newCloneditem, objOrder.Order_Code, true, objOrder.CreationDate ?? DateTime.Now, UserCookieManager.WEBOrganisationID, UserCookieManager.WBStoreId);

        }

        public ActionResult RemoveProduct(long ItemID, long OrderID)
        {
            bool result = false;
            List<ArtWorkAttatchment> itemAttatchments = null;
            Template clonedTempldateFiles = null;
            //string CacheKeyName = "CompanyBaseResponse";
            //ObjectCache cache = MemoryCache.Default;

            //MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            result = _ItemService.RemoveCloneItem(ItemID, out itemAttatchments, out clonedTempldateFiles);

            if (result)
            {
                if (clonedTempldateFiles != null)
                {
                    _TemplateService.DeleteTemplateFiles(clonedTempldateFiles.ProductId, StoreBaseResopnse.Company.OrganisationId ?? 0);
                }
                RemoveItemAttacmentPhysically(itemAttatchments);

            }

            ViewBag.OrderID = OrderID;
            ViewBag.Currency = StoreBaseResopnse.Currency;
            Response.Redirect("/ShopCart?OrderId=" + OrderID);
            return null;

        }

        private void SetLastItemTemplateMatchingSets(ShoppingCart shopCart, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse baseResponse)
        {

            MatchingSetViewModel MSViewModel = new MatchingSetViewModel();
            List<MappedCategoriesName> mappedCatList = new List<MappedCategoriesName>();
            try
            {
                if (shopCart != null && shopCart.CartItemsList != null)
                {
                    //Model.ProductItem item = shopCart.CartItemsList.Where(c => c.TemplateID.Value > 0 && c.Attatchment.FileTitle != null && !c.Attatchment.FileTitle.Contains("Uploaded ArtWork")).LastOrDefault();
                    ProductItem item = shopCart.CartItemsList.Where(c => c.TemplateID != null && c.TemplateID > 0).LastOrDefault();
                    if (item != null)
                    {

                        string TemplateName = _TemplateService.GetTemplateNameByTemplateID((int)item.TemplateID);
                        if (!string.IsNullOrEmpty(TemplateName))
                        {

                            List<MatchingSets> res = _TemplateService.BindTemplatesList(TemplateName, 1, baseResponse.Organisation.OrganisationId, (int)_myClaimHelper.loginContactCompanyID());

                            int isCalledFrom = 0;
                            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                                isCalledFrom = 4;
                            else
                                isCalledFrom = 3;

                            bool isEmbaded;
                            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp || UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)
                                isEmbaded = true;
                            else
                                isEmbaded = false;

                            bool isIncludeVAT;
                            if (UserCookieManager.isIncludeTax == false)
                            {
                                isIncludeVAT = false;
                            }
                            else
                            {
                                isIncludeVAT = true;
                            }

                            bool isShowPrices;
                            if (baseResponse.Company.ShowPrices ?? true)
                            {
                                isShowPrices = true;
                            }
                            else
                            {
                                isShowPrices = false;
                            }

                            if (res != null && res.Count > 0)
                            {

                                foreach (var set in res)
                                {
                                    ProductCategoriesView pCat = _ItemService.GetMappedCategory(set.CategoryName, (int)_myClaimHelper.loginContactCompanyID());

                                    MappedCategoriesName mcn = new MappedCategoriesName();

                                    mcn.CategoryName = pCat.CategoryName;
                                    mcn.ProductID = set.ProductID;
                                    mcn.CategoryID = pCat.ProductCategoryId ?? 0;
                                    mcn.ItemID = pCat.ItemId;
                                    mcn.IsCalledFrom = isCalledFrom;
                                    mcn.ProductName = set.ProductName;
                                    mcn.IsEmbaded = isEmbaded;
                                    mcn.MinPrice = pCat.MinPrice;
                                    // mcn.defaultItemTax = pCat.de
                                    mappedCatList.Add(mcn);

                                    ViewData["MappedCategoryName"] = mappedCatList;

                                    //PartialViews/TempDesigner/ItemID/TemplateID/IsCalledFrom/CV2/ProductName/ContactID/CompanyID/IsEmbaded;

                                }

                                MSViewModel.MatchingSetsList = res;
                                MSViewModel.MappedCategoriesName = mappedCatList;
                                MSViewModel.IsIncludeVAT = isIncludeVAT;
                                MSViewModel.Currency = baseResponse.Currency;
                                MSViewModel.IsShowPrices = isShowPrices;
                                ViewData["MSViewModel"] = MSViewModel;
                            }
                            else
                            {

                                MSViewModel = null;
                            }

                        }
                        MSViewModel = null;
                    }
                    MSViewModel = null;
                }
                MSViewModel = null;

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }


        

        public void RemoveItemAttacmentPhysically(List<ArtWorkAttatchment> attatchmentList)
        {
            string completePath = string.Empty;
            //@Server.MapPath(folderPath);
            try
            {
                if (attatchmentList != null)
                {
                    foreach (ArtWorkAttatchment itemAtt in attatchmentList)
                    {
                        completePath = "/" + itemAtt.FolderPath + "/" + itemAtt.FileName;
                        if (itemAtt.UploadFileType == UploadFileTypes.Artwork)
                        {
                            Utils.DeleteFile(completePath + "Thumb.png");
                            //delete the thumb nails as well.
                            // Utils.DeleteFile(completePath.Replace(itemAtt.FileExtention, "Thumb.png"));
                        }
                        Utils.DeleteFile(completePath + itemAtt.FileExtention); //
                    }
                }
                //System.Web

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}