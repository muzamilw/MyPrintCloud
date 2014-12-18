using System.Collections.Generic;
using System.Linq;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.ResponseModels;

    /// <summary>
    /// Item Vdp Price Mapper
    /// </summary>
    public static class ItemBaseResponseMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ItemBaseResponse CreateFrom(this DomainModels.ItemBaseResponse source)
        {
            return new ItemBaseResponse
            {
                CostCentres = source.CostCentres != null ? source.CostCentres.Select(cc => cc.CreateFrom()) : new List<CostCentre>()
            };
        }
        
    }
}