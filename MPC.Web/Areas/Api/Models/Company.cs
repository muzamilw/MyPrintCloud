using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.Models
{
    public class Company
    {
        #region Public

        #region Public Properties

        public long CompanyId { get; set; }
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string CreditReference { get; set; }
        public double? CreditLimit { get; set; }
        public string Terms { get; set; }
        public long TypeId { get; set; }
        public long? CurrentThemeId { get; set; }
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
        public string StoreImagePath { get; set; }
        public bool? isCalculateTaxByService { get; set; }
        public string ImageName { get; set; }
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
        public bool? isCanLaminate { get; set; }
        public bool? isCanRoundCorner { get; set; }
        public bool? isBrokerCanDeliverSameDay { get; set; }
        public bool? isCanAcceptPaymentOnline { get; set; }
        public bool? isOrderApprovalRequired { get; set; }
        public string isPaymentRequired { get; set; }
        public bool? isWhiteLabel { get; set; }
        public string TwitterURL { get; set; }
        public string FacebookURL { get; set; }
        public string LinkedinURL { get; set; }
        public string WebMasterTag { get; set; }
        public string WebAnalyticCode { get; set; }
        public int? isShowGoogleMap { get; set; }
        public string isTextWatermark { get; set; }
        public string WatermarkText { get; set; }
        public int? CoreCustomerId { get; set; }
        public string StoreBackgroundImage { get; set; }
        public int? PriceFlagId { get; set; }
        public string isIncludeVAT { get; set; }
        public bool? isAllowRegistrationFromWeb { get; set; }
        public string MarketingBriefRecipient { get; set; }
        public bool? isLoginFirstTime { get; set; }
        public string facebookAppId { get; set; }
        public string facebookAppKey { get; set; }
        public string twitterAppId { get; set; }
        public string twitterAppKey { get; set; }
        //public bool? isStoreModePrivate { get; set; }
        public string isStoreModePrivate { get; set; }
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
        public bool? IsDisplayDiscountVoucherCode { get; set; }

        public long? PickupAddressId { get; set; }
        public long? BussinessAddressId { get; set; }
        public int CompanyContactCount { get; set; }
        public int CompanyAddressesCount { get; set; }
        public long? ActiveBannerSetId { get; set; }
        public long? StoreId { get; set; }
        /// <summary>
        /// Map Image Url
        /// </summary>
        public string MapImageUrl { get; set; }
        public string DefaultContactEmail { get; set; }
        public string DefaultContact { get; set; }

        public byte[] MapImageS2CBytes { get; set; }

        public bool? IsEnableDataAsset { get; set; }
        /// <summary>
        /// Default Sprite Image Source
        /// </summary>
        public string MapImageSource
        {
            get
            {
                if (MapImageS2CBytes == null)
                {
                    return string.Empty;
                }

                string base64 = Convert.ToBase64String(MapImageS2CBytes);
                return string.Format("data:{0};base64,{1}", "image/jpg", base64);
            }
        }

        /// <summary>
        /// Tax Label
        /// </summary>
        public string TaxLabel { get; set; }
        public double? TaxRate { get; set; }

        public List<ScopeVariable> ScopeVariables { get; set; }
        public bool? isStoreLive { get; set; }
        public bool? CanUserUpdateAddress { get; set; }
        public bool IsClickReached { get; set; }
        public bool? IsRegisterAccessWebStore { get; set; }
        public bool? IsRegisterPlaceOrder { get; set; }
        public bool? IsRegisterPayOnlyByCreditCard { get; set; }
        public bool? IsRegisterPlaceDirectOrder { get; set; }
        public bool? IsRegisterPlaceOrderWithoutApproval { get; set; }
        public bool? IsAllowRequestaQuote { get; set; }
        public string RobotText { get; set; }
        public string SiteMap { get; set; }
        #endregion

        #region Public List Properties

        public List<RaveReview> RaveReviews { get; set; }
        public List<TemplateColorStyle> TemplateColorStyles { get; set; }
        public ICollection<PaymentGateway> PaymentGateways { get; set; }
        public virtual CompanyType CompanyType { get; set; }
        public List<CompanyContact> CompanyContacts { get; set; }
        public List<CompanyBannerSet> CompanyBannerSets { get; set; }
        public List<CmsPageForListView> CmsPages { get; set; }
        public List<PageCategory> PageCategories { get; set; }
        public List<Campaign> Campaigns { get; set; }
        public List<ProductCategoryListViewModel> ProductCategoriesListView { get; set; }
        public List<CmsPageDropDown> CmsPagesDropDownList { get; set; }
        /// <summary>
        /// Cms Page With Widget List 
        /// </summary>
        public List<CmsPageWithWidgetList> CmsPageWithWidgetList { get; set; }
        /// <summary>
        /// Have Items/Products List
        /// </summary>
        public ItemSearchResponse ItemsResponse { get; set; }

        /// <summary>
        /// Color Pallete
        /// </summary>
        public List<ColorPallete> ColorPalletes { get; set; }
        
        /// <summary>
        /// Cms Offers
        /// </summary>
        public List<CmsOffer> CmsOffers { get; set; }
        public List<CompanyDomain> CompanyDomains { get; set; }

        public List<MediaLibrary> MediaLibraries { get; set; }
        public List<CostCentreDropDown> CompanyCostCentres { get; set; }
        public List<FieldVariable> FieldVariables { get; set; }
        public List<SmartForm> SmartForms { get; set; }

        #region Campaigns
        public List<Campaign> NewAddedCampaigns { get; set; }
        public List<Campaign> EdittedCampaigns { get; set; }
        public List<Campaign> DeletedCampaigns { get; set; }
        #endregion

        #region CMS Pages

        public List<CmsPage> NewAddedCmsPages { get; set; }
        public List<CmsPage> EditCmsPages { get; set; }
        public List<CmsPage> DeletedCmsPages { get; set; }

        #endregion

        #region Company Territories

        public ICollection<CompanyTerritory> CompanyTerritories { get; set; }
        public ICollection<CompanyTerritory> NewAddedCompanyTerritories { get; set; }
        // Maintaining List for POST call to determine new Added List Of Territories
        public ICollection<CompanyTerritory> EdittedCompanyTerritories { get; set; }
        public ICollection<CompanyTerritory> DeletedCompanyTerritories { get; set; }

        #endregion

        #region Addresses

        public List<Address> Addresses { get; set; }
        public ICollection<Address> NewAddedAddresses { get; set; }
        // Maintaining List for POST call to determine new Added List Of Addresses
        public ICollection<Address> EdittedAddresses { get; set; }
        public ICollection<Address> DeletedAddresses { get; set; }

        #endregion

        #region Product Categories

        public ICollection<ProductCategory> NewProductCategories { get; set; }
        // Maintaining List for POST call to determine new Added List Of Addresses
        public ICollection<ProductCategory> EdittedProductCategories { get; set; }
        public ICollection<ProductCategory> DeletedProductCategories { get; set; }

        #endregion

        #region Company Contacts

        public ICollection<CompanyContact> NewAddedCompanyContacts { get; set; }
        public ICollection<CompanyContact> EdittedCompanyContacts { get; set; }
        public ICollection<CompanyContact> DeletedCompanyContacts { get; set; }

        #endregion

        #region Products

        public ICollection<Item> NewAddedProducts { get; set; }
        public ICollection<Item> EdittedProducts { get; set; }
        public ICollection<Item> Deletedproducts { get; set; }

        #endregion


        #endregion

        #region Public Image Source
        public string ImageBytes { get; set; }

        public byte[] Image { get; set; }

        /// <summary>
        /// Image Source
        /// </summary>
        public string ImageSource
        {
            get
            {
                if (Image == null)
                {
                    return string.Empty;
                }

                string base64 = Convert.ToBase64String(Image);
                return string.Format("data:{0};base64,{1}", "image/jpg", base64);
            }
        }


        #endregion



        #region Backgroud Image
        public string StoreBackgroundFile { get; set; }

        /// <summary>
        /// Store Backgroud Image Bytes
        /// </summary>
        public byte[] StoreBackgroudImage { get; set; }

        /// <summary>
        /// Store Backgroud Image Source
        /// </summary>
        public string StoreBackgroudImageSource
        {
            get
            {
                if (StoreBackgroudImage == null)
                {
                    return string.Empty;
                }

                string base64 = Convert.ToBase64String(StoreBackgroudImage);
                return string.Format("data:{0};base64,{1}", "image/jpg", base64);
            }
        }

        #endregion

        #region Sprite Image

        /// <summary>
        /// Default Sprite Source
        /// </summary>
        public string DefaultSpriteSource { get; set; }
        /// <summary>
        /// User Defined Sprite Source
        /// </summary>
        public string UserDefinedSpriteSource { get; set; }
        /// <summary>
        /// User Defined Sprite File Name
        /// </summary>
        public string UserDefinedSpriteFileName { get; set; }

        /// <summary>
        /// Default Sprite Image
        /// </summary>
        public byte[] DefaultSpriteImage { get; set; }

        /// <summary>
        /// Default Sprite Image Source
        /// </summary>
        public string DefaultSpriteImageSource
        {
            get
            {
                if (DefaultSpriteImage == null)
                {
                    return string.Empty;
                }

                string base64 = Convert.ToBase64String(DefaultSpriteImage);
                return string.Format("data:{0};base64,{1}", "image/jpg", base64);
            }
        }

        /// <summary>
        /// User Defined Sprite Image
        /// </summary>
        public byte[] UserDefinedSpriteImage { get; set; }

        /// <summary>
        /// User Defined Sprite Image Source
        /// </summary>
        public string UserDefinedSpriteImageSource
        {
            get
            {
                if (UserDefinedSpriteImage == null)
                {
                    return string.Empty;
                }

                string base64 = Convert.ToBase64String(UserDefinedSpriteImage);
                return string.Format("data:{0};base64,{1}", "image/jpg", base64);
            }
        }
        #endregion

        #region Supplier Image
        /// <summary>
        /// Company Logo Source
        /// </summary>
        public string CompanyLogoSource { get; set; }
        /// <summary>
        /// Company Logo Name
        /// </summary>
        public string CompanyLogoName { get; set; }
        #endregion

        #region Store Overflow Image

        // client to server
        public string StoreWorkflowImageBytes { get; set; }

        // client to server
        public string StoreWorkflowImage { get; set; }

        public byte[] WorkflowS2CBytes { get; set; }

        /// <summary>
        /// Store Backgroud Image Source
        /// </summary>
        public string WorkflowS2CBytesConverter
        {
            get
            {
                if (WorkflowS2CBytes == null)
                {
                    return string.Empty;
                }

                string base64 = Convert.ToBase64String(WorkflowS2CBytes);
                return string.Format("data:{0};base64,{1}", "image/jpg", base64);
            }
        }

        #endregion


        #endregion
    }
}