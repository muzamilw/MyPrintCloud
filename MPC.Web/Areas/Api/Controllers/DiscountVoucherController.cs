using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Discount Voucher API Controller
    /// </summary>
    public class DiscountVoucherController : ApiController
    {
        #region Private

        private readonly IDiscountVoucherService _discountVoucherService;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DiscountVoucherController(IDiscountVoucherService discountVoucherService)
        {
            _discountVoucherService = discountVoucherService;
        }

        #endregion
        #region Public


        /// <summary>
        /// Get Activity Detail By ID
        /// </summary>
        public Models.DiscountVoucherListViewResponse Get([FromUri]DiscountVoucherRequestModel request)
        {
          return  _discountVoucherService.GetDiscountVoucherListView(request).CreateFromListView();
        }

        #endregion
    }
}