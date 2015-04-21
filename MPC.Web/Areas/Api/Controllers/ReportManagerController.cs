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

        public ReportCategory get(long CategoryId)
        {
            return _IReportService.GetReportCategory(CategoryId).CreateFrom();
        }

        
    }
}