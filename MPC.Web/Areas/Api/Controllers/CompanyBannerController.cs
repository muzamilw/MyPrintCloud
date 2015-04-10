using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.Models;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Company Banner API Controller
    /// </summary>
    public class CompanyBannerController : ApiController
    {
         #region Private

        private readonly ICompanyService companyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyBannerController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        #endregion

        #region Public


   
        /// <summary>
        /// Delete Company Banner
        /// </summary>
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public void Delete(CompanyBanner banner)
        {
            if (banner == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            companyService.DeleteCompanyBanner(banner.CompanyBannerId);
        }
        #endregion
    }
}