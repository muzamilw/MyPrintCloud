using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using Microsoft.VisualBasic;
using MPC.ExceptionHandling;
using MPC.Interfaces.Common;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using CalculationMethods = MPC.Models.Common.CostCentrCalculationMethods;
using System.Linq;

namespace MPC.Implementation.MISServices
{
    public class CostCenterService : ICostCentersService
    {
        #region Private

        private readonly ICostCentreRepository _costCenterRepository;
        private readonly IChartOfAccountRepository _chartOfAccountRepository;
        private readonly ISystemUserRepository _systemUserRepository;
        private readonly ICostCenterTypeRepository _costcentreTypeRepository;
        private readonly IMarkupRepository _markupRepository;
        private readonly ICostCentreVariableRepository _costCentreVariableRepository;
        private readonly IDeliveryCarrierRepository _deliveryCarrierRepository;
        private readonly IOrganisationRepository _organisationRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IMachineClickChargeZoneRepository _machineClickChargeZoneRepository;

        #endregion

        #region Constructor

        public CostCenterService(ICostCentreRepository costCenterRepository, IChartOfAccountRepository chartOfAccountRepository, ISystemUserRepository systemUserRepository, ICostCenterTypeRepository costCenterTypeRepository,
            IMarkupRepository markupRepository, ICostCentreVariableRepository costCentreVariableRepository, IDeliveryCarrierRepository deliveryCarrierRepository, IOrganisationRepository organisationRepository, IItemRepository itemRepository,
            IMachineClickChargeZoneRepository machineClickChargeZoneRepository)
        {
            if (costCenterRepository == null)
            {
                throw new ArgumentNullException("costCenterRepository");
            }
            if (chartOfAccountRepository == null)
            {
                throw new ArgumentNullException("chartOfAccountRepository");
            }
            if (systemUserRepository == null)
            {
                throw new ArgumentNullException("systemUserRepository");
            }
            if (costCenterTypeRepository == null)
            {
                throw new ArgumentNullException("costCenterTypeRepository");
            }
            if (markupRepository == null)
            {
                throw new ArgumentNullException("markupRepository");
            }
            if (costCentreVariableRepository == null)
            {
                throw new ArgumentNullException("costCentreVariableRepository");
            }
            if (deliveryCarrierRepository == null)
            {
                throw new ArgumentNullException("deliveryCarrierRepository");
            }
            if (organisationRepository == null)
            {
                throw new ArgumentNullException("organisationRepository");
            }
            if (itemRepository == null)
            {
                throw new ArgumentNullException("itemRepository");
            }
            if (machineClickChargeZoneRepository == null)
            {
                throw new ArgumentNullException("machineClickChargeZoneRepository");
            }
            this._costCenterRepository = costCenterRepository;
            this._chartOfAccountRepository = chartOfAccountRepository;
            this._systemUserRepository = systemUserRepository;
            this._costcentreTypeRepository = costCenterTypeRepository;
            this._markupRepository = markupRepository;
            this._costCentreVariableRepository = costCentreVariableRepository;
            this._deliveryCarrierRepository = deliveryCarrierRepository;
            this._organisationRepository = organisationRepository;
            this._itemRepository = itemRepository;
            _machineClickChargeZoneRepository = machineClickChargeZoneRepository;
        }

        #endregion

        #region Public
        
        public IEnumerable<CostCentre> GetAll(CostCenterRequestModel request)
        {
            return _costCenterRepository.GetAllNonSystemCostCentres();
        }
        public CostCentreResponse GetAllForOrderProduct(GetCostCentresRequest requestModel)
        {
            return _costCenterRepository.GetAllNonSystemCostCentresForProduct(requestModel);
        }

        public CostCentre Add(CostCentre costcenter)
        {
            costcenter.ThumbnailImageURL = SaveCostCenterImage(costcenter);
            if(costcenter.Type == (int)CostCenterTypes.Delivery)
            {
                costcenter.IsParsed = true;
                costcenter.TypeName = "Delivery";
                _costCenterRepository.InsertCostCentre(costcenter);                
            }
            else
            {
                Organisation org = _organisationRepository.GetOrganizatiobByID();
                string sOrgName = specialCharactersEncoderCostCentre(org.OrganisationName);
                // _costCenterRepository.Add(costcenter);
                SaveCostCentre(costcenter, org.OrganisationId, sOrgName, true);
            }
           
            return costcenter;
        }

        public CostCentre Update(CostCentre costcenter)
        {
            costcenter.ThumbnailImageURL = SaveCostCenterImage(costcenter);
            _costCenterRepository.Update(costcenter);
            if (costcenter.Type == (int)CostCenterTypes.Delivery)
            {
                costcenter.IsParsed = true;
                _costCenterRepository.UpdateCostCentre(costcenter);
            }
            else
            {
                Organisation org = _organisationRepository.GetOrganizatiobByID();
                string sOrgName = specialCharactersEncoderCostCentre(org.OrganisationName);
                SaveCostCentre(costcenter, org.OrganisationId, sOrgName, false);  
            }
                      
            return costcenter;
        }

        public long CopyCostCenter(CostCentre costcenter)
        {
            Organisation org = _organisationRepository.GetOrganizatiobByID();
            string sOrgName = specialCharactersEncoderCostCentre(org.OrganisationName);
            CostCentre newCostCentre = new CostCentre();
            costcenter.ThumbnailImageURL = SaveCostCenterImage(costcenter);
            newCostCentre = CloneCostCenter(costcenter, newCostCentre);
            newCostCentre.OrganisationId = org.OrganisationId;
            _costCenterRepository.Add(newCostCentre);
            _costCenterRepository.SaveChanges();

           SaveCostCentre(newCostCentre, org.OrganisationId, sOrgName, false);

            return newCostCentre.CostCentreId;
        }

        private CostCentre CloneCostCenter(CostCentre source, CostCentre target)
        {
            target.Name = source.Name + " Copy";
            target.SetupCost = source.SetupCost;
            target.CalculationMethodType = source.CalculationMethodType;
            target.CostCentreType = source.CostCentreType;
            target.CostDefaultValue = source.CostDefaultValue;
            target.CostPerUnitQuantity = source.CostPerUnitQuantity;
            target.strActualCostLabourParsed = source.strActualCostLabourParsed;
            target.CostQuestionString = source.CostQuestionString;
            target.DefaultVA = source.DefaultVA;
            target.DefaultVAId = source.DefaultVAId;
            target.CreationDate = DateTime.Now;
            target.Description = source.Description;
            target.IsDisabled = source.IsDisabled;
            target.IsPrintOnJobCard = source.IsPrintOnJobCard;
            target.Priority = source.Priority;
            target.EstimateProductionTime = source.EstimateProductionTime;
            target.WebStoreDesc = source.WebStoreDesc;
            target.strPriceLabourParsed = source.strPriceLabourParsed;
            target.MinimumCost = source.MinimumCost;
            target.PerHourPrice = source.PerHourPrice;
            target.TimeVariableId = source.TimeVariableId;
            target.TimeSourceType = source.TimeSourceType;
            target.TimeQuestionDefaultValue = source.TimeQuestionDefaultValue;
            target.TimeQuestionString = source.TimeQuestionString;
            target.PricePerUnitQuantity = source.PricePerUnitQuantity;
            target.QuantityVariableId = source.QuantityVariableId;
            target.QuantityQuestionString = source.QuantityQuestionString;
            target.QuantityQuestionDefaultValue = source.QuantityQuestionDefaultValue;
            target.QuantitySourceType = source.QuantitySourceType;
            target.Type = source.Type;
            if (source.CostcentreInstructions != null && source.CostcentreInstructions.Count > 0)
            {
                target.CostcentreInstructions = new Collection<CostcentreInstruction>();
                foreach (var instruction in source.CostcentreInstructions)
                {
                    CostcentreInstruction newInstruction = new CostcentreInstruction
                    {
                        Instruction = instruction.Instruction,
                        CostCenterOption = instruction.CostCenterOption
                    };
                    
                    if (instruction.CostcentreWorkInstructionsChoices != null &&
                        instruction.CostcentreWorkInstructionsChoices.Count > 0)
                    {
                        newInstruction.CostcentreWorkInstructionsChoices = new Collection<CostcentreWorkInstructionsChoice>();
                        foreach (var choice in instruction.CostcentreWorkInstructionsChoices)
                        {
                            CostcentreWorkInstructionsChoice newChoice = new CostcentreWorkInstructionsChoice
                            {
                                Choice = choice.Choice
                            };
                            newInstruction.CostcentreWorkInstructionsChoices.Add(newChoice);
                        }
                    }
                    target.CostcentreInstructions.Add(newInstruction);
                }
            }


            return target;
        }

        

        public void CostCentreDLL(CostCentre costcenter,long organisationId)
        {
            Organisation org = _organisationRepository.GetOrganizatiobByOrganisationID(organisationId);
            string sOrgName = specialCharactersEncoderCostCentre(org.OrganisationName);
            SaveCostCentre(costcenter, org.OrganisationId, sOrgName, false);  


        }
        public bool Delete(long costcenterId)
        {
          //  _costCenterRepository.Delete(GetCostCentreById(costcenterId));
          //  _costCenterRepository.SaveChanges();
            return true;
        }

