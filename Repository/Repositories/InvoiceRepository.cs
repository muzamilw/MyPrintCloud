using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Invoce Repositroy 
    /// </summary>
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        #region Private
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Invoice> DbSet
        {
            get
            {
                return db.Invoices;
            }
        }


        /// <summary>
        /// For Sorring
        /// </summary>
        private readonly Dictionary<InvoiceByColumn, Func<Invoice, object>> invoiceOrderByClause = new Dictionary<InvoiceByColumn, Func<Invoice, object>>
                    {
                        {InvoiceByColumn.CompanyName, d => d.Company.Name}
                    };
        #endregion
        #region Constructor
        public InvoiceRepository(IUnityContainer container)
            : base(container)
        {
     
        }
        #endregion
        #region Pubilc

        /// <summary>
        /// Get Invoices list
        /// </summary>
        public InvoiceRequestResponseModel SearchInvoices(GetInvoicesRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            bool isStringSpecified = !string.IsNullOrEmpty(request.SearchString);
            Expression<Func<Invoice, bool>> query =
                invoice => invoice.CompanyId == request.CompanyId;
            int rowCount = DbSet.Count(query);
            IEnumerable<Invoice> invoices = request.IsAsc
                ? DbSet.Where(query)
                    .OrderBy(invoiceOrderByClause[request.ItemInvoiceBy])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet.Where(query)
                    .OrderByDescending(invoiceOrderByClause[request.ItemInvoiceBy])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();
            return new InvoiceRequestResponseModel
            {
                RowCount = rowCount,
                Invoices = invoices
            };
        }

        #endregion
    }
}
