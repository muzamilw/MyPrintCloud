using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
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
            this._StatusService =_StatusService;
            this._orderService = _orderService;
            this._CompanyService = _CompanyService;
            this._itemService = itemService;
            this._MISOrderService = MISOrderService;
        }
        public ActionResult Index()
        {
            long contactid = _myClaimHelper.loginContactID();
            int STATUS_TYPE_ID = 2;
            ViewBag.RecordStatus = false;
            SearchOrderViewModel model = new SearchOrderViewModel();
            if (_myClaimHelper.isUserLoggedIn())
            {
                model = BindStatusDropdown(STATUS_TYPE_ID);
            }
            return View("PartialViews/ProductOrderHistory", model);
        }
        public SearchOrderViewModel BindStatusDropdown(int STATUS_TYPE_ID)
        {
            SearchOrderViewModel SearchOrder = new SearchOrderViewModel();
              if (_myClaimHelper.loginContactRoleID() == (int)Roles.Adminstrator)
              {
                List<Status> statusList = _StatusService.GetStatusListByStatusTypeID(STATUS_TYPE_ID);
                List<Status> list = new List<Status>();
                list = ReturnStatus();
                foreach (var item in list)
                {
                    statusList.Add(item);
                }
                
                if (statusList.Count > 0)
                {
                    SearchOrder.DDOderStatus = new SelectList(statusList, "StatusId", "StatusName");
                }
            }
            else
            {
                    List<Status> list = new List<Status>();
                    list = ReturnStatus();
                    SearchOrder.DDOderStatus = new SelectList(list, "StatusId", "StatusName");
            }

            BindGrid(0, _myClaimHelper.loginContactID(), SearchOrder);
            return SearchOrder;
        }
        public void BindGrid(long statusID, long contactID, SearchOrderViewModel model)
        {
            List<Order> ordersList = null;
            
            OrderStatus? status = null;
            if (statusID > 0)
                status = (OrderStatus)statusID;

          //  ViewBag.ContactID = _myClaimHelper.loginContactRoleID();

            //if (UserCookieManager.StoreMode == (int)StoreMode.Corp && _myClaimHelper.loginContactRoleID() == (int)Roles.User)
            //{
            //    ordersList = _orderService.GetOrdersListExceptPendingOrdersByContactID(contactID, status, model.FromData, model.ToDate, model.poSearch, 0, 0);
                
            //}
            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp && _myClaimHelper.loginContactRoleID()== (int)Roles.Adminstrator)
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
            if (ordersList == null || ordersList.Count == 0)
            {
            }
            else
            {
                //if ((SessionParameters.StoreMode == StoreMode.Broker) && SessionParameters.IsUserAdmin == true)
                //{
                //    if (ddlClientStatus.SelectedIndex != -1 && ddlClientStatus.SelectedIndex != 0)
                //    {
                //        int SelectdIndexVal = Convert.ToInt32(ddlClientStatus.SelectedValue);
                //        ordersList = ordersList.Where(i => i.ClientStatusID == SelectdIndexVal).ToList();
                //        _totalRcordCount = ordersList.Count;
                //    }
                //}
                //else
                //{
                //    if (ddlOrderStatuses.SelectedIndex != -1 && ddlOrderStatuses.SelectedIndex != 0)
                //    {
                //        int SelectdIndexVal = Convert.ToInt32(ddlOrderStatuses.SelectedValue);
                //        ordersList = ordersList.Where(i => i.ClientStatusID == SelectdIndexVal).ToList();
                //        _totalRcordCount = ordersList.Count;
                //    }
                //}
                //lblTxtOfRest.Text = _totalRcordCount + " matches found"; //CountordersList.Count + " matches found";
                //lblTxtOfRest.Visible = true;

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
                    TempData["Status"] = "No Records Found";
                    TempData["HeaderStatus"] = false;
                }
                else {
                    TempData["Status"] = ordersList.Count+"    " + " Record Match ";
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
            if (_myClaimHelper.loginContactRoleID() == (int)Roles.Adminstrator)
            {
                List<Status> statusList = _StatusService.GetStatusListByStatusTypeID(2);
                List<Status> list = new List<Status>();
                list = ReturnStatus();
                foreach (var item in list)
                {
                    statusList.Add(item);
                }

                if (statusList.Count > 0)
                {
                    model.DDOderStatus = new SelectList(statusList, "StatusId", "StatusName");
                }
            }
            else
            {
                List<Status> list = new List<Status>();
                list = ReturnStatus();
                model.DDOderStatus = new SelectList(list, "StatusId", "StatusName");
            }
              BindGrid(model.SelectedOrder, _myClaimHelper.loginContactID(), model);
          //  List<Status> statusList = _StatusService.GetStatusListByStatusTypeID(2);
            //model.DDOderStatus = new SelectList(statusList, "StatusId", "StatusName");
            return View("PartialViews/ProductOrderHistory", model);
        }
        //[HttpGet]
        //public ActionResult OrderResult(long OrderId)
        //{
        //  //  _orderService.GetShopCartOrderAndDetails();
        //  //  ShoppingCart cart = LoadShoppingCart(OrderId);
        //    Order order = _orderService.GetOrderAndDetails(OrderId);
           
        //    CalculateProductDescription(order);
        //    ViewBag.order = order;
        //    ViewBag.BillingAddress = _orderService.GetBillingAddress(order.BillingAddressID);
        //    ViewBag.DeliveryAddress = _orderService.GetdeliveryAddress(order.DeliveryAddressID);
        //    //if (ViewBag.order && ViewBag.BillingAddress && ViewBag.DeliveryAddress != null)
        //   // {
                
        //   // }
        //        return Json("");
        //       // return PartialView("~/Views/Shared/PartialViews/ViewOrder");
        //}
        [HttpPost]
        public JsonResult OrderResult(long OrderId, string OrderType)
        {
            if (OrderType == "ReOrder") 
            {
                long UpdatedOrder = _itemService.ReOrder(OrderId, _myClaimHelper.loginContactID(), UserCookieManager.TaxRate, StoreMode.Retail, true, 0, UserCookieManager.WEBOrganisationID, UserCookieManager.WBStoreId);
                UserCookieManager.WEBOrderId = UpdatedOrder;

                return Json(UpdatedOrder, JsonRequestBehavior.DenyGet);
            }

            if (OrderType == "Download")
            {
                string Exception = "";
                try
                {
                    string DownloadFileLink = _MISOrderService.DownloadOrderArtwork((int)OrderId, "", UserCookieManager.WEBOrganisationID);
                    DownloadFileLink = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority + DownloadFileLink;
                    return Json(DownloadFileLink, JsonRequestBehavior.DenyGet);
                }
                catch (Exception e)
                {
                    throw e;
                }
                
            }
            return Json(true, JsonRequestBehavior.DenyGet);
           
        }
        //[HttpPost]
        //public JsonResult DownLoadArtWork(long OrderId)
        //{
           
        //}

   

        private ShoppingCart LoadShoppingCart(long orderID)
        {
            ShoppingCart shopCart = null;

            double _deliveryCost = 0;
            double _deliveryCostTaxVal = 0;
            shopCart = _orderService.GetShopCartOrderAndDetails(orderID, OrderStatus.ShoppingCart);
            if (shopCart != null)
            {
                //Model.SelectedItemsAddonsList = shopCart.ItemsSelectedAddonsList;
                ////ViewData["selectedItemsAddonsList"] = _selectedItemsAddonsList;
                ////global values for all items
                //CostCentre deliveryCostCenter = null;
                //int deliverCostCenterID;
                //_deliveryCost = shopCart.DeliveryCost;
                ////  ViewBag.DeliveryCost = shopCart.DeliveryCost;
                //Model.DeliveryCost = shopCart.DeliveryCost;
                //_deliveryCostTaxVal = shopCart.DeliveryTaxValue;
                ////  ViewBag.DeliveryCostTaxVal = _deliveryCostTaxVal;
                //Model.DeliveryCostTaxVal = shopCart.DeliveryTaxValue;
                //BillingID = shopCart.BillingAddressID;
                //ShippingID = shopCart.ShippingAddressID;

            }
            return shopCart;
        }

        private void CalculateProductDescription(Order order,out double GrandTotal,out double Subtotal,out double vat)
        {

            double Delevery = 0;
            double DeliveryTaxValue = 0;
            double TotalVat = 0;
            double calculate = 0;
             Subtotal = 0;
             vat = 0;
             GrandTotal = 0;

            {
                //List<tbl_items> items = context.tbl_items.Where(i => i.EstimateID == OrderID).ToList();
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
               // ViewBag.GrandTotal = GrandTotal;
               // ViewBag.SubTotal = Subtotal;
               // ViewBag.Vat = calculate;
            }
           
        }
        private List<Status> ReturnStatus()
        {
            List<Status> list = new List<Status>();
            Status newStatus1 = new Status();
            newStatus1.StatusId = 38;
            newStatus1.StatusName="In Progress";
            list.Add(newStatus1);
            Status newStatus2 = new Status();
            newStatus2.StatusId = 37;
            newStatus2.StatusName = "Completed";
            list.Add(newStatus2);
            return list;

        }
    }
    //public class JasonResponseObject
    //{
        
    //    public Address billingAddress;

    //}
}