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

                         functionReturnValue = obj.TestConnection(ResourceID); // _CostCentreRepository.ExecuteUserResource(ResourceID, ResourceReturnType.CostPerHour);
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
                         //Return CDbl(item.Answer)
                         //if MultipleQutantities
                         //new multiple qty logic goes here

                         if (CurrentQuantity <= MultipleQutantities)
                         {
                             switch (CurrentQuantity)
                             {
                                 case 1:
                                     return Convert.ToDouble(QuestionItem.Qty1Answer);
                                 case 2:
                                     if (QuestionItem.Qty2Answer == 0)
                                     {
                                         return Convert.ToDouble(QuestionItem.Qty1Answer);
                                     }
                                     else
                                     {
                                         return Convert.ToDouble(QuestionItem.Qty2Answer);
                                     }
                                     break;
                                 case 3:
                                     if (Convert.ToDouble(QuestionItem.Qty3Answer) == 0)
                                     {
                                         return Convert.ToDouble(QuestionItem.Qty1Answer);
                                     }
                                     else
                                     {
                                         return Convert.ToDouble(QuestionItem.Qty3Answer);
                                     }

                                     break;
                             }
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
                     //loading the Questions Information for populating in the Queue
                     MPC.Repository.Repositories.CostCentreExecution obj = new MPC.Repository.Repositories.CostCentreExecution();
                     CostCentreQuestion ovariable = obj.LoadQuestion(Convert.ToInt32(QuestionID));

                     QuestionItem = new QuestionQueueItem(QuestionID, ovariable.QuestionString, CostCentreID, ovariable.Type.Value, ovariable.QuestionString, ovariable.DefaultAnswer, "", false, 0, 0, 0, 0, 0, ovariable.AnswerCollection);
                     
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
                         //Return CDbl(item.Answer)
                         //multiple qty logic goes here

                         if (CurrentQuantity <= MultipleQutantities)
                         {
                             switch (CurrentQuantity)
                             {
                                 case 1:
                                     return Convert.ToDouble(QuestionItem.Qty1Answer);
                                 case 2:
                                     if (Convert.ToDouble(QuestionItem.Qty2Answer) == 0)
                                     {
                                         return Convert.ToDouble(QuestionItem.Qty1Answer);
                                     }
                                     else
                                     {
                                         return Convert.ToDouble(QuestionItem.Qty2Answer);
                                     }
                                     break;
                                 case 3:
                                     if (Convert.ToDouble(QuestionItem.Qty3Answer) == 0)
                                     {
                                         return Convert.ToDouble(QuestionItem.Qty1Answer);
                                     }
                                     else
                                     {
                                         return Convert.ToDouble(QuestionItem.Qty3Answer);
                                     }
                                     break;
                             }
                         }
                         else
                         {
                             throw new Exception("Invalid  Current Selected Multiple Quantitity");
                         }
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
                     CostCentreMatrix oMatrix = null; //_CostCentreMatrixRepository.GetMatrix(MatrixID);
                     QuestionItem = new QuestionQueueItem(MatrixID, oMatrix.Name, CostCentreID, 4, oMatrix.Description, "", "", false, 0);
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

    }
}
