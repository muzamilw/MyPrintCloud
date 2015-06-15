using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class ReportEmailController : ApiController
    {
         private readonly IReportService _IReportService;
         public ReportEmailController(IReportService IReportService)
        {
            if (IReportService == null)
            {
                throw new ArgumentNullException("IReportService");
            }
            this._IReportService = IReportService;



        }
         public ReportEmailResponse Get([FromUri] ReportEmailRequestModel request)
         {


             var result = _IReportService.GetReportEmailBaseData(request);
             return new ReportEmailResponse
             {
                 To = result.To,
                 Subject = result.Subject,
                 Attachment = result.Attachment,
                 AttachmentPath = result.AttachmentPath,
                 CC = result.CC,
                 Signature = result.Signature
                
             };

            
         }

    }
}
