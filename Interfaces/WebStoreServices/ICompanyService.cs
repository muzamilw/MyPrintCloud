using System.Collections.Generic;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using System;
using MPC.Models.Common;
using MPC.Webstore.Common;

namespace MPC.Interfaces.WebStoreServices
{
    /// <summary>
    /// My Organization Service Interface
    /// </summary>
    public interface ICompanyService
    {
        void AddlistingImages(long ListingId, List<ListingImage> Images);
        bool DeleteLisitngData(long ListingId);
        long AddNewListing(MPC.Models.DomainModels.Listing propertyListing);
        void DeleteBulletPoint(long BulletPointId, long ListingId);
        List<ListingBulletPoint> GetAllListingBulletPoints(long ListingID);
        void UpdateSingleBulletPoint(ListingBulletPoint BullentPoint);
        void AddSingleBulletPoint(ListingBulletPoint BullentPoint);
        void DeleteAjent(long ContactID);
        void DeleteListingImage(long listingImageID);
        void ListingImage(ListingImage NewImage);
        List<ListingImage> GetAllListingImages(long ListingID);
        void AddSingleAgent(CompanyContact NewAgent);
        void UpdateSignleAgent(CompanyContact Agent);

        void AddBulletPoint(List<ListingBulletPoint> model, long listingId);
        void UpdateBulletPoints(List<ListingBulletPoint> BulletPoints, long ListingId);
        void AddAgent(ListAgentMode model, long ContactCompanyId);
        void UpdateAgent(List<CompanyContact> model);
        long UpdateListing(MPC.Models.DomainModels.Listing propertyListing, MPC.Models.DomainModels.Listing tblListing);
        MPC.Models.DomainModels.Listing GetListingByListingID(int propertyId);
        List<CompanyContact> GetCorporateUserOnly(long companyId, long OrganisationId);
        List<CompanyContact> GetUsersByCompanyId(long CompanyId);
        void DeleteItems(List<Item> ItemList);
        long OrganisationThroughSystemUserEmail(string Email);
        void AddDataSystemUser(CompanyContact Contact);
        void UpdateDataSystemUser(CompanyContact Contact);
        CompanyContactRole GetRoleByID(int RoleID);
        List<ItemPriceMatrix> GetRetailProductsPriceMatrix(long CompanyID);
        List<ProductItem> GetAllRetailDisplayProductsQuickCalc(long CompanyID);
        List<CompanyContact> GetContactsByTerritory(long contactCompanyId, long territoryID);
        List<CompanyContact> GetSearched_Contacts(long contactCompanyId, String searchtxt, long territoryID);
        List<CompanyContactRole> GetContactRolesExceptAdmin(int AdminRole);
        List<CompanyContactRole> GetAllContactRoles();
        IEnumerable<RegistrationQuestion> GetAllQuestions();
        IEnumerable<CompanyTerritory> GetAllCompanyTerritories(long companyId);
        int GetSavedDesignCountByContactId(long ContactID);
        CompanyContact GetOrCreateContact(Company company, string ContactEmail, string ContactFirstName, string ContactLastName, string CompanyWebAccessCode);
        long ApproveOrRejectOrder(long orderID, long loggedInContactID, OrderStatus orderStatus, Guid OrdermangerID, string BrokerPO = "");
        List<Order> GetPendingApprovelOrdersList(long contactUserID, bool isApprover);
        CompanyContact GetContactByEmailID(string Email);
        Country GetCountryByCountryID(long CountryID);
        void ResetDefaultShippingAddress(Address address);
        List<State> GetAllStates();
        List<State> GetCountryStates(long CountryId);
        bool AddAddBillingShippingAdd(Address Address);
        bool AddressNameExist(Address address);
        bool UpdateBillingShippingAdd(Address Model);
        List<Address> GetsearchedAddress(long CompanyId, String searchtxt);
        MyCompanyDomainBaseReponse GetStoreFromCache(long companyId);
        long GetStoreIdFromDomain(string domain);
        List<ProductCategory> GetCompanyParentCategoriesById(long companyId, long OrganisationId);
        CompanyResponse GetAllCompaniesOfOrganisation(CompanyRequestModel request);
        CompanyContact GetUserByEmailAndPassword(string email, string password);

        CompanyContact GetContactByFirstName(string FName, long StoreId, long OrganisationId, int WebStoreMode, string providerKey);
        CompanyContact GetContactById(int contactId);
        CompanyContact GetContactByEmail(string Email, long OID, long StoreId);

        long CreateContact(CompanyContact Contact, string Name, long OrganizationID, int CustomerType, string TwitterScreanName, long SaleAndOrderManagerID, long StoreID);


        CompanyContact CreateCorporateContact(long CustomerId, CompanyContact regContact, string TwitterScreenName, long OrganisationId);
        Company GetCompanyByCompanyID(Int64 companyID);
        Company GetStoreReceiptPage(long companyId);

        CompanyContact GetContactByID(long contactID);

        List<Address> GetAddressesByTerritoryID(long TerritoryID);

        string GetUiCulture(long organisationId);

