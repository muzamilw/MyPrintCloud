using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class CrmSupplierResponse
    {
        /// <summary>
        /// Row Count
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// List of Companies
        /// </summary>
        public IEnumerable<CrmSupplierListViewModel> Companies { get; set; }
    }
}