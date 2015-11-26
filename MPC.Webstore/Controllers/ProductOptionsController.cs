using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Implementation.WebStoreServices;
using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.Common;
using MPC.Models.DomainModels;
using MPC.Models.Common;
using System.Globalization;
using MPC.Webstore.ViewModels;
using MPC.Webstore.ModelMappers;
using MPC.Webstore.ResponseModels;
using System.IO;
using MPC.Webstore.Models;
using Newtonsoft.Json;
using System.Runtime.Caching;
using MPC.Models.ResponseModels;

namespace MPC.Webstore.Controllers
{
    public class ProductOptionsController : Controller
    {
        #region Private

        private readonly ICompanyService _myCompanyService;

        private readonly IItemService _myItemService;

        private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;

        private readonly IOrderService _orderService;

        private readonly IWebstoreClaimsHelperService _myClaimHelper;

        private readonly ITemplateService _template;


        private string QuestionQueueItem = String.Empty;
        private string InputQueueItem = String.Empty;
        #endregion


        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ProductOptionsController(IItemService myItemService, ICompanyService myCompanyService,
            IWebstoreClaimsHelperService webstoreAuthorizationChecker, IOrderService orderService, IWebstoreClaimsHelperService myClaimHelper
            , ITemplateService template)
        {
            if (myItemService == null)
            {
                throw new ArgumentNullException("myItemService");
            }

            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }

            if (webstoreAuthorizationChecker == null)
            {
                throw new ArgumentNullException("webstoreAuthorizationChecker");
            }

            if (orderService == null)
            {
                throw new ArgumentNullException("orderService");
            }

            if (myClaimHelper == null)
            {
                throw new ArgumentNullException("myClaimHelper");
            }

            if (template == null)
            {
                throw new ArgumentNullException("templatePages");
            }
            this._myItemService = myItemService;
            this._myCompanyService = myCompanyService;
            this._webstoreAuthorizationChecker = webstoreAuthorizationChecker;
            this._orderService = orderService;
            this._myClaimHelper = myClaimHelper;
            this._template = template;
        }

