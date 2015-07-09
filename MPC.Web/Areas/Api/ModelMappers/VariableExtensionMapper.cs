using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Variable Extension API Mapper
    /// </summary>
    public static class VariableExtensionMapper
    {
        #region Public

        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static VariableExtension CreateFrom(this DomainModels.VariableExtension source)
        {
            return new VariableExtension()
            {
                Id = source.Id,
                CollapsePostfix = source.CollapsePostfix,
                CollapsePrefix = source.CollapsePrefix,
                CompanyId = source.CompanyId,
                OrganisationId = source.OrganisationId,
                VariablePostfix = source.VariablePostfix,
                VariablePrefix = source.VariablePrefix,
            };
        }

        /// <summary>
        /// Create From API Model
        /// </summary>
        public static DomainModels.VariableExtension CreateFrom(this VariableExtension source)
        {
            return new DomainModels.VariableExtension()
            {
                Id = source.Id,
                CollapsePostfix = source.CollapsePostfix,
                CollapsePrefix = source.CollapsePrefix,
                CompanyId = source.CompanyId,
                VariablePostfix = source.VariablePostfix,
                VariablePrefix = source.VariablePrefix,
            };
        }
        #endregion
    }
}