        public CostCentre GetCostCentreById(long id)
        {
             return _costCenterRepository.GetCostCentreByID(id);
        }
        public CostCentersResponse GetUserDefinedCostCenters(CostCenterRequestModel request)
        {
            return _costCenterRepository.GetUserDefinedCostCenters(request);
        }

        public CostCenterBaseResponse GetBaseData()
        {
            return _costCenterRepository.GetBaseData();
            
           
        }

        public void SaveCostCentre(CostCentre costcenter, long OrganisationId, string OrganisationName, bool isNew)
        {
            long _CostCentreID = costcenter.CostCentreId;
            //creating a costcentre code file and updating it and compile it.
            bool IsNewCostCentre = isNew;
            CostCentreTemplate oTemplate = _costCenterRepository.LoadCostCentreTemplate(2);
            string Header, Footer, Middle;
            double SetupCost = costcenter.SetupCost ?? 0;
            int SetupTime = costcenter.SetupTime ?? 0;
            double MinCost = costcenter.MinimumCost ?? 0;
            double DefaultProfitMargin = costcenter.DefaultVA ?? 0;
            string sCostPlant = string.Empty;
            string sCostLabour = string.Empty;
            string sCostStock = string.Empty;
            string sTime = string.Empty;
            string sPricePlant = string.Empty;
            string sPriceLabour = string.Empty;
            string sPriceStock = string.Empty;
            string sActualPlantCost = "";
            string sActualStockCost = "";
            string sActualLabourCost = "";
            StringBuilder sCode = new StringBuilder();
            if (costcenter.Type > 0)
            {
                CostCentreType otype = _costcentreTypeRepository.GetCostCenterTypeById(costcenter.Type);
                costcenter.TypeName = otype.TypeName;
            }
            if (costcenter.CalculationMethodType == (int)CalculationMethods.Fixed)
            {
                sCostPlant = " {cinput,id=\"1\",question=\"" + costcenter.CostQuestionString + "\",type=\"0\",InputType=\"0\",value=\"" + costcenter.CostDefaultValue + "\"} ";
                sPricePlant = " {cinput,id=\"2\",question=\"" + costcenter.PriceQuestionString + "\",type=\"0\",InputType=\"0\",value=\"" + costcenter.PriceDefaultValue + "\"} ";
                sTime = " {cinput,id=\"3\",question=\"" + costcenter.EstimatedTimeQuestionString + "\",type=\"0\",InputType=\"0\",value=\"" + costcenter.EstimatedTimeDefaultValue + "\"} ";
                sCostPlant = TokenParse(sCostPlant);
                sPricePlant = TokenParse(sPricePlant);
                sTime = TokenParse(sTime);
            }
            else if (costcenter.CalculationMethodType == (int)CalculationMethods.QuantityBase)
            {
                string sCodeString = string.Empty;
                if (costcenter.QuantitySourceType == 1)
                {
                    var varName = _costCentreVariableRepository.LoadVariable(costcenter.QuantityVariableId??0);
                    sCodeString = "Dim vQuantity as Integer = " + "{SystemVariable, ID=\"" + Convert.ToString(costcenter.QuantityVariableId) + "\",Name=\"" + (varName != null? varName.Name : "") + "\"}" + Environment.NewLine; 
                    sCodeString += "EstimatedPlantCost =  {cinput,id=\"2\",question=\"Setup Cost\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.SetupCost) + "\"} " + "  + (" + "{cinput,id=\"2\",question=\"Cost Per Unit Quantity\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.CostPerUnitQuantity) + "\"} * vQuantity )";
                    sCostPlant += sCodeString;

                    sCodeString = "Dim vQuantity as Integer = " + "{SystemVariable, ID=\"" + Convert.ToString(costcenter.QuantityVariableId) + "\",Name=\"" + (varName != null ? varName.Name : "") + "\"}" + Environment.NewLine; 
                    sCodeString += "QuotedPlantPrice =  ("  + Convert.ToString(costcenter.PricePerUnitQuantity) + " * vQuantity )";
                    sPricePlant += sCodeString;

                    sCodeString = "EstimatedTime =  " + "{cinput,id=\"1\",question=\"Setup Time\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.SetupTime) + "\"} " + " + ( " + "{cinput,id=\"6\",question=\"Time per Unit Quantity\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.TimePerUnitQuantity) + "\"} * vQuantity )";
                    sTime += sCodeString;
                }
                else if (costcenter.QuantitySourceType == 2)
                {
                    sCodeString = "Dim vQuantity as Integer =  {cinput,id=\"6\",question=\"" + costcenter.QuantityQuestionString + "\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.QuantityQuestionDefaultValue) + "\"} " + Environment.NewLine;
                    sCodeString += "EstimatedPlantCost =  " + "{cinput,id=\"2\",question=\"Setup Cost\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.SetupCost) + "\"} " + "  + (" + "{cinput,id=\"3\",question=\"Cost Per Unit Quantity\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.CostPerUnitQuantity) + "\"} * vQuantity ) ";
                    sCostPlant += sCodeString;

                    sCodeString = "Dim vQuantity as Integer =  {cinput,id=\"6\",question=\"" + costcenter.QuantityQuestionString + "\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.QuantityQuestionDefaultValue) + "\"} " + Environment.NewLine;
                    sCodeString += "QuotedPlantPrice =  (" + Convert.ToString(costcenter.PricePerUnitQuantity) + " * vQuantity ) ";
                    sPricePlant += sCodeString;

                    sCodeString = "EstimatedTime =  " + "{cinput,id=\"1\",question=\"Setup Time\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.SetupTime) + "\"} " + " + ( " + "{cinput,id=\"7\",question=\"Time per Unit Quantity\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.TimePerUnitQuantity) + "\"} * vQuantity ) ";
                    sTime += sCodeString;
                }
            }
            else if (costcenter.CalculationMethodType == (int)CalculationMethods.PerHour)
            {
                string sCodeString = string.Empty;
                if (costcenter.TimeSourceType == 1)
                {

                    var varName = _costCentreVariableRepository.LoadVariable(costcenter.TimeVariableId ?? 0);
                    sCodeString = "Dim vNoOfHours as Integer = " + "{SystemVariable, ID=\"" + Convert.ToString(costcenter.TimeVariableId) + "\",Name=\"" + (varName != null ? varName.Name : "") + "\"}" + Environment.NewLine;
                    sCodeString += "EstimatedPlantCost = 0 ";
                    sCostPlant += sCodeString;

                    sCodeString = "Dim vNoOfHours as Integer = " + "{SystemVariable, ID=\"" + Convert.ToString(costcenter.TimeVariableId) + "\",Name=\"" + (varName != null ? varName.Name : "") + "\"}" + Environment.NewLine;
                    sCodeString += "QuotedPlantPrice = SetupCost +  (vNoOfHours * " + Convert.ToString(costcenter.PerHourPrice == null ? "0" : costcenter.PerHourPrice.ToString()) + " )  ";
                    sPricePlant += sCodeString;

                    sCodeString = "EstimatedTime = (vNoOfHours + " + "{cinput,id=\"1\",question=\"Setup Time\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.SetupTime) + "\"} " + " ) ";
                    sTime += sCodeString;
                }
                else if (costcenter.TimeSourceType == 2)
                {
                    sCodeString = "Dim vNoOfHours as Integer =  {cinput,id=\"6\",question=\"" + costcenter.TimeQuestionString + "\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.SetupTime) + "\"} " + Environment.NewLine;
                    sCodeString += "EstimatedPlantCost = 0";
                    sCostPlant += sCodeString;

                    sCodeString = "Dim vNoOfHours as Integer =  {cinput,id=\"6\",question=\"" + costcenter.TimeQuestionString + "\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.TimeQuestionDefaultValue) + "\"} " + Environment.NewLine;
                    sCodeString += "QuotedPlantPrice = SetupCost +  (vNoOfHours * " + Convert.ToString(costcenter.PerHourPrice == null ? "0" : costcenter.PerHourPrice.ToString()) + " )  ";
                    sPricePlant += sCodeString;

                    sCodeString = "EstimatedTime = (vNoOfHours + " + "{cinput,id=\"1\",question=\"Setup Time\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.SetupTime) + "\"} " + " ) ";
                    sTime += sCodeString;
                }
            }
            else if (costcenter.CalculationMethodType == (int)CalculationMethods.FormulaBase)
            {
                
                // Set Cost Strings
                if (string.IsNullOrEmpty(costcenter.strCostPlantUnParsed))
                    sCostPlant = "EstimatedPlantCost = 0";
                else
                    sCostPlant = costcenter.strCostPlantUnParsed;
                if (string.IsNullOrEmpty(costcenter.strCostLabourUnParsed))
                    sCostLabour = "EstimatedLabourCost = 0";
                else
                    sCostLabour = costcenter.strCostLabourUnParsed;
                if (string.IsNullOrEmpty(costcenter.strCostMaterialUnParsed))
                    sCostStock = "EstimatedMaterialCost = 0";
                else
                    sCostStock = costcenter.strCostMaterialUnParsed;
                if (string.IsNullOrEmpty(costcenter.strTimeUnParsed))
                    sTime = "EstimatedTime = 0";
                else
                    sTime = costcenter.strTimeUnParsed;

                // Set Price Strings
                if (string.IsNullOrEmpty(costcenter.strPricePlantUnParsed))
                    sPricePlant = "QuotedPlantPrice = 0";
                else
                    sPricePlant = costcenter.strPricePlantUnParsed;
                if (string.IsNullOrEmpty(costcenter.strPriceLabourUnParsed))
                    sPriceLabour = "QuotedLabourPrice = 0";
                else
                    sPriceLabour = costcenter.strPriceLabourUnParsed;
                if (string.IsNullOrEmpty(costcenter.strPriceMaterialUnParsed))
                    sPriceStock = "QuotedMaterialPrice = 0";
                else
                    sPriceStock = costcenter.strPriceMaterialUnParsed;
                                
                //sCostPlant = TokenParse("EstimatedPlantCost = {SystemVariable, ID=\"1\",Name=\"Number of unique Inks used on Side 1\"} * {question, ID=\"13\",caption=\"How many boxes\"} * {matrix, ID=\"19\",Name=\"Super Formula Matrix\"} * {question, ID=\"34\",caption=\"How many sections to fold?\"} * {question, ID=\"51\",caption=\"Multiple Options\"} ");
                
                
            }
            
                sCostPlant = TokenParse(sCostPlant);
                sCostLabour = TokenParse(sCostLabour);
                sCostStock = TokenParse(sCostStock);
                sTime = TokenParse(sTime);

                sPricePlant = TokenParse(sPricePlant);
                sPriceLabour = TokenParse(sPriceLabour);
                sPriceStock = TokenParse(sPriceStock);
                costcenter.IsParsed = true;
            
            {
                char spacechar = '\0';
                spacechar = (char)95;
                //replacing any unwanted characters in the costcentrename and create file name

                Header = oTemplate.Header.Substring(0, oTemplate.Header.IndexOf("''<cost>") - 2);
                Middle = oTemplate.Middle.Substring(0, oTemplate.Middle.IndexOf("''<price>") - 2);
                Footer = oTemplate.Footer;

                //'getting new Cost centreID from Database

                if (_CostCentreID == 0)
                {
                    _CostCentreID = this.GetMaxCostCentreID(); 
                    IsNewCostCentre = true;
                }


                //'replacing the CostCentre name and ID in the header string
                Header = Header.Replace("ccname", "CLS_" + _CostCentreID.ToString());
                Header = Header.Replace("<ccid>", _CostCentreID.ToString());

                //replacing the attribs of cost centre
                Header = Header.Replace("<ccsc>", SetupCost.ToString());
                Header = Header.Replace("<ccst>", SetupTime.ToString());
                Header = Header.Replace("<ccmc>", MinCost.ToString());
                Header = Header.Replace("<ccva>", DefaultProfitMargin.ToString());

                //'now making a code file string

                sCode.Append(Header);
                sCode.Append(Environment.NewLine + "''<cost>" + Environment.NewLine);
                sCode.Append(Environment.NewLine + "''<plant>" + Environment.NewLine);
                sCode.Append(sCostPlant);
                sCode.Append(Environment.NewLine + "''</plant>" + Environment.NewLine);
                sCode.Append(Environment.NewLine + "''<labour>" + Environment.NewLine);
                sCode.Append(sCostLabour);
                sCode.Append(Environment.NewLine + "''</labour>" + Environment.NewLine);
                sCode.Append(Environment.NewLine + "''<material>" + Environment.NewLine);
                sCode.Append(sCostStock);
                sCode.Append(Environment.NewLine + "''</material>" + Environment.NewLine);
                sCode.Append(Environment.NewLine + "''<time>" + Environment.NewLine);
                sCode.Append(sTime);
                sCode.Append(Environment.NewLine + "''</time>" + Environment.NewLine);

                sCode.Append(Environment.NewLine + "''</cost>" + Environment.NewLine);

                sCode.Append(Middle);

                sCode.Append(Environment.NewLine + "''<price>" + Environment.NewLine);
                sCode.Append(Environment.NewLine + "''<plant>" + Environment.NewLine);
                sCode.Append(sPricePlant);
                sCode.Append(Environment.NewLine + "''</plant>" + Environment.NewLine);
                sCode.Append(Environment.NewLine + "''<labour>" + Environment.NewLine);
                sCode.Append(sPriceLabour);
                sCode.Append(Environment.NewLine + "''</labour>" + Environment.NewLine);
                sCode.Append(Environment.NewLine + "''<material>" + Environment.NewLine);
                sCode.Append(sPriceStock);
                sCode.Append(Environment.NewLine + "''</material>" + Environment.NewLine);
                sCode.Append(Environment.NewLine + "''</price>" + Environment.NewLine);


                //process the footer here..
                //since footer also contains the ActualCOST area, we have to remove that AREA
                //and replace it with the new ACTUAL COST code

                //our code is between ''<actualcost>   and  ''</actualcost>
                int iStart = 0;
                int iStart2 = 0;
                int iLength = 0;
                string sActualCostString = null;

                sActualCostString = "''<plant>" + sActualPlantCost + "''</plant>" + "''</material>" + sActualStockCost + "''</material>" + "''</labour>" + sActualLabourCost + "''</labour>";

                iStart = Footer.IndexOf("''<actualcost>") + 14;
                iStart2 = Footer.IndexOf("''</actualcost>");
                Footer.Remove(iStart, iStart2 - iStart);
                Footer.Insert(iStart, sActualCostString);

                sCode.Append(Footer);
                CostCentre oCostCentre = null;
                if (!IsNewCostCentre)
                    oCostCentre = GetCostCentreById(_CostCentreID);
                else
                    oCostCentre = costcenter;


                oCostCentre.CodeFileName = "CLS_" + _CostCentreID.ToString();
                string oSource = "";

                //Get Complete Code of the CostCentre from the DB and Recompile it with the new changes
                List<CostCentre> oAllCostCentresCode = GetCompleteCodeofAllCostCentres(OrganisationId);


                //oSource += "Imports Infinity" + Environment.NewLine;
                oSource += "Imports System" + Environment.NewLine;
                oSource += "Imports System.Data" + Environment.NewLine;
                oSource += "Imports Microsoft.VisualBasic" + Environment.NewLine;
                //oSource += System.Configuration.ConfigurationSettings.AppSettings("DALProviderNameSpace") + Environment.NewLine;
                oSource += "Imports MPC.Implementation.MISServices" + Environment.NewLine;
                oSource += "Imports MPC.Implementation.WebStoreServices" + Environment.NewLine;
                oSource += "imports MPC.Models.DomainModels" + Environment.NewLine;
                oSource += "imports MPC.Models.Common" + Environment.NewLine;
                oSource += "Imports System.Reflection" + Environment.NewLine;
                oSource += "Imports Microsoft.Practices.Unity" + Environment.NewLine;
                oSource += "Imports ICostCentreService = MPC.Interfaces.WebStoreServices.ICostCentreService" + Environment.NewLine;
                oSource += "Namespace UserCostCentres" + Environment.NewLine;
                

                if (IsNewCostCentre == true)
                {
                    string Str = sCode.ToString();
                    Str = Str.Replace("Namespace UserCostCentres", "");

                    Str = Str.Replace("End Namespace", "");

                    oSource += Environment.NewLine + Str + Environment.NewLine;

                }

                if (oAllCostCentresCode != null)
                {
                    foreach (var oOtherCostCentre in oAllCostCentresCode)
                    {
                        string Str = "";
                        if (_CostCentreID == oOtherCostCentre.CostCentreId)
                        {
                            Str = sCode.ToString();
                        }
                        else
                        {
                            Str = oOtherCostCentre.CompleteCode;
                        }


                        Str = Str.Replace("Namespace UserCostCentres", "");
                        Str = Str.Replace("End Namespace", "");

                        oSource += Environment.NewLine + Str + Environment.NewLine;

                    }
                }
                else
                {
                    string Str = "";
                    Str = sCode.ToString();
                    Str = Str.Replace("Namespace UserCostCentres", "");
                    Str = Str.Replace("End Namespace", "");
                    oSource += Environment.NewLine + Str + Environment.NewLine;
                }

                oSource += "End Namespace" + Environment.NewLine;

                //'if compilation fails then delete the code file

                System.IO.FileStream oFileStream; ;
                //string oCompanyName = OrganisationName;

                //replacing any unwanted characters in the CompanyNames and create file name

                bool IsCompiled = true;

                try
                {
                    //Compile Code of CostCentres
                    CompileBinaries(HttpContext.Current.Server.MapPath("/") + "\\ccAssembly\\", oSource, OrganisationName);

                    //Get CostCentre File Open it in Read Mode
                    //oFileStream = System.IO.File.OpenRead(HttpContext.Current.Server.MapPath("/") + "\\ccAssembly\\" + OrganisationName + "UserCostCentres.dll");

                    //Get Byte Array of the file and write it in the db
                    //byte[] CostCentreByte = new byte[Convert.ToInt32(oFileStream.Length - 1) + 1];

                    //oFileStream.Read(CostCentreByte, 0, Convert.ToInt32(oFileStream.Length - 1));

                    //CostCentreByte = null;
                }
                catch (Exception ex)
                {
                    //    BLL.CostCentres.CostCentre.DeleteCodeFile(sCostCentreFileName, Application.StartupPath.ToString + "\binaries\")
                    IsCompiled = false;
                        //throw new Exception("Error Compiling Costcentre", ex.ToString());

                    throw ex;

                }
                finally
                {

                    oFileStream = null;

                }

                oCostCentre.CompleteCode = sCode.ToString();

                if (IsNewCostCentre)
                {
                    _costCenterRepository.InsertCostCentre(oCostCentre);
                }
                else
                {
                    _costCenterRepository.UpdateCostCentre(costcenter);
                }
            }
            
            
        }

