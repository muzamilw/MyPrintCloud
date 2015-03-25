using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// CMS Tag API Controller
    /// </summary>
    public class CmsTagController : ApiController
    {
        #region Private
        private readonly ICompanyService companyService;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CmsTagController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        #endregion

        #region Public

        /// <summary>
        ///  Get CMS Tags For  Load Default page keywords of Cms Page
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        public string Get()
        {
            return companyService.GetCmsTagForCmsPage();
        }
        #endregion
    }
}