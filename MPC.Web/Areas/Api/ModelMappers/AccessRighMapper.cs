using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class AccessRighMapper
    {
        public static AccessRight CreateFrom(this DomainModels.AccessRight source)
        {
            return new AccessRight()
            {
                RightId = source.RightId,
                RightName = source.RightName,
                CanEdit = source.CanEdit,
                IsSelected = source.IsSelected,
                Description = source.Description

            };
        }
        public static DomainModels.AccessRight CreateFrom(this AccessRight source)
        {
            return new DomainModels.AccessRight()
            {
                RightId = source.RightId,
                RightName = source.RightName,
                CanEdit = source.CanEdit,
                IsSelected = source.IsSelected,
                Description = source.Description
            };
        }
    }
}