        #endregion
        // GET: ProductOptions
        public ActionResult Index(string CategoryId, string ItemId, string ItemMode, string TemplateId, string ViewToFire)
        {
            if (!string.IsNullOrEmpty(CategoryId) && !string.IsNullOrEmpty(ItemId) && !string.IsNullOrEmpty(ItemMode))
            {
                if (ItemMode.Contains("www."))
                {
                    TempData["ErrorMessage"] = "Your url is invalid.";
                    TempData["InvalidUrl"] = Request.Url.AbsoluteUri;
                     Response.Redirect("/Error");
                     return null;
                }
                Item clonedItem = null;
                //string CacheKeyName = "CompanyBaseResponse";
                //ObjectCache cache = MemoryCache.Default;
                long OrderID = 0;
                long referenceItemId = 0;
                long TemporaryRetailCompanyId = UserCookieManager.TemporaryCompanyId;
                //MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
                MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

                if (ItemMode == "UploadDesign")
                {

                    if (UserCookieManager.WEBOrderId == 0 || _orderService.IsRealCustomerOrder(UserCookieManager.WEBOrderId, _myClaimHelper.loginContactID(), _myClaimHelper.loginContactCompanyID()) == false)
                    {
                        OrderID = _orderService.ProcessPublicUserOrder(string.Empty, StoreBaseResopnse.Organisation.OrganisationId, (StoreMode)UserCookieManager.WEBStoreMode, _myClaimHelper.loginContactCompanyID(), _myClaimHelper.loginContactID(), ref TemporaryRetailCompanyId);
                        UserCookieManager.WEBOrderId = OrderID;
                        UserCookieManager.TemporaryCompanyId = TemporaryRetailCompanyId;
                        clonedItem = CloneItemAndUpdateCookie(StoreBaseResopnse, Convert.ToInt64(ItemId), OrderID);
                    }
                    else
                    {
                        OrderID = UserCookieManager.WEBOrderId;

                        MPC.Models.DomainModels.Estimate oCookieOrder = _orderService.GetOrderByOrderID(OrderID);

                        if (oCookieOrder != null && oCookieOrder.StatusId != (int)OrderStatus.ShoppingCart)
                        {
                            OrderID = _orderService.ProcessPublicUserOrder(string.Empty, StoreBaseResopnse.Organisation.OrganisationId, (StoreMode)UserCookieManager.WEBStoreMode, _myClaimHelper.loginContactCompanyID(), _myClaimHelper.loginContactID(), ref TemporaryRetailCompanyId);
                            UserCookieManager.WEBOrderId = OrderID;
                            UserCookieManager.TemporaryCompanyId = TemporaryRetailCompanyId;
                            clonedItem = CloneItemAndUpdateCookie(StoreBaseResopnse, Convert.ToInt64(ItemId), OrderID);
                        }
                        else
                        {
                            // gets the item from reference item id in case of upload design when user process the item but not add the item in cart
                            clonedItem = _myItemService.GetExisitingClonedItemInOrder(UserCookieManager.WEBOrderId, Convert.ToInt64(ItemId));

                            if (clonedItem == null)
                            {
                                clonedItem = _myItemService.CloneItem(Convert.ToInt64(ItemId), 0, UserCookieManager.WEBOrderId, UserCookieManager.WBStoreId, 0, 0, null, false, false, _myClaimHelper.loginContactID(), StoreBaseResopnse.Organisation.OrganisationId, true);
                            }
                        }

                    }
                    ViewData["ArtworkAttachments"] = clonedItem.ItemAttachments == null ? new List<MPC.Models.DomainModels.ItemAttachment>() : clonedItem.ItemAttachments.ToList();
                    referenceItemId = Convert.ToInt64(ItemId);
                    ViewData["Templates"] = null;
                }
                else if (ItemMode == "Modify")// modify item case
                {
                    OrderID = UserCookieManager.WEBOrderId;
                    clonedItem = _myItemService.GetClonedItemById(Convert.ToInt64(ItemId));
                    if (!string.IsNullOrEmpty(TemplateId))
                    {
                        ViewBag.ShowUploadArkworkPanel = true;
                        BindTemplatesList(Convert.ToInt64(TemplateId), clonedItem.ItemAttachments.ToList(), Convert.ToInt64(ItemId), clonedItem.DesignerCategoryId ?? 0, clonedItem.ProductName, clonedItem.IsTemplateDesignMode ?? 0);
                    }
                    else
                    {


                        ViewData["ArtworkAttachments"] = clonedItem.ItemAttachments == null ? new List<MPC.Models.DomainModels.ItemAttachment>() : clonedItem.ItemAttachments.ToList();
                        ViewData["Templates"] = null;

                    }


                    ViewBag.SelectedStockItemId = clonedItem.ItemSections.Where(s => s.SectionNo == 1).FirstOrDefault().StockItemID2; // This is a ItemStockOption id
                    ViewBag.SelectedQuantity = clonedItem.Qty1;

                    referenceItemId = clonedItem.RefItemId ?? 0;

                    if (UserCookieManager.WEBOrderId == 0)
                    {
                        UserCookieManager.WEBOrderId = clonedItem.EstimateId ?? 0;
                    }

                    QuestionQueueItem = clonedItem.ItemSections.Where(s => s.SectionNo == 1).FirstOrDefault().QuestionQueue;
                    InputQueueItem = clonedItem.ItemSections.Where(s => s.SectionNo == 1).FirstOrDefault().InputQueue;
                    if (QuestionQueueItem == null && InputQueueItem == null)
                    {
                        ViewBag.CostCentreQueueItems = null;
                    }
                    else
                    {
                        QuestionAndInputQueues QIQueuesObj = new QuestionAndInputQueues();
                        if (QuestionQueueItem != null)
                        {

                            List<QuestionQueueItem> QQueueList = JsonConvert.DeserializeObject<List<QuestionQueueItem>>(QuestionQueueItem);
                            QIQueuesObj.QuestionQueues = QQueueList.ToList();
                        }
                        else
                        {
                            QIQueuesObj.QuestionQueues = new List<QuestionQueueItem>();
                        }

                        if (InputQueueItem != null)
                        {

                            List<InputQueueItem> IQueueList = JsonConvert.DeserializeObject<List<InputQueueItem>>(InputQueueItem);
                            QIQueuesObj.InputQueues = IQueueList.ToList();
                        }
                        else
                        {
                            QIQueuesObj.InputQueues = new List<InputQueueItem>();
                        }
                        ViewBag.CostCentreQueueItems = QIQueuesObj;
                    }
                }
                else if (!string.IsNullOrEmpty(TemplateId))// template case
                {
                    ViewBag.ShowUploadArkworkPanel = true;
                    OrderID = UserCookieManager.WEBOrderId;
                    clonedItem = _myItemService.GetClonedItemById(Convert.ToInt64(ItemId));
                    if (clonedItem != null)
                    {
                        if (OrderID == 0)
                        {
                            OrderID = clonedItem.EstimateId ?? 0;
                            UserCookieManager.WEBOrderId = clonedItem.EstimateId ?? 0;
                        }
                        BindTemplatesList(Convert.ToInt64(TemplateId), clonedItem.ItemAttachments == null ? null : clonedItem.ItemAttachments.ToList(), Convert.ToInt64(ItemId), Convert.ToInt32(clonedItem.DesignerCategoryId), clonedItem.ProductName, clonedItem.IsTemplateDesignMode ?? 0);
                        referenceItemId = clonedItem.RefItemId ?? 0;
                        if (clonedItem.ItemSections != null)
                        {
                            if (clonedItem.ItemSections.Where(s => s.SectionNo == 1).FirstOrDefault().StockItemID1 != null && clonedItem.ItemSections.Where(s => s.SectionNo == 1).FirstOrDefault().StockItemID1 > 0)
                            {
                                ViewBag.SelectedStockItemId = clonedItem.ItemSections.Where(s => s.SectionNo == 1).FirstOrDefault().StockItemID2;
                                ViewBag.SelectedQuantity = clonedItem.Qty1;

                            }

                        }
                    }

                }

                ViewBag.ClonedItemId = clonedItem.ItemId;

                ViewBag.ClonedItem = clonedItem;

                ViewBag.AttachmentCount = clonedItem.ItemAttachments == null ? 0 : clonedItem.ItemAttachments.Count;

                if (!string.IsNullOrEmpty(TemplateId))
                {
                    DefaultSettings(referenceItemId, ItemMode, clonedItem.ItemId, OrderID, StoreBaseResopnse, true);
                }
                else
                {
                    DefaultSettings(referenceItemId, ItemMode, clonedItem.ItemId, OrderID, StoreBaseResopnse, false);
                }


                StoreBaseResopnse = null;
                TempData["ItemMode"] = ItemMode;
                ViewBag.ViewToFire = ViewToFire;
                if (ViewToFire == "ProductOptionsAndDetails")
                {
                    return View("PartialViews/ProductOptionsAndDetails");
                }
                else 
                {
                    return View("PartialViews/ProductOptions");
                }
               
            }
            else 
            {
                ControllerContext.HttpContext.Response.RedirectToRoute("Default");
                return null;
            }
           
        }

