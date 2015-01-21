﻿using System.IO;
using System.Linq;
using System.Web;
using MPC.MIS.Areas.Api.Models;
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
        public static Company CreateFrom(this DomainModels.Company source)
        {
            byte[] bytes = null;
            if (source.Image != null && File.Exists(source.Image))
            {
                bytes = source.Image != null ? File.ReadAllBytes(source.Image) : null;
            }
            byte[] storeBackgroundImageBytes = null;
            if (source.StoreBackgroundImage != null && File.Exists(source.StoreBackgroundImage))
            {
                storeBackgroundImageBytes = source.StoreBackgroundImage != null ? File.ReadAllBytes(source.StoreBackgroundImage) : null;
            }
            byte[] defaultSpriteBytes = null;
            string defaultSpritePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Organisations/Organisation" + source.OrganisationId + "/Store" + source.CompanyId + "/Sprites" + "\\" + source.CompanyId + "_sprite.backup.png");
            if (File.Exists(defaultSpritePath))
            {
                defaultSpriteBytes = File.ReadAllBytes(defaultSpritePath);
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
                isTextWatermark = source.isTextWatermark == true ? "true" : "false",
                WatermarkText = source.WatermarkText,
                facebookAppId = source.facebookAppId,
                facebookAppKey = source.facebookAppKey,
                twitterAppId = source.twitterAppId,
                twitterAppKey = source.twitterAppKey,
                isStoreModePrivate = source.isStoreModePrivate == true ? "true" : "false",
                TaxPercentageId = source.TaxPercentageId,
                canUserPlaceOrderWithoutApproval = source.canUserPlaceOrderWithoutApproval,
                CanUserEditProfile = source.CanUserEditProfile,
                SalesAndOrderManagerId1 = source.SalesAndOrderManagerId1,
                SalesAndOrderManagerId2 = source.SalesAndOrderManagerId2,
                ProductionManagerId1 = source.ProductionManagerId1,
                ProductionManagerId2 = source.ProductionManagerId2,
                StockNotificationManagerId1 = source.StockNotificationManagerId1,
                StockNotificationManagerId2 = source.StockNotificationManagerId2,
                IsDeliveryTaxAble = source.IsDeliveryTaxAble,
                IsDisplayDeliveryOnCheckout = source.IsDisplayDeliveryOnCheckout,
                DeliveryPickUpAddressId = source.DeliveryPickUpAddressId,
                isBrokerPaymentRequired = source.isBrokerPaymentRequired == true ? "true" : "false",
                isIncludeVAT = source.isIncludeVAT == true ? "true" : "false",
                includeEmailBrokerArtworkOrderReport = source.includeEmailBrokerArtworkOrderReport,
                includeEmailBrokerArtworkOrderXML = source.includeEmailBrokerArtworkOrderXML,
                includeEmailBrokerArtworkOrderJobCard = source.includeEmailBrokerArtworkOrderJobCard,
                makeEmailBrokerArtworkOrderProductionReady = source.makeEmailBrokerArtworkOrderProductionReady,

                CompanyType = source.CompanyType != null ? source.CompanyType.CreateFrom() : null,
                RaveReviews =
                    source.RaveReviews != null ? source.RaveReviews.Take(10).Select(x => x.CreateFrom()).ToList() : null,
                CompanyCmykColors =
                    source.CompanyCMYKColors != null
                        ? source.CompanyCMYKColors.Select(x => x.CreateFrom()).ToList()
                        : null,
                CompanyTerritories =
                    source.CompanyTerritories != null
                        ? source.CompanyTerritories.Take(10).Select(x => x.CreateFrom()).ToList()
                        : null,
                Addresses = source.Addresses != null ? source.Addresses.Take(10).Select(x => x.CreateFrom()).ToList() : null,
                CompanyBannerSets = source.CompanyBannerSets != null ? source.CompanyBannerSets.Select(x => x.CreateFrom()).ToList() : null,
                CompanyContacts =
                    source.CompanyContacts != null ? source.CompanyContacts.Take(10).Select(x => x.CreateFrom()).ToList() : null,
                Campaigns = source.Campaigns != null ? source.Campaigns.Select(x => x.CreateFrom()).ToList() : null,
                PaymentGateways = source.PaymentGateways != null ? source.PaymentGateways.Take(10).Select(x => x.CreateFrom()).ToList() : null,
                ProductCategoriesListView = source.ProductCategories != null ? source.ProductCategories.Take(10).Where(x => x.ParentCategoryId == null).Select(x => x.ListViewModelCreateFrom()).ToList() : null,
                CmsPagesDropDownList = source.CmsPages != null ? source.CmsPages.Select(x => x.CreateFromForDropDown()).ToList() : null,
                ColorPalletes = source.ColorPalletes != null ? source.ColorPalletes.Select(c => c.CreateFrom()).ToList() : null,
                StoreBackgroudImage = storeBackgroundImageBytes,
                DefaultSpriteImage = defaultSpriteBytes,
                UserDefinedSpriteImage = GetUserDefinedSpriteImage(source),
                UserDefinedSpriteFileName = GetUserDefinedSpriteImageName(source)
            };
        }

        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.Company CreateFrom(this Company source)
        {
            var company = new DomainModels.Company
            {
                CompanyId = source.CompanyId,
                Name = source.Name,
                Image = source.ImageBytes,
                AccountNumber = source.AccountNumber,
                URL = source.URL,
                CreditReference = source.CreditReference,
                CreditLimit = source.CreditLimit,
                Terms = source.Terms,
                TypeId = 52,
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
                isTextWatermark = source.isTextWatermark == "true" ? true : false,
                WatermarkText = source.WatermarkText,
                facebookAppId = source.facebookAppId,
                facebookAppKey = source.facebookAppKey,
                twitterAppId = source.twitterAppId,
                twitterAppKey = source.twitterAppKey,
                isStoreModePrivate = source.isStoreModePrivate == "true" ? true : false,
                TaxPercentageId = source.TaxPercentageId,
                canUserPlaceOrderWithoutApproval = source.canUserPlaceOrderWithoutApproval,
                CanUserEditProfile = source.CanUserEditProfile,
                SalesAndOrderManagerId1 = source.SalesAndOrderManagerId1,
                SalesAndOrderManagerId2 = source.SalesAndOrderManagerId2,
                ProductionManagerId1 = source.ProductionManagerId1,
                ProductionManagerId2 = source.ProductionManagerId2,
                StockNotificationManagerId1 = source.StockNotificationManagerId1,
                StockNotificationManagerId2 = source.StockNotificationManagerId2,
                IsDeliveryTaxAble = source.IsDeliveryTaxAble,
                IsDisplayDeliveryOnCheckout = source.IsDisplayDeliveryOnCheckout,
                DeliveryPickUpAddressId = source.DeliveryPickUpAddressId,
                isBrokerPaymentRequired = source.isBrokerPaymentRequired == "true" ? true : false,
                isIncludeVAT = source.isIncludeVAT == "true" ? true : false,
                includeEmailBrokerArtworkOrderReport = source.includeEmailBrokerArtworkOrderReport,
                includeEmailBrokerArtworkOrderXML = source.includeEmailBrokerArtworkOrderXML,
                includeEmailBrokerArtworkOrderJobCard = source.includeEmailBrokerArtworkOrderJobCard,
                makeEmailBrokerArtworkOrderProductionReady = source.makeEmailBrokerArtworkOrderProductionReady,
                CompanyType = source.CompanyType != null ? source.CompanyType.CreateFrom() : null,
                ImageName = source.ImageName,
                RaveReviews =
                    source.RaveReviews != null ? source.RaveReviews.Select(x => x.CreateFrom()).ToList() : null,
                CompanyCMYKColors =
                    source.CompanyCmykColors != null
                        ? source.CompanyCmykColors.Select(x => x.CreateFrom()).ToList()
                        : null,
                Addresses = source.Addresses != null ? source.Addresses.Select(x => x.CreateFrom()).ToList() : null,
                CompanyTerritories =
                    source.CompanyTerritories != null
                        ? source.CompanyTerritories.Select(x => x.CreateFrom()).ToList()
                        : null,
                CompanyBannerSets =
                    source.CompanyBannerSets != null
                        ? source.CompanyBannerSets.Select(x => x.CreateFrom()).ToList()
                        : null,
                CompanyContacts =
                    source.CompanyContacts != null ? source.CompanyContacts.Select(x => x.Createfrom()).ToList() : null,
                PaymentGateways = source.PaymentGateways != null ? source.PaymentGateways.Select(x => x.CreateFrom()).ToList() : null,
                Campaigns = source.Campaigns != null ? source.Campaigns.Select(x => x.CreateFrom()).ToList() : null,
                ColorPalletes = source.ColorPalletes != null ? source.ColorPalletes.Select(c => c.CreateFrom()).ToList() : null,
                StoreBackgroudImageImageSource = source.StoreBackgroudImageImageSource,
                StoreBackgroudImageFileName = source.StoreBackgroudImageFileName,
                CmsOffers = source.CmsOffers != null ? source.CmsOffers.Select(c => c.CreateFrom()).ToList() : null,
                DefaultSpriteSource = source.DefaultSpriteSource,
                UserDefinedSpriteSource = source.UserDefinedSpriteSource,
                UserDefinedSpriteFileName = source.UserDefinedSpriteFileName,
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
                Addresses =
                    source.Addresses != null ? source.Addresses.Select(add => add.CreateFromSupplier()).ToList() : null,
                CompanyContacts =
                    source.CompanyContacts != null
                        ? source.CompanyContacts.Select(c => c.CreateFromSupplier()).ToList()
                        : null,
            };
        }

        public static CompanyResponse CreateFrom(this DomainResponseModel.CompanyResponse source)
        {
            return new CompanyResponse
            {
                Company = source.Company.CreateFrom(),

                SecondaryPageResponse = new SecondaryPageResponse
                {
                    CmsPages =
                        source.SecondaryPageResponse.CmsPages != null
                            ? source.SecondaryPageResponse.CmsPages.Select(x => x.CreateFromForListView()).ToList()
                            : null,
                    RowCount = source.SecondaryPageResponse.RowCount

                }
            };


        }

        private static byte[] GetUserDefinedSpriteImage(DomainModels.Company company)
        {
            string directoryPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Organisations/Organisation" + company.OrganisationId + "/Store" + company.CompanyId + "/Sprites");
            if (directoryPath != null && Directory.Exists(directoryPath))
            {
                DirectoryInfo dir = new DirectoryInfo(directoryPath);
                FileInfo[] Files = dir.GetFiles();
                foreach (FileInfo file in Files)
                {
                    if (file.Name != "" && file.Name != company.CompanyId + "_sprite.backup.png")
                    {
                        if (File.Exists(directoryPath + "\\" + file.Name))
                            return File.ReadAllBytes(directoryPath + "\\" + file.Name);
                    }
                }
            }
            return null;
        }
        private static string GetUserDefinedSpriteImageName(DomainModels.Company company)
        {
            string directoryPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Organisations/Organisation" + company.OrganisationId + "/Store" + company.CompanyId + "/Sprites");
            if (directoryPath != null && Directory.Exists(directoryPath))
            {
                DirectoryInfo dir = new DirectoryInfo(directoryPath);
                FileInfo[] Files = dir.GetFiles();
                foreach (FileInfo file in Files)
                {
                    if (file.Name != "" && file.Name != company.CompanyId + "_sprite.backup.png")
                    {
                        if (File.Exists(directoryPath + "\\" + file.Name))
                            return file.Name;
                    }
                }
            }
            return null;
        }
        #endregion
    }
}