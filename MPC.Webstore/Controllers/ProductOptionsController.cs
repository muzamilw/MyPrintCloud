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

        private readonly ITemplatePageService _templatePages;

        #endregion


        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ProductOptionsController(IItemService myItemService, ICompanyService myCompanyService,
            IWebstoreClaimsHelperService webstoreAuthorizationChecker, IOrderService orderService, IWebstoreClaimsHelperService myClaimHelper
            , ITemplatePageService templatePages)
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

            if (templatePages == null)
            {
                throw new ArgumentNullException("templatePages");
            }
            this._myItemService = myItemService;
            this._myCompanyService = myCompanyService;
            this._webstoreAuthorizationChecker = webstoreAuthorizationChecker;
            this._orderService = orderService;
            this._myClaimHelper = myClaimHelper;
            this._templatePages = templatePages;
        }

        #endregion
        // GET: ProductOptions
        public ActionResult Index(string CategoryId, string ItemId, string ItemMode, string TemplateId)
        {
            Item clonedItem = null;

            if (ItemMode == "UploadDesign")
            {
                if (UserCookieManager.OrderId == 0)
                {
                    long TemporaryRetailCompanyId = UserCookieManager.TemporaryCompanyId;

                    // create new order
                    MyCompanyDomainBaseResponse organisationBaseResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromOrganisation();
                    long OrderID = _orderService.ProcessPublicUserOrder(string.Empty, organisationBaseResponse.Organisation.OrganisationId, (int)UserCookieManager.StoreMode, _myClaimHelper.loginContactCompanyID(), _myClaimHelper.loginContactID(), ref TemporaryRetailCompanyId);
                    if (OrderID > 0)
                    {
                        UserCookieManager.TemporaryCompanyId = TemporaryRetailCompanyId;
                        UserCookieManager.OrderId = OrderID;
                        clonedItem = _myItemService.CloneItem(Convert.ToInt64(ItemId), 0, OrderID, UserCookieManager.StoreId, 0, 0, null, false, false, _myClaimHelper.loginContactID());
                    }
                }
                else
                {
                    // gets the item from reference item id in case of upload design when user process the item but not add the item in cart
                    clonedItem = _myItemService.GetExisitingClonedItemInOrder(UserCookieManager.OrderId, Convert.ToInt64(ItemId));
                    if (clonedItem == null)
                    {
                        clonedItem = _myItemService.CloneItem(Convert.ToInt64(ItemId), 0, UserCookieManager.OrderId, UserCookieManager.StoreId, 0, 0, null, false, false, _myClaimHelper.loginContactID());
                    }
                }
            }
            else if (ItemMode == "Modify")// template case
            {
                clonedItem = _myItemService.GetItemById(Convert.ToInt64(ItemId));
                if (!string.IsNullOrEmpty(TemplateId))
                {
                    BindTemplatesList(Convert.ToInt64(TemplateId));
                }
                else 
                {
                    BindArtworkAttachmentsList(Convert.ToInt64(ItemId));
                }

                ViewBag.SelectedStockItemId = clonedItem.ItemSections.FirstOrDefault().StockItemID1;
                ViewBag.SelectedQuantity = clonedItem.Qty1;

            }
            else if (!string.IsNullOrEmpty(TemplateId))// template case
            {
                clonedItem = _myItemService.GetItemById(Convert.ToInt64(ItemId));
                BindTemplatesList(Convert.ToInt64(TemplateId));
            }

            ViewBag.ClonedItemId = clonedItem.ItemId;

            ViewBag.ClonedItem = clonedItem;

            DefaultSettings(ItemId);

            return View("PartialViews/ProductOptions");
        }

        [HttpPost]
        public ActionResult Index(ItemCartViewModel cartObject, string ReferenceItemId)
        {
            if (!string.IsNullOrEmpty(cartObject.ItemPrice) || !string.IsNullOrEmpty(cartObject.JsonPriceMatrix) || !string.IsNullOrEmpty(cartObject.StockId))
            {
                MyCompanyDomainBaseResponse baseResponseCompany = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();

                List<AddOnCostCenterViewModel> selectedAddOnsList = JsonConvert.DeserializeObject<List<AddOnCostCenterViewModel>>(cartObject.JsonAddOnsPrice);

                List<AddOnCostsCenter> ccObjectList = new List<AddOnCostsCenter>();

                AddOnCostsCenter ccObject = null;

                foreach (var addOn in selectedAddOnsList)
                {
                    ccObject = new AddOnCostsCenter();
                    ccObject.CostCenterID = addOn.CostCenterId;
                    if (addOn.Type == 2) //per quantity
                    {
                        ccObject.Qty1NetTotal = (Convert.ToDouble(cartObject.QuantityOrdered) * addOn.ActualPrice) + addOn.SetupCost;
                        if (ccObject.Qty1NetTotal < addOn.MinimumCost && addOn.MinimumCost != 0)
                        {
                            ccObject.Qty1NetTotal = addOn.MinimumCost;
                        }
                    }
                    else
                    {
                        ccObject.Qty1NetTotal = addOn.ActualPrice;
                    }
                    ccObjectList.Add(ccObject);
                }


                if (true) // upload design
                {
                    if (false) // calculate tax by service
                    {
                        _myItemService.UpdateCloneItemService(Convert.ToInt64(cartObject.ItemId), Convert.ToDouble(cartObject.QuantityOrdered), Convert.ToDouble(cartObject.ItemPrice), Convert.ToDouble(cartObject.AddOnPrice), Convert.ToInt64(cartObject.StockId), ccObjectList, UserCookieManager.StoreMode, Convert.ToInt64(baseResponseCompany.Company.OrganisationId), 0, 0); // set files count
                    }
                    else
                    {
                        _myItemService.UpdateCloneItemService(Convert.ToInt64(cartObject.ItemId), Convert.ToDouble(cartObject.QuantityOrdered), Convert.ToDouble(cartObject.ItemPrice), Convert.ToDouble(cartObject.AddOnPrice), Convert.ToInt64(cartObject.StockId), ccObjectList, UserCookieManager.StoreMode, Convert.ToInt64(baseResponseCompany.Company.OrganisationId), baseResponseCompany.Company.TaxRate ?? 0, 0); // set files count
                    }
                }
                else
                {
                    _myItemService.UpdateCloneItemService(Convert.ToInt64(cartObject.ItemId), Convert.ToDouble(cartObject.QuantityOrdered), Convert.ToDouble(cartObject.ItemPrice), Convert.ToDouble(cartObject.AddOnPrice), Convert.ToInt64(cartObject.StockId), ccObjectList, UserCookieManager.StoreMode, Convert.ToInt64(baseResponseCompany.Company.OrganisationId), baseResponseCompany.Company.TaxRate ?? 0);
                }
                selectedAddOnsList = null;
                ccObjectList = null;
                ccObject = null;
                Response.Redirect("/ShopCart/" + UserCookieManager.OrderId);

                return null;

            }
            else
            {
                DefaultSettings(ReferenceItemId);

                return View("PartialViews/ProductOptions");
            }


        }

        private void DefaultSettings(string ReferenceItemId)
        {
            List<ProductPriceMatrixViewModel> PriceMatrixObjectList = null;

            List<AddOnCostCenterViewModel> AddonObjectList = null;
            // get reference item, stocks, addons, price matrix
            Item referenceItem = _myItemService.GetItemById(Convert.ToInt64(ReferenceItemId));

            ViewData["StckOptions"] = _myItemService.GetStockList(Convert.ToInt64(ReferenceItemId), UserCookieManager.StoreId);

            List<AddOnCostsCenter> listOfCostCentres = _myItemService.GetStockOptionCostCentres(Convert.ToInt64(ReferenceItemId), UserCookieManager.StoreId);

            ViewData["CostCenters"] = listOfCostCentres;

            AddonObjectList = new List<AddOnCostCenterViewModel>();

            foreach (var addOn in listOfCostCentres)
            {
                AddOnCostCenterViewModel addOnsObject = new AddOnCostCenterViewModel
                {
                    Id = addOn.ProductAddOnID,
                    CostCenterId = addOn.CostCenterID,
                    Type = addOn.Type,
                    SetupCost = addOn.SetupCost,
                    MinimumCost = addOn.MinimumCost,
                    ActualPrice = addOn.AddOnPrice ?? 0.0,
                    StockOptionId = addOn.ItemStockId
                };
                AddonObjectList.Add(addOnsObject);
            }

            ViewBag.JsonAddonCostCentre = AddonObjectList;

            if (_webstoreAuthorizationChecker.isUserLoggedIn())
            {
                referenceItem.ItemPriceMatrices = _myItemService.GetPriceMatrix(referenceItem.ItemPriceMatrices.ToList(), referenceItem.IsQtyRanged ?? false, true, UserCookieManager.StoreId);
            }
            else
            {
                referenceItem.ItemPriceMatrices = _myItemService.GetPriceMatrix(referenceItem.ItemPriceMatrices.ToList(), referenceItem.IsQtyRanged ?? false, false, 0);
            }

            PriceMatrixObjectList = new List<ProductPriceMatrixViewModel>();
            foreach (var matrixItem in referenceItem.ItemPriceMatrices)
            {
                if (UserCookieManager.StoreMode == (int)StoreMode.Retail)
                {
                    if (UserCookieManager.isIncludeTax)
                    {
                        if (referenceItem.DefaultItemTax != null)
                        {
                            matrixItem.PricePaperType1 = Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PricePaperType1, CultureInfo.CurrentCulture), Convert.ToDouble(referenceItem.DefaultItemTax));
                            matrixItem.PricePaperType2 = Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PricePaperType2, CultureInfo.CurrentCulture), Convert.ToDouble(referenceItem.DefaultItemTax));
                            matrixItem.PricePaperType3 = Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PricePaperType3, CultureInfo.CurrentCulture), Convert.ToDouble(referenceItem.DefaultItemTax));
                            matrixItem.PriceStockType4 = matrixItem.PriceStockType4 == null ? 0 : Utils.CalculateTaxOnPrice(matrixItem.PriceStockType4 ?? 0, Convert.ToDouble(referenceItem.DefaultItemTax));
                            matrixItem.PriceStockType5 = matrixItem.PriceStockType5 == null ? 0 : Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PriceStockType5), Convert.ToDouble(referenceItem.DefaultItemTax));
                            matrixItem.PriceStockType6 = matrixItem.PriceStockType6 == null ? 0 : Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PriceStockType6), Convert.ToDouble(referenceItem.DefaultItemTax));
                            matrixItem.PriceStockType7 = matrixItem.PriceStockType7 == null ? 0 : Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PriceStockType7), Convert.ToDouble(referenceItem.DefaultItemTax));
                            matrixItem.PriceStockType8 = matrixItem.PriceStockType8 == null ? 0 : Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PriceStockType8), Convert.ToDouble(referenceItem.DefaultItemTax));
                            matrixItem.PriceStockType9 = matrixItem.PriceStockType9 == null ? 0 : Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PriceStockType9), Convert.ToDouble(referenceItem.DefaultItemTax));
                            matrixItem.PriceStockType10 = matrixItem.PriceStockType10 == null ? 0 : Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PriceStockType10), Convert.ToDouble(referenceItem.DefaultItemTax));
                            matrixItem.PriceStockType11 = matrixItem.PriceStockType11 == null ? 0 : Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PriceStockType11), Convert.ToDouble(referenceItem.DefaultItemTax));

                        }
                        else
                        {
                            matrixItem.PricePaperType1 = Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PricePaperType1, CultureInfo.CurrentCulture), UserCookieManager.TaxRate);
                            matrixItem.PricePaperType2 = Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PricePaperType2, CultureInfo.CurrentCulture), UserCookieManager.TaxRate);
                            matrixItem.PricePaperType3 = Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PricePaperType3, CultureInfo.CurrentCulture), UserCookieManager.TaxRate);
                            matrixItem.PriceStockType4 = matrixItem.PriceStockType4 == null ? 0 : Utils.CalculateTaxOnPrice(matrixItem.PriceStockType4 ?? 0, UserCookieManager.TaxRate);
                            matrixItem.PriceStockType5 = matrixItem.PriceStockType5 == null ? 0 : Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PriceStockType5), UserCookieManager.TaxRate);
                            matrixItem.PriceStockType6 = matrixItem.PriceStockType6 == null ? 0 : Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PriceStockType6), UserCookieManager.TaxRate);
                            matrixItem.PriceStockType7 = matrixItem.PriceStockType7 == null ? 0 : Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PriceStockType7), UserCookieManager.TaxRate);
                            matrixItem.PriceStockType8 = matrixItem.PriceStockType8 == null ? 0 : Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PriceStockType8), UserCookieManager.TaxRate);
                            matrixItem.PriceStockType9 = matrixItem.PriceStockType9 == null ? 0 : Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PriceStockType9), UserCookieManager.TaxRate);
                            matrixItem.PriceStockType10 = matrixItem.PriceStockType10 == null ? 0 : Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PriceStockType10), UserCookieManager.TaxRate);
                            matrixItem.PriceStockType11 = matrixItem.PriceStockType11 == null ? 0 : Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PriceStockType11), UserCookieManager.TaxRate);

                        }

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
                }
                else
                {
                    if (UserCookieManager.isIncludeTax)
                    {
                        matrixItem.PricePaperType1 = Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PricePaperType1, CultureInfo.CurrentCulture), UserCookieManager.TaxRate);
                        matrixItem.PricePaperType2 = Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PricePaperType2, CultureInfo.CurrentCulture), UserCookieManager.TaxRate);
                        matrixItem.PricePaperType3 = Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PricePaperType3, CultureInfo.CurrentCulture), UserCookieManager.TaxRate);
                        matrixItem.PriceStockType4 = matrixItem.PriceStockType4 == null ? 0 : Utils.CalculateTaxOnPrice(matrixItem.PriceStockType4 ?? 0, UserCookieManager.TaxRate);
                        matrixItem.PriceStockType5 = matrixItem.PriceStockType5 == null ? 0 : Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PriceStockType5), UserCookieManager.TaxRate);
                        matrixItem.PriceStockType6 = matrixItem.PriceStockType6 == null ? 0 : Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PriceStockType6), UserCookieManager.TaxRate);
                        matrixItem.PriceStockType7 = matrixItem.PriceStockType7 == null ? 0 : Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PriceStockType7), UserCookieManager.TaxRate);
                        matrixItem.PriceStockType8 = matrixItem.PriceStockType8 == null ? 0 : Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PriceStockType8), UserCookieManager.TaxRate);
                        matrixItem.PriceStockType9 = matrixItem.PriceStockType9 == null ? 0 : Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PriceStockType9), UserCookieManager.TaxRate);
                        matrixItem.PriceStockType10 = matrixItem.PriceStockType10 == null ? 0 : Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PriceStockType10), UserCookieManager.TaxRate);
                        matrixItem.PriceStockType11 = matrixItem.PriceStockType11 == null ? 0 : Utils.CalculateTaxOnPrice(Convert.ToDouble(matrixItem.PriceStockType11), UserCookieManager.TaxRate);

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

            MyCompanyDomainBaseResponse baseResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCurrency();
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

            ViewBag.Order = _orderService.GetOrderByID(UserCookieManager.OrderId);

            PriceMatrixObjectList = null;
            AddonObjectList = null;

            ViewBag.Item = referenceItem;
        }
        private void BindTemplatesList(long TemplateId) 
        {
            List<TemplatePage> TemplatePagesList = _templatePages.GetTemplatePages(TemplateId).ToList();
            if (TemplatePagesList != null && TemplatePagesList.Count > 2)
            {
                TemplatePagesList = TemplatePagesList.Take(2).ToList();
            }

            ViewData["Templates"] = TemplatePagesList.ToList();
            TemplatePagesList = null;
        }
        private void BindArtworkAttachmentsList(long ItemId)
        {

            ViewData["ArtworkAttachments"] = _myItemService.GetArtwork(Convert.ToInt64(ItemId)).ToList();
            
        }
    }
}