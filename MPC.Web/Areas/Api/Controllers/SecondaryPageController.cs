using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Secondary Page API Controller
    /// </summary>
    public class SecondaryPageController : ApiController
    {
        #region Private
        private readonly ICompanyService companyService;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public SecondaryPageController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        #endregion

        #region Public

        /// <summary>
        /// Get Addresses
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public SecondaryPageResponse Get([FromUri] SecondaryPageRequestModel request)
        {
            return companyService.GetCMSPages(request).CreateFrom();
        }

        /// <summary>
        /// Get Stock Item By Id
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public CmsPage Get(int id)
        {
            if (id <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return companyService.GetCmsPageById(id).CreateFrom();
        }


        /// <summary>
        /// Save Secondary Page
        /// </summary>
        public long Post(CmsPage cmsPage)
        {
            if (cmsPage == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return companyService.SaveSecondaryPage(cmsPage.CreateFrom());

        }

        /// <summary>
        /// Delete Secondary Page
        /// </summary>
        public void Delete(CmsPage cmsPage)
        {
            if (cmsPage == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            companyService.DeleteSecondaryPage(cmsPage.PageId);
        }
        #endregion
    }
}