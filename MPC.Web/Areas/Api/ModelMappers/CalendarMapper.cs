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
                SectionFlags = source.SectionFlags.Select(s => s.CreateFromDropDown()).ToList(),
                CompanyContacts = source.CompanyContacts.Select(cc => cc.CreateFromDropDown()).ToList(),
                PipeLineProducts = source.PipeLineProducts.Select(pl => pl.CreateFrom()).ToList(),
                PipeLineSources = source.PipeLineSources.Select(pls => pls.CreateFrom()).ToList(),
                SystemUsers = source.SystemUsers.Select(su => su.CreateFrom()).ToList(),
                ActivityTypes = source.ActivityTypes.Select(su => su.CreateFromDropDown()).ToList(),
                LoggedInUserId = source.LoggedInUserId,
            };
        }
        #endregion
    }
}