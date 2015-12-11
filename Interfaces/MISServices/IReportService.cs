using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrapeCity.ActiveReports;
using MPC.Models.ResponseModels;
using MPC.Models.RequestModels;


namespace MPC.Interfaces.MISServices
{
    public interface IReportService
    {
        List<ReportCategory> GetReportCategories();
        ReportCategory GetReportCategory(long CategoryId, int IsExternal);
        SectionReport GetReport(int iReportID, long itemid,int ComboValue,string DateFrom,string DateTo,string ParamTextBoxValue);

        List<StoresListResponse> GetStoreNameByOrganisationId();

        List<ReportNote> GetReportNoteByCompanyID(long CompanyID);

        IEnumerable<ReportNote> Update(IEnumerable<ReportNote> reportNotes);
        List<ReportparamResponse> getParamsById(long Id);

        ReportEmailResponseModel GetReportEmailBaseData(ReportEmailRequestModel request);

        void SendEmail(string EmailTo, string EmailCC, string EmailSubject, string Signature, long ContactId,Guid SystemUserId, string Path);

        string GetInternalReportEmailBaseData(ReportEmailRequestModel request);

        string DownloadExternalReport(int ReportId, bool isPDF);

        //SectionReport GetReportByParams(long ReportId, long ComboValue, string DateFrom, string DateTo, string ParamValue);
    }
}
