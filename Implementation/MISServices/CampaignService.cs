using System.Collections.ObjectModel;
using System.Linq;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
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
        private readonly ICampaignRepository _campaignRepository;
        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public CampaignService(ISectionFlagRepository sectionFlagRepository, ICompanyTypeRepository companyTypeRepository, IGroupRepository groupRepository,
            ISectionRepository sectionRepository, ICampaignRepository campaignRepository)
        {
            this.sectionFlagRepository = sectionFlagRepository;
            this.companyTypeRepository = companyTypeRepository;
            this.groupRepository = groupRepository;
            this.sectionRepository = sectionRepository;
            this._campaignRepository = campaignRepository;
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

        public Campaign SaveCampaign(Campaign campaign)
        {
            Campaign dbCampaign = _campaignRepository.Find(campaign.CampaignId);
            if (dbCampaign != null)
            {
                dbCampaign.CampaignName = campaign.CampaignName;
                dbCampaign.Description = campaign.Description;
                dbCampaign.CampaignType = campaign.CampaignType;
                dbCampaign.IsEnabled = campaign.IsEnabled;
                dbCampaign.IncludeCustomers = campaign.IncludeCustomers;
                dbCampaign.IncludeSuppliers = campaign.IncludeSuppliers;
                dbCampaign.IncludeProspects = campaign.IncludeProspects;
                dbCampaign.IncludeNewsLetterSubscribers = campaign.IncludeNewsLetterSubscribers;
                dbCampaign.IncludeFlag = campaign.IncludeFlag;
                dbCampaign.FlagIDs = campaign.FlagIDs;
                dbCampaign.CustomerTypeIDs = campaign.CustomerTypeIDs;
                dbCampaign.GroupIDs = campaign.GroupIDs;
                dbCampaign.SubjectA = campaign.SubjectA;
                dbCampaign.HTMLMessageA = campaign.HTMLMessageA;
                dbCampaign.StartDateTime = campaign.StartDateTime;
                dbCampaign.FromAddress = campaign.FromAddress;
                dbCampaign.ReturnPathAddress = campaign.ReturnPathAddress;
                dbCampaign.ReplyToAddress = campaign.ReplyToAddress;
                dbCampaign.EmailLogFileAddress2 = campaign.EmailLogFileAddress2;
                dbCampaign.EmailEvent = campaign.EmailEvent;
                dbCampaign.SendEmailAfterDays = campaign.SendEmailAfterDays;
                dbCampaign.FromName = campaign.FromName;
                dbCampaign.IncludeType = campaign.IncludeType;
                dbCampaign.IncludeCorporateCustomers = campaign.IncludeCorporateCustomers;
                dbCampaign.EnableLogFiles = campaign.EnableLogFiles;
                dbCampaign.EmailLogFileAddress3 = campaign.EmailLogFileAddress3;

                if (campaign.CampaignImages != null)
                {
                    foreach (var campaignImage in campaign.CampaignImages)
                    {
                        if (campaignImage.CampaignImageId == 0)
                        {
                            CampaignImage image = new CampaignImage {ImageName = campaignImage.ImageName, ImagePath = campaignImage.ImagePath};
                            if(dbCampaign.CampaignImages == null)
                                dbCampaign.CampaignImages = new Collection<CampaignImage>();
                            dbCampaign.CampaignImages.Add(image);
                        }
                    }
                }
                _campaignRepository.SaveChanges();
            }
            return dbCampaign;
        }
        #endregion
    }
}
