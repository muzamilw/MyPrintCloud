using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using APIDomainModels= MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
     public static class LookupMethodMapper
     {
        public static APIDomainModels.LookupMethod CreateFrom(this DomainModels.LookupMethod source)
        {
            return new APIDomainModels.LookupMethod
            {
                MethodId = source.MethodId,
                Name = source.Name,
                Type = source.Type,
                LockedBy = source.LockedBy,
                OrganisationId = source.OrganisationId,
                FlagId = source.FlagId,
                SystemSiteId = source.SystemSiteId



            };

        }
    }
}