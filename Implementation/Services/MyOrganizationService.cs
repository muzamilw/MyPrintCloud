using MPC.Interfaces.IServices;
using MPC.Interfaces.Repository;

namespace MPC.Implementation.Services
{
    /// <summary>
    /// My Organization Service
    /// </summary>
    public class MyOrganizationService : IMyOrganizationService
    {
        #region Private

        /// <summary>
        /// Private members
        /// </summary>
        private readonly ICompanySitesRepository companySitesRepository;
        private readonly IMarkupRepository markupRepository;
        private readonly ITaxRateRepository taxRateRepository;
        private readonly IChartOfAccountRepository chartOfAccountRepository;

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public MyOrganizationService(ICompanySitesRepository companySitesRepository, IMarkupRepository markupRepository,
            ITaxRateRepository taxRateRepository, IChartOfAccountRepository chartOfAccountRepository)
        {
            this.companySitesRepository = companySitesRepository;
            this.markupRepository = markupRepository;
            this.taxRateRepository = taxRateRepository;
            this.chartOfAccountRepository = chartOfAccountRepository;
        }

        #endregion

        public System.Collections.Generic.IList<int> GetOrganizationIds(int request)
        {
            throw new System.NotImplementedException();
        }
    }
}
