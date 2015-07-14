using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.MISServices
{
    public interface IDiscountVoucherService 
    {
        /// <summary>
        /// Discount Voucher List view 
        /// </summary>
        DiscountVoucherListViewResponse GetDiscountVoucherListView(DiscountVoucherRequestModel requestModel); 
    }
}
