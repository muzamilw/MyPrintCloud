using System.Collections.Generic;
using System.Linq;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.ResponseModels;
    public static class InquiryBaseResponseMapper
    {
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static OrderBaseResponse CreateFrom(this DomainModels.InquiryBaseResponse source)
        {
            return new OrderBaseResponse
            {
                SectionFlags = source.SectionFlags != null ? source.SectionFlags.Select(cc => cc.CreateFromDropDown()) :
                new List<SectionFlagDropDown>()
            };
        }
    }
}