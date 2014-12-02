using DomainModels = MPC.Models.DomainModels;
using ApiModels = MPC.MIS.Models;

namespace MPC.MIS.ModelMappers
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
        public static ApiModels.SectionFlagDropDown CreateFromDropDown(this DomainModels.SectionFlag source)
        {
            return new ApiModels.SectionFlagDropDown
            {
                SectionFlagId = source.SectionFlagId,
                FlagName = source.FlagName,
                FlagColor = source.FlagColor,
            };
        }
        #endregion
    }
}