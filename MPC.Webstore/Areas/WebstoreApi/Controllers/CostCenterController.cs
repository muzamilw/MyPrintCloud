using MPC.Interfaces.Common;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace MPC.Webstore.Areas.WebstoreApi.Controllers
{
    public class CostCenterController : ApiController
    {
        #region Private

        private readonly ICostCentreService _CostCentreService;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public CostCenterController(ICostCentreService CostCentreService)
        {
            this._CostCentreService = CostCentreService;
        }

        #endregion
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetDateTimeString(string parameter1, List<QuestionQueueItem> parameter2)
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

                if (parameter2 != null)
                {
                    _CostCentreParamsArray[1] = CostCentreExecutionMode.ExecuteMode;
                    _CostCentreParamsArray[2] = parameter2;
                }
                else 
                {
                    _CostCentreParamsArray[1] = CostCentreExecutionMode.PromptMode;
                    _CostCentreParamsArray[2] = new List<QuestionQueueItem>();
                }
                //_CostCentreParamsArray(0) = Common.g_GlobalData;
                //GlobalData
               
                //this mode will load the questionqueue
               
                //QuestionQueue / Execution Queue
                _CostCentreParamsArray[3] = CostCentreQueue;
                //CostCentreQueue
                _CostCentreParamsArray[4] = 1;
                //MultipleQuantities
                _CostCentreParamsArray[5] = 1;
                //CurrentQuantity
                _CostCentreParamsArray[6] = new List<StockQueueItem>();
                //StockQueue
                _CostCentreParamsArray[7] = new List<InputQueueItem>();
                //InputQueue
                _CostCentreParamsArray[8] = new ItemSection(); //this._CurrentItemDTO.ItemSection(this._CurrentCostCentreIndex);
                _CostCentreParamsArray[9] = 1;


                CostCentre oCostCentre = _CostCentreService.GetCostCentreByID(Convert.ToInt64(parameter1));

                CostCentreQueue.Add(new CostCentreQueueItem(oCostCentre.CostCentreId, oCostCentre.Name, 1, oCostCentre.CodeFileName, null, oCostCentre.SetupSpoilage, oCostCentre.RunningSpoilage));



                _oLocalObject = _CostCentreLaoderFactory.Create(HttpContext.Current.Server.MapPath("/") + "\\ccAssembly\\" + OrganizationName + "UserCostCentres.dll", "UserCostCentres." + oCostCentre.CodeFileName, null);
                _oRemoteObject = (ICostCentreLoader)_oLocalObject;

                CostCentreCostResult oResult = _oRemoteObject.returnCost(ref _CostCentreParamsArray);
                if (parameter2 != null)
                {
                    
                    JsonSerializerSettings jSettings = new Newtonsoft.Json.JsonSerializerSettings();
                    GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = jSettings;

                    return Request.CreateResponse(HttpStatusCode.OK, oResult.SetupCost);
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
    }
}
