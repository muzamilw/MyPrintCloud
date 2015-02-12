using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class MachineSpoilageMapper
    {

        public static MachineSpoilage CreateFrom(this DomainModels.MachineSpoilage source){
            return new MachineSpoilage
            {
                MachineSpoilageId = source.MachineSpoilageId,
                MachineId = source.MachineId,
                SetupSpoilage = source.SetupSpoilage,
                RunningSpoilage = source.RunningSpoilage,
                NoOfColors = source.NoOfColors
            };

        }
        public static DomainModels.MachineSpoilage CreateFrom(this MachineSpoilage source)
        {
            return new DomainModels.MachineSpoilage
            {
                MachineSpoilageId = source.MachineSpoilageId,
                MachineId = source.MachineId,
                SetupSpoilage = source.SetupSpoilage,
                RunningSpoilage = source.RunningSpoilage,
                NoOfColors = source.NoOfColors
            };

        }
    }
}