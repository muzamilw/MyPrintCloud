using System;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.Models.ResponseModels;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class PaperSheetBaseController : ApiController
    {
        #region Private

        private readonly IPaperSheetService paperSheetService;

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PaperSheetBaseController(IPaperSheetService paperSheetService)
        {
            if (paperSheetService == null)
            {
                throw new ArgumentNullException("paperSheetService");
            }

            this.paperSheetService = paperSheetService;
        }

        #endregion
        #region Public
        /// <summary>
        /// Get Item Base Data
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewPaperSheet })]
        public PaperSheetBaseResponse Get()
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return paperSheetService.GetBaseData();
        }

        #endregion
	}
}