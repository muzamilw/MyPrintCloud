using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Implementation.WebStoreServices
{
    [Serializable()]
    public class ExecutionService
    {
        public double ExecuteResource(ref object[] oParamsArray, long ResourceID, string ReturnValue)
        {
            double functionReturnValue = 0;

            try
            {
                CostCentreExecutionMode ExecutionMode = (CostCentreExecutionMode)oParamsArray[0];

                // If execution mode is for populating the Queue then return 0
                if (ExecutionMode == CostCentreExecutionMode.PromptMode)
                {
                    return 0;

                    //if its execution mode then

                }
                else if (ExecutionMode == CostCentreExecutionMode.ExecuteMode)
                {
                    if (ReturnValue == "costperhour")
                    {
                        MPC.Repository.Repositories.CostCentreExecution obj = new MPC.Repository.Repositories.CostCentreExecution();

                        functionReturnValue = obj.ExecuteUserResource(ResourceID, ResourceReturnType.CostPerHour, Convert.ToString(oParamsArray[10]));
                        obj = null;
                    }
                    else
                    {
                        functionReturnValue = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ExecuteResource", ex);
            }
            return functionReturnValue;
        }

        public double ExecuteQuestion(ref object[] oParamsArray, int QuestionID, long CostCentreID)
        {

            CostCentreExecutionMode ExecutionMode = (CostCentreExecutionMode)oParamsArray[1];
            // ItemSection oItemSection = (ItemSection)oParamsArray[8];
            int CurrentQuantity = Convert.ToInt32(oParamsArray[5]);
            int MultipleQutantities = Convert.ToInt32(oParamsArray[4]);
            List<QuestionQueueItem> QuestionQueue = oParamsArray[2] as List<QuestionQueueItem>;



            bool bFlag = false;
            QuestionQueueItem QuestionItem = null;
            try
            {
                //check if its the visual mode or Execution Mode
                if (ExecutionMode == CostCentreExecutionMode.ExecuteMode)
                {
                    //here the questions returned asnwer will ahave been loaded in the queue
                    //retreive the queue answer for this question and use.. :D
                    //use is simple only cast it in double and return..



                    foreach (QuestionQueueItem item in QuestionQueue)
                    {
                        //matching
                        if (item.ID == QuestionID & item.CostCentreID == CostCentreID)
                        {
                            bFlag = true;
                            QuestionItem = item;
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }

                    //if found question in queue then use its values
                    if (bFlag == true)
                    {
                        return Convert.ToDouble(QuestionItem.Qty1Answer);
                        //Return CDbl(item.Answer)
                        //if MultipleQutantities
                        //new multiple qty logic goes here

                        //if (CurrentQuantity <= MultipleQutantities)
                        //{
                        //    switch (CurrentQuantity)
                        //    {
                        //        case 1:
                        //            return Convert.ToDouble(QuestionItem.Qty1Answer);
                        //        case 2:
                        //            if (QuestionItem.Qty2Answer == 0)
                        //            {
                        //                return Convert.ToDouble(QuestionItem.Qty1Answer);
                        //            }
                        //            else
                        //            {
                        //                return Convert.ToDouble(QuestionItem.Qty2Answer);
                        //            }
                        //            break;
                        //        case 3:
                        //            if (Convert.ToDouble(QuestionItem.Qty3Answer) == 0)
                        //            {
                        //                return Convert.ToDouble(QuestionItem.Qty1Answer);
                        //            }
                        //            else
                        //            {
                        //                return Convert.ToDouble(QuestionItem.Qty3Answer);
                        //            }

                        //            break;
                        //    }
                        //}
                        //else
                        //{
                        //    throw new Exception("Invalid  Current Selected Multiple Quantitity");
                        //}
                    }
                    else
                    {
                        throw new Exception("Answer not find in Queue");
                    }

                }
                else if (ExecutionMode == CostCentreExecutionMode.PromptMode)
                {
                    //populate the question in the executionQueue
                    //loading the Questions Information for populating in the Queue
                    MPC.Repository.Repositories.CostCentreExecution obj = new MPC.Repository.Repositories.CostCentreExecution();
                    CostCentreQuestion ovariable = obj.LoadQuestion(Convert.ToInt32(QuestionID), Convert.ToString(oParamsArray[10]));

                    QuestionItem = new QuestionQueueItem(QuestionID, ovariable.QuestionString, CostCentreID, ovariable.Type == null ? (short)0 : ovariable.Type.Value, ovariable.QuestionString, ovariable.DefaultAnswer, "", false, 0, 0, 0, 0, 0, 0, 0, ovariable.AnswerCollection);

                    if (QuestionQueue != null)
                    {
                        QuestionQueue.Add(QuestionItem);
                    }
                    ovariable = null;

                    //exit normally 
                }
                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception("ExecuteQuestion", ex);
            }

        }


        public double ExecuteMatrix(ref object[] oParamsArray, int MatrixID, long CostCentreID)
        {
            bool bFlag = false;


            CostCentreExecutionMode ExecutionMode = (CostCentreExecutionMode)oParamsArray[1];
            ItemSection oItemSection = (ItemSection)oParamsArray[8];
            int CurrentQuantity = Convert.ToInt32(oParamsArray[5]);
            int MultipleQutantities = Convert.ToInt32(oParamsArray[4]);
            List<QuestionQueueItem> QuestionQueue = oParamsArray[2] as List<QuestionQueueItem>;
            QuestionQueueItem QuestionItem = null;

            try
            {
                //check if its the visual mode or Execution Mode
                if (ExecutionMode == CostCentreExecutionMode.ExecuteMode)
                {
                    //here the questions returned asnwer will ahave been loaded in the queue
                    //retreive the queue answer for this question and use.. :D
                    //use is simple only cast it in double and return..


                    foreach (var item in QuestionQueue)
                    {
                        //matching
                        if (item.ID == MatrixID & item.ItemType == 4)
                        {
                            bFlag = true;
                            QuestionItem = item;
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }

                    //if found question in queue then use its values
                    if (bFlag == true)
                    {
                        return Convert.ToDouble(QuestionItem.Qty1Answer);
                        //Return CDbl(item.Answer)
                        //multiple qty logic goes here

                        //if (CurrentQuantity <= MultipleQutantities)
                        //{
                        //    switch (CurrentQuantity)
                        //    {
                        //        case 1:
                        //            return Convert.ToDouble(QuestionItem.Qty1Answer);
                        //        case 2:
                        //            if (Convert.ToDouble(QuestionItem.Qty2Answer) == 0)
                        //            {
                        //                return Convert.ToDouble(QuestionItem.Qty1Answer);
                        //            }
                        //            else
                        //            {
                        //                return Convert.ToDouble(QuestionItem.Qty2Answer);
                        //            }
                        //            break;
                        //        case 3:
                        //            if (Convert.ToDouble(QuestionItem.Qty3Answer) == 0)
                        //            {
                        //                return Convert.ToDouble(QuestionItem.Qty1Answer);
                        //            }
                        //            else
                        //            {
                        //                return Convert.ToDouble(QuestionItem.Qty3Answer);
                        //            }
                        //            break;
                        //    }
                        //}
                        //else
                        //{
                        //    throw new Exception("Invalid  Current Selected Multiple Quantitity");
                        //}
                    }
                    else
                    {
                        throw new Exception("Answer not found in Queue");
                    }


                }
                else if (ExecutionMode == CostCentreExecutionMode.PromptMode)
                {
                    //populate the question in the executionQueue
                    //loading the Questions Information for populating in the Queue
                    MPC.Repository.Repositories.CostCentreExecution obj = new MPC.Repository.Repositories.CostCentreExecution();
                    CostCentreMatrix oMatrix = obj.GetMatrix(MatrixID, Convert.ToString(oParamsArray[10]));
                    QuestionItem = new QuestionQueueItem(MatrixID, oMatrix.Name, CostCentreID, 4, oMatrix.Description, "", "", false, 0, 0, 0, 0, 0, oMatrix.RowsCount, oMatrix.ColumnsCount, null, oMatrix.items);
                    QuestionQueue.Add(QuestionItem);
                    oMatrix = null;

                    //exit normally 
                }
                return 1;

            }
            catch (Exception ex)
            {
                throw new Exception("ExecuteMatrix", ex);
            }

        }

        public double ExecuteUserStockItem(int StockID, StockPriceType StockPriceType, out double Price, out double PerQtyQty)
        {
            try
            {
                MPC.Repository.Repositories.CostCentreExecution obj = new MPC.Repository.Repositories.CostCentreExecution();

                return obj.ExecuteUserStockItem(StockID, StockPriceType, "", out Price, out PerQtyQty);
            }
            catch (Exception ex)
            {
                throw new Exception("ExecuteUserStockItem", ex);
            }
        }

        public double ExecuteVariable(ref object[] oParamsArray, int VariableID)
        {
            double functionReturnValue = 0;
            MPC.Repository.Repositories.CostCentreExecution obj = new MPC.Repository.Repositories.CostCentreExecution();

            try
            {
                CostCentreExecutionMode ExecutionMode = (CostCentreExecutionMode)oParamsArray[1];
                ItemSection oItemSection = (ItemSection)oParamsArray[8];
                int CurrentQuantity = Convert.ToInt32(oParamsArray[5]);

                //if its Queue populating mode then return 0
                if (ExecutionMode == CostCentreExecutionMode.PromptMode)
                {
                    return 0;

                    //its porpper execution mode
                }
                else if (ExecutionMode == CostCentreExecutionMode.ExecuteMode)
                {

                    CostCentreVariable oVariable;
                    //First we have to fetch the Variable object which contains the information
                    oVariable = obj.LoadVariable(VariableID, Convert.ToString(oParamsArray[10]));

                    //now check the type of the variable.
                    //type 1 = system variable
                    //type 2 = Customized Variable
                    //type 3 = CostCentre Variable

                    // in this type the Criteria will be used that will be

                    if (oItemSection == null)
                    {
                        functionReturnValue = 0;
                        return functionReturnValue;
                    }

                    if (oVariable.Type == 1)
                    {
                        switch (oVariable.PropertyType)
                        {

                            case (int)VariableProperty.Side1Inks:
                                functionReturnValue = Convert.ToDouble(oItemSection.Side1Inks);
                                break;
                            case (int)VariableProperty.Side2Inks:
                                functionReturnValue = Convert.ToDouble(oItemSection.Side1Inks);

                                break;
                            case (int)VariableProperty.PrintSheetQty_ProRata:

                                //if (Convert.ToDouble(oParamsArray[11]) != 0)
                                //{
                                //    functionReturnValue = Convert.ToDouble(oParamsArray[11]);
                                //    return functionReturnValue;
                                //}

                                switch (CurrentQuantity)
                                {
                                    case 1:
                                        functionReturnValue = Convert.ToDouble(oItemSection.PrintSheetQty1);
                                        break;
                                    case 2:
                                        if (oItemSection.PrintSheetQty2 == 0)
                                        {
                                            functionReturnValue = Convert.ToDouble(oItemSection.PrintSheetQty1);
                                        }
                                        else
                                        {
                                            functionReturnValue = Convert.ToDouble(oItemSection.PrintSheetQty2);
                                        }
                                        break;
                                    case 3:
                                        if (oItemSection.PrintSheetQty3 == 0)
                                        {
                                            functionReturnValue = Convert.ToDouble(oItemSection.PrintSheetQty1);
                                        }
                                        else
                                        {
                                            functionReturnValue = Convert.ToDouble(oItemSection.PrintSheetQty3);
                                        }
                                        break;
                                }

                                break;
                            case (int)VariableProperty.PressSpeed_ProRata:

                                switch (CurrentQuantity)
                                {
                                    case 1:
                                        functionReturnValue = Convert.ToDouble(oItemSection.PressSpeed1);
                                        break;
                                    case 2:
                                        if (oItemSection.PressSpeed2 == 0)
                                        {
                                            functionReturnValue = Convert.ToDouble(oItemSection.PressSpeed1);
                                        }
                                        else
                                        {
                                            functionReturnValue = Convert.ToDouble(oItemSection.PressSpeed2);
                                        }
                                        break;
                                    case 3:
                                        if (oItemSection.PressSpeed3 == 0)
                                        {
                                            functionReturnValue = Convert.ToDouble(oItemSection.PressSpeed1);
                                        }
                                        else
                                        {
                                            functionReturnValue = Convert.ToDouble(oItemSection.PressSpeed3);
                                        }
                                        functionReturnValue = Convert.ToDouble(oItemSection.PressSpeed3);
                                        break;
                                    //Case 4
                                    //    ExecuteVariable = oItemSection.PressSpeed4
                                    //Case 5
                                    //    ExecuteVariable = oItemSection.PressSpeed5
                                }

                                break;
                            //case (int)VariableProperty.ColourHeads:
                            //    if ((oItemSection.Press != null)) //  ask sir nv to add referece in press to itemsection
                            //    {
                            //        functionReturnValue = oItemSection.Press.ColourHeads;
                            //    }
                            //    else
                            //    {
                            //        functionReturnValue = 0;
                            //    }

                            //    break;

                            case (int)VariableProperty.ImpressionQty_ProRata:
                                

                                switch (CurrentQuantity)
                                {

                                    case 1:
                                        functionReturnValue = Convert.ToDouble(oItemSection.ImpressionQty1);
                                        break;
                                    case 2:
                                        if (oItemSection.ImpressionQty2 == 0)
                                        {
                                            functionReturnValue = Convert.ToDouble(oItemSection.ImpressionQty1);
                                        }
                                        else
                                        {
                                            functionReturnValue = Convert.ToDouble(oItemSection.ImpressionQty2);
                                        }
                                        break;
                                    case 3:
                                        if (oItemSection.ImpressionQty3 == 0)
                                        {
                                            functionReturnValue = Convert.ToDouble(oItemSection.ImpressionQty1);
                                        }
                                        else
                                        {
                                            functionReturnValue = Convert.ToDouble(oItemSection.ImpressionQty3);
                                        }

                                        break;
                                    //Case 4
                                    //    ExecuteVariable = oItemSection.ImpressionQty4
                                    //Case 5
                                    //    ExecuteVariable = oItemSection.ImpressionQty5
                                }

                                break;
                            case (int)VariableProperty.PressHourlyCharge:
                                functionReturnValue = oItemSection.PressHourlyCharge ?? 0;

                                break;
                            //case (int)VariableProperty.MinInkDuctqty:
                            //    if (oItemSection.Press == null)
                            //    {
                            //        functionReturnValue = 0;
                            //    }
                            //    else
                            //    {
                            //        functionReturnValue = oItemSection.Press.MinInkDuctqty;

                            //    }

                            //    break;

                            //case (int)VariableProperty.MakeReadycharge:
                            //    if (oItemSection.Press == null)
                            //    {
                            //        functionReturnValue = 0;
                            //    }
                            //    else
                            //    {
                            //        functionReturnValue = oItemSection.Press.MakeReadyCost;
                            //    }
                            //    break;
                            case (int)VariableProperty.PrintChargeExMakeReady_ProRata:
                                switch (CurrentQuantity)
                                {
                                    case 1:
                                        functionReturnValue = Convert.ToDouble(oItemSection.ImpressionQty1);
                                        break;
                                    case 2:
                                        functionReturnValue = Convert.ToDouble(oItemSection.ImpressionQty2);
                                        break;
                                    case 3:
                                        functionReturnValue = Convert.ToDouble(oItemSection.ImpressionQty3);
                                        break;
                                    //Case 4
                                    //    ExecuteVariable = oItemSection.ImpressionQty4
                                    //Case 5
                                    //    ExecuteVariable = oItemSection.ImpressionQty5
                                }

                                break;
                            case (int)VariableProperty.PaperGsm:
                                functionReturnValue = oItemSection.PaperGsm ?? 0;

                                break;
                            case (int)VariableProperty.SetupSpoilage:
                                functionReturnValue = Convert.ToDouble(oItemSection.SetupSpoilage);

                                break;
                            case (int)VariableProperty.RunningSpoilage:
                                functionReturnValue = Convert.ToDouble(oItemSection.RunningSpoilage);

                                break;
                            case (int)VariableProperty.PaperPackPrice:
                                functionReturnValue = oItemSection.PaperPackPrice ?? 0;

                                break;
                            case (int)VariableProperty.AdditionalPlateUsed:
                                functionReturnValue = Convert.ToDouble(oItemSection.AdditionalPlateUsed);

                                break;
                            case (int)VariableProperty.AdditionalFilmUsed:
                                functionReturnValue = Convert.ToDouble(oItemSection.AdditionalFilmUsed);

                                break;
                            case (int)VariableProperty.ItemGutterHorizontal:
                                functionReturnValue = oItemSection.ItemGutterHorizontal ?? 0;

                                break;
                            case (int)VariableProperty.ItemGutterVertical:
                                functionReturnValue = oItemSection.ItemGutterVertical ?? 0;

                                break;
                            case (int)VariableProperty.PTVRows:
                                functionReturnValue = Convert.ToDouble(oItemSection.PTVRows);

                                break;
                            case (int)VariableProperty.PTVColoumns:
                                functionReturnValue = Convert.ToDouble(oItemSection.PTVColoumns);

                                break;
                            case (int)VariableProperty.PrintViewLayoutLandScape:
                                functionReturnValue = Convert.ToDouble(oItemSection.PrintViewLayoutLandScape);

                                break;
                            case (int)VariableProperty.PrintViewLayoutPortrait:
                                functionReturnValue = Convert.ToDouble(oItemSection.PrintViewLayoutPortrait);

                                break;
                            case (int)VariableProperty.PrintToView:
                                if (oItemSection.PrintViewLayout.Value == (int)PrintViewOrientation.Landscape)
                                {
                                    functionReturnValue = Convert.ToDouble(oItemSection.PrintViewLayoutLandScape);
                                }
                                else
                                {
                                    functionReturnValue = Convert.ToDouble(oItemSection.PrintViewLayoutPortrait);
                                }

                                break;
                            case (int)VariableProperty.FilmQty:
                                functionReturnValue = Convert.ToDouble(oItemSection.FilmQty);

                                break;
                            case (int)VariableProperty.PlateQty:
                                functionReturnValue = Convert.ToDouble(oItemSection.Side1PlateQty + oItemSection.Side2PlateQty);
                                break;
                            //case (int)VariableProperty.GuilotineMakeReadycharge:
                            //    if (oItemSection.Guilotine == null)
                            //    {
                            //        functionReturnValue = 0;
                            //    }
                            //    else
                            //    {
                            //        functionReturnValue = oItemSection.Guilotine.MakeReadyCost;
                            //    }

                            //    break;
                            //case (int)VariableProperty.GuilotineChargePerCut:
                            //    if (oItemSection.Guilotine == null)
                            //    {
                            //        functionReturnValue = 0;
                            //    }
                            //    else
                            //    {
                            //        functionReturnValue = oItemSection.Guilotine.CostPerCut;
                            //    }

                            //    break;
                            case (int)VariableProperty.GuillotineFirstCut:
                                functionReturnValue = Convert.ToDouble(oItemSection.GuillotineFirstCut);

                                break;
                            case (int)VariableProperty.GuillotineSecondCut:
                                functionReturnValue = Convert.ToDouble(oItemSection.GuillotineSecondCut);

                                break;
                            case (int)VariableProperty.FinishedItemQty_ProRata:

                                if (Convert.ToDouble(oParamsArray[11]) != 0)
                                {
                                    functionReturnValue = Convert.ToDouble(oParamsArray[11]);
                                    return functionReturnValue;
                                }
                                switch (CurrentQuantity)
                                {
                                    case 1:
                                        functionReturnValue = Convert.ToDouble(oItemSection.Qty1);
                                        break;
                                    case 2:
                                        if (oItemSection.Qty2 == 0)
                                        {
                                            functionReturnValue = Convert.ToDouble(oItemSection.Qty1);
                                        }
                                        else
                                        {
                                            functionReturnValue = Convert.ToDouble(oItemSection.Qty2);
                                        }
                                        break;
                                    case 3:
                                        if (oItemSection.Qty3 == 0)
                                        {
                                            functionReturnValue = Convert.ToDouble(oItemSection.Qty1);
                                        }
                                        else
                                        {
                                            functionReturnValue = Convert.ToDouble(oItemSection.Qty3);
                                        }

                                        break;
                                    //Case 4
                                    //    ExecuteVariable = oItemSection.Qty1
                                    //Case 5
                                    //    ExecuteVariable = oItemSection.Qty1
                                }

                                break;
                            //case (int)VariableProperty.TotalSections:
                            //    functionReturnValue = oItemSection.TotalSection;

                            //    break;
                            case (int)VariableProperty.PaperWeight_ProRata:

                                switch (CurrentQuantity)
                                {
                                    case 1:
                                        functionReturnValue = oItemSection.PaperWeight1 ?? 0;
                                        break;
                                    case 2:
                                        if (oItemSection.PaperWeight2 == 0)
                                        {
                                            functionReturnValue = oItemSection.PaperWeight1 ?? 0;
                                        }
                                        else
                                        {
                                            functionReturnValue = oItemSection.PaperWeight2 ?? 0;
                                        }
                                        break;
                                    case 3:
                                        if (oItemSection.PaperWeight3 == 0)
                                        {
                                            functionReturnValue = oItemSection.PaperWeight1 ?? 0;
                                        }
                                        else
                                        {
                                            functionReturnValue = oItemSection.PaperWeight3 ?? 0;
                                        }
                                        break;
                                    //Case 4
                                    //    ExecuteVariable = oItemSection.PaperWeight4
                                    //Case 5
                                    //    ExecuteVariable = oItemSection.PaperWeight5
                                }

                                break;
                            case (int)VariableProperty.PrintSheetQtyIncSpoilage_ProRata:

                                //if (Convert.ToDouble(oParamsArray[11]) != 0)
                                //{
                                //    functionReturnValue = Convert.ToDouble(oParamsArray[11]);
                                //    return functionReturnValue;
                                //}

                                switch (CurrentQuantity)
                                {
                                    case 1:
                                        functionReturnValue = Convert.ToDouble(oItemSection.PrintSheetQty1 + oItemSection.SetupSpoilage + (oItemSection.PrintSheetQty1 * oItemSection.RunningSpoilage / 100));
                                        break;
                                    case 2:
                                        functionReturnValue = Convert.ToDouble(oItemSection.PrintSheetQty2 + oItemSection.SetupSpoilage + (oItemSection.PrintSheetQty2 * oItemSection.RunningSpoilage / 100));
                                        break;
                                    case 3:
                                        functionReturnValue = Convert.ToDouble(oItemSection.PrintSheetQty3 + oItemSection.SetupSpoilage + (oItemSection.PrintSheetQty3 * oItemSection.RunningSpoilage / 100));
                                        break;
                                    //Case 4
                                    //    ExecuteVariable = oItemSection.PrintSheetQty4 + oItemSection.SetupSpoilage + (oItemSection.PrintSheetQty4 * oItemSection.RunningSpoilage / 100)
                                    //Case 5
                                    //    ExecuteVariable = oItemSection.PrintSheetQty5 + oItemSection.SetupSpoilage + (oItemSection.PrintSheetQty5 * oItemSection.RunningSpoilage / 100)
                                }

                                break;

                            case (int)VariableProperty.FinishedItemQtyIncSpoilage_ProRata:
                                if (Convert.ToDouble(oParamsArray[11]) != 0)
                                {
                                    functionReturnValue = Convert.ToDouble(oParamsArray[11]);
                                    return functionReturnValue;
                                }

                                switch (CurrentQuantity)
                                {
                                    case 1:
                                        functionReturnValue = Convert.ToDouble(oItemSection.FinishedItemQty1);
                                        break;
                                    case 2:
                                        if (oItemSection.FinishedItemQty2 == 0)
                                        {
                                            functionReturnValue = Convert.ToDouble(oItemSection.FinishedItemQty1);
                                        }
                                        else
                                        {
                                            functionReturnValue = Convert.ToDouble(oItemSection.FinishedItemQty2);
                                        }
                                        break;
                                    case 3:
                                        if (oItemSection.FinishedItemQty3 == 0)
                                        {
                                            functionReturnValue = Convert.ToDouble(oItemSection.FinishedItemQty1);
                                        }
                                        else
                                        {
                                            functionReturnValue = Convert.ToDouble(oItemSection.FinishedItemQty3);
                                        }
                                        break;
                                    //Case 4
                                    //    ExecuteVariable = oItemSection.FinishedItemQty4
                                    //Case 5
                                    //    ExecuteVariable = oItemSection.FinishedItemQty5
                                }

                                break;
                            case (int)VariableProperty.NoOfSides:
                                if (Convert.ToBoolean(oItemSection.IsDoubleSided) == true)
                                {
                                    functionReturnValue = 2;
                                }
                                else
                                {
                                    functionReturnValue = 1;
                                }

                                break;
                            //case (int)VariableProperty.PressSizeRatio:
                            //    if (oItemSection.Press == null)
                            //    {
                            //        functionReturnValue = 0;
                            //    }
                            //    else
                            //    {
                            //        functionReturnValue = oItemSection.Press.PressSizeRatio;
                            //    }

                            //    break;

                            case (int)VariableProperty.SectionPaperWeightExSelfQty_ProRata:
                                switch (CurrentQuantity)
                                {
                                    case 1:
                                        functionReturnValue = oItemSection.SectionPaperWeightExSelfQty1.Value;
                                        break;
                                    case 2:
                                        if (oItemSection.SectionPaperWeightExSelfQty2 == 0)
                                        {
                                            functionReturnValue = oItemSection.SectionPaperWeightExSelfQty1.Value;
                                        }
                                        else
                                        {
                                            functionReturnValue = oItemSection.SectionPaperWeightExSelfQty2.Value;
                                        }
                                        break;
                                    case 3:
                                        if (oItemSection.SectionPaperWeightExSelfQty3 == 0)
                                        {
                                            functionReturnValue = oItemSection.SectionPaperWeightExSelfQty1.Value;
                                        }
                                        else
                                        {
                                            functionReturnValue = oItemSection.SectionPaperWeightExSelfQty3.Value;
                                        }
                                        break;
                                    //Case 4
                                    //    ExecuteVariable = oItemSection.SectionPaperWeightExSelfQty4
                                    //Case 5
                                    //    ExecuteVariable = oItemSection.SectionPaperWeightExSelfQty5
                                }

                                break;

                            case (int)VariableProperty.WashupQty:
                                functionReturnValue = Convert.ToDouble(oItemSection.WashupQty);

                                break;
                            case (int)VariableProperty.MakeReadyQty:
                                functionReturnValue = Convert.ToDouble(oItemSection.MakeReadyQty);
                                break;
                            case (int)VariableProperty.SectionHeight:
                                functionReturnValue = Convert.ToDouble(oItemSection.SectionSizeHeight);
                                break;
                            case (int)VariableProperty.SectionWidth:
                                functionReturnValue = Convert.ToDouble(oItemSection.SectionSizeWidth);
                                break;
                            default:
                                functionReturnValue = 0;

                                break;
                        }


                    }
                    else if (oVariable.Type == 2)
                    {
                        return obj.ExecUserVariable(oVariable, Convert.ToString(oParamsArray[10]));
                    }
                    else if (oVariable.Type == 3)
                    {
                        return oVariable.VariableValue ?? 0;
                    }


                    oVariable = null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ExecuteVariable", ex);
            }
            return functionReturnValue;

        }

        public double ExecuteInput(ref object[] oParamsArray, string InputID, string Question, int ItemType, int InputType, string Value, int CostCentreID)
         {
	            bool bFlag = false;
	            InputQueueItem QuestionITEM = null;
	            CostCentreExecutionMode ExecutionMode = (CostCentreExecutionMode)oParamsArray[1];
	            ItemSection oItemSection = (ItemSection)oParamsArray[8];
	            int CurrentQuantity = Convert.ToInt32(oParamsArray[5]);
	            int MultipleQutantities = Convert.ToInt32(oParamsArray[4]);
	            InputQueue InputQueue = oParamsArray[7] as InputQueue;
                double dblReturn = 0;
                string connectionString = Convert.ToString(oParamsArray[10]);

	        try 
            {
		        //check if its the visual mode or Execution Mode
		        if (ExecutionMode == CostCentreExecutionMode.ExecuteMode) 
                {
			        //here the questions returned asnwer will ahave been loaded in the queue
			        //retreive the queue answer for this question and use.. :D
			        //use is simple only cast it in double and return..
                    List<InputQueueItem> InputQueueItem = oParamsArray[7] as List<InputQueueItem>;
			       // InputQueueItem item = null;
                    if (InputQueueItem != null)
                    {
                        foreach (var item in InputQueueItem)
                        {
                            //matching
                            if (item.ID == InputID & item.CostCentreID == CostCentreID & item.ItemType == ItemType)
                            {
                                bFlag = true;
                                QuestionITEM = item;
                                break; // TODO: might not be correct. Was : Exit For
                            }
                        }
                    }
			        
                    MPC.Repository.Repositories.CostCentreExecution obj = new MPC.Repository.Repositories.CostCentreExecution();
			        //if found question in queue then use its values
			        if (bFlag == true) {
				        //Return CDbl(item.Answer)
				        //multiple qty logic goes here

				        if (CurrentQuantity <= MultipleQutantities) {

					        switch (CurrentQuantity) {
						        case 1:
							        //its a normal input question, return the value :)
                                    if (QuestionITEM.ItemInputType == 0) 
                                    {
                                        return Convert.ToDouble(QuestionITEM.Qty1Answer);
								        //its a click charge formulae with variable input
							        }
                                    else if (QuestionITEM.ItemInputType == 1) 
                                    {
                                        return obj.CalculateLookup(Convert.ToInt32(QuestionITEM.ID), ExecuteVariable(ref oParamsArray, Convert.ToInt32(QuestionITEM.Value)), (ClickChargeReturnType)QuestionITEM.ItemType, connectionString);
								        //its a click charge formulae with Question Input
							        }
                                    else if (QuestionITEM.ItemInputType == 2) 
                                    {
                                        return obj.CalculateLookup(Convert.ToInt32(QuestionITEM.ID), QuestionITEM.Qty1Answer, (ClickChargeReturnType)QuestionITEM.ItemType, connectionString);
							        }

							        break;
						        case 2:
                                    if (QuestionITEM.Qty2Answer == 0) 
                                    {
                                        if (QuestionITEM.ItemInputType == 0) 
                                        {
                                            return Convert.ToDouble(QuestionITEM.Qty1Answer);
									        //its a click charge formulae with variable input
								        }
                                        else if (QuestionITEM.ItemInputType == 1) 
                                        {
                                            return obj.CalculateLookup(Convert.ToInt32(QuestionITEM.ID), ExecuteVariable(ref oParamsArray, Convert.ToInt32(QuestionITEM.Value)), (ClickChargeReturnType)QuestionITEM.ItemType);
									        //its a click charge formulae with Question Input
								        }
                                        else if (QuestionITEM.ItemInputType == 2) 
                                        {
                                            return obj.CalculateLookup(Convert.ToInt32(QuestionITEM.ID), QuestionITEM.Qty1Answer, (ClickChargeReturnType)QuestionITEM.ItemType);
								        }
							        } 
                                    else 
                                    {
                                        if (QuestionITEM.ItemInputType == 0) 
                                        {
                                            return Convert.ToDouble(QuestionITEM.Qty2Answer);
									        //its a click charge formulae with variable input
								        }
                                        else if (QuestionITEM.ItemInputType == 1) 
                                        {
                                            return obj.CalculateLookup(Convert.ToInt32(QuestionITEM.ID), ExecuteVariable(ref oParamsArray, Convert.ToInt32(QuestionITEM.Value)), (ClickChargeReturnType)QuestionITEM.ItemType);
									        //its a click charge formulae with Question Input
								        }
                                        else if (QuestionITEM.ItemInputType == 2)
                                        {
                                            return obj.CalculateLookup(Convert.ToInt32(QuestionITEM.ID), QuestionITEM.Qty2Answer, (ClickChargeReturnType)QuestionITEM.ItemType);
								        }
							        }
							        break;
						        case 3:
                                    if (Convert.ToDouble(QuestionITEM.Qty3Answer) == 0)
                                    {
                                        if (QuestionITEM.ItemInputType == 0) 
                                        {
                                            return Convert.ToDouble(QuestionITEM.Qty1Answer);
									        //its a click charge formulae with variable input
								        }
                                        else if (QuestionITEM.ItemInputType == 1) 
                                        {
                                            return obj.CalculateLookup(Convert.ToInt32(QuestionITEM.ID), ExecuteVariable(ref oParamsArray, Convert.ToInt32(QuestionITEM.Value)), (ClickChargeReturnType)QuestionITEM.ItemType);
									        //its a click charge formulae with Question Input
								        }
                                        else if (QuestionITEM.ItemInputType == 2)
                                        {
                                            return obj.CalculateLookup(Convert.ToInt32(QuestionITEM.ID), QuestionITEM.Qty1Answer, (ClickChargeReturnType)QuestionITEM.ItemType);
								        }
							        } 
                                    else 
                                    {
                                        if (QuestionITEM.ItemInputType == 0) 
                                        {
                                            return Convert.ToDouble(QuestionITEM.Qty3Answer);
									        //its a click charge formulae with variable input
								        }
                                        else if (QuestionITEM.ItemInputType == 1)
                                        {
                                            return obj.CalculateLookup(Convert.ToInt32(QuestionITEM.ID), ExecuteVariable(ref oParamsArray, Convert.ToInt32(QuestionITEM.Value)), (ClickChargeReturnType)QuestionITEM.ItemType);

									        //its a click charge formulae with Question Input
								        }
                                        else if (QuestionITEM.ItemInputType == 2)
                                        {
                                            return obj.CalculateLookup(Convert.ToInt32(QuestionITEM.ID), QuestionITEM.Qty3Answer, (ClickChargeReturnType)QuestionITEM.ItemType);
								        }
							        }
							        break;
					        }
					        //its a click charge formulae with variable input

				        } 
                        else 
                        {
					        throw new Exception("Invalid  Current Selected Multiple Quantitity");
				        }
			        } 
                    else 
                    {
				        throw new Exception("Answer not find in Queue");
			        }


		        } 
                else if (ExecutionMode == CostCentreExecutionMode.PromptMode) 
                {
			        //populate the question in the executionQueue
                    double Qty1Val = 0;
                    if(!string.IsNullOrEmpty(Value))
                    {
                        Qty1Val = Convert.ToDouble(Value);
                    }
                    InputQueue.addItem(InputID, Question, CostCentreID, ItemType, InputType, Question, Value, Qty1Val);
			        //
			        return 1;
			        //exit normally 
		        }

	        } 
            catch (Exception ex) 
            {
		        throw new Exception("ExecuteInput", ex);
	        }
            return dblReturn;
        }
    }
}