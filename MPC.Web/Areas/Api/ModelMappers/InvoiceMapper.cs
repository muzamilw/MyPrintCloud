using MPC.MIS.Areas.Api.Models;
using System.Linq;
using Invoice = MPC.Models.DomainModels.Invoice;

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
        private static Models.Invoice CreateFrom(this Invoice source)
        {
            return new Models.Invoice
            {
                InvoiceId = source.InvoiceId,
                CompanyName = source.Company!=null ? source.Company.Name: "",
                InvoiceCode = source.InvoiceCode,
                InvoiceName = source.InvoiceName,
                IsArchive = source.IsArchive,
                InvoiceDate = source.InvoiceDate,
                InvoiceTotal = source.InvoiceTotal,
                ContactName = source.CompanyContact != null ? source.CompanyContact.FirstName + " " + source.CompanyContact.LastName : "",
                Status = source.Status.StatusName
                
            };
        }

    }
}