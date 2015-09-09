using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.WebBase.Mvc;

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
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public List<ItemForWidgets> Get()
        {
            return companyService.GetItemsForWidgets().Select(i => i.CreateFromForWidgets()).ToList();

        }
        public List<ItemForWidgets> Get(long storeId)
        {
            return companyService.GetItemsForWidgetsByStoreId(storeId).Select(i => i.CreateFromForWidgets()).ToList();

        }
        #endregion
    }
}