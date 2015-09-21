using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using GrapeCity.Viewer.Common.Model;
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
        public HttpResponseMessage Get(HttpActionContext httpContext)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://myprintcloud.com/Account/Login");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string authCode = string.Empty;
                    string url = "?email=staging@myprintcloud.com&password=p@ssw0rd&RememberMe=false";
                    string responsestr = "";
                    var response = client.GetAsync(url);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        responsestr = response.Result.Content.ReadAsStringAsync().Result;
                        //HtmlString str = new HtmlString(responsestr.Replace("<br>", "<br />"));

                        XElement info = XElement.Parse(responsestr.Replace("<br>", "<br />"));
                        var code = (info.Descendants("input")).FirstOrDefault();

                        if (code != null)
                        {
                            authCode = code.LastAttribute.Value;
                            response = client.GetAsync(url + "&__RequestVerificationToken="+authCode);
                            if (response.Result.IsSuccessStatusCode)
                            {
                                responsestr = response.Result.Content.ReadAsStringAsync().Result;
                            }
                        }
                            
                    }

                }
                
                
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