using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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

            //byte[] imageMapBytes = null;
            //string imageMapPath = HttpContext.Current.Server.MapPath("~/" + source.MapImageUrl); 
            //if (File.Exists(imageMapPath))
            //{
            //    imageMapBytes = File.ReadAllBytes(imageMapPath);
            //}

            byte[] spriteBytes = null;
            string spritePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + source.OrganisationId + "/" + source.CompanyId + "/sprite.png");
            string spriteRelativePath = "MPC_Content/Assets/" + source.OrganisationId + "/" + source.CompanyId +
                                        "/sprite.png?" + DateTime.Now.ToString();
            //Code Commented by Naveed on 20150827
            //if (File.Exists(spritePath))
            //{
            //    spriteBytes = File.ReadAllBytes(spritePath);
            //}
            string defaultSpritePath = HttpContext.Current.Server.MapPath("~/MPC_Content/DefaultSprite/sprite.bakup.png");
            
            byte[] defaultSpriteBytes = null;
            //Code Commented by Naveed on 20150827
            //if (File.Exists(defaultSpritePath))
            //{
            //    defaultSpriteBytes = File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/MPC_Content/DefaultSprite/sprite.bakup.png"));
            //}
            string defaultCss = string.Empty;
            string defaultCssPath =
                HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + source.OrganisationId + "/" +
                                                   source.CompanyId + "/site.css");

            if (File.Exists(defaultCssPath))
            {
                defaultCss = File.ReadAllText(HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + source.OrganisationId + "/" + source.CompanyId + "/site.css"));
            }
            string defaultContact = null;
            string email = null;
            DomainModels.CompanyContact companyContact = source.CompanyContacts != null ? 
                source.CompanyContacts.FirstOrDefault(contact => contact.IsDefaultContact == 1) : null;
            if (companyContact != null)
            {
                defaultContact = companyContact.FirstName + " " + companyContact.LastName;
                email = companyContact.Email;
            }
            return new Company
            {
                CompanyId = source.CompanyId,
                Name = source.Name,
                StoreImagePath = !string.IsNullOrEmpty(source.Image) ? source.Image + "?" + DateTime.Now.ToString() : string.Empty,
                AccountNumber = source.AccountNumber,
                DefaultContactEmail = email,
                DefaultContact = defaultContact,
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
                CurrentThemeId = source.CurrentThemeId,
                CustomCSS = defaultCss,
                Notes = source.Notes,
                IsDisabled = source.IsDisabled,
                AccountBalance = source.AccountBalance,
                CreationDate = source.CreationDate,
                VATRegNumber = source.VATRegNumber,
                VATRegReference = source.VATRegReference,
                ActiveBannerSetId = source.ActiveBannerSetId,
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
                isDisplaySecondaryPages = source.isDisplaySecondaryPages,
                isAllowRegistrationFromWeb = source.isAllowRegistrationFromWeb,
                isDisplayFeaturedProducts = source.isDisplayFeaturedProducts,
                isDisplayPromotionalProducts = source.isDisplayPromotionalProducts,
                isDisplaySiteFooter = source.isDisplaySiteFooter,
                isDisplaySiteHeader = source.isDisplaySiteHeader,
                RedirectWebstoreURL = source.RedirectWebstoreURL,
                isShowGoogleMap = source.isShowGoogleMap,
                isTextWatermark = source.isTextWatermark == true ? "true" : "false",
                StoreWorkflowImage = source.StoreWorkflowImage,
                WatermarkText = source.WatermarkText,
                facebookAppId = source.facebookAppId,
                facebookAppKey = source.facebookAppKey,
                twitterAppId = source.twitterAppId,
                twitterAppKey = source.twitterAppKey,
                TwitterURL = source.TwitterURL,
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
                IsDeliveryTaxAble = source.IsDeliveryTaxAble ?? false,
                IsDisplayDeliveryOnCheckout = source.IsDisplayDeliveryOnCheckout,
                isPaymentRequired = source.isPaymentRequired == true ? "true" : "false",
                isIncludeVAT = source.isIncludeVAT == true ? "true" : "false",
                includeEmailArtworkOrderReport = source.includeEmailArtworkOrderReport,
                includeEmailArtworkOrderXML = source.includeEmailArtworkOrderXML,
                includeEmailArtworkOrderJobCard = source.includeEmailArtworkOrderJobCard,
                makeEmailArtworkOrderProductionReady = source.makeEmailArtworkOrderProductionReady,
                CompanyType = source.CompanyType != null ? source.CompanyType.CreateFrom() : null,
                PickupAddressId = source.PickupAddressId,
                WebAnalyticCode = source.WebAnalyticCode,
                WebMasterTag = source.WebMasterTag,
                FacebookURL = source.FacebookURL,
                LinkedinURL = source.LinkedinURL,
                isCalculateTaxByService = source.isCalculateTaxByService,
                TaxLabel = source.TaxLabel,
                TaxRate = source.TaxRate,
                MapImageUrl = source.MapImageUrl,
                IsDisplayDiscountVoucherCode = source.IsDisplayDiscountVoucherCode,
                isWhiteLabel = source.isWhiteLabel,
                PriceFlagId = source.PriceFlagId,
                StoreId = source.StoreId,
               isStoreLive = source.isStoreLive,
               CanUserUpdateAddress = source.CanUserUpdateAddress,
               IsClickReached = source.IsClickReached,
                RaveReviews =
                    source.RaveReviews != null ? source.RaveReviews.Select(x => x.CreateFrom()).ToList() : null,
                TemplateColorStyles =
                    source.TemplateColorStyles != null
                        ? source.TemplateColorStyles.Select(x => x.CreateFrom()).ToList()
                        : null,
                CompanyTerritories =
                    source.CompanyTerritories != null
                       ? source.CompanyTerritories.Take(1).Select(x => x.CreateFrom()).ToList()
                        : null,
                Addresses = source.Addresses != null ? source.Addresses.Take(1).Select(x => x.CreateFrom()).ToList() : null,
                CompanyBannerSets = source.CompanyBannerSets != null ? source.CompanyBannerSets.Select(x => x.CreateFrom()).ToList() : null,
                CompanyContacts =
                    source.CompanyContacts != null ? source.CompanyContacts.Take(1).Select(x => x.CreateFrom()).ToList() : null,
                Campaigns = source.Campaigns != null ? source.Campaigns.Select(x => x.CreateFromForListView()).ToList() : null,
                PaymentGateways = source.PaymentGateways != null ? source.PaymentGateways.Select(x => x.CreateFrom()).ToList() : null,
                ProductCategoriesListView = source.ProductCategories != null ? source.ProductCategories.Where(x => x.ParentCategoryId == null && x.isArchived != true).Select(x => x.ListViewModelCreateFrom()).ToList().OrderBy(x => x.DisplayOrder).ToList() : null,
                StoreBackgroundImage = source.StoreBackgroundImage,
                DefaultSpriteSource = spriteRelativePath,
                //DefaultSpriteImage = defaultSpriteBytes,
                UserDefinedSpriteImage = spriteBytes,
                MediaLibraries = source.MediaLibraries != null ? source.MediaLibraries.Select(m => m.CreateFrom()).ToList() : null,
                CompanyDomains = source.CompanyDomains != null ? source.CompanyDomains.Select(x => x.CreateFrom()).ToList() : null,
                CmsOffers = source.CmsOffers != null ? source.CmsOffers.Select(c => c.CreateFrom()).ToList() : null,
                CompanyCostCentres = source.CompanyCostCentres != null ? (source.CompanyCostCentres.Count != 0 ? source.CompanyCostCentres.FirstOrDefault().CostCentre != null ? source.CompanyCostCentres.Select(x => x.CostCentre.CostCentreDropDownCreateFrom()).ToList() : null : null) : null
            };
        }
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static Company CreateFromForCrm(this DomainModels.Company source)
        {
            byte[] bytes = null;
            string imagePath;
            if (!string.IsNullOrEmpty(source.Image))
            {
                imagePath = HttpContext.Current.Server.MapPath("~/" + source.Image);
                if (File.Exists(imagePath))
                {
                    bytes = source.Image != null ? File.ReadAllBytes(imagePath) : null;
                }
            }
            byte[] storeBackgroundImageBytes = null;
            if (!string.IsNullOrEmpty(source.StoreBackgroundImage))
            {
                imagePath = HttpContext.Current.Server.MapPath("~/" + source.StoreBackgroundImage);
                if (File.Exists(imagePath))
                {
                    storeBackgroundImageBytes = source.Image != null ? File.ReadAllBytes(imagePath) : null;
                }
            }
            byte[] spriteBytes = null;
            string spritePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + source.OrganisationId + "/" + source.CompanyId + "/sprite.png");
            if (File.Exists(spritePath))
            {
                spriteBytes = File.ReadAllBytes(spritePath);
            }
            byte[] defaultSpriteBytes = null;
            if (File.Exists(HttpContext.Current.Server.MapPath("~/MPC_Content/DefaultSprite/sprite.bakup.png")))
            {
                defaultSpriteBytes = File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/MPC_Content/DefaultSprite/sprite.bakup.png"));
            }
            string defaultCss = string.Empty;

            if (File.Exists(HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + source.OrganisationId + "/" + source.CompanyId + "/site.css")))
            {
                defaultCss = File.ReadAllText(HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + source.OrganisationId + "/" + source.CompanyId + "/site.css"));
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
                CustomCSS = defaultCss,
                Notes = source.Notes,
                IsDisabled = source.IsDisabled,
                AccountBalance = source.AccountBalance,
                CreationDate = source.CreationDate,
                VATRegNumber = source.VATRegNumber,
                VATRegReference = source.VATRegReference,
                ActiveBannerSetId = source.ActiveBannerSetId,
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
                isAllowRegistrationFromWeb = source.isAllowRegistrationFromWeb,
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
                IsDisplayDiscountVoucherCode = source.IsDisplayDiscountVoucherCode,
                isWhiteLabel = source.isWhiteLabel,
                canUserPlaceOrderWithoutApproval = source.canUserPlaceOrderWithoutApproval,
                CanUserEditProfile = source.CanUserEditProfile,
                SalesAndOrderManagerId1 = source.SalesAndOrderManagerId1,
                SalesAndOrderManagerId2 = source.SalesAndOrderManagerId2,
                ProductionManagerId1 = source.ProductionManagerId1,
                ProductionManagerId2 = source.ProductionManagerId2,
                StockNotificationManagerId1 = source.StockNotificationManagerId1,
                StockNotificationManagerId2 = source.StockNotificationManagerId2,
                IsDeliveryTaxAble = source.IsDeliveryTaxAble ?? false,
                IsDisplayDeliveryOnCheckout = source.IsDisplayDeliveryOnCheckout,
                isPaymentRequired = source.isPaymentRequired == true ? "true" : "false",
                isIncludeVAT = source.isIncludeVAT == true ? "true" : "false",
                includeEmailArtworkOrderReport = source.includeEmailArtworkOrderReport,
                includeEmailArtworkOrderXML = source.includeEmailArtworkOrderXML,
                includeEmailArtworkOrderJobCard = source.includeEmailArtworkOrderJobCard,
                makeEmailArtworkOrderProductionReady = source.makeEmailArtworkOrderProductionReady,
                TaxLabel = source.TaxLabel,
                TaxRate = source.TaxRate,
                MapImageUrl = source.MapImageUrl,
                CompanyType = source.CompanyType != null ? source.CompanyType.CreateFrom() : null,
                PickupAddressId = source.PickupAddressId,
                CompanyTerritories =
                    source.CompanyTerritories != null
                       ? source.CompanyTerritories.Take(1).Select(x => x.CreateFrom()).ToList()
                        : null,
                Addresses = source.Addresses != null ? source.Addresses.Take(10).Select(x => x.CreateFrom()).ToList() : null,
                CompanyContacts =
                    source.CompanyContacts != null ? source.CompanyContacts.Take(10).Select(x => x.CreateFrom()).ToList() : null,
                StoreBackgroudImage = storeBackgroundImageBytes,
                DefaultSpriteImage = defaultSpriteBytes,
                UserDefinedSpriteImage = spriteBytes,
                MediaLibraries = source.MediaLibraries != null ? source.MediaLibraries.Select(m => m.CreateFrom()).ToList() : null,
                CompanyContactCount = source.CompanyContacts != null ? source.CompanyContacts.Count : 0,
                CompanyAddressesCount = source.Addresses != null ? source.Addresses.Count : 0,
                isCalculateTaxByService = source.isCalculateTaxByService,
                PriceFlagId = source.PriceFlagId,
                StoreId = source.StoreId
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
                StoreBackgroundFile = source.StoreBackgroundFile,
                ActiveBannerSetId = source.ActiveBannerSetId,
                Name = source.Name,
                ImageBytes = source.ImageBytes,
                AccountNumber = source.AccountNumber,
                URL = source.URL,
                CreditReference = source.CreditReference,
                CreditLimit = source.CreditLimit,
                CreationDate = source.CreationDate ?? DateTime.Now,
                CurrentThemeId = source.CurrentThemeId,
                Terms = source.Terms,
                CustomCSS = source.CustomCSS,
                TypeId = 52,
                DefaultNominalCode = source.DefaultNominalCode,
                DefaultMarkUpId = source.DefaultMarkUpId,
                AccountOpenDate = source.AccountOpenDate,
                AccountManagerId = source.AccountManagerId,
                StoreBackgroundImage = source.StoreBackgroundImage,
                Status = source.Status,
                IsCustomer = source.IsCustomer,
                Notes = source.Notes,
                IsDisabled = source.IsDisabled,
                AccountBalance = source.AccountBalance,
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
                isDisplaySecondaryPages = source.isDisplaySecondaryPages,
                isAllowRegistrationFromWeb = source.isAllowRegistrationFromWeb,
                isDisplayFeaturedProducts = source.isDisplayFeaturedProducts,
                isDisplayPromotionalProducts = source.isDisplayPromotionalProducts,
                isDisplaySiteFooter = source.isDisplaySiteFooter,
                isDisplaySiteHeader = source.isDisplaySiteHeader,
                RedirectWebstoreURL = source.RedirectWebstoreURL,
                isShowGoogleMap = source.isShowGoogleMap,
                isTextWatermark = source.isTextWatermark == "true" ? true : false,
                WatermarkText = source.isTextWatermark == "true" ? source.WatermarkText : source.StoreWorkflowImage,
                MapImageUrl = source.MapImageUrl,
                StoreWorkflowImage = source.StoreWorkflowImage,
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
                isPaymentRequired = source.isPaymentRequired == "true" ? true : false,
                isIncludeVAT = source.isIncludeVAT == "true" ? true : false,
                includeEmailArtworkOrderReport = source.includeEmailArtworkOrderReport,
                includeEmailArtworkOrderXML = source.includeEmailArtworkOrderXML,
                includeEmailArtworkOrderJobCard = source.includeEmailArtworkOrderJobCard,
                makeEmailArtworkOrderProductionReady = source.makeEmailArtworkOrderProductionReady,
                CompanyType = source.CompanyType != null ? source.CompanyType.CreateFrom() : null,
                WebMasterTag = source.WebMasterTag ?? string.Empty,
                WebAnalyticCode = source.WebAnalyticCode ?? string.Empty,
                PickupAddressId = source.PickupAddressId,
                ImageName = source.ImageName,
                FacebookURL = source.FacebookURL,
                TwitterURL = source.TwitterURL,
                LinkedinURL = source.LinkedinURL,
                isCalculateTaxByService = source.isCalculateTaxByService,
                TaxLabel = source.TaxLabel,
                TaxRate = source.TaxRate,
                IsDisplayDiscountVoucherCode = source.IsDisplayDiscountVoucherCode,
                isWhiteLabel = source.isWhiteLabel,
                PriceFlagId = source.PriceFlagId,
                StoreId = source.StoreId,
                isStoreLive = source.isStoreLive,
                CanUserUpdateAddress = source.CanUserUpdateAddress,
                RaveReviews =
                    source.RaveReviews != null ? source.RaveReviews.Select(x => x.CreateFrom()).ToList() : null,
                TemplateColorStyles =
                    source.TemplateColorStyles != null
                        ? source.TemplateColorStyles.Select(x => x.CreateFrom()).ToList()
                        : null,
                CompanyBannerSets =
                    source.CompanyBannerSets != null
                        ? source.CompanyBannerSets.Select(x => x.CreateFrom()).ToList()
                        : null,
                PaymentGateways = source.PaymentGateways != null ? source.PaymentGateways.Select(x => x.CreateFrom()).ToList() : null,
                Campaigns = source.Campaigns != null ? source.Campaigns.Select(x => x.CreateFrom()).ToList() : null,
                CmsOffers = source.CmsOffers != null ? source.CmsOffers.Select(c => c.CreateFrom()).ToList() : null,
                UserDefinedSpriteSource = source.UserDefinedSpriteSource,
                MediaLibraries = source.MediaLibraries != null ? source.MediaLibraries.Select(m => m.CreateFrom()).ToList() : null,
                CompanyDomains = source.CompanyDomains != null ? source.CompanyDomains.Select(x => x.CreateFrom()).ToList() : null,
                CompanyCostCentres = source.CompanyCostCentres != null ? source.CompanyCostCentres.Select(x => x.CreateFrom()).ToList() : null,
                FieldVariables = source.FieldVariables != null ? source.FieldVariables.Select(x => x.CreateFrom()).ToList() : null,
                SmartForms = source.SmartForms != null ? source.SmartForms.Select(x => x.CreateFrom()).ToList() : null,
                ScopeVariables = source.ScopeVariables != null ? source.ScopeVariables.Select(ccv => ccv.CreateFrom()).ToList() : null
            };

            return company;
        }

        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static SupplierForInventory CreateFromForInventory(this DomainModels.Company source)
        {
            return new SupplierForInventory
            {
                Name = source.Name,
                SupplierId = source.CompanyId,
                URL = source.URL
            };
        }

        /// <summary>
        /// Supplier Base Response
        /// </summary>
        public static SupplierBaseResponse CreateFrom(this DomainResponseModel.SupplierBaseResponse source)
        {
            return new SupplierBaseResponse
            {
                CurrencySymbol = source.CurrencySymbol,
                CompanyTypes = source.CompanyTypes != null ? source.CompanyTypes.Select(ct => ct.CreateFrom()) : new List<CompanyType>(),
                Markups = source.Markups != null ? source.Markups.Select(m => m.CreateFrom()) : new List<Markup>(),
                NominalCodes = source.NominalCodes != null ? source.NominalCodes.Select(m => m.CreateFrom()) : new List<ChartOfAccount>(),
                SystemUsers = source.SystemUsers != null ? source.SystemUsers.Select(m => m.CreateFrom()) : new List<SystemUserDropDown>(),
                Flags = source.Flags != null ? source.Flags.Select(f => f.CreateFromDropDown()) : new List<SectionFlagDropDown>(),
                PriceFlags = source.Flags != null ? source.Flags.Select(pf => pf.CreateFromDropDown()) : new List<SectionFlagDropDown>(),
                RegistrationQuestions = source.RegistrationQuestions != null ? source.RegistrationQuestions.Select(pf => pf.CreateFromDropDown()) : new List<RegistrationQuestionDropDown>()
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
                CreationDate = source.CreationDate ?? DateTime.Now,
                VATRegNumber = source.VATRegNumber,
                VATRegReference = source.VATRegReference,
                FlagId = source.FlagId,
                PhoneNo = source.PhoneNo,
                TaxLabel = source.TaxLabel,
                TaxRate = source.TaxRate,
                CompanyLogoSource = source.CompanyLogoSource,
                CompanyLogoName = source.CompanyLogoName,
                MapImageUrl = source.MapImageUrl,
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
                NewUsersCount = source.NewUsersCount,
                NewOrdersCount = source.NewOrdersCount,
                SecondaryPageResponse = new SecondaryPageResponse
                {
                    CmsPages =
                        source.SecondaryPageResponse.CmsPages != null
                            ? source.SecondaryPageResponse.CmsPages.Select(x => x.CreateFromForListView()).ToList()
                            : null,
                    RowCount = source.SecondaryPageResponse.RowCount,

                    SystemPages =
                        source.SecondaryPageResponse.SystemPages != null
                            ? source.SecondaryPageResponse.SystemPages.Select(x => x.CreateFromForListView()).ToList()
                            : null,
                    SystemPagesRowCount = source.SecondaryPageResponse.SystemPagesRowCount

                }
            };
        }
        public static CompanyResponse CreateFromForCrm(this DomainResponseModel.CompanyResponse source)
        {
            return new CompanyResponse
            {
                Company = source.Company.CreateFromForCrm(),
            };
        }

        public static ApiModels.CrmSupplierListViewModel CrmSupplierListViewCreateFrom(this DomainModels.Company source)
        {
            byte[] bytes = null;
            string defaultContact = null;
            string defaultContactEmail = null;
            DomainModels.CompanyContact companyContact = source.CompanyContacts.FirstOrDefault(contact => contact.IsDefaultContact == 1);
            if (companyContact != null)
            {
                defaultContact = companyContact.FirstName + " " + companyContact.LastName;
                defaultContactEmail = companyContact.Email;
            }
            return new CrmSupplierListViewModel
            {
                AccountNumber = source.AccountNumber,
                DefaultContactName = defaultContact,
                DefaultContactEmail = defaultContactEmail,
                CompanyId = source.CompanyId,
                IsCustomer = source.IsCustomer,
                Name = source.Name,
                Status = source.Status,
                URL = source.URL,
                CreatedDate = source.CreationDate,
                // Email = source.c todo
                Image = bytes,
                StoreImagePath = !string.IsNullOrEmpty(source.Image) ? source.Image + "?" + DateTime.Now.ToString() : string.Empty,
            };
        }

        public static CompanyForCalender CreateFromForCalendar(this DomainModels.Company source)
        {
            return new CompanyForCalender
            {
                CompanyId = source.CompanyId,
                StoreId = source.StoreId,
                Name = source.Name,
                CreationDate = source.CreationDate,
                URL = source.URL,
                IsCustomer = source.IsCustomer,
                TaxRate = source.TaxRate
            };
        }

        public static StoresListDropDown CreateFromForDropDown(this DomainModels.Company source)
        {
            return new StoresListDropDown
            {
                CompanyId = source.CompanyId,
                Name = source.Name
            };
        }
        #endregion
    }
}