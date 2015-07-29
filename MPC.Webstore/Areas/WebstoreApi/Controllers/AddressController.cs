using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace MPC.Webstore.Areas.WebstoreApi.Controllers
{
    public class AddressController : ApiController
    {
        
        #region Private

       
        private readonly ICompanyService _companyService;
        #endregion
        #region Constructor

        public AddressController(ICompanyService companyService)
        {
            this._companyService = companyService;
           
        }

        #endregion
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage FillAddresses(long AddressId)
        {
            JsonAddressClass obj = new JsonAddressClass();
            Address Address = _companyService.GetAddressByID(AddressId);
            obj.Address = Address;
            obj.StateId = Address.StateId ?? 0;
            obj.CountryId = Address.CountryId ?? 0;
            obj.CompanyID = Address.CompanyId ?? 0;
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, obj, formatter);
        }
        public class JsonAddressClass
        {
            public Address Address;
            public long StateId;
            public long CountryId;
            public long CompanyID;
        }

    }
}