        public string TokenParse(string sText)
        {
            try
            {
                string ChangedStr = "";
                string[] oBeforeStr = sText.Split('{');
                ChangedStr = oBeforeStr[0];
                if (oBeforeStr.Length > 1)
                {
                    for (int i = 1; i <= oBeforeStr.Length - 1; i++)
                    {
                        string[] oAfterStr = oBeforeStr[i].Split('}');
                        if (oAfterStr.Length == 2)
                        {
                            string oReplaceStr = ReplaceTag(oAfterStr[0]);
                            if (!string.IsNullOrEmpty(oReplaceStr))
                            {
                                if (oAfterStr[1].Split('}').Length > 1)
                                {
                                    throw new Exception("Invalid Calculation String.");
                                    return "";
                                }
                                ChangedStr += oReplaceStr + oAfterStr[1];
                            }
                            else
                            {
                                return "";
                            }
                        }
                        else
                        {
                            throw new Exception("Invalid Calculation String.");
                            return "";
                        }
                    }
                }
                else
                {
                    string[] oAfterStr = sText.Split('}');
                    if (oAfterStr.Length > 1)
                    {
                        throw new Exception("Invalid Tag.");
                        return "";
                    }
                }
                return ChangedStr;
            }
            catch (Exception ex)
            {
                throw new Exception("Token Parse", ex);
            }
        }

