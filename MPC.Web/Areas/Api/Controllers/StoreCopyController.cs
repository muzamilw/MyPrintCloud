using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.Models;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.WebBase.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class StoreCopyController : ApiController
    {
          #region Private

        private readonly ICompanyService companyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public StoreCopyController(ICompanyService companyService)
        {
            if (companyService == null)
            {
                throw new ArgumentNullException("companyService");
            }
            this.companyService = companyService;
        }

        #endregion

        #region Public

        /// <summary>
        /// Post
        /// </summary>
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewProduct })]
        [CompressFilterAttribute]
        public Company Post(Company request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return companyService.CloneStore(request.CompanyId).CreateFrom();
        }

        #endregion

    }
}
