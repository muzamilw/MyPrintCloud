using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModels= MPC.Models.DomainModels;
using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class MachineResourcesMapper
    {
        public static MachineResource CreateFrom(this DomainModels.MachineResource source)
        {
            return new MachineResource
            {
                Id = source.Id,
                MachineId = source.MachineId,
                ResourceId = source.ResourceId
               
            };

        }
    }
}