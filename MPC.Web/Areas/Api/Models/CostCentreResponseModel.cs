using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class CostCentreResponseModel
    {
        /// <summary>
        /// Row Count
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// List of Company Cost Centres
        /// </summary>
        public IEnumerable<CostCentre> CostCentres { get; set; }
    }
}