using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Discount Vaoucher API Controller
    /// </summary>
    public class DiscountVaoucherController : ApiController
    {
        #region Private

        private readonly ICompanyService companyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DiscountVaoucherController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        #endregion

        #region Public


        [ApiException]
        [HttpPost]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public long Post(FieldVariable fieldVariable)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

           // return companyService.SaveDiscountVaoucher(fieldVariable.CreateFrom());
            return 0;
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