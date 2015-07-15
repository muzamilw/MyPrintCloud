using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Web model
    /// </summary>
    public class DiscountVoucherListViewResponse
    {
        /// <summary>
        /// Row Count
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// List of Discount Vouchers
        /// </summary>
        public IEnumerable<DiscountVoucherListViewModel> DiscountVoucherListView { get; set; }
    }
}