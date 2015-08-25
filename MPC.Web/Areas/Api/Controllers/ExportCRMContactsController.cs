using MPC.Interfaces.MISServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class ExportCRMContactsController : ApiController
    {

          #region Private
        private readonly ICompanyContactService companyContactService;

        #endregion
        #region Constructor
        public ExportCRMContactsController(ICompanyContactService CompanyContactService)
        {
            this.companyContactService = CompanyContactService;
        }
        #endregion



     

    }
}
