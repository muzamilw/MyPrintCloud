using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Microsoft.Practices.Unity;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Interfaces.Repository;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;
using System.Data.Entity;


namespace MPC.Repository.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        #region Private
        private readonly Dictionary<CompanyByColumn, Func<Company, object>> companyOrderByClause = new Dictionary<CompanyByColumn, Func<Company, object>>
                    {
                        {CompanyByColumn.Code, d => d.CompanyId},
                        {CompanyByColumn.Name, c => c.Name},
                        {CompanyByColumn.Type, d => d.TypeId},
                        {CompanyByColumn.Status, d => d.Status}
                    };
        #endregion
        public CompanyRepository(IUnityContainer container)
            : base(container)
        {
        }

        protected override IDbSet<Company> DbSet
        {
            get
            {
                return db.Companies;
            }
        }

        public override IEnumerable<Company> GetAll()
        {
            return DbSet.Where(c => c.OrganisationId == OrganisationId).ToList();
        }

        public long GetStoreIdFromDomain(string domain)
        {
            var companyDomain = db.CompanyDomains.Where(d => d.Domain.Contains(domain)).ToList();
            if (companyDomain.FirstOrDefault() != null)
            {
                return companyDomain.FirstOrDefault().CompanyId;

            }
            else
            {
                return 0;
            }
        }

        public CompanyResponse GetCompanyById(long companyId)
        {
            CompanyResponse companyResponse = new CompanyResponse();
            var company = DbSet.Find(companyId);
            companyResponse.SecondaryPageResponse = new SecondaryPageResponse();
            companyResponse.SecondaryPageResponse.RowCount = company.CmsPages.Count;
            companyResponse.SecondaryPageResponse.CmsPages = company.CmsPages.Take(5).ToList();
            companyResponse.Company = company;

            //companyResponse.CompanyTerritoryResponse = new CompanyTerritoryResponse();
            //companyResponse.AddressResponse = new AddressResponse();
            //companyResponse.CompanyContactResponse = new CompanyContactResponse();
            //companyResponse.CompanyTerritoryResponse.RowCount = company.CompanyTerritories.Count();
            //companyResponse.CompanyTerritoryResponse.CompanyTerritories = company.CompanyTerritories.Take(5).ToList();
            //companyResponse.AddressResponse.RowCount = company.Addresses.Count();
            //companyResponse.AddressResponse.Addresses = company.Addresses.Take(5).ToList();
            //companyResponse.CompanyContactResponse.CompanyContacts = company.CompanyContacts.Take(5).ToList();
            //companyResponse.CompanyContactResponse.RowCount = company.CompanyContacts.Count;
            return companyResponse;
        }

        /// <summary>
        /// Get Companies list for Companies List View
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CompanyResponse SearchCompanies(CompanyRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            bool isStringSpecified = !string.IsNullOrEmpty(request.SearchString);
            Expression<Func<Company, bool>> query =
                s =>
                    (isStringSpecified && (s.Name.Contains(request.SearchString)) ||
                     !isStringSpecified);

            int rowCount = DbSet.Count(query);
            IEnumerable<Company> companies = request.IsAsc
                ? DbSet.Where(query)
                    .OrderBy(companyOrderByClause[request.CompanyByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet.Where(query)
                    .OrderByDescending(companyOrderByClause[request.CompanyByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();
            return new CompanyResponse
            {
                RowCount = rowCount,
                Companies = companies
            };
        }

        /// <summary>
        /// Get Suppliers For Inventories
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SupplierSearchResponseForInventory GetSuppliersForInventories(SupplierRequestModelForInventory request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            bool isStringSpecified = !string.IsNullOrEmpty(request.SearchString);
            Expression<Func<Company, bool>> query =
                s =>
                    (isStringSpecified && (s.Name.Contains(request.SearchString)) ||
                     !isStringSpecified) && s.IsCustomer == 0;

            int rowCount = DbSet.Count(query);
            IEnumerable<Company> companies =
                DbSet.Where(query).OrderByDescending(x => x.Name)
                     .Skip(fromRow)
                    .Take(toRow)
                    .ToList();

            return new SupplierSearchResponseForInventory
            {
                TotalCount = rowCount,
                Suppliers = companies
            };
        }

        public Company GetStoreById(long companyId)
        {
            return db.Companies.FirstOrDefault(c => c.CompanyId == companyId);
        }

    }
}
