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
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;
            List<ProductPriceMatrixViewModel> ProductPriceMatrix = new List<ProductPriceMatrixViewModel>();
            string StockLabel = string.Empty;
            string Quantity = string.Empty;
            string Price = string.Empty;
            string DPrice = string.Empty;
            bool isDiscounted = false;
            double TaxRate = 0;
            bool includeVAT = false;
            List<ItemStockOptionList> StockOptions = new List<ItemStockOptionList>();
           // MyCompanyDomainBaseResponse baseResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();

            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.StoreId];
          //  MyCompanyDomainBaseResponse baseResponseCurrency = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCurrency();
            includeVAT = StoreBaseResopnse.Company.isIncludeVAT ?? false;
            TaxRate = StoreBaseResopnse.Company.TaxRate ?? 0;

            long CategoryID = Convert.ToInt64(id);
            ProductCategory Category = _myCompanyService.GetCategoryById(CategoryID);

            if (Category != null)
            {

                //SetPageMEtaTitle(Category.CategoryName, Category.MetaDescription, Category.MetaKeywords, Category.MetaTitle, baseResponse);

                List<ProductCategory> subCategoryList = new List<ProductCategory>();

                if (UserCookieManager.StoreMode == (int)StoreMode.Corp) // corporate case
                {
                    if (_myClaimHelper.loginContactRoleID() == Convert.ToInt32(Roles.Adminstrator))
                    {
                        subCategoryList = _myCompanyService.GetChildCategories(CategoryID);
                    }
                    else
                    {
                        subCategoryList = _myCompanyService.GetAllChildCorporateCatalogByTerritory((int)_myClaimHelper.loginContactCompanyID(), (int)_myClaimHelper.loginContactID(), CategoryID);
                    }

                }
                else // retail case
                {
                    subCategoryList = _myCompanyService.GetChildCategories(CategoryID);
                }

                BindCategoryData(subCategoryList);

                var productList = _myCompanyService.GetRetailOrCorpPublishedProducts(CategoryID);


                //  pnlAllProductTopLevel.Visible = true;
                if (productList != null && productList.Count > 0)
                {

                    foreach (var product in productList)
                    {
                        // for print products
                        int ItemID = (int)product.ItemId;

                        if (product.isMarketingBrief == null || product.isMarketingBrief == false)
                        {
                            ItemStockOption optSeq1 = _myCompanyService.GetFirstStockOptByItemID((int)product.ItemId, 0);

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
                                matrixlist = _IItemService.GetPriceMatrix(matrixlist, product.isQtyRanged ?? false, true, UserCookieManager.StoreId);
                            }
                            else
                            {
                                matrixlist = _IItemService.GetPriceMatrix(matrixlist, product.isQtyRanged ?? false, false, 0);
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
                                if (UserCookieManager.StoreMode == (int)StoreMode.Retail)
                                {

                                    if (includeVAT)
                                    {
                                        if (product.DefaultItemTax != null)
                                        {
                                            Price = StoreBaseResopnse.Currency + _myCompanyService.FormatDecimalValueToTwoDecimal(Convert.ToString(_myCompanyService.CalculateVATOnPrice(Convert.ToDouble(matrixlist[0].PricePaperType1), Convert.ToDouble(product.DefaultItemTax))));
                                        }
                                        else
                                        {
                                            Price = StoreBaseResopnse.Currency + _myCompanyService.FormatDecimalValueToTwoDecimal(Convert.ToString(_myCompanyService.CalculateVATOnPrice(Convert.ToDouble(matrixlist[0].PricePaperType1), TaxRate)));

                                        }


                                    }
                                    else
                                    {

                                        Price = StoreBaseResopnse.Currency + _myCompanyService.FormatDecimalValueToTwoDecimal(matrixlist[0].PricePaperType1.ToString());

                                    }
                                }
                                else
                                {// corp
                                    if (includeVAT)
                                    {
                                        Price = StoreBaseResopnse.Currency + _myCompanyService.FormatDecimalValueToTwoDecimal(Convert.ToString(_myCompanyService.CalculateVATOnPrice(Convert.ToDouble(matrixlist[0].PricePaperType1), TaxRate)));

                                    }
                                    else
                                    {
                                        Price = StoreBaseResopnse.Currency + _myCompanyService.FormatDecimalValueToTwoDecimal(matrixlist[0].PricePaperType1.ToString());

                                    }

                                    isDiscounted = false;
                                }


                                ProductPriceMatrixViewModel ppm = new ProductPriceMatrixViewModel();
                                ppm.Quantity = Quantity;
                                ppm.ItemID = (int)product.ItemId;

                                ppm.Price = StoreBaseResopnse.Currency + Price;
                                //if (!string.IsNullOrEmpty(DPrice))
                                //    ppm.DiscountPrice = Convert.ToDouble(DPrice);

                                //ppm.isDiscounted = isDiscounted;
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
                                    if (UserCookieManager.StoreMode == (int)StoreMode.Retail)
                                    {
                                        if (includeVAT)
                                        {
                                            if (product.DefaultItemTax != null)
                                            {
                                                Price = _myCompanyService.FormatDecimalValueToTwoDecimal(Convert.ToString(_myCompanyService.CalculateVATOnPrice(Convert.ToDouble(matrix.PricePaperType1), Convert.ToDouble(product.DefaultItemTax))));
                                            }
                                            else
                                            {
                                                Price = _myCompanyService.FormatDecimalValueToTwoDecimal(Convert.ToString(_myCompanyService.CalculateVATOnPrice(Convert.ToDouble(matrix.PricePaperType1), TaxRate)));
                                            }
                                        }
                                        else
                                        {
                                            Price = _myCompanyService.FormatDecimalValueToTwoDecimal(matrix.PricePaperType1.ToString());
                                        }
                                    }
                                    else
                                    {
                                        if (includeVAT)
                                        {

                                            Price = _myCompanyService.FormatDecimalValueToTwoDecimal(Convert.ToString(_myCompanyService.CalculateVATOnPrice(Convert.ToDouble(matrix.PricePaperType1), TaxRate)));

                                        }
                                        else
                                        {
                                            Price = _myCompanyService.FormatDecimalValueToTwoDecimal(matrix.PricePaperType1.ToString());

                                        }
                                    }
                                    //if (matrix.IsDiscounted == true) // IsDiscounted is removed from table
                                    //{
                                    //    isDiscounted = true;
                                    //    //lblPrice1.CssClass = "strikeThrough"; 
                                    //    //lblDiscountedPrice1.Visible = true;
                                    //    DPrice = baseResponseCurrency.Currency + _myCompanyService.FormatDecimalValueToTwoDecimal(_myCompanyService.CalculateDiscount(Convert.ToDouble(Price), Convert.ToDouble(product.PriceDiscountPercentage)).ToString());

                                    //}
                                    //else
                                    isDiscounted = false;


                                    //if (matrixlist[1].PricePaperType1 > 0)
                                    //{
                                    //    SecPricetr.Visible = true;
                                    //}
                                    //else
                                    //{
                                    //    SecPricetr.Visible = false;
                                    //}
                                  //  Price = StoreBaseResopnse.Currency + Price;

                                    ProductPriceMatrixViewModel ppm = new ProductPriceMatrixViewModel();
                                    ppm.Quantity = Quantity;
                                    ppm.ItemID = (int)product.ItemId;
                                    //if (!string.IsNullOrEmpty(Price))
                                    //    ppm.Price = Convert.ToDouble(Price);
                                    ppm.Price = StoreBaseResopnse.Currency + Price;
                                    ProductPriceMatrix.Add(ppm);

                                    ViewData["PriceMatrix"] = ProductPriceMatrix;

                                }

                            }
                        }
                        else
                        {
                            ViewData["PriceMatrix"] = null;
                            if (_webstoreAuthorizationChecker.isUserLoggedIn())
                            {
                                ViewBag.IsUserLogin = true;
                            }
                            else
                            {
                                ViewBag.IsUserLogin = false;
                            }

                        }
                    }



                }
                else 
                {
                    productList = null;
                }

                ViewData["Products"] = productList;

            }
            else
            {

            }

            return View("PartialViews/Category", Category);
        }

      
        public ActionResult CloneItem(long id)
        {
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;
            long ItemID = 0;
            long TemplateID = 0;
            bool isCorp = true;
            if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                isCorp = true;
            else
                isCorp = false;
            int TempDesignerID = 0;
            string ProductName = string.Empty;

            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.StoreId];

            long ContactID = _myClaimHelper.loginContactID();
            long CompanyID = _myClaimHelper.loginContactCompanyID();
            if (UserCookieManager.OrderId == 0)
            {
                long OrderID = 0;
                long TemporaryRetailCompanyId = 0;
                if (UserCookieManager.StoreMode == (int)StoreMode.Retail)
                {
                    TemporaryRetailCompanyId = UserCookieManager.TemporaryCompanyId;
                    OrderID = _orderService.ProcessPublicUserOrder(string.Empty, StoreBaseResopnse.Organisation.OrganisationId, (StoreMode)UserCookieManager.StoreMode, CompanyID, ContactID, ref TemporaryRetailCompanyId);
                    if (OrderID > 0)
                    {
                        UserCookieManager.OrderId = OrderID;
                    }
                    if (TemporaryRetailCompanyId != 0)
                    {
                        UserCookieManager.TemporaryCompanyId = TemporaryRetailCompanyId;
                        ContactID = _myCompanyService.GetContactIdByCompanyId(TemporaryRetailCompanyId);
                    }
                    CompanyID = TemporaryRetailCompanyId;

                }
                else 
                {
                    OrderID = _orderService.ProcessPublicUserOrder(string.Empty, StoreBaseResopnse.Organisation.OrganisationId, (StoreMode)UserCookieManager.StoreMode, CompanyID, ContactID, ref TemporaryRetailCompanyId);
                    if (OrderID > 0)
                    {
                        UserCookieManager.OrderId = OrderID;
                    }
                }

                // create new order


                Item item = _IItemService.CloneItem(id, 0, OrderID, CompanyID, 0, 0, null, false, false, ContactID, StoreBaseResopnse.Organisation.OrganisationId);

                    if (item != null)
                    {
                        ItemID = item.ItemId;
                        TemplateID = item.TemplateId ?? 0;
                        TempDesignerID = item.DesignerCategoryId ?? 0;
                        ProductName = item.ProductName;
                    }
                
            }
            else
            {
                if (UserCookieManager.TemporaryCompanyId == 0 && UserCookieManager.StoreMode == (int)StoreMode.Retail && ContactID == 0)
                {
                    long TemporaryRetailCompanyId = UserCookieManager.TemporaryCompanyId;

                    // create new order

                    long OrderID = _orderService.ProcessPublicUserOrder(string.Empty, StoreBaseResopnse.Organisation.OrganisationId, (StoreMode)UserCookieManager.StoreMode, CompanyID, ContactID, ref TemporaryRetailCompanyId);
                    if (OrderID > 0)
                    {
                        UserCookieManager.OrderId = OrderID;
                    }
                    if (TemporaryRetailCompanyId != 0)
                    {
                        UserCookieManager.TemporaryCompanyId = TemporaryRetailCompanyId;
                        ContactID = _myCompanyService.GetContactIdByCompanyId(TemporaryRetailCompanyId);
                    }
                    CompanyID = TemporaryRetailCompanyId;
                }
                else if (UserCookieManager.TemporaryCompanyId > 0 && UserCookieManager.StoreMode == (int)StoreMode.Retail)
                {
                    CompanyID = UserCookieManager.TemporaryCompanyId;
                    ContactID = _myCompanyService.GetContactIdByCompanyId(CompanyID);
                }
                Item item = _IItemService.CloneItem(id, 0, UserCookieManager.OrderId, CompanyID, 0, 0, null, false, false, ContactID, StoreBaseResopnse.Organisation.OrganisationId);

                if (item != null)
                {
                    ItemID = item.ItemId;
                    TemplateID = item.TemplateId ?? 0;
                    TempDesignerID = item.DesignerCategoryId ?? 0;
                    ProductName = Utils.specialCharactersEncoder(item.ProductName);
                }
            }

            int isCalledFrom = 0;
            if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                isCalledFrom = 4;
            else
                isCalledFrom = 3;

            bool isEmbedded;
            bool printWaterMark = true;
            if (UserCookieManager.StoreMode == (int)StoreMode.Corp || UserCookieManager.StoreMode == (int)StoreMode.Retail)
            {
                isEmbedded = true;
            }
            else {
                printWaterMark = false;
                isEmbedded = false;
            }

            ProductName = _IItemService.specialCharactersEncoder(ProductName);
            //Designer/productName/CategoryIDv2/TemplateID/ItemID/companyID/cotnactID/printCropMarks/printWaterMarks/isCalledFrom/IsEmbedded;
            bool printCropMarks = true;
            string URL = "/Designer/" + ProductName + "/" + TempDesignerID + "/" + TemplateID + "/" + ItemID + "/" + CompanyID + "/" + ContactID + "/" + isCalledFrom + "/" + UserCookieManager.OrganisationID + "/" + printCropMarks + "/" + printWaterMark + "/" + isEmbedded;

            // ItemID ok
            // TemplateID ok
            // iscalledfrom ok
            // cv scripts require
            // productName ok
            // contactid // ask from iqra about retail and corporate
            // companyID // ask from iqra
            // isembaded ook
            Response.Redirect(URL);
            return null;
        }

        //private void SetPageMEtaTitle(string CatName, string CatDes, string Keywords, string Title,  MPC.Models.ResponseModels.MyCompanyDomainBaseReponse baseResponse)
        //{

        //    Address DefaultAddress = _myCompanyService.GetDefaultAddressByStoreID(UserCookieManager.StoreId);
       
        //    string[] MetaTags = _myCompanyService.CreatePageMetaTags(Title, CatDes, Keywords, StoreMode.Retail, baseResponse.Company.Name, DefaultAddress);

        //    ViewBag.MetaTitle = MetaTags[0];
        //    ViewBag.MetaKeywords = MetaTags[1];
        //    ViewBag.MetaDescription = MetaTags[2];
        //}
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