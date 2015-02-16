using DomainModels = MPC.Models.DomainModels;
using APIDomainModels = MPC.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class MachineInkCoverageMapper
    {
        public static APIDomainModels.MachineInkCoverage CreateFrom(this DomainModels.MachineInkCoverage source)
        {
            return new APIDomainModels.MachineInkCoverage
            {
                Id = source.Id,
                SideInkOrder = source.SideInkOrder,
                SideInkOrderCoverage = source.SideInkOrderCoverage,
                MachineId = source.MachineId
            };

        }
        public static DomainModels.MachineInkCoverage CreateFrom(this APIDomainModels.MachineInkCoverage source)
        {
            return new DomainModels.MachineInkCoverage
            {
                Id = source.Id,
                SideInkOrder = source.SideInkOrder,
                SideInkOrderCoverage = source.SideInkOrderCoverage,
                MachineId = source.MachineId
            };

        }

    }
}