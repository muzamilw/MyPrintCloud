using MPC.ExceptionHandling;
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
        private readonly IItemService itemService;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public ImportExportOrganisationController(ICompanyService companyService, IItemService itemService)
        {
            this.companyService = companyService;
            this.itemService = itemService;
        }

        #endregion


        [HttpGet]
        public bool InsertOrganisation(long parameter1, string parameter2,bool parameter3)
        {
            try
            {
                return companyService.ImportOrganisation(parameter1, parameter2, parameter3);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }


        [HttpGet]
        public string ExportOrganisation(long parameter1, string parameter2, string parameter3, string parameter4, string parameter5)
        {
            string error = string.Empty;
            try
            {
                error = companyService.ExportOrganisation(parameter1, parameter2, parameter3, parameter4, parameter5);

                return error;
            }
            catch(Exception ex)
            {
                throw ex;

            }
          
        }

        [HttpPost]
        public bool ImportStore(long parameter1, string parameter2,string parameter3)
        {
            try
            {
                return companyService.ImportStore(parameter1, parameter2, parameter3);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        
    }
}
