using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using MPC.Webstore.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Webstore.ResponseModels;
using MPC.Webstore.ModelMappers;
using MPC.Webstore.ViewModels;

namespace MPC.Webstore.Controllers
{
    public class CategoryController : Controller
    {

        #region Private

        private readonly ICompanyService _myCompanyService;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        private readonly IItemService _IItemService;
        private readonly IOrderService _orderService;
        
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CategoryController(ICompanyService myCompanyService,IWebstoreClaimsHelperService myClaimHelper,IItemService itemService,IOrderService orderService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
           
            this._myCompanyService = myCompanyService;
            this._myClaimHelper = myClaimHelper;
            this._IItemService = itemService;
            this._orderService = orderService;
        }

        #endregion
        // GET: Category
        public ActionResult Index(string name,string id)
        {
            List<ProductPriceMatrixViewModel> ProductPriceMatrix = new List<ProductPriceMatrixViewModel>();
            string StockLabel = string.Empty;
            string Quantity = string.Empty;
            string Price = string.Empty;
            string DPrice = string.Empty;
            bool isDiscounted = false;
            double TaxRate = 0;
            bool includeVAT = false;
            List<ItemStockOptionList> StockOptions = new List<ItemStockOptionList>();
            MyCompanyDomainBaseResponse baseResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();


            MyCompanyDomainBaseResponse baseResponseCurrency = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCurrency();
            includeVAT = baseResponse.Company.isIncludeVAT ?? false;
            TaxRate = baseResponse.Company.TaxRate ?? 0;
          
            int CategoryID = Convert.ToInt32(id);
            ProductCategory Category = _myCompanyService.GetCategoryById(CategoryID);

            if (Category != null)
            {

               SetPageMEtaTitle(Category.CategoryName, Category.MetaDescription, Category.MetaKeywords, Category.MetaTitle,baseResponse);

                List<ProductCategory> subCategoryList = new List<ProductCategory>();

                if(UserCookieManager.StoreMode == (int)StoreMode.Corp) // corporate case
                {
                    if (_myClaimHelper.loginContactRoleID() == Convert.ToInt32(Roles.Adminstrator))
                    {
                        subCategoryList = _myCompanyService.GetChildCategories(CategoryID);
                    }
                    else
                    {
                        subCategoryList = _myCompanyService.GetAllChildCorporateCatalogByTerritory((int)_myClaimHelper.loginContactCompanyID(),(int) _myClaimHelper.loginContactID(), CategoryID);
                    }

                }
                else // retail case
                {
                    subCategoryList = _myCompanyService.GetChildCategories(CategoryID);
                }

                BindCategoryData(subCategoryList);

                var productList =  _myCompanyService.GetRetailOrCorpPublishedProducts(CategoryID);


              //  pnlAllProductTopLevel.Visible = true;
                if (productList != null && productList.Count > 0)
                {
                    
                    foreach (var product in productList)
                    {
                        // for print products
                        int ItemID = (int)product.ItemId;
                        

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
                                        Price = baseResponseCurrency.Currency + _myCompanyService.FormatDecimalValueToTwoDecimal(Convert.ToString(_myCompanyService.CalculateVATOnPrice(Convert.ToDouble(matrixlist[0].PricePaperType1), Convert.ToDouble(product.DefaultItemTax)))); 
                                    }
                                    else
                                    {
                                        Price = baseResponseCurrency.Currency + _myCompanyService.FormatDecimalValueToTwoDecimal(Convert.ToString(_myCompanyService.CalculateVATOnPrice(Convert.ToDouble(matrixlist[0].PricePaperType1), TaxRate))); 
                                       
                                    }


                                }
                                else
                                {

                                    Price = baseResponseCurrency.Currency + _myCompanyService.FormatDecimalValueToTwoDecimal(matrixlist[0].PricePaperType1.ToString());

                                }
                            }
                            else
                            {// corp
                                if (includeVAT)
                                {
                                    Price = baseResponseCurrency.Currency + _myCompanyService.FormatDecimalValueToTwoDecimal(Convert.ToString(_myCompanyService.CalculateVATOnPrice(Convert.ToDouble(matrixlist[0].PricePaperType1), TaxRate))); 

                                }
                                else
                                {
                                    Price = baseResponseCurrency.Currency + _myCompanyService.FormatDecimalValueToTwoDecimal(matrixlist[0].PricePaperType1.ToString());

                                }
                                if (matrixlist[0].IsDiscounted == true)
                                {
                                    isDiscounted = true;
                                    //lblPrice1.CssClass = "strikeThrough"; /* hellow
                                    //lblDiscountedPrice1.Visible = true;
                                    DPrice = baseResponseCurrency.Currency + _myCompanyService.FormatDecimalValueToTwoDecimal(_myCompanyService.CalculateDiscount(Convert.ToDouble(matrixlist[0].PricePaperType1), Convert.ToDouble(product.PriceDiscountPercentage)).ToString());
                                    // lblDiscountedPrice1.Text = this.GetCompanySiteWithCurrencySymbol.GenSettingsCurrencySymbol + Utils.FormatDecimalValueToTwoDecimal(ProductManager.CalculateDiscount(Convert.ToDouble(matrixlist[0].PricePaperType1), Convert.ToDouble(product.PriceDiscountPercentage)).ToString());
                                }
                                else
                                    isDiscounted = false;
                            }
                               
                                //lblPrice1.Text = this.GetCompanySiteWithCurrencySymbol.GenSettingsCurrencySymbol + lblPrice1.Text;
                                //lblPrice2.Text = this.GetCompanySiteWithCurrencySymbol.GenSettingsCurrencySymbol + lblPrice2.Text;

                                ProductPriceMatrixViewModel ppm = new ProductPriceMatrixViewModel();
                                ppm.Quantity = Quantity;
                                ppm.ItemID = (int)product.ItemId;
                                if (!string.IsNullOrEmpty(Price))
                                     ppm.Price = Convert.ToDouble(Price);
                                if (!string.IsNullOrEmpty(DPrice))
                                    ppm.DiscountPrice = Convert.ToDouble(DPrice);
                              
                                ppm.isDiscounted = isDiscounted;
                                ProductPriceMatrix.Add(ppm);

                                ViewData["PriceMatrix"] = ProductPriceMatrix;

                        }
                        else if (matrixlist.Count > 0)
                        {
                            foreach(var matrix in matrixlist)
                            {
                               if(product.isQtyRanged ?? false)
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
                                           Price = _myCompanyService.FormatDecimalValueToTwoDecimal(Convert.ToString(_myCompanyService.CalculateVATOnPrice(Convert.ToDouble(matrixlist[0].PricePaperType1), Convert.ToDouble(product.DefaultItemTax))));
                                       }
                                       else
                                       {
                                           Price = _myCompanyService.FormatDecimalValueToTwoDecimal(Convert.ToString(_myCompanyService.CalculateVATOnPrice(Convert.ToDouble(matrixlist[0].PricePaperType1), TaxRate)));
                                       }
                                   }
                                   else
                                   {
                                       Price = _myCompanyService.FormatDecimalValueToTwoDecimal(matrixlist[0].PricePaperType1.ToString());
                                   }
                               }
                               else
                               {
                                    if (includeVAT)
                                    {

                                        Price = _myCompanyService.FormatDecimalValueToTwoDecimal(Convert.ToString(_myCompanyService.CalculateVATOnPrice(Convert.ToDouble(matrixlist[0].PricePaperType1), TaxRate)));
                                        
                                    }
                                    else
                                    {
                                        Price = _myCompanyService.FormatDecimalValueToTwoDecimal(matrixlist[0].PricePaperType1.ToString());
                                        
                                    }
                               }
                               if (matrix.IsDiscounted == true)
                               {
                                   isDiscounted = true;
                                   //lblPrice1.CssClass = "strikeThrough"; 
                                   //lblDiscountedPrice1.Visible = true;
                                   DPrice = baseResponseCurrency.Currency + _myCompanyService.FormatDecimalValueToTwoDecimal(_myCompanyService.CalculateDiscount(Convert.ToDouble(Price), Convert.ToDouble(product.PriceDiscountPercentage)).ToString());

                               }
                               else
                                   isDiscounted = false;
                              

