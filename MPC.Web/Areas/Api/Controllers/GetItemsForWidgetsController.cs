using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Get Items For Widgets Api Controller
    /// </summary>
    public class GetItemsForWidgetsController : ApiController
    {
        #region Private

        private readonly ICompanyService companyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public GetItemsForWidgetsController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        #endregion

        #region Public
        /// <summary>
        /// Get Items For Widgets
        /// </summary>
        /// <returns></returns>
        public List<ItemForWidgets> Get()
        {
            return companyService.GetItemsForWidgets().Select(i => i.CreateFromForWidgets()).ToList();

        }
        #endregion
    }
}