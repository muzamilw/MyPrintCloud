﻿using System.Collections.Generic;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.MISServices
{
    public interface ICompanyContactService
    {
        /// <summary>
        /// Save
        /// </summary>
        CompanyContact Save(CompanyContact companyContact);
        /// <summary>
        /// Delete
        /// </summary>
        bool Delete(long companyContactId);
        /// <summary>
        /// Get Contacts for order screen
        /// </summary>
        ContactsResponseForOrder GetContactsForOrder(CompanyRequestModelForCalendar request);
        CrmContactResponse SearchAddressesAndTerritories(CompanyContactRequestModel request);
        CompanyBaseResponse GetContactDetail(short companyId);
        /// <summary>
        /// Get Company Contacts
        /// </summary>
        CompanyContactResponse SearchCompanyContacts(CompanyContactRequestModel request);

        /// <summary>
        /// Deletion for Crm
        /// </summary>
        bool DeleteContactForCrm(long companyContactId);

        /// <summary>
        /// Get Base Data
        /// </summary>
        CompanyBaseResponse GetBaseData();

        bool SaveImportedContact(IEnumerable<CompanyContact> companyContacts);

        CompanyContact GetContactByContactId(long ContactId);

        string ExportCSV(long CompanyId);

        string ExportCRMContacts();
        List<ZapierInvoiceDetail> GetStoreContactForZapier(long organisationId);

    }
}
