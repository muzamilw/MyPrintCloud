using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;

    /// <summary>
    /// Item Vdp Price Mapper
    /// </summary>
    public static class CostCentreMapper
    {
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static CostCentre CreateFrom(this DomainModels.CostCentre source)
        {
            return new CostCentre
            {
                CostCentreId = source.CostCentreId,
                Name = source.Name,
                Type = source.Type,
                TypeName = source.CostCentreType != null ? source.CostCentreType.TypeName : string.Empty
            };
        }
        
    }
}