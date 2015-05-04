using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrapeCity.ActiveReports;
using MPC.Models.ResponseModels;


namespace MPC.Interfaces.MISServices
{
    public interface IReportService
    {
        List<ReportCategory> GetReportCategories();
        ReportCategory GetReportCategory(long CategoryId, int IsExternal);
        SectionReport GetReport(int iReportID, long itemid);

        List<StoresListResponse> GetStoreNameByOrganisationId();

        List<ReportNote> GetReportNoteByCompanyID(long CompanyID);
    }
}
