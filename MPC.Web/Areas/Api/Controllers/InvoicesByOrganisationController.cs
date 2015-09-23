using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection.Emit;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using GrapeCity.Viewer.Common.Model;
using MPC.ExceptionHandling;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.Models.Common;
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
        //[ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrganisation })]
        public HttpResponseMessage Get()
        {
            try
            {
                long organisationId = 0;
                string param = string.Empty;
                param = Request.RequestUri.Query;
                string responsestr = GetActiveOrganisationId(param);

                if (string.IsNullOrEmpty(responsestr) || responsestr == "Fail")
                {
                    throw new MPCException("Service Not Authenticated!", organisationId);
                }
                else
                {
                    organisationId = Convert.ToInt64(responsestr);
                    var formatter = new JsonMediaTypeFormatter();
                    var json = formatter.SerializerSettings;
                    json.Formatting = Newtonsoft.Json.Formatting.Indented;
                    json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    return Request.CreateResponse(HttpStatusCode.OK, _invoiceService.GetZapierInvoiceDetail(organisationId), formatter);
                }
                
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        
        // GET api/<controller>/5
        public HttpResponseMessage Get(string storeCode)
        {
            try
            {
                long organisationId = 0;
                string param = string.Empty;
                param = Request.RequestUri.Query;
                string responsestr = GetActiveOrganisationId(param);
                
                if (string.IsNullOrEmpty(responsestr) || responsestr == "Fail")
                {
                    throw new MPCException("Service Not Authenticated!", organisationId);
                }
                else
                {
                    organisationId = Convert.ToInt64(responsestr);
                    var formatter = new JsonMediaTypeFormatter();
                    var json = formatter.SerializerSettings;
                    json.Formatting = Newtonsoft.Json.Formatting.Indented;
                    json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    return Request.CreateResponse(HttpStatusCode.OK, _invoiceService.GetZapierInvoiceDetail(organisationId), formatter);
                }


            }
            catch (Exception ex)
            {
                throw ex;

            }
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

        private string GetActiveOrganisationId(string param)
        {
            string responsestr = string.Empty;
            string credentials = param.Substring(param.IndexOf("username="),
                    param.Length - param.IndexOf("username="));
            if (!string.IsNullOrEmpty(credentials))
                credentials = credentials.Replace("username=", "email=");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://myprintcloud.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var uri = "/Account/GetCustomerId?" + credentials;
                var response = client.GetAsync(uri);
                if (response.Result.IsSuccessStatusCode)
                {
                    responsestr = response.Result.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    throw new MPCException("Service Not Authenticated!", 0);
                }

            }

            return responsestr;
        }
    }
}