using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class BestPressResponseMapper
    {
        public static BestPressResponse CreateFrom(this MPC.Models.ResponseModels.BestPressResponse source)
        {
            return new BestPressResponse
            {   
                PressList = source.PressList != null ? source.PressList.Select(p => p.CreateFrom()).ToList() : null,
                UserCostCenters = source.UserCostCenters != null ? source.UserCostCenters.Select(c => c.CreateFrom()).ToList() : null
            };
        }
    }
}