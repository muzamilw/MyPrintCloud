using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;
using MPC.Models.Common;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using System.Security.Cryptography;

namespace MPC.Interfaces.Repository
{
    public interface ICompanyContactRepository : IBaseRepository<CompanyContact, long>
    {
        
        void AddDataSystemUser(CompanyContact Contact);
        void UpdateDataSystemUser(CompanyContact Contact);
        
        List<CompanyContact> GetContactsByTerritory(long contactCompanyId, long territoryID);
        List<CompanyContact> GetSearched_Contacts(long contactCompanyId, String searchtxt, long territoryID);
        bool ValidatEmail(string email);
        CompanyContact createContact(int CCompanyId, string E, string F, string L, string AccountNumber = "", int questionID = 0, string Answer = "", string Password = "");
        //CompanyContact GetOrCreateContact(Company company, string ContactEmail, string ContactFirstName, string ContactLastName, string CompanyWebAccessCode);
        CompanyContact GetContactUser(string email, string password);
        CompanyContact GetContactByFirstName(string Fname);
        CompanyContact GetContactByEmail(string Email, long OID, long StoreId);
        CompanyContact GetContactById(int contactId);
        long CreateContact(CompanyContact Contact, string Name, long OrganizationID, int CustomerType, string TwitterScreanName, long SaleAndOrderManagerID, long StoreID);

        ContactsResponseForOrder GetContactsForOrder(CompanyRequestModelForCalendar request);
        CompanyContact CreateCorporateContact(long CustomerId, CompanyContact regContact, string TwitterScreenName, long OrganisationId, bool isAutoRegister);
        CompanyContact GetContactByID(Int64 ContactID);

        Models.ResponseModels.CompanyContactResponse GetCompanyContacts(
            Models.RequestModels.CompanyContactRequestModel request);

        CompanyContact GetContactByEmailAndMode(string Email, int Type, long customerID);
        CompanyContactResponse GetCompanyContactsForCrm(CompanyContactRequestModel request);

        string GeneratePasswordHash(string plainText);

        void UpdateUserPassword(int userId, string pass);

        CompanyContact GetCorporateUser(string emailAddress, string contactPassword, long companyId, long OrganisationId);

        long GetContactIdByCustomrID(long customerID);
        string GetContactMobile(long CID);

        bool canContactPlaceOrder(long contactID, out bool hasWebAccess);
        /// <summary>
        /// Gets the count of users register against a company by its id
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        int GetContactCountByCompanyId(long CompanyId);

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

        CompanyContact GetCorporateAdmin(long contactCompanyId);




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
        bool updateQuikcTextInfo(long contactId, QuickText objQuickText);

        long GetContactIdByRole(long CompanyID, int Role);
        long GetContactAddressID(long cID);
        IEnumerable<CompanyContact> GetCompanyContactsByCompanyId(long companyId);
        CompanyTerritory GetCcompanyByTerritoryID(Int64 ContactId);
        bool UpdateCompanyContactForRetail(CompanyContact Instance);
        bool UpdateCompanyContactForCorporate(CompanyContact Instance);

        /// <summary>
        /// Get All By Company ID
        /// </summary>
        IEnumerable<CompanyContact> GetContactsByCompanyId(long companyId);

        //bool VerifyHashSha1(string plainText, string compareWithSalt);

        string GetPasswordByContactID(long ContactID);

        bool SaveResetPassword(long ContactID, string Password);
        bool CheckDuplicatesOfContactEmailInStore(string email, long companyId, long companyContactId);

        CompanyContact GetContactByEmailID(string Email);

        /// <summary>
        /// Get Company Contact By search string and Customer Type
        /// </summary>
        CompanyContactResponse GetContactsBySearchNameAndType(CompanyContactForCalendarRequestModel request);
        /// <summary>
        /// get corporate user for auto login process
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="organistionId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        CompanyContact GetCorporateContactForAutoLogin(string emailAddress, long organistionId, long companyId);

        CompanyContact GetContactByContactId(long ContactId);
        List<CompanyContact> GetCompanyAdminByCompanyId(long CompanyId);
        CompanyContact GetCorporateContactByEmail(string Email, long OID, long StoreId);
        /// <summary>
        /// Load Property
        /// </summary>
        void LoadProperty<T>(object entity, Expression<Func<T>> propertyExpression, bool isCollection = false);

        CompanyContactResponse GetRetailContacts();
    }
}
