using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel = MPC.Models.DomainModels;
using ApiModel = MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class MachineMeterPerHourLookupMapper
    {
        public static DomainModel.MachineMeterPerHourLookup CreateFrom(this ApiModel.MachineMeterPerHourLookup source)
        {
            return new DomainModel.MachineMeterPerHourLookup
            {
                Id = source.Id,
                MethodId = source.MethodId,
                SheetsQty1 = source.SheetsQty1,
                SheetsQty2 = source.SheetsQty2,
                SheetsQty3 = source.SheetsQty3,
                SheetsQty4 = source.SheetsQty4,
                SheetsQty5 = source.SheetsQty5,
                SheetWeight1 = source.SheetWeight1,
                speedqty11 = source.speedqty11,
                speedqty12 = source.speedqty12,
                speedqty13 = source.speedqty13,
                speedqty14 = source.speedqty14,
                speedqty15 = source.speedqty15,
                SheetWeight2 = source.SheetWeight2,
                speedqty21 = source.speedqty21,
                speedqty22 = source.speedqty22,
                speedqty23 = source.speedqty23,
                speedqty24 = source.speedqty24,
                speedqty25 = source.speedqty25,
                SheetWeight3 = source.SheetWeight3,
                speedqty31 = source.speedqty31,
                speedqty32 = source.speedqty32,
                speedqty33 = source.speedqty33,
                speedqty34 = source.speedqty34,
                speedqty35 = source.speedqty35,
                hourlyCost = source.hourlyCost,
                hourlyPrice = source.hourlyPrice

            };
        }
        public static ApiModel.MachineMeterPerHourLookup CreateFrom(this DomainModel.MachineMeterPerHourLookup source)
        {
            return new ApiModel.MachineMeterPerHourLookup
            {
                Id = source.Id,
                MethodId = source.MethodId,
                SheetsQty1 = source.SheetsQty1,
                SheetsQty2 = source.SheetsQty2,
                SheetsQty3 = source.SheetsQty3,
                SheetsQty4 = source.SheetsQty4,
                SheetsQty5 = source.SheetsQty5,
                SheetWeight1 = source.SheetWeight1,
                speedqty11 = source.speedqty11,
                speedqty12 = source.speedqty12,
                speedqty13 = source.speedqty13,
                speedqty14 = source.speedqty14,
                speedqty15 = source.speedqty15,
                SheetWeight2 = source.SheetWeight2,
                speedqty21 = source.speedqty21,
                speedqty22 = source.speedqty22,
                speedqty23 = source.speedqty23,
                speedqty24 = source.speedqty24,
                speedqty25 = source.speedqty25,
                SheetWeight3 = source.SheetWeight3,
                speedqty31 = source.speedqty31,
                speedqty32 = source.speedqty32,
                speedqty33 = source.speedqty33,
                speedqty34 = source.speedqty34,
                speedqty35 = source.speedqty35,
                hourlyCost = source.hourlyCost,
                hourlyPrice = source.hourlyPrice

            };
        }
    }
}