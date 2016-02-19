using MPC.Interfaces.MISServices;
using MPC.WebBase.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class ExportInvoiceController : ApiController
    {
        #region Private

        private readonly IInvoiceService invoiceService;
      
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public ExportInvoiceController(IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
          
        }


        [CompressFilterAttribute]
        public string Get(long InvoiceId)
        {
            string path = invoiceService.ExportInvocie(InvoiceId);
            return path;
        }
        #endregion

    }
}