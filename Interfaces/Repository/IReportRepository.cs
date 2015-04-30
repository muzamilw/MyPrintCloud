﻿using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface IReportRepository
    {
        List<Report> GetReportsByOrganisationID(long OrganisationID);

        List<ReportNote> GetReportNotesByOrganisationID(long OrganisationID);

        Report GetReportByReportID(long iReportID);
        List<ReportCategory> GetReportCategories();
        ReportCategory GetReportCategory(long CategoryId);

        List<usp_JobCardReport_Result> getJobCardReportResult(long OrganisationID, long OrderID, long ItemID);

        List<usp_OrderReport_Result> getOrderReportResult(long OrganisationID, long OrderID);

        DataTable GetReportDataSourceByReportID(long ReportID, string CriteriaParam);

        ReportNote GetReportNoteByCompanyId(long CompanyId);

        List<usp_EstimateReport_Result> getEstimateReportResult(long OrganisationID, long EstimateID);

        List<usp_InvoiceReport_Result> getInvoiceReportResult(long OrganisationID, long InvoiceID);
    }
}
