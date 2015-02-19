using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using System.Net;
using System.Web;
using System.Web.Http;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Controller to get the base data for contacts
    /// </summary>
    public class CompanyContactBaseForCrmController : ApiController
    {
        #region Private
        private readonly ICompanyContactService companyContactService;
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyContactBaseForCrmController(ICompanyContactService companyContactService)
        {
            this.companyContactService = companyContactService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Addresses / Compnay Contacts
        /// </summary>
        public CompanyBaseResponse Get()
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
           return companyContactService.GetBaseData().CreateFrom();
        }
        #endregion
    }
}