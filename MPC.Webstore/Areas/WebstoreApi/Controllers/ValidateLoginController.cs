using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using System.Runtime.Caching;
using MPC.Models.Common;
using MPC.Models.ResponseModels;

namespace MPC.Webstore.Areas.WebstoreApi.Controllers
{
    public class ValidateLoginController : ApiController
    {
        #region Private

        private readonly ICompanyService _myCompanyService;
        #endregion
        #region Constructor
        public ValidateLoginController(ICompanyService myCompanyService)
        {

            this._myCompanyService = myCompanyService;
        }

        #endregion

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage ValidateUserLogin(string OrganisationDomain, string Email, string UserPassword)
        {
            List<string> messages = new List<string>();

            long OrganisationId = 0;

            OrganisationId = _myCompanyService.GetOrganisationIdByRequestUrl(OrganisationDomain);

            if (OrganisationId > 0)
            {
                CompanyContact oContact = _myCompanyService.GetContactOnUserNamePass(OrganisationId, Email, UserPassword);

                if (oContact != null)
                {
                    // Result = true;
                    MPC.Models.DomainModels.Company ContactCompany = _myCompanyService.GetCompanyByCompanyID(oContact.CompanyId);

                    long StoreId = 0;
                    if (ContactCompany.IsCustomer == (int)StoreMode.Corp)
                    {
                        StoreId = oContact.CompanyId;

                    }
                    else
                    {
                        StoreId = Convert.ToInt64(ContactCompany.StoreId);
                        ContactCompany = _myCompanyService.GetCompanyByCompanyID(StoreId);
                    }

                    if (StoreId > 0)
                    {
                        MPC.Models.DomainModels.CompanyDomain domain = _myCompanyService.GetDomainByCompanyId(StoreId);
                        string domainurl = "";
                        if (domain != null && ContactCompany != null)
                        {
                            domainurl = domain.Domain.Substring(0, domain.Domain.Length - 1);
                            if (domainurl.Contains("/store"))
                            {
                                string[] listOfSplitedDomain = domainurl.Split('/');
                                domainurl = "http://" + listOfSplitedDomain[0] + "/autologin?C=" + ContactCompany.WebAccessCode + "&F=" + oContact.FirstName + "&L=" + oContact.LastName + "&E=" + oContact.Email;
                            }
                        }
                        messages.Add("Success");
                        messages.Add(domainurl);
                    }
                    else
                    {
                        messages.Add("Fail");
                        messages.Add("Invalid customer");

                    }
                }
                else
                {
                    messages.Add("Fail");
                    messages.Add("Invalid login or password.");
                }
            }
            else 
            {
                messages.Add("Fail");
                messages.Add("Invalid Domain.");
            }

            
            JsonSerializerSettings jSettings = new Newtonsoft.Json.JsonSerializerSettings();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = jSettings;
            //System.Web.HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return Request.CreateResponse(HttpStatusCode.OK, messages);
        }

    }
}
