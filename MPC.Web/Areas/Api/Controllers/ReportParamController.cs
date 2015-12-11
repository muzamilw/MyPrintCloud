using GrapeCity.ActiveReports;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class ReportParamController : ApiController
    {
         private readonly IReportService _IReportService;
         public ReportParamController(IReportService IReportService)
        {
            if (IReportService == null)
            {
                throw new ArgumentNullException("IReportService");
            }
            this._IReportService = IReportService;

        }

         // GET: Api/ReportManager
         //  ReportCategoryRequestModel
         public ActionResult get([FromUri] ReportParamRequestModel req)
         {
             //return true;

           //  SectionReport report = _IReportService.GetReportByParams(req.ReportId, req.ComboValue, req.ParamDateFromValue, req.ParamDateToValue, req.ParamTextBoxValue);

             return null;
             //System.Web.Mvc.view
             
             // return _IReportService.GetReportCategory(req.CategoryId, req.IsExternal).CreateFrom();
         }
    }
}