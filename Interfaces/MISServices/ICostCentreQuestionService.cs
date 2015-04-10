using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.MISServices
{
    public interface ICostCentreQuestionService
    {
        bool update(CostCentreQuestion question, IEnumerable<CostCentreAnswer> answer);
        CostCentreQuestion Add(CostCentreQuestion question, IEnumerable<CostCentreAnswer> answer);
        IEnumerable<CostCentreAnswer> GetByQuestionId(int QuestionId);
        bool DeleteQuestionById(int QuestionId);
        bool DeleteMCQsQuestionAnswerById(int MCQsQuestionAnswerId);
        
    }
}
