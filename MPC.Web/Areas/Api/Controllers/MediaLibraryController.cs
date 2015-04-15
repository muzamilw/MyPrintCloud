using System;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.Models;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{

    /// <summary>
    /// Media Library API Controller
    /// </summary>
    public class MediaLibraryController : ApiController
    {
        #region Private

        private readonly ICompanyService companyService;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MediaLibraryController(ICompanyService companyService)
        {
            if (companyService == null)
            {
                throw new ArgumentNullException("companyService");
            }
            this.companyService = companyService;
        }
        #endregion

        #region Public
        [ApiException]
        public void Delete(MediaLibrary mediaLibrary)
        {
            if (mediaLibrary == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            companyService.DeleteMedia(mediaLibrary.MediaId);
        }
        #endregion
    }
}