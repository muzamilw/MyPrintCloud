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
        public ActionResult Index(string CategoryId, string ItemId, string ItemMode, string TemplateId)
        {
            Item clonedItem = null;
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;
            long OrderID = 0;
            long referenceItemId = 0;
            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
            if (ItemMode == "UploadDesign")
            {

                if (UserCookieManager.WEBOrderId == 0 || _orderService.IsRealCustomerOrder(UserCookieManager.WEBOrderId, _myClaimHelper.loginContactID(), _myClaimHelper.loginContactCompanyID()) == false)
                {
                    long TemporaryRetailCompanyId = UserCookieManager.TemporaryCompanyId;

                    // create new order

                    // MyCompanyDomainBaseResponse organisationBaseResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromOrganisation();
                    OrderID = _orderService.ProcessPublicUserOrder(string.Empty, StoreBaseResopnse.Organisation.OrganisationId, (StoreMode)UserCookieManager.WEBStoreMode, _myClaimHelper.loginContactCompanyID(), _myClaimHelper.loginContactID(), ref TemporaryRetailCompanyId);
                    if (OrderID > 0)
                    {
                        UserCookieManager.TemporaryCompanyId = TemporaryRetailCompanyId;
                        UserCookieManager.WEBOrderId = OrderID;
                        // gets the item from reference item id in case of upload design when user process the item but not add the item in cart
                        clonedItem = _myItemService.GetExisitingClonedItemInOrder(OrderID, Convert.ToInt64(ItemId));

                        if (clonedItem == null)
                        {
                            clonedItem = _myItemService.CloneItem(Convert.ToInt64(ItemId), 0, OrderID, UserCookieManager.WBStoreId, 0, 0, null, false, false, _myClaimHelper.loginContactID(), StoreBaseResopnse.Organisation.OrganisationId);

                        }
                    }
                }
                else
                {
                    OrderID = UserCookieManager.WEBOrderId;
                    // gets the item from reference item id in case of upload design when user process the item but not add the item in cart
                    clonedItem = _myItemService.GetExisitingClonedItemInOrder(UserCookieManager.WEBOrderId, Convert.ToInt64(ItemId));

                    if (clonedItem == null)
                    {
                        clonedItem = _myItemService.CloneItem(Convert.ToInt64(ItemId), 0, UserCookieManager.WEBOrderId, UserCookieManager.WBStoreId, 0, 0, null, false, false, _myClaimHelper.loginContactID(), StoreBaseResopnse.Organisation.OrganisationId);
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
                    BindTemplatesList(Convert.ToInt64(TemplateId), clonedItem.ItemAttachments.ToList(), Convert.ToInt64(ItemId), clonedItem.DesignerCategoryId ?? 0, clonedItem.ProductName);
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
                BindTemplatesList(Convert.ToInt64(TemplateId), clonedItem.ItemAttachments == null ? null : clonedItem.ItemAttachments.ToList(), Convert.ToInt64(ItemId), Convert.ToInt32(clonedItem.DesignerCategoryId), clonedItem.ProductName);
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
            return View("PartialViews/ProductOptions");
        }

        [HttpPost]
        public ActionResult Index(ItemCartViewModel cartObject, string ReferenceItemId)
        {
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;
            var ITemMode = TempData["ItemMode"];
            string CostCentreJsonQueue = "";
            string QuestionJsonQueue = cartObject.JsonAllQuestionQueue;
            string InputJsonQueue = cartObject.JsonAllInputQueue;
            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
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

                    _myItemService.UpdateCloneItemService(Convert.ToInt64(cartObject.ItemId), Convert.ToDouble(cartObject.QuantityOrdered), Convert.ToDouble(cartObject.ItemPrice), Convert.ToDouble(cartObject.AddOnPrice), Convert.ToInt64(cartObject.StockId), ccObjectList, UserCookieManager.WEBStoreMode, Convert.ToInt64(StoreBaseResopnse.Company.OrganisationId), 0, Convert.ToString(ITemMode), false, Convert.ToInt64(cartObject.ItemStockOptionId), 0, QuestionJsonQueue, CostCentreJsonQueue, InputJsonQueue); // set files count
                }
                else
                {
                    if (Convert.ToDouble(StoreBaseResopnse.Company.TaxRate) > 0)
                    {
                        _myItemService.UpdateCloneItemService(Convert.ToInt64(cartObject.ItemId), Convert.ToDouble(cartObject.QuantityOrdered), Convert.ToDouble(cartObject.ItemPrice), Convert.ToDouble(cartObject.AddOnPrice), Convert.ToInt64(cartObject.StockId), ccObjectList, UserCookieManager.WEBStoreMode, Convert.ToInt64(StoreBaseResopnse.Company.OrganisationId), Convert.ToDouble(StoreBaseResopnse.Company.TaxRate), Convert.ToString(ITemMode), true, Convert.ToInt64(cartObject.ItemStockOptionId), 0, QuestionJsonQueue, CostCentreJsonQueue, InputJsonQueue); // set files count
                    }
                    else
                    {
                        _myItemService.UpdateCloneItemService(Convert.ToInt64(cartObject.ItemId), Convert.ToDouble(cartObject.QuantityOrdered), Convert.ToDouble(cartObject.ItemPrice), Convert.ToDouble(cartObject.AddOnPrice), Convert.ToInt64(cartObject.StockId), ccObjectList, UserCookieManager.WEBStoreMode, Convert.ToInt64(StoreBaseResopnse.Company.OrganisationId), 0, Convert.ToString(ITemMode), false, Convert.ToInt64(cartObject.ItemStockOptionId),0, QuestionJsonQueue, CostCentreJsonQueue, InputJsonQueue); // set files count
                    }
                }


                Response.Redirect("/ShopCart/" + UserCookieManager.WEBOrderId);

                return null;

            }
            else
            {
                DefaultSettings(Convert.ToInt64(ReferenceItemId), "", Convert.ToInt64(cartObject.ItemId), Convert.ToInt64(cartObject.OrderId), StoreBaseResopnse, false);

                return View("PartialViews/ProductOptions");
            }


        }

        private void DefaultSettings(long ReferenceItemId, string mode, long ClonedItemId, long OrderId, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse, bool isTemplateProduct)
        {

            List<ProductPriceMatrixViewModel> PriceMatrixObjectList = null;

            List<AddOnCostCenterViewModel> AddonObjectList = null;
            // get reference item, stocks, addons, price matrix
            Item referenceItem = _myItemService.GetItemById(Convert.ToInt64(ReferenceItemId));

            ViewData["StckOptions"] = _myItemService.GetStockList(Convert.ToInt64(ReferenceItemId), UserCookieManager.WBStoreId);

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
                        ViewBag.ShowUploadArkworkPanel = true;
                    }
                    else
                    {
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
                        ViewBag.ShowUploadArkworkPanel = true;
                    }
                    else
                    {
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


            foreach (var addOn in listOfCostCentres)
            {
                if (clonedSectionCostCentres != null && clonedSectionCostCentres.Count > 0)// this will run in case of modify mode and cost centres selected
                {
                    //List<QuestionQueueItem> objSettings = JsonConvert.DeserializeObject<List<QuestionQueueItem>>(QueueItem);
                    bool isAddedToList = false;
                    foreach (var cItem in clonedSectionCostCentres)
                    {
                        if (cItem.CostCentreId == addOn.CostCenterID)
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
                                isChecked = true,
                                QuantitySourceType = addOn.QuantitySourceType,
                                TimeSourceType = addOn.TimeSourceType
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
                            isChecked = false,
                            QuantitySourceType = addOn.QuantitySourceType,
                            TimeSourceType = addOn.TimeSourceType
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
                        isChecked = false,
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

            MyCompanyDomainBaseResponse baseResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.WBStoreId).CreateFromCurrency();
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

            ViewBag.Order = _orderService.GetOrderByID(OrderId);

            PriceMatrixObjectList = null;
            AddonObjectList = null;


            ItemViewModel ItemModel = new ItemViewModel();
            ItemModel.File1 = referenceItem.File1;
            ItemModel.File2 = referenceItem.File2;
            ItemModel.File3 = referenceItem.File3;
            ItemModel.File4 = referenceItem.File4;
            ItemModel.GridImage = referenceItem.GridImage;
            ItemModel.IsQtyRanged = referenceItem.IsQtyRanged ?? false;
            ItemModel.isUploadImage = referenceItem.IsUploadImage ?? false;
            ItemModel.ItemPriceMatrices = referenceItem.ItemPriceMatrices.ToList();
            ItemModel.ProductName = referenceItem.ProductName;
            ItemModel.WebDescription = referenceItem.WebDescription;
            ItemModel.ItemId = referenceItem.ItemId;
            ItemModel.Mode = ViewData["Templates"] == null ? "UploadDesign" : "Template";
            ViewBag.ItemModel = ItemModel;
            ViewBag.CategoryName = _myItemService.GetCategoryNameById(0, ReferenceItemId);
            ViewBag.CategoryHRef = "/Category/" + Utils.specialCharactersEncoder(ViewBag.CategoryName) + "/" + _myItemService.GetCategoryIdByItemId(ReferenceItemId);

            SetPageMEtaTitle(referenceItem.ProductName, referenceItem.MetaDescription, referenceItem.MetaKeywords, referenceItem.MetaTitle, StoreBaseResopnse);

            referenceItem = null;
        }
        private void BindTemplatesList(long TemplateId, List<ItemAttachment> attachmentList, long ItemId, int DesignerCategoryId, string ProductName)
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

    }


}