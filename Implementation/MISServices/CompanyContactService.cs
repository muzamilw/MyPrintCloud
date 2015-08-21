using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using MPC.Common;
using MPC.ExceptionHandling;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using System.Text;

namespace MPC.Implementation.MISServices
{
    public class CompanyContactService : ICompanyContactService
    {
        private readonly ICompanyContactRepository companyContactRepository;
        private readonly ICompanyTerritoryRepository companyTerritoryRepository;
        private readonly ICompanyContactRoleRepository companyContactRoleRepository;
        private readonly IRegistrationQuestionRepository registrationQuestionRepository;
        private readonly IAddressRepository addressRepository;
        private readonly IStateRepository stateRepository;
        private readonly IScopeVariableRepository scopeVariableRepository;
        private CompanyContact Create(CompanyContact companyContact)
        {
            UpdateDefaultBehaviourOfContactCompany(companyContact);
            companyContact.OrganisationId = companyContactRepository.OrganisationId;
            companyContactRepository.Add(companyContact);
            companyContactRepository.SaveChanges();
            companyContact.image = SaveCompanyContactProfileImage(companyContact);
            companyContactRepository.Update(companyContact);
            companyContactRepository.SaveChanges();

            if (companyContact.ScopeVariables != null)
            {
                foreach (ScopeVariable scopeVariable in companyContact.ScopeVariables)
                {
                    scopeVariable.Id = companyContact.ContactId;
                    scopeVariableRepository.Add(scopeVariable);
                }
                scopeVariableRepository.SaveChanges();
            }

            return companyContact;
        }
        private CompanyContact Update(CompanyContact companyContact)
        {
            CompanyContact companyContactDbVersion = companyContactRepository.Find(companyContact.ContactId);
            if (companyContactDbVersion != null && companyContactDbVersion.Password != companyContact.Password)
            {
                companyContact.Password = HashingManager.ComputeHashSHA1(companyContact.Password);
            }
            UpdateDefaultBehaviourOfContactCompany(companyContact);
            companyContact.image = SaveCompanyContactProfileImage(companyContact);
            companyContact.OrganisationId = companyContactRepository.OrganisationId;
            companyContactRepository.Update(companyContact);
            if (companyContact.ScopeVariables != null)
            {
                UpdateScopVariables(companyContact);
            }
            companyContactRepository.SaveChanges();


            return companyContact;
        }

        /// <summary>
        /// Update Scop Variables
        /// </summary>
        private void UpdateScopVariables(CompanyContact companyContact)
        {
            IEnumerable<ScopeVariable> scopeVariables = scopeVariableRepository.GetContactVariableByContactId(companyContact.ContactId, (int)FieldVariableScopeType.Contact);
            foreach (ScopeVariable scopeVariable in companyContact.ScopeVariables)
            {
                ScopeVariable scopeVariableDbItem = scopeVariables.FirstOrDefault(
                    scv => scv.ScopeVariableId == scopeVariable.ScopeVariableId);
                if (scopeVariableDbItem != null)
                {
                    scopeVariableDbItem.Value = scopeVariable.Value;
                }
            }
        }

        private void UpdateDefaultBehaviourOfContactCompany(CompanyContact companyContact)
        {
            if (companyContact.IsDefaultContact == 1)
            {
                var allCompanyContactsOfCompany =
                    companyContactRepository.GetCompanyContactsByCompanyId(companyContact.CompanyId);
                foreach (var contact in allCompanyContactsOfCompany)
                {
                    if (contact.IsDefaultContact == 1)
                    {
                        contact.IsDefaultContact = 0;
                        contact.OrganisationId = companyContactRepository.OrganisationId;
                        companyContactRepository.Update(contact);
                    }
                }
            }
        }
        /// <summary>
        /// Method to check user's email Duplicates within store 
        /// </summary>
        /// <param name="companyContact"></param>
        /// <returns></returns>
        private bool CheckDuplicatesOfContactEmailInStore(CompanyContact companyContact)
        {
            var flag = companyContactRepository.CheckDuplicatesOfContactEmailInStore(companyContact.Email,
                companyContact.CompanyId, companyContact.ContactId);
            return flag;
        }
        #region Constructor

