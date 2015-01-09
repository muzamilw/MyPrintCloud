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
using MPC.Webstore.ResponseModels;
using MPC.Webstore.ModelMappers;
using MPC.Webstore.ViewModels;
namespace MPC.Webstore.Controllers
{
    public class ShopCartController : Controller
    {
        private readonly IOrderService _OrderService;
        private readonly ITemplateService _TemplateService;
        private readonly IItemService _ItemService;
        private readonly ICompanyService _myCompanyService;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        private List<AddOnCostsCenter> _selectedItemsAddonsList = null;
        private double _deliveryCost = 0;
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
        public ActionResult Index(int OrderID)
        {
            ShoppingCart shopCart = null;
            List<CostCentre> deliveryCostCentersList = null;
            MyCompanyDomainBaseResponse baseResponseCurrency = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCurrency();
            MyCompanyDomainBaseResponse baseResponseCompany = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();
            //long OID = baseResponseCompany.Company.OrganisationId ?? 0;
            MyCompanyDomainBaseResponse baseResponseOrg = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromOrganisation();


            ViewBag.Currency = baseResponseCurrency.Currency;
            if (OrderID > 0)
            {
                UserCookieManager.OrderId = OrderID;
            }
            else {

                int status = (int)OrderStatus.ShoppingCart;
                long sOrderID = _OrderService.GetUserShopCartOrderID(status);
               
                UserCookieManager.OrderId = (int)OrderID;
                   
            }

            shopCart = LoadShoppingCart(OrderID, 0);

            string Messege = ""; // if error comes ....... //HttpContext.Current.Request.QueryString["Error"];
            if (!string.IsNullOrEmpty(Messege))
            {
                //ErrorDisplyMes.Style.Add(HtmlTextWriterStyle.Display, "block");
                //if (Messege == "UserCancelled")
                //{
                //    ErrorMEsSummry.Text = (string)GetGlobalResourceObject("MyResource", "lnkPaymentCancelled");
                //}
                //else if (Messege == "Failed")
                //{
                //    ErrorMEsSummry.Text = HttpContext.Current.Request.QueryString["ErrorMessage"];
                //}
            }
            else
            {
                //ErrorDisplyMes.Style.Add(HtmlTextWriterStyle.Display, "none");
            }


            // setProofInfo();

            if (shopCart != null)
            {
                BindGridView(shopCart);
 
            }

            if (baseResponseCompany.Company.TaxRate != null)
                ViewBag.TaxRate = baseResponseCompany.Company.TaxRate;
            else
                ViewBag.TaxRate = "N/A";

            // start from here 
           
           //  MatchingSet1.Visible = false;
                // no Redeem Voucher options AT ALL for corporate customers
            
            if (baseResponseCompany.Company.ShowPrices ?? true)
            {
                ViewBag.IsShowPrices = true;
                    //do nothing because pricing are already visible.
            }
            else
            {
                ViewBag.IsShowPrices = false;
                  //  cntRightPricing1.Visible = false;
            }
            
            if (UserCookieManager.StoreMode != (int)StoreMode.Corp)
              SetLastItemTemplateMatchingSets(shopCart,baseResponseOrg,baseResponseCurrency,baseResponseCompany);

               
            if (baseResponseCompany.Company.isIncludeVAT.Value == false)
            {
                   ViewBag.isIncludeVAT = false;
            }
            else
            {
                   ViewBag.isIncludeVAT = true;
             }

            return View("PartialViews/ShopCart",shopCart);
        }
        [HttpPost]
        public ActionResult Index()
        {
            ShoppingCart shopCart = null;

            return View("PartialViews/ShopCart", shopCart);
        }
        private ShoppingCart LoadShoppingCart(long orderID, int BrokerID)
        {
            ShoppingCart shopCart = _OrderService.GetShopCartOrderAndDetails(orderID, OrderStatus.ShoppingCart);
            if (shopCart != null)
            {
                _selectedItemsAddonsList = shopCart.ItemsSelectedAddonsList; //global values for all items
                ViewData["selectedItemsAddonsList"] = shopCart.ItemsSelectedAddonsList;
                _deliveryCost = shopCart.DeliveryCost;
               
            }

            return shopCart;
        }


        private void BindGridView(ShoppingCart shopCart)
        {
            List<ProductItem> itemsList = null;

            if (shopCart != null)
            {
                itemsList = shopCart.CartItemsList;
                if (itemsList != null && itemsList.Count > 0)
                {
                    BindGriViewWithProductItemList(itemsList);
                    return;
                }
            }

          
        }


