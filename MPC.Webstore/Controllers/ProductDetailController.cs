using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.Common;
using MPC.Webstore.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Webstore.ModelMappers;
using MPC.Models.DomainModels;
using MPC.Models.Common;
using MPC.Webstore.ViewModels;
using System.Web.Configuration;
using MPC.ExceptionHandling;
using System.Globalization;

namespace MPC.Webstore.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly ICompanyService _myCompanyService;
        private readonly IItemService _IItemService;
        private readonly ITemplateService _ITemplateService;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        private List<ItemStockOption> _vwItemSect_StockItems = null;
        private int _priceMatrixListCount = 0;
        private long organisationID = 0;
        RelatedItemViewModel RIviewModel = new RelatedItemViewModel();
 
        public ProductDetailController(IWebstoreClaimsHelperService myClaimHelper, ICompanyService myCompanyService,IItemService IItemService,ITemplateService ITemplateService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
    
            this._myClaimHelper = myClaimHelper;
            this._myCompanyService = myCompanyService;
            this._IItemService = IItemService;
            this._ITemplateService = ITemplateService;
        
        }
        // GET: Default
        public ActionResult Index(int CategoryID,long ItemID,int TemplateID,string TemplateName,string UploadDesign)
        {
            try
            {
                
                double minimumPrice = 0;
                long ReferenceItemID;
                bool IsShowPrices;
                MyCompanyDomainBaseResponse baseResponseCompany = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();
                
                MyCompanyDomainBaseResponse baseresponseCurrency = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCurrency();
                MyCompanyDomainBaseResponse baseresponseOrg = _myCompanyService.GetStoreFromCache(baseResponseCompany.Company.OrganisationId ?? 0).CreateFromOrganisation();
                organisationID = baseresponseOrg.Organisation.OrganisationId;

                if (baseResponseCompany.Company.ShowPrices == true)
                {
                    ViewBag.IsShowPrices = true;
                    IsShowPrices = true;
                }
                else
                {
                    ViewBag.IsShowPrices = false;
                    IsShowPrices = false;

                }
                Item ItemRecord = _IItemService.GetItemById(ItemID);

               
                if (ItemRecord != null)
                {
                    // curProduct = ProductManager.GetProductItemByItemId(itemID);
                    if (TemplateID > 0)
                    {
                        ViewBag.TemplateID = TemplateID;
                        long TempID = TemplateID;
                        SetLastItemTemplateMatchingSets(ItemRecord, baseresponseOrg, baseresponseCurrency, baseResponseCompany, TempID);
                        PopulateTemplateObject(TempID, ItemRecord, ItemID);
                    }
                    else
                    {
                        ViewBag.TemplateID = 0;


                    }
                    ViewBag.btnUploadDesignPath = "/ProductOptions/" + CategoryID + "/" + ItemID + "/Mode=UploadDesign";

                    // LayoutGrid


                    if (ItemRecord.ProductType == (int)ProductType.FinishedGoodWithBanner || ItemRecord.ProductType == (int)ProductType.FinishedGoodWithImageRotator)
                    {

                        // ProductrightContainter.Style.Add("display", "none");
                        // SliderContainer.Style.Add("background-color", "#f3f3f3");
                        loadfinishedGoodsImages(ItemRecord, ItemID);
                    }

                    ViewBag.hfCategoryId = CategoryID;

                    SetPageMEtaTitle(ItemRecord.ProductName, ItemRecord.MetaDescription, ItemRecord.MetaKeywords, ItemRecord.MetaTitle, baseResponseCompany);

                    string CurrentProductCategoryName = string.Empty;
                    //Findout the minimum price
                    minimumPrice = _IItemService.FindMinimumPriceOfProduct(ItemRecord.ItemId);
                    string cateName = _IItemService.GetImmidiateParentCategory(ItemRecord.ItemId, out CurrentProductCategoryName);
                    string currency = baseresponseCurrency.Currency;
                    //Sets Heading
                    SetHeadings(CurrentProductCategoryName, ItemRecord.ProductName, cateName, minimumPrice.ToString(), ItemRecord.ProductCode, ItemRecord.WebDescription, ItemRecord.ItemId, currency);


                    _vwItemSect_StockItems = _IItemService.GetAllStockListByItemID(ItemRecord.ItemId, 0);
                    ItemStockOption stockOption = _vwItemSect_StockItems.Where(o => o.OptionSequence == 1).FirstOrDefault();
                    bool mode = false;
                    if (ItemRecord.IsQtyRanged.HasValue)
                    {
                        mode = ItemRecord.IsQtyRanged.Value;
                    }
                    //BindPrice Matrix The Product Text
                    if (ItemRecord.ItemPriceMatrices.Count > 0)
                    {
                        ReferenceItemID = ItemRecord.ItemId;
                        BindPriceMatrixData(ItemRecord.ItemPriceMatrices.Where(i => i.SupplierId == null).ToList(), mode, baseResponseCompany);
                    }
                    if (stockOption != null)
                    {
                        List<string> ListOfExtras = _IItemService.GetProductItemAddOnCostCentres(stockOption.ItemStockOptionId, UserCookieManager.StoreId);

                        if (ListOfExtras != null && ListOfExtras.Count > 0)
                        {
                            ViewData["CostCentersList"] = ListOfExtras;
                        }
                        else
                        {
                            ViewData["CostCentersList"] = null;
                        }

                    }
                    else
                    {
                        ViewData["CostCentersList"] = null;
                    }


                    if (ItemRecord.IsStockControl == true)
                    {
                        ItemStockControl StckItem = _IItemService.GetStockItem(ItemRecord.ItemId);
                        ViewData["StockControl"] = StckItem;
                        if (StckItem != null)
                        {
                            if (StckItem.isAllowBackOrder ?? false) // back ordering allowed
                            {
                                ViewBag.isAllowBackOrder = true;
                            }
                            else
                            {// no stock 
                                ViewBag.isAllowBackOrder = false;



                            }

                        }
                    }
                    else
                    {
                        ViewBag.isAllowBackOrder = false;
                    }


                    //Handle corporate scenario
                    //HandleCorporateScenario(curProduct);


                    LoadRelatedItems(ItemID, ItemRecord.ProductName, baseresponseCurrency, IsShowPrices);




                    if (UserCookieManager.StoreMode != (int)StoreMode.Corp)
                    {
                        if (TemplateID > 0)
                        {

                            // hfFbShareLink.Value = "http://pinkcards.com/ProductDetails.aspx?CategoryID=350&ItemID=3779&Mode=UploadDesign&Count=-1&TemplateID=1776&Number=0";
                            // hfFbShareLink.Value = "http://pinkcards.com/pd/Assisted-Living-Facility;350;3779;UploadDesign;-1;1776;0";
                            ViewBag.hfFbShareLink = "ProductDetail/" + CategoryID + "/" + ItemID + "/" + TemplateID + "/" + TemplateName + "/UploadDesign=0";
                            //   HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/pd/" + TempName + ";" + PageParameters.CategoryId.ToString() + ";ItemID=" + itemID + ";UploadDesign;0;" + TemplID + ";0";

                            //hfFbShareLink.Value = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/ProductDetails.aspx?CategoryID=" + PageParameters.CategoryId.ToString() + "&ItemID=" + itemID + "&Mode=UploadDesign&Count=-1&TemplateID=" + TemplID + "&Number=0";

                        }

                    }


                }
                else
                {
                    // string reff = Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : "no ref";
                    Response.Redirect("/Error");
                    // redirect to error page
                }
            }
            catch(Exception ex)
            {
                throw new MPCException(ex.ToString(), organisationID);
            }


            return View();
        }

        /// <summary>
        /// to display list of matching set on product detail page
        /// </summary>
        /// <param name="shopCart"></param>
        /// <param name="baseresponseOrg"></param>
        /// <param name="baseresponseCurrency"></param>
        /// <param name="baseresponseCompany"></param>
        private void SetLastItemTemplateMatchingSets(Item Product, MyCompanyDomainBaseResponse baseresponseOrg, MyCompanyDomainBaseResponse baseresponseCurrency, MyCompanyDomainBaseResponse baseresponseCompany,long tempID)
        {

            MatchingSetViewModel MSViewModel = new MatchingSetViewModel();
            List<MappedCategoriesName> mappedCatList = new List<MappedCategoriesName>();
            try
            {
                if (Product != null)
                {
                    //Model.ProductItem item = shopCart.CartItemsList.Where(c => c.TemplateID.Value > 0 && c.Attatchment.FileTitle != null && !c.Attatchment.FileTitle.Contains("Uploaded ArtWork")).LastOrDefault();
                    //ProductItem item = shopCart.CartItemsList.Where(c => c.TemplateID != null && c.TemplateID > 0).LastOrDefault();
                   

                        string TemplateName = _ITemplateService.GetTemplateNameByTemplateID(tempID);
                        if (!string.IsNullOrEmpty(TemplateName))
                        {

                            List<MatchingSets> res = _ITemplateService.BindTemplatesList(TemplateName, 1, baseresponseOrg.Organisation.OrganisationId, (int)_myClaimHelper.loginContactCompanyID());

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

                                foreach (var set in res)
                                {
                                    ProductCategoriesView pCat = _IItemService.GetMappedCategory(set.CategoryName, (int)_myClaimHelper.loginContactCompanyID());

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
            catch (Exception ex)
            {
                throw ex;

            }
        }
        /// <summary>
        /// populate template information by tempid
        /// </summary>
        /// <param name="TempID"></param>
        /// <param name="RecItem"></param>
        /// <param name="ItemID"></param>
        private void PopulateTemplateObject(long TempID,Item RecItem,long ItemID)
        {
             Template ObjTemp = new Template();
             string TempName = string.Empty;
             string option = string.Empty;
             List<TemplatePage> LstTempPages = new List<TemplatePage>();
             _ITemplateService.populateTemplateInfo(TempID,RecItem,out ObjTemp,out LstTempPages);
 
               int count = 0;
                if (LstTempPages != null)
                {
                    count = LstTempPages.Count();
                }
                if (ObjTemp != null)
                {
                    TempName = ObjTemp.ProductName;
                    ViewBag.hfTemplateName = Server.UrlEncode(ObjTemp.ProductName);
                    ViewBag.TemplateName = ObjTemp.ProductName;
                    ViewBag.ratingControlUserSelectedIndex = Convert.ToInt32(ObjTemp.MPCRating.Value);
                }

                ViewBag.txtNoOfPages = count.ToString();
                ViewBag.txtTemplateID = TempID;
                ViewBag.Count = count;

                if (UserCookieManager.OrderId != null)
                {

                            var result = _IItemService.GetItemByOrderAndItemID(ItemID,UserCookieManager.OrderId);
                            if (result != null)
                            {

                                var localTemplate = _ITemplateService.GetTemplateNameByTemplateID(result.TemplateId ?? 0);
                                if (localTemplate != null)
                                {
                                    if (localTemplate == ObjTemp.ProductName)
                                    {
                                        option = "SameTemplate";
                                    }
                                    else
                                    {

                                        option = "SameItem";
                                    }
                                }
                            }
                        }
                   
                ViewBag.hfEditTempType = option;
                if (_myClaimHelper.loginContactID() > 0)
                {
                    ViewBag.hfContactId = _myClaimHelper.loginContactID();

                    FavoriteDesign favoriteDesign = _IItemService.GetFavContactDesign(TempID, _myClaimHelper.loginContactID());
                    if (favoriteDesign != null && favoriteDesign.IsFavorite)
                    {
                        ViewBag.IsFavoriteDesign = true;


                    }
                    else
                    {
                        ViewBag.IsFavoriteDesign = false;

                    }
                }
                string html = "";
                if (RecItem.ProductType == (int)ProductType.TemplateProductWithBanner)
                {
                    ViewBag.IsTemplateProductWithBanner = true;
                 
                  //  SliderContainer.Style.Add("background-color", "#f3f3f3"); pending
                 
                    ViewBag.TempBannerImgURL = RecItem.ImagePath;
                }
                else
                {
                    string TemplateDesignerUrl = WebConfigurationManager.AppSettings["TemplateDesignsUrl"];
                    ViewBag.IsTemplateProductWithBanner = false;
                    html = "  <div id='slider' style='height:450px;'> ";
                    for (int i = 1; i <= count; i++)
                    {
                        string imgurl = string.Format("{0}{1}{2}", TemplateDesignerUrl, "designer/products/" + TempID + "/", "p" + i + ".png");
                        if(LstTempPages != null)
                            html += "<img class='sliderImgs' src=" + imgurl + " alt='" + LstTempPages[i - 1].PageName + "'  />";// orignal for image slider 
                        else
                            html += "<img class='sliderImgs' src=" + imgurl + "/>";// orignal for image slider 
                    }
                    html += "</div>";
                    ViewBag.Html = html;
                    
                }
            }
        /// <summary>
        /// Load images for finish goods
        /// </summary>
        /// <param name="objItem"></param>
        private void loadfinishedGoodsImages(Item objItem,long itemID)
        {
        
            //btnMatchingSets.Visible = false;
            // ifrCon.Visible = false;

            ViewBag.hfEditTempType = "FinishedGood";
            // loading finished goods
           
                //lblTemplateName.Text = "";
                List<ItemImage> images = _IItemService.getItemImagesByItemID(objItem.ItemId);
                // AppBasePath + images[0].ImageURL
                string html = "  <div id='slider' style='width: 650px !important; height:380px !important;'> ";
                if (images.Count != 0)
                {
                    ViewBag.txtNoOfPages = images.Count.ToString();
                    string AppBasePath = WebConfigurationManager.AppSettings["AppBasePath"];
                    foreach (var image in images)
                    {
                        string imgurl = string.Format("{0}{1}", AppBasePath, image.ImageURL);

                        html += "<img class='sliderImgs' src=" + imgurl + "   />";// orignal for image slider // alt='" + image.ImageTitle + "'

                    }
                }
                else
                {
                    ViewBag.txtNoOfPages = "1";

                    var item = _IItemService.GetItemById(itemID);
                        html += "<img class='sliderImgs' src=" + item.ImagePath + "   />";// orignal for image slider // alt='" + image.ImageTitle + "'

                }
                html += "</div>";
                ViewBag.Html = html;
            

        }
        /// <summary>
        /// to dispaly the meta titles of page
        /// </summary>
        /// <param name="CatName"></param>
        /// <param name="CatDes"></param>
        /// <param name="Keywords"></param>
        /// <param name="Title"></param>
        /// <param name="baseResponse"></param>
        private void SetPageMEtaTitle(string CatName, string CatDes, string Keywords, string Title, MyCompanyDomainBaseResponse baseResponse)
        {

            Address DefaultAddress = _myCompanyService.GetDefaultAddressByStoreID(UserCookieManager.StoreId);

            string[] MetaTags = _myCompanyService.CreatePageMetaTags(Title, CatDes, Keywords, StoreMode.Retail, baseResponse.Company.Name, DefaultAddress);

            ViewBag.MetaTitle = MetaTags[0];
            ViewBag.MetaKeywords = MetaTags[1];
            ViewBag.MetaDescription = MetaTags[2];
        }
        /// <summary>
        /// set heading of product
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="productName"></param>
        /// <param name="parentCatName"></param>
        /// <param name="mininumCost"></param>
        /// <param name="code"></param>
        /// <param name="description"></param>
        /// <param name="itemID"></param>
        private void SetHeadings(string categoryName, string productName, string parentCatName, string mininumCost, string code, string description, long itemID, string Currency)
        {
            //Sets Main Heading
            ViewBag.lblCategoryMainHeading = string.Format("{0}", productName);

            string replaceWith = "</br>";

            ViewBag.lblFromMinPrice = Currency + mininumCost;
            if (description != null)
            {
                ViewBag.lblCatDes = description.Replace("\r\n", replaceWith).Replace("\n", replaceWith).Replace("\r", replaceWith);
                ViewBag.ltrlCatDesc2 = description.Replace("\r\n", replaceWith).Replace("\n", replaceWith).Replace("\r", replaceWith);
            }

            ViewBag.lblCategoryCode = code;
            ViewBag.lblCategoryCode2 = code;

        }
        /// <summary>
        /// bind Price Matrix Data
        /// </summary>
        /// <param name="tblItemsPriceMatrix"></param>
        /// <param name="mode"></param>
        private void BindPriceMatrixData(List<ItemPriceMatrix> tblItemsPriceMatrix, bool mode, MyCompanyDomainBaseResponse baseResponse)
        {
            
            // Remove Zero entries
            if (mode == false)
            {
                tblItemsPriceMatrix = PriceMatrix(tblItemsPriceMatrix, false, baseResponse);
            }
            else
            {
                tblItemsPriceMatrix = PriceMatrix(tblItemsPriceMatrix, true, baseResponse);
            }

            long itemID = 0;
            if (tblItemsPriceMatrix.Count > 0)
            {
                itemID = tblItemsPriceMatrix[0].ItemId.Value;
                //This is required to dynamiacally build header 
                List<ItemStockOption> Stocks = null;

                Stocks = _vwItemSect_StockItems;
                
                if (Stocks != null)
                {
                    //Bind price data
                    _priceMatrixListCount = tblItemsPriceMatrix.Count;
                    // if mode is false then it is fixed quantity 
                    // if mode is true it is range
                    if (mode == true)
                    {

                       ViewBag.txtIsQuantityRanged = "true";
                    }
                    else
                    {
                        ViewBag.txtIsQuantityRanged = "false";
                    }

                    ViewData["PriceMatrix"] = tblItemsPriceMatrix;




                    if (Stocks != null)
                        ViewData["Stocks"] = Stocks;
                   
                }
            }
        }

        public List<ItemPriceMatrix> PriceMatrix(List<ItemPriceMatrix> tblRefItemsPriceMatrix, bool IsRanged, MyCompanyDomainBaseResponse baseResponse)
        {

            return GetStanderedPriceMatrix(tblRefItemsPriceMatrix, IsRanged,baseResponse);
          
        }
        public List<ItemPriceMatrix> GetStanderedPriceMatrix(List<ItemPriceMatrix> tblRefItemsPriceMatrix, bool IsRanged,MyCompanyDomainBaseResponse baseResponse)
        {
            int flagid = 0;
          
           
            if (_myClaimHelper.isUserLoggedIn())
            {
                flagid = Convert.ToInt32(baseResponse.Company.FlagId);
                if (flagid == 0)
                {
                    flagid = _IItemService.GetDefaultSectionPriceFlag();
                    if (IsRanged == true)
                    {
                        tblRefItemsPriceMatrix = tblRefItemsPriceMatrix.Where(c => c.QtyRangeFrom > 0 && c.QtyRangeTo > 0 && c.FlagId == flagid).ToList();
                    }
                    else
                    {
                        tblRefItemsPriceMatrix = tblRefItemsPriceMatrix.Where(c => c.Quantity > 0 && c.FlagId == flagid).ToList();
                    }
                }
                else
                {
                    if (IsRanged == true)
                    {
                        tblRefItemsPriceMatrix = tblRefItemsPriceMatrix.Where(c => c.QtyRangeFrom > 0 && c.QtyRangeTo > 0 && c.FlagId == flagid).ToList();
                    }
                    else
                    {
                        tblRefItemsPriceMatrix = tblRefItemsPriceMatrix.Where(c => c.Quantity > 0 && c.FlagId == flagid).ToList();
                    }
                    if (tblRefItemsPriceMatrix.Count == 0)
                    {
                        flagid = _IItemService.GetDefaultSectionPriceFlag();
                        if (IsRanged == true)
                        {
                            tblRefItemsPriceMatrix = tblRefItemsPriceMatrix.Where(c => c.QtyRangeFrom > 0 && c.QtyRangeTo > 0 && c.FlagId == flagid).ToList();
                        }
                        else
                        {
                            tblRefItemsPriceMatrix = tblRefItemsPriceMatrix.Where(c => c.Quantity > 0 && c.FlagId == flagid).ToList();
                        }
                    }
                }
            }
            else
            {
                flagid = _IItemService.GetDefaultSectionPriceFlag();
                if (IsRanged == true)
                {
                    tblRefItemsPriceMatrix = tblRefItemsPriceMatrix.Where(c => c.QtyRangeFrom > 0 && c.QtyRangeTo > 0 && c.FlagId == flagid).ToList();
                }
                else
                {
                    tblRefItemsPriceMatrix = tblRefItemsPriceMatrix.Where(c => c.Quantity > 0 && c.FlagId == flagid).ToList();
                }
            }

            return tblRefItemsPriceMatrix;
        }


      

        public void LoadRelatedItems(long ItemID, string sProductName, MyCompanyDomainBaseResponse baseResponseCurrency, bool IsShowPrices)
        {
            List<ProductItem> allRelatedItemsList = null;
           
             allRelatedItemsList = _IItemService.GetRelatedItemsByItemID(ItemID);

            

            if (allRelatedItemsList != null && allRelatedItemsList.Count > 0)
            {
              
                allRelatedItemsList = allRelatedItemsList.OrderBy(i => i.SortOrder).ToList();

                RIviewModel.ProductItems = allRelatedItemsList;
                RIviewModel.ProductName = sProductName;
                RIviewModel.CurrencySymbol = baseResponseCurrency.Currency;
                RIviewModel.isShowPrices = IsShowPrices;
                ViewData["RIViewModel"] = RIviewModel;
            }
            else
            {
                ViewData["RIViewModel"] = null;
            }

        }
       

        
      
    }
}