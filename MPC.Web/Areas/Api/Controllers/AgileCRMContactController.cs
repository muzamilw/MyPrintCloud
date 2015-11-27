using MPC.Interfaces.MISServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    
    public class AgileCRMContactController : ApiController
    {
        #region Private

        private readonly ICompanyContactService companyContactService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public AgileCRMContactController(ICompanyContactService companyContactService)
        {
            this.companyContactService = companyContactService;
            
        }

        #endregion

        public string AddAgileCrmContact(string email, string fullname, string Company, string phone, string region, string domain)
        {
            return companyContactService.AddAgileCrmContact(email, fullname, Company, phone, region, domain);
        }
    }
}