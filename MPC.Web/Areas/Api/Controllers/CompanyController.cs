using System.Linq;
using System.Web.Http;
using MPC.Interfaces.WebStoreServices;
using MPC.MIS.ModelMappers;
using MPC.Models.RequestModels;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class CompanyController : ApiController
    {
        #region Private

        private readonly ICompanyService companyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public CompanyController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        #endregion
        /// <summary>
        /// Get All Companies Of Organisation
        /// </summary>
        /// <returns></returns>
        public ResponseModels.CompanyResponse Get([FromUri] CompanyRequestModel request)
        {
            var result = companyService.GetAllCompaniesOfOrganisation(request);
            return new ResponseModels.CompanyResponse
            {
                Companies = result.Companies.Select(x => x.CreateFrom()),
                RowCount = result.RowCount
            };
        }
    }
}