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
using System.Runtime.Caching;
using System.Web.Http;
using MPC.Models.ResponseModels;
namespace MPC.Webstore.Areas.WebstoreApi.Controllers
{
    public class ApplyDeliveryMethodController : ApiController
    {
        
         #region Private
        private readonly ICostCentreService _CostCentreService;
        private readonly IOrderService _orderService;
        private readonly IItemService _ItemService;
        private readonly ICompanyService _myCompanyService;
        #endregion
        #region Constructor
        public ApplyDeliveryMethodController(ICostCentreService CostCentreService, 
            IItemService ItemService, IOrderService orderService
            , ICompanyService myCompanyService)
        {
            this._CostCentreService = CostCentreService;
            this._ItemService = ItemService;
            this._orderService = orderService;
            this._myCompanyService = myCompanyService;
        }

        #endregion

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage AddDelivery(long DeliveryMethodId, long FreeShippingVoucherId)
        {
            List<string> messages = new List<string>();

            //string CacheKeyName = "CompanyBaseResponse";
            //ObjectCache cache = MemoryCache.Default;

            //MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            double Baseamount = 0;
            double SurchargeAmount = 0;
            double Taxamount = 0;
            double CostOfDelivery = 0;
            bool serviceResult = true;



            //string ShipPostCode = model.ShippingAddress.PostCode;
            CostCentre SelecteddeliveryCostCenter = null;

            if (DeliveryMethodId > 0)
            {
                SelecteddeliveryCostCenter = _CostCentreService.GetCostCentreByID(DeliveryMethodId);

                if (SelecteddeliveryCostCenter.CostCentreId > 0)
                {
                    if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                    {
                       
                            //if (!string.IsNullOrEmpty(ShipPostCode) && Convert.ToInt32(SelecteddeliveryCostCenter.DeliveryType) == Convert.ToInt32(DeliveryCarriers.Fedex))
                            //{

                            //   // serviceResult = GetFedexResponse(out Baseamount, out SurchargeAmount, out Taxamount, out CostOfDelivery, BaseResponseOrganisation, baseResponseCompany, model);

                            //}
                       
                    }
                    else
                    {
                        //if (!string.IsNullOrEmpty(ShipPostCode) && Convert.ToInt32(SelecteddeliveryCostCenter.DeliveryType) == Convert.ToInt32(DeliveryCarriers.Fedex))
                        //{


                        //    if (model.SelectedDeliveryCountry == 0 || model.SelectedDeliveryState == 0)
                        //    {
                        //        model.LtrMessageToDisplay = true;
                        //        model.LtrMessage = "Please select country or state to countinue.";


                        //        serviceResult = false;
                        //    }
                        //    else
                        //    {
                        //        if (!string.IsNullOrEmpty(ShipPostCode) && Convert.ToInt32(SelecteddeliveryCostCenter.DeliveryType) == Convert.ToInt32(DeliveryCarriers.Fedex))
                        //        {
                        //            serviceResult = GetFedexResponse(out Baseamount, out SurchargeAmount, out Taxamount, out CostOfDelivery, BaseResponseOrganisation, baseResponseCompany, model);
                        //        }
                        //    }

                        //}
                    }

                    if (serviceResult)
                    {
                        if (CostOfDelivery == 0)
                        {
                            CostOfDelivery = Convert.ToDouble(SelecteddeliveryCostCenter.DeliveryCharges);
                        }

                        List<Item> DeliveryItemList = _ItemService.GetListOfDeliveryItemByOrderID(UserCookieManager.WEBOrderId);


                        if (DeliveryItemList.Count > 1)
                        {
                            if (_ItemService.RemoveListOfDeliveryItemCostCenter(Convert.ToInt32(UserCookieManager.WEBOrderId)))
                            {
                                AddNewDeliveryCostCentreToItem(SelecteddeliveryCostCenter, CostOfDelivery, StoreBaseResopnse.Company, FreeShippingVoucherId, StoreBaseResopnse.Organisation);
                            }
                        }
                        else
                        {
                            AddNewDeliveryCostCentreToItem(SelecteddeliveryCostCenter, CostOfDelivery, StoreBaseResopnse.Company, FreeShippingVoucherId, StoreBaseResopnse.Organisation);
                        }
                    }

                }
            }

            messages.Add("");
            JsonSerializerSettings jSettings = new Newtonsoft.Json.JsonSerializerSettings();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = jSettings;
            return Request.CreateResponse(HttpStatusCode.OK, messages);
        }

        private void AddNewDeliveryCostCentreToItem(CostCentre SelecteddeliveryCostCenter, double costOfDelivery, Company company, long FreeShippingVoucherId, Organisation organisation)
        {


            double GetServiceTAX = 0;//Convert.ToDouble(Session["ServiceTaxRate"]);
            if (SelecteddeliveryCostCenter != null)
            {
                if (SelecteddeliveryCostCenter.CostCentreId > 0)
                {
                    if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                    {
                        _ItemService.AddUpdateItemFordeliveryCostCenter(UserCookieManager.WEBOrderId, SelecteddeliveryCostCenter.CostCentreId, costOfDelivery, company.CompanyId, SelecteddeliveryCostCenter.Name, StoreMode.Corp, company.IsDeliveryTaxAble ?? false, company.isCalculateTaxByService ?? false, GetServiceTAX, company.TaxRate ?? 0, FreeShippingVoucherId, organisation);


                    }
                    else
                    {
                        _ItemService.AddUpdateItemFordeliveryCostCenter(UserCookieManager.WEBOrderId, SelecteddeliveryCostCenter.CostCentreId, costOfDelivery, company.CompanyId, SelecteddeliveryCostCenter.Name, StoreMode.Corp, company.IsDeliveryTaxAble ?? false, company.isCalculateTaxByService ?? false, GetServiceTAX, company.TaxRate ?? 0, FreeShippingVoucherId, organisation);
                    }

                }
                SelecteddeliveryCostCenter.SetupCost = costOfDelivery;
                bool resultOfDilveryCostCenter = _orderService.SaveDilveryCostCenter(UserCookieManager.WEBOrderId, SelecteddeliveryCostCenter);
            }
        }

    }
}
