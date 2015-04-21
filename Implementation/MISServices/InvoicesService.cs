using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

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
        #endregion
    }
}
