using System;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Campaign Base Api Controller
    /// </summary>
    public class CampaignBaseController : ApiController
    {
         #region Private

        private readonly ICampaignService campaignService;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CampaignBaseController(ICampaignService campaignService)
        {
            if (campaignService == null)
            {
                throw new ArgumentNullException("campaignService");
            }
            this.campaignService = campaignService;
        }

        #endregion

        #region Public
        /// <summary>
        /// Get Inventory Base Data
        /// </summary>
        public CampaignBaseResponse Get()
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return campaignService.GetBaseData().CreateFrom();
        }
        #endregion
    }
}