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

        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static SectionFlag CreateFrom(this DomainModels.SectionFlag source)
        {
            return new SectionFlag
            {
                SectionId = source.SectionId,
                SectionFlagId = source.SectionFlagId,
                FlagName = source.FlagName,
                FlagColor = source.FlagColor,
                FlagDescription = source.flagDescription
            };
        }


        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static DomainModels.SectionFlag CreateFrom(this SectionFlag source)
        {
            return new DomainModels.SectionFlag
            {
                SectionId = source.SectionId,
                SectionFlagId = source.SectionFlagId,
                FlagName = source.FlagName,
                FlagColor = source.FlagColor,
                flagDescription = source.FlagDescription
            };
        }
        #endregion
    }
}