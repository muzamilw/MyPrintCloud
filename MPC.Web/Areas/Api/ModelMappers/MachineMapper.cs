using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;
using DomainResponseModel = MPC.Models.ResponseModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Machine WebApi Mapper
    /// </summary>
    public static class MachineMapper
    {
        
        #region Public

        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static MachineSearchResponse CreateFrom(this DomainResponseModel.MachineSearchResponse source)
        {
            return new MachineSearchResponse
            {
                Machines = source.Machines != null ? source.Machines.Select(machine => machine.CreateFrom()).ToList() : null,
                TotalCount = source.TotalCount
            };
        }

        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static Machine CreateFrom(this DomainModels.Machine source)
        {
            return new Machine
            {
                MachineId = source.MachineId,
                MachineName = source.MachineName,
                DefaultPaperId = source.DefaultPaperId,
                MachineCatId = source.MachineCatId,
                Maximumsheetheight = source.maximumsheetheight,
                Maximumsheetweight = source.maximumsheetweight,
                Maximumsheetwidth = source.maximumsheetwidth,
                Minimumsheetheight = source.minimumsheetheight,
                Minimumsheetwidth = source.minimumsheetwidth
            };

        }
        
        #endregion

    }
}