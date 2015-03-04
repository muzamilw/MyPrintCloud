//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MigrationUtility
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_contactcompanies
    {
        public tbl_contactcompanies()
        {
            this.tbl_addresses = new HashSet<tbl_addresses>();
            this.tbl_contacts = new HashSet<tbl_contacts>();
            this.tbl_ContactCompanyTerritories = new HashSet<tbl_ContactCompanyTerritories>();
            this.tbl_estimates = new HashSet<tbl_estimates>();
            this.tbl_PC_PostCodesBrokers = new HashSet<tbl_PC_PostCodesBrokers>();
            this.tbl_ProductCategory = new HashSet<tbl_ProductCategory>();
        }
    
        public int ContactCompanyID { get; set; }
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string CreditReference { get; set; }
        public Nullable<double> CreditLimit { get; set; }
        public string Terms { get; set; }
        public int TypeID { get; set; }
        public int DefaultNominalCode { get; set; }
        public short DefaultTill { get; set; }
        public int DefaultMarkUpID { get; set; }
        public Nullable<System.DateTime> AccountOpenDate { get; set; }
        public int AccountManagerID { get; set; }
        public short Status { get; set; }
        public short IsCustomer { get; set; }
        public string Notes { get; set; }
        public string ISBN { get; set; }
        public Nullable<System.DateTime> NotesLastUpdatedDate { get; set; }
        public Nullable<int> NotesLastUpdatedBy { get; set; }
        public string AccountOnHandDesc { get; set; }
        public short AccountStatusID { get; set; }
        public short IsDisabled { get; set; }
        public int LockedBy { get; set; }
        public double AccountBalance { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string VATRegNumber { get; set; }
        public Nullable<short> IsParaentCompany { get; set; }
        public Nullable<int> ParaentCompanyID { get; set; }
        public Nullable<int> SystemSiteID { get; set; }
        public string VATRegReference { get; set; }
        public int FlagID { get; set; }
        public short IsEmailSubscription { get; set; }
        public short IsMailSubscription { get; set; }
        public short IsEmailFormat { get; set; }
        public string HomeContact { get; set; }
        public string AbountUs { get; set; }
        public string ContactUs { get; set; }
        public Nullable<short> IsGeneral { get; set; }
        public string WebAccessAdminUserName { get; set; }
        public string WebAccessAdminPassword { get; set; }
        public string WebAccessAdminPasswordHint { get; set; }
        public Nullable<bool> IsShowFinishedGoodPrices { get; set; }
        public Nullable<bool> IsReed { get; set; }
        public Nullable<int> DepartmentID { get; set; }
        public Nullable<int> SalesPerson { get; set; }
        public string Image { get; set; }
        public string WebAccessCode { get; set; }
        public Nullable<bool> isArchived { get; set; }
        public Nullable<bool> CanCreateShoppingAddress { get; set; }
        public Nullable<bool> PayByPersonalCredeitCard { get; set; }
        public Nullable<bool> PONumberRequired { get; set; }
        public Nullable<bool> ShowPrices { get; set; }
        public string CarrierWebPath { get; set; }
        public string CarrierTrackingPath { get; set; }
        public string CorporateOrderingPolicy { get; set; }
        public Nullable<bool> isDisplaySiteHeader { get; set; }
        public Nullable<bool> isDisplayMenuBar { get; set; }
        public Nullable<bool> isDisplayBanners { get; set; }
        public Nullable<bool> isDisplayFeaturedProducts { get; set; }
        public Nullable<bool> isDisplayPromotionalProducts { get; set; }
        public Nullable<bool> isDisplayChooseUsIcons { get; set; }
        public Nullable<bool> isDisplaySecondaryPages { get; set; }
        public Nullable<bool> isDisplaySiteFooter { get; set; }
        public string RedirectWebstoreURL { get; set; }
        public Nullable<int> defaultPalleteID { get; set; }
        public Nullable<bool> isDisplaylBrokerBanners { get; set; }
        public Nullable<bool> isBrokerCanLaminate { get; set; }
        public Nullable<bool> isBrokerCanRoundCorner { get; set; }
        public Nullable<bool> isBrokerCanDeliverSameDay { get; set; }
        public Nullable<bool> isBrokerCanAcceptPaymentOnline { get; set; }
        public Nullable<int> BrokerContactCompanyID { get; set; }
        public Nullable<bool> isBrokerOrderApprovalRequired { get; set; }
        public Nullable<bool> isBrokerPaymentRequired { get; set; }
        public Nullable<bool> isWhiteLabel { get; set; }
        public string TwitterURL { get; set; }
        public string FacebookURL { get; set; }
        public string LinkedinURL { get; set; }
        public string WebMasterTag { get; set; }
        public string WebAnalyticCode { get; set; }
        public Nullable<bool> isShowGoogleMap { get; set; }
        public Nullable<bool> isTextWatermark { get; set; }
        public string WatermarkText { get; set; }
        public Nullable<int> CoreCustomerID { get; set; }
        public string StoreBackgroundImage { get; set; }
        public Nullable<bool> isDisplayBrokerSecondaryPages { get; set; }
        public Nullable<int> PriceFlagID { get; set; }
        public Nullable<bool> isIncludeVAT { get; set; }
        public Nullable<bool> isAllowRegistrationFromWeb { get; set; }
        public string MarketingBriefRecipient { get; set; }
        public Nullable<bool> isLoginFirstTime { get; set; }
        public string facebookAppId { get; set; }
        public string facebookAppKey { get; set; }
        public string twitterAppId { get; set; }
        public string twitterAppKey { get; set; }
        public Nullable<bool> isStoreModePrivate { get; set; }
        public string CustomCSS { get; set; }
        public Nullable<int> TaxPercentageID { get; set; }
        public Nullable<bool> canUserPlaceOrderWithoutApproval { get; set; }
        public string XeroAccessCode { get; set; }
        public Nullable<bool> CanUserEditProfile { get; set; }
    
        public virtual ICollection<tbl_addresses> tbl_addresses { get; set; }
        public virtual tbl_contactcompanytypes tbl_contactcompanytypes { get; set; }
        public virtual ICollection<tbl_contacts> tbl_contacts { get; set; }
        public virtual ICollection<tbl_ContactCompanyTerritories> tbl_ContactCompanyTerritories { get; set; }
        public virtual ICollection<tbl_estimates> tbl_estimates { get; set; }
        public virtual ICollection<tbl_PC_PostCodesBrokers> tbl_PC_PostCodesBrokers { get; set; }
        public virtual ICollection<tbl_ProductCategory> tbl_ProductCategory { get; set; }
    }
}