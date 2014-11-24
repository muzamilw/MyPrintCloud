using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class Company
    {
        public long CompanyId { get; set; }
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string CreditReference { get; set; }
        public Nullable<double> CreditLimit { get; set; }
        public string Terms { get; set; }
        public int TypeId { get; set; }
        public int DefaultNominalCode { get; set; }
        public int DefaultMarkUpId { get; set; }
        public Nullable<System.DateTime> AccountOpenDate { get; set; }
        public int AccountManagerId { get; set; }
        public short Status { get; set; }
        public short IsCustomer { get; set; }
        public string Notes { get; set; }
        public Nullable<System.DateTime> NotesLastUpdatedDate { get; set; }
        public Nullable<int> NotesLastUpdatedBy { get; set; }
        public short AccountStatusId { get; set; }
        public short IsDisabled { get; set; }
        public int LockedBy { get; set; }
        public double AccountBalance { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string VATRegNumber { get; set; }
        public string VATRegReference { get; set; }
        public int FlagId { get; set; }
        public string PhoneNo { get; set; }
        public Nullable<short> IsGeneral { get; set; }
        public Nullable<int> SalesPerson { get; set; }
        public string Image { get; set; }
        public string WebAccessCode { get; set; }
        public Nullable<bool> isArchived { get; set; }
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
        public Nullable<int> defaultPalleteId { get; set; }
        public Nullable<bool> isDisplaylBrokerBanners { get; set; }
        public Nullable<bool> isBrokerCanLaminate { get; set; }
        public Nullable<bool> isBrokerCanRoundCorner { get; set; }
        public Nullable<bool> isBrokerCanDeliverSameDay { get; set; }
        public Nullable<bool> isBrokerCanAcceptPaymentOnline { get; set; }
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
        public Nullable<int> CoreCustomerId { get; set; }
        public string StoreBackgroundImage { get; set; }
        public Nullable<bool> isDisplayBrokerSecondaryPages { get; set; }
        public Nullable<int> PriceFlagId { get; set; }
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
        public Nullable<int> TaxPercentageId { get; set; }
        public string XeroAccessCode { get; set; }
        public Nullable<bool> canUserPlaceOrderWithoutApproval { get; set; }
        public Nullable<bool> CanUserEditProfile { get; set; }
        public Nullable<long> OrganisationId { get; set; }
        public Nullable<bool> includeEmailBrokerArtworkOrderReport { get; set; }
        public Nullable<bool> includeEmailBrokerArtworkOrderXML { get; set; }
        public Nullable<bool> includeEmailBrokerArtworkOrderJobCard { get; set; }
        public Nullable<bool> makeEmailBrokerArtworkOrderProductionReady { get; set; }
        public Nullable<long> SalesAndOrderManagerId1 { get; set; }
        public Nullable<long> SalesAndOrderManagerId2 { get; set; }
        public Nullable<long> ProductionManagerId1 { get; set; }
        public Nullable<long> ProductionManagerId2 { get; set; }
        public Nullable<long> StockNotificationManagerId1 { get; set; }
        public Nullable<long> StockNotificationManagerId2 { get; set; }
        public Nullable<bool> IsDeliveryTaxAble { get; set; }
        public Nullable<bool> IsDisplayDeliveryOnCheckout { get; set; }
        public Nullable<long> DeliveryPickUpAddressId { get; set; }
        public virtual ICollection<CompanyDomain> CompanyDomains { get; set; }
        public virtual ICollection<CmsSkinPageWidget> CmsSkinPageWidgets { get; set; }
       public virtual ICollection<ProductCategory> ProductCategories { get; set; }

        //public virtual ICollection<Address> Addresses { get; set; }
      
        //public virtual CompanyType CompanyType { get; set; }
        //public virtual ICollection<CompanyContact> CompanyContacts { get; set; }
        //public virtual ICollection<CompanyTerritory> CompanyTerritories { get; set; }
        //public virtual ICollection<Estimate> Estimates { get; set; }
        //public virtual ICollection<tbl_PC_PostCodesBrokers> tbl_PC_PostCodesBrokers { get; set; }
        //public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
