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
        #endregion
        #region Constructor

        public DiscountVoucherController(ICompanyService companyService, IItemService ItemService
            , IOrderService orderService)
        {
            this._companyService = companyService;
           
           
            this._ItemService = ItemService;
            this._orderService = orderService;
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

                string voucherErrorMesg = "";

                if (storeDiscountVoucher != null)
                {
                    if (storeDiscountVoucher.CouponUseType == (int)CouponUseType.OneTimeUseCoupon)
                    {
                        if (storeDiscountVoucher.IsSingleUseRedeemed == true)
                        {
                            messages.Add("Error");
                            messages.Add("This Voucher is Expired.");
                        }
                        else
                        {
                            voucherErrorMesg = _ItemService.ValidateDiscountVoucher(storeDiscountVoucher);
                            if (voucherErrorMesg == "Success")
                            {
                                if (_ItemService.ApplyDiscountOnCartProducts(storeDiscountVoucher, OrderId, Convert.ToDouble(StoreBaseResopnse.Company.TaxRate)))
                                {
                                    storeDiscountVoucher.IsSingleUseRedeemed = true;

                                    _ItemService.SaveOrUpdateDiscountVoucher(storeDiscountVoucher);

                                    messages.Add("Success");
                                   
                                }
                                else
                                {
                                    messages.Add("Error");
                                    messages.Add("This Voucher is not applicable on the product(s) in cart. Please try another one.");
                                }
                            }
                            else
                            {
                                messages.Add("Error");
                                messages.Add(voucherErrorMesg);
                            }
                        }
                    }

                    if (messages[0] == "Success") 
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
    }
}
