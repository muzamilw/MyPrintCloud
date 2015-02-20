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
        public ActionResult Paypal(int OrderID)
        {
            PaypalViewModel opaypal = new PaypalViewModel();
            try
            {

                if (OrderID > 0)
                {
                    
                   
                    PaymentGateway oGateWay = _ItemService.GetPaymentGatewayRecord(UserCookieManager.StoreId);



                    opaypal.return_url = "";
                    opaypal.notify_url = "";
                    opaypal.cancel_url = "";
                    opaypal.discount_amount_cart = "0";
                    opaypal.upload = "1";
                    opaypal.business = oGateWay.BusinessEmail; 

                    CultureInfo ci = new CultureInfo("en-us");

                   // opaypal.return_url += string.Format("?{0}={1}", ParameterName.ORDER_ID, PageParameters.OrderID);
                    opaypal.pageOrderID = OrderID.ToString();
                    // determining the URL to work with depending on whether sandbox or a real PayPal account should be used
                    if (ConfigurationManager.AppSettings["UseSandbox"].ToString() == "true")
                        opaypal.URL = "https://www.sandbox.paypal.com/cgi-bin/webscr";
                    else
                        opaypal.URL = "https://www.paypal.com/cgi-bin/webscr";


                    //This parameter determines the was information about successfull transaction 
                    //will be passed to the script specified in the return_url parameter.
                    // "1" - no parameters will be passed.
                    // "2" - the POST method will be used.
                    // "0" - the GET method will be used. 
                    // The parameter is "0" by deault.
                    if (ConfigurationManager.AppSettings["SendToReturnURL"].ToString() == "true")
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
                   
                    ShoppingCart shopCart = _OrderService.GetShopCartOrderAndDetails(OrderID, 0);
   
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
            catch (Exception ex)
            {
                
            }
            return View(opaypal);
        }



    }
}