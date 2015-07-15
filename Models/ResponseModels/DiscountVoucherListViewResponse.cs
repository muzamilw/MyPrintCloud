using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Domain model
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
        public IEnumerable<DiscountVoucher> DiscountVouchers { get; set; }
    }
}
