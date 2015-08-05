using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class DownloadReportController : ApiController
    {
       private readonly IReportService _IReportService;
       public DownloadReportController(IReportService IReportService)
        {
            if (IReportService == null)
            {
                throw new ArgumentNullException("IReportService");
            }
            this._IReportService = IReportService;



        }
        // GET: Api/ReportManager
      //  ReportCategoryRequestModel
        [HttpGet]
       public string get(int ReportId, bool Mode)
       {
           //bool GG = true;
           return _IReportService.DownloadExternalReport(ReportId, Mode);
       }
    }
}
