
using System;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.WebBase.Mvc;
using System.Collections.Generic;
using MPC.Models.DomainModels;
using System.Net.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using System.Net;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class LiveStoresController : ApiController
    {
        #region Private

        private readonly ICompanyService _CompanyService;

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public LiveStoresController(ICompanyService companyService)
        {
            if (companyService == null)
            {
                throw new ArgumentNullException("companyService");
            }
            this._CompanyService = companyService;
        }

        #endregion

        #region Public
        [ApiException]
        
        //[ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrganisation })]
        public HttpResponseMessage Get()
        {
            try
            {

                var formatter = new JsonMediaTypeFormatter();
                var json = formatter.SerializerSettings;
                json.Formatting = Newtonsoft.Json.Formatting.Indented;
                json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                return Request.CreateResponse(HttpStatusCode.OK, _CompanyService.GetLiveStoresJason(), formatter);

                    //stores = JsonConvert.SerializeObject(storeDetails, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                 

                
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public bool Get(long id)
        {
            try
            {
                return false;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        #endregion
        [ApiException]
        public bool Post()
        {
            try
            {
               return false;

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        
    }
}