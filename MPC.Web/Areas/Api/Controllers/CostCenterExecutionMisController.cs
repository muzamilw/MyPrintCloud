using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Common;
using MPC.Interfaces.Data;
using MPC.Interfaces.WebStoreServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.Common;
using MPC.WebBase.Mvc;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Interfaces.MISServices;
using Newtonsoft.Json;
using CostCentre = MPC.Models.DomainModels.CostCentre;
using CostcentreInstruction = MPC.Models.DomainModels.CostcentreInstruction;
using CostcentreWorkInstructionsChoice = MPC.Models.DomainModels.CostcentreWorkInstructionsChoice;
using ICompanyService = MPC.Interfaces.MISServices.ICompanyService;
using Organisation = MPC.Models.DomainModels.Organisation;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class CostCenterExecutionMisController : ApiController
    {
        #region Private

        private readonly ICostCentersService CostCenterService;
        private readonly MPC.Interfaces.MISServices.IItemService _ItemService;
        private readonly IMyOrganizationService _myOrganizationService;
        private readonly IItemSectionService _itemSectionService;
        

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CostCenterExecutionMisController(ICostCentersService costcenterService, MPC.Interfaces.MISServices.IItemService itemService, IMyOrganizationService myOrganizationService, IItemSectionService itemSectionService)
        {
            if (costcenterService == null)
            {
                throw new ArgumentNullException("costcenterService");
            }
            if (itemService == null)
            {
                throw new ArgumentNullException("itemService");
            }
            if (myOrganizationService == null)
            {
                throw new ArgumentNullException("myOrganizationService");
            }
            if (itemSectionService == null)
            {
                throw new ArgumentNullException("itemSectionService");
            }
            this.CostCenterService = costcenterService;
            this._ItemService = itemService;
            this._myOrganizationService = myOrganizationService;
            this._itemSectionService = itemSectionService;
        }

        #endregion

        #region Public
        public HttpResponseMessage Post(CostCenterExecutionParamRequest paramRequest)
        {
            if (paramRequest == null)
            {
                throw new ArgumentNullException("paramRequest");
            }
            double dblResult1 = 0;
            double dblResult2 = 0;
            double dblResult3 = 0;

            CostCentrePriceResult oResult = new CostCentrePriceResult();
            object[] _CostCentreParamsArray = new object[12];
            JsonSerializerSettings jSettings = new Newtonsoft.Json.JsonSerializerSettings();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = jSettings;
            //QuestionAndInputQueues Queues = new QuestionAndInputQueues();

            QuestionAndInputQueues Queues = paramRequest.Queues ?? null;
            ItemSection section = paramRequest.CurrentItemSection ?? null;
           
            _CostCentreParamsArray[5] = 1;
            _CostCentreParamsArray[4] = 1;
            dblResult1 = GetCostCenterPrice(paramRequest.CostCentreId, paramRequest.OrderedQuantity, paramRequest.CallMode, Queues, ref _CostCentreParamsArray, section);
            oResult.TotalPrice = dblResult1;

            if (Queues != null)
            {
                if (Convert.ToInt32(section.Qty2) > 0)
                {
                    _CostCentreParamsArray[5] = 2;
                    _CostCentreParamsArray[4] = 2;
                    dblResult2 = GetCostCenterPrice(paramRequest.CostCentreId, Convert.ToString(section.Qty2), "UpdateAllCostCentreOnQuantityChange", Queues, ref _CostCentreParamsArray, section);
                    oResult.TotalPriceQty2 = dblResult2;
                }
                if (Convert.ToInt32(section.Qty3) > 0)
                {
                    _CostCentreParamsArray[5] = 3;
                    _CostCentreParamsArray[4] = 3;
                    dblResult3 = GetCostCenterPrice(paramRequest.CostCentreId, Convert.ToString(section.Qty3), "UpdateAllCostCentreOnQuantityChange", Queues, ref _CostCentreParamsArray, section);
                    oResult.TotalPriceQty3 = dblResult3;
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, _CostCentreParamsArray);
            }

            return Request.CreateResponse(HttpStatusCode.OK, oResult);
        }
       
        #endregion

        //#region Cost Center Execution
        //[System.Web.Http.AcceptVerbs("GET", "POST")]
        //[System.Web.Http.HttpGet]
        //public HttpResponseMessage ExecuteCostCentre(string CostCentreId, string ClonedItemId, string OrderedQuantity, string CallMode, CostCenterExecutionParamRequest paramRequest, string qty2, string qty3, string itemSectionId)
        //{
        //    double dblResult1 = 0;
        //    double dblResult2 = 0;
        //    double dblResult3 = 0;
            
        //    CostCentrePriceResult oResult = new CostCentrePriceResult();
        //    object[] _CostCentreParamsArray = new object[12];
        //    JsonSerializerSettings jSettings = new Newtonsoft.Json.JsonSerializerSettings();
        //    GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = jSettings;
        //    //QuestionAndInputQueues Queues = new QuestionAndInputQueues();

        //    QuestionAndInputQueues Queues = paramRequest != null && paramRequest.Queues != null
        //        ? paramRequest.Queues
        //        : null;
        //    ItemSection section = paramRequest != null && paramRequest.CurrentItemSection != null
        //        ? paramRequest.CurrentItemSection
        //        : null;
        //    //ItemSection section = paramRequest != null 
        //    //    ? paramRequest.CurrentItemSection
        //    //    : null;
        //    _CostCentreParamsArray[5] = 1;
        //    dblResult1 = GetCostCenterPrice(CostCentreId, ClonedItemId, OrderedQuantity, CallMode, Queues, ref _CostCentreParamsArray, itemSectionId, section);
        //    oResult.TotalPrice = dblResult1;

        //    if (Queues != null)
        //    {
        //        if (Convert.ToInt32(qty2) > 0)
        //        {
        //            OrderedQuantity = qty2;
        //            dblResult2 = GetCostCenterPrice(CostCentreId, ClonedItemId, OrderedQuantity, "UpdateAllCostCentreOnQuantityChange", Queues, ref _CostCentreParamsArray, itemSectionId, section);
        //            oResult.TotalPriceQty2 = dblResult2;
        //        }
        //        if (Convert.ToInt32(qty3) > 0)
        //        {
        //            OrderedQuantity = qty3;
        //            dblResult3 = GetCostCenterPrice(CostCentreId, ClonedItemId, OrderedQuantity, "UpdateAllCostCentreOnQuantityChange", Queues, ref _CostCentreParamsArray, itemSectionId, section);
        //            oResult.TotalPriceQty3 = dblResult3;
        //        }
        //    }
        //    else
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK, _CostCentreParamsArray);
        //    }

        //    return Request.CreateResponse(HttpStatusCode.OK, oResult);
                        
        //    //if ((CallMode == "UpdateAllCostCentreOnQuantityChange" && Queues != null) || CallMode != "UpdateAllCostCentreOnQuantityChange")
        //    //{
        //    //    AppDomain _AppDomain = null;

        //    //    try
        //    //    {
        //    //        Organisation organisation = _myOrganizationService.GetOrganisation();
        //    //       // MyCompanyDomainBaseReponse companyBaseResponse = _companyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

        //    //        string orgName = string.Empty;
        //    //        if (organisation != null)
        //    //        {
        //    //            orgName = organisation.OrganisationName;
        //    //        }
                    
        //    //        string OrganizationName = orgName;
        //    //        OrganizationName = specialCharactersEncoderCostCentre(OrganizationName);

        //    //        AppDomainSetup _AppDomainSetup = new AppDomainSetup();


        //    //        object _oLocalObject;
        //    //        ICostCentreLoader _oRemoteObject;

        //    //        object[] _CostCentreParamsArray = new object[12];

        //    //        _AppDomainSetup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
        //    //        _AppDomainSetup.PrivateBinPath = Path.GetDirectoryName((new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath);


        //    //        _AppDomain = AppDomain.CreateDomain("CostCentresDomain", null, _AppDomainSetup);
        //    //        //Me._AppDomain.InitializeLifetimeService()

        //    //        List<CostCentreQueueItem> CostCentreQueue = new List<CostCentreQueueItem>();




        //    //        //Me._CostCentreLaoderFactory = CType(Me._AppDomain.CreateInstance(Common.g_GlobalData.AppSettings.ApplicationStartupPath + "\Infinity.Model.dll", "Infinity.Model.CostCentres.CostCentreLoaderFactory").Unwrap(), Model.CostCentres.CostCentreLoaderFactory)
        //    //        CostCentreLoaderFactory _CostCentreLaoderFactory = (CostCentreLoaderFactory)_AppDomain.CreateInstance("MPC.Interfaces", "MPC.Interfaces.WebStoreServices.CostCentreLoaderFactory").Unwrap();
        //    //        _CostCentreLaoderFactory.InitializeLifetimeService();

        //    //        CostCentre oCostCentre = CostCenterService.GetCostCentreById(Convert.ToInt64(CostCentreId));

        //    //        List<CostcentreInstruction> oInstList = new List<CostcentreInstruction>();
        //    //        List<CostcentreWorkInstructionsChoice> oInsChoicesList = null;
        //    //        foreach (CostcentreInstruction obj in oCostCentre.CostcentreInstructions)
        //    //        {
        //    //            CostcentreInstruction oObject = new CostcentreInstruction();
        //    //            oObject.CostCentreId = obj.CostCentreId;
        //    //            oObject.Instruction = obj.Instruction;
        //    //            oObject.InstructionId = obj.InstructionId;
        //    //            oInsChoicesList = new List<CostcentreWorkInstructionsChoice>();
        //    //            foreach (CostcentreWorkInstructionsChoice wI in obj.CostcentreWorkInstructionsChoices)
        //    //            {
        //    //                CostcentreWorkInstructionsChoice oChoicObject = new CostcentreWorkInstructionsChoice();
        //    //                oChoicObject.Choice = wI.Choice;
        //    //                oChoicObject.Id = wI.Id;
        //    //                oChoicObject.InstructionId = wI.InstructionId;
        //    //                oInsChoicesList.Add(oChoicObject);
        //    //            }
        //    //            oObject.CostcentreWorkInstructionsChoices = oInsChoicesList;
        //    //            oInstList.Add(oObject);// = oCostCentre.CostcentreInstructions.ToArray();
        //    //        }
        //    //        CostCentreQueue.Add(new CostCentreQueueItem(oCostCentre.CostCentreId, oCostCentre.Name, 1, oCostCentre.CodeFileName, oInstList.ToArray(), oCostCentre.SetupSpoilage, oCostCentre.RunningSpoilage));



        //    //        if (CallMode == "New")
        //    //        {
        //    //            if (Queues != null)
        //    //            {
        //    //                _CostCentreParamsArray[1] = CostCentreExecutionMode.ExecuteMode;
        //    //                // if queue contains item of other cost centre then this condition will filter the items of current cost centre

        //    //                if (Queues.QuestionQueues != null)
        //    //                {
        //    //                    _CostCentreParamsArray[2] = Queues.QuestionQueues.Where(c => c.CostCentreID == oCostCentre.CostCentreId).ToList(); ;
        //    //                }
        //    //                else
        //    //                {
        //    //                    _CostCentreParamsArray[2] = Queues.QuestionQueues;
        //    //                }


        //    //                if (Queues.InputQueues != null)
        //    //                {
        //    //                    _CostCentreParamsArray[7] = Queues.InputQueues.Where(c => c.CostCentreID == oCostCentre.CostCentreId).ToList();
        //    //                }
        //    //                else // else assign null
        //    //                {
        //    //                    _CostCentreParamsArray[7] = Queues.InputQueues;
        //    //                }

        //    //            }
        //    //            else
        //    //            {
        //    //                _CostCentreParamsArray[1] = CostCentreExecutionMode.PromptMode;
        //    //                _CostCentreParamsArray[2] = new List<QuestionQueueItem>();
        //    //                _CostCentreParamsArray[7] = new InputQueue();
        //    //            }
        //    //        }

        //    //        if (CallMode == "addAnother")
        //    //        {

        //    //            _CostCentreParamsArray[1] = CostCentreExecutionMode.PromptMode;
        //    //            _CostCentreParamsArray[2] = Queues.QuestionQueues;
        //    //            if (Queues.InputQueues != null)
        //    //            {
        //    //                InputQueue inputQueueObj = new InputQueue();
        //    //                List<InputQueueItem> Items = Queues.InputQueues.Where(c => c.CostCentreID == oCostCentre.CostCentreId).ToList();
        //    //                foreach (InputQueueItem obj in Items)
        //    //                {
        //    //                    inputQueueObj.addItem(obj.ID, obj.VisualQuestion, obj.CostCentreID, obj.ItemType, obj.ItemInputType, obj.VisualQuestion, obj.Value, obj.Qty1Answer);
        //    //                }
        //    //                _CostCentreParamsArray[7] = inputQueueObj;
        //    //            }
        //    //            else
        //    //            {
        //    //                _CostCentreParamsArray[7] = new InputQueue();
        //    //            }
        //    //        }


        //    //        //_CostCentreParamsArray(0) = Common.g_GlobalData;
        //    //        //GlobalData

        //    //        //this mode will load the questionqueue

        //    //        //QuestionQueue / Execution Queue
        //    //        _CostCentreParamsArray[3] = CostCentreQueue;
        //    //        // check if cc has wk ins


        //    //        //CostCentreQueue
        //    //        _CostCentreParamsArray[4] = 1;

        //    //        _CostCentreParamsArray[5] = 1;
        //    //        //MultipleQuantities


        //    //        _CostCentreParamsArray[11] = OrderedQuantity;

        //    //        //CurrentQuantity
        //    //        _CostCentreParamsArray[6] = new List<StockQueueItem>();
        //    //        //StockQueue

        //    //        //InputQueue

        //    //        if (!string.IsNullOrEmpty(ClonedItemId))
        //    //        {
        //    //            if (!string.IsNullOrEmpty(OrderedQuantity))
        //    //            {
        //    //                var section = _ItemService.GetItemFirstSectionByItemId(Convert.ToInt64(ClonedItemId));
        //    //                if (section != null)
        //    //                {
        //    //                    section.Qty1 = Convert.ToInt32(OrderedQuantity);
        //    //                }
        //    //                _CostCentreParamsArray[8] = section;
        //    //            }
        //    //            //if (OrderedQuantity == "null" || OrderedQuantity == null)
        //    //            //{
        //    //            //    // get first item section
        //    //            //    _CostCentreParamsArray[8] = _ItemService.GetItemFirstSectionByItemId(Convert.ToInt64(ClonedItemId));
        //    //            //}
        //    //            //else
        //    //            //{
        //    //            //    // update quantity in item section and return
        //    //            //    _CostCentreParamsArray[8] = _ItemService.UpdateItemFirstSectionByItemId(Convert.ToInt64(ClonedItemId), Convert.ToInt32(OrderedQuantity));

        //    //            //}
        //    //        }

        //    //        _CostCentreParamsArray[9] = 1;

        //    //        // connection string
        //    //        _CostCentreParamsArray[10] = "Persist Security Info=False;Integrated Security=false;Initial Catalog=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringDBName"] + ";server=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringServerName"] + "; user id=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringUserName"] + "; password=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringPasswordName"] + ";";






        //    //        _oLocalObject = _CostCentreLaoderFactory.Create(HttpContext.Current.Server.MapPath("/") + "\\ccAssembly\\" + OrganizationName + "UserCostCentres.dll", "UserCostCentres." + oCostCentre.CodeFileName, null);
        //    //        _oRemoteObject = (ICostCentreLoader)_oLocalObject;

        //    //        CostCentrePriceResult oResult = null;

        //    //        if (CallMode == "Modify")
        //    //        {
        //    //            _CostCentreParamsArray[1] = CostCentreExecutionMode.PromptMode;
        //    //            if (Queues.QuestionQueues != null)
        //    //            {
        //    //                _CostCentreParamsArray[2] = Queues.QuestionQueues.Where(c => c.CostCentreID == oCostCentre.CostCentreId).ToList();
        //    //            }
        //    //            else
        //    //            {
        //    //                _CostCentreParamsArray[2] = new List<QuestionQueueItem>();
        //    //            }

        //    //            if (Queues.InputQueues != null)
        //    //            {
        //    //                InputQueue inputQueueObj = new InputQueue();
        //    //                List<InputQueueItem> Items = Queues.InputQueues.Where(c => c.CostCentreID == oCostCentre.CostCentreId).ToList();
        //    //                foreach (InputQueueItem obj in Items)
        //    //                {
        //    //                    inputQueueObj.addItem(obj.ID, obj.VisualQuestion, obj.CostCentreID, obj.ItemType, obj.ItemInputType, obj.VisualQuestion, obj.Value, obj.Qty1Answer);
        //    //                }
        //    //                _CostCentreParamsArray[7] = inputQueueObj;
        //    //            }
        //    //            else
        //    //            {
        //    //                _CostCentreParamsArray[7] = new InputQueue();
        //    //            }


        //    //        }
        //    //        else
        //    //        {
        //    //            if (CallMode == "UpdateAllCostCentreOnQuantityChange")
        //    //            {
        //    //                _CostCentreParamsArray[1] = CostCentreExecutionMode.ExecuteMode;
        //    //                _CostCentreParamsArray[2] = Queues.QuestionQueues.ToList();
        //    //                if (Queues.InputQueues != null)
        //    //                {
        //    //                    InputQueue inputQueueObj = new InputQueue();
        //    //                    List<InputQueueItem> Items = Queues.InputQueues.ToList();
        //    //                    foreach (InputQueueItem obj in Items)
        //    //                    {
        //    //                        inputQueueObj.addItem(obj.ID, obj.VisualQuestion, obj.CostCentreID, obj.ItemType, obj.ItemInputType, obj.VisualQuestion, obj.Value, obj.Qty1Answer);
        //    //                    }
        //    //                    _CostCentreParamsArray[7] = inputQueueObj.Items;
        //    //                }
        //    //                else
        //    //                {
        //    //                    _CostCentreParamsArray[7] = new InputQueue();
        //    //                }
        //    //            }
        //    //            oResult = _oRemoteObject.returnPrice(ref _CostCentreParamsArray);
                        
                       
        //    //        }

        //    //        if ((Queues != null && CallMode != "Modify" && CallMode != "addAnother" && oResult != null) || (Queues != null && CallMode == "Update"))
        //    //        {


        //    //            JsonSerializerSettings jSettings = new Newtonsoft.Json.JsonSerializerSettings();
        //    //            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = jSettings;

        //    //            double actualPrice = oResult.TotalPrice;

        //    //            if (actualPrice < oCostCentre.MinimumCost && oCostCentre.MinimumCost != 0)
        //    //            {
        //    //                actualPrice = oCostCentre.MinimumCost ?? 0;
        //    //            }

        //    //            return Request.CreateResponse(HttpStatusCode.OK, actualPrice);
        //    //        }
        //    //        else
        //    //        {
        //    //            JsonSerializerSettings jSettings = new Newtonsoft.Json.JsonSerializerSettings();
        //    //            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = jSettings;

        //    //            return Request.CreateResponse(HttpStatusCode.OK, _CostCentreParamsArray);
        //    //        }

        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        throw ex;
        //    //    }
        //    //    finally
        //    //    {
        //    //        AppDomain.Unload(_AppDomain);
        //    //    }

        //    //}
        //    //else
        //    //{
        //    //    return Request.CreateResponse(HttpStatusCode.OK, "");
        //    //}

        //}

        private double GetCostCenterPrice(string CostCentreId, string OrderedQuantity, string CallMode, QuestionAndInputQueues Queues, ref object[] _CostCentreParamsArray, ItemSection section)
        {
            double dblPrice = 0;
            if (CallMode == "" && Queues == null && section != null)
            {
                section.Qty1 = Convert.ToInt32(OrderedQuantity);
                var oSection = section.CreateFrom();
                _CostCentreParamsArray[8] = oSection;
            }
            if ((CallMode == "UpdateAllCostCentreOnQuantityChange" && Queues != null) || CallMode != "UpdateAllCostCentreOnQuantityChange")
            {
                AppDomain _AppDomain = null;

                try
                {
                    Organisation organisation = _myOrganizationService.GetOrganisation();
                    // MyCompanyDomainBaseReponse companyBaseResponse = _companyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

                    string orgName = string.Empty;
                    if (organisation != null)
                    {
                        orgName = organisation.OrganisationName;
                    }

                    string OrganizationName = orgName;
                    OrganizationName = specialCharactersEncoderCostCentre(OrganizationName);

                    AppDomainSetup _AppDomainSetup = new AppDomainSetup();


                    object _oLocalObject;
                    ICostCentreLoader _oRemoteObject;

                   // object[] _CostCentreParamsArray = new object[12];

                    _AppDomainSetup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
                    _AppDomainSetup.PrivateBinPath = Path.GetDirectoryName((new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath);


                    _AppDomain = AppDomain.CreateDomain("CostCentresDomain", null, _AppDomainSetup);
                    //Me._AppDomain.InitializeLifetimeService()

                    List<CostCentreQueueItem> CostCentreQueue = new List<CostCentreQueueItem>();




                    //Me._CostCentreLaoderFactory = CType(Me._AppDomain.CreateInstance(Common.g_GlobalData.AppSettings.ApplicationStartupPath + "\Infinity.Model.dll", "Infinity.Model.CostCentres.CostCentreLoaderFactory").Unwrap(), Model.CostCentres.CostCentreLoaderFactory)
                    CostCentreLoaderFactory _CostCentreLaoderFactory = (CostCentreLoaderFactory)_AppDomain.CreateInstance("MPC.Interfaces", "MPC.Interfaces.WebStoreServices.CostCentreLoaderFactory").Unwrap();
                    _CostCentreLaoderFactory.InitializeLifetimeService();

                    CostCentre oCostCentre = CostCenterService.GetCostCentreById(Convert.ToInt64(CostCentreId));

                    List<CostcentreInstruction> oInstList = new List<CostcentreInstruction>();
                    List<CostcentreWorkInstructionsChoice> oInsChoicesList = null;
                    foreach (CostcentreInstruction obj in oCostCentre.CostcentreInstructions)
                    {
                        CostcentreInstruction oObject = new CostcentreInstruction();
                        oObject.CostCentreId = obj.CostCentreId;
                        oObject.Instruction = obj.Instruction;
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
                   // _CostCentreParamsArray[4] = 1;

                   // _CostCentreParamsArray[5] = 1;
                    //MultipleQuantities


                    _CostCentreParamsArray[11] = OrderedQuantity;

                    //CurrentQuantity
                    _CostCentreParamsArray[6] = new List<StockQueueItem>();
                    //StockQueue

                    //InputQueue
                    if (section != null)
                    {
                        section.Qty1 = Convert.ToInt32(OrderedQuantity);
                        section.SectionInkCoverages = null;
                        _CostCentreParamsArray[8] = section.CreateFrom();
                    }
                    
                    _CostCentreParamsArray[9] = 1;

                    // connection string
                    _CostCentreParamsArray[10] = "Persist Security Info=False;Integrated Security=false;Initial Catalog=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringDBName"] + ";server=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringServerName"] + "; user id=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringUserName"] + "; password=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringPasswordName"] + ";";
                    
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

                        dblPrice = actualPrice;
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
            
            return dblPrice;
        }

        private string specialCharactersEncoderCostCentre(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = value.Replace("/", "");
                value = value.Replace(" ", "");
                value = value.Replace(";", "");
                value = value.Replace("&#34;", "");
                value = value.Replace("&", "");
                value = value.Replace("+", "");
            }

            return value;
        }

        
    }
}
