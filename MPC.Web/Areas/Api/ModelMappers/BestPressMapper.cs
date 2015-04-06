using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class BestPressMapper
    {
        public static BestPress CreateFrom(this MPC.Models.Common.BestPress source)
        {
            return new BestPress 
            { 
                MachineID = source.MachineID,
                MachineName = source.MachineName,
                isSelected = source.isSelected,
                Qty1Cost = source.Qty1Cost,
                Qty1RunTime = source.Qty1RunTime,
                Qty2Cost = source.Qty2Cost,
                Qty2RunTime = source.Qty2RunTime,
                Qty3Cost = source.Qty3Cost,
                Qty3RunTime = source.Qty3RunTime
            };
        }
    }
}