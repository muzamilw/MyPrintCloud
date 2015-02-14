using System;
using System.Collections;
using System.Collections.Generic;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.MISServices
{
    /// <summary>
    /// Calendar Service
    /// </summary>
    public class CalendarService : ICalendarService
    {
        #region Private

        /// <summary>
        /// Private members
        /// </summary>
        private readonly ISystemUserRepository systemUserRepository;
        private readonly ICompanyContactRepository companyContactRepository;
        private readonly IPipeLineProductRepository pipeLineProductRepository;
        private readonly IPipeLineSourceRepository pipeLineSourceRepository;
        private readonly ISectionFlagRepository sectionFlagRepository;
        private readonly ICompanyRepository companyRepository;
        private readonly IActivityTypeRepository activityTypeRepository;
        private readonly IActivityRepository activityRepository;

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public CalendarService(ISystemUserRepository systemUserRepository, ICompanyContactRepository companyContactRepository,
            IPipeLineProductRepository pipeLineProductRepository, IPipeLineSourceRepository pipeLineSourceRepository,
            ISectionFlagRepository sectionFlagRepository, ICompanyRepository companyRepository, IActivityTypeRepository activityTypeRepository,
            IActivityRepository activityRepository)
        {
            this.systemUserRepository = systemUserRepository;
            this.companyContactRepository = companyContactRepository;
            this.pipeLineProductRepository = pipeLineProductRepository;
            this.pipeLineSourceRepository = pipeLineSourceRepository;
            this.sectionFlagRepository = sectionFlagRepository;
            this.companyRepository = companyRepository;
            this.activityTypeRepository = activityTypeRepository;
            this.activityRepository = activityRepository;
        }

        #endregion

        #region Public
        /// <summary>
        /// Get Base Data
        /// </summary>
        public CalendarBaseResponse GetBaseData()
        {
            return new CalendarBaseResponse
            {
                SystemUsers = systemUserRepository.GetAll(),
                PipeLineProducts = pipeLineProductRepository.GetAll(),
                PipeLineSources = pipeLineSourceRepository.GetAll(),
                ActivityTypes = activityTypeRepository.GetAll(),
                LoggedInUserId = activityTypeRepository.LoggedInUserId,
                SectionFlags = sectionFlagRepository.GetSectionFlagBySectionId((int)SectionEnum.CRM),
            };
        }

        /// <summary>
        /// Get Company Contacts By Company ID
        /// </summary>
        public IEnumerable<CompanyContact> GetCompanyContactsByCompanyId(long companyId)
        {
            return companyContactRepository.GetContactsByCompanyId(companyId);

        }
        /// <summary>
        /// Get Companies By Is Customer Type
        /// </summary>
        public CompanySearchResponseForCalendar GetCompaniesByCustomerType(CompanyRequestModelForCalendar request)
        {
            return companyRepository.GetByIsCustomerType(request);
        }

        /// <summary>
        /// Save Activity
        /// </summary>
        public int SaveActivity(Activity activity)
        {
            Activity dbVersionActivity = activityRepository.Find(activity.ActivityId);
            if (dbVersionActivity == null)
            {
                return AddActivity(activity);
            }
            else
            {
                return UpdateActivity(activity, dbVersionActivity);
            }
        }

        /// <summary>
        /// Add Activity
        /// </summary>
        public int AddActivity(Activity activity)
        {
            //activity.SystemUserId = activityRepository.LoggedInUserId;
            activity.ActivityTime = DateTime.Now;
            activityRepository.Add(activity);
            activityRepository.SaveChanges();
            return activity.ActivityId;
        }
        /// <summary>
        /// Update Activity
        /// </summary>
        public int UpdateActivity(Activity activity, Activity dbVersionActivity)
        {
            dbVersionActivity.ActivityTypeId = activity.ActivityTypeId;
            dbVersionActivity.ContactId = activity.ContactId;
            dbVersionActivity.SourceId = activity.SourceId;
            dbVersionActivity.ActivityEndTime = activity.ActivityEndTime;
            dbVersionActivity.ActivityNotes = activity.ActivityNotes;
            dbVersionActivity.ActivityRef = activity.ActivityRef;
            dbVersionActivity.ActivityStartTime = activity.ActivityStartTime;
            dbVersionActivity.CompanyId = activity.CompanyId;
            dbVersionActivity.FlagId = activity.FlagId;
            dbVersionActivity.IsCustomerActivity = activity.IsCustomerActivity;
            dbVersionActivity.IsPrivate = activity.IsPrivate;
            dbVersionActivity.ProductTypeId = activity.ProductTypeId;
            dbVersionActivity.SystemUserId = activity.SystemUserId;
            activityRepository.SaveChanges();
            return activity.ActivityId;
        }

        /// <summary>
        /// Save Activity On Drop Or Resize IN Calendar
        /// </summary>
        public void SaveActivityDropOrResize(Activity activity)
        {
            Activity dbVersionActivity = activityRepository.Find(activity.ActivityId);
            if (dbVersionActivity != null)
            {
                dbVersionActivity.ActivityEndTime = activity.ActivityEndTime;
                dbVersionActivity.ActivityStartTime = activity.ActivityStartTime;
                activityRepository.SaveChanges();
            }

        }
        /// <summary>
        /// Delete Activity
        /// </summary>
        public void DeleteActivity(int activityId)
        {
            Activity activity = activityRepository.Find(activityId);
            if (activity != null)
            {
                activityRepository.Delete(activity);
                activityRepository.SaveChanges();
            }
        }

        /// <summary>
        ///  Activity Detail By ID
        /// </summary>
        public Activity ActivityDetail(int activityId)
        {
            return activityRepository.Find(activityId);
        }

        /// <summary>
        /// Get Activities
        /// </summary>
        public IEnumerable<Activity> GetActivities(ActivityRequestModel request)
        {
            return activityRepository.GetActivitiesByUserId(request.StartDateTime, request.EndDateTime);

        }

        #endregion
    }
}
