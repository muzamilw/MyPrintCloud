using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using MPC.ExceptionHandling;
using MPC.Interfaces.MISServices;
using MPC.WebBase.Mvc;
using Newtonsoft.Json;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class ContactsByOrganisationAndStoreController : ApiController
    {
        #region Private
        
        private readonly ICompanyContactService _companyContactService;

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ContactsByOrganisationAndStoreController(ICompanyContactService companyContactService)
        {
            if (companyContactService == null)
            {
                throw new ArgumentNullException("companyContactService");
            }
            this._companyContactService = companyContactService;
        }

        #endregion
       
        [ApiException]
        public HttpResponseMessage Get()
        {
            try
            {
                long organisationId = 1;
                string param = Request.RequestUri.Query;
                string responsestr = GetActiveOrganisationId(param);
                // responsestr = Temporarily set for local testing
                if (string.IsNullOrEmpty(responsestr) || responsestr == "Fail")
                {
                    throw new MPCException("Service Not Authenticated!", organisationId);
                }
                else
                {
                    organisationId = Convert.ToInt64(responsestr);
                }
                var formatter = new JsonMediaTypeFormatter();
                var json = formatter.SerializerSettings;
                json.Formatting = Newtonsoft.Json.Formatting.Indented;
                json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                return Request.CreateResponse(HttpStatusCode.OK, _companyContactService.GetContactForZapierPooling(organisationId), formatter);


            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public HttpResponseMessage Post(string value)
        {
            
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, _companyContactService.GetContactForZapierPooling(0), formatter);
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
            string credentials = !string.IsNullOrEmpty(param) ? param.Substring(param.IndexOf("username="), param.Length - param.IndexOf("username=")) : string.Empty;
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