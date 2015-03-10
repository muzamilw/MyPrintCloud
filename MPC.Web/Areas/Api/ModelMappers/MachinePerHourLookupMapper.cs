using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel = MPC.Models.DomainModels;
using ApiModel = MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class MachinePerHourLookupMapper
    {
        public static DomainModel.MachinePerHourLookup CreateFrom(this ApiModel.MachinePerHourLookup source)
        {
            return new DomainModel.MachinePerHourLookup
            {
                Id = source.Id,
                MethodId = source.MethodId,
                SpeedCost = source.SpeedCost,
                Speed = source.Speed,
                SpeedPrice = source.SpeedPrice

            };
        }
        public static ApiModel.MachinePerHourLookup CreateFrom(this DomainModel.MachinePerHourLookup source)
        {
            return new ApiModel.MachinePerHourLookup
            {
                Id = source.Id,
                MethodId = source.MethodId,
                SpeedCost = source.SpeedCost,
                Speed = source.Speed,
                SpeedPrice = source.SpeedPrice

            };
        }
    }
}