﻿
using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// APi model for response
    /// </summary>
    public class InvoiceListResponseModel
    {

        /// <summary>
        /// Row Count
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// List of Invoices
        /// </summary>
        public IEnumerable<InvoicesListModel> Invoices { get; set; }
    }
}