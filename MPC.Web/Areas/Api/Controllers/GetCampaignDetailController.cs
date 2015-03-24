using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Get Campaign Detail API Controller
    /// </summary>
    public class GetCampaignDetailController : ApiController
    {

        #region Private
        private readonly ICompanyService companyService;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public GetCampaignDetailController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        #endregion

        #region Public

        /// <summary>
        ///  Get Campaign By Campaign Id
        /// </summary>
        public Campaign Get([FromUri]long campaignId)
        {
            return companyService.GetCampaignById(campaignId).CreateFrom();
        }
        #endregion
    }
}