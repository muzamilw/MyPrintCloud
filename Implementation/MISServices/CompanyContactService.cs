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
    }
}
