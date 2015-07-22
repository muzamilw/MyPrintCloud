using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Discount Vaoucher API Controller
    /// </summary>
    public class DiscountVaoucherDetailController : ApiController
    {
        #region Private

        private readonly ICompanyService companyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DiscountVaoucherDetailController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        #endregion

        #region Public


        [ApiException]
        [HttpPost]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public DiscountVoucher Post(DiscountVoucher discountVoucher)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            return companyService.SaveDiscountVoucher(discountVoucher.CreateFrom()).CreateFromDetail();
        }

        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public DiscountVoucher Get(long discountVoucherId)
        {
            return companyService.GetDiscountVoucherById(discountVoucherId).CreateFromDetail();
        }


        //[ApiException]
        //[ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        //[CompressFilterAttribute]
        //[HttpDelete]
        //public int Delete(FieldVariable fieldVariable)
        //{
        //    if (fieldVariable == null || !ModelState.IsValid)
        //    {
        //        throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
        //    }
        //    companyService.DeleteFieldVariable(fieldVariable.VariableId);
        //    return 1;
        //}
        #endregion

    }
}