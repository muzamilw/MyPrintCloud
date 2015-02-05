using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Response to Get Customer Request
    /// </summary>
    public class CustomerResponse
    {
        /// <summary>
        /// Customers for listing
        /// </summary>
        public IEnumerable<CustomerListViewModel> Customers { get; set; }

        /// <summary>
        /// Row Count
        /// </summary>
        public int RowCount { get; set; }
    }
}