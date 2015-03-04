﻿using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Invoce Repositroy interface
    /// </summary>
    public interface IInvoiceRepository : IBaseRepository<Invoice, long>
    {
        /// <summary>
        /// Get Invoices list
        /// </summary>
       InvoiceRequestResponseModel SearchInvoices(GetInvoicesRequestModel request);
    }
}