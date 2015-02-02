using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Campaign Email Variable Mapper
    /// </summary>
    public static class CampaignEmailVariableMapper
    {
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static CampaignEmailVariable CreateFrom(this DomainModels.CampaignEmailVariable source)
        {
            return new CampaignEmailVariable()
            {
                VariableId = source.VariableId,
                VariableName = source.VariableName
            };
        }
    }
}