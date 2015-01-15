using MPC.Interfaces.WebStoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MPC.Webstore.Areas.DesignerApi.Controllers
{
    public class ImagePermissionController : ApiController
    {
       #region Private

        private readonly IImagePermissionsService imagePermissionService;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public ImagePermissionController(IImagePermissionsService imagePermissionService)
        {
            this.imagePermissionService = imagePermissionService;
        }

        #endregion
    }
}
