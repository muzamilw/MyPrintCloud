using System.Data.SqlTypes;
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
        private readonly Dictionary<InvoiceByColumn, Func<Invoice, object>> invoiceOrderByCreationDate = new Dictionary<InvoiceByColumn, Func<Invoice, object>>
                    {
                        {InvoiceByColumn.CreationDate, d => d.CreationDate}
                    };
        private readonly Dictionary<InvoiceByColumn, Func<Invoice, object>> invoiceOrderByDate = new Dictionary<InvoiceByColumn, Func<Invoice, object>>
                    {
                        {InvoiceByColumn.InvoiceDate, d => d.InvoiceDate}
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
                    || invoice.Company.Name.Contains(request.SearchString) || invoice.InvoiceCode.Contains(request.SearchString) || invoice.RefOrderCode.Contains(request.SearchString)
                    || invoice.InvoiceName.Contains(request.SearchString)
                    || (invoice.CompanyContact != null && invoice.CompanyContact.FirstName.Contains(request.SearchString))
                    || (invoice.CompanyContact != null && invoice.CompanyContact.LastName.Contains(request.SearchString)))
                    &&
                    (!isSectionFlagZero || invoice.FlagID==request.FilterFlag)
                     &&
                    (!isEstimateTypeZero || invoice.InvoiceType == request.OrderTypeFilter)

                    && invoice.OrganisationId == OrganisationId && invoice.IsArchive != true &&
                    ((!isStatusSpecified && invoice.InvoiceStatus == request.Status || isStatusSpecified));
            int rowCount = DbSet.Count(query);
            IEnumerable<Invoice> invoices = request.IsAsc
                ? DbSet.Where(query)
                    .OrderBy(invoiceOrderByDate[request.ItemOrderBy])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet.Where(query)
                    .OrderByDescending(invoiceOrderByDate[request.ItemOrderBy])
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

            if (inv != null && inv.InvoiceStatus == Convert.ToInt16(InvoiceStatuses.Posted))
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
                    InvoiceStatus = inv.OrderNo,
                    InvoiceItems = inv.Items.Select(p => new ZapierInvoiceItem
                    {
                        ProductCode = p.ProductCode,
                        ProductDescription = p.ProductSpecification,
                        Quantity = p.Qty1 ?? 0,
                        NetTotal = p.Qty1NetTotal ?? 0,
                        TaxValue = p.Qty1Tax1Value ?? 0,
                        GrossTotal = p.Qty1GrossTotal ?? 0,
                        ProductName = p.ProductName,
                        PricePerUnit = ((p.Qty1NetTotal ?? 0) / (p.Qty1 ?? 1))

                    }).ToList()

                });
                if (inv.InvoiceDetails != null && lstInvoiceDetails.Count > 0)
                {
                    inv.InvoiceDetails.ToList()
                        .ForEach(id => lstInvoiceDetails[0].InvoiceItems.Add(new ZapierInvoiceItem
                        {
                            ProductCode = Convert.ToString(id.InvoiceDetailId),
                            ProductDescription = id.Description,
                            Quantity = Convert.ToInt32(id.Quantity),
                            NetTotal = id.ItemCharge,
                            TaxValue = id.TaxValue ?? 0,
                            GrossTotal = id.ItemGrossTotal ?? 0,
                            ProductName = id.InvoiceTitle,
                            PricePerUnit = ((id.ItemCharge) / (id.Quantity))
                        }));
                }
            }
            return lstInvoiceDetails;    
            
            

        }

        public List<ZapierInvoiceDetail> GetInvoiceDetailForZapierPolling(long organisationId)
        {
            //List<ZapierInvoiceDetail> lstInvoiceDetails = new List<ZapierInvoiceDetail>();
            //lstInvoiceDetails.Add(new ZapierInvoiceDetail
            //{
            //    CustomerName = "Sample Company My Print Store",
            //    Address1 = "Sample Address 1",
            //        Address2 = "Sample Address 2",
            //        AddressCity = "Sydney",
            //        AddressCountry = "Australia",
            //        AddressState = "Australian Capital Territory (ACT)",
            //        AddressName = "Head Offiec Address",
            //        AddressPostalCode = "1234",
            //        ContactId = 121,
            //        ContactFirstName = "John",
            //        ContactLastName = "Doe",
            //        ContactEmail = "john_doe@myprintstore.com",
            //        ContactPhone = "+61 121 234 4567",
            //        VatNumber = "ATG101",
            //        CustomerUrl = "http://www.myprintstore.com",
            //        TaxRate = 20,
            //    InvoiceCode = "INV-001-1214",
            //    InvoiceDate = DateTime.Now,
            //    InvoiceDueDate = DateTime.Now.AddDays(1),
            //    InvoiceId = 1144
            //});
            //List<ZapierInvoiceItem> zapInvItems = new List<ZapierInvoiceItem>();

            //zapInvItems.Add(new ZapierInvoiceItem
            //{
            //    ProductCode = "ITM-001-1144",
            //    ProductDescription = "This is sample product pooling to Zapier sample data",
            //    Quantity = 1000,
            //    PricePerUnit = 2.5,
            //    NetTotal = 2500,
            //    TaxValue = 500,
            //    GrossTotal = 3000,
            //    ProductName = "Sample Business Cards"

            //});
            //zapInvItems.Add(new ZapierInvoiceItem
            //{
            //    ProductCode = "ITM-001-1145",
            //    ProductDescription = "This is second sample product pooling to Zapier sample data",
            //    Quantity = 2000,
            //    PricePerUnit = 2.00,
            //    NetTotal = 4000,
            //    TaxValue = 800,
            //    GrossTotal = 4800,
            //    ProductName = "Sample Gloss Matt Business Cards"

            //});
            //lstInvoiceDetails.FirstOrDefault().InvoiceItems = zapInvItems;
            //return lstInvoiceDetails; 
            List<ZapierInvoiceDetail> lstInvoiceDetails = new List<ZapierInvoiceDetail>();
            var inv = DbSet.ToList().LastOrDefault(i => i.InvoiceStatus == Convert.ToInt16(InvoiceStatuses.Posted) && i.OrganisationId == organisationId);

            if (inv != null)
            {
                var address = inv.Company != null
                    ? inv.Company.Addresses.FirstOrDefault(a => a.AddressId == (inv.AddressId ?? 0)) : null;
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
                    TaxRate = inv.Company != null ? inv.Company.TaxRate ?? 0 : 0,
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
                        ProductName = p.ProductName,
                        PricePerUnit = ((p.Qty1GrossTotal ?? 0) / (p.Qty1 ?? 0))

                    }).ToList()
                });
            }
            return lstInvoiceDetails; 
        }


        public void ArchiveInvoice(int InvoiceId)
        {
            try
            {
                Invoice targetInvoice = db.Invoices.Where(c => c.InvoiceId == InvoiceId).FirstOrDefault();
                if(targetInvoice != null)
                {
                    targetInvoice.InvoiceStatus = (int)InvoiceStatuses.Archived;
                    db.SaveChanges();
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Invoice GetInvoiceByCode(string sInvoiceCode, long organisationId)
        {
            return DbSet.FirstOrDefault(i => i.InvoiceCode == sInvoiceCode && i.OrganisationId == organisationId);
        }

        public List<usp_ExportInvoice_Result> GetInvoiceDataForExport(long InvoiceId)
        {
            return db.usp_ExportInvoice(InvoiceId).ToList();
        }

        
        #endregion
    }
}
