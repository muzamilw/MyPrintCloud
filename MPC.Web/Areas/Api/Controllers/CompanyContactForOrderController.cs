using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;
using System.Net;
using System.Web;
using System.Web.Http;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Company Contact For Order API Controller
    /// </summary>
    public class CompanyContactForOrderController : ApiController
    {
        #region Private

        private readonly ICompanyContactService _companyContactService;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyContactForOrderController(ICompanyContactService companyContactService)
        {
            _companyContactService = companyContactService;
        }

        #endregion
        #region Public
        /// <summary>
        /// Get Contacts for Order 
        /// </summary>
        public ContactResponseForOrder Get([FromUri]CompanyRequestModelForCalendar request)
        {
           return _companyContactService.GetContactsForOrder(request).CreateFrom();
        }

        /// <summary>
        /// Save Contact
        /// </summary>
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public CompanyContact Post(CompanyContactDeleteModel request)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return _companyContactService.UnArchiveCompanyContact(request.CompanyContactId).CreateFrom();
           
        }
        #endregion
    }
}