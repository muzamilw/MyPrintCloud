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
            long OrderId = 0;
            ShoppingCart shopCart = null;
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;
            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
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

                shopCart = LoadShoppingCart(OrderId);

                if (shopCart != null)
                {
                    BindGridView(shopCart, StoreBaseResopnse, StoreBaseResopnse.Company.ShowPrices ?? false);

                }
                ViewBag.OrderID = OrderId;

                if (StoreBaseResopnse.Company.TaxRate != null)
                    ViewBag.TaxRate = StoreBaseResopnse.Company.TaxRate;
                else
                    ViewBag.TaxRate = "N/A";

                // start from here 

               
                // no Redeem Voucher options AT ALL for corporate customers

                if (StoreBaseResopnse.Company.ShowPrices ?? true)
                {
                    ViewBag.IsShowPrices = true;
                    
                }
                else
                {
                    ViewBag.IsShowPrices = false;
                }

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
                ViewBag.CancelPaymentMessage = "The Order payment processing has been cancelled";
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
            StoreBaseResopnse = null;
            return View("PartialViews/ShopCart", shopCart);
        }
        [HttpPost]
        public ActionResult Index()
        {
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;
            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
            string IsCallFrom = Request.Form["hfIsCallFrom"];
            string OrderID = Request.Form["hfOrderID"].ToString();
            string ItemID = Request.Form["hfItemID"].ToString();
            ShoppingCart shopCart = null;
            if(IsCallFrom == "Checkout")// to redirect add select page if login successfull
            {
                bool result = false;

                long sOrderID = 0;
                string voucherCode = string.Empty;
                int deliverCostCenterID = 0;
                double deliveryCost = 0;
                string deliveryCompletionTime = string.Empty;
                int DeliveryTime = 0;//Standard is seven
                bool hasWebAccess;
                bool IsPlaceOrder = _myCompanyService.canContactPlaceOrder(_myClaimHelper.loginContactID(),out hasWebAccess);
                double grandOrderTotal = 0;
                string DeliveryName = null;
                
                if (!string.IsNullOrEmpty(OrderID))
                {
                    sOrderID = Convert.ToInt32(OrderID);
                }
              //  bool login = true;
                if (_myClaimHelper.isUserLoggedIn())//
                {
                    if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                    {
                        
                        if (IsPlaceOrder == false)
                        {

                            //ShowMessage((string)GetGlobalResourceObject("MyResource", "shopcartCorporateOrderCantPlace"));
                            //return;
                        }
                    }

                    //These parameters are going to update in Order and Of course Order Total
                  //  double voucherDiscountRate = Convert.ToDouble(txtVoucherDiscountRate.Value);
                 //   voucherCode = txtDiscountVoucherCode.Text.Trim();
                    string total = Request.Form["hfGrandTotal"].ToString();
                    if(!string.IsNullOrEmpty(total))
                    {
                        grandOrderTotal = Convert.ToDouble(total);
                    }
                    


                    deliveryCompletionTime = Request.Form["numberOfDaysAddedTodelivery"];

                    if (!string.IsNullOrEmpty(deliveryCompletionTime))
                    {
                        DeliveryTime = 0;// Convert.ToInt32(deliveryCompletionTime);
                    }
                  
                  
                    if(UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                    {
                        result = _OrderService.UpdateOrderWithDetails(sOrderID, _myClaimHelper.loginContactID(), grandOrderTotal, DeliveryTime, StoreMode.Corp);
                    }
                    else if (UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)
                    {
                        result = _OrderService.UpdateOrderWithDetails(sOrderID, _myClaimHelper.loginContactID(), grandOrderTotal, DeliveryTime,StoreMode.Retail);
                    }
                    

                    if (result)
                    {
                        StoreBaseResopnse = null;
                        //string URL = "PartialViews/ShopCartAddressSelect/"+ sOrderID;

                        //return RedirectToAction("Index", "ShopCartAddressSelect", new { OrderID = sOrderID });
                        Response.Redirect("/ShopCartAddressSelect/" + sOrderID);
                        return null;
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
                        ValidateOrderForCorporateLogin(sOrderID, IsPlaceOrder,StoreBaseResopnse,hasWebAccess); // rediret user to the corp login page.
                    }

                    StoreBaseResopnse = null;
                    Response.Redirect("/Login");
                    return null;
                   
                }

            }
            else
            {

                ShoppingCart shopCarts = CopyProduct(Convert.ToInt32(ItemID), Convert.ToInt32(OrderID));
                return View("PartialViews/ShopCart", shopCarts);
            }
            //else
            //{
            //    ShoppingCart shopCarts = RemoveProduct(Convert.ToInt32(ItemID), Convert.ToInt32(OrderID));
            //    return View("PartialViews/ShopCart", shopCarts);

            //}

            
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


        public void ValidateOrderForCorporateLogin(long orderID,bool isPlaceOrder,MPC.Models.ResponseModels.MyCompanyDomainBaseReponse baseResponse,bool isWebAccess)
        {
            long CustomerID = 0;
            bool result = _OrderService.ValidateOrderForCorporateLogin(orderID, isPlaceOrder, baseResponse.Company.IsCustomer, isWebAccess,out CustomerID);
            if(result)
            {
                RedirectToAction("Login");
            }
            else
            {
                // nothing
            }
         
        }


      

        private void BindGridView(ShoppingCart shopCart, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse baseResponse, bool IsShowPrices)
        {
            List<ProductItem> itemsList = null;

            if (shopCart != null)
            {
                shopCart.TaxLabel = baseResponse.Company.TaxLabel + ":";
                itemsList = shopCart.CartItemsList;

                if (itemsList != null && itemsList.Count > 0)
                {
                    BindGriViewWithProductItemList(itemsList, baseResponse, IsShowPrices);
                    return;
                }
                
            }


        }


        private void BindGriViewWithProductItemList(List<ProductItem> itemsList, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse baseResponse, bool IsShowPrices)
        {
            if (itemsList != null && itemsList.Count > 0)
            {
                NumberOfRecords = itemsList.Count;
            }
            else
            {
                NumberOfRecords = 0;
            }

            ViewBag.NumberOfRecords = NumberOfRecords;
            //Hide the PRogress bar contrl
            //OrderStepsControl.Visible = true;
            ViewData["ProductItemList"] = itemsList;


            #region RelatedItems
            // if store is not corp then related items
            if (UserCookieManager.WEBStoreMode != (int)StoreMode.Corp)
            {
            //    LoadRelatedItems(itemsList, baseResponse, IsShowPrices);

            }
            #endregion
        }


        public ShoppingCart CopyProduct(int ItemID, int OrderID)
        {
            Item newCloneditem = null;
             string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;

             MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];

            newCloneditem = _ItemService.CloneItem(ItemID, 0, OrderID, 0, 0, 0, null, false, true, _myClaimHelper.loginContactID(),StoreBaseResopnse.Organisation.OrganisationId);

            Estimate objOrder = _OrderService.GetOrderByID(OrderID);
            _ItemService.CopyAttachments(ItemID, newCloneditem, objOrder.Order_Code, true, objOrder.CreationDate ?? DateTime.Now,Convert.ToInt64(StoreBaseResopnse.Company.OrganisationId), StoreBaseResopnse.Company.CompanyId);

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

            if (StoreBaseResopnse.Company.isIncludeVAT.Value == false)
            {
                ViewBag.isIncludeVAT = false;
            }
            else
            {
                ViewBag.isIncludeVAT = true;
            }
            ShoppingCart shopCart = LoadShoppingCart(OrderID);

            BindGridView(shopCart, StoreBaseResopnse, StoreBaseResopnse.Company.ShowPrices ?? false);
            if (UserCookieManager.WEBStoreMode != (int)StoreMode.Corp)
                SetLastItemTemplateMatchingSets(shopCart, StoreBaseResopnse);

            ViewBag.OrderID = OrderID;
            ViewBag.Currency = StoreBaseResopnse.Currency;
            StoreBaseResopnse = null;
            return shopCart;
          //  return View("PartialViews/ShopCart", shopCart);
        }

        public ActionResult RemoveProduct(long ItemID, long OrderID)
        {
            bool result = false;
            List<ArtWorkAttatchment> itemAttatchments = null;
            Template clonedTempldateFiles = null;
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;

           MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];

            result = _ItemService.RemoveCloneItem(ItemID, out itemAttatchments, out clonedTempldateFiles);

            if (result)
            {
                if (clonedTempldateFiles != null)
                {
                    _TemplateService.DeleteTemplateFiles(clonedTempldateFiles.ProductId, StoreBaseResopnse.Company.OrganisationId ?? 0);
                }


                RemoveItemAttacmentPhysically(itemAttatchments);
                
                // remove physicall files as it will do it by file table

                //BLL.ProductManager.RemoveItemAttacmentPhysically(itemAttatchments); // file removing physicslly
                //BLL.ProductManager.RemoveItemTemplateFilesPhysically(clonedTempldateFiles); // file removing


                //MyBaseMasterPage.UpdateCartItemsDisplay();


            }
           

            //ShoppingCart shopCart = LoadShoppingCart(OrderID);
          //  BindGridView(shopCart, StoreBaseResopnse, StoreBaseResopnse.Company.ShowPrices ?? false);
            //if (UserCookieManager.StoreMode != (int)StoreMode.Corp)
            //    SetLastItemTemplateMatchingSets(shopCart, StoreBaseResopnse);

            //StoreBaseResopnse = null;
            ViewBag.OrderID = OrderID;
            ViewBag.Currency = StoreBaseResopnse.Currency;
            Response.Redirect("/ShopCart?OrderId=" + OrderID);
            return null;
           // return View("PartialViews/ShopCart", shopCart);
        }
        public ActionResult ApplyDiscountVoucherCode(string DiscountVoucher, int OrderID)
        {
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;

            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];

            string voucherCode = DiscountVoucher.Trim();
            double voucherDiscRate = 0;
            double voucherDiscountedAmount = 0;
            ShoppingCart shopCart = null;
            if (!string.IsNullOrEmpty(voucherCode))
            {
                bool Result = false;

                Result = _OrderService.IsVoucherValid(voucherCode);



                if (Result == true)
                {
                    
                    Estimate RecordOfOrderIfDiscuntApplied = _OrderService.CheckDiscountApplied(OrderID);
                    if (RecordOfOrderIfDiscuntApplied.DiscountVoucherID.HasValue && RecordOfOrderIfDiscuntApplied.VoucherDiscountRate > 0)
                    {
                        double taxRate = UserCookieManager.TaxRate;
                        if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                        {
                            _OrderService.RollBackDiscountedItems(OrderID, taxRate, StoreMode.Corp);
                        }
                        else
                        {
                            _OrderService.RollBackDiscountedItems(OrderID, taxRate, StoreMode.Retail);
                        }


                    }
                    double VDiscountRate = _OrderService.SaveVoucherCodeAndRate(OrderID, voucherCode);
                    if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                    {
                        voucherDiscountedAmount = _OrderService.PerformVoucherdiscountOnEachItem(OrderID, OrderStatus.ShoppingCart, UserCookieManager.TaxRate, VDiscountRate, StoreMode.Corp);
                    }
                    else
                    {
                        voucherDiscountedAmount = _OrderService.PerformVoucherdiscountOnEachItem(OrderID, OrderStatus.ShoppingCart, UserCookieManager.TaxRate, VDiscountRate, StoreMode.Retail);

                    }
                    ViewBag.DiscAmount = Utils.FormatDecimalValueToTwoDecimal(voucherDiscountedAmount.ToString(), StoreBaseResopnse.Currency);



                }
                else
                {
                    // voucher invalid case

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



                    //  SetLastItemTemplateMatchingSets(shopCart);



                    if (StoreBaseResopnse.Company.isIncludeVAT.Value == false)
                    {
                        ViewBag.isIncludeVAT = false;
                    }
                    else
                    {
                        ViewBag.isIncludeVAT = true;
                    }
                    shopCart = LoadShoppingCart(OrderID);
                    BindGridView(shopCart, StoreBaseResopnse, StoreBaseResopnse.Company.ShowPrices ?? false);


                }

            }
            StoreBaseResopnse = null;
            return View("PartialView/ShopCart", shopCart);
        }

        public ActionResult RemoveDiscountVoucherCode(int OrderID)
        {
          

            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;

            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];

            ShoppingCart shopCart = null;
            Estimate RecordOfOrderIfDiscuntApplied = _OrderService.CheckDiscountApplied(OrderID);
            if (RecordOfOrderIfDiscuntApplied.DiscountVoucherID.HasValue)
            {
                if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                {
                    _OrderService.RollBackDiscountedItems(OrderID, UserCookieManager.TaxRate, StoreMode.Corp);
                }
                else
                {
                    _OrderService.RollBackDiscountedItems(OrderID, UserCookieManager.TaxRate, StoreMode.Retail);
                }
                _OrderService.ResetOrderVoucherCode(OrderID);
            }

            BindGridView(LoadShoppingCart(OrderID), StoreBaseResopnse, StoreBaseResopnse.Company.ShowPrices ?? false);
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


            //  SetLastItemTemplateMatchingSets(shopCart);



            if (StoreBaseResopnse.Company.isIncludeVAT.Value == false)
            {
                ViewBag.isIncludeVAT = false;
            }
            else
            {
                ViewBag.isIncludeVAT = true;
            }
            shopCart = LoadShoppingCart(OrderID);
            BindGridView(shopCart, StoreBaseResopnse, StoreBaseResopnse.Company.ShowPrices ?? false);

            StoreBaseResopnse = null;
            return View("PartialViews/ShopCart", shopCart);
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


        #region RelatedItems
        public void LoadRelatedItems(List<ProductItem> itemsList, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse baseResponse, bool IsShowPrices)
        {

            List<ProductItem> allRelatedItemsList = new List<ProductItem>();

            allRelatedItemsList = _ItemService.GetRelatedItemsList();

            allRelatedItemsList = FilterRelatedItems(RemoveDuplicatesItems(itemsList), allRelatedItemsList);
            List<ProductItem> allRelatedItemsListNotNull = new List<ProductItem>();
            foreach (var c in allRelatedItemsList)
            {
                if (c != null)
                {
                    allRelatedItemsListNotNull.Add(c);
                }
            }
            BindDataList(allRelatedItemsListNotNull);
            if (allRelatedItemsList.Count > 0)
            {
                RIviewModel.ProductName = itemsList[0].ProductName;
                RIviewModel.CurrencySymbol = baseResponse.Currency;
                RIviewModel.isShowPrices = IsShowPrices;
                ViewData["RIViewModel"] = RIviewModel;

            }


        }

        private List<int> RemoveDuplicatesItems(List<ProductItem> orderedItemsList)
        {
            List<int> orderedUniqueItemsList = new List<int>();

            if (orderedItemsList != null && orderedItemsList.Count > 1)
            {
                orderedItemsList.ForEach(orderItem =>
                {
                    if (orderItem.RefItemID.HasValue && !orderedUniqueItemsList.Contains(orderItem.RefItemID.Value))
                    {
                        orderedUniqueItemsList.Add(orderItem.RefItemID.Value);
                    }
                });
            }
            else
            {
                if (orderedItemsList != null && orderedItemsList.Count == 1 && orderedItemsList[0].RefItemID.HasValue)
                    orderedUniqueItemsList.Add(orderedItemsList[0].RefItemID.Value);

            }

            return orderedUniqueItemsList;

        }

        private List<ProductItem> FilterRelatedItems(List<int> orderedItemsList, List<ProductItem> allRelatedItemsList)
        {

            List<ProductItem> filteredItems = null;
            List<ProductItem> subItems = null;

            ProductItem curItem = null;

            if (allRelatedItemsList != null && allRelatedItemsList.Count > 0)
            {
                filteredItems = new List<ProductItem>();
                if (orderedItemsList != null && orderedItemsList.Count > 0)
                {
                    orderedItemsList.ForEach(cartitemID =>
                    {
                        subItems = null;
                        subItems = allRelatedItemsList.Where(relItem => relItem.ItemID == cartitemID && relItem.RelatedItemID > 0).ToList();

                        if (subItems != null && subItems.Count > 0)
                        {
                            subItems.ForEach(filterItem =>
                            {

                                curItem = null;
                                curItem = allRelatedItemsList.Where(currItem => (filterItem.RelatedItemID > 0 && filterItem.RelatedItemID == currItem.ItemID)).FirstOrDefault();
                                filteredItems.Add(curItem);
                            });
                        }
                    });
                }
            }

            if (filteredItems != null)
            {
                var query = (from fitem in filteredItems
                             select fitem).Distinct().ToList();

                query = query.ToList<ProductItem>();
                filteredItems = query;
                return filteredItems.Take(6).ToList();
            }


            else
                return filteredItems;
        }

        // bind list of related items
        private void BindDataList(List<ProductItem> filteredList)
        {
            if (filteredList != null && filteredList.Count > 0)
            {

                filteredList = filteredList.OrderBy(i => i.SortOrder).ToList();

                RIviewModel.ProductItems = filteredList;
                ViewData["RIViewModel"] = RIviewModel;
            }

        }

     
        #endregion

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