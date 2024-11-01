﻿using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Interfaces.Repository;

namespace MPC.Interfaces.Repository
{
    public interface ICostCentreQuestionRepository
    {
        CostCentreQuestion LoadQuestion(int QuestionID);

        List<CostCentreAnswer> LoadAnswer(int QuestionID);

        List<CostCentreQuestion> GetCostCentreQuestionsByOID(long OrganisationID, out List<CostCentreAnswer> CostAnswers);
        bool update(CostCentreQuestion question, IEnumerable<CostCentreAnswer> answer);
        CostCentreQuestion Add(CostCentreQuestion question, IEnumerable<CostCentreAnswer> answer);
        bool DeleteQuestionById(int QuestionId);
    }
}
