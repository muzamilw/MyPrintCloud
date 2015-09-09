using System;
using System.Collections.Generic;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Activity Repository Interface
    /// </summary>
    public interface IDiscountVoucherRepository : IBaseRepository<DiscountVoucher, long>
    {

        /// <summary>
        /// Discount Voucher List view 
        /// </summary>
        DiscountVoucherListViewResponse GetDiscountVoucherListView(DiscountVoucherRequestModel requestModel);
        List<DiscountVoucher> GetStoreDefaultDiscountVouchers(long StoreId, long OrganisationId);
        DiscountVoucher GetDiscountVoucherById(long DiscountVoucherId);
        DiscountVoucher GetDiscountVoucherByCouponCode(string DiscountVoucherName, long StoreId, long OrganisationId);

        long IsStoreHaveFreeShippingDiscountVoucher(long StoreId, long OrganisationId);
        DiscountVoucher UpdateVoucher(DiscountVoucher discountVoucher);

        DiscountVoucher GetDiscountVoucherByVoucherId(long DVId);

        DiscountVoucher CreateDiscountVoucher(DiscountVoucher discountVoucher);
        bool isCouponVoucher(long DiscountVoucherId);

        List<DiscountVoucher> getDiscountVouchersByCompanyId(long companyId);
    }
}
