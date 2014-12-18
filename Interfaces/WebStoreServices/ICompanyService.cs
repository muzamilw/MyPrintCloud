﻿using System.Collections.Generic;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using System;
using MPC.Models.Common;

namespace MPC.Interfaces.WebStoreServices
{
    /// <summary>
    /// My Organization Service Interface
    /// </summary>
    public interface ICompanyService
    {
        MyCompanyDomainBaseReponse GetStoreFromCache(long companyId);
        long GetStoreIdFromDomain(string domain);
        List<ProductCategory> GetCompanyParentCategoriesById(long companyId);
        CompanyResponse GetAllCompaniesOfOrganisation(CompanyRequestModel request);
        CompanyContact GetContactUser(string email, string password);

        CompanyContact GetContactByFirstName(string FName);

        CompanyContact GetContactByEmail(string Email);

        Int64 CreateContact(CompanyContact Contact, string Name, int OrganizationID, int CustomerType, string TwitterScreanName);


        CompanyContact CreateCorporateContact(int CustomerId, CompanyContact regContact, string TwitterScreenName);
        Company GetCompanyByCompanyID(Int64 companyID);

        CompanyContact GetContactByID(Int64 contactID);

        List<Address> GetAddressesByTerritoryID(Int64 TerritoryID);

        string GetUiCulture(long organisationId);

        CompanyContact GetContactByEmailAndMode(string Email, int Type, int customerID);

        string GeneratePasswordHash(string plainText);

        void UpdateUserPassword(int userId, string pass);

        SystemUser GetSystemUserById(long SystemUserId);

        List<ProductCategory> GetAllParentCorporateCatalogByTerritory(int customerId, int ContactId);
        List<ProductCategory> GetAllParentCorporateCatalog(int customerId);

        List<ProductCategory> GetParentCategories();

        ProductCategory GetCategoryById(int categoryId);

        List<ProductCategory> GetChildCategories(int categoryId);

        List<ProductCategory> GetAllChildCorporateCatalogByTerritory(int customerId, int ContactId, int ParentCatId);

        string[] CreatePageMetaTags(string MetaTitle, string metaDesc, string metaKeyword, StoreMode mode,string StoreName, Address address = null);

        Address GetDefaultAddressByStoreID(Int64 StoreID);

        List<GetProductsListView> GetRetailOrCorpPublishedProducts(int ProductCategoryID);

        ItemStockOption GetFirstStockOptByItemID(int ItemId, int CompanyId);

        List<ItemPriceMatrix> GetPriceMatrixByItemID(int ItemId);
        string FormatDecimalValueToTwoDecimal(string valueToFormat);

        double CalculateVATOnPrice(double ActualPrice, double TaxValue);

        double CalculateDiscount(double price, double discountPrecentage);
    }
}
