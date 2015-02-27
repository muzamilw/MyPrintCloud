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
        

        public ProductOrderHistoryController(IWebstoreClaimsHelperService _myClaimHelper, IStatusService _StatusService, IOrderService _orderService)
        {
            this._myClaimHelper = _myClaimHelper;
            this._StatusService =_StatusService;
            this._orderService = _orderService;
        }
        public ActionResult Index()
        {
            
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
            if (_myClaimHelper.loginContactRoleID() ==(int) Roles.Adminstrator)
            {
                List<Status> statusList = _StatusService.GetStatusListByStatusTypeID(STATUS_TYPE_ID);
                if (statusList.Count > 0)
                {
                    SearchOrder.DDOderStatus = new SelectList(statusList, "StatusId", "StatusName");
                }

                BindGrid(0, _myClaimHelper.loginContactID(),SearchOrder);
            }
            return SearchOrder;
        }
        public void BindGrid(long statusID, long contactID, SearchOrderViewModel model)
        {
            List<Order> ordersList = null;
            
            OrderStatus? status = null;
            if (statusID > 0)
                status = (OrderStatus)statusID;
            
            if (UserCookieManager.StoreMode == (int)StoreMode.Corp && _myClaimHelper.loginContactRoleID() == (int)Roles.User)
            {
                ordersList = _orderService.GetOrdersListExceptPendingOrdersByContactID(contactID, status, model.FromData, model.ToDate, model.poSearch, 0, 0);
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
             if (ordersList == null || ordersList.Count == 0)
                {
                    TempData["Status"] = "No Records Found";
                }
                else {
                    TempData["Status"] = ordersList.Count+"    " + " Record Match ";
                }
               ViewBag.OrderList = ordersList;
        }
        [HttpPost]
        public ActionResult Index(SearchOrderViewModel model)
        {
            BindGrid(model.SelectedOrder, _myClaimHelper.loginContactID(), model);
            List<Status> statusList = _StatusService.GetStatusListByStatusTypeID(2);
            model.DDOderStatus = new SelectList(statusList, "StatusId", "StatusName");
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
        public JsonResult OrderResult(long OrderId)
        {
              long UpdatedOrder = _orderService.ReOrder(OrderId, _myClaimHelper.loginContactID(), UserCookieManager.TaxRate, StoreMode.Retail, true, 0);
              UserCookieManager.OrderId = UpdatedOrder;
              //JasonResponseObject obj = new JasonResponseObject();
            //obj.billingAddress = _orderService.GetBillingAddress(159296);
           
            //var formatter = new JsonMediaTypeFormatter();
            //var json = formatter.SerializerSettings;
            //json.Formatting = Newtonsoft.Json.Formatting.Indented;
            //json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
           // Response.Write(list);

             return Json(true,JsonRequestBehavior.DenyGet);
        }
       
        //public   ShowPartialView(long OrderId)
        //{
        //    Order order = _orderService.GetOrderAndDetails(OrderId);
        //    CalculateProductDescription(order);
        //    ViewBag.order = order;
        //    ViewBag.BillingAddress = _orderService.GetBillingAddress(order.BillingAddressID);
        //    ViewBag.DeliveryAddress = _orderService.GetdeliveryAddress(order.DeliveryAddressID);
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
      
    }
    //public class JasonResponseObject
    //{
        
    //    public Address billingAddress;

    //}
}