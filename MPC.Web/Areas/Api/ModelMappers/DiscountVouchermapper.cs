using System.Linq;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

using System.Collections.Generic;


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
                CouponCode = source.CouponCode,
                DiscountType = source.DiscountType,
                DiscountRate = source.DiscountRate,
                CouponUseType = source.CouponUseType,
                HasCoupon = source.HasCoupon
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

        /// <summary>
        /// Create From Api Model 
        /// </summary>
        public static DomainModels.DiscountVoucher CreateFrom(this DiscountVoucher source)
        {

            return new DomainModels.DiscountVoucher
            {
                DiscountVoucherId = source.DiscountVoucherId,
                VoucherName = source.VoucherName,
                VoucherCode = string.Empty,
                CouponCode = source.CouponCode,
                DiscountType = source.DiscountType,
                DiscountRate = source.DiscountRate,
                CouponUseType = source.CouponUseType,
                HasCoupon = source.HasCoupon,
                IsOrderPriceRequirement = source.IsOrderPriceRequirement,
                IsQtyRequirement = source.IsQtyRequirement,
                IsQtySpan = source.IsQtySpan,
                IsTimeLimit = source.IsTimeLimit,
                IsUseWithOtherCoupon = source.IsUseWithOtherCoupon,
                MaxRequiredOrderPrice = source.MaxRequiredOrderPrice,
                MaxRequiredQty = source.MaxRequiredQty,
                MinRequiredOrderPrice = source.MinRequiredOrderPrice,
                MinRequiredQty = source.MinRequiredQty,
                ValidFromDate = source.ValidFromDate,
                ValidUptoDate = source.ValidUptoDate,
                CompanyId = source.CompanyId,
                IsEnabled = source.IsEnabled,
                ProductCategoryVouchers = source.ProductCategoryVouchers != null ? source.ProductCategoryVouchers.Select(pci => pci.CreateFrom()).ToList() :
              new List<DomainModels.ProductCategoryVoucher>(),
                ItemsVouchers = source.ItemsVouchers != null ? source.ItemsVouchers.Select(pci => pci.CreateFrom()).ToList() :
               new List<DomainModels.ItemsVoucher>(),
            };
        }
        /// <summary>
        /// Create From Domain Model 
        /// </summary>
        public static DiscountVoucher CreateFromDetail(this DomainModels.DiscountVoucher source)
        {

            return new DiscountVoucher
            {
                DiscountVoucherId = source.DiscountVoucherId,
                VoucherName = source.VoucherName,
                CouponCode = source.CouponCode,
                DiscountType = source.DiscountType,
                DiscountRate = source.DiscountRate,
                CouponUseType = source.CouponUseType,
                HasCoupon = source.HasCoupon,
                IsOrderPriceRequirement = source.IsOrderPriceRequirement,
                IsQtyRequirement = source.IsQtyRequirement,
                IsQtySpan = source.IsQtySpan,
                IsTimeLimit = source.IsTimeLimit,
                IsUseWithOtherCoupon = source.IsUseWithOtherCoupon,
                MaxRequiredOrderPrice = source.MaxRequiredOrderPrice,
                MaxRequiredQty = source.MaxRequiredQty,
                MinRequiredOrderPrice = source.MinRequiredOrderPrice,
                MinRequiredQty = source.MinRequiredQty,
                ValidFromDate = source.ValidFromDate,
                ValidUptoDate = source.ValidUptoDate,
                CompanyId = source.CompanyId,
                IsEnabled = source.IsEnabled,
                ProductCategoryVouchers = source.ProductCategoryVouchers != null ? source.ProductCategoryVouchers.Select(pci => pci.CreateFrom()) : new List<ProductCategoryVoucher>(),
                ItemsVouchers = source.ItemsVouchers != null ? source.ItemsVouchers.Select(pci => pci.CreateFrom()) : new List<ItemsVoucher>(),

            };
        }
        #endregion
    }
}