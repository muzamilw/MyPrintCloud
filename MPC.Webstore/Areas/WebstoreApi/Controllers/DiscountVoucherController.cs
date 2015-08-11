using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using System.Runtime.Caching;
using MPC.Models.Common;

namespace MPC.Webstore.Areas.WebstoreApi.Controllers
{
    public class DiscountVoucherController : ApiController
    {
        #region Private

        private readonly ICompanyService _companyService;
        private readonly IItemService _ItemService;
        private readonly IOrderService _orderService;
        private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;
        #endregion
        #region Constructor

        public DiscountVoucherController(ICompanyService companyService, IItemService ItemService
            , IOrderService orderService, IWebstoreClaimsHelperService webstoreAuthorizationChecker)
        {
            this._companyService = companyService;
            this._ItemService = ItemService;
            this._orderService = orderService;
            this._webstoreAuthorizationChecker = webstoreAuthorizationChecker;
        }
        #endregion
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage ValidateDiscountVouchers(string DiscountVoucher, long StoreId, long OrderId, long OrganisationId)
        {
            List<string> messages = new List<string>();
            
            if (!string.IsNullOrEmpty(DiscountVoucher))
            {
                DiscountVoucher storeDiscountVoucher = _ItemService.GetDiscountVoucherByCouponCode(DiscountVoucher, StoreId, OrganisationId);

                string CacheKeyName = "CompanyBaseResponse";
                ObjectCache cache = MemoryCache.Default;

                MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];

                if (storeDiscountVoucher != null)
                {
                    if (storeDiscountVoucher.CouponUseType == (int)CouponUseType.UnlimitedUse)
                    {
                        if (storeDiscountVoucher.DiscountType == (int)DiscountTypes.FreeShippingonEntireorder)
                        {
                            _ItemService.ApplyDiscountOnDeliveryItemAlreadyAddedToCart(storeDiscountVoucher, OrderId, Convert.ToDouble(StoreBaseResopnse.Company.TaxRate));
                        }
                        else 
                        {
                            ApplyVoucher(storeDiscountVoucher, OrderId, Convert.ToDouble(StoreBaseResopnse.Company.TaxRate), messages);
                        }
                        
                    }
                    else if (storeDiscountVoucher.CouponUseType == (int)CouponUseType.OneTimeUsePerCustomer)
                    {
                        if (_companyService.IsVoucherUsedByCustomer(_webstoreAuthorizationChecker.loginContactID(), _webstoreAuthorizationChecker.loginContactCompanyID(), storeDiscountVoucher.DiscountVoucherId))
                        {
                            if (storeDiscountVoucher.DiscountType == (int)DiscountTypes.FreeShippingonEntireorder)
                            {
                                _ItemService.ApplyDiscountOnDeliveryItemAlreadyAddedToCart(storeDiscountVoucher, OrderId, Convert.ToDouble(StoreBaseResopnse.Company.TaxRate));
                            }
                            else
                            {
                                ApplyVoucher(storeDiscountVoucher, OrderId, Convert.ToDouble(StoreBaseResopnse.Company.TaxRate), messages);
                            }
                            
                            _companyService.AddReedem(_webstoreAuthorizationChecker.loginContactID(), _webstoreAuthorizationChecker.loginContactCompanyID(), storeDiscountVoucher.DiscountVoucherId);
                        }
                        else 
                        {
                            messages.Add("Error");
                            messages.Add("The Coupon Code has already been redeemed.");
                        }
                    }
                    else if (storeDiscountVoucher.CouponUseType == (int)CouponUseType.OneTimeUseCoupon)
                    {
                        if (storeDiscountVoucher.IsSingleUseRedeemed == true)
                        {
                            messages.Add("Error");
                            messages.Add("This Voucher is Expired.");
                        }
                        else
                        {
                            if (storeDiscountVoucher.DiscountType == (int)DiscountTypes.FreeShippingonEntireorder)
                            {
                                _ItemService.ApplyDiscountOnDeliveryItemAlreadyAddedToCart(storeDiscountVoucher, OrderId, Convert.ToDouble(StoreBaseResopnse.Company.TaxRate));
                            }
                            else
                            {
                                ApplyVoucher(storeDiscountVoucher, OrderId, Convert.ToDouble(StoreBaseResopnse.Company.TaxRate), messages);
                            }
                            

                            if (messages[0] == "Success")
                            {
                                storeDiscountVoucher.IsSingleUseRedeemed = true;

                                _ItemService.SaveOrUpdateDiscountVoucher(storeDiscountVoucher);
                            }
                        }
                    }

                    if (messages[0] == "Success" || messages[0] == "Free") 
                    {
                        Estimate order = _orderService.GetOrderByID(OrderId);
                        order.DiscountVoucherID = storeDiscountVoucher.DiscountVoucherId;
                        order.VoucherDiscountRate = storeDiscountVoucher.DiscountRate;
                        _orderService.SaveOrUpdateOrder();
                    }
                }
                else
                {
                    messages.Add("Error");
                    messages.Add("Your Discount Voucher is invalid.");
                }
            }
            else
            {
                messages.Add("Error");
                messages.Add("Please enter Voucher Code to proceed.");
            }

           

            JsonSerializerSettings jSettings = new Newtonsoft.Json.JsonSerializerSettings();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = jSettings;

            return Request.CreateResponse(HttpStatusCode.OK, messages);
        }

        void ApplyVoucher(DiscountVoucher storeDiscountVoucher, long OrderId, double StoreTaxRate, List<string> messages) 
        {
            string voucherDisplayMesg = "";
            long FreeShippingVoucherId = 0;
            voucherDisplayMesg = _ItemService.ValidateDiscountVoucher(storeDiscountVoucher);
            if (voucherDisplayMesg == "Success")
            {
                if (_ItemService.ApplyDiscountOnCartProducts(storeDiscountVoucher, OrderId, StoreTaxRate, ref FreeShippingVoucherId, ref voucherDisplayMesg))
                {
                    messages.Add("Success");
                }
                else
                {
                    if (FreeShippingVoucherId > 0)
                    {
                        _ItemService.ApplyDiscountOnDeliveryItemAlreadyAddedToCart(storeDiscountVoucher, OrderId, StoreTaxRate);
                        messages.Add("Free");
                        messages.Add(FreeShippingVoucherId.ToString());
                        UserCookieManager.FreeShippingVoucherId = FreeShippingVoucherId;
                    }
                    else 
                    {
                        messages.Add("Error");
                        if (!string.IsNullOrEmpty(voucherDisplayMesg))
                        {
                            messages.Add(voucherDisplayMesg);
                        }
                        else 
                        {
                            messages.Add("This Voucher is not applicable on the product(s) in cart. Please try another one.");
                        }
                    }
                }
            }
            else
            {
                messages.Add("Error");
                messages.Add(voucherDisplayMesg);
            }
        }
    }
}
