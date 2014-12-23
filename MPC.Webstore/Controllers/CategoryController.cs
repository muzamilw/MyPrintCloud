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
                        int TemplateID = 0;
                        if (product.ProductType == (int)ProductType.TemplateProductWithBanner && product.ProductType == (int)ProductType.TemplateProductWithImage)
                        {
                            if(product.IsUploadImage == true)// is popular will replace by isuploadImage
                            {
                                // goto landing page
                                ViewBag.ProductOptionURL = "/ProductOptions/" + CategoryID + "/" + product.ItemId + "/mode=UploadDesign";
                            }
                            else
                            {
                              //  (new ProductManager()).CloneItem(ItemID, 0, 0, SessionParameters.ProductSelection.OrderID, SessionParameters.CustomerID, 0, TemplateID, 0, null, true, false, false, SessionParameters.ContactCompany, SessionParameters.CustomerContact, SessionParameters.BrokerContactCompany);
                               
                         
                                    // clone Item
                            }
                        }
                        else if (product.ProductType == (int)ProductType.FinishedGoodWithBanner && product.ProductType == (int)ProductType.FinishedGoodWithImageRotator) // for non print product
                        {
                            ViewBag.ProductOptionURL = "/ProductOptions/" + CategoryID + "/" + product.ItemId + "/mode=UploadDesign";
                            // goto landing page
                        }

                        ItemStockOption optSeq1 = _myCompanyService.GetFirstStockOptByItemID((int)product.ItemId, 0);
                        if (optSeq1 != null)
                            ViewBag.StockLabel = optSeq1.StockLabel;
                        else
                            ViewBag.StockLabel = "N/A";


                        List<ItemPriceMatrix> matrixlist = _myCompanyService.GetPriceMatrixByItemID((int)product.ItemId);
                      
                        if (matrixlist.Count > 0 && matrixlist.Count == 1)
                        {
                            if (product.IsQtyRanged == true)
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
                               
                                ppm.Price = Price;
                                ppm.DiscountPrice = DPrice;
                                ppm.isDiscounted = isDiscounted;
                                ProductPriceMatrix.Add(ppm);

                                ViewData["PriceMatrix"] = ProductPriceMatrix;

                        }
                        else if (matrixlist.Count > 0)
                        {
                            foreach(var matrix in matrixlist)
                            {
                               if(product.IsQtyRanged ?? false)
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
                                   //lblPrice1.CssClass = "strikeThrough"; /* hellow */
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
                             
                               ppm.Price = Price;
                               ppm.DiscountPrice = DPrice;
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
            MyCompanyDomainBaseResponse baseResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();
            int ContactID = (int)_myClaimHelper.loginContactID();
                int OrderID = ProcessOrder(baseResponse);
                if (OrderID > 0)
                {
                    Item item = _IItemService.CloneItem(id, 0, 0, OrderID, (int)baseResponse.Company.CompanyId, 0, 0, 0, null, true, false, false, ContactID);

                    if (item != null)
                    {
                        ItemID = (int)item.ItemId;
                        TemplateID = item.TemplateId.Value;
                    }
                }

            return View();
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