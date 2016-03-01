using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
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
            if(order != null)
            {
                if (order.OrderDetails.CartItemsList != null && order.OrderDetails.CartItemsList.Count() > 0) 
                {
                    order.OrderDetails.CartItemsList = order.OrderDetails.CartItemsList.Where(i => i.Status != (int)OrderStatus.ShoppingCart).ToList();
                }
            }
           
            Address BillingAddress = _orderService.GetBillingAddress(order.BillingAddressID);
            Address ShippingAddress = _orderService.GetdeliveryAddress(order.DeliveryAddressID);
            
            MyCompanyDomainBaseReponse StoreBaseResopnse = _companyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            CalculateProductDescription(order, out GrandTotal, out Subtotal, out vat);
            JasonResponseObject obj = new JasonResponseObject();
            obj.CurrencySymbol = "";
            if (StoreBaseResopnse != null) 
            {
                if (StoreBaseResopnse.Organisation != null) 
                {
                    obj.CurrencySymbol = _companyService.GetCurrencySymbolById(Convert.ToInt64(StoreBaseResopnse.Organisation.CurrencyId));
                }
            }
            obj.CartItemsList = order.OrderDetails.CartItemsList;
            obj.ItemsSelectedAddonsList = order.OrderDetails.ItemsSelectedAddonsList;
            obj.OrderCode = order.OrderCode;
            obj.PlacedBy=order.PlacedBy;
            obj.StatusName = order.StatusName;
            obj.CompanyName = order.CompanyName;
            obj.SubTotal = Utils.FormatDecimalValueToTwoDecimal(Subtotal.ToString(), obj.CurrencySymbol);
            obj.GrossTotal = Utils.FormatDecimalValueToTwoDecimal(GrandTotal.ToString(), obj.CurrencySymbol);
            obj.VAT = Utils.FormatDecimalValueToTwoDecimal(vat.ToString(), obj.CurrencySymbol);
            obj.DeliveryCostCharges = Utils.FormatDecimalValueToTwoDecimal(order.DeliveryCost.ToString(), obj.CurrencySymbol);
            AddressModel BillingAddess = new AddressModel();
            BillingAddess.AddressName = BillingAddress.AddressName;
            BillingAddess.Address1 = BillingAddress.Address1;
            BillingAddess.Address2 = BillingAddress.Address2;
            BillingAddess.Address3 = BillingAddress.Address3;
            BillingAddess.City = BillingAddress.City;
            BillingAddess.Tel1 = BillingAddress.Tel1;
            BillingAddess.PostCode = BillingAddress.PostCode;
            obj.billingAddress = BillingAddess;

            AddressModel ShippingAddresss = new AddressModel();
            ShippingAddresss.AddressName = ShippingAddress.AddressName;
            ShippingAddresss.Address1 = ShippingAddress.Address1;
            ShippingAddresss.Address2 = ShippingAddress.Address2;
            ShippingAddresss.Address3 = ShippingAddress.Address3;
            ShippingAddresss.City = ShippingAddress.City;
            ShippingAddresss.Tel1 = ShippingAddress.Tel1;
            ShippingAddresss.PostCode = ShippingAddress.PostCode;
            obj.shippingAddress = ShippingAddresss;

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

            double DeliveryItemTotal = 0;
            double DeliveryTaxVal = 0;
            double ItemsTotalVat = 0;
            
            Subtotal = 0;
            vat = 0;
            GrandTotal = 0;

                foreach (var item in order.OrderDetails.CartItemsList)
                {

                    if (item.ItemType != (int)ItemTypes.Delivery)
                    {
                        Subtotal = Subtotal + item.Qty1BaseCharge1 ?? 0;
                        ItemsTotalVat = ItemsTotalVat + item.Qty1Tax1Value ?? 0;
                    }
                }
                DeliveryItemTotal = order.OrderDetails.DeliveryCost;
                DeliveryTaxVal = order.OrderDetails.DeliveryTaxValue;
                GrandTotal = Subtotal + ItemsTotalVat + DeliveryTaxVal + DeliveryItemTotal;
                vat = ItemsTotalVat + DeliveryTaxVal;

        }
     
  
        public class JasonResponseObject
        {
            //public Order order;
            public AddressModel billingAddress { get; set;}
            public AddressModel shippingAddress { get; set;}
            public string OrderCode;
            public string OrderDate;
            public string CompanyName;
            public string PlacedBy;
            public string StatusName;
            public string DeliveryDate;
            public List<ProductItem> CartItemsList;

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
            public List<AddOnCostsCenter> ItemsSelectedAddonsList { get; set; }
        }
       

        public class AddressModel
        {
            public string AddressName { get; set;}
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Address3 { get; set; }
            public string City { get; set; }
            public string Tel1 { get; set; }
            public string PostCode { get; set; }
        }

    }
}
