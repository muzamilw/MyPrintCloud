using System.Collections.Generic;
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
        List<ProductCategory> GetCompanyParentCategoriesById(long companyId, long OrganisationId);
        CompanyResponse GetAllCompaniesOfOrganisation(CompanyRequestModel request);
        CompanyContact GetUserByEmailAndPassword(string email, string password);

        CompanyContact GetContactByFirstName(string FName);

        CompanyContact GetContactByEmail(string Email);

        long CreateContact(CompanyContact Contact, string Name, long OrganizationID, int CustomerType, string TwitterScreanName, long SaleAndOrderManagerID, long StoreID);


        CompanyContact CreateCorporateContact(int CustomerId, CompanyContact regContact, string TwitterScreenName);
        Company GetCompanyByCompanyID(Int64 companyID);

        CompanyContact GetContactByID(long contactID);

        List<Address> GetAddressesByTerritoryID(long TerritoryID);

        string GetUiCulture(long organisationId);

        CompanyContact GetContactByEmailAndMode(string Email, int Type, int customerID);

        string GeneratePasswordHash(string plainText);

        void UpdateUserPassword(int userId, string pass);

        SystemUser GetSystemUserById(long SystemUserId);

        List<ProductCategory> GetAllParentCorporateCatalogByTerritory(int customerId, int ContactId);
        List<ProductCategory> GetAllParentCorporateCatalog(int customerId);

        List<ProductCategory> GetStoreParentCategories(long companyId, long OrganisationId);
        List<ProductCategory> GetAllCategories(long companyId);
        CompanyContact GetCorporateUserByEmailAndPassword(string email, string password, long companyId);

        ProductCategory GetCategoryById(int categoryId);

        List<ProductCategory> GetChildCategories(int categoryId);

        List<ProductCategory> GetAllChildCorporateCatalogByTerritory(int customerId, int ContactId, int ParentCatId);

        string[] CreatePageMetaTags(string MetaTitle, string metaDesc, string metaKeyword, StoreMode mode,string StoreName, Address address = null);

        Address GetDefaultAddressByStoreID(Int64 StoreID);

        List<GetCategoryProduct> GetRetailOrCorpPublishedProducts(int ProductCategoryID);
        void GetStoreFromCache(long companyId, bool clearcache);

        ItemStockOption GetFirstStockOptByItemID(int ItemId, int CompanyId);

        List<ItemPriceMatrix> GetPriceMatrixByItemID(int ItemId);
        string FormatDecimalValueToTwoDecimal(string valueToFormat);

        double CalculateVATOnPrice(double ActualPrice, double TaxValue);

        double CalculateDiscount(double price, double discountPrecentage);
        long CreateCustomer(string name, bool isEmailSubScription, bool isNewsLetterSubscription, CompanyTypes customerType, string RegWithTwitter, long OrganisationId, CompanyContact regContact = null);
        Organisation getOrganisatonByID(int OID);
        string GetContactMobile(long CID);

        CmsPage getPageByID(long PageID);
        bool canContactPlaceOrder(long contactID, out bool hasWebAccess);
        Address GetAddressByID(long AddressID);

        CompanyContact GetCorporateAdmin(long contactCompanyId);

        List<Address> GetAddressByCompanyID(long companyID);

        CompanyTerritory GetTerritoryById(long territoryId);

        List<Address> GetAdressesByContactID(long contactID);

        List<Address> GetBillingAndShippingAddresses(long TerritoryID);

        List<Address> GetContactCompanyAddressesList(long customerID);


        State GetStateFromStateID(long StateID);

        
          /// <summary>
        /// gets the name of the country by its id
        /// </summary>
        /// <param name="CountryId"></param>
        /// <returns></returns>
        string GetCountryNameById(long CountryId);
         /// <summary>
        /// gets the name of the state by its id
        /// </summary>
        /// <param name="CountryId"></param>
        /// <returns></returns>
        string GetStateNameById(long StateId);
        /// <summary>
        /// Gets the count of users register against a company by its id
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        int GetContactCountByCompanyId(long CompanyId);
        

        /// <summary>
        /// Gets favorite design count Of a login user to display on dashboard
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        int GetFavDesignCountByContactId(long contactId);
         /// <summary>
        /// Gets the contact orders count by Status
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        int GetOrdersCountByStatus(long contactId, OrderStatus statusId);
           /// <summary>
        /// Gets pending approval orders count
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="isApprover"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        int GetPendingOrdersCountByTerritory(long companyId, OrderStatus statusId, int TerritoryID);

        /// <summary>
        /// Gets all pending approval orders count for corporate customers
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="isApprover"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        int GetAllPendingOrders(long CompanyId, OrderStatus statusId);
        /// <summary>
        /// Get all orders count placed against a company
        /// </summary>
        /// <param name="CCID"></param>
        /// <returns></returns>
        int GetAllOrdersCount(long CompanyId);
        /// <summary>
        /// Gets login user orders count which are placed and not archieved
        /// </summary>
        /// <param name="CID"></param>
        /// <param name="CCID"></param>
        /// <returns></returns>
        int AllOrders(long contactID, long CompanyID);
        /// <summary>
        /// Get retail user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        CompanyContact GetRetailUser(string email, string password);

        long GetContactTerritoryID(long CID);

        long GetContactAddressID(long cID);

        string GetStateCodeById(long stateId);

        string GetCountryCodeById(long countryId);
        
         List<Address> GetContactCompanyAddressesList(long BillingAddressId, long ShippingAddressid, long PickUpAddressId);
        /// <summary>
        /// get the contactid 
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        long GetContactIdByCompanyId(long CompanyId);

        long GetContactIdByRole(long CompanyID, int Role);
        string SystemWeight(long OrganisationID);

        string SystemLength(long OrganisationID);
    }
}
