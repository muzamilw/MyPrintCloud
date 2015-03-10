using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel = MPC.Models.DomainModels;
using ApiModel = MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class MachineGuilotinePtvMapper
    {
        public static DomainModel.MachineGuilotinePtv CreateFrom(this ApiModel.MachineGuilotinePtv source)
        {
            return new DomainModel.MachineGuilotinePtv
            {
                Id = source.Id,
                NoofSections = source.NoofSections,
                NoofUps = source.NoofUps,
                Noofcutswithoutgutters = source.Noofcutswithoutgutters,
                Noofcutswithgutters = source.Noofcutswithgutters,
                GuilotineId = source.GuilotineId

            };
        }
        public static ApiModel.MachineGuilotinePtv CreateFrom(this DomainModel.MachineGuilotinePtv source)
        {
            return new ApiModel.MachineGuilotinePtv
            {
                Id = source.Id,
                NoofSections = source.NoofSections,
                NoofUps = source.NoofUps,
                Noofcutswithoutgutters = source.Noofcutswithoutgutters,
                Noofcutswithgutters = source.Noofcutswithgutters,
                GuilotineId = source.GuilotineId

            };
        }
    }
}