        private void BindGriViewWithProductItemList(List<ProductItem> itemsList)
        {
            if (itemsList != null && itemsList.Count > 0)
            {
                NumberOfRecords = itemsList.Count;
            }
            else
            {
                NumberOfRecords = 0;
            }
         

            //Hide the PRogress bar contrl
            //OrderStepsControl.Visible = true;
            ViewData["ProductItemList"] = itemsList;
            decimal noOfCostCentreProdDays = 0;
            decimal _numOfProductionDays = 0;
            decimal numberOfDaysAddedTodelivery = 0;
          
            //relatedItems
            if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
            {
                // nothing to do
            }
            else
            {
              
                //relateditemsWidget.Visible = true;
                //relateditemsWidget.LoadRelatedItems(itemsList);
                
            }
        }
      
       
        public ActionResult CopyProduct(int ItemID,int OrderID)
        {
            Item newCloneditem = null;

            MyCompanyDomainBaseResponse baseResponseCurrency = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCurrency();
            MyCompanyDomainBaseResponse baseResponseCompany = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();
            MyCompanyDomainBaseResponse baseResponeOrg = _myCompanyService.GetStoreFromCache(UserCookieManager.OrganisationID).CreateFromOrganisation();

             newCloneditem = _ItemService.CloneItem(ItemID, 0.0, 0, 0, 0, 0, 0, 0, null, false, true,(int) _myClaimHelper.loginContactID());

             Estimate objOrder = _OrderService.GetOrderByID(OrderID);
             _ItemService.CopyAttachments(ItemID, newCloneditem, objOrder.Order_Code, true, objOrder.CreationDate ?? DateTime.Now);

             if (baseResponseCompany.Company.ShowPrices ?? true)
             {
                 ViewBag.IsShowPrices = true;
                 //do nothing because pricing are already visible.
             }
             else
             {
                 ViewBag.IsShowPrices = false;
                 //  cntRightPricing1.Visible = false;
             }


          
           

             if (baseResponseCompany.Company.isIncludeVAT.Value == false)
             {
                 ViewBag.isIncludeVAT = false;
             }
             else
             {
                 ViewBag.isIncludeVAT = true;
             }
             ShoppingCart shopCart = LoadShoppingCart(OrderID,0);
             
             BindGridView(shopCart);
             if (UserCookieManager.StoreMode != (int)StoreMode.Corp)
                 SetLastItemTemplateMatchingSets(shopCart, baseResponeOrg, baseResponseCurrency, baseResponseCompany);
             return View("PartialViews/ShopCart", shopCart);
        }

