using MPC.Interfaces.WebStoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MPC.Webstore.Areas.DesignerApi.Controllers
{
    public class TemplateBackgroundImageController : ApiController
    {
        #region Private

        private readonly ITemplateBackgroundImagesService templateBackgroundImages;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public TemplateBackgroundImageController(ITemplateBackgroundImagesService templateBackgroundImages)
        {
            this.templateBackgroundImages = templateBackgroundImages;
        }

        #endregion
    }
}
