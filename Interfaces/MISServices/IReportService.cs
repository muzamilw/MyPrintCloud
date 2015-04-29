using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrapeCity.ActiveReports;


namespace MPC.Interfaces.MISServices
{
    public interface IReportService
    {
        List<ReportCategory> GetReportCategories();
        ReportCategory GetReportCategory(long CategoryId);
        SectionReport GetReport(int iReportID);
    }
}
