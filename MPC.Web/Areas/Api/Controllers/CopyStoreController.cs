using MPC.Interfaces.MISServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class CopyStoreController : ApiController
    {
       #region Private

        private readonly ICompanyService companyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CopyStoreController(ICompanyService companyService)
        {
            if (companyService == null)
            {
                throw new ArgumentNullException("companyService");
            }
            this.companyService = companyService;
        }

      
        #endregion

        [HttpGet]
        public bool ExportStore(long parameter1, long parameter2)
        {
            return companyService.ExportStoreZip(parameter1, parameter2);
        }

      
        [HttpPost]
        public bool ImportStore(long parameter1, string parameter2)
        {
            try
            {
                return companyService.ImportStoreZip(parameter1, parameter2);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
    }
}