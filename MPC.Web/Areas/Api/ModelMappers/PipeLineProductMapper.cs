using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Pipe Line Product Mapper
    /// </summary>
    public static class PipeLineProductMapper
    {
        #region Public
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static PipeLineProduct CreateFrom(this DomainModels.PipeLineProduct source)
        {
            return new PipeLineProduct
            {
                ProductId = source.ProductId,
                Description = source.Description
            };
        }

        #endregion
    }
}