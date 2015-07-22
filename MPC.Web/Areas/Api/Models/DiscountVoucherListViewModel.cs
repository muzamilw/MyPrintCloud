
namespace MPC.MIS.Areas.Api.Models
{
    public class DiscountVoucherListViewModel
    {
        public long DiscountVoucherId { get; set; }

        public string VoucherName { get; set; }

        public string CouponCode { get; set; }
        public int? DiscountType { get; set; }
        public double DiscountRate { get; set; }

        public int? CouponUseType { get; set; }
     
    }
}