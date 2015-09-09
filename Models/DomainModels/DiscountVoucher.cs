using System;
using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Discount Voucher Domain Model
    /// </summary>
    public class DiscountVoucher
    {
        public long DiscountVoucherId { get; set; }
        public string VoucherCode { get; set; }
        public DateTime? ValidFromDate { get; set; }
        public DateTime? ValidUptoDate { get; set; }
        public long? OrderId { get; set; }
        public double DiscountRate { get; set; }
        public DateTime? ConsumedDate { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CompanyId { get; set; }
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
        public double? MinRequiredOrderPrice { get; set; }
        public double? MaxRequiredOrderPrice { get; set; }
        public long? CustomerId { get; set; }
        public bool? IsSingleUseRedeemed { get; set; }
        public bool? IsQtySpan { get; set; }
        public long? OrganisationId { get; set; }


        public virtual ICollection<ItemsVoucher> ItemsVouchers { get; set; }
        public virtual ICollection<ProductCategoryVoucher> ProductCategoryVouchers { get; set; }
    }
}
