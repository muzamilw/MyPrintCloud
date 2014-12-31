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

namespace MPC.Webstore.Controllers
{
    public class ProductOptionsController : Controller
    {
         #region Private

        private readonly ICompanyService _myCompanyService;

        private readonly  IItemService _myItemService;

        private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;

        #endregion


        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ProductOptionsController(IItemService myItemService, ICompanyService myCompanyService, IWebstoreClaimsHelperService webstoreAuthorizationChecker)
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
            this._myItemService = myItemService;
            this._myCompanyService = myCompanyService;
            this._webstoreAuthorizationChecker = webstoreAuthorizationChecker;
        }

        #endregion
        // GET: ProductOptions
        public ActionResult Index(string Cid, string Itemid, string Mode)
        {
            // add falg idd

            List<ProductPriceMatrixViewModel> jasonObjectList = new List<ProductPriceMatrixViewModel>();
            Item referenceItem = _myItemService.GetItemById(Convert.ToInt64(Itemid));
            ViewData["StckOptions"] = _myItemService.GetStockList(Convert.ToInt64(Itemid), UserCookieManager.StoreId);
          
            if(_webstoreAuthorizationChecker.isUserLoggedIn())
            {
                referenceItem.ItemPriceMatrices = _myItemService.GetPriceMatrix(referenceItem.ItemPriceMatrices.ToList(), referenceItem.IsQtyRanged ?? false, true, UserCookieManager.StoreId);
            }
            else
            {
                referenceItem.ItemPriceMatrices = _myItemService.GetPriceMatrix(referenceItem.ItemPriceMatrices.ToList(), referenceItem.IsQtyRanged ?? false, false, 0);
            }

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
                    jasonObjectList.Add(priceObject);
                    
                }
                else
                {
                    //json object
                    ProductPriceMatrixViewModel priceObject = new ProductPriceMatrixViewModel
                    {
                        ItemID = Convert.ToInt32(matrixItem.PriceMatrixId),
                        Quantity = matrixItem.Quantity.ToString()
                    };
                    jasonObjectList.Add(priceObject);
                }
            }

            ViewBag.jasonObjectList = jasonObjectList;

            MyCompanyDomainBaseResponse baseResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCurrency();
            ViewBag.Currency =  baseResponse.Currency;

            return View("PartialViews/ProductOptions", referenceItem);
        }
    }
}