using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class SectionInkCoverageMapper
    {

        public static SectionInkCoverage CreateFrom(this DomainModels.SectionInkCoverage source)
        {
            return new SectionInkCoverage
            {
                Id = source.Id,
                CoverageGroupId = source.CoverageGroupId,
                InkId = source.InkId,
                InkOrder = source.InkOrder,
                SectionId = source.SectionId,
                Side = source.Side,
                CoverageRate = source.CoverageRate
            };
        }

        public static DomainModels.SectionInkCoverage CreateFrom(this SectionInkCoverage source)
        {
            return new DomainModels.SectionInkCoverage
            {
                Id = source.Id,
                CoverageGroupId = source.CoverageGroupId,
                InkId = source.InkId,
                InkOrder = source.InkOrder,
                SectionId = source.SectionId,
                Side = source.Side,
                CoverageRate = source.CoverageRate
            };
        }
    }
}