using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.WebStoreServices
{
    public interface ICostCentreQuestionRepository
    {
        double ExecuteQuestion(ref object[] oParamsArray, int QuestionID, long CostCentreID);
    }
}
