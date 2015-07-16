using System;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Discount Voucher API Model
    /// </summary>
    public class DiscountVoucher
    {
        public long DiscountVoucherId { get; set; }
        public DateTime? ValidFromDate { get; set; }
        public DateTime? ValidUptoDate { get; set; }
        public long? CompanyId { get; set; }
        public double DiscountRate { get; set; }
        public string VoucherName { get; set; }
        public int? DiscountType { get; set; }
        public bool? HasCoupon { get; set; }
        public string CouponCode { get; set; }
        public int? CouponUseType { get; set; }
        public bool? IsUseWithOtherCoupon { get; set; }
        public bool? IsTimeLimit { get; set; }
        public bool? IsQtyRequirement { get; set; }
        public int? MinRequiredQty { get; set; }
        public int? MaxRequiredQty { get; set; }
        public bool? IsOrderPriceRequirement { get; set; }
        public int? MinRequiredOrderPrice { get; set; }
        public int? MaxRequiredOrderPrice { get; set; }
        public bool? IsQtySpan { get; set; }
    }
}