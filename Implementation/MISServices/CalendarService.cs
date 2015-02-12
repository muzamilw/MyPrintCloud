﻿using System.Collections;
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
                CompanyContacts = companyContactRepository.GetAll(),
                ActivityTypes = activityTypeRepository.GetAll(),
                LoggedInUserId = activityTypeRepository.LoggedInUserId,
                SectionFlags = sectionFlagRepository.GetSectionFlagBySectionId((int)SectionEnum.CRM),
            };
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
            activity.SystemUserId = activityRepository.LoggedInUserId;
            activityRepository.Add(activity);
            activityRepository.SaveChanges();
            return activity.ActivityId;
        }
        /// <summary>
        /// Update Activity
        /// </summary>
        public int UpdateActivity(Activity activity, Activity dbVersionActivity)
        {
            activity.ActivityTypeId = dbVersionActivity.ActivityTypeId;
            activity.ContactId = dbVersionActivity.ContactId;
            activity.SourceId = dbVersionActivity.SourceId;
            activity.ActivityEndTime = dbVersionActivity.ActivityEndTime;
            activity.ActivityNotes = dbVersionActivity.ActivityNotes;
            activity.ActivityRef = dbVersionActivity.ActivityRef;
            activity.ActivityStartTime = dbVersionActivity.ActivityStartTime;
            activity.CompanyId = dbVersionActivity.CompanyId;
            activity.FlagId = dbVersionActivity.FlagId;
            activity.IsCustomerActivity = dbVersionActivity.IsCustomerActivity;
            activity.IsPrivate = dbVersionActivity.IsPrivate;
            activity.ProductTypeId = dbVersionActivity.ProductTypeId;
            activity.SystemUserId = dbVersionActivity.SystemUserId;
            activityRepository.SaveChanges();
            return activity.ActivityId;
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
        #endregion
    }
}
