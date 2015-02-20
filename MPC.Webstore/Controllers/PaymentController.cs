using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Models.DomainModels;
using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.Common;
using MPC.Webstore.ViewModels;
using System.Globalization;
using System.Configuration;
using MPC.Models.Common;
using System.Runtime.Caching;

namespace MPC.Webstore.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IItemService _ItemService;
        private readonly IOrderService _OrderService;

        public PaymentController(IItemService ItemService, IOrderService OrderService)
        {
            this._ItemService = ItemService;
            this._OrderService = OrderService;
        }

        // GET: Payment
        public ActionResult PaypalSubmit(int OrderID)
        {
           // int OrderID = 16633;
            PaypalViewModel opaypal = new PaypalViewModel();
            try
            {

                if (OrderID > 0)
                {
                    string CacheKeyName = "CompanyBaseResponse";
                    ObjectCache cache = MemoryCache.Default;
                    MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.StoreId];
                    
                    PaymentGateway oGateWay = _ItemService.GetPaymentGatewayRecord(UserCookieManager.StoreId);
                    if (oGateWay != null)
                    {
                        opaypal.return_url = oGateWay.ReturnUrl;
                        opaypal.notify_url = oGateWay.NotifyUrl;
                        opaypal.cancel_url = oGateWay.CancelPurchaseUrl;
                        opaypal.discount_amount_cart = "0";
                        opaypal.upload = "1";
                        opaypal.business = oGateWay.BusinessEmail;
                        opaypal.cmd = "_cart";
                        opaypal.currency_code = StoreBaseResopnse.Currency;
                        opaypal.no_shipping = "1";
                        opaypal.handling_cart = "0";


                        opaypal.return_url += string.Format("?{0}={1}", "OrderID", OrderID);
                        opaypal.pageOrderID = OrderID.ToString();
                        // determining the URL to work with depending on whether sandbox or a real PayPal account should be used
                        if (oGateWay.UseSandbox)
                            opaypal.URL = oGateWay.TestApiUrl;// "https://www.sandbox.paypal.com/cgi-bin/webscr";
                        else
                            opaypal.URL = oGateWay.LiveApiUrl;// "https://www.paypal.com/cgi-bin/webscr";

                        if (oGateWay.SendToReturnURL)
                            opaypal.rm = "2";
                        else
                            opaypal.rm = "1";


                        Estimate order = _OrderService.GetOrderByID(OrderID);
                        if (order != null && order.DeliveryCost.HasValue)
                        {
                            opaypal.handling_cart = Math.Round(order.DeliveryCost.Value, 2, MidpointRounding.AwayFromZero).ToString("#.##");
                        }
                        else
                        {
                            opaypal.handling_cart = "0";
                        }

                        ShoppingCart shopCart = _OrderService.GetShopCartOrderAndDetails(OrderID, OrderStatus.ShoppingCart);

                        if (shopCart != null && shopCart.CartItemsList != null)
                        {
                            List<PaypalOrderParameter> itemsList = new List<PaypalOrderParameter>();

                            foreach (ProductItem item in shopCart.CartItemsList)
                            {
                                PaypalOrderParameter prodItem = new PaypalOrderParameter
                                {
                                    ProductName = item.ProductName,
                                    UnitPrice = Math.Round((item.Qty1GrossTotal ?? 1.00), 2, MidpointRounding.AwayFromZero),
                                    TotalQuantity = 1
                                };

                                itemsList.Add(prodItem);
                            }

                            opaypal.txtJason = Newtonsoft.Json.JsonConvert.SerializeObject(itemsList);

                        }

                    }
                    
                }
            }
            catch (Exception ex)
            {
                
            }
            return View(opaypal);
        }



        public ActionResult PaypalIPN()
        {
            return View();
        }
        //public ActionResult IPNHandler() { }
        

    }
}