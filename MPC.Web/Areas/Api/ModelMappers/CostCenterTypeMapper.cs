using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;
using System.Linq;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class CostCenterTypeMapper
    {
        public static CostCentreType CreateFrom(this DomainModels.CostCentreType source)
        {
            return new CostCentreType
            {
                TypeId = source.TypeId,
                TypeName = source.TypeName,
                IsExternal = source.IsExternal,
                IsSystem = source.IsSystem,
                CostCentres = source.CostCentres != null ? source.CostCentres.Select(x => x.CreateFrom()).ToList() : null
            };
        }

        public static DomainModels.CostCentreType CreateFrom(this CostCentreType source)
        {
            return new DomainModels.CostCentreType
            {
                TypeId = source.TypeId,
                TypeName = source.TypeName,
                IsExternal = source.IsExternal?? 0,
                IsSystem = source.IsSystem ?? 0,
                CostCentres = source.CostCentres != null ? source.CostCentres.Select(x => x.CreateFrom()).ToList() : null
            };
        }
    }
}