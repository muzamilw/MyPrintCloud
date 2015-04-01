using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using System.Runtime.Caching;

namespace MPC.Webstore.Controllers
{
    
    public class FeaturedProductCarousalController : Controller
    {
         #region Private

        private readonly IItemService _itemService;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        private readonly IOrderService _orderService;
        private readonly ICompanyService _myCompanyService;
        private readonly IItemService _IItemService;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public FeaturedProductCarousalController(IItemService itemService, IWebstoreClaimsHelperService myClaimHelper, IOrderService orderService, ICompanyService myCompanyService)
        {
            if (itemService == null)
            {
                throw new ArgumentNullException("itemService");
            }
            this._myClaimHelper = myClaimHelper;
            this._itemService = itemService;
            this._orderService = orderService;
            this._myCompanyService = myCompanyService;
            this._IItemService = itemService;
        }

        #endregion
        // GET: FeaturedProductCarousal
        public ActionResult Index()
        {
            List<Item> featuredProducts = _itemService.GetProductsWithDisplaySettings(ProductWidget.FeaturedProducts, UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);

            ViewBag.ProductsCount = featuredProducts.Count;
            return PartialView("PartialViews/FeaturedProductCarousal", featuredProducts);
         
        }

        public ActionResult CloneFeatureItem(long id)
        {
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;
            long ItemID = 0;
            long TemplateID = 0;
            bool isCorp = true;
            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                isCorp = true;
            else
                isCorp = false;
            int TempDesignerID = 0;
            string ProductName = string.Empty;

            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];

            long ContactID = _myClaimHelper.loginContactID();
            long CompanyID = _myClaimHelper.loginContactCompanyID();
            if (UserCookieManager.WEBOrderId == 0)
            {
                long OrderID = 0;
                long TemporaryRetailCompanyId = 0;
                if (UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)
                {
                    TemporaryRetailCompanyId = UserCookieManager.TemporaryCompanyId;
                    OrderID = _orderService.ProcessPublicUserOrder(string.Empty, StoreBaseResopnse.Organisation.OrganisationId, (StoreMode)UserCookieManager.WEBStoreMode, CompanyID, ContactID, ref TemporaryRetailCompanyId);
                    if (OrderID > 0)
                    {
                        UserCookieManager.WEBOrderId = OrderID;
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
                    OrderID = _orderService.ProcessPublicUserOrder(string.Empty, StoreBaseResopnse.Organisation.OrganisationId, (StoreMode)UserCookieManager.WEBStoreMode, CompanyID, ContactID, ref TemporaryRetailCompanyId);
                    if (OrderID > 0)
                    {
                        UserCookieManager.WEBOrderId = OrderID;
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
                if (UserCookieManager.TemporaryCompanyId == 0 && UserCookieManager.WEBStoreMode == (int)StoreMode.Retail && ContactID == 0)
                {
                    long TemporaryRetailCompanyId = UserCookieManager.TemporaryCompanyId;

                    // create new order

                    long OrderID = _orderService.ProcessPublicUserOrder(string.Empty, StoreBaseResopnse.Organisation.OrganisationId, (StoreMode)UserCookieManager.WEBStoreMode, CompanyID, ContactID, ref TemporaryRetailCompanyId);
                    if (OrderID > 0)
                    {
                        UserCookieManager.WEBOrderId = OrderID;
                    }
                    if (TemporaryRetailCompanyId != 0)
                    {
                        UserCookieManager.TemporaryCompanyId = TemporaryRetailCompanyId;
                        ContactID = _myCompanyService.GetContactIdByCompanyId(TemporaryRetailCompanyId);
                    }
                    CompanyID = TemporaryRetailCompanyId;
                }
                else if (UserCookieManager.TemporaryCompanyId > 0 && UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)
                {
                    CompanyID = UserCookieManager.TemporaryCompanyId;
                    ContactID = _myCompanyService.GetContactIdByCompanyId(CompanyID);
                }
                Item item = _IItemService.CloneItem(id, 0, UserCookieManager.WEBOrderId, CompanyID, 0, 0, null, false, false, ContactID, StoreBaseResopnse.Organisation.OrganisationId);

                if (item != null)
                {
                    ItemID = item.ItemId;
                    TemplateID = item.TemplateId ?? 0;
                    TempDesignerID = item.DesignerCategoryId ?? 0;
                    ProductName = Utils.specialCharactersEncoder(item.ProductName);
                }
            }

            int isCalledFrom = 0;
            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                isCalledFrom = 4;
            else
                isCalledFrom = 3;

            bool isEmbedded;
            bool printWaterMark = true;
            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp || UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)
            {
                isEmbedded = true;
            }
            else
            {
                printWaterMark = false;
                isEmbedded = false;
            }

            ProductName = _IItemService.specialCharactersEncoder(ProductName);
            //Designer/productName/CategoryIDv2/TemplateID/ItemID/companyID/cotnactID/printCropMarks/printWaterMarks/isCalledFrom/IsEmbedded;
            bool printCropMarks = true;
            string URL = "/Designer/" + ProductName + "/" + TempDesignerID + "/" + TemplateID + "/" + ItemID + "/" + CompanyID + "/" + ContactID + "/" + isCalledFrom + "/" + UserCookieManager.WEBOrganisationID + "/" + printCropMarks + "/" + printWaterMark + "/" + isEmbedded;

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
    }
}