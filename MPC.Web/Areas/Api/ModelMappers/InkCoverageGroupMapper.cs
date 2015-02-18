using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class InkCoverageGroupMapper
    {
        public static InkCoverageGroup CreateFrom(this DomainModels.InkCoverageGroup source)
        {
            return new InkCoverageGroup
            {
                CoverageGroupId = source.CoverageGroupId,
                GroupName = source.GroupName,
                Percentage = source.Percentage,
                IsFixed = source.IsFixed,
                SystemSiteId = source.SystemSiteId
            };

        }
    }
}