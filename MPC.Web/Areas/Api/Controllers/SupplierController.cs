using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.Models;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Supplier Api Controller
    /// </summary>
    public class SupplierController : ApiController
    {
        #region Private

        private readonly ICompanyService companyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SupplierController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        #endregion
        /// <summary>
        /// Add/Update Company
        /// </summary>
        [ApiException]
        public Company Post(Company company)
        {
            //if (company == null || !ModelState.IsValid)
            //{
            //    throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            //}
            //return companyService.SaveCompany(company.CreateFrom()).CreateFrom();
            return null;
        }
    }
}