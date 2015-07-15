using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.MISServices
{
    public class DiscountVoucherService : IDiscountVoucherService
    {
        #region Private

        private readonly IDiscountVoucherRepository _discountVoucherRepository;
        #endregion
        #region Constructor

        public DiscountVoucherService(IDiscountVoucherRepository discountVoucherRepository)
        {
            this._discountVoucherRepository = discountVoucherRepository;
        }

        #endregion
        #region Public
        /// <summary>
        /// Discount Voucher List view 
        /// </summary>
        public  DiscountVoucherListViewResponse GetDiscountVoucherListView(DiscountVoucherRequestModel requestModel)
        {
            return _discountVoucherRepository.GetDiscountVoucherListView(requestModel);
        }
        #endregion

    }
}
