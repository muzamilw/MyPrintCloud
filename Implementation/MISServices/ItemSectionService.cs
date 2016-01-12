using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using Ionic.Zip;
using System.Text;
using System.Xml;
using MPC.Models.ResponseModels;
using MPC.Models.RequestModels;
using lengthunit = MPC.Models.Common.LengthUnit;
using MPC.ExceptionHandling;

namespace MPC.Implementation.MISServices
{
    public class ItemSectionService : IItemSectionService
    {

        #region Private
        private readonly IOrganisationRepository organisationRepository;
        private readonly IItemSectionRepository itemsectionRepository;
        private readonly IMachineRepository machineRepository;
        #endregion

        #region Constructor
        public ItemSectionService(IOrganisationRepository _organisationRepository, IItemSectionRepository _itemsectionRepository, IMachineRepository _machineRepository)
        {
            if (_organisationRepository == null)
            {
                throw new ArgumentNullException("organisationRepository");
            }
            if (_itemsectionRepository == null)
            {
                throw new ArgumentNullException("itemsectionRepository");
            }
            if (_machineRepository == null)
            {
                throw new ArgumentNullException("machineRepository");
            }
            this.organisationRepository = _organisationRepository;
            this.itemsectionRepository = _itemsectionRepository;
            this.machineRepository = _machineRepository;
        }
        #endregion
        #region Print View Plan Code

        public PtvDTO GetPTV(PTVRequestModel request)
        {
            Organisation organisation = organisationRepository.GetOrganizatiobByID();

            if (organisation != null)
            {
                request.ItemHeight = LengthConversionHelper.ConvertLength(request.ItemHeight, MPC.Models.Common.LengthUnit.Mm, organisation.LengthUnit);
                request.ItemWidth = LengthConversionHelper.ConvertLength(request.ItemWidth, MPC.Models.Common.LengthUnit.Mm, organisation.LengthUnit);
                request.PrintHeight = LengthConversionHelper.ConvertLength(request.PrintHeight, MPC.Models.Common.LengthUnit.Mm, organisation.LengthUnit);
                request.PrintWidth = LengthConversionHelper.ConvertLength(request.PrintWidth, MPC.Models.Common.LengthUnit.Mm, organisation.LengthUnit);
                request.ItemHorizentalGutter = LengthConversionHelper.ConvertLength(request.ItemHorizentalGutter, MPC.Models.Common.LengthUnit.Mm, organisation.LengthUnit);
                request.ItemVerticalGutter = LengthConversionHelper.ConvertLength(request.ItemVerticalGutter, MPC.Models.Common.LengthUnit.Mm, organisation.LengthUnit);
            }


            return DrawPTV((PrintViewOrientation)request.Orientation, request.ReversePtvRows, request.ReversePtvCols, request.isDoubleSided, request.isWorknTrun, request.isWorknTumble, request.ApplyPressRestrict, request.ItemHeight, request.ItemWidth, request.PrintHeight, request.PrintWidth, (GripSide)request.Grip, request.GripDepth, request.HeadDepth, request.PrintGutter, request.ItemHorizentalGutter, request.ItemVerticalGutter);
        }



        public PtvDTO GetPTVCalculation(PTVRequestModel request)
        {
            Organisation organisation = organisationRepository.GetOrganizatiobByID();

            if (organisation != null)
            {
                request.ItemHeight = LengthConversionHelper.ConvertLength(request.ItemHeight, MPC.Models.Common.LengthUnit.Mm, organisation.LengthUnit);
                request.ItemWidth = LengthConversionHelper.ConvertLength(request.ItemWidth, MPC.Models.Common.LengthUnit.Mm, organisation.LengthUnit);
                request.PrintHeight = LengthConversionHelper.ConvertLength(request.PrintHeight, MPC.Models.Common.LengthUnit.Mm, organisation.LengthUnit);
                request.PrintWidth = LengthConversionHelper.ConvertLength(request.PrintWidth, MPC.Models.Common.LengthUnit.Mm, organisation.LengthUnit);
                request.ItemHorizentalGutter = LengthConversionHelper.ConvertLength(request.ItemHorizentalGutter, MPC.Models.Common.LengthUnit.Mm, organisation.LengthUnit);
                request.ItemVerticalGutter = LengthConversionHelper.ConvertLength(request.ItemVerticalGutter, MPC.Models.Common.LengthUnit.Mm, organisation.LengthUnit);
            }
            return CalculatePTV(request.ReversePtvRows, request.ReversePtvCols, request.isDoubleSided, false, request.ApplyPressRestrict, request.ItemHeight, request.ItemWidth, request.PrintHeight, request.PrintWidth, 0, request.Grip, request.GripDepth, request.HeadDepth, request.PrintGutter, request.ItemHorizentalGutter, request.ItemVerticalGutter, request.isWorknTrun, request.isWorknTumble);
        }

        #endregion

        #region Estimation Methods


        public ItemSection CalculatePressCostWithSides(ItemSection oItemSection, int PressID, bool IsReRun = false, bool IsWorkInstructionsLocked = false, int PressReRunMode = (int)PressReRunModes.NotReRun, int PressReRunQuantityIndex = 1, double OverrideValue = 0, bool isBestPress = false, bool isPressSide2 = false)
        {
            
            JobPreference oJobCardOptionsDTO = itemsectionRepository.GetJobPreferences(1);
            bool functionReturnValue = false;
            string sMinimumCost = null;
            double dblPressPass = 0;

            dblPressPass = isPressSide2 ? Convert.ToDouble(oItemSection.PassesSide2) : Convert.ToDouble(oItemSection.PassesSide1);
            
            //Get Machine Query
            Machine oPressDTO = itemsectionRepository.GetPressById(PressID);

            oItemSection.IsPlateUsed = false;
            oItemSection.IsWashup = false;
            oItemSection.IsMakeReadyUsed = false;

            int SheetPTV = 0;
            int SetupSpoilage = 0;
            double RunningSpoilagePercentage = 0;
            //Get Press Cost Center Query
            CostCentre oCostCentreDTO = itemsectionRepository.GetCostCenterBySystemType((int)SystemCostCenterTypes.Press);

            double PressCost1 = 0;
            double PressPrice1 = 0;
            double PressCost2 = 0;
            double PressPrice2 = 0;
            double PressCost3 = 0;
            double PressPrice3 = 0;
            double PressRunTime1 = 0;
            double PressRunTime2 = 0;
            double PressRunTime3 = 0;


            //'# of times the printsheets need to be passed thru the given press, based on the # of colours
            //'that are printed on the printsheets and the # of colours the press can print at a time, i.e.
            //'how many heads it has.

            try
            {
                //Checking whether the press can do perfecting or not

                double dblSide1Ink = Convert.ToDouble(oItemSection.Side1Inks);
                double dblSide2Ink = Convert.ToDouble(oItemSection.Side2Inks);
                double dblPressHeads = Convert.ToDouble(oPressDTO.ColourHeads);

               
                StockItem oPaperDTO = new StockItem();
                //Get Paper Stock Query
                oPaperDTO = itemsectionRepository.GetStockById(Convert.ToInt64(oItemSection.StockItemID1));// db.StockItems.Where(s => s.StockItemId == oItemSection.StockItemID1).FirstOrDefault();

                if (oItemSection.PrintViewLayout == (int)PrintViewOrientation.Landscape)
                {
                    SheetPTV = Convert.ToInt32(oItemSection.PrintViewLayoutLandScape);
                }
                else
                {
                    SheetPTV = Convert.ToInt32(oItemSection.PrintViewLayoutPortrait);
                }

                SetupSpoilage = Convert.ToInt32(oItemSection.SetupSpoilage);
                RunningSpoilagePercentage = Convert.ToDouble(oItemSection.RunningSpoilage);

                //This variable array is to hold the Working Sheet Qty For each multiple qty
                double[] intWorkSheetQty = new double[5];
                intWorkSheetQty[0] = Convert.ToInt32(oItemSection.Qty1 / SheetPTV) + SetupSpoilage + Convert.ToInt32(Convert.ToInt32(oItemSection.Qty1 / SheetPTV) * RunningSpoilagePercentage / 100);
                intWorkSheetQty[1] = Convert.ToInt32(oItemSection.Qty2 / SheetPTV) + SetupSpoilage + Convert.ToInt32(Convert.ToInt32(oItemSection.Qty2 / SheetPTV) * RunningSpoilagePercentage / 100);
                intWorkSheetQty[2] = Convert.ToInt32(oItemSection.Qty3 / SheetPTV) + SetupSpoilage + Convert.ToInt32(Convert.ToInt32(oItemSection.Qty3 / SheetPTV) * RunningSpoilagePercentage / 100);

                //Model.LookupMethods.MethodDTO oModelLookUpMethod = BLL.LookupMethods.Method.GetMachineLookUpMethod(GlobalData, oItemSection.SelectedPressCalculationMethodID);
                LookupMethod oModelLookUpMethod = new LookupMethod();
                oModelLookUpMethod = itemsectionRepository.GetLookupMethodById(Convert.ToInt64(oPressDTO.LookupMethodId));                

                double[] dblPrintCost = new double[3];
                double[] dblPrintPrice = new double[3];
                double[] dblPrintRun = new double[3];
                double[] dblPrintPlateQty = new double[3];
                int temp = 0;
                int temp2 = 0;
                if (PressReRunMode == (int)PressReRunModes.NotReRun)
                {
                    temp = 0;
                    temp2 = 2;
                }
                else if (PressReRunMode == (int)PressReRunModes.ReRunPress)
                {
                    temp = PressReRunQuantityIndex - 1;
                    temp2 = PressReRunQuantityIndex - 1;
                }

                switch (oModelLookUpMethod.Type)
                {
                    case (int)MethodTypes.SpeedWeight:
                        //Speed Weight
                        ///'=====================================================
                        int dblPrintChge = 0;
                        int dblPrintChgeRate = 0;
                        int[] intGSM = new int[4];
                        int[] intSheets = new int[6];
                        double[,] intSpeed = new double[4, 6];
                        double dblSpeed1 = 0;
                        double dblSpeed2 = 0;
                        double[] dblPrintSpeed = new double[5];
                        //Getting the Plant Lookup Information Query

                        MachineSpeedWeightLookup oModelSpeedWeight = itemsectionRepository.GetMachineSpeedWeightLookup(oModelLookUpMethod.MethodId);
                        //There are 3 GSM Setting their Values
                        intGSM[1] = Convert.ToInt32(oModelSpeedWeight.SheetWeight1);
                        intGSM[2] = Convert.ToInt32(oModelSpeedWeight.SheetWeight2);
                        intGSM[3] = Convert.ToInt32(oModelSpeedWeight.SheetWeight3);
                        //Setting the Sheet Values
                        intSheets[1] = Convert.ToInt32(oModelSpeedWeight.SheetsQty1);
                        intSheets[2] = Convert.ToInt32(oModelSpeedWeight.SheetsQty2);
                        intSheets[3] = Convert.ToInt32(oModelSpeedWeight.SheetsQty3);
                        intSheets[4] = Convert.ToInt32(oModelSpeedWeight.SheetsQty4);
                        intSheets[5] = Convert.ToInt32(oModelSpeedWeight.SheetsQty5);

                        //Setting up the Speed Values
                        intSpeed[1, 1] = Convert.ToDouble(oModelSpeedWeight.speedqty11);
                        intSpeed[1, 2] = Convert.ToDouble(oModelSpeedWeight.speedqty12);
                        intSpeed[1, 3] = Convert.ToDouble(oModelSpeedWeight.speedqty13);
                        intSpeed[1, 4] = Convert.ToDouble(oModelSpeedWeight.speedqty14);
                        intSpeed[1, 5] = Convert.ToDouble(oModelSpeedWeight.speedqty15);
                        intSpeed[2, 1] = Convert.ToDouble(oModelSpeedWeight.speedqty21);
                        intSpeed[2, 2] = Convert.ToDouble(oModelSpeedWeight.speedqty22);
                        intSpeed[2, 3] = Convert.ToDouble(oModelSpeedWeight.speedqty23);
                        intSpeed[2, 4] = Convert.ToDouble(oModelSpeedWeight.speedqty24);
                        intSpeed[2, 5] = Convert.ToDouble(oModelSpeedWeight.speedqty25);
                        intSpeed[3, 1] = Convert.ToDouble(oModelSpeedWeight.speedqty31);
                        intSpeed[3, 2] = Convert.ToDouble(oModelSpeedWeight.speedqty32);
                        intSpeed[3, 3] = Convert.ToDouble(oModelSpeedWeight.speedqty33);
                        intSpeed[3, 4] = Convert.ToDouble(oModelSpeedWeight.speedqty34);
                        intSpeed[3, 5] = Convert.ToDouble(oModelSpeedWeight.speedqty35);

                        //Checking that the press support this GSM or Not
                        if (oPaperDTO.ItemWeight > oPressDTO.maximumsheetweight)
                        {
                            // return functionReturnValue;
                            ///Please select a paper that can be supported by the selected press.
                        }

                        byte btGSM = 0;
                        byte btRun = 0;
                        double dblGSM = 0;
                        double dblRun = 0;
                        //'Get the GSM
                        //Checking for the PaperGSM
                        if (oPaperDTO.ItemWeight == intGSM[1])
                        {
                            btGSM = 1;
                            dblGSM = 0;
                        }
                        else if (oPaperDTO.ItemWeight == intGSM[2])
                        {
                            btGSM = 2;
                            dblGSM = 0;
                        }
                        else if (oPaperDTO.ItemWeight == intGSM[3])
                        {
                            btGSM = 3;
                            dblGSM = 0;
                        }
                        else
                        {
                            if (oPaperDTO.ItemWeight > intGSM[1] && oPaperDTO.ItemWeight < intGSM[2])
                            {
                                dblGSM = (double)(intGSM[2] - oPaperDTO.ItemWeight) / (intGSM[2] - intGSM[1]);
                                btGSM = 1;
                            }
                            else if (oPaperDTO.ItemWeight > intGSM[2] && oPaperDTO.ItemWeight < intGSM[3])
                            {
                                dblGSM = (double)(intGSM[3] - oPaperDTO.ItemWeight) / (intGSM[3] - intGSM[2]);
                                btGSM = 2;
                            }
                        }


                        //'Get the Sheets
                        for (int i = temp; i <= temp2; i++)
                        {
                            //Exact Values i.e. 2000 Sheets, 3000 Sheets etc.
                            //*****Difference between dblRun and btRun
                            if (intWorkSheetQty[i] <= intSheets[1])
                            {
                                dblRun = 0;
                                btRun = 1;
                            }
                            else if (intWorkSheetQty[i] == intSheets[2])
                            {
                                dblRun = 0;
                                btRun = 2;
                            }
                            else if (intWorkSheetQty[i] == intSheets[3])
                            {
                                dblRun = 0;
                                btRun = 3;
                            }
                            else if (intWorkSheetQty[i] == intSheets[4])
                            {
                                dblRun = 0;
                                btRun = 4;
                            }
                            else if (intWorkSheetQty[i] >= intSheets[5])
                            {
                                dblRun = 0;
                                btRun = 5;
                            }       //Now determining which run for Other than Exact values and checking in which range it lies for example 2300
                            //Running the Previous run for example for 2300, 2000 run would be used
                            else
                            {
                                if (intWorkSheetQty[i] < intSheets[2] & intWorkSheetQty[i] > intSheets[1])
                                {
                                    dblRun = (intSheets[2] - intWorkSheetQty[i]) / (intSheets[2] - intSheets[1]);
                                    btRun = 1;
                                }
                                else if (intWorkSheetQty[i] < intSheets[3] & intWorkSheetQty[i] > intSheets[2])
                                {
                                    dblRun = (intSheets[3] - intWorkSheetQty[i]) / (intSheets[3] - intSheets[2]);
                                    btRun = 2;
                                }
                                else if (intWorkSheetQty[i] < intSheets[4] & intWorkSheetQty[i] > intSheets[3])
                                {
                                    dblRun = (intSheets[4] - intWorkSheetQty[i]) / (intSheets[4] - intSheets[3]);
                                    btRun = 3;
                                }
                                else if (intWorkSheetQty[i] < intSheets[5] & intWorkSheetQty[i] > intSheets[4])
                                {
                                    dblRun = (intSheets[5] - intWorkSheetQty[i]) / (intSheets[5] - intSheets[4]);
                                    btRun = 4;
                                }
                            }


                            if (btRun == 5)
                            {
                                //Determining Which Speed for GSM1 and GSM2
                                dblSpeed1 = intSpeed[btGSM - 1, btRun] - ((intSpeed[btGSM - 1, btRun] - intSpeed[btGSM, btRun]) * dblGSM);
                                dblSpeed2 = intSpeed[btGSM, btRun] - ((intSpeed[btGSM, btRun] - intSpeed[btGSM, btRun]) * dblGSM);
                                //Other Than 5
                            }
                            else
                            {
                                dblSpeed1 = intSpeed[btGSM, btRun + 1] - ((intSpeed[btGSM, btRun + 1] - intSpeed[btGSM, btRun]) * dblGSM);
                                dblSpeed2 = intSpeed[btGSM, btRun] - ((intSpeed[btGSM - 1, btRun] - intSpeed[btGSM, btRun]) * dblGSM);
                            }
                            //Setting the Exact Speed

                            if (PressReRunMode == (int)PressReRunModes.CalculateValuesToShow)
                            {
                                dblPrintSpeed[i] = dblSpeed2 - ((dblSpeed2 - dblSpeed1) * dblRun);
                                if (PressReRunQuantityIndex - 1 == i)
                                {
                                    OverrideValue = dblPrintSpeed[i];
                                    //  return functionReturnValue;
                                }

                            }
                            else if (PressReRunMode == (int)PressReRunModes.NotReRun)
                            {
                                dblPrintSpeed[i] = dblSpeed2 - ((dblSpeed2 - dblSpeed1) * dblRun);
                            }
                            else if (PressReRunMode == (int)PressReRunModes.ReRunPress)
                            {
                                if (PressReRunQuantityIndex - 1 == i)
                                {
                                    dblPrintSpeed[i] = OverrideValue;
                                }
                            }


                            //Updating the Press Speed (Pro Rata)
                            if (PressReRunMode == (int)PressReRunModes.NotReRun)
                            {
                                oItemSection.PressSpeed1 = Convert.ToInt32(dblPrintSpeed[0]);
                                oItemSection.PressSpeed2 = Convert.ToInt32(dblPrintSpeed[1]);
                                oItemSection.PressSpeed3 = Convert.ToInt32(dblPrintSpeed[2]);

                                PressRunTime1 = Convert.ToDouble(intWorkSheetQty[0] / oItemSection.PressSpeed1);
                                PressRunTime2 = Convert.ToDouble(intWorkSheetQty[1] / oItemSection.PressSpeed2);
                                PressRunTime3 = Convert.ToDouble(intWorkSheetQty[2] / oItemSection.PressSpeed3);
                            }
                            else if (PressReRunMode == (int)PressReRunModes.ReRunPress)
                            {
                                if (PressReRunQuantityIndex == 1)
                                {
                                    oItemSection.PressSpeed1 = Convert.ToInt32(dblPrintSpeed[0]);
                                    PressRunTime1 = Convert.ToDouble(intWorkSheetQty[0] / oItemSection.PressSpeed1);
                                }
                                else if (PressReRunQuantityIndex == 2)
                                {
                                    oItemSection.PressSpeed2 = Convert.ToInt32(dblPrintSpeed[1]);
                                    PressRunTime2 = Convert.ToDouble(intWorkSheetQty[1] / oItemSection.PressSpeed2);
                                }
                                else if (PressReRunQuantityIndex == 3)
                                {
                                    oItemSection.PressSpeed3 = Convert.ToInt32(dblPrintSpeed[2]);
                                    PressRunTime3 = Convert.ToDouble(intWorkSheetQty[2] / oItemSection.PressSpeed3);
                                }

                            }

                            double dblCoverageMultiple = 1;
                            if (isPressSide2)
                            {
                                if (oItemSection.ImpressionCoverageSide2 == 1)
                                    dblCoverageMultiple = oPressDTO.CoverageHigh ?? 1;
                                else if (oItemSection.ImpressionCoverageSide2 == 2)
                                    dblCoverageMultiple = oPressDTO.CoverageMedium ?? 1;
                                else if (oItemSection.ImpressionCoverageSide2 == 3)
                                    dblCoverageMultiple = oPressDTO.CoverageLow ?? 1;
                            }
                            else
                            {
                                if (oItemSection.ImpressionCoverageSide1 == 1)
                                    dblCoverageMultiple = oPressDTO.CoverageHigh ?? 1;
                                else if (oItemSection.ImpressionCoverageSide1 == 2)
                                    dblCoverageMultiple = oPressDTO.CoverageMedium ?? 1;
                                else if (oItemSection.ImpressionCoverageSide1 == 3)
                                    dblCoverageMultiple = oPressDTO.CoverageLow ?? 1;
                            }

                            //Checing Whether Print is Double sided and Press can't perform perfecting
                            dblPrintCost[i] = Convert.ToDouble((dblPressPass * ((intWorkSheetQty[i] / dblPrintSpeed[i]) * oModelSpeedWeight.hourlyCost) * dblCoverageMultiple) + oPressDTO.SetupCharge);
                            dblPrintRun[i] = (dblPressPass * intWorkSheetQty[i]);
                            dblPrintPrice[i] = Convert.ToDouble((dblPressPass * ((intWorkSheetQty[i] / dblPrintSpeed[i]) * oModelSpeedWeight.hourlyPrice) * dblCoverageMultiple )+ oPressDTO.SetupCharge);
                        }


                        if (PressReRunMode == (int)PressReRunModes.NotReRun)
                        {
                            PressCost1 = dblPrintCost[0];
                            PressCost2 = dblPrintCost[1];
                            PressCost3 = dblPrintCost[2];
                            
                            PressPrice1 = dblPrintPrice[0];
                            PressPrice2 = dblPrintPrice[1];
                            PressPrice3 = dblPrintPrice[2];

                            oItemSection.PressHourlyCharge = oModelSpeedWeight.hourlyPrice;
                        }
                        else if (PressReRunMode == (int)PressReRunModes.ReRunPress)
                        {
                            if (PressReRunQuantityIndex == 1)
                            {
                                PressCost1 = dblPrintCost[0];
                                PressPrice1 = dblPrintPrice[0];
                            }
                            else if (PressReRunQuantityIndex == 2)
                            {
                                PressCost2 = dblPrintCost[1];
                                PressPrice2 = dblPrintPrice[1];
                            }
                            else if (PressReRunQuantityIndex == 3)
                            {
                                PressCost3 = dblPrintCost[2];
                                PressPrice3 = dblPrintPrice[2];
                            }
                        }

                        break;
                        ///==================================================
                    case (int)MethodTypes.ClickChargeZone:
                        //Click Charge Zone
                        ///==================================================
                        int[] intPrintChgeCZ = new int[3];
                        double[] dblCostCZ = new double[3];
                        double[] dblPriceCZ = new double[3];

                        int[] rngFrom = new int[15];
                        int[] rngTo = new int[15];
                        int[] dblClickSheet = new int[15];
                        double[] dblClickCost = new double[15];
                        double[] dblClickPrice = new double[15];

                        //Getting the Values for the LookUp Query

                        MachineClickChargeZone oModelClickChargeZone = itemsectionRepository.GetMachineClickChargeZone(oModelLookUpMethod.MethodId);
                        double TimePerChargeableSheets = Convert.ToDouble(oModelClickChargeZone.TimePerHour ?? 1);

                        //Setting the Range and Rate 
                        rngFrom[0] = Convert.ToInt32(oModelClickChargeZone.From1);
                        rngFrom[1] = Convert.ToInt32(oModelClickChargeZone.From2);
                        rngFrom[2] = Convert.ToInt32(oModelClickChargeZone.From3);
                        rngFrom[3] = Convert.ToInt32(oModelClickChargeZone.From4);
                        rngFrom[4] = Convert.ToInt32(oModelClickChargeZone.From5);
                        rngFrom[5] = Convert.ToInt32(oModelClickChargeZone.From6);
                        rngFrom[6] = Convert.ToInt32(oModelClickChargeZone.From7);
                        rngFrom[7] = Convert.ToInt32(oModelClickChargeZone.From8);
                        rngFrom[8] = Convert.ToInt32(oModelClickChargeZone.From9);
                        rngFrom[9] = Convert.ToInt32(oModelClickChargeZone.From10);
                        rngFrom[10] = Convert.ToInt32(oModelClickChargeZone.From11);
                        rngFrom[11] = Convert.ToInt32(oModelClickChargeZone.From12);
                        rngFrom[12] = Convert.ToInt32(oModelClickChargeZone.From13);
                        rngFrom[13] = Convert.ToInt32(oModelClickChargeZone.From14);
                        rngFrom[14] = Convert.ToInt32(oModelClickChargeZone.From15);
                        rngTo[0] = Convert.ToInt32(oModelClickChargeZone.To1);
                        rngTo[1] = Convert.ToInt32(oModelClickChargeZone.To2);
                        rngTo[2] = Convert.ToInt32(oModelClickChargeZone.To3);
                        rngTo[3] = Convert.ToInt32(oModelClickChargeZone.To4);
                        rngTo[4] = Convert.ToInt32(oModelClickChargeZone.To5);
                        rngTo[5] = Convert.ToInt32(oModelClickChargeZone.To6);
                        rngTo[6] = Convert.ToInt32(oModelClickChargeZone.To7);
                        rngTo[7] = Convert.ToInt32(oModelClickChargeZone.To8);
                        rngTo[8] = Convert.ToInt32(oModelClickChargeZone.To9);
                        rngTo[9] = Convert.ToInt32(oModelClickChargeZone.To10);
                        rngTo[10] = Convert.ToInt32(oModelClickChargeZone.To11);
                        rngTo[11] = Convert.ToInt32(oModelClickChargeZone.To12);
                        rngTo[12] = Convert.ToInt32(oModelClickChargeZone.To13);
                        rngTo[13] = Convert.ToInt32(oModelClickChargeZone.To14);
                        rngTo[14] = int.MaxValue;
                        dblClickCost[0] = Convert.ToDouble(oModelClickChargeZone.SheetCost1);
                        dblClickCost[1] = Convert.ToDouble(oModelClickChargeZone.SheetCost2);
                        dblClickCost[2] = Convert.ToDouble(oModelClickChargeZone.SheetCost3);
                        dblClickCost[3] = Convert.ToDouble(oModelClickChargeZone.SheetCost4);
                        dblClickCost[4] = Convert.ToDouble(oModelClickChargeZone.SheetCost5);
                        dblClickCost[5] = Convert.ToDouble(oModelClickChargeZone.SheetCost6);
                        dblClickCost[6] = Convert.ToDouble(oModelClickChargeZone.SheetCost7);
                        dblClickCost[7] = Convert.ToDouble(oModelClickChargeZone.SheetCost8);
                        dblClickCost[8] = Convert.ToDouble(oModelClickChargeZone.SheetCost9);
                        dblClickCost[9] = Convert.ToDouble(oModelClickChargeZone.SheetCost10);
                        dblClickCost[10] = Convert.ToDouble(oModelClickChargeZone.SheetCost11);
                        dblClickCost[11] = Convert.ToDouble(oModelClickChargeZone.SheetCost12);
                        dblClickCost[12] = Convert.ToDouble(oModelClickChargeZone.SheetCost13);
                        dblClickCost[13] = Convert.ToDouble(oModelClickChargeZone.SheetCost14);
                        dblClickCost[14] = Convert.ToDouble(oModelClickChargeZone.SheetCost15);
                        dblClickPrice[0] = Convert.ToDouble(oModelClickChargeZone.SheetPrice1);
                        dblClickPrice[1] = Convert.ToDouble(oModelClickChargeZone.SheetPrice2);
                        dblClickPrice[2] = Convert.ToDouble(oModelClickChargeZone.SheetPrice3);
                        dblClickPrice[3] = Convert.ToDouble(oModelClickChargeZone.SheetPrice4);
                        dblClickPrice[4] = Convert.ToDouble(oModelClickChargeZone.SheetPrice5);
                        dblClickPrice[5] = Convert.ToDouble(oModelClickChargeZone.SheetPrice6);
                        dblClickPrice[6] = Convert.ToDouble(oModelClickChargeZone.SheetPrice7);
                        dblClickPrice[7] = Convert.ToDouble(oModelClickChargeZone.SheetPrice8);
                        dblClickPrice[8] = Convert.ToDouble(oModelClickChargeZone.SheetPrice9);
                        dblClickPrice[9] = Convert.ToDouble(oModelClickChargeZone.SheetPrice10);
                        dblClickPrice[10] = Convert.ToDouble(oModelClickChargeZone.SheetPrice11);
                        dblClickPrice[11] = Convert.ToDouble(oModelClickChargeZone.SheetPrice12);
                        dblClickPrice[12] = Convert.ToDouble(oModelClickChargeZone.SheetPrice13);
                        dblClickPrice[13] = Convert.ToDouble(oModelClickChargeZone.SheetPrice14);
                        dblClickPrice[14] = Convert.ToDouble(oModelClickChargeZone.SheetPrice15);
                        dblClickSheet[0] = Convert.ToInt32(oModelClickChargeZone.Sheets1);
                        dblClickSheet[1] = Convert.ToInt32(oModelClickChargeZone.Sheets2);
                        dblClickSheet[2] = Convert.ToInt32(oModelClickChargeZone.Sheets3);
                        dblClickSheet[3] = Convert.ToInt32(oModelClickChargeZone.Sheets4);
                        dblClickSheet[4] = Convert.ToInt32(oModelClickChargeZone.Sheets5);
                        dblClickSheet[5] = Convert.ToInt32(oModelClickChargeZone.Sheets6);
                        dblClickSheet[6] = Convert.ToInt32(oModelClickChargeZone.Sheets7);
                        dblClickSheet[7] = Convert.ToInt32(oModelClickChargeZone.Sheets8);
                        dblClickSheet[8] = Convert.ToInt32(oModelClickChargeZone.Sheets9);
                        dblClickSheet[9] = Convert.ToInt32(oModelClickChargeZone.Sheets10);
                        dblClickSheet[10] = Convert.ToInt32(oModelClickChargeZone.Sheets11);
                        dblClickSheet[11] = Convert.ToInt32(oModelClickChargeZone.Sheets12);
                        dblClickSheet[12] = Convert.ToInt32(oModelClickChargeZone.Sheets13);
                        dblClickSheet[13] = Convert.ToInt32(oModelClickChargeZone.Sheets14);
                        dblClickSheet[14] = Convert.ToInt32(oModelClickChargeZone.Sheets15);
                        double[] intTEMPPrintQty = new double[5];
                        //Checking whether it is Accumulative Rate Across all parts

                        if (!(oModelClickChargeZone.isaccumulativecharge == 0))
                        {
                            //Setting the Original Print sheet Quantities

                            intTEMPPrintQty[0] = intWorkSheetQty[0];
                            intTEMPPrintQty[1] = intWorkSheetQty[1];
                            intTEMPPrintQty[2] = intWorkSheetQty[2];
                            intTEMPPrintQty[3] = intWorkSheetQty[3];
                            intTEMPPrintQty[4] = intWorkSheetQty[4];

                            //Setting the Total Print Sheet Quantity to AccumulateSheetQty Including All Spoilage and Current Section

                            intWorkSheetQty[0] = Convert.ToInt32(oItemSection.PrintSheetQty1);
                            intWorkSheetQty[1] = Convert.ToInt32(oItemSection.PrintSheetQty2);
                            intWorkSheetQty[2] = Convert.ToInt32(oItemSection.PrintSheetQty3);

                        }
                        //Looping through Multiple Quantities
                        for (int i = 0; i <= 2; i++)
                        {
                            //Checking Total Passes in the Press
                            //Setting the Print Charge and Print Charge Rate according to the LookUp
                            //*The LookUp show few other ranges
                            if ((intWorkSheetQty[i] * dblPressPass) >= rngFrom[0] && (intWorkSheetQty[i] * dblPressPass) <= rngTo[0])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[0];
                                dblCostCZ[i] = dblClickCost[0];
                                dblPriceCZ[i] = dblClickPrice[0];
                            }
                            else if ((intWorkSheetQty[i] * dblPressPass) >= rngFrom[1] && (intWorkSheetQty[i] * dblPressPass) <= rngTo[1])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[1];
                                dblCostCZ[i] = dblClickCost[1];
                                dblPriceCZ[i] = dblClickPrice[1];
                            }
                            else if ((intWorkSheetQty[i] * dblPressPass) >= rngFrom[2] && (intWorkSheetQty[i] * dblPressPass) <= rngTo[2])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[2];
                                dblCostCZ[i] = dblClickCost[2];
                                dblPriceCZ[i] = dblClickPrice[2];
                            }
                            else if ((intWorkSheetQty[i] * dblPressPass) >= rngFrom[3] && (intWorkSheetQty[i] * dblPressPass) <= rngTo[3])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[3];
                                dblCostCZ[i] = dblClickCost[3];
                                dblPriceCZ[i] = dblClickPrice[3];
                            }
                            else if ((intWorkSheetQty[i] * dblPressPass) >= rngFrom[4] && (intWorkSheetQty[i] * dblPressPass) <= rngTo[4])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[4];
                                dblCostCZ[i] = dblClickCost[4];
                                dblPriceCZ[i] = dblClickPrice[4];
                            }
                            else if ((intWorkSheetQty[i] * dblPressPass) >= rngFrom[5] && (intWorkSheetQty[i] * dblPressPass) <= rngTo[5])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[5];
                                dblCostCZ[i] = dblClickCost[5];
                                dblPriceCZ[i] = dblClickPrice[5];
                            }
                            else if ((intWorkSheetQty[i] * dblPressPass) >= rngFrom[6] && (intWorkSheetQty[i] * dblPressPass) <= rngTo[6])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[6];
                                dblCostCZ[i] = dblClickCost[6];
                                dblPriceCZ[i] = dblClickPrice[6];
                            }
                            else if ((intWorkSheetQty[i] * dblPressPass) >= rngFrom[7] && (intWorkSheetQty[i] * dblPressPass) <= rngTo[7])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[7];
                                dblCostCZ[i] = dblClickCost[7];
                                dblPriceCZ[i] = dblClickPrice[7];
                            }
                            else if ((intWorkSheetQty[i] * dblPressPass) >= rngFrom[8] && (intWorkSheetQty[i] * dblPressPass) <= rngTo[8])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[8];
                                dblCostCZ[i] = dblClickCost[8];
                                dblPriceCZ[i] = dblClickPrice[8];
                            }
                            else if ((intWorkSheetQty[i] * dblPressPass) >= rngFrom[9] && (intWorkSheetQty[i] * dblPressPass) <= rngTo[9])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[9];
                                dblCostCZ[i] = dblClickCost[9];
                                dblPriceCZ[i] = dblClickPrice[9];
                            }
                            else if ((intWorkSheetQty[i] * dblPressPass) >= rngFrom[10] && (intWorkSheetQty[i] * dblPressPass) <= rngTo[10])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[10];
                                dblCostCZ[i] = dblClickCost[10];
                                dblPriceCZ[i] = dblClickPrice[10];
                            }
                            else if ((intWorkSheetQty[i] * dblPressPass) >= rngFrom[11] && (intWorkSheetQty[i] * dblPressPass) <= rngTo[11])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[11];
                                dblCostCZ[i] = dblClickCost[11];
                                dblPriceCZ[i] = dblClickPrice[11];
                            }
                            else if ((intWorkSheetQty[i] * dblPressPass) >= rngFrom[12] && (intWorkSheetQty[i] * dblPressPass) <= rngTo[12])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[12];
                                dblCostCZ[i] = dblClickCost[12];
                                dblPriceCZ[i] = dblClickPrice[12];
                            }
                            else if ((intWorkSheetQty[i] * dblPressPass) >= rngFrom[13] && (intWorkSheetQty[i] * dblPressPass) <= rngTo[13])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[13];
                                dblCostCZ[i] = dblClickCost[13];
                                dblPriceCZ[i] = dblClickPrice[13];
                            }
                            else if ((intWorkSheetQty[i] * dblPressPass) >= rngFrom[14] && (intWorkSheetQty[i] * dblPressPass) <= rngTo[14])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[14];
                                dblCostCZ[i] = dblClickCost[14];
                                dblPriceCZ[i] = dblClickPrice[14];
                            }
                        }

                        //Checking whether if press is Accumulative click charge 
                        if (!(oModelClickChargeZone.isaccumulativecharge == 0))
                        {
                            intWorkSheetQty[0] = intTEMPPrintQty[0];
                            intWorkSheetQty[1] = intTEMPPrintQty[1];
                            intWorkSheetQty[2] = intTEMPPrintQty[2];
                            intWorkSheetQty[3] = intTEMPPrintQty[3];
                            intWorkSheetQty[4] = intTEMPPrintQty[4];
                        }

                        if (PressReRunMode == (int)PressReRunModes.CalculateValuesToShow)
                        {
                            OverrideValue = dblPriceCZ[PressReRunQuantityIndex - 1];
                            //return functionReturnValue;
                        }
                        else if (PressReRunMode == (int)PressReRunModes.NotReRun)
                        {
                            //do nothing
                            //intPrintChge = oModelPerHourLookUp.Speed
                        }
                        else if (PressReRunMode == (int)PressReRunModes.ReRunPress)
                        {
                            dblPriceCZ[PressReRunQuantityIndex - 1] = OverrideValue;
                        }
                        for (int i = temp; i <= temp2; i++)
                        {
                            //Checking whether press is can't perform perfecting 
                            //if (oItemSection.IsDoubleSided == true && oPressDTO.isPerfecting == false)
                            //{
                            //    //Calculating 3000 print quantity with single sided no spoilage with per 1000 22$
                            //    //Print Cost=(1*(3000/1000)*22) =66
                            //    dblPrintCost[i] = Convert.ToDouble(((dblPassFront * (intWorkSheetQty[i] / intPrintChgeCZ[i]) * dblCostCZ[i]) + (dblPassBack * (intWorkSheetQty[i] / intPrintChgeCZ[i]) * dblCostCZ[i]) + oPressDTO.SetupCharge));

                            //    dblPrintPrice[i] = Convert.ToDouble(((dblPassFront * (intWorkSheetQty[i] / intPrintChgeCZ[i]) * dblPriceCZ[i]) + (dblPassBack * (intWorkSheetQty[i] / intPrintChgeCZ[i]) * dblPriceCZ[i]) + oPressDTO.SetupCharge));

                            //    //Calculating Print Run
                            //    //dblPrintRun(i) = (dblPassFront * intWorkSheetQty(0)) + (dblPassBack * intWorkSheetQty(0))
                            //    dblPrintRun[i] = Convert.ToInt32(dblPassFront * intWorkSheetQty[i] / intPrintChgeCZ[i] + dblPassBack * intWorkSheetQty[i] / intPrintChgeCZ[i]);
                            //}
                            //else
                            //{
                            //    //For Other than Perfecting
                            //    dblPrintCost[i] = Convert.ToDouble(((dblPassFront * (intWorkSheetQty[i] / intPrintChgeCZ[i]) * dblCostCZ[i]) + oPressDTO.SetupCharge));
                            //    dblPrintPrice[i] = Convert.ToDouble(((dblPassFront * (intWorkSheetQty[i] / intPrintChgeCZ[i]) * dblPriceCZ[i]) + oPressDTO.SetupCharge));
                            //    //dblPrintRun(i) = (dblPassFront * intWorkSheetQty(i))
                            //    dblPrintRun[i] = Convert.ToInt32(dblPassFront * intWorkSheetQty[i] / intPrintChgeCZ[i]);
                            //}

                            //Check the coverage high, medium and low and set multiplier.
                            double dblCoverageMultiple = 1;
                            if(isPressSide2)
                            {
                                if (oItemSection.ImpressionCoverageSide2 == 1)
                                    dblCoverageMultiple = oPressDTO.CoverageHigh??1;
                                else if(oItemSection.ImpressionCoverageSide2 == 2)
                                    dblCoverageMultiple = oPressDTO.CoverageMedium ?? 1;
                                else if (oItemSection.ImpressionCoverageSide2 == 3)
                                    dblCoverageMultiple = oPressDTO.CoverageLow ?? 1;
                            }
                            else
                            {
                                if (oItemSection.ImpressionCoverageSide1 == 1)
                                    dblCoverageMultiple = oPressDTO.CoverageHigh ?? 1;
                                else if (oItemSection.ImpressionCoverageSide1 == 2)
                                    dblCoverageMultiple = oPressDTO.CoverageMedium ?? 1;
                                else if (oItemSection.ImpressionCoverageSide1 == 3)
                                    dblCoverageMultiple = oPressDTO.CoverageLow ?? 1;
                            }


                            //dblPrintCost[i] = Convert.ToDouble(((dblPressPass * (intWorkSheetQty[i] / intPrintChgeCZ[i]) * dblCostCZ[i]) + oPressDTO.SetupCharge));
                            //dblPrintPrice[i] = Convert.ToDouble(((dblPressPass * (intWorkSheetQty[i] / intPrintChgeCZ[i]) * dblPriceCZ[i]) + oPressDTO.SetupCharge));
                            //dblPrintRun[i] = Convert.ToInt32(dblPressPass * intWorkSheetQty[i] / intPrintChgeCZ[i]);
                            dblPrintCost[i] = Convert.ToDouble(((dblPressPass * (((intWorkSheetQty[i] / intPrintChgeCZ[i]) * dblCostCZ[i]) * dblCoverageMultiple)) + oPressDTO.SetupCharge));
                            dblPrintPrice[i] = Convert.ToDouble(((dblPressPass * (((intWorkSheetQty[i] / intPrintChgeCZ[i]) * dblPriceCZ[i]) * dblCoverageMultiple)) + oPressDTO.SetupCharge));
                            dblPrintRun[i] = Convert.ToInt32(dblPressPass * ((intWorkSheetQty[i] / intPrintChgeCZ[i]) * dblCoverageMultiple));
                        }

                        if (PressReRunMode == (int)PressReRunModes.NotReRun)
                        {
                            oItemSection.PressHourlyCharge = dblPrintRun[0] / (intPrintChgeCZ[0] / TimePerChargeableSheets) * dblCostCZ[0];
                            oItemSection.PressSpeed1 = Convert.ToInt32(dblPrintRun[0]);
                            oItemSection.PressSpeed2 = Convert.ToInt32(dblPrintRun[1]);
                            oItemSection.PressSpeed3 = Convert.ToInt32(dblPrintRun[2]);

                            PressRunTime1 = Convert.ToDouble((oItemSection.PressSpeed1 / TimePerChargeableSheets));
                            PressRunTime2 = Convert.ToDouble((oItemSection.PressSpeed2 / TimePerChargeableSheets));
                            PressRunTime3 = Convert.ToDouble((oItemSection.PressSpeed3 / TimePerChargeableSheets));

                            PressCost1 = dblPrintCost[0];
                            PressCost2 = dblPrintCost[1];
                            PressCost3 = dblPrintCost[2];

                            PressPrice1 = dblPrintPrice[0];
                            PressPrice2 = dblPrintPrice[1];
                            PressPrice3 = dblPrintPrice[2];
                        }
                        else if (PressReRunMode == (int)PressReRunModes.ReRunPress)
                        {
                            oItemSection.PressHourlyCharge = dblPrintRun[0] / (intPrintChgeCZ[0] / TimePerChargeableSheets) * dblCostCZ[0];
                            if (PressReRunQuantityIndex == 1)
                            {
                                oItemSection.PressSpeed1 = Convert.ToInt32(dblPrintRun[0]);
                                PressRunTime1 = Convert.ToDouble((oItemSection.PressSpeed1 / TimePerChargeableSheets));
                                PressCost1 = dblPrintCost[0];
                                PressPrice1 = dblPrintPrice[0];
                            }
                            else if (PressReRunQuantityIndex == 2)
                            {
                                oItemSection.PressSpeed2 = Convert.ToInt32(dblPrintRun[1]);
                                PressRunTime2 = Convert.ToDouble((oItemSection.PressSpeed2 / TimePerChargeableSheets));
                                PressCost2 = dblPrintCost[1];
                                PressPrice2 = dblPrintPrice[1];
                            }
                            else if (PressReRunQuantityIndex == 3)
                            {
                                oItemSection.PressSpeed3 = Convert.ToInt32(dblPrintRun[2]);
                                PressRunTime3 = Convert.ToDouble((oItemSection.PressSpeed3 / TimePerChargeableSheets));
                                PressCost3 = dblPrintCost[2];
                                PressPrice3 = dblPrintPrice[2];
                            }
                        }
                        break;
                }
                //End LookUp Calculations


                for (int i = temp; i <= temp2; i++)
                {
                    //Checking whether Print Cost is Less than Press Minimum Charge if it is then setting to Press Minimum Charge
                    if (dblPrintPrice[i] < oPressDTO.MinimumCharge)
                    {
                        dblPrintPrice[i] = Convert.ToDouble(oPressDTO.MinimumCharge);
                        sMinimumCost += "1";
                    }
                    else
                    {
                        sMinimumCost += "0";
                    }
                }

                //Updating the 'Print Charge (excl Make Readies) (Pro Rata)
                if (PressReRunMode == (int)PressReRunModes.NotReRun)
                {
                    oItemSection.PrintChargeExMakeReady1 = dblPrintPrice[0] - oPressDTO.SetupCharge;
                    oItemSection.PrintChargeExMakeReady2 = dblPrintPrice[1] - oPressDTO.SetupCharge;
                    oItemSection.PrintChargeExMakeReady3 = dblPrintPrice[2] - oPressDTO.SetupCharge;
                }
                else
                {
                    if (PressReRunQuantityIndex == 1)
                    {
                        oItemSection.PrintChargeExMakeReady1 = dblPrintPrice[0] - oPressDTO.SetupCharge;
                    }
                    else if (PressReRunQuantityIndex == 2)
                    {
                        oItemSection.PrintChargeExMakeReady2 = dblPrintPrice[1] - oPressDTO.SetupCharge;
                    }
                    else if (PressReRunQuantityIndex == 3)
                    {
                        oItemSection.PrintChargeExMakeReady3 = dblPrintPrice[2] - oPressDTO.SetupCharge;
                    }

                }
                //===============================================================================================
                //Updating the press object   
                //oItemSection.Press = oModelPress
                SectionCostcentre oItemSectionCostCenter = new SectionCostcentre();
                if (IsReRun == false)
                {
                    oItemSectionCostCenter.ItemSectionId = oItemSection.ItemSectionId;
                    oItemSectionCostCenter.CostCentreId = (int)oCostCentreDTO.CostCentreId;
                    oItemSectionCostCenter.SystemCostCentreType = (int)SystemCostCenterTypes.Press;
                    oItemSectionCostCenter.Order = 107;
                    oItemSectionCostCenter.IsOptionalExtra = 0;
                    oItemSectionCostCenter.CostCentreType = 1;
                    oItemSectionCostCenter.IsDirectCost = Convert.ToInt16(oPressDTO.DirectCost);
                    oItemSectionCostCenter.SectionCostCentreDetails = new List<SectionCostCentreDetail>();
                }
                else
                {
                    //
                }

                oItemSectionCostCenter.SetupTime = Convert.ToDouble((oPressDTO.SetupTime == 0 ? 0 : oPressDTO.SetupTime / 60));
                oItemSectionCostCenter.IsPrintable = Convert.ToInt16(oJobCardOptionsDTO.IsDefaultPressInstruction);
                //Getting Markup Query
                double ProfitMargin = 0;
                var markup = itemsectionRepository.GetMarkupById(oCostCentreDTO.DefaultVAId);
                if (markup != null)
                    ProfitMargin = Convert.ToDouble(markup.MarkUpRate);
                if (oItemSection.Qty1 > 0)
                {
                    if (PressReRunMode == (int)PressReRunModes.NotReRun)
                    {
                        //Setting Cost to Print Cost calculated + Press Setup Cost
                        oItemSectionCostCenter.Qty1Charge = dblPrintPrice[0];
                        oItemSectionCostCenter.Qty1MarkUpID = oCostCentreDTO.DefaultVAId;
                        oItemSectionCostCenter.Qty1MarkUpValue = oItemSectionCostCenter.Qty1Charge * ProfitMargin / 100;
                        oItemSectionCostCenter.Qty1NetTotal = oItemSectionCostCenter.Qty1Charge + oItemSectionCostCenter.Qty1MarkUpValue;
                        oItemSectionCostCenter.Qty1EstimatedTime = Math.Round(PressRunTime1 + oItemSectionCostCenter.SetupTime, 2);
                        oItemSectionCostCenter.Qty1EstimatedPlantCost = dblPrintCost[0];

                        oItemSection.ImpressionQty1 = Convert.ToInt32(intWorkSheetQty[0] * dblPressPass);

                        if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsDefaultPressInstruction == true)
                        {

                            if (oJobCardOptionsDTO.IsWorkingSize == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions = "Working Size:=" + oItemSection.SectionSizeHeight + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.SectionSizeWidth + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsItemSize == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += "Item Size (Flat):= " + oItemSection.ItemSizeHeight + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.ItemSizeWidth + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsPrintSheetQty == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += "Print Sheet Qty:= " + Math.Ceiling(intWorkSheetQty[0]) + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsNumberOfPasses == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += "Number of Passes:=" + dblPressPass.ToString() + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsImpressionCount == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += "Impression Count:= " + oItemSection.ImpressionQty1.ToString() + Environment.NewLine;
                            }

                            if (oJobCardOptionsDTO.IsPressEstTime == true)
                            {
                                //oItemSectionCostCenter.Qty1WorkInstructions += "Estimated Time:= " + Math.Round(oItemSectionCostCenter.Qty1EstimatedTime, 2).ToString("f") + " hours";
                            }
                        }
                    }
                    else if (PressReRunMode == (int)PressReRunModes.ReRunPress && PressReRunQuantityIndex == 1)
                    {
                        //Setting Cost to Print Cost calculated + Press Setup Cost
                        oItemSectionCostCenter.Qty1Charge = dblPrintPrice[0];
                        oItemSectionCostCenter.Qty1MarkUpID = oCostCentreDTO.DefaultVAId;
                        oItemSectionCostCenter.Qty1MarkUpValue = oItemSectionCostCenter.Qty1Charge * ProfitMargin / 100;
                        oItemSectionCostCenter.Qty1NetTotal = oItemSectionCostCenter.Qty1Charge + oItemSectionCostCenter.Qty1MarkUpValue;
                        oItemSectionCostCenter.Qty1EstimatedTime = Math.Round(PressRunTime1 + oItemSectionCostCenter.SetupTime, 2);
                        oItemSectionCostCenter.Qty1EstimatedPlantCost = dblPrintCost[0];

                        oItemSection.ImpressionQty1 = Convert.ToInt32(intWorkSheetQty[0] * dblPressPass);

                        if (IsWorkInstructionsLocked == false && oJobCardOptionsDTO.IsDefaultPressInstruction == true)
                        {
                            if (oJobCardOptionsDTO.IsWorkingSize == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions = "Working Size:=" + oItemSection.SectionSizeHeight + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.SectionSizeWidth + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsItemSize == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += "Item Size (Flat):= " + oItemSection.ItemSizeHeight + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.ItemSizeWidth + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsPrintSheetQty == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += "Print Sheet Qty:= " + Math.Ceiling(intWorkSheetQty[0]) + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsNumberOfPasses == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += "Number of Passes:=" + dblPressPass.ToString() + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsImpressionCount == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += "Impression Count:= " + oItemSection.ImpressionQty1.ToString() + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsPressEstTime == true)
                            {
                                // oItemSectionCostCenter.Qty1WorkInstructions += "Estimated Time:= " + Math.Round(oItemSectionCostCenter.Qty1EstimatedTime, 2).ToString("f") + " hours";
                            }
                        }

                    }
                }

                if (oItemSection.Qty2 > 0)
                {
                    if (PressReRunMode == (int)PressReRunModes.NotReRun)
                    {
                        oItemSectionCostCenter.Qty2Charge = dblPrintPrice[1];
                        oItemSectionCostCenter.Qty2MarkUpID = oCostCentreDTO.DefaultVAId;
                        oItemSectionCostCenter.Qty2MarkUpValue = Convert.ToDouble(oItemSectionCostCenter.Qty2Charge * ProfitMargin / 100);
                        oItemSectionCostCenter.Qty2NetTotal = oItemSectionCostCenter.Qty2Charge + oItemSectionCostCenter.Qty2MarkUpValue;
                        oItemSectionCostCenter.Qty2EstimatedTime = Math.Round(PressRunTime2 + oItemSectionCostCenter.SetupTime, 2);
                        oItemSectionCostCenter.Qty2EstimatedPlantCost = dblPrintCost[1];

                        oItemSection.ImpressionQty2 = Convert.ToInt32(intWorkSheetQty[1] * dblPressPass);


                        if (IsWorkInstructionsLocked == false && oJobCardOptionsDTO.IsDefaultPressInstruction == true)
                        {
                            if (oJobCardOptionsDTO.IsWorkingSize == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions = "Working Size:=" + oItemSection.SectionSizeHeight + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.SectionSizeWidth + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsItemSize == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += "Item Size (Flat):= " + oItemSection.ItemSizeHeight + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.ItemSizeWidth + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsPrintSheetQty == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += "Print Sheet Qty:= " + Math.Ceiling(intWorkSheetQty[1]) + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsNumberOfPasses == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += "Number of Passes:=" + dblPressPass.ToString() + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsImpressionCount == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += "Impression Count:= " + oItemSection.ImpressionQty2.ToString() + Environment.NewLine;
                            }

                            if (oJobCardOptionsDTO.IsPressEstTime == true)
                            {
                                //oItemSectionCostCenter.Qty2WorkInstructions += "Estimated Time:= " + Math.Round(oItemSectionCostCenter.Qty2EstimatedTime, 2).ToString("f") + " hours";
                            }

                        }

                    }
                    else if (PressReRunMode == (int)PressReRunModes.ReRunPress && PressReRunQuantityIndex == 2)
                    {
                        oItemSectionCostCenter.Qty2Charge = dblPrintPrice[1];
                        oItemSectionCostCenter.Qty2MarkUpID = oCostCentreDTO.DefaultVAId;
                        oItemSectionCostCenter.Qty2MarkUpValue = Convert.ToDouble(oItemSectionCostCenter.Qty2Charge * ProfitMargin / 100);
                        oItemSectionCostCenter.Qty2NetTotal = oItemSectionCostCenter.Qty2Charge + oItemSectionCostCenter.Qty2MarkUpValue;
                        oItemSectionCostCenter.Qty2EstimatedTime = Math.Round(PressRunTime2 + oItemSectionCostCenter.SetupTime, 2);
                        oItemSectionCostCenter.Qty2EstimatedPlantCost = dblPrintCost[1];

                        oItemSection.ImpressionQty2 = Convert.ToInt32(intWorkSheetQty[1] * dblPressPass);
                        if (IsWorkInstructionsLocked == false && oJobCardOptionsDTO.IsDefaultPressInstruction == true)
                        {
                            if (oJobCardOptionsDTO.IsWorkingSize == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions = "Working Size:=" + oItemSection.SectionSizeHeight + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.SectionSizeWidth + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsItemSize == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += "Item Size (Flat):= " + oItemSection.ItemSizeHeight + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.ItemSizeWidth + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsPrintSheetQty == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += "Print Sheet Qty:= " + Math.Ceiling(intWorkSheetQty[1]) + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsNumberOfPasses == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += "Number of Passes:=" + dblPressPass.ToString() + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsImpressionCount == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += "Impression Count:= " + oItemSection.ImpressionQty2.ToString() + Environment.NewLine;
                            }

                            if (oJobCardOptionsDTO.IsPressEstTime == true)
                            {
                                //oItemSectionCostCenter.Qty2WorkInstructions += "Estimated Time:= " + Math.Round(oItemSectionCostCenter.Qty2EstimatedTime, 2).ToString("f") + " hours";
                            }

                        }
                    }
                }

                if (oItemSection.Qty3 > 0)
                {
                    if (PressReRunMode == (int)PressReRunModes.NotReRun)
                    {
                        oItemSectionCostCenter.Qty3Charge = dblPrintPrice[2];
                        oItemSectionCostCenter.Qty3MarkUpID = oCostCentreDTO.DefaultVAId;
                        oItemSectionCostCenter.Qty3MarkUpValue = oItemSectionCostCenter.Qty2Charge * ProfitMargin / 100;
                        oItemSectionCostCenter.Qty3NetTotal = oItemSectionCostCenter.Qty3Charge + oItemSectionCostCenter.Qty3MarkUpValue;
                        oItemSectionCostCenter.Qty3EstimatedTime = Math.Round(PressRunTime3 + oItemSectionCostCenter.SetupTime, 2);
                        oItemSectionCostCenter.Qty3EstimatedPlantCost = dblPrintCost[2];

                        oItemSection.ImpressionQty3 = Convert.ToInt32(intWorkSheetQty[2] * dblPressPass);

                        if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsDefaultPressInstruction == true)
                        {
                            if (oJobCardOptionsDTO.IsWorkingSize == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions = "Working Size:=" + oItemSection.SectionSizeHeight + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.SectionSizeWidth + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsItemSize == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += "Item Size (Flat):= " + oItemSection.ItemSizeHeight + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.ItemSizeWidth + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsPrintSheetQty == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += "Print Sheet Qty:= " + Math.Ceiling(intWorkSheetQty[2]) + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsNumberOfPasses == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += "Number of Passes:=" + dblPressPass.ToString() + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsImpressionCount == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += "Impression Count:= " + oItemSection.ImpressionQty3.ToString() + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsPressEstTime == true)
                            {
                                //oItemSectionCostCenter.Qty3WorkInstructions += "Estimated Time:= " + Math.Round(oItemSectionCostCenter.Qty3EstimatedTime, 2).ToString("f") + " hours";
                            }

                        }
                    }
                    else if (PressReRunMode == (int)PressReRunModes.ReRunPress & PressReRunQuantityIndex == 3)
                    {
                        oItemSectionCostCenter.Qty3Charge = dblPrintPrice[2];
                        oItemSectionCostCenter.Qty3MarkUpID = oCostCentreDTO.DefaultVAId;
                        oItemSectionCostCenter.Qty3MarkUpValue = oItemSectionCostCenter.Qty2Charge * ProfitMargin / 100;
                        oItemSectionCostCenter.Qty3NetTotal = oItemSectionCostCenter.Qty3Charge + oItemSectionCostCenter.Qty3MarkUpValue;
                        oItemSectionCostCenter.Qty3EstimatedTime = Math.Round(PressRunTime3 + oItemSectionCostCenter.SetupTime, 2);
                        oItemSectionCostCenter.Qty3EstimatedPlantCost = dblPrintCost[2];

                        oItemSection.ImpressionQty3 = Convert.ToInt32(intWorkSheetQty[2] * dblPressPass);

                        if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsDefaultPressInstruction == true)
                        {
                            if (oJobCardOptionsDTO.IsWorkingSize == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions = "Working Size:=" + oItemSection.SectionSizeHeight + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.SectionSizeWidth + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsItemSize == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += "Item Size (Flat):= " + oItemSection.ItemSizeHeight + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.ItemSizeWidth + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsPrintSheetQty == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += "Print Sheet Qty:= " + Math.Ceiling(intWorkSheetQty[2]) + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsNumberOfPasses == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += "Number of Passes:=" + dblPressPass.ToString() + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsImpressionCount == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += "Impression Count:= " + oItemSection.ImpressionQty3.ToString() + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsPressEstTime == true)
                            {
                                // oItemSectionCostCenter.Qty3WorkInstructions += "Estimated Time:= " + Math.Round(oItemSectionCostCenter.Qty3EstimatedTime, 2).ToString("f") + " hours";
                            }
                        }
                    }
                }

                if (sMinimumCost != string.Empty)
                {
                    oItemSectionCostCenter.IsMinimumCost = Convert.ToInt16(sMinimumCost);
                }
                else
                {
                    oItemSectionCostCenter.IsMinimumCost = 0;
                }


                //Setting up the QTY in the object
                // Model.Items.ItemSectionCostCentreDetailDTO oItemSectionCostCenterDetail = default(Model.Items.ItemSectionCostCentreDetailDTO);
                SectionCostCentreDetail oItemSectionCostCenterDetail;
                if (IsReRun == false)
                {
                    oItemSectionCostCenterDetail = new SectionCostCentreDetail();
                }
                else
                {
                    oItemSectionCostCenterDetail = oItemSectionCostCenter.SectionCostCentreDetails.FirstOrDefault();

                }
                oItemSectionCostCenterDetail.Qty1 = 1;
                if (oItemSection.Qty2 > 0)
                {
                    oItemSectionCostCenterDetail.Qty2 = 1;
                }
                if (oItemSection.Qty3 > 0)
                {
                    oItemSectionCostCenterDetail.Qty3 = 1;
                }

                //oItemSectionCostCenterDetail.StockId = oItemSection.PressID;
                oItemSectionCostCenterDetail.CostPrice = dblPrintCost[0];
                //Naveed: condition added for setting name of the press while calling this function to get best press list from service
                string sSide = isPressSide2 ? "Side 2" : "Side 1";
                if (isBestPress)
                    oItemSectionCostCenter.Name = oPressDTO.MachineName;
                else
                    oItemSectionCostCenter.Name = "Press ( " + oPressDTO.MachineName + " ) " + sSide;
                oItemSectionCostCenter.Qty1 = oItemSection.Qty1;
                oItemSectionCostCenter.Qty2 = oItemSection.Qty2;
                oItemSectionCostCenter.Qty3 = oItemSection.Qty3;
               
                if (IsReRun == false)
                {
                    if (oItemSectionCostCenter.SectionCostCentreDetails == null)
                    {
                        oItemSectionCostCenter.SectionCostCentreDetails = new List<SectionCostCentreDetail>();
                    }
                    oItemSectionCostCenter.SectionCostCentreDetails.Add(oItemSectionCostCenterDetail);
                    if (oItemSection.SectionCostcentres == null)
                    {
                        oItemSection.SectionCostcentres = new List<SectionCostcentre>();
                    }
                    oItemSection.SectionCostcentres.Add(oItemSectionCostCenter);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("CalculatePressCost", ex);
            }

            return oItemSection;//.tbl_section_costcentres.ToList();
        }

        public ItemSection CalculatePressCost(ItemSection oItemSection, int PressID, bool IsReRun = false, bool IsWorkInstructionsLocked = false, int PressReRunMode = (int)PressReRunModes.NotReRun, int PressReRunQuantityIndex = 1, double OverrideValue = 0, bool isBestPress = false, bool isPressSide2 = false)
        {
            List<MachineSpoilage> machineSpoilageList = itemsectionRepository.GetSpoilageByPressId(PressID);     


            if (machineSpoilageList != null && machineSpoilageList.Count > 0)
            {
                int InkColors = oItemSection.Side1Inks + oItemSection.Side2Inks;
                var maxPressColor = machineSpoilageList.Max(m => m.NoOfColors);
                if (maxPressColor != null && InkColors > maxPressColor)
                    InkColors = (int)maxPressColor;

                var setupspoilage = machineSpoilageList.Where(m => m.NoOfColors == InkColors).FirstOrDefault();
                if (setupspoilage != null)
                {
                    oItemSection.SetupSpoilage = setupspoilage.SetupSpoilage;
                    oItemSection.RunningSpoilage = Convert.ToInt16(setupspoilage.RunningSpoilage);
                }
            }

            JobPreference oJobCardOptionsDTO = itemsectionRepository.GetJobPreferences(1);
            bool functionReturnValue = false;
            string sMinimumCost = null;
            double dblPassFront = 0;
            double dblPassBack = 0;
            string strPrintLookup = null;
            double dblPlateQty = 0;
            int TotalNoOfPasses = 0;
            dblPassFront = Convert.ToDouble(oItemSection.PassesSide1 ?? 0);
            dblPassBack = Convert.ToDouble(oItemSection.PassesSide2 ?? 0);
            //TotalNoOfPasses = oItemSection.PassesSide1??0 + oItemSection.PassesSide2??0;
            SectionCostCentreResource oResourceDto = new SectionCostCentreResource();
            //Get Machine Query
            Machine oPressDTO = itemsectionRepository.GetPressById(PressID);

            if (oPressDTO.isplateused == true)
            {
                oItemSection.IsPlateUsed = true;
                oItemSection.PlateId = oPressDTO.DefaultPlateId;
            }
            else
                oItemSection.IsPlateUsed = false;
            oItemSection.IsWashup = oPressDTO.iswashupused == true ? true : false;
            oItemSection.IsMakeReadyUsed = oPressDTO.ismakereadyused == true ? true : false;



            int SheetPTV = 0;
            int SetupSpoilage = 0;
            double RunningSpoilagePercentage = 0;
            //Get Press Cost Center Query
            CostCentre oCostCentreDTO = itemsectionRepository.GetCostCenterBySystemType((int)SystemCostCenterTypes.Press);

            double PressCost1 = 0;
            double PressPrice1 = 0;
            double PressCost2 = 0;
            double PressPrice2 = 0;
            double PressCost3 = 0;
            double PressPrice3 = 0;
            double PressRunTime1 = 0;
            double PressRunTime2 = 0;
            double PressRunTime3 = 0;


            //'# of times the printsheets need to be passed thru the given press, based on the # of colours
            //'that are printed on the printsheets and the # of colours the press can print at a time, i.e.
            //'how many heads it has.

            try
            {
                //Checking whether the press can do perfecting or not
                
                double dblSide1Ink = Convert.ToDouble(oItemSection.Side1Inks);
                double dblSide2Ink = Convert.ToDouble(oItemSection.Side2Inks);
                double dblPressHeads = Convert.ToDouble(oPressDTO.ColourHeads);

                if (oPressDTO.ColourHeads != 0)
                {
                    if (oPressDTO.isPerfecting == true)
                    {
                        //Determining how many front passes an item will have to be                     
                        dblPassFront = Convert.ToDouble((dblSide1Ink + dblSide2Ink) / dblPressHeads);
                    }
                    else
                    {
                        //Determining Front Passes if press can't perform Perfecting
                        dblPassFront = Convert.ToDouble((dblSide1Ink / dblPressHeads));
                    }
                }
                else
                {
                    dblPassFront = 0;
                }
                //Rounding to the next value
                dblPassFront = Math.Ceiling(dblPassFront);

                if (oPressDTO.ColourHeads != 0)
                {
                    //If press can perform perfecting Then Back Passes=0
                    if (oPressDTO.isPerfecting == false)
                    {
                        //If Press cannot perform perfecting then calculating the Back Passes
                        dblPassBack = Math.Round((double)(dblSide2Ink / dblPressHeads), 1);
                    }
                    else
                    {
                        dblPassBack = 0;
                    }
                }
                else
                {
                    dblPassBack = 0;
                }


                //Rounding to the next number
                dblPassBack = Math.Ceiling(dblPassBack);

                oItemSection.PressPassesQty = Convert.ToInt32(dblPassFront + dblPassBack);
                oItemSection.PressPassesQty = TotalNoOfPasses;
                    
                ///'''=======================================================================
                StockItem oPaperDTO = new StockItem();
                //Get Paper Stock Query
                oPaperDTO = itemsectionRepository.GetStockById(Convert.ToInt64(oItemSection.StockItemID1));// db.StockItems.Where(s => s.StockItemId == oItemSection.StockItemID1).FirstOrDefault();

                if (oItemSection.PrintViewLayout == (int)PrintViewOrientation.Landscape)
                {
                    SheetPTV = Convert.ToInt32(oItemSection.PrintViewLayoutLandScape);
                }
                else
                {
                    SheetPTV = Convert.ToInt32(oItemSection.PrintViewLayoutPortrait);
                }

                SetupSpoilage = Convert.ToInt32(oItemSection.SetupSpoilage);
                RunningSpoilagePercentage = Convert.ToDouble(oItemSection.RunningSpoilage);

                //This variable array is to hold the Working Sheet Qty For each multiple qty
                double[] intWorkSheetQty = new double[5];
                intWorkSheetQty[0] = Convert.ToInt32(oItemSection.Qty1 / SheetPTV) + SetupSpoilage + Convert.ToInt32(Convert.ToInt32(oItemSection.Qty1 / SheetPTV) * RunningSpoilagePercentage / 100);
                intWorkSheetQty[1] = Convert.ToInt32(oItemSection.Qty2 / SheetPTV) + SetupSpoilage + Convert.ToInt32(Convert.ToInt32(oItemSection.Qty2 / SheetPTV) * RunningSpoilagePercentage / 100);
                intWorkSheetQty[2] = Convert.ToInt32(oItemSection.Qty3 / SheetPTV) + SetupSpoilage + Convert.ToInt32(Convert.ToInt32(oItemSection.Qty3 / SheetPTV) * RunningSpoilagePercentage / 100);

                //Model.LookupMethods.MethodDTO oModelLookUpMethod = BLL.LookupMethods.Method.GetMachineLookUpMethod(GlobalData, oItemSection.SelectedPressCalculationMethodID);
                LookupMethod oModelLookUpMethod = new LookupMethod();
                //if(isPressSide2)
                //    oModelLookUpMethod = itemsectionRepository.GetLookupMethodById(Convert.ToInt64(oItemSection.Side2LookUp));
                //else
                //    oModelLookUpMethod = itemsectionRepository.GetLookupMethodById(Convert.ToInt64(oItemSection.Side1LookUp));
                //oItemSection.SelectedPressCalculationMethodId = Convert.ToInt32(oPressDTO.LookupMethodId); // Commented when applied press for side 2 logic on 2015 05 20

                //Get LookupMethod Query
                oModelLookUpMethod = itemsectionRepository.GetLookupMethodById(Convert.ToInt32(oPressDTO.LookupMethodId));

                double[] dblPrintCost = new double[3];
                double[] dblPrintPrice = new double[3];
                double[] dblPrintRun = new double[3];
                double[] dblPrintPlateQty = new double[3];
                int temp = 0;
                int temp2 = 0;
                if (PressReRunMode == (int)PressReRunModes.NotReRun)
                {
                    temp = 0;
                    temp2 = 2;
                }
                else if (PressReRunMode == (int)PressReRunModes.ReRunPress)
                {
                    temp = PressReRunQuantityIndex - 1;
                    temp2 = PressReRunQuantityIndex - 1;
                }

                switch (oModelLookUpMethod.Type)
                {
                    //'=============================================================================
                    case (int)MethodTypes.ClickCharge:
                        //ClickCharge LookUp
                        //'=========================================================================
                        int intPrintChge = 0;
                        double dblCost = 0;
                        double dblPrice = 0;

                        //Getting the Values for Click Charge LookUp Query

                        MachineClickChargeLookup oModelClickChargeLookUp = itemsectionRepository.GetMachineClickChargeById(oModelLookUpMethod.MethodId);
                        intPrintChge = oModelClickChargeLookUp.Sheets != null ? Convert.ToInt32(oModelClickChargeLookUp.Sheets) : 0;
                        dblCost = oModelClickChargeLookUp.SheetCost != null ? Convert.ToDouble(oModelClickChargeLookUp.SheetCost) : 0;

                        if (PressReRunMode == (int)PressReRunModes.CalculateValuesToShow)
                        {
                            dblPrice = oModelClickChargeLookUp.SheetPrice != null ? Convert.ToDouble(oModelClickChargeLookUp.SheetPrice) : 0;
                            OverrideValue = dblPrice;
                            //return functionReturnValue;
                        }
                        else if (PressReRunMode == (int)PressReRunModes.NotReRun)
                        {
                            dblPrice = oModelClickChargeLookUp.SheetPrice != null ? Convert.ToDouble(oModelClickChargeLookUp.SheetPrice) : 0;
                        }
                        else if (PressReRunMode == (int)PressReRunModes.ReRunPress)
                        {
                            dblPrice = OverrideValue;
                        }

                        double TimePerSheets = oModelClickChargeLookUp.TimePerHour != null ? Convert.ToDouble(oModelClickChargeLookUp.TimePerHour) : 0;

                        //Updating the Press Hourly Charge
                        oItemSection.PressHourlyCharge = TimePerSheets;
                        //Updating the Press Speed PRORATA
                        if (PressReRunMode == (int)PressReRunModes.NotReRun)
                        {
                            if (!(oItemSection.IsDoubleSided == false) && oPressDTO.isPerfecting == false)
                            {
                                oItemSection.PressSpeed1 = Convert.ToInt32(dblPassFront * intWorkSheetQty[0] / oModelClickChargeLookUp.Sheets + dblPassBack * intWorkSheetQty[0] / oModelClickChargeLookUp.Sheets);
                            }
                            else
                            {
                                oItemSection.PressSpeed1 = Convert.ToInt32(dblPassFront * intWorkSheetQty[0] / oModelClickChargeLookUp.Sheets);
                            }
                            PressCost1 = Convert.ToDouble(oItemSection.PressSpeed1 * oModelClickChargeLookUp.SheetCost);
                            dblPrintCost[0] = Convert.ToDouble(PressCost1 + oPressDTO.SetupCharge);
                            PressPrice1 = Convert.ToDouble(oItemSection.PressSpeed1 * dblPrice);
                            dblPrintPrice[0] = Convert.ToDouble(PressPrice1 + oPressDTO.SetupCharge);
                            PressRunTime1 = Convert.ToDouble(oItemSection.PressSpeed1 / TimePerSheets);
                        }
                        else if (PressReRunMode == (int)PressReRunModes.ReRunPress && PressReRunQuantityIndex == 1)
                        {
                            if (!(oItemSection.IsDoubleSided == false) && oPressDTO.isPerfecting == false)
                            {
                                oItemSection.PressSpeed1 = Convert.ToInt32(dblPassFront * intWorkSheetQty[0] / oModelClickChargeLookUp.Sheets + dblPassBack * intWorkSheetQty[0] / oModelClickChargeLookUp.Sheets);
                            }
                            else
                            {
                                oItemSection.PressSpeed1 = Convert.ToInt32(dblPassFront * intWorkSheetQty[0] / oModelClickChargeLookUp.Sheets);
                            }
                            PressCost1 = Convert.ToDouble(oItemSection.PressSpeed1 * oModelClickChargeLookUp.SheetCost);
                            dblPrintCost[0] = Convert.ToDouble(PressCost1 + oPressDTO.SetupCharge);
                            PressPrice1 = Convert.ToDouble(oItemSection.PressSpeed1 * dblPrice);
                            dblPrintPrice[0] = Convert.ToDouble(PressPrice1 + oPressDTO.SetupCharge);
                            PressRunTime1 = Convert.ToDouble(oItemSection.PressSpeed1 / TimePerSheets);
                        }

                        //qty 2
                        if (PressReRunMode == (int)PressReRunModes.NotReRun)
                        {
                            if (!(oItemSection.IsDoubleSided == false) && oPressDTO.isPerfecting == false)
                            {
                                oItemSection.PressSpeed2 = Convert.ToInt32(dblPassFront * intWorkSheetQty[1] / oModelClickChargeLookUp.Sheets + dblPassBack * intWorkSheetQty[1] / oModelClickChargeLookUp.Sheets);
                            }
                            else
                            {
                                oItemSection.PressSpeed2 = Convert.ToInt32(dblPassFront * intWorkSheetQty[1] / oModelClickChargeLookUp.Sheets);
                            }
                            PressCost2 = Convert.ToDouble(oItemSection.PressSpeed2 * oModelClickChargeLookUp.SheetCost);
                            dblPrintCost[1] = Convert.ToDouble(PressCost2 + oPressDTO.SetupCharge);
                            PressPrice2 = Convert.ToDouble(oItemSection.PressSpeed2 * oModelClickChargeLookUp.SheetPrice);
                            dblPrintPrice[1] = Convert.ToDouble(PressPrice2 + oPressDTO.SetupCharge);
                            PressRunTime2 = Convert.ToDouble(oItemSection.PressSpeed2 / TimePerSheets);
                        }
                        else if (PressReRunMode == (int)PressReRunModes.ReRunPress && PressReRunQuantityIndex == 2)
                        {
                            if (!(oItemSection.IsDoubleSided == false) & oPressDTO.isPerfecting == false)
                            {
                                oItemSection.PressSpeed2 = Convert.ToInt32(dblPassFront * intWorkSheetQty[1] / oModelClickChargeLookUp.Sheets + dblPassBack * intWorkSheetQty[1] / oModelClickChargeLookUp.Sheets);
                            }
                            else
                            {
                                oItemSection.PressSpeed2 = Convert.ToInt32(dblPassFront * intWorkSheetQty[1] / oModelClickChargeLookUp.Sheets);
                            }
                            PressCost2 = Convert.ToDouble(oItemSection.PressSpeed2 * oModelClickChargeLookUp.SheetCost);
                            dblPrintCost[1] = Convert.ToDouble(PressCost2 + oPressDTO.SetupCharge);
                            PressPrice2 = Convert.ToDouble(oItemSection.PressSpeed2 * oModelClickChargeLookUp.SheetPrice);
                            dblPrintPrice[1] = Convert.ToDouble(PressPrice2 + oPressDTO.SetupCharge);
                            PressRunTime2 = Convert.ToDouble(oItemSection.PressSpeed2 / TimePerSheets);
                        }
                        //qty 3
                        if (PressReRunMode == (int)PressReRunModes.NotReRun)
                        {
                            if (!(oItemSection.IsDoubleSided == false) && oPressDTO.isPerfecting == false)
                            {
                                oItemSection.PressSpeed3 = Convert.ToInt32(dblPassFront * intWorkSheetQty[2] / oModelClickChargeLookUp.Sheets + dblPassBack * intWorkSheetQty[2] / oModelClickChargeLookUp.Sheets);

                            }
                            else
                            {
                                oItemSection.PressSpeed3 = Convert.ToInt32(dblPassFront * intWorkSheetQty[2] / oModelClickChargeLookUp.Sheets);
                            }
                            PressCost3 = Convert.ToDouble(oItemSection.PressSpeed3 * oModelClickChargeLookUp.SheetCost);
                            dblPrintCost[2] = Convert.ToDouble(PressCost3 + oPressDTO.SetupCharge);
                            PressPrice3 = Convert.ToDouble(oItemSection.PressSpeed3 * oModelClickChargeLookUp.SheetPrice);
                            dblPrintPrice[2] = Convert.ToDouble(PressPrice3 + oPressDTO.SetupCharge);
                            PressRunTime3 = Convert.ToDouble(oItemSection.PressSpeed3 / TimePerSheets);
                        }
                        else if (PressReRunMode == (int)PressReRunModes.ReRunPress && PressReRunQuantityIndex == 3)
                        {
                            if (!(oItemSection.IsDoubleSided == false) && oPressDTO.isPerfecting == false)
                            {
                                oItemSection.PressSpeed3 = Convert.ToInt32(dblPassFront * intWorkSheetQty[2] / oModelClickChargeLookUp.Sheets + dblPassBack * intWorkSheetQty[2] / oModelClickChargeLookUp.Sheets);

                            }
                            else
                            {
                                oItemSection.PressSpeed3 = Convert.ToInt32(dblPassFront * intWorkSheetQty[2] / oModelClickChargeLookUp.Sheets);
                            }
                            PressCost3 = Convert.ToDouble(oItemSection.PressSpeed3 * oModelClickChargeLookUp.SheetCost);
                            dblPrintCost[2] = Convert.ToDouble(PressCost3 + oPressDTO.SetupCharge);
                            PressPrice3 = Convert.ToDouble(oItemSection.PressSpeed3 * oModelClickChargeLookUp.SheetPrice);
                            dblPrintPrice[2] = Convert.ToDouble(PressPrice3 + oPressDTO.SetupCharge);
                            PressRunTime3 = Convert.ToDouble(oItemSection.PressSpeed3 / TimePerSheets);
                        }

                        break;
                    ///'=====================================================
                    case (int)MethodTypes.SpeedWeight:
                        //Speed Weight
                        ///'=====================================================
                        int dblPrintChge = 0;
                        int dblPrintChgeRate = 0;
                        int[] intGSM = new int[4];
                        int[] intSheets = new int[6];
                        double[,] intSpeed = new double[4, 6];
                        double dblSpeed1 = 0;
                        double dblSpeed2 = 0;
                        double[] dblPrintSpeed = new double[5];
                        //Getting the Plant Lookup Information Query

                        MachineSpeedWeightLookup oModelSpeedWeight = itemsectionRepository.GetMachineSpeedWeightLookup(oModelLookUpMethod.MethodId);
                        //There are 3 GSM Setting their Values
                        intGSM[1] = Convert.ToInt32(oModelSpeedWeight.SheetWeight1);
                        intGSM[2] = Convert.ToInt32(oModelSpeedWeight.SheetWeight2);
                        intGSM[3] = Convert.ToInt32(oModelSpeedWeight.SheetWeight3);
                        //Setting the Sheet Values
                        intSheets[1] = Convert.ToInt32(oModelSpeedWeight.SheetsQty1);
                        intSheets[2] = Convert.ToInt32(oModelSpeedWeight.SheetsQty2);
                        intSheets[3] = Convert.ToInt32(oModelSpeedWeight.SheetsQty3);
                        intSheets[4] = Convert.ToInt32(oModelSpeedWeight.SheetsQty4);
                        intSheets[5] = Convert.ToInt32(oModelSpeedWeight.SheetsQty5);

                        //Setting up the Speed Values
                        intSpeed[1, 1] = Convert.ToDouble(oModelSpeedWeight.speedqty11);
                        intSpeed[1, 2] = Convert.ToDouble(oModelSpeedWeight.speedqty12);
                        intSpeed[1, 3] = Convert.ToDouble(oModelSpeedWeight.speedqty13);
                        intSpeed[1, 4] = Convert.ToDouble(oModelSpeedWeight.speedqty14);
                        intSpeed[1, 5] = Convert.ToDouble(oModelSpeedWeight.speedqty15);
                        intSpeed[2, 1] = Convert.ToDouble(oModelSpeedWeight.speedqty21);
                        intSpeed[2, 2] = Convert.ToDouble(oModelSpeedWeight.speedqty22);
                        intSpeed[2, 3] = Convert.ToDouble(oModelSpeedWeight.speedqty23);
                        intSpeed[2, 4] = Convert.ToDouble(oModelSpeedWeight.speedqty24);
                        intSpeed[2, 5] = Convert.ToDouble(oModelSpeedWeight.speedqty25);
                        intSpeed[3, 1] = Convert.ToDouble(oModelSpeedWeight.speedqty31);
                        intSpeed[3, 2] = Convert.ToDouble(oModelSpeedWeight.speedqty32);
                        intSpeed[3, 3] = Convert.ToDouble(oModelSpeedWeight.speedqty33);
                        intSpeed[3, 4] = Convert.ToDouble(oModelSpeedWeight.speedqty34);
                        intSpeed[3, 5] = Convert.ToDouble(oModelSpeedWeight.speedqty35);

                        //Checking that the press support this GSM or Not
                        if (oPaperDTO.ItemWeight > oPressDTO.maximumsheetweight)
                        {
                            // return functionReturnValue;
                            ///Please select a paper that can be supported by the selected press.
                        }

                        byte btGSM = 0;
                        byte btRun = 0;
                        double dblGSM = 0;
                        double dblRun = 0;
                        //'Get the GSM
                        //Checking for the PaperGSM
                        if (oPaperDTO.ItemWeight == intGSM[1])
                        {
                            btGSM = 1;
                            dblGSM = 0;
                        }
                        else if (oPaperDTO.ItemWeight == intGSM[2])
                        {
                            btGSM = 2;
                            dblGSM = 0;
                        }
                        else if (oPaperDTO.ItemWeight == intGSM[3])
                        {
                            btGSM = 3;
                            dblGSM = 0;
                        }
                        else
                        {
                            if (oPaperDTO.ItemWeight > intGSM[1] && oPaperDTO.ItemWeight < intGSM[2])
                            {
                                dblGSM = (double)(intGSM[2] - oPaperDTO.ItemWeight) / (intGSM[2] - intGSM[1]);
                                btGSM = 1;
                            }
                            else if (oPaperDTO.ItemWeight > intGSM[2] && oPaperDTO.ItemWeight < intGSM[3])
                            {
                                dblGSM = (double)(intGSM[3] - oPaperDTO.ItemWeight) / (intGSM[3] - intGSM[2]);
                                btGSM = 2;
                            }
                        }


                        //'Get the Sheets
                        for (int i = temp; i <= temp2; i++)
                        {
                            //Exact Values i.e. 2000 Sheets, 3000 Sheets etc.
                            //*****Difference between dblRun and btRun
                            if (intWorkSheetQty[i] <= intSheets[1])
                            {
                                dblRun = 0;
                                btRun = 1;
                            }
                            else if (intWorkSheetQty[i] == intSheets[2])
                            {
                                dblRun = 0;
                                btRun = 2;
                            }
                            else if (intWorkSheetQty[i] == intSheets[3])
                            {
                                dblRun = 0;
                                btRun = 3;
                            }
                            else if (intWorkSheetQty[i] == intSheets[4])
                            {
                                dblRun = 0;
                                btRun = 4;
                            }
                            else if (intWorkSheetQty[i] >= intSheets[5])
                            {
                                dblRun = 0;
                                btRun = 5;
                            }       //Now determining which run for Other than Exact values and checking in which range it lies for example 2300
                            //Running the Previous run for example for 2300, 2000 run would be used
                            else
                            {
                                if (intWorkSheetQty[i] < intSheets[2] & intWorkSheetQty[i] > intSheets[1])
                                {
                                    dblRun = (intSheets[2] - intWorkSheetQty[i]) / (intSheets[2] - intSheets[1]);
                                    btRun = 1;
                                }
                                else if (intWorkSheetQty[i] < intSheets[3] & intWorkSheetQty[i] > intSheets[2])
                                {
                                    dblRun = (intSheets[3] - intWorkSheetQty[i]) / (intSheets[3] - intSheets[2]);
                                    btRun = 2;
                                }
                                else if (intWorkSheetQty[i] < intSheets[4] & intWorkSheetQty[i] > intSheets[3])
                                {
                                    dblRun = (intSheets[4] - intWorkSheetQty[i]) / (intSheets[4] - intSheets[3]);
                                    btRun = 3;
                                }
                                else if (intWorkSheetQty[i] < intSheets[5] & intWorkSheetQty[i] > intSheets[4])
                                {
                                    dblRun = (intSheets[5] - intWorkSheetQty[i]) / (intSheets[5] - intSheets[4]);
                                    btRun = 4;
                                }
                            }


                            if (btRun == 5)
                            {
                                //Determining Which Speed for GSM1 and GSM2
                                dblSpeed1 = intSpeed[btGSM - 1, btRun] - ((intSpeed[btGSM - 1, btRun] - intSpeed[btGSM, btRun]) * dblGSM);
                                dblSpeed2 = intSpeed[btGSM, btRun] - ((intSpeed[btGSM, btRun] - intSpeed[btGSM, btRun]) * dblGSM);
                                //Other Than 5
                            }
                            else
                            {
                                dblSpeed1 = intSpeed[btGSM, btRun + 1] - ((intSpeed[btGSM, btRun + 1] - intSpeed[btGSM, btRun]) * dblGSM);
                                dblSpeed2 = intSpeed[btGSM, btRun] - ((intSpeed[btGSM - 1, btRun] - intSpeed[btGSM, btRun]) * dblGSM);
                            }
                            //Setting the Exact Speed

                            if (PressReRunMode == (int)PressReRunModes.CalculateValuesToShow)
                            {
                                dblPrintSpeed[i] = dblSpeed2 - ((dblSpeed2 - dblSpeed1) * dblRun);
                                if (PressReRunQuantityIndex - 1 == i)
                                {
                                    OverrideValue = dblPrintSpeed[i];
                                    //  return functionReturnValue;
                                }

                            }
                            else if (PressReRunMode == (int)PressReRunModes.NotReRun)
                            {
                                dblPrintSpeed[i] = dblSpeed2 - ((dblSpeed2 - dblSpeed1) * dblRun);
                            }
                            else if (PressReRunMode == (int)PressReRunModes.ReRunPress)
                            {
                                if (PressReRunQuantityIndex - 1 == i)
                                {
                                    dblPrintSpeed[i] = OverrideValue;
                                }
                            }


                            //Updating the Press Speed (Pro Rata)
                            if (PressReRunMode == (int)PressReRunModes.NotReRun)
                            {
                                oItemSection.PressSpeed1 = Convert.ToInt32(dblPrintSpeed[0]);
                                oItemSection.PressSpeed2 = Convert.ToInt32(dblPrintSpeed[1]);
                                oItemSection.PressSpeed3 = Convert.ToInt32(dblPrintSpeed[2]);

                                PressRunTime1 = Convert.ToDouble(intWorkSheetQty[0] / oItemSection.PressSpeed1);
                                PressRunTime2 = Convert.ToDouble(intWorkSheetQty[1] / oItemSection.PressSpeed2);
                                PressRunTime3 = Convert.ToDouble(intWorkSheetQty[2] / oItemSection.PressSpeed3);
                            }
                            else if (PressReRunMode == (int)PressReRunModes.ReRunPress)
                            {
                                if (PressReRunQuantityIndex == 1)
                                {
                                    oItemSection.PressSpeed1 = Convert.ToInt32(dblPrintSpeed[0]);
                                    PressRunTime1 = Convert.ToDouble(intWorkSheetQty[0] / oItemSection.PressSpeed1);
                                }
                                else if (PressReRunQuantityIndex == 2)
                                {
                                    oItemSection.PressSpeed2 = Convert.ToInt32(dblPrintSpeed[1]);
                                    PressRunTime2 = Convert.ToDouble(intWorkSheetQty[1] / oItemSection.PressSpeed2);
                                }
                                else if (PressReRunQuantityIndex == 3)
                                {
                                    oItemSection.PressSpeed3 = Convert.ToInt32(dblPrintSpeed[2]);
                                    PressRunTime3 = Convert.ToDouble(intWorkSheetQty[2] / oItemSection.PressSpeed3);
                                }

                            }
                            
                            

                            //Checing Whether Print is Double sided and Press can't perform perfecting
                            if (Convert.ToBoolean(oItemSection.IsDoubleSided) && !Convert.ToBoolean(oPressDTO.isPerfecting ?? false))
                            {
                                //Calculating and Setting Print Cost
                                dblPrintCost[i] = Convert.ToDouble((dblPassFront * ((intWorkSheetQty[i] / dblPrintSpeed[i]) * oModelSpeedWeight.hourlyCost)) + ((dblPassBack * ((intWorkSheetQty[i] / dblPrintSpeed[i]) * oModelSpeedWeight.hourlyCost)) + oPressDTO.SetupCharge));

                                dblPrintPrice[i] = Convert.ToDouble((dblPassFront * ((intWorkSheetQty[i] / dblPrintSpeed[i]) * oModelSpeedWeight.hourlyPrice)) + ((dblPassBack * ((intWorkSheetQty[i] / dblPrintSpeed[i]) * oModelSpeedWeight.hourlyPrice)) + oPressDTO.SetupCharge));

                                //Calculating and Setting Print Run
                                dblPrintRun[i] = (dblPassFront * intWorkSheetQty[i]) + (dblPassBack * intWorkSheetQty[i]);
                                

                            }
                            else
                            {
                                dblPrintCost[i] = Convert.ToDouble(dblPassFront * ((intWorkSheetQty[i] / dblPrintSpeed[i]) * oModelSpeedWeight.hourlyCost) + oPressDTO.SetupCharge);
                                dblPrintRun[i] = (dblPassFront * intWorkSheetQty[i]);

                                dblPrintPrice[i] = Convert.ToDouble(dblPassFront * ((intWorkSheetQty[i] / dblPrintSpeed[i]) * oModelSpeedWeight.hourlyPrice) + oPressDTO.SetupCharge);
                            }
                        }


                        if (PressReRunMode == (int)PressReRunModes.NotReRun)
                        {
                            PressCost1 = dblPrintCost[0];
                            PressCost2 = dblPrintCost[1];
                            PressCost3 = dblPrintCost[2];

                            PressPrice1 = dblPrintPrice[0];
                            PressPrice2 = dblPrintPrice[1];
                            PressPrice3 = dblPrintPrice[2];

                            oItemSection.PressHourlyCharge = oModelSpeedWeight.hourlyPrice;
                        }
                        else if (PressReRunMode == (int)PressReRunModes.ReRunPress)
                        {
                            if (PressReRunQuantityIndex == 1)
                            {
                                PressCost1 = dblPrintCost[0];
                                PressPrice1 = dblPrintPrice[0];
                            }
                            else if (PressReRunQuantityIndex == 2)
                            {
                                PressCost2 = dblPrintCost[1];
                                PressPrice2 = dblPrintPrice[1];
                            }
                            else if (PressReRunQuantityIndex == 3)
                            {
                                PressCost3 = dblPrintCost[2];
                                PressPrice3 = dblPrintPrice[2];
                            }
                        }

                        break;
                    ///''====================================================
                    case (int)MethodTypes.PerHour:
                        //PerHour
                        ///''====================================================
                        int intPrintChgeph = 0;
                        double dblCostph = 0;
                        double dblPriceph = 0;
                        //Getting Press lookup Information Query
                        MachinePerHourLookup oModelPerHourLookUp = itemsectionRepository.GetMachinePerHourLookup(oModelLookUpMethod.MethodId);

                        //Setting Print Charge

                        if (PressReRunMode == (int)PressReRunModes.CalculateValuesToShow)
                        {
                            intPrintChgeph = Convert.ToInt32(oModelPerHourLookUp.Speed);
                            OverrideValue = Convert.ToInt32(oModelPerHourLookUp.Speed);
                            // return functionReturnValue;
                        }
                        else if (PressReRunMode == (int)PressReRunModes.NotReRun)
                        {
                            intPrintChgeph = Convert.ToInt32(oModelPerHourLookUp.Speed);
                        }
                        else if (PressReRunMode == (int)PressReRunModes.ReRunPress)
                        {
                            intPrintChgeph = Convert.ToInt32(OverrideValue);
                        }
                        //Setting Print Charge Rate
                        dblCostph = Convert.ToDouble(oModelPerHourLookUp.SpeedCost);
                        dblPriceph = oModelPerHourLookUp.SpeedPrice;

                        for (int i = temp; i <= temp2; i++)
                        {
                            //Checking item is Double sided and Press can't perform perfecting
                            if (oItemSection.IsDoubleSided == true && oPressDTO.isPerfecting == false)
                            {
                                //Setting Print Cost
                                dblPrintCost[i] = Convert.ToDouble((dblPassFront * (intWorkSheetQty[i] / intPrintChgeph) * dblCostph) + (dblPassBack * (intWorkSheetQty[i] / intPrintChgeph) * dblCostph) + oPressDTO.SetupCharge);
                                dblPrintPrice[i] = Convert.ToDouble((dblPassFront * (intWorkSheetQty[i] / intPrintChgeph) * dblPriceph) + (dblPassBack * (intWorkSheetQty[i] / intPrintChgeph) * dblPriceph) + oPressDTO.SetupCharge);
                                dblPrintRun[i] = (dblPassFront * intWorkSheetQty[i]) + (dblPassBack * intWorkSheetQty[i]);
                                

                            }
                            else
                            {
                                //Setting Print Cost
                                dblPrintCost[i] = Convert.ToDouble((dblPassFront * (intWorkSheetQty[i] / intPrintChgeph) * dblCostph) + oPressDTO.SetupCharge);

                                dblPrintPrice[i] = Convert.ToDouble((dblPassFront * (intWorkSheetQty[i] / intPrintChgeph) * dblPriceph) + oPressDTO.SetupCharge);
                                //Setting Print Run 
                                dblPrintRun[i] = (dblPassFront * intWorkSheetQty[i]);
                            }
                        }


                        oItemSection.PressHourlyCharge = oModelPerHourLookUp.SpeedPrice;
                        if (PressReRunMode == (int)PressReRunModes.NotReRun)
                        {
                            oItemSection.PressSpeed1 = intPrintChgeph;
                            oItemSection.PressSpeed2 = intPrintChgeph;
                            oItemSection.PressSpeed3 = intPrintChgeph;

                            PressRunTime1 = Convert.ToDouble(intWorkSheetQty[0] / oItemSection.PressSpeed1);
                            PressRunTime2 = Convert.ToDouble(intWorkSheetQty[1] / oItemSection.PressSpeed2);
                            PressRunTime3 = Convert.ToDouble(intWorkSheetQty[2] / oItemSection.PressSpeed3);

                            PressCost1 = dblPrintCost[0];
                            PressCost2 = dblPrintCost[1];
                            PressCost3 = dblPrintCost[2];

                            PressPrice1 = dblPrintPrice[0];
                            PressPrice2 = dblPrintPrice[1];
                            PressPrice3 = dblPrintPrice[2];
                        }
                        else if (PressReRunMode == (int)PressReRunModes.ReRunPress)
                        {
                            if (PressReRunQuantityIndex == 1)
                            {
                                oItemSection.PressSpeed1 = intPrintChgeph;
                                PressRunTime1 = Convert.ToDouble(intWorkSheetQty[0] / oItemSection.PressSpeed1);
                                PressCost1 = dblPrintCost[0];
                                PressPrice1 = dblPrintPrice[0];
                            }
                            else if (PressReRunQuantityIndex == 2)
                            {
                                oItemSection.PressSpeed2 = intPrintChgeph;
                                PressRunTime2 = Convert.ToDouble(intWorkSheetQty[1] / oItemSection.PressSpeed2);
                                PressCost2 = dblPrintCost[1];
                                PressPrice2 = dblPrintPrice[1];
                            }
                            else if (PressReRunQuantityIndex == 3)
                            {
                                oItemSection.PressSpeed3 = intPrintChgeph;
                                PressRunTime3 = Convert.ToDouble(intWorkSheetQty[2] / oItemSection.PressSpeed3);
                                PressCost3 = dblPrintCost[2];
                                PressPrice3 = dblPrintPrice[2];
                            }
                        }

                        break;
                    ///==================================================
                    case (int)MethodTypes.ClickChargeZone:
                        //Click Charge Zone
                        ///==================================================
                        int[] intPrintChgeCZ = new int[3];
                        double[] dblCostCZ = new double[3];
                        double[] dblPriceCZ = new double[3];

                        int[] rngFrom = new int[15];
                        int[] rngTo = new int[15];
                        int[] dblClickSheet = new int[15];
                        double[] dblClickCost = new double[15];
                        double[] dblClickPrice = new double[15];

                        //Getting the Values for the LookUp Query

                        MachineClickChargeZone oModelClickChargeZone = itemsectionRepository.GetMachineClickChargeZone(oModelLookUpMethod.MethodId);
                        double TimePerChargeableSheets = Convert.ToDouble(oModelClickChargeZone.TimePerHour ?? 1);

                        //Setting the Range and Rate 
                        rngFrom[0] = Convert.ToInt32(oModelClickChargeZone.From1);
                        rngFrom[1] = Convert.ToInt32(oModelClickChargeZone.From2);
                        rngFrom[2] = Convert.ToInt32(oModelClickChargeZone.From3);
                        rngFrom[3] = Convert.ToInt32(oModelClickChargeZone.From4);
                        rngFrom[4] = Convert.ToInt32(oModelClickChargeZone.From5);
                        rngFrom[5] = Convert.ToInt32(oModelClickChargeZone.From6);
                        rngFrom[6] = Convert.ToInt32(oModelClickChargeZone.From7);
                        rngFrom[7] = Convert.ToInt32(oModelClickChargeZone.From8);
                        rngFrom[8] = Convert.ToInt32(oModelClickChargeZone.From9);
                        rngFrom[9] = Convert.ToInt32(oModelClickChargeZone.From10);
                        rngFrom[10] = Convert.ToInt32(oModelClickChargeZone.From11);
                        rngFrom[11] = Convert.ToInt32(oModelClickChargeZone.From12);
                        rngFrom[12] = Convert.ToInt32(oModelClickChargeZone.From13);
                        rngFrom[13] = Convert.ToInt32(oModelClickChargeZone.From14);
                        rngFrom[14] = Convert.ToInt32(oModelClickChargeZone.From15);
                        rngTo[0] = Convert.ToInt32(oModelClickChargeZone.To1);
                        rngTo[1] = Convert.ToInt32(oModelClickChargeZone.To2);
                        rngTo[2] = Convert.ToInt32(oModelClickChargeZone.To3);
                        rngTo[3] = Convert.ToInt32(oModelClickChargeZone.To4);
                        rngTo[4] = Convert.ToInt32(oModelClickChargeZone.To5);
                        rngTo[5] = Convert.ToInt32(oModelClickChargeZone.To6);
                        rngTo[6] = Convert.ToInt32(oModelClickChargeZone.To7);
                        rngTo[7] = Convert.ToInt32(oModelClickChargeZone.To8);
                        rngTo[8] = Convert.ToInt32(oModelClickChargeZone.To9);
                        rngTo[9] = Convert.ToInt32(oModelClickChargeZone.To10);
                        rngTo[10] = Convert.ToInt32(oModelClickChargeZone.To11);
                        rngTo[11] = Convert.ToInt32(oModelClickChargeZone.To12);
                        rngTo[12] = Convert.ToInt32(oModelClickChargeZone.To13);
                        rngTo[13] = Convert.ToInt32(oModelClickChargeZone.To14);
                        rngTo[14] = int.MaxValue;
                        dblClickCost[0] = Convert.ToDouble(oModelClickChargeZone.SheetCost1);
                        dblClickCost[1] = Convert.ToDouble(oModelClickChargeZone.SheetCost2);
                        dblClickCost[2] = Convert.ToDouble(oModelClickChargeZone.SheetCost3);
                        dblClickCost[3] = Convert.ToDouble(oModelClickChargeZone.SheetCost4);
                        dblClickCost[4] = Convert.ToDouble(oModelClickChargeZone.SheetCost5);
                        dblClickCost[5] = Convert.ToDouble(oModelClickChargeZone.SheetCost6);
                        dblClickCost[6] = Convert.ToDouble(oModelClickChargeZone.SheetCost7);
                        dblClickCost[7] = Convert.ToDouble(oModelClickChargeZone.SheetCost8);
                        dblClickCost[8] = Convert.ToDouble(oModelClickChargeZone.SheetCost9);
                        dblClickCost[9] = Convert.ToDouble(oModelClickChargeZone.SheetCost10);
                        dblClickCost[10] = Convert.ToDouble(oModelClickChargeZone.SheetCost11);
                        dblClickCost[11] = Convert.ToDouble(oModelClickChargeZone.SheetCost12);
                        dblClickCost[12] = Convert.ToDouble(oModelClickChargeZone.SheetCost13);
                        dblClickCost[13] = Convert.ToDouble(oModelClickChargeZone.SheetCost14);
                        dblClickCost[14] = Convert.ToDouble(oModelClickChargeZone.SheetCost15);
                        dblClickPrice[0] = Convert.ToDouble(oModelClickChargeZone.SheetPrice1);
                        dblClickPrice[1] = Convert.ToDouble(oModelClickChargeZone.SheetPrice2);
                        dblClickPrice[2] = Convert.ToDouble(oModelClickChargeZone.SheetPrice3);
                        dblClickPrice[3] = Convert.ToDouble(oModelClickChargeZone.SheetPrice4);
                        dblClickPrice[4] = Convert.ToDouble(oModelClickChargeZone.SheetPrice5);
                        dblClickPrice[5] = Convert.ToDouble(oModelClickChargeZone.SheetPrice6);
                        dblClickPrice[6] = Convert.ToDouble(oModelClickChargeZone.SheetPrice7);
                        dblClickPrice[7] = Convert.ToDouble(oModelClickChargeZone.SheetPrice8);
                        dblClickPrice[8] = Convert.ToDouble(oModelClickChargeZone.SheetPrice9);
                        dblClickPrice[9] = Convert.ToDouble(oModelClickChargeZone.SheetPrice10);
                        dblClickPrice[10] = Convert.ToDouble(oModelClickChargeZone.SheetPrice11);
                        dblClickPrice[11] = Convert.ToDouble(oModelClickChargeZone.SheetPrice12);
                        dblClickPrice[12] = Convert.ToDouble(oModelClickChargeZone.SheetPrice13);
                        dblClickPrice[13] = Convert.ToDouble(oModelClickChargeZone.SheetPrice14);
                        dblClickPrice[14] = Convert.ToDouble(oModelClickChargeZone.SheetPrice15);
                        dblClickSheet[0] = Convert.ToInt32(oModelClickChargeZone.Sheets1);
                        dblClickSheet[1] = Convert.ToInt32(oModelClickChargeZone.Sheets2);
                        dblClickSheet[2] = Convert.ToInt32(oModelClickChargeZone.Sheets3);
                        dblClickSheet[3] = Convert.ToInt32(oModelClickChargeZone.Sheets4);
                        dblClickSheet[4] = Convert.ToInt32(oModelClickChargeZone.Sheets5);
                        dblClickSheet[5] = Convert.ToInt32(oModelClickChargeZone.Sheets6);
                        dblClickSheet[6] = Convert.ToInt32(oModelClickChargeZone.Sheets7);
                        dblClickSheet[7] = Convert.ToInt32(oModelClickChargeZone.Sheets8);
                        dblClickSheet[8] = Convert.ToInt32(oModelClickChargeZone.Sheets9);
                        dblClickSheet[9] = Convert.ToInt32(oModelClickChargeZone.Sheets10);
                        dblClickSheet[10] = Convert.ToInt32(oModelClickChargeZone.Sheets11);
                        dblClickSheet[11] = Convert.ToInt32(oModelClickChargeZone.Sheets12);
                        dblClickSheet[12] = Convert.ToInt32(oModelClickChargeZone.Sheets13);
                        dblClickSheet[13] = Convert.ToInt32(oModelClickChargeZone.Sheets14);
                        dblClickSheet[14] = Convert.ToInt32(oModelClickChargeZone.Sheets15);
                        double[] intTEMPPrintQty = new double[5];
                        //Checking whether it is Accumulative Rate Across all parts

                        if (!(oModelClickChargeZone.isaccumulativecharge == 0))
                        {
                            //Setting the Original Print sheet Quantities

                            intTEMPPrintQty[0] = intWorkSheetQty[0];
                            intTEMPPrintQty[1] = intWorkSheetQty[1];
                            intTEMPPrintQty[2] = intWorkSheetQty[2];
                            intTEMPPrintQty[3] = intWorkSheetQty[3];
                            intTEMPPrintQty[4] = intWorkSheetQty[4];

                            //Setting the Total Print Sheet Quantity to AccumulateSheetQty Including All Spoilage and Current Section

                            intWorkSheetQty[0] = Convert.ToInt32(oItemSection.PrintSheetQty1);
                            intWorkSheetQty[1] = Convert.ToInt32(oItemSection.PrintSheetQty2);
                            intWorkSheetQty[2] = Convert.ToInt32(oItemSection.PrintSheetQty3);

                        }
                        //Looping through Multiple Quantities
                        for (int i = 0; i <= 2; i++)
                        {
                            //Checking Total Passes in the Press
                            //Setting the Print Charge and Print Charge Rate according to the LookUp
                            //*The LookUp show few other ranges
                            if ((intWorkSheetQty[i] * (dblPassFront + dblPassBack)) >= rngFrom[0] && (intWorkSheetQty[i] * (dblPassFront + dblPassBack)) <= rngTo[0])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[0];
                                dblCostCZ[i] = dblClickCost[0];
                                dblPriceCZ[i] = dblClickPrice[0];
                            }
                            else if ((intWorkSheetQty[i] * (dblPassFront + dblPassBack)) >= rngFrom[1] && (intWorkSheetQty[i] * (dblPassFront + dblPassBack)) <= rngTo[1])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[1];
                                dblCostCZ[i] = dblClickCost[1];
                                dblPriceCZ[i] = dblClickPrice[1];
                            }
                            else if ((intWorkSheetQty[i] * (dblPassFront + dblPassBack)) >= rngFrom[2] && (intWorkSheetQty[i] * (dblPassFront + dblPassBack)) <= rngTo[2])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[2];
                                dblCostCZ[i] = dblClickCost[2];
                                dblPriceCZ[i] = dblClickPrice[2];
                            }
                            else if ((intWorkSheetQty[i] * (dblPassFront + dblPassBack)) >= rngFrom[3] && (intWorkSheetQty[i] * (dblPassFront + dblPassBack)) <= rngTo[3])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[3];
                                dblCostCZ[i] = dblClickCost[3];
                                dblPriceCZ[i] = dblClickPrice[3];
                            }
                            else if ((intWorkSheetQty[i] * (dblPassFront + dblPassBack)) >= rngFrom[4] && (intWorkSheetQty[i] * (dblPassFront + dblPassBack)) <= rngTo[4])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[4];
                                dblCostCZ[i] = dblClickCost[4];
                                dblPriceCZ[i] = dblClickPrice[4];
                            }
                            else if ((intWorkSheetQty[i] * (dblPassFront + dblPassBack)) >= rngFrom[5] && (intWorkSheetQty[i] * (dblPassFront + dblPassBack)) <= rngTo[5])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[5];
                                dblCostCZ[i] = dblClickCost[5];
                                dblPriceCZ[i] = dblClickPrice[5];
                            }
                            else if ((intWorkSheetQty[i] * (dblPassFront + dblPassBack)) >= rngFrom[6] && (intWorkSheetQty[i] * (dblPassFront + dblPassBack)) <= rngTo[6])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[6];
                                dblCostCZ[i] = dblClickCost[6];
                                dblPriceCZ[i] = dblClickPrice[6];
                            }
                            else if ((intWorkSheetQty[i] * (dblPassFront + dblPassBack)) >= rngFrom[7] && (intWorkSheetQty[i] * (dblPassFront + dblPassBack)) <= rngTo[7])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[7];
                                dblCostCZ[i] = dblClickCost[7];
                                dblPriceCZ[i] = dblClickPrice[7];
                            }
                            else if ((intWorkSheetQty[i] * (dblPassFront + dblPassBack)) >= rngFrom[8] && (intWorkSheetQty[i] * (dblPassFront + dblPassBack)) <= rngTo[8])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[8];
                                dblCostCZ[i] = dblClickCost[8];
                                dblPriceCZ[i] = dblClickPrice[8];
                            }
                            else if ((intWorkSheetQty[i] * (dblPassFront + dblPassBack)) >= rngFrom[9] && (intWorkSheetQty[i] * (dblPassFront + dblPassBack)) <= rngTo[9])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[9];
                                dblCostCZ[i] = dblClickCost[9];
                                dblPriceCZ[i] = dblClickPrice[9];
                            }
                            else if ((intWorkSheetQty[i] * (dblPassFront + dblPassBack)) >= rngFrom[10] && (intWorkSheetQty[i] * (dblPassFront + dblPassBack)) <= rngTo[10])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[10];
                                dblCostCZ[i] = dblClickCost[10];
                                dblPriceCZ[i] = dblClickPrice[10];
                            }
                            else if ((intWorkSheetQty[i] * (dblPassFront + dblPassBack)) >= rngFrom[11] && (intWorkSheetQty[i] * (dblPassFront + dblPassBack)) <= rngTo[11])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[11];
                                dblCostCZ[i] = dblClickCost[11];
                                dblPriceCZ[i] = dblClickPrice[11];
                            }
                            else if ((intWorkSheetQty[i] * (dblPassFront + dblPassBack)) >= rngFrom[12] && (intWorkSheetQty[i] * (dblPassFront + dblPassBack)) <= rngTo[12])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[12];
                                dblCostCZ[i] = dblClickCost[12];
                                dblPriceCZ[i] = dblClickPrice[12];
                            }
                            else if ((intWorkSheetQty[i] * (dblPassFront + dblPassBack)) >= rngFrom[13] && (intWorkSheetQty[i] * (dblPassFront + dblPassBack)) <= rngTo[13])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[13];
                                dblCostCZ[i] = dblClickCost[13];
                                dblPriceCZ[i] = dblClickPrice[13];
                            }
                            else if ((intWorkSheetQty[i] * (dblPassFront + dblPassBack)) >= rngFrom[14] && (intWorkSheetQty[i] * (dblPassFront + dblPassBack)) <= rngTo[14])
                            {
                                intPrintChgeCZ[i] = dblClickSheet[14];
                                dblCostCZ[i] = dblClickCost[14];
                                dblPriceCZ[i] = dblClickPrice[14];
                            }
                        }

                        //Checking whether if press is Accumulative click charge 
                        if (!(oModelClickChargeZone.isaccumulativecharge == 0))
                        {
                            intWorkSheetQty[0] = intTEMPPrintQty[0];
                            intWorkSheetQty[1] = intTEMPPrintQty[1];
                            intWorkSheetQty[2] = intTEMPPrintQty[2];
                            intWorkSheetQty[3] = intTEMPPrintQty[3];
                            intWorkSheetQty[4] = intTEMPPrintQty[4];
                        }

                        if (PressReRunMode == (int)PressReRunModes.CalculateValuesToShow)
                        {
                            OverrideValue = dblPriceCZ[PressReRunQuantityIndex - 1];
                            //return functionReturnValue;
                        }
                        else if (PressReRunMode == (int)PressReRunModes.NotReRun)
                        {
                            //do nothing
                            //intPrintChge = oModelPerHourLookUp.Speed
                        }
                        else if (PressReRunMode == (int)PressReRunModes.ReRunPress)
                        {
                            dblPriceCZ[PressReRunQuantityIndex - 1] = OverrideValue;
                        }
                        for (int i = temp; i <= temp2; i++)
                        {
                            //Checking whether press is can't perform perfecting 
                            if (oItemSection.IsDoubleSided == true && oPressDTO.isPerfecting == false)
                            {
                                //Calculating 3000 print quantity with single sided no spoilage with per 1000 22$
                                //Print Cost=(1*(3000/1000)*22) =66
                                dblPrintCost[i] = Convert.ToDouble(((dblPassFront * (intWorkSheetQty[i] / intPrintChgeCZ[i]) * dblCostCZ[i]) + (dblPassBack * (intWorkSheetQty[i] / intPrintChgeCZ[i]) * dblCostCZ[i]) + oPressDTO.SetupCharge));

                                dblPrintPrice[i] = Convert.ToDouble(((dblPassFront * (intWorkSheetQty[i] / intPrintChgeCZ[i]) * dblPriceCZ[i]) + (dblPassBack * (intWorkSheetQty[i] / intPrintChgeCZ[i]) * dblPriceCZ[i]) + oPressDTO.SetupCharge));

                                //Calculating Print Run
                                //dblPrintRun(i) = (dblPassFront * intWorkSheetQty(0)) + (dblPassBack * intWorkSheetQty(0))
                                dblPrintRun[i] = Convert.ToInt32(dblPassFront * intWorkSheetQty[i] / intPrintChgeCZ[i] + dblPassBack * intWorkSheetQty[i] / intPrintChgeCZ[i]);
                            }
                            else
                            {
                                //For Other than Perfecting
                                dblPrintCost[i] = Convert.ToDouble(((dblPassFront * (intWorkSheetQty[i] / intPrintChgeCZ[i]) * dblCostCZ[i]) + oPressDTO.SetupCharge));
                                dblPrintPrice[i] = Convert.ToDouble(((dblPassFront * (intWorkSheetQty[i] / intPrintChgeCZ[i]) * dblPriceCZ[i]) + oPressDTO.SetupCharge));
                                //dblPrintRun(i) = (dblPassFront * intWorkSheetQty(i))
                                dblPrintRun[i] = Convert.ToInt32(dblPassFront * intWorkSheetQty[i] / intPrintChgeCZ[i]);
                            }
                        }

                        if (PressReRunMode == (int)PressReRunModes.NotReRun)
                        {
                            oItemSection.PressHourlyCharge = dblPrintRun[0] / (intPrintChgeCZ[0] / TimePerChargeableSheets) * dblCostCZ[0];
                            oItemSection.PressSpeed1 = Convert.ToInt32(dblPrintRun[0]);
                            oItemSection.PressSpeed2 = Convert.ToInt32(dblPrintRun[1]);
                            oItemSection.PressSpeed3 = Convert.ToInt32(dblPrintRun[2]);

                            PressRunTime1 = Convert.ToDouble((oItemSection.PressSpeed1 / TimePerChargeableSheets));
                            PressRunTime2 = Convert.ToDouble((oItemSection.PressSpeed2 / TimePerChargeableSheets));
                            PressRunTime3 = Convert.ToDouble((oItemSection.PressSpeed3 / TimePerChargeableSheets));

                            PressCost1 = dblPrintCost[0];
                            PressCost2 = dblPrintCost[1];
                            PressCost3 = dblPrintCost[2];

                            PressPrice1 = dblPrintPrice[0];
                            PressPrice2 = dblPrintPrice[1];
                            PressPrice3 = dblPrintPrice[2];
                        }
                        else if (PressReRunMode == (int)PressReRunModes.ReRunPress)
                        {
                            oItemSection.PressHourlyCharge = dblPrintRun[0] / (intPrintChgeCZ[0] / TimePerChargeableSheets) * dblCostCZ[0];
                            if (PressReRunQuantityIndex == 1)
                            {
                                oItemSection.PressSpeed1 = Convert.ToInt32(dblPrintRun[0]);
                                PressRunTime1 = Convert.ToDouble((oItemSection.PressSpeed1 / TimePerChargeableSheets));
                                PressCost1 = dblPrintCost[0];
                                PressPrice1 = dblPrintPrice[0];
                            }
                            else if (PressReRunQuantityIndex == 2)
                            {
                                oItemSection.PressSpeed2 = Convert.ToInt32(dblPrintRun[1]);
                                PressRunTime2 = Convert.ToDouble((oItemSection.PressSpeed2 / TimePerChargeableSheets));
                                PressCost2 = dblPrintCost[1];
                                PressPrice2 = dblPrintPrice[1];
                            }
                            else if (PressReRunQuantityIndex == 3)
                            {
                                oItemSection.PressSpeed3 = Convert.ToInt32(dblPrintRun[2]);
                                PressRunTime3 = Convert.ToDouble((oItemSection.PressSpeed3 / TimePerChargeableSheets));
                                PressCost3 = dblPrintCost[2];
                                PressPrice3 = dblPrintPrice[2];
                            }
                        }
                        break;
                }
                //End LookUp Calculations


                for (int i = temp; i <= temp2; i++)
                {
                    //Checking whether Print Cost is Less than Press Minimum Charge if it is then setting to Press Minimum Charge
                    if (dblPrintPrice[i] < oPressDTO.MinimumCharge)
                    {
                        dblPrintPrice[i] = Convert.ToDouble(oPressDTO.MinimumCharge);
                        sMinimumCost += "1";
                    }
                    else
                    {
                        sMinimumCost += "0";
                    }
                }

                //Updating the 'Print Charge (excl Make Readies) (Pro Rata)
                if (PressReRunMode == (int)PressReRunModes.NotReRun)
                {
                    oItemSection.PrintChargeExMakeReady1 = dblPrintPrice[0] - oPressDTO.SetupCharge;
                    oItemSection.PrintChargeExMakeReady2 = dblPrintPrice[1] - oPressDTO.SetupCharge;
                    oItemSection.PrintChargeExMakeReady3 = dblPrintPrice[2] - oPressDTO.SetupCharge;
                }
                else
                {
                    if (PressReRunQuantityIndex == 1)
                    {
                        oItemSection.PrintChargeExMakeReady1 = dblPrintPrice[0] - oPressDTO.SetupCharge;
                    }
                    else if (PressReRunQuantityIndex == 2)
                    {
                        oItemSection.PrintChargeExMakeReady2 = dblPrintPrice[1] - oPressDTO.SetupCharge;
                    }
                    else if (PressReRunQuantityIndex == 3)
                    {
                        oItemSection.PrintChargeExMakeReady3 = dblPrintPrice[2] - oPressDTO.SetupCharge;
                    }

                }
                //===============================================================================================
                //Updating the press object   
                //oItemSection.Press = oModelPress
                SectionCostcentre oItemSectionCostCenter = new SectionCostcentre();
                if (IsReRun == false)
                {
                    oItemSectionCostCenter.ItemSectionId = oItemSection.ItemSectionId;
                    oItemSectionCostCenter.CostCentreId = (int)oCostCentreDTO.CostCentreId;
                    oItemSectionCostCenter.SystemCostCentreType = (int)SystemCostCenterTypes.Press;
                    oItemSectionCostCenter.Order = 107;
                    oItemSectionCostCenter.IsOptionalExtra = 0;
                    oItemSectionCostCenter.CostCentreType = 1;
                    oItemSectionCostCenter.IsDirectCost = Convert.ToInt16(oPressDTO.DirectCost);
                    oItemSectionCostCenter.SectionCostCentreDetails = new List<SectionCostCentreDetail>();
                }
                else
                {
                    //
                }

                oItemSectionCostCenter.SetupTime = Convert.ToDouble((oPressDTO.SetupTime == 0 ? 0 : oPressDTO.SetupTime / 60));
                oItemSectionCostCenter.IsPrintable = Convert.ToInt16(oJobCardOptionsDTO.IsDefaultPressInstruction);
                //Getting Markup Query
                double ProfitMargin = 0;
                var markup = itemsectionRepository.GetMarkupById(oCostCentreDTO.DefaultVAId);
                if (markup != null)
                    ProfitMargin = Convert.ToDouble(markup.MarkUpRate);
                if (oItemSection.Qty1 > 0)
                {
                    if (PressReRunMode == (int)PressReRunModes.NotReRun)
                    {
                        //Setting Cost to Print Cost calculated + Press Setup Cost
                        oItemSectionCostCenter.Qty1Charge = dblPrintPrice[0];
                        oItemSectionCostCenter.Qty1MarkUpID = oCostCentreDTO.DefaultVAId;
                        oItemSectionCostCenter.Qty1MarkUpValue = oItemSectionCostCenter.Qty1Charge * ProfitMargin / 100;
                        oItemSectionCostCenter.Qty1NetTotal = oItemSectionCostCenter.Qty1Charge + oItemSectionCostCenter.Qty1MarkUpValue;
                        oItemSectionCostCenter.Qty1EstimatedTime = Math.Round(PressRunTime1 + oItemSectionCostCenter.SetupTime, 2);
                        oItemSectionCostCenter.Qty1EstimatedPlantCost = dblPrintCost[0];

                        oItemSection.ImpressionQty1 = Convert.ToInt32(intWorkSheetQty[0] * (dblPassBack + dblPassFront) * Convert.ToInt32((Convert.ToBoolean(oItemSection.IsDoubleSided) == true ? 2 : 1)));

                        if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsDefaultPressInstruction == true)
                        {

                            if (oJobCardOptionsDTO.IsWorkingSize == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions = "Working Size:=" + oItemSection.SectionSizeHeight + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.SectionSizeWidth + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsItemSize == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += "Item Size (Flat):= " + oItemSection.ItemSizeHeight + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.ItemSizeWidth + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsPrintSheetQty == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += "Print Sheet Qty:= " + intWorkSheetQty[0] + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsNumberOfPasses == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += "Number of Passes:=" + (dblPassBack + dblPassFront).ToString() + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsImpressionCount == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += "Impression Count:= " + oItemSection.ImpressionQty1.ToString() + Environment.NewLine;
                            }

                            if (oJobCardOptionsDTO.IsPressEstTime == true)
                            {
                                //oItemSectionCostCenter.Qty1WorkInstructions += "Estimated Time:= " + Math.Round(oItemSectionCostCenter.Qty1EstimatedTime, 2).ToString("f") + " hours";
                            }
                        }
                    }
                    else if (PressReRunMode == (int)PressReRunModes.ReRunPress && PressReRunQuantityIndex == 1)
                    {
                        //Setting Cost to Print Cost calculated + Press Setup Cost
                        oItemSectionCostCenter.Qty1Charge = dblPrintPrice[0];
                        oItemSectionCostCenter.Qty1MarkUpID = oCostCentreDTO.DefaultVAId;
                        oItemSectionCostCenter.Qty1MarkUpValue = oItemSectionCostCenter.Qty1Charge * ProfitMargin / 100;
                        oItemSectionCostCenter.Qty1NetTotal = oItemSectionCostCenter.Qty1Charge + oItemSectionCostCenter.Qty1MarkUpValue;
                        oItemSectionCostCenter.Qty1EstimatedTime = Math.Round(PressRunTime1 + oItemSectionCostCenter.SetupTime, 2);
                        oItemSectionCostCenter.Qty1EstimatedPlantCost = dblPrintCost[0];

                        oItemSection.ImpressionQty1 = Convert.ToInt32(intWorkSheetQty[0] * (dblPassBack + dblPassFront) * Convert.ToInt32((Convert.ToBoolean(oItemSection.IsDoubleSided) == true ? 2 : 1)));

                        if (IsWorkInstructionsLocked == false && oJobCardOptionsDTO.IsDefaultPressInstruction == true)
                        {
                            if (oJobCardOptionsDTO.IsWorkingSize == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions = "Working Size:=" + oItemSection.SectionSizeHeight + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.SectionSizeWidth + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsItemSize == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += "Item Size (Flat):= " + oItemSection.ItemSizeHeight + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.ItemSizeWidth + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsPrintSheetQty == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += "Print Sheet Qty:= " + intWorkSheetQty[0] + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsNumberOfPasses == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += "Number of Passes:=" + (dblPassBack + dblPassFront).ToString() + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsImpressionCount == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += "Impression Count:= " + oItemSection.ImpressionQty1.ToString() + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsPressEstTime == true)
                            {
                                // oItemSectionCostCenter.Qty1WorkInstructions += "Estimated Time:= " + Math.Round(oItemSectionCostCenter.Qty1EstimatedTime, 2).ToString("f") + " hours";
                            }
                        }

                    }
                }

                if (oItemSection.Qty2 > 0)
                {
                    if (PressReRunMode == (int)PressReRunModes.NotReRun)
                    {
                        oItemSectionCostCenter.Qty2Charge = dblPrintPrice[1];
                        oItemSectionCostCenter.Qty2MarkUpID = oCostCentreDTO.DefaultVAId;
                        oItemSectionCostCenter.Qty2MarkUpValue = Convert.ToDouble(oItemSectionCostCenter.Qty2Charge * ProfitMargin / 100);
                        oItemSectionCostCenter.Qty2NetTotal = oItemSectionCostCenter.Qty2Charge + oItemSectionCostCenter.Qty2MarkUpValue;
                        oItemSectionCostCenter.Qty2EstimatedTime = Math.Round(PressRunTime2 + oItemSectionCostCenter.SetupTime, 2);
                        oItemSectionCostCenter.Qty2EstimatedPlantCost = dblPrintCost[1];

                        oItemSection.ImpressionQty2 = Convert.ToInt32(intWorkSheetQty[1] * (dblPassBack + dblPassFront) * Convert.ToInt32((Convert.ToBoolean(oItemSection.IsDoubleSided) == true ? 2 : 1)));


                        if (IsWorkInstructionsLocked == false && oJobCardOptionsDTO.IsDefaultPressInstruction == true)
                        {
                            if (oJobCardOptionsDTO.IsWorkingSize == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions = "Working Size:=" + oItemSection.SectionSizeHeight + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.SectionSizeWidth + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsItemSize == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += "Item Size (Flat):= " + oItemSection.ItemSizeHeight + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.ItemSizeWidth + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsPrintSheetQty == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += "Print Sheet Qty:= " + intWorkSheetQty[1] + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsNumberOfPasses == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += "Number of Passes:=" + (dblPassBack + dblPassFront).ToString() + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsImpressionCount == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += "Impression Count:= " + oItemSection.ImpressionQty2.ToString() + Environment.NewLine;
                            }

                            if (oJobCardOptionsDTO.IsPressEstTime == true)
                            {
                                //oItemSectionCostCenter.Qty2WorkInstructions += "Estimated Time:= " + Math.Round(oItemSectionCostCenter.Qty2EstimatedTime, 2).ToString("f") + " hours";
                            }

                        }

                    }
                    else if (PressReRunMode == (int)PressReRunModes.ReRunPress && PressReRunQuantityIndex == 2)
                    {
                        oItemSectionCostCenter.Qty2Charge = dblPrintPrice[1];
                        oItemSectionCostCenter.Qty2MarkUpID = oCostCentreDTO.DefaultVAId;
                        oItemSectionCostCenter.Qty2MarkUpValue = Convert.ToDouble(oItemSectionCostCenter.Qty2Charge * ProfitMargin / 100);
                        oItemSectionCostCenter.Qty2NetTotal = oItemSectionCostCenter.Qty2Charge + oItemSectionCostCenter.Qty2MarkUpValue;
                        oItemSectionCostCenter.Qty2EstimatedTime = Math.Round(PressRunTime2 + oItemSectionCostCenter.SetupTime, 2);
                        oItemSectionCostCenter.Qty2EstimatedPlantCost = dblPrintCost[1];

                        oItemSection.ImpressionQty2 = Convert.ToInt32(intWorkSheetQty[1] * (dblPassBack + dblPassFront) * Convert.ToInt32((Convert.ToBoolean(oItemSection.IsDoubleSided) == true ? 2 : 1)));
                        if (IsWorkInstructionsLocked == false && oJobCardOptionsDTO.IsDefaultPressInstruction == true)
                        {
                            if (oJobCardOptionsDTO.IsWorkingSize == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions = "Working Size:=" + oItemSection.SectionSizeHeight + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.SectionSizeWidth + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsItemSize == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += "Item Size (Flat):= " + oItemSection.ItemSizeHeight + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.ItemSizeWidth + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsPrintSheetQty == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += "Print Sheet Qty:= " + intWorkSheetQty[1] + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsNumberOfPasses == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += "Number of Passes:=" + (dblPassBack + dblPassFront).ToString() + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsImpressionCount == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += "Impression Count:= " + oItemSection.ImpressionQty2.ToString() + Environment.NewLine;
                            }

                            if (oJobCardOptionsDTO.IsPressEstTime == true)
                            {
                                //oItemSectionCostCenter.Qty2WorkInstructions += "Estimated Time:= " + Math.Round(oItemSectionCostCenter.Qty2EstimatedTime, 2).ToString("f") + " hours";
                            }

                        }
                    }
                }

                if (oItemSection.Qty3 > 0)
                {
                    if (PressReRunMode == (int)PressReRunModes.NotReRun)
                    {
                        oItemSectionCostCenter.Qty3Charge = dblPrintPrice[2];
                        oItemSectionCostCenter.Qty3MarkUpID = oCostCentreDTO.DefaultVAId;
                        oItemSectionCostCenter.Qty3MarkUpValue = oItemSectionCostCenter.Qty2Charge * ProfitMargin / 100;
                        oItemSectionCostCenter.Qty3NetTotal = oItemSectionCostCenter.Qty3Charge + oItemSectionCostCenter.Qty3MarkUpValue;
                        oItemSectionCostCenter.Qty3EstimatedTime = Math.Round(PressRunTime3 + oItemSectionCostCenter.SetupTime, 2);
                        oItemSectionCostCenter.Qty3EstimatedPlantCost = dblPrintCost[2];

                        oItemSection.ImpressionQty3 = Convert.ToInt32(intWorkSheetQty[2] * (dblPassBack + dblPassFront) * Convert.ToInt32((Convert.ToBoolean(oItemSection.IsDoubleSided) == true ? 2 : 1)));

                        if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsDefaultPressInstruction == true)
                        {
                            if (oJobCardOptionsDTO.IsWorkingSize == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions = "Working Size:=" + oItemSection.SectionSizeHeight + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.SectionSizeWidth + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsItemSize == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += "Item Size (Flat):= " + oItemSection.ItemSizeHeight + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.ItemSizeWidth + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsPrintSheetQty == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += "Print Sheet Qty:= " + intWorkSheetQty[2] + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsNumberOfPasses == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += "Number of Passes:=" + (dblPassBack + dblPassFront).ToString() + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsImpressionCount == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += "Impression Count:= " + oItemSection.ImpressionQty3.ToString() + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsPressEstTime == true)
                            {
                                //oItemSectionCostCenter.Qty3WorkInstructions += "Estimated Time:= " + Math.Round(oItemSectionCostCenter.Qty3EstimatedTime, 2).ToString("f") + " hours";
                            }

                        }
                    }
                    else if (PressReRunMode == (int)PressReRunModes.ReRunPress & PressReRunQuantityIndex == 3)
                    {
                        oItemSectionCostCenter.Qty3Charge = dblPrintPrice[2];
                        oItemSectionCostCenter.Qty3MarkUpID = oCostCentreDTO.DefaultVAId;
                        oItemSectionCostCenter.Qty3MarkUpValue = oItemSectionCostCenter.Qty2Charge * ProfitMargin / 100;
                        oItemSectionCostCenter.Qty3NetTotal = oItemSectionCostCenter.Qty3Charge + oItemSectionCostCenter.Qty3MarkUpValue;
                        oItemSectionCostCenter.Qty3EstimatedTime = Math.Round(PressRunTime3 + oItemSectionCostCenter.SetupTime, 2);
                        oItemSectionCostCenter.Qty3EstimatedPlantCost = dblPrintCost[2];

                        oItemSection.ImpressionQty3 = Convert.ToInt32(intWorkSheetQty[2] * (dblPassBack + dblPassFront) * Convert.ToInt32((Convert.ToBoolean(oItemSection.IsDoubleSided) == true ? 2 : 1)));

                        if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsDefaultPressInstruction == true)
                        {
                            if (oJobCardOptionsDTO.IsWorkingSize == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions = "Working Size:=" + oItemSection.SectionSizeHeight + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.SectionSizeWidth + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsItemSize == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += "Item Size (Flat):= " + oItemSection.ItemSizeHeight + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.ItemSizeWidth + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsPrintSheetQty == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += "Print Sheet Qty:= " + intWorkSheetQty[2] + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsNumberOfPasses == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += "Number of Passes:=" + (dblPassBack + dblPassFront).ToString() + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsImpressionCount == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += "Impression Count:= " + oItemSection.ImpressionQty3.ToString() + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsPressEstTime == true)
                            {
                                // oItemSectionCostCenter.Qty3WorkInstructions += "Estimated Time:= " + Math.Round(oItemSectionCostCenter.Qty3EstimatedTime, 2).ToString("f") + " hours";
                            }
                        }
                    }
                }

                if (sMinimumCost != string.Empty)
                {
                    oItemSectionCostCenter.IsMinimumCost = Convert.ToInt16(sMinimumCost);
                }
                else
                {
                    oItemSectionCostCenter.IsMinimumCost = 0;
                }


                //Setting up the QTY in the object
                // Model.Items.ItemSectionCostCentreDetailDTO oItemSectionCostCenterDetail = default(Model.Items.ItemSectionCostCentreDetailDTO);
                SectionCostCentreDetail oItemSectionCostCenterDetail;
                if (IsReRun == false)
                {
                    oItemSectionCostCenterDetail = new SectionCostCentreDetail();
                }
                else
                {
                    oItemSectionCostCenterDetail = oItemSectionCostCenter.SectionCostCentreDetails.FirstOrDefault();

                }
                oItemSectionCostCenterDetail.Qty1 = 1;
                if (oItemSection.Qty2 > 0)
                {
                    oItemSectionCostCenterDetail.Qty2 = 1;
                }
                if (oItemSection.Qty3 > 0)
                {
                    oItemSectionCostCenterDetail.Qty3 = 1;
                }

                //oItemSectionCostCenterDetail.StockId = oItemSection.PressID;
                oItemSectionCostCenterDetail.CostPrice = dblPrintCost[0];
                //Naveed: condition added for setting name of the press while calling this function to get best press list from service
                if (isBestPress)
                    oItemSectionCostCenter.Name = oPressDTO.MachineName;
                else
                    oItemSectionCostCenter.Name = "Press ( " + oPressDTO.MachineName + " )";
                oItemSectionCostCenter.Qty1 = oItemSection.Qty1;
                oItemSectionCostCenter.Qty2 = oItemSection.Qty2;
                oItemSectionCostCenter.Qty3 = oItemSection.Qty3;
                //handling costcentre resources
                //oItemSectionCostCenter.SectionCostCentreResources.ToList().ForEach(c => db.SectionCostCentreResources.Remove(c));
                //foreach (var orow in oItemSectionCostCenter.SectionCostCentreResources)
                //{
                //    oResourceDto = new SectionCostCentreResource { ResourceId = orow.ResourceId, SectionCostcentreId = oItemSectionCostCenter.SectionCostcentreId };
                //    oItemSectionCostCenter.SectionCostCentreResources.Add(oResourceDto);
                //}
                if (IsReRun == false)
                {
                    if (oItemSectionCostCenter.SectionCostCentreDetails == null)
                    {
                        oItemSectionCostCenter.SectionCostCentreDetails = new List<SectionCostCentreDetail>();
                    }
                    oItemSectionCostCenter.SectionCostCentreDetails.Add(oItemSectionCostCenterDetail);
                    if (oItemSection.SectionCostcentres == null)
                    {
                        oItemSection.SectionCostcentres = new List<SectionCostcentre>();
                    }
                    oItemSection.SectionCostcentres.Add(oItemSectionCostCenter);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("CalculatePressCost", ex);
            }

            return oItemSection;//.tbl_section_costcentres.ToList();
        }

        public ItemSection CalculatePlateCost(ItemSection oItemSection, bool IsReRun = false, bool IsWorkInstructionsLocked = false, bool isSide1 = true)
        {

            // oItemSection.SectionCostcentres.ToList().ForEach(c => oItemSection.SectionCostcentres.Remove(c));

            JobPreference oJobCardOptionsDTO = itemsectionRepository.GetJobPreferences(1);
            bool IsSectionCostCentreFoundInReRun = false;
            string sMinimumCost = null;
            SectionCostCentreResource oResourceDto;

            double dblPlateUnitCost = 0;
            double dblPlateUnitPrice = 0;
            double dblPlateCost = 0;
            double dblPlatePrice = 0;
            double dblPlateProcessingtCost = 0;
            double dblPlateProcessingPrice = 0;

            StockItem oPlateDTO = itemsectionRepository.GetStockById(Convert.ToInt64(oItemSection.PlateId));
            CostCentre oPlateCostCentreDTO = itemsectionRepository.GetCostCenterBySystemType((int)SystemCostCenterTypes.Plate);
            double dblItemProcessingCharge = 0;
            //int PlatesQty = (oItemSection.Side1PlateQty > 0 ? (int)oItemSection.Side1PlateQty : 0) + (oItemSection.Side2PlateQty > 0 ? (int)oItemSection.Side2PlateQty : 0); //Commented when code changed for both sides calculation seperately
            int PlatesQty = isSide1 ? oItemSection.Side1PlateQty ?? 0 : oItemSection.Side2PlateQty ?? 0;
            if (oItemSection.IsPlateUsed != false && oItemSection.IsPlateSupplied == false)
            {
                int PlateID = Convert.ToInt32(oItemSection.PlateId);
                GlobalData gData = GetItemPriceCost(PlateID, true);
                if (gData != null)
                {
                    dblPlateUnitCost = gData.dblUnitCost;
                    dblPlateUnitPrice = gData.dblUnitPrice;
                    dblPlateProcessingtCost = gData.dblPlateProcessingtCost;
                    dblPlateProcessingPrice = gData.dblPlateProcessingPrice;
                }

                dblPlateUnitCost = oPlateDTO.PerQtyQty > 0 ? dblPlateUnitCost / Convert.ToDouble(oPlateDTO.PerQtyQty) : 0;
                dblPlateUnitPrice = oPlateDTO.PerQtyQty > 0 ? dblPlateUnitPrice / Convert.ToDouble(oPlateDTO.PerQtyQty) : 0;

                dblItemProcessingCharge = oPlateDTO.ItemProcessingCharge > 0 ? Convert.ToDouble(oPlateDTO.ItemProcessingCharge) : 0;

                dblPlateCost = PlatesQty > 0 ? PlatesQty * (dblPlateUnitCost + dblPlateProcessingtCost) : 0;
                dblPlateUnitCost += dblPlateProcessingtCost;

                dblPlatePrice = PlatesQty > 0 ? PlatesQty * (dblPlateUnitPrice + dblPlateProcessingPrice) : 0;
                dblPlateUnitPrice += dblPlateProcessingPrice;
            }
            else
            {
                dblPlateCost = 0;
                dblPlateUnitCost = 0;
                dblPlatePrice = 0;
                dblPlateUnitPrice = 0;
            }

            SectionCostcentre oItemSectionCostCenter = null;
            if (IsReRun == false)
            {
                oItemSectionCostCenter = new SectionCostcentre();

                oItemSectionCostCenter.ItemSectionId = oItemSection.ItemSectionId;
                oItemSectionCostCenter.CostCentreId = oPlateCostCentreDTO.CostCentreId;  //13 is Plate Type cost center
                oItemSectionCostCenter.SystemCostCentreType = (int)SystemCostCenterTypes.Plate; // Cost Center system type 14;
                oItemSectionCostCenter.Order = 104;
                oItemSectionCostCenter.IsOptionalExtra = 0;
                oItemSectionCostCenter.CostCentreType = 1;
                oItemSectionCostCenter.IsDirectCost = Convert.ToInt16(oPlateCostCentreDTO.IsDirectCost);
                oItemSectionCostCenter.IsPrintable = oJobCardOptionsDTO.IsdefaultPlateUsed != null ? oJobCardOptionsDTO.IsdefaultPlateUsed == true ? (short)1 : (short)0 : (short)0;
                oItemSectionCostCenter.SectionCostCentreDetails = new List<SectionCostCentreDetail>();
            }
            else
            {
                foreach (var secCostCenter in oItemSection.SectionCostcentres)
                {
                    if (secCostCenter.SystemCostCentreType == 4)
                    {
                        IsSectionCostCentreFoundInReRun = true;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
                if (IsSectionCostCentreFoundInReRun == true)
                {
                    //
                }
                else
                {
                    oItemSectionCostCenter = new SectionCostcentre();

                    oItemSectionCostCenter.ItemSectionId = oItemSection.ItemSectionId;
                    oItemSectionCostCenter.CostCentreId = oPlateCostCentreDTO.CostCentreId;
                    oItemSectionCostCenter.SystemCostCentreType = (int)SystemCostCenterTypes.Plate;
                    oItemSectionCostCenter.Order = 104;
                    oItemSectionCostCenter.IsOptionalExtra = 0;
                    oItemSectionCostCenter.CostCentreType = 4;
                    oItemSectionCostCenter.IsDirectCost = Convert.ToInt16(oPlateCostCentreDTO.IsDirectCost);
                    oItemSectionCostCenter.IsPrintable = oJobCardOptionsDTO.IsdefaultPlateUsed != null ? oJobCardOptionsDTO.IsdefaultPlateUsed == true ? (short)1 : (short)0 : (short)0;

                }
            }

            oItemSectionCostCenter.Qty1Charge = dblPlatePrice;

            if (oItemSectionCostCenter.Qty1Charge < oPlateCostCentreDTO.MinimumCost && PlatesQty > 0)
            {
                oItemSectionCostCenter.Qty1Charge = oPlateCostCentreDTO.MinimumCost;
                sMinimumCost = "1";
            }
            else
            {
                sMinimumCost = "0";
            }
            var markup = itemsectionRepository.GetMarkupById(oPlateCostCentreDTO.DefaultVAId);
            var ProfitMargin = markup != null ? markup.MarkUpRate : 0;

            oItemSectionCostCenter.Qty1MarkUpID = oPlateCostCentreDTO.DefaultVAId;
            oItemSectionCostCenter.Qty1MarkUpValue = oItemSectionCostCenter.Qty1Charge * ProfitMargin / 100;
            oItemSectionCostCenter.Qty1NetTotal = oItemSectionCostCenter.Qty1Charge + oItemSectionCostCenter.Qty1MarkUpValue;
            oItemSectionCostCenter.Qty1EstimatedStockCost = dblPlateCost;
            oItemSection.BaseCharge1 += oItemSectionCostCenter.Qty1NetTotal;

            if (IsWorkInstructionsLocked == false && oJobCardOptionsDTO.IsdefaultPlateUsed == true)
            {
                if (Convert.ToBoolean(oItemSection.IsPlateSupplied) == false)
                    oItemSectionCostCenter.Qty1WorkInstructions = "Plate Qty:= " + PlatesQty + Environment.NewLine + "Plate Supplied:= No";
                else
                    oItemSectionCostCenter.Qty1WorkInstructions = "Plate Qty:= " + PlatesQty + Environment.NewLine + "Plate Supplied:= Yes";
            }

            if (oItemSection.Qty2 > 0)
            {
                oItemSectionCostCenter.Qty2Charge = dblPlatePrice;

                if (oItemSectionCostCenter.Qty2Charge < oPlateCostCentreDTO.MinimumCost & PlatesQty > 0)
                {
                    oItemSectionCostCenter.Qty2Charge = oPlateCostCentreDTO.MinimumCost;
                    sMinimumCost += "1";
                }
                else
                {
                    sMinimumCost += "0";
                }

                oItemSectionCostCenter.Qty2MarkUpID = oPlateCostCentreDTO.DefaultVAId;
                oItemSectionCostCenter.Qty2MarkUpValue = oItemSectionCostCenter.Qty2Charge * ProfitMargin / 100;
                oItemSectionCostCenter.Qty2NetTotal = oItemSectionCostCenter.Qty2Charge + oItemSectionCostCenter.Qty2MarkUpValue;
                oItemSectionCostCenter.Qty2EstimatedStockCost = dblPlateCost;
                if (IsWorkInstructionsLocked == false && oJobCardOptionsDTO.IsdefaultPlateUsed == true)
                {
                    if (Convert.ToBoolean(oItemSection.IsPlateSupplied) == false)
                        oItemSectionCostCenter.Qty2WorkInstructions = "Plate Qty:= " + PlatesQty + Environment.NewLine + "Plate Supplied:= No";
                    else
                        oItemSectionCostCenter.Qty2WorkInstructions = "Plate Qty:= " + PlatesQty + Environment.NewLine + "Plate Supplied:= Yes";
                }

            }
            if (oItemSection.Qty3 > 0)
            {
                oItemSectionCostCenter.Qty3Charge = dblPlatePrice;

                if (oItemSectionCostCenter.Qty3Charge < oPlateCostCentreDTO.MinimumCost & PlatesQty > 0)
                {
                    oItemSectionCostCenter.Qty3Charge = oPlateCostCentreDTO.MinimumCost;
                    sMinimumCost += "1";
                }
                else
                    sMinimumCost += "0";

                oItemSectionCostCenter.Qty3MarkUpID = oPlateCostCentreDTO.DefaultVAId;
                oItemSectionCostCenter.Qty3MarkUpValue = oItemSectionCostCenter.Qty3Charge * ProfitMargin / 100;
                oItemSectionCostCenter.Qty3NetTotal = oItemSectionCostCenter.Qty3Charge + oItemSectionCostCenter.Qty3MarkUpValue;

                oItemSectionCostCenter.Qty3EstimatedStockCost = dblPlateCost;
                if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsdefaultPlateUsed == true)
                {
                    if (Convert.ToBoolean(oItemSection.IsPlateSupplied) == false)
                        oItemSectionCostCenter.Qty3WorkInstructions = "Plate Qty:= " + PlatesQty + Environment.NewLine + "Plate Supplied:= No";
                    else
                        oItemSectionCostCenter.Qty3WorkInstructions = "Plate Qty:= " + PlatesQty + Environment.NewLine + "Plate Supplied:= Yes";
                }

            }
            if (sMinimumCost != string.Empty)
                oItemSectionCostCenter.IsMinimumCost = Convert.ToInt16(sMinimumCost);
            else
                oItemSectionCostCenter.IsMinimumCost = 0;

            SectionCostCentreDetail oItemSectionCostCenterDetail;
            if (IsReRun == false | IsSectionCostCentreFoundInReRun == false)
                oItemSectionCostCenterDetail = new SectionCostCentreDetail();
            else
                oItemSectionCostCenterDetail = oItemSectionCostCenter.SectionCostCentreDetails.FirstOrDefault();

            if (oItemSection.Qty1 > 0)
            {
                oItemSectionCostCenterDetail.Qty1 = Convert.ToDouble(PlatesQty);
                if (oItemSection.Qty2 > 0)
                    oItemSectionCostCenterDetail.Qty2 = Convert.ToDouble(PlatesQty);
                if (oItemSection.Qty3 > 0)
                    oItemSectionCostCenterDetail.Qty3 = Convert.ToDouble(PlatesQty);

            }
            else
            {
                oItemSectionCostCenterDetail.Qty1 = 0;
                oItemSectionCostCenterDetail.Qty2 = 0;
                oItemSectionCostCenterDetail.Qty3 = 0;

            }

            if (oItemSection.IsPlateSupplied == false)
                oItemSectionCostCenterDetail.CostPrice = dblPlateUnitPrice;
            else
                oItemSectionCostCenterDetail.CostPrice = 0;

            oItemSectionCostCenterDetail.StockId = oPlateDTO.StockItemId;
            oItemSectionCostCenterDetail.StockName = oPlateDTO.ItemName;
            oItemSectionCostCenter.Qty1 = oItemSection.Qty1;
            oItemSectionCostCenter.Qty2 = oItemSection.Qty2;
            oItemSectionCostCenter.Qty3 = oItemSection.Qty3;


            oItemSectionCostCenterDetail.SupplierId = Convert.ToInt32(oPlateDTO.SupplierId);

            oItemSectionCostCenter.Name = string.Format("Plate Side {0} ({1}) ", isSide1 ? 1 : 2, oPlateDTO.ItemName);// "Plate ( " + oPlateDTO.ItemName + " )" + isSide1 ? "Side 1" : "Side 2";


            if (IsReRun == false || IsSectionCostCentreFoundInReRun == false)
            {
                oItemSectionCostCenter.SectionCostCentreDetails.Add(oItemSectionCostCenterDetail);
                oItemSection.SectionCostcentres.Add(oItemSectionCostCenter);
            }

            return oItemSection;
        }

        public ItemSection CalculateWashUpCost(ItemSection objSection, int PressID, bool IsReRun = false, bool IsWorkInstructionsLocked = false, bool isSide1 = true)
        {
            // objSection.SectionCostcentres.ToList().ForEach(c => objSection.SectionCostcentres.Remove(c));
            JobPreference oJobCardOptionsDTO = itemsectionRepository.GetJobPreferences(1);
            bool IsSectionCostCentreFoundInReRun = false;
            string sMinimumCost = null;
            double dblWashUpCost = 0;
            double dblWashupPrice = 0;
            ItemSection oItemSection = objSection;
            Machine oPressDTO = itemsectionRepository.GetPressById(PressID);
            SectionCostCentreResource oResourceDto;
            CostCentre oCostCentreDTO;
            SectionCostcentre oItemSectionCostCenter;
            CostCentre oWashupCostCentreDTO = itemsectionRepository.GetCostCenterBySystemType((int)SystemCostCenterTypes.Washup);

            dblWashUpCost = Convert.ToDouble(oItemSection.WashupQty * oPressDTO.WashupCost);
            dblWashupPrice = Convert.ToDouble(oItemSection.WashupQty * oPressDTO.WashupPrice);
            if (IsReRun == false)
            {
                oItemSectionCostCenter = new SectionCostcentre();

                oItemSectionCostCenter.ItemSectionId = oItemSection.ItemSectionId;
                oItemSectionCostCenter.CostCentreId = oWashupCostCentreDTO.CostCentreId; ;
                oItemSectionCostCenter.SystemCostCentreType = (int)SystemCostCenterTypes.Washup;
                oItemSectionCostCenter.Order = 106;
                oItemSectionCostCenter.IsOptionalExtra = (short)0;
                oItemSectionCostCenter.CostCentreType = 1;
                oItemSectionCostCenter.IsDirectCost = Convert.ToInt16(oWashupCostCentreDTO.IsDirectCost);
                oItemSectionCostCenter.SectionCostCentreDetails = new List<SectionCostCentreDetail>();

            }
            else
            {
                foreach (var sectionCC in oItemSection.SectionCostcentres)
                {
                    if (sectionCC.SystemCostCentreType == (int)SystemCostCenterTypes.Washup)
                    {
                        IsSectionCostCentreFoundInReRun = true;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }

                if (IsSectionCostCentreFoundInReRun == true)
                {
                    oItemSectionCostCenter = oItemSection.SectionCostcentres.FirstOrDefault();
                }
                else
                {
                    oItemSectionCostCenter = new SectionCostcentre();
                    oItemSectionCostCenter.ItemSectionId = oItemSection.ItemSectionId;
                    oItemSectionCostCenter.CostCentreId = oWashupCostCentreDTO != null ? oWashupCostCentreDTO.CostCentreId : 18;
                    oItemSectionCostCenter.SystemCostCentreType = (int)SystemCostCenterTypes.Washup;
                    oItemSectionCostCenter.Order = 106;
                    oItemSectionCostCenter.IsOptionalExtra = 0;
                    oItemSectionCostCenter.CostCentreType = 1;
                    oItemSectionCostCenter.IsDirectCost = Convert.ToInt16(oWashupCostCentreDTO.IsDirectCost);
                    oItemSectionCostCenter.SectionCostCentreDetails = new List<SectionCostCentreDetail>();
                }
            }

            //oItemSectionCostCenter.IsPrintable = Convert.ToInt16(oJobCardOptionsDTO.IsDefaultWashupUsed);
            if (oItemSection.IsWashup != false)
            {
                oItemSectionCostCenter.Qty1Charge = dblWashupPrice;
                if (oItemSectionCostCenter.Qty1Charge < oWashupCostCentreDTO.MinimumCost && oItemSection.WashupQty > 0)
                {
                    oItemSectionCostCenter.Qty1Charge = oWashupCostCentreDTO.MinimumCost;
                    sMinimumCost = "1";
                }
                else
                {
                    sMinimumCost = "0";
                }
                double ProfitMargin = 0;
                var markup = itemsectionRepository.GetMarkupById(oWashupCostCentreDTO.DefaultVAId);
                if (markup != null)
                    ProfitMargin = Convert.ToDouble(markup.MarkUpRate);

                oItemSectionCostCenter.Qty1MarkUpID = oWashupCostCentreDTO.DefaultVAId;
                oItemSectionCostCenter.Qty1MarkUpValue = oItemSectionCostCenter.Qty1Charge * ProfitMargin / 100;
                oItemSectionCostCenter.Qty1NetTotal = oItemSectionCostCenter.Qty1Charge + oItemSectionCostCenter.Qty1MarkUpValue;

                oItemSectionCostCenter.Qty1EstimatedPlantCost = dblWashUpCost;
                oItemSectionCostCenter.Qty1EstimatedTime = Math.Round(Convert.ToDouble(oItemSection.WashupQty * oPressDTO.WashupTime) / 60, 2);

                if (IsWorkInstructionsLocked == false && oJobCardOptionsDTO.IsDefaultWashupUsed == true)
                {
                    oItemSectionCostCenter.Qty1WorkInstructions = "WashUp Qty:= " + oItemSection.WashupQty.ToString() + Environment.NewLine;

                    if (oJobCardOptionsDTO.IsPressEstTime == true)
                    {
                        //oItemSectionCostCenter.Qty1WorkInstructions += " Estimated Time:= " + Math.Round(oItemSectionCostCenter.Qty1EstimatedTime, 2).ToString() + " hours";
                    }
                }

                if (oItemSection.Qty2 > 0)
                {
                    oItemSectionCostCenter.Qty2Charge = dblWashupPrice;

                    if (oItemSectionCostCenter.Qty2Charge < oWashupCostCentreDTO.MinimumCost & oItemSection.WashupQty > 0)
                    {
                        oItemSectionCostCenter.Qty2Charge = oWashupCostCentreDTO.MinimumCost;
                        sMinimumCost += "1";
                    }
                    else
                    {
                        sMinimumCost += "0";
                    }
                    oItemSectionCostCenter.Qty2MarkUpID = oWashupCostCentreDTO.DefaultVAId;
                    oItemSectionCostCenter.Qty2MarkUpValue = oItemSectionCostCenter.Qty2Charge * ProfitMargin / 100;
                    oItemSectionCostCenter.Qty2NetTotal = oItemSectionCostCenter.Qty2Charge + oItemSectionCostCenter.Qty2MarkUpValue;
                    oItemSectionCostCenter.Qty2EstimatedPlantCost = dblWashUpCost;
                    oItemSectionCostCenter.Qty2EstimatedTime = Math.Round(Convert.ToDouble((oItemSection.WashupQty * oPressDTO.WashupTime) / 60), 2);

                    if (IsWorkInstructionsLocked == false && oJobCardOptionsDTO.IsDefaultWashupUsed == true)
                    {
                        oItemSectionCostCenter.Qty2WorkInstructions = "WashUp Qty:= " + oItemSection.WashupQty.ToString() + Environment.NewLine;

                        if (oJobCardOptionsDTO.IsPressEstTime == true)
                        {
                            //oItemSectionCostCenter.Qty2WorkInstructions += " Estimated Time:= " + Math.Round(oItemSectionCostCenter.Qty2EstimatedTime, 2).ToString() + " hours";
                        }
                    }
                }
                if (oItemSection.Qty3 > 0)
                {
                    oItemSectionCostCenter.Qty3Charge = dblWashupPrice;

                    if (oItemSectionCostCenter.Qty3Charge < oWashupCostCentreDTO.MinimumCost && oItemSection.WashupQty > 0)
                    {
                        oItemSectionCostCenter.Qty3Charge = oWashupCostCentreDTO.MinimumCost;
                        sMinimumCost += "1";
                    }
                    else
                    {
                        sMinimumCost += "0";
                    }
                    oItemSectionCostCenter.Qty3MarkUpID = oWashupCostCentreDTO.DefaultVAId;
                    oItemSectionCostCenter.Qty3MarkUpValue = oItemSectionCostCenter.Qty3Charge * ProfitMargin / 100;
                    oItemSectionCostCenter.Qty3NetTotal = oItemSectionCostCenter.Qty3Charge + oItemSectionCostCenter.Qty3MarkUpValue;
                    oItemSectionCostCenter.Qty3EstimatedPlantCost = dblWashUpCost;
                    oItemSectionCostCenter.Qty1EstimatedTime = Math.Round(Convert.ToDouble(oItemSection.WashupQty * oPressDTO.WashupTime) / 60, 2);

                    if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsDefaultWashupUsed == true)
                    {
                        oItemSectionCostCenter.Qty3WorkInstructions = "WashUp Qty:= " + oItemSection.WashupQty.ToString() + Environment.NewLine;
                        if (oJobCardOptionsDTO.IsPressEstTime == true)
                        {
                            // oItemSectionCostCenter.Qty3WorkInstructions += " Estimated Time:= " + Math.Round(oItemSectionCostCenter.Qty3EstimatedTime, 2).ToString() + " hours";
                        }
                    }
                }


                if (sMinimumCost != string.Empty)
                {
                    oItemSectionCostCenter.IsMinimumCost = Convert.ToInt16(sMinimumCost);
                }
                else
                {
                    oItemSectionCostCenter.IsMinimumCost = 0;
                }
            }

            SectionCostCentreDetail oItemSectionCostCenterDetail;

            if (IsReRun == false || IsSectionCostCentreFoundInReRun == false)
            {
                oItemSectionCostCenterDetail = new SectionCostCentreDetail();
            }
            else
            {
                oItemSectionCostCenterDetail = oItemSectionCostCenter.SectionCostCentreDetails.FirstOrDefault();
            }

            if (oItemSection.Qty1 > 0)
            {
                oItemSectionCostCenterDetail.Qty1 = Convert.ToDouble(oItemSection.WashupQty);
            }
            if (oItemSection.Qty2 > 0)
            {
                oItemSectionCostCenterDetail.Qty2 = Convert.ToDouble(oItemSection.WashupQty);
            }
            if (oItemSection.Qty3 > 0)
            {
                oItemSectionCostCenterDetail.Qty3 = Convert.ToDouble(oItemSection.WashupQty);
            }

            if (!(oItemSection.IsWashup == false))
            {
                oItemSectionCostCenterDetail.CostPrice = dblWashupPrice;
            }
            else
            {
                oItemSectionCostCenterDetail.CostPrice = 0;
            }
            string side = isSide1 ? "Sdie 1" : "Side 2";
            oItemSectionCostCenter.Name = "Washups " + side;
            oItemSectionCostCenter.Qty1 = oItemSection.Qty1;
            oItemSectionCostCenter.Qty2 = oItemSection.Qty2;
            oItemSectionCostCenter.Qty3 = oItemSection.Qty3;

            if (IsReRun == false || IsSectionCostCentreFoundInReRun == false)
            {
                oItemSectionCostCenter.SectionCostCentreDetails.Add(oItemSectionCostCenterDetail);
                oItemSection.SectionCostcentres.Add(oItemSectionCostCenter);
            }

            return oItemSection;
        }

        public ItemSection CalculateReelMakeReadyCost(ItemSection oItemSection, int PressID, bool IsReRun = false, bool IsWorkInstructionsLocked = false)
        {
            // oItemSection.SectionCostcentres.ToList().ForEach(c => oItemSection.SectionCostcentres.Remove(c));
            JobPreference oJobCardOptionsDTO = itemsectionRepository.GetJobPreferences(1);
            string sMinimumCost = null;
            double ReelMakeReadyCost = 0;
            double ReelMakeReadyPrice = 0;
            SectionCostCentreResource oResourceDto;
            Machine oPressDTO = itemsectionRepository.GetPressById(PressID);
            CostCentre oMakeReadyCostCentreDTO = itemsectionRepository.GetCostCenterBySystemType((int)SystemCostCenterTypes.ReelMakeready);

            ReelMakeReadyCost = Convert.ToDouble(oItemSection.WebReelMakereadyQty * oPressDTO.ReelMRCost);

            ReelMakeReadyPrice = Convert.ToDouble(oItemSection.WebReelMakereadyQty * oPressDTO.ReelMRPrice);

            SectionCostcentre oItemSectionCostCenter;
            if (IsReRun == false)
            {
                oItemSectionCostCenter = new SectionCostcentre();
                oItemSectionCostCenter.ItemSectionId = oItemSection.ItemSectionId;
                oItemSectionCostCenter.CostCentreId = oMakeReadyCostCentreDTO.CostCentreId;
                oItemSectionCostCenter.SystemCostCentreType = 12;
                oItemSectionCostCenter.Order = 105;
                oItemSectionCostCenter.IsOptionalExtra = 0;
                oItemSectionCostCenter.CostCentreType = 1;
                oItemSectionCostCenter.IsDirectCost = Convert.ToInt16(oMakeReadyCostCentreDTO.IsDirectCost);
                oItemSectionCostCenter.SectionCostCentreDetails = new List<SectionCostCentreDetail>();
            }
            else
            {
                foreach (var sectionCC in oItemSection.SectionCostcentres)
                {
                    if (sectionCC.SystemCostCentreType == (int)SystemCostCenterTypes.ReelMakeready)
                    {
                        oItemSectionCostCenter = sectionCC;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
                oItemSectionCostCenter = oItemSection.SectionCostcentres.FirstOrDefault();
            }

            oItemSectionCostCenter.IsPrintable = Convert.ToInt16(oJobCardOptionsDTO.IsDefaultMakereadyUsed);

            if (!(oItemSection.IsMakeReadyUsed == false))
            {
                if (ReelMakeReadyPrice < oMakeReadyCostCentreDTO.MinimumCost && oItemSection.WebReelMakereadyQty > 0)
                {
                    oItemSectionCostCenter.Qty1Charge = oMakeReadyCostCentreDTO.MinimumCost;
                    sMinimumCost = "1";
                }
                else
                {
                    oItemSectionCostCenter.Qty1Charge = ReelMakeReadyPrice;
                    sMinimumCost = "0";
                }

                var markup = itemsectionRepository.GetMarkupById(oMakeReadyCostCentreDTO.DefaultVAId);
                var ProfitMargin = markup != null ? markup.MarkUpRate : 0;
                oItemSectionCostCenter.Qty1MarkUpID = oMakeReadyCostCentreDTO.DefaultVAId;
                oItemSectionCostCenter.Qty1MarkUpValue = oItemSectionCostCenter.Qty1Charge * ProfitMargin / 100;
                oItemSectionCostCenter.Qty1NetTotal = oItemSectionCostCenter.Qty1Charge + oItemSectionCostCenter.Qty1MarkUpValue;

                oItemSectionCostCenter.Qty1EstimatedTime = Math.Round(Convert.ToDouble(oPressDTO.ReelMakereadyTime / 60 * oItemSection.WebReelMakereadyQty), 2);

                if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsReelMakeReady == true)
                {
                    oItemSectionCostCenter.Qty1WorkInstructions = "Reel MakeReadies:= " + oItemSection.WebReelMakereadyQty.ToString() + Environment.NewLine;

                    if (oJobCardOptionsDTO.IsReelMakeReadyTime == true)
                    {
                        // oItemSectionCostCenter.Qty1WorkInstructions += "Estimated Time := " + Math.Round(oItemSectionCostCenter.Qty1EstimatedTime, 2).ToString() + " hours";
                    }
                }

                oItemSectionCostCenter.Qty1EstimatedPlantCost = ReelMakeReadyCost;

                if (oItemSection.Qty2 > 0)
                {
                    if (ReelMakeReadyPrice < oMakeReadyCostCentreDTO.MinimumCost && oItemSection.WebReelMakereadyQty > 0)
                    {
                        oItemSectionCostCenter.Qty2Charge = oMakeReadyCostCentreDTO.MinimumCost;
                        sMinimumCost += "1";
                    }
                    else
                    {
                        oItemSectionCostCenter.Qty2Charge = ReelMakeReadyPrice;
                        sMinimumCost += "0";
                    }

                    oItemSectionCostCenter.Qty2MarkUpID = oMakeReadyCostCentreDTO.DefaultVAId;
                    oItemSectionCostCenter.Qty2MarkUpValue = oItemSectionCostCenter.Qty2Charge * ProfitMargin / 100;
                    oItemSectionCostCenter.Qty2NetTotal = oItemSectionCostCenter.Qty2Charge + oItemSectionCostCenter.Qty2MarkUpValue;
                    oItemSectionCostCenter.Qty2EstimatedTime = Math.Round(Convert.ToDouble(oPressDTO.ReelMakereadyTime / 60 * oItemSection.WebReelMakereadyQty), 2);

                    if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsReelMakeReady == true)
                    {
                        oItemSectionCostCenter.Qty2WorkInstructions = "Reel MakeReadies:= " + oItemSection.WebReelMakereadyQty.ToString() + Environment.NewLine;

                        if (oJobCardOptionsDTO.IsReelMakeReadyTime == true)
                        {
                            //oItemSectionCostCenter.Qty2WorkInstructions += "Estimated Time := " + Math.Round(oItemSectionCostCenter.Qty2EstimatedTime, 2).ToString() + " hours";
                        }
                    }

                    oItemSectionCostCenter.Qty2EstimatedPlantCost = ReelMakeReadyCost;

                }
                if (oItemSection.Qty3 > 0)
                {
                    if (ReelMakeReadyPrice < oMakeReadyCostCentreDTO.MinimumCost && oItemSection.WebReelMakereadyQty > 0)
                    {
                        oItemSectionCostCenter.Qty3Charge = oMakeReadyCostCentreDTO.MinimumCost;
                        sMinimumCost += "1";
                    }
                    else
                    {
                        oItemSectionCostCenter.Qty3Charge = ReelMakeReadyPrice;
                        sMinimumCost += "0";
                    }

                    oItemSectionCostCenter.Qty3MarkUpID = oMakeReadyCostCentreDTO.DefaultVAId;
                    oItemSectionCostCenter.Qty3MarkUpValue = oItemSectionCostCenter.Qty3Charge * ProfitMargin / 100;
                    oItemSectionCostCenter.Qty3NetTotal = oItemSectionCostCenter.Qty3Charge + oItemSectionCostCenter.Qty3MarkUpValue;
                    oItemSectionCostCenter.Qty3EstimatedTime = Math.Round(Convert.ToDouble(oPressDTO.ReelMakereadyTime / 60 * oItemSection.WebReelMakereadyQty), 2);

                    if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsReelMakeReady == true)
                    {
                        oItemSectionCostCenter.Qty3WorkInstructions = "Reel MakeReadies:= " + oItemSection.WebReelMakereadyQty.ToString() + Environment.NewLine;

                        if (oJobCardOptionsDTO.IsReelMakeReadyTime == true)
                        {
                            //oItemSectionCostCenter.Qty3WorkInstructions += "Estimated Time := " + Math.Round(oItemSectionCostCenter.Qty3EstimatedTime, 2).ToString() + " hours";
                        }
                    }

                    oItemSectionCostCenter.Qty3EstimatedPlantCost = ReelMakeReadyCost;

                }
            }
            else
            {
                oItemSectionCostCenter.Qty1Charge = 0;
                oItemSectionCostCenter.Qty2Charge = 0;
                oItemSectionCostCenter.Qty3Charge = 0;
            }

            if (sMinimumCost != string.Empty)
            {
                oItemSectionCostCenter.IsMinimumCost = Convert.ToInt16(sMinimumCost);
            }
            else
            {
                oItemSectionCostCenter.IsMinimumCost = 0;
            }
            SectionCostCentreDetail oItemSectionCostCenterDetail;
            if (IsReRun == false)
            {
                oItemSectionCostCenterDetail = new SectionCostCentreDetail();
            }
            else
            {
                oItemSectionCostCenterDetail = oItemSectionCostCenter.SectionCostCentreDetails.FirstOrDefault();
            }

            if (oItemSection.Qty1 > 0)
            {
                oItemSectionCostCenterDetail.Qty1 = Convert.ToDouble(oItemSection.WebReelMakereadyQty);
            }
            if (oItemSection.Qty2 > 0)
            {
                oItemSectionCostCenterDetail.Qty2 = Convert.ToDouble(oItemSection.WebReelMakereadyQty);
            }
            if (oItemSection.Qty3 > 0)
            {
                oItemSectionCostCenterDetail.Qty3 = Convert.ToDouble(oItemSection.WebReelMakereadyQty);
            }

            if (!(oItemSection.IsMakeReadyUsed == false))
            {
                oItemSectionCostCenterDetail.CostPrice = oPressDTO.MakeReadyCost;
            }
            else
            {
                oItemSectionCostCenterDetail.CostPrice = 0;
            }

            oItemSectionCostCenter.Name = "Reel Makeready";
            oItemSectionCostCenter.Qty1 = oItemSection.Qty1;
            oItemSectionCostCenter.Qty2 = oItemSection.Qty2;
            oItemSectionCostCenter.Qty3 = oItemSection.Qty3;

            if (IsReRun == false)
            {
                oItemSectionCostCenter.SectionCostCentreDetails.Add(oItemSectionCostCenterDetail);
                oItemSection.SectionCostcentres.Add(oItemSectionCostCenter);
            }
            return oItemSection;
        }

        public ItemSection CalculateMakeReadyCost(ItemSection oItemSection, int PressID, bool IsReRun = false, bool IsWorkInstructionsLocked = false, bool isSide1 = true)
        {

            JobPreference oJobCardOptionsDTO = itemsectionRepository.GetJobPreferences(1);
            bool IsSectionCostCentreFoundInReRun = false;
            string sMinimumCost = null;
            double dblMakeReadyCost = 0;
            double dblMakeReadyPrice = 0;
            SectionCostCentreResource oResourceDto;
            Machine oPressDTO = itemsectionRepository.GetPressById(PressID);
            CostCentre oMakeReadyCostCentreDTO = itemsectionRepository.GetCostCenterBySystemType((int)SystemCostCenterTypes.Makeready);


            dblMakeReadyCost = Convert.ToDouble(oItemSection.MakeReadyQty * oPressDTO.MakeReadyCost);
            dblMakeReadyPrice = Convert.ToDouble(oItemSection.MakeReadyQty * oPressDTO.MakeReadyPrice);

            SectionCostcentre oItemSectionCostCenter;
            if (IsReRun == false)
            {
                oItemSectionCostCenter = new SectionCostcentre();
                oItemSectionCostCenter.ItemSectionId = oItemSection.ItemSectionId;
                oItemSectionCostCenter.CostCentreId = oMakeReadyCostCentreDTO.CostCentreId;
                oItemSectionCostCenter.SystemCostCentreType = (int)SystemCostCenterTypes.Makeready;
                oItemSectionCostCenter.Order = 105;
                oItemSectionCostCenter.IsOptionalExtra = 0;
                oItemSectionCostCenter.CostCentreType = 1;
                oItemSectionCostCenter.IsDirectCost = Convert.ToInt16(oMakeReadyCostCentreDTO.IsDirectCost);
            }
            else
            {
                SectionCostcentre sectionCC = oItemSection.SectionCostcentres.Where(cc => cc.SystemCostCentreType == (int)SystemCostCenterTypes.Makeready).FirstOrDefault();
                if (sectionCC != null)
                    oItemSectionCostCenter = sectionCC;
                else
                    oItemSectionCostCenter = new SectionCostcentre();

                oItemSectionCostCenter.ItemSectionId = oItemSection.ItemSectionId;
                oItemSectionCostCenter.CostCentreId = oMakeReadyCostCentreDTO.CostCentreId;
                oItemSectionCostCenter.SystemCostCentreType = (int)SystemCostCenterTypes.Makeready;
                oItemSectionCostCenter.Order = 105;
                oItemSectionCostCenter.IsOptionalExtra = 0;
                oItemSectionCostCenter.CostCentreType = 1;
                oItemSectionCostCenter.IsDirectCost = Convert.ToInt16(oMakeReadyCostCentreDTO.IsDirectCost);
            }
            if (oItemSectionCostCenter.SectionCostCentreDetails == null)
                oItemSectionCostCenter.SectionCostCentreDetails = new List<SectionCostCentreDetail>();

            var markup = itemsectionRepository.GetMarkupById(oMakeReadyCostCentreDTO.DefaultVAId);
            var ProfitMargin = markup != null ? markup.MarkUpRate : 0;
            oItemSectionCostCenter.IsPrintable = Convert.ToInt16(oJobCardOptionsDTO.IsDefaultMakereadyUsed);

            if (!(oItemSection.IsMakeReadyUsed == false))
            {
                if (dblMakeReadyCost < oMakeReadyCostCentreDTO.MinimumCost && oItemSection.MakeReadyQty > 0)
                {
                    oItemSectionCostCenter.Qty1Charge = oMakeReadyCostCentreDTO.MinimumCost;
                    sMinimumCost = "1";
                }
                else
                {
                    oItemSectionCostCenter.Qty1Charge = dblMakeReadyPrice;
                    sMinimumCost = "0";
                }
                oItemSectionCostCenter.Qty1MarkUpID = oMakeReadyCostCentreDTO.DefaultVAId;
                oItemSectionCostCenter.Qty1MarkUpValue = oItemSectionCostCenter.Qty1Charge * ProfitMargin / 100;
                oItemSectionCostCenter.Qty1NetTotal = oItemSectionCostCenter.Qty1Charge + oItemSectionCostCenter.Qty1MarkUpValue;

                oItemSectionCostCenter.Qty1EstimatedTime = Math.Round(Convert.ToDouble((oPressDTO.MakeReadyTime * oItemSection.MakeReadyQty) / 60), 2);

                if (IsWorkInstructionsLocked == false && oJobCardOptionsDTO.IsDefaultMakereadyUsed == true)
                {
                    oItemSectionCostCenter.Qty1WorkInstructions = "Plate Makereadies:= " + oItemSection.MakeReadyQty.ToString() + Environment.NewLine;

                    if (oJobCardOptionsDTO.IsGuillotineEstTime == true)
                    {
                        //oItemSectionCostCenter.Qty1WorkInstructions += "Estimated Time := " + Math.Round(oItemSectionCostCenter.Qty1EstimatedTime, 2).ToString() + " hours";
                    }
                }
                oItemSectionCostCenter.Qty1EstimatedPlantCost = dblMakeReadyCost;

                if (oItemSection.Qty2 > 0)
                {
                    if (dblMakeReadyCost < oMakeReadyCostCentreDTO.MinimumCost & oItemSection.MakeReadyQty > 0)
                    {
                        oItemSectionCostCenter.Qty2Charge = oMakeReadyCostCentreDTO.MinimumCost;
                        sMinimumCost += "1";
                    }
                    else
                    {
                        oItemSectionCostCenter.Qty2Charge = dblMakeReadyPrice;
                        sMinimumCost += "0";
                    }

                    oItemSectionCostCenter.Qty2MarkUpID = oMakeReadyCostCentreDTO.DefaultVAId;
                    oItemSectionCostCenter.Qty2MarkUpValue = oItemSectionCostCenter.Qty2Charge * ProfitMargin / 100;
                    oItemSectionCostCenter.Qty2NetTotal = oItemSectionCostCenter.Qty2Charge + oItemSectionCostCenter.Qty2MarkUpValue;
                    oItemSectionCostCenter.Qty2EstimatedTime = Math.Round(Convert.ToDouble((oPressDTO.MakeReadyTime * oItemSection.MakeReadyQty) / 60), 2);
                    if (IsWorkInstructionsLocked == false && oJobCardOptionsDTO.IsDefaultMakereadyUsed == true)
                    {
                        oItemSectionCostCenter.Qty2WorkInstructions = "Plate Makereadies:= " + oItemSection.MakeReadyQty.ToString() + Environment.NewLine;

                        if (oJobCardOptionsDTO.IsGuillotineEstTime == true)
                        {
                            // oItemSectionCostCenter.Qty2WorkInstructions += "Estimated Time := " + Math.Round(oItemSectionCostCenter.Qty2EstimatedTime, 2).ToString() + " hours";
                        }
                    }

                    oItemSectionCostCenter.Qty2EstimatedPlantCost = dblMakeReadyCost;

                }
                if (oItemSection.Qty3 > 0)
                {
                    if (dblMakeReadyCost < oMakeReadyCostCentreDTO.MinimumCost & oItemSection.MakeReadyQty > 0)
                    {
                        oItemSectionCostCenter.Qty3Charge = oMakeReadyCostCentreDTO.MinimumCost;
                        sMinimumCost += "1";
                    }
                    else
                    {
                        oItemSectionCostCenter.Qty3Charge = dblMakeReadyPrice;
                        sMinimumCost += "0";
                    }

                    oItemSectionCostCenter.Qty3MarkUpID = oMakeReadyCostCentreDTO.DefaultVAId;
                    oItemSectionCostCenter.Qty3MarkUpValue = oItemSectionCostCenter.Qty3Charge * ProfitMargin / 100;
                    oItemSectionCostCenter.Qty3NetTotal = oItemSectionCostCenter.Qty3Charge + oItemSectionCostCenter.Qty3MarkUpValue;
                    oItemSectionCostCenter.Qty3EstimatedTime = Math.Round(Convert.ToDouble((oPressDTO.MakeReadyTime * oItemSection.MakeReadyQty) / 60), 2);
                    if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsDefaultMakereadyUsed == true)
                    {
                        oItemSectionCostCenter.Qty3WorkInstructions = "Plate Makereadies:= " + oItemSection.MakeReadyQty.ToString() + Environment.NewLine;

                        if (oJobCardOptionsDTO.IsGuillotineEstTime == true)
                        {
                            // oItemSectionCostCenter.Qty3WorkInstructions += "Estimated Time := " + Math.Round(oItemSectionCostCenter.Qty3EstimatedTime, 2).ToString() + " hours";
                        }
                    }

                    oItemSectionCostCenter.Qty3EstimatedPlantCost = dblMakeReadyCost;

                }
            }
            else
            {
                oItemSectionCostCenter.Qty1Charge = 0;
                oItemSectionCostCenter.Qty2Charge = 0;
                oItemSectionCostCenter.Qty3Charge = 0;
            }

            if (sMinimumCost != string.Empty)
            {
                oItemSectionCostCenter.IsMinimumCost = Convert.ToInt16(sMinimumCost);
            }
            else
            {
                oItemSectionCostCenter.IsMinimumCost = 0;
            }

            SectionCostCentreDetail oItemSectionCostCenterDetail;
            if (IsReRun == false || IsSectionCostCentreFoundInReRun == false)
            {
                oItemSectionCostCenterDetail = new SectionCostCentreDetail();
            }
            else
            {
                oItemSectionCostCenterDetail = oItemSectionCostCenter.SectionCostCentreDetails.FirstOrDefault();
            }

            if (oItemSection.Qty1 > 0)
            {
                oItemSectionCostCenterDetail.Qty1 = Convert.ToDouble(oItemSection.MakeReadyQty);
            }
            if (oItemSection.Qty2 > 0)
            {
                oItemSectionCostCenterDetail.Qty2 = Convert.ToDouble(oItemSection.MakeReadyQty);
            }
            if (oItemSection.Qty3 > 0)
            {
                oItemSectionCostCenterDetail.Qty3 = Convert.ToDouble(oItemSection.MakeReadyQty);
            }

            if (!(oItemSection.IsMakeReadyUsed == false))
            {
                oItemSectionCostCenterDetail.CostPrice = oPressDTO.MakeReadyCost;
            }
            else
            {
                oItemSectionCostCenterDetail.CostPrice = 0;
            }

            string side = isSide1 ? "Sdie 1" : "Side 2";
            oItemSectionCostCenter.Name = "Plate Makereadies " + side;
            oItemSectionCostCenter.Qty1 = oItemSection.Qty1;
            oItemSectionCostCenter.Qty2 = oItemSection.Qty2;
            oItemSectionCostCenter.Qty3 = oItemSection.Qty3;

            if (IsReRun == false || IsSectionCostCentreFoundInReRun == false)
            {
                oItemSectionCostCenter.SectionCostCentreDetails.Add(oItemSectionCostCenterDetail);
                oItemSection.SectionCostcentres.Add(oItemSectionCostCenter);
            }

            return oItemSection;
        }

        public ItemSection CalculatePaperCostWebPress(ItemSection oItemSection, int PressID, bool IsReRun = false, bool IsWorkInstructionsLocked = false)
        {

            JobPreference oJobCardOptionsDTO = itemsectionRepository.GetJobPreferences(1);
            string sMinimumCost = string.Empty;
            double UnitPrice = 0;
            double PackPrice = 0;
            //'For Unit Price Calculation of Paper
            double UnitCost = 0;
            double PackCost = 0;
            Machine oPressDTO = itemsectionRepository.GetPressById(PressID);
            SectionCostCentreResource oResourceDto;
            int OrderPTV = 0;
            int PrintSheetPTV = 0;

            double[] OrderPaperLengthWithSpoilage = new double[3];
            double[] OrderPaperLengthWithoutSpoilage = new double[3];
            double[] OrderPaperLengthWithSpoilageSqMeters = new double[3];
            double[] OrderPaperLengthWithoutSpoilageSqMeters = new double[3];
            double[] OrderPaperReelsWithSpoilageQty = new double[3];
            double[] OrderPaperReelsWithoutSpoilageQty = new double[3];
            double[] OrderPaperWeightWithSpoilage = new double[3];
            double[] OrderPaperWeightWithoutSpoilage = new double[3];
            int TempQuantity = 0;
            double[] Spoilage = new double[3];
            //converted into mm heights/widths
            double SectionHeight = 0;
            double SectionWidth = 0;

            double ReelLength = 0;
            double ReelWidth = 0;
            Organisation org = organisationRepository.GetOrganizatiobByID();

            CostCentre oPaperCostCentreDTO = itemsectionRepository.GetCostCenterBySystemType((int)SystemCostCenterTypes.Paper);
            StockItem oPaperDTO = itemsectionRepository.GetStockById(Convert.ToInt64(oItemSection.StockItemID1));
            //Updating the Paper Gsm in Item Section
            oItemSection.PaperGsm = oPaperDTO.ItemWeight;
            //because this is a Reel/continous paper

            //convert reel width from whatever standard into mm


            if (oPaperDTO.RollStandards == (int)lengthunit.Cm)
                ReelWidth = ConvertLength(Convert.ToDouble(oPaperDTO.RollWidth), lengthunit.Cm, lengthunit.Mm);
            else if (oPaperDTO.RollStandards == (int)lengthunit.Inch)
                ReelWidth = ConvertLength(Convert.ToDouble(oPaperDTO.RollWidth), lengthunit.Inch, lengthunit.Mm);
            else
                ReelWidth = Convert.ToDouble(oPaperDTO.RollWidth);

            // ReelWidth = ConvertLength((double)oPaperDTO.RollWidth, roleStandard , MPC.Models.Common.LengthUnit.Mm);
            //roll length is always going into meters
            ReelLength = Convert.ToDouble(oPaperDTO.RollLength);

            SectionHeight = ConvertLength(Convert.ToDouble(oItemSection.SectionSizeHeight), (lengthunit)org.SystemLengthUnit, lengthunit.Mm);

            SectionWidth = ConvertLength(Convert.ToDouble(oItemSection.SectionSizeWidth), (lengthunit)org.SystemLengthUnit, lengthunit.Mm);

            if (oItemSection.PrintViewLayout == 1) // 1 is for Landscape 0 is for portrait
            {
                PrintSheetPTV = oItemSection.PrintViewLayoutLandScape != null ? (int)oItemSection.PrintViewLayoutLandScape : 0;
            }
            else
            {
                PrintSheetPTV = oItemSection.PrintViewLayoutPortrait != null ? (int)oItemSection.PrintViewLayoutPortrait : 0;
            }

            if (PrintSheetPTV == 0)
            {
                return oItemSection;//.tbl_section_costcentres.ToList();
            }


            for (int i = 0; i <= 2; i++)
            {
                if (i == 0)
                {
                    TempQuantity = oItemSection.Qty1 != null ? (int)oItemSection.Qty1 : 0;
                }
                else if (i == 1)
                {
                    TempQuantity = oItemSection.Qty2 != null ? (int)oItemSection.Qty2 : 0;
                }
                else if (i == 2)
                {
                    TempQuantity = oItemSection.Qty3 != null ? (int)oItemSection.Qty3 : 0;
                }

                double SectionQtyWithoutSpoilage = TempQuantity / PrintSheetPTV;
                double SectionQtyWithSpoilage = SectionQtyWithoutSpoilage;
                //calculating spoilage
                //to do apply condition here ..
                //in case spoilage is in sheets
                if (oItemSection.WebSpoilageType == (int)WebSpoilageTypes.inSheets)
                {
                    Spoilage[i] = Convert.ToDouble((oItemSection.SetupSpoilage + (SectionQtyWithoutSpoilage * oItemSection.RunningSpoilage / 100)));
                    SectionQtyWithSpoilage += Spoilage[i];
                }
                else
                {
                    //in case spoilage is in Meters
                    Spoilage[i] = Convert.ToDouble((oItemSection.SetupSpoilage * SectionHeight / 1000 + (SectionQtyWithoutSpoilage * oItemSection.RunningSpoilage / 100 * SectionHeight / 1000)));
                    SectionQtyWithSpoilage += Spoilage[i];
                }
                //calculating paper required in meters
                OrderPaperLengthWithoutSpoilage[i] = SectionQtyWithoutSpoilage * SectionHeight / 1000;
                OrderPaperLengthWithSpoilage[i] = SectionQtyWithSpoilage * SectionHeight / 1000;
                //OrderPaperLengthWithSpoilage(i) = Model.Common.RoundUp(OrderPaperLengthWithSpoilage(i) / ReelLength)
                //OrderPaperLengthWithoutSpoilage(i) = Model.Common.RoundUp(OrderPaperLengthWithoutSpoilage(i) / ReelLength)
                OrderPaperLengthWithSpoilageSqMeters[i] = (OrderPaperLengthWithSpoilage[i] * SectionWidth / 1000) / 100;
                OrderPaperLengthWithoutSpoilageSqMeters[i] = (OrderPaperLengthWithoutSpoilage[i] * SectionWidth / 1000) / 100;

                OrderPaperReelsWithSpoilageQty[i] = (OrderPaperLengthWithSpoilage[i] / ReelLength);
                OrderPaperReelsWithoutSpoilageQty[i] = (OrderPaperLengthWithoutSpoilage[i] / ReelLength);

                OrderPaperWeightWithSpoilage[i] = (ConvertWeight(Convert.ToDouble(oPaperDTO.ItemWeight), WeightUnits.GSM, WeightUnits.lbs) * 100 * OrderPaperLengthWithSpoilageSqMeters[i]);
                OrderPaperWeightWithoutSpoilage[i] = (ConvertWeight(Convert.ToDouble(oPaperDTO.ItemWeight), WeightUnits.GSM, WeightUnits.lbs) * 100 * OrderPaperLengthWithoutSpoilageSqMeters[i]);

            }
            oItemSection.PaperWeight1 = OrderPaperWeightWithSpoilage[0];
            oItemSection.PaperWeight2 = OrderPaperWeightWithSpoilage[1];
            oItemSection.PaperWeight3 = OrderPaperWeightWithSpoilage[2];
            //new logic for paper cost and price calcuation Implemetned by Muzzammil
            //dblUPrice = Round(oPaperDTO.CostPrice / oPaperDTO.PackageQty, 4)

            //GlobalData gData = GetItemPriceCost(oItemSection.StockItemID1, UnitCost, UnitPrice, PackCost, PackPrice);
            GlobalData gData = GetItemPriceCost((int)oItemSection.StockItemID1);
            if (gData != null)
            {
                UnitCost = gData.dblUnitCost;
                UnitPrice = gData.dblUnitPrice;
                PackCost = gData.dblPackCost;
                PackPrice = gData.dblPackPrice;
            }

            //Updating the Cost price per pack of Stock used in Item Section
            oItemSection.PaperPackPrice = gData.dblPackPrice;
            //* oPaperDTO.PackageQty

            //Setting the Cost for each quantity
            SectionCostcentre oItemSectionCostCenter;
            if (IsReRun == false)
            {
                oItemSectionCostCenter = new SectionCostcentre();
                oItemSectionCostCenter.ItemSectionId = oItemSection.ItemSectionId;
                oItemSectionCostCenter.CostCentreId = oPaperCostCentreDTO.CostCentreId;
                oItemSectionCostCenter.SystemCostCentreType = (int)SystemCostCenterTypes.Paper;
                oItemSectionCostCenter.Order = 102;
                oItemSectionCostCenter.IsOptionalExtra = 0;
                oItemSectionCostCenter.CostCentreType = 1;
                oItemSectionCostCenter.IsDirectCost = Convert.ToInt16(oPaperCostCentreDTO.IsDirectCost);
                oItemSectionCostCenter.SectionCostCentreDetails = new List<SectionCostCentreDetail>();
            }
            else
            {
                foreach (var sectionCC in oItemSection.SectionCostcentres)
                {
                    if (sectionCC.SystemCostCentreType == (int)SystemCostCenterTypes.Paper)
                    {
                        oItemSectionCostCenter = sectionCC;
                        break; // TODO: might not be correct. Was : Exit For
                    }

                }
                oItemSectionCostCenter = new SectionCostcentre();
                oItemSectionCostCenter.ItemSectionId = oItemSection.ItemSectionId;
                oItemSectionCostCenter.CostCentreId = oPaperCostCentreDTO.CostCentreId;
                oItemSectionCostCenter.SystemCostCentreType = (int)SystemCostCenterTypes.Paper;
                oItemSectionCostCenter.Order = 102;
                oItemSectionCostCenter.IsOptionalExtra = 0;
                oItemSectionCostCenter.CostCentreType = 1;
                oItemSectionCostCenter.IsDirectCost = Convert.ToInt16(oPaperCostCentreDTO.IsDirectCost);
                oItemSectionCostCenter.SectionCostCentreDetails = new List<SectionCostCentreDetail>();
            }

            oItemSectionCostCenter.IsPrintable = Convert.ToInt16(oJobCardOptionsDTO.IsDefaultStockDetail);

            //if paper is not supplied and we have to use it ourself then add the prices :)

            if (oItemSection.IsPaperSupplied != true)
            {
                oItemSectionCostCenter.Qty1Charge = OrderPaperLengthWithSpoilageSqMeters[0] * UnitPrice;

                if (oItemSectionCostCenter.Qty1Charge < oPaperCostCentreDTO.MinimumCost)
                {
                    oItemSectionCostCenter.Qty1Charge = oPaperCostCentreDTO.MinimumCost;
                    sMinimumCost = "1";
                }
                else
                {
                    sMinimumCost = "0";
                }
                var markup = itemsectionRepository.GetMarkupById(oPaperCostCentreDTO.DefaultVAId);
                var ProfitMargin = markup != null ? markup.MarkUpRate : 0;
                oItemSectionCostCenter.Qty1MarkUpID = oPaperCostCentreDTO.DefaultVAId;
                oItemSectionCostCenter.Qty1MarkUpValue = oItemSectionCostCenter.Qty1Charge * ProfitMargin / 100;
                oItemSectionCostCenter.Qty1NetTotal = oItemSectionCostCenter.Qty1Charge + oItemSectionCostCenter.Qty1MarkUpValue;

                oItemSectionCostCenter.Qty1EstimatedStockCost = OrderPaperLengthWithSpoilageSqMeters[0] * UnitCost;

                if (IsWorkInstructionsLocked == false && oJobCardOptionsDTO.IsDefaultStockDetail == true)
                {
                    if (oJobCardOptionsDTO.IsPaperSheetQty == true)
                    {
                        oItemSectionCostCenter.Qty1WorkInstructions = "Paper Length Required = " + OrderPaperLengthWithSpoilage[0].ToString() + " Meters(s)" + Environment.NewLine;
                    }

                    if (oJobCardOptionsDTO.IsSpoilageAllowed == true)
                    {
                        oItemSectionCostCenter.Qty1WorkInstructions += "Print Sheet Spoilage = " + Spoilage[0] + " Sheets" + Environment.NewLine;
                        oItemSectionCostCenter.Qty1WorkInstructions += "Print Sheet Spoilage = " + (OrderPaperLengthWithSpoilage[0] - OrderPaperLengthWithoutSpoilage[0]).ToString() + " Meters" + Environment.NewLine;
                    }

                    if (oJobCardOptionsDTO.IsOrderSheetSize == true)
                    {
                        oItemSectionCostCenter.Qty1WorkInstructions += "Reel Size = " + ReelWidth.ToString() + "mm x " + ReelLength.ToString() + " Meters " + Environment.NewLine;
                    }

                    oItemSectionCostCenter.Qty1WorkInstructions += "Item Size (Flat) = " + oItemSection.ItemSizeHeight.ToString() + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + Convert.ToString(oItemSection.ItemSizeHeight) + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + Environment.NewLine;

                    if (oJobCardOptionsDTO.IsPaperWeight == true)
                    {
                        oItemSectionCostCenter.Qty1WorkInstructions += "Total Paper Weight = " + Math.Round(OrderPaperWeightWithSpoilage[0], 2) + " " + itemsectionRepository.GetWeightUnitName((int)CompanyGeneralSettings().SystemWeightUnit) + Environment.NewLine;
                    }
                    oItemSectionCostCenter.Qty1WorkInstructions += "Paper Supplied = No ";
                }


                if (oItemSection.Qty2 > 0)
                {
                    oItemSectionCostCenter.Qty2Charge = OrderPaperLengthWithSpoilageSqMeters[1] * UnitPrice;

                    if (oItemSectionCostCenter.Qty2Charge < oPaperCostCentreDTO.MinimumCost)
                    {
                        oItemSectionCostCenter.Qty2Charge = oPaperCostCentreDTO.MinimumCost;
                        sMinimumCost += "1";
                    }
                    else
                    {
                        sMinimumCost += "0";
                    }

                    oItemSectionCostCenter.Qty2MarkUpID = oPaperCostCentreDTO.DefaultVAId;
                    oItemSectionCostCenter.Qty2MarkUpValue = oItemSectionCostCenter.Qty2Charge * ProfitMargin / 100;
                    oItemSectionCostCenter.Qty2NetTotal = oItemSectionCostCenter.Qty2Charge + oItemSectionCostCenter.Qty2MarkUpValue;

                    oItemSectionCostCenter.Qty2EstimatedStockCost = OrderPaperLengthWithSpoilageSqMeters[1] * UnitCost;

                    if (IsWorkInstructionsLocked == false && oJobCardOptionsDTO.IsDefaultStockDetail == true)
                    {
                        if (oJobCardOptionsDTO.IsPaperSheetQty == true)
                        {
                            oItemSectionCostCenter.Qty1WorkInstructions = "Paper Length Required = " + OrderPaperLengthWithSpoilage[1].ToString() + " Meters(s)" + Environment.NewLine;
                        }

                        if (oJobCardOptionsDTO.IsSpoilageAllowed == true)
                        {
                            oItemSectionCostCenter.Qty1WorkInstructions += "Print Sheet Spoilage = " + Spoilage[1] + " Sheets" + Environment.NewLine;
                            oItemSectionCostCenter.Qty1WorkInstructions += "Print Sheet Spoilage = " + (OrderPaperLengthWithSpoilage[1] - OrderPaperLengthWithoutSpoilage[1]).ToString() + " Meters" + Environment.NewLine;
                        }

                        if (oJobCardOptionsDTO.IsOrderSheetSize == true)
                        {
                            oItemSectionCostCenter.Qty1WorkInstructions += "Reel Size = " + ReelWidth.ToString() + "mm x " + ReelLength.ToString() + " Meters " + Environment.NewLine;
                        }

                        oItemSectionCostCenter.Qty1WorkInstructions += "Item Size (Flat) = " + Convert.ToString(oItemSection.ItemSizeHeight) + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + Convert.ToString(oItemSection.ItemSizeHeight) + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + Environment.NewLine;

                        if (oJobCardOptionsDTO.IsPaperWeight == true)
                        {
                            oItemSectionCostCenter.Qty1WorkInstructions += "Total Paper Weight = " + Math.Round(OrderPaperWeightWithSpoilage[1], 2) + " " + itemsectionRepository.GetWeightUnitName((int)CompanyGeneralSettings().SystemWeightUnit) + Environment.NewLine;
                        }


                        oItemSectionCostCenter.Qty1WorkInstructions += "Paper Supplied = No ";

                    }
                }
                if (oItemSection.Qty3 > 0)
                {
                    oItemSectionCostCenter.Qty3Charge = OrderPaperLengthWithSpoilageSqMeters[2] * UnitPrice;

                    if (oItemSectionCostCenter.Qty3Charge < oPaperCostCentreDTO.MinimumCost)
                    {
                        oItemSectionCostCenter.Qty3Charge = oPaperCostCentreDTO.MinimumCost;
                        sMinimumCost += "1";
                    }
                    else
                    {
                        sMinimumCost += "0";
                    }

                    oItemSectionCostCenter.Qty3MarkUpID = oPaperCostCentreDTO.DefaultVAId;
                    oItemSectionCostCenter.Qty3MarkUpValue = oItemSectionCostCenter.Qty3Charge * ProfitMargin / 100;
                    oItemSectionCostCenter.Qty3NetTotal = oItemSectionCostCenter.Qty3Charge + oItemSectionCostCenter.Qty3MarkUpValue;

                    oItemSectionCostCenter.Qty3EstimatedStockCost = OrderPaperLengthWithSpoilageSqMeters[2] * UnitCost;

                    if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsDefaultStockDetail == true)
                    {
                        if (oJobCardOptionsDTO.IsPaperSheetQty == true)
                        {
                            oItemSectionCostCenter.Qty1WorkInstructions = "Paper Length Required = " + OrderPaperLengthWithSpoilage[2].ToString() + " Meters(s)" + Environment.NewLine;
                        }

                        if (oJobCardOptionsDTO.IsSpoilageAllowed == true)
                        {
                            oItemSectionCostCenter.Qty1WorkInstructions += "Print Sheet Spoilage = " + Spoilage[2] + " Sheets" + Environment.NewLine;
                            oItemSectionCostCenter.Qty1WorkInstructions += "Print Sheet Spoilage = " + (OrderPaperLengthWithSpoilage[2] - OrderPaperLengthWithoutSpoilage[2]).ToString() + " Meters" + Environment.NewLine;
                        }

                        if (oJobCardOptionsDTO.IsOrderSheetSize == true)
                        {
                            oItemSectionCostCenter.Qty1WorkInstructions += "Reel Size = " + ReelWidth.ToString() + "mm x " + ReelLength.ToString() + " Meters " + Environment.NewLine;
                        }

                        oItemSectionCostCenter.Qty1WorkInstructions += "Item Size (Flat) = " + Convert.ToString(oItemSection.ItemSizeHeight) + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + Convert.ToString(oItemSection.ItemSizeHeight) + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + Environment.NewLine;

                        if (oJobCardOptionsDTO.IsPaperWeight == true)
                        {
                            oItemSectionCostCenter.Qty1WorkInstructions += "Total Paper Weight = " + Math.Round(OrderPaperWeightWithSpoilage[2], 2) + " " + itemsectionRepository.GetWeightUnitName((int)CompanyGeneralSettings().SystemWeightUnit) + Environment.NewLine;
                        }


                        oItemSectionCostCenter.Qty1WorkInstructions += "Paper Supplied = No ";

                    }

                }
            }
            else
            {
                if (oItemSection.Qty1 > 0)
                {
                    oItemSectionCostCenter.Qty1Charge = 0;
                    oItemSectionCostCenter.Qty1MarkUpID = 0;
                    oItemSectionCostCenter.Qty1MarkUpValue = 0;
                    oItemSectionCostCenter.Qty1NetTotal = 0;
                    oItemSectionCostCenter.Qty1EstimatedStockCost = 0;
                    sMinimumCost = "0";
                    if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsDefaultStockDetail == true)
                    {
                        if (oJobCardOptionsDTO.IsPaperSheetQty == true)
                        {
                            oItemSectionCostCenter.Qty1WorkInstructions = "Paper Length Required = " + OrderPaperLengthWithSpoilage[0].ToString() + " Meters(s)" + Environment.NewLine;
                        }

                        if (oJobCardOptionsDTO.IsSpoilageAllowed == true)
                        {
                            oItemSectionCostCenter.Qty1WorkInstructions += "Print Sheet Spoilage = " + Spoilage[0] + " Sheets" + Environment.NewLine;
                            oItemSectionCostCenter.Qty1WorkInstructions += "Print Sheet Spoilage = " + (OrderPaperLengthWithSpoilage[0] - OrderPaperLengthWithoutSpoilage[0]).ToString() + " Meters" + Environment.NewLine;
                        }

                        if (oJobCardOptionsDTO.IsOrderSheetSize == true)
                        {
                            oItemSectionCostCenter.Qty1WorkInstructions += "Reel Size = " + ReelWidth.ToString() + "mm x " + ReelLength.ToString() + " Meters " + Environment.NewLine;
                        }

                        oItemSectionCostCenter.Qty1WorkInstructions += "Item Size (Flat) = " + Convert.ToString(oItemSection.ItemSizeHeight) + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + Convert.ToString(oItemSection.ItemSizeHeight) + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + Environment.NewLine;

                        if (oJobCardOptionsDTO.IsPaperWeight == true)
                        {
                            oItemSectionCostCenter.Qty1WorkInstructions += "Total Paper Weight = " + Math.Round(OrderPaperWeightWithSpoilage[0], 2) + " " + itemsectionRepository.GetWeightUnitName((int)CompanyGeneralSettings().SystemWeightUnit) + Environment.NewLine;
                        }


                        oItemSectionCostCenter.Qty1WorkInstructions += "Paper Supplied = Yes ";

                    }
                }
                if (oItemSection.Qty2 > 0)
                {
                    oItemSectionCostCenter.Qty2Charge = 0;
                    oItemSectionCostCenter.Qty2MarkUpID = 0;
                    oItemSectionCostCenter.Qty2MarkUpValue = 0;
                    oItemSectionCostCenter.Qty2NetTotal = 0;
                    oItemSectionCostCenter.Qty2EstimatedStockCost = 0;
                    sMinimumCost += "0";
                    if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsDefaultStockDetail == true)
                    {
                        if (oJobCardOptionsDTO.IsPaperSheetQty == true)
                        {
                            oItemSectionCostCenter.Qty1WorkInstructions = "Paper Length Required = " + OrderPaperLengthWithSpoilage[1].ToString() + " Meters(s)" + Environment.NewLine;
                        }

                        if (oJobCardOptionsDTO.IsSpoilageAllowed == true)
                        {
                            oItemSectionCostCenter.Qty1WorkInstructions += "Print Sheet Spoilage = " + Spoilage[1] + " Sheets" + Environment.NewLine;
                            oItemSectionCostCenter.Qty1WorkInstructions += "Print Sheet Spoilage = " + (OrderPaperLengthWithSpoilage[1] - OrderPaperLengthWithoutSpoilage[1]).ToString() + " Meters" + Environment.NewLine;
                        }

                        if (oJobCardOptionsDTO.IsOrderSheetSize == true)
                        {
                            oItemSectionCostCenter.Qty1WorkInstructions += "Reel Size = " + ReelWidth.ToString() + "mm x " + ReelLength.ToString() + " Meters " + Environment.NewLine;
                        }

                        oItemSectionCostCenter.Qty1WorkInstructions += "Item Size (Flat) = " + Convert.ToString(oItemSection.ItemSizeHeight) + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + Convert.ToString(oItemSection.ItemSizeHeight) + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + Environment.NewLine;

                        if (oJobCardOptionsDTO.IsPaperWeight == true)
                        {
                            oItemSectionCostCenter.Qty1WorkInstructions += "Total Paper Weight = " + Math.Round(OrderPaperWeightWithSpoilage[1], 2) + " " + itemsectionRepository.GetWeightUnitName((int)CompanyGeneralSettings().SystemWeightUnit) + Environment.NewLine;
                        }


                        oItemSectionCostCenter.Qty1WorkInstructions += "Paper Supplied = Yes ";

                    }
                }
                if (oItemSection.Qty3 > 0)
                {
                    oItemSectionCostCenter.Qty3Charge = 0;
                    oItemSectionCostCenter.Qty3MarkUpID = 0;
                    oItemSectionCostCenter.Qty3MarkUpValue = 0;
                    oItemSectionCostCenter.Qty3NetTotal = 0;
                    oItemSectionCostCenter.Qty3EstimatedStockCost = 0;
                    sMinimumCost += "0";
                    if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsDefaultStockDetail == true)
                    {
                        if (oJobCardOptionsDTO.IsPaperSheetQty == true)
                        {
                            oItemSectionCostCenter.Qty1WorkInstructions = "Paper Length Required = " + OrderPaperLengthWithSpoilage[2].ToString() + " Meters(s)" + Environment.NewLine;
                        }

                        if (oJobCardOptionsDTO.IsSpoilageAllowed == true)
                        {
                            oItemSectionCostCenter.Qty1WorkInstructions += "Print Sheet Spoilage = " + Spoilage[2] + " Sheets" + Environment.NewLine;
                            oItemSectionCostCenter.Qty1WorkInstructions += "Print Sheet Spoilage = " + (OrderPaperLengthWithSpoilage[2] - OrderPaperLengthWithoutSpoilage[2]).ToString() + " Meters" + Environment.NewLine;
                        }

                        if (oJobCardOptionsDTO.IsOrderSheetSize == true)
                        {
                            oItemSectionCostCenter.Qty1WorkInstructions += "Reel Size = " + ReelWidth.ToString() + "mm x " + ReelLength.ToString() + " Meters " + Environment.NewLine;
                        }

                        oItemSectionCostCenter.Qty1WorkInstructions += "Item Size (Flat) = " + Convert.ToString(oItemSection.ItemSizeHeight) + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + Convert.ToString(oItemSection.ItemSizeHeight) + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + Environment.NewLine;

                        if (oJobCardOptionsDTO.IsPaperWeight == true)
                        {
                            oItemSectionCostCenter.Qty1WorkInstructions += "Total Paper Weight = " + Math.Round(OrderPaperWeightWithSpoilage[2], 2) + " " + itemsectionRepository.GetWeightUnitName((int)CompanyGeneralSettings().SystemWeightUnit) + Environment.NewLine;
                        }


                        oItemSectionCostCenter.Qty1WorkInstructions += "Paper Supplied = Yes ";

                    }
                }

            }

            if (sMinimumCost != string.Empty)
            {
                oItemSectionCostCenter.IsMinimumCost = Convert.ToInt16(sMinimumCost);
            }
            else
            {
                oItemSectionCostCenter.IsMinimumCost = 0;
            }

            SectionCostCentreDetail oItemSectionCostCenterDetail;
            if (IsReRun == false)
            {
                oItemSectionCostCenterDetail = new SectionCostCentreDetail();
            }
            else
            {
                oItemSectionCostCenterDetail = oItemSectionCostCenter.SectionCostCentreDetails.FirstOrDefault();
            }
            if (oItemSection.Qty1 > 0)
            {
                oItemSectionCostCenterDetail.Qty1 = OrderPaperLengthWithSpoilageSqMeters[0];
            }
            if (oItemSection.Qty2 > 0)
            {
                oItemSectionCostCenterDetail.Qty2 = OrderPaperLengthWithSpoilageSqMeters[1];
            }
            if (oItemSection.Qty3 > 0)
            {
                oItemSectionCostCenterDetail.Qty3 = OrderPaperLengthWithSpoilageSqMeters[2];
            }

            if (oItemSection.IsPaperSupplied != true)
            {
                oItemSectionCostCenterDetail.CostPrice = UnitPrice;
            }
            else
            {
                oItemSectionCostCenterDetail.CostPrice = 0;
            }
            oItemSectionCostCenterDetail.StockId = oPaperDTO.StockItemId;
            oItemSectionCostCenterDetail.StockName = oPaperDTO.ItemName;
            oItemSectionCostCenterDetail.SupplierId = Convert.ToInt32(oPaperDTO.SupplierId);

            oItemSectionCostCenter.Name = oPaperDTO.ItemName;
            oItemSectionCostCenter.Qty1 = oItemSection.Qty1;
            oItemSectionCostCenter.Qty2 = oItemSection.Qty2;
            oItemSectionCostCenter.Qty3 = oItemSection.Qty3;

            if (IsReRun == false)
            {
                oItemSectionCostCenter.SectionCostCentreDetails.Add(oItemSectionCostCenterDetail);
                oItemSection.SectionCostcentres.Add(oItemSectionCostCenter);
            }


            return oItemSection;
        }

        public ItemSection CalculateInkCost(ItemSection oItemSection, int CurrentCostCentreIndex, int PressID, bool IsReRun = false, bool IsWorkInstructionsLocked = false, List<SectionInkCoverage> oSectionAllInks = null)
        {

            JobPreference oJobCardOptionsDTO = itemsectionRepository.GetJobPreferences(1);
            string sMinimumCost = null;
            double intPrintArea = 0;
            //Variable to Hold Working Sheet Area
            //Dim dblSPK As Double    'Variable to Hold the Value of Ink Cost Price
            double dblInkCost = 0;
            double dblInkPrice = 0;
            //Variable to Hold the Value of Ink Cost Price to be calculated
            //Dim strPigm As String   'Variable to Hold the Ink is Black or Not
            //Dim dblPigValue As Double   'Variable to Hold Pigmentation Value
            //Dim dblMultiple As Double   'Variable to Hold Pigmentation Multiple
            double dblDuctCost = 0;
            double dblDuctPrice = 0;
            //Variable to Hold DuctCost for Press
            double[] dblQty = new double[5];
            //variable to Hold the Ink Qty for each Multiple qty
            double[] dblTotalPrice = new double[3];
            double[] dblTotalCost = new double[3];
            //Variable to Hold the Total Price for each Multiple Qty
            double dblMinCharge = 0;
            //Variable to hold the Minimum charge for Ink Cost Center
            string strSide1Description = null;
            string strSide2Description = null;
            int Side1InkCounter = 0;
            int Side2InkCounter = 0;
            string InksDescription = null;
            int UniqueInks = 0;
            Machine oPressDTO = itemsectionRepository.GetPressById(PressID);
            SectionCostCentreResource oResourceDto;
            CostCentre oCostCentreDTO;
            //Calculate Spoilage
            int FinishedItemQty1 = Convert.ToInt32(oItemSection.Qty1);
            int FinishedItemQty2 = Convert.ToInt32(oItemSection.Qty2);
            int FinishedItemQty3 = Convert.ToInt32(oItemSection.Qty3);
            int NoofSheetsQty1 = 0;
            int NoofSheetsQty2 = 0;
            int NoofSheetsQty3 = 0;
            int SetupSpoilage = oItemSection.SetupSpoilage ?? 0;
            double RunningSpoilage = oItemSection.RunningSpoilage ??0;
           
            int NoofInks = 0;
            if (oSectionAllInks != null && oSectionAllInks.Count > 0)
                NoofInks = oSectionAllInks.Count;

            
            if (oItemSection.IsPortrait == true)
            {
                NoofSheetsQty1 = Convert.ToInt32(oItemSection.Qty1) / Convert.ToInt32(oItemSection.PrintViewLayoutPortrait);
                NoofSheetsQty2 = Convert.ToInt32(oItemSection.Qty2) / Convert.ToInt32(oItemSection.PrintViewLayoutPortrait);
                NoofSheetsQty3 = Convert.ToInt32(oItemSection.Qty3) / Convert.ToInt32(oItemSection.PrintViewLayoutPortrait);
            }
            else
            {
                NoofSheetsQty1 = Convert.ToInt32(oItemSection.Qty1) / Convert.ToInt32(oItemSection.PrintViewLayoutLandScape);
                NoofSheetsQty2 = Convert.ToInt32(oItemSection.Qty2) / Convert.ToInt32(oItemSection.PrintViewLayoutLandScape);
                NoofSheetsQty3 = Convert.ToInt32(oItemSection.Qty3) / Convert.ToInt32(oItemSection.PrintViewLayoutLandScape);
            }

            FinishedItemQty1 = (NoofSheetsQty1 * Convert.ToInt32(RunningSpoilage / 100)) + NoofSheetsQty1 + SetupSpoilage;
            FinishedItemQty2 = (NoofSheetsQty2 * Convert.ToInt32(RunningSpoilage / 100)) + NoofSheetsQty2 + SetupSpoilage;
            FinishedItemQty3 = (NoofSheetsQty3 * Convert.ToInt32(RunningSpoilage / 100)) + NoofSheetsQty3 + SetupSpoilage;
            oItemSection.SetupSpoilage = SetupSpoilage;
            oItemSection.RunningSpoilage = Convert.ToInt32(RunningSpoilage);
            oItemSection.FinishedItemQty1 = FinishedItemQty1;
            oItemSection.FinishedItemQty2 = FinishedItemQty2;
            oItemSection.FinishedItemQty3 = FinishedItemQty3;
            //------ End of Spoilage Calculation
            //List<tbl_section_inkcoverage> oSectionAllInks = ObjectContext.tbl_section_inkcoverage.Where(i => i.SectionID == oItemSection.ItemSectionID).ToList();
            List<SectionInkCoverage> oSectionUniqueInks;
            double InkPercentage = 0;

            oSectionUniqueInks = oSectionAllInks;

            CostCentre oInksCostcentreDTO = itemsectionRepository.GetCostCenterBySystemType((int)SystemCostCenterTypes.Ink);
            Machine Press = itemsectionRepository.GetPressById(Convert.ToInt32(oItemSection.PressId));
            dblMinCharge = Press != null ? Convert.ToDouble(Press.InkChargeForUniqueColors) : 0;
            // dblMinCharge = oItemSection.Press.InkChargeForUniqueColors;

            UniqueInks = oSectionAllInks.Count; // is to confirm from Muzammil what will be the unique Inks

            StockItem oPaper = itemsectionRepository.GetStockById(Convert.ToInt64(oItemSection.StockItemID1));

            SectionCostcentre oItemSectionCostCentre;
            if (IsReRun == false)
            {
                oItemSectionCostCentre = new SectionCostcentre();
                oItemSectionCostCentre.ItemSectionId = oItemSection.ItemSectionId;
                oItemSectionCostCentre.CostCentreId = oInksCostcentreDTO.CostCentreId;
                oItemSectionCostCentre.SystemCostCentreType = (int)SystemCostCenterTypes.Ink;
                oItemSectionCostCentre.Order = 101;
                oItemSectionCostCentre.IsOptionalExtra = 0;
                oItemSectionCostCentre.CostCentreType = 1;
                oItemSectionCostCentre.IsDirectCost = Convert.ToInt16(oInksCostcentreDTO.IsDirectCost);
                oItemSectionCostCentre.SectionCostCentreDetails = new List<SectionCostCentreDetail>();
            }
            else
            {

                oItemSectionCostCentre = oItemSection.SectionCostcentres.FirstOrDefault();
            }

            oItemSectionCostCentre.IsPrintable = Convert.ToInt16(oJobCardOptionsDTO.IsDefaultInkColorUsed);

            //handling costcentre resources
            //oCostCentreDTO = db.CostCentres.Where(c => c.CostCentreId == oItemSectionCostCentre.CostCentreId).FirstOrDefault();
            lengthunit height = (lengthunit)CompanyGeneralSettings().SystemLengthUnit;
            lengthunit width = (lengthunit)CompanyGeneralSettings().SystemLengthUnit;
            if (oItemSection.IsSectionSizeCustom == true)
            {
                intPrintArea = (int)oItemSection.ItemSizeHeight * (int)oItemSection.ItemSizeWidth;
            }
            else
            {
                intPrintArea = ConvertLength((int)oItemSection.ItemSizeId, height, lengthunit.Inch) * ConvertLength((int)oItemSection.ItemSizeId, width, lengthunit.Inch);
            }


            //oItemSection.ItemSizeWidth
            //creation the work instructions / item description
            foreach (var icounter in oSectionAllInks)
            {
                StockItem oInkDTO = itemsectionRepository.GetStockById((int)icounter.InkId);
                if (oInkDTO != null)
                {
                    if (icounter.Side == 1)
                    {
                        strSide1Description += oInkDTO.ItemName + ", ";
                        Side1InkCounter += 1;
                    }
                    else
                    {
                        strSide2Description += oInkDTO.ItemName + ", ";
                        Side2InkCounter += 1;
                    }
                }
            }


            if ((strSide1Description != null))
            {
                strSide1Description = strSide1Description.Substring(0, strSide1Description.Length - 2);
            }

            if ((strSide2Description != null))
            {
                strSide2Description = strSide2Description.Substring(0, strSide2Description.Length - 2);
            }

            if (Convert.ToBoolean(oItemSection.IsDoubleSided) == true)
            {
                InksDescription = "Side 1 : " + Side1InkCounter.ToString() + " Colors" + Environment.NewLine;
                InksDescription += strSide1Description + Environment.NewLine;

                InksDescription += "Side 2 : " + Side2InkCounter.ToString() + " Colors" + Environment.NewLine;
                InksDescription += strSide2Description;
            }
            else
            {
                InksDescription = "Side 1 : " + Side1InkCounter.ToString() + " Colors" + Environment.NewLine;
                InksDescription += strSide1Description;
            }

            //Looping through each Section Ink Found in Table
            for (int i = 0; i <= oSectionUniqueInks.Count - 1; i++)
            {
                //Getting Each Ink Detail Used in Wizard from Stock Items
                int iInkID = (int)oSectionUniqueInks[i].InkId;
                StockItem oRowInkDetail;
                oRowInkDetail = itemsectionRepository.GetStockById(iInkID);

                //loading cost / price tables
                //, dblInkCost, dblInkPrice
                GlobalData gData = GetItemPriceCost(Convert.ToInt32(oSectionUniqueInks[i].InkId), true);
                if (gData != null)
                {
                    dblInkCost = gData.dblUnitCost;
                    dblInkPrice = gData.dblUnitPrice;
                }
                int iCoverGroupID = (int)oSectionUniqueInks[i].CoverageGroupId;
                var InkCoverageGroup = itemsectionRepository.GetInkCoverageById(iCoverGroupID);
                InkPercentage = InkCoverageGroup != null ? (double)InkCoverageGroup.Percentage : 0;
                dblQty[0] = Convert.ToDouble((((InkPercentage * 0.01) * (intPrintArea * Convert.ToDouble(oItemSection.FinishedItemQty1)) / oRowInkDetail.InkYield) / 2) * (oPaper.InkAbsorption * 0.01));
                dblQty[1] = Convert.ToDouble((((InkPercentage * 0.01) * (intPrintArea * Convert.ToDouble(oItemSection.FinishedItemQty2)) / oRowInkDetail.InkYield) / 2) * (oPaper.InkAbsorption * 0.01));
                dblQty[2] = Convert.ToDouble((((InkPercentage * 0.01) * (intPrintArea * Convert.ToDouble(oItemSection.FinishedItemQty3)) / oRowInkDetail.InkYield) / 2) * (oPaper.InkAbsorption * 0.01));
                //(((Coverage * 0.01) *(PrintArea * ItemQty) / YeildValue))  /2)*(InkAbsorption * 0.01)  
                //dblQty(1) = Round(CDbl(oSectionUniqueInks.Rows(i).Item("Percentage")) * 0.001) * (intPrintArea * CLng(oItemSection.FinishedItemQty2))) / oRowInkDetail.InkYield) / 2), 4)
                //dblQty(2) = Round(CDbl(oSectionUniqueInks.Rows(i).Item("Percentage")) * 0.001) * (intPrintArea * CLng(oItemSection.FinishedItemQty3))) / oRowInkDetail.InkYield) / 2), 4)

                //Else
                //For BookLet
                //intQty(i) = Round((((rsaddPrint!Coverage * 0.01) * (lngPrintArea * (colItemPart.dblItemQty(i) * .SectionOnly(0)))) / dblPigValue) / 2, 5)
                //End If

                //Setting InkCost
                dblInkCost = (double)(dblInkCost / oRowInkDetail.PackageQty);
                dblInkPrice = (double)(dblInkPrice / oRowInkDetail.PackageQty);
                //Getting the Press Information

                //Setting Press Minimum DuctCost
                dblDuctCost = ConvertWeight((int)oPressDTO.MinInkDuctqty, WeightUnits.KG, (WeightUnits)oRowInkDetail.InkStandards) * dblInkCost;
                dblDuctPrice = ConvertWeight((int)oPressDTO.MinInkDuctqty, WeightUnits.KG, (WeightUnits)oRowInkDetail.InkStandards) * dblInkPrice;

                //For j As Integer = 0 To 4
                //    'Adjusting the Quantity
                //    dblQty(j) = dblQty(j) * 0.5
                //    dblQty(j) = Round(dblQty(j), 4)
                //Next

                //Calculating the Total Price for the Previous and This Multiple Qty
                dblTotalCost[0] = dblTotalCost[0] + (dblInkCost * dblQty[0] + dblDuctCost);
                dblTotalPrice[0] = dblTotalPrice[0] + (dblInkPrice * dblQty[0] + dblDuctPrice);

                if (dblTotalPrice[0] < dblMinCharge)
                {
                    dblTotalPrice[0] = dblMinCharge;
                    sMinimumCost = "1";
                }
                else
                {
                    sMinimumCost = "0";
                }
                //If dblTotalCost(0) < dblMinCharge Then dblTotalCost(0) = dblMinCharge

                if (oItemSection.Qty2 > 0)
                {
                    dblTotalCost[1] = dblTotalCost[1] + (dblInkCost * dblQty[1] + dblDuctCost);
                    //If dblTotalCost(1) < dblMinCharge Then dblTotalCost(1) = dblMinCharge

                    dblTotalPrice[1] = dblTotalPrice[1] + (dblInkPrice * dblQty[1] + dblDuctPrice);
                    if (dblTotalPrice[1] < dblMinCharge)
                    {
                        dblTotalPrice[1] = dblMinCharge;
                        sMinimumCost += "1";
                    }
                    else
                    {
                        sMinimumCost += "0";
                    }
                }

                if (oItemSection.Qty3 > 0)
                {
                    dblTotalCost[2] = dblTotalCost[2] + (dblInkCost * dblQty[2] + dblDuctCost);
                    //If dblTotalCost(2) < dblMinCharge Then dblTotalCost(2) = dblMinCharge

                    dblTotalPrice[2] = dblTotalPrice[2] + (dblInkPrice * dblQty[2] + dblDuctPrice);
                    if (dblTotalPrice[2] < dblMinCharge)
                    {
                        dblTotalPrice[2] = dblMinCharge;
                        sMinimumCost += "1";
                    }
                    else
                    {
                        sMinimumCost += "0";
                    }
                }


                //Updating the Quantity
                SectionCostCentreDetail oItemSectionCostCentreDetail = new SectionCostCentreDetail();
                oItemSectionCostCentreDetail.Qty1 = Math.Round(dblQty[0], 3);
                oItemSectionCostCentreDetail.Qty2 = Math.Round(dblQty[1], 3);
                oItemSectionCostCentreDetail.Qty3 = Math.Round(dblQty[2], 3);

                oItemSectionCostCentreDetail.CostPrice = dblInkPrice;
                oItemSectionCostCentreDetail.StockId = oRowInkDetail.StockItemId;
                oItemSectionCostCentreDetail.SupplierId = Convert.ToInt32(oRowInkDetail.SupplierId);
                oItemSectionCostCentreDetail.StockName = oRowInkDetail.ItemName;

                oItemSectionCostCentre.SectionCostCentreDetails.Add(oItemSectionCostCentreDetail);

            }
            var markup = itemsectionRepository.GetMarkupById(oInksCostcentreDTO.DefaultVAId);
            var ProfitMargin = markup != null ? markup.MarkUpRate : 0;
            //Calculating and setting cost for qty1
            if (oItemSection.Qty1 >= 0)
            {

                oItemSectionCostCentre.Qty1Charge = Math.Round(dblTotalPrice[0], 2);
                oItemSectionCostCentre.Qty1MarkUpID = oInksCostcentreDTO.DefaultVAId;
                oItemSectionCostCentre.Qty1MarkUpValue = oItemSectionCostCentre.Qty1Charge * ProfitMargin / 100;

                oItemSectionCostCentre.Qty1NetTotal = oItemSectionCostCentre.Qty1Charge + oItemSectionCostCentre.Qty1MarkUpValue;

                oItemSectionCostCentre.Qty1EstimatedStockCost = dblTotalCost[0];

                if (oJobCardOptionsDTO.IsDefaultInkColorUsed == true)
                {
                    oItemSectionCostCentre.Qty1WorkInstructions = InksDescription;
                }
            }
            if (oItemSection.Qty2 > 0)
            {
                oItemSectionCostCentre.Qty2Charge = dblTotalPrice[1];
                oItemSectionCostCentre.Qty2MarkUpID = oInksCostcentreDTO.DefaultVAId;
                oItemSectionCostCentre.Qty2MarkUpValue = oItemSectionCostCentre.Qty2Charge * ProfitMargin / 100;
                oItemSectionCostCentre.Qty2NetTotal = oItemSectionCostCentre.Qty2Charge + oItemSectionCostCentre.Qty2MarkUpValue;

                oItemSectionCostCentre.Qty2EstimatedStockCost = dblTotalCost[1];

                if (oJobCardOptionsDTO.IsDefaultInkColorUsed == true)
                {
                    oItemSectionCostCentre.Qty2WorkInstructions = InksDescription;
                }

            }

            if (oItemSection.Qty3 > 0)
            {
                oItemSectionCostCentre.Qty3Charge = dblTotalPrice[2];
                oItemSectionCostCentre.Qty3MarkUpID = oInksCostcentreDTO.DefaultVAId;
                oItemSectionCostCentre.Qty3MarkUpValue = oItemSectionCostCentre.Qty3Charge * ProfitMargin / 100;
                oItemSectionCostCentre.Qty3NetTotal = oItemSectionCostCentre.Qty3Charge + oItemSectionCostCentre.Qty3MarkUpValue;
                oItemSectionCostCentre.Qty3EstimatedStockCost = dblTotalCost[2];

                if (oJobCardOptionsDTO.IsDefaultInkColorUsed == true)
                {
                    oItemSectionCostCentre.Qty3WorkInstructions = InksDescription;
                }
            }

            if (sMinimumCost != string.Empty)
            {
                oItemSectionCostCentre.IsMinimumCost = Convert.ToInt16(sMinimumCost);
            }
            else
            {
                oItemSectionCostCentre.IsMinimumCost = 0;
            }

            oItemSectionCostCentre.Qty1 = oItemSection.Qty1;
            oItemSectionCostCentre.Qty2 = oItemSection.Qty2;
            oItemSectionCostCentre.Qty3 = oItemSection.Qty3;
            if (IsReRun == false)
            {
                oItemSectionCostCentre.Name = "Inks";
                if (oItemSection.SectionCostcentres == null)
                {
                    oItemSection.SectionCostcentres = new List<SectionCostcentre>();
                }
                oItemSection.SectionCostcentres.Add(oItemSectionCostCentre);
            }
            return oItemSection;//.tbl_section_costcentres.ToList();
        }

        public ItemSection CalculatePaperCost(ItemSection oItemSection, int PressID, bool IsReRun = false, bool IsWorkInstructionsLocked = false)
        {

            JobPreference oJobCardOptionsDTO = itemsectionRepository.GetJobPreferences(1);
            string sMinimumCost = null;
            double UnitPrice = 0;
            double PackPrice = 0;
            //'For Unit Price Calculation of Paper
            double UnitCost = 0;
            double PackCost = 0;
            Machine oPressDTO = itemsectionRepository.GetPressById(PressID);
            SectionCostCentreResource oResourceDto;
            double OrderSheetHeight = 0;
            double OrderSheetWidth = 0;
            int OrderPTV = 0;
            int PrintSheetPTV = 0;
            double[] OrderSheetQuantity = new double[3];
            double[] OrderSheetPackQuantity = new double[3];
            double[] OrderSheetSpoilage = new double[3];
            double[] OrderSheetWeight = new double[3];
            double PrintSheetHeight = 0;
            double PrintSheetWidth = 0;
            double[] PrintSheetQuantity = new double[3];
            double[] PrintSheetSpoilage = new double[3];
            int TempQuantity = 0;

            CostCentre oPaperCostCentreDTO = itemsectionRepository.GetCostCenterBySystemType((int)SystemCostCenterTypes.Paper);
            StockItem oPaperDTO = itemsectionRepository.GetStockById(Convert.ToInt64(oItemSection.StockItemID1));
            //Updating the Paper Gsm in Item Section
            oItemSection.PaperGsm = oPaperDTO.ItemWeight;
            //Goto Paper Sizes Table and get the paper size
            if (oPaperDTO.ItemSizeCustom == 0)
            {
                PaperSize oPaperSize = itemsectionRepository.GetPaperSizeById(Convert.ToInt32(oPaperDTO.ItemSizeId));
                if ((oPaperSize != null))
                {
                    OrderSheetHeight = (double)oPaperSize.Height;
                    OrderSheetWidth = (double)oPaperSize.Width;
                }
                else
                {
                    OrderSheetHeight = 0;
                    OrderSheetWidth = 0;
                }
            }
            else
            {
                OrderSheetHeight = (double)oPaperDTO.ItemSizeHeight;
                OrderSheetWidth = (double)oPaperDTO.ItemSizeWidth;
            }

            if (oItemSection.PrintViewLayout == (int)PrintViewOrientation.Landscape)
            {
                PrintSheetPTV = (int)oItemSection.PrintViewLayoutLandScape;
            }
            else
            {
                PrintSheetPTV = (int)oItemSection.PrintViewLayoutPortrait;
            }

            if (PrintSheetPTV == 0)
            {
                return oItemSection;//.tbl_section_costcentres.ToList();
            }
            //OrderSheet PTV Calculation
            //Calculating the PTV for Paper Sheet and Print Sheet
            if (oItemSection.IsSectionSizeCustom != true)
            {
                PaperSize size = itemsectionRepository.GetPaperSizeById(Convert.ToInt32(oItemSection.SectionSizeId));
                if (size != null)
                {
                    oItemSection.SectionSizeWidth = size.Width;
                    oItemSection.SectionSizeHeight = size.Height;
                }
            }
            double gutterValue = oItemSection.ItemGutterHorizontal ?? 0;
            PtvDTO oPTV = CalculatePTV(0, 0, false, false, false, Convert.ToInt32(oItemSection.SectionSizeHeight), Convert.ToInt32(oItemSection.SectionSizeWidth), Convert.ToInt32(OrderSheetHeight), Convert.ToInt32(OrderSheetWidth), 0,
            (int)GripSide.LongSide, 0, 0, gutterValue, gutterValue, gutterValue, (bool)oItemSection.isWorknTurn, false);

            if (oPTV.LandscapePTV > oPTV.PortraitPTV)
            {
                OrderPTV = oPTV.LandscapePTV;
            }
            else
            {
                OrderPTV = oPTV.PortraitPTV;
            }

            if (OrderPTV == 0)
            {
                OrderPTV = 1;
            }

            //Calculating Total Items On Sheet
            oItemSection.OverAllPTV = PrintSheetPTV * OrderPTV;
            oItemSection.WebSpoilageType = oItemSection.PrintingType == 1 ? Convert.ToInt16(1) : Convert.ToInt16(2);

            for (int i = 0; i <= 2; i++)
            {
                if (i == 0)
                {
                    TempQuantity = Convert.ToInt32(oItemSection.Qty1);
                }
                else if (i == 1)
                {
                    TempQuantity = Convert.ToInt32(oItemSection.Qty2);
                }
                else if (i == 2)
                {
                    TempQuantity = Convert.ToInt32(oItemSection.Qty3);
                }

                PrintSheetQuantity[i] = TempQuantity / PrintSheetPTV;
                //Rounding to the Next Whole Number
                PrintSheetQuantity[i] = PrintSheetQuantity[i];


                //calculate spoilage on print sheet quantity
                if (oItemSection.WebSpoilageType == Convert.ToInt32(WebSpoilageTypes.inSheets))
                {
                    PrintSheetSpoilage[i] = Convert.ToDouble(oItemSection.SetupSpoilage + (PrintSheetQuantity[i] * oItemSection.RunningSpoilage / 100));
                    PrintSheetQuantity[i] += PrintSheetSpoilage[i];
                }
                else
                {
                    //in case spoilage is in Meters
                    PrintSheetSpoilage[i] = Convert.ToDouble(oItemSection.SetupSpoilage * oItemSection.SectionSizeHeight / 1000 + (PrintSheetQuantity[i] * oItemSection.RunningSpoilage / 100 * oItemSection.SectionSizeHeight / 1000));
                    PrintSheetQuantity[i] += PrintSheetSpoilage[i];
                }
            

                OrderSheetQuantity[i] = PrintSheetQuantity[i] / OrderPTV;

                OrderSheetSpoilage[i] = PrintSheetSpoilage[i] / OrderPTV;


                OrderSheetPackQuantity[i] = Convert.ToDouble(OrderSheetQuantity[i] / oPaperDTO.PerQtyQty ?? 1);

                if (oItemSection.EstimateForWholePacks != 0)
                {
                    OrderSheetPackQuantity[i] = OrderSheetPackQuantity[i];
                }

                double areainMeters =   OrderSheetQuantity[i] * (Convert.ToDouble(OrderSheetHeight) / 1000) * (Convert.ToDouble(OrderSheetWidth) / 1000);


                OrderSheetWeight[i] = (oPaperDTO.ItemWeight.Value * areainMeters) / 1000;
            }

            ///*******
            //Updating Print Sheets Qty incl Spoilage (Pro Rata)
            oItemSection.PrintSheetQty1 = Convert.ToInt32(PrintSheetQuantity[0]);
            oItemSection.PrintSheetQty2 = Convert.ToInt32(PrintSheetQuantity[1]);
            oItemSection.PrintSheetQty3 = Convert.ToInt32(PrintSheetQuantity[2]);

            //Update the Finished Item Qty incl Spoilage (Pro Rata)
            oItemSection.FinishedItemQty1 = Convert.ToInt32(PrintSheetSpoilage[0]) + oItemSection.Qty1;
            oItemSection.FinishedItemQty2 = Convert.ToInt32(PrintSheetSpoilage[1]) + oItemSection.Qty2;
            oItemSection.FinishedItemQty3 = Convert.ToInt32(PrintSheetSpoilage[2]) + oItemSection.Qty3;

            //Updating the Weight of paper used for this Section Only ( PRO RATA )
            oItemSection.PaperWeight1 = OrderSheetWeight[0];
            oItemSection.PaperWeight2 = OrderSheetWeight[1];
            oItemSection.PaperWeight3 = OrderSheetWeight[2];

            //new logic for paper cost and price calculation Implemented by Muzzammil
            //dblUPrice = Round(oPaperDTO.CostPrice / oPaperDTO.PackageQty, 4)

            GlobalData gData = GetItemPriceCost((int)oItemSection.StockItemID1, true);
            if (gData != null)
            {
                UnitCost = gData.dblUnitCost;
                UnitPrice = gData.dblUnitPrice;
                PackCost = gData.dblPackCost;
                PackPrice = gData.dblPackPrice;
            }


            //Updating the Cost price per pack of Stock used in Item Section
            oItemSection.PaperPackPrice = PackPrice;
            //* oPaperDTO.PackageQty

            //Setting the Cost for each quantity
            SectionCostcentre oItemSectionCostCenter;
            if (IsReRun == false)
            {
                oItemSectionCostCenter = new SectionCostcentre();
                oItemSectionCostCenter.ItemSectionId = oItemSection.ItemSectionId;
                oItemSectionCostCenter.CostCentreId = oPaperCostCentreDTO.CostCentreId;
                oItemSectionCostCenter.SystemCostCentreType = (int)SystemCostCenterTypes.Paper;
                oItemSectionCostCenter.Order = 102;
                oItemSectionCostCenter.IsOptionalExtra = 0;
                oItemSectionCostCenter.CostCentreType = 1;
                oItemSectionCostCenter.IsDirectCost = Convert.ToInt16(oPaperCostCentreDTO.IsDirectCost);
                oItemSectionCostCenter.SectionCostCentreDetails = new List<SectionCostCentreDetail>();
            }
            else
            {
                foreach (var sectioncc in oItemSection.SectionCostcentres)
                {
                    if (sectioncc.SystemCostCentreType == (int)SystemCostCenterTypes.Paper)
                    {
                        oItemSectionCostCenter = sectioncc;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
                oItemSectionCostCenter = oItemSection.SectionCostcentres.FirstOrDefault();
            }

            oItemSectionCostCenter.IsPrintable = Convert.ToInt16(oJobCardOptionsDTO.IsDefaultStockDetail);

            //if paper is not supplied and we have to use it ourself then add the prices :)

            if (oItemSection.IsPaperSupplied != true)
            {
                oItemSectionCostCenter.Qty1Charge = OrderSheetPackQuantity[0] * UnitPrice;

                if (oItemSectionCostCenter.Qty1Charge < oPaperCostCentreDTO.MinimumCost)
                {
                    oItemSectionCostCenter.Qty1Charge = oPaperCostCentreDTO.MinimumCost;
                    sMinimumCost = "1";
                }
                else
                {
                    sMinimumCost = "0";
                }
                var markup = itemsectionRepository.GetMarkupById(oPaperCostCentreDTO.DefaultVAId);
                var ProfitMargin = markup != null ? markup.MarkUpRate : 0;
                oItemSectionCostCenter.Qty1MarkUpID = oPaperCostCentreDTO.DefaultVAId;
                oItemSectionCostCenter.Qty1MarkUpValue = oItemSectionCostCenter.Qty1Charge * ProfitMargin / 100;
                oItemSectionCostCenter.Qty1NetTotal = oItemSectionCostCenter.Qty1Charge + oItemSectionCostCenter.Qty1MarkUpValue;

                oItemSectionCostCenter.Qty1EstimatedStockCost = OrderSheetPackQuantity[0] * UnitCost;

                if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsDefaultStockDetail == true)
                {
                    if (oJobCardOptionsDTO.IsPaperSheetQty == true)
                    {
                        oItemSectionCostCenter.Qty1WorkInstructions = "Order Sheet Qty (inc. spoilage):=" + Math.Ceiling(OrderSheetQuantity[0]) + " Sheets" + Environment.NewLine;
                    }

                    if (oJobCardOptionsDTO.IsSpoilageAllowed == true)
                    {
                        oItemSectionCostCenter.Qty1WorkInstructions += "Order Sheet Spoilage:=" + Math.Ceiling(OrderSheetSpoilage[0]) + " Sheets" + Environment.NewLine;
                    }

                    if (oJobCardOptionsDTO.IsOrderSheetSize == true)
                    {
                        oItemSectionCostCenter.Qty1WorkInstructions += "Order Sheet Size (Flat):= " + Convert.ToString(OrderSheetHeight) + " x " + Convert.ToString(OrderSheetWidth) + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                    }

                    oItemSectionCostCenter.Qty1WorkInstructions += "Item Size (Flat):= " + Convert.ToString(oItemSection.ItemSizeHeight) + " x " + Convert.ToString(oItemSection.ItemSizeWidth) + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;

                    if (oJobCardOptionsDTO.IsPaperWeight == true)
                    {
                        oItemSectionCostCenter.Qty1WorkInstructions += "Total Paper Weight:=" + Math.Round(OrderSheetWeight[0], 2) + " " + itemsectionRepository.GetWeightUnitName((int)CompanyGeneralSettings().SystemWeightUnit) + Environment.NewLine;
                    }
                    oItemSectionCostCenter.Qty1WorkInstructions += "Paper Supplied:=No ";
                }

                if (oItemSection.Qty2 > 0)
                {
                    oItemSectionCostCenter.Qty2Charge = OrderSheetPackQuantity[1] * UnitPrice;

                    if (oItemSectionCostCenter.Qty2Charge < oPaperCostCentreDTO.MinimumCost)
                    {
                        oItemSectionCostCenter.Qty2Charge = oPaperCostCentreDTO.MinimumCost;
                        sMinimumCost += "1";
                    }
                    else
                    {
                        sMinimumCost += "0";
                    }

                    oItemSectionCostCenter.Qty2MarkUpID = oPaperCostCentreDTO.DefaultVAId;
                    oItemSectionCostCenter.Qty2MarkUpValue = oItemSectionCostCenter.Qty2Charge * ProfitMargin / 100;
                    oItemSectionCostCenter.Qty2NetTotal = oItemSectionCostCenter.Qty2Charge + oItemSectionCostCenter.Qty2MarkUpValue;

                    oItemSectionCostCenter.Qty2EstimatedStockCost = OrderSheetPackQuantity[1] * UnitCost;

                    if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsDefaultStockDetail == true)
                    {
                        if (oJobCardOptionsDTO.IsPaperSheetQty == true)
                        {
                            oItemSectionCostCenter.Qty2WorkInstructions = "Order Sheet Qty (inc. spoilage):=" + Math.Ceiling(OrderSheetQuantity[1]) + " Sheets" + Environment.NewLine;
                        }

                        if (oJobCardOptionsDTO.IsSpoilageAllowed == true)
                        {
                            oItemSectionCostCenter.Qty2WorkInstructions += "Order Sheet Spoilage=:" + Math.Ceiling(OrderSheetSpoilage[1]) + " Sheets" + Environment.NewLine;
                        }

                        if (oJobCardOptionsDTO.IsOrderSheetSize == true)
                        {
                            oItemSectionCostCenter.Qty2WorkInstructions += "Order Sheet Size (Flat):= " + Convert.ToString(OrderSheetHeight) + " x " + Convert.ToString(OrderSheetWidth) + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                        }

                        oItemSectionCostCenter.Qty2WorkInstructions += "Item Size (Flat):= " + Convert.ToString(oItemSection.ItemSizeHeight) + " x " + Convert.ToString(oItemSection.ItemSizeHeight) + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;

                        if (oJobCardOptionsDTO.IsPaperWeight == true)
                        {
                            oItemSectionCostCenter.Qty2WorkInstructions += "Total Paper Weight:=" + Math.Round(OrderSheetWeight[1], 2) + " " + itemsectionRepository.GetWeightUnitName((int)CompanyGeneralSettings().SystemWeightUnit) + Environment.NewLine;
                        }
                        oItemSectionCostCenter.Qty2WorkInstructions += "Paper Supplied:=No ";
                    }
                }

                if (oItemSection.Qty3 > 0)
                {
                    oItemSectionCostCenter.Qty3Charge = OrderSheetPackQuantity[2] * UnitPrice;

                    if (oItemSectionCostCenter.Qty3Charge < oPaperCostCentreDTO.MinimumCost)
                    {
                        oItemSectionCostCenter.Qty3Charge = oPaperCostCentreDTO.MinimumCost;
                        sMinimumCost += "1";
                    }
                    else
                    {
                        sMinimumCost += "0";
                    }

                    oItemSectionCostCenter.Qty3MarkUpID = oPaperCostCentreDTO.DefaultVAId;
                    oItemSectionCostCenter.Qty3MarkUpValue = oItemSectionCostCenter.Qty3Charge * ProfitMargin / 100;
                    oItemSectionCostCenter.Qty3NetTotal = oItemSectionCostCenter.Qty3Charge + oItemSectionCostCenter.Qty3MarkUpValue;

                    oItemSectionCostCenter.Qty3EstimatedStockCost = OrderSheetPackQuantity[2] * UnitCost;

                    if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsDefaultStockDetail == true)
                    {
                        if (oJobCardOptionsDTO.IsPaperSheetQty == true)
                        {
                            oItemSectionCostCenter.Qty3WorkInstructions = "Order Sheet Qty (inc. spoilage):=" + Math.Ceiling(OrderSheetQuantity[2]) + " Sheets" + Environment.NewLine;
                        }

                        if (oJobCardOptionsDTO.IsSpoilageAllowed == true)
                        {
                            oItemSectionCostCenter.Qty3WorkInstructions += "Order Sheet Spoilage=:" + Math.Ceiling(OrderSheetSpoilage[2]) + " Sheets" + Environment.NewLine;
                        }

                        if (oJobCardOptionsDTO.IsOrderSheetSize == true)
                        {
                            oItemSectionCostCenter.Qty3WorkInstructions += "Order Sheet Size (Flat):= " + Convert.ToString(OrderSheetHeight) + " x " + Convert.ToString(OrderSheetWidth) + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                        }

                        oItemSectionCostCenter.Qty3WorkInstructions += "Item Size (Flat):= " + Convert.ToString(oItemSection.ItemSizeHeight) + " x " + Convert.ToString(oItemSection.ItemSizeHeight) + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;

                        if (oJobCardOptionsDTO.IsPaperWeight == true)
                        {
                            oItemSectionCostCenter.Qty3WorkInstructions += "Total Paper Weight:=" + Math.Round(OrderSheetWeight[2], 2) + " " + itemsectionRepository.GetWeightUnitName((int)CompanyGeneralSettings().SystemWeightUnit) + Environment.NewLine;
                        }
                        oItemSectionCostCenter.Qty3WorkInstructions += "Paper Supplied:=No ";
                    }

                }

            }
            else
            {
                if (oItemSection.Qty1 > 0)
                {
                    oItemSectionCostCenter.Qty1Charge = 0;
                    oItemSectionCostCenter.Qty1MarkUpID = 0;
                    oItemSectionCostCenter.Qty1MarkUpValue = 0;
                    oItemSectionCostCenter.Qty1NetTotal = 0;
                    oItemSectionCostCenter.Qty1EstimatedStockCost = 0;
                    sMinimumCost = "0";

                    if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsDefaultStockDetail == true)
                    {
                        if (oJobCardOptionsDTO.IsPaperSheetQty == true)
                        {
                            oItemSectionCostCenter.Qty1WorkInstructions = "Order Sheet Qty (inc. spoilage):=" + Math.Ceiling(OrderSheetQuantity[0]) + " Sheets" + Environment.NewLine;
                        }

                        if (oJobCardOptionsDTO.IsSpoilageAllowed == true)
                        {
                            oItemSectionCostCenter.Qty1WorkInstructions += "Order Sheet Spoilage=:" + Math.Ceiling(PrintSheetSpoilage[0]) + " Sheets" + Environment.NewLine;
                        }

                        if (oJobCardOptionsDTO.IsOrderSheetSize == true)
                        {
                            oItemSectionCostCenter.Qty1WorkInstructions += "Order Sheet Size (Flat):= " + Convert.ToString(OrderSheetHeight) + " x " + Convert.ToString(OrderSheetWidth) + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                        }

                        oItemSectionCostCenter.Qty1WorkInstructions += "Item Size (Flat):= " + Convert.ToString(oItemSection.ItemSizeHeight) + " x " + Convert.ToString(oItemSection.ItemSizeHeight) + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;

                        if (oJobCardOptionsDTO.IsPaperWeight == true)
                        {
                            oItemSectionCostCenter.Qty1WorkInstructions += "Total Paper Weight:=" + Math.Round(OrderSheetWeight[0], 2) + " " + itemsectionRepository.GetWeightUnitName((int)CompanyGeneralSettings().SystemWeightUnit) + Environment.NewLine;
                        }
                        oItemSectionCostCenter.Qty1WorkInstructions += "Paper Supplied:=Yes ";
                    }
                }
                if (oItemSection.Qty2 > 0)
                {
                    oItemSectionCostCenter.Qty2Charge = 0;
                    oItemSectionCostCenter.Qty2MarkUpID = 0;
                    oItemSectionCostCenter.Qty2MarkUpValue = 0;
                    oItemSectionCostCenter.Qty2NetTotal = 0;
                    oItemSectionCostCenter.Qty2EstimatedStockCost = 0;
                    sMinimumCost += "0";
                    if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsDefaultStockDetail == true)
                    {
                        if (oJobCardOptionsDTO.IsPaperSheetQty == true)
                        {
                            oItemSectionCostCenter.Qty2WorkInstructions = "Order Sheet Qty (inc. spoilage):=" + Math.Ceiling(OrderSheetQuantity[1]) + " Sheets" + Environment.NewLine;
                        }

                        if (oJobCardOptionsDTO.IsSpoilageAllowed == true)
                        {
                            oItemSectionCostCenter.Qty2WorkInstructions += "Order Sheet Spoilage=:" + Math.Ceiling(PrintSheetSpoilage[1]) + " Sheets" + Environment.NewLine;
                        }

                        if (oJobCardOptionsDTO.IsOrderSheetSize == true)
                        {
                            oItemSectionCostCenter.Qty2WorkInstructions += "Order Sheet Size (Flat):= " + Convert.ToString(OrderSheetHeight) + " x " + Convert.ToString(OrderSheetWidth) + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                        }

                        oItemSectionCostCenter.Qty2WorkInstructions += "Item Size (Flat):= " + Convert.ToString(oItemSection.ItemSizeHeight) + " x " + Convert.ToString(oItemSection.ItemSizeHeight) + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;

                        if (oJobCardOptionsDTO.IsPaperWeight == true)
                        {
                            oItemSectionCostCenter.Qty2WorkInstructions += "Total Paper Weight:=" + Math.Round(OrderSheetWeight[1], 2) + " " + itemsectionRepository.GetWeightUnitName((int)CompanyGeneralSettings().SystemWeightUnit) + Environment.NewLine;
                        }
                        oItemSectionCostCenter.Qty2WorkInstructions += "Paper Supplied:=Yes ";

                    }
                }
                if (oItemSection.Qty3 > 0)
                {
                    oItemSectionCostCenter.Qty3Charge = 0;
                    oItemSectionCostCenter.Qty3MarkUpID = 0;
                    oItemSectionCostCenter.Qty3MarkUpValue = 0;
                    oItemSectionCostCenter.Qty3NetTotal = 0;
                    oItemSectionCostCenter.Qty3EstimatedStockCost = 0;
                    sMinimumCost += "0";
                    if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsDefaultStockDetail == true)
                    {
                        if (oJobCardOptionsDTO.IsPaperSheetQty == true)
                        {
                            oItemSectionCostCenter.Qty3WorkInstructions = "Order Sheet Qty (inc. spoilage):=" + Math.Ceiling(OrderSheetQuantity[2]) + " Sheets" + Environment.NewLine;
                        }

                        if (oJobCardOptionsDTO.IsSpoilageAllowed == true)
                        {
                            oItemSectionCostCenter.Qty3WorkInstructions += "Order Sheet Spoilage=:" + Math.Ceiling(PrintSheetSpoilage[2]) + " Sheets" + Environment.NewLine;
                        }

                        if (oJobCardOptionsDTO.IsOrderSheetSize == true)
                        {
                            oItemSectionCostCenter.Qty3WorkInstructions += "Order Sheet Size (Flat):= " + Convert.ToString(OrderSheetHeight) + " x " + Convert.ToString(OrderSheetWidth) + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                        }

                        oItemSectionCostCenter.Qty3WorkInstructions += "Item Size (Flat):= " + Convert.ToString(oItemSection.ItemSizeHeight) + " x " + Convert.ToString(oItemSection.ItemSizeHeight) + itemsectionRepository.GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;

                        if (oJobCardOptionsDTO.IsPaperWeight == true)
                        {
                            oItemSectionCostCenter.Qty3WorkInstructions += "Total Paper Weight:=" + Math.Round(OrderSheetWeight[2], 2) + " " + itemsectionRepository.GetWeightUnitName((int)CompanyGeneralSettings().SystemWeightUnit) + Environment.NewLine;
                        }
                        oItemSectionCostCenter.Qty3WorkInstructions += "Paper Supplied:=Yes ";
                    }
                }

            }

            if (sMinimumCost != string.Empty)
            {
                oItemSectionCostCenter.IsMinimumCost = Convert.ToInt16(sMinimumCost);
            }
            else
            {
                oItemSectionCostCenter.IsMinimumCost = 0;
            }

            SectionCostCentreDetail oItemSectionCostCenterDetail;
            if (IsReRun == false)
            {
                oItemSectionCostCenterDetail = new SectionCostCentreDetail();
            }
            else
            {
                oItemSectionCostCenterDetail = oItemSectionCostCenter.SectionCostCentreDetails.FirstOrDefault();
            }

            if (oItemSection.Qty1 > 0)
            {
                oItemSectionCostCenterDetail.Qty1 = Math.Ceiling(OrderSheetQuantity[0]);
            }
            if (oItemSection.Qty2 > 0)
            {
                oItemSectionCostCenterDetail.Qty2 = Math.Ceiling(OrderSheetQuantity[1]);
            }
            if (oItemSection.Qty3 > 0)
            {
                oItemSectionCostCenterDetail.Qty3 = Math.Ceiling(OrderSheetQuantity[2]);
            }

            if (oItemSection.IsPaperSupplied != true)
            {
                oItemSectionCostCenterDetail.CostPrice = UnitPrice / oPaperDTO.PerQtyQty;
            }
            else
            {
                oItemSectionCostCenterDetail.CostPrice = 0;
            }
            oItemSectionCostCenter.Name = "Paper ( " + oPaperDTO.ItemName + " )";
            oItemSectionCostCenterDetail.StockId = oPaperDTO.StockItemId;
            oItemSectionCostCenterDetail.StockName = oPaperDTO.ItemName;
            oItemSectionCostCenterDetail.SupplierId = Convert.ToInt32(oPaperDTO.SupplierId);
            //oItemSectionCostCenter.SectionCostCentreResources.ToList().ForEach(c => db.SectionCostCentreResources.Remove(c));
            //adding new resources.
            oItemSectionCostCenter.Qty1 = oItemSection.Qty1;
            oItemSectionCostCenter.Qty2 = oItemSection.Qty2;
            oItemSectionCostCenter.Qty3 = oItemSection.Qty3;

            if (IsReRun == false)
            {
                oItemSectionCostCenter.SectionCostCentreDetails.Add(oItemSectionCostCenterDetail);
                oItemSection.SectionCostcentres.Add(oItemSectionCostCenter);
            }

            return oItemSection;


        }

        public ItemSection CalculateGuillotineCost(ItemSection oItemSection, int PressID, bool IsReRun = false, bool IsWorkInstructionsLocked = false)
        {

            JobPreference oJobCardOptionsDTO = itemsectionRepository.GetJobPreferences(1);
            //bool functionReturnValue = false;
            try
            {
                string sMinimumCost = null;
                //Total Paper Sheet Area
                double intOrderArea = 0;
                //Total PrintSize Area 
                double intPrintArea = 0;
                //Item Area
                double intItemArea = 0;
                byte btRge = 0;
                double dblRge = 0;
                double dblCut = 0;
                double dblGuillMinCharge = 0;

                int[] intPaperRange = new int[6];
                int[] intPaperThick = new int[6];

                double dblBundle = 0;
                double intOrderSheetHeight = 0;
                double intOrderSheetWidth = 0;

                CostCentre oCostCentreDTO = new CostCentre();

                double dblGuillotineCost = 0;
                double dblGuillotinePrice = 0;
                double[] dblFirstTrimCost = new double[3];
                double[] dblFirstTrimPrice = new double[3];
                int intFirstTrimCut = 0;
                double[] dblSecondTrimCost = new double[3];
                double[] dblSecondTrimPrice = new double[3];
                int intSecondTrimCut = 0;
                int intItemPTV = 0;
                int intOrderPTV = 0;
                int[] int2ndBundles = new int[3];
                int[] TotalCutsFirstTrim = new int[3];
                int[] int1stBundles = new int[3];
                int[] TotalCutsSecondTrim = new int[3];
                int[] intPrintSheetQty = new int[3];
                //Variable to Hold Print Sheets required
                int[] intOrderSheetQty = new int[3];
                //Variable to Hold Order Sheets required
                int i = 0;

                //Getting the Paper Used Information
                StockItem oPaperDTO = itemsectionRepository.GetStockById(Convert.ToInt64(oItemSection.StockItemID1));
                CostCentre oGuillotinCostCentreDTO = itemsectionRepository.GetCostCenterBySystemType((int)SystemCostCenterTypes.Guillotine);
                //Goto Paper Sizes Table and get the paper size
                if (oPaperDTO.ItemSizeCustom == 0)
                {
                    PaperSize orowPaperSize = new PaperSize();
                    orowPaperSize = itemsectionRepository.GetPaperSizeById(Convert.ToInt32(oPaperDTO.ItemSizeId));
                    if ((orowPaperSize != null))
                    {
                        intOrderSheetHeight = Convert.ToDouble(orowPaperSize.Height);
                        intOrderSheetWidth = Convert.ToDouble(orowPaperSize.Width);
                    }
                    else
                    {
                        intOrderSheetHeight = 0;
                        intOrderSheetWidth = 0;
                    }
                }
                else
                {
                    intOrderSheetHeight = Convert.ToInt32(oPaperDTO.ItemSizeHeight);
                    intOrderSheetWidth = Convert.ToInt32(oPaperDTO.ItemSizeWidth);
                }

                intOrderArea = intOrderSheetHeight * intOrderSheetWidth;

                intPrintArea = Convert.ToDouble(oItemSection.SectionSizeHeight * oItemSection.SectionSizeWidth);
                intItemArea = Convert.ToDouble(oItemSection.ItemSizeHeight * oItemSection.ItemSizeWidth);

                //Dim oGuillotineCostCentreDTO As Model.CostCentres.CostCentreDTO = BLL.CostCentres.CostCentre.GetCostCentreSummaryByID(19, GlobalData) ' GetSystemCostCentreDetail(19)

                //Getting the Guillitone information
                Machine oModelGuillotine = itemsectionRepository.GetPressById(Convert.ToInt32(oItemSection.GuillotineId));

                // DataRow[] oRow = oModelGuillotine.Methods.Select("DefaultMethod=1");

                dblGuillMinCharge = Convert.ToDouble(oModelGuillotine.MinimumCharge);
                //Dim oModelGuillotinecalc As Model.Machine.GuilotineCalc = oModelGuillotine.GuilotineCalc
                //Model.LookupMethods.MethodDTO oModelLookUpMethod = BLL.LookupMethods.Method.GetMachineLookUpMethod(GlobalData, Convert.ToInt32(oRow[0]["MethodID"]));
                //Model.LookupMethods.GuilotineCalcDTO oModelGuillotinecalc = oModelLookUpMethod.GuilotineLookUp;
                MachineGuillotineCalc oModelGuillotinecalc = itemsectionRepository.GetGuillotineCalcMethod(Convert.ToInt64(oModelGuillotine.LookupMethodId));

                //Getting the Guillitone Lookup      'Starting Index from 1  in Class Index is from 0
                intPaperRange[1] = Convert.ToInt32(oModelGuillotinecalc.PaperWeight1);
                intPaperRange[2] = Convert.ToInt32(oModelGuillotinecalc.PaperWeight2);
                intPaperRange[3] = Convert.ToInt32(oModelGuillotinecalc.PaperWeight3);
                intPaperRange[4] = Convert.ToInt32(oModelGuillotinecalc.PaperWeight4);
                intPaperRange[5] = Convert.ToInt32(oModelGuillotinecalc.PaperWeight5);

                intPaperThick[1] = Convert.ToInt32(oModelGuillotinecalc.PaperThroatQty1);
                intPaperThick[2] = Convert.ToInt32(oModelGuillotinecalc.PaperThroatQty2);
                intPaperThick[3] = Convert.ToInt32(oModelGuillotinecalc.PaperThroatQty3);
                intPaperThick[4] = Convert.ToInt32(oModelGuillotinecalc.PaperThroatQty4);
                intPaperThick[5] = Convert.ToInt32(oModelGuillotinecalc.PaperThroatQty5);

                double dblPaperRange1 = Convert.ToDouble(intPaperRange[1]);
                double dblPaperRange2 = Convert.ToDouble(intPaperRange[2]);
                double dblPaperRange3 = Convert.ToDouble(intPaperRange[3]);
                double dblPaperRange4 = Convert.ToDouble(intPaperRange[4]);
                double dblPaperRange5 = Convert.ToDouble(intPaperRange[5]);

                if (oPaperDTO.ItemWeight == (int)intPaperRange[1])
                {
                    btRge = 1;
                    dblRge = 0;
                }
                else if (oPaperDTO.ItemWeight == (int)intPaperRange[2])
                {
                    btRge = 2;
                    dblRge = 0;
                }
                else if (oPaperDTO.ItemWeight == (int)intPaperRange[3])
                {
                    btRge = 3;
                    dblRge = 0;
                }
                else if (oPaperDTO.ItemWeight == (int)intPaperRange[4])
                {
                    btRge = 4;
                    dblRge = 0;
                }
                else if (oPaperDTO.ItemWeight == (int)intPaperRange[2])
                {
                    btRge = 5;
                    dblRge = 0;
                }
                else
                {
                    if (oPaperDTO.ItemWeight > intPaperRange[1] && oPaperDTO.ItemWeight < intPaperRange[2])
                    {
                        btRge = 1;

                        dblRge = (double)((dblPaperRange2 - oPaperDTO.ItemWeight) / dblPaperRange1);
                    }
                    else if (oPaperDTO.ItemWeight > intPaperRange[2] & oPaperDTO.ItemWeight < intPaperRange[3])
                    {
                        btRge = 2;
                        dblRge = (double)((dblPaperRange3 - oPaperDTO.ItemWeight) / dblPaperRange2);
                    }
                    else if (oPaperDTO.ItemWeight > intPaperRange[3] & oPaperDTO.ItemWeight < intPaperRange[4])
                    {
                        btRge = 3;
                        dblRge = (double)((dblPaperRange4 - oPaperDTO.ItemWeight) / dblPaperRange3);
                    }
                    else if (oPaperDTO.ItemWeight > intPaperRange[4] & oPaperDTO.ItemWeight < intPaperRange[5])
                    {
                        btRge = 4;
                        dblRge = (double)((dblPaperRange5 - oPaperDTO.ItemWeight) / dblPaperRange4);
                    }
                }
                //  Get CutPackQty of PaperPerCut for that GSM range
                if (btRge == 5)
                {
                    dblCut = intPaperThick[btRge];
                }
                else
                {
                    //Calculating the Cut
                    dblCut = intPaperThick[btRge + 1] - ((intPaperThick[btRge + 1] - intPaperThick[btRge]) * dblRge);
                }

                if (dblCut == 0)
                {
                    //return functionReturnValue;
                    //Please select a larger Order paper or a smaller Print size.word
                }



                //Have Used 1 for LandScape
                //If oItemSection.PrintViewLayout = 0 Then
                if (oItemSection.PrintViewLayout == (int)PrintViewOrientation.Portrait)
                {
                    intItemPTV = Convert.ToInt32(oItemSection.PrintViewLayoutPortrait);
                }
                else
                {
                    intItemPTV = Convert.ToInt32(oItemSection.PrintViewLayoutLandScape);
                }

                //Calculating the PTV of Working Sheet and Print Sheet / Order PTV
                if (oItemSection.IsSectionSizeCustom != true)
                {
                    PaperSize size = itemsectionRepository.GetPaperSizeById(Convert.ToInt32(oItemSection.SectionSizeId));
                    if (size != null)
                    {
                        oItemSection.SectionSizeWidth = size.Width;
                        oItemSection.SectionSizeHeight = size.Height;
                    }
                }
                double dblGutterValue = oItemSection.ItemGutterHorizontal ?? 0;
                PtvDTO oPTV = CalculatePTV(0, 0, false, false, false, Convert.ToInt32(oItemSection.SectionSizeHeight), Convert.ToInt32(oItemSection.SectionSizeWidth), Convert.ToInt32(intOrderSheetHeight), Convert.ToInt32(intOrderSheetWidth), 0,
                (int)GripSide.LongSide, 0, 0, dblGutterValue, dblGutterValue, dblGutterValue, oItemSection.isWorknTurn ?? false, oItemSection.isWorkntumble ?? false);

                if (oPTV.LandscapePTV > oPTV.PortraitPTV)
                {
                    intOrderPTV = oPTV.LandscapePTV;
                }
                else
                {
                    intOrderPTV = oPTV.PortraitPTV;
                }

                if (intItemPTV > 0)
                {
                    intPrintSheetQty[0] = Convert.ToInt32(oItemSection.Qty1 / intItemPTV);
                    intPrintSheetQty[0] += Convert.ToInt32(intPrintSheetQty[0] * oItemSection.RunningSpoilage / 100) + (int)oItemSection.SetupSpoilage;
                    intPrintSheetQty[1] = Convert.ToInt32(oItemSection.Qty2 / intItemPTV);
                    intPrintSheetQty[1] += Convert.ToInt32(intPrintSheetQty[1] * oItemSection.RunningSpoilage / 100) + (int)oItemSection.SetupSpoilage;
                    intPrintSheetQty[2] = Convert.ToInt32(oItemSection.Qty3 / intItemPTV);
                    intPrintSheetQty[2] += Convert.ToInt32(intPrintSheetQty[2] * oItemSection.RunningSpoilage / 100) + (int)oItemSection.SetupSpoilage;
                }


                if (intOrderPTV > 0)
                {
                    intOrderSheetQty[0] = Convert.ToInt32(intPrintSheetQty[0] / intOrderPTV);
                    intOrderSheetQty[0] += Convert.ToInt32(intOrderSheetQty[0] * oItemSection.RunningSpoilage / 100) + (int)oItemSection.SetupSpoilage;
                    intOrderSheetQty[1] = Convert.ToInt32(intPrintSheetQty[1] / intOrderPTV);
                    intOrderSheetQty[1] += Convert.ToInt32(intOrderSheetQty[1] * oItemSection.RunningSpoilage / 100) + (int)oItemSection.SetupSpoilage;
                    intOrderSheetQty[2] = Convert.ToInt32(intPrintSheetQty[2] / intOrderPTV);
                    intOrderSheetQty[2] += Convert.ToInt32(intOrderSheetQty[2] * oItemSection.RunningSpoilage / 100) + (int)oItemSection.SetupSpoilage;
                }
                //Checking if First Trim
                if (Convert.ToBoolean(oItemSection.IsFirstTrim) == true && intOrderPTV > 0)
                {
                    if (intOrderPTV == 0)
                    {
                        //return functionReturnValue;
                        //Please select a larger Order paper or a smaller Print size.
                    }

                    if (intOrderPTV == 1 && intOrderArea == intPrintArea)
                    {
                        //The Print Area=OrderArea
                        intFirstTrimCut = 0;
                        dblFirstTrimCost[0] = 0;
                        dblFirstTrimCost[1] = 0;
                        dblFirstTrimCost[2] = 0;

                        dblFirstTrimPrice[0] = 0;
                        dblFirstTrimPrice[1] = 0;
                        dblFirstTrimPrice[2] = 0;

                    }
                    else if (intOrderArea != intPrintArea)
                    {
                        //intFirstTrimCut = intOrderPTV + 3
                        MachineGuilotinePtv PTVTable = itemsectionRepository.GetGuillotinePtv(oModelGuillotine.MachineId, intOrderPTV);

                        if ((PTVTable != null))
                        {
                            intFirstTrimCut = PTVTable.Noofcutswithoutgutters;
                        }
                    }
                    else
                    {
                        intFirstTrimCut = 0;
                    }
                    //'Guillotine Charge for 1st Trim
                    //Determining the Bundle
                    for (int j = 0; j <= 2; j++)
                    {
                        dblBundle = (intOrderSheetQty[j] / dblCut);
                        dblBundle = Math.Ceiling(dblBundle);
                        //Calculating Charge for the First Trim  
                        dblFirstTrimCost[j] = Math.Round((dblBundle * intFirstTrimCut) * (double)oModelGuillotine.CostPerCut, 2);
                        dblFirstTrimPrice[j] = Math.Round((dblBundle * intFirstTrimCut) * oModelGuillotine.PricePerCut, 2);
                        int1stBundles[j] = Convert.ToInt32(dblBundle);
                        TotalCutsFirstTrim[j] = Convert.ToInt32(dblBundle) * intFirstTrimCut;
                    }
                }


                //For 2nd Trim
                if (Convert.ToBoolean(oItemSection.IsSecondTrim) == true & intItemPTV > 0)
                {
                    if (intItemPTV == 1 & intPrintArea == intItemArea)
                    {
                        intSecondTrimCut = 0;
                        dblSecondTrimCost[0] = 0;
                        dblSecondTrimCost[1] = 0;
                        dblSecondTrimCost[2] = 0;

                        dblSecondTrimPrice[0] = 0;
                        dblSecondTrimPrice[1] = 0;
                        dblSecondTrimPrice[2] = 0;
                    }
                    else
                    {
                        MachineGuilotinePtv PTVTable = itemsectionRepository.GetGuillotinePtv(oModelGuillotine.MachineId, intItemPTV);
                        //DataRow[] oRows = oModelGuillotinecalc.PTVTable.Select("NoOfUps=" + intItemPTV.ToString());
                        if (PTVTable != null)
                        {
                            if (oItemSection.IncludeGutter == false)
                            {
                                intSecondTrimCut = PTVTable.Noofcutswithoutgutters;
                            }
                            else
                            {
                                intSecondTrimCut = PTVTable.Noofcutswithgutters;
                            }
                        }

                    }

                    for (int j = 0; j <= 2; j++)
                    {
                        //'Guillotine Charge for 2nd Trim
                        dblBundle = (intPrintSheetQty[j] / dblCut);
                        if (string.Compare("1", dblBundle.ToString()) > 0)
                        {
                            dblBundle = Math.Round(dblBundle, 0);
                        }
                        //To Understand the logic
                        //if (Strings.InStr(1, Convert.ToString(dblBundle), ".", Constants.vbTextCompare) > 0)
                        //{
                        //    dblBundle = Convert.ToDouble(Strings.Left(Convert.ToString(dblBundle), Strings.InStr(1, Convert.ToString(dblBundle), ".", Constants.vbTextCompare) - 1)) + 1;
                        //}
                        dblSecondTrimCost[j] = Math.Round((dblBundle * intSecondTrimCut) * (double)oModelGuillotine.CostPerCut, 2);
                        dblSecondTrimPrice[j] = Math.Round((dblBundle * intSecondTrimCut) * (double)oModelGuillotine.PricePerCut, 2);
                        int2ndBundles[j] = Convert.ToInt32(dblBundle);
                        TotalCutsSecondTrim[j] = Convert.ToInt32(dblBundle) * intSecondTrimCut;
                    }
                }

                //updating and Setting the Value of First Trim Cut and Second Trim Cut
                oItemSection.GuillotineFirstCut = intFirstTrimCut;
                oItemSection.GuillotineSecondCut = intSecondTrimCut;

                oItemSection.GuillotineQty1BundlesFirstTrim = int1stBundles[0];
                oItemSection.GuillotineQty2BundlesFirstTrim = int1stBundles[1];
                oItemSection.GuillotineQty3BundlesFirstTrim = int1stBundles[2];

                oItemSection.GuillotineQty1BundlesSecondTrim = int2ndBundles[0];
                oItemSection.GuillotineQty2BundlesSecondTrim = int2ndBundles[1];
                oItemSection.GuillotineQty3BundlesSecondTrim = int2ndBundles[2];

                oItemSection.GuillotineQty1FirstTrimCuts = TotalCutsFirstTrim[0];
                oItemSection.GuillotineQty2FirstTrimCuts = TotalCutsFirstTrim[1];
                oItemSection.GuillotineQty3FirstTrimCuts = TotalCutsFirstTrim[2];


                oItemSection.GuillotineQty1SecondTrimCuts = TotalCutsSecondTrim[0];
                oItemSection.GuillotineQty2SecondTrimCuts = TotalCutsSecondTrim[1];
                oItemSection.GuillotineQty3SecondTrimCuts = TotalCutsSecondTrim[2];

                oItemSection.GuillotineQty1TotalsCuts = TotalCutsFirstTrim[0] + TotalCutsSecondTrim[0];
                oItemSection.GuillotineQty2TotalsCuts = TotalCutsFirstTrim[1] + TotalCutsSecondTrim[1];
                oItemSection.GuillotineQty3TotalsCuts = TotalCutsFirstTrim[2] + TotalCutsSecondTrim[2];

                double[] dblGuillTotalCharge = new double[5];
                double ProfitMargin = 0;
                var markup = itemsectionRepository.GetMarkupById(oGuillotinCostCentreDTO.DefaultVAId);
                if (markup != null)
                    ProfitMargin = (double)markup.MarkUpRate;

                if ((oItemSection.IsFirstTrim != false || oItemSection.IsSecondTrim != false))
                {
                    SectionCostcentre oItemSectionCostCenter;
                    if (IsReRun == false)
                    {
                        oItemSectionCostCenter = new SectionCostcentre();
                        oItemSectionCostCenter.ItemSectionId = oItemSection.ItemSectionId;
                        oItemSectionCostCenter.CostCentreId = oGuillotinCostCentreDTO.CostCentreId;
                        oItemSectionCostCenter.SystemCostCentreType = (int)SystemCostCenterTypes.Guillotine;
                        oItemSectionCostCenter.Order = 108;
                        oItemSectionCostCenter.IsOptionalExtra = 0;
                        oItemSectionCostCenter.CostCentreType = 1;
                        oItemSectionCostCenter.IsDirectCost = Convert.ToInt16(oModelGuillotine.DirectCost);
                        oItemSectionCostCenter.SectionCostCentreDetails = new List<SectionCostCentreDetail>();

                    }
                    else
                    {
                        oItemSectionCostCenter = oItemSection.SectionCostcentres.FirstOrDefault();
                    }

                    oItemSectionCostCenter.SetupTime = Convert.ToDouble((oModelGuillotine.SetupTime == 0 ? 0 : oModelGuillotine.SetupTime / 60));
                    oItemSectionCostCenter.IsPrintable = Convert.ToInt16(oJobCardOptionsDTO.IsDefaultGuilotine);

                    if (oItemSection.Qty1 > 0)
                    {
                        //price
                        if (dblFirstTrimPrice[0] + dblSecondTrimPrice[0] + oModelGuillotine.SetupCharge > dblGuillMinCharge)
                        {
                            oItemSectionCostCenter.Qty1Charge = dblFirstTrimPrice[0] + dblSecondTrimPrice[0] + oModelGuillotine.SetupCharge;
                            sMinimumCost = "0";
                        }
                        else
                        {
                            oItemSectionCostCenter.Qty1Charge = dblGuillMinCharge;
                            sMinimumCost = "1";
                        }

                        //cost
                        if (dblFirstTrimCost[0] + dblSecondTrimCost[0] + oModelGuillotine.SetupCharge > dblGuillMinCharge)
                        {
                            oItemSectionCostCenter.Qty1EstimatedPlantCost = dblFirstTrimCost[0] + dblSecondTrimCost[0] + oModelGuillotine.SetupCharge;
                        }
                        else
                        {
                            oItemSectionCostCenter.Qty1EstimatedPlantCost = dblGuillMinCharge;
                        }


                        //we are getting time in seconds
                        oItemSectionCostCenter.Qty1EstimatedTime = Math.Round(Convert.ToDouble((TotalCutsFirstTrim[0] + TotalCutsSecondTrim[0] * oModelGuillotine.TimePerCut / 60 + oItemSectionCostCenter.SetupTime) / 60), 2);
                        oItemSectionCostCenter.Qty1MarkUpID = oGuillotinCostCentreDTO.DefaultVAId;
                        oItemSectionCostCenter.Qty1MarkUpValue = oItemSectionCostCenter.Qty1Charge * ProfitMargin / 100;
                        oItemSectionCostCenter.Qty1NetTotal = oItemSectionCostCenter.Qty1Charge + oItemSectionCostCenter.Qty1MarkUpValue;

                        if (IsWorkInstructionsLocked == false && oJobCardOptionsDTO.IsDefaultGuilotine == true)
                        {
                            if (oJobCardOptionsDTO.IsGuilotineWorkingSize == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions = "Working Size:=" + oItemSection.SectionSizeHeight + "mm x " + oItemSection.SectionSizeWidth + "mm " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsGuilotineItemSize == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += "Item Size (Flat):= " + oItemSection.ItemSizeHeight + "mm x " + oItemSection.ItemSizeWidth + "mm " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsNoOfTrims == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += "First Trim:=" + Convert.ToString((oItemSection.IsFirstTrim == false ? "No: " : "Yes: ")) + intFirstTrimCut + " cuts  Second Trim:= " + Convert.ToString((oItemSection.IsSecondTrim == false ? "No :" : "Yes : ")) + intSecondTrimCut + " cuts " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsNoOfCuts == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += "Total Number of Cuts:= " + TotalCutsFirstTrim[0].ToString() + TotalCutsSecondTrim[0].ToString() + Environment.NewLine;
                            }

                            if (oJobCardOptionsDTO.IsGuillotineEstTime == true)
                            {
                                // oItemSectionCostCenter.Qty1WorkInstructions += "Estimated Time := " + Math.Round(((double)oItemSectionCostCenter.Qty1EstimatedTime), 2).ToString() + " hours";
                            }
                        }
                    }

                    if (oItemSection.Qty2 > 0)
                    {
                        if (dblFirstTrimPrice[1] + dblSecondTrimPrice[1] + oModelGuillotine.SetupCharge > dblGuillMinCharge)
                        {
                            oItemSectionCostCenter.Qty2Charge = dblFirstTrimPrice[1] + dblSecondTrimPrice[1] + oModelGuillotine.SetupCharge;
                            sMinimumCost += "0";
                        }
                        else
                        {
                            oItemSectionCostCenter.Qty2Charge = dblGuillMinCharge;
                            sMinimumCost += "1";
                        }

                        if (dblFirstTrimCost[1] + dblSecondTrimCost[1] + oModelGuillotine.SetupCharge > dblGuillMinCharge)
                        {
                            oItemSectionCostCenter.Qty2EstimatedPlantCost = dblFirstTrimCost[1] + dblSecondTrimCost[1] + oModelGuillotine.SetupCharge;
                        }
                        else
                        {
                            oItemSectionCostCenter.Qty2EstimatedPlantCost = dblGuillMinCharge;
                        }

                        oItemSectionCostCenter.Qty2EstimatedTime = Math.Round(Convert.ToDouble((TotalCutsFirstTrim[1] + TotalCutsSecondTrim[1] * oModelGuillotine.TimePerCut / 60 + oItemSectionCostCenter.SetupTime) / 60), 2);

                        //IIf(dblFirstTrim(1) + dblSecondTrim(1) + oModelGuillotine.SetUpCharge > dblGuillMinCharge, oItemSectionCostCenter.Qty2Charge = dblFirstTrim(1) + dblSecondTrim(1) + oModelGuillotine.SetUpCharge, oItemSectionCostCenter.Qty2Charge = dblGuillMinCharge)
                        oItemSectionCostCenter.Qty2MarkUpID = oGuillotinCostCentreDTO.DefaultVAId;
                        oItemSectionCostCenter.Qty2MarkUpValue = oItemSectionCostCenter.Qty2Charge * ProfitMargin / 100;
                        oItemSectionCostCenter.Qty2NetTotal = oItemSectionCostCenter.Qty2Charge + oItemSectionCostCenter.Qty2MarkUpValue;

                        if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsDefaultGuilotine == true)
                        {
                            if (oJobCardOptionsDTO.IsGuilotineWorkingSize == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions = "Working Size:=" + oItemSection.SectionSizeHeight + "mm x " + oItemSection.SectionSizeWidth + "mm " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsGuilotineItemSize == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += "Item Size (Flat):= " + oItemSection.ItemSizeHeight + "mm x " + oItemSection.ItemSizeWidth + "mm " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsNoOfTrims == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += "First Trim:=" + Convert.ToString((oItemSection.IsFirstTrim == false ? "No: " : "Yes: ")) + intFirstTrimCut + " cuts  Second Trim:= " + Convert.ToString((oItemSection.IsSecondTrim == false ? "No :" : "Yes : ")) + intSecondTrimCut + " cuts " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsNoOfCuts == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += "Total Number of Cuts:= " + TotalCutsFirstTrim[1].ToString() + TotalCutsSecondTrim[2].ToString() + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsGuillotineEstTime == true)
                            {
                                //oItemSectionCostCenter.Qty2WorkInstructions += "Estimated Time := " + Math.Round(oItemSectionCostCenter.Qty2EstimatedTime, 2).ToString() + " hours";
                            }
                        }
                    }

                    if (oItemSection.Qty3 > 0)
                    {
                        //IIf(dblFirstTrim(2) + dblSecondTrim(2) + oModelGuillotine.SetUpCharge > dblGuillMinCharge, oItemSectionCostCenter.Qty3Charge = dblFirstTrim(2) + dblSecondTrim(2) + oModelGuillotine.SetUpCharge, oItemSectionCostCenter.Qty3Charge = dblGuillMinCharge)
                        if (dblFirstTrimPrice[2] + dblSecondTrimPrice[2] + oModelGuillotine.SetupCharge > dblGuillMinCharge)
                        {
                            oItemSectionCostCenter.Qty3Charge = dblFirstTrimPrice[2] + dblSecondTrimPrice[2] + oModelGuillotine.SetupCharge;
                            sMinimumCost += "0";
                        }
                        else
                        {
                            oItemSectionCostCenter.Qty3Charge = dblGuillMinCharge;
                            sMinimumCost += "1";
                        }

                        if (dblFirstTrimCost[2] + dblSecondTrimCost[2] + oModelGuillotine.SetupCharge > dblGuillMinCharge)
                        {
                            oItemSectionCostCenter.Qty3EstimatedPlantCost = dblFirstTrimCost[2] + dblSecondTrimCost[2] + oModelGuillotine.SetupCharge;
                        }
                        else
                        {
                            oItemSectionCostCenter.Qty3EstimatedPlantCost = dblGuillMinCharge;
                        }

                        oItemSectionCostCenter.Qty3EstimatedTime = Math.Round(Convert.ToDouble(((TotalCutsFirstTrim[2] + TotalCutsSecondTrim[2]) * Convert.ToInt32(oModelGuillotine.TimePerCut) / 60 + oItemSectionCostCenter.SetupTime) / 60), 2);

                        oItemSectionCostCenter.Qty3MarkUpID = oGuillotinCostCentreDTO.DefaultVAId;
                        oItemSectionCostCenter.Qty3MarkUpValue = oItemSectionCostCenter.Qty3Charge * ProfitMargin / 100;
                        oItemSectionCostCenter.Qty3NetTotal = oItemSectionCostCenter.Qty3Charge + oItemSectionCostCenter.Qty3MarkUpValue;

                        if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsDefaultGuilotine == true)
                        {
                            if (oJobCardOptionsDTO.IsGuilotineWorkingSize == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions = "Working Size:=" + oItemSection.SectionSizeHeight + "mm x " + oItemSection.SectionSizeWidth + "mm " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsGuilotineItemSize == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += "Item Size (Flat):= " + oItemSection.ItemSizeHeight + "mm x " + oItemSection.ItemSizeWidth + "mm " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsNoOfTrims == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += "First Trim:=" + Convert.ToString((oItemSection.IsFirstTrim == false ? "No: " : "Yes: ")) + intFirstTrimCut + " cuts  Second Trim:= " + Convert.ToString((oItemSection.IsSecondTrim == false ? "No :" : "Yes : ")) + intSecondTrimCut + " cuts " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsNoOfCuts == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += "Total Number of Cuts:= " + TotalCutsFirstTrim[2].ToString() + TotalCutsSecondTrim[2].ToString() + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsGuillotineEstTime == true)
                            {
                                // oItemSectionCostCenter.Qty3WorkInstructions += "Estimated Time := " + Math.Round(oItemSectionCostCenter.Qty3EstimatedTime, 2).ToString() + " hours";
                            }
                        }

                    }

                    if (sMinimumCost != string.Empty)
                    {
                        oItemSectionCostCenter.IsMinimumCost = Convert.ToInt16(sMinimumCost);
                    }
                    else
                    {
                        oItemSectionCostCenter.IsMinimumCost = 0;
                    }

                    // Model.Items.ItemSectionCostCentreDetailDTO oItemSectionCostCenterDetail = default(Model.Items.ItemSectionCostCentreDetailDTO);
                    SectionCostCentreDetail oItemSectionCostCenterDetail;
                    if (IsReRun == false)
                    {
                        oItemSectionCostCenterDetail = new SectionCostCentreDetail();
                    }
                    else
                    {
                        oItemSectionCostCenterDetail = oItemSectionCostCenter.SectionCostCentreDetails.FirstOrDefault();
                    }
                    oItemSectionCostCenterDetail.Qty1 = 1;
                    oItemSectionCostCenterDetail.Qty2 = 1;
                    oItemSectionCostCenterDetail.Qty3 = 1;

                    oItemSectionCostCenter.Name = "Guillotine ( " + oModelGuillotine.MachineName + " )";
                    oItemSectionCostCenterDetail.CostPrice = oItemSectionCostCenter.Qty1Charge;

                    if (IsReRun == false)
                    {
                        oItemSectionCostCenter.SectionCostCentreDetails.Add(oItemSectionCostCenterDetail);
                        oItemSection.SectionCostcentres.Add(oItemSectionCostCenter);
                    }
                }
                else
                {
                    SectionCostcentre oItemSectionCostCenter;
                    if (IsReRun == false)
                    {
                        oItemSectionCostCenter = new SectionCostcentre();
                        oItemSectionCostCenter.ItemSectionId = oItemSection.ItemSectionId;
                        oItemSectionCostCenter.CostCentreId = oGuillotinCostCentreDTO.CostCentreId;
                        oItemSectionCostCenter.SystemCostCentreType = (int)SystemCostCenterTypes.Guillotine;
                        oItemSectionCostCenter.Order = 108;
                        oItemSectionCostCenter.IsOptionalExtra = Convert.ToInt16(oModelGuillotine.IsAdditionalOption);
                        oItemSectionCostCenter.CostCentreType = 1;
                        oItemSectionCostCenter.IsDirectCost = Convert.ToInt16(oModelGuillotine.DirectCost);
                        oItemSectionCostCenter.SectionCostCentreDetails = new List<SectionCostCentreDetail>();

                    }
                    else
                    {
                        oItemSectionCostCenter = oItemSection.SectionCostcentres.FirstOrDefault();
                    }

                    oItemSectionCostCenter.Qty1Charge = 0;
                    oItemSectionCostCenter.Qty1MarkUpID = oGuillotinCostCentreDTO.DefaultVAId;
                    oItemSectionCostCenter.Qty1MarkUpValue = oItemSectionCostCenter.Qty1Charge * ProfitMargin / 100;
                    oItemSectionCostCenter.Qty1NetTotal = oItemSectionCostCenter.Qty1Charge + oItemSectionCostCenter.Qty1MarkUpValue;
                    oItemSectionCostCenter.Qty2Charge = 0;

                    oItemSectionCostCenter.Qty2MarkUpID = oGuillotinCostCentreDTO.DefaultVAId;
                    oItemSectionCostCenter.Qty2MarkUpValue = oItemSectionCostCenter.Qty2Charge * ProfitMargin / 100;
                    oItemSectionCostCenter.Qty2NetTotal = oItemSectionCostCenter.Qty2Charge + oItemSectionCostCenter.Qty2MarkUpValue;

                    oItemSectionCostCenter.Qty3Charge = 0;
                    oItemSectionCostCenter.Qty3MarkUpID = oGuillotinCostCentreDTO.DefaultVAId;
                    oItemSectionCostCenter.Qty3MarkUpValue = oItemSectionCostCenter.Qty3Charge * ProfitMargin / 100;
                    oItemSectionCostCenter.Qty3NetTotal = oItemSectionCostCenter.Qty3Charge + oItemSectionCostCenter.Qty3MarkUpValue;
                    oItemSectionCostCenter.Qty1WorkInstructions = " First Trim:= No , Second Trim:= No";
                    oItemSectionCostCenter.Qty2WorkInstructions = " First Trim:= No , Second Trim:= No";
                    oItemSectionCostCenter.Qty3WorkInstructions = " First Trim:= No , Second Trim:= No";

                    SectionCostCentreDetail oItemSectionCostCenterDetail;
                    if (IsReRun == false)
                    {
                        oItemSectionCostCenterDetail = new SectionCostCentreDetail();
                    }
                    else
                    {
                        oItemSectionCostCenterDetail = oItemSectionCostCenter.SectionCostCentreDetails.FirstOrDefault();
                    }

                    oItemSectionCostCenter.Name = "Guillotine ( " + oModelGuillotine.MachineName + " )";
                    oItemSectionCostCenter.Qty1 = oItemSection.Qty1;
                    oItemSectionCostCenter.Qty2 = oItemSection.Qty2;
                    oItemSectionCostCenter.Qty3 = oItemSection.Qty3;

                    oItemSectionCostCenterDetail.Qty1 = 0;
                    oItemSectionCostCenterDetail.Qty2 = 0;
                    oItemSectionCostCenterDetail.Qty3 = 0;
                    oItemSectionCostCenterDetail.CostPrice = 0;

                    if (IsReRun == false)
                    {
                        oItemSectionCostCenter.SectionCostCentreDetails.Add(oItemSectionCostCenterDetail);
                        oItemSection.SectionCostcentres.Add(oItemSectionCostCenter);
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception("CalculateGuillotineCost", ex);
            }
            return oItemSection;
        }


        public double ConvertLength(double Input, lengthunit InputUnit, lengthunit OutputUnit)
        {
            double ConversionUnit = 0;
            //Getting LengthUnit Query
            MPC.Models.DomainModels.LengthUnit oRows = itemsectionRepository.GetLengthUnitByInput((int)InputUnit);
            if (oRows != null)
            {
                switch (OutputUnit)
                {
                    case lengthunit.Cm:
                        ConversionUnit = (double)oRows.CM;
                        break;
                    case lengthunit.Inch:
                        ConversionUnit = (double)oRows.Inch;
                        break;
                    case lengthunit.Mm:
                        ConversionUnit = (double)oRows.MM;
                        break;
                }
            }

            return Input * ConversionUnit;
        }

        public double ConvertWeight(double Input, WeightUnits InputUnit, WeightUnits OutputUnit)
        {
            double ConversionUnit = 0;
            WeightUnit oRows = itemsectionRepository.GetWeightUnitByInput((int)InputUnit);
            if (oRows != null)
            {
                switch (OutputUnit)
                {
                    case WeightUnits.GSM:
                        ConversionUnit = Convert.ToDouble(oRows.GSM);
                        break;
                    case WeightUnits.KG:
                        ConversionUnit = Convert.ToDouble(oRows.KG);
                        break;
                    case WeightUnits.lbs:
                        ConversionUnit = Convert.ToDouble(oRows.Pound);
                        break;
                }
            }
            return Input * ConversionUnit;

        }

        public GlobalData GetItemPriceCost(int StockItemID)
        {
            GlobalData gData = new GlobalData();

            double Cost = 0;
            double Price = 0;
            double PackCost = 0;
            double PackPrice = 0;
            int ReturnColumnIndex = 0;
            DateTime currentDate = DateTime.Now;
            //Getting Stock Cost n Price Query
            List<StockCostAndPrice> InkCostTable = itemsectionRepository.GetStockPricingByStockId(StockItemID, false);
            List<StockCostAndPrice> InkPriceTable = itemsectionRepository.GetStockPricingByStockId(StockItemID, true);
            ReturnColumnIndex = 2;

            //COST calculation
            //there is only one only 1 cost available use it.. anyway
            if (InkCostTable.Count == 1)
            {
                Cost = InkCostTable[0].CostPrice;

            }
            else if (InkCostTable.Count > 1)
            {
                //multiple costs available. now apply the date filter
                // DataRow[] oRows = InkCostTable.Select("FromDate <='" + System.DateTime.Today.ToShortDateString() + "' and ToDate >='" + System.DateTime.Today.ToShortDateString() + "'");
                var oRows = InkCostTable.Where(c => c.FromDate <= currentDate && c.ToDate >= currentDate).LastOrDefault();
                if (oRows != null)
                {
                    //we have some rows to get the cost, get the last row
                    Cost = oRows.CostPrice;
                }
                else
                {
                    //no rows to select, get the last row if available 
                    Cost = InkCostTable[InkCostTable.Count - 1].CostPrice;
                }
            }
            else if (InkCostTable.Count == 0)
            {
                Cost = 0;
            }

            //PRICE calculation
            //there is only one only 1 cost available use it.. anyway
            if (InkPriceTable.Count == 1)
            {
                Price = InkPriceTable[0].CostPrice;
            }
            else if (InkPriceTable.Count > 1)
            {
                //multiple costs available. now apply the date filter
                // DataRow[] oRows = InkPriceTable.Select("FromDate <='" + System.DateTime.Today.ToShortDateString() + "' and ToDate >='" + System.DateTime.Today.ToShortDateString() + "'");
                var oRows = InkPriceTable.Where(c => c.FromDate <= currentDate && c.ToDate >= currentDate).LastOrDefault();
                if (oRows != null)
                {
                    //we have some rows to get the cost, get the last row
                    Price = oRows.CostPrice;
                }
                else
                {
                    //no rows to select, get the last row if available 
                    Price = InkPriceTable[InkPriceTable.Count - 1].CostPrice;
                }
            }
            else if (InkPriceTable.Count == 0)
            {
                //Since no price is available, use the COST
                Price = Cost;
            }

            //PACK Information
            ReturnColumnIndex = 3;
            //COST calculation
            //there is only one only 1 cost available use it.. anyway
            if (InkCostTable.Count == 1)
            {
                PackCost = InkCostTable[0].PackCostPrice;

            }
            else if (InkCostTable.Count > 1)
            {
                //multiple costs available. now apply the date filter
                //DataRow[] oRows = InkCostTable.Select("FromDate <='" + System.DateTime.Today.ToShortDateString() + "' and ToDate >='" + System.DateTime.Today.ToShortDateString() + "'");
                var oRows = InkCostTable.Where(c => c.FromDate <= currentDate && c.ToDate >= currentDate).LastOrDefault();

                if (oRows != null)
                {
                    //we have some rows to get the cost, get the last row
                    PackCost = oRows.PackCostPrice;
                }
                else
                {
                    //no rows to select, get the last row if available 
                    PackCost = InkCostTable[InkCostTable.Count - 1].PackCostPrice;
                }
            }
            else if (InkCostTable.Count == 0)
            {
                //exeptional case
                PackCost = 0;
            }

            //PRICE calculation
            //there is only one only 1 cost available use it.. anyway
            if (InkPriceTable.Count == 1)
            {
                PackPrice = InkPriceTable[0].PackCostPrice;
            }
            else if (InkPriceTable.Count > 1)
            {
                //multiple costs available. now apply the date filter
                var oRows = InkPriceTable.Where(c => c.FromDate <= currentDate && c.ToDate >= currentDate).LastOrDefault();

                if (oRows != null)
                {
                    //we have some rows to get the cost, get the last row
                    PackPrice = oRows.PackCostPrice;
                }
                else
                {
                    //no rows to select, get the last row if available 
                    PackPrice = InkPriceTable[InkPriceTable.Count - 1].PackCostPrice;
                }
            }
            else if (InkPriceTable.Count == 0)
            {
                //Since no price is available, use the COST
                PackPrice = Cost;
            }

            gData.dblUnitCost = Cost;
            gData.dblUnitPrice = Price;
            gData.dblPackCost = PackCost;
            gData.dblPackPrice = PackPrice;

            return gData;
        }

        private GlobalData GetItemPriceCost(int StockItemID, bool ReturnUnitCost)
        {
            double Cost = 0;
            double Price = 0;

            double PackCost = 0;
            double PackPrice = 0;
            DateTime currentDate = DateTime.Now;
            GlobalData gData = new GlobalData();
            //INK is just a prefix, dont confuse it for only inks table, its general
            //Getting StockCostnPriceQuery
            List<StockCostAndPrice> InkCostTable = itemsectionRepository.GetStockPricingByStockId(StockItemID, false);
            List<StockCostAndPrice> InkPriceTable = itemsectionRepository.GetStockPricingByStockId(StockItemID, true);

            //COST calculation
            //there is only one only 1 cost available use it.. anyway
            if (InkCostTable.Count == 1)
            {
                if (ReturnUnitCost)
                    Cost = InkCostTable[0].CostPrice;
                else
                    PackCost = InkCostTable[0].PackCostPrice;
                // CostProcessingCharge = InkCostTable[0].ProcessingCharge;
                gData.dblFilmCostProcessCharge = InkCostTable[0].ProcessingCharge;
                gData.dblPlateProcessingtCost = InkCostTable[0].ProcessingCharge;
            }
            else if (InkCostTable.Count > 1)
            {
                //multiple costs available. now apply the date filter
                var oRows = InkCostTable.Where(c => c.FromDate <= currentDate && c.ToDate >= currentDate).FirstOrDefault();
                if (oRows != null)
                {
                    //we have some rows to get the cost, get the last row
                    if (ReturnUnitCost)
                        Cost = oRows.CostPrice;
                    else
                        PackCost = oRows.PackCostPrice;
                    //CostProcessingCharge = oRows.ProcessingCharge;
                    gData.dblFilmCostProcessCharge = oRows.ProcessingCharge;
                    gData.dblPlateProcessingtCost = oRows.ProcessingCharge;
                }
                else
                {
                    //no rows to select, get the last row if available 
                    if (ReturnUnitCost)
                        Cost = InkCostTable[InkCostTable.Count - 1].CostPrice;
                    else
                        PackCost = InkCostTable[InkCostTable.Count - 1].PackCostPrice;
                    gData.dblFilmCostProcessCharge = InkCostTable[InkCostTable.Count - 1].ProcessingCharge;
                    gData.dblPlateProcessingtCost = InkCostTable[InkCostTable.Count - 1].ProcessingCharge;
                }
            }
            else if (InkCostTable.Count == 0)
            {
                //exeptional case
                Cost = 0;
            }

            //PRICE calculation
            //there is only one only 1 cost available use it.. anyway
            if (InkPriceTable.Count == 1)
            {
                if (ReturnUnitCost)
                    Price = InkPriceTable[0].CostPrice;
                else
                    PackPrice = InkPriceTable[0].PackCostPrice;
                gData.dblFilmCostProcessCharge = InkPriceTable[0].ProcessingCharge;
                gData.dblPlateProcessingPrice = InkPriceTable[0].ProcessingCharge;
            }
            else if (InkPriceTable.Count > 1)
            {
                //multiple costs available. now apply the date filter
                var oRows = InkPriceTable.Where(c => c.FromDate <= currentDate && c.ToDate >= currentDate).FirstOrDefault();
                if (oRows != null)
                {
                    //we have some rows to get the cost, get the last row
                    if (ReturnUnitCost)
                        Price = oRows.CostPrice;
                    else
                        PackPrice = oRows.PackCostPrice;
                    gData.dblFilmCostProcessCharge = oRows.ProcessingCharge;
                    gData.dblPlateProcessingPrice = oRows.ProcessingCharge;
                }
                else
                {
                    if (ReturnUnitCost)
                        Price = InkPriceTable[InkPriceTable.Count - 1].CostPrice;
                    else
                        PackPrice = InkPriceTable[InkPriceTable.Count - 1].PackCostPrice;
                    gData.dblFilmCostProcessCharge = InkPriceTable[InkPriceTable.Count - 1].ProcessingCharge;
                    gData.dblPlateProcessingPrice = InkPriceTable[InkPriceTable.Count - 1].ProcessingCharge;
                }
            }
            else if (InkPriceTable.Count == 0)
            {
                //Since no price is available, use the COST
                Price = Cost;
                PackPrice = PackCost;
                //PriceProcessingCharge = CostProcessingCharge;
                gData.dblFilmPriceProcessCharge = gData.dblFilmCostProcessCharge;
                gData.dblPlateProcessingPrice = gData.dblPlateProcessingtCost;
            }

            gData.dblUnitCost = Math.Round(Cost, 3);
            gData.dblUnitPrice = Math.Round(Price, 3);

            gData.dblPackCost = PackCost;
            gData.dblPackPrice = PackPrice;

            //ReturnCostValue = Cost;
            //ReturnPriceValue = Price;

            return gData;
        }

        #endregion

        #region PTV
        /// <summary>
        /// Calculate PTV called from Controller
        /// </summary>
        /// <param name="ReversePTVRows"></param>
        /// <param name="ReversePTVCols"></param>
        /// <param name="IsDoubleSided"></param>
        /// <param name="ApplySwing"></param>
        /// <param name="ApplyPressRestrict"></param>
        /// <param name="ItemHeight"></param>
        /// <param name="ItemWidth"></param>
        /// <param name="PrintHeight"></param>
        /// <param name="PrintWidth"></param>
        /// <param name="ColorBar"></param>
        /// <param name="Grip"></param>
        /// <param name="GripDepth"></param>
        /// <param name="HeadDepth"></param>
        /// <param name="PrintGutter"></param>
        /// <param name="ItemHorizontalGutter"></param>
        /// <param name="ItemVerticalGutter"></param>
        /// <param name="IsWorknTurn"></param>
        /// <param name="IsWorknTumble"></param>
        /// <returns></returns>
        public PtvDTO CalculatePTV(int ReversePTVRows, int ReversePTVCols, bool IsDoubleSided, bool ApplySwing, bool ApplyPressRestrict, double ItemHeight, double ItemWidth, double PrintHeight, double PrintWidth, int ColorBar,
                                   int Grip, double GripDepth, double HeadDepth, double PrintGutter, double ItemHorizontalGutter, double ItemVerticalGutter, bool IsWorknTurn, bool IsWorknTumble)
        {

            double vPrintAreaHeight = 0;
            double vPrintAreaWidth = 0;
            byte mCurrOrient = 0;
            int LandScapeRows = 0;
            int LandScapeCols = 0;
            int PortraitRows = 0;
            int PortraitCols = 0;
            int LandScapeSwing = 0;
            int PortraitSwing = 0;
            long LandScapeColSwing = 0;
            long PortraitColSwing = 0;
            long LandScapeRowSwing = 0;
            long PortraitRowSwing = 0;
            long LandScapeRowsRemaining = 0;
            long LandScapeColsRemaining = 0;
            long PortraitRowsRemaining = 0;
            long PortraitColsRemaining = 0;
            int LandscapePTV = 0;
            int PortraitPTV = 0;
            double LandScapeItemHeight = 0;
            double PortraitItemHeight = 0;
            double LandScapeItemWidth = 0;
            double PortraitItemWidth = 0;

            //Grip = UCase(Left(Grip, 1))
            vPrintAreaHeight = PrintHeight;
            vPrintAreaWidth = PrintWidth;

            LandScapeItemHeight = GetItemHeight(ItemHeight, ItemWidth, PrintViewOrientation.Landscape); //ItemHeight; //
            LandScapeItemWidth = GetItemWidth(ItemHeight, ItemWidth, PrintViewOrientation.Landscape); //ItemWidth;//
            //Getting area excluding Gutters and Press Restrictions and Color Bar

            SetPrintView(ref vPrintAreaHeight, ref vPrintAreaWidth, LandScapeItemHeight, LandScapeItemWidth, ApplyPressRestrict, GripSide.LongSide, Convert.ToByte(PrintViewOrientation.Landscape), ColorBar, PrintHeight, PrintWidth,
            GripDepth, HeadDepth, PrintGutter, IsWorknTurn, IsWorknTumble);

            if (ReversePTVCols > 0 && ReversePTVRows > 0)
            {
                LandScapeRows = ReversePTVRows;
                //Setting the Custom Landscape Rows if provided by user
                LandScapeCols = ReversePTVCols;
                //Setting the Custom Landscape Cols if provided by user
            }
            else
            {
                CalcRowsToFit(vPrintAreaHeight, ref LandScapeRows, ref LandScapeRowsRemaining, LandScapeItemHeight, ItemVerticalGutter, IsWorknTurn, IsWorknTumble);
                CalcColsToFit(vPrintAreaWidth, ref LandScapeCols, ref LandScapeColsRemaining, LandScapeItemWidth, ItemHorizontalGutter, IsWorknTurn, IsWorknTumble);
            }

            LandscapePTV = (LandScapeRows * LandScapeCols) + Convert.ToInt32((ApplySwing ? LandScapeSwing : 0));

            //'Check for Portrait view
            vPrintAreaHeight = PrintHeight;
            vPrintAreaWidth = PrintWidth;
            PortraitItemHeight = GetItemHeight(ItemHeight, ItemWidth, PrintViewOrientation.Portrait); //ItemHeight; //
            PortraitItemWidth = GetItemWidth(ItemHeight, ItemWidth, PrintViewOrientation.Portrait); //ItemWidth;//

            //This is the Check wheter the user has sent custom Rows and Columns
            if (ReversePTVCols > 0 && ReversePTVRows > 0)
            {
                PortraitRows = ReversePTVRows;
                PortraitCols = ReversePTVCols;
            }
            else
            {
                SetPrintView(ref vPrintAreaHeight, ref vPrintAreaWidth, PortraitItemHeight, PortraitItemWidth, ApplyPressRestrict, GripSide.ShortSide, Convert.ToByte(PrintViewOrientation.Portrait), ColorBar, PrintHeight, PrintWidth,
                GripDepth, HeadDepth, PrintGutter, IsWorknTurn, IsWorknTumble);
                CalcRowsToFit(vPrintAreaHeight, ref PortraitRows, ref PortraitRowsRemaining, PortraitItemHeight, ItemVerticalGutter, IsWorknTurn, IsWorknTumble);
                CalcColsToFit(vPrintAreaWidth, ref PortraitCols, ref PortraitColsRemaining, PortraitItemWidth, ItemHorizontalGutter, IsWorknTurn, IsWorknTumble);
            }

            PortraitPTV = (PortraitRows * PortraitCols) + Convert.ToInt32((ApplySwing ? PortraitSwing : 0));
            PtvDTO oPTVResults = new PtvDTO { LandScapeRows = LandScapeRows, LandScapeCols = LandScapeCols, PortraitRows = PortraitRows, PortraitCols = PortraitCols, LandScapeSwing = LandScapeSwing, PortraitSwing = PortraitSwing, LandscapePTV = LandscapePTV, PortraitPTV = PortraitPTV };
            return oPTVResults;
        }

        /// <summary>
        /// Draw PTV called internally as well as from controller
        /// </summary>
        /// <param name="strOrient"></param>
        /// <param name="ReversePTVRows"></param>
        /// <param name="ReversePTVCols"></param>
        /// <param name="IsDoubleSided"></param>
        /// <param name="IsWorknTurn"></param>
        /// <param name="IsWorknTumble"></param>
        /// <param name="ApplyPressRestrict"></param>
        /// <param name="ItemHeight"></param>
        /// <param name="ItemWidth"></param>
        /// <param name="PrintHeight"></param>
        /// <param name="PrintWidth"></param>
        /// <param name="Grip"></param>
        /// <param name="GripDepth"></param>
        /// <param name="HeadDepth"></param>
        /// <param name="PrintGutter"></param>
        /// <param name="ItemHorizontalGutter"></param>
        /// <param name="ItemVerticalGutter"></param>
        /// <returns></returns>
        public PtvDTO DrawPTV(PrintViewOrientation strOrient, int ReversePTVRows, int ReversePTVCols, bool IsDoubleSided, bool IsWorknTurn, bool IsWorknTumble, bool ApplyPressRestrict, double ItemHeight, double ItemWidth, double PrintHeight, double PrintWidth, GripSide Grip, double GripDepth, double HeadDepth, double PrintGutter, double ItemHorizontalGutter, double ItemVerticalGutter)
        {
            Image imgCardL = Image.FromFile(HttpContext.Current.Server.MapPath("../Content/Images/ptv/front-up.gif"));
            Image imgCardBackL = Image.FromFile(HttpContext.Current.Server.MapPath("../Content/Images/ptv/back-up.gif"));
            Image imgCardDownL = Image.FromFile(HttpContext.Current.Server.MapPath("../Content/Images/ptv/front-down.gif"));
            Image imgCardBackDownL = Image.FromFile(HttpContext.Current.Server.MapPath("../Content/Images/ptv/back-down.gif"));
            Image imgCardP = Image.FromFile(HttpContext.Current.Server.MapPath("../Content/Images/ptv/front-upP.gif"));
            Image imgCardBackP = Image.FromFile(HttpContext.Current.Server.MapPath("../Content/Images/ptv/back-upP.gif"));
            Image imgCardDownP = Image.FromFile(HttpContext.Current.Server.MapPath("../Content/Images/ptv/front-downP.gif"));
            Image imgCardBackDownP = Image.FromFile(HttpContext.Current.Server.MapPath("../Content/Images/ptv/back-downP.gif"));
            Image imgCardBackW = Image.FromFile(HttpContext.Current.Server.MapPath("../Content/Images/ptv/back-upW.gif"));
            Image DottedImage = Image.FromFile(HttpContext.Current.Server.MapPath("../Content/Images/ptv/dotTexture.gif"));



            Bitmap bm = default(Bitmap);
            Bitmap bm2 = default(Bitmap);
            Graphics gs = default(Graphics);
            Graphics gs2 = default(Graphics);

            try
            {




                Pen oPen = new Pen(Color.Blue, 1);
                Image imgApply = default(Image);
                Image imgApplyBack = default(Image);
                SolidBrush oBrush = new SolidBrush(ColorTranslator.FromHtml("#92bbe2"));
                TextureBrush oDottedBrush = new TextureBrush(DottedImage);
                double vPageWidth = 0;
                double vPageHeight = 0;
                double vIWidth = 0;
                double vIHeight = 0;
                double dblSpace = 0;
                int x1 = 0;
                int x2 = 0;
                int x3 = 0;
                int y1 = 0;
                int y2 = 0;
                int y3 = 0;
                int i = 0;
                int j = 0;
                int n = 0;
                byte nFactor = 3;
                //'Additional gap for Page View
                int vOddSide = 0;
                //Dim blnWork As Boolean
                int intSymmetry = 0;

                int myRows = 0;
                int myCols = 0;
                int[, ,] mItemPosition = new int[2, 2, 4];
                //'Rows, Cols, (x0,y0, itemX, itemY)
                string myFlip = null;
                bool bColMode = false;
                bool bRowMode = false;
                bool IsBeforeHalfSideHorizontal = true;
                bool IsBeforeHalfSideVertical = true;

                int xFactor = 0;
                int yFactor = 0;
                int xSwing = 0;
                int ySwing = 0;
                int vRowCount = 0;
                int vColumnCount = 0;
                int vRowSwing = 0;
                int vColSwing = 0;
                double vRightPad = 0;
                double vLeftPad = 0;
                double vBottomPad = 0;
                double vTopPad = 0;
                List<DividerLine> DividerLines = new List<DividerLine>();

                //strOrient = Left(strOrient, 1)


                PtvDTO oPTVResult = CalculatePTV(ReversePTVRows, ReversePTVCols, IsDoubleSided, false, ApplyPressRestrict, ItemHeight, ItemWidth, PrintHeight, PrintWidth, 0,
                (int)Grip, GripDepth, HeadDepth, PrintGutter, ItemHorizontalGutter, ItemVerticalGutter, IsWorknTurn, IsWorknTumble);


                vIWidth = GetItemWidth(ItemHeight, ItemWidth, strOrient);
                vIHeight = GetItemHeight(ItemHeight, ItemWidth, strOrient);
                vPageWidth = PrintWidth;
                vPageHeight = PrintHeight;

                bm = new Bitmap(Convert.ToInt32(vPageWidth + nFactor), Convert.ToInt32(vPageHeight + nFactor));
                bm2 = new Bitmap(Convert.ToInt32(vPageWidth + nFactor), Convert.ToInt32(vPageHeight + nFactor));
                gs = Graphics.FromImage(bm);
                gs2 = Graphics.FromImage(bm2);
                gs.TextRenderingHint = TextRenderingHint.AntiAlias;
                gs2.TextRenderingHint = TextRenderingHint.AntiAlias;
                //'applying Background
                gs.Clear(Color.WhiteSmoke);
                gs2.Clear(Color.WhiteSmoke);
                gs.DrawRectangle(new Pen(Color.Black, 1), 1, 1, Convert.ToInt32(vPageWidth), Convert.ToInt32(vPageHeight));
                gs2.DrawRectangle(new Pen(Color.Black, 1), 1, 1, Convert.ToInt32(vPageWidth), Convert.ToInt32(vPageHeight));


                ///''''''''''''''''''''''''
                /// Drawing Gutter
                ///''''''''''''''''''''''''
                if (PrintGutter > 0)
                {
                    PrintGutter = 5;
                    if (Grip == GripSide.LongSide & PrintHeight <= PrintWidth)
                    {
                        gs.FillRectangle(new SolidBrush(Color.SeaGreen), 0, 0, Convert.ToInt32(PrintGutter), Convert.ToInt32(vPageHeight));
                        gs2.FillRectangle(new SolidBrush(Color.SeaGreen), 0, 0, Convert.ToInt32(PrintGutter), Convert.ToInt32(vPageHeight));
                        gs.FillRectangle(new SolidBrush(Color.SeaGreen), Convert.ToInt32(vPageWidth - PrintGutter), 0, Convert.ToInt32(PrintGutter), Convert.ToInt32(vPageHeight));
                        //'drawing grip on right side
                        gs2.FillRectangle(new SolidBrush(Color.SeaGreen), Convert.ToInt32(vPageWidth - PrintGutter), 0, Convert.ToInt32(PrintGutter), Convert.ToInt32(vPageHeight));
                        //'drawing grip on right side
                        vPageWidth = vPageWidth - (PrintGutter * 2);
                    }
                    else
                    {
                        gs.FillRectangle(new SolidBrush(Color.SeaGreen), 0, 0, Convert.ToInt32(vPageWidth), Convert.ToInt32(PrintGutter));
                        gs2.FillRectangle(new SolidBrush(Color.SeaGreen), 0, 0, Convert.ToInt32(vPageWidth), Convert.ToInt32(PrintGutter));
                        gs.FillRectangle(new SolidBrush(Color.SeaGreen), 0, Convert.ToInt32(vPageHeight - PrintGutter), Convert.ToInt32(vPageWidth), Convert.ToInt32(PrintGutter));
                        //'drawing grip at bottom side
                        gs2.FillRectangle(new SolidBrush(Color.SeaGreen), 0, Convert.ToInt32(vPageHeight - PrintGutter), Convert.ToInt32(vPageWidth), Convert.ToInt32(PrintGutter));
                        //'drawing grip at bottom side
                        vPageHeight = vPageHeight - (PrintGutter * 2);
                    }
                }
                ///''''''''''''''''''''''''

                ///''''''''''''''''''''''''
                /// Drawing Head
                ///''''''''''''''''''''''''
                if (HeadDepth > 0)
                {
                    HeadDepth = 5;
                    if (Grip == GripSide.LongSide & PrintHeight <= PrintWidth)
                    {
                        gs.FillRectangle(new SolidBrush(Color.Black), Convert.ToInt32(PrintGutter), 0, Convert.ToInt32(vPageWidth), Convert.ToInt32(HeadDepth));
                        gs2.FillRectangle(new SolidBrush(Color.Black), Convert.ToInt32(PrintGutter), 0, Convert.ToInt32(vPageWidth), Convert.ToInt32(HeadDepth));
                        vPageHeight = vPageHeight - HeadDepth;
                    }
                    else
                    {
                        gs.FillRectangle(new SolidBrush(Color.Black), Convert.ToInt32(vPageWidth - HeadDepth), Convert.ToInt32(PrintGutter), Convert.ToInt32(HeadDepth), Convert.ToInt32(vPageHeight));
                        gs2.FillRectangle(new SolidBrush(Color.Black), Convert.ToInt32(vPageWidth - HeadDepth), Convert.ToInt32(PrintGutter), Convert.ToInt32(HeadDepth), Convert.ToInt32(vPageHeight));
                        vPageWidth = vPageWidth - HeadDepth;
                    }
                }
                ///''''''''''''''''''''''''

                ///''''''''''''''''''''''''
                /// Drawing Grip 
                ///''''''''''''''''''''''''
                if (GripDepth > 0)
                {
                    GripDepth = 5;
                    if (Grip == GripSide.LongSide & PrintHeight <= PrintWidth)
                    {
                        gs.FillRectangle(new SolidBrush(Color.DarkSalmon), Convert.ToInt32(PrintGutter), Convert.ToInt32(HeadDepth + (vPageHeight - GripDepth)), Convert.ToInt32(vPageWidth), Convert.ToInt32(GripDepth));
                        gs2.FillRectangle(new SolidBrush(Color.DarkSalmon), Convert.ToInt32(PrintGutter), Convert.ToInt32(HeadDepth + (vPageHeight - GripDepth)), Convert.ToInt32(vPageWidth), Convert.ToInt32(GripDepth));
                        vPageHeight = vPageHeight - GripDepth;
                    }
                    else
                    {
                        gs.FillRectangle(new SolidBrush(Color.DarkSalmon), 0, Convert.ToInt32(PrintGutter), Convert.ToInt32(GripDepth), Convert.ToInt32(vPageHeight));
                        gs2.FillRectangle(new SolidBrush(Color.DarkSalmon), 0, Convert.ToInt32(PrintGutter), Convert.ToInt32(GripDepth), Convert.ToInt32(vPageHeight));
                        vPageWidth = vPageWidth - GripDepth;
                    }
                }


                ///''''''''''''''''''''''''''''''
                i = 0;
                j = 0;
                x1 = 0;
                x2 = 0;
                x3 = 0;
                y1 = 0;
                y2 = 0;
                y3 = 0;
                ///''''''''''''''''''''''''''''''
                ///Item drawing starts here...
                ///''''''''''''''''''''''''''''''



                if (strOrient == PrintViewOrientation.Portrait)
                {
                    vRowCount = oPTVResult.PortraitRows;
                    vColumnCount = oPTVResult.PortraitCols;
                }
                else
                {
                    vRowCount = oPTVResult.LandScapeRows;
                    vColumnCount = oPTVResult.LandScapeCols;
                }

                if (ItemHorizontalGutter > 0)
                {
                    if (ItemHorizontalGutter < ItemVerticalGutter)
                    {
                        vRightPad = (vIWidth * 1 / 50);
                    }
                    else
                    {
                        vRightPad = (vIWidth * 1 / 25);
                    }
                }
                if (ItemVerticalGutter > 0)
                {
                    if (ItemVerticalGutter < ItemHorizontalGutter)
                    {
                        vTopPad = (vIHeight * 1 / 50);
                    }
                    else
                    {
                        vTopPad = (vIHeight * 1 / 25);
                    }
                }

                vIWidth = Convert.ToInt32(vIWidth - vRightPad);
                vIHeight = Convert.ToInt32(vIHeight - vTopPad);

                x2 = Convert.ToInt32((vColumnCount) * (vIWidth + vRightPad));
                y2 = Convert.ToInt32((vRowCount + Convert.ToDouble((vRowSwing > 0 | vColSwing > 0 ? 0.5 : 0))) * (vIHeight + vTopPad));


                //'Start printing images
                for (i = 0; i <= vRowCount - 1; i++)
                {
                    if (i == 0)
                    {
                        yFactor = Convert.ToInt32((vPageHeight - y2) / 2);
                    }
                    else
                    {
                        yFactor = Convert.ToInt32(yFactor + vTopPad + vIHeight);
                    }

                    for (j = 0; j <= vColumnCount - 1; j++)
                    {
                        if (j == 0)
                        {
                            xFactor = Convert.ToInt32((vPageWidth - x2) / 2);
                        }
                        else
                        {
                            xFactor = Convert.ToInt32(xFactor + vRightPad + vIWidth);
                        }





                        if (IsWorknTumble == true)
                        {
                            if (vRowCount / 2 == i + 1)
                            {
                                DividerLines.Add(new DividerLine(0, Convert.ToInt32(vPageWidth), Convert.ToInt32(yFactor + vIHeight), Convert.ToInt32(yFactor + vIHeight)));
                            }


                            if (i < vRowCount / 2)
                            {
                                if (strOrient == PrintViewOrientation.Portrait)
                                {
                                    imgApply = imgCardP;
                                }
                                else
                                {
                                    imgApply = imgCardL;
                                }
                            }
                            else
                            {
                                if (strOrient == PrintViewOrientation.Portrait)
                                {
                                    imgApply = imgCardBackDownP;
                                }
                                else
                                {
                                    imgApply = imgCardBackDownL;
                                }
                            }


                            gs.DrawImage(imgApply, Convert.ToInt32(xFactor + PrintGutter), Convert.ToInt32(yFactor + HeadDepth), Convert.ToInt32(vIWidth - 1), Convert.ToInt32(vIHeight - 1));
                            //checks performed horizontal :)

                        }
                        else if (IsWorknTurn == true)
                        {
                            //draw the divider line :)
                            if (vColumnCount / 2 == j + 1)
                            {
                                DividerLines.Add(new DividerLine(Convert.ToInt32(xFactor + GripDepth + vIWidth), Convert.ToInt32(xFactor + GripDepth + vIWidth), 0, Convert.ToInt32(vPageHeight)));
                            }

                            if (j < vColumnCount / 2)
                            {
                                if (strOrient == PrintViewOrientation.Portrait)
                                {
                                    imgApply = imgCardP;
                                }
                                else
                                {
                                    imgApply = imgCardL;
                                }
                            }
                            else
                            {
                                if (strOrient == PrintViewOrientation.Portrait)
                                {
                                    imgApply = imgCardBackP;
                                }
                                else
                                {
                                    imgApply = imgCardBackL;
                                }
                            }


                            gs.DrawImage(imgApply, Convert.ToInt32(xFactor + PrintGutter), Convert.ToInt32(yFactor + HeadDepth), Convert.ToInt32(vIWidth - 1), Convert.ToInt32(vIHeight - 1));

                            //double sided case. where simple drawing is required..

                        }
                        else if (IsWorknTurn == false & IsWorknTumble == false)
                        {
                            if (strOrient == PrintViewOrientation.Portrait)
                            {
                                imgApply = imgCardP;
                                //'imgCard.RotateFlip(RotateFlipType.Rotate90FlipNone)
                                imgApplyBack = imgCardBackP;
                                //'imgCardBack.RotateFlip(RotateFlipType.Rotate90FlipNone)
                            }
                            else
                            {
                                imgApply = imgCardL;
                                imgApplyBack = imgCardBackL;
                            }

                            //Short Side                        
                            gs.DrawImage(imgApply, Convert.ToInt32(xFactor + PrintGutter), Convert.ToInt32(yFactor + HeadDepth), Convert.ToInt32(vIWidth - 1), Convert.ToInt32(vIHeight - 1));
                            gs2.DrawImage(imgApplyBack, Convert.ToInt32(xFactor + PrintGutter), Convert.ToInt32(yFactor + HeadDepth), Convert.ToInt32(vIWidth - 1), Convert.ToInt32(vIHeight - 1));

                        }

                    }


                }

                //drawing the divider lines from the divierlines list
                if (DividerLines.Count > 0)
                {
                    foreach (DividerLine oDivider in DividerLines)
                    {
                        gs.DrawLine(new Pen(oDottedBrush, 5), oDivider.X1, oDivider.Y1, oDivider.X2, oDivider.Y2);
                    }
                }

                gs.TextRenderingHint = TextRenderingHint.AntiAlias;
                gs.Flush();


                System.IO.MemoryStream stream = new System.IO.MemoryStream();
                bm.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

                System.IO.MemoryStream stream2 = new System.IO.MemoryStream();
                bm2.Save(stream2, System.Drawing.Imaging.ImageFormat.Png);


                oPTVResult.Side1Image = stream.ToArray();
                stream.Dispose();

                if (IsDoubleSided == true)
                {
                    if (IsWorknTurn == false & IsWorknTumble == false)
                    {
                        oPTVResult.Side2Image = stream2.ToArray();
                        stream2.Dispose();
                    }
                }

                return oPTVResult;
            }
            catch (Exception ex)
            {
                throw new Exception("DrawPTV", ex);
            }
            finally
            {
                if (bm != null)
                    bm.Dispose();

                if (bm2 != null)
                    bm2.Dispose();

                if (gs != null)
                    gs.Dispose();

                if (gs2 != null)
                    gs2.Dispose();
            }
        }

        private bool CalcRowsToFit(double PrintAreaHeight, ref int Rows, ref long RowsRemaining, double ItemHeight, double ItemVertGutter, bool IsWorknTurn, bool IsWorknTumble)
        {
            if (IsWorknTurn == true)
            {
                Rows = Convert.ToInt32(Math.Floor(Convert.ToDouble(PrintAreaHeight) / (Convert.ToDouble(ItemHeight) + ItemVertGutter)));
                RowsRemaining = Convert.ToInt64(((Convert.ToDouble(PrintAreaHeight) / (Convert.ToDouble(ItemHeight) + ItemVertGutter)) - Convert.ToDouble(Rows)) * (Convert.ToDouble(ItemHeight) + ItemVertGutter));
            }
            else if (IsWorknTumble == true)
            {
                Rows = Convert.ToInt32(Math.Floor(Convert.ToDouble(PrintAreaHeight) / (Convert.ToDouble(ItemHeight) + ItemVertGutter)));
                RowsRemaining = Convert.ToInt64(((Convert.ToDouble(PrintAreaHeight) / (Convert.ToDouble(ItemHeight) + ItemVertGutter)) - Convert.ToDouble(Rows)) * (Convert.ToDouble(ItemHeight) + ItemVertGutter));

                if (Rows % 2 == 1)
                {
                    Rows -= 1;
                }
            }
            else
            {
                Rows = Convert.ToInt32(Math.Floor(Convert.ToDouble(PrintAreaHeight) / (ItemHeight + ItemVertGutter)));
                RowsRemaining = Convert.ToInt64(((Convert.ToDouble(PrintAreaHeight) / (Convert.ToDouble(ItemHeight) + ItemVertGutter)) - Convert.ToDouble(Rows)) * (Convert.ToDouble(ItemHeight) + ItemVertGutter));
            }
            return true;
        }
        private static bool CalcColsToFit(double PrintAreaWidth, ref int Cols, ref long ColsRemaining, double ItemWidth, double ItemHorzGutter, bool IsWorknTurn, bool IsWorknTumble)
        {
            if (IsWorknTurn == true)
            {
                Cols = Convert.ToInt32(Math.Floor(Convert.ToDouble(PrintAreaWidth) / (Convert.ToDouble(ItemWidth) + ItemHorzGutter)));
                ColsRemaining = Convert.ToInt64(((Convert.ToDouble(PrintAreaWidth) / (Convert.ToDouble(ItemWidth) + ItemHorzGutter)) - Convert.ToDouble(Cols)) * (Convert.ToDouble(ItemWidth) + ItemHorzGutter));
                if (Cols % 2 == 1)
                {
                    Cols -= 1;
                }
            }
            else if (IsWorknTumble == true)
            {
                Cols = Convert.ToInt32(Math.Floor(Convert.ToDouble(PrintAreaWidth) / (Convert.ToDouble(ItemWidth) + ItemHorzGutter)));
                ColsRemaining = Convert.ToInt64(((Convert.ToDouble(PrintAreaWidth) / (Convert.ToDouble(ItemWidth) + ItemHorzGutter)) - Convert.ToDouble(Cols)) * (Convert.ToDouble(ItemWidth) + ItemHorzGutter));
            }
            else
            {
                Cols = Convert.ToInt32(Math.Floor(Convert.ToDouble(PrintAreaWidth) / (Convert.ToDouble(ItemWidth) + ItemHorzGutter)));
                ColsRemaining = Convert.ToInt64(((Convert.ToDouble(PrintAreaWidth) / (Convert.ToDouble(ItemWidth) + ItemHorzGutter)) - Convert.ToDouble(Cols)) * (Convert.ToDouble(ItemWidth) + ItemHorzGutter));
            }
            return true;
        }

        public static bool SetPrintView(ref double vPrintAreaHeight, ref double vPrintAreaWidth, double ItemHeight, double ItemWidth, bool PressRestrictions, GripSide Grip, byte View, int ColorBar, double PrintHeight, double PrintWidth,
                                        double GripDepth, double HeadDepth, double PrintGutter, bool IsWorknTurn, bool IsWorknTumble)
        {

            //This Function would set the following Variables which are sent byref others are for calculation
            //printAreaHeight i.e. vPrintAreaHeight
            //printAreaWidth  i.e. vPrintAreaWidth

            //If View = 0 Then  ''Landscape
            if (View == (byte)PrintViewOrientation.Landscape)
            {
                if (PressRestrictions == true)
                {
                    GetPrintArea(ref vPrintAreaHeight, ref vPrintAreaWidth, Grip, PrintHeight, PrintWidth, PrintGutter, GripDepth, HeadDepth, ColorBar, IsWorknTurn,
                    IsWorknTumble);
                }
                else
                {
                    if (Grip == GripSide.LongSide)
                    {
                        if (PrintHeight > PrintWidth)
                        {
                            vPrintAreaHeight = PrintHeight;
                            vPrintAreaWidth = PrintWidth - ColorBar;
                        }
                        else
                        {
                            vPrintAreaHeight = PrintHeight - ColorBar;
                            vPrintAreaWidth = PrintWidth;
                        }
                    }
                    else if (Grip == GripSide.ShortSide)
                    {
                        if (PrintHeight < PrintWidth)
                        {
                            vPrintAreaHeight = PrintHeight;
                            vPrintAreaWidth = PrintWidth - ColorBar;
                        }
                        else
                        {
                            vPrintAreaHeight = PrintHeight - ColorBar;
                            vPrintAreaWidth = PrintWidth;
                        }
                    }
                }
                //ElseIf View = 1 Then ''Portrait
                //'Portrait
            }
            else if (View == (byte)PrintViewOrientation.Portrait)
            {

                if (PressRestrictions)
                {
                    GetPrintArea(ref vPrintAreaHeight, ref vPrintAreaWidth, Grip, PrintHeight, PrintWidth, PrintGutter, GripDepth, HeadDepth, ColorBar, IsWorknTurn,
                    IsWorknTumble);
                }
                else
                {
                    if (Grip == GripSide.LongSide)
                    {
                        if (PrintHeight > PrintWidth)
                        {
                            vPrintAreaHeight = PrintHeight;
                            vPrintAreaWidth = PrintWidth - ColorBar;
                        }
                        else
                        {
                            vPrintAreaHeight = PrintHeight - ColorBar;
                            vPrintAreaWidth = PrintWidth;
                        }
                    }
                    else if (Grip == GripSide.ShortSide)
                    {
                        if (PrintHeight < PrintWidth)
                        {
                            vPrintAreaHeight = PrintHeight;
                            vPrintAreaWidth = PrintWidth - ColorBar;
                        }
                        else
                        {
                            vPrintAreaHeight = PrintHeight - ColorBar;
                            vPrintAreaWidth = PrintWidth;
                        }
                    }
                }
            }
            return true;
        }

        public static bool GetPrintArea(ref double vPrintAreaHeight, ref double vPrintAreaWidth, GripSide strGrip, double PrintHeight, double PrintWidth, double PrintGutter, double GripDepth, double HeadDepth, int ColorBar, bool IsWorknTurn,
                                        bool IsWorknTumble)
        {

            if (strGrip == GripSide.LongSide)
            {
                if (PrintHeight > PrintWidth)
                {
                    vPrintAreaHeight = PrintHeight - (PrintGutter * 2);
                    vPrintAreaWidth = PrintWidth - GripDepth - HeadDepth - ColorBar;
                }
                else
                {
                    vPrintAreaHeight = PrintHeight - GripDepth - HeadDepth - ColorBar;
                    vPrintAreaWidth = PrintWidth - (PrintGutter * 2);
                }
            }
            else if (strGrip == GripSide.ShortSide)
            {
                if (PrintHeight < PrintWidth)
                {
                    vPrintAreaHeight = PrintHeight - GripDepth - HeadDepth - ColorBar;
                    vPrintAreaWidth = PrintWidth - (PrintGutter * 2);
                }
                else
                {
                    vPrintAreaHeight = PrintHeight - (PrintGutter * 2);
                    vPrintAreaWidth = PrintWidth - GripDepth - HeadDepth - ColorBar;
                }
            }
            return true;
        }



        private double GetItemHeight(double OrignalItemHeight, double OrignalItemWidth, PrintViewOrientation CurrentOrientation)
        {
            if (CurrentOrientation == PrintViewOrientation.Landscape)     //LandScape
                return OrignalItemHeight;
            else                            //Portrait
                return OrignalItemWidth;

        }



        private double GetItemWidth(double OrignalItemHeight, double OrignalItemWidth, PrintViewOrientation CurrentOrientation)
        {
            if (CurrentOrientation == PrintViewOrientation.Landscape) //'LandScape
                return OrignalItemWidth;
            else     //'Portrait
                return OrignalItemHeight;

        }


        #endregion

        #region service Request Response
        public List<BestPress> GetBestPresses(ItemSection currentSection)
        {
            List<BestPress> bestpress = new List<BestPress>();
            List<Machine> EnablePresses = itemsectionRepository.GetEnabledPresses(Convert.ToDouble(currentSection.SectionSizeHeight), Convert.ToDouble(currentSection.SectionSizeWidth));
            CostCentre oPressCostCentre = itemsectionRepository.GetCostCenterBySystemType((int)SystemCostCenterTypes.Press);

            foreach (var press in EnablePresses)
            {

                //currentSection.PressId = press.MachineId;
                

                ItemSection updateSection = CalculatePressCost(currentSection, press.MachineId, false, false, (int)PressReRunModes.NotReRun, 1, 0, true);
                SectionCostcentre presscc = updateSection.SectionCostcentres.Where(c => c.CostCentreId == oPressCostCentre.CostCentreId && c.CostCentreType == 1).FirstOrDefault();
                if (presscc != null)
                {
                    bestpress.Add(new BestPress { MachineID = press.MachineId, MachineName = press.MachineName, Qty1Cost = Math.Round(presscc.Qty1NetTotal ?? 0, 2), Qty1RunTime = Math.Round(presscc.Qty1EstimatedTime, 2), Qty2Cost = Math.Round(presscc.Qty2NetTotal ?? 0, 2), Qty2RunTime = presscc.Qty2EstimatedTime, Qty3Cost = Math.Round(presscc.Qty3NetTotal ?? 0, 2), Qty3RunTime = presscc.Qty3EstimatedTime });
                    updateSection.SectionCostcentres.ToList().ForEach(a => updateSection.SectionCostcentres.Remove(a));
                }
            }
            return bestpress.OrderBy(p => p.Qty1Cost).ToList();

        }
        /// <summary>
        /// Best Prssses List Called from Controller
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public BestPressResponse GetBestPressResponse(ItemSection section)
        {
            var uCostCenters = itemsectionRepository.GetUserDefinedCostCentres();
            return new BestPressResponse
            {
                PressList = GetBestPresses(section),
                UserCostCenters = uCostCenters
            };
        }

        public ItemSection GetUpdatedSectionWithSystemCostCenters(ItemSection currentSection)
        {
            //List<SectionInkCoverage> AllInks = currentSection.SectionInkCoverages.ToList();
            if (currentSection.Qty1 <= 0)
                return currentSection;
            int PressId = Convert.ToInt32(currentSection.PressId);
            int PressIdSide2 = Convert.ToInt32(currentSection.PressIdSide2);
            List<int> SystemCostCenterTypes = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            if (currentSection.SectionCostcentres != null)
                currentSection.SectionCostcentres.Where(a => SystemCostCenterTypes.Contains(a.SystemCostCentreType ?? 0) || a.Name == "Web Order Cost Center" || a.Name == "Blank Sheet" || a.Name == "Stock(s)").ToList().ForEach(a => currentSection.SectionCostcentres.Remove(a));
            ItemSection updatedSection = currentSection;
            int SetupSpoilage = 0;
            double RunningSpoilage = 0;
            //int NoofInks = AllInks != null? AllInks.Count() : 0;
            var pressSide1 = itemsectionRepository.GetPressById(PressId);
            var pressSide2 = itemsectionRepository.GetPressById(PressIdSide2);
            
            //Highest setup spoilage between the two presses will be set.
           var value= currentSection.IsDoubleSided ?? false ;
           if (value)
           {
               if (currentSection.isWorknTurn == true)
                   updatedSection.PassesSide1 = (pressSide1.Passes ?? 1) * 2;
               else
               {
                   updatedSection.PassesSide1 = pressSide1.Passes ?? 1;
                   updatedSection.PassesSide2 = pressSide2.Passes ?? 1;
               }
                   
               if (pressSide1.SetupSpoilage > pressSide2.SetupSpoilage)
                    SetupSpoilage = pressSide1.SetupSpoilage ?? 0;
                else
                    SetupSpoilage = pressSide2.SetupSpoilage ?? 0;
                //Highest running spoilage between the two presses will be set.
                if (pressSide1.RunningSpoilage > pressSide2.RunningSpoilage)
                    RunningSpoilage = pressSide1.RunningSpoilage ?? 0;
                else
                    RunningSpoilage = pressSide2.RunningSpoilage ?? 0;
           }
            else
            {
                SetupSpoilage = pressSide1.SetupSpoilage ?? 0;
                RunningSpoilage = pressSide1.RunningSpoilage ?? 0;
                updatedSection.PassesSide1 = pressSide1.Passes ?? 1;
            }
            


            updatedSection.SetupSpoilage = SetupSpoilage;
            updatedSection.RunningSpoilage = Convert.ToInt32(RunningSpoilage);

           // updatedSection = CalculateInkCost(updatedSection, 1, PressId, false, false, AllInks); //Ink Cost Center
            if (updatedSection.PrintingType != null && updatedSection.PrintingType != (int)PrintingTypeEnum.SheetFed)//paper costcentre
            {
                updatedSection = CalculatePaperCostWebPress(updatedSection, Convert.ToInt32(updatedSection.PressId), false, false);
            }
            else
            {
                if (updatedSection.PrintViewLayout == null || updatedSection.PrintViewLayout == 0)
                    updatedSection.PrintViewLayout = 0;
                else
                    updatedSection.PrintViewLayout = 1;
                updatedSection = CalculatePaperCost(updatedSection, Convert.ToInt32(updatedSection.PressId), false, false);
            }
            if (pressSide1.isplateused != null && pressSide1.isplateused != false)//Plates
            {
                if (updatedSection.IsPlateSupplied == null)
                    updatedSection.IsPlateSupplied = false;
                updatedSection.Side1PlateQty = pressSide1.ColourHeads;
                updatedSection.PlateId = pressSide1.DefaultPlateId;
                updatedSection = CalculatePlateCost(updatedSection, false, false, true);
            }
            if (pressSide1.ismakereadyused != null && pressSide1.ismakereadyused == true)//Make Readies
            {
                updatedSection.MakeReadyQty = pressSide1.ColourHeads;
                updatedSection = CalculateMakeReadyCost(updatedSection, Convert.ToInt32(updatedSection.PressId), false, false, true);
            }
            if (pressSide1.iswashupused != null && pressSide1.iswashupused == true)//Washups
            {
                updatedSection.WashupQty = pressSide1.ColourHeads;
                updatedSection = CalculateWashUpCost(updatedSection, Convert.ToInt32(updatedSection.PressId), false, false, true);
            }
            if (updatedSection.PrintingType != null && updatedSection.PrintingType != (int)PrintingTypeEnum.SheetFed)
            {
               // CalculatePressCostWebPress
                //CalculateGuillotineCostWebPress
                updatedSection = CalculatePressCostWebPress(updatedSection, pressSide1, false, false, PressReRunModes.NotReRun, 1, 0, false);
            }
            else
            {
                updatedSection = CalculatePressCostWithSides(updatedSection, (int)updatedSection.PressId, false, false, 1, 1, 0);
               // updatedSection = CalculatePressCost(updatedSection, (int)updatedSection.PressId, false, false, 1, 1, 0);
            }

            if (updatedSection.IsDoubleSided == true && updatedSection.isWorknTurn != true && (updatedSection.PrintingType != null && updatedSection.PrintingType == (int)PrintingTypeEnum.SheetFed))
            {
                if (pressSide2.isplateused != null && pressSide2.isplateused != false)//Plates
                {
                    if (updatedSection.IsPlateSupplied == null)
                        updatedSection.IsPlateSupplied = false;
                    updatedSection.Side2PlateQty = pressSide2.ColourHeads;
                    updatedSection.PlateId = pressSide1.DefaultPlateId;
                    updatedSection = CalculatePlateCost(updatedSection, false, false, false);
                }
                if (pressSide2.ismakereadyused != null && pressSide2.ismakereadyused == true)//Make Readies
                {
                    updatedSection.MakeReadyQty = pressSide2.ColourHeads;
                    updatedSection = CalculateMakeReadyCost(updatedSection, Convert.ToInt32(updatedSection.PressIdSide2), false, false, false);
                }
                if (pressSide2.iswashupused != null && pressSide2.iswashupused == true)//Washups
                {
                    updatedSection.WashupQty = pressSide2.ColourHeads;
                    updatedSection = CalculateWashUpCost(updatedSection, Convert.ToInt32(updatedSection.PressIdSide2), false, false, false);
                }
                
                
                if(updatedSection.PressIdSide2 != null && updatedSection.PressIdSide2 > 0)
                    updatedSection = CalculatePressCostWithSides(updatedSection, (int)updatedSection.PressIdSide2, false, false, 1, 1, 0, false, true);
            }

            if (updatedSection.IsSecondTrim == true)
            {
                updatedSection.IsFirstTrim = true;
                var guillotine = machineRepository.GetDefaultGuillotine();
                if (guillotine != null)
                {
                    updatedSection.GuillotineId = guillotine.MachineId;
                    updatedSection = CalculateGuillotineCost(updatedSection, guillotine.MachineId, false, false);
                }
                    
            }

            updatedSection.BaseCharge1 = updatedSection.SectionCostcentres.Sum(a => a.Qty1NetTotal);
            updatedSection.BaseCharge2 = updatedSection.SectionCostcentres.Sum(a => a.Qty2NetTotal);
            updatedSection.Basecharge3 = updatedSection.SectionCostcentres.Sum(a => a.Qty3NetTotal);
            if (updatedSection.SimilarSections > 0)
            {
                updatedSection.BaseCharge1 = Math.Round((updatedSection.BaseCharge1 ?? 0 * updatedSection.SimilarSections ?? 1), 2);
                updatedSection.BaseCharge2 = Math.Round((updatedSection.BaseCharge2 ?? 0 * updatedSection.SimilarSections ?? 1), 2);
                updatedSection.Basecharge3 = Math.Round((updatedSection.Basecharge3 ?? 0 * updatedSection.SimilarSections ?? 1), 2);
            }
            if (updatedSection.StockItem == null)
                updatedSection.StockItem = itemsectionRepository.GetStockById(Convert.ToInt64(updatedSection.StockItemID1));

            return updatedSection;

        }
        #endregion

        private Organisation CompanyGeneralSettings()
        {
            return organisationRepository.GetOrganizatiobByID();
        }

        public bool SaveItemSection(ItemSection currItemSection)
        {
            if (currItemSection.ItemSectionId > 0)
            {
                itemsectionRepository.Update(currItemSection);
                itemsectionRepository.SaveChanges();
            }
            else
            {
                
            }
            return true;
        }

        public ItemSection GetItemSectionById(long itemsectionId)
        {
            return itemsectionRepository.GetItemSectionById(itemsectionId);
        }

        public ItemSection CalculatePressCostWebPress(ItemSection oItemSection, Machine oPressDTO, bool IsReRun = false,
            bool IsWorkInstructionsLocked = false, PressReRunModes PressReRunMode = PressReRunModes.NotReRun,
            int PressReRunQuantityIndex = 1, double OverrideValue = 0, bool isPressSide2 = false)
        {
            JobPreference oJobCardOptionsDTO = itemsectionRepository.GetJobPreferences(1);
            bool functionReturnValue = false;
            string sMinimumCost = null;
            double PassesFront = 0;
            double PassesBack = 0;
            string strPrintLookup = null;
            double dblPlateQty = 0;
            CostCentre oCostCentreDTO =
                itemsectionRepository.GetCostCenterBySystemType((int) SystemCostCenterTypes.Press);

            int PrintSheetPTV = 0;
            int SetupSpoilage = 0;
            double RunningSpoilagePercentage = 0;

            double PressCost1 = 0;
            double PressPrice1 = 0;
            double PressCost2 = 0;
            double PressPrice2 = 0;
            double PressCost3 = 0;
            double PressPrice3 = 0;
            double PressRunTime1 = 0;
            double PressRunTime2 = 0;
            double PressRunTime3 = 0;
            double dblPressPass = 0;

            dblPressPass = isPressSide2
                ? Convert.ToDouble(oItemSection.PassesSide2)
                : Convert.ToDouble(oItemSection.PassesSide1);
            //'# of times the printsheets need to be passed thru the given press, based on the # of colours
            //'that are printed on the printsheets and the # of colours the press can print at a time, i.e.
            //'how many heads it has.

            try
            {


                StockItem oPaperDTO = new StockItem();
                oPaperDTO = itemsectionRepository.GetStockById(Convert.ToInt64(oItemSection.StockItemID1));

                double[] OrderPaperLengthWithSpoilage = new double[3];
                double[] OrderPaperLengthWithoutSpoilage = new double[3];
                double[] OrderPaperLengthWithSpoilageSqMeters = new double[3];
                double[] OrderPaperLengthWithoutSpoilageSqMeters = new double[3];
                double[] OrderPaperReelsWithSpoilageQty = new double[1];
                double[] OrderPaperReelsWithoutSpoilageQty = new double[1];
                double[] OrderPaperWeightWithSpoilage = new double[3];
                double[] OrderPaperWeightWithoutSpoilage = new double[3];
                double[] SectionQtyWithoutSpoilage = new double[3];
                double[] SectionQtyWithSpoilage = new double[3];
                int TempQuantity = 0;
                double[] Spoilage = new double[3];
                //converted into mm heights/widths
                double SectionHeight = 0;
                double SectionWidth = 0;

                //double ReelLength = 0;
                //double ReelWidth = 0;

                ////convert reel width from whatever standard into mm
                //if (oPaperDTO.RollStandards == (int)lengthunit.Cm)
                //    ReelWidth = ConvertLength(Convert.ToDouble(oPaperDTO.RollWidth), lengthunit.Cm, lengthunit.Mm);
                //else if (oPaperDTO.RollStandards == (int)lengthunit.Inch)
                //    ReelWidth = ConvertLength(Convert.ToDouble(oPaperDTO.RollWidth), lengthunit.Inch, lengthunit.Mm);
                //else
                //    ReelWidth = Convert.ToDouble(oPaperDTO.RollWidth);
                ////roll length is always going into meters
                //ReelLength = oPaperDTO.RollLength ?? 1;

                SectionHeight = oItemSection.SectionSizeHeight ?? 1;

                SectionWidth = oItemSection.SectionSizeWidth ?? 1;

                if (oItemSection.PrintViewLayout == (int) PrintViewOrientation.Landscape)
                {
                    PrintSheetPTV = Convert.ToInt32(oItemSection.PrintViewLayoutLandScape);
                }
                else
                {
                    PrintSheetPTV = Convert.ToInt32(oItemSection.PrintViewLayoutPortrait);
                }

                if (PrintSheetPTV == 0)
                {
                    throw new MPCException("Please set correct print dimensions.", itemsectionRepository.OrganisationId);
                }


                for (int i = 0; i <= 2; i++)
                {
                    if (i == 0)
                    {
                        TempQuantity = oItemSection.Qty1 ?? 0;
                    }
                    else if (i == 1)
                    {
                        TempQuantity = oItemSection.Qty2 ?? 0;
                    }
                    else if (i == 2)
                    {
                        TempQuantity = oItemSection.Qty3 ?? 0;
                    }

                    SectionQtyWithoutSpoilage[i] = Math.Ceiling(Convert.ToDouble(TempQuantity/PrintSheetPTV));
                    SectionQtyWithSpoilage[i] = SectionQtyWithoutSpoilage[i];

                    //calculating spoilage

                    //todo apply condition here ..
                    //in case spoilage is in sheets
                    if (oItemSection.WebSpoilageType == (int) WebSpoilageTypes.inSheets)
                    {
                        Spoilage[i] = oItemSection.SetupSpoilage ??
                                      0 + (SectionQtyWithoutSpoilage[i]*oItemSection.RunningSpoilage ?? 1/100);
                        SectionQtyWithSpoilage[i] += Spoilage[i];
                    }
                    else
                    {
                        //in case spoilage is in Meters
                        Spoilage[i] = oItemSection.SetupSpoilage ??
                                      1*SectionHeight/1000 +
                                      (SectionQtyWithoutSpoilage[i]*oItemSection.RunningSpoilage ??
                                       1/100*SectionHeight/1000);
                        SectionQtyWithSpoilage[i] += Spoilage[i];
                    }

                    //calculating paper required in meters

                    OrderPaperLengthWithoutSpoilage[i] = SectionQtyWithoutSpoilage[i]*SectionHeight/1000;
                    OrderPaperLengthWithSpoilage[i] = SectionQtyWithSpoilage[i]*SectionHeight/1000;


                    //OrderPaperLengthWithSpoilage(i) = Model.Common.RoundUp(OrderPaperLengthWithSpoilage(i) / ReelLength)
                    //OrderPaperLengthWithoutSpoilage(i) = Model.Common.RoundUp(OrderPaperLengthWithoutSpoilage(i) / ReelLength)

                    OrderPaperLengthWithSpoilageSqMeters[i] = (OrderPaperLengthWithSpoilage[i]*SectionWidth/1000)/100;
                    OrderPaperLengthWithoutSpoilageSqMeters[i] = (OrderPaperLengthWithoutSpoilage[i]*SectionWidth/1000)/
                                                                 100;

                    OrderPaperWeightWithSpoilage[i] =
                        (ConvertWeight((double) oPaperDTO.ItemWeight, WeightUnits.GSM, WeightUnits.lbs)*100*
                         OrderPaperLengthWithSpoilageSqMeters[i]);
                    OrderPaperWeightWithoutSpoilage[i] =
                        (ConvertWeight((double) oPaperDTO.ItemWeight, WeightUnits.GSM, WeightUnits.lbs)*100*
                         OrderPaperLengthWithoutSpoilageSqMeters[i]);

                }

                //using the selected press method in the wizard
                //Dim oRows() As DataRow = oPressDTO.Methods.Select("DefaultMethod=1")
                //Dim oModelLookUpMethod As Model.LookupMethods.MethodDTO = BLL.LookupMethods.Method.GetMachineLookUpMethod(GlobalData, CInt(oRows(0)("MethodID")))
                int temp = 0;
                int temp2 = 0;
                if (PressReRunMode == PressReRunModes.NotReRun)
                {
                    temp = 0;
                    temp2 = 2;
                }
                else if (PressReRunMode == PressReRunModes.ReRunPress)
                {
                    temp = PressReRunQuantityIndex - 1;
                    temp2 = PressReRunQuantityIndex - 1;
                }

                LookupMethod oModelLookUpMethod = new LookupMethod();
                oModelLookUpMethod = itemsectionRepository.GetLookupMethodById(Convert.ToInt64(oPressDTO.LookupMethodId));

                double[] dblPrintCost = new double[3];
                double[] dblPrintPrice = new double[3];
                double[] dblPrintRun = new double[3];
                double[] dblPrintPlateQty = new double[3];

                switch (oModelLookUpMethod.Type)
                {

                    case (int) MethodTypes.MeterPerHour:
                        //Speed Weight
                        ///'=====================================================
                        int dblPrintChge = 0;
                        int dblPrintChgeRate = 0;
                        int[] intGSM = new int[4];
                        int[] intSheets = new int[6];
                        double[,] intSpeed = new double[4, 6];
                        double dblSpeed1 = 0;
                        double dblSpeed2 = 0;
                        double[] dblPrintSpeed = new double[5];


                        //Getting the Plant Lookup Information itemsectionRepository.GetMachineSpeedWeightLookup(oModelLookUpMethod.MethodId);
                        MachineMeterPerHourLookup oModelSpeedWeight =
                            itemsectionRepository.GetMachinemeterPerHourLookup(oModelLookUpMethod.MethodId);

                        //There are 3 GSM Setting their Values
                        intGSM[1] = Convert.ToInt32(oModelSpeedWeight.SheetWeight1);
                        intGSM[2] = Convert.ToInt32(oModelSpeedWeight.SheetWeight2);
                        intGSM[3] = Convert.ToInt32(oModelSpeedWeight.SheetWeight3);
                        //Setting the Sheet Values
                        intSheets[1] = Convert.ToInt32(oModelSpeedWeight.SheetsQty1);
                        intSheets[2] = Convert.ToInt32(oModelSpeedWeight.SheetsQty2);
                        intSheets[3] = Convert.ToInt32(oModelSpeedWeight.SheetsQty3);
                        intSheets[4] = Convert.ToInt32(oModelSpeedWeight.SheetsQty4);
                        intSheets[5] = Convert.ToInt32(oModelSpeedWeight.SheetsQty5);

                        //Setting up the Speed Values
                        intSpeed[1, 1] = Convert.ToInt32(oModelSpeedWeight.speedqty11);
                        intSpeed[1, 2] = Convert.ToInt32(oModelSpeedWeight.speedqty12);
                        intSpeed[1, 3] = Convert.ToInt32(oModelSpeedWeight.speedqty13);
                        intSpeed[1, 4] = Convert.ToInt32(oModelSpeedWeight.speedqty14);
                        intSpeed[1, 5] = Convert.ToInt32(oModelSpeedWeight.speedqty15);
                        intSpeed[2, 1] = Convert.ToInt32(oModelSpeedWeight.speedqty21);
                        intSpeed[2, 2] = Convert.ToInt32(oModelSpeedWeight.speedqty22);
                        intSpeed[2, 3] = Convert.ToInt32(oModelSpeedWeight.speedqty23);
                        intSpeed[2, 4] = Convert.ToInt32(oModelSpeedWeight.speedqty24);
                        intSpeed[2, 5] = Convert.ToInt32(oModelSpeedWeight.speedqty25);
                        intSpeed[3, 1] = Convert.ToInt32(oModelSpeedWeight.speedqty31);
                        intSpeed[3, 2] = Convert.ToInt32(oModelSpeedWeight.speedqty32);
                        intSpeed[3, 3] = Convert.ToInt32(oModelSpeedWeight.speedqty33);
                        intSpeed[3, 4] = Convert.ToInt32(oModelSpeedWeight.speedqty34);
                        intSpeed[3, 5] = Convert.ToInt32(oModelSpeedWeight.speedqty35);


                        //Checking that the press support this GSM or Not
                        if (oPaperDTO.ItemWeight > oPressDTO.maximumsheetweight)
                        {
                            throw new MPCException(LanguageResources.ItemService_PressNoSupported, itemsectionRepository.OrganisationId);
                            
                        }

                        byte btGSM = 0;
                        byte btRun = 0;
                        double dblGSM = 0;
                        double dblRun = 0;

                        //'Get the GSM
                        //Checking for the PaperGSM

                        if (oPaperDTO.ItemWeight == intGSM[1])
                        {
                            btGSM = 1;
                            dblGSM = 0;
                        }
                        else if (oPaperDTO.ItemWeight == intGSM[2])
                        {
                            btGSM = 2;
                            dblGSM = 0;
                        }
                        else if (oPaperDTO.ItemWeight == intGSM[3])
                        {
                            btGSM = 3;
                            dblGSM = 0;
                        }
                        else
                        {
                            if (oPaperDTO.ItemWeight > intGSM[1] && oPaperDTO.ItemWeight < intGSM[2])
                            {
                                dblGSM = (double) (intGSM[2] - oPaperDTO.ItemWeight)/(intGSM[2] - intGSM[1]);
                                btGSM = 1;
                            }
                            else if (oPaperDTO.ItemWeight > intGSM[2] && oPaperDTO.ItemWeight < intGSM[3])
                            {
                                dblGSM = (double) (intGSM[3] - oPaperDTO.ItemWeight)/(intGSM[3] - intGSM[2]);
                                btGSM = 2;
                            }
                        }


                        //'Get the Sheets
                        for (int i = temp; i <= temp2; i++)
                        {
                            //Exact Values i.e. 2000 Sheets, 3000 Sheets etc.
                            //*****Difference between dblRun and btRun
                            if (OrderPaperLengthWithSpoilage[i] <= intSheets[1])
                            {
                                dblRun = 0;
                                btRun = 1;
                            }
                            else if (OrderPaperLengthWithSpoilage[i] == intSheets[2])
                            {
                                dblRun = 0;
                                btRun = 2;
                            }
                            else if (OrderPaperLengthWithSpoilage[i] == intSheets[3])
                            {
                                dblRun = 0;
                                btRun = 3;
                            }
                            else if (OrderPaperLengthWithSpoilage[i] == intSheets[4])
                            {
                                dblRun = 0;
                                btRun = 4;
                            }
                            else if (OrderPaperLengthWithSpoilage[i] >= intSheets[5])
                            {
                                dblRun = 0;
                                btRun = 5;
                            }
                                //Now determining which run for Other than Exact values and checking in which range it lies for example 2300
                                //Running the Previous run for example for 2300, 2000 run would be used
                            else
                            {
                                if (OrderPaperLengthWithSpoilage[i] < intSheets[2] &
                                    OrderPaperLengthWithSpoilage[i] > intSheets[1])
                                {
                                    dblRun = (intSheets[2] - OrderPaperLengthWithSpoilage[i])/
                                             (intSheets[2] - intSheets[1]);
                                    btRun = 1;
                                }
                                else if (OrderPaperLengthWithSpoilage[i] < intSheets[3] &
                                         OrderPaperLengthWithSpoilage[i] > intSheets[2])
                                {
                                    dblRun = (intSheets[3] - OrderPaperLengthWithSpoilage[i])/
                                             (intSheets[3] - intSheets[2]);
                                    btRun = 2;
                                }
                                else if (OrderPaperLengthWithSpoilage[i] < intSheets[4] &
                                         OrderPaperLengthWithSpoilage[i] > intSheets[3])
                                {
                                    dblRun = (intSheets[4] - OrderPaperLengthWithSpoilage[i])/
                                             (intSheets[4] - intSheets[3]);
                                    btRun = 3;
                                }
                                else if (OrderPaperLengthWithSpoilage[i] < intSheets[5] &
                                         OrderPaperLengthWithSpoilage[i] > intSheets[4])
                                {
                                    dblRun = (intSheets[5] - OrderPaperLengthWithSpoilage[i])/
                                             (intSheets[5] - intSheets[4]);
                                    btRun = 4;
                                }
                            }


                            if (btRun == 5)
                            {
                                //Determining Which Speed for GSM1 and GSM2
                                dblSpeed1 = intSpeed[btGSM - 1, btRun] -
                                            ((intSpeed[btGSM - 1, btRun] - intSpeed[btGSM, btRun])*dblGSM);
                                dblSpeed2 = intSpeed[btGSM, btRun] -
                                            ((intSpeed[btGSM, btRun] - intSpeed[btGSM, btRun])*dblGSM);
                                //Other Than 5
                            }
                            else
                            {
                                dblSpeed1 = intSpeed[btGSM, btRun + 1] -
                                            ((intSpeed[btGSM, btRun + 1] - intSpeed[btGSM, btRun])*dblGSM);
                                dblSpeed2 = intSpeed[btGSM, btRun] -
                                            ((intSpeed[btGSM - 1, btRun] - intSpeed[btGSM, btRun])*dblGSM);
                            }
                            //Setting the Exact Speed

                            if (PressReRunMode == PressReRunModes.CalculateValuesToShow)
                            {
                                dblPrintSpeed[i] = dblSpeed2 - ((dblSpeed2 - dblSpeed1)*dblRun);
                                if (PressReRunQuantityIndex - 1 == i)
                                {
                                    OverrideValue = dblPrintSpeed[i];
                                    //  return functionReturnValue;
                                }

                            }
                            else if (PressReRunMode == PressReRunModes.NotReRun)
                            {
                                dblPrintSpeed[i] = dblSpeed2 - ((dblSpeed2 - dblSpeed1)*dblRun);
                            }
                            else if (PressReRunMode == PressReRunModes.ReRunPress)
                            {
                                if (PressReRunQuantityIndex - 1 == i)
                                {
                                    dblPrintSpeed[i] = OverrideValue;
                                }
                            }


                            //Updating the Press Speed (Pro Rata)
                            if (PressReRunMode == PressReRunModes.NotReRun)
                            {
                                oItemSection.PressSpeed1 = Convert.ToInt32(dblPrintSpeed[0]);
                                oItemSection.PressSpeed2 = Convert.ToInt32(dblPrintSpeed[1]);
                                oItemSection.PressSpeed3 = Convert.ToInt32(dblPrintSpeed[2]);

                                PressRunTime1 =
                                    Convert.ToDouble(OrderPaperLengthWithSpoilage[0]/oItemSection.PressSpeed1);
                                PressRunTime2 =
                                    Convert.ToDouble(OrderPaperLengthWithSpoilage[1]/oItemSection.PressSpeed2);
                                PressRunTime3 =
                                    Convert.ToDouble(OrderPaperLengthWithSpoilage[2]/oItemSection.PressSpeed3);
                            }
                            else if (PressReRunMode == PressReRunModes.ReRunPress)
                            {
                                if (PressReRunQuantityIndex == 1)
                                {
                                    oItemSection.PressSpeed1 = Convert.ToInt32(dblPrintSpeed[0]);
                                    PressRunTime1 =
                                        Convert.ToDouble(OrderPaperLengthWithSpoilage[0]/oItemSection.PressSpeed1);
                                }
                                else if (PressReRunQuantityIndex == 2)
                                {
                                    oItemSection.PressSpeed2 = Convert.ToInt32(dblPrintSpeed[1]);
                                    PressRunTime2 =
                                        Convert.ToDouble(OrderPaperLengthWithSpoilage[1]/oItemSection.PressSpeed2);
                                }
                                else if (PressReRunQuantityIndex == 3)
                                {
                                    oItemSection.PressSpeed3 = Convert.ToInt32(dblPrintSpeed[2]);
                                    PressRunTime3 =
                                        Convert.ToDouble(OrderPaperLengthWithSpoilage[2]/oItemSection.PressSpeed3);
                                }

                            }

                            double dblCoverageMultiple = 1;
                            if (isPressSide2)
                            {
                                if (oItemSection.ImpressionCoverageSide2 == 1)
                                    dblCoverageMultiple = oPressDTO.CoverageHigh ?? 1;
                                else if (oItemSection.ImpressionCoverageSide2 == 2)
                                    dblCoverageMultiple = oPressDTO.CoverageMedium ?? 1;
                                else if (oItemSection.ImpressionCoverageSide2 == 3)
                                    dblCoverageMultiple = oPressDTO.CoverageLow ?? 1;
                            }
                            else
                            {
                                if (oItemSection.ImpressionCoverageSide1 == 1)
                                    dblCoverageMultiple = oPressDTO.CoverageHigh ?? 1;
                                else if (oItemSection.ImpressionCoverageSide1 == 2)
                                    dblCoverageMultiple = oPressDTO.CoverageMedium ?? 1;
                                else if (oItemSection.ImpressionCoverageSide1 == 3)
                                    dblCoverageMultiple = oPressDTO.CoverageLow ?? 1;
                            }

                            //Will be calculated for both sides, 
                            dblPrintCost[i] =
                                Convert.ToDouble((dblPressPass*
                                                  ((OrderPaperLengthWithSpoilage[i]/dblPrintSpeed[i])*
                                                   oModelSpeedWeight.hourlyCost)*dblCoverageMultiple) +
                                                 oPressDTO.SetupCharge);
                            dblPrintRun[i] = (dblPressPass*OrderPaperLengthWithSpoilage[i]);
                            dblPrintPrice[i] =
                                Convert.ToDouble((dblPressPass*
                                                  ((OrderPaperLengthWithSpoilage[i]/dblPrintSpeed[i])*
                                                   oModelSpeedWeight.hourlyPrice)*dblCoverageMultiple) +
                                                 oPressDTO.SetupCharge);
                        }








                        if (PressReRunMode == PressReRunModes.NotReRun)
                        {
                            PressCost1 = dblPrintCost[0];
                            PressCost2 = dblPrintCost[1];
                            PressCost3 = dblPrintCost[2];

                            PressPrice1 = dblPrintPrice[0];
                            PressPrice2 = dblPrintPrice[1];
                            PressPrice3 = dblPrintPrice[2];

                            oItemSection.PressHourlyCharge = oModelSpeedWeight.hourlyPrice;
                        }
                        else if (PressReRunMode == PressReRunModes.ReRunPress)
                        {
                            if (PressReRunQuantityIndex == 1)
                            {
                                PressCost1 = dblPrintCost[0];
                                PressPrice1 = dblPrintPrice[0];
                            }
                            else if (PressReRunQuantityIndex == 2)
                            {
                                PressCost2 = dblPrintCost[1];
                                PressPrice2 = dblPrintPrice[1];
                            }
                            else if (PressReRunQuantityIndex == 3)
                            {
                                PressCost3 = dblPrintCost[2];
                                PressPrice3 = dblPrintPrice[2];
                            }
                        }

                        break;
                }
                //End LookUp Calculations


                for (int i = temp; i <= temp2; i++)
                {
                    //Checking whether Print Cost is Less than Press Minimum Charge if it is then setting to Press Minimum Charge
                    if (dblPrintPrice[i] < oPressDTO.MinimumCharge)
                    {
                        dblPrintPrice[i] = oPressDTO.MinimumCharge ?? 0;
                        sMinimumCost += "1";
                    }
                    else
                    {
                        sMinimumCost += "0";
                    }
                }

                //Updating the 'Print Charge (excl Make Readies) (Pro Rata)
                //Updating the 'Print Charge (excl Make Readies) (Pro Rata)
                if (PressReRunMode == PressReRunModes.NotReRun)
                {
                    oItemSection.PrintChargeExMakeReady1 = dblPrintPrice[0] - oPressDTO.SetupCharge;
                    oItemSection.PrintChargeExMakeReady2 = dblPrintPrice[1] - oPressDTO.SetupCharge;
                    oItemSection.PrintChargeExMakeReady3 = dblPrintPrice[2] - oPressDTO.SetupCharge;
                }
                else
                {
                    if (PressReRunQuantityIndex == 1)
                    {
                        oItemSection.PrintChargeExMakeReady1 = dblPrintPrice[0] - oPressDTO.SetupCharge;
                    }
                    else if (PressReRunQuantityIndex == 2)
                    {
                        oItemSection.PrintChargeExMakeReady2 = dblPrintPrice[1] - oPressDTO.SetupCharge;
                    }
                    else if (PressReRunQuantityIndex == 3)
                    {
                        oItemSection.PrintChargeExMakeReady3 = dblPrintPrice[2] - oPressDTO.SetupCharge;
                    }

                }
                //===============================================================================================



                //Updating the press object   
                //oItemSection.Press = oModelPress

                SectionCostcentre oItemSectionCostCenter = new SectionCostcentre();
                if (IsReRun == false)
                {

                    oItemSectionCostCenter.ItemSectionId = oItemSection.ItemSectionId;
                    oItemSectionCostCenter.CostCentreId = oCostCentreDTO.CostCentreId;
                    oItemSectionCostCenter.SystemCostCentreType = (int) SystemCostCenterTypes.Press;
                    oItemSectionCostCenter.Order = 107;
                    oItemSectionCostCenter.IsOptionalExtra = 0;
                    oItemSectionCostCenter.CostCentreType = 1;
                    oItemSectionCostCenter.IsDirectCost = Convert.ToInt16(oPressDTO.DirectCost);
                }
                else
                {
                    //
                }

                oItemSectionCostCenter.SetupTime =
                    Convert.ToDouble((oPressDTO.SetupTime == 0 ? 0 : oPressDTO.SetupTime/60));

                oItemSectionCostCenter.IsPrintable = Convert.ToInt16(oJobCardOptionsDTO.IsDefaultPressInstruction);
                double ProfitMargin = 0;
                var markup = itemsectionRepository.GetMarkupById(oCostCentreDTO.DefaultVAId);
                if (markup != null)
                    ProfitMargin = Convert.ToDouble(markup.MarkUpRate);

                if (oItemSection.Qty1 > 0)
                {
                    if (PressReRunMode == PressReRunModes.NotReRun)
                    {
                        //Setting Cost to Print Cost calculated + Press Setup Cost
                        oItemSectionCostCenter.Qty1Charge = dblPrintPrice[0];
                        oItemSectionCostCenter.Qty1MarkUpID = oCostCentreDTO.DefaultVAId;
                        oItemSectionCostCenter.Qty1MarkUpValue = oItemSectionCostCenter.Qty1Charge*ProfitMargin/100;
                        oItemSectionCostCenter.Qty1NetTotal = oItemSectionCostCenter.Qty1Charge +
                                                              oItemSectionCostCenter.Qty1MarkUpValue;
                        oItemSectionCostCenter.Qty1EstimatedTime = PressRunTime1 + oItemSectionCostCenter.SetupTime;
                        oItemSectionCostCenter.Qty1EstimatedPlantCost = dblPrintCost[0];

                        oItemSection.ImpressionQty1 = Convert.ToInt32(SectionQtyWithSpoilage[0]*dblPressPass);

                        if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsDefaultPressInstruction == true)
                        {
                            if (oJobCardOptionsDTO.IsWorkingSize == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions = "Working Size = " +
                                                                              oItemSection.SectionSizeHeight +
                                                                              itemsectionRepository.GetLengthUnitName(
                                                                                  (int)
                                                                                      CompanyGeneralSettings()
                                                                                          .SystemLengthUnit) + " x " +
                                                                              oItemSection.SectionSizeWidth +
                                                                              itemsectionRepository.GetLengthUnitName(
                                                                                  (int)
                                                                                      CompanyGeneralSettings()
                                                                                          .SystemLengthUnit) + " " +
                                                                              Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsItemSize == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += "Item Size (Flat) = " +
                                                                               oItemSection.ItemSizeHeight +
                                                                               itemsectionRepository.GetLengthUnitName(
                                                                                   (int)
                                                                                       CompanyGeneralSettings()
                                                                                           .SystemLengthUnit) + " x " +
                                                                               oItemSection.ItemSizeWidth +
                                                                               itemsectionRepository.GetLengthUnitName(
                                                                                   (int)
                                                                                       CompanyGeneralSettings()
                                                                                           .SystemLengthUnit) + " " +
                                                                               Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsPrintSheetQty == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += "Print Sheet Qty = " +
                                                                               SectionQtyWithSpoilage[0];
                            }
                            if (oJobCardOptionsDTO.IsNumberOfPasses == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += " ;  Number of Passes = " +
                                                                               (PassesBack + PassesFront) +
                                                                               Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsImpressionCount == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += " Impression Count = " +
                                                                               oItemSection.ImpressionQty1 +
                                                                               Environment.NewLine;
                            }

                            if (oJobCardOptionsDTO.IsPressEstTime == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += " Estimated Time:= " +
                                                                               oItemSectionCostCenter.Qty1EstimatedTime
                                                                                   .ToString("f") + " hours";
                            }


                        }
                    }
                    else if (PressReRunMode == PressReRunModes.ReRunPress & PressReRunQuantityIndex == 1)
                    {
                        //Setting Cost to Print Cost calculated + Press Setup Cost
                        oItemSectionCostCenter.Qty1Charge = dblPrintPrice[0];
                        oItemSectionCostCenter.Qty1MarkUpID = oCostCentreDTO.DefaultVAId;
                        oItemSectionCostCenter.Qty1MarkUpValue = oItemSectionCostCenter.Qty1Charge*ProfitMargin/100;
                        oItemSectionCostCenter.Qty1NetTotal = oItemSectionCostCenter.Qty1Charge +
                                                              oItemSectionCostCenter.Qty1MarkUpValue;
                        oItemSectionCostCenter.Qty1EstimatedTime = PressRunTime1 + oItemSectionCostCenter.SetupTime;
                        oItemSectionCostCenter.Qty1EstimatedPlantCost = dblPrintCost[0];

                        oItemSection.ImpressionQty1 = Convert.ToInt32(SectionQtyWithSpoilage[0]*dblPressPass);

                        if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsDefaultPressInstruction == true)
                        {
                            if (oJobCardOptionsDTO.IsWorkingSize == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions = "Working Size = " +
                                                                              oItemSection.SectionSizeHeight +
                                                                              itemsectionRepository.GetLengthUnitName(
                                                                                  (int)
                                                                                      CompanyGeneralSettings()
                                                                                          .SystemLengthUnit) + " x " +
                                                                              oItemSection.SectionSizeWidth +
                                                                              itemsectionRepository.GetLengthUnitName(
                                                                                  (int)
                                                                                      CompanyGeneralSettings()
                                                                                          .SystemLengthUnit) + " " +
                                                                              Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsItemSize == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += "Item Size (Flat) = " +
                                                                               oItemSection.ItemSizeHeight +
                                                                               itemsectionRepository.GetLengthUnitName(
                                                                                   (int)
                                                                                       CompanyGeneralSettings()
                                                                                           .SystemLengthUnit) + " x " +
                                                                               oItemSection.ItemSizeWidth +
                                                                               itemsectionRepository.GetLengthUnitName(
                                                                                   (int)
                                                                                       CompanyGeneralSettings()
                                                                                           .SystemLengthUnit) + " " +
                                                                               Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsPrintSheetQty == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += "Print Sheet Qty = " +
                                                                               SectionQtyWithSpoilage[0];
                            }
                            if (oJobCardOptionsDTO.IsNumberOfPasses == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += " ;  Number of Passes = " +
                                                                               (PassesBack + PassesFront) +
                                                                               Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsImpressionCount == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += " Impression Count = " +
                                                                               oItemSection.ImpressionQty1 +
                                                                               Environment.NewLine;
                            }

                            if (oJobCardOptionsDTO.IsPressEstTime == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += " Estimated Time:= " +
                                                                               oItemSectionCostCenter.Qty1EstimatedTime
                                                                                   .ToString("f") + " hours";
                            }
                        }
                    }
                }


                if (oItemSection.Qty2 > 0)
                {
                    if (PressReRunMode == PressReRunModes.NotReRun)
                    {
                        oItemSectionCostCenter.Qty2Charge = dblPrintPrice[1];
                        oItemSectionCostCenter.Qty2MarkUpID = oCostCentreDTO.DefaultVAId;
                        oItemSectionCostCenter.Qty2MarkUpValue =
                            Convert.ToDouble(oItemSectionCostCenter.Qty2Charge*ProfitMargin/100);
                        oItemSectionCostCenter.Qty2NetTotal = oItemSectionCostCenter.Qty2Charge +
                                                              oItemSectionCostCenter.Qty2MarkUpValue;
                        oItemSectionCostCenter.Qty2EstimatedTime = PressRunTime2 + oItemSectionCostCenter.SetupTime;
                        oItemSectionCostCenter.Qty2EstimatedPlantCost = dblPrintCost[1];

                        oItemSection.ImpressionQty2 = Convert.ToInt32(SectionQtyWithSpoilage[1]*dblPressPass);


                        if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsDefaultPressInstruction == true)
                        {
                            if (oJobCardOptionsDTO.IsWorkingSize == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions = "Working Size = " +
                                                                              oItemSection.SectionSizeHeight +
                                                                              itemsectionRepository.GetLengthUnitName(
                                                                                  (int)
                                                                                      CompanyGeneralSettings()
                                                                                          .SystemLengthUnit) + " x " +
                                                                              oItemSection.SectionSizeWidth +
                                                                              itemsectionRepository.GetLengthUnitName(
                                                                                  (int)
                                                                                      CompanyGeneralSettings()
                                                                                          .SystemLengthUnit) + " " +
                                                                              Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsItemSize == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += "Item Size (Flat) = " +
                                                                               oItemSection.ItemSizeHeight +
                                                                               itemsectionRepository.GetLengthUnitName(
                                                                                   (int)
                                                                                       CompanyGeneralSettings()
                                                                                           .SystemLengthUnit) + " x " +
                                                                               oItemSection.ItemSizeWidth +
                                                                               itemsectionRepository.GetLengthUnitName(
                                                                                   (int)
                                                                                       CompanyGeneralSettings()
                                                                                           .SystemLengthUnit) + " " +
                                                                               Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsPrintSheetQty == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += "Print Sheet Qty = " +
                                                                               SectionQtyWithSpoilage[1];
                            }
                            if (oJobCardOptionsDTO.IsNumberOfPasses == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += " ;  Number of Passes = " +
                                                                               (PassesBack + PassesFront) +
                                                                               Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsImpressionCount == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += " Impression Count = " +
                                                                               oItemSection.ImpressionQty2 +
                                                                               Environment.NewLine;
                            }

                            if (oJobCardOptionsDTO.IsPressEstTime == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += " Estimated Time = " +
                                                                               oItemSectionCostCenter.Qty2EstimatedTime
                                                                                   .ToString("f") + " hours";
                            }

                        }

                    }
                    else if (PressReRunMode == PressReRunModes.ReRunPress & PressReRunQuantityIndex == 2)
                    {
                        oItemSectionCostCenter.Qty2Charge = dblPrintPrice[1];
                        oItemSectionCostCenter.Qty2MarkUpID = oCostCentreDTO.DefaultVAId;
                        oItemSectionCostCenter.Qty2MarkUpValue =
                            Convert.ToDouble(oItemSectionCostCenter.Qty2Charge*ProfitMargin/100);
                        oItemSectionCostCenter.Qty2NetTotal = oItemSectionCostCenter.Qty2Charge +
                                                              oItemSectionCostCenter.Qty2MarkUpValue;
                        oItemSectionCostCenter.Qty2EstimatedTime = PressRunTime2 + oItemSectionCostCenter.SetupTime;
                        oItemSectionCostCenter.Qty2EstimatedPlantCost = dblPrintCost[1];

                        oItemSection.ImpressionQty2 = Convert.ToInt32(SectionQtyWithSpoilage[1]*dblPressPass);


                        if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsDefaultPressInstruction == true)
                        {
                            if (oJobCardOptionsDTO.IsWorkingSize == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions = "Working Size = " +
                                                                              oItemSection.SectionSizeHeight +
                                                                              itemsectionRepository.GetLengthUnitName(
                                                                                  (int)
                                                                                      CompanyGeneralSettings()
                                                                                          .SystemLengthUnit) + " x " +
                                                                              oItemSection.SectionSizeWidth +
                                                                              itemsectionRepository.GetLengthUnitName(
                                                                                  (int)
                                                                                      CompanyGeneralSettings()
                                                                                          .SystemLengthUnit) + " " +
                                                                              Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsItemSize == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += "Item Size (Flat) = " +
                                                                               oItemSection.ItemSizeHeight +
                                                                               itemsectionRepository.GetLengthUnitName(
                                                                                   (int)
                                                                                       CompanyGeneralSettings()
                                                                                           .SystemLengthUnit) + " x " +
                                                                               oItemSection.ItemSizeWidth +
                                                                               itemsectionRepository.GetLengthUnitName(
                                                                                   (int)
                                                                                       CompanyGeneralSettings()
                                                                                           .SystemLengthUnit) + " " +
                                                                               Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsPrintSheetQty == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += "Print Sheet Qty = " +
                                                                               SectionQtyWithSpoilage[1];
                            }
                            if (oJobCardOptionsDTO.IsNumberOfPasses == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += " ;  Number of Passes = " +
                                                                               (PassesBack + PassesFront) +
                                                                               Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsImpressionCount == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += " Impression Count = " +
                                                                               oItemSection.ImpressionQty2 +
                                                                               Environment.NewLine;
                            }

                            if (oJobCardOptionsDTO.IsPressEstTime == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += " Estimated Time = " +
                                                                               oItemSectionCostCenter.Qty2EstimatedTime
                                                                                   .ToString("f") + " hours";
                            }

                        }
                    }
                }

                if (oItemSection.Qty3 > 0)
                {
                    if (PressReRunMode == PressReRunModes.NotReRun)
                    {
                        oItemSectionCostCenter.Qty3Charge = dblPrintPrice[2];
                        oItemSectionCostCenter.Qty3MarkUpID = oCostCentreDTO.DefaultVAId;
                        oItemSectionCostCenter.Qty3MarkUpValue = oItemSectionCostCenter.Qty2Charge*ProfitMargin/100;
                        oItemSectionCostCenter.Qty3NetTotal = oItemSectionCostCenter.Qty3Charge +
                                                              oItemSectionCostCenter.Qty3MarkUpValue;
                        oItemSectionCostCenter.Qty3EstimatedTime = PressRunTime3 + oItemSectionCostCenter.SetupTime;
                        oItemSectionCostCenter.Qty3EstimatedPlantCost = dblPrintCost[2];

                        oItemSection.ImpressionQty3 = Convert.ToInt32(SectionQtyWithSpoilage[2]*dblPressPass);

                        if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsDefaultPressInstruction == true)
                        {
                            if (oJobCardOptionsDTO.IsWorkingSize == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions = "Working Size = " +
                                                                              oItemSection.SectionSizeHeight +
                                                                              itemsectionRepository.GetLengthUnitName(
                                                                                  (int)
                                                                                      CompanyGeneralSettings()
                                                                                          .SystemLengthUnit) + " x " +
                                                                              oItemSection.SectionSizeWidth +
                                                                              itemsectionRepository.GetLengthUnitName(
                                                                                  (int)
                                                                                      CompanyGeneralSettings()
                                                                                          .SystemLengthUnit) + " " +
                                                                              Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsItemSize == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += "Item Size (Flat) = " +
                                                                               oItemSection.ItemSizeHeight +
                                                                               itemsectionRepository.GetLengthUnitName(
                                                                                   (int)
                                                                                       CompanyGeneralSettings()
                                                                                           .SystemLengthUnit) + " x " +
                                                                               oItemSection.ItemSizeWidth +
                                                                               itemsectionRepository.GetLengthUnitName(
                                                                                   (int)
                                                                                       CompanyGeneralSettings()
                                                                                           .SystemLengthUnit) + " " +
                                                                               Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsPrintSheetQty == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += "Print Sheet Qty = " +
                                                                               SectionQtyWithSpoilage[2];
                            }
                            if (oJobCardOptionsDTO.IsNumberOfPasses == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += " ;  Number of Passes = " +
                                                                               (PassesBack + PassesFront) +
                                                                               Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsImpressionCount == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += " Impression Count = " +
                                                                               oItemSection.ImpressionQty3 +
                                                                               Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsPressEstTime == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += " Estimated Time = " +
                                                                               oItemSectionCostCenter.Qty3EstimatedTime
                                                                                   .ToString("f") + " hours";
                            }

                        }

                    }
                    else if (PressReRunMode == PressReRunModes.ReRunPress & PressReRunQuantityIndex == 3)
                    {
                        oItemSectionCostCenter.Qty3Charge = dblPrintPrice[2];
                        oItemSectionCostCenter.Qty3MarkUpID = oCostCentreDTO.DefaultVAId;
                        oItemSectionCostCenter.Qty3MarkUpValue = oItemSectionCostCenter.Qty2Charge*ProfitMargin/100;
                        oItemSectionCostCenter.Qty3NetTotal = oItemSectionCostCenter.Qty3Charge +
                                                              oItemSectionCostCenter.Qty3MarkUpValue;
                        oItemSectionCostCenter.Qty3EstimatedTime = PressRunTime3 + oItemSectionCostCenter.SetupTime;
                        oItemSectionCostCenter.Qty3EstimatedPlantCost = dblPrintCost[2];

                        oItemSection.ImpressionQty3 = Convert.ToInt32(SectionQtyWithSpoilage[2]*dblPressPass);

                        if (IsWorkInstructionsLocked == false & oJobCardOptionsDTO.IsDefaultPressInstruction == true)
                        {
                            if (oJobCardOptionsDTO.IsWorkingSize == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions = "Working Size = " +
                                                                              oItemSection.SectionSizeHeight +
                                                                              itemsectionRepository.GetLengthUnitName(
                                                                                  (int)
                                                                                      CompanyGeneralSettings()
                                                                                          .SystemLengthUnit) + " x " +
                                                                              oItemSection.SectionSizeWidth +
                                                                              itemsectionRepository.GetLengthUnitName(
                                                                                  (int)
                                                                                      CompanyGeneralSettings()
                                                                                          .SystemLengthUnit) + " " +
                                                                              Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsItemSize == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += "Item Size (Flat) = " +
                                                                               oItemSection.ItemSizeHeight +
                                                                               itemsectionRepository.GetLengthUnitName(
                                                                                   (int)
                                                                                       CompanyGeneralSettings()
                                                                                           .SystemLengthUnit) + " x " +
                                                                               oItemSection.ItemSizeWidth +
                                                                               itemsectionRepository.GetLengthUnitName(
                                                                                   (int)
                                                                                       CompanyGeneralSettings()
                                                                                           .SystemLengthUnit) + " " +
                                                                               Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsPrintSheetQty == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += "Print Sheet Qty = " +
                                                                               SectionQtyWithSpoilage[2];
                            }
                            if (oJobCardOptionsDTO.IsNumberOfPasses == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += " ;  Number of Passes = " +
                                                                               dblPressPass +
                                                                               Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsImpressionCount == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += " Impression Count = " +
                                                                               oItemSection.ImpressionQty3 +
                                                                               Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsPressEstTime == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += " Estimated Time = " +
                                                                               oItemSectionCostCenter.Qty3EstimatedTime
                                                                                   .ToString("f") + " hours";
                            }

                        }

                    }
                }


                if (sMinimumCost != string.Empty)
                {
                    oItemSectionCostCenter.IsMinimumCost = Convert.ToInt16(sMinimumCost);
                }
                else
                {
                    oItemSectionCostCenter.IsMinimumCost = 0;
                }


                //Setting up the QTY in the object
                SectionCostCentreDetail oItemSectionCostCenterDetail = new SectionCostCentreDetail();

                oItemSectionCostCenterDetail.StockId = oItemSection.StockItemID1;
                oItemSectionCostCenterDetail.CostPrice = dblPrintCost[0];

                oItemSectionCostCenter.Name = "Press ( " + oPressDTO.MachineName + " )";

                oItemSectionCostCenter.Qty1 = oItemSection.Qty1;
                oItemSectionCostCenter.Qty2 = oItemSection.Qty2;
                oItemSectionCostCenter.Qty3 = oItemSection.Qty3;

                if (IsReRun == false)
                {
                    if (oItemSectionCostCenter.SectionCostCentreDetails == null)
                    {
                        oItemSectionCostCenter.SectionCostCentreDetails = new List<SectionCostCentreDetail>();
                    }
                    oItemSectionCostCenter.SectionCostCentreDetails.Add(oItemSectionCostCenterDetail);
                    if (oItemSection.SectionCostcentres == null)
                    {
                        oItemSection.SectionCostcentres = new List<SectionCostcentre>();
                    }
                    oItemSection.SectionCostcentres.Add(oItemSectionCostCenter);
                }


            }
            catch (Exception ex)
            {
                throw new MPCException(ex.Message, itemsectionRepository.OrganisationId);
            }
            return oItemSection;
        }


    }
}
