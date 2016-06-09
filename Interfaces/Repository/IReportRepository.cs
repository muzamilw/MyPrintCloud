using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface IReportRepository : IBaseRepository<Report, long>
    {
        long GetReportIdByName(string ReportName);
        List<Report> GetReportsByOrganisationID(long OrganisationID);

        List<ReportNote> GetReportNotesByOrganisationID(long OrganisationID);

        Report GetReportByReportID(long iReportID);
        List<ReportCategory> GetReportCategories();
        ReportCategory GetReportCategory(long CategoryId, int IsExternal);

        List<usp_JobCardReport_Result> getJobCardReportResult(long OrganisationID, long OrderID, long ItemID);

        List<usp_OrderReport_Result> getOrderReportResult(long OrganisationID, long OrderID);

        DataTable GetReportDataSourceByReportID(long ReportID, string CriteriaParam, long WebStoreOrganisationId = 0);

        List<ReportNote> GetReportNoteByCompanyId(long CompanyId);

        List<usp_EstimateReport_Result> getEstimateReportResult(long OrganisationID, long EstimateID);

        List<usp_InvoiceReport_Result> getInvoiceReportResult(long OrganisationID, long InvoiceID);

        void UpdateReportNotes(List<ReportNote> reportNotes);
        List<ReportparamResponse> getParamsById(long Id);

        List<usp_PurchaseOrderReport_Result> GetPOReport(long PurchaseId);

        ReportEmailResponseModel GetReportEmailBaseData(ReportEmailRequestModel request, string Path);

        List<usp_DeliveryReport_Result> GetDeliveryNoteReport(long deliveryId);

        List<Reportparam> getReportParamsByReportId(long ReportId);

        Report CheckCustomReportOfOrg(long reportId);

        long CheckCustomReportForPOEmail();

        bool isCorporateCustomer(int StoreId);

        string GetReportName(long ReportId);

    }
}
