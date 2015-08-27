using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
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
    public class ExportController : ApiController
    {
        #region Private
        private readonly ICompanyContactService companyContactService;

        #endregion
        #region Constructor
        public ExportController(ICompanyContactService CompanyContactService)
        {
            this.companyContactService = CompanyContactService;
        }
        #endregion


        /// <summary>
        /// Get Product Categories for Company
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore, SecurityAccessRight.CanViewProduct })]
        [CompressFilterAttribute]
        public string Get(long id)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            long? companyId = id > 0 ? id : (long?)null;

           return companyContactService.ExportCSV(companyId ?? 0,false);
            // return itemService.GetProductCategoriesForCompany(companyId).CreateFrom();
        }

        

    }
}
