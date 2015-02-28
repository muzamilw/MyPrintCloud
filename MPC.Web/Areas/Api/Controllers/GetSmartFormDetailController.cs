using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Get Smart Form Detail API Controller
    /// </summary>
    public class GetSmartFormDetailController : ApiController
    {

        #region Private

        private readonly ICompanyService companyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public GetSmartFormDetailController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        #endregion

        #region Public

        /// <summary>
        /// Get Smart Forms
        /// </summary>
        public IEnumerable<SmartFormDetail> Get([FromUri] long smartFormId)
        {
            return companyService.GetSmartFormDetailBySmartFormId(smartFormId).Select(sfd => sfd.CreateFrom());
        }

        #endregion

    }
}