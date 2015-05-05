using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.MIS.Areas.Api.ModelMappers;
using System.Web.Http;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class ReportManagerController : ApiController
    {
        private readonly IReportService _IReportService;
        public ReportManagerController(IReportService IReportService)
        {
            if (IReportService == null)
            {
                throw new ArgumentNullException("IReportService");
            }
            this._IReportService = IReportService;



        }
        // GET: Api/ReportManager
      //  ReportCategoryRequestModel
        public ReportCategory get([FromUri] ReportCategoryRequestModel req)
        {
            return _IReportService.GetReportCategory(req.CategoryId, req.IsExternal).CreateFrom();
        }

        public void getParamsById(long Id)
        {

        }
        
    }
}