using System;
namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Staging Import Company Contact Address Domain Model
    /// </summary>
    public class StagingImportCompanyContactAddress
    {
        public long StagingId { get; set; }
        public string CompanyName { get; set; }
        public Nullable<long> CompanyId { get; set; }
        public Nullable<long> AddressId { get; set; }
        public string AddressName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public Nullable<long> StateId { get; set; }
        public string Country { get; set; }
        public Nullable<long> CountryId { get; set; }
        public string Postcode { get; set; }
        public Nullable<long> TerritoryId { get; set; }
        public string TerritoryName { get; set; }
        public string AddressPhone { get; set; }
        public string AddressFax { get; set; }
        public Nullable<long> ContactId { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string JobTitle { get; set; }
        public string Email { get; set; }
        public string password { get; set; }
        public string Mobile { get; set; }
        public Nullable<long> RoleId { get; set; }
        public string ContactPhone { get; set; }
        public string ContactFax { get; set; }
        public string AddInfo1 { get; set; }
        public string AddInfo2 { get; set; }
        public string AddInfo3 { get; set; }
        public string AddInfo4 { get; set; }
        public string AddInfo5 { get; set; }
        public Nullable<long> OrganisationId { get; set; }
        public string SkypeId { get; set; }
        public string LinkedInUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public Nullable<bool> CanEditProfile { get; set; }
        public Nullable<bool> CanPlaceOrderWithoutApproval { get; set; }
        public Nullable<bool> CanPlaceDirectOrder { get; set; }
        public Nullable<bool> CanPayByPersonalCreditCard { get; set; }
        public Nullable<bool> CanSeePrices { get; set; }
        public Nullable<bool> HasWebAccess { get; set; }
        public Nullable<bool> CanPlaceOrder { get; set; }
        public string DirectLine { get; set; }
        public string CorporateUnit { get; set; }
        public string TradingName { get; set; }
        public string BPayCRN { get; set; }
        public string ACN { get; set; }
        public string ContractorName { get; set; }
        public string ABN { get; set; }
        public string Notes { get; set; }
        public Nullable<decimal> CreditLimit { get; set; }
        public Nullable<bool> IsNewsLetterSubscription { get; set; }
        public Nullable<bool> IsEmailSubscription { get; set; }
        public Nullable<bool> IsDefaultContact { get; set; }
        public string POAddress { get; set; }
        public string StoreName { get; set; }
        public string WebAccessCode { get; set; }
    }
}
