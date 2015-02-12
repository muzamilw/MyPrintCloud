﻿using System.Windows.Forms;
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
         private CompanyContact Create(CompanyContact companyContact)
         {
             UpdateDefaultBehaviourOfContactCompany(companyContact);
             companyContactRepository.Add(companyContact);
             companyContactRepository.SaveChanges();
             return companyContact;
         }
         private CompanyContact Update(CompanyContact companyContact)
         {
             UpdateDefaultBehaviourOfContactCompany(companyContact);
             companyContactRepository.Update(companyContact);
             companyContactRepository.SaveChanges();
             return companyContact;
         }

        private void UpdateDefaultBehaviourOfContactCompany(CompanyContact companyContact)
        {
            if (companyContact.IsDefaultContact == 1 )
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

         public CompanyContactService(ICompanyContactRepository companyContactRepository)
        {
            this.companyContactRepository = companyContactRepository;
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
            if (dbCompanyContact != null )
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

       
    }
}
