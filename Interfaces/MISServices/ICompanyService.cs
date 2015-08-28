
﻿using System.Collections.Generic;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.MISServices
{
    public interface ICompanyService
    {

        /// <summary>
        /// Delete Media Library Item By Id
        /// </summary>
        void DeleteMedia(long mediaId);
        /// <summary>
        /// Deletes a company permanently
        /// </summary>
        void DeleteCompanyPermanently(long companyId);

        CompanyResponse GetAllCompaniesOfOrganisation(CompanyRequestModel request);
        CompanyTerritoryResponse SearchCompanyTerritories(CompanyTerritoryRequestModel request);
        CompanyContactResponse SearchCompanyContacts(CompanyContactRequestModel request);
        AddressResponse SearchAddresses(AddressRequestModel request);
        PaymentGatewayResponse SearchPaymentGateways(PaymentGatewayRequestModel request);
        CompanyResponse GetCompanyById(long companyId);
        CompanyBaseResponse GetBaseDataForNewCompany();
        CompanyBaseResponse GetBaseData(long clubId);
        /// <summary>
        /// Save File Path In Db against organization ID
        /// </summary>
        void SaveFile(string filePath, long companyId);

        Company SaveCompany(CompanySavingModel company);
        long GetOrganisationId();

        /// <summary>
        /// Get CMS Pages
        /// </summary>
        SecondaryPageResponse GetCMSPages(SecondaryPageRequestModel request);

        /// <summary>
        /// Get Cms Page By Id
        /// </summary>
        CmsPage GetCmsPageById(long pageId);


        /// <summary>
        /// Get Cms Page Widget By Page Id
        /// </summary>
        IEnumerable<CmsSkinPageWidget> GetCmsPageWidgetByPageId(long pageId, long companyId);
        /// <summary>
        /// Load Items, based on search filters
        /// </summary>
        ItemListViewSearchResponse GetItems(CompanyProductSearchRequestModel request);

        Company DeleteCompany(long companyId);

        /// <summary>
        /// Get Items For Widgets
        /// </summary>
        List<Item> GetItemsForWidgets();

        /// <summary>
        /// Save Field Variable
        /// </summary>
        long SaveFieldVariable(FieldVariable fieldVariable);

        /// <summary>
        /// Get Field Variables
        /// </summary>
        FieldVariableResponse GetFieldVariables(FieldVariableRequestModel request);

        /// <summary>
        /// Get Field Variable Detail
        /// </summary>
        FieldVariable GetFieldVariableDetail(long fieldId);

        /// <summary>
        /// Get Company Contact Varibale By Contact ID
        /// </summary>
        IEnumerable<ScopeVariable> GetContactVariableByContactId(long contactId, int scope);

        /// <summary>
        /// Get Field Varibale By Company ID and By Sope Type
        /// </summary>
        IEnumerable<FieldVariable> GetFieldVariableByCompanyIdAndScope(long companyId, int scope);


        /// <summary>
        /// Save Smart Form
        /// </summary>
        long SaveSmartForm(SmartForm smartForm);

        /// <summary>
        /// Get Smart Forms
        /// </summary>
        SmartFormResponse GetSmartForms(SmartFormRequestModel request);

        /// <summary>
        /// Get Smart Form Detail By Smart Form Id
        /// </summary>
        IEnumerable<SmartFormDetail> GetSmartFormDetailBySmartFormId(long smartFormId);

        void DeleteCompanyBanner(long companyBannerId);

        /// <summary>
        /// Apply Theme
        /// </summary>
        void ApplyTheme(int themeId, string themeName, long companyId);

        /// <summary>
        /// Get Cms Tags For Cms Page Load Default page keywords
        /// </summary>
        string GetCmsTagForCmsPage();

        /// <summary>
        /// Get Campaign Detail By Campaign ID
        /// </summary>
        Campaign GetCampaignById(long campaignId);
        /// <summary>
        /// Base Data for Crm Screen (prospect/customer and suppliers)
        /// </summary>
        /// <returns></returns>
        CrmBaseResponse GetBaseDataForCrm();

        CompanyResponse GetCompanyByIdForCrm(long companyId);

        /// <summary>
        /// Save Cms Page
        /// </summary>
        long SaveSecondaryPage(CmsPage cmsPage);

        /// <summary>
        /// Delete Secondary Page
        /// </summary>
        void DeleteSecondaryPage(long pageId);

        /// <summary>
        /// Delete Field Variable
        /// </summary>
        void DeleteFieldVariable(long variableId);

        /// <summary>
        /// Save Imported Company Contact
        /// </summary>
        /// <param name="stagingImportCompanyContact"></param>
        /// <returns></returns>
        bool SaveImportedCompanyContact(IEnumerable<StagingImportCompanyContactAddress> stagingImportCompanyContact);

        bool SaveCRMImportedCompanyContact(IEnumerable<StagingImportCompanyContactAddress> stagingImportCompanyContact);

        void DeleteCrmCompanyPermanently(long companyId);

        /// <summary>
        /// Get System Variables
        /// </summary>
        FieldVariableResponse GetSystemVariables(FieldVariableRequestModel request);

        /// <summary>
        /// Add/Update Discount Voucher
        /// </summary>
        DiscountVoucher SaveDiscountVoucher(DiscountVoucher discountVoucher);

        /// <summary>
        /// Get Discount Voucher By Id
        /// </summary>
        DiscountVoucher GetDiscountVoucherById(long discountVoucherId);

        #region exportOrganisation

        bool ExportOrganisation(long OrganisationID, string RetailName, string RetailNameWOP, string CorporateName, string CorporateNameWOP);

        bool ImportOrganisation(long OrganisationId, string SubDomain, bool isCorpStore);


        bool ImportStore(long OrganisationId, string StoreName, string SubDomain);
        #endregion


    }
}