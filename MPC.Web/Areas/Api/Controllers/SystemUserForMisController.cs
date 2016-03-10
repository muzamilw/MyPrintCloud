using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MPC.ExceptionHandling;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.Models.DomainModels;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class SystemUserForMisController : ApiController
    {
        private readonly ISystemUserService _systemUserService;

        public SystemUserForMisController(ISystemUserService iSystemUserService)
        {
            if (iSystemUserService == null)
            {
                throw new ArgumentNullException("iSystemUserService");
            }

            this._systemUserService = iSystemUserService;
        }
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewDashboard })]
        [CompressFilter]
        public string Get()
        {
            try
            {
                return _systemUserService.GetEmailSignature();

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewDashboard })]
        [CompressFilter]
        public string Get(int id)
        {
          return  string.Empty;
        }

        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewDashboard })]
        [CompressFilter]
        public void Post(SystemUser value)
        {
            if (value == null || !ModelState.IsValid)
            {
                throw new MPCException("Invalid Data ", 0);
            }
            try
            {
                _systemUserService.UpdateEmailSignature(value.EmailSignature);

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}