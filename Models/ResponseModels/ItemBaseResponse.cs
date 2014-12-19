using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Item Base Response
    /// </summary>
    public class ItemBaseResponse
    {
        /// <summary>
        /// Cost Centres
        /// </summary>
        public IEnumerable<CostCentre> CostCentres { get; set; }

    }
}