        public ActionResult RemoveProduct(int ItemID,int OrderID)
        {
            bool result = false;
            List<ArtWorkAttatchment> itemAttatchments = null;
            Template clonedTempldateFiles = null;
            MyCompanyDomainBaseResponse baseResponseCurrency = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCurrency();
            MyCompanyDomainBaseResponse baseResponseCompany = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();
            MyCompanyDomainBaseResponse baseResponseOrg = _myCompanyService.GetStoreFromCache(UserCookieManager.OrganisationID).CreateFromOrganisation();

            result =   _ItemService.RemoveCloneItem(ItemID, out itemAttatchments, out clonedTempldateFiles);

            if (result)
            {
                // remove physicall files as it will do it by file table

                //BLL.ProductManager.RemoveItemAttacmentPhysically(itemAttatchments); // file removing physicslly
                //BLL.ProductManager.RemoveItemTemplateFilesPhysically(clonedTempldateFiles); // file removing
               
                
                //MyBaseMasterPage.UpdateCartItemsDisplay();
               

            }
            if (baseResponseCompany.Company.ShowPrices ?? true)
            {
                ViewBag.IsShowPrices = true;
                //do nothing because pricing are already visible.
            }
            else
            {
                ViewBag.IsShowPrices = false;
                //  cntRightPricing1.Visible = false;
            }


            
            if (baseResponseCompany.Company.isIncludeVAT.Value == false)
            {
                ViewBag.isIncludeVAT = false;
            }
            else
            {
                ViewBag.isIncludeVAT = true;
            }

            ShoppingCart shopCart = LoadShoppingCart(OrderID, 0);
            BindGridView(shopCart);
            if (UserCookieManager.StoreMode != (int)StoreMode.Corp)
              SetLastItemTemplateMatchingSets(shopCart, baseResponseOrg, baseResponseCurrency, baseResponseCompany);
            return View("PartialViews/ShopCart", shopCart);
        }
        public ActionResult ApplyDiscountVoucherCode(string DiscountVoucher,int OrderID)
        {
            MyCompanyDomainBaseResponse baseResponseCurrency = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCurrency();
            MyCompanyDomainBaseResponse baseResponseCompany = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();
            
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
                        if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                        {
                            _OrderService.RollBackDiscountedItems(OrderID, taxRate, StoreMode.Corp);
                        }
                        else
                        {
                            _OrderService.RollBackDiscountedItems(OrderID, taxRate, StoreMode.Retail);
                        }


                    }
                    double VDiscountRate = _OrderService.SaveVoucherCodeAndRate(OrderID, voucherCode);
                    if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                    {
                        voucherDiscountedAmount = _OrderService.PerformVoucherdiscountOnEachItem(OrderID, OrderStatus.ShoppingCart, UserCookieManager.TaxRate, VDiscountRate, StoreMode.Corp);
                    }
                    else
                    {
                        voucherDiscountedAmount = _OrderService.PerformVoucherdiscountOnEachItem(OrderID, OrderStatus.ShoppingCart, UserCookieManager.TaxRate, VDiscountRate, StoreMode.Retail);

                    }
                    ViewBag.DiscAmount = Utils.FormatDecimalValueToTwoDecimal(voucherDiscountedAmount.ToString(), baseResponseCurrency.Currency);

                  

                }
                else
                {
                    // voucher invalid case

                    if (baseResponseCompany.Company.ShowPrices ?? true)
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


                   
                    if (baseResponseCompany.Company.isIncludeVAT.Value == false)
                    {
                        ViewBag.isIncludeVAT = false;
                    }
                    else
                    {
                        ViewBag.isIncludeVAT = true;
                    }
                    shopCart = LoadShoppingCart(OrderID, 0);
                    BindGridView(shopCart);
                        
                    
                }
               
            }
            return View("PartialView/ShopCart", shopCart);
        }

        public ActionResult RemoveDiscountVoucherCode(int OrderID)
        {
            MyCompanyDomainBaseResponse baseResponseCurrency = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCurrency();
            MyCompanyDomainBaseResponse baseResponseCompany = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();
            ShoppingCart shopCart = null;
            Estimate RecordOfOrderIfDiscuntApplied = _OrderService.CheckDiscountApplied(OrderID);
            if (RecordOfOrderIfDiscuntApplied.DiscountVoucherID.HasValue)
            {
                if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                {
                    _OrderService.RollBackDiscountedItems(OrderID, UserCookieManager.TaxRate, StoreMode.Corp);
                }
                else
                {
                    _OrderService.RollBackDiscountedItems(OrderID, UserCookieManager.TaxRate, StoreMode.Retail);
                }
                _OrderService.ResetOrderVoucherCode(OrderID);
            }

            BindGridView(LoadShoppingCart(OrderID, 0));
            if (baseResponseCompany.Company.ShowPrices ?? true)
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


            
            if (baseResponseCompany.Company.isIncludeVAT.Value == false)
            {
                ViewBag.isIncludeVAT = false;
            }
            else
            {
                ViewBag.isIncludeVAT = true;
            }
            shopCart = LoadShoppingCart(OrderID, 0);
            BindGridView(shopCart);
            return View("PartialViews/ShopCart", shopCart);
        }

        private void SetLastItemTemplateMatchingSets(ShoppingCart shopCart, MyCompanyDomainBaseResponse baseresponseOrg, MyCompanyDomainBaseResponse baseresponseCurrency, MyCompanyDomainBaseResponse baseresponseCompany)
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
                              
                                List<MatchingSets> res = _TemplateService.BindTemplatesList(TemplateName, 1,baseresponseOrg.Organisation.OrganisationId,(int)_myClaimHelper.loginContactCompanyID());

                                int isCalledFrom = 0;
                                if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                                    isCalledFrom = 4;
                                else
                                    isCalledFrom = 3;

                                bool isEmbaded;
                                if (UserCookieManager.StoreMode == (int)StoreMode.Corp || UserCookieManager.StoreMode == (int)StoreMode.Retail)
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
                                if (baseresponseCompany.Company.ShowPrices ?? true)
                                {
                                    isShowPrices = true;
                                }
                                else
                                {
                                    isShowPrices = false;
                                }
                               
                                if (res != null && res.Count > 0)
                                {
                                   
                                    foreach(var set in res)
                                    {
                                        ProductCategoriesView pCat = _ItemService.GetMappedCategory(set.CategoryName,(int)_myClaimHelper.loginContactCompanyID());

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
                                    MSViewModel.Currency = baseresponseCurrency.Currency;
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

    }
}