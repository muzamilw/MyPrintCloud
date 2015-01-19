using Microsoft.Practices.Unity;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Repository.Repositories
{
    public class CostCentreQuestionRepository : BaseRepository<CostCentreQuestion>, ICostCentreQuestionRepository
    {
        public CostCentreQuestionRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<CostCentreQuestion> DbSet
        {
            get
            {
                return db.CostCentreQuestions;
            }
        }


        /// <summary>
        /// Question Execution Function
        /// </summary>
        /// <param name="QuestionID"></param>
        /// <param name="oConnection"></param>
        /// <param name="ExecutionMode"></param>
        /// <param name="QuestionQueue"></param>
        /// <returns></returns>
        public double ExecuteQuestion(ref object[] oParamsArray, int QuestionID, long CostCentreID)
{

    CostCentreExecutionMode ExecutionMode = (CostCentreExecutionMode)oParamsArray[1];
    ItemSection oItemSection = (ItemSection)oParamsArray[8];
    int CurrentQuantity = Convert.ToInt32(oParamsArray[5]);
    int MultipleQutantities = Convert.ToInt32(oParamsArray[4]);
    List<ExecutionQueueDTO> QuestionQueue = oParamsArray[2] as List<ExecutionQueueDTO>;



    bool bFlag = false;
    QueueItemDTO QuestionITEM = null;
    try {
        //check if its the visual mode or Execution Mode
        if (ExecutionMode == CostCentreExecutionMode.ExecuteMode) {
            //here the questions returned asnwer will ahave been loaded in the queue
            //retreive the queue answer for this question and use.. :D
            //use is simple only cast it in double and return..

            
           // QueueItemDTO item = default(Model.costcentres.QueueItemDTO);
            //foreach (var item in QuestionQueue) {
            //    //matching
            //    if (item.ID == QuestionID & item.CostCentreID == CostCentreID) {
            //        bFlag = true;
            //        QuestionITEM = item;
            //        break; // TODO: might not be correct. Was : Exit For
            //    }
            //}

            //if found question in queue then use its values
            if (bFlag == true) {
                //Return CDbl(item.Answer)
                //if MultipleQutantities
                //new multiple qty logic goes here

                if (CurrentQuantity <= MultipleQutantities) {
                    switch (CurrentQuantity) {
                        case 1:
                            return Convert.ToDouble(QuestionITEM.Qty1Answer);
                        case 2:
                            if (QuestionITEM.Qty2Answer == 0) {
                                return Convert.ToDouble(QuestionITEM.Qty1Answer);
                            } else {
                                return Convert.ToDouble(QuestionITEM.Qty2Answer);
                            }
                            break;
                        case 3:
                            if (Convert.ToDouble(QuestionITEM.Qty3Answer) == 0) {
                                return Convert.ToDouble(QuestionITEM.Qty1Answer);
                            } else {
                                return Convert.ToDouble(QuestionITEM.Qty3Answer);
                            }

                            break;
                    }
                } else {
                    throw new Exception("Invalid  Current Selected Multiple Quantitity");
                }
            } else {
                throw new Exception("Answer not find in Queue");
            }

        } else if (ExecutionMode == CostCentreExecutionMode.PromptMode) {
            //populate the question in the executionQueue
            //loading the Questions Information for populating in the Queue
            CostCentreQuestions ovariable = LoadQuestion(Convert.ToInt32(QuestionID));
            QuestionITEM = ExecutionQueueDTO.addItem(QuestionID, "", CostCentreID, ovariable.Type ?? 0, ovariable.QuestionString, ovariable.DefaultAnswer, "", false, ovariable.AnswerCollection, QuestionQueue);
            if (QuestionITEM != null) 
            {
                //QuestionQueue.Add(QuestionITEM);
            }
            ovariable = null;
           
            //exit normally 
        }
        return 1;
    } catch (Exception ex) {
        throw new Exception("ExecuteQuestion", ex);
    }

}

        /// <summary>
        /// Functions returns the Question information with Answer collection Depending upon the Questiontype
        /// </summary>
        /// <param name="QuestionID">QuestionID</param>
        /// <param name="oConnection">DB Connection</param>
        /// <returns>Question</returns>
        public CostCentreQuestions LoadQuestion(int QuestionID)
        {
            try
            {
                CostCentreQuestions QuestionObj = new CostCentreQuestions();
                //querying the DB to retrieve the Question information
                CostCentreQuestion oQuestion = db.CostCentreQuestions.Where(q => q.Id == QuestionID).FirstOrDefault();
                //if Question information was found
                if (oQuestion != null)
                {
                    

                    QuestionObj.Id = oQuestion.Id;
                    QuestionObj.QuestionString = oQuestion.QuestionString;
                    QuestionObj.Type = oQuestion.Type;
                    //now here we are going to load possible answers againt the Question into the Question object
                    //if question contains
                    if (oQuestion.Type == (int)QuestionType.MultipleChoiceQuestion)
                    {
                        QuestionObj.AnswerCollection = LoadAnswer(QuestionID);
                    }
                }

                //finally returning the populated Question object
                return QuestionObj;
            }
            catch (Exception ex)
            {
                throw new Exception("LoadQuestions", ex);
            }
        }

        /// <summary>
        /// Loading answers of multiple choice questions
        /// </summary>
        /// <param name="QuestionID"></param>
        /// <returns></returns>
        public List<CostCentreAnswer> LoadAnswer(int QuestionID)
        {
            try
            {
                return db.CostCentreAnswers.Where(a => a.QuestionId == QuestionID).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Load Answer", ex);
            }
        }

    }
}
