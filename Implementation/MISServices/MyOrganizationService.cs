using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.MISServices
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

        #region Public
        /// <summary>
        /// Load My Organization Base data
        /// </summary>
        public MyOrganizationBaseResponse GetBaseData()
        {
            return new MyOrganizationBaseResponse
            {
                ChartOfAccounts = chartOfAccountRepository.GetAll(),
                Markups = markupRepository.GetAll(),
                TaxRates = taxRateRepository.GetAll(),
            };
        }

        /// <summary>
        ///  Find Company Site Detail By Company Site ID
        /// </summary>
        public CompanySites FindDetailById(int companySiteId)
        {
            return companySitesRepository.Find(companySiteId);
        }


        /// <summary>
        /// Add/Update Company Sites
        /// </summary>
        public int SaveCompanySite(CompanySites companySites)
        {
            CompanySites companySitesDbVersion = companySitesRepository.Find(companySites.CompanySiteId);
            if (companySitesDbVersion == null)
            {
                return Save(companySites);
            }
            else
            {
                //Set updated fields
                return Update(companySitesDbVersion);
            }
        }

        /// <summary>
        /// Add New Company Sites
        /// </summary>
        private int Save(CompanySites companySites)
        {
            companySites.UserDomainKey = companySitesRepository.UserDomainKey;
            companySitesRepository.Add(companySites);
            companySitesRepository.SaveChanges();
            return companySites.CompanySiteId;
        }

        /// <summary>
        /// Update Company Sites
        /// </summary>
        private int Update(CompanySites companySites)
        {
            companySitesRepository.Update(companySites);
            companySitesRepository.SaveChanges();
            return companySites.CompanySiteId;
        }

        #endregion

        public System.Collections.Generic.IList<int> GetOrganizationIds(int request)
        {
            throw new System.NotImplementedException();
        }
    }
}
