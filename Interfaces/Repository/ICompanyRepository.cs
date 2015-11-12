using System;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using System.Collections.Generic;

namespace MPC.Interfaces.Repository
{
    public interface ICompanyRepository : IBaseRepository<Company, long>
    {
      
        CompanyResponse GetCompanyById(long companyId);
        /// <summary>
        /// USer count in last few days
        /// </summary>
        int UserCount(long? storeId, int numberOfDays);
        long GetStoreIdFromDomain(string domain);
        CompanyResponse SearchCompanies(CompanyRequestModel request);
        Guid? GetStoreJobManagerId(long storeId);
        Company GetCustomer(int CompanyId);
        /// <summary>
        /// Get Suppliers For Inventories
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        SupplierSearchResponseForInventory GetSuppliersForInventories(SupplierRequestModelForInventory request);

        /// <summary>
        /// Get Store By Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        Company GetStoreById(long companyId);
        Company GetStoreReceiptPage(long companyId);
        long CreateCustomer(string CompanyName, bool isEmailSubscriber, bool isNewsLetterSubscriber, CompanyTypes customerType, string RegWithSocialMedia, long OrganisationId,long StoreId, CompanyContact contact = null);
        /// <summary>
        /// Get Company Price Flag id for Price Matrix in webstore
        /// </summary>
        int? GetPriceFlagIdByCompany(long CompanyId);

        /// <summary>
        /// Get All Suppliers For Current Organisation
        /// </summary>
        IEnumerable<Company> GetAllSuppliers();

        string SystemWeight(long OrganisationID);

        string SystemLength(long OrganisationID);
        bool UpdateCompanyName(Company Instance);
        Company GetStoreByStoreId(long companyId);

        ExportSets ExportRetailCompany(long CompanyId);
        ExportSets ExportCorporateCompany(long CompanyId);

        long GetCompanyByName(long OID, string Name);
        /// <summary>
        /// Get Company By Is Customer Type
        /// </summary>
        CompanySearchResponseForCalendar GetByIsCustomerType(CompanyRequestModelForCalendar request);
        /// <summary>
        /// Count of live stores
        /// </summary>
        int LiveStoresCountForDashboard();

        CompanyResponse SearchCompaniesForSupplier(CompanyRequestModel request);

        CompanyResponse SearchCompaniesForCustomer(CompanyRequestModel request);
        CompanyResponse SearchCompaniesForCustomerOnDashboard(CompanyRequestModel request);

        Company GetCompanyByCompanyID(long CompanyID);

        bool InsertStore(long OID, ExportOrganisation objExpCorporate, ExportOrganisation objExpRetail, ExportOrganisation objExpCorporateWOP,ExportOrganisation objExpRetailWOP,string StoreName, ExportSets Sets,string SubDomain,string status);
        ExportSets ExportCorporateCompanyWithoutProducts(long CompanyId);

        ExportSets ExportRetailCompanyWithoutProducts(long CompanyId);

        void DeleteStoryBySP(long StoreID);
        IEnumerable<Company> GetAllRetailAndCorporateStores();
        CompanyResponse GetCompanyByIdForCrm(long companyId);
        double? GetTaxRateByStoreId(long storeId);

        List<Company> GetSupplierByOrganisationid(long OID);

       // Company GetCompanyByCompanyIDforArtwork(long CompanyID);

        string GetSupplierNameByID(int CID);

        /// <summary>
        /// Check web access code exists
        /// </summary>
        /// <param name="subscriptionCode"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Company isValidWebAccessCode(string WebAccessCode, long OrganisationId);

        List<StoresListResponse> GetStoresNameByOrganisationId();
        IEnumerable<Company> GetAllRetailStores();
        // ReSharper disable once InconsistentNaming
        void DeleteCrmCompanyBySP(long storeId);

        void UpdateLiveStores(long organisationId, int storesCount);
        int GetLiveStoresCount(long organisationId);
        bool IsStoreLive(long storeId);
        List<usp_GetLiveStores_Result> GetLiveStoresList();

        void CopyProductByStore(long NewStoreId, long OldStoreId);

        Company LoadCompanyWithItems(long StoreId);

        void InsertItem(Company objCompany, long OldCompanyId);

        void InsertProductCategories(Company objCompany, long OldCompanyId);

        void InsertProductCategoryItems(Company NewCompany, Company OldCompany);

        void SetTerritoryIdAddress(Company objCompany, long OldCompanyId);
        long GetStoreIdByAccessCode(string sWebAccessCode);

        RealEstateVariableIconsListViewResponse GetCompanyVariableIcons(CompanyVariableIconRequestModel request);

        void DeleteCompanyVariableIcon(long iconId);

        void SaveCompanyVariableIcon(CompanyVariableIconRequestModel request);

        void SaveSystemVariableExtension(long oldCompanyId, long NewCompanyid);

        bool IsDuplicateWebAccessCode(string webCode, long? companyId);

        bool SaveUserActionLog(string Comment, long CompanyId);

        ExportStore ExportStore(long CompanyId,long OrganisationId);

        bool InsertStoreZip(ExportStore ObjExportStore, long OrganisationId, string SubDomain);

        long GetOrganisationIdByCompanyId(long companyid);
    }
}
