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

        private readonly Dictionary<InvoiceByColumn, Func<Invoice, object>> invoiceOrderByName = new Dictionary<InvoiceByColumn, Func<Invoice, object>>
                    {
                        {InvoiceByColumn.InvoiceName, d => d.InvoiceName}
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

        public InvoiceRequestResponseModel GetInvoicesList(InvoicesRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            bool isStatusSpecified = request.Status == 0;

            bool isStringSpecified = !string.IsNullOrEmpty(request.SearchString);
            bool isSectionFlagZero = request.FilterFlag != 0;
            bool isEstimateTypeZero = request.OrderTypeFilter != 2; // 2 -> All

            Expression<Func<Invoice, bool>> query =
                invoice => (!isStringSpecified
                    || invoice.Company.Name.Contains(request.SearchString) || invoice.InvoiceCode.Contains(request.SearchString))

                    &&
                    (!isSectionFlagZero || invoice.FlagID==request.FilterFlag)
                     &&
                    (!isEstimateTypeZero || invoice.InvoiceType == request.OrderTypeFilter)

                    && invoice.OrganisationId == OrganisationId && invoice.IsArchive != true &&
                    ((!isStatusSpecified && invoice.InvoiceStatus == request.Status || isStatusSpecified));
            int rowCount = DbSet.Count(query);
            IEnumerable<Invoice> invoices = request.IsAsc
                ? DbSet.Where(query)
                    .OrderBy(invoiceOrderByName[request.ItemOrderBy])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet.Where(query)
                    .OrderByDescending(invoiceOrderByName[request.ItemOrderBy])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();
            return new InvoiceRequestResponseModel
            {
                RowCount = rowCount,
                Invoices = invoices
            };
        }

        public InvoiceBaseResponse GetInvoiceBaseResponse()
        {
            Organisation org = db.Organisations.Where(o => o.OrganisationId == this.OrganisationId).FirstOrDefault();
            return new InvoiceBaseResponse
            {
                SystemUsers = db.SystemUsers.Where(o => o.OrganizationId == this.OrganisationId && o.IsAccountDisabled != 1).ToList(),
                SectionFlags = db.SectionFlags.Where(o => o.OrganisationId == this.OrganisationId && o.SectionId == (int)SectionEnum.Invoices),
                CurrencySymbol = org.Currency != null ? org.Currency.CurrencySymbol : "$"
            };
        }

        /// <summary>
        /// Get Invoice By Id
        /// </summary>
        public Invoice GetInvoiceById(long Id)
        {
            return DbSet.FirstOrDefault(i => i.InvoiceId == Id);
        }

        /// <summary>
        /// Get Invoice By Estimate Id
        /// </summary>
        public Invoice GetInvoiceByEstimateId(long Id)
        {
            return DbSet.FirstOrDefault(i => i.EstimateId == Id);
        }

        /// <summary>
        /// Get flag for INVOICE
        /// </summary>
        public long GetInvoieFlag()
        {
            return db.SectionFlags.Where(c => c.SectionId == (int)SectionEnum.Invoices).Select(c => c.SectionFlagId).FirstOrDefault();
        }
        #endregion
    }
}
