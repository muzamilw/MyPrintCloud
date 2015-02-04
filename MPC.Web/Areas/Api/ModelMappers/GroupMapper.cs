using MPC.MIS.Areas.Api.Models;
using MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Group Mapper
    /// </summary>
    public static class GroupMapper
    {
        #region Public

        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static GroupForCampaign CreateFrom(this Group source)
        {
            return new GroupForCampaign()
            {
                GroupId = source.GroupId,
                GroupName = source.GroupName
            };
        }
        #endregion
    }
}