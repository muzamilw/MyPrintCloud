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

        public List<ZapierInvoiceDetail> GetZapierInvoiceDetails(long invoiceId)
        {
            List<ZapierInvoiceDetail> lstInvoiceDetails = new List<ZapierInvoiceDetail>();
            var inv = DbSet.FirstOrDefault(i => i.InvoiceId == invoiceId);

            if (inv != null)
            {
                var address = inv.Company != null
                    ? inv.Company.Addresses.FirstOrDefault(a => a.AddressId == (inv.AddressId ?? 0)): null;
                lstInvoiceDetails.Add(new ZapierInvoiceDetail
                {
                    CustomerName = inv.Company != null ? inv.Company.Name : string.Empty,
                    Address1 = address != null ? address.Address1 : "",
                    Address2 = address != null ? address.Address2 : "",
                    AddressCity = address != null ? address.City : "",
                    AddressCountry = address != null ? address.Country != null ? address.Country.CountryName : "" : "",
                    AddressName = address != null ? address.AddressName : "",
                    AddressState = address != null ? address.State != null ? address.State.StateName : "" : "",
                    AddressPostalCode = address != null ? address.PostCode : "",
                    VatNumber = inv.Company != null ? inv.Company.VATRegNumber : string.Empty,
                    CustomerUrl = inv.Company != null ? inv.Company.URL : string.Empty,
                    ContactFirstName = inv.CompanyContact != null ? inv.CompanyContact.FirstName : string.Empty,
                    ContactLastName = inv.CompanyContact != null ? inv.CompanyContact.LastName : string.Empty,
                    ContactEmail = inv.CompanyContact != null ? inv.CompanyContact.Email : string.Empty,
                    ContactPhone = inv.CompanyContact != null ? inv.CompanyContact.HomeTel1 : string.Empty,
                    TaxRate = inv.Company != null ? inv.Company.TaxRate??0 : 0,
                    InvoiceCode = inv.InvoiceCode,
                    InvoiceDate = inv.InvoiceDate ?? DateTime.Now,
                    InvoiceId = inv.InvoiceId,
                    ContactId = inv.CompanyContact != null ? inv.CompanyContact.ContactId : 0,
                    InvoiceItems = inv.Items.Select(p => new ZapierInvoiceItem
                    {
                        ProductCode = p.ProductCode,
                        ProductDescription = p.ProductSpecification,
                        Quantity = p.Qty1 ?? 0,
                        NetTotal = p.Qty1NetTotal ?? 0,
                        TaxValue = p.Qty1Tax1Value ?? 0,
                        GrossTotal = p.Qty1GrossTotal ?? 0,
                        ProductName = p.ProductName

                    }).ToList()
                });
            }
            return lstInvoiceDetails;    
            ////var invd = DbSet
            ////    .Where(i => i.OrganisationId == organizationId && i.IsRead == false).ToList();
            ////if (invd.Any())
            ////{
            ////   var zapDetail = invd.Select(i => new
            ////    {
            ////        CustomerName = i.Company != null? i.Company.Name : string.Empty,
            ////        URL = i.Company != null? i.Company.URL : string.Empty,
            ////        TaxRate = i.Company != null ? i.Company.TaxRate??0 : 0,
            ////        VatNumber = i.Company != null ? i.Company.VATRegNumber: string.Empty,
            ////        FirstName = i.CompanyContact != null ? i.CompanyContact.FirstName : string.Empty,
            ////        LastName = i.CompanyContact != null ?i.CompanyContact.LastName: string.Empty,
            ////        Email = i.CompanyContact != null ?i.CompanyContact.Email:string.Empty,
            ////        Phone = i.CompanyContact != null ?i.CompanyContact.HomeTel1:string.Empty,
            ////        ContactId = i.CompanyContact != null ? i.CompanyContact.ContactId : 0,
            ////        BillingAddresss = i.Company != null ? i.Company.Addresses.Where(a => a.AddressId == (i.AddressId?? 0)).FirstOrDefault() : null,
            ////        InvoiceCode = i.InvoiceCode,
            ////        InvoiceDate = i.InvoiceDate,
            ////        InvoiceId = i.InvoiceId,
            ////        InvoicedItems = i.Items.Select(p => new ZapierInvoiceItem
            ////        {
            ////            ProductCode = p.ProductCode,
            ////            ProductDescription = p.ProductSpecification,
            ////            Quantity = p.Qty1?? 0,
            ////            NetTotal = p.Qty1NetTotal ?? 0,
            ////            TaxValue = p.Qty1Tax1Value ?? 0,
            ////            GrossTotal = p.Qty1GrossTotal?? 0,
            ////            ProductName = p.ProductName
                        
            ////        }).ToList()
                    

            ////    }).ToList().Select(c => new ZapierInvoiceDetail
            ////    {
            ////        CustomerName = c.CustomerName,
            ////        Address1 = c.BillingAddresss != null ? c.BillingAddresss.Address1 : "",
            ////        Address2 = c.BillingAddresss != null ? c.BillingAddresss.Address2 : "",
            ////        AddressCity = c.BillingAddresss != null ? c.BillingAddresss.City : "",
            ////        AddressCountry = c.BillingAddresss != null ? c.BillingAddresss.Country != null ? c.BillingAddresss.Country.CountryName: "" : "",
            ////        AddressName = c.BillingAddresss != null ? c.BillingAddresss.AddressName : "",
            ////        AddressState = c.BillingAddresss != null ? c.BillingAddresss.State != null ? c.BillingAddresss.State.StateName: "" : "",
            ////        AddressPostalCode = c.BillingAddresss != null ? c.BillingAddresss.PostCode : "",
            ////        VatNumber = c.VatNumber,
            ////        CustomerUrl = c.URL,
            ////        ContactFirstName = c.FirstName,
            ////        ContactLastName = c.LastName,
            ////        ContactEmail = c.Email,
            ////        ContactPhone = c.Phone,
            ////        TaxRate = c.TaxRate,
            ////        InvoiceItems = c.InvoicedItems,
            ////        InvoiceCode = c.InvoiceCode,
            ////        InvoiceDate = c.InvoiceDate ?? DateTime.Now,
            ////        InvoiceId = c.InvoiceId,
            ////        ContactId = c.ContactId

            ////    }).ToList();
            //    return zapDetail;
            //}
            //else
            //{
            //    return null;
            //}
            

        }
        #endregion
    }
}
