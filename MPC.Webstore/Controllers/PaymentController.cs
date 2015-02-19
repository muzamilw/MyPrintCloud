using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        //public ActionResult Paypal(int OrderID)
        //{
        //    try
        //    {

        //        if (OrderID > 0)
        //        {

        //            tbl_PaymentGateways oGateWay = null;


        //            if (SessionParameters.StoreMode == StoreMode.Retail)
        //            {
        //                oGateWay = PaymentsManager.GetPaymentGatewayRecord();
        //            }
        //            else if (SessionParameters.StoreMode == StoreMode.Broker && SessionParameters.BrokerContactCompany.isBrokerCanAcceptPaymentOnline == true)
        //            {
        //                oGateWay = PaymentsManager.GetPaymentGatewayRecordByCompanyID(SessionParameters.BrokerContactCompany.ContactCompanyID);
        //            }
        //            else if (SessionParameters.StoreMode == StoreMode.Broker && SessionParameters.BrokerContactCompany.isBrokerCanAcceptPaymentOnline == false)
        //            {
        //                oGateWay = PaymentsManager.GetPaymentGatewayRecord();
        //            }
        //            else if (SessionParameters.StoreMode == StoreMode.Corp && SessionParameters.ContactCompany.isBrokerCanAcceptPaymentOnline == true)
        //            {
        //                oGateWay = PaymentsManager.GetCorporatePaymentGatewayRecord(SessionParameters.ContactCompany.ContactCompanyID);
        //            }
        //            else if (SessionParameters.StoreMode == StoreMode.Corp && SessionParameters.ContactCompany.isBrokerCanAcceptPaymentOnline == false)
        //            {
        //                oGateWay = PaymentsManager.GetPaymentGatewayRecord();
        //            }

        //            return_url = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + return_url;
        //            notify_url = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + notify_url;
        //            cancel_url = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + cancel_url;

        //            business = oGateWay.BusinessEmail;

        //            CultureInfo ci = new CultureInfo("en-us");

        //            return_url += string.Format("?{0}={1}", ParameterName.ORDER_ID, PageParameters.OrderID);
        //            pageOrderID = PageParameters.OrderID.ToString();
        //            // determining the URL to work with depending on whether sandbox or a real PayPal account should be used
        //            if (ConfigurationManager.AppSettings["UseSandbox"].ToString() == "true")
        //                URL = "https://www.sandbox.paypal.com/cgi-bin/webscr";
        //            else
        //                URL = "https://www.paypal.com/cgi-bin/webscr";


        //            //This parameter determines the was information about successfull transaction 
        //            //will be passed to the script specified in the return_url parameter.
        //            // "1" - no parameters will be passed.
        //            // "2" - the POST method will be used.
        //            // "0" - the GET method will be used. 
        //            // The parameter is "0" by deault.
        //            if (ConfigurationManager.AppSettings["SendToReturnURL"].ToString() == "true")
        //                rm = "2";
        //            else
        //                rm = "1";


        //            tbl_estimates order = OrderManager.GetOrderByID(PageParameters.OrderID);
        //            if (order != null && order.DeliveryCost.HasValue)
        //            {
        //                handling_cart = Math.Round(order.DeliveryCost.Value, 2, MidpointRounding.AwayFromZero).ToString("#.##");
        //            }
        //            else
        //            {
        //                handling_cart = "0";
        //            }
        //            Model.ShoppingCart shopCart = null;
        //            if (SessionParameters.StoreMode == StoreMode.Broker)
        //            {
        //                shopCart = LoadShoppingCart(Convert.ToInt64(PageParameters.OrderID), SessionParameters.BrokerContactCompany.ContactCompanyID);
        //            }
        //            else
        //            {
        //                shopCart = LoadShoppingCart(Convert.ToInt64(PageParameters.OrderID), 0);
        //            }


        //            if (shopCart != null && shopCart.CartItemsList != null)
        //            {
        //                List<Model.PaypalOrderParameter> itemsList = new List<Model.PaypalOrderParameter>();

        //                foreach (Model.ProductItem item in shopCart.CartItemsList)
        //                {
        //                    Model.PaypalOrderParameter prodItem = new Model.PaypalOrderParameter
        //                    {
        //                        ProductName = item.ProductName,
        //                        UnitPrice = Math.Round((item.Qty1GrossTotal ?? 1.00), 2, MidpointRounding.AwayFromZero),
        //                        TotalQuantity = 1
        //                    };

        //                    itemsList.Add(prodItem);
        //                }

        //                string convert = Newtonsoft.Json.JsonConvert.SerializeObject(itemsList);
        //                txtHiddenJason.Value = convert;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogError(ex);
        //    }
        //    return View();
        //}



    }
}