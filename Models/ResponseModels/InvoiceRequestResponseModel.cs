using MPC.Models.DomainModels;
using System.Collections.Generic;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Invoices Request Response Model
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
