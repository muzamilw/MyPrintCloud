﻿using System;
using System.Collections.Generic;

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
        public int DefaultNominalCode { get; set; }
        public int DefaultMarkUpId { get; set; }
        public DateTime? AccountOpenDate { get; set; }
        public int AccountManagerId { get; set; }
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
        public byte[] Image { get; set; }
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
        public bool? isDisplaylBrokerBanners { get; set; }
        public bool? isBrokerCanLaminate { get; set; }
        public bool? isBrokerCanRoundCorner { get; set; }
        public bool? isBrokerCanDeliverSameDay { get; set; }
        public bool? isBrokerCanAcceptPaymentOnline { get; set; }
        public bool? isBrokerOrderApprovalRequired { get; set; }
        public string isBrokerPaymentRequired { get; set; }
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
        public bool? isDisplayBrokerSecondaryPages { get; set; }
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
        public bool? includeEmailBrokerArtworkOrderReport { get; set; }
        public bool? includeEmailBrokerArtworkOrderXML { get; set; }
        public bool? includeEmailBrokerArtworkOrderJobCard { get; set; }
        public bool? makeEmailBrokerArtworkOrderProductionReady { get; set; }
        public long? SalesAndOrderManagerId1 { get; set; }
        public long? SalesAndOrderManagerId2 { get; set; }
        public long? ProductionManagerId1 { get; set; }
        public long? ProductionManagerId2 { get; set; }
        public long? StockNotificationManagerId1 { get; set; }
        public long? StockNotificationManagerId2 { get; set; }
        public bool? IsDeliveryTaxAble { get; set; }
        public bool? IsDisplayDeliveryOnCheckout { get; set; }
        public long? DeliveryPickUpAddressId { get; set; }
        public long? BussinessAddressId { get; set; }

        #endregion

        #region Public List Properties

        public List<RaveReview> RaveReviews { get; set; }
        public List<CompanyCMYKColor> CompanyCmykColors { get; set; }
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

        public List<MediaLibrary> MediaLibraries { get; set; }

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
        /// <summary>
        /// Store Backgroud Image Image Source
        /// </summary>
        public string StoreBackgroudImageImageSource { get; set; }

        /// <summary>
        /// Store Backgroud Image File Name
        /// </summary>
        public string StoreBackgroudImageFileName { get; set; }

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
        #endregion
    }
}