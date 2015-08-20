
using MPC.Models.DomainModels;
using System.Collections.Generic;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Orders response model
    /// </summary>
    public class OrdersForCrmResponse
    {
        /// <summary>
        /// Count of Orders
        /// </summary>
        public int RowCount { get; set; }
        public string CurrecySymbol { get; set; }
        /// <summary>
        /// List of Orders
        /// </summary>
        public IEnumerable<Estimate> Orders { get; set; }
    }
}
