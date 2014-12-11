using System.IO;
using System.Linq;
using ApiModels = MPC.MIS.Areas.Api.Models;
using DomainResponseModel = MPC.Models.ResponseModels;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class CompanyMapper
    {
        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ApiModels.Company CreateFrom(this DomainModels.Company source)
        {
            byte[] bytes = null;
            if (source.Image != null && File.Exists(source.Image))
            {
                bytes = source.Image != null ? File.ReadAllBytes(source.Image) : null;
            }
            return new ApiModels.Company
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
                CompanyType = source.CompanyType != null ? source.CompanyType.CreateFrom() : null,
                RaveReviews = source.RaveReviews.Select(x => x.CreateFrom()).ToList(),
                CompanyCmykColors = source.CompanyCMYKColors.Select(x => x.CreateFrom()).ToList(),
                CompanyTerritories = source.CompanyTerritories.Select(x => x.CreateFrom()).ToList(),
                Addresses = source.Addresses.Select(x => x.CreateFrom()).ToList()
            };
        }
        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.Company CreateFrom(this ApiModels.Company source)
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
                              CompanyType = source.CompanyType != null ? source.CompanyType.CreateFrom() : null,
                              RaveReviews =source.RaveReviews != null? source.RaveReviews.Select(x => x.CreateFrom()).ToList(): null,
                              CompanyCMYKColors = source.CompanyCmykColors != null ? source.CompanyCmykColors.Select(x => x.CreateFrom()).ToList() : null,
                              CompanyTerritories = source.CompanyTerritories != null ? source.CompanyTerritories.Select(x => x.CreateFrom()).ToList() : null,
                              Addresses = source.Addresses != null ? source.Addresses.Select(x => x.CreateFrom()).ToList() : null
                          };
            
            return company;
        }

        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ApiModels.SupplierForInventory CreateFromForInventory(this DomainModels.Company source)
        {
            return new ApiModels.SupplierForInventory
            {
                Name = source.Name,
                SupplierId = source.CompanyId,
                URL = source.URL
            };
        }

        /// <summary>
        /// Supplier Base Response
        /// </summary>
        public static ApiModels.SupplierBaseResponse CreateFrom(this DomainResponseModel.SupplierBaseResponse source)
        {
            return new ApiModels.SupplierBaseResponse
            {
                CompanyTypes = source.CompanyTypes.Select(ct => ct.CreateFrom()),
                Markups = source.Markups.Select(m => m.CreateFrom()),
                NominalCodes = source.NominalCodes.Select(m => m.CreateFrom()),
                SystemUsers = source.SystemUsers.Select(m => m.CreateFrom()),
                Flags = source.Flags.Select(f => f.CreateFromDropDown()),
                PriceFlags = source.Flags.Select(pf => pf.CreateFromDropDown()),
                RegistrationQuestions = source.RegistrationQuestions.Select(pf => pf.CreateFromDropDown())
            };
        }

        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.Company CreateFromSupplier(this ApiModels.Company source)
        {
            return new DomainModels.Company
            {
                CompanyId = source.CompanyId,
                Name = source.Name,
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
                AccountBalance = source.AccountBalance,
                CreationDate = source.CreationDate,
                VATRegNumber = source.VATRegNumber,
                VATRegReference = source.VATRegReference,
                FlagId = source.FlagId,
                PhoneNo = source.PhoneNo,
                Addresses = source.Addresses != null ? source.Addresses.Select(add => add.CreateFromSupplier()).ToList() : null,
                CompanyContacts = source.CompanyContacts != null ? source.CompanyContacts.Select(c => c.CreateFromSupplier()).ToList() : null,
            };
        }

        #endregion
    }
}