                               //if (matrixlist[1].PricePaperType1 > 0)
                               //{
                               //    SecPricetr.Visible = true;
                               //}
                               //else
                               //{
                               //    SecPricetr.Visible = false;
                               //}
                               Price = baseResponseCurrency.Currency + Price;

                               ProductPriceMatrixViewModel ppm = new ProductPriceMatrixViewModel();
                               ppm.Quantity = Quantity;
                               ppm.ItemID = (int)product.ItemId;
                               if (!string.IsNullOrEmpty(Price))
                                   ppm.Price = Convert.ToDouble(Price);
                               if (!string.IsNullOrEmpty(DPrice))
                                   ppm.DiscountPrice = Convert.ToDouble(DPrice);
                             
                               ppm.isDiscounted = isDiscounted;
                               ProductPriceMatrix.Add(ppm);

                               ViewData["PriceMatrix"] = ProductPriceMatrix;

                            }
  
                        }
                      

                    }
                }

                 ViewData["Products"] = productList;
              
            }
            else
            {

            }

            return View("PartialViews/Category",Category);
        }


        public ActionResult CloneItem(int id)
        {
            int ItemID = 0;
            int TemplateID = 0;
            bool isCorp = true;
            if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                isCorp = true;
            else
                isCorp = false;
             int TempDesignerID = 0;
            string ProductName = string.Empty;
            MyCompanyDomainBaseResponse baseResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();
            MyCompanyDomainBaseResponse baseResponseorg = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromOrganisation();
            int ContactID = (int)_myClaimHelper.loginContactID();
            int OrderID = ProcessOrder(baseResponseorg);
                if (OrderID > 0)
                {
                    Item item = _IItemService.CloneItem(id, 0, 0, OrderID, (int)baseResponse.Company.CompanyId, 0, 0, 0, null, isCorp, false, false, ContactID);

                    if (item != null)
                    {
                        ItemID = (int)item.ItemId;
                        TemplateID = item.TemplateId ?? 0;
                        TempDesignerID = item.DesignerCategoryId ?? 0;
                        ProductName = item.ProductName;
                    }
                }
            int isCalledFrom = 0;
            if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                isCalledFrom = 4;
            else
                isCalledFrom = 3;

            bool isEmbaded;
            if (UserCookieManager.StoreMode == (int)StoreMode.Corp ||  UserCookieManager.StoreMode == (int)StoreMode.Retail)
              isEmbaded = true;
            else
                isEmbaded = false;

            ProductName = _IItemService.specialCharactersEncoder(ProductName);
            //PartialViews/TempDesigner/ItemID/TemplateID/IsCalledFrom/CV2/ProductName/ContactID/CompanyID/IsEmbaded;
            string URL = "PartialViews/TempDesigner/" + ItemID + "/" + TemplateID + "/" + isCalledFrom + "/" + TempDesignerID + "/" + ProductName + "/" + ContactID + "/" + (int)baseResponse.Company.CompanyId + "/" + isEmbaded;
           
            // ItemID ok
            // TemplateID ok
            // iscalledfrom ok
            // cv scripts require
            // productName ok
            // contactid // ask from iqra about retail and corporate
            // companyID // ask from iqra
            // isembaded ook
            return View(URL);
        }

        public int ProcessOrder(MyCompanyDomainBaseResponse baseResponse)
        {

            int orderID = 0;
            
            int ordID = (int)baseResponse.Organisation.OrganisationId;

            Organisation org = _myCompanyService.getOrganisatonByID(ordID);
            
            orderID =  _orderService.ProcessPublicUserOrder(string.Empty,org);
            return orderID;
        }

        private void SetPageMEtaTitle(string CatName, string CatDes, string Keywords, string Title, MyCompanyDomainBaseResponse baseResponse)
        {

            Address DefaultAddress = _myCompanyService.GetDefaultAddressByStoreID(UserCookieManager.StoreId);
          
            string[] MetaTags  = _myCompanyService.CreatePageMetaTags(Title, CatDes, Keywords, StoreMode.Retail, baseResponse.Company.Name, DefaultAddress);

            ViewBag.MetaTitle = MetaTags[0];
            ViewBag.MetaKeywords = MetaTags[1];
            ViewBag.MetaDescription = MetaTags[2];
        }
        private void BindCategoryData(List<ProductCategory> productCatList)
        {
            if (productCatList != null)
            {
                if (productCatList.Count > 0)
                {
                   
                    productCatList = productCatList.OrderBy(c => c.DisplayOrder).ToList();
                   
                }
               
            }
            ViewData["ProductCategory"] = productCatList;
        }
       
    }
}