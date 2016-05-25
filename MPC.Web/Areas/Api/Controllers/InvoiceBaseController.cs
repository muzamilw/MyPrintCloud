using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.Models;
using MPC.WebBase.Mvc;
using MPC.MIS.Areas.Api.ModelMappers;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class InvoiceBaseController : ApiController
    {
       #region Private

        private readonly IInvoiceService invoiceService;

        #endregion
       #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public InvoiceBaseController(IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
        }

        #endregion
       #region Public

         
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewInvoicing })]
        [CompressFilterAttribute]
        public InvoiceBaseResponse Get()
        {
            return invoiceService.GetInvoiceBaseResponse().CreateFrom();
        }

       #endregion
    }
}
