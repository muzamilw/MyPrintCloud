using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Models.DomainModels;

namespace MPC.Implementation.MISServices
{
    public class InvoicesService : IInvoiceService
    {
         #region Private

        private readonly IInvoiceRepository invoiceRepository;


        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public InvoicesService(IInvoiceRepository invoiceRepository)
        {
            this.invoiceRepository = invoiceRepository;
        }
        #endregion
        #region Public
        /// <summary>
        /// Get Invoices list
        /// </summary>
        public InvoiceRequestResponseModel GetInvoicesList(InvoicesRequestModel request)
        {
            return invoiceRepository.GetInvoicesList(request);
        }

        public InvoiceRequestResponseModel SearchInvoices(GetInvoicesRequestModel request)
        {
            return invoiceRepository.SearchInvoices(request);
        }
        public InvoiceBaseResponse GetInvoiceBaseResponse ()
        {
            return invoiceRepository.GetInvoiceBaseResponse();
        }

        public Invoice GetInvoiceById(long Id)
        {
            return invoiceRepository.GetInvoiceById(Id);
        }

        public Invoice SaveInvoice(Invoice request)
        {
            request.OrganisationId = invoiceRepository.OrganisationId;
            if (request.InvoiceId > 0)
            {
                return UpdateInvoice(request);
            }
            else
            {
                return SaveNewInvoice(request);
            }
        }
        private Invoice UpdateInvoice(Invoice invoice)
        {
            Invoice oInvoice = invoiceRepository.Find(invoice.InvoiceId);
            oInvoice.InvoiceName = invoice.InvoiceName;
            oInvoice.InvoiceType = invoice.InvoiceType;
            oInvoice.Status = invoice.Status;
            oInvoice.InvoiceDate = invoice.InvoiceDate;
            oInvoice.InvoiceStatus = invoice.InvoiceStatus;
            oInvoice.FlagID = invoice.FlagID;
            oInvoice.HeadNotes = invoice.HeadNotes;
            oInvoice.FootNotes = invoice.FootNotes;
            oInvoice.GrandTotal = invoice.GrandTotal;
            oInvoice.CompanyId = invoice.CompanyId;
            oInvoice.ContactId = invoice.ContactId;
            oInvoice.AddressId = invoice.AddressId;
            return UpdateInvoiceDetails(invoice, oInvoice);

        }
        private Invoice UpdateInvoiceDetails(Invoice invoice, Invoice dbVersion)
        {
            if(invoice.InvoiceDetails != null)
            {
                foreach(var detail in invoice.InvoiceDetails)
                {
                    InvoiceDetail invDetailDB = dbVersion.InvoiceDetails.Where(i => i.InvoiceDetailId == detail.InvoiceDetailId).FirstOrDefault();
                    if(invDetailDB != null)
                    {
                        detail.InvoiceTitle = invDetailDB.InvoiceTitle;
                        detail.Quantity = invDetailDB.Quantity;
                        detail.ItemCharge = invDetailDB.ItemCharge;
                        detail.ItemTaxValue = invDetailDB.ItemTaxValue;
                    }
                }
            }
            invoiceRepository.SaveChanges();
            return invoice;
        }
        private Invoice SaveNewInvoice(Invoice invoice)
        {
            return invoice;
        }
        #endregion
    }
}
