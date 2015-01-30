using System.Collections.Generic;
using ApiModels = MPC.MIS.Areas.Api.Models;
using DomainResponseModel = MPC.Models.ResponseModels;
using DomainModels = MPC.Models.DomainModels;
using System.IO;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class MachineListMapper
    {
        public static ApiModels.MachineList ListViewModelCreateFrom(this DomainModels.Machine source)
        {
            byte[] bytes = null;
            if (source.Image != null && File.Exists(source.Image))
            {
                bytes = source.Image != null ? File.ReadAllBytes(source.Image) : null;
            }
            return new ApiModels.MachineList
            {
                MachineId= source.MachineId,
                MachineCatId= source.MachineCatId,
                MachineName = source.MachineName,
                Description = source.Description,
                maximumsheetwidth = source.maximumsheetwidth,
                maximumsheetheight= source.maximumsheetheight,
                minimumsheetwidth= source.minimumsheetwidth,
                minimumsheetheight = source.minimumsheetheight,
                Image = bytes
            };
        }
        
    }
}