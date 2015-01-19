﻿using Microsoft.Practices.Unity;
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
