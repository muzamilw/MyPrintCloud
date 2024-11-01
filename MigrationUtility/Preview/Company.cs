//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MigrationUtility.Preview
{
    using System;
    using System.Collections.Generic;
    
    public partial class Company
    {
        public Company()
        {
            this.Activities = new HashSet<Activity>();
            this.Addresses = new HashSet<Address>();
            this.Campaigns = new HashSet<Campaign>();
            this.CmsOffers = new HashSet<CmsOffer>();
            this.CmsPages = new HashSet<CmsPage>();
            this.CmsSkinPageWidgets = new HashSet<CmsSkinPageWidget>();
            this.ColorPalletes = new HashSet<ColorPallete>();
            this.CompanyCMYKColors = new HashSet<CompanyCMYKColor>();
            this.CompanyCostCentres = new HashSet<CompanyCostCentre>();
            this.CompanyDomains = new HashSet<CompanyDomain>();
            this.CompanyBannerSets = new HashSet<CompanyBannerSet>();
            this.Items = new HashSet<Item>();
            this.MediaLibraries = new HashSet<MediaLibrary>();
            this.PaymentGateways = new HashSet<PaymentGateway>();
            this.RaveReviews = new HashSet<RaveReview>();
            this.StockItems = new HashSet<StockItem>();
            this.CompanyContacts = new HashSet<CompanyContact>();
            this.CompanyTerritories = new HashSet<CompanyTerritory>();
            this.Estimates = new HashSet<Estimate>();
            this.ProductCategories = new HashSet<ProductCategory>();
        }
    
        public long CompanyId { get; set; }
        public Nullable<long> StoreId { get; set; }
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string CreditReference { get; set; }
        public Nullable<double> CreditLimit { get; set; }
        public string Terms { get; set; }
        public long TypeId { get; set; }
        public int DefaultNominalCode { get; set; }
        public int DefaultMarkUpId { get; set; }
        public Nullable<System.DateTime> AccountOpenDate { get; set; }
        public Nullable<System.Guid> AccountManagerId { get; set; }
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
        public Nullable<bool> isLaminate { get; set; }
        public Nullable<bool> isRoundCorner { get; set; }
        public Nullable<bool> isBrokerCanDeliverSameDay { get; set; }
        public Nullable<bool> isAcceptPaymentOnline { get; set; }
        public Nullable<bool> isOrderApprovalRequired { get; set; }
        public Nullable<bool> isPaymentRequired { get; set; }
        public Nullable<bool> isWhiteLabel { get; set; }
        public string TwitterURL { get; set; }
        public string FacebookURL { get; set; }
        public string LinkedinURL { get; set; }
        public string WebMasterTag { get; set; }
        public string WebAnalyticCode { get; set; }
        public Nullable<int> isShowGoogleMap { get; set; }
        public Nullable<bool> isTextWatermark { get; set; }
        public string WatermarkText { get; set; }
        public Nullable<int> CoreCustomerId { get; set; }
        public string StoreBackgroundImage { get; set; }
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
        public Nullable<bool> includeEmailArtworkOrderReport { get; set; }
        public Nullable<bool> includeEmailArtworkOrderXML { get; set; }
        public Nullable<bool> includeEmailArtworkOrderJobCard { get; set; }
        public Nullable<bool> makeEmailArtworkOrderProductionReady { get; set; }
        public Nullable<System.Guid> SalesAndOrderManagerId1 { get; set; }
        public Nullable<System.Guid> SalesAndOrderManagerId2 { get; set; }
        public Nullable<System.Guid> ProductionManagerId1 { get; set; }
        public Nullable<System.Guid> ProductionManagerId2 { get; set; }
        public Nullable<System.Guid> StockNotificationManagerId1 { get; set; }
        public Nullable<System.Guid> StockNotificationManagerId2 { get; set; }
        public Nullable<bool> IsDeliveryTaxAble { get; set; }
        public Nullable<bool> IsDisplayDeliveryOnCheckout { get; set; }
        public Nullable<double> TaxRate { get; set; }
        public Nullable<bool> IsDisplayDiscountVoucherCode { get; set; }
        public Nullable<bool> IsDisplayCorporateBinding { get; set; }
        public string MapImageURL { get; set; }
        public Nullable<long> PickupAddressId { get; set; }
        public string TaxLabel { get; set; }
        public Nullable<bool> isAddCropMarks { get; set; }
        public Nullable<bool> isCalculateTaxByService { get; set; }
    
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Campaign> Campaigns { get; set; }
        public virtual ICollection<CmsOffer> CmsOffers { get; set; }
        public virtual ICollection<CmsPage> CmsPages { get; set; }
        public virtual ICollection<CmsSkinPageWidget> CmsSkinPageWidgets { get; set; }
        public virtual ICollection<ColorPallete> ColorPalletes { get; set; }
        public virtual ICollection<CompanyCMYKColor> CompanyCMYKColors { get; set; }
        public virtual ICollection<CompanyCostCentre> CompanyCostCentres { get; set; }
        public virtual ICollection<CompanyDomain> CompanyDomains { get; set; }
        public virtual ICollection<CompanyBannerSet> CompanyBannerSets { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<MediaLibrary> MediaLibraries { get; set; }
        public virtual ICollection<PaymentGateway> PaymentGateways { get; set; }
        public virtual ICollection<RaveReview> RaveReviews { get; set; }
        public virtual ICollection<StockItem> StockItems { get; set; }
        public virtual CompanyType CompanyType { get; set; }
        public virtual ICollection<CompanyContact> CompanyContacts { get; set; }
        public virtual ICollection<CompanyTerritory> CompanyTerritories { get; set; }
        public virtual ICollection<Estimate> Estimates { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
