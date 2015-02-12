
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class ActivityTypeMapper
    {
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static ActivityTypeDropDown CreateFromDropDown(this DomainModels.ActivityType source)
        {
            return new ActivityTypeDropDown
            {
                ActivityTypeId = source.ActivityTypeId,
                ActivityName = source.ActivityName
            };
        }
    }
}