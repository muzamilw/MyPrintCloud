
using System;
namespace MPC.Models.DomainModels
{
    public class CompanyVoucherRedeem
    {
        /// <summary>
        /// Voucher Redeem Id
        /// </summary>
        public long VoucherRedeemId { get; set; }
        /// <summary>
        /// Company Id
        /// </summary>
        public long? CompanyId { get; set; }
        /// <summary>
        /// Discount Voucher Id
        /// </summary>
        public long? DiscountVoucherId { get; set; }
        /// <summary>
        /// Discount Redeem Date
        /// </summary>
        public DateTime? RedeemDate { get; set; }
        /// <summary>
        /// Company Id
        /// </summary>
        public long? ContactId { get; set; }
    }
}
