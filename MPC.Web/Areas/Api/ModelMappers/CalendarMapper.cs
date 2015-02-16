using System.Linq;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;
using ResponseModels = MPC.Models.ResponseModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Calendar Mapper
    /// </summary>
    public static class CalendarMapper
    {
        #region
        /// <summary>
        ///  Crete From Domain Model
        /// </summary>
        public static CalendarBaseResponse CreateFrom(this ResponseModels.CalendarBaseResponse source)
        {
            return new CalendarBaseResponse()
            {
                SectionFlags = source.SectionFlags != null ? source.SectionFlags.Select(s => s.CreateFromDropDown()).ToList() : null,
                PipeLineProducts = source.PipeLineProducts != null ? source.PipeLineProducts.Select(pl => pl.CreateFrom()).ToList() : null,
                PipeLineSources = source.PipeLineSources != null ? source.PipeLineSources.Select(pls => pls.CreateFrom()).ToList() : null,
                SystemUsers = source.SystemUsers != null ? source.SystemUsers.Select(su => su.CreateFrom()).ToList() : null,
                ActivityTypes = source.ActivityTypes != null ? source.ActivityTypes.Select(su => su.CreateFromDropDown()).ToList() : null,
                LoggedInUserId = source.LoggedInUserId,
            };
        }
        #endregion
    }
}