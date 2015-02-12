﻿using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.MISServices
{

    /// <summary>
    /// Interface Calendar Service
    /// </summary>
    public interface ICalendarService
    {
        /// <summary>
        /// Get Base Data
        /// </summary>
        CalendarBaseResponse GetBaseData();

        /// <summary>
        /// Get Companies By Is Customer Type
        /// </summary>
        CompanySearchResponseForCalendar GetCompaniesByCustomerType(CompanyRequestModelForCalendar request);


        /// <summary>
        /// Save Activity
        /// </summary>
        int SaveActivity(Activity activity);

        /// <summary>
        /// Delete Activity
        /// </summary>
        void DeleteActivity(int activityId);
    }
}
