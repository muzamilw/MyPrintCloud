using System.Linq;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.Models.RequestModels;

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
        public bool Get([FromUri] long addressId)
        {
            var address = addressService.Get(addressId);
            if (address != null)
            {
                if (address.CompanyContacts != null )
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        #endregion
    }
}