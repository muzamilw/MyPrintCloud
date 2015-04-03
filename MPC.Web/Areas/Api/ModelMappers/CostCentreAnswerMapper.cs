using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel = MPC.Models.DomainModels;
using ApiModel = MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class CostCentreAnswerMapper
    {

        public static ApiModel.CostCentreAnswer CreateFrom(this DomainModel.CostCentreAnswer source){
            return new ApiModel.CostCentreAnswer
            {
                Id = source.Id,
                QuestionId = source.QuestionId,
                AnswerString = source.AnswerString
            };
        }
        public static DomainModel.CostCentreAnswer CreateFrom(this ApiModel.CostCentreAnswer source)
        {
            return new DomainModel.CostCentreAnswer
            {
                Id = source.Id,
                QuestionId = source.QuestionId,
                AnswerString = source.AnswerString
            };
        }
    }
}