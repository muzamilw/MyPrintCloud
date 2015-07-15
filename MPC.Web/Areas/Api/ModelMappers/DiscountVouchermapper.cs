using System.Linq;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;


namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Discount Voucher Mapper
    /// </summary>
    public static class DiscountVoucherMapper
    {
        #region Public
       

        /// <summary>
        /// Create From Domain Model [List view ]
        /// </summary>
        public static DiscountVoucherListViewModel CreateFrom(this DomainModels.DiscountVoucher source)
        {
            
            return new DiscountVoucherListViewModel
            {
               DiscountVoucherId = source.DiscountVoucherId,
               VoucherName = source.VoucherName,
               CouponCode = source.CouponCode , 
               DiscountType = source.DiscountType,
               DiscountRate = source.DiscountRate,
               CouponUseType = source.CouponUseType
            };
        }
        
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static DiscountVoucherListViewResponse CreateFromListView(this  MPC.Models.ResponseModels.DiscountVoucherListViewResponse source)
        {
            return new DiscountVoucherListViewResponse
            {
                RowCount = source.RowCount,
                DiscountVoucherListView = source.DiscountVouchers.Select(voucher => voucher.CreateFrom())
            };
        }

        #endregion
    }
}