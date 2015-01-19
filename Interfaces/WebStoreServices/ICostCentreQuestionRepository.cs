﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.WebStoreServices
{
    public interface ICostCentreQuestionRepository
    {
        double ExecuteQuestion(ref object[] oParamsArray, int QuestionID, long CostCentreID);
    }
}
