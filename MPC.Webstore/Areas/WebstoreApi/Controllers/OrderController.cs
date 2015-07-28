using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Runtime.Caching;
using System.Web.Http;

namespace MPC.Webstore.Areas.WebstoreApi.Controllers
{

    public class OrderController : ApiController
    {
          #region Private

       
        private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;
        private readonly IItemService _ItemService;
        private readonly ICompanyService _companyService;
        private readonly ICampaignService _campaignService;
        private readonly IUserManagerService _usermanagerService;
        private readonly ICompanyContactRepository _companyContact;
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        private readonly IOrderService _orderService;
        public OrderController(IItemService ItemService, IOrderService _orderService, ICompanyService companyService, IWebstoreClaimsHelperService _webstoreAuthorizationChecker, ICampaignService _campaignService, IUserManagerService _usermanagerService, ICompanyContactRepository _companyContact)
        {
            
            this._ItemService = ItemService;
            this._orderService = _orderService;
            this._companyService = companyService;
            this._webstoreAuthorizationChecker = _webstoreAuthorizationChecker;
            this._campaignService = _campaignService;
            this._usermanagerService = _usermanagerService;
            this._companyContact = _companyContact;
        }

        #endregion
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetData(long orderID)
        {
            double GrandTotal = 0;
            double Subtotal = 0;
            double vat = 0;
            Order order = _orderService.GetOrderAndDetails(orderID);
            Address BillingAddress = _orderService.GetBillingAddress(order.BillingAddressID);
            Address ShippingAddress = _orderService.GetdeliveryAddress(order.DeliveryAddressID);
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;
            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
            CalculateProductDescription(order, out GrandTotal, out Subtotal, out vat);
            JasonResponseObject obj = new JasonResponseObject();
            obj.order = order;
            obj.SubTotal = Utils.FormatDecimalValueToTwoDecimal(Subtotal.ToString(), StoreBaseResopnse.Currency);
            obj.GrossTotal = Utils.FormatDecimalValueToTwoDecimal(GrandTotal.ToString(), StoreBaseResopnse.Currency);
            obj.VAT = Utils.FormatDecimalValueToTwoDecimal(vat.ToString(), StoreBaseResopnse.Currency);
            obj.DeliveryCostCharges = Utils.FormatDecimalValueToTwoDecimal(order.DeliveryCost.ToString(), StoreBaseResopnse.Currency);
            obj.billingAddress = BillingAddress;
            obj.shippingAddress = ShippingAddress;
            if (BillingAddress.CountryId != null && BillingAddress.CountryId > 0)
            {
                obj.BillingCountry = _companyService.GetCountryNameById(BillingAddress.CountryId ?? 0);
            }
            else
            {
                obj.BillingCountry = string.Empty;
            }
            if (BillingAddress.StateId != null && BillingAddress.StateId > 0)
            {
                obj.BillingState = _companyService.GetStateNameById(BillingAddress.StateId ?? 0);
            }
            else
            {
                obj.BillingState = string.Empty;
            }

            if (ShippingAddress.CountryId != null && ShippingAddress.CountryId > 0)
            {
                obj.ShippingCountry = _companyService.GetCountryNameById(ShippingAddress.CountryId ?? 0);
            }
            else
            {
                obj.ShippingCountry = string.Empty;
            }

            if (ShippingAddress.StateId != null && ShippingAddress.StateId > 0)
            {
                obj.ShippingState = _companyService.GetStateNameById(ShippingAddress.StateId ?? 0);
            }
            else
            {
                obj.ShippingState = string.Empty;
            }
            obj.CurrencySymbol = StoreBaseResopnse.Currency;
            obj.OrderDateValue = Utils.FormatDateValue(order.OrderDate);
            obj.DeliveryDateValue = Utils.FormatDateValue(order.DeliveryDate);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, obj, formatter);
        }



        private void CalculateProductDescription(Order order, out double GrandTotal, out double Subtotal, out double vat)
        {

            double Delevery = 0;
            double DeliveryTaxValue = 0;
            double TotalVat = 0;
            double calculate = 0;
            Subtotal = 0;
            vat = 0;
            GrandTotal = 0;

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

        }
     
  
        public class JasonResponseObject
        {
            public Order order;
            public Address billingAddress;
            public Address shippingAddress;
            public string GrossTotal;
            public string SubTotal;
            public string VAT;
            public string DeliveryCostCharges;
            public string CurrencySymbol;
            public string OrderDateValue;
            public string DeliveryDateValue;
            public string BillingCountry;
            public string BillingState;
            public string ShippingCountry;
            public string ShippingState;
        }

    }
}
