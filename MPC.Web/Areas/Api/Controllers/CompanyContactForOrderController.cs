using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
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
        #endregion
    }
}