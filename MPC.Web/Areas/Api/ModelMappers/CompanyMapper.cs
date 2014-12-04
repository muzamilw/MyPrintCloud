using System.IO;
using MPC.MIS.Areas.Api.Models;
using MPC.MIS.ModelMappers;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class CompanyMapper
    {
        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static Company CreateFrom(this MPC.Models.DomainModels.Company source)
        {
            byte[] bytes = null;
            if (source.Image != null && File.Exists(source.Image))
            {
                bytes = source.Image != null ? File.ReadAllBytes(source.Image) : null;
            }
            return new Company
            {
                CompanyId = source.CompanyId,
                Name = source.Name,
                Image = bytes,
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
                CompanyType = source.CompanyType != null ?source.CompanyType.CreateFrom(): null
            };
        }
        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static MPC.Models.DomainModels.Company CreateFrom(this Company source)
        {
            var company = new MPC.Models.DomainModels.Company
                          {
                              CompanyId = source.CompanyId,
                              Name = source.Name,
                              //Image = source.Image,
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
                              CompanyType = source.CompanyType != null ? source.CompanyType.CreateFrom() : null
                          };
            return company;
        }
        #endregion
    }
}