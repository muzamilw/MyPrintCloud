using MPC.Interfaces.WebStoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MPC.Webstore.Areas.DesignerApi.Controllers
{
    public class SmartFormController : ApiController
    {
       #region Private

        private readonly ISmartFormService smartFormService;
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public SmartFormController(ISmartFormService smartFormService)
        {
            this.smartFormService = smartFormService;
        }

        #endregion
    }
}
