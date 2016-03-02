using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
using MPC.Webstore.Common;
using MPC.Webstore.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class ProductOrderHistoryController : Controller
    {
        // GET: ProductOrderHistory
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        private readonly IStatusService _StatusService;
        private readonly IOrderService _orderService;
        private readonly ICompanyService _CompanyService;
        private readonly IItemService _itemService;
        private readonly MPC.Interfaces.MISServices.IOrderService _MISOrderService;
        public ProductOrderHistoryController(IWebstoreClaimsHelperService _myClaimHelper, IStatusService _StatusService,
            IOrderService _orderService, ICompanyService _CompanyService, IItemService itemService,
            MPC.Interfaces.MISServices.IOrderService MISOrderService)
        {
            this._myClaimHelper = _myClaimHelper;
            this._StatusService = _StatusService;
            this._orderService = _orderService;
            this._CompanyService = _CompanyService;
            this._itemService = itemService;
            this._MISOrderService = MISOrderService;
        }
        public ActionResult Index()
        {
            MyCompanyDomainBaseReponse StoreBaseResopnse = _CompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            long contactid = _myClaimHelper.loginContactID();
            int STATUS_TYPE_ID = 2;
            ViewBag.RecordStatus = false;
            SearchOrderViewModel model = new SearchOrderViewModel();
            if (_myClaimHelper.isUserLoggedIn())
            {
                model = BindStatusDropdown(STATUS_TYPE_ID);
            }
            ViewBag.IsShowPrices = _CompanyService.ShowPricesOnStore(UserCookieManager.WEBStoreMode, StoreBaseResopnse.Company.ShowPrices ?? false, _myClaimHelper.loginContactID(), UserCookieManager.ShowPriceOnWebstore);

            ViewBag.LoginContactId = _myClaimHelper.loginContactID();

            ViewBag.ReporTID= _CompanyService.GetReportIdByName("Order Report By Store");
            ViewBag.StoreID = UserCookieManager.WBStoreId;

            ViewBag.LoginContactRoleID = _myClaimHelper.loginContactRoleID();
            ViewBag.RejectedOrder = OrderStatus.RejectOrder;
            return View("PartialViews/ProductOrderHistory", model);
        }
        public SearchOrderViewModel BindStatusDropdown(int STATUS_TYPE_ID)
        {
            SearchOrderViewModel SearchOrder = new SearchOrderViewModel();

            List<Status> statusList = _StatusService.GetStatusListByStatusTypeID(STATUS_TYPE_ID);

            if (statusList.Count > 0)
            {
                SearchOrder.DDOderStatus = new SelectList(statusList, "StatusId", "StatusName");
            }


            BindGrid(0, _myClaimHelper.loginContactID(), SearchOrder);
            ViewBag.LoginContactRoleID = _myClaimHelper.loginContactRoleID();
            return SearchOrder;
        }
        public void BindGrid(long statusID, long contactID, SearchOrderViewModel model)
        {
            ViewBag.LoginContactRoleID = _myClaimHelper.loginContactRoleID();
            List<Order> ordersList = null;

            OrderStatus? status = null;
            if (statusID > 0)
                status = (OrderStatus)statusID;

            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp && _myClaimHelper.loginContactRoleID() == (int)Roles.Adminstrator)
            {
                ordersList = _orderService.GetAllCorpOrders(_myClaimHelper.loginContactCompanyID(), status, model.FromData, model.ToDate, model.poSearch, false, _myClaimHelper.loginContactTerritoryID());
            }
            else if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp && _myClaimHelper.loginContactRoleID() == (int)Roles.Manager)
            {
                ordersList = _orderService.GetAllCorpOrders(_myClaimHelper.loginContactCompanyID(), status, model.FromData, model.ToDate, model.poSearch, true, _myClaimHelper.loginContactTerritoryID());
            }
            else
            {
                ordersList = _orderService.GetOrdersListByContactID(contactID, status, model.FromData, model.ToDate, model.poSearch, 0, 0);
            }

            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
            {
                ViewBag.res = null;
            }
            else
            {
                ViewBag.res = string.Empty;
            }
            if (ordersList == null || ordersList.Count == 0)
            {
                TempData["Status"] = Utils.GetKeyValueFromResourceFile("ltrlNoRecFound", UserCookieManager.WBStoreId, "No Records Found");
                TempData["HeaderStatus"] = false;
            }
            else
            {
                TempData["Status"] = ordersList.Count + "    " + Utils.GetKeyValueFromResourceFile("ltrlrocrdsmatch", UserCookieManager.WBStoreId, "Record Match");
                TempData["HeaderStatus"] = true;
            }
            ViewBag.OrderList = ordersList;
            ViewBag.TotalOrder = ordersList.Count;
            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
            {
                ViewBag.res = null;
            }
            else
            {
                ViewBag.res = string.Empty;
            }
        }

        [HttpPost]
        public ActionResult Index(SearchOrderViewModel model)
        {
            ViewBag.LoginContactRoleID = _myClaimHelper.loginContactRoleID();
            //if (ModelState.IsValid)
           // {
                List<Status> statusList = _StatusService.GetStatusListByStatusTypeID(2);

                if (statusList.Count > 0)
                {
                    model.DDOderStatus = new SelectList(statusList, "StatusId", "StatusName");
                }

                BindGrid(model.SelectedOrder, _myClaimHelper.loginContactID(), model);
                MyCompanyDomainBaseReponse StoreBaseResopnse = _CompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);
                ViewBag.IsShowPrices = _CompanyService.ShowPricesOnStore(UserCookieManager.WEBStoreMode, StoreBaseResopnse.Company.ShowPrices ?? false, _myClaimHelper.loginContactID(), UserCookieManager.ShowPriceOnWebstore);
                return View("PartialViews/ProductOrderHistory", model);
            ///}
            //else 
           // {
             //   ControllerContext.HttpContext.Response.RedirectToRoute("Orderhistory");
              //  return null;
            //}
           
        }

        [HttpPost]
        public JsonResult OrderResult(long OrderId, string OrderType)
            {
            Estimate Estimate = _orderService.GetOrderByID(OrderId);
          
            if (OrderType == "ReOrder")
            {
                if (Estimate.StatusId == Convert.ToInt16(OrderStatus.RejectOrder))
                {

                  bool Result=  _CompanyService.UpdateOderStatus(Estimate);

                  if (Result)
                  {

                      bool ItemResult = _CompanyService.UpdateItemsStatus(OrderId);


                      if (ItemResult)
                      {
                          if (UserCookieManager.WEBOrderId == 0)
                          {
                              if (_myClaimHelper.loginContactID() > 0) // is user logged in
                              {
                                  UserCookieManager.WEBOrderId = _orderService.GetOrderIdByContactId(_myClaimHelper.loginContactID(), _myClaimHelper.loginContactCompanyID());
                              }
                          }
                         bool FinalResult= _CompanyService.UpdateOrderAndItemsForRejectOrder(OrderId, UserCookieManager.WEBOrderId);

                         if (FinalResult)
                         {
                             UserCookieManager.WEBOrderId = OrderId;
                         }

                      }
                  }

                }
                else
                {
                    long UpdatedOrder = _itemService.ReOrder(OrderId, _myClaimHelper.loginContactID(), UserCookieManager.TaxRate, StoreMode.Retail, true, 0, UserCookieManager.WEBOrganisationID, UserCookieManager.WBStoreId);
                    UserCookieManager.WEBOrderId = UpdatedOrder;

                    return Json(UserCookieManager.WEBOrderId, JsonRequestBehavior.DenyGet);
                }
            }

            if (OrderType == "Download")
            {

                string DownloadFileLink = _MISOrderService.DownloadOrderArtwork((int)OrderId, "", UserCookieManager.WEBOrganisationID);
                DownloadFileLink = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority + DownloadFileLink;
                return Json(DownloadFileLink, JsonRequestBehavior.DenyGet);

            }
            return Json(UserCookieManager.WEBOrderId, JsonRequestBehavior.DenyGet);

        }
        private ShoppingCart LoadShoppingCart(long orderID)
        {
            ShoppingCart shopCart = _orderService.GetShopCartOrderAndDetails(orderID, OrderStatus.ShoppingCart);

            return shopCart;
        }

        private void CalculateProductDescription(Order order, out double GrandTotal, out double Subtotal, out double vat)
        {

            double Delevery = 0;
            double DeliveryTaxValue = 0;
            double TotalVat = 0;
            double calculate = 0;
            Subtotal = 0;
            vat = 0;
            GrandTotal = 0;

            foreach (var item in order.OrderDetails.CartItemsList)
            {

                if (item.ItemType == (int)ItemTypes.Delivery)
                {
                    Delevery = Convert.ToDouble(item.Qty1NetTotal);
                    DeliveryTaxValue = Convert.ToDouble(item.Qty1GrossTotal - item.Qty1NetTotal);
                }
                else
                {

                    Subtotal = Subtotal + Convert.ToDouble(item.Qty1NetTotal);
                    TotalVat = Convert.ToDouble(item.Qty1GrossTotal) - Convert.ToDouble(item.Qty1NetTotal);
                    calculate = calculate + TotalVat;
                }

            }

            GrandTotal = Subtotal + calculate + DeliveryTaxValue + Delevery;
            vat = calculate;

        }
    }
}