using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface ICostCentreAnswerRepository : IBaseRepository<CostCentreAnswer, long>
    {
        IEnumerable<CostCentreAnswer> GetByQuestionId(int QuestionId);
        bool DeleteMCQsQuestionAnswerById(int MCQsQuestionAnswerId);

    }
}
    