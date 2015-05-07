﻿using MPC.Interfaces.MISServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class SystemUserController : ApiController
    {
        private readonly ISystemUserService _ISystemUserService;
        public SystemUserController(ISystemUserService ISystemUserServic)
        {
            this._ISystemUserService = ISystemUserServic;
        }
        // GET: Api/SystemUser
        public bool Get(System.Guid Id, string Email, string FullName,int OrganizationId)
        {
            try
            {
                

                return _ISystemUserService.Update(Id, Email, FullName, OrganizationId);
               


            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        
        public string Get(string id)
        {
            return "";
        }
    }
}