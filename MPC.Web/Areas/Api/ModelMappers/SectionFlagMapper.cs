using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Section Flag Mapper
    /// </summary>
    public static class SectionFlagMapper
    {
        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static SectionFlagDropDown CreateFromDropDown(this DomainModels.SectionFlag source)
        {
            return new SectionFlagDropDown
            {
                SectionFlagId = source.SectionFlagId,
                FlagName = source.FlagName,
                FlagColor = source.FlagColor,
            };
        }
        #endregion
    }
}