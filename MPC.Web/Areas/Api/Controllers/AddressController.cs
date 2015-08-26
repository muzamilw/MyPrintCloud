using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class AddressController : ApiController
    {
        #region Private

        private readonly ICompanyService companyService;
        private readonly IAddressService addressService;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public AddressController(ICompanyService companyService, IAddressService addressService)
        {
            this.companyService = companyService;
            this.addressService = addressService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Addresses
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public Models.AddressResponse Get([FromUri] AddressRequestModel request)
        {
            var result = companyService.SearchAddresses(request);
            return new Models.AddressResponse
            {
                Addresses = result.Addresses.Select(x => x.CreateFrom()),
                RowCount = result.RowCount
            };
        }
        /// <summary>
        /// Get address By id
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public bool Get([FromUri] long addressId)
        {
            var address = addressService.Get(addressId);
            if (address != null)
            {
                if (address.CompanyContacts.Count != 0)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
        [ApiException]
        [HttpPost]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public Address Post(Address address)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return addressService.Save(address.CreateFrom()).CreateFrom();
        }

        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public bool Delete(CompanyAddressDeleteModel request)
        {
            if (request == null || !ModelState.IsValid || request.AddressId <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }
            return addressService.Delete(request.AddressId);
        }
        #endregion
    }
}