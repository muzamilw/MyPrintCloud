using MPC.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.MISServices
{
    public interface IExportReportHelper
    {

        string ExportPDF(int iReportID, long iRecordID, ReportType type, long OrderID, string CriteriaParam);

        string ExportOrderReportXML(long iRecordID, string OrderCode, string XMLFormat);

        string ExportExcel(int iReportID, long iRecordID, ReportType type, long OrderID, string CriteriaParam);
    }
}
