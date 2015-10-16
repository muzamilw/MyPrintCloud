using MPC.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class ReportParamResponseMapper
    {
        public static ReportparamResponse CreateFrom(this MPC.Models.ResponseModels.ReportparamResponse source)
        {
            return new ReportparamResponse
            {

                param = source.param.CreateFrom(),
                ComboList = source.ComboList != null ? source.ComboList.Select(c => c.CreateFrom()).ToList() : null
            };
        }
    }
}