        [HttpPost]
        public ActionResult Index(ItemCartViewModel cartObject, string ReferenceItemId)
        {
            //string CacheKeyName = "CompanyBaseResponse";
            //ObjectCache cache = MemoryCache.Default;
            var ITemMode = TempData["ItemMode"];
            string CostCentreJsonQueue = "";
            string QuestionJsonQueue = cartObject.JsonAllQuestionQueue;
            string InputJsonQueue = cartObject.JsonAllInputQueue;
            //MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            if (!string.IsNullOrEmpty(cartObject.ItemPrice) || !string.IsNullOrEmpty(cartObject.JsonPriceMatrix) || !string.IsNullOrEmpty(cartObject.StockId) || !string.IsNullOrEmpty(cartObject.ItemStockOptionId))
            {

                List<AddOnCostsCenter> ccObjectList = null;
                if (cartObject.JsonAddOnsPrice != null)
                {
                    CostCentreJsonQueue = cartObject.JsonAddOnsPrice;
                    List<AddOnCostCenterViewModel> selectedAddOnsList = JsonConvert.DeserializeObject<List<AddOnCostCenterViewModel>>(cartObject.JsonAddOnsPrice);

                    ccObjectList = new List<AddOnCostsCenter>();

                    AddOnCostsCenter ccObject = null;
                    double AddOnPrices = 0;
                    foreach (var addOn in selectedAddOnsList)
                    {
                        ccObject = new AddOnCostsCenter();

                        ccObject.CostCenterID = addOn.CostCenterId;

                        ccObject.Qty1NetTotal = addOn.ActualPrice;


                        AddOnPrices += addOn.ActualPrice;

                        ccObjectList.Add(ccObject);
                    }
                    cartObject.AddOnPrice = AddOnPrices.ToString();
                }



                if (StoreBaseResopnse.Company.isCalculateTaxByService == true) // calculate tax by service
                {

                    _myItemService.UpdateCloneItemService(Convert.ToInt64(cartObject.ItemId), Convert.ToDouble(cartObject.QuantityOrdered), Convert.ToDouble(cartObject.ItemPrice), Convert.ToDouble(cartObject.AddOnPrice), Convert.ToInt64(cartObject.StockId), ccObjectList, UserCookieManager.WEBStoreMode, Convert.ToInt64(StoreBaseResopnse.Company.OrganisationId), 0, Convert.ToString(ITemMode), false, Convert.ToInt64(cartObject.ItemStockOptionId), UserCookieManager.WBStoreId, 0, QuestionJsonQueue, CostCentreJsonQueue, InputJsonQueue); // set files count
                }
                else
                {
                    if (Convert.ToDouble(StoreBaseResopnse.Company.TaxRate) > 0)
                    {
                        _myItemService.UpdateCloneItemService(Convert.ToInt64(cartObject.ItemId), Convert.ToDouble(cartObject.QuantityOrdered), Convert.ToDouble(cartObject.ItemPrice), Convert.ToDouble(cartObject.AddOnPrice), Convert.ToInt64(cartObject.StockId), ccObjectList, UserCookieManager.WEBStoreMode, Convert.ToInt64(StoreBaseResopnse.Company.OrganisationId), Convert.ToDouble(StoreBaseResopnse.Company.TaxRate), Convert.ToString(ITemMode), true, Convert.ToInt64(cartObject.ItemStockOptionId), UserCookieManager.WBStoreId, 0, QuestionJsonQueue, CostCentreJsonQueue, InputJsonQueue); // set files count
                    }
                    else
                    {
                        _myItemService.UpdateCloneItemService(Convert.ToInt64(cartObject.ItemId), Convert.ToDouble(cartObject.QuantityOrdered), Convert.ToDouble(cartObject.ItemPrice), Convert.ToDouble(cartObject.AddOnPrice), Convert.ToInt64(cartObject.StockId), ccObjectList, UserCookieManager.WEBStoreMode, Convert.ToInt64(StoreBaseResopnse.Company.OrganisationId), 0, Convert.ToString(ITemMode), false, Convert.ToInt64(cartObject.ItemStockOptionId), UserCookieManager.WBStoreId, 0, QuestionJsonQueue, CostCentreJsonQueue, InputJsonQueue); // set files count
                    }
                }


                Response.Redirect("/ShopCart?OrderId=" + UserCookieManager.WEBOrderId);

                return null;

            }
            else
            {
                DefaultSettings(Convert.ToInt64(ReferenceItemId), "", Convert.ToInt64(cartObject.ItemId), Convert.ToInt64(cartObject.OrderId), StoreBaseResopnse, false);

                if (Request.Form["ViewToFire"] == "ProductOptionsAndDetails")
                {
                    return View("PartialViews/ProductOptionsAndDetails");
                }
                else
                {
                    return View("PartialViews/ProductOptions");
                }
            }


        }

