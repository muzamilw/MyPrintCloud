using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.WebStoreServices
{
    /// <summary>
    /// Invoice Service
    /// </summary>
    public class InvoiceService 
    {
        #region Private

        private readonly IInvoiceRepository invoiceRepository;


        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            this.invoiceRepository = invoiceRepository;
        }
        #endregion
        #region Public
        /// <summary>
        /// Get Invoices list
        /// </summary>
        public InvoiceRequestResponseModel SearchInvoices(GetInvoicesRequestModel request)
        {
            
            var result = invoiceRepository.SearchInvoices(request);
            return result;
        }
        #endregion
    }
}
