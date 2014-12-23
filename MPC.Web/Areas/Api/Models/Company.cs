using System;
using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.Models
{
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
        public bool? isBrokerPaymentRequired { get; set; }
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
        public bool? isDisplayBrokerSecondaryPages { get; set; }
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
        public List<RaveReview> RaveReviews { get; set; }
        public List<CompanyCMYKColor> CompanyCmykColors { get; set; }
        public ICollection<CompanyTerritory> CompanyTerritories { get; set; }
        public ICollection<CompanyTerritory> NewAddedCompanyTerritories { get; set; }// Maintaining List for POST call to determine new Added List Of Territories
        public ICollection<CompanyTerritory> EdittedCompanyTerritories { get; set; }
        public ICollection<CompanyTerritory> DeletedCompanyTerritories { get; set; }
        public ICollection<Address> NewAddedAddresses { get; set; }// Maintaining List for POST call to determine new Added List Of Addresses
        public ICollection<Address> EdittedAddresses { get; set; }
        public ICollection<Address> DeletedAddresses { get; set; }
        public ICollection<CompanyContact> NewAddedCompanyContacts { get; set; }
        public ICollection<CompanyContact> EdittedCompanyContacts { get; set; }
        public ICollection<CompanyContact> DeletedCompanyContacts { get; set; }
        public ICollection<PaymentGateway> PaymentGateways { get; set; }
        public virtual CompanyType CompanyType { get; set; }
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
        public List<Address> Addresses { get; set; }
        public List<CompanyContact> CompanyContacts { get; set; }
        public List<CompanyBannerSet> CompanyBannerSets { get; set; }
        public List<CmsPageForListView> CmsPages { get; set; }

        public List<CmsPage> NewAddedCmsPages { get; set; }
        public List<CmsPage> EditCmsPages { get; set; }
        public List<CmsPage> DeletedCmsPages { get; set; }
        public List<PageCategory> PageCategories { get; set; }
        public List<Campaign> Campaigns { get; set; }


        //public virtual ICollection<CompanyDomain> CompanyDomains { get; set; }
        //public virtual ICollection<CmsSkinPageWidget> CmsSkinPageWidgets { get; set; }
        //public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        //public virtual ICollection<CompanyBannerSet> CompanyBannerSets { get; set; }
        //public virtual ICollection<CmsPage> CmsPages { get; set; }
    }
}