        private void DefaultSettings(long ReferenceItemId, string mode, long ClonedItemId, long OrderId, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse, bool isTemplateProduct)
        {

            List<ProductPriceMatrixViewModel> PriceMatrixObjectList = null;

            List<AddOnCostCenterViewModel> AddonObjectList = null;
            // get reference item, stocks, addons, price matrix
            Item referenceItem = _myItemService.GetItemById(Convert.ToInt64(ReferenceItemId));

            List<ItemStockOption> stockOptList = _myItemService.GetStockList(Convert.ToInt64(ReferenceItemId), UserCookieManager.WBStoreId);

            ViewData["StckOptions"] = stockOptList.ToList();

            if (referenceItem.IsStockControl == true && referenceItem.ProductType == (int)ProductType.NonPrintProduct)
            {
                List<StockItemViewModel> stockItemOfStockOption = new List<StockItemViewModel>();

                foreach (ItemStockOption option in stockOptList)
                {
                    if (option.StockItem != null && stockItemOfStockOption.Where(s => s.StockId == option.StockId).FirstOrDefault() == null)
                    {
                        StockItemViewModel sItemObj = new StockItemViewModel();
                        sItemObj.StockId = option.StockId ?? 0;
                        sItemObj.InStockValue = option.StockItem.inStock ?? 0;
                        sItemObj.StockOptionId = option.ItemStockOptionId;
                        if (option.StockItem.isAllowBackOrder == true) // back ordering allowed
                        {
                            sItemObj.isAllowBackOrder = true;
                        }
                        else
                        {
                            sItemObj.isAllowBackOrder = false;
                        }
                        sItemObj.StockTextToDisplay = Utils.GetKeyValueFromResourceFile("lblitemInStock", UserCookieManager.WBStoreId) + option.StockItem.inStock;
                        if (option.StockItem.inStock == 0 || option.StockItem.inStock < 0) // no stock available 
                        {

                            if (option.StockItem.isAllowBackOrder ?? false) // back ordering allowed
                            {
                                sItemObj.StockTextToDisplay = Utils.GetKeyValueFromResourceFile("lblBackOrder", UserCookieManager.WBStoreId);
                                sItemObj.isItemInStock = true;
                            }
                            else
                            {
                                sItemObj.isItemInStock = false;
                                sItemObj.StockTextToDisplay = Utils.GetKeyValueFromResourceFile("lblItemOutOfStock", UserCookieManager.WBStoreId);
                            }
                        } // stock available
                        else
                        {
                            sItemObj.isItemInStock = true;

                        }
                        stockItemOfStockOption.Add(sItemObj);
                    }
                }
                ViewData["stockControlItems"] = stockItemOfStockOption;
            }
            else
            {
                ViewData["stockControlItems"] = null;
            }





            List<AddOnCostsCenter> listOfCostCentres = _myItemService.GetStockOptionCostCentres(Convert.ToInt64(ReferenceItemId), UserCookieManager.WBStoreId);

            List<SectionCostcentre> clonedSectionCostCentres = null;

            if (mode == "Modify")
            {
                ViewBag.Mode = "Modify";
                if (isTemplateProduct == true)
                {
                    ViewBag.ShowUploadArkworkPanel = true;
                }
                else
                {
                    if (referenceItem.IsUploadImage == true)
                    {
                        ViewBag.isUploadImageAlso = true;
                        ViewBag.ShowUploadArkworkPanel = true;
                    }
                    else
                    {
                        ViewBag.isUploadImageAlso = false;
                        ViewBag.ShowUploadArkworkPanel = false;
                    }
                }
            }
            else
            {
                ViewBag.Mode = "";

                if (mode == "UploadDesign")
                {
                    if (referenceItem.IsUploadImage == true)
                    {
                        ViewBag.isUploadImageAlso  = true;
                        ViewBag.ShowUploadArkworkPanel = true;
                    }
                    else
                    {
                        ViewBag.isUploadImageAlso = false;
                        ViewBag.ShowUploadArkworkPanel = false;
                    }
                }
            }

            if (referenceItem.ProductType == (int)ProductType.NonPrintProduct)
            {
                if (referenceItem.ThumbnailPath == null)
                {
                    ViewBag.FinishedGoodProduct = null;
                }
                else
                {
                    ViewBag.FinishedGoodProduct = referenceItem.ThumbnailPath;
                }
            }
            else
            {
                ViewBag.FinishedGoodProduct = null;
            }



            clonedSectionCostCentres = _myItemService.GetClonedItemAddOnCostCentres(ClonedItemId, UserCookieManager.WEBOrganisationID);

            if (listOfCostCentres == null || listOfCostCentres.Count == 0)
            {
                ViewData["CostCenters"] = null;
            }
            else
            {
                ViewData["CostCenters"] = listOfCostCentres;
            }


            AddonObjectList = new List<AddOnCostCenterViewModel>();

            long stockOptionSelected = Convert.ToInt64(ViewBag.SelectedStockItemId);
            

            foreach (var addOn in listOfCostCentres)
            {
                if (clonedSectionCostCentres != null && clonedSectionCostCentres.Count > 0)// this will run in case of modify mode and cost centres selected
                {
                    //List<QuestionQueueItem> objSettings = JsonConvert.DeserializeObject<List<QuestionQueueItem>>(QueueItem);
                    bool isAddedToList = false;
                    foreach (var cItem in clonedSectionCostCentres)
                    {
                        if (cItem.CostCentreId == addOn.CostCenterID && addOn.ItemStockOptionId == stockOptionSelected)
                        {
                            // var objCS = objSettings.Where(g => g.CostCentreID == cItem.CostCentreId).ToList();

                            //if (addOn.Type == 4)
                            //{
                            AddOnCostCenterViewModel addOnsObject = new AddOnCostCenterViewModel
                            {
                                Id = addOn.ProductAddOnID,
                                CostCenterId = addOn.CostCenterID,
                                Type = addOn.Type,
                                SetupCost = addOn.SetupCost,
                                MinimumCost = addOn.MinimumCost,
                                ActualPrice = cItem.Qty1NetTotal ?? 0,
                                StockOptionId = addOn.ItemStockId,
                                Description = "",
                                isChecked = 1,
                                QuantitySourceType = addOn.QuantitySourceType,
                                TimeSourceType = addOn.TimeSourceType,
                                ItemStockOptionId = addOn.ItemStockOptionId
                                // CostCentreJasonData = JsonConvert.SerializeObject(objCS, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
                                //   CostCenterModifiedJson =  objCS
                            };
                            AddonObjectList.Add(addOnsObject);
                            //}
                            //else
                            //{
                            //    AddOnCostCenterViewModel addOnsObject = new AddOnCostCenterViewModel
                            //    {
                            //        Id = addOn.ProductAddOnID,
                            //        CostCenterId = addOn.CostCenterID,
                            //        Type = addOn.Type,
                            //        SetupCost = addOn.SetupCost,
                            //        MinimumCost = addOn.MinimumCost,
                            //        ActualPrice = addOn.AddOnPrice ?? 0.0,
                            //        StockOptionId = addOn.ItemStockId,
                            //        Description = "",
                            //        isChecked = true,
                            //        QuantitySourceType = addOn.QuantitySourceType,
                            //        TimeSourceType = addOn.TimeSourceType
                            //    };
                            //    AddonObjectList.Add(addOnsObject);
                            //}

                            isAddedToList = true;
                            break;
                        }
                    }
                    if (!isAddedToList)
                    {
                        isAddedToList = false;
                        AddOnCostCenterViewModel addOnsObject = new AddOnCostCenterViewModel
                        {
                            Id = addOn.ProductAddOnID,
                            CostCenterId = addOn.CostCenterID,
                            Type = addOn.Type,
                            SetupCost = addOn.SetupCost,
                            MinimumCost = addOn.MinimumCost,
                            ActualPrice = addOn.AddOnPrice ?? 0.0,
                            StockOptionId = addOn.ItemStockId,
                            Description = "",
                            isChecked = 0,
                            QuantitySourceType = addOn.QuantitySourceType,
                            TimeSourceType = addOn.TimeSourceType,
                            ItemStockOptionId = addOn.ItemStockOptionId
                        };
                        AddonObjectList.Add(addOnsObject);
                    }
                }
                else
                {
                    AddOnCostCenterViewModel addOnsObject = new AddOnCostCenterViewModel
                    {
                        Id = addOn.ProductAddOnID,
                        CostCenterId = addOn.CostCenterID,
                        Type = addOn.Type,
                        SetupCost = addOn.SetupCost,
                        MinimumCost = addOn.MinimumCost,
                        ActualPrice = addOn.AddOnPrice ?? 0.0,
                        StockOptionId = addOn.ItemStockId,
                        Description = "",
                        isChecked = 0,
                        QuantitySourceType = addOn.QuantitySourceType,
                        TimeSourceType = addOn.TimeSourceType
                    };
                    AddonObjectList.Add(addOnsObject);
                }

            }

            ViewBag.JsonAddonCostCentre = AddonObjectList;

            if (_webstoreAuthorizationChecker.isUserLoggedIn())
            {
                referenceItem.ItemPriceMatrices = _myItemService.GetPriceMatrix(referenceItem.ItemPriceMatrices.ToList(), referenceItem.IsQtyRanged ?? false, true, UserCookieManager.WBStoreId, Convert.ToInt64(StoreBaseResopnse.Company.OrganisationId));
            }
            else
            {
                referenceItem.ItemPriceMatrices = _myItemService.GetPriceMatrix(referenceItem.ItemPriceMatrices.ToList(), referenceItem.IsQtyRanged ?? false, false, 0, Convert.ToInt64(StoreBaseResopnse.Company.OrganisationId));
            }

            PriceMatrixObjectList = new List<ProductPriceMatrixViewModel>();
            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)
            {
                if (StoreBaseResopnse.Company.isIncludeVAT == true)
                {
                    ViewBag.VATLabel = "inc. " + StoreBaseResopnse.Company.TaxLabel;
                    if (referenceItem.DefaultItemTax != null)
                    {
                        ViewBag.TaxRate = Convert.ToDouble(referenceItem.DefaultItemTax);

                    }
                    else if (Convert.ToDouble(StoreBaseResopnse.Company.TaxRate) > 0)
                    {
                        ViewBag.TaxRate = Convert.ToDouble(StoreBaseResopnse.Company.TaxRate);

                    }
                    else
                    {
                        ViewBag.VATLabel = "ex. " + StoreBaseResopnse.Company.TaxLabel;
                        ViewBag.TaxRate = 0;

                    }

                }
                else
                {
                    ViewBag.VATLabel = "ex. " + StoreBaseResopnse.Company.TaxLabel;
                    ViewBag.TaxRate = 0;

                }
            }
            else
            {
                if (StoreBaseResopnse.Company.isIncludeVAT == true)
                {
                    ViewBag.VATLabel = "inc. " + StoreBaseResopnse.Company.TaxLabel;
                    ViewBag.TaxRate = Convert.ToDouble(StoreBaseResopnse.Company.TaxRate);

                }
                else
                {
                    ViewBag.VATLabel = "ex. " + StoreBaseResopnse.Company.TaxLabel;
                    ViewBag.TaxRate = 0;

                }
            }
            foreach (var matrixItem in referenceItem.ItemPriceMatrices.ToList())
            {
                if (UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)
                {

                }
                else
                {

                    matrixItem.PricePaperType1 = matrixItem.PricePaperType1;
                    matrixItem.PricePaperType2 = matrixItem.PricePaperType2;
                    matrixItem.PricePaperType3 = matrixItem.PricePaperType3;
                    matrixItem.PriceStockType4 = matrixItem.PriceStockType4 == null ? 0 : matrixItem.PriceStockType4;
                    matrixItem.PriceStockType5 = matrixItem.PriceStockType5 == null ? 0 : matrixItem.PriceStockType5;
                    matrixItem.PriceStockType6 = matrixItem.PriceStockType6 == null ? 0 : matrixItem.PriceStockType6;
                    matrixItem.PriceStockType7 = matrixItem.PriceStockType7 == null ? 0 : matrixItem.PriceStockType7;
                    matrixItem.PriceStockType8 = matrixItem.PriceStockType8 == null ? 0 : matrixItem.PriceStockType8;
                    matrixItem.PriceStockType9 = matrixItem.PriceStockType9 == null ? 0 : matrixItem.PriceStockType9;
                    matrixItem.PriceStockType10 = matrixItem.PriceStockType10 == null ? 0 : matrixItem.PriceStockType10;
                    matrixItem.PriceStockType11 = matrixItem.PriceStockType11 == null ? 0 : matrixItem.PriceStockType11;
                }
                if (referenceItem.IsQtyRanged == true)
                {

                    //json object
                    ProductPriceMatrixViewModel priceObject = new ProductPriceMatrixViewModel
                    {
                        ItemID = Convert.ToInt32(matrixItem.PriceMatrixId),
                        QtyRangeFrom = Convert.ToDouble(matrixItem.QtyRangeFrom),
                        QtyRangeTo = Convert.ToDouble(matrixItem.QtyRangeTo)
                    };
                    PriceMatrixObjectList.Add(priceObject);

                }
                else
                {

                    //json object
                    ProductPriceMatrixViewModel priceObject = new ProductPriceMatrixViewModel
                    {
                        ItemID = Convert.ToInt32(matrixItem.PriceMatrixId),
                        Quantity = matrixItem.Quantity.ToString()
                    };
                    PriceMatrixObjectList.Add(priceObject);
                }
            }

