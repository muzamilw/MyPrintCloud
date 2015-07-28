
using System;
using System.Collections.Generic;
using System.Web.Http;
using MPC.Interfaces.MISServices;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class OrganisationLicensingController : ApiController
    {
        #region Private

        private readonly IMyOrganizationService _myOrganizationService;

        #endregion
         #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public OrganisationLicensingController(IMyOrganizationService myOrganizationService)
        {
            if (myOrganizationService == null)
            {
                throw new ArgumentNullException("myOrganizationService");
            }
            this._myOrganizationService = myOrganizationService;
        }

        #endregion

        #region Public
        public bool Get()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        #endregion

        [HttpPost]
        public bool Post([FromUri]long organisationId, int storesCount, bool isTrial)
        {
            try
            {
                _myOrganizationService.UpdateOrganisationLicensing(organisationId, storesCount, isTrial);
                return true;

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        
    }
}