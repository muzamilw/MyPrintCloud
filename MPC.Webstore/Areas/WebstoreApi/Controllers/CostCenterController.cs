using MPC.Interfaces.Common;
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public CostCenterController(ICostCentreService CostCentreService, IItemService ItemService, IOrderService _orderService, ICompanyService companyService, IWebstoreClaimsHelperService _webstoreAuthorizationChecker, ICampaignService _campaignService, IUserManagerService _usermanagerService, ICompanyContactRepository _companyContact)
        {
            this._CostCentreService = CostCentreService;
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
        public HttpResponseMessage ExecuteCostCentre(string CostCentreId, string ClonedItemId, string OrderedQuantity, string CallMode, QuestionAndInputQueues Queues)
        {
            if ((CallMode == "UpdateAllCostCentreOnQuantityChange" && Queues != null) || CallMode != "UpdateAllCostCentreOnQuantityChange")
            {
                AppDomain _AppDomain = null;

                try
                {
                    string CacheKeyName = "CompanyBaseResponse";
                    ObjectCache cache = MemoryCache.Default;

// ReSharper disable SuggestUseVarKeywordEvident
                    Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse> companyBaseResponse =
// ReSharper restore SuggestUseVarKeywordEvident
                        (cache.Get(CacheKeyName) as
                            Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>);
                    string orgName = string.Empty;
                    if (companyBaseResponse != null && UserCookieManager.WBStoreId != 0)
                    {
                        MPC.Models.ResponseModels.MyCompanyDomainBaseReponse myCompanyBaseResponseFromCache = companyBaseResponse[UserCookieManager.WBStoreId];    
                        if (myCompanyBaseResponseFromCache != null && myCompanyBaseResponseFromCache.Organisation != null)
                        {
                            orgName = myCompanyBaseResponseFromCache.Organisation.OrganisationName;
                        }
                    }
                    else
                    {
                        Organisation organisation = _CostCentreService.GetOrganisation(Convert.ToInt64(CostCentreId));
                        if (organisation != null)
                        {
                            orgName = organisation.OrganisationName;
                        }
                    }
                    string OrganizationName = orgName;
                    OrganizationName = Utils.specialCharactersEncoderCostCentre(OrganizationName);

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
                  
                    CostCentre  oCostCentre = _CostCentreService.GetCostCentreByID(Convert.ToInt64(CostCentreId));

                    List<CostcentreInstruction> oInstList = new List<CostcentreInstruction>();
                    List<CostcentreWorkInstructionsChoice> oInsChoicesList = null;
                    foreach (CostcentreInstruction obj in oCostCentre.CostcentreInstructions)
                    {
                        CostcentreInstruction oObject = new CostcentreInstruction();
                        oObject.CostCentreId = obj.CostCentreId;
                        oObject.Instruction =obj.Instruction;
                        oObject.InstructionId = obj.InstructionId;
                        oInsChoicesList = new List<CostcentreWorkInstructionsChoice>();
                        foreach (CostcentreWorkInstructionsChoice wI in obj.CostcentreWorkInstructionsChoices)
                        {
                            CostcentreWorkInstructionsChoice oChoicObject = new CostcentreWorkInstructionsChoice();
                            oChoicObject.Choice = wI.Choice;
                            oChoicObject.Id = wI.Id;
                            oChoicObject.InstructionId = wI.InstructionId;
                            oInsChoicesList.Add(oChoicObject);
                        }
                        oObject.CostcentreWorkInstructionsChoices = oInsChoicesList;
                        oInstList.Add(oObject);// = oCostCentre.CostcentreInstructions.ToArray();
                    }
                    CostCentreQueue.Add(new CostCentreQueueItem(oCostCentre.CostCentreId, oCostCentre.Name, 1, oCostCentre.CodeFileName, oInstList.ToArray(), oCostCentre.SetupSpoilage, oCostCentre.RunningSpoilage));

                    

                    if (CallMode == "New")
                    {
                        if (Queues != null)
                        {
                            _CostCentreParamsArray[1] = CostCentreExecutionMode.ExecuteMode;
                            // if queue contains item of other cost centre then this condition will filter the items of current cost centre

                            if (Queues.QuestionQueues != null)
                            {
                                _CostCentreParamsArray[2] = Queues.QuestionQueues.Where(c => c.CostCentreID == oCostCentre.CostCentreId).ToList(); ;
                            }
                            else 
                            {
                                _CostCentreParamsArray[2] = Queues.QuestionQueues;
                            }
                            
                         
                            if (Queues.InputQueues != null) 
                            {
                                _CostCentreParamsArray[7] = Queues.InputQueues.Where(c => c.CostCentreID == oCostCentre.CostCentreId).ToList();
                            }
                            else // else assign null
                            {
                                _CostCentreParamsArray[7] = Queues.InputQueues;
                            }
                            
                        }
                        else
                        {
                            _CostCentreParamsArray[1] = CostCentreExecutionMode.PromptMode;
                            _CostCentreParamsArray[2] = new List<QuestionQueueItem>();
                            _CostCentreParamsArray[7] = new InputQueue();
                        }
                    }

                    if (CallMode == "addAnother")
                    {

                        _CostCentreParamsArray[1] = CostCentreExecutionMode.PromptMode;
                        _CostCentreParamsArray[2] = Queues.QuestionQueues;
                        if (Queues.InputQueues != null)
                        {
                            InputQueue inputQueueObj = new InputQueue();
                            List<InputQueueItem> Items = Queues.InputQueues.Where(c => c.CostCentreID == oCostCentre.CostCentreId).ToList();
                            foreach (InputQueueItem obj in Items)
                            {
                                inputQueueObj.addItem(obj.ID, obj.VisualQuestion, obj.CostCentreID, obj.ItemType, obj.ItemInputType, obj.VisualQuestion, obj.Value, obj.Qty1Answer);
                            }
                            _CostCentreParamsArray[7] = inputQueueObj;
                        }
                        else
                        {
                            _CostCentreParamsArray[7] = new InputQueue();
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
                 
                    //InputQueue

                    if (!string.IsNullOrEmpty(ClonedItemId))
                    {
                        if (OrderedQuantity == "null" || OrderedQuantity == null)
                        {
                            // get first item section
                            _CostCentreParamsArray[8] = _ItemService.GetItemFirstSectionByItemId(Convert.ToInt64(ClonedItemId));
                        }
                        else
                        {
                            // update quantity in item section and return
                            _CostCentreParamsArray[8] = _ItemService.UpdateItemFirstSectionByItemId(Convert.ToInt64(ClonedItemId), Convert.ToInt32(OrderedQuantity));

                        }    
                    }

                    _CostCentreParamsArray[9] = 1;

                    // connection string
                    _CostCentreParamsArray[10] =  "Persist Security Info=False;Integrated Security=false;Initial Catalog=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringDBName"] + ";server=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringServerName"] + "; user id=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringUserName"] + "; password=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringPasswordName"] + ";";
                  


                   


                    _oLocalObject = _CostCentreLaoderFactory.Create(HttpContext.Current.Server.MapPath("/") + "\\ccAssembly\\" + OrganizationName + "UserCostCentres.dll", "UserCostCentres." + oCostCentre.CodeFileName, null);
                    _oRemoteObject = (ICostCentreLoader)_oLocalObject;

                    CostCentrePriceResult oResult = null;

                    if (CallMode == "Modify")
                    {
                        _CostCentreParamsArray[1] = CostCentreExecutionMode.PromptMode;
                        if (Queues.QuestionQueues != null)
                        {
                            _CostCentreParamsArray[2] = Queues.QuestionQueues.Where(c => c.CostCentreID == oCostCentre.CostCentreId).ToList();
                        }
                        else 
                        {
                            _CostCentreParamsArray[2] = new List<QuestionQueueItem>();
                        }

                        if (Queues.InputQueues != null)
                        {
                            InputQueue inputQueueObj = new InputQueue();
                            List<InputQueueItem> Items = Queues.InputQueues.Where(c => c.CostCentreID == oCostCentre.CostCentreId).ToList();
                            foreach (InputQueueItem obj in Items)
                            {
                                inputQueueObj.addItem(obj.ID, obj.VisualQuestion, obj.CostCentreID, obj.ItemType, obj.ItemInputType, obj.VisualQuestion, obj.Value, obj.Qty1Answer);
                            }
                            _CostCentreParamsArray[7] = inputQueueObj;
                        }
                        else
                        {
                            _CostCentreParamsArray[7] = new InputQueue();
                        }
                        
                        
                    }
                    else
                    {
                        if (CallMode == "UpdateAllCostCentreOnQuantityChange") 
                        { 
                            _CostCentreParamsArray[1] = CostCentreExecutionMode.ExecuteMode;
                            _CostCentreParamsArray[2] = Queues.QuestionQueues.ToList();
                            if (Queues.InputQueues != null)
                            {
                                InputQueue inputQueueObj = new InputQueue();
                                List<InputQueueItem> Items = Queues.InputQueues.ToList();
                                foreach (InputQueueItem obj in Items)
                                {
                                    inputQueueObj.addItem(obj.ID, obj.VisualQuestion, obj.CostCentreID, obj.ItemType, obj.ItemInputType, obj.VisualQuestion, obj.Value, obj.Qty1Answer);
                                }
                                _CostCentreParamsArray[7] = inputQueueObj.Items;
                            }
                            else
                            {
                                _CostCentreParamsArray[7] = new InputQueue();
                            }
                        }
                        oResult = _oRemoteObject.returnPrice(ref _CostCentreParamsArray);

                    }

                    if ((Queues != null && CallMode != "Modify" && CallMode != "addAnother" && oResult != null) || (Queues != null && CallMode == "Update"))
                    {


                        JsonSerializerSettings jSettings = new Newtonsoft.Json.JsonSerializerSettings();
                        GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = jSettings;

                        double actualPrice = oResult.TotalPrice;

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

     


     

        [HttpPost]
        public void DeleteArtworkAttachment(long AttachmentId)
        {
            _ItemService.DeleteItemAttachment(AttachmentId);
        }
       
     
       
    }
}
