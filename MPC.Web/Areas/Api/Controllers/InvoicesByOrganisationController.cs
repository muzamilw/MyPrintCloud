using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.WebBase.Mvc;
using Newtonsoft.Json;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class InvoicesByOrganisationController : ApiController
    {
        #region Private

        private readonly IInvoiceService _invoiceService;

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public InvoicesByOrganisationController(IInvoiceService invoiceService)
        {
            if (invoiceService == null)
            {
                throw new ArgumentNullException("invoiceService");
            }
            this._invoiceService = invoiceService;
        }

        #endregion
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrganisation })]
        public HttpResponseMessage Get()
        {
            try
            {

                var formatter = new JsonMediaTypeFormatter();
                var json = formatter.SerializerSettings;
                json.Formatting = Newtonsoft.Json.Formatting.Indented;
                json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                return Request.CreateResponse(HttpStatusCode.OK, _invoiceService.GetZapierInvoiceDetail(), formatter);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        
        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}