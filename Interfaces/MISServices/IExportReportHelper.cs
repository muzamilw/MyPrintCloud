using MPC.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.MISServices
{
    public interface IExportReportHelper
    {

        string ExportPDF(int iReportID, long iRecordID, ReportType type, long OrderID, string CriteriaParam, long WebStoreOrganisationId = 0, bool isFromExternal = true);

        string ExportOrderReportXML(long iRecordID, string OrderCode, string XMLFormat, long WebStoreOrganisationId = 0);

        string ExportExcel(int iReportID, long iRecordID, ReportType type, long OrderID, string CriteriaParam , long WebStoreOrganisationId = 0,bool isFromExternal = true);

        string ExportProductTemplateReportPdf(int reportId, List<usp_GetStoreProductTemplatesList_Result> dataSource,
            bool isPdf, long organisationId);
    }
}
