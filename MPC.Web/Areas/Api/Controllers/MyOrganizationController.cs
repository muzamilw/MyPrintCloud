using System;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.WebBase.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// My Organization Api Controller
    /// </summary>
    public class MyOrganizationController : ApiController
    {
        #region Private

        private readonly IMyOrganizationService myOrganizationService;
        private readonly ICompanyService myCompanyService;
       

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MyOrganizationController(IMyOrganizationService myOrganizationService, ICompanyService myCompanyService)
        {
            if (myOrganizationService == null)
            {
                throw new ArgumentNullException("myOrganizationService");
            }
            this.myOrganizationService = myOrganizationService;
            this.myCompanyService = myCompanyService;
        }

        #endregion

        #region Public
        /// <summary>
        /// Get Organization By Id
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrganisation })]
        [CompressFilterAttribute]
        public Organisation Get()
        {
            return myOrganizationService.GetOrganisationDetail().CreateFrom();
        }

        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrganisation })]
        [CompressFilterAttribute]
        public IEnumerable<Markup> Get(bool isMarkup)
        {
            return myOrganizationService.GetMarkups().Select(c => c.CreateFrom());
        }
        /// <summary>
        /// Add/Update a Organization
        /// </summary>
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrganisation })]
        [CompressFilterAttribute]
        public MyOrganizationSaveResponse Post(Organisation organisation)
        {
            if (organisation == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return myOrganizationService.SaveOrganization(organisation.CreateFrom()).CreateFrom();
        }


       
       
        #endregion

    }
}