﻿using System;
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
        public PaymentController(IItemService ItemService, IOrderService OrderService, ICampaignService campaignService, ICompanyService myCompanyService, IWebstoreClaimsHelperService myClaimHelper, IUserManagerService usermanagerService, IPrePaymentService IPrePaymentService, IPayPalResponseService _PayPalResponseService)
        {
            this._ItemService = ItemService;
            this._OrderService = OrderService;
            this._campaignService = campaignService;
            this._myCompanyService = myCompanyService;
            this._myClaimHelper = myClaimHelper;
            this._usermanagerService = usermanagerService;
            this._IPrePaymentService = IPrePaymentService;
            this._PayPalResponseService = _PayPalResponseService;
        }

        // GET: Payment
        public ActionResult PaypalSubmit(int OrderID)
        {
             OrderID = 16633;
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
                        if (oGateWay.UseSandbox.HasValue && oGateWay.UseSandbox.Value)
                            opaypal.URL = oGateWay.TestApiUrl;// "https://www.sandbox.paypal.com/cgi-bin/webscr";
                        else
                            opaypal.URL = oGateWay.LiveApiUrl;// "https://www.paypal.com/cgi-bin/webscr";

                        if (oGateWay.SendToReturnURL.HasValue && oGateWay.SendToReturnURL.Value)
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
        [HttpPost]
        public ActionResult PaypalIPN()
        {
            try
            {
                Estimate modelOrder = null;
                string strFormValues = Encoding.ASCII.GetString(Request.BinaryRead(Request.ContentLength));
                string strNewValue;
                PaymentGateway oGateWay = _ItemService.GetPaymentGatewayRecord(UserCookieManager.StoreId);
                // getting the URL to work with
                string URL;
                if ((bool)oGateWay.UseSandbox)
                    URL = oGateWay.TestApiUrl;
                else
                    URL = oGateWay.LiveApiUrl;

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
                int outCustomRequestID = 0;
              
                int.TryParse(Request["custom"], out  outCustomRequestID);
                double.TryParse(Request["mc_gross"], out outGrossTotal);

                int orderID = outCustomRequestID;// GetRequestOrderID(outCustomRequestID);

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
                            cep.CompanySiteID = 1;
                            string HTMLOfShopReceipt = null;
                            cep.ContactId = modelOrder.ContactId ?? 0;
                            cep.CompanyId = modelOrder.CompanyId;
                            cep.EstimateID = orderID; //PageParameters.OrderID;
                            Company CustomerCompany = _myCompanyService.GetCompanyByCompanyID(modelOrder.CompanyId);
                            CompanyContact CustomrContact = _myCompanyService.GetContactByID(cep.ContactId);
                            _OrderService.SetOrderCreationDateAndCode(orderID);
                            SystemUser EmailOFSM = _usermanagerService.GetSalesManagerDataByID(Convert.ToInt32(CustomerCompany.SalesAndOrderManagerId1));

                            if (CustomerCompany.IsCustomer == (int)CustomerTypes.Corporate)
                            {
                                HTMLOfShopReceipt = _campaignService.GetPinkCardsShopReceiptPage(orderID, CustomrContact.ContactId); // corp
                                ModeOfStore = StoreMode.Corp;
                            }
                            else
                            {
                                HTMLOfShopReceipt = _campaignService.GetPinkCardsShopReceiptPage(orderID, 0); // retail
                            }

                            Campaign OnlineOrderCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.OnlineOrder);
                            cep.SalesManagerContactID = Convert.ToInt32(modelOrder.ContactId);
                            cep.StoreID = Convert.ToInt32(modelOrder.CompanyId);
                            cep.AddressID = Convert.ToInt32(modelOrder.CompanyId);
                            long ManagerID = _myCompanyService.GetContactIdByRole(_myClaimHelper.loginContactCompanyID(), (int)Roles.Manager); //ContactManager.GetBrokerByRole(Convert.ToInt32(modelOrder.CompanyId), (int)Roles.Manager); 
                            cep.CorporateManagerID = ManagerID;
                            if (CustomerCompany.StoreId != null) ///Retail Mode
                            {
                                _campaignService.SendEmailToSalesManager((int)Events.NewQuoteToSalesManager, (int)modelOrder.ContactId, (int)modelOrder.CompanyId, 0, orderID, 0, 0, StoreMode.Retail, CustomerCompany, EmailOFSM);
                            }
                            else
                            {
                                _campaignService.SendEmailToSalesManager((int)Events.NewOrderToSalesManager, Convert.ToInt32(modelOrder.ContactId), Convert.ToInt32(modelOrder.CompanyId), 0, orderID, 0, Convert.ToInt32(ManagerID), StoreMode.Corp, CustomerCompany, EmailOFSM);
                           
                            }
                            
                            //in case of retail <<SalesManagerEmail>> variable should be resolved by organization's sales manager
                            // thats why after getting the sales manager records ew are sending his email as a parameter in email body genetor
                          

                            _campaignService.emailBodyGenerator(OnlineOrderCampaign, cep, CustomrContact, StoreMode.Retail, Convert.ToInt32(CustomerCompany.OrganisationId), "", HTMLOfShopReceipt, "", EmailOFSM.Email);

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
            catch (Exception ex)
            {
                //   LogError(ex);
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


    }
}