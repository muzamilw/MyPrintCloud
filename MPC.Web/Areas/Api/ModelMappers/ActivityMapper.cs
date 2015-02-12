using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;


namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Activity Mapper
    /// </summary>
    public static class ActivityMapper
    {
        #region Public
        /// <summary>
        /// Create From Web Model
        /// </summary>
        public static DomainModels.Activity CreateFrom(this Activity source)
        {
            return new DomainModels.Activity
            {
                ActivityId = source.ActivityId,
                ActivityTypeId = source.ActivityTypeId,
                ContactId = source.ContactId,
                SourceId = source.SourceId,
                ActivityEndTime = source.ActivityEndTime,
                ActivityNotes = source.ActivityNotes,
                ActivityRef = source.ActivityRef,
                ActivityStartTime = source.ActivityStartTime,
                CompanyId = source.CompanyId,
                FlagId = source.FlagId,
                IsCustomerActivity = source.IsCustomerActivity,
                IsPrivate = source.IsPrivate,
                ProductTypeId = source.ProductTypeId,
                SystemUserId = source.SystemUserId,
            };
        }

        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static Activity CreateFrom(this DomainModels.Activity source)
        {
            return new Activity
            {
                ActivityId = source.ActivityId,
                ActivityTypeId = source.ActivityTypeId,
                ContactId = source.ContactId,
                SourceId = source.SourceId,
                ActivityEndTime = source.ActivityEndTime,
                ActivityNotes = source.ActivityNotes,
                ActivityRef = source.ActivityRef,
                ActivityStartTime = source.ActivityStartTime,
                CompanyId = source.CompanyId,
                FlagId = source.FlagId,
                IsCustomerActivity = source.IsCustomerActivity,
                IsPrivate = source.IsPrivate,
                ProductTypeId = source.ProductTypeId,
                SystemUserId = source.SystemUserId,
                CompanyName = source.Company != null ? source.Company.Name : string.Empty,
                IsCustomerType = source.Company != null ? source.Company.IsCustomer : 1
            };
        }

        #endregion
    }
}