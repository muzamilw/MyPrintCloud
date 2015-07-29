﻿using MPC.Interfaces.WebStoreServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MPC.Webstore.Areas.WebstoreApi.Controllers
{
    public class DiscountVoucherController : ApiController
    {
         #region Private

       
        private readonly ICompanyService _companyService;
        #endregion
        #region Constructor

        public DiscountVoucherController(ICompanyService companyService)
        {
            this._companyService = companyService;
           
        }
        #endregion
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage ValidateDiscountVouchers(string DiscountVoucher, long StoreId)
        {

            JsonSerializerSettings jSettings = new Newtonsoft.Json.JsonSerializerSettings();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = jSettings;

            return Request.CreateResponse(HttpStatusCode.OK, "");
        }
    }
}
