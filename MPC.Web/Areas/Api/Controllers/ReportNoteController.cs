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
using MPC.ExceptionHandling;
using MPC.Models.RequestModels;

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

        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
       
        public IEnumerable<ReportNote> GetReportNote(long CompanyID)
        {
            return reportService.GetReportNoteByCompanyID(CompanyID).Select(c => c.CreateFrom());

        }

        [ApiException]
        [CompressFilterAttribute]
        public void Post(ReportNoteRequestModel reportNotes)
        {
            if (ModelState.IsValid)
            {
                try
                {
                     reportService.Update(reportNotes.ReportsBanners);
                   

                }
                catch (Exception exception)
                {
                    throw new MPCException(exception.Message, 0);
                }

            }
            else
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
           
        }

        #endregion
    }
}
