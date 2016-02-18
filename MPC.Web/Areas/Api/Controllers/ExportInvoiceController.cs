using MPC.Interfaces.MISServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class ExportInvoiceController : Controller
    {
        #region Private

        private readonly IReportService reportService;
      
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public ExportInvoiceController(IReportService reportService)
        {
            this.reportService = reportService;
          
        }



        #endregion

    }
}