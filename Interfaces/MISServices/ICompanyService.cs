using System.Collections.Generic;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.MISServices
{
    public interface ICompanyService
    {
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
        IEnumerable<CompanyContactVariable> GetContactVariableByContactId(long contactId);

        /// <summary>
        /// Get Field Varibale By Company ID
        /// </summary>
        IEnumerable<FieldVariable> GetFieldVariableByCompanyId(long companyId);


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

        #region exportOrganisation

        void ExportOrganisation(long OrganisationID);

        void ImportOrganisation(long OrganisationId, string ZipPath);
        #endregion
    }
}
