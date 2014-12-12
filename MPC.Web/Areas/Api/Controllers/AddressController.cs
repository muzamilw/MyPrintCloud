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

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public AddressController(ICompanyService companyService)
        {
            this.companyService = companyService;
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

        #endregion
    }
}