        CompanyContact GetContactByEmailAndMode(string Email, int Type, long customerID);

        string GeneratePasswordHash(string plainText);

        void UpdateUserPassword(int userId, string pass);

        SystemUser GetSystemUserById(Guid SystemUserId);

        List<ProductCategory> GetAllParentCorporateCatalogByTerritory(int customerId, int ContactId);
        List<ProductCategory> GetAllParentCorporateCatalog(int customerId);

        List<ProductCategory> GetStoreParentCategories(long companyId, long OrganisationId);
        List<ProductCategory> GetAllCategories(long companyId, long OrganisationId);
        CompanyContact GetCorporateUserByEmailAndPassword(string email, string password, long companyId, long OrganisationId);

        ProductCategory GetCategoryById(long categoryId);

        List<ProductCategory> GetChildCategories(long categoryId, long CompanyId);

        List<ProductCategory> GetAllChildCorporateCatalogByTerritory(long customerId, long ContactId, long ParentCatId);

        string[] CreatePageMetaTags(string MetaTitle, string metaDesc, string metaKeyword,string StoreName, Address address = null);

        Address GetDefaultAddressByStoreID(Int64 StoreID);

        List<GetCategoryProduct> GetRetailOrCorpPublishedProducts(long ProductCategoryID);
        void GetStoreFromCache(long companyId, bool clearcache);

        ItemStockOption GetFirstStockOptByItemID(long ItemId, long CompanyId);

        List<ItemPriceMatrix> GetPriceMatrixByItemID(int ItemId);
        //string FormatDecimalValueToTwoDecimal(string valueToFormat);

        double CalculateVATOnPrice(double ActualPrice, double TaxValue);

        double CalculateDiscount(double price, double discountPrecentage);
        long CreateCustomer(string name, bool isEmailSubScription, bool isNewsLetterSubscription, CompanyTypes customerType, string RegWithTwitter, long OrganisationId,long StoreId, CompanyContact regContact = null);
        Organisation GetOrganisatonById(long OrganisationId);
        string GetContactMobile(long CID);

        CmsPage getPageByID(long PageID);
        bool canContactPlaceOrder(long contactID, out bool hasWebAccess);
        Address GetAddressByID(long AddressID);

        CompanyContact GetCorporateAdmin(long contactCompanyId);
        Company GetCustomer(int CompanyId);
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
        CompanyContact GetRetailUser(string email, string password, long OrganisationId, long StoreId);

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
        CompanyTerritory GetCcompanyByTerritoryID(Int64 ContactId);
        void UpdateCompanyOrderingPolicy(Company Instance);
        bool UpdateCompanyContactForRetail(CompanyContact Instance);
        bool  UpdateCompanyContactForCorporate(CompanyContact Instance);
        bool UpdateCompanyName(Company Instance);

        bool VerifyHashSha1(string plainText, string compareWithSalt);

        string GetPasswordByContactID(long ContactID);

        bool SaveResetPassword(long ContactID, string Password);
        List<Address> GetAddressesListByContactCompanyID(long contactCompanyId);
        List<CmsSkinPageWidget> GetStoreWidgets(long CompanyId);
        State GetStateByStateID(long StateID);
        List<Country> GetAllCountries();
        NewsLetterSubscriber GetSubscriber(string email, long CompanyId);
        int AddSubscriber(NewsLetterSubscriber subsriber);
        bool UpdateSubscriber(string subscriptionCode, SubscriberStatus status);
        RaveReview GetRaveReview();
        /// <summary>
        /// Check web access code exists
        /// </summary>
        /// <param name="subscriptionCode"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Company isValidWebAccessCode(string WebAccessCode, long OrganisationId);

        CompanyContact GetCorporateContactForAutoLogin(string emailAddress, long organistionId, long companyId);
        List<ProductCategory> GetAllRetailPublishedCat();
        List<ProductCategory> GetAllCategories();
        string GetCurrencyCodeById(long currencyId);
        List<CompanyContact> GetCompanyAdminByCompanyId(long CompanyId);
        CompanyContact GetCorporateContactByEmail(string Email, long OID, long StoreId);
        string OrderConfirmationPDF(long OrderId, long StoreId);
        bool IsVoucherUsedByCustomer(long contactId, long companyId, long DiscountVoucherId);
        double? GetOrderTotalById(long OrderId);
        void AddReedem(long contactId, long companyId, long DiscountVoucherId);
        MyCompanyDomainBaseReponse GetStoreCachedObject(long StoreId);
        RegistrationQuestion GetSecretQuestionByID(int QuestionID);
        bool ShowPricesOnStore(int storeModeFromCookie, bool PriceFlagOfStore, long loginContactId, bool PriceFlagFromCookie);
        string GetCurrencySymbolById(long currencyId);
        void AddScopeVariables(long ContactId, long StoreId);
        long GetOrganisationIdByRequestUrl(string Url);
        CompanyContact GetContactBySocialNameAndEmail(string FName, long StoreId, long OrganisationId, int WebStoreMode, string Email);
    }
}
