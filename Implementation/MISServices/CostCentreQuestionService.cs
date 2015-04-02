using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;

namespace MPC.Implementation.MISServices
{
    public class CostCentreQuestionService : ICostCentreQuestionService
    {
        private readonly ICostCentreQuestionRepository CostCentreQuestion;
        private readonly ICostCentreAnswerRepository _CostCentreAnswer;

        public CostCentreQuestionService(ICostCentreQuestionRepository CostCentreQuestion, ICostCentreAnswerRepository _CostCentreAnswer)
        {
            if (_CostCentreAnswer == null)
            {
                throw new ArgumentNullException("CostCentreAnswer");
            }
            this._CostCentreAnswer = _CostCentreAnswer; 
            this.CostCentreQuestion = CostCentreQuestion;

        }
        public IEnumerable<CostCentreAnswer> GetByQuestionId(int QuestionId)
        {
            return _CostCentreAnswer.GetByQuestionId(QuestionId);
        }
        public bool update(CostCentreQuestion question, IEnumerable<CostCentreAnswer> answer)
        {
            return CostCentreQuestion.update(question, answer);
            
        }
    }
}
