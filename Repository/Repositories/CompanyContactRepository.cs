
using Microsoft.Practices.Unity;
using MPC.Common;
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
    public class CompanyContactRepository : BaseRepository<CompanyContact>, ICompanyContactRepository
    {

        #region Private
        private readonly Dictionary<ContactByColumn, Func<CompanyContact, object>> contactOrderByClause = new Dictionary<ContactByColumn, Func<CompanyContact, object>>
                    {
                        {ContactByColumn.Code, d => d.Company.Name}
                    };
        #endregion

        public CompanyContactRepository(IUnityContainer container)
            : base(container)
        {
        }
        protected override IDbSet<CompanyContact> DbSet
        {
            get
            {

                return db.CompanyContacts;
            }
        }

        /// <summary>
        /// Get All 
        /// </summary>
        public override IEnumerable<CompanyContact> GetAll()
        {
            return DbSet.Where(cc => cc.OrganisationId == OrganisationId).ToList();
        }

        /// <summary>
        /// Get All By Company ID
        /// </summary>
        public IEnumerable<CompanyContact> GetContactsByCompanyId(long companyId)
        {
            return DbSet.Where(cc => cc.CompanyId == companyId).ToList();
        }

        /// <summary>
        /// Get Contacts for order screen
        /// </summary>
        public ContactsResponseForOrder GetContactsForOrder(CompanyRequestModelForCalendar request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            var query = (from contact in db.CompanyContacts
                from cmp in db.Companies.Where(c => c.CompanyId == contact.Company.StoreId).DefaultIfEmpty()
                where (string.IsNullOrEmpty(request.SearchString)
                       ||
                       (contact.FirstName.Contains(request.SearchString)) ||
                       (contact.MiddleName.Contains(request.SearchString)) ||
                       (contact.LastName.Contains(request.SearchString)) ||
                       (contact.Email.Contains(request.SearchString)) ||
                       cmp.Name.Contains(request.SearchString) ||
                       contact.Company.Name.Contains(request.SearchString)) &&
                      (request.CustomerTypes.Any(obj => contact.Company.IsCustomer == obj)) &&
                      (!contact.isArchived.HasValue || contact.isArchived.Value == false) &&
                      contact.OrganisationId == OrganisationId && contact.Company.TypeId != (int)CompanyTypes.TemporaryCustomer 
            

                select new 
                {
                    contact.FirstName,
                    contact.LastName,
                    contact.ContactId,
                    contact.AddressId,
                    contact.CompanyId,
                    Company = new
                    {
                        contact.CompanyId, 
                        contact.Company.Name,
                        contact.Company.StoreId,
                        StoreName = cmp != null ? cmp.Name : string.Empty,
                        contact.Company.IsCustomer
                    }
                });
            var que = query.Distinct().OrderBy(contact => contact.FirstName).Skip(fromRow).Take(toRow).ToList();
            int rowCount = query.Distinct().Count();
            return new ContactsResponseForOrder
            {
                RowCount = rowCount,
                CompanyContacts = que.Select(contact => new CompanyContact
                {
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    ContactId = contact.ContactId,
                    AddressId = contact.AddressId,
                    CompanyId = contact.CompanyId,
                    Company = new Company
                    {
                        CompanyId = contact.Company.CompanyId,
                        Name = contact.Company.Name,
                        StoreId = contact.Company.StoreId,
                        StoreName = contact.Company.StoreName,
                        IsCustomer = contact.Company.IsCustomer
                    }
                }).ToList()
            };
        }
        /// <summary>
        /// Get Company Contact By search string and Customer Type
        /// </summary>
        public CompanyContactResponse GetContactsBySearchNameAndType(CompanyContactForCalendarRequestModel request)
        {

            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            bool isStringSpecified = !string.IsNullOrEmpty(request.SearchFilter);
            Expression<Func<CompanyContact, bool>> query =
                s =>
                    (!isStringSpecified || (s.FirstName.Contains(request.SearchFilter) || (s.LastName.Contains(request.SearchFilter)))) &&
                    s.OrganisationId == OrganisationId && s.Company.IsCustomer == request.CustomerType;

            int rowCount = DbSet.Count(query);
            IEnumerable<CompanyContact> companies =
                DbSet.Where(query)
                    .OrderBy(x => x.FirstName)
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();

            return new CompanyContactResponse
            {
                RowCount = rowCount,
                CompanyContacts = companies
            };
        }
        ///// <summary>
        ///// Get Compnay Contacts
        ///// </summary>
        //public CompanyContactResponse GetCompanyContacts(CompanyContactRequestModel request)
        //{
        //    int fromRow = (request.PageNo - 1) * request.PageSize;
        //    int toRow = request.PageSize;
        //    bool isStringSpecified = !string.IsNullOrEmpty(request.SearchString);
        //    Expression<Func<CompanyContact, bool>> query =
        //        s =>
        //            (isStringSpecified && (s.Company.Name.Contains(request.SearchString)) &&
        //            s.OrganisationId == OrganisationId && s.isArchived != true);

        //    int rowCount = DbSet.Count(query);
        //    IEnumerable<CompanyContact> companies = request.IsAsc
        //        ? DbSet.Where(query)
        //            .OrderBy(contactOrderByClause[request.ContactByColumn])
        //            .Skip(fromRow)
        //            .Take(toRow)
        //            .ToList()
        //        : DbSet.Where(query)
        //            .OrderByDescending(contactOrderByClause[request.ContactByColumn])
        //            .Skip(fromRow)
        //            .Take(toRow)
        //            .ToList();
        //    return new CompanyContactResponse
        //    {
        //        RowCount = rowCount,
        //        CompanyContacts = companies
        //    };
        //}


        public CompanyContact GetContactUser(string email, string password)
        {
            try
            {
                var qury = from contacts in db.CompanyContacts
                           join contactCompany in db.Companies on contacts.CompanyId equals contactCompany.CompanyId
                           where string.Compare(contacts.Email, email, true) == 0
                           select contacts;

                return qury.ToList().Where(contct => HashingManager.VerifyHashSha1(password, contct.Password) == true).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public CompanyContact GetContactByFirstName(string FName)
        {
            try
            {
                var qry = from contacts in db.CompanyContacts
                          join contactCompany in db.Companies on contacts.CompanyId equals contactCompany.CompanyId
                          where string.Compare(contacts.twitterScreenName, FName, true) == 0
                          select contacts;

                return qry.ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public CompanyContact GetContactById(int contactId)
        {
            return (from c in db.CompanyContacts.Include("CompanyTerritory")
                    where c.ContactId == contactId
                    select c).FirstOrDefault();

        }
        public CompanyContact GetContactByEmail(string Email, long OID, long StoreId)
        {
            try
            {
                var qry = from contacts in db.CompanyContacts
                          join contactCompany in db.Companies on contacts.CompanyId equals contactCompany.CompanyId
                          where string.Compare(contacts.Email, Email, true) == 0 && contacts.OrganisationId == OID
                          && contactCompany.StoreId == StoreId
                          select contacts;

                return qry.ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public string GetContactMobile(long CID)
        {
            try
            {
                return db.CompanyContacts.Where(c => c.ContactId == CID).Select(s => s.Mobile).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public string GeneratePasswordHash(string plainText)
        {
            try
            {
                return HashingManager.ComputeHashSHA1(plainText);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //public bool VerifyHashSha1(string plainText, string compareWithSalt)
        //{
        //    bool result = false;

        //    try
        //    {
        //        result = VerifyHash(plainText, "SHA1", compareWithSalt);

        //    }
        //    catch (CryptographicException)
        //    {
        //        result = false;
        //    }
        //    catch (Exception)
        //    {
        //        result = false;
        //    }

        //    return result;
        //}

        //private static bool VerifyHash(string plainText,
        //                        string hashAlgorithm,
        //                        string hashValue)
        //{
        //    try
        //    {
        //        // Convert base64-encoded hash value into a byte array.
        //        byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

        //        // We must know size of hash (without salt).
        //        int hashSizeInBits, hashSizeInBytes;

        //        //// Make sure that hashing algorithm name is specified.
        //        //if (hashAlgorithm == null)
        //        //    hashAlgorithm = "";
        //        hashSizeInBits = InitializeHashSize(hashAlgorithm);

        //        // Convert size of hash from bits to bytes.
        //        hashSizeInBytes = hashSizeInBits / 8;

        //        // Make sure that the specified hash value is long enough.
        //        if (hashWithSaltBytes.Length < hashSizeInBytes)
        //            return false;

        //        // Allocate array to hold original salt bytes retrieved from hash.
        //        byte[] saltBytes = new byte[hashWithSaltBytes.Length -
        //                                    hashSizeInBytes];

        //        // Copy salt from the end of the hash to the new array.
        //        for (int i = 0; i < saltBytes.Length; i++)
        //            saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

        //        // Compute a new hash string.
        //        string expectedHashString =
        //                    ComputeHash(plainText, hashAlgorithm, saltBytes);

        //        // If the computed hash matches the specified hash,
        //        // the plain text value must be correct.
        //        return (hashValue == expectedHashString);
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        //private static int InitializeHashSize(string hashAlgorithm)
        //{
        //    try
        //    {
        //        int hashSizeInBits = 0;
        //        // Size of hash is based on the specified algorithm.
        //        switch (hashAlgorithm)
        //        {
        //            case "SHA1":
        //                hashSizeInBits = 160;
        //                break;

        //            case "SHA256":
        //                hashSizeInBits = 256;
        //                break;

        //            case "SHA384":
        //                hashSizeInBits = 384;
        //                break;

        //            case "SHA512":
        //                hashSizeInBits = 512;
        //                break;

        //            default: // Must be MD5
        //                hashSizeInBits = 128;
        //                break;
        //        }
        //        return hashSizeInBits;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        //private static string ComputeHash(string plainText,
        //                            string hashAlgorithm,
        //                            byte[] saltBytes)
        //{
        //    try
        //    {
        //        // If salt is not specified, generate it on the fly.
        //        if (saltBytes == null)
        //        {
        //            // Define min and max salt sizes.
        //            int minSaltSize = 4;
        //            int maxSaltSize = 8;

        //            // Generate a random number for the size of the salt.
        //            Random random = new Random();
        //            int saltSize = random.Next(minSaltSize, maxSaltSize);

        //            // Allocate a byte array, which will hold the salt.
        //            saltBytes = new byte[saltSize];

        //            // Initialize a random number generator.
        //            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

        //            // Fill the salt with cryptographically strong byte values.
        //            rng.GetNonZeroBytes(saltBytes);
        //        }

        //        // Convert plain text into a byte array.
        //        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

        //        // Allocate array, which will hold plain text and salt.
        //        byte[] plainTextWithSaltBytes =
        //                new byte[plainTextBytes.Length + saltBytes.Length];

        //        // Copy plain text bytes into resulting array.
        //        for (int i = 0; i < plainTextBytes.Length; i++)
        //            plainTextWithSaltBytes[i] = plainTextBytes[i];

        //        // Append salt bytes to the resulting array.
        //        for (int i = 0; i < saltBytes.Length; i++)
        //            plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

        //        // Because we support multiple hashing algorithms, we must define
        //        // hash object as a common (abstract) base class. We will specify the
        //        // actual hashing algorithm class later during object creation.
        //        HashAlgorithm hash;



        //        // Make sure hashing algorithm name is specified.
        //        //if (hashAlgorithm == null)
        //        //    hashAlgorithm = "";
        //        hash = CreateHashAlgoFactory(hashAlgorithm);

        //        // Compute hash value of our plain text with appended salt.
        //        byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

        //        // Create array which will hold hash and original salt bytes.
        //        byte[] hashWithSaltBytes = new byte[hashBytes.Length +
        //                                            saltBytes.Length];

        //        // Copy hash bytes into resulting array.
        //        for (int i = 0; i < hashBytes.Length; i++)
        //            hashWithSaltBytes[i] = hashBytes[i];

        //        // Append salt bytes to the result.
        //        for (int i = 0; i < saltBytes.Length; i++)
        //            hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

        //        // Convert result into a base64-encoded string.
        //        string hashValue = Convert.ToBase64String(hashWithSaltBytes);

        //        // Return the result.
        //        return hashValue;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        //private static HashAlgorithm CreateHashAlgoFactory(string hashAlgorithm)
        //{
        //    try
        //    {
        //        HashAlgorithm hash = null; ;
        //        // Initialize appropriate hashing algorithm class.
        //        switch (hashAlgorithm)
        //        {
        //            case "SHA1":
        //                hash = new SHA1Managed();
        //                break;

        //            case "SHA256":
        //                hash = new SHA256Managed();
        //                break;

        //            case "SHA384":
        //                hash = new SHA384Managed();
        //                break;

        //            case "SHA512":
        //                hash = new SHA512Managed();
        //                break;

        //            default:
        //                hash = new MD5CryptoServiceProvider(); // mdf default
        //                break;
        //        }
        //        return hash;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        //private static string ComputeHashSHA1(string plainText)
        //{
        //    try
        //    {
        //        string salt = string.Empty;

        //        try
        //        {
        //            salt = ComputeHash(plainText, "SHA1", null);

        //        }
        //        catch (CryptographicException ex)
        //        {
        //            throw ex;
        //        }


        //        return salt;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        public long CreateContact(CompanyContact Contact, string Name, long OrganizationID, int CustomerType, string TwitterScreanName, long SaleAndOrderManagerID, long StoreID)
        {
            try
            {
                Address address = null;
                CompanyContact tblContacts = null;

                long customerID = 0;

                //CompanySiteManager companySiteManager = new CompanySiteManager();
                Company Company = new Company();
                Organisation organization = null;// CompanySiteManager.GetCompanySite();

                Company.isArchived = false;
                Company.AccountNumber = "123";
                Company.AccountOpenDate = DateTime.Now;
                Company.Name = Name;
                Company.TypeId = CustomerType; //contactCompanyType.TypeID;
                Company.Status = 0;

                if (Contact != null && !string.IsNullOrEmpty(Contact.Mobile))
                    Company.PhoneNo = Contact.Mobile;


                Company.CreationDate = DateTime.Now;
                Company.AccountManagerId = Company.AccountManagerId;
                Company.CreditLimit = 0;
                Company.IsCustomer = Convert.ToInt16(CustomerType);

                //Company.SalesAndOrderManagerId1 = SaleAndOrderManagerID;
                Company.StoreId = StoreID;
                Company.OrganisationId = OrganizationID;
                //if (BrokerContactCompanyID != null)
                //{
                //    contactCompany.BrokerContactCompanyID = BrokerContactCompanyID;
                //    contactCompany.IsCustomer = (int)CustomerTypes.Customers;
                //}
                //else 
                //{
                //    contactCompany.IsCustomer = 0; //prospect
                //}


                Markup zeroMarkup = GetZeroMarkup();
                if (zeroMarkup != null)
                {
                    Company.DefaultMarkUpId = Convert.ToInt16(zeroMarkup.MarkUpId);
                }
                else
                {
                    Company.DefaultMarkUpId = 1;
                }

                //Create Customer
                db.Companies.Add(Company);

                //Create Billing Address and Delivery Address and mark them default billing and shipping
                address = PopulateAddressObject(0, (Int16)Company.CompanyId, true, true);
                db.Addesses.Add(address);

                //Create Contact
                tblContacts = PopulateContactsObject((Int16)Company.CompanyId, (Int16)address.AddressId, true);
                tblContacts.isArchived = false;

                if (Contact != null)
                {
                    tblContacts.FirstName = Contact.FirstName;
                    tblContacts.LastName = Contact.LastName;
                    tblContacts.Email = Contact.Email;
                    tblContacts.Mobile = Contact.Mobile;
                    tblContacts.Password = HashingManager.ComputeHashSHA1(Contact.Password);
                    tblContacts.QuestionId = 1;
                    tblContacts.SecretAnswer = "";
                    tblContacts.ClaimIdentifer = Contact.ClaimIdentifer;
                    tblContacts.AuthentifiedBy = Contact.AuthentifiedBy;
                    //Quick Text Fields
                    tblContacts.quickAddress1 = Contact.quickAddress1;
                    tblContacts.quickAddress2 = Contact.quickAddress2;
                    tblContacts.quickAddress3 = Contact.quickAddress3;
                    tblContacts.quickCompanyName = Contact.quickCompanyName;
                    tblContacts.quickCompMessage = Contact.quickCompMessage;
                    tblContacts.quickEmail = Contact.quickEmail;
                    tblContacts.quickFax = Contact.quickFax;
                    tblContacts.quickFullName = Contact.quickFullName;
                    tblContacts.quickPhone = Contact.quickPhone;
                    tblContacts.quickTitle = Contact.quickTitle;
                    tblContacts.quickWebsite = Contact.quickWebsite;
                    if (!string.IsNullOrEmpty(TwitterScreanName))
                    {
                        tblContacts.twitterScreenName = TwitterScreanName;
                    }


                }
                db.CompanyContacts.Add(tblContacts);


                if (db.SaveChanges() > 0)
                {
                    customerID = Company.CompanyId; // customer id
                    if (Contact != null)
                    {
                        Contact.ContactId = tblContacts.ContactId;
                        Contact.CompanyId = customerID;
                    }
                }

                return customerID;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Markup GetZeroMarkup()
        {
            try
            {
                return db.Markups.Where(c => c.MarkUpRate.Value == 0).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private Address PopulateAddressObject(int dummyAddreID, int customerID, bool isDefaulAddress, bool isDefaultShippingAddress)
        {
            try
            {
                Address tblAddres = new Address();

                tblAddres.AddressId = dummyAddreID;
                tblAddres.CompanyId = customerID;
                tblAddres.AddressName = "Address Name";
                tblAddres.IsDefaultAddress = isDefaulAddress;
                tblAddres.IsDefaultShippingAddress = isDefaultShippingAddress;
                tblAddres.Address1 = "Address 1";
                tblAddres.City = "City";
                tblAddres.isArchived = false;

                return tblAddres;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private CompanyContact PopulateContactsObject(int customerID, int addressID, bool isDefaultContact)
        {
            try
            {
                CompanyContact tblContacts = new CompanyContact();
                tblContacts.CompanyId = customerID;
                tblContacts.AddressId = addressID;
                tblContacts.FirstName = string.Empty;
                tblContacts.IsDefaultContact = isDefaultContact == true ? 1 : 0;

                return tblContacts;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public CompanyContact GetContactByID(Int64 ContactID)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.CompanyContacts.Where(c => c.ContactId == ContactID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public CompanyTerritory GetCcompanyByTerritoryID(Int64 CompanyId)
        {
            try
            {
                var Query = (from Comp in db.Companies join Tero in db.CompanyTerritories on Comp.CompanyId equals Tero.CompanyId where Tero.CompanyId == CompanyId select Tero).FirstOrDefault();
                return Query;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public CompanyContact CreateCorporateContact(long CustomerId, CompanyContact regContact, string TwitterScreenName, long OrganisationId, bool isAutoRegister)
        {
            try
            {
                if (regContact != null)
                {

                    // int defaultAddressID = addmgr.GetCompanyDefaultAddressID(CustomerId);
                    CompanyTerritory companyTerritory = db.CompanyTerritories.Where(t => t.isDefault == true && t.CompanyId == CustomerId).FirstOrDefault();
                    CompanyContact Contact = new CompanyContact(); // ContactManager.PopulateContactsObject(CustomerId, defaultAddressID, false);

                    Contact.CompanyId = CustomerId;
                    //  Contact.AddressID = defaultAddressID;
                    Contact.FirstName = string.Empty;
                    Contact.IsDefaultContact = 0;
                    Contact.FirstName = regContact.FirstName;
                    Contact.LastName = regContact.LastName;
                    Contact.Email = regContact.Email;
                    Contact.Mobile = regContact.Mobile;
                    Contact.Password = HashingManager.ComputeHashSHA1(regContact.Password);
                    Contact.QuestionId = 1;
                    Contact.SecretAnswer = "";
                    Contact.ClaimIdentifer = regContact.ClaimIdentifer;
                    Contact.AuthentifiedBy = regContact.AuthentifiedBy;
                    Contact.isArchived = false;
                    Contact.twitterScreenName = TwitterScreenName;
                    if (isAutoRegister == true)
                    {
                        Contact.isWebAccess = true;
                    }
                    else 
                    {
                        Contact.isWebAccess = false;
                    }
                   
                    Contact.ContactRoleId = Convert.ToInt32(Roles.User);
                    Contact.OrganisationId = OrganisationId;
                    Contact.isPlaceOrder = true;

                    //Quick Text Fields
                    Contact.quickAddress1 = regContact.quickAddress1;
                    Contact.quickAddress2 = regContact.quickAddress2;
                    Contact.quickAddress3 = regContact.quickAddress3;
                    Contact.quickCompanyName = regContact.quickCompanyName;
                    Contact.quickCompMessage = regContact.quickCompMessage;
                    Contact.quickEmail = regContact.quickEmail;
                    Contact.quickFax = regContact.quickFax;
                    Contact.quickFullName = regContact.quickFullName;
                    Contact.quickPhone = regContact.quickPhone;
                    Contact.quickTitle = regContact.quickTitle;
                    Contact.quickWebsite = regContact.quickWebsite;
                    Contact.IsPricingshown = true;
                    if (companyTerritory != null)
                    {
                        List<Address> addresses = GetAddressesByTerritoryID(companyTerritory.TerritoryId);
                        if (addresses != null && addresses.Count > 0)
                        {
                            foreach (var address in addresses)
                            {

                                if (address.isDefaultTerrorityBilling == true && address.isDefaultTerrorityShipping == true)
                                {
                                    Contact.AddressId = (int)address.AddressId;
                                    Contact.ShippingAddressId = (int)address.AddressId;
                                }
                                else
                                {

                                    if (address.isDefaultTerrorityBilling == true)
                                        Contact.AddressId = (int)address.AddressId;
                                    if (address.isDefaultTerrorityShipping == true)
                                        Contact.ShippingAddressId = (int)address.AddressId;
                                }
                            }
                        }
                        Contact.TerritoryId = companyTerritory.TerritoryId;
                        //Contact.ShippingAddressID = companyTerritory.ShippingAddressID;
                        //if (companyTerritory.BillingAddressID != null)
                        //     Contact.AddressID = (int)companyTerritory.BillingAddressI
                    }
                    else
                    {
                        Contact.TerritoryId = 0;
                        Contact.ShippingAddressId = 0;
                    }

                    db.CompanyContacts.Add(Contact);

                    if (db.SaveChanges() > 0)
                    {
                        return Contact;
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public List<Address> GetAddressesByTerritoryID(Int64 TerritoryID)
        {
            try
            {
                return db.Addesses.Where(a => a.TerritoryId == TerritoryID && (a.isArchived == null || a.isArchived.Value == false) && (a.isPrivate == false || a.isPrivate == null)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public Models.ResponseModels.CompanyContactResponse GetCompanyContacts(
            Models.RequestModels.CompanyContactRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            bool isSearchFilterSpecified = !string.IsNullOrEmpty(request.SearchFilter);
            bool isTerritoryFilterSpecified = request.TerritoryId != 0;

            Expression<Func<CompanyContact, bool>> query =
                s =>
                    (isSearchFilterSpecified && (s.Email.Contains(request.SearchFilter)) ||
                     (s.quickCompanyName.Contains(request.SearchFilter)) ||
                     !isSearchFilterSpecified) && s.CompanyId == request.CompanyId && s.isArchived != true && ((isTerritoryFilterSpecified && s.TerritoryId == request.TerritoryId) || !isTerritoryFilterSpecified);//&& s.OrganisationId == OrganisationId

            int rowCount = DbSet.Count(query);
            // ReSharper disable once ConditionalTernaryEqualBranch
            IEnumerable<CompanyContact> companyContacts = request.IsAsc
                ? DbSet.Where(query)
                    .OrderByDescending(x => x.CompanyId)
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet.Where(query)
                    .OrderByDescending(x => x.CompanyId)
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();
            return new CompanyContactResponse
            {
                RowCount = rowCount,
                CompanyContacts = companyContacts
            };
        }
        public CompanyContactResponse GetCompanyContactsForCrm(CompanyContactRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;

            Expression<Func<CompanyContact, bool>> query =
                contact =>
                    (string.IsNullOrEmpty(request.SearchFilter) ||
                    (contact.FirstName.Contains(request.SearchFilter)) ||
                    (contact.MiddleName.Contains(request.SearchFilter)) ||
                    (contact.LastName.Contains(request.SearchFilter)) ||
                    (contact.Email.Contains(request.SearchFilter))||
                    contact.Company.Name.Contains(request.SearchFilter)) &&
                    (contact.Company.IsCustomer == 0 || contact.Company.IsCustomer == 1) &&
                    (contact.isArchived == false || contact.isArchived == null) && contact.OrganisationId==OrganisationId;

            int rowCount = DbSet.Count(query);
            IEnumerable<CompanyContact> companyContacts = request.IsAsc
                ? DbSet.Where(query)
                    .OrderByDescending(x => x.CompanyId)
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet.Where(query)
                    .OrderByDescending(x => x.CompanyId)
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();
            foreach (var comp in companyContacts)
            {
                comp.Company.StoreName = GetStoreNameByStoreId(comp.Company.StoreId ?? 0);
            }
            return new CompanyContactResponse
            {
                RowCount = rowCount,
                CompanyContacts = companyContacts
            };
        }

        public string GetStoreNameByStoreId(long StoreId)
        {
            try
            {
                return db.Companies.Where(c => c.CompanyId == StoreId).Select(c => c.Name).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CompanyContact GetContactByEmailAndMode(string Email, int Type, long customerID)
        {
            var query = (from c in db.CompanyContacts
                         join cc in db.Companies on c.CompanyId equals cc.CompanyId
                         where c.Email == Email && cc.IsCustomer == Type
                         select c).FirstOrDefault();
            return query;

        }


        public void UpdateUserPassword(int userId, string pass)
        {
            CompanyContact contacts = db.CompanyContacts.Where(c => c.ContactId == userId).FirstOrDefault();
            contacts.Password = pass;
            db.SaveChanges();

        }

        public CompanyContact GetCorporateUser(string emailAddress, string contactPassword, long companyId, long OrganisationId)
        {

            db.Configuration.LazyLoadingEnabled = false;
            var qury = from Contacts in db.CompanyContacts
                       join ContactCompany in db.Companies on Contacts.CompanyId equals ContactCompany.CompanyId
                       where string.Compare(Contacts.Email, emailAddress, true) == 0
                             && Contacts.CompanyId == companyId && (ContactCompany.IsCustomer == (int)CustomerTypes.Corporate)
                             && Contacts.OrganisationId == OrganisationId
                       select Contacts;

            return qury.ToList().Where(contct => HashingManager.VerifyHashSha1(contactPassword, contct.Password) == true).FirstOrDefault();

        }

        public long GetContactIdByCustomrID(long customerID)
        {

            CompanyContact contact = db.CompanyContacts.Where(i => i.CompanyId == customerID).FirstOrDefault();
            if (contact != null)
            {
                return contact.ContactId;
            }
            else
            {
                return 0;
            }


        }
        public bool canContactPlaceOrder(long contactID, out bool hasWebAccess)
        {
            try
            {
                CompanyContact contact = db.CompanyContacts.Where(c => c.ContactId == contactID).FirstOrDefault();
                if (contact != null)
                {
                    hasWebAccess = contact.isWebAccess ?? false;
                    return contact.isPlaceOrder ?? false;
                }
                else
                {
                    hasWebAccess = false;
                    return false;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CompanyContact GetCorporateAdmin(long contactCompanyId)
        {
            try
            {

                return db.CompanyContacts.Where(c => c.CompanyId == contactCompanyId && c.ContactRoleId.HasValue && c.ContactRoleId.Value == (int)ContactCompanyUserRoles.Administrator).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Gets the count of users register against a company by its id
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public int GetContactCountByCompanyId(long CompanyId)
        {
            try
            {
                return db.CompanyContacts.Where(c => c.CompanyId == CompanyId).Count();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Gets the contact orders count by Status
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public int GetOrdersCountByStatus(long contactId, OrderStatus statusId)
        {
            try
            {
                return (from estimate in db.Estimates
                        where estimate.ContactId == contactId && estimate.StatusId == (int)statusId
                            && estimate.isEstimate == false
                        select estimate).Count();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Gets pending approval orders count by territory for corporate customers
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="isApprover"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public int GetPendingOrdersCountByTerritory(long companyId, OrderStatus statusId, int TerritoryID)
        {
            try
            {
                return (from estimate in db.Estimates
                        join contact in db.CompanyContacts on estimate.ContactId equals contact.ContactId
                        where estimate.CompanyId == companyId && estimate.StatusId == (int)statusId && estimate.isEstimate == false
                        && contact.TerritoryId == TerritoryID
                        select estimate).Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Gets all pending approval orders count for corporate customers
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="isApprover"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public int GetAllPendingOrders(long CompanyId, OrderStatus statusId)
        {
            try
            {
                return (from estimate in db.Estimates
                        where estimate.CompanyId == CompanyId
                         && estimate.StatusId == (int)statusId
                            && estimate.isEstimate == false
                        select estimate).Count();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all orders count placed against a company
        /// </summary>
        /// <param name="CCID"></param>
        /// <returns></returns>
        public int GetAllOrdersCount(long CompanyId)
        {
            try
            {

                return (from estimate in db.Estimates
                        //join status in db.Status on estimate.StatusID equals status.StatusID
                        where estimate.CompanyId == CompanyId
                        && estimate.StatusId != (int)OrderStatus.ShoppingCart && estimate.StatusId != (int)OrderStatus.ArchivedOrder
                            //&& status.StatusType == 2
                        && estimate.isEstimate == false
                        select estimate).Count();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Gets login user orders count which are placed and not archieved
        /// </summary>
        /// <param name="CID"></param>
        /// <param name="CCID"></param>
        /// <returns></returns>
        public int AllOrders(long contactID, long CompanyID)
        {
            try
            {

                return (from estimate in db.Estimates
                        //join status in db.Status on estimate.StatusID equals status.StatusID
                        where estimate.CompanyId == CompanyID && estimate.ContactId == contactID
                        && estimate.StatusId != (int)OrderStatus.ShoppingCart && estimate.StatusId != (int)OrderStatus.ArchivedOrder
                            //&& status.StatusType == 2 
                        && estimate.isEstimate == false
                        select estimate).Count();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get retail user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public CompanyContact GetRetailUser(string email, string password, long OrganisationId, long StoreId)
        {
            var qury = from contacts in db.CompanyContacts
                       join contactCompany in db.Companies on contacts.CompanyId equals contactCompany.CompanyId
                       where (contactCompany.IsCustomer == (int)CustomerTypes.Customers || contactCompany.IsCustomer == (int)CustomerTypes.Prospects) && string.Compare(contacts.Email, email, true) == 0
                       && contacts.OrganisationId == OrganisationId && contactCompany.StoreId == StoreId
                       select contacts;
            if (qury != null)
            {
                return qury.ToList().Where(contct => HashingManager.VerifyHashSha1(password, contct.Password) == true).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
        public long GetContactTerritoryID(long CID)
        {
            try
            {
                return db.CompanyContacts.Where(c => c.ContactId == CID).Select(c => c.TerritoryId ?? 0).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool updateQuikcTextInfo(long contactId, QuickText objQuickText)
        {
            bool result = false;
            var contact = db.CompanyContacts.Where(g => g.ContactId == contactId).FirstOrDefault();
            if (contact != null)
            {

                contact.quickAddress1 = objQuickText.Address1 ?? string.Empty;

                contact.quickCompanyName = objQuickText.Company ?? string.Empty;
                contact.quickCompMessage = objQuickText.CompanyMessage ?? string.Empty;
                contact.quickEmail = objQuickText.Email ?? string.Empty;
                contact.quickFax = objQuickText.Fax ?? string.Empty;
                contact.quickFullName = objQuickText.Name ?? string.Empty;
                contact.quickPhone = objQuickText.Telephone ?? string.Empty;
                contact.quickTitle = objQuickText.Title ?? string.Empty;
                contact.quickWebsite = objQuickText.Website ?? string.Empty;


                contact.quickMobileNumber = objQuickText.MobileNumber ?? string.Empty;
                contact.quickFacebookId = objQuickText.FacebookID ?? string.Empty;
                contact.quickTwitterId = objQuickText.TwitterID ?? string.Empty;
                contact.quickLinkedInId = objQuickText.LinkedInID ?? string.Empty;
                contact.quickOtherId = objQuickText.OtherId ?? string.Empty;
                db.SaveChanges();
                result = true;
            }
            return result;
        }

        public long GetContactAddressID(long cID)
        {
            try
            {
                return db.CompanyContacts.Where(c => c.ContactId == cID).Select(s => s.AddressId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// get contact list by role and company id
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="Role"></param>
        /// <returns></returns>
        public long GetContactIdByRole(long CompanyID, int Role)
        {

            List<CompanyContact> ListOfAdmins = db.CompanyContacts.Where(i => i.CompanyId == CompanyID && i.ContactRoleId == Role).ToList();
            if (ListOfAdmins.Count > 0)
            {
                return ListOfAdmins[0].ContactId;
            }
            else
            {
                return 0;
            }

        }

        public IEnumerable<CompanyContact> GetCompanyContactsByCompanyId(long companyId)
        {
            return db.CompanyContacts.Where(x => x.CompanyId == companyId && x.OrganisationId == OrganisationId).ToList();
        }

        //Update The CompnayContact for The Retail Customer
        public bool UpdateCompanyContactForRetail(CompanyContact Instance)
        {
            bool Result = false;
            try
            {

                CompanyContact oContct = db.CompanyContacts.Where(c => c.ContactId == Instance.ContactId).FirstOrDefault();
                if (oContct != null)
                {
                    oContct.FirstName = Instance.FirstName;
                    oContct.LastName = Instance.LastName;
                    oContct.Email = Instance.Email;
                    oContct.JobTitle = Instance.JobTitle;
                    oContct.HomeTel1 = Instance.HomeTel1;
                    oContct.Mobile = Instance.Mobile;
                    oContct.FAX = Instance.FAX;
                    oContct.quickWebsite = Instance.quickWebsite;
                    oContct.image = Instance.image;
                    oContct.IsEmailSubscription = Instance.IsEmailSubscription;
                    oContct.IsNewsLetterSubscription = Instance.IsNewsLetterSubscription;
                    db.CompanyContacts.Attach(oContct);

                    db.Entry(oContct).State = EntityState.Modified;
                    if (db.SaveChanges() > 0)
                    {
                        Result = true;
                    }
                    else
                    {
                        Result = false;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;

            }
            return Result;
        }
        //Update The CompnayContact for The Corporate Customer
        public bool UpdateCompanyContactForCorporate(CompanyContact Instance)
        {
            bool Result = false;
            try
            {
                CompanyContact oContct = db.CompanyContacts.Where(c => c.ContactId == Instance.ContactId).FirstOrDefault();
                if (oContct != null)
                {
                    oContct.FirstName = Instance.FirstName;
                    oContct.LastName = Instance.LastName;
                    oContct.Email = Instance.Email;
                    oContct.JobTitle = Instance.JobTitle;
                    oContct.HomeTel1 = Instance.HomeTel1;
                    oContct.Mobile = Instance.Mobile;
                    oContct.FAX = Instance.FAX;
                    oContct.quickWebsite = Instance.quickWebsite;
                    oContct.image = Instance.image;
                    oContct.IsEmailSubscription = Instance.IsEmailSubscription;
                    oContct.IsNewsLetterSubscription = Instance.IsNewsLetterSubscription;
                    oContct.POBoxAddress = Instance.POBoxAddress;
                    oContct.CorporateUnit = Instance.CorporateUnit;
                    oContct.OfficeTradingName = Instance.OfficeTradingName;
                    oContct.ContractorName = Instance.ContractorName;
                    oContct.BPayCRN = Instance.BPayCRN;
                    oContct.ABN = Instance.ABN;
                    oContct.ACN = Instance.ACN;
                    oContct.AdditionalField1 = Instance.AdditionalField1;
                    oContct.AdditionalField2 = Instance.AdditionalField2;
                    oContct.AdditionalField3 = Instance.AdditionalField3;
                    oContct.AdditionalField4 = Instance.AdditionalField4;
                    oContct.AdditionalField5 = Instance.AdditionalField5;

                    db.CompanyContacts.Attach(oContct);

                    db.Entry(oContct).State = EntityState.Modified;
                    if (db.SaveChanges() > 0)
                    {
                        Result = true;
                    }
                    else
                    {
                        Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return Result;
        }

        public string GetPasswordByContactID(long ContactID)
        {
            try
            {
                return db.CompanyContacts.Where(c => c.ContactId == ContactID).Select(s => s.Password).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool SaveResetPassword(long ContactID, string Password)
        {
            bool result = false;
            try
            {
                CompanyContact tblContact = db.CompanyContacts.Where(c => c.ContactId == ContactID).FirstOrDefault();

                if (tblContact != null)
                {
                    tblContact.Password = HashingManager.ComputeHashSHA1(Password);
                    result = db.SaveChanges() > 0 ? true : false;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public CompanyContact GetContactByEmailID(string Email)
        {
            return db.CompanyContacts.Where(u => u.Email == Email).FirstOrDefault();
        }
         public  bool ValidatEmail(string email)
         {
            if (System.Text.RegularExpressions.Regex.IsMatch(email, "^[A-Za-z0-9](([_\\.\\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\\.\\-]?[a-zA-Z0-9]+)*)\\.([A-Za-z]{2,})$"))
            {
                return true;
            }
            else
            {
                return false;
            }
         }
        public bool CheckDuplicatesOfContactEmailInStore(string email, long companyId, long companyContactId)
        {
            return DbSet.Any(x => x.Email == email && x.CompanyId == companyId && x.ContactId != companyContactId);
        }
        public CompanyContact createContact(int CCompanyId, string E, string F, string L, string AccountNumber = "", int questionID = 0, string Answer = "", string Password = "")
        {
            CompanyContact tblContacts = new CompanyContact();
            
            tblContacts.isArchived = false;
            tblContacts.CompanyId = CCompanyId;
            tblContacts.FirstName = F;
            tblContacts.LastName = L;
            tblContacts.Email = E;
            if (string.IsNullOrEmpty(Password))
            {
                tblContacts.Password = "1234";
            }
            else
            {
                tblContacts.Password = HashingManager.ComputeHashSHA1(Password);
            }
            if (questionID == 0)
            {
                tblContacts.QuestionId = 1;
                tblContacts.SecretAnswer = "abc";
            }
            else
            {
                tblContacts.QuestionId = Convert.ToInt32(questionID);
                tblContacts.SecretAnswer = Answer;
            }

            tblContacts.ClaimIdentifer = "";
            tblContacts.AuthentifiedBy = "";
            tblContacts.isWebAccess = true;
            tblContacts.isPlaceOrder = true;
            tblContacts.ContactRoleId = 3;
            //Quick Text Fields
            tblContacts.quickAddress1 = "";
            tblContacts.quickAddress2 = "";
            tblContacts.quickAddress3 = "";
            tblContacts.quickCompanyName = "";
            tblContacts.quickCompMessage = "";
            tblContacts.quickEmail = "";
            tblContacts.quickFax = "";
            tblContacts.quickFullName = "";
            tblContacts.quickPhone = "";
            tblContacts.quickTitle = "";
            tblContacts.quickWebsite = "";
            tblContacts.Notes = AccountNumber;
         

            // get default territory Id

            CompanyTerritory oTerritory =  db.CompanyTerritories.Where(t => t.isDefault == true && t.CompanyId == CCompanyId).FirstOrDefault();

            if (oTerritory != null)
            {
                tblContacts.TerritoryId = oTerritory.TerritoryId;
                Address oAddress = db.Addesses.Where(t => t.TerritoryId == oTerritory.TerritoryId).FirstOrDefault();
                if (oAddress != null)
                {
                    tblContacts.AddressId = oAddress.AddressId;
                    tblContacts.ShippingAddressId = oAddress.AddressId;
                }
                else 
                {
                    Address oCompanyAddress = db.Addesses.Where(t => t.CompanyId == CCompanyId).FirstOrDefault();
                    if (oAddress != null)
                    {
                        tblContacts.AddressId = oCompanyAddress.AddressId;
                        tblContacts.ShippingAddressId = oCompanyAddress.AddressId;
                    }
                }
                
            }

            db.CompanyContacts.Add(tblContacts);
            if (db.SaveChanges() > 0)
            {
                return tblContacts;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// get corporate user for auto login process
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="organistionId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public CompanyContact GetCorporateContactForAutoLogin(string emailAddress, long organistionId, long companyId)
        {
            return db.CompanyContacts.Where(c => c.CompanyId == companyId && c.OrganisationId == organistionId && c.Email == emailAddress && c.isWebAccess == true && (c.isArchived == false || c.isArchived == null)).SingleOrDefault();
        }



        public CompanyContact GetContactByContactId(long ContactId)
        {

            db.Configuration.LazyLoadingEnabled = false;
            return db.CompanyContacts.Where(c => c.ContactId == ContactId).FirstOrDefault();

            
        }
        }

    }


