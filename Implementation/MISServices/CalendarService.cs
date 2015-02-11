using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
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

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public CalendarService(ISystemUserRepository systemUserRepository)
        {
            this.systemUserRepository = systemUserRepository;
        }

        #endregion

        #region Public
        /// <summary>
        /// Get Base Data
        /// </summary>
        public CalendarBaseResponse GetSections()
        {
            //return sectionRepository.GetSectionsForPhraseLibrary();
            return null;
        }

        #endregion
    }
}
