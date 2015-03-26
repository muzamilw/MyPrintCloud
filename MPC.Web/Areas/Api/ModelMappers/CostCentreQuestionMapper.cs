using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class CostCentreQuestionMapper
    {
        public static CostCentreQuestion CreateFrom(this MPC.Models.DomainModels.CostCentreQuestion source)
        {
            return new CostCentreQuestion
            {
                Id = source.Id,
                QuestionString = source.QuestionString,
                Type = source.Type,
                DefaultAnswer = source.DefaultAnswer,
                CompanyId = source.CompanyId,
                SystemSiteId = source.SystemSiteId
            };
        }

        public static MPC.Models.DomainModels.CostCentreQuestion CreateFrom(this CostCentreQuestion source)
        {
            return new MPC.Models.DomainModels.CostCentreQuestion
            {
                Id = source.Id,
                QuestionString = source.QuestionString,
                Type = source.Type,
                DefaultAnswer = source.DefaultAnswer,
                CompanyId = source.CompanyId,
                SystemSiteId = source.SystemSiteId
            };
        }
    }
}