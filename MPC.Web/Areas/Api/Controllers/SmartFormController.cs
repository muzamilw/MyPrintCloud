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
    /// Smart Form Api Controller
    /// </summary>
    public class SmartFormController : ApiController
    {
        #region Private

        private readonly ICompanyService companyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SmartFormController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        #endregion

        #region Public

        /// <summary>
        /// Get Smart Forms
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public SmartFormResponse Get([FromUri] SmartFormRequestModel request)
        {
            return companyService.GetSmartForms(request).CreateFrom();
        }

        [ApiException]
        [HttpPost]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public long Post(SmartForm smartForm)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            return companyService.SaveSmartForm(smartForm.CreateFrom());
        }

        #endregion
    }
}