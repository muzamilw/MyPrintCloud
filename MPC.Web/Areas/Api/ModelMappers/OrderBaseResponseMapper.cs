using System.Collections.Generic;
using System.Linq;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.ResponseModels;

    /// <summary>
    /// Order Base Response WebApi Mapper
    /// </summary>
    public static class OrderBaseResponseMapper
    {
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static OrderBaseResponse CreateFrom(this DomainModels.OrderBaseResponse source)
        {
            return new OrderBaseResponse
            {
                SectionFlags = source.SectionFlags != null ? source.SectionFlags.Select(cc => cc.CreateFromDropDown()) : 
                new List<SectionFlagDropDown>(),
                SystemUsers = source.SystemUsers != null ? source.SystemUsers.Select(cc => cc.CreateFrom()) : 
                new List<SystemUserDropDown>(),
                PipeLineSources = source.PipeLineSources != null ? source.PipeLineSources.Select(cc => cc.CreateFrom()) :
                new List<PipeLineSource>()
            };
        }
        
    }
}