using System;
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
    /// My Organization Base Api Controller
    /// </summary>
    public class MyOrganizationBaseController : ApiController
    {
        #region Private

        private readonly IMyOrganizationService myOrganizationService;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MyOrganizationBaseController(IMyOrganizationService myOrganizationService)
        {
            if (myOrganizationService == null)
            {
                throw new ArgumentNullException("myOrganizationService");
            }
            this.myOrganizationService = myOrganizationService;
        }

        #endregion

        #region Public
        /// <summary>
        /// Get My Organization Base Data
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrganisation })]
        [CompressFilterAttribute]
        public MyOrganizationBaseResponse Get()
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return myOrganizationService.GetBaseData().CreateFrom();
        }

        public MyOrganizationBaseResponse GetRegionalSettings(bool isRegional)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return myOrganizationService.GetRegionalSettingBaseData().CreateFrom();
        }
        #endregion
    }
}