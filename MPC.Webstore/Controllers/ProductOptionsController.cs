﻿using System;
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

        #endregion


        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ProductOptionsController(IItemService myItemService, ICompanyService myCompanyService,
            IWebstoreClaimsHelperService webstoreAuthorizationChecker, IOrderService orderService, IWebstoreClaimsHelperService myClaimHelper)
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
            this._myItemService = myItemService;
            this._myCompanyService = myCompanyService;
            this._webstoreAuthorizationChecker = webstoreAuthorizationChecker;
            this._orderService = orderService;
            this._myClaimHelper = myClaimHelper;
        }

        #endregion
        // GET: ProductOptions
        public ActionResult Index(string Cid, string Itemid, string Mode)
        {

            if (Mode == "UploadDesign")
            {
                Item clonedItem = null;
                if (UserCookieManager.OrderId == 0)
                {
                    // create new order
                    MyCompanyDomainBaseResponse organisationBaseResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromOrganisation();
                    long OrderID = _orderService.ProcessPublicUserOrder(string.Empty, organisationBaseResponse.Organisation.OrganisationId, (int)UserCookieManager.StoreMode, UserCookieManager.StoreId);
                    if (OrderID > 0)
                    {
                        UserCookieManager.OrderId = OrderID;
                        clonedItem = _myItemService.CloneItem(Convert.ToInt64(Itemid), 0, OrderID, UserCookieManager.StoreId, 0, 0, null, false, false, _myClaimHelper.loginContactID());
                    }
                }
                else
                {
                    clonedItem = _myItemService.CloneItem(Convert.ToInt64(Itemid), 0, UserCookieManager.OrderId, UserCookieManager.StoreId, 0, 0, null, false, false, _myClaimHelper.loginContactID());
                }

                ViewBag.ClonedItemId = clonedItem.ItemId;
            }

            DefaultSettings(Itemid);

            return View("PartialViews/ProductOptions");
        }

        [HttpPost]
        public ActionResult Index(ItemCartViewModel cartObject, string ReferenceItemId)
        {
            if (!string.IsNullOrEmpty(cartObject.ItemPrice) || !string.IsNullOrEmpty(cartObject.JsonPriceMatrix) || !string.IsNullOrEmpty(cartObject.StockId))
            {
                MyCompanyDomainBaseResponse baseResponseCompany = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();

                ProductPriceMatrixViewModel selectedPriceMatrix = JsonConvert.DeserializeObject<ProductPriceMatrixViewModel>(cartObject.JsonPriceMatrix);


                AddOnCostCenterViewModel selectedAddOns = JsonConvert.DeserializeObject<AddOnCostCenterViewModel>(cartObject.JsonPriceMatrix);

                List<AddOnCostsCenter> ccObjectList = new List<AddOnCostsCenter>();

                AddOnCostsCenter ccObject = null;
                //foreach (var addOn in selectedAddOns)
                //{
                //    ccObject = new AddOnCostsCenter();

                //    if (addOn.Type == 2) //per quantity
                //    {
                //        ccObject.Qty1NetTotal = (Convert.ToDouble(cartObject.QuantityOrdered) * addOn.ActualPrice) + addOn.SetupCost;
                //        if (ccObject.Qty1NetTotal < addOn.MinimumCost && addOn.MinimumCost != 0)
                //        {
                //            ccObject.Qty1NetTotal = addOn.MinimumCost;
                //        }
                //    }
                //    else
                //    {
                //        ccObject.Qty1NetTotal = addOn.ActualPrice;
                //    }
                //    ccObjectList.Add(ccObject);
                //}


                if (true) // upload design
                {
                    if (false) // calculate tax by service
                    {
                        _myItemService.UpdateCloneItem(Convert.ToInt64(cartObject.ItemId), Convert.ToDouble(cartObject.QuantityOrdered), Convert.ToDouble(cartObject.ItemPrice), Convert.ToDouble(cartObject.AddOnPrice), Convert.ToInt64(cartObject.StockId), ccObjectList, UserCookieManager.StoreMode, Convert.ToInt64(baseResponseCompany.Company.OrganisationId), 0, 0); // set files count
                    }
                    else
                    {
                        _myItemService.UpdateCloneItem(Convert.ToInt64(cartObject.ItemId), Convert.ToDouble(cartObject.QuantityOrdered), Convert.ToDouble(cartObject.ItemPrice), Convert.ToDouble(cartObject.AddOnPrice), Convert.ToInt64(cartObject.StockId), ccObjectList, UserCookieManager.StoreMode, Convert.ToInt64(baseResponseCompany.Company.OrganisationId), baseResponseCompany.Company.TaxRate ?? 0, 0); // set files count
                    }
                }
                else
                {
                    _myItemService.UpdateCloneItem(Convert.ToInt64(cartObject.ItemId), Convert.ToDouble(cartObject.QuantityOrdered), Convert.ToDouble(cartObject.ItemPrice), Convert.ToDouble(cartObject.AddOnPrice), Convert.ToInt64(cartObject.StockId), ccObjectList, UserCookieManager.StoreMode, Convert.ToInt64(baseResponseCompany.Company.OrganisationId), baseResponseCompany.Company.TaxRate ?? 0);
                }

                ControllerContext.HttpContext.Response.Redirect("/ShopCart");
                return null;
            }
            else
            {
                DefaultSettings(ReferenceItemId);

                return View("PartialViews/ProductOptions");
            }

            
        }

        private void DefaultSettings(string Itemid)
        {
            List<ProductPriceMatrixViewModel> PriceMatrixObjectList = null;

            List<AddOnCostCenterViewModel> AddonObjectList = null;
            // get reference item, stocks, addons, price matrix
            Item referenceItem = _myItemService.GetItemById(Convert.ToInt64(Itemid));

            ViewData["StckOptions"] = _myItemService.GetStockList(Convert.ToInt64(Itemid), UserCookieManager.StoreId);

            List<AddOnCostsCenter> listOfCostCentres = _myItemService.GetStockOptionCostCentres(Convert.ToInt64(Itemid), UserCookieManager.StoreId);

            ViewData["CostCenters"] = listOfCostCentres;

            AddonObjectList = new List<AddOnCostCenterViewModel>();

            foreach (var addOn in listOfCostCentres)
            {
                AddOnCostCenterViewModel addOnsObject = new AddOnCostCenterViewModel
                {
                    Id = addOn.ProductAddOnID,
                    Type = addOn.Type,
                    SetupCost = addOn.SetupCost,
                    MinimumCost = addOn.MinimumCost,
                    ActualPrice = addOn.AddOnPrice ?? 0.0
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
            PriceMatrixObjectList = null;
            AddonObjectList = null;

            ViewBag.Item = referenceItem;
        }

    }
}