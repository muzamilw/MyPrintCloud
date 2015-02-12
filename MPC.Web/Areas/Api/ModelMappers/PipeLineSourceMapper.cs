using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// PipeLine Source Mapper
    /// </summary>
    public static class PipeLineSourceMapper
    {
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static PipeLineSource CreateFrom(this DomainModels.PipeLineSource source)
        {
            return new PipeLineSource
            {
                SourceId = source.SourceId,
                Description = source.Description
            };
        }
    }
}