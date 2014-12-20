using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Email Event Mapper
    /// </summary>
    public static class EmailEventMapper
    {
        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static EmailEvent CreateFrom(this DomainModels.EmailEvent source)
        {
            return new EmailEvent
            {
                EmailEventId = source.EmailEventId,
                EventName = source.EventName,
            };
        }
        #endregion
    }
}