        public string ReplaceTag(string sText)
        {
            try
            {
                string[] oSpiltTokens = sText.Split(',');
                // For i As Integer = 0 To oSpiltTokens.Length - 1

                if (oSpiltTokens.Length > 1)
                {

                    string[] GetID = oSpiltTokens[1].Split('\"');
                    string[] GetReturnValue = null;
                    string[] GetValue = null;

                    switch (oSpiltTokens[0].ToLower())
                    {


                        case "systemvariable":
                            GetID = oSpiltTokens[1].Split('\"');
                            if (GetID.Length == 3)
                            {
                                return " _CostCentreService.ExecuteVariable(ParamsArray ,\"" + GetID[1] + "\") ";
                            }
                            else
                            {
                                throw new Exception("Invalid Calculation String.");
                                return "";
                            }

                            break;
                        case "question":
                            GetID = oSpiltTokens[1].Split('\"');
                            if (GetID.Length == 3)
                            {
                                return " _CostCentreService.ExecuteQuestion(ParamsArray,\"" + GetID[1] + "\",CostCentreID) ";
                            }
                            else
                            {
                                throw new Exception("Invalid Calculation String.");
                                return "";
                            }

                            break;
                        case "subcostcentre":

                            GetID = oSpiltTokens[1].Split('\"');
                            GetReturnValue = oSpiltTokens[3].Split('\"');
                            if (GetID.Length == 3)
                            {
                                return " _CostCentreService.ExecuteCostCentre(ParamsArray,\"" + GetID[1] + "\",\"" + GetReturnValue[1] + "\") ";
                            }
                            else
                            {
                                throw new Exception("Invalid Calculation String.");
                                return "";
                            }

                            break;
                        case "resource":

                            GetID = oSpiltTokens[1].Split('\"');
                            GetReturnValue = oSpiltTokens[3].Split('\"');
                            if (GetID.Length == 3 & GetReturnValue.Length == 3)
                            {
                                return " _CostCentreService.ExecuteResource(ParamsArray,\"" + GetID[1] + "\",\"" + GetReturnValue[1] + "\") ";
                            }
                            else
                            {
                                throw new Exception("Invalid Calculation String.");
                                return "";
                            }

                            break;
                        case "stock":

                            GetID = oSpiltTokens[1].Split('\"');
                            string[] GetName = oSpiltTokens[2].Split('\"');
                            string[] GetQType = oSpiltTokens[3].Split('\"');
                            //Like per unit , per package
                            string[] GetQtyType = oSpiltTokens[4].Split('\"');
                            //Qty,Variable or string
                            GetValue = oSpiltTokens[5].Split('\"');

                            if (((GetID.Length == 3 & GetName.Length == 3) & (GetQType.Length == 3 & GetQtyType.Length == 3) & GetValue.Length == 3))
                            {
                                return " _CostCentreService.ExecuteStockItem(ParamsArray,\"" + GetID[1] + "\",\"" + GetName[1] + "\",\"" + GetQtyType[1] + "\",\"" + GetValue[1] + "\",\"" + GetQType[1] + "\",cint(CostCentreID)) ";
                            }
                            else
                            {
                                throw new Exception("Invalid Calculation String.");
                                return "";
                            }

                            break;
                        case "matrix":

                            GetID = oSpiltTokens[1].Split('\"');
                            if (GetID.Length == 3)
                            {
                                return " _CostCentreService.ExecuteMatrix(ParamsArray,\"" + GetID[1] + "\",cint(CostCentreID)) ";
                            }
                            else
                            {
                                throw new Exception("Invalid Calculation String.");
                                return "";
                            }

                            break;
                        case "cinput":

                            GetID = oSpiltTokens[1].Split('\"');
                            string[] GetQuestion = oSpiltTokens[2].Split('\"');
                            string[] GetTypes = oSpiltTokens[3].Split('\"');
                            string[] GetInputTypes = oSpiltTokens[4].Split('\"');
                            GetValue = oSpiltTokens[5].Split('\"');

                            if ((((GetID.Length == 3 & GetQuestion.Length == 3) & (GetTypes.Length == 3 & GetValue.Length == 3)) & GetInputTypes.Length == 3))
                            {
                                return " _CostCentreService.ExecuteInput(ParamsArray,\"" + GetID[1] + "\",\"" + GetQuestion[1] + "\"," + GetTypes[1] + "," + GetInputTypes[1] + ",\"" + GetValue[1] + "\",cint(CostCentreID)) ";
                            }
                            else
                            {
                                throw new Exception("Invalid Calculation String.");
                                return "";
                            }

                            break;
                        default:
                            throw new Exception("Invalid Calculation String.");
                            return "";
                    }
                }
                else
                {
                    return "";
                }
                //Next
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }

        /// <summary>
        ///     Compile the code with the source provided (Source provided will be in the form of text string)and generate dll.
        /// </summary>
        ///         ''' 
        public object CompileBinaries(string sOutputPath, string Source, string CompanyName)
        {
            try
            {
                VBCodeProvider csc = new VBCodeProvider();
                ICodeCompiler icc = csc.CreateCompiler();
                ICodeParser icp = csc.CreateParser();
                string errorString = null;

                //// Set input params for the compiler
                CompilerParameters co = new CompilerParameters();
                co.OutputAssembly = sOutputPath + CompanyName + "UserCostCentres.dll";
                //co.OutputAssembly = "c:\UserCostCentres.dll"

                co.GenerateInMemory = false;


                ///'''Dim assemblyFileName As String
                ///'''assemblyFileName = [Assembly].GetExecutingAssembly().GetName().CodeBase
                ///'''co.ReferencedAssemblies.Add(assemblyFileName.Substring("file:///".Length))
                ///'''co.ReferencedAssemblies.Add(sOutputPath + "CostCentreEngine.dll")
                ///'''
                ///'''

                ///////////////////////////////////
                //// Add available assemblies - this should be enough
                //for /////////the simplest            // applications.
                //System.Reflection.Assembly oAsm = default(System.Reflection.Assembly);



                foreach (var oAsm in AppDomain.CurrentDomain.GetAssemblies())
                {
                    //If Not (oAsm.GetType Is System.Reflection.Emit.AssemblyBuilder) Then
                    //co.ReferencedAssemblies.Add(oAsm.Location)
                    //End If

                    if ((!object.ReferenceEquals(oAsm.GetType(), typeof(System.Reflection.Emit.AssemblyBuilder))) && !oAsm.IsDynamic)
                    {
                        if (oAsm.Location.Length > 0)
                        {
                            co.ReferencedAssemblies.Add(oAsm.Location);
                        }
                    }
                }

                //Dim oFile As New IO.StreamWriter("c:/Test/Test.txt")
                //oFile.Write(Source)
                //oFile.Close()





                //For iCounter As Integer = co.ReferencedAssemblies.Count - 1 To 0 Step -1
                //    If co.ReferencedAssemblies.Item(icounter).Trim() = String.Empty Then
                //        co.ReferencedAssemblies.RemoveAt(icounter)
                //    End If
                //Next

                //co.ReferencedAssemblies.Add("E:\Development\Infinity\Infinity.UI\bin\Infinity.Componetns.CostCentreLoader.dll")
                //co.ReferencedAssemblies.Add("system.data.dll")
                //co.ReferencedAssemblies.Add("system.xml.dll")
                //co.ReferencedAssemblies.Add("system.drawing.dll")

                //co.ReferencedAssemblies.Add(sOutputPath + "ByteFX.Data.dll")
                //////////////////////////////////
                co.IncludeDebugInformation = true;
                //co.TreatWarningsAsErrors = True
                //co.CompilerOptions
                //co.WarningLevel = 1

                //// we want to genereate a DLL
                co.GenerateExecutable = false;

                //here we will have to retrieve the names of all vb files in binaries folder and pass them to compiler for compilation



                //source(0) = "costcentre.vb"
                //source(1) = "assemblyinfo.vb"
                //source(2) = "assemblyinfo.vb"

                //// Run the compiling process

                CompilerResults result = icc.CompileAssemblyFromSource(co, Source);


                //Dim result As CompilerResults = icc.CompileAssemblyFromSourceBatch(co, source)

                if (result.Errors.HasErrors == true)
                {
                    int iCounter = 0;
                    for (iCounter = 0; iCounter <= result.Errors.Count - 1; iCounter++)
                    {
                        errorString += result.Errors[iCounter].ToString() + Environment.NewLine;
                    }
                    result = null;
                    Source = null;
                    co = null;
                    throw new Exception("There are syntax errors in Cost Center Charge String. Please review Cost Center charge string. <br><br>Error Details : " + errorString + "<br><br> Output :");
                }
                else
                {
                    result = null;
                    Source = null;
                    co = null;
                }
                return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///     Compile the code and return the errors.
        /// </summary>
        ///
        public CompilerResults CompileSource(string Source)
        {
            try
            {
                VBCodeProvider csc = new VBCodeProvider();
                ICodeCompiler icc = csc.CreateCompiler();
                ICodeParser icp = csc.CreateParser();
                string errorString = null;

                //// Set input params for the compiler
                CompilerParameters co = new CompilerParameters();
                //co.OutputAssembly = "c:\UserCostCentres.dll"

                co.GenerateInMemory = true;


                ///'''Dim assemblyFileName As String
                ///'''assemblyFileName = [Assembly].GetExecutingAssembly().GetName().CodeBase
                ///'''co.ReferencedAssemblies.Add(assemblyFileName.Substring("file:///".Length))
                ///'''co.ReferencedAssemblies.Add(sOutputPath + "CostCentreEngine.dll")
                ///'''
                ///'''

                ///////////////////////////////////
                //// Add available assemblies - this should be enough
                //for /////////the simplest            // applications.
                //System.Reflection.Assembly oAsm = default(System.Reflection.Assembly);

                foreach (System.Reflection.Assembly oAsm in AppDomain.CurrentDomain.GetAssemblies())
                {
                    //If Not (oAsm.GetType Is System.Reflection.Emit.AssemblyBuilder) Then
                    //co.ReferencedAssemblies.Add(oAsm.Location)
                    //End If

                    if ((!object.ReferenceEquals(oAsm.GetType(), typeof(System.Reflection.Emit.AssemblyBuilder))) && !oAsm.IsDynamic)
                    {
                        co.ReferencedAssemblies.Add(oAsm.Location);
                    }

                }

                for (int iCounter = co.ReferencedAssemblies.Count - 1; iCounter >= 0; iCounter += -1)
                {
                    if (co.ReferencedAssemblies[iCounter].Trim() == string.Empty)
                    {
                        co.ReferencedAssemblies.RemoveAt(iCounter);
                    }
                }

                //co.ReferencedAssemblies.Add("E:\Development\Infinity\Infinity.UI\bin\Infinity.Componetns.CostCentreLoader.dll")
                //co.ReferencedAssemblies.Add("system.data.dll")
                //co.ReferencedAssemblies.Add("system.xml.dll")
                //co.ReferencedAssemblies.Add("system.drawing.dll")

                //co.ReferencedAssemblies.Add(sOutputPath + "ByteFX.Data.dll")
                //////////////////////////////////
                co.IncludeDebugInformation = false;
                //co.TreatWarningsAsErrors = True
                //co.CompilerOptions
                //co.WarningLevel = 1

                //// we want to genereate a DLL
                co.GenerateExecutable = false;

                //here we will have to retrieve the names of all vb files in binaries folder and pass them to compiler for compilation



                //source(0) = "costcentre.vb"
                //source(1) = "assemblyinfo.vb"
                //source(2) = "assemblyinfo.vb"

                //// Run the compiling process

                CompilerResults result = icc.CompileAssemblyFromSource(co, Source);


                //Dim result As CompilerResults = icc.CompileAssemblyFromSourceBatch(co, source)

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///     Get Max CostCentreID
        /// </summary>
        ///         ''' 
        public long GetMaxCostCentreID()
        {
            try
            {
                return _costCenterRepository.GetMaxCostCentreID();


            }
            catch (Exception ex)
            {
                throw new Exception("GetMaxCostCentreID", ex);
            }
        }
        /// <summary>
        ///     Load Pre Defined Cost Centre Template in DB
        /// </summary>
        ///         ''' 
        public CostCentreTemplate LoadCostCentreTemplate(int TemplateID)
        {
            try
            {

                return _costCenterRepository.LoadCostCentreTemplate(TemplateID);
            }
            catch (Exception ex)
            {
                throw new Exception("LoadCostCentreTemplate", ex);
            }
        }
        /// <summary>
        ///    A Datatable with only code attribute will return a complete code of one site
        /// </summary>
        ///         ''' 
        public List<CostCentre> GetCompleteCodeofAllCostCentres(long OrganisationId)
        {
            try
            {
                return _costCenterRepository.GetCompleteCodeofAllCostCentres(OrganisationId);
            }
            catch (Exception ex)
            {
                throw new Exception("GetCompleteCodeofAllCostCentres", ex);
            }
        }

        private string SaveCostCenterImage(CostCentre costcenter)
        {
            if (costcenter.ImageBytes != null)
            {
                string base64 = costcenter.ImageBytes.Substring(costcenter.ImageBytes.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);

                long costCenterId = costcenter.CostCentreId == 0 ? this.GetMaxCostCentreID() : costcenter.CostCentreId;
                string directoryPath = HttpContext.Current.Server.MapPath("~/MPC_Content/CostCentres/" + _costCenterRepository.OrganisationId + "/" + costCenterId);

                

                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string savePath = directoryPath + "\\thumbnail.png";
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                return savePath;
            }
            return costcenter.ThumbnailImageURL;
        }
        public IEnumerable<CostCentreVariable> GetVariableList()
        {
            return _costCentreVariableRepository.GetVariableList();
        }
        public CostCenterVariablesResponseModel GetCostCenterVariablesTree(int id)
        {
            return _costCenterRepository.GetCostCenterVariablesTree(id);
        }

        public double GetCostCenterExecutionResult(CostCenterExecutionRequest request)
        {
            double dblResult = 0;

            return dblResult;
        }

        public CostCenterExecutionResponse GetCostCenterPrompts(CostCenterExecutionRequest request)
        {
            object[] _CostCentreParamsArray = new object[12];
            double actualPrice = 0;
            if ((request.Action == "Update" && request.QuestionQueueItems != null) || request.Action != "Update")
            {
                AppDomain _AppDomain = null;

                try
                {

                    Organisation org = _organisationRepository.GetOrganizatiobByID();
                    string OrganizationName = org.OrganisationName.Replace(" ", "").Trim();
                    AppDomainSetup _AppDomainSetup = new AppDomainSetup();


                    object _oLocalObject;
                    ICostCentreLoader _oRemoteObject;                    

                    _AppDomainSetup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
                    _AppDomainSetup.PrivateBinPath = Path.GetDirectoryName((new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath);


                    _AppDomain = AppDomain.CreateDomain("CostCentresDomain", null, _AppDomainSetup);

                    List<CostCentreQueueItem> CostCentreQueue = new List<CostCentreQueueItem>();
                    
                    CostCentreLoaderFactory _CostCentreLaoderFactory = (CostCentreLoaderFactory)_AppDomain.CreateInstance("MPC.Interfaces", "MPC.Interfaces.WebStoreServices.CostCentreLoaderFactory").Unwrap();
                    _CostCentreLaoderFactory.InitializeLifetimeService();

                    if (request.Action == "New")
                    {
                        if (request.QuestionQueueItems != null)
                        {
                            _CostCentreParamsArray[1] = CostCentreExecutionMode.ExecuteMode;
                            _CostCentreParamsArray[2] = request.QuestionQueueItems;
                        }
                        else
                        {
                            _CostCentreParamsArray[1] = CostCentreExecutionMode.PromptMode;
                            _CostCentreParamsArray[2] = new List<QuestionQueueItem>();
                        }
                    }

                    if (request.Action == "addAnother")
                    {

                        _CostCentreParamsArray[1] = CostCentreExecutionMode.PromptMode;
                        _CostCentreParamsArray[2] = request.QuestionQueueItems;

                    }

                    if (request.Action == "Update")
                    {
                        if (request.QuestionQueueItems != null)
                        {
                            _CostCentreParamsArray[1] = CostCentreExecutionMode.ExecuteMode;
                            _CostCentreParamsArray[2] = request.QuestionQueueItems;
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

                    if (request.Quantity <= 0)
                    {
                        // get first item section
                        _CostCentreParamsArray[8] = _itemRepository.GetItemFirstSectionByItemId(request.ItemId);
                    }
                    else
                    {
                        // update quantity in item section and return
                        _CostCentreParamsArray[8] = _itemRepository.UpdateItemFirstSectionByItemId(request.ItemId, request.Quantity);
                        //first update item section quatity
                        //persist queue
                        // run multiple cost centre
                        // after calculating cost centre 

                    }


                    _CostCentreParamsArray[9] = 1;


                    CostCentre oCostCentre = _costCenterRepository.GetCostCentreByID(request.CostCenterId);



                    CostCentreQueue.Add(new CostCentreQueueItem(oCostCentre.CostCentreId, oCostCentre.Name, 1, oCostCentre.CodeFileName, null, oCostCentre.SetupSpoilage, oCostCentre.RunningSpoilage));



                    _oLocalObject = _CostCentreLaoderFactory.Create(HttpContext.Current.Server.MapPath("/") + "\\ccAssembly\\" + OrganizationName + "UserCostCentres.dll", "UserCostCentres." + oCostCentre.CodeFileName, null);
                    _oRemoteObject = (ICostCentreLoader)_oLocalObject;

                    CostCentreCostResult oResult = null;

                    if (request.Action == "Modify")
                    {
                        _CostCentreParamsArray[1] = CostCentreExecutionMode.PromptMode;
                        _CostCentreParamsArray[2] = request.QuestionQueueItems.Where(c => c.CostCentreID == oCostCentre.CostCentreId).ToList();
                    }
                    else
                    {
                        if (request.Action == "Update") // dummy condition
                        {
                           // return Request.CreateResponse(HttpStatusCode.OK, 131);
                        }
                        oResult = _oRemoteObject.returnCost(ref _CostCentreParamsArray);

                    }

                    if ((request.QuestionQueueItems != null && request.Action != "Modify" && request.Action != "addAnother" && oResult != null) || (request.QuestionQueueItems != null && request.Action == "Update"))
                    {

                        actualPrice = oResult.TotalCost;

                        if (actualPrice < oCostCentre.MinimumCost && oCostCentre.MinimumCost != 0)
                        {
                            actualPrice = oCostCentre.MinimumCost ?? 0;
                        }
                    }
                    else
                    {

                       // return Request.CreateResponse(HttpStatusCode.OK, _CostCentreParamsArray);
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
            
            return new CostCenterExecutionResponse 
            { 
                CostCentreParamsArray = _CostCentreParamsArray ,
                dblReturnCost = actualPrice
            };
        }

        public string specialCharactersEncoderCostCentre(string value)
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



        public bool ReCompileAllCostCentres(long OrganisationId)
        {

            Organisation org = _organisationRepository.GetOrganizatiobByOrganisationID(OrganisationId);
            string OrganisationName = specialCharactersEncoderCostCentre(org.OrganisationName);

            CostCentreTemplate oTemplate = _costCenterRepository.LoadCostCentreTemplate(2);

            StringBuilder sSuperCode = new StringBuilder();
            StringBuilder sCode = new StringBuilder();

            foreach (var costcenter in _costCenterRepository.GetAllCostCentresForRecompiling(OrganisationId))
            {

                long _CostCentreID = costcenter.CostCentreId;
                //creating a costcentre code file and updating it and compile it.

                string Header, Footer, Middle;
                double SetupCost = costcenter.SetupCost ?? 0;
                int SetupTime = costcenter.SetupTime ?? 0;
                double MinCost = costcenter.MinimumCost ?? 0;
                double DefaultProfitMargin = costcenter.DefaultVA ?? 0;
                string sCostPlant = string.Empty;
                string sCostLabour = string.Empty;
                string sCostStock = string.Empty;
                string sTime = string.Empty;
                string sPricePlant = string.Empty;
                string sPriceLabour = string.Empty;
                string sPriceStock = string.Empty;
                string sActualPlantCost = "";
                string sActualStockCost = "";
                string sActualLabourCost = "";
              
                if (costcenter.Type > 0)
                {
                    CostCentreType otype = _costcentreTypeRepository.GetCostCenterTypeById(costcenter.Type);
                    costcenter.TypeName = otype.TypeName;
                }
                if (costcenter.CalculationMethodType == (int)CalculationMethods.Fixed)
                {
                    sCostPlant = " {cinput,id=\"1\",question=\"" + costcenter.CostQuestionString + "\",type=\"0\",InputType=\"0\",value=\"" + costcenter.CostDefaultValue + "\"} ";
                    sPricePlant = " {cinput,id=\"2\",question=\"" + costcenter.PriceQuestionString + "\",type=\"0\",InputType=\"0\",value=\"" + costcenter.PriceDefaultValue + "\"} ";
                    sTime = " {cinput,id=\"3\",question=\"" + costcenter.EstimatedTimeQuestionString + "\",type=\"0\",InputType=\"0\",value=\"" + costcenter.EstimatedTimeDefaultValue + "\"} ";
                    sCostPlant = TokenParse(sCostPlant);
                    sPricePlant = TokenParse(sPricePlant);
                    sTime = TokenParse(sTime);
                }
                else if (costcenter.CalculationMethodType == (int)CalculationMethods.QuantityBase)
                {
                    string sCodeString = string.Empty;
                    if (costcenter.QuantitySourceType == 1)
                    {
                        var varName = _costCentreVariableRepository.LoadVariable(costcenter.QuantityVariableId ?? 0);
                        sCodeString = "Dim vQuantity as Integer = " + "{SystemVariable, ID=\"" + Convert.ToString(costcenter.QuantityVariableId) + "\",Name=\"" + (varName != null ? varName.Name : "") + "\"}" + Environment.NewLine;
                        sCodeString += "EstimatedPlantCost =  {cinput,id=\"1\",question=\"Setup Cost\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.SetupCost) + "\"} " + "  + (" + "{cinput,id=\"2\",question=\"Cost Per Unit Quantity\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.CostPerUnitQuantity) + "\"} * vQuantity )";
                        sCostPlant += sCodeString;

                        sCodeString = "Dim vQuantity as Integer = " + "{SystemVariable, ID=\"" + Convert.ToString(costcenter.QuantityVariableId) + "\",Name=\"" + (varName != null ? varName.Name : "") + "\"}" + Environment.NewLine;
                        sCodeString += "QuotedPlantPrice =  " + "{cinput,id=\"1\",question=\"Setup Cost\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.SetupCost) + "\"} " + "  + (" + Convert.ToString(costcenter.PricePerUnitQuantity) + " * vQuantity )";
                        sPricePlant += sCodeString;

                        sCodeString = "EstimatedTime =  " + "{cinput,id=\"5\",question=\"Setup Time\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.SetupTime) + "\"} " + " + ( " + "{cinput,id=\"6\",question=\"Time per Unit Quantity\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.TimePerUnitQuantity) + "\"} * vQuantity )";
                        sTime += sCodeString;
                    }
                    else if (costcenter.QuantitySourceType == 2)
                    {
                        sCodeString = "Dim vQuantity as Integer =  {cinput,id=\"1\",question=\"" + costcenter.QuantityQuestionString + "\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.QuantityQuestionDefaultValue) + "\"} " + Environment.NewLine;
                        sCodeString += "EstimatedPlantCost =  " + "{cinput,id=\"2\",question=\"Setup Cost\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.SetupCost) + "\"} " + "  + (" + "{cinput,id=\"3\",question=\"Cost Per Unit Quantity\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.CostPerUnitQuantity) + "\"} * vQuantity ) ";
                        sCostPlant += sCodeString;

                        sCodeString = "Dim vQuantity as Integer =  {cinput,id=\"1\",question=\"" + costcenter.QuantityQuestionString + "\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.QuantityQuestionDefaultValue) + "\"} " + Environment.NewLine;
                        sCodeString += "QuotedPlantPrice =  " + "{cinput,id=\"2\",question=\"Setup Cost\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.SetupCost) + "\"} " + "  + (" +  Convert.ToString(costcenter.PricePerUnitQuantity) + " * vQuantity ) ";
                        sPricePlant += sCodeString;

                        sCodeString = "EstimatedTime =  " + "{cinput,id=\"6\",question=\"Setup Time\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.SetupTime) + "\"} " + " + ( " + "{cinput,id=\"7\",question=\"Time per Unit Quantity\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.TimePerUnitQuantity) + "\"} * vQuantity ) ";
                        sTime += sCodeString;
                    }
                }
                else if (costcenter.CalculationMethodType == (int)CalculationMethods.PerHour)
                {
                    string sCodeString = string.Empty;
                    if (costcenter.TimeSourceType == 1)
                    {

                        var varName = _costCentreVariableRepository.LoadVariable(costcenter.TimeVariableId ?? 0);
                        sCodeString = "Dim vNoOfHours as Integer = " + "{SystemVariable, ID=\"" + Convert.ToString(costcenter.TimeVariableId) + "\",Name=\"" + (varName != null ? varName.Name : "") + "\"}" + Environment.NewLine;
                        sCodeString += "EstimatedPlantCost = 0    ";
                        sCostPlant += sCodeString;

                        sCodeString = "Dim vNoOfHours as Integer = " + "{SystemVariable, ID=\"" + Convert.ToString(costcenter.TimeVariableId) + "\",Name=\"" + (varName != null ? varName.Name : "") + "\"}" + Environment.NewLine;
                        sCodeString += "QuotedPlantPrice = SetupCost +  (vNoOfHours * " + Convert.ToString(costcenter.PerHourPrice == null ? "0" : costcenter.PerHourPrice.ToString()) + " )  ";
                        sPricePlant += sCodeString;

                        sCodeString = "EstimatedTime = (vNoOfHours + " + "{cinput,id=\"1\",question=\"Setup Time\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.SetupTime) + "\"} " + " ) ";
                        sTime += sCodeString;
                    }
                    else if (costcenter.TimeSourceType == 2)
                    {
                        sCodeString = "Dim vNoOfHours as Integer =  {cinput,id=\"6\",question=\"" + costcenter.TimeQuestionString + "\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.SetupTime) + "\"} " + Environment.NewLine;
                        sCodeString += "EstimatedPlantCost = 0 ";
                        sCostPlant += sCodeString;

                        sCodeString = "Dim vNoOfHours as Integer =  {cinput,id=\"6\",question=\"" + costcenter.TimeQuestionString + "\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.TimeQuestionDefaultValue) + "\"} " + Environment.NewLine;
                        sCodeString += "QuotedPlantPrice = SetupCost +  (vNoOfHours * " + Convert.ToString(costcenter.PerHourPrice == null ? "0" : costcenter.PerHourPrice.ToString()) + " )  ";
                        sPricePlant += sCodeString;

                        sCodeString = "EstimatedTime = (vNoOfHours + " + "{cinput,id=\"1\",question=\"Setup Time\",type=\"0\",InputType=\"0\",value=\"" + Convert.ToString(costcenter.SetupTime) + "\"} " + " ) ";
                        sTime += sCodeString;
                    }
                }
                else if (costcenter.CalculationMethodType == (int)CalculationMethods.FormulaBase)
                {

                    // Set Cost Strings
                    if (string.IsNullOrEmpty(costcenter.strCostPlantUnParsed))
                        sCostPlant = "EstimatedPlantCost = 0";
                    else
                        sCostPlant = costcenter.strCostPlantUnParsed;
                    if (string.IsNullOrEmpty(costcenter.strCostLabourUnParsed))
                        sCostLabour = "EstimatedLabourCost = 0";
                    else
                        sCostLabour = costcenter.strCostLabourUnParsed;
                    if (string.IsNullOrEmpty(costcenter.strCostMaterialUnParsed))
                        sCostStock = "EstimatedMaterialCost = 0";
                    else
                        sCostStock = costcenter.strCostMaterialUnParsed;
                    if (string.IsNullOrEmpty(costcenter.strTimeUnParsed))
                        sTime = "EstimatedTime = 0";
                    else
                        sTime = costcenter.strTimeUnParsed;

                    // Set Price Strings
                    if (string.IsNullOrEmpty(costcenter.strPricePlantUnParsed))
                        sPricePlant = "QuotedPlantPrice = 0";
                    else
                        sPricePlant = costcenter.strPricePlantUnParsed;
                    if (string.IsNullOrEmpty(costcenter.strPriceLabourUnParsed))
                        sPriceLabour = "QuotedLabourPrice = 0";
                    else
                        sPriceLabour = costcenter.strPriceLabourUnParsed;
                    if (string.IsNullOrEmpty(costcenter.strPriceMaterialUnParsed))
                        sPriceStock = "QuotedMaterialPrice = 0";
                    else
                        sPriceStock = costcenter.strPriceMaterialUnParsed;

                    //sCostPlant = TokenParse("EstimatedPlantCost = {SystemVariable, ID=\"1\",Name=\"Number of unique Inks used on Side 1\"} * {question, ID=\"13\",caption=\"How many boxes\"} * {matrix, ID=\"19\",Name=\"Super Formula Matrix\"} * {question, ID=\"34\",caption=\"How many sections to fold?\"} * {question, ID=\"51\",caption=\"Multiple Options\"} ");


                }

                sCostPlant = TokenParse(sCostPlant);
                sCostLabour = TokenParse(sCostLabour);
                sCostStock = TokenParse(sCostStock);
                sTime = TokenParse(sTime);

                sPricePlant = TokenParse(sPricePlant);
                sPriceLabour = TokenParse(sPriceLabour);
                sPriceStock = TokenParse(sPriceStock);
                costcenter.IsParsed = true;

                {
                    char spacechar = '\0';
                    spacechar = (char)95;
                    //replacing any unwanted characters in the costcentrename and create file name

                    Header = oTemplate.Header.Substring(0, oTemplate.Header.IndexOf("''<cost>") - 2);
                    Middle = oTemplate.Middle.Substring(0, oTemplate.Middle.IndexOf("''<price>") - 2);
                    Footer = oTemplate.Footer;

                    //'getting new Cost centreID from Database



                    //'replacing the CostCentre name and ID in the header string
                    Header = Header.Replace("ccname", "CLS_" + _CostCentreID.ToString());
                    Header = Header.Replace("<ccid>", _CostCentreID.ToString());

                    //replacing the attribs of cost centre
                    Header = Header.Replace("<ccsc>", SetupCost.ToString());
                    Header = Header.Replace("<ccst>", SetupTime.ToString());
                    Header = Header.Replace("<ccmc>", MinCost.ToString());
                    Header = Header.Replace("<ccva>", DefaultProfitMargin.ToString());

                    //'now making a code file string

                    sCode.Clear();
                    sCode.Append(Header);
                    sCode.Append(Environment.NewLine + "''<cost>" + Environment.NewLine);
                    sCode.Append(Environment.NewLine + "''<plant>" + Environment.NewLine);
                    sCode.Append(sCostPlant);
                    sCode.Append(Environment.NewLine + "''</plant>" + Environment.NewLine);
                    sCode.Append(Environment.NewLine + "''<labour>" + Environment.NewLine);
                    sCode.Append(sCostLabour);
                    sCode.Append(Environment.NewLine + "''</labour>" + Environment.NewLine);
                    sCode.Append(Environment.NewLine + "''<material>" + Environment.NewLine);
                    sCode.Append(sCostStock);
                    sCode.Append(Environment.NewLine + "''</material>" + Environment.NewLine);
                    sCode.Append(Environment.NewLine + "''<time>" + Environment.NewLine);
                    sCode.Append(sTime);
                    sCode.Append(Environment.NewLine + "''</time>" + Environment.NewLine);

                    sCode.Append(Environment.NewLine + "''</cost>" + Environment.NewLine);

                    sCode.Append(Middle);

                    sCode.Append(Environment.NewLine + "''<price>" + Environment.NewLine);
                    sCode.Append(Environment.NewLine + "''<plant>" + Environment.NewLine);
                    sCode.Append(sPricePlant);
                    sCode.Append(Environment.NewLine + "''</plant>" + Environment.NewLine);
                    sCode.Append(Environment.NewLine + "''<labour>" + Environment.NewLine);
                    sCode.Append(sPriceLabour);
                    sCode.Append(Environment.NewLine + "''</labour>" + Environment.NewLine);
                    sCode.Append(Environment.NewLine + "''<material>" + Environment.NewLine);
                    sCode.Append(sPriceStock);
                    sCode.Append(Environment.NewLine + "''</material>" + Environment.NewLine);
                    sCode.Append(Environment.NewLine + "''</price>" + Environment.NewLine);


                    //process the footer here..
                    //since footer also contains the ActualCOST area, we have to remove that AREA
                    //and replace it with the new ACTUAL COST code

                    //our code is between ''<actualcost>   and  ''</actualcost>
                    int iStart = 0;
                    int iStart2 = 0;
                    int iLength = 0;
                    string sActualCostString = null;

                    sActualCostString = "''<plant>" + sActualPlantCost + "''</plant>" + "''</material>" + sActualStockCost + "''</material>" + "''</labour>" + sActualLabourCost + "''</labour>";

                    iStart = Footer.IndexOf("''<actualcost>") + 14;
                    iStart2 = Footer.IndexOf("''</actualcost>");
                    Footer.Remove(iStart, iStart2 - iStart);
                    Footer.Insert(iStart, sActualCostString);

                    sCode.Append(Footer);
                    sSuperCode.Append(sCode);

                }
                costcenter.CodeFileName = "CLS_" + _CostCentreID.ToString();
                costcenter.CompleteCode = sCode.ToString();
                _costCenterRepository.UpdateCostCentre(costcenter);

            }

            string oFullSource = sSuperCode.ToString();

            oFullSource = oFullSource.Replace("Namespace UserCostCentres", "");

            oFullSource = oFullSource.Replace("End Namespace", "");

                string oSource = "";

                //Get Complete Code of the CostCentre from the DB and Recompile it with the new changes
                //List<CostCentre> oAllCostCentresCode = GetCompleteCodeofAllCostCentres(OrganisationId);


                //oSource += "Imports Infinity" + Environment.NewLine;
                oSource += "Imports System" + Environment.NewLine;
                oSource += "Imports System.Data" + Environment.NewLine;
                oSource += "Imports Microsoft.VisualBasic" + Environment.NewLine;
                //oSource += System.Configuration.ConfigurationSettings.AppSettings("DALProviderNameSpace") + Environment.NewLine;
                oSource += "Imports MPC.Implementation.MISServices" + Environment.NewLine;
                oSource += "Imports MPC.Implementation.WebStoreServices" + Environment.NewLine;
                oSource += "imports MPC.Models.DomainModels" + Environment.NewLine;
                oSource += "imports MPC.Models.Common" + Environment.NewLine;
                oSource += "Imports System.Reflection" + Environment.NewLine;
                oSource += "Imports Microsoft.Practices.Unity" + Environment.NewLine;
                oSource += "Imports ICostCentreService = MPC.Interfaces.WebStoreServices.ICostCentreService" + Environment.NewLine;
                oSource += "Namespace UserCostCentres" + Environment.NewLine;


                oSource += oFullSource;


                oSource += "End Namespace" + Environment.NewLine;

                //'if compilation fails then delete the code file

                System.IO.FileStream oFileStream; ;
                //string oCompanyName = OrganisationName;

                //replacing any unwanted characters in the CompanyNames and create file name

                bool IsCompiled = true;

                try
                {
                    //Compile Code of CostCentres
                    CompileBinaries(HttpContext.Current.Server.MapPath("/") + "\\ccAssembly\\", oSource, OrganisationName);

                    //Get CostCentre File Open it in Read Mode
                    //oFileStream = System.IO.File.OpenRead(HttpContext.Current.Server.MapPath("/") + "\\ccAssembly\\" + OrganisationName + "UserCostCentres.dll");

                    //Get Byte Array of the file and write it in the db
                    //byte[] CostCentreByte = new byte[Convert.ToInt32(oFileStream.Length - 1) + 1];

                    //oFileStream.Read(CostCentreByte, 0, Convert.ToInt32(oFileStream.Length - 1));

                    //CostCentreByte = null;
                }
                catch (Exception ex)
                {
                    //    BLL.CostCentres.CostCentre.DeleteCodeFile(sCostCentreFileName, Application.StartupPath.ToString + "\binaries\")
                    IsCompiled = false;
                    //throw new Exception("Error Compiling Costcentre", ex.ToString());
                    //ex.HelpLink = oSource;
                    throw ex;

                }
                finally
                {

                    oFileStream = null;

                }

                return true;


        }

        public void DeleteCostCentre(long CostCentreId)
        {
            _costCenterRepository.DeleteCostCentre(CostCentreId);
        }

        public bool DeleteClickChargeZone(long zoneId)
        {
            bool isDeleted = false;
            try
            {
                MachineClickChargeZone clickChargeZone = _machineClickChargeZoneRepository.Find(zoneId);
                if (clickChargeZone != null)
                {
                    _machineClickChargeZoneRepository.Delete(clickChargeZone);
                    _machineClickChargeZoneRepository.SaveChanges();
                    isDeleted = true;
                }
            }
            catch (Exception)
            {
                throw new MPCException("Failed to delete click charge zone", _costCenterRepository.OrganisationId);
            }
            return isDeleted;
        }

        public MachineClickChargeZone SaveClickChargeZone(MachineClickChargeZone zone)
        {
            MachineClickChargeZone clickChargeZone = zone.Id > 0
                ? _machineClickChargeZoneRepository.Find(zone.Id)
                : new MachineClickChargeZone();
            clickChargeZone = UpdateClickChargeZone(zone, clickChargeZone);
            if(clickChargeZone.Id == 0)
                _machineClickChargeZoneRepository.Add(clickChargeZone);
            _machineClickChargeZoneRepository.SaveChanges();
            return clickChargeZone;
        }

        private MachineClickChargeZone UpdateClickChargeZone(MachineClickChargeZone source, MachineClickChargeZone target)
        {
            target.ZoneName = source.ZoneName;
            target.From1 = source.From1;
            target.From2 = source.From2;
            target.From3 = source.From3;
            target.From4 = source.From4;
            target.From5 = source.From5;
            target.From6 = source.From6;
            target.From7 = source.From7;
            target.From8 = source.From8;
            target.From9 = source.From9;
            target.From10 = source.From10;
            target.From11 = source.From11;
            target.From12 = source.From12;
            target.From13 = source.From13;
            target.From14 = source.From14;
            target.From15 = source.From15;
            target.To1 = source.To1;
            target.To2 = source.To2;
            target.To3 = source.To3;
            target.To4 = source.To4;
            target.To5 = source.To5;
            target.To6 = source.To6;
            target.To7 = source.To7;
            target.To8 = source.To8;
            target.To9 = source.To9;
            target.To10 = source.To10;
            target.To11 = source.To11;
            target.To12 = source.To12;
            target.To13 = source.To13;
            target.To14 = source.To14;
            target.To15 = source.To15;
            target.Sheets1 = source.Sheets1;
            target.Sheets2 = source.Sheets2;
            target.Sheets3 = source.Sheets3;
            target.Sheets4 = source.Sheets4;
            target.Sheets5 = source.Sheets5;
            target.Sheets6 = source.Sheets6;
            target.Sheets7 = source.Sheets7;
            target.Sheets8 = source.Sheets8;
            target.Sheets9 = source.Sheets9;
            target.Sheets10 = source.Sheets10;
            target.Sheets11 = source.Sheets11;
            target.Sheets12 = source.Sheets12;
            target.Sheets13 = source.Sheets13;
            target.Sheets14 = source.Sheets14;
            target.Sheets15 = source.Sheets15;
            target.SheetCost1 = source.SheetCost1;
            target.SheetCost2 = source.SheetCost2;
            target.SheetCost3 = source.SheetCost3;
            target.SheetCost4 = source.SheetCost4;
            target.SheetCost5 = source.SheetCost5;
            target.SheetCost6 = source.SheetCost6;
            target.SheetCost7 = source.SheetCost7;
            target.SheetCost8 = source.SheetCost8;
            target.SheetCost9 = source.SheetCost9;
            target.SheetCost10 = source.SheetCost10;
            target.SheetCost11 = source.SheetCost11;
            target.SheetCost12 = source.SheetCost12;
            target.SheetCost13 = source.SheetCost13;
            target.SheetCost14 = source.SheetCost14;
            target.SheetCost15 = source.SheetCost15;
            target.SheetPrice1 = source.SheetPrice1;
            target.SheetPrice2 = source.SheetPrice2;
            target.SheetPrice3 = source.SheetPrice3;
            target.SheetPrice4 = source.SheetPrice4;
            target.SheetPrice5 = source.SheetPrice5;
            target.SheetPrice6 = source.SheetPrice6;
            target.SheetPrice7 = source.SheetPrice7;
            target.SheetPrice8 = source.SheetPrice8;
            target.SheetPrice9 = source.SheetPrice9;
            target.SheetPrice10 = source.SheetPrice10;
            target.SheetPrice11 = source.SheetPrice11;
            target.SheetPrice12 = source.SheetPrice12;
            target.SheetPrice13 = source.SheetPrice13;
            target.SheetPrice14 = source.SheetPrice14;
            target.SheetPrice15 = source.SheetPrice15;
            target.IsCostCenterZone = true;
            target.OrganisationId = _costCenterRepository.OrganisationId;
            

            return target;
        }
        #endregion

    }
}




    