            ViewBag.JasonPriceMatrix = PriceMatrixObjectList;

            MyCompanyDomainBaseReponse baseResponse =  _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId); //_myCompanyService.GetStoreFromCache(UserCookieManager.WBStoreId).CreateFromCurrency();
            ViewBag.Currency = baseResponse.Currency;

            if (referenceItem.IsStockControl == true)
            {
                ViewBag.ItemStock = _myItemService.GetStockItem(referenceItem.ItemId);
            }
            else
            {
                ViewBag.ItemStock = null;
            }
            ViewBag.DesignServiceUrl = Utils.GetAppBasePath();

            Estimate userOrder = _orderService.GetOrderByID(OrderId);
            if (userOrder != null)
            {
                ViewBag.Order = userOrder;
            }
            else 
            {
                userOrder = _orderService.GetOrderByID(UserCookieManager.WEBOrderId);
                ViewBag.Order = userOrder;
            }

            

            PriceMatrixObjectList = null;
            AddonObjectList = null;


            ItemViewModel ItemModel = new ItemViewModel();
            ItemModel.File1 = referenceItem.File1;
            ItemModel.File2 = referenceItem.File2;
            ItemModel.File3 = referenceItem.File3;
            ItemModel.File4 = referenceItem.File4;
            ItemModel.GridImage = referenceItem.GridImage;
            ItemModel.IsQtyRanged = referenceItem.IsQtyRanged ?? false;

            ItemModel.ItemPriceMatrices = referenceItem.ItemPriceMatrices.ToList();
            ItemModel.ProductName = referenceItem.ProductName;
            ItemModel.ProductFriendlyName = referenceItem.Title;
            ItemModel.WebDescription = referenceItem.WebDescription;
            ItemModel.ItemId = referenceItem.ItemId;
            ItemModel.ProductDisplayOptions = referenceItem.ProductDisplayOptions;
            ItemModel.ProductSpecification = referenceItem.ProductSpecification;
            ItemModel.TipsAndHints = referenceItem.TipsAndHints;
            if (ViewData["Templates"] == null)
            {
                ItemModel.isUploadImage = referenceItem.IsUploadImage == true ? 1 : 0;
            }
            else
            {
                ItemModel.isUploadImage = 0;
            }
            if (referenceItem.ProductDisplayOptions == (int)ProductDisplayOption.ThumbWithMultipleBanners)
            {
                List<ItemImage> bannersList = _myItemService.getItemImagesByItemID(referenceItem.ItemId);
                ItemModel.ProductBannerThumbnailList = bannersList;
            }
            else if (referenceItem.ProductDisplayOptions == (int)ProductDisplayOption.ThumbAndBanner)
            {
                ItemModel.ProductBannerThumbnail = referenceItem.ImagePath;
            }
            if (!string.IsNullOrEmpty(ItemModel.File1))
            {
                string FileExtension = System.IO.Path.GetExtension(ItemModel.File1);
                if (FileExtension == ".ai")
                {

                    ItemModel.File1Url = "/Content/Images/IcoIllustrator.png";
                }
                else if (FileExtension == ".jpg")
                {
                    ItemModel.File1Url = "/Content/Images/icoJPG.png";
                }
                else if (FileExtension == ".png")
                {
                    ItemModel.File1Url = "/Content/Images/icoPNG.png";
                }
                else if (FileExtension == ".psd")
                {
                    ItemModel.File1Url = "/Content/Images/IcoPhotoshop.png";
                }
                else if (FileExtension == ".indd" || FileExtension == ".ind")
                {
                    ItemModel.File1Url = "/Content/Images/Icoindesign.png";
                }
                else if (FileExtension == ".pdf")
                {
                    ItemModel.File1Url = "/Content/Images/Page_pdf.png";
                }
                else
                {
                    ItemModel.File1Url = "/Content/download.png";
                }

            }
            if (!string.IsNullOrEmpty(ItemModel.File2))
            {
                string FileExtension = System.IO.Path.GetExtension(ItemModel.File2);
                if (FileExtension == ".ai")
                {

                    ItemModel.File2Url = "/Content/Images/IcoIllustrator.png";
                }
                else if (FileExtension == ".jpg")
                {
                    ItemModel.File2Url = "/Content/Images/icoJPG.png";
                }
                else if (FileExtension == ".png")
                {
                    ItemModel.File2Url = "/Content/Images/icoPNG.png";
                }
                else if (FileExtension == ".psd")
                {
                    ItemModel.File2Url = "/Content/Images/IcoPhotoshop.png";
                }
                else if (FileExtension == ".indd" || FileExtension == ".ind")
                {
                    ItemModel.File2Url = "/Content/Images/Icoindesign.png";
                }
                else if (FileExtension == ".pdf")
                {
                    ItemModel.File2Url = "/Content/Images/Page_pdf.png";
                }
                else
                {
                    ItemModel.File2Url = "/Content/download.png";
                }

            }
            if (!string.IsNullOrEmpty(ItemModel.File3))
            {
                string FileExtension = System.IO.Path.GetExtension(ItemModel.File3);
                if (FileExtension == ".ai")
                {

                    ItemModel.File3Url = "/Content/Images/IcoIllustrator.png";
                }
                else if (FileExtension == ".jpg")
                {
                    ItemModel.File3Url = "/Content/Images/icoJPG.png";
                }
                else if (FileExtension == ".png")
                {
                    ItemModel.File3Url = "/Content/Images/icoPNG.png";
                }
                else if (FileExtension == ".psd")
                {
                    ItemModel.File3Url = "/Content/Images/IcoPhotoshop.png";
                }
                else if (FileExtension == ".indd" || FileExtension == ".ind")
                {
                    ItemModel.File3Url = "/Content/Images/Icoindesign.png";
                }
                else if (FileExtension == ".pdf")
                {
                    ItemModel.File3Url = "/Content/Images/Page_pdf.png";
                }
                else
                {
                    ItemModel.File3Url = "/Content/download.png";
                }
            }
            if (!string.IsNullOrEmpty(ItemModel.File4))
            {
                string FileExtension = System.IO.Path.GetExtension(ItemModel.File4);
                if (FileExtension == ".ai")
                {

                    ItemModel.File4Url = "/Content/Images/IcoIllustrator.png";
                }
                else if (FileExtension == ".jpg")
                {
                    ItemModel.File4Url = "/Content/Images/icoJPG.png";
                }
                else if (FileExtension == ".png")
                {
                    ItemModel.File4Url = "/Content/Images/icoPNG.png";
                }
                else if (FileExtension == ".psd")
                {
                    ItemModel.File4Url = "/Content/Images/IcoPhotoshop.png";
                }
                else if (FileExtension == ".indd" || FileExtension == ".ind")
                {
                    ItemModel.File4Url = "/Content/Images/Icoindesign.png";
                }
                else if (FileExtension == ".pdf")
                {
                    ItemModel.File4Url = "/Content/Images/Page_pdf.png";
                }
                else
                {
                    ItemModel.File4Url = "/Content/download.png";
                }
            }
            if (!string.IsNullOrEmpty(ItemModel.File5))
            {
                string FileExtension = System.IO.Path.GetExtension(ItemModel.File5);
                if (FileExtension == ".ai")
                {

                    ItemModel.File5Url = "/Content/Images/IcoIllustrator.png";
                }
                else if (FileExtension == ".jpg")
                {
                    ItemModel.File5Url = "/Content/Images/icoJPG.png";
                }
                else if (FileExtension == ".png")
                {
                    ItemModel.File5Url = "/Content/Images/icoPNG.png";
                }
                else if (FileExtension == ".psd")
                {
                    ItemModel.File5Url = "/Content/Images/IcoPhotoshop.png";
                }
                else if (FileExtension == ".indd" || FileExtension == ".ind")
                {
                    ItemModel.File5Url = "/Content/Images/Icoindesign.png";
                }
                else if (FileExtension == ".pdf")
                {
                    ItemModel.File5Url = "/Content/Images/Page_pdf.png";
                }
                else
                {
                    ItemModel.File5Url = "/Content/download.png";
                }
            }
            ViewBag.ItemModel = ItemModel;
            ViewBag.CategoryName = _myItemService.GetCategoryNameById(0, ReferenceItemId);
            ViewBag.CategoryHRef = "/Category/" + Utils.specialCharactersEncoder(ViewBag.CategoryName) + "/" + _myItemService.GetCategoryIdByItemId(ReferenceItemId);

            SetPageMEtaTitle(referenceItem.ProductName, referenceItem.MetaDescription, referenceItem.MetaKeywords, referenceItem.MetaTitle, StoreBaseResopnse);

            ViewBag.IsShowPrices = _myCompanyService.ShowPricesOnStore(UserCookieManager.WEBStoreMode, StoreBaseResopnse.Company.ShowPrices ?? false, _myClaimHelper.loginContactID(), UserCookieManager.ShowPriceOnWebstore);

            referenceItem = null;
        }
        private void BindTemplatesList(long TemplateId, List<ItemAttachment> attachmentList, long ItemId, int DesignerCategoryId, string ProductName, int isTemplateDesignMode)
        {
            List<TemplateViewData> Templates = new List<TemplateViewData>();
            Template Template = _template.GetTemplate(TemplateId);// _templatePages.GetTemplatePages(TemplateId).ToList();
            if (attachmentList != null && attachmentList.Count > 2)
            {
                attachmentList = attachmentList.Take(2).ToList();
            }

            TemplateViewData objTemplate = null;

            foreach (var attach in attachmentList)
            {
                objTemplate = new TemplateViewData();
                if (UserCookieManager.WEBStoreMode == (int)StoreMode.Retail && _myClaimHelper.isUserLoggedIn() == false)
                {
                    if (UserCookieManager.TemporaryCompanyId > 0)
                    {
                        objTemplate.ContactId = _myCompanyService.GetContactIdByCompanyId(UserCookieManager.TemporaryCompanyId); ;
                        objTemplate.CustomerId = UserCookieManager.TemporaryCompanyId;
                    }
                }
                else
                {
                    objTemplate.ContactId = _myClaimHelper.loginContactID();
                    objTemplate.CustomerId = _myClaimHelper.loginContactCompanyID();
                }

                objTemplate.TemplateId = Template.ProductId;
                objTemplate.TemplateName = objTemplate.TemplateName == null ? Utils.specialCharactersEncoder(ProductName) : Utils.specialCharactersEncoder(Template.ProductName);
                objTemplate.ItemId = ItemId;
                objTemplate.FileName = attach.FileName;
                objTemplate.FolderPath = attach.FolderPath;
                objTemplate.OrganisationID = UserCookieManager.WEBOrganisationID;
                objTemplate.CategoryId = DesignerCategoryId;
                objTemplate.printWaterMark = true;
                if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                    objTemplate.isCalledFrom = 4;
                else
                    objTemplate.isCalledFrom = 3;

                if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp || UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)
                {
                    objTemplate.isEmbedded = true;
                }
                else
                {

                    objTemplate.isEmbedded = false;
                    objTemplate.printWaterMark = false;
                }

                objTemplate.printCropMarks = true;
                objTemplate.isTemplateDesignMode = isTemplateDesignMode;
                Templates.Add(objTemplate);
            }

            ViewData["Templates"] = Templates.ToList();
            Template = null;
            Templates = null;
        }
        private void BindArtworkAttachmentsList(long ItemId)
        {

            ViewData["ArtworkAttachments"] = _myItemService.GetArtwork(Convert.ToInt64(ItemId)).ToList();

        }

        /// <summary>
        /// to dispaly the meta titles of page
        /// </summary>
        /// <param name="CatName"></param>
        /// <param name="CatDes"></param>
        /// <param name="Keywords"></param>
        /// <param name="Title"></param>
        /// <param name="baseResponse"></param>
        private void SetPageMEtaTitle(string CatName, string CatDes, string Keywords, string Title, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse baseResponse)
        {
            string[] MetaTags = _myCompanyService.CreatePageMetaTags(Title == null ? "" : Title, CatDes == null ? "" : CatDes, Keywords == null ? "" : Keywords, baseResponse.Company.Name, baseResponse.StoreDetaultAddress);

            TempData["MetaTitle"] = MetaTags[0];
            TempData.Keep("MetaTitle");
            //ViewBag.MetaTitle  = MetaTags[0];
            TempData["MetaKeywords"] = MetaTags[1];
            TempData.Keep("MetaKeywords");
            //ViewBag.MetaKeywords = MetaTags[1];
            TempData["MetaDescription"] = MetaTags[2];
            TempData.Keep("MetaDescription");

        }


        private Item CloneItemAndUpdateCookie(MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse, long ItemId, long OrderID)
        {

            Item clonedItem = null;
            // create new order

            if (OrderID > 0)
            {

                UserCookieManager.WEBOrderId = OrderID;
                // gets the item from reference item id in case of upload design when user process the item but not add the item in cart
                clonedItem = _myItemService.GetExisitingClonedItemInOrder(OrderID, ItemId);

                if (clonedItem == null)
                {
                    clonedItem = _myItemService.CloneItem(ItemId, 0, OrderID, UserCookieManager.WBStoreId, 0, 0, null, false, false, _myClaimHelper.loginContactID(), StoreBaseResopnse.Organisation.OrganisationId, true);

                }


            }
            return clonedItem;
        }
    }


}