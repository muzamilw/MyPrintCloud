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

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public CalendarService(ISystemUserRepository systemUserRepository, ICompanyContactRepository companyContactRepository,
            IPipeLineProductRepository pipeLineProductRepository, IPipeLineSourceRepository pipeLineSourceRepository,
            ISectionFlagRepository sectionFlagRepository, ICompanyRepository companyRepository)
        {
            this.systemUserRepository = systemUserRepository;
            this.companyContactRepository = companyContactRepository;
            this.pipeLineProductRepository = pipeLineProductRepository;
            this.pipeLineSourceRepository = pipeLineSourceRepository;
            this.sectionFlagRepository = sectionFlagRepository;
            this.companyRepository = companyRepository;
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

        #endregion
    }
}
