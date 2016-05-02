using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using MPC.ExceptionHandling;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.Models;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class XeroInvoiceController : ApiController
    {
        #region Private
        private readonly IInvoiceService _invoiceService;
        #endregion

        #region Constructor

        public XeroInvoiceController(IInvoiceService invoiceService)
        {
            if (invoiceService == null)
            {
                throw new ArgumentNullException("invoiceService");
            }

            this._invoiceService = invoiceService;
        }
        #endregion
        #region Public

        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrder })]
        [CompressFilterAttribute]
        public string Get(string oauth_token, string oauth_verifier, string org)
        {
            if (string.IsNullOrEmpty(oauth_token))
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }
           string sResult = _invoiceService.PostInvoiceToXero(oauth_token, oauth_verifier, org);
           return sResult;
        }

        #endregion
        

        
    }
}