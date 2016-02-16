
using System.Collections.Generic;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.MISServices
{
    /// <summary>
    /// Invoice Service Interfcace
    /// </summary>
    public interface IInvoiceService
    {
        /// <summary>
        /// Get Invoices list
        /// </summary>
        InvoiceRequestResponseModel SearchInvoices(GetInvoicesRequestModel request);
        InvoiceRequestResponseModel GetInvoicesList(InvoicesRequestModel request);
        InvoiceBaseResponse GetInvoiceBaseResponse();
        Invoice GetInvoiceById(long Id);
        Invoice SaveInvoice(Invoice request);
        List<ZapierInvoiceDetail> GetZapierInvoiceDetail(long organisationId);
        void PostDataToZapier(long invoiceId);
        List<ZapierInvoiceDetail> GetInvoiceDetailForZapierPooling(long organisationId);
        void ArchiveInvoice(int InvoiceId);
        void UpdateInvoiceFromZapier(ZapierInvoiceDetail zapInvoice, long organisationId);
    }
}
