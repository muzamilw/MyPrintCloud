using System.Collections.Generic;
using System.IO;
using System.Text;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.Common;
using MPC.Models.RequestModels;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.WebBase.Mvc;
using Newtonsoft.Json;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Company Contact Controller 
    /// </summary>
    public class CompanyContactController : ApiController
    {
        #region Private
        private readonly ICompanyService companyService;
        private readonly ICompanyContactService companyContactService;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyContactController(ICompanyService companyService, ICompanyContactService companyContactService)
        {
            this.companyService = companyService;
            this.companyContactService = companyContactService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Addresses / Compnay Contacts
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public CompanyContactResponse Get([FromUri] CompanyContactRequestModel request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return companyService.SearchCompanyContacts(request).CreateFrom();
        }

        /// <summary>
        /// Save Contact
        /// </summary>
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public CompanyContact Post(CompanyContact companyContact)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            PostDataToZapier(companyContact);
            return companyContactService.Save(companyContact.Createfrom()).CreateFrom();
        }


      

        /// <summary>
        /// Delete Contact
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public CompanyContact Delete(CompanyContactDeleteModel request)
        {
            if (request == null || !ModelState.IsValid || request.CompanyContactId <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }
            return companyContactService.Delete(request.CompanyContactId).CreateFrom();
        }

        private void PostDataToZapier(CompanyContact companyContact)
        {

           var resp =  companyContactService.GetStoreContactForZapier(companyContact.ContactId);
           string sData = JsonConvert.SerializeObject(resp, Formatting.None);

            //string sData = string.Empty;
           var request = System.Net.WebRequest.Create("https://zapier.com/hooks/catch/3hqi47/");
            request.ContentType = "application/json";
            request.Method = "POST";
            byte[] byteArray = Encoding.UTF8.GetBytes(sData);
            request.ContentLength = byteArray.Length;
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                var response = request.GetResponse();
            }

        }
        #endregion
	}
}