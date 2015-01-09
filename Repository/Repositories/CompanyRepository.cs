using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Interfaces.Repository;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;


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

        /// <summary>
        /// Get Company Price Flag id for Price Matrix in webstore
        /// </summary>
        public int? GetPriceFlagIdByCompany(long CompanyId)
        {
            return DbSet.Where(c => c.CompanyId == CompanyId).Select(f => f.PriceFlagId).FirstOrDefault();
        }

        /// <summary>
        /// Get All Suppliers For Current Organisation
        /// </summary>
        public IEnumerable<Company> GetAllSuppliers()
        {
            return DbSet.Where(supplier => supplier.OrganisationId == OrganisationId && supplier.IsCustomer == 0).ToList();
        }

        public int CreateCustomer(string name, bool isEmailSubScription, bool isNewsLetterSubscription, ContactCompanyTypes customerType, string RegWithTwitter,Markup zeroMarkup, CompanyContact regContact = null)
        {
            Address tblAddress = null;
            CompanyContact tblContacts = null;

            int customerID = 0;

            //CompanySiteManager companySiteManager = new CompanySiteManager();
            Company contactCompany = new Company();
          //  Organisation tblCompSite = CompanySiteManager.GetCompanySite();

            contactCompany.isArchived = false;
            contactCompany.AccountNumber = "123";
            contactCompany.AccountOpenDate = DateTime.Now;
            contactCompany.Name = name;
            contactCompany.TypeId = (int)customerType; //contactCompanyType.TypeID;
            contactCompany.Status = 0;

            if (regContact != null && !string.IsNullOrEmpty(regContact.Mobile))
                contactCompany.PhoneNo = regContact.Mobile;

            contactCompany.CreationDate = DateTime.Now;

            contactCompany.CreditLimit = 0;

            contactCompany.IsCustomer = 0; //prospect


           
            if (zeroMarkup != null)
            {
                contactCompany.DefaultMarkUpId = (int)zeroMarkup.MarkUpId;
            }
            else
            {
                contactCompany.DefaultMarkUpId = 1;
            }

            //Create Customer
            db.Companies.Add(contactCompany);

            //Create Billing Address and Delivery Address and mark them default billing and shipping
            tblAddress = PopulateAddressObject(0, (int)contactCompany.CompanyId, true, true);
            db.Addesses.Add(tblAddress);

            //Create Contact
            tblContacts = PopulateContactsObject((int)contactCompany.CompanyId, (int)tblAddress.AddressId, true);
            tblContacts.isArchived = false;

            if (regContact != null)
            {
                tblContacts.FirstName = regContact.FirstName;
                tblContacts.LastName = regContact.LastName;
                tblContacts.Email = regContact.Email;
                tblContacts.Mobile = regContact.Mobile;
                tblContacts.Password = ComputeHashSHA1(regContact.Password);
                tblContacts.QuestionId = 1;
                tblContacts.SecretAnswer = "";
                tblContacts.ClaimIdentifer = regContact.ClaimIdentifer;
                tblContacts.AuthentifiedBy = regContact.AuthentifiedBy;
                //Quick Text Fields
                tblContacts.quickAddress1 = regContact.quickAddress1;
                tblContacts.quickAddress2 = regContact.quickAddress2;
                tblContacts.quickAddress3 = regContact.quickAddress3;
                tblContacts.quickCompanyName = regContact.quickCompanyName;
                tblContacts.quickCompMessage = regContact.quickCompMessage;
                tblContacts.quickEmail = regContact.quickEmail;
                tblContacts.quickFax = regContact.quickFax;
                tblContacts.quickFullName = regContact.quickFullName;
                tblContacts.quickPhone = regContact.quickPhone;
                tblContacts.quickTitle = regContact.quickTitle;
                tblContacts.quickWebsite = regContact.quickWebsite;
                if (!string.IsNullOrEmpty(RegWithTwitter))
                {
                    tblContacts.twitterScreenName = RegWithTwitter;
                }


            }

            db.CompanyContacts.Add(tblContacts);

            if (db.SaveChanges() > 0)
            {
                customerID = (int)contactCompany.CompanyId; // customer id
                if (regContact != null)
                {
                    regContact.ContactId = tblContacts.ContactId;
                    regContact.CompanyId = customerID;
                }
            }
            //}

            return customerID;
        }
        public  CompanyContact PopulateContactsObject(int customerID, int addressID, bool isDefaultContact)
        {
            CompanyContact tblContacts = new CompanyContact();
            tblContacts.CompanyId = customerID;
            tblContacts.AddressId = addressID;
            tblContacts.FirstName = string.Empty;
            tblContacts.IsDefaultContact = isDefaultContact == true ? 1 : 0;

            return tblContacts;
        }
        public Address PopulateAddressObject(int dummyAddreID, int customerID, bool isDefaulAddress, bool isDefaultShippingAddress)
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
        private static string ComputeHashSHA1(string plainText)
        {
            string salt = string.Empty;

          
                salt = ComputeHash(plainText, "SHA1", null);

          
            return salt;
        }
        private static string ComputeHash(string plainText,
                                    string hashAlgorithm,
                                    byte[] saltBytes)
        {
            // If salt is not specified, generate it on the fly.
            if (saltBytes == null)
            {
                // Define min and max salt sizes.
                int minSaltSize = 4;
                int maxSaltSize = 8;

                // Generate a random number for the size of the salt.
                Random random = new Random();
                int saltSize = random.Next(minSaltSize, maxSaltSize);

                // Allocate a byte array, which will hold the salt.
                saltBytes = new byte[saltSize];

                // Initialize a random number generator.
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

                // Fill the salt with cryptographically strong byte values.
                rng.GetNonZeroBytes(saltBytes);
            }

            // Convert plain text into a byte array.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // Allocate array, which will hold plain text and salt.
            byte[] plainTextWithSaltBytes =
                    new byte[plainTextBytes.Length + saltBytes.Length];

            // Copy plain text bytes into resulting array.
            for (int i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSaltBytes[i] = plainTextBytes[i];

            // Append salt bytes to the resulting array.
            for (int i = 0; i < saltBytes.Length; i++)
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

            // Because we support multiple hashing algorithms, we must define
            // hash object as a common (abstract) base class. We will specify the
            // actual hashing algorithm class later during object creation.
            HashAlgorithm hash;



            // Make sure hashing algorithm name is specified.
            //if (hashAlgorithm == null)
            //    hashAlgorithm = "";
            hash = CreateHashAlgoFactory(hashAlgorithm);

            // Compute hash value of our plain text with appended salt.
            byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

            // Create array which will hold hash and original salt bytes.
            byte[] hashWithSaltBytes = new byte[hashBytes.Length +
                                                saltBytes.Length];

            // Copy hash bytes into resulting array.
            for (int i = 0; i < hashBytes.Length; i++)
                hashWithSaltBytes[i] = hashBytes[i];

            // Append salt bytes to the result.
            for (int i = 0; i < saltBytes.Length; i++)
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

            // Convert result into a base64-encoded string.
            string hashValue = Convert.ToBase64String(hashWithSaltBytes);

            // Return the result.
            return hashValue;
        }

        private static HashAlgorithm CreateHashAlgoFactory(string hashAlgorithm)
        {
            HashAlgorithm hash = null; ;
            // Initialize appropriate hashing algorithm class.
            switch (hashAlgorithm)
            {
                case "SHA1":
                    hash = new SHA1Managed();
                    break;

                case "SHA256":
                    hash = new SHA256Managed();
                    break;

                case "SHA384":
                    hash = new SHA384Managed();
                    break;

                case "SHA512":
                    hash = new SHA512Managed();
                    break;

                default:
                    hash = new MD5CryptoServiceProvider(); // mdf default
                    break;
            }
            return hash;
        }
    }
}
