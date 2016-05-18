using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class RoleMapper
    {
        #region Public
        /// <summary>
        /// Create From Domain Model
        /// </summary>

        public static Role CreateFrom(this DomainModels.Role source)
        {
            return new Role()
            {
               RoleId = source.RoleId,
               RoleName = source.RoleName,
               RoleDescription = source.RoleDescription,
               IsCompanyLevel = source.IsCompanyLevel,
               Rolerights = source.Rolerights != null? source.Rolerights.Select(a => new RoleRight{RoleId = a.RoleId, RightId = a.RightId, RoleName = a.Role.RoleName, RightName = a.AccessRight.RightName}).ToList() : null

            };
        }
        public static DomainModels.Role CreateFrom(this Role source)
        {
            return new DomainModels.Role()
            {
                RoleId = source.RoleId,
                RoleName = source.RoleName,
                RoleDescription = source.RoleDescription,
                IsCompanyLevel = source.IsCompanyLevel,
                Rolerights = source.Rolerights != null ? source.Rolerights.Select(a => new DomainModels.Roleright { RoleId = a.RoleId, RightId = a.RightId}).ToList() : null
            };
        }

        
        #endregion
    }
}