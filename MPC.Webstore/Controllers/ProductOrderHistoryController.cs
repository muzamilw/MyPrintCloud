using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using MPC.Webstore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public void BindGrid(int statusID, long contactID, SearchOrderViewModel model)
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

            //grdViewOrderhistory.DataSource = ordersList;
            //grdViewOrderhistory.DataBind();

            //Do pager Setting
            //TotalRecordsCount = _totalRcordCount;
            //ControlPagerSettings(TotalRecordsCount, pageNumber);
            ViewBag.OrderList = ordersList;
        }
        [HttpPost]
        public ActionResult Index(SearchOrderViewModel model)
        {

            BindGrid(model.SelectedOrder, _myClaimHelper.loginContactID(), model);

            return View("PartialViews/ProductOrderHistory", model);
        }
        
    }
}