using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Webstore.ModelMappers;
using MPC.Webstore.ViewModels;
using System.Runtime.Caching;
using MPC.Models.ResponseModels;

namespace MPC.Webstore.Controllers
{
    public class CategoryController : Controller
    {

        #region Private

        private readonly ICompanyService _myCompanyService;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        private readonly IItemService _IItemService;
        private readonly IOrderService _orderService;
        private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CategoryController(ICompanyService myCompanyService, IWebstoreClaimsHelperService myClaimHelper, IItemService itemService, IOrderService orderService, IWebstoreClaimsHelperService webstoreAuthorizationChecker)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }

            this._myCompanyService = myCompanyService;
            this._myClaimHelper = myClaimHelper;
            this._IItemService = itemService;
            this._orderService = orderService;
            this._webstoreAuthorizationChecker = webstoreAuthorizationChecker;
        }

        #endregion
        // GET: Category
        public ActionResult Index(string name, string id)
        {
            //string CacheKeyName = "CompanyBaseResponse";
            //ObjectCache cache = MemoryCache.Default;
            List<ProductPriceMatrixViewModel> ProductPriceMatrix = new List<ProductPriceMatrixViewModel>();
            string StockLabel = string.Empty;
            string Quantity = string.Empty;
            string Price = string.Empty;
            string DPrice = string.Empty;
            bool isDiscounted = false;
            double TaxRate = 0;
            bool includeVAT = false;
            List<ItemStockOptionList> StockOptions = new List<ItemStockOptionList>();
         
           
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);
            ViewBag.Organisation = StoreBaseResopnse.Organisation;
            includeVAT = StoreBaseResopnse.Company.isIncludeVAT ?? false;
            TaxRate = StoreBaseResopnse.Company.TaxRate ?? 0;
            ViewBag.organisationId = StoreBaseResopnse.Organisation.OrganisationId;
         
            ViewBag.CompanyID = _myClaimHelper.loginContactCompanyID();
            long CategoryID = 0;
            bool result = Int64.TryParse(id, out CategoryID);
            
            ProductCategory Category = null;
            if (result)
            {
                CategoryID = Convert.ToInt64(id);
                Category = _myCompanyService.GetCategoryById(CategoryID);

            }
           
            if (Category != null)
            {
                SetCategoryMEtaTitle(Category, StoreBaseResopnse.StoreDetaultAddress, StoreBaseResopnse);
                List<ProductCategory> subCategoryList = new List<ProductCategory>();

                if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp) // corporate case
                {
                    if (_myClaimHelper.loginContactRoleID() == Convert.ToInt32(Roles.Adminstrator))
                    {
                        subCategoryList = _myCompanyService.GetChildCategories(CategoryID, UserCookieManager.WBStoreId);
                    }
                    else
                    {
                        subCategoryList = _myCompanyService.GetAllChildCorporateCatalogByTerritory((int)_myClaimHelper.loginContactCompanyID(), (int)_myClaimHelper.loginContactID(), CategoryID);
                    }

                }
                else // retail case
                {
                    subCategoryList = _myCompanyService.GetChildCategories(CategoryID, UserCookieManager.WBStoreId);
                }

                BindCategoryData(subCategoryList);

                var productList = _myCompanyService.GetRetailOrCorpPublishedProducts(CategoryID);
                productList = productList.Where(p => p.CompanyId == UserCookieManager.WBStoreId).ToList();
                //Dictionary<long, string> productCostCentreList = null;
                List<AddOnCostsCenter> listOfCostCentresAllItems = null;
                bool hasOnePinkProduct = false;

                if ((productList != null && productList.Count == 1) && StoreBaseResopnse.Company.CurrentThemeId == 10012) 
                {
                    hasOnePinkProduct = true;
                }

                if (hasOnePinkProduct)
                {
                    Response.Redirect("/ProductOptions/" + productList.FirstOrDefault().ProductCategoryId + "/" + productList.FirstOrDefault().ItemId + "/DesignOrUpload");
                    return null;
                }
                else if (productList != null && productList.Count > 0)
                {
                    
                    
                    if (_webstoreAuthorizationChecker.loginContactID() > 0)
                    {
                        ViewBag.IsUserLogin = 1;
                    }
                    else
                    {
                        ViewBag.IsUserLogin = 0;
                    }
                   
                    foreach (var product in productList)
                    {
                        // for print products
                        int ItemID = (int)product.ItemId;

                        if (product.isMarketingBrief == null || product.isMarketingBrief == false)
                        {
                            ItemStockOption optSeq1 = _myCompanyService.GetFirstStockOptByItemID(product.ItemId, UserCookieManager.WBStoreId);

                            ItemStockOptionList Sqn = new ItemStockOptionList();
                            Sqn.ItemID = (int)product.ItemId;
                            if (optSeq1 != null)
                                Sqn.StockLabel = optSeq1.StockLabel;
                            else
                                Sqn.StockLabel = "N/A";


                            StockOptions.Add(Sqn);
                            ViewData["StockOptions"] = StockOptions;
                            List<ItemPriceMatrix> matrixlist = _myCompanyService.GetPriceMatrixByItemID((int)product.ItemId);
                            if (_webstoreAuthorizationChecker.isUserLoggedIn())
                            {
                                matrixlist = _IItemService.GetPriceMatrix(matrixlist, product.isQtyRanged ?? false, true, UserCookieManager.WBStoreId, Convert.ToInt64(StoreBaseResopnse.Company.OrganisationId));
                            }
                            else
                            {
                                matrixlist = _IItemService.GetPriceMatrix(matrixlist, product.isQtyRanged ?? false, false, 0, Convert.ToInt64(StoreBaseResopnse.Company.OrganisationId));
                            }
                            matrixlist = matrixlist.Take(2).ToList();
                            if (matrixlist.Count > 0 && matrixlist.Count == 1)
                            {
                                if (product.isQtyRanged == true)
                                {
                                    Quantity = matrixlist[0].QtyRangeFrom + " - " + matrixlist[0].QtyRangeTo;
                                }
                                else
                                {
                                    Quantity = matrixlist[0].Quantity + "";
                                }
                                if (UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)
                                {

                                    if (includeVAT)
                                    {
                                        if (product.DefaultItemTax != null)
                                        {
                                            Price = StoreBaseResopnse.Currency + Utils.FormatDecimalValueToTwoDecimal(Convert.ToString(_myCompanyService.CalculateVATOnPrice(Convert.ToDouble(matrixlist[0].PricePaperType1), Convert.ToDouble(product.DefaultItemTax))));
                                        }
                                        else
                                        {
                                            Price = StoreBaseResopnse.Currency + Utils.FormatDecimalValueToTwoDecimal(Convert.ToString(_myCompanyService.CalculateVATOnPrice(Convert.ToDouble(matrixlist[0].PricePaperType1), TaxRate)));

                                        }


                                    }
                                    else
                                    {

                                        Price = StoreBaseResopnse.Currency + Utils.FormatDecimalValueToTwoDecimal(matrixlist[0].PricePaperType1.ToString());

                                    }
                                }
                                else
                                {// corp
                                    if (includeVAT)
                                    {
                                        Price = StoreBaseResopnse.Currency + Utils.FormatDecimalValueToTwoDecimal(Convert.ToString(_myCompanyService.CalculateVATOnPrice(Convert.ToDouble(matrixlist[0].PricePaperType1), TaxRate)));

                                    }
                                    else
                                    {
                                        Price = StoreBaseResopnse.Currency + Utils.FormatDecimalValueToTwoDecimal(matrixlist[0].PricePaperType1.ToString());

                                    }

                                    isDiscounted = false;
                                }


                                ProductPriceMatrixViewModel ppm = new ProductPriceMatrixViewModel();
                                ppm.Quantity = Quantity;
                                ppm.ItemID = (int)product.ItemId;

                                ppm.Price = Price;

                                ProductPriceMatrix.Add(ppm);

                                ViewData["PriceMatrix"] = ProductPriceMatrix;

                            }
                            else if (matrixlist.Count > 0)
                            {
                                foreach (var matrix in matrixlist)
                                {
                                    if (product.isQtyRanged ?? false)
                                    {
                                        Quantity = matrix.QtyRangeFrom + " - " + matrix.QtyRangeTo;

                                    }
                                    else
                                    {
                                        Quantity = matrix.Quantity + "";
                                    }
                                    if (UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)
                                    {
                                        if (includeVAT)
                                        {
                                            if (product.DefaultItemTax != null)
                                            {
                                                Price = StoreBaseResopnse.Currency + Utils.FormatDecimalValueToTwoDecimal(Convert.ToString(_myCompanyService.CalculateVATOnPrice(Convert.ToDouble(matrix.PricePaperType1), Convert.ToDouble(product.DefaultItemTax))));
                                            }
                                            else
                                            {
                                                Price = StoreBaseResopnse.Currency + Utils.FormatDecimalValueToTwoDecimal(Convert.ToString(_myCompanyService.CalculateVATOnPrice(Convert.ToDouble(matrix.PricePaperType1), TaxRate)));
                                            }
                                        }
                                        else
                                        {
                                            Price = StoreBaseResopnse.Currency + Utils.FormatDecimalValueToTwoDecimal(matrix.PricePaperType1.ToString());
                                        }
                                    }
                                    else
                                    {
                                        if (includeVAT)
                                        {

                                            Price = StoreBaseResopnse.Currency + Utils.FormatDecimalValueToTwoDecimal(Convert.ToString(_myCompanyService.CalculateVATOnPrice(Convert.ToDouble(matrix.PricePaperType1), TaxRate)));

                                        }
                                        else
                                        {
                                            Price = StoreBaseResopnse.Currency + Utils.FormatDecimalValueToTwoDecimal(matrix.PricePaperType1.ToString());

                                        }
                                    }

                                    isDiscounted = false;

                                    ProductPriceMatrixViewModel ppm = new ProductPriceMatrixViewModel();
                                    ppm.Quantity = Quantity;
                                    ppm.ItemID = (int)product.ItemId;
                                   
                                    ppm.Price =  Price;
                                    ProductPriceMatrix.Add(ppm);

                                    ViewData["PriceMatrix"] = ProductPriceMatrix;

                                }

                            }
                        }
                        else
                        {
                            ViewData["PriceMatrix"] = null;
                           
                        }

                        List<AddOnCostsCenter> listOfCostCentresPerItem = _IItemService.GetStockOptionCostCentres(product.ItemId, UserCookieManager.WBStoreId);
                        if(listOfCostCentresPerItem != null  && listOfCostCentresPerItem.Count > 0)
                        {
                             if (listOfCostCentresAllItems == null) 
                            {
                                listOfCostCentresAllItems = new List<AddOnCostsCenter>();
                            }

                           listOfCostCentresAllItems.AddRange(listOfCostCentresPerItem);
                        }
                       
                    }
                }
                else 
                {
                    productList = null;

                    if (subCategoryList.Count() == 1 && subCategoryList.FirstOrDefault() != null) 
                    {
                        Response.Redirect("/Category/" + Utils.specialCharactersEncoder(subCategoryList.FirstOrDefault().CategoryName) + "/" + subCategoryList.FirstOrDefault().ProductCategoryId);
                        return null;
                    }
                }

                ViewData["Products"] = productList;
                ViewData["ProductCostCenterList"] = listOfCostCentresAllItems;
            }

            ViewBag.ContactId = _webstoreAuthorizationChecker.loginContactID();
            ViewBag.IsShowPrices = _myCompanyService.ShowPricesOnStore(UserCookieManager.WEBStoreMode, StoreBaseResopnse.Company.ShowPrices ?? false, _myClaimHelper.loginContactID(), UserCookieManager.ShowPriceOnWebstore);
            if (StoreBaseResopnse.Company.CurrentThemeId == 10012)
            {
                ViewBag.isPinkTheme = 1;
            }
            else 
            {
                ViewBag.isPinkTheme = 0;
            }
            return View("PartialViews/Category", Category);
        }

        private void SetCategoryMEtaTitle(ProductCategory productParentCategory, MPC.Models.DomainModels.Address DefaultAddress, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse CompanyObject)
        {
            string[] MetaTags = _myCompanyService.CreatePageMetaTags(productParentCategory.MetaTitle == null ? "" : productParentCategory.MetaTitle, productParentCategory.MetaDescription == null ? "" : productParentCategory.MetaDescription, productParentCategory.MetaKeywords == null ? "" : productParentCategory.MetaKeywords, CompanyObject.Company.Name, DefaultAddress);
            TempData.Remove("MetaTitle");
            TempData["MetaTitle"] = MetaTags[0];
            TempData.Remove("MetaKeywords");
            //ViewBag.MetaTitle  = MetaTags[0];
            TempData["MetaKeywords"] = MetaTags[1];
            TempData.Remove("MetaDescription");
            //ViewBag.MetaKeywords = MetaTags[1];
            TempData["MetaDescription"] = MetaTags[2];

            //ViewBag.MetaDescription = MetaTags[2];
        }

        public ActionResult CloneItem(long id)
        {
            ItemCloneResult cloneObject = _IItemService.CloneItemAndLoadDesigner(id, (StoreMode)UserCookieManager.WEBStoreMode, UserCookieManager.WEBOrderId, _myClaimHelper.loginContactID(), _myClaimHelper.loginContactCompanyID(), UserCookieManager.TemporaryCompanyId, UserCookieManager.WEBOrganisationID,UserCookieManager.WBStoreId);
            UserCookieManager.TemporaryCompanyId = cloneObject.TemporaryCustomerId;
            UserCookieManager.WEBOrderId = cloneObject.OrderId;
            Response.Redirect(cloneObject.RedirectUrl);
            return null;
        }

        private void BindCategoryData(List<ProductCategory> productCatList)
        {
            if (productCatList != null && productCatList.Count > 0)
            {
                productCatList = productCatList.OrderBy(c => c.DisplayOrder).ToList();
                ViewData["ProductCategory"] = productCatList;
            }
            else
            {
                ViewData["ProductCategory"] = null;
            }

        }

    }
}