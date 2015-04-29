using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.Models;
using MPC.WebBase.Mvc;
using MPC.MIS.Areas.Api.ModelMappers;
using System.Web;
using MPC.Models.ResponseModels;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class ReportNoteController : ApiController
    {
        #region Private

        private readonly IReportService reportService;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary
        public ReportNoteController(IReportService _reportService)
        {
            if (_reportService == null)
            {
                throw new ArgumentNullException("reportService");
            }
            this.reportService = _reportService;
        }

        #endregion

        #region Public
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public List<StoresListResponse> Get()
        {

            return reportService.GetStoreNameByOrganisationId();
        }
        #endregion
    }
}
