using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    public class PurchaseResponseModel
    {
        /// <summary>
        /// Row Count
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// List of Purchases
        /// </summary>
        public IEnumerable<PurchaseListView> PurchasesList{ get; set; }
        public string HeadNote { get; set; }
        public string FootNote { get; set; }
    }
}