        public CompanyContactService(ICompanyContactRepository companyContactRepository, ICompanyTerritoryRepository companyTerritoryRepository,
            ICompanyContactRoleRepository companyContactRoleRepository, IRegistrationQuestionRepository registrationQuestionRepository,
            IAddressRepository addressRepository, IStateRepository stateRepository, IScopeVariableRepository scopeVariableRepository)
        {
            this.companyContactRepository = companyContactRepository;
            this.companyTerritoryRepository = companyTerritoryRepository;
            this.companyContactRoleRepository = companyContactRoleRepository;
            this.registrationQuestionRepository = registrationQuestionRepository;
            this.addressRepository = addressRepository;
            this.stateRepository = stateRepository;
            this.scopeVariableRepository = scopeVariableRepository;
        }

        #endregion

        /// <summary>
        /// Get Company Contacts
        /// </summary>
        public CompanyContactResponse SearchCompanyContacts(CompanyContactRequestModel request)
        {
            return companyContactRepository.GetCompanyContactsForCrm(request);
        }
        /// <summary>
        /// Get Addresses and Territories Of "Company Contact's company"
        /// </summary>
        public CrmContactResponse SearchAddressesAndTerritories(CompanyContactRequestModel request)
        {
            return new CrmContactResponse
            {
                Addresses = addressRepository.GetAddressByCompanyID(request.CompanyId),
                CompanyTerritories = companyTerritoryRepository.GetCompanyTerritory(new CompanyTerritoryRequestModel { CompanyId = request.CompanyId }).CompanyTerritories
            };
        }
        public bool Delete(long companyContactId)
        {
            var dbCompanyContact = companyContactRepository.GetContactByID(companyContactId);
            if (dbCompanyContact != null)
            {
                companyContactRepository.Delete(dbCompanyContact);
                companyContactRepository.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Deletion for Crm
        /// </summary>
        public bool DeleteContactForCrm(long companyContactId)
        {
            var dbCompanyContact = companyContactRepository.GetContactByID(companyContactId);
            if (dbCompanyContact != null)
            {
                dbCompanyContact.isArchived = true;
                companyContactRepository.SaveChanges();
                return true;
            }
            return false;
        }

        public CompanyContact Save(CompanyContact companyContact)
        {
            if (!CheckDuplicatesOfContactEmailInStore(companyContact))
            {
                CompanyContact contact;
                CompanyContact contactToReturn;
                if (companyContact.ContactId <= 0)
                {
                    companyContact.Password = HashingManager.ComputeHashSHA1(companyContact.Password);
                    contact = Create(companyContact);
                    //contactToReturn = companyContactRepository.GetContactByContactId(contact.ContactId);
                    //companyContactRepository.LoadProperty(contactToReturn, () => contactToReturn.Company);
                    return contact;
                }
                contact = Update(companyContact);
                contactToReturn = companyContactRepository.GetContactByContactId(contact.ContactId);
                //companyContactRepository.LoadProperty(contactToReturn, () => contactToReturn.Company);
                return contactToReturn;
            }
            throw new MPCException("Duplicate Email/Username are not allowed", companyContactRepository.OrganisationId);
        }

        /// <summary>
        /// Get Base Data
        /// </summary>
        public CompanyBaseResponse GetBaseData()
        {
            return new CompanyBaseResponse
            {
                CompanyContactRoles = companyContactRoleRepository.GetAll(),
                RegistrationQuestions = registrationQuestionRepository.GetAll(),
                States = stateRepository.GetAll()
            };
        }

        /// <summary>
        /// Get Contact Detail
        /// </summary>
        public CompanyBaseResponse GetContactDetail(short companyId)
        {
            return new CompanyBaseResponse
            {
                CompanyTerritories = companyTerritoryRepository.GetAllCompanyTerritories(companyId),
                Addresses = addressRepository.GetAllAddressByStoreId(companyId),
            };
        }

        /// <summary>
        /// Save Images for Company Contact Profile Image
        /// </summary>
        private string SaveCompanyContactProfileImage(CompanyContact companyContact)
        {
            if (companyContact.ContactProfileImage != null)
            {
                string base64 = companyContact.ContactProfileImage.Substring(companyContact.ContactProfileImage.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);

                string directoryPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyContactRepository.OrganisationId + "/" + companyContact.CompanyId + "/Contacts");

                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string savePath =
                    directoryPath + "\\" +
                    companyContact.ContactId + "_" + StringHelper.SimplifyString(companyContact.FirstName) + "_profile.png";
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                return savePath;
            }
            return null;
        }

        public bool SaveImportedContact(IEnumerable<CompanyContact> companyContacts)
        {
            foreach (var companyContact in companyContacts)
            {
                if (!CheckDuplicatesOfContactEmailInStore(companyContact))
                {
                    companyContact.Password = HashingManager.ComputeHashSHA1(companyContact.Password);//todo check is it req. or not later
                    UpdateDefaultBehaviourOfContactCompany(companyContact);
                    companyContact.OrganisationId = companyContactRepository.OrganisationId;
                    companyContactRepository.Add(companyContact);
                }
                else
                {
                    throw new MPCException("Duplicate Email/Username are not allowed", companyContactRepository.OrganisationId);
                }
            }
            companyContactRepository.SaveChanges();
            return true;
        }

        /// <summary>
        /// Get Contacts for order screen
        /// </summary>
        public ContactsResponseForOrder GetContactsForOrder(CompanyRequestModelForCalendar request)
        {
            return companyContactRepository.GetContactsForOrder(request);
        }


        public CompanyContact GetContactByContactId(long ContactId)
        {
           return companyContactRepository.GetContactByContactId(ContactId);
        }

        public string ExportCSV(long CompanyId)
        {
            SaveFileDialog csvDialog = new SaveFileDialog();
            csvDialog.Filter = "CSV Files| *.csv";
            csvDialog.DefaultExt = "csv";

            List<string> FileHeader = new List<string>();
             // List<string> FileHeader = new List<string>();

            FileHeader = HeaderList(FileHeader);

            long OrganisationId = 0;
            IEnumerable<CompanyContact> companyContacts = companyContactRepository.GetContactsByCompanyId(CompanyId);


            StringBuilder CSV = new StringBuilder();
                   string csv = string.Empty;

                    foreach (string column in FileHeader)
                    {
                        //Add the Header row for CSV file.
                        csv += column + '|';
                    }

                            //Add new line.
                            csv += "\r\n";
                            CSV.Append(csv);
                          //  sr.WriteLine(csv);

                         //   csv = string.Empty;


                            string AddressName = string.Empty;
                             string Address1 = string.Empty;
                             string Address2 = string.Empty;
                             string City = string.Empty;
                             string State = string.Empty;
                             string Postcode = string.Empty;
                            string TerritoryName = string.Empty;
                            string Fax = string.Empty;
                            string FirstName = string.Empty;
                            string LastName = string.Empty;
                            string JobTitle = string.Empty;
                            string HomeTel = string.Empty;
                            string Email = string.Empty;
                            string Mobile = string.Empty;
                            string ContactFax = string.Empty;
                           string AddField1 = string.Empty;
                          string AddField2 = string.Empty;
                          string AddField3 = string.Empty;
                          string AddField4 = string.Empty;
                          string AddField5 = string.Empty;
                          string SkypeId = string.Empty;
                          string LinkedIn = string.Empty;
                          string FacebookURL = string.Empty;
                          string TwitterURL = string.Empty;
                          string POBoxAddress = string.Empty;
                          string CorporateUnit = string.Empty;
                          string OfficeTradingName = string.Empty;
                          string BPayCRN = string.Empty;
                          string ACN = string.Empty;
                          string ContractorName = string.Empty;
                          string ABN = string.Empty;
                          string Notes = string.Empty;
                            string CanUserEditProfile = string.Empty;
                            string CanPayByPersonalCreditCard = string.Empty;
                            string CanSeePrices= string.Empty;
                            string HasWebAccess = string.Empty;
                            string CanPlaceDirectOrder = string.Empty;
                            string CanPlaceOrderWithoutApproval = string.Empty;
                            string CanPlaceOrder = string.Empty;
                            string Role = string.Empty;
                             string IsNewsLetterSubscription = string.Empty;
                            string IsEmailSubscription = string.Empty;
                            string IsDefaultContact = string.Empty;
                 
                            foreach(var contact in companyContacts)
                            {
                                string data = string.Empty;
                                OrganisationId = contact.OrganisationId ?? 0;
                                if(contact.Address != null)
                                {
                                    AddressName = contact.Address.AddressName;
                                    Address1 = contact.Address.Address1;
                                    Address2 = contact.Address.Address2;
                                    City =  contact.Address.City;
                                    if(contact.Address.State != null)
                                    {
                                        State = contact.Address.State.StateName;
                                        Postcode = contact.Address.PostCode;
                                    }
                                    Fax = contact.Address.Fax;
                                }
                                if(contact.CompanyTerritory != null)
                                {
                                    TerritoryName = contact.CompanyTerritory.TerritoryName;
                                }

                                if(!string.IsNullOrEmpty(contact.FirstName))
                                    FirstName = contact.FirstName;

                                 if(!string.IsNullOrEmpty(contact.LastName))
                                    LastName = contact.LastName;

                                if(!string.IsNullOrEmpty(contact.JobTitle))
                                    JobTitle = contact.JobTitle;

                                 if(!string.IsNullOrEmpty(contact.HomeTel1))
                                    HomeTel = contact.HomeTel1;

                                if(!string.IsNullOrEmpty(contact.Email))
                                    Email = contact.Email;

                                 if(!string.IsNullOrEmpty(contact.Mobile))
                                    Mobile = contact.Mobile;

                                if(!string.IsNullOrEmpty(contact.FAX))
                                    ContactFax = contact.FAX;

                                 if(!string.IsNullOrEmpty(contact.AdditionalField1))
                                   AddField1 = contact.AdditionalField1;

                                 if(!string.IsNullOrEmpty(contact.AdditionalField2))
                                    AddField2 = contact.AdditionalField2;

                                 if(!string.IsNullOrEmpty(contact.AdditionalField3))
                                    AddField3 = contact.AdditionalField3;

                                 if(!string.IsNullOrEmpty(contact.AdditionalField4))
                                    AddField4 = contact.AdditionalField4;

                                 if(!string.IsNullOrEmpty(contact.AdditionalField5))
                                    AddField5 = contact.AdditionalField5;


                                 if (!string.IsNullOrEmpty(contact.SkypeId))
                                     SkypeId = contact.SkypeId;

                                 if (!string.IsNullOrEmpty(contact.LinkedinURL))
                                     LinkedIn = contact.LinkedinURL;

                                 if (!string.IsNullOrEmpty(contact.FacebookURL))
                                     FacebookURL = contact.FacebookURL;

                                 if (!string.IsNullOrEmpty(contact.TwitterURL))
                                     TwitterURL = contact.TwitterURL;

                                 if (!string.IsNullOrEmpty(contact.POBoxAddress))
                                     POBoxAddress = contact.POBoxAddress;


                                 if (!string.IsNullOrEmpty(contact.CorporateUnit))
                                     CorporateUnit = contact.CorporateUnit;

                                 if (!string.IsNullOrEmpty(contact.OfficeTradingName))
                                     OfficeTradingName = contact.OfficeTradingName;

                                 if (!string.IsNullOrEmpty(contact.BPayCRN))
                                     BPayCRN = contact.BPayCRN;

                                 if (!string.IsNullOrEmpty(contact.ACN))
                                     ACN = contact.ACN;

                                 if (!string.IsNullOrEmpty(contact.ContractorName))
                                     ContractorName = contact.ContractorName;

                                 if (!string.IsNullOrEmpty(contact.ABN))
                                     ABN = contact.ABN;

                                 if (!string.IsNullOrEmpty(contact.Notes))
                                     Notes = contact.Notes;


                                if(contact.CanUserEditProfile ?? false)
                                   CanUserEditProfile = "True";
                                else
                                     CanUserEditProfile = "False";



                                 if(contact.canUserPlaceOrderWithoutApproval ?? false)
                                   CanPlaceOrderWithoutApproval = "True";
                                else
                                     CanPlaceOrderWithoutApproval = "False";

                
                                 if(contact.canPlaceDirectOrder ?? false)
                                   CanPlaceDirectOrder = "True";
                                else
                                     CanPlaceDirectOrder = "False";



                                if(contact.IsPayByPersonalCreditCard ?? false)
                                   CanPayByPersonalCreditCard = "True";
                                else
                                     CanPayByPersonalCreditCard = "False";

                                  if(contact.IsPricingshown ?? false)
                                   CanSeePrices = "True";
                                else
                                     CanSeePrices = "False";

                                 if(contact.isWebAccess ?? false)
                                   HasWebAccess = "True";
                                else
                                     HasWebAccess = "False";

                                  if(contact.canPlaceDirectOrder ?? false)
                                   CanPlaceOrder = "True";
                                else
                                     CanPlaceOrder = "False";

                                 if(contact.ContactRoleId == 1)
                                   Role = "A";
                                else if(contact.ContactRoleId == 2)
                                     Role = "B";
                                 else
                                     Role = "C";

                                if(contact.IsNewsLetterSubscription ?? false)
                                   IsNewsLetterSubscription = "True";
                                else
                                     IsNewsLetterSubscription = "False";

                                  if(contact.IsEmailSubscription ?? false)
                                   IsEmailSubscription = "True";
                                else
                                     IsEmailSubscription = "False";

                                  if(contact.IsDefaultContact == 1)
                                   IsDefaultContact = "True";
                                else
                                     IsDefaultContact = "False";







                                  data = AddressName + "|" + Address1 + "|" + Address2 + "|" + City + "|" + State + "|" + Postcode + "|" +
                                      TerritoryName + "|" + Fax
                                      + "|" + FirstName + "|" + LastName + "|" +
                                      JobTitle + "|" + HomeTel 
                                      + "|" + Email + "|" 
                                      + Mobile  + "|" 
                                      + ContactFax
                                      + "|" + AddField1
                                      + "|" + AddField2 + "|" +
                                      AddField3 
                                      + "|" + AddField4
                                      + "|" + AddField5 
                                      + "|" + SkypeId + "|" 
                                      + LinkedIn + "|"
                                      + FacebookURL
                                      + "|" + TwitterURL + "|" 
                                      + CanUserEditProfile + "|" + CanPlaceOrderWithoutApproval + "|" + CanPlaceDirectOrder
                                      + "|" + CanPayByPersonalCreditCard + "|" + CanSeePrices + "|" + HasWebAccess + "|"
                                      + CanPlaceOrder + "|" + HomeTel 
                                      + "|" + Role + "|" + POBoxAddress + "|" +
                                      CorporateUnit
                                      + "|" + OfficeTradingName + "|" + BPayCRN + "|" + ACN + "|" + ContractorName + "|" + ABN + "|" + Notes + "|" + IsNewsLetterSubscription + '|' + IsEmailSubscription + "|" + IsDefaultContact + "\r\n";
                    

                                ////Add new line.
                                //csv += "\r\n";

                                CSV.Append(data);

                                //sr.WriteLine(csv);

                                //csv = string.Empty;
                            }

                          



                           string CSVSavePath = string.Empty;
                           string ReturnPath = string.Empty;

                            if(OrganisationId > 0)
                            {
                                CSVSavePath = HttpContext.Current.Server.MapPath("/MPC_Content/Reports/" + OrganisationId + "/" + CompanyId + "_CompanyContacts.csv");
                                ReturnPath = "/MPC_Content/Reports/" + OrganisationId + "/" + CompanyId + "_CompanyContacts.csv";

                            }
                            else
                            {
                                CSVSavePath = HttpContext.Current.Server.MapPath("/MPC_Content/Reports/" + CompanyId + "_CompanyContacts.csv");
                                ReturnPath = "/MPC_Content/Reports/" + CompanyId + "_CompanyContacts.csv";

                            }

                            
                            
                           
                            StreamWriter sw = new StreamWriter(CSVSavePath, false, Encoding.UTF8);
                            sw.Write(CSV);
                            sw.Close();

                            return ReturnPath;
          

        }


        public List<string> HeaderList(List<string> FileHeader)
        {

            FileHeader.Add("AddressName");
            FileHeader.Add("Address1");
            FileHeader.Add("Address2");
            FileHeader.Add("City");
            FileHeader.Add("State");
            FileHeader.Add("Postcode");
            FileHeader.Add("TerritoryName");
            FileHeader.Add("AddressFax");
            FileHeader.Add("ContactFirstName");
            FileHeader.Add("ContactLastName");
            FileHeader.Add("JobTitle");
            FileHeader.Add("ContactPhone");
            FileHeader.Add("Email");
            FileHeader.Add("Mobile");
            FileHeader.Add("ContactFax");
            FileHeader.Add("AddInfo1");
            FileHeader.Add("AddInfo2");
            FileHeader.Add("AddInfo3");
            FileHeader.Add("AddInfo4");
            FileHeader.Add("AddInfo5");
            FileHeader.Add("SkypeId");
            FileHeader.Add("LinkedInUrl");
            FileHeader.Add("FacebookUrl");
            FileHeader.Add("TwitterUrl");
            FileHeader.Add("CanEditProfile");
            FileHeader.Add("CanPlaceOrderWithoutApproval");
            FileHeader.Add("CanPlaceDirectOrder");
            FileHeader.Add("CanPayByPersonalCreditCard");

            FileHeader.Add("CanSeePrices");
            FileHeader.Add("HasWebAccess");
            FileHeader.Add("CanPlaceOrder");
            FileHeader.Add("DirectLine");


            FileHeader.Add("UserRole");
            FileHeader.Add("POBoxAddress");
            FileHeader.Add("CorporateUnit");
            FileHeader.Add("TradingName");
            FileHeader.Add("BPayCRN");
            FileHeader.Add("ACN");
            FileHeader.Add("ContractorName");
            FileHeader.Add("ABN");
            FileHeader.Add("Notes");

            FileHeader.Add("CreditLimit");
            FileHeader.Add("IsNewsLetterSubscription");
            FileHeader.Add("IsEmailSubscription");
            FileHeader.Add("IsDefaultContact");


            return FileHeader;

        }

    

    }
}
