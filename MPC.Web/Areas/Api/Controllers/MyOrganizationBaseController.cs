using System;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.IServices;
using MPC.Web.ModelMappers;
using MPC.Web.ResponseModels;

namespace MPC.Web.Areas.Api.Controllers
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
        public MyOrganizationBaseResponse Get()
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return myOrganizationService.GetBaseData().CreateFrom();
        }
        #endregion
    }
}