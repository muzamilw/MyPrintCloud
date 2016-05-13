using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class SystemUserMapper
    {
        public static SystemUser CreateFromList(this DomainModels.SystemUser source)
        {
            return new SystemUser()
            {
                RoleId = source.RoleId,
                Email = source.Email,
                FullName = source.FullName,
                SystemUserId = source.SystemUserId,
                RoleName = source.RoleName
                
            };
        }
        public static DomainModels.SystemUser CreateFromList(this SystemUser source)
        {
            return new DomainModels.SystemUser()
            {
                RoleId = source.RoleId,
                Email = source.Email,
                FullName = source.FullName,
                SystemUserId = source.SystemUserId,
                RoleName = source.RoleName
            };
        }
        
    }
}