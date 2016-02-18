using System.Collections.Generic;
using ApiModels = MPC.MIS.Areas.Api.Models;
using DomainResponseModel = MPC.Models.ResponseModels;
using DomainModels = MPC.Models.DomainModels;
using System.IO;
using System.Linq;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class MachineListMapper
    {
        public static ApiModels.MachineList ListViewModelCreateFrom(this DomainModels.Machine source, IEnumerable<DomainModels.LookupMethod> lookupMthd)
        {
            byte[] bytes = null;
            if (source.Image != null && File.Exists(source.Image))
            {
                bytes = source.Image != null ? File.ReadAllBytes(source.Image) : null;
            }
            DomainModels.LookupMethod LookupMethod = source.LookupMethodId != null ? lookupMthd.Where(g => g.MethodId == source.LookupMethodId).FirstOrDefault() : null;
          //  DomainModels.LookupMethod LookupMethod = null;
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
                LookupMethodName = LookupMethod !=null? LookupMethod.Name:null,
                isSheetFed = source.isSheetFed,
                Image = bytes,
                LookupMethodId = source.LookupMethodId,
                IsSpotColor = source.IsSpotColor,
                IsDigital = source.IsDigitalPress
            };
        }
        
    }
}