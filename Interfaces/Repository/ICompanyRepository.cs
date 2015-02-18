﻿using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using System.Collections.Generic;

namespace MPC.Interfaces.Repository
{
    public interface ICompanyRepository : IBaseRepository<Company, long>
    {
        CompanyResponse GetCompanyById(long companyId);

        long GetStoreIdFromDomain(string domain);
        CompanyResponse SearchCompanies(CompanyRequestModel request);

        /// <summary>
        /// Get Suppliers For Inventories
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        SupplierSearchResponseForInventory GetSuppliersForInventories(SupplierRequestModelForInventory request);

        /// <summary>
        /// Get Store By Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        Company GetStoreById(long companyId);

        long CreateCustomer(string CompanyName, bool isEmailSubscriber, bool isNewsLetterSubscriber, CompanyTypes customerType, string RegWithSocialMedia, long OrganisationId, CompanyContact contact = null);
        /// <summary>
        /// Get Company Price Flag id for Price Matrix in webstore
        /// </summary>
        int? GetPriceFlagIdByCompany(long CompanyId);

        /// <summary>
        /// Get All Suppliers For Current Organisation
        /// </summary>
        IEnumerable<Company> GetAllSuppliers();

        string SystemWeight(long OrganisationID);

        string SystemLength(long OrganisationID);
        bool UpdateCompanyName(Company Instance);
        Company GetStoreByStoreId(long companyId);

        ExportOrganisation ExportCompany(ExportOrganisation ObjExportOrg, long CompanyId);
        /// <summary>
        /// Get Company By Is Customer Type
        /// </summary>
        CompanySearchResponseForCalendar GetByIsCustomerType(CompanyRequestModelForCalendar request);

        /// <summary>
        /// Count of live stores
        /// </summary>
        int LiveStoresCountForDashboard();
        
    }
}
