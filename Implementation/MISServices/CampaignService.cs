using System.Linq;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.MISServices
{
    /// <summary>
    /// Campaign Service
    /// </summary>
    public class CampaignService : ICampaignService
    {
        #region Private
        private readonly ISectionFlagRepository sectionFlagRepository;
        private readonly ICompanyTypeRepository companyTypeRepository;
        private readonly IGroupRepository groupRepository;
        private readonly ISectionRepository sectionRepository;
        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public CampaignService(ISectionFlagRepository sectionFlagRepository, ICompanyTypeRepository companyTypeRepository, IGroupRepository groupRepository,
            ISectionRepository sectionRepository)
        {
            this.sectionFlagRepository = sectionFlagRepository;
            this.companyTypeRepository = companyTypeRepository;
            this.groupRepository = groupRepository;
            this.sectionRepository = sectionRepository;
        }

        #endregion

        #region Public

        public CampaignBaseResponse GetBaseData()
        {
            return new CampaignBaseResponse
            {
                CompanyTypes = companyTypeRepository.GetAllForCampaign().ToList(),
                SectionFlags = sectionFlagRepository.GetAllForCampaign().ToList(),
                Groups = groupRepository.GetAll().ToList(),
                CampaignSections = sectionRepository.GetCampaignSections().ToList()
            };
        }
        #endregion
    }
}
