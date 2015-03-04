using MPC.Interfaces.Common;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Runtime.Caching;
using System.Web;
using System.Web.Http;

namespace MPC.Webstore.Areas.WebstoreApi.Controllers
{
    public class CostCenterController : ApiController
    {
        #region Private

        private readonly ICostCentreService _CostCentreService;

        private readonly IItemService _ItemService;
        private readonly ICompanyService _companyService;
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        private readonly IOrderService _orderService;
        public CostCenterController(ICostCentreService CostCentreService, IItemService ItemService, IOrderService _orderService, ICompanyService companyService)
        {
            this._CostCentreService = CostCentreService;
            this._ItemService = ItemService;
            this._orderService = _orderService;
            this._companyService = companyService;
        }

        #endregion
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetDateTimeString(string parameter1, string parameter2, string parameter3, string parameter4, List<QuestionQueueItem> parameter5)
        {
            if ((parameter4 == "Update" && parameter5 != null) || parameter4 != "Update")
            {
                AppDomain _AppDomain = null;

                try
                {

                    string OrganizationName = "Test";
                    AppDomainSetup _AppDomainSetup = new AppDomainSetup();


                    object _oLocalObject;
                    ICostCentreLoader _oRemoteObject;

                    object[] _CostCentreParamsArray = new object[12];

                    _AppDomainSetup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
                    _AppDomainSetup.PrivateBinPath = Path.GetDirectoryName((new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath);


                    _AppDomain = AppDomain.CreateDomain("CostCentresDomain", null, _AppDomainSetup);
                    //Me._AppDomain.InitializeLifetimeService()

                    List<CostCentreQueueItem> CostCentreQueue = new List<CostCentreQueueItem>();


                    //Me._CostCentreLaoderFactory = CType(Me._AppDomain.CreateInstance(Common.g_GlobalData.AppSettings.ApplicationStartupPath + "\Infinity.Model.dll", "Infinity.Model.CostCentres.CostCentreLoaderFactory").Unwrap(), Model.CostCentres.CostCentreLoaderFactory)
                    CostCentreLoaderFactory _CostCentreLaoderFactory = (CostCentreLoaderFactory)_AppDomain.CreateInstance("MPC.Interfaces", "MPC.Interfaces.WebStoreServices.CostCentreLoaderFactory").Unwrap();
                    _CostCentreLaoderFactory.InitializeLifetimeService();

                    if (parameter4 == "New")
                    {
                        if (parameter5 != null)
                        {
                            _CostCentreParamsArray[1] = CostCentreExecutionMode.ExecuteMode;
                            _CostCentreParamsArray[2] = parameter5;
                        }
                        else
                        {
                            _CostCentreParamsArray[1] = CostCentreExecutionMode.PromptMode;
                            _CostCentreParamsArray[2] = new List<QuestionQueueItem>();
                        }
                    }

                    if (parameter4 == "addAnother")
                    {
                       
                            _CostCentreParamsArray[1] = CostCentreExecutionMode.PromptMode;
                            _CostCentreParamsArray[2] = parameter5;
                       
                    }

                    if (parameter4 == "Update")
                    {
                        if (parameter5 != null)
                        {
                            _CostCentreParamsArray[1] = CostCentreExecutionMode.ExecuteMode;
                            _CostCentreParamsArray[2] = parameter5;
                        }
                    }

                    //_CostCentreParamsArray(0) = Common.g_GlobalData;
                    //GlobalData

                    //this mode will load the questionqueue

                    //QuestionQueue / Execution Queue
                    _CostCentreParamsArray[3] = CostCentreQueue;
                    // check if cc has wk ins


                    //CostCentreQueue
                    _CostCentreParamsArray[4] = 1;

                    _CostCentreParamsArray[5] = 1;
                    //MultipleQuantities

                    //CurrentQuantity
                    _CostCentreParamsArray[6] = new List<StockQueueItem>();
                    //StockQueue
                    _CostCentreParamsArray[7] = new List<InputQueueItem>();
                    //InputQueue

                    if (parameter3 == "null" || parameter3 == null)
                    {
                        // get first item section
                        _CostCentreParamsArray[8] = _ItemService.GetItemFirstSectionByItemId(Convert.ToInt64(parameter2));
                    }
                    else
                    {
                        // update quantity in item section and return
                        _CostCentreParamsArray[8] = _ItemService.UpdateItemFirstSectionByItemId(Convert.ToInt64(parameter2), Convert.ToInt32(parameter3));
                        //first update item section quatity
                        //persist queue
                        // run multiple cost centre
                        // after calculating cost centre 

                    }


                    _CostCentreParamsArray[9] = 1;


                    CostCentre oCostCentre = _CostCentreService.GetCostCentreByID(Convert.ToInt64(parameter1));

                    

                    CostCentreQueue.Add(new CostCentreQueueItem(oCostCentre.CostCentreId, oCostCentre.Name, 1, oCostCentre.CodeFileName, null, oCostCentre.SetupSpoilage, oCostCentre.RunningSpoilage));



                    _oLocalObject = _CostCentreLaoderFactory.Create(HttpContext.Current.Server.MapPath("/") + "\\ccAssembly\\" + OrganizationName + "UserCostCentres.dll", "UserCostCentres." + oCostCentre.CodeFileName, null);
                    _oRemoteObject = (ICostCentreLoader)_oLocalObject;

                    CostCentreCostResult oResult = null;

                    if (parameter4 == "Modify")
                    {
                        _CostCentreParamsArray[1] = CostCentreExecutionMode.PromptMode;
                        _CostCentreParamsArray[2] = parameter5.Where(c => c.CostCentreID == oCostCentre.CostCentreId).ToList();
                    }
                    else
                    {
                        if (parameter4 == "Update") // dummy condition
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, 131);
                        }
                        oResult = _oRemoteObject.returnCost(ref _CostCentreParamsArray);

                    }

                    if ((parameter5 != null && parameter4 != "Modify" && parameter4 != "addAnother" && oResult != null) || (parameter5 != null && parameter4 == "Update"))
                    {


                        JsonSerializerSettings jSettings = new Newtonsoft.Json.JsonSerializerSettings();
                        GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = jSettings;

                        double actualPrice = oResult.TotalCost;

                        if (actualPrice < oCostCentre.MinimumCost && oCostCentre.MinimumCost != 0)
                        {
                            actualPrice = oCostCentre.MinimumCost ?? 0;
                        }

                        return Request.CreateResponse(HttpStatusCode.OK, actualPrice);
                    }
                    else
                    {
                        JsonSerializerSettings jSettings = new Newtonsoft.Json.JsonSerializerSettings();
                        GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = jSettings;

                        return Request.CreateResponse(HttpStatusCode.OK, _CostCentreParamsArray);
                    }



                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    AppDomain.Unload(_AppDomain);
                }

            }
            else 
            {
                return Request.CreateResponse(HttpStatusCode.OK, "");
            }

        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetData(long orderID)
        {
             double GrandTotal = 0;
             double Subtotal = 0;
             double vat = 0;
             Order order = _orderService.GetOrderAndDetails(orderID);

             string CacheKeyName = "CompanyBaseResponse";
             ObjectCache cache = MemoryCache.Default;
             MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.StoreId];
             CalculateProductDescription(order,out GrandTotal,out Subtotal,out vat);
             JasonResponseObject obj=new JasonResponseObject();
             obj.order=order;
             obj.SubTotal=Math.Round(Subtotal,2);
             obj.GrossTotal=Math.Round(GrandTotal,2);
             obj.VAT=Math.Round(vat,2);
             obj.DeliveryCostCharges=order.DeliveryCost;
             obj.billingAddress= _orderService.GetBillingAddress(order.BillingAddressID);
             obj.shippingAddress=_orderService.GetdeliveryAddress(order.DeliveryAddressID);
             obj.CurrencySymbol = StoreBaseResopnse.Currency;
             obj.OrderDateValue = FormatDateValue(order.OrderDate);
             obj.DeliveryDateValue = FormatDateValue(order.DeliveryDate);
             var formatter = new JsonMediaTypeFormatter();
             var json = formatter.SerializerSettings;
             json.Formatting = Newtonsoft.Json.Formatting.Indented;
             json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
             return Request.CreateResponse(HttpStatusCode.OK, obj, formatter);
        }
        
                  
        private void CalculateProductDescription(Order order,out double GrandTotal,out double Subtotal,out double vat)
        {

            double Delevery = 0;
            double DeliveryTaxValue = 0;
            double TotalVat = 0;
            double calculate = 0;
             Subtotal = 0;
             vat = 0;
             GrandTotal = 0;

            {
                //List<tbl_items> items = context.tbl_items.Where(i => i.EstimateID == OrderID).ToList();

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
               
               // ViewBag.GrandTotal = GrandTotal;
               // ViewBag.SubTotal = Subtotal;
               // ViewBag.Vat = calculate;
            }
           
        }
        public  string FormatDateValue(DateTime? dateTimeValue, string formatString = null)
        {
            const string defaultFormat = "MMMM d, yyyy";

            if (dateTimeValue.HasValue)
                return dateTimeValue.Value.ToString(string.IsNullOrWhiteSpace(formatString) ? defaultFormat : formatString);
            else
                return string.Empty;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage WidgetJson(string StoreId)
        {
            List<CmsSkinPageWidget> oStoreWidgets = _companyService.GetStoreWidgets(Convert.ToInt64(StoreId));
            var objSer = JsonConvert.SerializeObject(oStoreWidgets, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }

    }
      public class JasonResponseObject
          {
          public Order order;
          public Address billingAddress;
          public Address shippingAddress;
          public double GrossTotal;
          public double SubTotal;
          public double VAT;
          public double DeliveryCostCharges;
          public string CurrencySymbol;
          public string OrderDateValue;
          public string DeliveryDateValue;
         }
}
         