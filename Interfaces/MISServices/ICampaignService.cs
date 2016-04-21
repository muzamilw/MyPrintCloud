using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.MISServices
{
    /// <summary>
    /// Campaign Service Interface
    /// </summary>
    public interface ICampaignService
    {
        CampaignBaseResponse GetBaseData();
        Campaign SaveCampaign(Campaign campaign);
    }
}
