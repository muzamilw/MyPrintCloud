
using System;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class LiveStoresController : ApiController
    {
        #region Private

        private readonly ICompanyService _CompanyService;

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public LiveStoresController(ICompanyService companyService)
        {
            if (companyService == null)
            {
                throw new ArgumentNullException("companyService");
            }
            this._CompanyService = companyService;
        }

        #endregion

        #region Public
        [ApiException]
        //[ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrganisation })]
        public string Get()
        {
            try
            {
                return _CompanyService.GetLiveStoresJason();
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public bool Get(long id)
        {
            try
            {
                return false;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        #endregion
        [ApiException]
        public bool Post()
        {
            try
            {
               return false;

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        
    }
}