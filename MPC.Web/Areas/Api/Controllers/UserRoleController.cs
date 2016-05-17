using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MPC.ExceptionHandling;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.Models;
using MPC.WebBase.Mvc;
using SystemUser = MPC.Models.DomainModels.SystemUser;
using MPC.MIS.Areas.Api.ModelMappers;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class UserRoleController : ApiController
    {
        private readonly IRoleService _roleService;

        public UserRoleController(IRoleService roleService)
        {
            if (roleService == null)
            {
                throw new ArgumentNullException("roleService");
            }

            this._roleService = roleService;
        }
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewDashboard })]
        [CompressFilter]
        public UserRoleResponse Get()
        {
            try
            {
                var response = _roleService.GetUserRoles();
                var accessRights = _roleService.GetUserAccessRights();
                return new UserRoleResponse
                {
                    UserRoles = response.Select(r => r.CreateFrom()).ToList(),
                    AccessRights = accessRights.Select(a => a.CreateFrom()).ToList()
                };

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
        public void Post(Role value)
        {
            if (value == null || !ModelState.IsValid)
            {
                throw new MPCException("Invalid Data ", 0);
            }
            try
            {
                _roleService.SaveUserRole(value.CreateFrom());

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