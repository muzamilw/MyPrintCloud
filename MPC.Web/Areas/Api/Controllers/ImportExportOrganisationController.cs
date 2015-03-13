using MPC.Interfaces.MISServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class ImportExportOrganisationController : ApiController
    {
         #region Private

        private readonly ICompanyService companyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public ImportExportOrganisationController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        #endregion

      
        [HttpGet]
        public bool InsertOrganisation(long parameter1, bool parameter2)
        {
            try
            {
               return companyService.ImportOrganisation(parameter1, parameter2);
            }
            catch (Exception ex)
            {
                throw ex;

            }
         
        }


        [HttpGet]
        public bool ExportOrganisation(long id)
        {
            try
            {
                return companyService.ExportOrganisation(id);
            }
            catch(Exception ex)
            {
                throw ex;

            }
          
        }
    }
}
