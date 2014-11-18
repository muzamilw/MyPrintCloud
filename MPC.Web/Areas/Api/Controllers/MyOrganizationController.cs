using System;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.IServices;
using MPC.Web.ModelMappers;
using MPC.Web.Models;
using MPC.WebBase.Mvc;

namespace MPC.Web.Areas.Api.Controllers
{
    /// <summary>
    /// My Organization Api Controller
    /// </summary>
    public class MyOrganizationController : ApiController
    {
        #region Private

        private readonly IMyOrganizationService myOrganizationService;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MyOrganizationController(IMyOrganizationService myOrganizationService)
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
        /// Get My Organization By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Models.CompanySites Get(int id)
        {
            return myOrganizationService.FindDetailById(id).CreateFrom();
        }

        /// <summary>
        /// Add/Update a My Organization
        /// </summary>
        [ApiException]
        public int Post(CompanySites companySites)
        {
            if (companySites == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return myOrganizationService.SaveCompanySite(companySites.CreateFrom());
        }
        #endregion

    }
}