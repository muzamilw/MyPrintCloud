using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class StoreBaseController : ApiController
    {
        #region Private

        private readonly ICompanyService companyService;

        #endregion
        #region constructor

        public StoreBaseController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }
        #endregion
        #region Public
        public CompanyBaseResponse Get(long companyId)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            var result = companyService.GetBaseData(companyId);
            return new CompanyBaseResponse
                   {
                       SystemUsers = result.SystemUsers.Select(x => x.CreateFrom()),
                       CompanyTerritories = result.CompanyTerritories.Select(x=> x.CreateFrom())
                   };
        }
        #endregion

    }
}