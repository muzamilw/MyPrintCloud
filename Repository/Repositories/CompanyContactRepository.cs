
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Practices.Unity;
using MPC.Models.DomainModels;
using MPC.Interfaces.Repository;
using MPC.Repository.BaseRepository;
using System.Data.Entity;
using MPC.Common;

namespace MPC.Repository.Repositories
{
    public class CompanyContactRepository : BaseRepository<CompanyContact>, ICompanyContactRepository
    {
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

        public CompanyContact GetContactUser(string email, string password)
        {
            var qury = from contacts in db.CompanyContacts
                       join contactCompany in db.Company on contacts.CompanyId equals contactCompany.CompanyId
                       where string.Compare(contacts.Email, email, true) == 0
                       select contacts;

            return qury.ToList().Where(contct => VerifyHashSha1(password, contct.Password) == true).FirstOrDefault();
        } 
        public CompanyContact GetContactByFirstName(string FName)
        {
            var qry = from contacts in db.CompanyContacts
                      join contactCompany in db.Company on contacts.CompanyId equals contactCompany.CompanyId
                      where string.Compare(contacts.twitterScreenName, FName, true) == 0
                      select contacts;

            return qry.ToList().FirstOrDefault();

        }

        public CompanyContact GetContactByEmail(string Email)
        {
            var qry = from contacts in db.CompanyContacts
                      join contactCompany in db.Company on contacts.CompanyId equals contactCompany.CompanyId
                      where string.Compare(contacts.Email, Email, true) == 0
                      select contacts;

            return qry.ToList().FirstOrDefault();

        }
        private static bool VerifyHashSha1(string plainText, string compareWithSalt)
        {
            bool result = false;

            try
            {
                result = VerifyHash(plainText, "SHA1", compareWithSalt);

            }
            catch (CryptographicException)
            {
                result = false;
            }
            catch (Exception)
            {
                result = false;
            }


            return result;
        }

        private static bool VerifyHash(string plainText,
                                string hashAlgorithm,
                                string hashValue)
        {
            // Convert base64-encoded hash value into a byte array.
            byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

            // We must know size of hash (without salt).
            int hashSizeInBits, hashSizeInBytes;

            //// Make sure that hashing algorithm name is specified.
            //if (hashAlgorithm == null)
            //    hashAlgorithm = "";
            hashSizeInBits = InitializeHashSize(hashAlgorithm);

            // Convert size of hash from bits to bytes.
            hashSizeInBytes = hashSizeInBits / 8;

            // Make sure that the specified hash value is long enough.
            if (hashWithSaltBytes.Length < hashSizeInBytes)
                return false;

            // Allocate array to hold original salt bytes retrieved from hash.
            byte[] saltBytes = new byte[hashWithSaltBytes.Length -
                                        hashSizeInBytes];

            // Copy salt from the end of the hash to the new array.
            for (int i = 0; i < saltBytes.Length; i++)
                saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

            // Compute a new hash string.
            string expectedHashString =
                        ComputeHash(plainText, hashAlgorithm, saltBytes);

            // If the computed hash matches the specified hash,
            // the plain text value must be correct.
            return (hashValue == expectedHashString);
        }
        private static int InitializeHashSize(string hashAlgorithm)
        {
            int hashSizeInBits = 0;
            // Size of hash is based on the specified algorithm.
            switch (hashAlgorithm)
            {
                case "SHA1":
                    hashSizeInBits = 160;
                    break;

                case "SHA256":
                    hashSizeInBits = 256;
                    break;

                case "SHA384":
                    hashSizeInBits = 384;
                    break;

                case "SHA512":
                    hashSizeInBits = 512;
                    break;

                default: // Must be MD5
                    hashSizeInBits = 128;
                    break;
            }
            return hashSizeInBits;
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

        private static string ComputeHashSHA1(string plainText)
        {
            string salt = string.Empty;

            try
            {
                salt = ComputeHash(plainText, "SHA1", null);

            }
            catch (CryptographicException ex)
            {
                throw ex;
            }


            return salt;
        }
        public Int64 CreateContact(CompanyContact Contact, string Name,int OrganizationID,int CustomerType,string TwitterScreanName)
        {
            Address address = null;
            CompanyContact tblContacts = null;

            Int64 customerID = 0;

            //CompanySiteManager companySiteManager = new CompanySiteManager();
            Company Company = new Company();
            Organisation organization =  null;// CompanySiteManager.GetCompanySite();

            Company.isArchived = false;
            Company.AccountNumber = "123";
            Company.AccountOpenDate = DateTime.Now;
            Company.Name = Name;
            Company.TypeId = CustomerType; //contactCompanyType.TypeID;
            Company.Status = 0;

            if (Contact != null && !string.IsNullOrEmpty(Contact.Mobile ))
                Company.PhoneNo = Contact.Mobile;
            
          
            Company.CreationDate = DateTime.Now;
            Company.AccountManagerId = Company.AccountManagerId;
            Company.CreditLimit = 0;
            Company.IsCustomer = Convert.ToInt16(CustomerType);
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
                    db.Company.Add(Company);

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
                        tblContacts.Password = ComputeHashSHA1(Contact.Password);
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
                    db.CompanyContacts.Add(Contact);
                   

                    if (db.SaveChanges() > 0)
                    {
                        customerID =  Company.CompanyId; // customer id
                        if (Contact != null)
                        {
                            Contact.ContactId = tblContacts.ContactId;
                            Contact.CompanyId = customerID;
                        }
                    }

                    return customerID;
                }

        public Markup GetZeroMarkup()
        {
            return db.Markups.Where(c => c.MarkUpRate.Value == 0).FirstOrDefault();
        }

        private Address PopulateAddressObject(int dummyAddreID, int customerID, bool isDefaulAddress, bool isDefaultShippingAddress)
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

        private CompanyContact PopulateContactsObject(int customerID, int addressID, bool isDefaultContact)
        {
            CompanyContact tblContacts = new CompanyContact();
            tblContacts.CompanyId = customerID;
            tblContacts.AddressId = addressID;
            tblContacts.FirstName = string.Empty;
            tblContacts.IsDefaultContact = isDefaultContact == true ? 1 : 0;

            return tblContacts;
        }

        public CompanyContact GetContactByID(Int64 ContactID)
        {
            return db.CompanyContacts.FirstOrDefault();
        }


    }
}
