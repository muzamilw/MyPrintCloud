using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;
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
         private readonly ICompanyContactService _CompanyContactService;
         private readonly MPC.Interfaces.WebStoreServices.ICampaignService _ICampaignService;
         public ReportEmailController(IReportService IReportService, MPC.Interfaces.WebStoreServices.ICampaignService ICampaignService, ICompanyContactService CompanyContactService)
        {
            if (IReportService == null)
            {
                throw new ArgumentNullException("IReportService");
            }
            this._IReportService = IReportService;
            this._ICampaignService = ICampaignService;
            this._CompanyContactService = CompanyContactService;

        }
         public ReportEmailResponse Get([FromUri] ReportEmailRequestModel request)
         {

             if(request.ReportType == MPC.Models.Common.ReportType.Internal)
             {
                 string Path = _IReportService.GetInternalReportEmailBaseData(request);
                 return new ReportEmailResponse
                 {
                     To = string.Empty,
                     Subject = string.Empty,
                     Attachment = "MPCInternalReport.pdf",
                     AttachmentPath = Path,
                     CC = string.Empty,
                     Signature = string.Empty

                 };

             }
             else
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

         [ApiException]
         public void Post(ReportEmailResponse request)
         {




             _IReportService.SendEmail(request.To,request.CC, request.Subject, request.Signature, request.ContactId,request.SignedBy,request.AttachmentPath);
            



         }



    }
}
