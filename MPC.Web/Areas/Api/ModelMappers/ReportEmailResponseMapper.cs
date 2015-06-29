using MPC.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MPC.Models.DomainModels;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class ReportEmailResponseMapper
    {

        public static MPC.Models.ResponseModels.ReportEmailResponse CreateFrom(this ReportEmailResponse source)
        {
            return new MPC.Models.ResponseModels.ReportEmailResponse
            {
                To = source.To,
                CC = source.CC,
                Subject = source.Subject,
                Signature = source.Signature,
                ContactId = source.ContactId


          

            };
        }
        public static ReportEmailResponse CreateFrom(this MPC.Models.ResponseModels.ReportEmailResponse source)
        {
            return new ReportEmailResponse
            {
                To = source.To,
                CC = source.CC,
                Subject = source.Subject,
                Signature = source.Signature,
                ContactId = source.ContactId

            };
        }

    }
}