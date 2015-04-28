using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel = MPC.Models.DomainModels;
using ApiModel = MPC.MIS.Areas.Api.Models;


namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class MachineClickChargeLookupMapper
    {
        public static DomainModel.MachineClickChargeLookup CreateFrom(this ApiModel.MachineClickChargeLookup source)
        {
            return new DomainModel.MachineClickChargeLookup
            {
                Id = source.Id,
                MethodId = source.MethodId,
                SheetCost = source.SheetCost,
                Sheets = source.Sheets,
                SheetPrice = source.SheetPrice,
                TimePerHour = source.TimePerHour

            };
        }
        public static ApiModel.MachineClickChargeLookup CreateFrom(this DomainModel.MachineClickChargeLookup source)
        {
            return new ApiModel.MachineClickChargeLookup
            {
                Id = source.Id,
                MethodId = source.MethodId,
                SheetCost = source.SheetCost,
                Sheets = source.Sheets,
                SheetPrice = source.SheetPrice,
                TimePerHour = source.TimePerHour

            };
        }
    }
}