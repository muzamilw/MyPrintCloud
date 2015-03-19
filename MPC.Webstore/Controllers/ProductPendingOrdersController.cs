using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
   
    public class ProductPendingOrdersController : Controller
    {
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        private readonly IStatusService _StatusService;
        private readonly IOrderService _orderService;
        private readonly ICompanyService _CompanyService;
        // GET: ProductPendingOrders

        public ProductPendingOrdersController(IWebstoreClaimsHelperService _myClaimHelper, IStatusService _StatusService, IOrderService _orderService, ICompanyService _CompanyService)
        {
            this._myClaimHelper = _myClaimHelper;
            this._StatusService = _StatusService;
            this._orderService = _orderService;
            this._CompanyService = _CompanyService;
        }
        public ActionResult Index()
        {
            if (_myClaimHelper.isUserLoggedIn())
            {
                bool ApproveOrders = false;
                CompanyContact LoginContact = _CompanyService.GetContactByID(_myClaimHelper.loginContactID());
                if (LoginContact != null)
                {
                    if (LoginContact.ContactRoleId == Convert.ToInt32(Roles.Adminstrator) || LoginContact.ContactRoleId == Convert.ToInt32(Roles.Manager))
                    {
                        ApproveOrders = true;
                    }
                    else
                    {
                        ApproveOrders = false;
                    }
                }
                BindGrid(ApproveOrders, _myClaimHelper.loginContactID(), LoginContact);
            }
            return View("PartialViews/ProductPendingOrders");
        }

        public void BindGrid(bool ApproveOrders,long ContactID,CompanyContact LoginContact)
        {
            List<Order> ordersList = null;
            List<Order> ManagerordersList = new List<Order>();
            ordersList = _CompanyService.GetPendingApprovelOrdersList(ContactID, ApproveOrders);
            if (ordersList == null || ordersList.Count == 0)
            {
                // do nothing
            }
            else {
                if (LoginContact.ContactRoleId == Convert.ToInt32(Roles.Manager))
                {
                    foreach (var o in ordersList)
                    {
                        if (o.ContactTerritoryID == LoginContact.TerritoryId)
                        {
                            ManagerordersList.Add(o);
                        }
                    }
                    if (ManagerordersList == null || ManagerordersList.Count == 0)
                    {
                        
                    }
                    else
                    {
                        ViewBag.OrderList = ManagerordersList;
                    }
                }
                else
                {
                    ViewBag.OrderList= ordersList;
                }
            }
        }
    }
}