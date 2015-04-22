using System;
using MPC.MIS.Areas.Api.Models;
using System.Linq;
using Invoice = MPC.Models.DomainModels.Invoice;
using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class InvoiceMapper
    {
        /// <summary>
        /// Domain response to Web response invoice mapper
        /// </summary>
        public static InvoiceRequestResponseModel CreateFrom(this MPC.Models.ResponseModels.InvoiceRequestResponseModel source)
        {
            return new InvoiceRequestResponseModel
            {
                RowCount = source.RowCount,
                Invoices = source.Invoices.Select(invoice => invoice.CreateFrom())
            };
        }

        /// <summary>
        /// Domain invoice to web invoice mapper
        /// </summary>
        public static Models.Invoice CreateFrom(this Invoice source)
        {
            return new Models.Invoice
            {
                InvoiceId = source.InvoiceId,
                CompanyId = source.CompanyId,
                ContactId = source.ContactId,
                CompanyName = source.Company!=null ? source.Company.Name: "",
                InvoiceCode = source.InvoiceCode,
                InvoiceName = source.InvoiceName,
                IsArchive = source.IsArchive,
                InvoiceDate = source.InvoiceDate,
                InvoiceTotal = Math.Round((double) source.InvoiceTotal,2),
                ContactName = source.CompanyContact != null ? source.CompanyContact.FirstName + " " + source.CompanyContact.LastName : "",
                Status =source.Status!=null  ? source.Status.StatusName: "-",
                FlagId = source.FlagID,
                InvoiceType = source.InvoiceType,
                GrandTotal = Math.Round((double)source.GrandTotal, 2),
                OrderNo = source.OrderNo,
                AccountNumber = source.AccountNumber,
                ReportSignedBy = source.ReportSignedBy,
                InvoiceDetails = source.InvoiceDetails != null ? source.InvoiceDetails.Select(i => i.CreateFrom()).ToList() : new List<InvoiceDetail>()
            };
        }
        public static Invoice CreateFrom(this Models.Invoice source)
        {
            return new Invoice
            {
                InvoiceId = source.InvoiceId,
                CompanyId = source.CompanyId,
                ContactId = source.ContactId,
                InvoiceCode = source.InvoiceCode,
                InvoiceName = source.InvoiceName,
                IsArchive = source.IsArchive,
                InvoiceDate = source.InvoiceDate,
                InvoiceTotal = Math.Round((double) source.InvoiceTotal,2),
                FlagID = source.FlagId,
                InvoiceType = source.InvoiceType,
                GrandTotal = Math.Round((double)source.GrandTotal, 2),
                OrderNo = source.OrderNo,
                AccountNumber = source.AccountNumber,
                ReportSignedBy = source.ReportSignedBy,
                InvoiceDetails = source.InvoiceDetails != null ? source.InvoiceDetails.Select(i => i.CreateFrom()).ToList() : null
            };
        }

        private static Models.InvoicesListModel CreateForList(this Invoice source)
        {
            return new Models.InvoicesListModel
            {
                InvoiceId = source.InvoiceId,
                CompanyName = source.Company != null ? source.Company.Name : "",
                InvoiceCode = source.InvoiceCode,
                InvoiceName = source.InvoiceName,
                InvoiceDate = source.InvoiceDate,
                GrandTotal = Math.Round((double)source.GrandTotal, 2),
                StatusId = source.Status != null ? source.Status.StatusId : 0,
                FlagId = source.FlagID,
                OrderNo = source.OrderNo,
                ItemsCount = source.InvoiceDetails != null ? source.InvoiceDetails.Count() : 0
            };
        }

        public static InvoiceListResponseModel CreateFromList(this MPC.Models.ResponseModels.InvoiceRequestResponseModel source)
        {
            return new  InvoiceListResponseModel
            {
                RowCount = source.RowCount,
                Invoices = source.Invoices.Select(invoice => invoice.CreateForList())
            };
            
        }

    }
}