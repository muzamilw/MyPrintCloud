using AutoMapper;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
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
    [Serializable()]
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
        public bool DeleteQuestionById(int QuestionId)
        {
            CostCentreQuestion oQuestion= db.CostCentreQuestions.Where(g=>g.Id==QuestionId).SingleOrDefault();
            if (oQuestion != null)
            {
                if (oQuestion.Type == 2)
                {

                    //     IEnumerable<CostCentreAnswer> answerList = db.CostCentreAnswers.Where(g => g.QuestionId == QuestionId).ToList();
                    db.CostCentreAnswers.RemoveRange(db.CostCentreAnswers.Where(g => g.QuestionId == QuestionId));
                }
                db.CostCentreQuestions.Remove(oQuestion);
            }
           
            if (db.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public CostCentreQuestion Add(CostCentreQuestion question, IEnumerable<CostCentreAnswer> answer)
        {
            CostCentreQuestion oQuestion = new CostCentreQuestion();
            oQuestion.QuestionString = question.QuestionString;
            oQuestion.Type = question.Type;
            oQuestion.DefaultAnswer = question.DefaultAnswer;
            db.CostCentreQuestions.Add(oQuestion);
            if (db.SaveChanges() > 0)
            {
                if (question.Type == 3 && answer.Count() > 0)
                {
                    foreach (CostCentreAnswer ans in answer)
                    {
                        CostCentreAnswer oAns = new CostCentreAnswer();
                        oAns.AnswerString = ans.AnswerString;
                        oAns.QuestionId = oQuestion.Id;
                        db.CostCentreAnswers.Add(oAns);


                    }


                }
                db.SaveChanges();
                return oQuestion;

            }          
            
            else
            {
                return null;
            }

        }
        public bool update(CostCentreQuestion question, IEnumerable<CostCentreAnswer> answer)
        {
            CostCentreQuestion oQuestion = db.CostCentreQuestions.Where(g => g.Id == question.Id).SingleOrDefault();
            oQuestion.QuestionString = question.QuestionString;
            oQuestion.Type = question.Type;
            oQuestion.DefaultAnswer = question.DefaultAnswer;
            if (question.Type == 3 && answer.Count() >0)
            {
                foreach (CostCentreAnswer ans in answer)
                {
                    if (ans.Id > 0)
                    {
                        CostCentreAnswer oAns = db.CostCentreAnswers.Where(g => g.Id == ans.Id).FirstOrDefault();
                        oAns.AnswerString = ans.AnswerString;
                    }
                    else
                    {
                        CostCentreAnswer oAns = new CostCentreAnswer();
                        oAns.AnswerString = ans.AnswerString;
                        oAns.QuestionId = question.Id;
                        db.CostCentreAnswers.Add(oAns);
                    }
                    
                }
               

            }
            if (db.SaveChanges() > 0)
            {
                return true;
                
            }
            else
            {
                return false;
            }

            
        }
        

        /// <summary>
        /// Functions returns the Question information with Answer collection Depending upon the Questiontype
        /// </summary>
        /// <param name="QuestionID">QuestionID</param>
        /// <param name="oConnection">DB Connection</param>
        /// <returns>Question</returns>
        public CostCentreQuestion LoadQuestion(int QuestionID)
        {
            try
            {
                CostCentreQuestion QuestionObj = new CostCentreQuestion();
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
                    //if (oQuestion.Type == (int)QuestionType.MultipleChoiceQuestion)
                    //{
                    //    QuestionObj.AnswerCollection = LoadAnswer(QuestionID);
                    //}
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

        public List<CostCentreQuestion> GetCostCentreQuestionsByOID(long OrganisationID,out List<CostCentreAnswer> CostAnswers)
        {
            try
            {

                
                List<CostCentreQuestion> questions = db.CostCentreQuestions.Where(a => a.CompanyId == OrganisationID).ToList();
                List<CostCentreAnswer> costCentreAnswers = new List<CostCentreAnswer>();
                 List<CostCentreAnswer> answers = new List<CostCentreAnswer>();
                if(questions != null && questions.Count > 0)
                {
                    foreach(var question in questions)
                    {
                        answers = db.CostCentreAnswers.Where(c => c.QuestionId == question.Id).ToList();
                       if(answers != null && answers.Count > 0)
                       {
                           foreach(var ans in answers)
                           {
                               costCentreAnswers.Add(ans);
                           }
                       }
                        
                    }

                }
                CostAnswers = costCentreAnswers;
                return db.CostCentreQuestions.Where(a => a.CompanyId == OrganisationID).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception("Load Answer", ex);
            }
        }

       

    }
}
