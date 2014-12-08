using DomainModels = MPC.Models.DomainModels;
using ApiModels = MPC.Webstore.Models;


namespace MPC.Webstore.ModelMappers
{
    public static class CompanyMapper
    {
        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ApiModels.Company CreateFrom(this DomainModels.Company source)
        {
            return new ApiModels.Company
            {
                CompanyId = source.CompanyId,
                Name = source.Name,
                Image = source.Image,
                AccountNumber = source.AccountNumber,
                URL = source.URL,
                CreditReference = source.CreditReference,
                CreditLimit = source.CreditLimit,
                Terms = source.Terms,
                TypeId = source.TypeId,
                DefaultNominalCode =  source.DefaultNominalCode,
                DefaultMarkUpId = source.DefaultMarkUpId,
                AccountOpenDate = source.AccountOpenDate,
                AccountManagerId = source.AccountManagerId,
                Status =  source.Status,
                IsCustomer = source.IsCustomer,
                Notes = source.Notes,
                IsDisabled = source.IsDisabled,
                AccountBalance = source.AccountBalance,
                CreationDate = source.CreationDate,
                VATRegNumber = source.VATRegNumber,
                VATRegReference = source.VATRegReference,
                FlagId = source.FlagId,
                PhoneNo = source.PhoneNo,
                IsGeneral = source.IsGeneral,
                WebAccessCode = source.WebAccessCode,
                isArchived = source.isArchived,
                PayByPersonalCredeitCard = source.PayByPersonalCredeitCard,
                PONumberRequired = source.PONumberRequired,
                ShowPrices = source.ShowPrices,
                isDisplayBanners = source.isDisplayBanners,
                isDisplayMenuBar = source.isDisplayMenuBar,
                isDisplayBrokerSecondaryPages = source.isDisplayBrokerSecondaryPages,
                isAllowRegistrationFromWeb = source.isAllowRegistrationFromWeb,
                isBrokerCanAcceptPaymentOnline = source.isBrokerCanAcceptPaymentOnline,
                isDisplayFeaturedProducts = source.isDisplayFeaturedProducts,
                isDisplayPromotionalProducts = source.isDisplayPromotionalProducts,
                isDisplaySiteFooter = source.isDisplaySiteFooter,
                isDisplaySiteHeader = source.isDisplaySiteHeader,
                RedirectWebstoreURL = source.RedirectWebstoreURL,
                isShowGoogleMap = source.isShowGoogleMap,
                isTextWatermark = source.isTextWatermark,
                WatermarkText = source.WatermarkText,
                facebookAppId = source.facebookAppId,
                facebookAppKey = source.facebookAppKey,
                twitterAppId = source.twitterAppId,
                twitterAppKey = source.twitterAppKey,
                isStoreModePrivate = source.isStoreModePrivate,
                TaxPercentageId = source.TaxPercentageId,
                canUserPlaceOrderWithoutApproval = source.canUserPlaceOrderWithoutApproval,
                CanUserEditProfile = source.CanUserEditProfile,
                OrganisationId = source.OrganisationId,
                SalesAndOrderManagerId1 = source.SalesAndOrderManagerId1,
                SalesAndOrderManagerId2 = source.SalesAndOrderManagerId2,
                ProductionManagerId1 = source.ProductionManagerId1,
                ProductionManagerId2 = source.ProductionManagerId2,
                StockNotificationManagerId1 = source.StockNotificationManagerId1,
                StockNotificationManagerId2 = source.StockNotificationManagerId2,
                IsDeliveryTaxAble = source.IsDeliveryTaxAble,
                IsDisplayDeliveryOnCheckout = source.IsDisplayDeliveryOnCheckout,
                DeliveryPickUpAddressId = source.DeliveryPickUpAddressId,
                WebMasterTag = source.WebMasterTag,
                WebAnalyticCode = source.WebAnalyticCode
            };
        }

        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.Company CreateFrom(this ApiModels.Company source)
        {
            return new DomainModels.Company
            {
                CompanyId = source.CompanyId,
                Name = source.Name,
                Image = source.Image,
                AccountNumber = source.AccountNumber,
                URL = source.URL,
                CreditReference = source.CreditReference,
                CreditLimit = source.CreditLimit,
                Terms = source.Terms,
                TypeId = source.TypeId,
                DefaultNominalCode = source.DefaultNominalCode,
                DefaultMarkUpId = source.DefaultMarkUpId,
                AccountOpenDate = source.AccountOpenDate,
                AccountManagerId = source.AccountManagerId,
                Status = source.Status,
                IsCustomer = source.IsCustomer,
                Notes = source.Notes,
                IsDisabled = source.IsDisabled,
                AccountBalance = source.AccountBalance,
                CreationDate = source.CreationDate,
                VATRegNumber = source.VATRegNumber,
                VATRegReference = source.VATRegReference,
                FlagId = source.FlagId,
                PhoneNo = source.PhoneNo,
                IsGeneral = source.IsGeneral,
                WebAccessCode = source.WebAccessCode,
                isArchived = source.isArchived,
                PayByPersonalCredeitCard = source.PayByPersonalCredeitCard,
                PONumberRequired = source.PONumberRequired,
                ShowPrices = source.ShowPrices,
                isDisplayBanners = source.isDisplayBanners,
                isDisplayMenuBar = source.isDisplayMenuBar,
                isDisplayBrokerSecondaryPages = source.isDisplayBrokerSecondaryPages,
                isAllowRegistrationFromWeb = source.isAllowRegistrationFromWeb,
                isBrokerCanAcceptPaymentOnline = source.isBrokerCanAcceptPaymentOnline,
                isDisplayFeaturedProducts = source.isDisplayFeaturedProducts,
                isDisplayPromotionalProducts = source.isDisplayPromotionalProducts,
                isDisplaySiteFooter = source.isDisplaySiteFooter,
                isDisplaySiteHeader = source.isDisplaySiteHeader,
                RedirectWebstoreURL = source.RedirectWebstoreURL,
                isShowGoogleMap = source.isShowGoogleMap,
                isTextWatermark = source.isTextWatermark,
                WatermarkText = source.WatermarkText,
                facebookAppId = source.facebookAppId,
                facebookAppKey = source.facebookAppKey,
                twitterAppId = source.twitterAppId,
                twitterAppKey = source.twitterAppKey,
                isStoreModePrivate = source.isStoreModePrivate,
                TaxPercentageId = source.TaxPercentageId,
                canUserPlaceOrderWithoutApproval = source.canUserPlaceOrderWithoutApproval,
                CanUserEditProfile = source.CanUserEditProfile,
                OrganisationId = source.OrganisationId,
                SalesAndOrderManagerId1 = source.SalesAndOrderManagerId1,
                SalesAndOrderManagerId2 = source.SalesAndOrderManagerId2,
                ProductionManagerId1 = source.ProductionManagerId1,
                ProductionManagerId2 = source.ProductionManagerId2,
                StockNotificationManagerId1 = source.StockNotificationManagerId1,
                StockNotificationManagerId2 = source.StockNotificationManagerId2,
                IsDeliveryTaxAble = source.IsDeliveryTaxAble,
                IsDisplayDeliveryOnCheckout = source.IsDisplayDeliveryOnCheckout,
                DeliveryPickUpAddressId = source.DeliveryPickUpAddressId,
                WebMasterTag = source.WebMasterTag,
                WebAnalyticCode = source.WebAnalyticCode
            };
        }

        #endregion
    }
}