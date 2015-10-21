using System;
using System.IO;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.Models;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class StoreCssController : ApiController
    {
        #region Private

        private readonly ICompanyService _companyService;

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public StoreCssController(ICompanyService companyService)
        {
            if (companyService == null)
            {
                throw new ArgumentNullException("companyService");
            }
            this._companyService = companyService;
        }

        #endregion

        #region Public
        [ApiException]
        
        public bool Get()
        {

            return false;
        }
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        public string Get(long companyId)
        {
            try
            {
                return _companyService.GetCompanyCss(companyId);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        #endregion
        
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        public Company Post(UpdateCssRequest request)
        {
            try
            {
               _companyService.UpdateCompanyCss(request.CustomCss, request.CompanyId);
                return null;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
