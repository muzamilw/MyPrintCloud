using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;


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
                IsSystem = source.IsSystem
            };
        }

        public static DomainModels.CostCentreType CreateFrom(this CostCentreType source)
        {
            return new DomainModels.CostCentreType
            {
                TypeId = source.TypeId,
                TypeName = source.TypeName,
                IsExternal = source.IsExternal?? 0,
                IsSystem = source.IsSystem ?? 0
            };
        }
    }
}