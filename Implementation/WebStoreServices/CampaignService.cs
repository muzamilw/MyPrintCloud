using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Implementation.WebStoreServices
{
    public class CampaignService : ICampaignService
    {
        public readonly ICampaignRepository _CampaignRepository;

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public CampaignService(ICampaignRepository CampaignRepository)
        {
            this._CampaignRepository = CampaignRepository;
          
        }


        #endregion

        public Campaign GetCampaignRecordByEmailEvent(int iEmailEvent)
        {
            return _CampaignRepository.GetCampaignRecordByEmailEvent(iEmailEvent);
        }
    }

    
}
