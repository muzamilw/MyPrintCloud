using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MPC.Models.DomainModels;
using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.Common;
using MPC.Webstore.ViewModels;
using System.Globalization;
using MPC.Models.Common;
using System.Runtime.Caching;
using System.IO;
using System.Net;
using System.Text;
using MPC.ExceptionHandling;
using MPC.Interfaces.Logger;
using Microsoft.Practices.Unity;
using System.Diagnostics;
using System.Collections;
using System.Linq;
using MPC.Models.ResponseModels;

namespace MPC.Webstore.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IItemService _ItemService;
        private readonly IOrderService _OrderService;
        private readonly ICampaignService _campaignService;
        private readonly ICompanyService _myCompanyService;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        private readonly IUserManagerService _usermanagerService;
        private readonly IPrePaymentService _IPrePaymentService;
        private readonly IPayPalResponseService _PayPalResponseService;
        private readonly ITemplateService _templateService;
        private readonly IPaymentGatewayService _paymentGatewayService;

        public PaymentController(IItemService ItemService, IOrderService OrderService, ICampaignService campaignService, ICompanyService myCompanyService, IWebstoreClaimsHelperService myClaimHelper, IUserManagerService usermanagerService, IPrePaymentService IPrePaymentService, IPayPalResponseService _PayPalResponseService
           , ITemplateService templateService
            , IPaymentGatewayService paymentGatewayService)
        {
            this._ItemService = ItemService;
            this._OrderService = OrderService;
            this._campaignService = campaignService;
            this._myCompanyService = myCompanyService;
            this._myClaimHelper = myClaimHelper;
            this._usermanagerService = usermanagerService;
            this._IPrePaymentService = IPrePaymentService;
            this._PayPalResponseService = _PayPalResponseService;
            this._templateService = templateService;
            this._paymentGatewayService = paymentGatewayService;
        }

        // GET: Payment
        public ActionResult PaypalSubmit(long OrderId)
        {
            PaypalViewModel opaypal = new PaypalViewModel();
            try
            {

                if (OrderId > 0)
                {
                    //string CacheKeyName = "CompanyBaseResponse";
                    //ObjectCache cache = MemoryCache.Default;
                    //MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
                    MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

                    PaymentGateway oGateWay = _paymentGatewayService.GetPaymentByMethodId(StoreBaseResopnse.Company.CompanyId, (int)PaymentMethods.PayPal);
                    if (oGateWay != null)
                    {
                        opaypal.return_url = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/Receipt/" + OrderId;
                        opaypal.notify_url = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/PaypalIPN";
                        opaypal.cancel_url = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/ShopCart?OrderId=" + OrderId + "&Error=UserCancelled";

                       // opaypal.return_url = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority + "/Receipt/" + OrderId;//oGateWay.ReturnUrl;
                       // opaypal.notify_url = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority + "/PaypalIPN"; //oGateWay.NotifyUrl;
                       // opaypal.cancel_url = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority + "/ShopCart/" + OrderId; //oGateWay.CancelPurchaseUrl;
                        opaypal.discount_amount_cart = "0";
                        opaypal.upload = "1";
                        opaypal.business = oGateWay.BusinessEmail;
                        opaypal.cmd = "_cart";
                        opaypal.currency_code = _myCompanyService.GetCurrencyCodeById(Convert.ToInt64(StoreBaseResopnse.Organisation.CurrencyId));
                        opaypal.no_shipping = "1";
                        opaypal.handling_cart = "0";
                        opaypal.custom = OrderId.ToString();

                        //opaypal.pageOrderID = OrderId.ToString();
                        // determining the URL to work with depending on whether sandbox or a real PayPal account should be used
                        if (oGateWay.UseSandbox.HasValue && oGateWay.UseSandbox.Value)
                            opaypal.URL = "https://www.sandbox.paypal.com/cgi-bin/webscr"; //oGateWay.TestApiUrl;//
                        else
                            opaypal.URL = "https://www.paypal.com/cgi-bin/webscr"; //oGateWay.LiveTestUrl;

                        if (true)//oGateWay.SendToReturnURL.HasValue && oGateWay.SendToReturnURL.Value
                            opaypal.rm = "2";
                        else
                            opaypal.rm = "1";


                        Estimate order = _OrderService.GetOrderByID(OrderId);


                        List<Item> CartItemsList = _OrderService.GetOrderItemsIncludingDelivery(OrderId, (int)OrderStatus.ShoppingCart);
                        Item DeliveryItem = CartItemsList.Where(c => c.ItemType == (int)ItemTypes.Delivery).FirstOrDefault();
                        double VATTotal = 0;
                        if (CartItemsList != null)
                        {
                            List<PaypalOrderParameter> itemsList = new List<PaypalOrderParameter>();

                            foreach (Item item in CartItemsList)
                            {
                                if (item.ItemType != (int)ItemTypes.Delivery)
                                {
                                    PaypalOrderParameter prodItem = new PaypalOrderParameter
                                    {
                                        ProductName = item.ProductName,
                                        UnitPrice = Utils.FormatDecimalValueToTwoDecimal(item.Qty1BaseCharge1),
                                        TotalQuantity = 1
                                    };
                                    VATTotal = VATTotal + item.Qty1Tax1Value ?? 0;
                                    itemsList.Add(prodItem);
                                }
                            }

                            if(DeliveryItem != null)
                            {
                                PaypalOrderParameter prodItem = new PaypalOrderParameter
                                {
                                    ProductName = "Shipping: " + DeliveryItem.ProductName,
                                    UnitPrice = Utils.FormatDecimalValueToTwoDecimal(DeliveryItem.Qty1BaseCharge1),
                                    TotalQuantity = 1
                                };
                                VATTotal = VATTotal + DeliveryItem.Qty1Tax1Value ?? 0;
                                itemsList.Add(prodItem);
                            }

                            if (VATTotal == 0)
                            {
                                opaypal.tax_cart = "0.00";
                            }
                            else 
                            {
                                opaypal.tax_cart = VATTotal.ToString("#.##");
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

        [HttpPost]
        public ActionResult PaypalIPN()
        {
            try
            {
                
              
                int outCustomRequestID = 0;

                int.TryParse(Request["custom"], out  outCustomRequestID);
                int orderID = outCustomRequestID;// GetRequestOrderID(outCustomRequestID);
                Estimate modelOrder = null;
                string strFormValues = Encoding.ASCII.GetString(Request.BinaryRead(Request.ContentLength));
                string strNewValue;

                long StoreId = _OrderService.GetStoreIdByOrderId(orderID);

                if (StoreId > 0)
                {
                    PaymentGateway oGateWay = _paymentGatewayService.GetPaymentByMethodId(StoreId, (int)PaymentMethods.PayPal);
                    // getting the URL to work with
                    string URL;
                    if (oGateWay.UseSandbox.HasValue && oGateWay.UseSandbox.Value)
                        URL = "https://www.sandbox.paypal.com/cgi-bin/webscr";//oGateWay.TestApiUrl;
                    else
                        URL = "https://www.paypal.com/cgi-bin/webscr"; //oGateWay.LiveApiUrl;

                    // Create the request back
                    HttpWebRequest req = (HttpWebRequest)(WebRequest.Create(URL));

                    // Set values for the request back
                    req.Method = "POST";
                    req.ContentType = "application/x-www-form-urlencoded";
                    strNewValue = strFormValues + "&cmd=_notify-validate";
                    req.ContentLength = strNewValue.Length;

                    // Write the request back IPN strings
                    StreamWriter stOut = new StreamWriter(req.GetRequestStream(), Encoding.ASCII);
                    stOut.Write(strNewValue);
                    stOut.Close();

                    // send the request, read the response
                    HttpWebResponse strResponse = (HttpWebResponse)(req.GetResponse());
                    Stream IPNResponseStream = strResponse.GetResponseStream();
                    Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                    StreamReader readStream = new StreamReader(IPNResponseStream, encode);
                    string strResponseString = readStream.ReadToEnd();
                    readStream.Close();
                    strResponse.Close();

                    double outGrossTotal = 0;

                    double.TryParse(Request["mc_gross"], out outGrossTotal);



                    //string amount = GetRequestPrice(this.Request["custom"].ToString());

                    // if the request is verified
                    if (String.Compare(strResponseString, "VERIFIED", false) == 0)
                    {

                        string pendingReasion = string.IsNullOrWhiteSpace(this.Request["pending_reason"]) ? string.Empty : this.Request["pending_reason"];

                        // write a response from PayPal
                        long payPalResponseID = this.CreatePaymentResponses(orderID, this.Request["txn_id"],
                             outGrossTotal,
                             this.Request["payer_email"],
                             this.Request["first_name"],
                             this.Request["last_name"],
                             this.Request["address_street"],
                             this.Request["address_city"],
                             this.Request["address_state"],
                             this.Request["address_zip"],
                             this.Request["address_country"],
                             outCustomRequestID, false, pendingReasion);
                        //Carts.WriteFile("Success in IPNHandler: PaymentResponses created");
                        ///////////////////////////////////////////////////
                        // Here we notify the person responsible for goods delivery that the payment was performed and 
                        // providing him with all needed information about the payment. Some flags informing that user paid 
                        // for a services can be also set here. For example, if user paid for registration on the site, 
                        // then the flag should be set allowing the user who paid to access the site
                        //////////////////////////////////////////////////

                        long? customerID = null;
                        if (payPalResponseID > 0)
                        {
                            //CompanySiteManager CSM = new CompanySiteManager();
                            //   company_sites Serversettingss = CompanySiteManager.GetCompanySite();
                            string paymentStatus = this.Request["payment_status"];
                            StoreMode ModeOfStore = StoreMode.Retail;
                            if (string.Compare(paymentStatus, "pending", true) == 0 || string.Compare(paymentStatus, "completed", true) == 0)
                            {
                                modelOrder = _OrderService.GetOrderByID(orderID);

                                if (modelOrder != null)
                                    customerID = modelOrder.CompanyId;

                                // order code and order creation date
                                CampaignEmailParams cep = new CampaignEmailParams();

                                string AttachmentPath = null;
                                cep.ContactId = modelOrder.ContactId ?? 0;
                                cep.CompanyId = modelOrder.CompanyId;
                                cep.EstimateId = orderID; //PageParameters.OrderID;
                                Company Store = _myCompanyService.GetCompanyByCompanyID(StoreId);
                                Company CustomerCompany = _myCompanyService.GetCompanyByCompanyID(modelOrder.CompanyId);
                                CompanyContact CustomrContact = _myCompanyService.GetContactByID(cep.ContactId);
                                _OrderService.SetOrderCreationDateAndCode(orderID, UserCookieManager.WEBOrganisationID);
                                SystemUser EmailOFSM = _usermanagerService.GetSalesManagerDataByID(Store.SalesAndOrderManagerId1.Value);

                                if (Store.IsCustomer == (int)CustomerTypes.Corporate)
                                {
                                    AttachmentPath = _templateService.OrderConfirmationPDF(orderID, StoreId);
                                    ModeOfStore = StoreMode.Corp;
                                }
                                else
                                {
                                    AttachmentPath = _templateService.OrderConfirmationPDF(orderID, StoreId);
                                }
                                List<string> AttachmentList = new List<string>();
                                AttachmentList.Add(AttachmentPath);
                                cep.OrganisationId = Store.OrganisationId ?? 0;
                                Campaign OnlineOrderCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.OnlineOrder, Store.OrganisationId ?? 0, Store.CompanyId);
                                cep.SalesManagerContactID = Convert.ToInt32(modelOrder.ContactId);
                                cep.StoreId = Store.CompanyId;
                                cep.AddressId = Convert.ToInt32(modelOrder.CompanyId);
                                long ManagerID = _myCompanyService.GetContactIdByRole(_myClaimHelper.loginContactCompanyID(), (int)Roles.Manager); //ContactManager.GetBrokerByRole(Convert.ToInt32(modelOrder.CompanyId), (int)Roles.Manager); 
                                cep.CorporateManagerID = ManagerID;
                                if (CustomerCompany.IsCustomer == (int)CustomerTypes.Customers) ///Retail Mode
                                {
                                    _campaignService.emailBodyGenerator(OnlineOrderCampaign, cep, CustomrContact, StoreMode.Retail, Convert.ToInt32(Store.OrganisationId), "", "", "", EmailOFSM.Email, "", "", AttachmentList);
                                    _campaignService.SendEmailToSalesManager((int)Events.NewOrderToSalesManager, (int)modelOrder.ContactId, (int)modelOrder.CompanyId, orderID, Store.OrganisationId ?? 0, 0, StoreMode.Retail, Store.CompanyId, EmailOFSM);
                                }
                                else
                                {
                                    _campaignService.emailBodyGenerator(OnlineOrderCampaign, cep, CustomrContact, StoreMode.Corp, Convert.ToInt32(Store.OrganisationId), "", "", "", EmailOFSM.Email, "", "", AttachmentList);
                                    _campaignService.SendEmailToSalesManager((int)Events.NewOrderToSalesManager, Convert.ToInt32(modelOrder.ContactId), Convert.ToInt32(modelOrder.CompanyId), orderID, Store.OrganisationId ?? 0, Convert.ToInt32(ManagerID), StoreMode.Corp, Store.CompanyId, EmailOFSM);

                                }

                                //in case of retail <<SalesManagerEmail>> variable should be resolved by organization's sales manager
                                // thats why after getting the sales manager records ew are sending his email as a parameter in email body genetor




                                _IPrePaymentService.CreatePrePayment(PaymentMethods.PayPal, orderID, Convert.ToInt32(customerID), payPalResponseID, this.Request["txn_id"], outGrossTotal, ModeOfStore);

                               
                            }
                            else
                            {
                                throw new Exception("Invalid Payment Status");
                            }
                        }
                        else
                        {
                            throw new Exception("Invalid paypal responseid.");
                        }
                    }
                    else
                    {
                        throw new Exception("INVALID HandShake_against_RequestID =  " + outCustomRequestID.ToString() + " " + DateTime.Now.ToString());
                    }
                }
                else
                {
                    throw new Exception("No Paymnet Gatway Set.");
                }

            }
            catch (Exception ex)
            {
                string virtualFolderPth = System.Web.HttpContext.Current.Server.MapPath("~/mpc_content/Exception/ErrorLog.txt");
                using (StreamWriter writer = new StreamWriter(virtualFolderPth, true))
                {
                    writer.WriteLine("From PayPal IPN Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                       "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                }
            }
            return View();
        }

        // GET: Payment
        public ActionResult ANZSubmit(long OrderId)
        {

            string queryString = "https://migs.mastercard.com.au/vpcpay";
            string seperator = "?";
            try
            {

                if (OrderId > 0)
                {
                    //string CacheKeyName = "CompanyBaseResponse";
                    //ObjectCache cache = MemoryCache.Default;
                    //MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
                    MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

                    PaymentGateway oGateWay = _paymentGatewayService.GetPaymentByMethodId(StoreBaseResopnse.Company.CompanyId, (int)PaymentMethods.ANZ);
                    if (oGateWay != null)
                    {
                        System.Collections.SortedList transactionData = new System.Collections.SortedList(new VPCStringComparer());

                        transactionData.Add("vpc_Version", 1);
                        transactionData.Add("vpc_Command", "pay");
                        transactionData.Add("vpc_AccessCode", oGateWay.IdentityToken);
                        transactionData.Add("vpc_MerchTxnRef", OrderId.ToString());
                        transactionData.Add("vpc_Merchant", oGateWay.BusinessEmail);
                        string SECURE_SECRET = oGateWay.SecureHash;
                        string rawHashData = oGateWay.SecureHash;

                        Estimate order = _OrderService.GetOrderByID(OrderId);
                       

                        transactionData.Add("vpc_OrderInfo", order.EstimateId.ToString());
                        transactionData.Add("vpc_Amount", Convert.ToInt32(order.Estimate_Total * 100));
                        transactionData.Add("vpc_ReturnURL", Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/ANZResponse");
                        transactionData.Add("vpc_TicketNo", "");
                        transactionData.Add("vpc_Locale", "en");

                        transactionData.Add("AgainLink", Request.ServerVariables["HTTP_REFERER"]);


                        // Loop through all the data in the SortedList transaction data
                        foreach (System.Collections.DictionaryEntry item in transactionData)
                        {

                            // build the query string, URL Encoding all keys and their values
                            queryString += seperator + System.Web.HttpUtility.UrlEncode(item.Key.ToString()) + "=" + System.Web.HttpUtility.UrlEncode(item.Value.ToString());
                            seperator = "&";

                            // Collect the data required for the MD5 sugnature if required
                            if (SECURE_SECRET.Length > 0)
                            {
                                rawHashData += item.Value.ToString();
                            }
                        }
                        //transactionData.Add("Title", Request.Form["Title"]);

                        // Create the MD5 signature if required
                        string signature = "";
                        if (SECURE_SECRET.Length > 0)
                        {
                            // create the signature and add it to the query string
                            signature = CreateMD5Signature(rawHashData);
                            queryString += seperator + "vpc_SecureHash=" + signature;

                        }

                    
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return Redirect(queryString);
        }


        public ActionResult WorldPaySubmit(long OrderId)
        {
            WorldPayViewModel opaypal = new WorldPayViewModel();
            try
            {

                if (OrderId > 0)
                {
                    //string CacheKeyName = "CompanyBaseResponse";
                    //ObjectCache cache = MemoryCache.Default;
                    //MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
                    MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

                    PaymentGateway oGateWay = _paymentGatewayService.GetPaymentByMethodId(StoreBaseResopnse.Company.CompanyId, (int)PaymentMethods.WorldPay);
                    if (oGateWay != null)
                    {
                        opaypal.return_url = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/Receipt/" + OrderId;
                        opaypal.notify_url = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/PaypalIPN";
                        opaypal.cancel_url = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/ShopCart?OrderId=" + OrderId + "&Error=UserCancelled";

                        // opaypal.return_url = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority + "/Receipt/" + OrderId;//oGateWay.ReturnUrl;
                        // opaypal.notify_url = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority + "/PaypalIPN"; //oGateWay.NotifyUrl;
                        // opaypal.cancel_url = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority + "/ShopCart/" + OrderId; //oGateWay.CancelPurchaseUrl;


                        opaypal.InstallationID = oGateWay.BusinessEmail;
                        
                        opaypal.currency_code = _myCompanyService.GetCurrencyCodeById(Convert.ToInt64(StoreBaseResopnse.Organisation.CurrencyId));
                        opaypal.no_shipping = "1";
                        opaypal.handling_cart = "0";
                        opaypal.OrderID = OrderId.ToString();

                          


                        Estimate order = _OrderService.GetOrderByID(OrderId);
                        


                        var oContact = _myCompanyService.GetContactByID(order.ContactId.Value);
                        var oAddress = _myCompanyService.GetAddressByID(order.BillingAddressId.Value);

                        opaypal.fullName = oContact.FirstName + " " + oContact.LastName;
                        opaypal.email = oContact.Email;
                        opaypal.OrderTotal = order.Estimate_Total.Value;


                        opaypal.address1 = oAddress.Address1;
                        opaypal.address2 = oAddress.Address2;
                        opaypal.city = oAddress.City;
                        opaypal.postcode = oAddress.PostCode;
                        
                        opaypal.phone = oContact.Mobile;


                       

                        string Description = "";


                        List<Item> CartItemsList = _OrderService.GetOrderItemsIncludingDelivery(OrderId, (int)OrderStatus.ShoppingCart);
                        Item DeliveryItem = CartItemsList.Where(c => c.ItemType == (int)ItemTypes.Delivery).FirstOrDefault();
                        double VATTotal = 0;
                        if (CartItemsList != null)
                        {
                            List<PaypalOrderParameter> itemsList = new List<PaypalOrderParameter>();

                            foreach (Item item in CartItemsList)
                            {
                                if (item.ItemType != (int)ItemTypes.Delivery)
                                {
                                    
                                        Description += item.ProductName + "<br>";
                                        
                                        
                                    
                                    VATTotal = VATTotal + item.Qty1Tax1Value ?? 0;
                                   
                                }
                            }

                            if (DeliveryItem != null)
                            {
                               
                                     Description += "Shipping: " + DeliveryItem.ProductName;
                                   
                                
                                VATTotal = VATTotal + DeliveryItem.Qty1Tax1Value ?? 0;
                               
                            }

                            if (VATTotal == 0)
                            {
                                opaypal.tax_cart = "0.00";
                            }
                            else
                            {
                                opaypal.tax_cart = VATTotal.ToString("#.##");
                            }

                            opaypal.description = Description;

                        }

                    }

                }
            }
            catch (Exception ex)
            {

            }
            return View("WorldPayGateway", opaypal);
        }


             [HttpPost]
        public ActionResult WorldPayResponse()
        {
            try
            {



                int orderID = Convert.ToInt32(Request["cartId"]);

                if (orderID > 0)
                {
                    long StoreId = _OrderService.GetStoreIdByOrderId(orderID);

                    PaymentGateway oGateWay = _paymentGatewayService.GetPaymentByMethodId(StoreId, (int)PaymentMethods.WorldPay);

                    string output = "";

                    // error exists flag
                    bool errorExists = false;

                    // transaction response code
                    string txnResponseCode = "";



                    // Initialise the Local Variables
                    output = "<font color='orange'><b>NOT CALCULATED</b></font>";
                    bool hashValidated = true;

                    try
                    {


                        // Get the standard receipt data from the parsed response
                        txnResponseCode = Request["transStatus"].Length > 0 ? Request["transStatus"] : "Unknown";

                        //if (!hashValidated)
                        //{
                        //    Response.Redirect("/ShopCart?Error=Failed&ErrorMessage=ANZ Hash validation failed, Query string is tempered. Contact support");
                        //}
                        if (txnResponseCode == "C")
                        {
                            Response.Redirect("/ShopCart?Error=UserCancelled");
                        }
                        //else if (!txnResponseCode.Equals("0"))
                        //{
                        //    Response.Redirect("/ShopCart?Error=Failed&ErrorMessage=" + getANZResponseDescription(txnResponseCode));
                        //}

                        output += "Response Code : " + txnResponseCode;
                        output += "Response Code desc : " + getANZResponseDescription(txnResponseCode);

                        output += "amount : " + (Request["amount"].Length > 0 ? Request["amount"] : "Unknown");

                        //output += "merchant id  : " + (Request["vpc_Merchant"].Length > 0 ? Request["vpc_Merchant"] : "Unknown");

                        // only display this data if not an error condition
                        if (!errorExists && txnResponseCode.Equals("Y"))
                        {
                            output += "instId  : " + (Request["instId"].Length > 0 ? Request["instId"] : "Unknown");
                            output += "card type  : " + (Request["cardType"].Length > 0 ? Request["cardType"] : "Unknown");
                            output += "transId  : " + (Request["transId"].Length > 0 ?Request["transId"] : "Unknown");

                            output += "cartId  : " + (Request["cartId"].Length > 0 ? Request["cartId"] : "Unknown");


                            ////////////////////////////////processing the payment information
                            long? customerID = null;
                            Estimate modelOrder = _OrderService.GetOrderByID(orderID);
                            StoreMode ModeOfStore = StoreMode.Retail;
                            if (modelOrder != null)
                                customerID = modelOrder.CompanyId;

                            // order code and order creation date
                            CampaignEmailParams cep = new CampaignEmailParams();

                            string AttachmentPath = null;
                            cep.ContactId = modelOrder.ContactId ?? 0;
                            cep.CompanyId = modelOrder.CompanyId;
                            cep.EstimateId = orderID; //PageParameters.OrderID;
                            Company Store = _myCompanyService.GetCompanyByCompanyID(StoreId);
                            Company CustomerCompany = _myCompanyService.GetCompanyByCompanyID(modelOrder.CompanyId);
                            CompanyContact CustomrContact = _myCompanyService.GetContactByID(cep.ContactId);
                            _OrderService.SetOrderCreationDateAndCode(orderID, UserCookieManager.WEBOrganisationID);
                            SystemUser EmailOFSM = _usermanagerService.GetSalesManagerDataByID(Store.SalesAndOrderManagerId1.Value);

                            if (Store.IsCustomer == (int)CustomerTypes.Corporate)
                            {
                                AttachmentPath = _templateService.OrderConfirmationPDF(orderID, StoreId); // corp
                                ModeOfStore = StoreMode.Corp;
                            }
                            else
                            {
                                AttachmentPath = _templateService.OrderConfirmationPDF(orderID, StoreId); // retail
                            }
                            List<string> AttachmentList = new List<string>();
                            AttachmentList.Add(AttachmentPath);
                            cep.OrganisationId = Store.OrganisationId ?? 0;
                            Campaign OnlineOrderCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.OnlineOrder, Store.OrganisationId ?? 0, Store.CompanyId);
                            cep.SalesManagerContactID = Convert.ToInt32(modelOrder.ContactId);
                            cep.StoreId = Store.CompanyId;
                            cep.AddressId = Convert.ToInt32(modelOrder.CompanyId);
                            long ManagerID = _myCompanyService.GetContactIdByRole(_myClaimHelper.loginContactCompanyID(), (int)Roles.Manager); //ContactManager.GetBrokerByRole(Convert.ToInt32(modelOrder.CompanyId), (int)Roles.Manager); 
                            cep.CorporateManagerID = ManagerID;
                            if (CustomerCompany.IsCustomer == (int)CustomerTypes.Customers) ///Retail Mode
                            {
                                _campaignService.emailBodyGenerator(OnlineOrderCampaign, cep, CustomrContact, StoreMode.Retail, Convert.ToInt32(Store.OrganisationId), "", "", "", EmailOFSM.Email, "", "", AttachmentList);
                                _campaignService.SendEmailToSalesManager((int)Events.NewOrderToSalesManager, (int)modelOrder.ContactId, (int)modelOrder.CompanyId, orderID, Store.OrganisationId ?? 0, 0, StoreMode.Retail, Store.CompanyId, EmailOFSM);
                            }
                            else
                            {
                                _campaignService.emailBodyGenerator(OnlineOrderCampaign, cep, CustomrContact, StoreMode.Corp, Convert.ToInt32(Store.OrganisationId), "", "", "", EmailOFSM.Email, "", "", AttachmentList);
                                _campaignService.SendEmailToSalesManager((int)Events.NewOrderToSalesManager, Convert.ToInt32(modelOrder.ContactId), Convert.ToInt32(modelOrder.CompanyId), orderID, Store.OrganisationId ?? 0, Convert.ToInt32(ManagerID), StoreMode.Corp, Store.CompanyId, EmailOFSM);

                            }

                            //in case of retail <<SalesManagerEmail>> variable should be resolved by organization's sales manager
                            // thats why after getting the sales manager records ew are sending his email as a parameter in email body genetor




                            _IPrePaymentService.CreatePrePayment(PaymentMethods.PayPal, orderID, Convert.ToInt32(customerID), 0, Request.QueryString["vpc_TransactionNo"], Convert.ToDouble(modelOrder.Estimate_Total), ModeOfStore);


                            Response.Redirect("/Receipt/" + orderID.ToString());

                        }


                    }
                    catch (Exception ex)
                    {
                        throw ex;

                    }

                }
                else
                {
                        Response.Redirect("/ShopCart?Error=Failed&ErrorMessage=InvalidOrderID" );
                }



                }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }
       
        public ActionResult ANZResponse()
        {
            try
            {

                /*
            <summary>Checks a VPC 3-Party transaction response from an incoming HTTP GET</summary>
            <remarks>
            <para>
            This program loops through the QueryString data and calcultaes an MD5 hash 
            signature can be calculated to check if the data has changed in
            transmission. Remember the data is returned back to the merchant via a 
            cardholder's browser redirect so the data has the potential to be changed. 
            </para>

            <para>
            If the MD5 hash signature is not the same value as the incoming signature
            that was calculated by the Payment Server then the receipt data has changed 
            in transit and should be checked.
            </para>

            <para>
            To find out what is included in your queryString data you can enable DEBUG 
            and run a test transaction. The postData string will then be output to the 
            screen. This debug code allows the user to see all the data associated with 
            the transaction. DEBUG should be disabled or removed entirely in production 
            code.
            </para>

            <para>
            To enable DEBUG, change the ASP directive at the top of this file.
            <para>

            <code>   <%@ Page Language="C#" DEBUG=false %>   </code>
            <para>                    to                     </para>
            <code>   <%@ Page Language="C#" DEBUG=true %>    </code>

            </remarks>
            */

                /*********************************************
                 * Define Variables
                 *********************************************/

                int orderID = Convert.ToInt32(Request.QueryString["vpc_MerchTxnRef"]);

                if (orderID > 0)
                {
                    long StoreId = _OrderService.GetStoreIdByOrderId(orderID);

                    PaymentGateway oGateWay = _paymentGatewayService.GetPaymentByMethodId(StoreId, (int)PaymentMethods.ANZ);

                    string output = "";


                    // This is secret for encoding the MD5 hash
                    // This secret will vary from merchant to merchant
                    // To not create a secure hash, let SECURE_SECRET be an empty string - ""
                    // SECURE_SECRET can be found in Merchant Administration/Setup page
                    string SECURE_SECRET = oGateWay != null ? oGateWay.SecureHash : "";

                
                    // define message string for errors
                    string message = "";

                    // error exists flag
                    bool errorExists = false;

                    // transaction response code
                    string txnResponseCode = "";

                    string rawHashData = SECURE_SECRET;

                    // Initialise the Local Variables
                    output = "<font color='orange'><b>NOT CALCULATED</b></font>";
                    bool hashValidated = true;

                    try
                    {
                        /*
                         *************************
                         * START OF MAIN PROGRAM *
                         *************************
                         */

                        // collect debug information
# if DEBUG
                        debugData += "<br/><u>Start of Debug Data</u><br/><br/>";
# endif

                        // If we have a SECURE_SECRET then validate the incoming data using the MD5 hash
                        //included in the incoming data
                        if (Request.QueryString["vpc_SecureHash"] != null && Request.QueryString["vpc_SecureHash"].Length > 0)
                        {

                            // collect debug information
# if DEBUG
                            debugData += "<u>Data from Payment Server</u><br/>";
# endif

                            // loop through all the data in the Request.Form
                            foreach (string item in Request.QueryString)
                            {

                                // collect debug information
# if DEBUG
                                debugData += item + "=" + Request.QueryString[item] + "<br/>";
# endif

                                // Collect the data required for the MD5 sugnature if required
                                if (SECURE_SECRET.Length > 0 && !item.Equals("vpc_SecureHash"))
                                {
                                    rawHashData += Request.QueryString[item];
                                }
                            }
                        }

                        // Create the MD5 signature if required
                        string signature = "";
                        if (SECURE_SECRET.Length > 0)
                        {
                            // create the signature and add it to the query string
                            signature = CreateMD5Signature(rawHashData);

                            // Collect debug information
# if DEBUG
                            debugData += "<br/><u>Hash Data Input</u>: " + rawHashData + "<br/><br/><u>Signature Created</u>: " + signature + "<br/>";
# endif

                            // Validate the Secure Hash (remember MD5 hashes are not case sensitive)
                            if (Request.QueryString["vpc_SecureHash"] != null && Request.QueryString["vpc_SecureHash"].Equals(signature))
                            {
                                // Secure Hash validation succeeded,
                                // add a data field to be displayed later.
                                output += "<font color='#00AA00'><b>CORRECT</b></font>";
                            }
                            else
                            {
                                // Secure Hash validation failed, add a data field to be displayed
                                // later.
                                output += "<font color='#FF0066'><b>INVALID HASH</b></font>";
                                hashValidated = false;
                            }
                        }


                        // Get the standard receipt data from the parsed response
                        txnResponseCode = Request.QueryString["vpc_TxnResponseCode"].Length > 0 ? Request.QueryString["vpc_TxnResponseCode"] : "Unknown";

                        if (!hashValidated)
                        {
                            Response.Redirect("/ShopCart?Error=Failed&ErrorMessage=ANZ Hash validation failed, Query string is tempered. Contact support");
                        }
                        else if (txnResponseCode == "C")
                        {
                            Response.Redirect("/ShopCart?Error=UserCancelled");
                        }
                        else if (!txnResponseCode.Equals("0"))
                        {
                            Response.Redirect("/ShopCart?Error=Failed&ErrorMessage=" + getANZResponseDescription(txnResponseCode));
                        }

                        output += "Response Code : " + txnResponseCode;
                        output += "Response Code desc : " + getANZResponseDescription(txnResponseCode);

                        output += "amount : " + (Request.QueryString["vpc_Amount"].Length > 0 ? Request.QueryString["vpc_Amount"] : "Unknown");
                        output += "command  : " + (Request.QueryString["vpc_Command"].Length > 0 ? Request.QueryString["vpc_Command"] : "Unknown");
                        output += "version  : " + ( Request.QueryString["vpc_Version"].Length > 0 ? Request.QueryString["vpc_Version"] : "Unknown");
                        output += "order ibnfo  : " + ( Request.QueryString["vpc_OrderInfo"].Length > 0 ? Request.QueryString["vpc_OrderInfo"] : "Unknown");
                        output += "merchant id  : " + ( Request.QueryString["vpc_Merchant"].Length > 0 ? Request.QueryString["vpc_Merchant"] : "Unknown");

                        // only display this data if not an error condition
                        if (!errorExists && txnResponseCode.Equals("0"))
                        {
                            output += "batch no  : " + ( Request.QueryString["vpc_BatchNo"].Length > 0 ? Request.QueryString["vpc_BatchNo"] : "Unknown");
                            output += "card type  : " + ( Request.QueryString["vpc_Card"].Length > 0 ? Request.QueryString["vpc_Card"] : "Unknown");
                            output += "receipt no  : " + ( Request.QueryString["vpc_ReceiptNo"].Length > 0 ? Request.QueryString["vpc_ReceiptNo"] : "Unknown");
                            output += "authorize id  : " + (Request.QueryString["vpc_AuthorizeId"].Length > 0 ? Request.QueryString["vpc_AuthorizeId"] : "Unknown");
                            output += "merch txn ref  : " + ( Request.QueryString["vpc_MerchTxnRef"].Length > 0 ? Request.QueryString["vpc_MerchTxnRef"] : "Unknown");
                            output += "acq resp code  : " + ( Request.QueryString["vpc_AcqResponseCode"].Length > 0 ? Request.QueryString["vpc_AcqResponseCode"] : "Unknown");
                            output += "txn no  : " + ( Request.QueryString["vpc_TransactionNo"].Length > 0 ? Request.QueryString["vpc_TransactionNo"] : "Unknown");
                            




                            ////////////////////////////////processing the payment information
                            long? customerID = null;
                            Estimate modelOrder = _OrderService.GetOrderByID(orderID);
                            StoreMode ModeOfStore = StoreMode.Retail;
                            if (modelOrder != null)
                                customerID = modelOrder.CompanyId;

                            // order code and order creation date
                            CampaignEmailParams cep = new CampaignEmailParams();

                            string AttachmentPath = null;
                            cep.ContactId = modelOrder.ContactId ?? 0;
                            cep.CompanyId = modelOrder.CompanyId;
                            cep.EstimateId = orderID; //PageParameters.OrderID;
                            Company Store = _myCompanyService.GetCompanyByCompanyID(StoreId);
                            Company CustomerCompany = _myCompanyService.GetCompanyByCompanyID(modelOrder.CompanyId);
                            CompanyContact CustomrContact = _myCompanyService.GetContactByID(cep.ContactId);
                            _OrderService.SetOrderCreationDateAndCode(orderID, UserCookieManager.WEBOrganisationID);
                            SystemUser EmailOFSM = _usermanagerService.GetSalesManagerDataByID(Store.SalesAndOrderManagerId1.Value);

                            if (Store.IsCustomer == (int)CustomerTypes.Corporate)
                            {
                                AttachmentPath = _templateService.OrderConfirmationPDF(orderID, StoreId); // corp
                                ModeOfStore = StoreMode.Corp;
                            }
                            else
                            {
                                AttachmentPath = _templateService.OrderConfirmationPDF(orderID, StoreId); // retail
                            }
                            List<string> AttachmentList = new List<string>();
                            AttachmentList.Add(AttachmentPath);
                            cep.OrganisationId = Store.OrganisationId ?? 0;
                            Campaign OnlineOrderCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.OnlineOrder, Store.OrganisationId ?? 0, Store.CompanyId);
                            cep.SalesManagerContactID = Convert.ToInt32(modelOrder.ContactId);
                            cep.StoreId = Store.CompanyId;
                            cep.AddressId = Convert.ToInt32(modelOrder.CompanyId);
                            long ManagerID = _myCompanyService.GetContactIdByRole(_myClaimHelper.loginContactCompanyID(), (int)Roles.Manager); //ContactManager.GetBrokerByRole(Convert.ToInt32(modelOrder.CompanyId), (int)Roles.Manager); 
                            cep.CorporateManagerID = ManagerID;
                            if (CustomerCompany.IsCustomer == (int)CustomerTypes.Customers) ///Retail Mode
                            {
                                _campaignService.emailBodyGenerator(OnlineOrderCampaign, cep, CustomrContact, StoreMode.Retail, Convert.ToInt32(Store.OrganisationId), "", "", "", EmailOFSM.Email, "", "", AttachmentList);
                                _campaignService.SendEmailToSalesManager((int)Events.NewOrderToSalesManager, (int)modelOrder.ContactId, (int)modelOrder.CompanyId, orderID, Store.OrganisationId ?? 0, 0, StoreMode.Retail, Store.CompanyId, EmailOFSM);
                            }
                            else
                            {
                                _campaignService.emailBodyGenerator(OnlineOrderCampaign, cep, CustomrContact, StoreMode.Corp, Convert.ToInt32(Store.OrganisationId), "", "", "", EmailOFSM.Email, "", "", AttachmentList);
                                _campaignService.SendEmailToSalesManager((int)Events.NewOrderToSalesManager, Convert.ToInt32(modelOrder.ContactId), Convert.ToInt32(modelOrder.CompanyId), orderID, Store.OrganisationId ?? 0, Convert.ToInt32(ManagerID), StoreMode.Corp, Store.CompanyId, EmailOFSM);

                            }

                            //in case of retail <<SalesManagerEmail>> variable should be resolved by organization's sales manager
                            // thats why after getting the sales manager records ew are sending his email as a parameter in email body genetor




                            _IPrePaymentService.CreatePrePayment(PaymentMethods.PayPal, orderID, Convert.ToInt32(customerID), 0, Request.QueryString["vpc_TransactionNo"], Convert.ToDouble(modelOrder.Estimate_Total), ModeOfStore);


                            Response.Redirect("/Receipt/" + orderID.ToString());

                        }

                        // if message was not provided locally then obtain value from server
                        if (message.Length == 0)
                        {
                            message = Request.QueryString["vpc_Message"].Length > 0 ? Request.QueryString["vpc_Message"] : "Unknown";
                        }
                    }
                    catch (Exception ex)
                    {
                        message = "(51) Exception encountered. " + ex.Message;
                        if (ex.StackTrace.Length > 0)
                        {
                            output  += ex.ToString();
                           
                        }
                        errorExists = true;
                    }

                    // output the message field
                    output += message;

                    /* The URL AgainLink and Title are only used for display purposes.
                     * Note: This is ONLY used for this example and is not required for 
                     * production code.  */

                    // Create a link to the example's HTML order page
                    //Label_AgainLink.Text = "<a href=\"" + Request.QueryString["AgainLink"] + "\">Another Transaction</a>";

                    // Determine the appropriate title for the receipt page
                   output += (errorExists || txnResponseCode.Equals("7") || txnResponseCode.Equals("Unknown") || hashValidated == false) ? Request.QueryString["Title"] + " Error Page" : Request.QueryString["Title"] + " Receipt Page";

                    // output debug data to the screen
# if DEBUG
                    debugData += "<br/><u>End of debug information</u><br/>";
                    output += debugData;
                   
# endif

                }





            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }
       



        public long CreatePaymentResponses(int orderID, string txn_id, double payment_price, string payerEmail, string first_name, string last_name, string street, string city, string state, string zip,
                                      string country, int request_id, bool is_success, string reason_fault)
        {

           
            long pkey = 0;
            try
            {
                CultureInfo ci = new CultureInfo("en-us");
            
                pkey = _PayPalResponseService.WritePayPalResponse(request_id,
                     Convert.ToInt32(orderID),
                     txn_id,
                     this.Request["txn_type"],
                     payment_price,
                     this.Request["receiver_email"],
                     this.Request["payment_status"],
                     this.Request["payment_type"],
                     payerEmail,
                     this.Request["payer_status"],
                     first_name, last_name, street, city, state, zip, country, is_success, reason_fault, ci);
            }
            catch (Exception ex)
            {
               // LogError(ex);
                //KBSoft.Carts.WriteFile("Error in IPNHandler.CreatePaymentResponses(): " + ex.Message)
            }

         

            return pkey;
        }



        /*
   Version 3.1

   ---------------- Disclaimer --------------------------------------------------

   Copyright 2004 Dialect Solutions Holdings.  All rights reserved.

   This document is provided by Dialect Holdings on the basis that you will treat
   it as confidential.

   No part of this document may be reproduced or copied in any form by any means
   without the written permission of Dialect Holdings.  Unless otherwise
   expressly agreed in writing, the information contained in this document is
   subject to change without notice and Dialect Holdings assumes no
   responsibility for any alteration to, or any error or other deficiency, in
   this document.

   All intellectual property rights in the Document and in all extracts and
   things derived from any part of the Document are owned by Dialect and will be
   assigned to Dialect on their creation. You will protect all the intellectual
   property rights relating to the Document in a manner that is equal to the
   protection you provide your own intellectual property.  You will notify
   Dialect immediately, and in writing where you become aware of a breach of
   Dialect's intellectual property rights in relation to the Document.

   The names "Dialect", "QSI Payments" and all similar words are trademarks of
   Dialect Holdings and you must not use that name or any similar name.

   Dialect may at its sole discretion terminate the rights granted in this
   document with immediate effect by notifying you in writing and you will
   thereupon return (or destroy and certify that destruction to Dialect) all
   copies and extracts of the Document in its possession or control.

   Dialect does not warrant the accuracy or completeness of the Document or its
   content or its usefulness to you or your merchant customers.   To the extent
   permitted by law, all conditions and warranties implied by law (whether as to
   fitness for any particular purpose or otherwise) are excluded.  Where the
   exclusion is not effective, Dialect limits its liability to $100 or the
   resupply of the Document (at Dialect's option).

   Data used in examples and sample data files are intended to be fictional and
   any resemblance to real persons or companies is entirely coincidental.

   Dialect does not indemnify you or any third party in relation to the content
   or any use of the content as contemplated in these terms and conditions.

   Mention of any product not owned by Dialect does not constitute an endorsement
   of that product.

   This document is governed by the laws of New South Wales, Australia and is
   intended to be legally binding.

   Author: Dialect Solutions Group Pty Ltd

   ------------------------------------------------------------------------------*/

        /*
        <summary>ASP.NET C# 3-Party example for the Virtual Payment Client</summary>
        <remarks>

        <para>
        This example assumes that a HTTP Form POST has been sent to this example with
        the required fields. The example then creates a payment request that is sent to
        the Payment Server via a cardholder's browser redirect.
        </para>

        <para>
        Before it redirects, the payment request creates an MD5 security signature to 
        ensure that if any data changes occur during transmission, they are picked up by 
        the Payment Server. If the data is changed, the MD5 signature will compute 
        incorrectly at the Payment Server and the transaction will not complete.
        </para>

        <para>
        To instantiate the MD5 signature check, the MD5 seed must be first saved in the 
        SECURE_SECRET value which is first parameter in the PageLoad() class. The 
        SECURE_SECRET value can be found in Merchant Administration/Setup page on the 
        Payment Server. Without this seed the MD5 signature will not be created.
        </para>

        <para>
        TRANSACTION DATA SHOULD NOT BE PASSED THROUGH THE ORDER PAGE AS HIDDEN FIELDS. 
        It is very easy for the cardholder to use the browser 'View/Source' function 
        to see, and change the data while in transit. SIMILARLY CLIENT SIDE SESSION 
        COOKIES ALSO SHOULD NOT BE USED TO STORE TRANSACTION DATA.
        </para>

        <para>
        To avoid this issue you can add sensitive data direct from a database query or a 
        session variable stored server side on this page. In fact no transaction data 
        should be passed in from the order page at all.
        </para>

        <para>
        You can simply retreive the transaction data from the server and add the data 
        directly to the SortedList transactionData as key value pairs as per the 
        AgainLink and Title vales shown below. 
        </para>

        <para>
        If used in this way there is no need to retrieve any form data or loop 
        through the <c>Request.Form</c> collection as done in this example.
        </para>

        </remarks>
        */

        // _____________________________________________________________________________

        // Declare the global variables
        private string debugData = "";

        //______________________________________________________________________________

        private class VPCStringComparer : IComparer
        {
            /*
             <summary>Customised Compare Class</summary>
             <remarks>
             <para>
             The Virtual Payment Client need to use an Ordinal comparison to Sort on 
             the field names to create the MD5 Signature for validation of the message. 
             This class provides a Compare method that is used to allow the sorted list 
             to be ordered using an Ordinal comparison.
             </para>
             </remarks>
             */

            public int Compare(Object a, Object b)
            {
                /*
                 <summary>Compare method using Ordinal comparison</summary>
                 <param name="a">The first string in the comparison.</param>
                 <param name="b">The second string in the comparison.</param>
                 <returns>An int containing the result of the comparison.</returns>
                 */

                // Return if we are comparing the same object or one of the 
                // objects is null, since we don't need to go any further.
                if (a == b) return 0;
                if (a == null) return -1;
                if (b == null) return 1;

                // Ensure we have string to compare
                string sa = a as string;
                string sb = b as string;

                // Get the CompareInfo object to use for comparing
                System.Globalization.CompareInfo myComparer = System.Globalization.CompareInfo.GetCompareInfo("en-US");
                if (sa != null && sb != null)
                {
                    // Compare using an Ordinal Comparison.
                    return myComparer.Compare(sa, sb, System.Globalization.CompareOptions.Ordinal);
                }
                throw new ArgumentException("a and b should be strings.");
            }
        }

        //______________________________________________________________________________

        private string CreateMD5Signature(string RawData)
        {
            /*
             <summary>Creates a MD5 Signature</summary>
             <param name="RawData">The string used to create the MD5 signautre.</param>
             <returns>A string containing the MD5 signature.</returns>
             */

            System.Security.Cryptography.MD5 hasher = System.Security.Cryptography.MD5CryptoServiceProvider.Create();
            byte[] HashValue = hasher.ComputeHash(Encoding.ASCII.GetBytes(RawData));

            string strHex = "";
            foreach (byte b in HashValue)
            {
                strHex += b.ToString("x2");
            }
            return strHex.ToUpper();
        }


        private string getANZResponseDescription(string vResponseCode)
        {
            /* 
             <summary>Maps the vpc_TxnResponseCode to a relevant description</summary>
             <param name="vResponseCode">The vpc_TxnResponseCode returned by the transaction.</param>
             <returns>The corresponding description for the vpc_TxnResponseCode.</returns>
             */
            string result = "Unknown";

            if (vResponseCode.Length > 0)
            {
                switch (vResponseCode)
                {
                    case "0": result = "Transaction Successful"; break;
                    case "1": result = "Transaction Declined"; break;
                    case "2": result = "Bank Declined Transaction"; break;
                    case "3": result = "No Reply from Bank"; break;
                    case "4": result = "Expired Card"; break;
                    case "5": result = "Insufficient Funds"; break;
                    case "6": result = "Error Communicating with Bank"; break;
                    case "7": result = "Payment Server detected an error"; break;
                    case "8": result = "Transaction Type Not Supported"; break;
                    case "9": result = "Bank declined transaction (Do not contact Bank)"; break;
                    case "A": result = "Transaction Aborted"; break;
                    case "B": result = "Transaction Declined - Contact the Bank"; break;
                    case "C": result = "Transaction Cancelled"; break;
                    case "D": result = "Deferred transaction has been received and is awaiting processing"; break;
                    case "F": result = "3-D Secure Authentication failed"; break;
                    case "I": result = "Card Security Code verification failed"; break;
                    case "L": result = "Shopping Transaction Locked (Please try the transaction again later)"; break;
                    case "N": result = "Cardholder is not enrolled in Authentication scheme"; break;
                    case "P": result = "Transaction has been received by the Payment Adaptor and is being processed"; break;
                    case "R": result = "Transaction was not processed - Reached limit of retry attempts allowed"; break;
                    case "S": result = "Duplicate SessionID"; break;
                    case "T": result = "Address Verification Failed"; break;
                    case "U": result = "Card Security Code Failed"; break;
                    case "V": result = "Address Verification and Card Security Code Failed"; break;
                    default: result = "Unable to be determined"; break;
                }
            }
            return result;
        }
    }
}