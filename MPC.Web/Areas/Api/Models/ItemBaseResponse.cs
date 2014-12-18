using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Item Base Response Web Api Model
    /// </summary>
    public class ItemBaseResponse
    {
        /// <summary>
        /// Cost Centres
        /// </summary>
        public IEnumerable<CostCentre> CostCentres { get; set; }

    }
}