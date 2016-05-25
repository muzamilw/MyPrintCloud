using System.Linq;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using System.Web.Http;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class CrmContactController : ApiController
    {
        #region Private
        private readonly ICompanyContactService companyContactService;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CrmContactController(ICompanyContactService companyContactService)
        {
            this.companyContactService = companyContactService;
        }

        #endregion
        #region Public
        /// <summary>
        /// Get Addresses and Territories Of "Company Contact's company"
        /// </summary>
        [ApiAuthorize(AccessRights = new[] {SecurityAccessRight.CanViewCrm})]
        [CompressFilterAttribute]
        public CrmContactResponse Get([FromUri] CompanyContactRequestModel request)
        {
            var response = companyContactService.SearchAddressesAndTerritories(request);
            return new CrmContactResponse
            {
                Addresses = response.Addresses != null ? response.Addresses.Select(x=>x.CreateFrom()) : null,
                CompanyTerritories = response.CompanyTerritories != null ? response.CompanyTerritories.Select(x=>x.CreateFrom()): null
            };
        }
        #endregion



    }
}