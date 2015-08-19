
using System;
using System.Collections.Generic;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.WebBase.Mvc;

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
                var isValid = _myOrganizationService.CanStoreMakeLive();
                return isValid;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        
        public bool Get(int organisationId, int storesCount, bool isTrial, int misOrderCount, int webOrderCount)
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
        //[ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrganisation })]
        public bool Post(int organisationId, int storesCount, bool isTrial, int misOrderCount, int webOrderCount, DateTime billingDate)
        {
            try
            {
                _myOrganizationService.UpdateOrganisationLicensing(organisationId, storesCount, isTrial, misOrderCount, webOrderCount, billingDate);
                return true;

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        
    }
}