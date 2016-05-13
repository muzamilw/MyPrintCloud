using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MPC.ExceptionHandling;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.WebBase.Mvc;


namespace MPC.MIS.Areas.Api.Controllers
{
    public class SystemUserListController : ApiController
    {
        private readonly ISystemUserService _systemUserService;
        private readonly IRoleService _roleService;

        public SystemUserListController(ISystemUserService iSystemUserService, IRoleService roleService)
        {
            if (iSystemUserService == null)
            {
                throw new ArgumentNullException("iSystemUserService");
            }
            if (roleService == null)
            {
                throw new ArgumentNullException("roleService");
            }

            this._systemUserService = iSystemUserService;
            this._roleService = roleService;
        }
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrganisation })]
        [CompressFilter]
        public SystemUsersResponse Get()
        {
            try
            {
                var users = _systemUserService.GetAllUserByOrganisation();
                var roles = _roleService.GetUserRoles();
                foreach (var user in users)
                {
                    var role = roles.FirstOrDefault(a => a.RoleId == user.RoleId);
                    user.RoleName = role != null ? role.RoleName : string.Empty;
                }
                return new SystemUsersResponse
                {
                    SystemUsers = users != null ? users.ToList().Select(a => a.CreateFromList()).ToList() : null,
                    UserRoles = roles != null ? roles.Select(a => a.CreateFrom()).ToList() : null
                };
                    

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrganisation })]
        [CompressFilter]
        public string Get(int id)
        {
          return  string.Empty;
        }

        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrganisation })]
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