
using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// APi model for response
    /// </summary>
    public class InvoiceRequestResponseModel
    {

        /// <summary>
        /// Row Count
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// List of Invoices
        /// </summary>
        public IEnumerable<Invoice> Invoices { get; set; }
        public string HeadNote { get; set; }
        public string FootNote { get; set; }
    }
}