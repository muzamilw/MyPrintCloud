using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
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
        private CompanyContact Create(CompanyContact companyContact)
        {
            UpdateDefaultBehaviourOfContactCompany(companyContact);
            companyContactRepository.Add(companyContact);
            companyContactRepository.SaveChanges();
            companyContact.image = SaveCompanyContactProfileImage(companyContact);
            companyContactRepository.Update(companyContact);
            companyContactRepository.SaveChanges();
            return companyContact;
        }
        private CompanyContact Update(CompanyContact companyContact)
        {
            UpdateDefaultBehaviourOfContactCompany(companyContact);
            companyContact.image = SaveCompanyContactProfileImage(companyContact);
            companyContactRepository.Update(companyContact);
            companyContactRepository.SaveChanges();
            if (companyContact.CompanyContactVariables != null)
            {
                updateCompanyContactvariable(companyContact);
            }


            return companyContact;
        }

        private void updateCompanyContactvariable(CompanyContact companyContact)
        {
            CompanyContact companyContactDbVesion = companyContactRepository.Find(companyContact.ContactId);
            foreach (var companyContactVariable in companyContact.CompanyContactVariables)
            {
                CompanyContactVariable companyContactVariableDbItem = companyContactDbVesion.CompanyContactVariables.FirstOrDefault(
                    ccv => ccv.ContactVariableId == companyContactVariable.ContactVariableId);
                if (companyContactVariableDbItem != null)
                {
                    companyContactVariableDbItem.Value = companyContactVariable.Value;
                }
            }
            companyContactRepository.SaveChanges();
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
                        companyContactRepository.Update(contact);
                    }
                }
            }
        }
        #region Constructor

        public CompanyContactService(ICompanyContactRepository companyContactRepository, ICompanyTerritoryRepository companyTerritoryRepository, ICompanyContactRoleRepository companyContactRoleRepository, IRegistrationQuestionRepository registrationQuestionRepository, IAddressRepository addressRepository, IStateRepository stateRepository)
        {
            this.companyContactRepository = companyContactRepository;
            this.companyTerritoryRepository = companyTerritoryRepository;
            this.companyContactRoleRepository = companyContactRoleRepository;
            this.registrationQuestionRepository = registrationQuestionRepository;
            this.addressRepository = addressRepository;
            this.stateRepository = stateRepository;
        }

        #endregion

        /// <summary>
        /// Get Company Contacts
        /// </summary>
        public CompanyContactResponse SearchCompanyContacts(CompanyContactRequestModel request)
        {
            return companyContactRepository.GetCompanyContactsForCrm(request);
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
            if (companyContact.ContactId == 0)
            {
                return Create(companyContact);
            }
            return Update(companyContact);
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
                string savePath = directoryPath + "\\" + companyContact.ContactId + "_profile" + ".png";
                File.WriteAllBytes(savePath, data);
                return savePath;
            }
            return null;
        }
    }
}
