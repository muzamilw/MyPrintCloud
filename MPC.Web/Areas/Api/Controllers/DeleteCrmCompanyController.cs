using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;
namespace MPC.MIS.Areas.Api.Controllers
{
    public class DeleteCrmCompanyController : ApiController
    { 
        #region Private

        private readonly ICompanyService companyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DeleteCrmCompanyController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        #endregion
        
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [ApiException]
        [CompressFilterAttribute]
        public void Delete(DeleteCompanyRequest model)
        {
            if (model.CompanyId == 0 || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            companyService.DeleteCrmCompanyPermanently(model.CompanyId);
        }

    }
}