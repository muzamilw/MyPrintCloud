using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class OrderConfirmationController : Controller
    {
        private readonly IOrderService _OrderService;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        private readonly IItemService _ItemService;
        private readonly ICompanyService _myCompanyService;
        public OrderConfirmationController(IOrderService OrderService, IWebstoreClaimsHelperService myClaimHelper, ICompanyService myCompanyService, IItemService ItemService)
        {
            if (OrderService == null)
            {
                throw new ArgumentNullException("OrderService");
            }
            this._OrderService = OrderService;
            this._myClaimHelper = myClaimHelper;
            this._myCompanyService = myCompanyService;
            this._ItemService = ItemService;
        }
        // GET: OrderConfirmation
        public ActionResult Index(string OrderId)
        {
            long OrderID = Convert.ToInt64(OrderId);
            if (OrderID > 0)
            {
                ShoppingCart shopCart = _OrderService.GetShopCartOrderAndDetails(OrderID, OrderStatus.ShoppingCart);
                if (shopCart != null)
                {

                    if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                    {
                        CompanyContact oContact = _myCompanyService.GetContactByID(_myClaimHelper.loginContactRoleID());
                        ViewBag.LoginUser = oContact;

                        ViewData["OrderAddresses"] = _myCompanyService.GetContactCompanyAddressesList(shopCart.BillingAddressID, shopCart.ShippingAddressID, oContact.AddressId);
                    }
                    else
                    {
                        ViewData["OrderAddresses"] = _myCompanyService.GetContactCompanyAddressesList(shopCart.BillingAddressID, shopCart.ShippingAddressID, 0);

                    }
                    return View("PartialViews/OrderConfirmation", shopCart);
                }
                else
                {
                    Response.Redirect("/");
                    return null;
                }
            }
            else
            {
                Response.Redirect("/");
                return null;
            }


        }
    }
}