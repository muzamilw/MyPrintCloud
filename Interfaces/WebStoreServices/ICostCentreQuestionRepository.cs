using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.WebStoreServices
{
    public interface ICostCentreQuestionRepository
    {
        CostCentreQuestions LoadQuestion(int QuestionID);

        List<CostCentreAnswer> LoadAnswer(int QuestionID);
    }
}
