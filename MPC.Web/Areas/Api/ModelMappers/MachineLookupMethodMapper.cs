using MPC.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class MachineLookupMethodMapper
    {
        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static MachineLookupMethod CreateFrom(this MPC.Models.DomainModels.MachineLookupMethod source)
        {
            return new MachineLookupMethod
            {
                Id = source.Id,
                MachineId = source.MachineId,
                MethodId = source.MethodId,
                DefaultMethod = source.DefaultMethod
            };
        }

        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static MPC.Models.DomainModels.MachineLookupMethod CreateFrom(this MachineLookupMethod source)
        {
            return new MPC.Models.DomainModels.MachineLookupMethod
            {

                Id = source.Id,
                MachineId = source.MachineId,
                MethodId = source.MethodId,
                DefaultMethod = source.DefaultMethod
            };
        }
        #endregion
    }
}