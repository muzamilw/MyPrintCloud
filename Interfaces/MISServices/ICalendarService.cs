using System.Collections.Generic;
using MPC.Models.DomainModels;
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
        /// Save Activity On Drop Or Resize IN Calendar
        /// </summary>
        void SaveActivityDropOrResize(Activity activity);

        /// <summary>
        /// Delete Activity
        /// </summary>
        void DeleteActivity(int activityId);

        /// <summary>
        ///  Activity Detail By ID
        /// </summary>
        Activity ActivityDetail(int activityId);

        /// <summary>
        /// Get Company Contacts By Company ID
        /// </summary>
        IEnumerable<CompanyContact> GetCompanyContactsByCompanyId(long companyId);

        /// <summary>
        /// Get Activities
        /// </summary>
        IEnumerable<Activity> GetActivities(ActivityRequestModel request);

        CompanyContactResponse GetCompanyContacts(CompanyContactForCalendarRequestModel request);
    }
}
