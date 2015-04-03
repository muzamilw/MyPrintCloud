using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System.Linq;
using MPC.Models.Common;
using System;
using System.Collections.Generic;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Item Section Repository
    /// </summary>
    public class ItemSectionRepository : BaseRepository<ItemSection>, IItemSectionRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemSectionRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ItemSection> DbSet
        {
            get
            {
                return db.ItemSections;
            }
        }

        #endregion

        #region public
        /// <summary>
        /// CalculatePressCost
        /// </summary>
        /// <param name="oItemSection"></param>
        /// <param name="PressID"></param>
        /// <param name="IsReRun"></param>
        /// <param name="IsWorkInstructionsLocked"></param>
        /// <param name="PressReRunMode"></param>
        /// <param name="PressReRunQuantityIndex"></param>
        /// <param name="OverrideValue"></param>
        /// <param name="isBestPress"></param>
        /// <returns></returns>
        public ItemSection CalculatePressCost(ItemSection oItemSection, int PressID, bool IsReRun = false, bool IsWorkInstructionsLocked = false, int PressReRunMode = (int)PressReRunModes.NotReRun, int PressReRunQuantityIndex = 1, double OverrideValue = 0, bool isBestPress = false)
        {

            oItemSection.SectionCostcentres.ToList().ForEach(c => oItemSection.SectionCostcentres.Remove(c));
            tbl_job_preferences oJobCardOptionsDTO = this.GetJobPreferences(1);
            bool functionReturnValue = false;
            string sMinimumCost = null;
            double dblPassFront = 0;
            double dblPassBack = 0;
            string strPrintLookup = null;
            double dblPlateQty = 0;
            SectionCostCentreResource oResourceDto = new SectionCostCentreResource();
            Machine oPressDTO = db.Machines.Where(m => m.MachineId == PressID).FirstOrDefault();
            int SheetPTV = 0;
            int SetupSpoilage = 0;
            double RunningSpoilagePercentage = 0;
            CostCentre oCostCentreDTO = db.CostCentres.Where(c => c.SystemTypeId == (int)SystemCostCenterTypes.Press && c.SystemSiteID == 1 && c.OrganisationId == this.OrganisationId).FirstOrDefault();

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

                ///'''=======================================================================
                StockItem oPaperDTO = new StockItem();
                oPaperDTO = db.StockItems.Where(s => s.StockItemId == oItemSection.StockItemID1).FirstOrDefault();

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

                oItemSection.SelectedPressCalculationMethodId = Convert.ToInt32(oPressDTO.LookupMethodId);


                oModelLookUpMethod = db.LookupMethods.Where(m => m.MethodId == oItemSection.SelectedPressCalculationMethodId).FirstOrDefault();

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

                        //Getting the Values for Click Charge LookUp
                        //Model.LookupMethods.ClickChargeDTO oModelClickChargeLookUp = oModelLookUpMethod.ClickCharge;

                        MachineClickChargeLookup oModelClickChargeLookUp = db.MachineClickChargeLookups.Where(l => l.MethodId == oModelLookUpMethod.MethodId).FirstOrDefault();
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
                        //Getting the Plant Lookup Information
                        // Model.LookupMethods.SpeedWeightDTO oModelSpeedWeight = oModelLookUpMethod.SpeedWeight;

                        MachineSpeedWeightLookup oModelSpeedWeight = db.MachineSpeedWeightLookups.Where(l => l.MethodId == oModelLookUpMethod.MethodId).FirstOrDefault();
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
                            if (Convert.ToBoolean(oItemSection.IsDoubleSided) && !Convert.ToBoolean((oPressDTO.isPerfecting)) ?? false)
                            {
                                //Calculating and Setting Print Cost
                                dblPrintCost[i] = Convert.ToDouble((dblPassFront * ((intWorkSheetQty[i] / dblPrintSpeed[i]) * oModelSpeedWeight.hourlyCost)) + ((dblPassBack * ((intWorkSheetQty[i] / dblPrintSpeed[i]) * oModelSpeedWeight.hourlyCost)) + oPressDTO.SetupCharge));

                                dblPrintPrice[i] = Convert.ToDouble((dblPassFront * ((intWorkSheetQty[i] / dblPrintSpeed[i]) * oModelSpeedWeight.hourlyCost)) + ((dblPassBack * ((intWorkSheetQty[i] / dblPrintSpeed[i]) * oModelSpeedWeight.hourlyPrice)) + oPressDTO.SetupCharge));

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
                        //Getting Press lookup Information
                        
                        MachinePerHourLookup oModelPerHourLookUp = db.MachinePerHourLookups.Where(p => p.MethodId == oModelLookUpMethod.MethodId).FirstOrDefault();
                        //Model.LookupMethods.PerHourDTO oModelPerHourLookUp = oModelLookUpMethod.PerHour;
                        //Setting Print Charge

                        if (PressReRunMode == (int)PressReRunModes.CalculateValuesToShow)
                        {
                            intPrintChgeph =  Convert.ToInt32(oModelPerHourLookUp.Speed);
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

                        //Getting the Values for the LookUp
                        
                        MachineClickChargeZone oModelClickChargeZone = db.MachineClickChargeZones.Where(z => z.MethodId == oModelLookUpMethod.MethodId).FirstOrDefault();
                        double TimePerChargeableSheets = Convert.ToDouble(oModelClickChargeZone.TimePerHour?? 1) ;

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
                    oItemSectionCostCenter.ItemSectionID = oItemSection.ItemSectionID;
                    oItemSectionCostCenter.CostCentreID = (int)oCostCentreDTO.CostCentreID;
                    oItemSectionCostCenter.SystemCostCentreType = (int)SystemCostCenterTypes.Press;
                    oItemSectionCostCenter.Order = 107;
                    oItemSectionCostCenter.IsOptionalExtra = 0;
                    oItemSectionCostCenter.CostCentreType = 1;
                    oItemSectionCostCenter.IsDirectCost = Convert.ToInt16(oPressDTO.DirectCost);
                }
                else
                {
                    //
                }

                oItemSectionCostCenter.SetupTime = Convert.ToDouble((oPressDTO.SetupTime == 0 ? 0 : oPressDTO.SetupTime / 60));
                oItemSectionCostCenter.IsPrintable = Convert.ToInt16(oJobCardOptionsDTO.IsDefaultPressInstruction);
                double ProfitMargin = 0;
                var markup = db.Markups.Where(m => m.MarkUpId == oCostCentreDTO.DefaultVAId).FirstOrDefault();
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
                                oItemSectionCostCenter.Qty1WorkInstructions = "Working Size:=" + oItemSection.SectionSizeHeight + GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.SectionSizeWidth + GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsItemSize == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += "Item Size (Flat):= " + oItemSection.ItemSizeHeight + GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.ItemSizeWidth + GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
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
                                oItemSectionCostCenter.Qty1WorkInstructions = "Working Size:=" + oItemSection.SectionSizeHeight + GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.SectionSizeWidth + GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsItemSize == true)
                            {
                                oItemSectionCostCenter.Qty1WorkInstructions += "Item Size (Flat):= " + oItemSection.ItemSizeHeight + GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.ItemSizeWidth + GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
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
                                oItemSectionCostCenter.Qty2WorkInstructions = "Working Size:=" + oItemSection.SectionSizeHeight + GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.SectionSizeWidth + GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsItemSize == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += "Item Size (Flat):= " + oItemSection.ItemSizeHeight + GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.ItemSizeWidth + GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
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
                                oItemSectionCostCenter.Qty2WorkInstructions = "Working Size:=" + oItemSection.SectionSizeHeight + GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.SectionSizeWidth + GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsItemSize == true)
                            {
                                oItemSectionCostCenter.Qty2WorkInstructions += "Item Size (Flat):= " + oItemSection.ItemSizeHeight + GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.ItemSizeWidth + GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
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
                                oItemSectionCostCenter.Qty3WorkInstructions = "Working Size:=" + oItemSection.SectionSizeHeight + GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.SectionSizeWidth + GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsItemSize == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += "Item Size (Flat):= " + oItemSection.ItemSizeHeight + GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.ItemSizeWidth + GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
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
                                oItemSectionCostCenter.Qty3WorkInstructions = "Working Size:=" + oItemSection.SectionSizeHeight + GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.SectionSizeWidth + GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
                            }
                            if (oJobCardOptionsDTO.IsItemSize == true)
                            {
                                oItemSectionCostCenter.Qty3WorkInstructions += "Item Size (Flat):= " + oItemSection.ItemSizeHeight + GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " x " + oItemSection.ItemSizeWidth + GetLengthUnitName((int)CompanyGeneralSettings().SystemLengthUnit) + " " + Environment.NewLine;
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
                oItemSectionCostCenter.SectionCostCentreResources.ToList().ForEach(c => db.SectionCostCentreResources.Remove(c));
                foreach (var orow in oItemSectionCostCenter.SectionCostCentreResources)
                {
                    oResourceDto = new SectionCostCentreResource { ResourceId = orow.ResourceId, SectionCostcentreId = oItemSectionCostCenter.SectionCostcentreId };
                    oItemSectionCostCenter.SectionCostCentreResources.Add(oResourceDto);
                }
                if (IsReRun == false)
                {
                    oItemSectionCostCenter.SectionCostCentreDetails.Add(oItemSectionCostCenterDetail);
                    oItemSection.SectionCostcentres.Add(oItemSectionCostCenter);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("CalculatePressCost", ex);
            }

            return oItemSection;//.tbl_section_costcentres.ToList();
        }


        #region Estimating Functions
        public GlobalData GetItemPriceCost(int StockItemID)
        {
            GlobalData gData = new GlobalData();

            double Cost = 0;
            double Price = 0;
            double PackCost = 0;
            double PackPrice = 0;
            int ReturnColumnIndex = 0;
            DateTime currentDate = DateTime.Now;

            List<StockCostAndPrice> InkCostTable = stockCostnPriceRepository.GetAll().Where(s => s.ItemId == StockItemID && s.CostOrPriceIdentifier == 0).ToList();
            List<StockCostAndPrice> InkPriceTable = stockCostnPriceRepository.GetAll().Where(s => s.ItemId == StockItemID && s.CostOrPriceIdentifier == 1).ToList();
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
            List<StockCostAndPrice> InkCostTable = stockCostnPriceRepository.GetAll().Where(s => s.ItemId == StockItemID && s.CostOrPriceIdentifier == 0).ToList();
            List<StockCostAndPrice> InkPriceTable = stockCostnPriceRepository.GetAll().Where(s => s.ItemId == StockItemID && s.CostOrPriceIdentifier == -1).ToList();

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

        private tbl_job_preferences GetJobPreferences(int SystemSiteID)
        {
            return this.ObjectContext.tbl_job_preferences.Where(g => g.SystemSiteID == SystemSiteID).Single();
        }
        private int GetSystemCostCentreID(SystemCostCenterTypes SystemType)
        {
            return costcentreRepository.GetAll().Where(g => g.SystemTypeId == (int)SystemType).Select(g => g.CostCentreId).Single();
        }

        public ItemSection CalculatePlateCost(ItemSection oItemSection, bool IsReRun = false, bool IsWorkInstructionsLocked = false)
        {

            oItemSection.SectionCostcentres.ToList().ForEach(c => oItemSection.SectionCostcentres.Remove(c));

            tbl_job_preferences oJobCardOptionsDTO = this.GetJobPreferences(1);
            bool IsSectionCostCentreFoundInReRun = false;
            string sMinimumCost = null;
            SectionCostCentreResource oResourceDto;

            double dblPlateUnitCost = 0;
            double dblPlateUnitPrice = 0;
            double dblPlateCost = 0;
            double dblPlatePrice = 0;
            double dblPlateProcessingtCost = 0;
            double dblPlateProcessingPrice = 0;

            StockItem oPlateDTO = stockItemRepository.GetAll().Where(s => s.StockItemId == oItemSection.PlateId).FirstOrDefault();
            CostCentre oPlateCostCentreDTO = costcentreRepository.GetAll().Where(c => c.SystemTypeId == (int)SystemCostCenterTypes.Plate).FirstOrDefault();
            double dblItemProcessingCharge = 0;
            int PlatesQty = (oItemSection.Side1PlateQty > 0 ? (int)oItemSection.Side1PlateQty : 0) + (oItemSection.Side2PlateQty > 0 ? (int)oItemSection.Side2PlateQty : 0);
            if (oItemSection.IsPlateUsed != false && oItemSection.IsPlateSupplied == false)
            {
                int PlateID = (int)oItemSection.PlateID;
                GlobalData gData = GetItemPriceCost(PlateID, true);
                if (gData != null)
                {
                    dblPlateUnitCost = gData.dblUnitCost;
                    dblPlateUnitPrice = gData.dblUnitPrice;
                    dblPlateProcessingtCost = gData.dblPlateProcessingtCost;
                    dblPlateProcessingPrice = gData.dblPlateProcessingPrice;
                }

                dblPlateUnitCost = oPlateDTO.PerQtyQty > 0 ? dblPlateUnitCost / (double)oPlateDTO.PerQtyQty : 0;
                dblPlateUnitPrice = oPlateDTO.PerQtyQty > 0 ? dblPlateUnitPrice / (double)oPlateDTO.PerQtyQty : 0;

                dblItemProcessingCharge = oPlateDTO.ItemProcessingCharge > 0 ? (double)oPlateDTO.ItemProcessingCharge : 0;

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
                oItemSectionCostCenter.CostCentreId = GetSystemCostCentreID(SystemCostCenterTypes.Plate);  //13 is Plate Type cost center
                oItemSectionCostCenter.SystemCostCentreType = (int)SystemCostCenterTypes.Plate; // Cost Center system type 14;
                oItemSectionCostCenter.Order = 104;
                oItemSectionCostCenter.IsOptionalExtra = 0;
                oItemSectionCostCenter.CostCentreType = 1;
                oItemSectionCostCenter.IsDirectCost = (short)oPlateCostCentreDTO.IsDirectCost;
                oItemSectionCostCenter.IsPrintable = oJobCardOptionsDTO.IsdefaultPlateUsed != null ? oJobCardOptionsDTO.IsdefaultPlateUsed == true ? (short)1 : (short)0 : (short)0;
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
                    oItemSectionCostCenter.CostCentreId = 13;
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
            var markup = _markupRepository.GetAll().Where(m => m.MarkUpId == oPlateCostCentreDTO.DefaultVAId).FirstOrDefault();
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


            oItemSectionCostCenterDetail.SupplierId = oPlateDTO.SupplierId;

            oItemSectionCostCenter.Name = "Plate ( " + oPlateDTO.ItemName + " )";


            //Section CostCentre Resource repository is to add and update below line
            oItemSectionCostCenter.SectionCostCentreResources.ToList().ForEach(c => ObjectContext.tbl_section_costcentre_resources.DeleteObject(c));
            //adding new resources.
            foreach (var orow in oItemSectionCostCenter.SectionCostCentreResources)
            {
                oResourceDto = new SectionCostCentreResource { ResourceId = orow.ResourceId, SectionCostcentreId = oItemSectionCostCenter.SectionCostcentreId };
                oItemSectionCostCenter.SectionCostCentreResources.Add(oResourceDto);
            }
            if (IsReRun == false || IsSectionCostCentreFoundInReRun == false)
            {
                oItemSectionCostCenter.SectionCostCentreDetails.Add(oItemSectionCostCenterDetail);
                oItemSection.SectionCostcentres.Add(oItemSectionCostCenter);
            }

            return oItemSection;
        }



        public List<BestPress> GetBestPresses(ItemSection currentSection)
        {
            List<BestPress> bestpress = new List<BestPress>();
            List<Machine> EnablePresses = machineRepository.GetAll().Where(m => m.MachineCatId != (int)MachineCategories.Guillotin && m.minimumsheetheight <= currentSection.SectionSizeHeight && m.minimumsheetwidth <= currentSection.SectionSizeWidth && m.OrganisationId == machineRepository.OrganisationId && (m.IsDisabled != true)).ToList();
            CostCentre oPressCostCentre = costcentreRepository.GetAll().Where(c => c.SystemTypeId == (int)SystemCostCenterTypes.Press && c.SystemSiteId == 1 && c.OrganisationId == costcentreRepository.OrganisationId).FirstOrDefault();

            foreach (var press in EnablePresses)
            {

                currentSection.PressId = press.MachineId;
                List<MachineSpoilage> machineSpoilageList = machineRepository.GetMachineSpoilageItems(press.MachineId);
                if (press.isplateused == true)
                {
                    currentSection.IsPlateUsed = true;
                    currentSection.PlateID = press.DefaultPlateid;
                }
                else
                    currentSection.IsPlateUsed = false;
                currentSection.IsWashup = press.iswashupused == true ? true : false;
                currentSection.IsMakeReadyUsed = press.ismakereadyused == true ? true : false;


                if (machineSpoilageList != null && machineSpoilageList.Count > 0)
                {
                    int InkColors = currentSection.Side1Inks + currentSection.Side2Inks;
                    var maxPressColor = machineSpoilageList.Max(m => m.NoOfColors);
                    if (maxPressColor != null && InkColors > maxPressColor)
                        InkColors = (int)maxPressColor;

                    var setupspoilage = machineSpoilageList.Where(m => m.NoOfColors == InkColors).FirstOrDefault();
                    if (setupspoilage != null)
                    {
                        currentSection.SetupSpoilage = setupspoilage.SetupSpoilage;
                        currentSection.RunningSpoilage = Convert.ToInt16(setupspoilage.RunningSpoilage);

                    }

                }

                ItemSection updateSection = CalculatePressCost(currentSection, press.MachineId, false, false, (int)PressReRunModes.NotReRun, 1, 0, true);
                SectionCostcentre presscc = updateSection.SectionCostcentres.Where(c => c.CostCentreId == oPressCostCentre.CostCentreId).FirstOrDefault();
                if (presscc != null)
                {
                    bestpress.Add(new BestPress { MachineID = press.MachineId, MachineName = press.MachineName, Qty1Cost = presscc.Qty1NetTotal ?? 0, Qty1RunTime = presscc.Qty1EstimatedTime, Qty2Cost = presscc.Qty2NetTotal ?? 0, Qty2RunTime = presscc.Qty2EstimatedTime, Qty3Cost = presscc.Qty3NetTotal ?? 0, Qty3RunTime = presscc.Qty3EstimatedTime });
                }
            }
            return bestpress.OrderBy(p => p.Qty1Cost).ToList();

        }
        #endregion


        #endregion

        
    }
}
