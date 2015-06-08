using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Company Domain Model
    /// </summary>
    public class Company
    {
        public long CompanyId { get; set; }
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string CreditReference { get; set; }
        public double? CreditLimit { get; set; }
        public string Terms { get; set; }
        public long TypeId { get; set; }
        public int DefaultNominalCode { get; set; }
        public int DefaultMarkUpId { get; set; }
        public DateTime? AccountOpenDate { get; set; }
        public Guid? AccountManagerId { get; set; }
        public short Status { get; set; }
        public short IsCustomer { get; set; }
        public string Notes { get; set; }
        public DateTime? NotesLastUpdatedDate { get; set; }
        public int? NotesLastUpdatedBy { get; set; }
        public short AccountStatusId { get; set; }
        public short IsDisabled { get; set; }
        public int LockedBy { get; set; }
        public double AccountBalance { get; set; }
        public DateTime? CreationDate { get; set; }
        public string VATRegNumber { get; set; }
        public string VATRegReference { get; set; }
        public int FlagId { get; set; }
        public string PhoneNo { get; set; }
        public short? IsGeneral { get; set; }
        public int? SalesPerson { get; set; }
        public string Image { get; set; }
        public string WebAccessCode { get; set; }
        public bool? isArchived { get; set; }
        public bool? PayByPersonalCredeitCard { get; set; }
        public bool? PONumberRequired { get; set; }
        public bool? ShowPrices { get; set; }
        public string CarrierWebPath { get; set; }
        public string CarrierTrackingPath { get; set; }
        public string CorporateOrderingPolicy { get; set; }
        public bool? isDisplaySiteHeader { get; set; }
        public bool? isDisplayMenuBar { get; set; }
        public bool? isDisplayBanners { get; set; }
        public bool? isDisplayFeaturedProducts { get; set; }
        public bool? isDisplayPromotionalProducts { get; set; }
        public bool? isDisplayChooseUsIcons { get; set; }
        public bool? isDisplaySecondaryPages { get; set; }
        public bool? isDisplaySiteFooter { get; set; }
        public string RedirectWebstoreURL { get; set; }
        public int? defaultPalleteId { get; set; }
        public bool? isLaminate { get; set; }
        public bool? isRoundCorner { get; set; }
        public bool? isBrokerCanDeliverSameDay { get; set; }
        public bool? isAcceptPaymentOnline { get; set; }
        public bool? isOrderApprovalRequired { get; set; }
        public bool? isPaymentRequired { get; set; }
        public bool? isWhiteLabel { get; set; }
        public string TwitterURL { get; set; }
        public string FacebookURL { get; set; }
        public string LinkedinURL { get; set; }
        public string WebMasterTag { get; set; }
        public string WebAnalyticCode { get; set; }
        public int? isShowGoogleMap { get; set; }
        public bool? isTextWatermark { get; set; }
        public string WatermarkText { get; set; }
        public int? CoreCustomerId { get; set; }
        public string StoreBackgroundImage { get; set; }
        public int? PriceFlagId { get; set; }
        public bool? isIncludeVAT { get; set; }
        public bool? isAllowRegistrationFromWeb { get; set; }
        public string MarketingBriefRecipient { get; set; }
        public bool? isLoginFirstTime { get; set; }
        public string facebookAppId { get; set; }
        public string facebookAppKey { get; set; }
        public string twitterAppId { get; set; }
        public string twitterAppKey { get; set; }
        public bool? isStoreModePrivate { get; set; }
        public string CustomCSS { get; set; }
        public int? TaxPercentageId { get; set; }
        public string XeroAccessCode { get; set; }
        public bool? canUserPlaceOrderWithoutApproval { get; set; }
        public bool? CanUserEditProfile { get; set; }
        public long? OrganisationId { get; set; }
        public bool? includeEmailArtworkOrderReport { get; set; }
        public bool? includeEmailArtworkOrderXML { get; set; }
        public bool? includeEmailArtworkOrderJobCard { get; set; }
        public bool? makeEmailArtworkOrderProductionReady { get; set; }
        public Guid? SalesAndOrderManagerId1 { get; set; }
        public Guid? SalesAndOrderManagerId2 { get; set; }
        public Guid? ProductionManagerId1 { get; set; }
        public Guid? ProductionManagerId2 { get; set; }
        public Guid? StockNotificationManagerId1 { get; set; }
        public Guid? StockNotificationManagerId2 { get; set; }
        public bool? IsDeliveryTaxAble { get; set; }
        public bool? IsDisplayDeliveryOnCheckout { get; set; }
        public double? TaxRate { get; set; }
        public bool? IsDisplayDiscountVoucherCode { get; set; }
        public bool? IsDisplayCorporateBinding { get; set; }
        public long? CurrentThemeId { get; set; }

        /// <summary>
        /// Map Image Url
        /// </summary>
        public string MapImageUrl { get; set; }
        [NotMapped]
        public byte[] MapImageUrlSourceBytes
        {
            get
            {
                if (string.IsNullOrEmpty(MapImageUrl))
                {
                    return null;
                }

                int firtsAppearingCommaIndex = MapImageUrl.IndexOf(',');

                if (firtsAppearingCommaIndex < 0)
                {
                    return null;
                }

                if (MapImageUrl.Length < firtsAppearingCommaIndex + 1)
                {
                    return null;
                }

                string sourceSubString = MapImageUrl.Substring(firtsAppearingCommaIndex + 1);

                try
                {
                    return Convert.FromBase64String(sourceSubString.Trim('\0'));
                }
                catch (FormatException)
                {
                    return null;
                }
            }
        }

        public long? PickupAddressId { get; set; }

        /// <summary>
        /// Tax Label
        /// </summary>
        public string TaxLabel { get; set; }

        public long? StoreId { get; set; }

        public bool? isAddCropMarks { get; set; }

        public bool? isCalculateTaxByService { get; set; }

        public long? ActiveBannerSetId { get; set; }

        [NotMapped]
        public string ImageName { get; set; }

        public virtual ICollection<CompanyBannerSet> CompanyBannerSets { get; set; }

        public virtual ICollection<CompanyCMYKColor> CompanyCMYKColors { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<CmsPage> CmsPages { get; set; }
        [NotMapped]
        public virtual ICollection<CmsPage> SystemPages { get; set; }
        public virtual ICollection<CmsSkinPageWidget> CmsSkinPageWidgets { get; set; }
        public virtual ICollection<CompanyDomain> CompanyDomains { get; set; }
        public virtual ICollection<RaveReview> RaveReviews { get; set; }
        public virtual CompanyType CompanyType { get; set; }
        public virtual ICollection<CompanyContact> CompanyContacts { get; set; }
        public virtual ICollection<CompanyTerritory> CompanyTerritories { get; set; }
        public virtual ICollection<Estimate> Estimates { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        public virtual ICollection<Campaign> Campaigns { get; set; }
        public virtual ICollection<PaymentGateway> PaymentGateways { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<ColorPallete> ColorPalletes { get; set; }
        public virtual ICollection<StockItem> StockItems { get; set; }
        public virtual ICollection<CmsOffer> CmsOffers { get; set; }
        public virtual ICollection<MediaLibrary> MediaLibraries { get; set; }
        public virtual ICollection<CompanyCostCentre> CompanyCostCentres { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<SmartForm> SmartForms { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<FieldVariable> FieldVariables { get; set; }
        public virtual ICollection<TemplateColorStyle> TemplateColorStyles { get; set; }
        public virtual ICollection<DeliveryNote> DeliveryNotes { get; set; }
        public virtual ICollection<Inquiry> Inquiries { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }

        #region Additional Properties

        /// <summary>
        /// Default Sprite Source
        /// </summary>
        [NotMapped]
        public string DefaultSpriteSource { get; set; }
        /// <summary>
        /// User Defined Sprite Source
        /// </summary>
        [NotMapped]
        public string UserDefinedSpriteSource { get; set; }
        /// <summary>
        /// User Defined Sprite File Name
        /// </summary>
        [NotMapped]
        public string UserDefinedSpriteFileName { get; set; }

        /// <summary>
        /// Store Background File
        /// </summary>
        public string StoreBackgroundFile { get; set; }

        /// <summary>
        /// Logo Image Bytes
        /// </summary>
        public string ImageBytes { get; set; }

        /// <summary>
        /// Company Logo Source
        /// </summary>
        [NotMapped]
        public string CompanyLogoSource { get; set; }
        /// <summary>
        /// Company Logo Name
        /// </summary>
        [NotMapped]
        public string CompanyLogoName { get; set; }

        /// <summary>
        /// store work flow File Name
        /// </summary>
        [NotMapped]
         public string StoreWorkflowImage { get; set; }

        /// <summary>
        /// Scope Variables
        /// </summary>
        [NotMapped]
        public List<ScopeVariable> ScopeVariables { get; set; }

        /// <summary>
        /// File Source Bytes - byte[] representation of Base64 string FileSource
        /// </summary>
        [NotMapped]
        public byte[] StoreWorkFlowFileSourceBytes
        {
            get
            {
                if (string.IsNullOrEmpty(WatermarkText))
                {
                    return null;
                }

                int firtsAppearingCommaIndex = WatermarkText.IndexOf(',');

                if (firtsAppearingCommaIndex < 0)
                {
                    return null;
                }

                if (WatermarkText.Length < firtsAppearingCommaIndex + 1)
                {
                    return null;
                }

                string sourceSubString = WatermarkText.Substring(firtsAppearingCommaIndex + 1);

                try
                {
                    return Convert.FromBase64String(sourceSubString.Trim('\0'));
                }
                catch (FormatException)
                {
                    return null;
                }
            }
        }
        #endregion
    }
}
