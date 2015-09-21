using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class RealEstateListViewResponse
    {
        /// <summary>
        /// Row Count
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// List of Discount Vouchers
        /// </summary>
        public IEnumerable<vw_RealEstateProperties> RealEstateList { get; set; }
    }
}