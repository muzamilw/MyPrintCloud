using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Interfaces.Repository;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using AutoMapper;
using System.Web;
using System.IO;
using System.Configuration;


namespace MPC.Repository.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        #region Private
        private readonly Dictionary<CompanyByColumn, Func<Company, object>> companyOrderByClause = new Dictionary<CompanyByColumn, Func<Company, object>>
                    {
                        {CompanyByColumn.Code, d => d.CompanyId},
                        {CompanyByColumn.Name, c => c.Name},
                        {CompanyByColumn.Type, d => d.TypeId},
                        {CompanyByColumn.Status, d => d.Status}
                    };
        #endregion
        public CompanyRepository(IUnityContainer container)
            : base(container)
        {
        }

        protected override IDbSet<Company> DbSet
        {
            get
            {
                return db.Companies;
            }
        }

        /// <summary>
        /// USer count in last few days
        /// </summary>
        public int UserCount(long? storeId, int numberOfDays)
        {
            DateTime currenteDate = DateTime.UtcNow.Date.AddDays(-numberOfDays);
            return DbSet.Count(company => storeId == company.StoreId && company.CreationDate >= currenteDate);
        }

        public override IEnumerable<Company> GetAll()
        {
            try
            {
                return DbSet.Where(c => c.OrganisationId == OrganisationId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<Company> GetAllRetailAndCorporateStores()
        {
            try
            {
                return DbSet.Where(c => c.OrganisationId == OrganisationId && (c.IsCustomer == 3 || c.IsCustomer == 4)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Req: Get all retail stores for crm
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Company> GetAllRetailStores()
        {
            try
            {
                return DbSet.Where(c => c.OrganisationId == OrganisationId && (c.IsCustomer == 4)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long GetStoreIdFromDomain(string domain)
        {
            try
            {
                var companyDomain = db.CompanyDomains.Where(d => d.Domain == domain).ToList();
                if (companyDomain.FirstOrDefault() != null)
                {
                    return companyDomain.FirstOrDefault().CompanyId;

                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public Company GetCustomer(int CompanyId)
        {
            try
            {
                //Create Customer
                return db.Companies.Include("Addresses").Include("CompanyContacts").Where(customer => customer.CompanyId == CompanyId).FirstOrDefault<Company>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public CompanyResponse GetCompanyById(long companyId)
        {
            try
            {

                CompanyResponse companyResponse = new CompanyResponse();
                var company = db.Companies.Include(c => c.CmsPages)
                    .Include(c => c.RaveReviews)
                    .Include(c => c.TemplateColorStyles)
                    .Include(c => c.CompanyBannerSets)
                    .Include(c => c.Campaigns)
                    .Include(c => c.PaymentGateways)
                    .Include(c => c.ProductCategories)
                    .Include(c => c.MediaLibraries)
                    .Include(c => c.CompanyDomains)
                    .Include(c => c.CmsOffers)
                    .Include(c => c.CompanyCostCentres)
                    .Include(c => c.Addresses)
                    .Include(c => c.CompanyContacts)
                    .Include(c => c.CompanyTerritories)
                    .Where(c => c.CompanyId == companyId)
                    .Select(c => new
                    {
                        c.CompanyId,
                        c.Name,
                        c.AccountNumber,
                        c.URL,
                        c.CreditReference,
                        c.CreditLimit,
                        c.Terms,
                        c.TypeId,
                        c.DefaultMarkUpId,
                        c.AccountOpenDate,
                        c.AccountManagerId,
                        c.DefaultNominalCode,
                        c.CurrentThemeId,
                        c.Status,
                        c.IsCustomer,
                        c.Notes,
                        c.IsDisabled,
                        c.AccountBalance,
                        c.CreationDate,
                        c.VATRegNumber,
                        c.VATRegReference,
                        c.FlagId,
                        c.PhoneNo,
                        c.IsGeneral,
                        c.WebAccessCode,
                        c.isArchived,
                        c.PayByPersonalCredeitCard,
                        c.PONumberRequired,
                        c.ShowPrices,
                        c.isDisplayBanners,
                        c.isDisplayMenuBar,
                        c.isDisplaySecondaryPages,
                        c.isAllowRegistrationFromWeb,
                        c.isDisplayFeaturedProducts,
                        c.isDisplayPromotionalProducts,
                        c.isDisplaySiteFooter,
                        c.isDisplaySiteHeader,
                        c.isShowGoogleMap,
                        c.RedirectWebstoreURL,
                        c.isTextWatermark,
                        c.WatermarkText,
                        c.MapImageUrl,
                        c.facebookAppId,
                        c.facebookAppKey,
                        c.twitterAppId,
                        c.twitterAppKey,
                        c.TwitterURL,
                        c.isStoreModePrivate,
                        c.TaxPercentageId,
                        c.canUserPlaceOrderWithoutApproval,
                        c.CanUserEditProfile,
                        c.SalesAndOrderManagerId1,
                        c.SalesAndOrderManagerId2,
                        c.ProductionManagerId1,
                        c.ProductionManagerId2,
                        c.StockNotificationManagerId1,
                        c.StockNotificationManagerId2,
                        c.IsDeliveryTaxAble,
                        c.IsDisplayDeliveryOnCheckout,
                        c.isPaymentRequired,
                        c.isIncludeVAT,
                        c.includeEmailArtworkOrderReport,
                        c.includeEmailArtworkOrderXML,
                        c.includeEmailArtworkOrderJobCard,
                        c.StoreBackgroundImage,
                        c.makeEmailArtworkOrderProductionReady,
                        c.CompanyType,
                        c.PickupAddressId,
                        c.WebAnalyticCode,
                        c.WebMasterTag,
                        c.FacebookURL,
                        c.LinkedinURL,
                        c.isCalculateTaxByService,
                        c.isWhiteLabel,
                        c.TaxLabel,
                        c.TaxRate,
                        c.IsDisplayDiscountVoucherCode,
                        c.PriceFlagId,
                        c.StoreId,
                        c.isStoreLive,
                        c.CanUserUpdateAddress,
                        RaveReviews = c.RaveReviews.OrderBy(r => r.SortOrder).ToList(),
                        CmsPages = c.CmsPages.Where(page => page.isUserDefined == true).Take(5).Select(cms => new
                        {
                            cms.PageId,
                            cms.PageTitle,
                            cms.isDisplay,
                            cms.isEnabled,
                            cms.Meta_Title,
                            cms.isUserDefined,
                            cms.PageCategory,
                            cms.PageBanner
                        }).ToList(),
                        SystemPages = c.CmsPages.Where(page => page.isUserDefined == false).Take(5).Select(cms => new
                        {
                            cms.PageId,
                            cms.PageTitle,
                            cms.isDisplay,
                            cms.isEnabled,
                            cms.Meta_Title,
                            cms.isUserDefined,
                            cms.PageCategory,
                            cms.PageBanner
                        }).ToList(),
                        c.TemplateColorStyles,
                        c.CompanyBannerSets,
                        Campaigns = c.Campaigns.Select(cam => new
                        {
                            cam.CampaignId,
                            cam.CampaignName,
                            cam.CampaignType,
                            cam.StartDateTime,
                            cam.SendEmailAfterDays,
                            cam.IsEnabled,
                            cam.CampaignEmailEvent
                        }),
                        c.PaymentGateways,
                        ProductCategories = c.ProductCategories.Where(pc => pc.ParentCategoryId == null),
                        c.MediaLibraries,
                        c.CompanyDomains,
                        c.CmsOffers,
                        c.CompanyCostCentres,
                        CompanyTerritories = c.CompanyTerritories.Take(1).ToList(),
                        Addresses = c.Addresses.Where(address => (!address.isArchived.HasValue || !address.isArchived.Value)).Take(1).ToList(),
                        CompanyContacts = c.CompanyContacts.Take(1).ToList(),
                        c.Image,
                        c.ActiveBannerSetId,
                        c.OrganisationId
                    }).ToList().Select(c => new Company
                    {
                        CompanyId = c.CompanyId,
                        Name = c.Name,
                        AccountNumber = c.AccountNumber,
                        URL = c.URL,
                        CreditReference = c.CreditReference,
                        CreditLimit = c.CreditLimit,
                        Terms = c.Terms,
                        TypeId = c.TypeId,
                        DefaultMarkUpId = c.DefaultMarkUpId,
                        AccountOpenDate = c.AccountOpenDate,
                        AccountManagerId = c.AccountManagerId,
                        DefaultNominalCode = c.DefaultNominalCode,
                        Status = c.Status,
                        IsCustomer = c.IsCustomer,
                        Notes = c.Notes,
                        IsDisabled = c.IsDisabled,
                        CurrentThemeId = c.CurrentThemeId,
                        AccountBalance = c.AccountBalance,
                        CreationDate = c.CreationDate,
                        VATRegNumber = c.VATRegNumber,
                        VATRegReference = c.VATRegReference,
                        FlagId = c.FlagId,
                        PhoneNo = c.PhoneNo,
                        IsDisplayDiscountVoucherCode = c.IsDisplayDiscountVoucherCode,
                        isWhiteLabel = c.isWhiteLabel,
                        IsGeneral = c.IsGeneral,
                        WebAccessCode = c.WebAccessCode,
                        isArchived = c.isArchived,
                        PayByPersonalCredeitCard = c.PayByPersonalCredeitCard,
                        PONumberRequired = c.PONumberRequired,
                        ShowPrices = c.ShowPrices,
                        isDisplayBanners = c.isDisplayBanners,
                        isDisplayMenuBar = c.isDisplayMenuBar,
                        isDisplaySecondaryPages = c.isDisplaySecondaryPages,
                        isAllowRegistrationFromWeb = c.isAllowRegistrationFromWeb,
                        isDisplayFeaturedProducts = c.isDisplayFeaturedProducts,
                        isDisplayPromotionalProducts = c.isDisplayPromotionalProducts,
                        isDisplaySiteFooter = c.isDisplaySiteFooter,
                        isDisplaySiteHeader = c.isDisplaySiteHeader,
                        isShowGoogleMap = c.isShowGoogleMap,
                        RedirectWebstoreURL = c.RedirectWebstoreURL,
                        isTextWatermark = c.isTextWatermark,
                        WatermarkText = c.WatermarkText,
                        MapImageUrl = c.MapImageUrl,
                        facebookAppId = c.facebookAppId,
                        facebookAppKey = c.facebookAppKey,
                        twitterAppId = c.twitterAppId,
                        twitterAppKey = c.twitterAppKey,
                        TwitterURL = c.TwitterURL,
                        isStoreModePrivate = c.isStoreModePrivate,
                        TaxPercentageId = c.TaxPercentageId,
                        canUserPlaceOrderWithoutApproval = c.canUserPlaceOrderWithoutApproval,
                        CanUserEditProfile = c.CanUserEditProfile,
                        SalesAndOrderManagerId1 = c.SalesAndOrderManagerId1,
                        SalesAndOrderManagerId2 = c.SalesAndOrderManagerId2,
                        ProductionManagerId1 = c.ProductionManagerId1,
                        ProductionManagerId2 = c.ProductionManagerId2,
                        StockNotificationManagerId1 = c.StockNotificationManagerId1,
                        StockNotificationManagerId2 = c.StockNotificationManagerId2,
                        IsDeliveryTaxAble = c.IsDeliveryTaxAble,
                        IsDisplayDeliveryOnCheckout = c.IsDisplayDeliveryOnCheckout,
                        isPaymentRequired = c.isPaymentRequired,
                        isIncludeVAT = c.isIncludeVAT,
                        includeEmailArtworkOrderReport = c.includeEmailArtworkOrderReport,
                        includeEmailArtworkOrderXML = c.includeEmailArtworkOrderXML,
                        includeEmailArtworkOrderJobCard = c.includeEmailArtworkOrderJobCard,
                        StoreBackgroundImage = c.StoreBackgroundImage,
                        makeEmailArtworkOrderProductionReady = c.makeEmailArtworkOrderProductionReady,
                        CompanyType = c.CompanyType,
                        PickupAddressId = c.PickupAddressId,
                        WebAnalyticCode = c.WebAnalyticCode,
                        WebMasterTag = c.WebMasterTag,
                        FacebookURL = c.FacebookURL,
                        LinkedinURL = c.LinkedinURL,
                        isCalculateTaxByService = c.isCalculateTaxByService,
                        RaveReviews = c.RaveReviews,
                        TaxLabel = c.TaxLabel,
                        PriceFlagId = c.PriceFlagId,
                        TaxRate = c.TaxRate,
                        StoreId = c.StoreId,
                        isStoreLive = c.isStoreLive,
                        CanUserUpdateAddress = c.CanUserUpdateAddress,
                        CmsPages = c.CmsPages.Select(cms => new CmsPage
                        {
                            PageId = cms.PageId,
                            PageTitle = cms.PageTitle,
                            isDisplay = cms.isDisplay,
                            isEnabled = cms.isEnabled,
                            Meta_Title = cms.Meta_Title,
                            isUserDefined = cms.isUserDefined,
                            PageCategory = cms.PageCategory,
                            PageBanner = cms.PageBanner
                        }).ToList(),
                        SystemPages = c.SystemPages.Select(cms => new CmsPage
                        {
                            PageId = cms.PageId,
                            PageTitle = cms.PageTitle,
                            isDisplay = cms.isDisplay,
                            isEnabled = cms.isEnabled,
                            Meta_Title = cms.Meta_Title,
                            isUserDefined = cms.isUserDefined,
                            PageCategory = cms.PageCategory,
                            PageBanner = cms.PageBanner
                        }).ToList(),
                        TemplateColorStyles = c.TemplateColorStyles,
                        CompanyBannerSets = c.CompanyBannerSets,
                        Campaigns = c.Campaigns.Select(cam => new Campaign
                        {
                            CampaignName = cam.CampaignName,
                            CampaignId = cam.CampaignId,
                            CampaignType = cam.CampaignType,
                            StartDateTime = cam.StartDateTime,
                            SendEmailAfterDays = cam.SendEmailAfterDays,
                            IsEnabled = cam.IsEnabled,
                            CampaignEmailEvent = cam.CampaignEmailEvent
                        }).ToList(),
                        PaymentGateways = c.PaymentGateways,
                        ProductCategories = c.ProductCategories.ToList(),
                        MediaLibraries = c.MediaLibraries,
                        CompanyDomains = c.CompanyDomains,
                        CmsOffers = c.CmsOffers,
                        CompanyCostCentres = c.CompanyCostCentres,
                        Image = c.Image,
                        ActiveBannerSetId = c.ActiveBannerSetId,
                        OrganisationId = c.OrganisationId,
                        CompanyTerritories = c.CompanyTerritories.ToList(),
                        Addresses = c.Addresses.ToList(),
                        CompanyContacts = c.CompanyContacts.ToList()
                    }).FirstOrDefault();

                companyResponse.SecondaryPageResponse = new SecondaryPageResponse
                {
                    RowCount =
                        db.CmsPages.Count(
                            cmp =>
                        cmp.CompanyId == companyId && cmp.isUserDefined == true),
                    CmsPages =
                        company != null
                            ? company.CmsPages
                            : new List<CmsPage>(),

                    SystemPagesRowCount =
                db.CmsPages.Count(
                    cmp =>
                cmp.CompanyId == companyId && cmp.isUserDefined == false),
                    SystemPages =
                        company != null
                            ? company.SystemPages
                            : new List<CmsPage>()
                };
                companyResponse.Company = company;
                return companyResponse;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        public CompanyResponse GetCompanyByIdForCrm(long companyId)
        {
            try
            {
                CompanyResponse companyResponse = new CompanyResponse();
                var company = db.Companies.Include(c => c.CmsPages)
                    .Include(c => c.Addresses)
                    .Include(c => c.CompanyContacts)
                    .Include(c => c.CompanyTerritories)
                    .Where(c => c.CompanyId == companyId)
                    .Select(c => new
                    {
                        c.CompanyId,
                        c.Name,
                        c.AccountNumber,
                        c.URL,
                        c.CreditReference,
                        c.CreditLimit,
                        c.Terms,
                        c.TypeId,
                        c.DefaultMarkUpId,
                        c.AccountOpenDate,
                        c.AccountManagerId,
                        c.DefaultNominalCode,
                        c.CurrentThemeId,
                        c.Status,
                        c.IsCustomer,
                        c.Notes,
                        c.IsDisabled,
                        c.AccountBalance,
                        c.CreationDate,
                        c.VATRegNumber,
                        c.VATRegReference,
                        c.FlagId,
                        c.PhoneNo,
                        c.IsGeneral,
                        c.WebAccessCode,
                        c.isArchived,
                        c.PayByPersonalCredeitCard,
                        c.PONumberRequired,
                        c.ShowPrices,
                        c.isDisplayBanners,
                        c.isDisplayMenuBar,
                        c.isDisplaySecondaryPages,
                        c.isAllowRegistrationFromWeb,
                        c.isDisplayFeaturedProducts,
                        c.isDisplayPromotionalProducts,
                        c.isDisplaySiteFooter,
                        c.isDisplaySiteHeader,
                        c.isShowGoogleMap,
                        c.RedirectWebstoreURL,
                        c.isTextWatermark,
                        c.WatermarkText,
                        c.MapImageUrl,
                        c.facebookAppId,
                        c.facebookAppKey,
                        c.twitterAppId,
                        c.twitterAppKey,
                        c.TwitterURL,
                        c.isStoreModePrivate,
                        c.TaxPercentageId,
                        c.canUserPlaceOrderWithoutApproval,
                        c.CanUserEditProfile,
                        c.SalesAndOrderManagerId1,
                        c.SalesAndOrderManagerId2,
                        c.ProductionManagerId1,
                        c.ProductionManagerId2,
                        c.StockNotificationManagerId1,
                        c.StockNotificationManagerId2,
                        c.IsDeliveryTaxAble,
                        c.IsDisplayDeliveryOnCheckout,
                        c.isPaymentRequired,
                        c.isIncludeVAT,
                        c.includeEmailArtworkOrderReport,
                        c.includeEmailArtworkOrderXML,
                        c.includeEmailArtworkOrderJobCard,
                        c.StoreBackgroundImage,
                        c.makeEmailArtworkOrderProductionReady,
                        c.CompanyType,
                        c.PickupAddressId,
                        c.WebAnalyticCode,
                        c.WebMasterTag,
                        c.FacebookURL,
                        c.LinkedinURL,
                        c.isCalculateTaxByService,
                        c.isWhiteLabel,
                        c.TaxLabel,
                        c.TaxRate,
                        c.IsDisplayDiscountVoucherCode,
                        c.PriceFlagId,
                        c.StoreId,
                        c.CompanyCMYKColors,
                        c.CompanyBannerSets,
                        c.PaymentGateways,
                        ProductCategories = c.ProductCategories.Where(pc => pc.ParentCategoryId == null),
                        c.MediaLibraries,
                        c.CompanyDomains,
                        c.CmsOffers,
                        c.CompanyCostCentres,
                        CompanyTerritories = c.CompanyTerritories.Take(1).ToList(),
                        Addresses = c.Addresses.Where(address => (!address.isArchived.HasValue || !address.isArchived.Value)).Take(1).ToList(),
                        CompanyContacts = c.CompanyContacts.Take(1).ToList(),
                        c.Image,
                        c.ActiveBannerSetId,
                        c.OrganisationId
                    }).ToList().Select(c => new Company
                    {
                        CompanyId = c.CompanyId,
                        Name = c.Name,
                        AccountNumber = c.AccountNumber,
                        URL = c.URL,
                        CreditReference = c.CreditReference,
                        CreditLimit = c.CreditLimit,
                        Terms = c.Terms,
                        TypeId = c.TypeId,
                        DefaultMarkUpId = c.DefaultMarkUpId,
                        AccountOpenDate = c.AccountOpenDate,
                        AccountManagerId = c.AccountManagerId,
                        DefaultNominalCode = c.DefaultNominalCode,
                        Status = c.Status,
                        IsCustomer = c.IsCustomer,
                        Notes = c.Notes,
                        IsDisabled = c.IsDisabled,
                        CurrentThemeId = c.CurrentThemeId,
                        AccountBalance = c.AccountBalance,
                        CreationDate = c.CreationDate,
                        VATRegNumber = c.VATRegNumber,
                        VATRegReference = c.VATRegReference,
                        FlagId = c.FlagId,
                        PhoneNo = c.PhoneNo,
                        IsDisplayDiscountVoucherCode = c.IsDisplayDiscountVoucherCode,
                        isWhiteLabel = c.isWhiteLabel,
                        IsGeneral = c.IsGeneral,
                        WebAccessCode = c.WebAccessCode,
                        isArchived = c.isArchived,
                        PayByPersonalCredeitCard = c.PayByPersonalCredeitCard,
                        PONumberRequired = c.PONumberRequired,
                        ShowPrices = c.ShowPrices,
                        isDisplayBanners = c.isDisplayBanners,
                        isDisplayMenuBar = c.isDisplayMenuBar,
                        isDisplaySecondaryPages = c.isDisplaySecondaryPages,
                        isAllowRegistrationFromWeb = c.isAllowRegistrationFromWeb,
                        isDisplayFeaturedProducts = c.isDisplayFeaturedProducts,
                        isDisplayPromotionalProducts = c.isDisplayPromotionalProducts,
                        isDisplaySiteFooter = c.isDisplaySiteFooter,
                        isDisplaySiteHeader = c.isDisplaySiteHeader,
                        isShowGoogleMap = c.isShowGoogleMap,
                        RedirectWebstoreURL = c.RedirectWebstoreURL,
                        isTextWatermark = c.isTextWatermark,
                        WatermarkText = c.WatermarkText,
                        MapImageUrl = c.MapImageUrl,
                        facebookAppId = c.facebookAppId,
                        facebookAppKey = c.facebookAppKey,
                        twitterAppId = c.twitterAppId,
                        twitterAppKey = c.twitterAppKey,
                        TwitterURL = c.TwitterURL,
                        isStoreModePrivate = c.isStoreModePrivate,
                        TaxPercentageId = c.TaxPercentageId,
                        canUserPlaceOrderWithoutApproval = c.canUserPlaceOrderWithoutApproval,
                        CanUserEditProfile = c.CanUserEditProfile,
                        SalesAndOrderManagerId1 = c.SalesAndOrderManagerId1,
                        SalesAndOrderManagerId2 = c.SalesAndOrderManagerId2,
                        ProductionManagerId1 = c.ProductionManagerId1,
                        ProductionManagerId2 = c.ProductionManagerId2,
                        StockNotificationManagerId1 = c.StockNotificationManagerId1,
                        StockNotificationManagerId2 = c.StockNotificationManagerId2,
                        IsDeliveryTaxAble = c.IsDeliveryTaxAble,
                        IsDisplayDeliveryOnCheckout = c.IsDisplayDeliveryOnCheckout,
                        isPaymentRequired = c.isPaymentRequired,
                        isIncludeVAT = c.isIncludeVAT,
                        includeEmailArtworkOrderReport = c.includeEmailArtworkOrderReport,
                        includeEmailArtworkOrderXML = c.includeEmailArtworkOrderXML,
                        includeEmailArtworkOrderJobCard = c.includeEmailArtworkOrderJobCard,
                        StoreBackgroundImage = c.StoreBackgroundImage,
                        makeEmailArtworkOrderProductionReady = c.makeEmailArtworkOrderProductionReady,
                        CompanyType = c.CompanyType,
                        PickupAddressId = c.PickupAddressId,
                        WebAnalyticCode = c.WebAnalyticCode,
                        WebMasterTag = c.WebMasterTag,
                        FacebookURL = c.FacebookURL,
                        LinkedinURL = c.LinkedinURL,
                        isCalculateTaxByService = c.isCalculateTaxByService,
                        TaxLabel = c.TaxLabel,
                        PriceFlagId = c.PriceFlagId,
                        TaxRate = c.TaxRate,
                        StoreId = c.StoreId,
                        Image = c.Image,
                        ActiveBannerSetId = c.ActiveBannerSetId,
                        OrganisationId = c.OrganisationId,
                        CompanyTerritories = c.CompanyTerritories.ToList(),
                        Addresses = c.Addresses.ToList(),
                        CompanyContacts = c.CompanyContacts.ToList()
                    }).FirstOrDefault();


                companyResponse.Company = company;
                return companyResponse;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        /// <summary>
        /// Get Companies list for Companies List View
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CompanyResponse SearchCompanies(CompanyRequestModel request)
        {
            try
            {
                int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;
                bool isStringSpecified = !string.IsNullOrEmpty(request.SearchString);
                bool isTypeSpecified = request.CustomerType != null;
                long type = request.CustomerType ?? 0;
                Expression<Func<Company, bool>> query =
                    s =>
                    ((!isStringSpecified || s.Name.Contains(request.SearchString)) && (isTypeSpecified && s.TypeId == type || !isTypeSpecified)) &&
                    s.OrganisationId == OrganisationId && 
                    (!s.isArchived.HasValue || !s.isArchived.Value) && 
                    (s.IsCustomer == 3 || s.IsCustomer == 4);

                int rowCount = DbSet.Count(query);
                IEnumerable<Company> companies = request.IsAsc
                    ? DbSet.Where(query)
                        .OrderBy(companyOrderByClause[CompanyByColumn.Name])
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : DbSet.Where(query)
                        .OrderByDescending(companyOrderByClause[CompanyByColumn.Name])
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();
                return new CompanyResponse
                {
                    RowCount = rowCount,
                    Companies = companies
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public CompanyResponse SearchCompaniesForCustomer(CompanyRequestModel request)
        {
            try
            {
                int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;
                bool isStringSpecified = !string.IsNullOrEmpty(request.SearchString);
                bool isTypeSpecified = request.CustomerType != null;
                long type = request.CustomerType ?? 0;
                int companyType = request.IsCustomer;
                Expression<Func<Company, bool>> query =
                    s =>
                    ((!isStringSpecified
                    || s.Name.Contains(request.SearchString)
                    || (s.CompanyContacts.FirstOrDefault(x => x.IsDefaultContact == 1) != null
                        && (
                            s.CompanyContacts.FirstOrDefault(x => x.IsDefaultContact == 1).FirstName.Contains(request.SearchString)
                            || s.CompanyContacts.FirstOrDefault(x => x.IsDefaultContact == 1).Email.Contains(request.SearchString)
                            || s.CompanyContacts.FirstOrDefault(x => x.IsDefaultContact == 1).LastName.Contains(request.SearchString))
                            )
                       )
                    && (isTypeSpecified && s.TypeId == type || !isTypeSpecified)) &&
                    (s.OrganisationId == OrganisationId && s.isArchived != true)
                    && ((companyType != 2 && (s.IsCustomer == companyType)) || (companyType == 2 && (s.IsCustomer == 0 || s.IsCustomer == 1)));

                int rowCount = DbSet.Count(query);
                IEnumerable<Company> companies = request.IsAsc
                    ? DbSet.Where(query)
                        .OrderBy(cmp => cmp.Name).ThenByDescending(cmp => cmp.CreationDate)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : DbSet.Where(query)
                      .OrderBy(cmp => cmp.Name).ThenByDescending(cmp => cmp.CreationDate)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();
                foreach(var comp in companies)
                {
                    comp.StoreName = GetStoreNameByStoreId(comp.StoreId ?? 0);
                }
                return new CompanyResponse
                {
                    RowCount = rowCount,
                    Companies = companies
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }


        public string GetStoreNameByStoreId(long StoreId)
        {
            try
            {
                return db.Companies.Where(c => c.CompanyId == StoreId).Select(c => c.Name).FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public CompanyResponse SearchCompaniesForCustomerOnDashboard(CompanyRequestModel request)
        {
            {
                try
                {
                    int fromRow = (request.PageNo - 1) * request.PageSize;
                    int toRow = request.PageSize;
                    bool isStringSpecified = !string.IsNullOrEmpty(request.SearchString);
                    bool isTypeSpecified = request.CustomerType != null;
                    long type = request.CustomerType ?? 0;
                    string userName = string.Empty;
                    int companyType = request.IsCustomer;
                    Expression<Func<Company, bool>> query =
                        s =>
                        ((!isStringSpecified
                        || s.Name.Contains(request.SearchString)
                        || (s.CompanyContacts.FirstOrDefault(x => x.IsDefaultContact == 1) != null
                            && (
                                s.CompanyContacts.FirstOrDefault(x => x.IsDefaultContact == 1).FirstName.Contains(request.SearchString)
                                || s.CompanyContacts.FirstOrDefault(x => x.IsDefaultContact == 1).Email.Contains(request.SearchString)
                                || s.CompanyContacts.FirstOrDefault(x => x.IsDefaultContact == 1).LastName.Contains(request.SearchString))
                                )
                           )
                        && (isTypeSpecified && s.TypeId == type || !isTypeSpecified)) &&
                        (s.OrganisationId == OrganisationId && s.isArchived != true) && s.TypeId != 53
                        && ((companyType != 2 && (s.IsCustomer == companyType)) || (companyType == 2 && (s.IsCustomer == 0 || s.IsCustomer == 1))) && (true);

                    int rowCount = DbSet.Count(query);
                    IEnumerable<Company> companies = 
                      
                        DbSet.Where(query)
                            .OrderByDescending(customer => customer.CompanyId).Take(5).ToList();

                    foreach (var comp in companies)
                    {
                        comp.StoreName = GetStoreNameByStoreId(comp.StoreId ?? 0);
                    }
                    return new CompanyResponse
                    {
                        RowCount = rowCount,
                        Companies = companies
                    };
                }
                catch (Exception ex)
                {
                    throw ex;

                }
            } 
        }

        /// <summary>
        /// Get Companies list for Supplier List View
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CompanyResponse SearchCompaniesForSupplier(CompanyRequestModel request)
        {
            try
            {
                int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;
                bool isStringSpecified = !string.IsNullOrEmpty(request.SearchString);
                bool isTypeSpecified = request.CustomerType != null;
                long type = request.CustomerType ?? 0;
                Expression<Func<Company, bool>> query =
                    s =>
                    ((!isStringSpecified
                    || s.Name.Contains(request.SearchString)
                    || (s.CompanyContacts.FirstOrDefault(x => x.IsDefaultContact == 1) != null && s.CompanyContacts.FirstOrDefault(x => x.IsDefaultContact == 1).Email.Contains(request.SearchString))
                    || (s.CompanyContacts.FirstOrDefault(x => x.IsDefaultContact == 1) != null && s.CompanyContacts.FirstOrDefault(x => x.IsDefaultContact == 1).FirstName.Contains(request.SearchString))
                    || (s.CompanyContacts.FirstOrDefault(x => x.IsDefaultContact == 1) != null && s.CompanyContacts.FirstOrDefault(x => x.IsDefaultContact == 1).LastName.Contains(request.SearchString))
                    )) &&
                    (s.OrganisationId == OrganisationId && s.isArchived != true) && (s.IsCustomer == 2);

                int rowCount = DbSet.Count(query);
                IEnumerable<Company> companies = request.IsAsc
                    ? DbSet.Where(query)
                        .OrderBy(supplier => supplier.Name).ThenByDescending(supp=> supp.CreationDate)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : DbSet.Where(query)
                       .OrderBy(supplier => supplier.Name).ThenByDescending(supp => supp.CreationDate)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();
                return new CompanyResponse
                {
                    RowCount = rowCount,
                    Companies = companies
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        /// <summary>
        /// Get Suppliers For Inventories
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SupplierSearchResponseForInventory GetSuppliersForInventories(SupplierRequestModelForInventory request)
        {
            try
            {
                int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;
                bool isStringSpecified = !string.IsNullOrEmpty(request.SearchString);
                Expression<Func<Company, bool>> query =
                    s =>
                        (isStringSpecified && (s.Name.Contains(request.SearchString)) ||
                         !isStringSpecified) && s.IsCustomer == 2 && s.OrganisationId == OrganisationId;

                int rowCount = DbSet.Count(query);
                IEnumerable<Company> companies =
                    DbSet.Where(query).OrderByDescending(x => x.Name)
                         .Skip(fromRow)
                        .Take(toRow)
                        .ToList();

                return new SupplierSearchResponseForInventory
                {
                    TotalCount = rowCount,
                    Suppliers = companies
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public Company GetStoreById(long companyId)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Companies.FirstOrDefault(c => c.CompanyId == companyId);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        public Company GetStoreByStoreId(long companyId)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = true;
                db.Configuration.ProxyCreationEnabled = true;
                return db.Companies.Where(c => c.CompanyId == companyId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;

            }
            //db.Configuration.LazyLoadingEnabled = false;
            //db.Configuration.ProxyCreationEnabled = false;
            // return db.Companies.Include("CompanyDomains").Include("CmsOffers").Include("MediaLibraries").Include("CompanyBannerSets").Include("CompanyBannerSets.CompanyBanners").Include("CmsPages").Include("RaveReviews").Include("CompanyTerritories").Include("Addresses").Include("CompanyContacts").Include("ProductCategories").Include("Items").Include("Items.ItemSections").Include("Items.ItemSections.SectionCostcentres").Include("Items.ItemSections.SectionCostcentres.SectionCostCentreResources").Include("PaymentGateways").Include("CmsSkinPageWidgets").Include("CompanyCostCentres").Include("CompanyCMYKColors").FirstOrDefault(c => c.CompanyId == companyId);

        }




        public ExportSets ExportRetailCompany(long CompanyId)
        {
            try
            {

                ExportSets sets = new ExportSets();

                sets.ExportRetailStore1 = ExportRetailCompany1(CompanyId, sets, true);
                sets.ExportRetailStore3 = ExportRetailCompany3(CompanyId, sets, 2);// 2 for retail store
                sets.ExportRetailStore2 = ExportRetailCompany2(CompanyId, sets, 2);// 2 for retail store
                // sets.ExportRetailStore4 = ExportRetailCompany4(CompanyId, sets, 2); // 2 for retail store

                return sets;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // export retail wihout products
        public ExportSets ExportRetailCompanyWithoutProducts(long CompanyId)
        {
            try
            {

                ExportSets sets = new ExportSets();

                sets.ExportRetailStore1WOP = ExportRetailCompany1(CompanyId, sets, false);
                sets.ExportRetailStore3WOP = ExportRetailCompany3(CompanyId, sets, 4);// 4 for retail store without products
                sets.ExportRetailStore2WOP = ExportRetailCompany2(CompanyId, sets, 4);// 4 for retail store without products
                //  sets.ExportRetailStore4WOP = ExportRetailCompany4(CompanyId, sets, 4); // 4 for retail store without products

                return sets;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ExportOrganisation ExportRetailCompany1(long CompanyId, ExportSets Sets, bool isWithProducts)
        {
            try
            {
                List<TemplateColorStyle> TemplateColorStyle = new List<TemplateColorStyle>();
                ExportOrganisation ObjExportOrg = new ExportOrganisation();
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;

                Mapper.CreateMap<Company, Company>()
             .ForMember(x => x.Activities, opt => opt.Ignore())
                .ForMember(x => x.ColorPalletes, opt => opt.Ignore())
                .ForMember(x => x.Estimates, opt => opt.Ignore())
                .ForMember(x => x.Invoices, opt => opt.Ignore())
                .ForMember(x => x.Items, opt => opt.Ignore())
                .ForMember(x => x.ProductCategories, opt => opt.Ignore());

                Mapper.CreateMap<CompanyDomain, CompanyDomain>()
                .ForMember(x => x.Company, opt => opt.Ignore());

                Mapper.CreateMap<CmsOffer, CmsOffer>()
               .ForMember(x => x.Company, opt => opt.Ignore());

                Mapper.CreateMap<CmsPage, CmsPage>()
                .ForMember(x => x.CmsPageTags, opt => opt.Ignore())
                .ForMember(x => x.PageCategory, opt => opt.Ignore())
                .ForMember(x => x.CmsSkinPageWidgets, opt => opt.Ignore())
                .ForMember(x => x.Company, opt => opt.Ignore());

                Mapper.CreateMap<MediaLibrary, MediaLibrary>()
              .ForMember(x => x.Company, opt => opt.Ignore());

                Mapper.CreateMap<CompanyBannerSet, CompanyBannerSet>()
            .ForMember(x => x.Company, opt => opt.Ignore());

                Mapper.CreateMap<CompanyBanner, CompanyBanner>()
             .ForMember(x => x.CompanyBannerSet, opt => opt.Ignore());


                Mapper.CreateMap<RaveReview, RaveReview>()
                 .ForMember(x => x.Company, opt => opt.Ignore());

                Mapper.CreateMap<CompanyTerritory, CompanyTerritory>()
              .ForMember(x => x.Addresses, opt => opt.Ignore())
              .ForMember(x => x.Company, opt => opt.Ignore())
              .ForMember(x => x.CompanyContacts, opt => opt.Ignore());


                Mapper.CreateMap<Address, Address>()
             .ForMember(x => x.Company, opt => opt.Ignore())
             .ForMember(x => x.CompanyContacts, opt => opt.Ignore())
             .ForMember(x => x.CompanyTerritory, opt => opt.Ignore())
             .ForMember(x => x.ShippingCompanyContacts, opt => opt.Ignore())
             .ForMember(x => x.State, opt => opt.Ignore())
             .ForMember(x => x.Country, opt => opt.Ignore());

                Mapper.CreateMap<CompanyContact, CompanyContact>()
            .ForMember(x => x.Company, opt => opt.Ignore())
            .ForMember(x => x.Address, opt => opt.Ignore())
             .ForMember(x => x.CompanyTerritory, opt => opt.Ignore())
              .ForMember(x => x.Estimates, opt => opt.Ignore())
              .ForMember(x => x.Inquiries, opt => opt.Ignore())
               .ForMember(x => x.Invoices, opt => opt.Ignore())
                .ForMember(x => x.NewsLetterSubscribers, opt => opt.Ignore())
                .ForMember(x => x.ShippingAddress, opt => opt.Ignore());


                Mapper.CreateMap<Campaign, Campaign>()
                   .ForMember(x => x.Company, opt => opt.Ignore());

                Mapper.CreateMap<PaymentGateway, PaymentGateway>()
                  .ForMember(x => x.Company, opt => opt.Ignore())
                  .ForMember(x => x.PaymentMethod, opt => opt.Ignore());

                Mapper.CreateMap<CmsSkinPageWidget, CmsSkinPageWidget>()
                  .ForMember(x => x.Company, opt => opt.Ignore())
                  .ForMember(x => x.Organisation, opt => opt.Ignore())
                  .ForMember(x => x.Widget, opt => opt.Ignore())
                  .ForMember(x => x.CmsPage, opt => opt.Ignore());


                Mapper.CreateMap<CmsSkinPageWidgetParam, CmsSkinPageWidgetParam>()
                 .ForMember(x => x.CmsSkinPageWidget, opt => opt.Ignore());

                Mapper.CreateMap<CompanyCostCentre, CompanyCostCentre>()
                  .ForMember(x => x.Company, opt => opt.Ignore())
                  .ForMember(x => x.CostCentre, opt => opt.Ignore());


                Mapper.CreateMap<CompanyCMYKColor, CompanyCMYKColor>()
                  .ForMember(x => x.Company, opt => opt.Ignore());


                Mapper.CreateMap<SmartForm, SmartForm>()
                  .ForMember(x => x.Company, opt => opt.Ignore());

                Mapper.CreateMap<SmartFormDetail, SmartFormDetail>()
                .ForMember(x => x.SmartForm, opt => opt.Ignore());

                Mapper.CreateMap<FieldVariable, FieldVariable>()
                    .ForMember(x => x.SmartFormDetails, opt => opt.Ignore())
                  .ForMember(x => x.Company, opt => opt.Ignore());

                Mapper.CreateMap<TemplateColorStyle, TemplateColorStyle>()
                   .ForMember(x => x.Company, opt => opt.Ignore())
                 .ForMember(x => x.Template, opt => opt.Ignore());

                db.Database.CommandTimeout = 1080;

                Company ObjCompany = db.Companies.Include("CompanyDomains").Include("CmsOffers").Include("MediaLibraries").Include("CompanyBannerSets.CompanyBanners").Include("RaveReviews").Include("CompanyTerritories").Include("Addresses").Include("CompanyContacts").Include("Campaigns").Include("PaymentGateways").Include("CompanyCostCentres").Include("CompanyCmykColors").Include("SmartForms.SmartFormDetails").Include("FieldVariables").Where(c => c.CompanyId == CompanyId).FirstOrDefault();


                //Include("CmsSkinPageWidgets")

                List<CmsSkinPageWidget> widgets = db.PageWidgets.Include("CmsSkinPageWidgetParams").Where(c => c.CompanyId == CompanyId && c.PageId != null).ToList();

                if (widgets != null && widgets.Count > 0)
                {
                    ObjCompany.CmsSkinPageWidgets = widgets;
                }

                List<CmsPage> pages = db.CmsPages.Where(c => c.CompanyId == CompanyId).ToList();

                if (pages != null && pages.Count > 0)
                {
                    ObjCompany.CmsPages = pages;
                }
                //  template color style
                List<TemplateColorStyle> lstTemplateColorStyle = db.TemplateColorStyles.Where(c => c.CustomerId == CompanyId).ToList();
                if (lstTemplateColorStyle != null && lstTemplateColorStyle.Count > 0)
                {
                    ObjCompany.TemplateColorStyles = lstTemplateColorStyle;

                }



                var omappedCompany = Mapper.Map<Company, Company>(ObjCompany);

                ObjExportOrg.RetailCompany = omappedCompany;



                //  template color style
                //List<TemplateColorStyle> lstTemplateColorStyle = db.TemplateColorStyles.Where(c => c.CustomerId == CompanyId).ToList();
                //if (lstTemplateColorStyle != null && lstTemplateColorStyle.Count > 0)
                //{
                //    foreach (var tempStyle in lstTemplateColorStyle)
                //    {
                //        TemplateColorStyle.Add(tempStyle);
                //    }

                //}

                //ObjExportOrg.RetailTemplateColorStyle = TemplateColorStyle;

                List<TemplateFont> templateFonts = new List<TemplateFont>();
                List<TemplateFont> lstTemplateFonts = db.TemplateFonts.Where(c => c.CustomerId == CompanyId).ToList();
                if (lstTemplateFonts != null && lstTemplateFonts.Count > 0)
                {
                    foreach (var tempFonts in lstTemplateFonts)
                    {
                        templateFonts.Add(tempFonts);
                    }

                }

                ObjExportOrg.RetailTemplateFonts = templateFonts;

                //Mapper.CreateMap<DiscountVoucher, DiscountVoucher>();
            
                //List<DiscountVoucher> DiscountVouchers = new List<DiscountVoucher>();
                //List<DiscountVoucher> lstDiscountVouchers = db.DiscountVouchers.Include("ProductCategoryVouchers").Include("ItemsVouchers").Where(c => c.CustomerId == CompanyId).ToList();

                //if (lstDiscountVouchers != null && lstDiscountVouchers.Count > 0)
                //{
                //    foreach (var vouch in lstDiscountVouchers)
                //    {
                       
                //        var omappedItem = Mapper.Map<DiscountVoucher, DiscountVoucher>(vouch);
                //        DiscountVouchers.Add(omappedItem);
                //    }
                //}
                //ObjExportOrg.DiscountVouchers = DiscountVouchers;



                string JsonRetail = JsonConvert.SerializeObject(ObjExportOrg, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                // export json file
                if (isWithProducts)
                {
                    string sRetailPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/RetailJson1.txt";
                    System.IO.File.WriteAllText(sRetailPath, JsonRetail);
                }
                else
                {
                    string sRetailPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/RetailJson1WOP.txt";
                    System.IO.File.WriteAllText(sRetailPath, JsonRetail);
                }

                Sets.ExportRetailStore1 = ObjExportOrg;
                ObjExportOrg = null;
                JsonRetail = string.Empty;
                GC.Collect();
                return Sets.ExportRetailStore1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductCategory> ExportRetailCompany2(long CompanyId, ExportSets Sets, int StoreType)
        {
            try
            {

                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;

                List<ProductCategory> productCategories = new List<ProductCategory>();

                Mapper.CreateMap<ProductCategory, ProductCategory>()
               .ForMember(x => x.Company, opt => opt.Ignore())
               .ForMember(x => x.ProductCategoryItems, opt => opt.Ignore());

                Mapper.CreateMap<CategoryTerritory, CategoryTerritory>()
              .ForMember(x => x.ProductCategory, opt => opt.Ignore());

                List<ProductCategory> categories = db.ProductCategories.Include("CategoryTerritories").Where(s => s.isArchived != true && s.CompanyId == CompanyId).ToList();
                //categories.ToList().ForEach(p => p.Company = null);
                //categories.ToList().ForEach(p => p.ProductCategoryItems = null);
                //productCategories = categories;

                List<ProductCategory> oOutputProdCat = new List<ProductCategory>();

                if (categories != null && categories.Count > 0)
                {
                    foreach (var cat in categories)
                    {
                        var omappedItem = Mapper.Map<ProductCategory, ProductCategory>(cat);
                        oOutputProdCat.Add(omappedItem);
                    }
                }

                string JsonRetail = JsonConvert.SerializeObject(oOutputProdCat, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                if (StoreType == 1) // corporate
                {
                    string sRetailPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/CorporateProductCategories.txt";
                    System.IO.File.WriteAllText(sRetailPath, JsonRetail);
                }
                else if (StoreType == 2) // retail
                {
                    string sRetailPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/RetailProductCategories.txt";
                    System.IO.File.WriteAllText(sRetailPath, JsonRetail);
                }
                else if (StoreType == 3) // corporate without products
                {
                    string sRetailPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/CorporateProductCategoriesWOP.txt";
                    System.IO.File.WriteAllText(sRetailPath, JsonRetail);
                }
                else if (StoreType == 4) // retail without products
                {
                    string sRetailPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/RetailProductCategoriesWOP.txt";
                    System.IO.File.WriteAllText(sRetailPath, JsonRetail);
                }


                JsonRetail = string.Empty;
                GC.Collect();
                return oOutputProdCat;

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public List<Item> ExportRetailCompany3(long CompanyId, ExportSets Sets, int StoreType)
        {

            try
            {
                // Item Mapper
                ExportOrganisation ObjExportOrg = new ExportOrganisation();

                Mapper.CreateMap<Item, Item>()
                   .ForMember(x => x.Company, opt => opt.Ignore())
                   .ForMember(x => x.ItemAttachments, opt => opt.Ignore())
                   .ForMember(x => x.Estimate, opt => opt.Ignore())
                   .ForMember(x => x.Invoice, opt => opt.Ignore())
                   .ForMember(x => x.DeliveryNoteDetails, opt => opt.Ignore());


                Mapper.CreateMap<ItemSection, ItemSection>()
                .ForMember(x => x.Item, opt => opt.Ignore())
                .ForMember(x => x.StockItem, opt => opt.Ignore())
                .ForMember(x => x.Machine, opt => opt.Ignore())
                .ForMember(x => x.MachineSide2, opt => opt.Ignore());

                Mapper.CreateMap<SectionCostcentre, SectionCostcentre>()
                .ForMember(x => x.CostCentre, opt => opt.Ignore());

                Mapper.CreateMap<SectionCostCentreResource, SectionCostCentreResource>();


                Mapper.CreateMap<ItemSection, ItemSection>()
            .ForMember(x => x.Item, opt => opt.Ignore())
            .ForMember(x => x.StockItem, opt => opt.Ignore())
            .ForMember(x => x.Machine, opt => opt.Ignore());

                Mapper.CreateMap<ItemStockOption, ItemStockOption>()
              .ForMember(x => x.Item, opt => opt.Ignore())
              .ForMember(x => x.StockItem, opt => opt.Ignore());


                Mapper.CreateMap<ItemAddonCostCentre, ItemAddonCostCentre>()
            .ForMember(x => x.CostCentre, opt => opt.Ignore())
            .ForMember(x => x.ItemStockOption, opt => opt.Ignore());



                Mapper.CreateMap<ProductCategoryItem, ProductCategoryItem>()
            .ForMember(x => x.Item, opt => opt.Ignore())
            .ForMember(x => x.ProductCategory, opt => opt.Ignore());

                Mapper.CreateMap<ItemVdpPrice, ItemVdpPrice>()
                    .ForMember(x => x.Item, opt => opt.Ignore());

                Mapper.CreateMap<ItemVideo, ItemVideo>()
                    .ForMember(x => x.Item, opt => opt.Ignore());


                Mapper.CreateMap<ItemRelatedItem, ItemRelatedItem>()
                    .ForMember(x => x.Item, opt => opt.Ignore())
                    .ForMember(x => x.RelatedItem, opt => opt.Ignore());

                Mapper.CreateMap<ItemImage, ItemImage>()
               .ForMember(x => x.Item, opt => opt.Ignore());


                Mapper.CreateMap<ItemStateTax, ItemStateTax>()
               .ForMember(x => x.Item, opt => opt.Ignore())
               .ForMember(x => x.State, opt => opt.Ignore())
               .ForMember(x => x.Country, opt => opt.Ignore());

                Mapper.CreateMap<ItemPriceMatrix, ItemPriceMatrix>()
                  .ForMember(x => x.Item, opt => opt.Ignore());

                Mapper.CreateMap<ItemProductDetail, ItemProductDetail>()
               .ForMember(x => x.Item, opt => opt.Ignore());

                Mapper.CreateMap<Template, Template>()
                    .ForMember(x => x.Items, opt => opt.Ignore());

                Mapper.CreateMap<TemplatePage, TemplatePage>()
                .ForMember(x => x.Template, opt => opt.Ignore());

                Mapper.CreateMap<TemplateObject, TemplateObject>()
              .ForMember(x => x.Template, opt => opt.Ignore());

                Mapper.CreateMap<TemplateBackgroundImage, TemplateBackgroundImage>()
                      .ForMember(x => x.Template, opt => opt.Ignore());

                Mapper.CreateMap<ImagePermission, ImagePermission>()
                  .ForMember(x => x.TemplateBackgroundImage, opt => opt.Ignore());

                db.Database.CommandTimeout = 1080;

                List<Item> items = db.Items.Include("ItemSections.SectionCostcentres.SectionCostCentreResources").Include("ItemStockOptions.ItemAddonCostCentres").Include("ProductCategoryItems").Include("ItemVdpPrices").Include("ItemPriceMatrices").Include("ItemProductDetails").Include("ItemStateTaxes").Include("ItemImages").Include("ItemRelatedItems").Include("ItemVideos").Include("Template.TemplatePages").Include("Template.TemplateObjects").Include("Template.TemplateBackgroundImages.ImagePermissions").Where(i => i.IsArchived != true && i.CompanyId == CompanyId && i.EstimateId == null).ToList();
                List<Item> oOutputItems = new List<Item>();

                if (items != null && items.Count > 0)
                {
                    foreach (var item in items)
                    {
                        var omappedItem = Mapper.Map<Item, Item>(item);
                        oOutputItems.Add(omappedItem);
                    }
                }



                string jsonRetail = JsonConvert.SerializeObject(oOutputItems, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

                if (StoreType == 1)// for corporate
                {
                    string sCorpPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/CorporateJson2.txt";
                    System.IO.File.WriteAllText(sCorpPath, jsonRetail);

                }
                else if (StoreType == 2)// for retail
                {
                    string sRetailPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/RetailJson2.txt";
                    System.IO.File.WriteAllText(sRetailPath, jsonRetail);

                }
                else if (StoreType == 3)// for corporate without products
                {
                    string sRetailPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/CorporateJson2WOP.txt";
                    System.IO.File.WriteAllText(sRetailPath, jsonRetail);

                }
                else if (StoreType == 4)// for retail without products
                {
                    string sRetailPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/RetailJson2WOP.txt";
                    System.IO.File.WriteAllText(sRetailPath, jsonRetail);

                }

                jsonRetail = string.Empty;
                Sets.ExportRetailStore3 = oOutputItems;
                //ObjExportOrg = null;
                return Sets.ExportRetailStore3;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CmsPage> ExportRetailCompany4(long CompanyId, ExportSets sets, int StoreType)
        {
            try
            {


                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;

                Mapper.CreateMap<CmsPage, CmsPage>()
                .ForMember(x => x.Company, opt => opt.Ignore())
                .ForMember(x => x.PageCategory, opt => opt.Ignore())
                .ForMember(x => x.CmsSkinPageWidgets, opt => opt.Ignore());

                List<CmsPage> pages = db.CmsPages.Where(c => c.CompanyId == CompanyId).ToList();

                //pages.ToList().ForEach(s => s.CmsSkinPageWidgets = null);
                //pages.ToList().ForEach(s => s.PageCategory = null);
                //pages.ToList().ForEach(s => s.Company = null);

                List<CmsPage> oOutputCMSPage = new List<CmsPage>();

                if (pages != null && pages.Count > 0)
                {
                    foreach (var pag in pages)
                    {
                        var omappedItem = Mapper.Map<CmsPage, CmsPage>(pag);
                        oOutputCMSPage.Add(omappedItem);
                    }
                }

                string JsonRetail = JsonConvert.SerializeObject(oOutputCMSPage, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

                if (StoreType == 1) // for corporate
                {
                    string CorpPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/CorporateSecondaryPages.txt";
                    System.IO.File.WriteAllText(CorpPath, JsonRetail);

                }
                else if (StoreType == 2) // for retail
                {
                    string sRetailPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/RetailSecondaryPages.txt";
                    System.IO.File.WriteAllText(sRetailPath, JsonRetail);


                }
                else if (StoreType == 3) // for corporate without products
                {
                    string sRetailPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/CorporateSecondaryPagesWOP.txt";
                    System.IO.File.WriteAllText(sRetailPath, JsonRetail);


                }
                else if (StoreType == 4) // for retail without products
                {
                    string sRetailPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/RetailSecondaryPagesWOP.txt";
                    System.IO.File.WriteAllText(sRetailPath, JsonRetail);


                }

                JsonRetail = string.Empty;
                GC.Collect();

                return oOutputCMSPage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExportSets ExportCorporateCompany(long CompanyId)
        {

            ExportOrganisation ObjExportOrg = new ExportOrganisation();
            try
            {


                ExportSets sets = new ExportSets();

                sets.ExportStore1 = ExportCorporateCompany1(CompanyId, sets, true);
                sets.ExportStore3 = ExportRetailCompany3(CompanyId, sets, 1);// 1 to make coporate json file
                sets.ExportStore2 = ExportRetailCompany2(CompanyId, sets, 1);// 1 to make coporate json file
                //sets.ExportStore4 = ExportRetailCompany4(CompanyId, sets, 1);// 1 to make coporate json file

                return sets;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ExportSets ExportCorporateCompanyWithoutProducts(long CompanyId)
        {

            ExportOrganisation ObjExportOrg = new ExportOrganisation();
            try
            {


                ExportSets sets = new ExportSets();

                sets.ExportStore1WOP = ExportCorporateCompany1(CompanyId, sets, false);
                sets.ExportStore3WOP = ExportRetailCompany3(CompanyId, sets, 3); // 3 to make corporate json without products
                sets.ExportStore2WOP = ExportRetailCompany2(CompanyId, sets, 3); // 3 to make corporate json without products
                // sets.ExportStore4WOP = ExportRetailCompany4(CompanyId, sets, 3); // 3 to make corporate json without products

                return sets;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ExportOrganisation ExportCorporateCompany1(long CompanyId, ExportSets Sets, bool isWithProducts)
        {
            try
            {
                List<TemplateColorStyle> TemplateColorStyle = new List<TemplateColorStyle>();
                ExportOrganisation ObjExportOrg = new ExportOrganisation();
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;

                Mapper.CreateMap<Company, Company>()
             .ForMember(x => x.Activities, opt => opt.Ignore())
                .ForMember(x => x.ColorPalletes, opt => opt.Ignore())
                .ForMember(x => x.Estimates, opt => opt.Ignore())
                .ForMember(x => x.Invoices, opt => opt.Ignore())
                .ForMember(x => x.Items, opt => opt.Ignore())
                .ForMember(x => x.ProductCategories, opt => opt.Ignore());

                Mapper.CreateMap<CompanyDomain, CompanyDomain>()
                .ForMember(x => x.Company, opt => opt.Ignore());

                Mapper.CreateMap<CmsOffer, CmsOffer>()
               .ForMember(x => x.Company, opt => opt.Ignore());



                Mapper.CreateMap<CmsPage, CmsPage>()
                    .ForMember(x => x.CmsPageTags, opt => opt.Ignore())
                    .ForMember(x => x.PageCategory, opt => opt.Ignore())
                    .ForMember(x => x.CmsSkinPageWidgets, opt => opt.Ignore())
                    .ForMember(x => x.Company, opt => opt.Ignore());

                Mapper.CreateMap<MediaLibrary, MediaLibrary>()
              .ForMember(x => x.Company, opt => opt.Ignore());

                Mapper.CreateMap<CompanyBannerSet, CompanyBannerSet>()
            .ForMember(x => x.Company, opt => opt.Ignore());

                Mapper.CreateMap<CompanyBanner, CompanyBanner>()
             .ForMember(x => x.CompanyBannerSet, opt => opt.Ignore());


                Mapper.CreateMap<RaveReview, RaveReview>()
                 .ForMember(x => x.Company, opt => opt.Ignore());

                Mapper.CreateMap<CompanyTerritory, CompanyTerritory>()
              .ForMember(x => x.Addresses, opt => opt.Ignore())
              .ForMember(x => x.Company, opt => opt.Ignore())
              .ForMember(x => x.CompanyContacts, opt => opt.Ignore());


                Mapper.CreateMap<Address, Address>()
             .ForMember(x => x.Company, opt => opt.Ignore())
             .ForMember(x => x.CompanyContacts, opt => opt.Ignore())
             .ForMember(x => x.CompanyTerritory, opt => opt.Ignore())
             .ForMember(x => x.ShippingCompanyContacts, opt => opt.Ignore())
             .ForMember(x => x.State, opt => opt.Ignore())
             .ForMember(x => x.Country, opt => opt.Ignore());

                Mapper.CreateMap<CompanyContact, CompanyContact>()
            .ForMember(x => x.Company, opt => opt.Ignore())
            .ForMember(x => x.Address, opt => opt.Ignore())
             .ForMember(x => x.CompanyTerritory, opt => opt.Ignore())
              .ForMember(x => x.Estimates, opt => opt.Ignore())
              .ForMember(x => x.Inquiries, opt => opt.Ignore())
               .ForMember(x => x.Invoices, opt => opt.Ignore())
                .ForMember(x => x.NewsLetterSubscribers, opt => opt.Ignore())
                .ForMember(x => x.ShippingAddress, opt => opt.Ignore());


                Mapper.CreateMap<Campaign, Campaign>()
                   .ForMember(x => x.Company, opt => opt.Ignore());

                Mapper.CreateMap<PaymentGateway, PaymentGateway>()
                  .ForMember(x => x.Company, opt => opt.Ignore())
                  .ForMember(x => x.PaymentMethod, opt => opt.Ignore());

                Mapper.CreateMap<CmsSkinPageWidget, CmsSkinPageWidget>()
                  .ForMember(x => x.Company, opt => opt.Ignore())
                  .ForMember(x => x.Organisation, opt => opt.Ignore())
                  .ForMember(x => x.Widget, opt => opt.Ignore())
                  .ForMember(x => x.CmsPage, opt => opt.Ignore());

                Mapper.CreateMap<CmsSkinPageWidgetParam, CmsSkinPageWidgetParam>()
                    .ForMember(x => x.CmsSkinPageWidget, opt => opt.Ignore());

                Mapper.CreateMap<CompanyCostCentre, CompanyCostCentre>()
                  .ForMember(x => x.Company, opt => opt.Ignore())
                  .ForMember(x => x.CostCentre, opt => opt.Ignore());


                Mapper.CreateMap<CompanyCMYKColor, CompanyCMYKColor>()
                  .ForMember(x => x.Company, opt => opt.Ignore());


                Mapper.CreateMap<SmartForm, SmartForm>()
                  .ForMember(x => x.Company, opt => opt.Ignore());

                Mapper.CreateMap<SmartFormDetail, SmartFormDetail>()
                .ForMember(x => x.SmartForm, opt => opt.Ignore());

                Mapper.CreateMap<FieldVariable, FieldVariable>()
                    .ForMember(x => x.SmartFormDetails, opt => opt.Ignore())
                  .ForMember(x => x.Company, opt => opt.Ignore());

                Mapper.CreateMap<TemplateColorStyle, TemplateColorStyle>()
             .ForMember(x => x.Template, opt => opt.Ignore())
             .ForMember(x => x.Company, opt => opt.Ignore());

                db.Database.CommandTimeout = 1080;

                Company ObjCompany = db.Companies.Include("CompanyDomains").Include("CmsOffers").Include("MediaLibraries").Include("CompanyBannerSets.CompanyBanners").Include("RaveReviews").Include("CompanyTerritories").Include("Addresses").Include("CompanyContacts").Include("Campaigns").Include("PaymentGateways").Include("CompanyCostCentres").Include("CompanyCmykColors").Include("SmartForms.SmartFormDetails").Include("FieldVariables").Where(c => c.CompanyId == CompanyId).FirstOrDefault();


                //Include("CmsSkinPageWidgets")

                List<CmsSkinPageWidget> widgets = db.PageWidgets.Include("CmsSkinPageWidgetParams").Where(c => c.CompanyId == CompanyId && c.PageId != null).ToList();
                List<CmsPage> pages = db.CmsPages.Where(c => c.CompanyId == CompanyId).ToList();
                if (widgets != null && widgets.Count > 0)
                {
                    ObjCompany.CmsSkinPageWidgets = widgets;
                }
                if (pages != null && pages.Count > 0)
                {
                    ObjCompany.CmsPages = pages;
                }
                List<TemplateColorStyle> lstTemplateColorStyle = db.TemplateColorStyles.Where(c => c.CustomerId == CompanyId).ToList();
                if (lstTemplateColorStyle != null && lstTemplateColorStyle.Count > 0)
                {
                    ObjCompany.TemplateColorStyles = lstTemplateColorStyle;

                }

                var omappedCompany = Mapper.Map<Company, Company>(ObjCompany);

                ObjExportOrg.Company = omappedCompany;


                //  template color style


               // ObjExportOrg.TemplateColorStyle = lstTemplateColorStyle;
                List<TemplateFont> templateFonts = new List<TemplateFont>();
                List<TemplateFont> lstTemplateFonts = db.TemplateFonts.Where(c => c.CustomerId == CompanyId).ToList();
                if (lstTemplateFonts != null && lstTemplateFonts.Count > 0)
                {
                    foreach (var tempFonts in lstTemplateFonts)
                    {
                        templateFonts.Add(tempFonts);
                    }

                }
                ObjExportOrg.TemplateFonts = templateFonts;



                //Mapper.CreateMap<DiscountVoucher, DiscountVoucher>();
                
                //List<DiscountVoucher> DiscountVouchers = new List<DiscountVoucher>();
                //List<DiscountVoucher> lstDiscountVouchers = db.DiscountVouchers.Include("ProductCategoryVouchers").Include("ItemsVouchers").Where(c => c.CustomerId == CompanyId).ToList();

                //if (lstDiscountVouchers != null && lstDiscountVouchers.Count > 0)
                //{
                //    foreach (var vouch in lstDiscountVouchers)
                //    {
                //        var omappedItem = Mapper.Map<DiscountVoucher, DiscountVoucher>(vouch);
                //        DiscountVouchers.Add(omappedItem);
                //    }
                //}
                //ObjExportOrg.DiscountVouchers = DiscountVouchers;



                TemplateColorStyle = null;
                templateFonts = null;
                string JsonRetail = JsonConvert.SerializeObject(ObjExportOrg, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                // export json file
                if (isWithProducts)
                {
                    string sRetailPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/CorporateJson1.txt";
                    System.IO.File.WriteAllText(sRetailPath, JsonRetail);
                }
                else
                {
                    string sRetailPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/CorporateJson1WOP.txt";
                    System.IO.File.WriteAllText(sRetailPath, JsonRetail);
                }

                Sets.ExportRetailStore1 = ObjExportOrg;
                ObjExportOrg = null;
                JsonRetail = string.Empty;
                GC.Collect();
                return Sets.ExportRetailStore1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // export retail company 


        public long GetCompanyByName(long OID, string Name)
        {
            try
            {
                return db.Companies.Where(o => o.OrganisationId == OID && o.Name == Name).Select(c => c.CompanyId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Company Price Flag id for Price Matrix in webstore
        /// </summary>
        public int? GetPriceFlagIdByCompany(long CompanyId)
        {
            return DbSet.Where(c => c.CompanyId == CompanyId).Select(f => f.PriceFlagId).FirstOrDefault();
        }

        /// <summary>
        /// Get All Suppliers For Current Organisation
        /// </summary>
        public IEnumerable<Company> GetAllSuppliers()
        {
            try
            {
                return DbSet.Where(supplier => supplier.OrganisationId == OrganisationId && 
                    supplier.IsCustomer == (int)CustomerTypes.Suppliers &&
                    (!supplier.isArchived.HasValue || !supplier.isArchived.Value)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public long CreateCustomer(string CompanyName, bool isEmailSubscriber, bool isNewsLetterSubscriber, CompanyTypes customerType, string RegWithSocialMedia, long OrganisationId, long StoreId, CompanyContact contact = null)
        {
            try
            {
                //bool isCreateTemporaryCompany = true;
                //if ((int)customerType == (int)CompanyTypes.TemporaryCustomer)
                //{
                //    Company ContactCompany = db.Companies.Where(c => c.TypeId == (int)customerType && c.OrganisationId == OrganisationId).FirstOrDefault();
                //    if (ContactCompany != null)
                //    {
                //        isCreateTemporaryCompany = false;
                //        return ContactCompany.CompanyId;
                //    }
                //    else
                //    {
                //        isCreateTemporaryCompany = true;
                //    }

                //}

                //if (isCreateTemporaryCompany)
                //{
                Address Contactaddress = null;

                CompanyTerritory ContactTerritory = null;

                CompanyContact ContactPerson = null;

                long customerID = 0;


                Company ContactCompany = new Company();

                ContactCompany.isArchived = false;

                ContactCompany.AccountNumber = "123";

                ContactCompany.AccountOpenDate = DateTime.Now;

                ContactCompany.Name = CompanyName;

                ContactCompany.TypeId = (int)customerType;

                ContactCompany.Status = 0;

                if (contact != null && !string.IsNullOrEmpty(contact.Mobile))
                    ContactCompany.PhoneNo = contact.Mobile;

                ContactCompany.CreationDate = DateTime.Now;

                ContactCompany.CreditLimit = 0;

                ContactCompany.IsCustomer = 0; //prospect

                ContactCompany.OrganisationId = OrganisationId;

                ContactCompany.StoreId = StoreId;

                

                Markup OrgMarkup = db.Markups.Where(m => m.OrganisationId == OrganisationId && m.IsDefault == true).FirstOrDefault();

                if (OrgMarkup != null)
                {
                    ContactCompany.DefaultMarkUpId = (int)OrgMarkup.MarkUpId;
                }
                else
                {
                    ContactCompany.DefaultMarkUpId = 0;
                }


                //Create Customer
                db.Companies.Add(ContactCompany);

                ContactTerritory = PopulateTerritoryObject(ContactCompany.CompanyId);
                db.CompanyTerritories.Add(ContactTerritory);
                //Create Billing Address and Delivery Address and mark them default billing and shipping
                Contactaddress = PopulateAddressObject(0, ContactCompany.CompanyId, true, true, ContactTerritory.TerritoryId);
                db.Addesses.Add(Contactaddress);


                //Create Contact
                ContactPerson = PopulateContactsObject(ContactCompany.CompanyId, Contactaddress.AddressId, true);
                ContactPerson.isArchived = false;

                if (contact != null)
                {
                    ContactPerson.FirstName = contact.FirstName;
                    ContactPerson.LastName = contact.LastName;
                    ContactPerson.Email = contact.Email;
                    ContactPerson.Mobile = contact.Mobile;
                    ContactPerson.Password = ComputeHashSHA1(contact.Password);
                    ContactPerson.QuestionId = 1;
                    ContactPerson.SecretAnswer = "";
                    ContactPerson.ClaimIdentifer = contact.ClaimIdentifer;
                    ContactPerson.AuthentifiedBy = contact.AuthentifiedBy;
                    //Quick Text Fields
                    ContactPerson.quickAddress1 = contact.quickAddress1;
                    ContactPerson.quickAddress2 = contact.quickAddress2;
                    ContactPerson.quickAddress3 = contact.quickAddress3;
                    ContactPerson.quickCompanyName = contact.quickCompanyName;
                    ContactPerson.quickCompMessage = contact.quickCompMessage;
                    ContactPerson.quickEmail = contact.quickEmail;
                    ContactPerson.quickFax = contact.quickFax;
                    ContactPerson.quickFullName = contact.quickFullName;
                    ContactPerson.quickPhone = contact.quickPhone;
                    ContactPerson.quickTitle = contact.quickTitle;
                    ContactPerson.quickWebsite = contact.quickWebsite;
                    ContactPerson.TerritoryId = ContactTerritory.TerritoryId;
                    ContactPerson.OrganisationId = OrganisationId;
                    ContactPerson.IsPricingshown = true;
                    if (!string.IsNullOrEmpty(RegWithSocialMedia))
                    {
                        ContactPerson.twitterScreenName = RegWithSocialMedia;
                    }


                }

                db.CompanyContacts.Add(ContactPerson);

                if (db.SaveChanges() > 0)
                {
                    customerID = ContactCompany.CompanyId; // customer id
                    if (contact != null)
                    {
                        contact.ContactId = ContactPerson.ContactId;
                        contact.CompanyId = customerID;
                    }
                }

                return customerID;
                //}
                //else
                //{
                //    return 0;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private CompanyContact PopulateContactsObject(long customerID, long addressID, bool isDefaultContact)
        {
            try
            {
                CompanyContact contactObject = new CompanyContact();
                contactObject.CompanyId = customerID;
                contactObject.AddressId = addressID;
                contactObject.FirstName = string.Empty;
                contactObject.IsDefaultContact = isDefaultContact == true ? 1 : 0;

                return contactObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private Address PopulateAddressObject(long addressId, long companyId, bool isDefaulAddress, bool isDefaultShippingAddress, long TerritoryId)
        {
            try
            {
                Address addressObject = new Address();

                addressObject.AddressId = addressId;
                addressObject.CompanyId = companyId;
                addressObject.AddressName = "Address Name";
                addressObject.IsDefaultAddress = isDefaulAddress;
                addressObject.IsDefaultShippingAddress = isDefaultShippingAddress;
                addressObject.Address1 = "Address 1";
                addressObject.City = "City";
                addressObject.isArchived = false;
                addressObject.TerritoryId = TerritoryId;
                return addressObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        private static string ComputeHashSHA1(string plainText)
        {
            try
            {
                string salt = string.Empty;


                salt = ComputeHash(plainText, "SHA1", null);

                return salt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private static string ComputeHash(string plainText,
                                    string hashAlgorithm,
                                    byte[] saltBytes)
        {
            try
            {
                // If salt is not specified, generate it on the fly.
                if (saltBytes == null)
                {
                    // Define min and max salt sizes.
                    int minSaltSize = 4;
                    int maxSaltSize = 8;

                    // Generate a random number for the size of the salt.
                    Random random = new Random();
                    int saltSize = random.Next(minSaltSize, maxSaltSize);

                    // Allocate a byte array, which will hold the salt.
                    saltBytes = new byte[saltSize];

                    // Initialize a random number generator.
                    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

                    // Fill the salt with cryptographically strong byte values.
                    rng.GetNonZeroBytes(saltBytes);
                }

                // Convert plain text into a byte array.
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                // Allocate array, which will hold plain text and salt.
                byte[] plainTextWithSaltBytes =
                        new byte[plainTextBytes.Length + saltBytes.Length];

                // Copy plain text bytes into resulting array.
                for (int i = 0; i < plainTextBytes.Length; i++)
                    plainTextWithSaltBytes[i] = plainTextBytes[i];

                // Append salt bytes to the resulting array.
                for (int i = 0; i < saltBytes.Length; i++)
                    plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

                // Because we support multiple hashing algorithms, we must define
                // hash object as a common (abstract) base class. We will specify the
                // actual hashing algorithm class later during object creation.
                HashAlgorithm hash;

                // Make sure hashing algorithm name is specified.
                //if (hashAlgorithm == null)
                //    hashAlgorithm = "";
                hash = CreateHashAlgoFactory(hashAlgorithm);

                // Compute hash value of our plain text with appended salt.
                byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

                // Create array which will hold hash and original salt bytes.
                byte[] hashWithSaltBytes = new byte[hashBytes.Length +
                                                    saltBytes.Length];

                // Copy hash bytes into resulting array.
                for (int i = 0; i < hashBytes.Length; i++)
                    hashWithSaltBytes[i] = hashBytes[i];

                // Append salt bytes to the result.
                for (int i = 0; i < saltBytes.Length; i++)
                    hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

                // Convert result into a base64-encoded string.
                string hashValue = Convert.ToBase64String(hashWithSaltBytes);

                // Return the result.
                return hashValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private static HashAlgorithm CreateHashAlgoFactory(string hashAlgorithm)
        {
            try
            {
                HashAlgorithm hash = null; ;
                // Initialize appropriate hashing algorithm class.
                switch (hashAlgorithm)
                {
                    case "SHA1":
                        hash = new SHA1Managed();
                        break;

                    case "SHA256":
                        hash = new SHA256Managed();
                        break;

                    case "SHA384":
                        hash = new SHA384Managed();
                        break;

                    case "SHA512":
                        hash = new SHA512Managed();
                        break;

                    default:
                        hash = new MD5CryptoServiceProvider(); // mdf default
                        break;
                }
                return hash;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string SystemWeight(long OrganisationID)
        {
            try
            {

                var qry = from systemWeight in db.WeightUnits
                          join organisation in db.Organisations on systemWeight.Id equals organisation.SystemWeightUnit
                          where organisation.OrganisationId == OrganisationID
                          select systemWeight.UnitName;

                return qry.ToString();

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        public string SystemLength(long OrganisationID)
        {
            try
            {

                var qry = from systemLength in db.LengthUnits
                          join organisation in db.Organisations on systemLength.Id equals organisation.SystemLengthUnit
                          where organisation.OrganisationId == OrganisationID
                          select systemLength.UnitName;

                return qry.ToString();

                //  return db.Organisations.Where(o => o.OrganisationId == OrganisationID).Select(s => s.SystemLengthUnit ?? 0).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        //Update Just Company Name 
        public bool UpdateCompanyName(Company Instance)
        {
            bool Result = false;
            try
            {
                Company Company = db.Companies.Where(i => i.CompanyId == Instance.CompanyId).FirstOrDefault();
                Company.Name = Instance.Name;
                db.Companies.Attach(Company);
                db.Entry(Company).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    Result = true;

                }
                else
                {
                    Result = false;

                }
            }
            catch (Exception ex)
            {

                throw ex;

            }
            return Result;
        }

        /// <summary>
        /// Get Company By Is Customer Type
        /// </summary>
        public CompanySearchResponseForCalendar GetByIsCustomerType(CompanyRequestModelForCalendar request)
        {
            try
            {
                int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;
                bool isStringSpecified = !string.IsNullOrEmpty(request.SearchString);
                Expression<Func<Company, bool>> query =
                    s =>
                        (isStringSpecified && (s.Name.Contains(request.SearchString)) ||
                         !isStringSpecified) && request.CustomerTypes.Contains(s.IsCustomer) &&
                        ((s.IsCustomer != 0 && s.IsCustomer != 1) || (s.StoreId.HasValue && s.StoreId.Value > 0)) &&
                        s.OrganisationId == OrganisationId;


                int rowCount = DbSet.Count(query);
                IEnumerable<Company> companies =
                    DbSet.Where(query).OrderBy(x => x.Name)
                         .Skip(fromRow)
                        .Take(toRow)
                        .ToList();

                return new CompanySearchResponseForCalendar
                {
                    TotalCount = rowCount,
                    Companies = companies
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Count of live stores
        /// </summary>
        public int LiveStoresCountForDashboard()
        {
            try
            {
                return DbSet.Count(company => (!company.isArchived.HasValue || !company.isArchived.Value) && 
                    company.OrganisationId == OrganisationId &&
                    (company.IsCustomer == 3 || company.IsCustomer == 4));

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private CompanyTerritory PopulateTerritoryObject(long CompanyId)
        {
            try
            {
                CompanyTerritory objTerritory = new CompanyTerritory();
                objTerritory.TerritoryId = 0;
                objTerritory.TerritoryName = "Default Retail Customer Territory";
                objTerritory.CompanyId = CompanyId;
                objTerritory.TerritoryCode = "TC-RT";
                objTerritory.isDefault = true;

                return objTerritory;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        // get company by company id (lazyloading enable)
        public Company GetCompanyByCompanyID(long CompanyID)
        {
            try
            {
                return db.Companies.Where(c => c.CompanyId == CompanyID).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool InsertStore(long OID, ExportOrganisation objExpCorporate, ExportOrganisation objExpRetail, ExportOrganisation objExpCorporateWOP, ExportOrganisation objExpRetailWOP, string StoreName, ExportSets Sets, string SubDomain, string status)
        {
            try
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    List<string> DestinationsPath = new List<string>();
                    try
                    {


                        long OrganisationID = 0;
                        Organisation newOrg = new Organisation();

                        ImportOrganisation ImportIDs = new ImportOrganisation();

                        ImportIDs.CostCentreIDs = new List<long>();
                        objExpCorporate = Sets.ExportStore1;
                        if (objExpCorporate != null)
                        {
                            if (objExpCorporate.Company != null)
                            {
                                ImportIDs.OldCompanyID = objExpCorporate.Company.CompanyId;

                            }
                        }


                        objExpRetail = Sets.ExportRetailStore1;
                        if (objExpRetail != null)
                        {
                            if (objExpRetail.RetailCompany != null)
                            {
                                ImportIDs.RetailOldCompanyID = objExpRetail.RetailCompany.CompanyId;
                            }
                        }


                        objExpCorporateWOP = Sets.ExportStore1WOP;
                        if (objExpCorporateWOP != null)
                        {
                            if (objExpCorporateWOP.Company != null)
                            {
                                ImportIDs.OldCompanyIDWOP = objExpCorporateWOP.Company.CompanyId;

                            }
                        }


                        objExpRetailWOP = Sets.ExportRetailStore1WOP;
                        if (objExpRetailWOP != null)
                        {
                            if (objExpRetailWOP.RetailCompany != null)
                            {
                                ImportIDs.RetailOldCompanyIDWOP = objExpRetailWOP.RetailCompany.CompanyId;

                            }
                        }


                        status += "old ids done";
                        Organisation objExpOrg = Sets.ExportOrganisationSet1.Organisation;

                        ImportIDs.NewOrganisationID = OID;
                        ImportIDs.OldOrganisationID = objExpOrg.OrganisationId;
                        OrganisationID = OID;

                        status += "get old org id done";
                        //company flow 

                        // region to import corporate store

                        // insert company
                        long oCID = 0;
                        long oRetailCID = 0;
                        long oCIDWOP = 0;
                        long oRetailCIDWOP = 0;
                        string SName = ConfigurationManager.AppSettings["RetailStoreName"];
                        string SNameWOP = ConfigurationManager.AppSettings["RetailStoreNameWOP"];
                        string SCName = ConfigurationManager.AppSettings["CorporateStoreName"];
                        string SCNameWOP = ConfigurationManager.AppSettings["CorporateStoreNameWOP"];

                        List<CostCentre> CostCentres = db.CostCentres.Where(c => c.OrganisationId == OrganisationID).ToList();
                        List<Machine> machines = db.Machines.Where(c => c.OrganisationId == OrganisationID).ToList();
                        List<Company> Suppliers = db.Companies.Where(s => s.OrganisationId == OrganisationID && s.IsCustomer == 2).ToList();
                        int FlagID = db.SectionFlags.Where(c => c.OrganisationId == OrganisationID & c.SectionId == 81 && c.isDefault == true).Select(c => c.SectionFlagId).FirstOrDefault();
                        status += "setting webconfig done";
                        if (StoreName == SName)
                        {
                            Company comp = new Company();
                            comp = objExpRetail.RetailCompany;
                            comp.OrganisationId = OrganisationID;
                            comp.Name = objExpRetail.RetailCompany.Name;
                            comp.IsDisabled = 0;
                            comp.PriceFlagId = FlagID;
                            comp.CompanyDomains = null;

                            comp.CompanyContacts.ToList().ForEach(c => c.Address = null);
                            comp.CompanyContacts.ToList().ForEach(c => c.CompanyTerritory = null);

                            //comp.CompanyContacts.ToList().ForEach(c => c.TerritoryId = null);
                            //comp.CompanyContacts.ToList().ForEach(c => c.AddressId = null);
                            comp.Addresses.ToList().ForEach(a => a.CompanyContacts = null);
                            comp.Addresses.ToList().ForEach(v => v.CompanyTerritory = null);
                            if (comp.CmsPages != null && comp.CmsPages.Count > 0)
                            {
                                comp.CmsPages.ToList().ForEach(x => x.PageCategory = null);
                                comp.CmsPages.ToList().ForEach(x => x.Company = null);
                                comp.CmsPages.ToList().ForEach(x => x.OrganisationId = OrganisationID);
                            }
                            if (comp.CmsSkinPageWidgets != null && comp.CmsSkinPageWidgets.Count > 0)
                            {
                                comp.CmsSkinPageWidgets.ToList().ForEach(x => x.CmsPage = null);
                                comp.CmsSkinPageWidgets.ToList().ForEach(x => x.Company = null);
                                comp.CmsSkinPageWidgets.ToList().ForEach(x => x.Organisation = null);

                            }



                            // setting organisationid 

                            if (comp.CompanyBannerSets != null && comp.CompanyBannerSets.Count > 0)
                            {
                                comp.CompanyBannerSets.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                                // comp.ActiveBannerSetId = comp.CompanyBannerSets.Select(c => c.CompanySetId).FirstOrDefault();
                            }
                            if (comp.RaveReviews != null && comp.RaveReviews.Count > 0)
                                comp.RaveReviews.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.Addresses != null && comp.Addresses.Count > 0)
                                comp.Addresses.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.CompanyContacts != null && comp.CompanyContacts.Count > 0)
                                comp.CompanyContacts.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.Campaigns != null && comp.Campaigns.Count > 0)
                                comp.Campaigns.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.CompanyCostCentres != null && comp.CompanyCostCentres.Count > 0)
                                comp.CompanyCostCentres.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.CmsSkinPageWidgets != null && comp.CmsSkinPageWidgets.Count > 0)
                                comp.CmsSkinPageWidgets.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.FieldVariables != null && comp.FieldVariables.Count > 0)
                                comp.FieldVariables.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.TemplateColorStyles != null && comp.TemplateColorStyles.Count > 0)
                                comp.TemplateColorStyles.ToList().ForEach(c => c.ProductId = null);

                            db.Configuration.LazyLoadingEnabled = false;
                            db.Configuration.ProxyCreationEnabled = false;
                            if (comp.CompanyCostCentres != null && comp.CompanyCostCentres.Count > 0)
                            {
                                foreach (var ccc in comp.CompanyCostCentres)
                                {
                                    if (CostCentres != null && CostCentres.Count > 0)
                                    {
                                        long id = CostCentres.Where(c => c.OrganisationId == OrganisationID && c.CCIDOption3 == ccc.CostCentreId).Select(c => c.CostCentreId).FirstOrDefault();

                                        if (id > 0)
                                        {
                                            ccc.CostCentreId = id;
                                        }
                                        else
                                        {
                                            id = CostCentres.Where(c => c.OrganisationId == OrganisationID).Select(c => c.CostCentreId).FirstOrDefault();
                                            ccc.CostCentreId = id;

                                        }
                                    }


                                }
                            }

                            //comp.CmsPages.ToList().ForEach(c => c.)
                            db.Companies.Add(comp);
                            db.SaveChanges();
                            oRetailCID = comp.CompanyId;

                            status += "companyImport done";
                            if (comp.CompanyBannerSets != null && comp.CompanyBannerSets.Count > 0)
                            {
                                comp.ActiveBannerSetId = comp.CompanyBannerSets.Select(c => c.CompanySetId).FirstOrDefault();

                            }
                            // add companydomain
                            string DomainName = SubDomain + "/store/" + objExpRetail.RetailCompany.WebAccessCode;
                            CompanyDomain domain = new CompanyDomain();
                            domain.Domain = DomainName;
                            domain.CompanyId = oRetailCID;
                            db.CompanyDomains.Add(domain);
                            db.SaveChanges();

                            status += "companydomain done";
                            // List<long> OldCatIds = new List<long>();
                            long OldCatIds = 0;
                            long TerritoryId = 0;
                            // product categories

                            if (comp != null)
                            {
                                if (comp.CompanyTerritories != null)
                                {
                                    TerritoryId = comp.CompanyTerritories.Select(c => c.TerritoryId).FirstOrDefault();
                                }

                            }
                            // product categories
                            List<ProductCategory> prodCats = Sets.ExportRetailStore2;
                            if (prodCats != null && prodCats.Count > 0)
                            {
                                foreach (var cat in prodCats)
                                {
                                    if (cat.ProductCategoryId != null)
                                        cat.ContentType = cat.ProductCategoryId.ToString(); // 8888
                                    //if(cat.ParentCategoryId != null)
                                    //    cat.Description2 = cat.ParentCategoryId.ToString(); // 11859

                                    //cat.ParentCategoryId = null;
                                    cat.Sides = (int)cat.ProductCategoryId;
                                    cat.OrganisationId = OrganisationID;
                                    cat.CompanyId = oRetailCID;
                                    if (cat.CategoryTerritories != null && cat.CategoryTerritories.Count > 0)
                                    {
                                        foreach (var territory in cat.CategoryTerritories)
                                        {
                                            territory.CompanyId = oRetailCID;
                                            territory.OrganisationId = OrganisationID;

                                            territory.TerritoryId = TerritoryId;
                                        }
                                    }
                                    db.ProductCategories.Add(cat);
                                    db.SaveChanges();


                                  


                                }



                            }


                            // 
                            if (comp.ProductCategories != null && comp.ProductCategories.Count > 0)
                            {
                                foreach (var item in comp.ProductCategories)
                                {
                                    if (item.ParentCategoryId > 0) // 11859
                                    {


                                        //  string scat = item.Description2;
                                        var pCat = comp.ProductCategories.Where(g => g.ContentType.Contains(item.ParentCategoryId.Value.ToString())).FirstOrDefault();
                                        if (pCat != null)
                                        {
                                            item.ParentCategoryId = Convert.ToInt32(pCat.ProductCategoryId);
                                            db.SaveChanges();
                                        }
                                    }
                                }
                            }

                            status += "product cat done";
                            //  import items
                            List<StockItem> stockitems = db.StockItems.Where(c => c.OrganisationId == OrganisationID).ToList();
                            List<PaperSize> paperSizes = db.PaperSizes.Where(c => c.OrganisationId == OrganisationID).ToList();
                            List<Item> items = Sets.ExportRetailStore3;
                            if (items != null && items.Count > 0)
                            {
                                foreach (var item in items)
                                {

                                    item.OrganisationId = OrganisationID;
                                    item.CompanyId = oRetailCID;
                                    item.FlagId = FlagID;
                                    if (comp != null)
                                    {
                                        if (comp.SmartForms != null && comp.SmartForms.Count > 0)
                                        {
                                            item.SmartFormId = comp.SmartForms.Select(c => c.SmartFormId).FirstOrDefault();
                                        }
                                    }
                                    else
                                    {
                                        item.SmartFormId = 0;
                                    }

                                    if (item.ItemSections != null && item.ItemSections.Count > 0)
                                    {
                                        foreach (var itm in item.ItemSections)
                                        {
                                            itm.MachineSide2 = null;
                                            if (stockitems != null && stockitems.Count > 0)
                                            {
                                                long SID = stockitems.Where(c => c.RollStandards == itm.StockItemID1).Select(s => s.StockItemId).FirstOrDefault();
                                                if (SID > 0)
                                                {
                                                    itm.StockItemID1 = SID;
                                                }
                                                else
                                                {
                                                    SID = stockitems.Select(s => s.StockItemId).FirstOrDefault();
                                                    itm.StockItemID1 = SID;


                                                }
                                            }
                                            // for SectionSizeId
                                            if(paperSizes != null && paperSizes.Count > 0)
                                            {
                                                int PID = paperSizes.Where(c => c.SizeMeasure == itm.SectionSizeId).Select(c => c.PaperSizeId).FirstOrDefault();
                                                if (PID > 0)
                                                {
                                                    itm.SectionSizeId = PID;
                                                }
                                                else
                                                {
                                                    PID = paperSizes.Select(s => s.PaperSizeId).FirstOrDefault();
                                                    itm.SectionSizeId = PID;


                                                }
                                                int ISID = paperSizes.Where(c => c.SizeMeasure == itm.ItemSizeId).Select(c => c.PaperSizeId).FirstOrDefault();
                                                if (ISID > 0)
                                                {
                                                    itm.ItemSizeId = ISID;
                                                }
                                                else
                                                {
                                                    ISID = paperSizes.Select(s => s.PaperSizeId).FirstOrDefault();
                                                    itm.ItemSizeId = ISID;


                                                }

                                            }
                                            if (machines != null && machines.Count > 0)
                                            {
                                                long MID = machines.Where(c => c.SystemSiteId == itm.PressId).Select(s => s.MachineId).FirstOrDefault();
                                                long MIDSide2 = machines.Where(c => c.SystemSiteId == itm.PressIdSide2).Select(s => s.MachineId).FirstOrDefault();
                                                if (MID > 0)
                                                {
                                                    itm.PressId = (int)MID;
                                                }
                                                else
                                                {
                                                   // MID = machines.Select(s => s.MachineId).FirstOrDefault();
                                                    itm.PressId = null;
                                                  
                                                }
                                                if (MIDSide2 > 0)
                                                {
                                                    itm.PressIdSide2 = (int)MIDSide2;
                                                }
                                                else
                                                {
                                                   // MIDSide2 = machines.Select(s => s.MachineId).FirstOrDefault();
                                                    itm.PressIdSide2 = null;
                                                    //itm.PressId = null;
                                                }


                                            }
                                            //if (machines != null && machines.Count > 0)
                                            //{
                                            //    long MID = machines.Where(c => c.SystemSiteId == itm.PressId).Select(s => s.MachineId).FirstOrDefault();
                                            //    if (MID > 0)
                                            //    {
                                            //        itm.PressId = (int)MID;
                                            //    }
                                            //    else
                                            //    {
                                            //        MID = machines.Select(s => s.MachineId).FirstOrDefault();
                                            //       // itm.PressId = (int)MID;
                                            //        itm.PressId = null;


                                            //    }
                                            //}

                                        }
                                    }
                                    if (item.ItemStockOptions != null && item.ItemStockOptions.Count > 0)
                                    {
                                        foreach (var iso in item.ItemStockOptions)
                                        {
                                            if (stockitems != null && stockitems.Count > 0)
                                            {
                                                long SID = stockitems.Where(c => c.RollStandards == iso.StockId).Select(s => s.StockItemId).FirstOrDefault();
                                                if (SID > 0)
                                                {
                                                    iso.StockId = SID;
                                                }
                                                else
                                                {
                                                    SID = stockitems.Select(s => s.StockItemId).FirstOrDefault();
                                                    iso.StockId = SID;


                                                }
                                            }
                                            if (iso.ItemAddonCostCentres != null && iso.ItemAddonCostCentres.Count > 0)
                                            {
                                                foreach (var itmAdd in iso.ItemAddonCostCentres)
                                                {
                                                    if (CostCentres != null && CostCentres.Count > 0)
                                                    {

                                                        long id = CostCentres.Where(c => c.OrganisationId == OrganisationID && c.CCIDOption3 == itmAdd.CostCentreId).Select(c => c.CostCentreId).FirstOrDefault();
                                                        if (id > 0)
                                                        {

                                                            itmAdd.CostCentreId = id;
                                                        }
                                                        else
                                                        {
                                                            id = CostCentres.Where(c => c.OrganisationId == OrganisationID).Select(c => c.CostCentreId).FirstOrDefault();
                                                            itmAdd.CostCentreId = id;
                                                        }


                                                    }
                                                }


                                            }

                                        }
                                    }
                                    if (item.ProductCategoryItems != null && item.ProductCategoryItems.Count > 0)
                                    {
                                        foreach (var pci in item.ProductCategoryItems)
                                        {
                                            if (comp.ProductCategories != null && comp.ProductCategories.Count > 0)
                                            {
                                                long PID = comp.ProductCategories.Where(c => c.Sides == pci.CategoryId).Select(x => x.ProductCategoryId).FirstOrDefault();
                                                if (PID > 0)
                                                {
                                                    pci.CategoryId = PID;
                                                }
                                                else
                                                {
                                                   // PID = stockitems.Select(s => s.StockItemId).FirstOrDefault();
                                                    pci.CategoryId = null;


                                                }
                                            }

                                        }
                                    }
                                    if (item.ItemRelatedItems != null && item.ItemRelatedItems.Count > 0)
                                    {
                                        foreach (var pci in item.ItemRelatedItems)
                                        {
                                            pci.RelatedItemId = item.ItemId;
                                        }
                                    }
                                    if (item.ItemPriceMatrices != null && item.ItemPriceMatrices.Count > 0)
                                    {
                                        foreach (var price in item.ItemPriceMatrices)
                                        {
                                            int OldSupId = price.SupplierId ?? 0;
                                            if (price.SupplierId != null)
                                            {
                                                long SupId = Suppliers.Where(c => c.TaxPercentageId == OldSupId).Select(c => c.CompanyId).FirstOrDefault();
                                                price.SupplierId = (int)SupId;
                                            }
                                            price.FlagId = FlagID;
                                        }
                                    }
                                    db.Items.Add(item);

                                }

                                db.SaveChanges();

                            }

                            //

                            status += "items done";
                            //if (objExpRetail.RetailTemplateColorStyle != null && objExpRetail.RetailTemplateColorStyle.Count > 0)
                            //{
                            //    foreach (var color in objExpRetail.RetailTemplateColorStyle)
                            //    {
                            //        TemplateColorStyle objColor = new TemplateColorStyle();
                            //        objColor.CustomerId = (int)oRetailCID;
                            //        db.TemplateColorStyles.Add(objColor);
                            //    }
                            //    db.SaveChanges();
                            //}
                            if (objExpRetail.RetailTemplateFonts != null && objExpRetail.RetailTemplateFonts.Count > 0)
                            {
                                foreach (var color in objExpRetail.RetailTemplateFonts)
                                {
                                    TemplateFont objFont = new TemplateFont();
                                    objFont = color;
                                    objFont.ProductId = null;
                                    objFont.CustomerId = (int)oRetailCID;
                                    db.TemplateFonts.Add(objFont);
                                }
                                db.SaveChanges();
                            }
                        }
                        else if (StoreName == SNameWOP) // done
                        {
                            Company comp = new Company();
                            comp = objExpRetailWOP.RetailCompany;
                            comp.OrganisationId = OrganisationID;
                            comp.Name = objExpRetailWOP.RetailCompany.Name;
                            comp.IsDisabled = 0;
                            comp.PriceFlagId = FlagID;
                            comp.CompanyDomains = null;
                            comp.CompanyContacts.ToList().ForEach(c => c.Address = null);
                            comp.CompanyContacts.ToList().ForEach(c => c.CompanyTerritory = null);

                            //comp.CompanyContacts.ToList().ForEach(c => c.TerritoryId = null);
                            //comp.CompanyContacts.ToList().ForEach(c => c.AddressId = null);
                            comp.Addresses.ToList().ForEach(a => a.CompanyContacts = null);
                            comp.Addresses.ToList().ForEach(v => v.CompanyTerritory = null);
                            if (comp.CmsPages != null && comp.CmsPages.Count > 0)
                            {
                                comp.CmsPages.ToList().ForEach(x => x.PageCategory = null);
                                comp.CmsPages.ToList().ForEach(x => x.Company = null);
                                comp.CmsPages.ToList().ForEach(x => x.OrganisationId = OrganisationID);
                            }
                            if (comp.CmsSkinPageWidgets != null && comp.CmsSkinPageWidgets.Count > 0)
                            {
                                comp.CmsSkinPageWidgets.ToList().ForEach(x => x.CmsPage = null);
                                comp.CmsSkinPageWidgets.ToList().ForEach(x => x.Company = null);
                                comp.CmsSkinPageWidgets.ToList().ForEach(x => x.Organisation = null);

                            }



                            // setting organisationid 

                            if (comp.CompanyBannerSets != null && comp.CompanyBannerSets.Count > 0)
                            {
                                comp.CompanyBannerSets.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                                comp.ActiveBannerSetId = comp.CompanyBannerSets.Select(c => c.CompanySetId).FirstOrDefault();
                            }
                            if (comp.RaveReviews != null && comp.RaveReviews.Count > 0)
                                comp.RaveReviews.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.Addresses != null && comp.Addresses.Count > 0)
                                comp.Addresses.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.CompanyContacts != null && comp.CompanyContacts.Count > 0)
                                comp.CompanyContacts.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.Campaigns != null && comp.Campaigns.Count > 0)
                                comp.Campaigns.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.CompanyCostCentres != null && comp.CompanyCostCentres.Count > 0)
                                comp.CompanyCostCentres.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.CmsSkinPageWidgets != null && comp.CmsSkinPageWidgets.Count > 0)
                                comp.CmsSkinPageWidgets.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.FieldVariables != null && comp.FieldVariables.Count > 0)
                                comp.FieldVariables.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.TemplateColorStyles != null && comp.TemplateColorStyles.Count > 0)
                                comp.TemplateColorStyles.ToList().ForEach(c => c.ProductId = null);


                            if (comp.CompanyCostCentres != null && comp.CompanyCostCentres.Count > 0)
                            {
                                foreach (var ccc in comp.CompanyCostCentres)
                                {
                                    long id = CostCentres.Where(c => c.OrganisationId == OrganisationID && c.CCIDOption3 == ccc.CostCentreId).Select(c => c.CostCentreId).FirstOrDefault();

                                    if (id > 0)
                                    {
                                        ccc.CostCentreId = id;
                                    }
                                    else
                                    {
                                        id = CostCentres.Where(c => c.OrganisationId == OrganisationID).Select(c => c.CostCentreId).FirstOrDefault();
                                        ccc.CostCentreId = id;

                                    }

                                }
                            }


                            //comp.CmsPages.ToList().ForEach(c => c.)
                            db.Companies.Add(comp);
                            db.SaveChanges();
                            oRetailCIDWOP = comp.CompanyId;
                            status += "companyImport doneWOP";

                            if (comp.CompanyBannerSets != null && comp.CompanyBannerSets.Count > 0)
                            {
                                comp.ActiveBannerSetId = comp.CompanyBannerSets.Select(c => c.CompanySetId).FirstOrDefault();

                            }
                            // add companydomain
                            string DomainName = SubDomain + "/store/" + objExpRetailWOP.RetailCompany.WebAccessCode;
                            CompanyDomain domain = new CompanyDomain();
                            domain.Domain = DomainName;
                            domain.CompanyId = oRetailCIDWOP;
                            db.CompanyDomains.Add(domain);
                            db.SaveChanges();

                            // List<long> OldCatIds = new List<long>();
                            long OldCatIds = 0;
                            // product categories
                            long TerritoryId = 0;
                            // product categories

                            if (comp != null)
                            {
                                if (comp.CompanyTerritories != null)
                                {
                                    TerritoryId = comp.CompanyTerritories.Select(c => c.TerritoryId).FirstOrDefault();
                                }

                            }

                            List<ProductCategory> prodCats = Sets.ExportRetailStore2WOP;
                            if (prodCats != null && prodCats.Count > 0)
                            {
                                foreach (var cat in prodCats)
                                {
                                    if (cat.ProductCategoryId != null)
                                        cat.ContentType = cat.ProductCategoryId.ToString(); // 8888
                                    //if(cat.ParentCategoryId != null)
                                    //    cat.Description2 = cat.ParentCategoryId.ToString(); // 11859

                                    //cat.ParentCategoryId = null;
                                    cat.Sides = (int)cat.ProductCategoryId;
                                    cat.OrganisationId = OrganisationID;
                                    cat.CompanyId = oRetailCIDWOP;
                                    if (cat.CategoryTerritories != null && cat.CategoryTerritories.Count > 0)
                                    {
                                        foreach (var territory in cat.CategoryTerritories)
                                        {
                                            territory.CompanyId = oRetailCIDWOP;
                                            territory.OrganisationId = OrganisationID;

                                            territory.TerritoryId = TerritoryId;
                                        }
                                    }
                                    db.ProductCategories.Add(cat);
                                    db.SaveChanges();
                                  


                                }


                            }


                            // 
                            if (comp.ProductCategories != null && comp.ProductCategories.Count > 0)
                            {
                                foreach (var item in comp.ProductCategories)
                                {
                                    if (item.ParentCategoryId > 0) // 11859
                                    {


                                        //  string scat = item.Description2;
                                        var pCat = comp.ProductCategories.Where(g => g.ContentType.Contains(item.ParentCategoryId.Value.ToString())).FirstOrDefault();
                                        if (pCat != null)
                                        {
                                            item.ParentCategoryId = Convert.ToInt32(pCat.ProductCategoryId);
                                            db.SaveChanges();
                                        }
                                    }
                                }
                            }
                            //  import items
                            List<StockItem> stockitems = db.StockItems.Where(c => c.OrganisationId == OrganisationID).ToList();
                            List<PaperSize> paperSizes = db.PaperSizes.Where(c => c.OrganisationId == OrganisationID).ToList();
                            List<Item> items = Sets.ExportRetailStore3WOP;
                            if (items != null && items.Count > 0)
                            {
                                foreach (var item in items)
                                {

                                    item.OrganisationId = OrganisationID;
                                    item.CompanyId = oRetailCIDWOP;
                                    item.FlagId = FlagID;
                                    if (comp != null)
                                    {
                                        if (comp.SmartForms != null && comp.SmartForms.Count > 0)
                                        {
                                            item.SmartFormId = comp.SmartForms.Select(c => c.SmartFormId).FirstOrDefault();
                                        }
                                    }
                                    else
                                    {
                                        item.SmartFormId = 0;
                                    }

                                    if (item.ItemSections != null && item.ItemSections.Count > 0)
                                    {
                                        foreach (var itm in item.ItemSections)
                                        {
                                            itm.MachineSide2 = null;
                                            if (stockitems != null && stockitems.Count > 0)
                                            {
                                                long SID = stockitems.Where(c => c.RollStandards == itm.StockItemID1).Select(s => s.StockItemId).FirstOrDefault();
                                                if (SID > 0)
                                                {
                                                    itm.StockItemID1 = SID;
                                                }
                                                else
                                                {
                                                    SID = stockitems.Select(s => s.StockItemId).FirstOrDefault();
                                                    itm.StockItemID1 = SID;


                                                }
                                            }
                                            if (paperSizes != null && paperSizes.Count > 0)
                                            {
                                                int PID = paperSizes.Where(c => c.SizeMeasure == itm.SectionSizeId).Select(c => c.PaperSizeId).FirstOrDefault();
                                                if (PID > 0)
                                                {
                                                    itm.SectionSizeId = PID;
                                                }
                                                else
                                                {
                                                    PID = paperSizes.Select(s => s.PaperSizeId).FirstOrDefault();
                                                    itm.SectionSizeId = PID;


                                                }
                                                int ISID = paperSizes.Where(c => c.SizeMeasure == itm.ItemSizeId).Select(c => c.PaperSizeId).FirstOrDefault();
                                                if (ISID > 0)
                                                {
                                                    itm.ItemSizeId = ISID;
                                                }
                                                else
                                                {
                                                    ISID = paperSizes.Select(s => s.PaperSizeId).FirstOrDefault();
                                                    itm.ItemSizeId = ISID;


                                                }

                                            }
                                            if (machines != null && machines.Count > 0)
                                            {
                                                long MID = machines.Where(c => c.SystemSiteId == itm.PressId).Select(s => s.MachineId).FirstOrDefault();
                                                long MIDSide2 = machines.Where(c => c.SystemSiteId == itm.PressIdSide2).Select(s => s.MachineId).FirstOrDefault();
                                                if (MID > 0)
                                                {
                                                    itm.PressId = (int)MID;
                                                }
                                                else
                                                {
                                                   // MID = machines.Select(s => s.MachineId).FirstOrDefault();
                                                    itm.PressId = null;
                                                  
                                                }
                                                if (MIDSide2 > 0)
                                                {
                                                    itm.PressIdSide2 = (int)MIDSide2;
                                                }
                                                else
                                                {
                                                    //MIDSide2 = machines.Select(s => s.MachineId).FirstOrDefault();
                                                    itm.PressIdSide2 = null;
                                                   
                                                }


                                            }
                                           

                                        }
                                    }
                                    if (item.ItemStockOptions != null && item.ItemStockOptions.Count > 0)
                                    {
                                        foreach (var iso in item.ItemStockOptions)
                                        {
                                            if (stockitems != null && stockitems.Count > 0)
                                            {
                                                long SID = stockitems.Where(c => c.RollStandards == iso.StockId).Select(s => s.StockItemId).FirstOrDefault();
                                                if (SID > 0)
                                                {
                                                    iso.StockId = SID;
                                                }
                                                else
                                                {
                                                    SID = stockitems.Select(s => s.StockItemId).FirstOrDefault();
                                                    iso.StockId = SID;


                                                }
                                            }
                                            if (iso.ItemAddonCostCentres != null && iso.ItemAddonCostCentres.Count > 0)
                                            {
                                                foreach (var itmAdd in iso.ItemAddonCostCentres)
                                                {
                                                    if (CostCentres != null && CostCentres.Count > 0)
                                                    {

                                                        long id = CostCentres.Where(c => c.OrganisationId == OrganisationID && c.CCIDOption3 == itmAdd.CostCentreId).Select(c => c.CostCentreId).FirstOrDefault();
                                                        if (id > 0)
                                                        {

                                                            itmAdd.CostCentreId = id;
                                                        }
                                                        else
                                                        {
                                                            id = CostCentres.Where(c => c.OrganisationId == OrganisationID).Select(c => c.CostCentreId).FirstOrDefault();
                                                            itmAdd.CostCentreId = id;
                                                        }


                                                    }
                                                }


                                            }

                                        }
                                    }
                                    if (item.ProductCategoryItems != null && item.ProductCategoryItems.Count > 0)
                                    {
                                        foreach (var pci in item.ProductCategoryItems)
                                        {
                                            if (comp.ProductCategories != null && comp.ProductCategories.Count > 0)
                                            {
                                                long PID = comp.ProductCategories.Where(c => c.Sides == pci.CategoryId).Select(x => x.ProductCategoryId).FirstOrDefault();
                                                if (PID > 0)
                                                {
                                                    pci.CategoryId = PID;
                                                }
                                                else
                                                {
                                                   // PID = stockitems.Select(s => s.StockItemId).FirstOrDefault();
                                                    pci.CategoryId = null;


                                                }
                                            }

                                        }
                                    }
                                    if (item.ItemRelatedItems != null && item.ItemRelatedItems.Count > 0)
                                    {
                                        foreach (var pci in item.ItemRelatedItems)
                                        {
                                            pci.RelatedItemId = item.ItemId;
                                        }
                                    }
                                    if (item.ItemPriceMatrices != null && item.ItemPriceMatrices.Count > 0)
                                    {
                                        foreach (var price in item.ItemPriceMatrices)
                                        {
                                            int OldSupId = price.SupplierId ?? 0;
                                            if (price.SupplierId != null)
                                            {
                                                long SupId = Suppliers.Where(c => c.TaxPercentageId == OldSupId).Select(c => c.CompanyId).FirstOrDefault();
                                                price.SupplierId = (int)SupId;
                                            }
                                            price.FlagId = FlagID;
                                        }
                                    }

                                    db.Items.Add(item);

                                }

                                db.SaveChanges();

                            }

                            //


                            //if (objExpRetailWOP.RetailTemplateColorStyle != null && objExpRetailWOP.RetailTemplateColorStyle.Count > 0)
                            //{
                            //    foreach (var color in objExpRetailWOP.RetailTemplateColorStyle)
                            //    {
                            //        TemplateColorStyle objColor = new TemplateColorStyle();
                            //        objColor.CustomerId = (int)oRetailCIDWOP;
                            //        db.TemplateColorStyles.Add(objColor);
                            //    }
                            //    db.SaveChanges();
                            //}
                            if (objExpRetailWOP.RetailTemplateFonts != null && objExpRetailWOP.RetailTemplateFonts.Count > 0)
                            {
                                foreach (var color in objExpRetailWOP.RetailTemplateFonts)
                                {
                                    TemplateFont objFont = new TemplateFont();
                                    objFont = color;
                                    objFont.ProductId = null;
                                    objFont.CustomerId = (int)oRetailCIDWOP;
                                    db.TemplateFonts.Add(objFont);
                                }
                                db.SaveChanges();
                            }

                        }
                        else if (StoreName == SCName) // done
                        {
                            Company comp = new Company();
                            comp = objExpCorporate.Company;
                            comp.OrganisationId = OrganisationID;
                            comp.Name = objExpCorporate.Company.Name + "- Copy";
                            comp.IsDisabled = 0;
                            comp.PriceFlagId = FlagID;
                            comp.CompanyDomains = null;

                            comp.CompanyContacts.ToList().ForEach(c => c.Address = null);
                            comp.CompanyContacts.ToList().ForEach(c => c.CompanyTerritory = null);
                            comp.Addresses.ToList().ForEach(a => a.CompanyContacts = null);
                            comp.Addresses.ToList().ForEach(v => v.CompanyTerritory = null);
                            if (comp.CmsPages != null && comp.CmsPages.Count > 0)
                            {
                                comp.CmsPages.ToList().ForEach(x => x.PageCategory = null);
                                comp.CmsPages.ToList().ForEach(x => x.Company = null);
                                comp.CmsPages.ToList().ForEach(x => x.OrganisationId = OrganisationID);
                            }
                            if (comp.CmsSkinPageWidgets != null && comp.CmsSkinPageWidgets.Count > 0)
                            {
                                comp.CmsSkinPageWidgets.ToList().ForEach(x => x.CmsPage = null);
                                comp.CmsSkinPageWidgets.ToList().ForEach(x => x.Company = null);
                                comp.CmsSkinPageWidgets.ToList().ForEach(x => x.Organisation = null);

                            }

                            if (comp.CompanyBannerSets != null && comp.CompanyBannerSets.Count > 0)
                            {
                                comp.CompanyBannerSets.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                                comp.ActiveBannerSetId = comp.CompanyBannerSets.Select(c => c.CompanySetId).FirstOrDefault();
                            }
                            if (comp.RaveReviews != null && comp.RaveReviews.Count > 0)
                                comp.RaveReviews.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.Addresses != null && comp.Addresses.Count > 0)
                                comp.Addresses.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.CompanyContacts != null && comp.CompanyContacts.Count > 0)
                                comp.CompanyContacts.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.Campaigns != null && comp.Campaigns.Count > 0)
                                comp.Campaigns.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.CompanyCostCentres != null && comp.CompanyCostCentres.Count > 0)
                                comp.CompanyCostCentres.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.CmsSkinPageWidgets != null && comp.CmsSkinPageWidgets.Count > 0)
                                comp.CmsSkinPageWidgets.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.FieldVariables != null && comp.FieldVariables.Count > 0)
                                comp.FieldVariables.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.TemplateColorStyles != null && comp.TemplateColorStyles.Count > 0)
                                comp.TemplateColorStyles.ToList().ForEach(c => c.ProductId = null);
                            if (comp.TemplateColorStyles != null && comp.TemplateColorStyles.Count > 0)
                                comp.TemplateColorStyles.ToList().ForEach(c => c.ProductId = null);


                            if (comp.CompanyCostCentres != null && comp.CompanyCostCentres.Count > 0)
                            {
                                foreach (var ccc in comp.CompanyCostCentres)
                                {
                                    long id = CostCentres.Where(c => c.OrganisationId == OrganisationID && c.CCIDOption3 == ccc.CostCentreId).Select(c => c.CostCentreId).FirstOrDefault();

                                    if (id > 0)
                                    {
                                        ccc.CostCentreId = id;
                                    }
                                    else
                                    {
                                        id = CostCentres.Where(c => c.OrganisationId == OrganisationID).Select(c => c.CostCentreId).FirstOrDefault();
                                        ccc.CostCentreId = id;

                                    }

                                }
                            }


                            db.Companies.Add(comp);
                            db.SaveChanges();
                            oCID = comp.CompanyId;

                            if (comp.CompanyBannerSets != null && comp.CompanyBannerSets.Count > 0)
                            {
                                comp.ActiveBannerSetId = comp.CompanyBannerSets.Select(c => c.CompanySetId).FirstOrDefault();

                            }
                            // add companydomain
                            string DomainName = SubDomain + "/store/" + objExpCorporate.Company.WebAccessCode;
                            CompanyDomain domain = new CompanyDomain();
                            domain.Domain = DomainName;
                            domain.CompanyId = oCID;
                            db.CompanyDomains.Add(domain);
                            db.SaveChanges();


                            //List<long> OldCatIds = new List<long>();
                            long OldCatIds = 0;
                            long TerritoryId = 0;
                            // product categories

                            if (comp != null)
                            {
                                if (comp.CompanyTerritories != null)
                                {
                                    TerritoryId = comp.CompanyTerritories.Select(c => c.TerritoryId).FirstOrDefault();
                                }

                            }

                            // product categories
                            List<ProductCategory> prodCats = Sets.ExportStore2;
                            if (prodCats != null && prodCats.Count > 0)
                            {
                                foreach (var cat in prodCats)
                                {
                                    if (cat.ProductCategoryId != null)
                                        cat.ContentType = cat.ProductCategoryId.ToString(); // 8888
                                    //if(cat.ParentCategoryId != null)
                                    //    cat.Description2 = cat.ParentCategoryId.ToString(); // 11859

                                    //cat.ParentCategoryId = null;
                                    cat.Sides = (int)cat.ProductCategoryId;

                                    cat.OrganisationId = OrganisationID;
                                    cat.CompanyId = oCID;
                                    if (cat.CategoryTerritories != null && cat.CategoryTerritories.Count > 0)
                                    {
                                        foreach (var territory in cat.CategoryTerritories)
                                        {
                                            territory.CompanyId = oCID;
                                            territory.OrganisationId = OrganisationID;

                                            territory.TerritoryId = TerritoryId;
                                        }
                                    }
                                    db.ProductCategories.Add(cat);
                                    db.SaveChanges();
                                    //  var gg = comp.Items.Where(c => c.ProductCategoryItems.t)
                                    //if (comp.Items != null && comp.Items.Count > 0)
                                    //{
                                    //    foreach (var itm in comp.Items)
                                    //    {
                                    //        if (itm.ProductCategoryItems != null)
                                    //        {
                                    //            List<ProductCategoryItem> pcis = itm.ProductCategoryItems.Where(c => c.CategoryId == OldCatIds).ToList();

                                    //            if (pcis != null && pcis.Count > 0)
                                    //            {
                                    //                foreach (var pc in pcis)
                                    //                {
                                    //                    pc.CategoryId = cat.ProductCategoryId;
                                    //                }
                                    //            }
                                    //        }



                                    //    }
                                    //    db.SaveChanges();
                                    //}

                                }


                            }


                            // 
                            if (comp.ProductCategories != null && comp.ProductCategories.Count > 0)
                            {
                                foreach (var item in comp.ProductCategories)
                                {
                                    if (item.ParentCategoryId > 0) // 11859
                                    {


                                        //  string scat = item.Description2;
                                        var pCat = comp.ProductCategories.Where(g => g.ContentType.Contains(item.ParentCategoryId.Value.ToString())).FirstOrDefault();
                                        if (pCat != null)
                                        {
                                            item.ParentCategoryId = Convert.ToInt32(pCat.ProductCategoryId);
                                            db.SaveChanges();
                                        }
                                    }
                                }
                            }


                            //  import items
                            List<StockItem> stockitems = db.StockItems.Where(c => c.OrganisationId == OrganisationID).ToList();
                            List<PaperSize> paperSizes = db.PaperSizes.Where(c => c.OrganisationId == OrganisationID).ToList();
                            List<Item> items = Sets.ExportStore3;
                            if (items != null && items.Count > 0)
                            {
                                foreach (var item in items)
                                {

                                    item.OrganisationId = OrganisationID;
                                    item.CompanyId = oCID;
                                    item.FlagId = FlagID;
                                    if (comp != null)
                                    {
                                        if (comp.SmartForms != null && comp.SmartForms.Count > 0)
                                        {
                                            item.SmartFormId = comp.SmartForms.Select(c => c.SmartFormId).FirstOrDefault();
                                        }
                                    }
                                    else
                                    {
                                        item.SmartFormId = 0;
                                    }

                                    if (item.ItemSections != null && item.ItemSections.Count > 0)
                                    {
                                        foreach (var itm in item.ItemSections)
                                        {
                                            itm.MachineSide2 = null;
                                            if (stockitems != null && stockitems.Count > 0)
                                            {
                                                long SID = stockitems.Where(c => c.RollStandards == itm.StockItemID1).Select(s => s.StockItemId).FirstOrDefault();
                                                if (SID > 0)
                                                {
                                                    itm.StockItemID1 = SID;
                                                }
                                                else
                                                {
                                                    SID = stockitems.Select(s => s.StockItemId).FirstOrDefault();
                                                    itm.StockItemID1 = SID;


                                                }
                                            }
                                            if (paperSizes != null && paperSizes.Count > 0)
                                            {
                                                int PID = paperSizes.Where(c => c.SizeMeasure == itm.SectionSizeId).Select(c => c.PaperSizeId).FirstOrDefault();
                                                if (PID > 0)
                                                {
                                                    itm.SectionSizeId = PID;
                                                }
                                                else
                                                {
                                                    PID = paperSizes.Select(s => s.PaperSizeId).FirstOrDefault();
                                                    itm.SectionSizeId = PID;


                                                }
                                                int ISID = paperSizes.Where(c => c.SizeMeasure == itm.ItemSizeId).Select(c => c.PaperSizeId).FirstOrDefault();
                                                if (ISID > 0)
                                                {
                                                    itm.ItemSizeId = ISID;
                                                }
                                                else
                                                {
                                                    ISID = paperSizes.Select(s => s.PaperSizeId).FirstOrDefault();
                                                    itm.ItemSizeId = ISID;


                                                }

                                            }
                                            if (machines != null && machines.Count > 0)
                                            {
                                                long MID = machines.Where(c => c.SystemSiteId == itm.PressId).Select(s => s.MachineId).FirstOrDefault();
                                                long MIDSide2 = machines.Where(c => c.SystemSiteId == itm.PressIdSide2).Select(s => s.MachineId).FirstOrDefault();
                                                if (MID > 0)
                                                {
                                                    itm.PressId = (int)MID;
                                                }
                                                else
                                                {
                                                  //  MID = machines.Select(s => s.MachineId).FirstOrDefault();
                                                    itm.PressId = null;
                                                  
                                                }
                                                if (MIDSide2 > 0)
                                                {
                                                    itm.PressIdSide2 = (int)MIDSide2;
                                                }
                                                else
                                                {
                                                   // MIDSide2 = machines.Select(s => s.MachineId).FirstOrDefault();
                                                    itm.PressIdSide2 = null;
                                                    //itm.PressId = null;
                                                }


                                            }

                                            //if (machines != null && machines.Count > 0)
                                            //{
                                            //    long MID = machines.Where(c => c.SystemSiteId == itm.PressId).Select(s => s.MachineId).FirstOrDefault();
                                            //    if (MID > 0)
                                            //    {
                                            //        itm.PressId = (int)MID;
                                            //    }
                                            //    else
                                            //    {
                                            //        MID = machines.Select(s => s.MachineId).FirstOrDefault();
                                            //       // itm.PressId = (int)MID;
                                            //        itm.PressId = null;

                                            //    }
                                            //}

                                        }
                                    }
                                    if (item.ItemStockOptions != null && item.ItemStockOptions.Count > 0)
                                    {
                                        foreach (var iso in item.ItemStockOptions)
                                        {
                                            if (stockitems != null && stockitems.Count > 0)
                                            {
                                                long SID = stockitems.Where(c => c.RollStandards == iso.StockId).Select(s => s.StockItemId).FirstOrDefault();
                                                if (SID > 0)
                                                {
                                                    iso.StockId = SID;
                                                }
                                                else
                                                {
                                                    SID = stockitems.Select(s => s.StockItemId).FirstOrDefault();
                                                    iso.StockId = SID;


                                                }
                                            }
                                            if (iso.ItemAddonCostCentres != null && iso.ItemAddonCostCentres.Count > 0)
                                            {
                                                foreach (var itmAdd in iso.ItemAddonCostCentres)
                                                {
                                                    if (CostCentres != null && CostCentres.Count > 0)
                                                    {

                                                        long id = CostCentres.Where(c => c.OrganisationId == OrganisationID && c.CCIDOption3 == itmAdd.CostCentreId).Select(c => c.CostCentreId).FirstOrDefault();
                                                        if (id > 0)
                                                        {

                                                            itmAdd.CostCentreId = id;
                                                        }
                                                        else
                                                        {
                                                            id = CostCentres.Where(c => c.OrganisationId == OrganisationID).Select(c => c.CostCentreId).FirstOrDefault();
                                                            itmAdd.CostCentreId = id;
                                                        }


                                                    }
                                                }


                                            }

                                        }
                                    }
                                    if (item.ProductCategoryItems != null && item.ProductCategoryItems.Count > 0)
                                    {
                                        foreach (var pci in item.ProductCategoryItems)
                                        {
                                            if (comp.ProductCategories != null && comp.ProductCategories.Count > 0)
                                            {
                                                long PID = comp.ProductCategories.Where(c => c.Sides == pci.CategoryId).Select(x => x.ProductCategoryId).FirstOrDefault();
                                                if (PID > 0)
                                                {
                                                    pci.CategoryId = PID;
                                                }
                                                else
                                                {
                                                   // PID = stockitems.Select(s => s.StockItemId).FirstOrDefault();
                                                    pci.CategoryId = null;


                                                }
                                            }

                                        }
                                    }
                                    if (item.ItemRelatedItems != null && item.ItemRelatedItems.Count > 0)
                                    {
                                        foreach (var pci in item.ItemRelatedItems)
                                        {
                                            pci.RelatedItemId = item.ItemId;
                                        }
                                    }
                                    if (item.ItemPriceMatrices != null && item.ItemPriceMatrices.Count > 0)
                                    {
                                        foreach (var price in item.ItemPriceMatrices)
                                        {
                                            int OldSupId = price.SupplierId ?? 0;
                                            if (price.SupplierId != null)
                                            {
                                                long SupId = Suppliers.Where(c => c.TaxPercentageId == OldSupId).Select(c => c.CompanyId).FirstOrDefault();
                                                price.SupplierId = (int)SupId;
                                            }
                                            price.FlagId = FlagID;
                                        }
                                    }

                                    db.Items.Add(item);

                                }

                                db.SaveChanges();

                            }

                            //// product categories
                            //List<ProductCategory> prodCats = Sets.ExportStore2;
                            //if (prodCats != null && prodCats.Count > 0)
                            //{
                            //    foreach (var cat in prodCats)
                            //    {
                            //        cat.OrganisationId = OrganisationID;
                            //        cat.CompanyId = oCID;
                            //        db.ProductCategories.Add(cat);

                            //    }
                            //    db.SaveChanges();
                            //}


                            //
                            //if (objExpCorporate.TemplateColorStyle != null && objExpCorporate.TemplateColorStyle.Count > 0)
                            //{
                            //    foreach (var color in objExpCorporate.TemplateColorStyle)
                            //    {
                            //        TemplateColorStyle objColor = new TemplateColorStyle();
                            //        objColor.CustomerId = (int)oCID;
                            //        db.TemplateColorStyles.Add(objColor);
                            //    }
                            //    db.SaveChanges();
                            //}
                            if (objExpCorporate.TemplateFonts != null && objExpCorporate.TemplateFonts.Count > 0)
                            {
                                foreach (var color in objExpCorporate.TemplateFonts)
                                {
                                    TemplateFont objFont = new TemplateFont();
                                    objFont = color;
                                    objFont.ProductId = null;
                                    objFont.CustomerId = (int)oCID;
                                    db.TemplateFonts.Add(objFont);
                                }
                                db.SaveChanges();
                            }

                        }
                        else if (StoreName == SCNameWOP) // done
                        {
                            Company comp = new Company();
                            comp = objExpCorporateWOP.Company;
                            comp.OrganisationId = OrganisationID;
                            comp.Name = objExpCorporateWOP.Company.Name + "- Copy";
                            comp.IsDisabled = 0;
                            comp.PriceFlagId = FlagID;
                            comp.CompanyDomains = null;
                            comp.CompanyContacts.ToList().ForEach(c => c.Address = null);
                            comp.CompanyContacts.ToList().ForEach(c => c.CompanyTerritory = null);
                            comp.Addresses.ToList().ForEach(a => a.CompanyContacts = null);
                            comp.Addresses.ToList().ForEach(v => v.CompanyTerritory = null);
                            if (comp.CmsPages != null && comp.CmsPages.Count > 0)
                            {
                                comp.CmsPages.ToList().ForEach(x => x.PageCategory = null);
                                comp.CmsPages.ToList().ForEach(x => x.Company = null);
                                comp.CmsPages.ToList().ForEach(x => x.CmsPageTags = null);
                                comp.CmsPages.ToList().ForEach(x => x.OrganisationId = OrganisationID);

                            }
                            if (comp.CmsSkinPageWidgets != null && comp.CmsSkinPageWidgets.Count > 0)
                            {
                                comp.CmsSkinPageWidgets.ToList().ForEach(x => x.CmsPage = null);
                                comp.CmsSkinPageWidgets.ToList().ForEach(x => x.Company = null);
                                comp.CmsSkinPageWidgets.ToList().ForEach(x => x.Organisation = null);

                            }

                            if (comp.CompanyBannerSets != null && comp.CompanyBannerSets.Count > 0)
                            {
                                comp.CompanyBannerSets.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                                comp.ActiveBannerSetId = comp.CompanyBannerSets.Select(c => c.CompanySetId).FirstOrDefault();
                            }
                            if (comp.RaveReviews != null && comp.RaveReviews.Count > 0)
                                comp.RaveReviews.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.Addresses != null && comp.Addresses.Count > 0)
                                comp.Addresses.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.CompanyContacts != null && comp.CompanyContacts.Count > 0)
                                comp.CompanyContacts.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.Campaigns != null && comp.Campaigns.Count > 0)
                                comp.Campaigns.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.CompanyCostCentres != null && comp.CompanyCostCentres.Count > 0)
                                comp.CompanyCostCentres.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.CmsSkinPageWidgets != null && comp.CmsSkinPageWidgets.Count > 0)
                                comp.CmsSkinPageWidgets.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.FieldVariables != null && comp.FieldVariables.Count > 0)
                                comp.FieldVariables.ToList().ForEach(c => c.OrganisationId = OrganisationID);
                            if (comp.TemplateColorStyles != null && comp.TemplateColorStyles.Count > 0)
                                comp.TemplateColorStyles.ToList().ForEach(c => c.ProductId = null);


                            if (comp.CompanyCostCentres != null && comp.CompanyCostCentres.Count > 0)
                            {
                                foreach (var ccc in comp.CompanyCostCentres)
                                {
                                    long id = CostCentres.Where(c => c.OrganisationId == OrganisationID && c.CCIDOption3 == ccc.CostCentreId).Select(c => c.CostCentreId).FirstOrDefault();

                                    if (id > 0)
                                    {
                                        ccc.CostCentreId = id;
                                    }
                                    else
                                    {
                                        id = CostCentres.Where(c => c.OrganisationId == OrganisationID).Select(c => c.CostCentreId).FirstOrDefault();
                                        ccc.CostCentreId = id;

                                    }

                                }
                            }


                            db.Companies.Add(comp);
                            db.SaveChanges();
                            oCIDWOP = comp.CompanyId;

                            if (comp.CompanyBannerSets != null && comp.CompanyBannerSets.Count > 0)
                            {
                                comp.ActiveBannerSetId = comp.CompanyBannerSets.Select(c => c.CompanySetId).FirstOrDefault();

                            }
                            // add companydomain
                            string DomainName = SubDomain + "/store/" + objExpCorporate.Company.WebAccessCode;
                            CompanyDomain domain = new CompanyDomain();
                            domain.Domain = DomainName;
                            domain.CompanyId = oCIDWOP;
                            db.CompanyDomains.Add(domain);
                            db.SaveChanges();


                            // List<long> OldCatIds = new List<long>();
                            long OldCatIds = 0;
                            List<ProductCategory> prodCats = Sets.ExportStore2WOP;
                            if (prodCats != null && prodCats.Count > 0)
                            {
                                foreach (var cat in prodCats)
                                {
                                    if (cat.ProductCategoryId != null && cat.ProductCategoryId > 0)
                                        cat.ContentType = cat.ProductCategoryId.ToString(); // 8888
                                    //if(cat.ParentCategoryId != null)
                                    //    cat.Description2 = cat.ParentCategoryId.ToString(); // 11859

                                    //cat.ParentCategoryId = null;
                                    cat.OrganisationId = OrganisationID;
                                    cat.CompanyId = oCIDWOP;
                                    cat.Sides = (int)cat.ProductCategoryId;
                                    long TerritoryId = 0;
                                    // product categories

                                    if (comp != null)
                                    {
                                        if (comp.CompanyTerritories != null)
                                        {
                                            TerritoryId = comp.CompanyTerritories.Select(c => c.TerritoryId).FirstOrDefault();
                                        }

                                    }

                                    if (cat.CategoryTerritories != null && cat.CategoryTerritories.Count > 0)
                                    {
                                        foreach (var territory in cat.CategoryTerritories)
                                        {
                                            territory.CompanyId = oCIDWOP;
                                            territory.OrganisationId = OrganisationID;

                                            territory.TerritoryId = TerritoryId;
                                        }
                                    }

                                    db.ProductCategories.Add(cat);
                                    db.SaveChanges();

                                    ////  var gg = comp.Items.Where(c => c.ProductCategoryItems.t)
                                    //if (comp.Items != null && comp.Items.Count > 0)
                                    //{
                                    //    foreach (var itm in comp.Items)
                                    //    {
                                    //        if (itm.ProductCategoryItems != null)
                                    //        {
                                    //            List<ProductCategoryItem> pcis = itm.ProductCategoryItems.Where(c => c.CategoryId == OldCatIds).ToList();
                                    //            if (pcis != null && pcis.Count > 0)
                                    //            {
                                    //                if (pcis != null && pcis.Count > 0)
                                    //                {
                                    //                    foreach (var pc in pcis)
                                    //                    {
                                    //                        pc.CategoryId = cat.ProductCategoryId;
                                    //                    }
                                    //                }
                                    //            }
                                    //        }



                                    //    }
                                    //    db.SaveChanges();
                                    //}


                                }



                            }


                            // 
                            if (comp.ProductCategories != null && comp.ProductCategories.Count > 0)
                            {
                                foreach (var item in comp.ProductCategories)
                                {
                                    if (item.ParentCategoryId > 0) // 11859
                                    {


                                        //  string scat = item.Description2;
                                        var pCat = comp.ProductCategories.Where(g => g.ContentType.Contains(item.ParentCategoryId.Value.ToString())).FirstOrDefault();
                                        if (pCat != null)
                                        {
                                            item.ParentCategoryId = Convert.ToInt32(pCat.ProductCategoryId);
                                            db.SaveChanges();
                                        }
                                    }
                                }
                            }


                            List<StockItem> stockitems = db.StockItems.Where(c => c.OrganisationId == OrganisationID).ToList();
                            List<PaperSize> paperSizes = db.PaperSizes.Where(c => c.OrganisationId == OrganisationID).ToList();
                            //  import items
                            List<Item> items = Sets.ExportStore3WOP;
                            if (items != null && items.Count > 0)
                            {
                                foreach (var item in items)
                                {

                                    item.OrganisationId = OrganisationID;
                                    item.CompanyId = oCIDWOP;
                                    item.FlagId = FlagID;
                                    if (comp != null)
                                    {
                                        if (comp.SmartForms != null && comp.SmartForms.Count > 0)
                                        {
                                            item.SmartFormId = comp.SmartForms.Select(c => c.SmartFormId).FirstOrDefault();
                                        }
                                    }
                                    else
                                    {
                                        item.SmartFormId = 0;
                                    }

                                    if (item.ItemSections != null && item.ItemSections.Count > 0)
                                    {
                                       
                                        foreach (var itm in item.ItemSections)
                                        {
                                            itm.MachineSide2 = null;
                                            if (stockitems != null && stockitems.Count > 0)
                                            {
                                                long SID = stockitems.Where(c => c.RollStandards == itm.StockItemID1).Select(s => s.StockItemId).FirstOrDefault();
                                                if (SID > 0)
                                                {
                                                    itm.StockItemID1 = SID;
                                                }
                                                else
                                                {
                                                    SID = stockitems.Select(s => s.StockItemId).FirstOrDefault();
                                                    itm.StockItemID1 = SID;


                                                }
                                            }
                                            if (paperSizes != null && paperSizes.Count > 0)
                                            {
                                                int PID = paperSizes.Where(c => c.SizeMeasure == itm.SectionSizeId).Select(c => c.PaperSizeId).FirstOrDefault();
                                                if (PID > 0)
                                                {
                                                    itm.SectionSizeId = PID;
                                                }
                                                else
                                                {
                                                    PID = paperSizes.Select(s => s.PaperSizeId).FirstOrDefault();
                                                    itm.SectionSizeId = PID;


                                                }
                                                int ISID = paperSizes.Where(c => c.SizeMeasure == itm.ItemSizeId).Select(c => c.PaperSizeId).FirstOrDefault();
                                                if (ISID > 0)
                                                {
                                                    itm.ItemSizeId = ISID;
                                                }
                                                else
                                                {
                                                    ISID = paperSizes.Select(s => s.PaperSizeId).FirstOrDefault();
                                                    itm.ItemSizeId = ISID;


                                                }

                                            }
                                            if (machines != null && machines.Count > 0)
                                            {
                                                long MID = machines.Where(c => c.SystemSiteId == itm.PressId).Select(s => s.MachineId).FirstOrDefault();
                                                long MIDSide2 = machines.Where(c => c.SystemSiteId == itm.PressIdSide2).Select(s => s.MachineId).FirstOrDefault();
                                                if (MID > 0)
                                                {
                                                    itm.PressId = (int)MID;
                                                }
                                                else
                                                {
                                                  //  MID = machines.Select(s => s.MachineId).FirstOrDefault();
                                                    itm.PressId = null;
                                                  //  itm.PressId = (int)MID;
                                                }
                                                if (MIDSide2 > 0)
                                                {
                                                    itm.PressIdSide2 = (int)MIDSide2;
                                                }
                                                else
                                                {
                                                   // MIDSide2 = machines.Select(s => s.MachineId).FirstOrDefault();
                                                    itm.PressIdSide2 = null;
                                                    //itm.PressId = null;
                                                }


                                            }
                                          

                                        }
                                    }
                                    if (item.ItemStockOptions != null && item.ItemStockOptions.Count > 0)
                                    {
                                        foreach (var iso in item.ItemStockOptions)
                                        {
                                            if (stockitems != null && stockitems.Count > 0)
                                            {
                                                long SID = stockitems.Where(c => c.RollStandards == iso.StockId).Select(s => s.StockItemId).FirstOrDefault();
                                                if (SID > 0)
                                                {
                                                    iso.StockId = SID;
                                                }
                                                else
                                                {
                                                    SID = stockitems.Select(s => s.StockItemId).FirstOrDefault();
                                                    iso.StockId = SID;


                                                }
                                            }
                                            if (iso.ItemAddonCostCentres != null && iso.ItemAddonCostCentres.Count > 0)
                                            {
                                                foreach (var itmAdd in iso.ItemAddonCostCentres)
                                                {
                                                    if (CostCentres != null && CostCentres.Count > 0)
                                                    {

                                                        long id = CostCentres.Where(c => c.OrganisationId == OrganisationID && c.CCIDOption3 == itmAdd.CostCentreId).Select(c => c.CostCentreId).FirstOrDefault();
                                                        if (id > 0)
                                                        {

                                                            itmAdd.CostCentreId = id;
                                                        }
                                                        else
                                                        {
                                                            id = CostCentres.Where(c => c.OrganisationId == OrganisationID).Select(c => c.CostCentreId).FirstOrDefault();
                                                            itmAdd.CostCentreId = id;
                                                        }


                                                    }
                                                }


                                            }

                                        }
                                    }
                                    if (item.ProductCategoryItems != null && item.ProductCategoryItems.Count > 0)
                                    {
                                        foreach (var pci in item.ProductCategoryItems)
                                        {
                                            if (comp.ProductCategories != null && comp.ProductCategories.Count > 0)
                                            {
                                                long PID = comp.ProductCategories.Where(c => c.Sides == pci.CategoryId).Select(x => x.ProductCategoryId).FirstOrDefault();
                                                if (PID > 0)
                                                {
                                                    pci.CategoryId = PID;
                                                }
                                                else
                                                {
                                                   // PID = stockitems.Select(s => s.StockItemId).FirstOrDefault();
                                                    pci.CategoryId = null;


                                                }
                                            }

                                        }
                                    }
                                    if (item.ItemRelatedItems != null && item.ItemRelatedItems.Count > 0)
                                    {
                                        foreach (var pci in item.ItemRelatedItems)
                                        {
                                            pci.RelatedItemId = item.ItemId;
                                        }
                                    }
                                    if (item.ItemPriceMatrices != null && item.ItemPriceMatrices.Count > 0)
                                    {
                                        foreach (var price in item.ItemPriceMatrices)
                                        {
                                            int OldSupId = price.SupplierId ?? 0;
                                            if (price.SupplierId != null)
                                            {
                                                long SupId = Suppliers.Where(c => c.TaxPercentageId == OldSupId).Select(c => c.CompanyId).FirstOrDefault();
                                                price.SupplierId = (int)SupId;
                                            }

                                            price.FlagId = FlagID;
                                        }
                                    }
                                    db.Items.Add(item);

                                }

                                db.SaveChanges();

                            }

                            //// product categories


                            //if (objExpCorporateWOP.TemplateColorStyle != null && objExpCorporateWOP.TemplateColorStyle.Count > 0)
                            //{
                            //    foreach (var color in objExpCorporateWOP.TemplateColorStyle)
                            //    {
                            //        TemplateColorStyle objColor = new TemplateColorStyle();
                            //        objColor.CustomerId = (int)oCIDWOP;
                            //        db.TemplateColorStyles.Add(objColor);
                            //    }
                            //    db.SaveChanges();
                            //}

                            if (objExpCorporate.TemplateFonts != null && objExpCorporate.TemplateFonts.Count > 0)
                            {
                                foreach (var color in objExpCorporate.TemplateFonts)
                                {
                                    TemplateFont objFont = new TemplateFont();
                                    objFont = color;
                                    objFont.CustomerId = (int)oCIDWOP;
                                    objFont.ProductId = null;
                                    db.TemplateFonts.Add(objFont);
                                }
                                db.SaveChanges();
                            }
                        }




                        // Organisation org = objOrg;
                        string DestinationMISLogoFilePath = string.Empty;
                        string DestinationWebSiteLogoFilePath = string.Empty;
                        string DestinationThumbPath = string.Empty;
                        string DestinationMainPath = string.Empty;
                        string DestinationReportPath = string.Empty;

                        string DestinationLanguageDirectory = string.Empty;
                        string DestinationLanguageFilePath = string.Empty;

                        status += "start copying done";
                        if (StoreName == SName)
                        {
                            status += CopyCompanyFiles(oRetailCID, DestinationsPath, ImportIDs.OldOrganisationID, ImportIDs.NewOrganisationID, ImportIDs.RetailOldCompanyID, status);
                        }
                        else if (StoreName == SNameWOP)
                        {
                            status += CopyCompanyFiles(oRetailCIDWOP, DestinationsPath, ImportIDs.OldOrganisationID, ImportIDs.NewOrganisationID, ImportIDs.RetailOldCompanyIDWOP, status);
                        }
                        else if (StoreName == SCName)
                        {
                            status += CopyCompanyFiles(oCID, DestinationsPath, ImportIDs.OldOrganisationID, ImportIDs.NewOrganisationID, ImportIDs.OldCompanyID, status);
                        }
                        else if (StoreName == SCNameWOP)
                        {
                            status += CopyCompanyFiles(oCIDWOP, DestinationsPath, ImportIDs.OldOrganisationID, ImportIDs.NewOrganisationID, ImportIDs.OldCompanyIDWOP, status);
                        }




                        db.SaveChanges();
                        dbContextTransaction.Commit();

                        string SourceImportOrg = HttpContext.Current.Server.MapPath("~/MPC_Content/Artworks/ImportStore");

                        if (Directory.Exists(SourceImportOrg))
                        {


                            Directory.Delete(SourceImportOrg, true);
                        }

                        return true;
                        // 
                        // }
                    }
                    catch (Exception ex)
                    {
                        // return status += "error";

                        dbContextTransaction.Rollback();

                        // Delete files if it was copied before exception
                        if (DestinationsPath != null)
                        {
                            foreach (string Path in DestinationsPath)
                            {
                                DeletePhysicallFiles(Path);
                            }
                        }


                        throw ex;
                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

      

        public string CopyCompanyFiles(long oCID, List<string> DestinationsPath, long oldOrgID, long NewOrgID, long OldCompanyID, string status)
        {
            try
            {
                string DestinationThumbPath = string.Empty;
                string DestinationMainPath = string.Empty;
                string DestinationContactFilesPath = string.Empty;
                string DestinationMediaFilesPath = string.Empty;
                string DestinationReportPath = string.Empty;
                string DestinationThumbPathCat = string.Empty;
                string DestinationThumbnailPath = string.Empty;
                string DestinationImagePath = string.Empty;
                string DestinationGridPath = string.Empty;
                string DestinationFile1Path = string.Empty;
                string DestinationFile2Path = string.Empty;
                string DestinationFil3Path = string.Empty;
                string DestinationFile4Path = string.Empty;
                string DestinationFile5Path = string.Empty;
                string DestinationSiteFile = string.Empty;
                string DestinationSpriteFile = string.Empty;
                string DestinationLanguageDirectory = string.Empty;
                string DestinationLanguageFilePath = string.Empty;
                string DestinationItemAttachmentsPath = string.Empty;
                string DestinationFont1 = string.Empty;
                string DestinationFont2 = string.Empty;
                string DestinationFont3 = string.Empty;

                Company ObjCompany = db.Companies.Where(c => c.CompanyId == oCID).FirstOrDefault();
                status += "companyGet";
                if (ObjCompany != null)
                {
                    // company logo
                    string CompanyPathOld = string.Empty;
                    string CompanylogoPathNew = string.Empty;
                    if (ObjCompany.Image != null)
                    {
                        CompanyPathOld = Path.GetFileName(ObjCompany.Image);

                        CompanylogoPathNew = CompanyPathOld.Replace(OldCompanyID + "_", oCID + "_");

                        string DestinationCompanyLogoFilePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + NewOrgID + "/" + oCID + "/" + CompanylogoPathNew);
                        DestinationsPath.Add(DestinationCompanyLogoFilePath);
                        string DestinationCompanyLogoDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + NewOrgID + "/" + oCID);
                        string CompanyLogoSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Artworks/ImportStore/Assets/" + oldOrgID + "/" + OldCompanyID + "/" + CompanyPathOld);
                        if (!System.IO.Directory.Exists(DestinationCompanyLogoDirectory))
                        {
                            Directory.CreateDirectory(DestinationCompanyLogoDirectory);
                            if (Directory.Exists(DestinationCompanyLogoDirectory))
                            {
                                if (File.Exists(CompanyLogoSourcePath))
                                {
                                    if (!File.Exists(DestinationCompanyLogoFilePath))
                                        File.Copy(CompanyLogoSourcePath, DestinationCompanyLogoFilePath);
                                }


                            }


                        }
                        else
                        {
                            if (File.Exists(CompanyLogoSourcePath))
                            {
                                if (!File.Exists(DestinationCompanyLogoFilePath))
                                    File.Copy(CompanyLogoSourcePath, DestinationCompanyLogoFilePath);
                            }
                        }
                        ObjCompany.Image = "MPC_Content/Assets/" + NewOrgID + "/" + oCID + "/" + CompanylogoPathNew;
                    }

                    if (ObjCompany.StoreBackgroundImage != null)
                    {
                        CompanyPathOld = Path.GetFileName(ObjCompany.StoreBackgroundImage);

                        CompanylogoPathNew = CompanyPathOld.Replace(OldCompanyID + "_", oCID + "_");

                        string DestinationCompanyBackgroundFilePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + NewOrgID + "/" + oCID + "/" + CompanylogoPathNew);
                        DestinationsPath.Add(DestinationCompanyBackgroundFilePath);
                        string DestinationCompanyBackgroundDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + NewOrgID + "/" + oCID);
                        string CompanyLogoSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Artworks/ImportStore/Assets/" + oldOrgID + "/" + OldCompanyID + "/" + CompanyPathOld);
                        if (!System.IO.Directory.Exists(DestinationCompanyBackgroundDirectory))
                        {
                            Directory.CreateDirectory(DestinationCompanyBackgroundDirectory);
                            if (Directory.Exists(DestinationCompanyBackgroundDirectory))
                            {
                                if (File.Exists(CompanyLogoSourcePath))
                                {
                                    if (!File.Exists(DestinationCompanyBackgroundFilePath))
                                        File.Copy(CompanyLogoSourcePath, DestinationCompanyBackgroundFilePath);
                                }


                            }


                        }
                        else
                        {
                            if (File.Exists(CompanyLogoSourcePath))
                            {
                                if (!File.Exists(DestinationCompanyBackgroundFilePath))
                                    File.Copy(CompanyLogoSourcePath, DestinationCompanyBackgroundFilePath);
                            }
                        }
                        ObjCompany.StoreBackgroundImage = "MPC_Content/Assets/" + NewOrgID + "/" + oCID + "/" + CompanylogoPathNew;
                    }

                    status += "company logo done";
                    if (ObjCompany.CompanyContacts != null && ObjCompany.CompanyContacts.Count > 0)
                    {
                        foreach (var contact in ObjCompany.CompanyContacts)
                        {
                            string OldContactImage = string.Empty;
                            string NewContactImage = string.Empty;
                            string OldContactID = string.Empty;
                            if (contact.image != null)
                            {
                                string name = Path.GetFileName(contact.image);
                                string[] SplitMain = name.Split('_');
                                if (SplitMain[0] != string.Empty)
                                {
                                    OldContactID = SplitMain[0];

                                }

                                OldContactImage = Path.GetFileName(contact.image);
                                NewContactImage = OldContactImage.Replace(OldContactID + "_", contact.ContactId + "_");

                                DestinationContactFilesPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + NewOrgID + "/" + oCID + "/Contacts/" + contact.ContactId + "/" + NewContactImage);
                                DestinationsPath.Add(DestinationContactFilesPath);
                                string DestinationContactFilesDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + NewOrgID + "/" + oCID + "/Contacts/" + contact.ContactId);
                                string ContactFilesSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Artworks/ImportStore/Assets/" + oldOrgID + "/" + OldCompanyID + "/Contacts/" + OldContactID + "/" + OldContactImage);
                                if (!System.IO.Directory.Exists(DestinationContactFilesDirectory))
                                {
                                    Directory.CreateDirectory(DestinationContactFilesDirectory);
                                    if (Directory.Exists(DestinationContactFilesDirectory))
                                    {
                                        if (File.Exists(ContactFilesSourcePath))
                                        {
                                            if (!File.Exists(DestinationContactFilesPath))
                                                File.Copy(ContactFilesSourcePath, DestinationContactFilesPath);
                                        }


                                    }



                                }
                                else
                                {
                                    if (File.Exists(ContactFilesSourcePath))
                                    {
                                        if (!File.Exists(DestinationContactFilesPath))
                                            File.Copy(ContactFilesSourcePath, DestinationContactFilesPath);
                                    }

                                }
                                contact.image = "/MPC_Content/Assets/" + NewOrgID + "/" + oCID + "/Contacts/" + contact.ContactId + "/" + NewContactImage;
                            }
                        }
                    }
                    Dictionary<string, string> dictionaryMediaIds = new Dictionary<string, string>();
                    status += "company contacts done";
                    // copy media files
                    if (ObjCompany.MediaLibraries != null && ObjCompany.MediaLibraries.Count > 0)
                    {
                        foreach (var media in ObjCompany.MediaLibraries)
                        {
                            string OldMediaFilePath = string.Empty;
                            string NewMediaFilePath = string.Empty;
                            string OldMediaID = string.Empty;
                            string NewMediaID = string.Empty;
                            if (media.FilePath != null)
                            {
                                string name = Path.GetFileName(media.FilePath);
                                string[] SplitMain = name.Split('_');
                                if (SplitMain[0] != string.Empty)
                                {
                                    OldMediaID = SplitMain[0];

                                }

                                status += "media " + media.MediaId + Environment.NewLine;

                                if (media.MediaId > 0)
                                    NewMediaID = Convert.ToString(media.MediaId);



                                dictionaryMediaIds.Add(OldMediaID, NewMediaID);

                                OldMediaFilePath = Path.GetFileName(media.FilePath);
                                NewMediaFilePath = OldMediaFilePath.Replace(OldMediaID + "_", media.MediaId + "_");

                                DestinationMediaFilesPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Media/" + NewOrgID + "/" + oCID + "/" + NewMediaFilePath);
                                DestinationsPath.Add(DestinationMediaFilesPath);
                                string DestinationMediaFilesDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Media/" + NewOrgID + "/" + oCID);
                                string MediaFilesSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Artworks/ImportStore/Media/" + oldOrgID + "/" + OldCompanyID + "/" + OldMediaFilePath);
                                if (!System.IO.Directory.Exists(DestinationMediaFilesDirectory))
                                {
                                    Directory.CreateDirectory(DestinationMediaFilesDirectory);
                                    if (Directory.Exists(DestinationMediaFilesDirectory))
                                    {
                                        if (File.Exists(MediaFilesSourcePath))
                                        {
                                            if (!File.Exists(DestinationMediaFilesPath))
                                                File.Copy(MediaFilesSourcePath, DestinationMediaFilesPath);
                                        }


                                    }



                                }
                                else
                                {
                                    if (File.Exists(MediaFilesSourcePath))
                                    {
                                        if (!File.Exists(DestinationMediaFilesPath))
                                            File.Copy(MediaFilesSourcePath, DestinationMediaFilesPath);
                                    }

                                }
                                media.FilePath = "MPC_Content/Media/" + NewOrgID + "/" + oCID + "/" + NewMediaFilePath;
                            }

                        }
                    }
                    status += "company media done" + Environment.NewLine;
                    if (ObjCompany.CompanyBannerSets != null && ObjCompany.CompanyBannerSets.Count > 0)
                    {
                        foreach (var sets in ObjCompany.CompanyBannerSets)
                        {


                            if (sets.CompanyBanners != null && sets.CompanyBanners.Count > 0)
                            {
                                foreach (var bann in sets.CompanyBanners)
                                {
                                    if (!string.IsNullOrEmpty(bann.ImageURL))
                                    {
                                        string OldMediaID = string.Empty;
                                        string newMediaID = string.Empty;
                                        string name = Path.GetFileName(bann.ImageURL);
                                        string[] SplitMain = name.Split('_');
                                        status += "loop start";
                                        if (SplitMain != null)
                                        {
                                            if (SplitMain[0] != string.Empty)
                                            {
                                                OldMediaID = SplitMain[0];

                                            }
                                        }
                                        status += "SPLIT MAIN 0000" + SplitMain[0];
                                        status += "call to dictionary " + OldMediaID;
                                        if (dictionaryMediaIds != null && dictionaryMediaIds.Count > 0)
                                        {
                                            var dec = dictionaryMediaIds.Where(s => s.Key == OldMediaID).Select(s => s.Value).FirstOrDefault();
                                            if (dec != null)
                                            {
                                                newMediaID = dec.ToString();
                                            }
                                        }


                                        string NewBannerPath = name.Replace(OldMediaID + "_", newMediaID + "_");

                                        bann.ImageURL = "/MPC_Content/Media/" + NewOrgID + "/" + oCID + "/" + NewBannerPath;
                                    }
                                }
                            }

                        }
                    }
                    status += "company banner done";
                    if (ObjCompany.CmsPages != null && ObjCompany.CmsPages.Count > 0)
                    {
                        foreach (var pages in ObjCompany.CmsPages)
                        {
                            if (!string.IsNullOrEmpty(pages.PageBanner))
                            {
                                string name = Path.GetFileName(pages.PageBanner);
                                pages.PageBanner = "/MPC_Content/Media/" + NewOrgID + "/" + oCID + "/" + name;
                            }

                        }
                    }
                    status += "company cms page done";
                    if (ObjCompany.ProductCategories != null && ObjCompany.ProductCategories.Count > 0)
                    {
                        foreach (var prodCat in ObjCompany.ProductCategories)
                        {
                            string ProdCatID = string.Empty;
                            string CatName = string.Empty;

                            if (!string.IsNullOrEmpty(prodCat.ThumbnailPath))
                            {
                                string OldThumbnailPath = string.Empty;
                                string NewThumbnailPath = string.Empty;

                                string name = Path.GetFileName(prodCat.ThumbnailPath);
                                string[] SplitMain = name.Split('_');
                                if (SplitMain[1] != string.Empty)
                                {
                                    ProdCatID = SplitMain[1];

                                }

                                OldThumbnailPath = Path.GetFileName(prodCat.ThumbnailPath);
                                NewThumbnailPath = OldThumbnailPath.Replace(ProdCatID + "_", prodCat.ProductCategoryId + "_");



                                DestinationThumbPathCat = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + NewOrgID + "/" + oCID + "/ProductCategories/" + NewThumbnailPath);
                                DestinationsPath.Add(DestinationThumbPathCat);
                                string DestinationThumbDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + NewOrgID + "/" + oCID + "/ProductCategories");
                                string ThumbSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Artworks/ImportStore/Assets/" + oldOrgID + "/" + OldCompanyID + "/ProductCategories/" + OldThumbnailPath);
                                if (!System.IO.Directory.Exists(DestinationThumbDirectory))
                                {
                                    Directory.CreateDirectory(DestinationThumbDirectory);
                                    if (Directory.Exists(DestinationThumbDirectory))
                                    {
                                        if (File.Exists(ThumbSourcePath))
                                        {
                                            if (!File.Exists(DestinationThumbPathCat))
                                                File.Copy(ThumbSourcePath, DestinationThumbPathCat);
                                        }


                                    }

                                }
                                else
                                {
                                    if (File.Exists(ThumbSourcePath))
                                    {
                                        if (!File.Exists(DestinationThumbPathCat))
                                            File.Copy(ThumbSourcePath, DestinationThumbPathCat);
                                    }

                                }
                                prodCat.ThumbnailPath = "MPC_Content/Assets/" + NewOrgID + "/" + oCID + "/ProductCategories/" + NewThumbnailPath;
                            }

                            if (!string.IsNullOrEmpty(prodCat.ImagePath))
                            {
                                string OldImagePath = string.Empty;
                                string NewImagePath = string.Empty;

                                string name = Path.GetFileName(prodCat.ImagePath);
                                string[] SplitMain = name.Split('_');
                                if (SplitMain[1] != string.Empty)
                                {
                                    ProdCatID = SplitMain[1];

                                }

                                OldImagePath = Path.GetFileName(prodCat.ImagePath);
                                NewImagePath = OldImagePath.Replace(ProdCatID + "_", prodCat.ProductCategoryId + "_");

                                DestinationImagePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + NewOrgID + "/" + oCID + "/ProductCategories/" + NewImagePath);
                                DestinationsPath.Add(DestinationImagePath);
                                string DestinationImageDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + NewOrgID + "/" + oCID + "/ProductCategories");
                                string ImageSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Artworks/ImportStore/Assets/" + oldOrgID + "/" + OldCompanyID + "/ProductCategories/" + OldImagePath);

                                if (!System.IO.Directory.Exists(DestinationImageDirectory))
                                {
                                    Directory.CreateDirectory(DestinationImageDirectory);
                                    if (Directory.Exists(DestinationImageDirectory))
                                    {
                                        if (File.Exists(ImageSourcePath))
                                        {
                                            if (!File.Exists(DestinationImagePath))
                                                File.Copy(ImageSourcePath, DestinationImagePath);
                                        }


                                    }

                                }
                                else
                                {
                                    if (File.Exists(ImageSourcePath))
                                    {
                                        if (!File.Exists(DestinationImagePath))
                                            File.Copy(ImageSourcePath, DestinationImagePath);
                                    }

                                }
                                prodCat.ImagePath = "MPC_Content/Assets/" + NewOrgID + "/" + oCID + "/ProductCategories/" + NewImagePath;
                            }


                        }
                    }
                    status += "company product cat done";
                    if (ObjCompany.Items != null && ObjCompany.Items.Count > 0)
                    {
                        string ItemID = string.Empty;
                        string ItemName = string.Empty;
                        foreach (var item in ObjCompany.Items)
                        {
                            // thumbnail images
                            if (!string.IsNullOrEmpty(item.ThumbnailPath))
                            {
                                string OldThumbnailPath = string.Empty;
                                string NewThumbnailPath = string.Empty;

                                string name = Path.GetFileName(item.ThumbnailPath);
                                string[] SplitMain = name.Split('_');
                                if (SplitMain != null)
                                {
                                    if (SplitMain[1] != string.Empty)
                                    {
                                        ItemID = SplitMain[1];

                                    }
                                    int i = 0;
                                    // string s = "108";
                                    bool result = int.TryParse(ItemID, out i);
                                    if (!result)
                                    {
                                        ItemID = SplitMain[0];
                                    }
                                }
                                OldThumbnailPath = Path.GetFileName(item.ThumbnailPath);
                                NewThumbnailPath = OldThumbnailPath.Replace(ItemID + "_", item.ItemId + "_");


                                DestinationThumbnailPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + NewOrgID + "/" + item.ItemId + "/" + NewThumbnailPath);
                                DestinationsPath.Add(DestinationThumbnailPath);
                                string DestinationThumbnailDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + NewOrgID + "/" + item.ItemId);
                                string ThumbnailSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Artworks/ImportStore/Products/" + oldOrgID + "/" + ItemID + "/" + OldThumbnailPath);
                                if (!System.IO.Directory.Exists(DestinationThumbnailDirectory))
                                {
                                    Directory.CreateDirectory(DestinationThumbnailDirectory);
                                    if (Directory.Exists(DestinationThumbnailDirectory))
                                    {
                                        if (File.Exists(ThumbnailSourcePath))
                                        {
                                            if (!File.Exists(DestinationThumbnailPath))
                                                File.Copy(ThumbnailSourcePath, DestinationThumbnailPath);
                                        }


                                    }

                                }
                                else
                                {
                                    if (File.Exists(ThumbnailSourcePath))
                                    {
                                        if (!File.Exists(DestinationThumbnailPath))
                                            File.Copy(ThumbnailSourcePath, DestinationThumbnailPath);
                                    }

                                }
                                item.ThumbnailPath = "MPC_Content/Products/" + NewOrgID + "/" + item.ItemId + "/" + NewThumbnailPath;
                            }

                            // main image
                            if (!string.IsNullOrEmpty(item.ImagePath))
                            {

                                string OldImagePath = string.Empty;
                                string NewImagePath = string.Empty;


                                string name = Path.GetFileName(item.ImagePath);
                                string[] SplitMain = name.Split('_');
                                if (SplitMain != null)
                                {
                                    if (SplitMain[1] != string.Empty)
                                    {
                                        ItemID = SplitMain[1];

                                    }
                                    int i = 0;
                                    // string s = "108";
                                    bool result = int.TryParse(ItemID, out i);
                                    if (!result)
                                    {
                                        ItemID = SplitMain[0];
                                    }
                                }

                                OldImagePath = Path.GetFileName(item.ImagePath);
                                NewImagePath = OldImagePath.Replace(ItemID + "_", item.ItemId + "_");


                                DestinationImagePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + NewOrgID + "/" + item.ItemId + "/" + NewImagePath);
                                DestinationsPath.Add(DestinationImagePath);
                                string DestinationImageDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + NewOrgID + "/" + item.ItemId);
                                string ImageSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Artworks/ImportStore/Products/" + oldOrgID + "/" + ItemID + "/" + OldImagePath);
                                if (!System.IO.Directory.Exists(DestinationImageDirectory))
                                {
                                    Directory.CreateDirectory(DestinationImageDirectory);
                                    if (Directory.Exists(DestinationImageDirectory))
                                    {
                                        if (File.Exists(ImageSourcePath))
                                        {
                                            if (!File.Exists(DestinationImagePath))
                                                File.Copy(ImageSourcePath, DestinationImagePath);
                                        }


                                    }

                                }
                                else
                                {
                                    if (File.Exists(ImageSourcePath))
                                    {
                                        if (!File.Exists(DestinationImagePath))
                                            File.Copy(ImageSourcePath, DestinationImagePath);
                                    }

                                }
                                item.ImagePath = "MPC_Content/Products/" + NewOrgID + "/" + item.ItemId + "/" + NewImagePath;
                            }

                            // Gird image
                            if (!string.IsNullOrEmpty(item.GridImage))
                            {
                                string OldGridPath = string.Empty;
                                string NewGridPath = string.Empty;

                                string name = Path.GetFileName(item.GridImage);
                                string[] SplitMain = name.Split('_');
                                if (SplitMain[1] != string.Empty)
                                {
                                    ItemID = SplitMain[1];

                                }
                                //int i = 0;
                                //// string s = "108";
                                //bool result = int.TryParse(ItemID, out i);
                                //if (!result)
                                //{
                                //    ItemID = SplitMain[0];
                                //}

                                OldGridPath = Path.GetFileName(item.GridImage);
                                NewGridPath = OldGridPath.Replace(ItemID + "_", item.ItemId + "_");

                                DestinationGridPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + NewOrgID + "/" + item.ItemId + "/" + NewGridPath);
                                DestinationsPath.Add(DestinationGridPath);
                                string DestinationGridDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + NewOrgID + "/" + item.ItemId);
                                string GridSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Artworks/ImportStore/Products/" + oldOrgID + "/" + ItemID + "/" + OldGridPath);
                                if (!System.IO.Directory.Exists(DestinationGridDirectory))
                                {
                                    Directory.CreateDirectory(DestinationGridDirectory);
                                    if (Directory.Exists(DestinationGridDirectory))
                                    {
                                        if (File.Exists(GridSourcePath))
                                        {
                                            if (!File.Exists(DestinationGridPath))
                                                File.Copy(GridSourcePath, DestinationGridPath);
                                        }


                                    }

                                }
                                else
                                {
                                    if (File.Exists(GridSourcePath))
                                    {
                                        if (!File.Exists(DestinationGridPath))
                                            File.Copy(GridSourcePath, DestinationGridPath);

                                    }
                                }
                                item.GridImage = "MPC_Content/Products/" + NewOrgID + "/" + item.ItemId + "/" + NewGridPath;
                            }

                            // file 1
                            if (!string.IsNullOrEmpty(item.File1))
                            {
                                string OldF1Path = string.Empty;
                                string NewF1Path = string.Empty;

                                string name = Path.GetFileName(item.File1);
                                string[] SplitMain = name.Split('_');
                                if (SplitMain[0] != string.Empty)
                                {
                                    ItemID = SplitMain[0];

                                }

                                OldF1Path = Path.GetFileName(item.File1);
                                NewF1Path = OldF1Path.Replace(ItemID + "_", item.ItemId + "_");

                                DestinationFile1Path = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + NewOrgID + "/" + item.ItemId + "/" + NewF1Path);
                                DestinationsPath.Add(DestinationFile1Path);
                                string DestinationFile1Directory = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + NewOrgID + "/" + item.ItemId);
                                string File1SourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Artworks/ImportStore/Products/" + oldOrgID + "/" + ItemID + "/" + OldF1Path);
                                if (!System.IO.Directory.Exists(DestinationFile1Directory))
                                {
                                    Directory.CreateDirectory(DestinationFile1Directory);
                                    if (Directory.Exists(DestinationFile1Directory))
                                    {
                                        if (File.Exists(File1SourcePath))
                                        {
                                            if (!File.Exists(DestinationFile1Path))
                                                File.Copy(File1SourcePath, DestinationFile1Path);
                                        }


                                    }

                                }
                                else
                                {
                                    if (File.Exists(File1SourcePath))
                                    {
                                        if (!File.Exists(DestinationFile1Path))
                                            File.Copy(File1SourcePath, DestinationFile1Path);
                                    }

                                }
                                item.File1 = "MPC_Content/Products/" + NewOrgID + "/" + item.ItemId + "/" + NewF1Path;

                            }

                            // file 2
                            if (!string.IsNullOrEmpty(item.File2))
                            {
                                string OldF2Path = string.Empty;
                                string NewF2Path = string.Empty;

                                string name = Path.GetFileName(item.File2);

                                string[] SplitMain = name.Split('_');
                                if (SplitMain[0] != string.Empty)
                                {
                                    ItemID = SplitMain[0];

                                }

                                OldF2Path = Path.GetFileName(item.File2);
                                NewF2Path = OldF2Path.Replace(ItemID + "_", item.ItemId + "_");

                                DestinationFile2Path = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + NewOrgID + "/" + item.ItemId + "/" + NewF2Path);
                                DestinationsPath.Add(DestinationFile2Path);
                                string DestinationFile2Directory = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + NewOrgID + "/" + item.ItemId);
                                string File2SourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Artworks/ImportStore/Products/" + oldOrgID + "/" + ItemID + "/" + OldF2Path);
                                if (!System.IO.Directory.Exists(DestinationFile2Directory))
                                {
                                    Directory.CreateDirectory(DestinationFile2Directory);
                                    if (Directory.Exists(DestinationFile2Directory))
                                    {
                                        if (File.Exists(File2SourcePath))
                                        {
                                            if (!File.Exists(DestinationFile2Path))
                                                File.Copy(File2SourcePath, DestinationFile2Path);
                                        }


                                    }

                                }
                                else
                                {
                                    if (File.Exists(File2SourcePath))
                                    {
                                        if (!File.Exists(DestinationFile2Path))
                                            File.Copy(File2SourcePath, DestinationFile2Path);
                                    }

                                }
                                item.File2 = "MPC_Content/Products/" + NewOrgID + "/" + item.ItemId + "/" + NewF2Path;
                            }

                            // file 3
                            if (!string.IsNullOrEmpty(item.File3))
                            {
                                string OldF3Path = string.Empty;
                                string NewF3Path = string.Empty;

                                string name = Path.GetFileName(item.File3);
                                string[] SplitMain = name.Split('_');
                                if (SplitMain[0] != string.Empty)
                                {
                                    ItemID = SplitMain[0];

                                }

                                OldF3Path = Path.GetFileName(item.File3);
                                NewF3Path = OldF3Path.Replace(ItemID + "_", item.ItemId + "_");

                                DestinationFil3Path = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + NewOrgID + "/" + item.ItemId + "/" + NewF3Path);
                                DestinationsPath.Add(DestinationFil3Path);
                                string DestinationFile3Directory = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + NewOrgID + "/" + item.ItemId);
                                string File3SourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Artworks/ImportStore/Products/" + oldOrgID + "/" + ItemID + "/" + OldF3Path);
                                if (!System.IO.Directory.Exists(DestinationFile3Directory))
                                {
                                    Directory.CreateDirectory(DestinationFile3Directory);
                                    if (Directory.Exists(DestinationFile3Directory))
                                    {
                                        if (File.Exists(File3SourcePath))
                                        {
                                            if (!File.Exists(DestinationFil3Path))
                                                File.Copy(File3SourcePath, DestinationFil3Path);
                                        }


                                    }

                                }
                                else
                                {
                                    if (File.Exists(File3SourcePath))
                                    {
                                        if (!File.Exists(DestinationFil3Path))
                                            File.Copy(File3SourcePath, DestinationFil3Path);
                                    }

                                }
                                item.File3 = "MPC_Content/Products/" + NewOrgID + "/" + item.ItemId + "/" + NewF3Path;
                            }

                            // file 4
                            if (!string.IsNullOrEmpty(item.File4))
                            {
                                string OldF4Path = string.Empty;
                                string NewF4Path = string.Empty;

                                string name = Path.GetFileName(item.File4);
                                string[] SplitMain = name.Split('_');
                                if (SplitMain[0] != string.Empty)
                                {
                                    ItemID = SplitMain[0];

                                }

                                OldF4Path = Path.GetFileName(item.File4);
                                NewF4Path = OldF4Path.Replace(ItemID + "_", item.ItemId + "_");

                                DestinationFile4Path = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + NewOrgID + "/" + item.ItemId + "/" + NewF4Path);
                                DestinationsPath.Add(DestinationFile4Path);
                                string DestinationFile4Directory = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + NewOrgID + "/" + item.ItemId);
                                string File4SourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Artworks/ImportStore/Products/" + oldOrgID + "/" + ItemID + "/" + OldF4Path);
                                if (!System.IO.Directory.Exists(DestinationFile4Directory))
                                {
                                    Directory.CreateDirectory(DestinationFile4Directory);
                                    if (Directory.Exists(DestinationFile4Directory))
                                    {
                                        if (File.Exists(File4SourcePath))
                                        {
                                            if (!File.Exists(DestinationFile4Path))
                                                File.Copy(File4SourcePath, DestinationFile4Path);
                                        }


                                    }

                                }
                                else
                                {
                                    if (File.Exists(File4SourcePath))
                                    {
                                        if (!File.Exists(DestinationFile4Path))
                                            File.Copy(File4SourcePath, DestinationFile4Path);
                                    }

                                }
                                item.File4 = "MPC_Content/Products/" + NewOrgID + "/" + item.ItemId + "/" + NewF4Path;
                            }

                            // file 5
                            if (!string.IsNullOrEmpty(item.File5))
                            {
                                string OldF5Path = string.Empty;
                                string NewF5Path = string.Empty;

                                string name = Path.GetFileName(item.File5);
                                string[] SplitMain = name.Split('_');
                                if (SplitMain[0] != string.Empty)
                                {
                                    ItemID = SplitMain[0];

                                }

                                OldF5Path = Path.GetFileName(item.File5);
                                NewF5Path = OldF5Path.Replace(ItemID + "_", item.ItemId + "_");

                                DestinationFile5Path = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + NewOrgID + "/" + item.ItemId + "/" + NewF5Path);
                                DestinationsPath.Add(DestinationFile5Path);
                                string DestinationFile5Directory = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + NewOrgID + "/" + item.ItemId);
                                string File5SourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Artworks/ImportStore/Products/" + oldOrgID + "/" + ItemID + "/" + OldF5Path);
                                if (!System.IO.Directory.Exists(DestinationFile5Directory))
                                {
                                    Directory.CreateDirectory(DestinationFile5Directory);
                                    if (Directory.Exists(DestinationFile5Directory))
                                    {
                                        if (File.Exists(File5SourcePath))
                                        {
                                            if (!File.Exists(DestinationFile5Path))
                                                File.Copy(File5SourcePath, DestinationFile5Path);
                                        }


                                    }

                                }
                                else
                                {
                                    if (File.Exists(File5SourcePath))
                                    {
                                        if (!File.Exists(DestinationFile5Path))
                                            File.Copy(File5SourcePath, DestinationFile5Path);
                                    }

                                }
                                item.File5 = "MPC_Content/Products/" + NewOrgID + "/" + item.ItemId + "/" + NewF5Path;
                            }
                            if (item.ItemImages != null && item.ItemImages.Count > 0)
                            {
                                foreach (var img in item.ItemImages)
                                {
                                    if (!string.IsNullOrEmpty(img.ImageURL))
                                    {
                                        string OldImagePath = string.Empty;
                                        string NewImagePath = string.Empty;

                                        string name = Path.GetFileName(img.ImageURL);

                                        string DestinationItemImagePath = HttpContext.Current.Server.MapPath("/MPC_Content/Products/" + NewOrgID + "/" + item.ItemId + "/" + name);
                                        DestinationsPath.Add(DestinationItemImagePath);
                                        string DestinationItemImageDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Products/" + NewOrgID + "/" + item.ItemId);
                                        string ItemImageSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportStore/Products/" + oldOrgID + "/" + ItemID + "/" + name);
                                        if (!System.IO.Directory.Exists(DestinationItemImageDirectory))
                                        {
                                            Directory.CreateDirectory(DestinationItemImageDirectory);
                                            if (Directory.Exists(DestinationItemImageDirectory))
                                            {
                                                if (File.Exists(ItemImageSourcePath))
                                                {
                                                    if (!File.Exists(DestinationItemImagePath))
                                                        File.Copy(ItemImageSourcePath, DestinationItemImagePath);
                                                }


                                            }

                                        }
                                        else
                                        {
                                            if (File.Exists(ItemImageSourcePath))
                                            {
                                                if (!File.Exists(DestinationItemImagePath))
                                                    File.Copy(ItemImageSourcePath, DestinationItemImagePath);
                                            }

                                        }
                                        img.ImageURL = "MPC_Content/Products/" + NewOrgID + "/" + item.ItemId + "/" + name;
                                        // item.ThumbnailPath = "MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId + "/" + NewThumbnailPath;
                                    }
                                }
                            }
                            if (item.TemplateId != null && item.TemplateId > 0)
                            {
                                if (item.DesignerCategoryId == 0 || item.DesignerCategoryId == null)
                                {
                                    if (item.Template != null)
                                    {

                                        // template background images
                                        if (item.Template.TemplateBackgroundImages != null && item.Template.TemplateBackgroundImages.Count > 0)
                                        {
                                            foreach (var tempImg in item.Template.TemplateBackgroundImages)
                                            {
                                                if (!string.IsNullOrEmpty(tempImg.ImageName))
                                                {
                                                    if (tempImg.ImageName.Contains("UserImgs/"))
                                                    {
                                                        string name = tempImg.ImageName;

                                                        string ImageName = Path.GetFileName(tempImg.ImageName);

                                                        string NewPath = "UserImgs/" + oCID + "/" + ImageName;

                                                        string[] tempID = tempImg.ImageName.Split('/');

                                                        string OldTempID = tempID[1];

                                                        string DestinationTempBackGroundImages = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + NewOrgID + "/Templates/" + NewPath);
                                                        DestinationsPath.Add(DestinationTempBackGroundImages);
                                                        string DestinationTempBackgroundDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + NewOrgID + "/Templates/UserImgs/" + oCID);
                                                        string FileBackGroundSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Designer/Organisation" + oldOrgID + "/Templates/UserImgs/" + OldCompanyID + "/" + ImageName);
                                                        if (!System.IO.Directory.Exists(DestinationTempBackgroundDirectory))
                                                        {
                                                            Directory.CreateDirectory(DestinationTempBackgroundDirectory);
                                                            if (Directory.Exists(DestinationTempBackgroundDirectory))
                                                            {
                                                                if (File.Exists(FileBackGroundSourcePath))
                                                                {
                                                                    if (!File.Exists(DestinationTempBackGroundImages))
                                                                        File.Copy(FileBackGroundSourcePath, DestinationTempBackGroundImages);
                                                                }


                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (File.Exists(FileBackGroundSourcePath))
                                                            {
                                                                if (!File.Exists(DestinationTempBackGroundImages))
                                                                    File.Copy(FileBackGroundSourcePath, DestinationTempBackGroundImages);
                                                            }

                                                        }
                                                        tempImg.ImageName = NewPath;
                                                    }
                                                    else
                                                    {
                                                        string name = tempImg.ImageName;

                                                        string ImageName = Path.GetFileName(tempImg.ImageName);

                                                        string NewPath = tempImg.ProductId + "/" + ImageName;

                                                        string[] tempID = tempImg.ImageName.Split('/');

                                                        string OldTempID = tempID[0];


                                                        string DestinationTempBackGroundImages = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + NewOrgID + "/Templates/" + NewPath);
                                                        DestinationsPath.Add(DestinationTempBackGroundImages);
                                                        string DestinationTempBackgroundDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + NewOrgID + "/Templates/" + tempImg.ProductId);
                                                        string FileBackGroundSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Designer/Organisation" + oldOrgID + "/Templates/" + OldTempID + "/" + ImageName);
                                                        if (!System.IO.Directory.Exists(DestinationTempBackgroundDirectory))
                                                        {
                                                            Directory.CreateDirectory(DestinationTempBackgroundDirectory);
                                                            if (Directory.Exists(DestinationTempBackgroundDirectory))
                                                            {
                                                                if (File.Exists(FileBackGroundSourcePath))
                                                                {
                                                                    if (!File.Exists(DestinationTempBackGroundImages))
                                                                        File.Copy(FileBackGroundSourcePath, DestinationTempBackGroundImages);
                                                                }


                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (File.Exists(FileBackGroundSourcePath))
                                                            {
                                                                if (!File.Exists(DestinationTempBackGroundImages))
                                                                    File.Copy(FileBackGroundSourcePath, DestinationTempBackGroundImages);
                                                            }

                                                        }
                                                        tempImg.ImageName = NewPath;
                                                    }



                                                }

                                            }
                                        }
                                        if (item.Template.TemplatePages != null && item.Template.TemplatePages.Count > 0)
                                        {
                                            foreach (var tempPage in item.Template.TemplatePages)
                                            {
                                                if (!string.IsNullOrEmpty(tempPage.BackgroundFileName))
                                                {
                                                    string name = tempPage.BackgroundFileName;

                                                    string FileName = Path.GetFileName(tempPage.BackgroundFileName);

                                                    string NewPath = tempPage.ProductId + "/" + FileName;

                                                    string[] tempID = tempPage.BackgroundFileName.Split('/');

                                                    string OldTempID = tempID[0];


                                                    string DestinationTempBackGroundImages = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + NewOrgID + "/Templates/" + NewPath);
                                                    DestinationsPath.Add(DestinationTempBackGroundImages);
                                                    string DestinationTempBackgroundDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + NewOrgID + "/Templates/" + tempPage.ProductId);
                                                    string FileBackGroundSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Designer/Organisation" + oldOrgID + "/Templates/" + OldTempID + "/" + FileName);
                                                    if (!System.IO.Directory.Exists(DestinationTempBackgroundDirectory))
                                                    {
                                                        Directory.CreateDirectory(DestinationTempBackgroundDirectory);
                                                        if (Directory.Exists(DestinationTempBackgroundDirectory))
                                                        {
                                                            if (File.Exists(FileBackGroundSourcePath))
                                                            {
                                                                if (!File.Exists(DestinationTempBackGroundImages))
                                                                    File.Copy(FileBackGroundSourcePath, DestinationTempBackGroundImages);
                                                            }


                                                        }

                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(FileBackGroundSourcePath))
                                                        {
                                                            if (!File.Exists(DestinationTempBackGroundImages))
                                                                File.Copy(FileBackGroundSourcePath, DestinationTempBackGroundImages);
                                                        }

                                                    }
                                                    tempPage.BackgroundFileName = NewPath;
                                                }
                                                string fileName = "templatImgBk" + tempPage.PageNo + ".jpg";
                                                string sPath = "/MPC_Content/Designer/Organisation" + NewOrgID + "/Templates/" + tempPage.ProductId + "/" + fileName;
                                                string FilePaths = HttpContext.Current.Server.MapPath("~/" + sPath);


                                                string DestinationDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + NewOrgID + "/Templates/" + tempPage.ProductId);
                                                string SourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Designer/Organisation" + NewOrgID + "/Templates/" + tempPage.ProductId + "/" + fileName);
                                                string DestinationPath = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + NewOrgID + "/Templates/" + tempPage.ProductId + "/" + fileName);
                                                if (!System.IO.Directory.Exists(DestinationDirectory))
                                                {
                                                    Directory.CreateDirectory(DestinationDirectory);
                                                    if (Directory.Exists(DestinationDirectory))
                                                    {
                                                        if (File.Exists(SourcePath))
                                                        {
                                                            if (!File.Exists(DestinationPath))
                                                                File.Copy(SourcePath, DestinationPath);
                                                        }


                                                    }

                                                }
                                                else
                                                {
                                                    if (File.Exists(SourcePath))
                                                    {
                                                        if (!File.Exists(DestinationPath))
                                                            File.Copy(SourcePath, DestinationPath);
                                                    }

                                                }


                                            }
                                        }



                                    }

                                }

                            }

                        }
                    }

                    List<TemplateFont> templatefonts = db.TemplateFonts.Where(c => c.CustomerId == oCID).ToList();
                    if (templatefonts != null && templatefonts.Count > 0)
                    {
                        foreach (var fonts in templatefonts)
                        {
                            string DestinationFontDirectory = string.Empty;
                            string companyoid = string.Empty;
                            string FontSourcePath = string.Empty;
                            string FontSourcePath1 = string.Empty;
                            string FontSourcePath2 = string.Empty;
                            string NewFilePath = string.Empty;
                            if (!string.IsNullOrEmpty(fonts.FontPath))
                            {

                                string NewPath = "Organisation" + NewOrgID + "/WebFonts/" + fonts.CustomerId;


                                DestinationFont1 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + NewPath + fonts.FontFile + ".eot");

                                DestinationFont2 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + NewPath + fonts.FontFile + ".ttf");

                                DestinationFont3 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + NewPath + fonts.FontFile + ".woff");

                                DestinationFontDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + NewPath);

                                FontSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Artworks/ImportStore/Designer/" + oldOrgID + "/WebFonts/" + fonts.FontPath + fonts.FontFile + ".eot");

                                FontSourcePath1 = HttpContext.Current.Server.MapPath("~/MPC_Content/Artworks/ImportStore/Designer/" + oldOrgID + "/WebFonts/" + fonts.FontPath + fonts.FontFile + ".ttf");

                                FontSourcePath2 = HttpContext.Current.Server.MapPath("~/MPC_Content/Artworks/ImportStore/Designer/" + oldOrgID + "/WebFonts/" + fonts.FontPath + fonts.FontFile + ".woff");

                                if (!System.IO.Directory.Exists(DestinationFontDirectory))
                                {
                                    Directory.CreateDirectory(DestinationFontDirectory);
                                    if (Directory.Exists(DestinationFontDirectory))
                                    {
                                        if (File.Exists(FontSourcePath))
                                        {
                                            if (!File.Exists(DestinationFont1))
                                                File.Copy(FontSourcePath, DestinationFont1);
                                        }

                                        if (File.Exists(FontSourcePath1))
                                        {
                                            if (!File.Exists(DestinationFont2))
                                                File.Copy(FontSourcePath1, DestinationFont2);

                                        }

                                        if (File.Exists(FontSourcePath2))
                                        {
                                            if (!File.Exists(DestinationFont3))
                                                File.Copy(FontSourcePath2, DestinationFont3);

                                        }

                                    }

                                }
                                else
                                {
                                    if (File.Exists(FontSourcePath))
                                    {
                                        if (!File.Exists(DestinationFont1))
                                            File.Copy(FontSourcePath, DestinationFont1);
                                    }

                                    if (File.Exists(FontSourcePath1))
                                    {
                                        if (!File.Exists(DestinationFont2))
                                            File.Copy(FontSourcePath1, DestinationFont2);

                                    }

                                    if (File.Exists(FontSourcePath2))
                                    {
                                        if (!File.Exists(DestinationFont3))
                                            File.Copy(FontSourcePath2, DestinationFont3);

                                    }

                                }
                                fonts.FontPath = NewPath;
                            }
                            //else
                            //{
                            //    DestinationFont1 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + NewOrgID + "/WebFonts/" + fonts.FontFile + ".eot");
                            //    DestinationFont2 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + NewOrgID + "/WebFonts/" + fonts.FontFile + ".ttf");
                            //    DestinationFont3 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + NewOrgID + "/WebFonts/" + fonts.FontFile + ".woff");

                            //    DestinationFontDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + NewOrgID + "/WebFonts");

                            //    FontSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Artworks/ImportStore/Designer/" + oldOrgID + "/WebFonts/" + fonts.FontFile + ".eot");

                            //    FontSourcePath1 = HttpContext.Current.Server.MapPath("~/MPC_Content/Artworks/ImportStore/Designer/" + oldOrgID + "/WebFonts/" + fonts.FontFile + ".ttf");

                            //    FontSourcePath2 = HttpContext.Current.Server.MapPath("~/MPC_Content/Artworks/ImportStore/Designer/" + oldOrgID + "/WebFonts/" + fonts.FontFile + ".woff");

                            //    if (!System.IO.Directory.Exists(DestinationFontDirectory))
                            //    {
                            //        Directory.CreateDirectory(DestinationFontDirectory);
                            //        if (Directory.Exists(DestinationFontDirectory))
                            //        {
                            //            if (File.Exists(FontSourcePath))
                            //            {
                            //                if (!File.Exists(DestinationFont1))
                            //                    File.Copy(FontSourcePath, DestinationFont1);
                            //            }

                            //            if (File.Exists(FontSourcePath1))
                            //            {
                            //                if (!File.Exists(DestinationFont2))
                            //                    File.Copy(FontSourcePath1, DestinationFont2);

                            //            }

                            //            if (File.Exists(FontSourcePath2))
                            //            {
                            //                if (!File.Exists(DestinationFont3))
                            //                    File.Copy(FontSourcePath2, DestinationFont3);

                            //            }

                            //        }

                            //    }
                            //    else
                            //    {
                            //        if (File.Exists(FontSourcePath))
                            //        {
                            //            if (!File.Exists(DestinationFont1))
                            //                File.Copy(FontSourcePath, DestinationFont1);
                            //        }

                            //        if (File.Exists(FontSourcePath1))
                            //        {
                            //            if (!File.Exists(DestinationFont2))
                            //                File.Copy(FontSourcePath1, DestinationFont2);

                            //        }

                            //        if (File.Exists(FontSourcePath2))
                            //        {
                            //            if (!File.Exists(DestinationFont3))
                            //                File.Copy(FontSourcePath2, DestinationFont3);

                            //        }

                            //    }

                            //}

                            DestinationsPath.Add(DestinationFont1);
                            DestinationsPath.Add(DestinationFont2);
                            DestinationsPath.Add(DestinationFont3);



                        }
                    }
                    db.SaveChanges();
                    status += "company items done";
                    // site.css
                    DestinationSiteFile = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + NewOrgID + "/" + oCID + "/Site.css");
                    DestinationsPath.Add(DestinationSiteFile);
                    string DestinationSiteFileDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + NewOrgID + "/" + oCID);
                    string SourceSiteFile = HttpContext.Current.Server.MapPath("~/MPC_Content/Artworks/ImportStore/Assets/" + oldOrgID + "/" + OldCompanyID + "/Site.css");
                    if (!System.IO.Directory.Exists(DestinationSiteFileDirectory))
                    {
                        Directory.CreateDirectory(DestinationSiteFileDirectory);
                        if (Directory.Exists(DestinationSiteFileDirectory))
                        {
                            if (File.Exists(SourceSiteFile))
                            {
                                if (!File.Exists(DestinationSiteFile))
                                    File.Copy(SourceSiteFile, DestinationSiteFile);
                            }


                        }


                    }
                    else
                    {
                        if (File.Exists(SourceSiteFile))
                        {
                            if (!File.Exists(DestinationSiteFile))
                                File.Copy(SourceSiteFile, DestinationSiteFile);
                        }

                    }

                    // sprite.png
                    DestinationSpriteFile = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + NewOrgID + "/" + oCID + "/Sprite.png");
                    DestinationsPath.Add(DestinationSpriteFile);
                    string DestinationSpriteDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + NewOrgID + "/" + oCID);
                    string SourceSpriteFile = HttpContext.Current.Server.MapPath("~/MPC_Content/Artworks/ImportStore/Assets/" + oldOrgID + "/" + OldCompanyID + "/Sprite.png");
                    if (!System.IO.Directory.Exists(DestinationSpriteDirectory))
                    {
                        Directory.CreateDirectory(DestinationSpriteDirectory);
                        if (Directory.Exists(DestinationSpriteDirectory))
                        {
                            if (File.Exists(SourceSiteFile))
                            {
                                if (!File.Exists(DestinationSpriteFile))
                                    File.Copy(SourceSpriteFile, DestinationSpriteFile);
                            }

                        }
                        else
                        {
                            if (File.Exists(SourceSpriteFile))
                            {
                                if (!File.Exists(DestinationSpriteFile))
                                    File.Copy(SourceSpriteFile, DestinationSpriteFile);
                            }

                        }


                    }
                    else
                    {
                        if (File.Exists(SourceSpriteFile))
                        {
                            if (!File.Exists(DestinationSpriteFile))
                                File.Copy(SourceSpriteFile, DestinationSpriteFile);
                        }
                    }
                    status += "company site or sprit done";
                }
                return status;
            }
            catch (Exception ex)
            {
                return status + "error copying";
                throw ex;
            }



        }

        public void DeletePhysicallFiles(string Path)
        {
            if (File.Exists(Path))
            {
                File.Delete(Path);
            }
        }
        public void DeleteStoryBySP(long StoreID)
        {
            try
            {
                db.usp_DeleteContactCompanyByID(Convert.ToInt32(StoreID));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void DeleteCrmCompanyBySP(long StoreId)
        {
            try
            {
                db.usp_DeleteCRMCompanyByID(Convert.ToInt32(StoreId));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public double? GetTaxRateByStoreId(long storeId)
        {
            if (storeId == 0)
            {
                return null;
            }
            Company company = DbSet.FirstOrDefault(x => x.CompanyId == storeId);
            return company != null ? company.TaxRate : null;
        }
        public Guid? GetStoreJobManagerId(long storeId)
        {
            if (storeId == 0)
            {
                return null;
            }
            Company company = DbSet.FirstOrDefault(x => x.CompanyId == storeId);
            return company != null ? company.ProductionManagerId1 : null;
        }

        public List<Company> GetSupplierByOrganisationid(long OID)
        {
            try
            {

                List<TemplateColorStyle> TemplateColorStyle = new List<TemplateColorStyle>();
                ExportOrganisation ObjExportOrg = new ExportOrganisation();
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;

                Mapper.CreateMap<Company, Company>()
             .ForMember(x => x.Activities, opt => opt.Ignore())
                .ForMember(x => x.ColorPalletes, opt => opt.Ignore())
                .ForMember(x => x.Estimates, opt => opt.Ignore())
                .ForMember(x => x.Invoices, opt => opt.Ignore())
                .ForMember(x => x.Items, opt => opt.Ignore())
                .ForMember(x => x.ProductCategories, opt => opt.Ignore());

                Mapper.CreateMap<CompanyDomain, CompanyDomain>()
                .ForMember(x => x.Company, opt => opt.Ignore());

                Mapper.CreateMap<CmsOffer, CmsOffer>()
               .ForMember(x => x.Company, opt => opt.Ignore());



                Mapper.CreateMap<CmsPage, CmsPage>()
                    .ForMember(x => x.CmsPageTags, opt => opt.Ignore())
                    .ForMember(x => x.PageCategory, opt => opt.Ignore())
                    .ForMember(x => x.CmsSkinPageWidgets, opt => opt.Ignore())
                    .ForMember(x => x.Company, opt => opt.Ignore());

                Mapper.CreateMap<MediaLibrary, MediaLibrary>()
              .ForMember(x => x.Company, opt => opt.Ignore());

                Mapper.CreateMap<CompanyBannerSet, CompanyBannerSet>()
            .ForMember(x => x.Company, opt => opt.Ignore());

                Mapper.CreateMap<CompanyBanner, CompanyBanner>()
             .ForMember(x => x.CompanyBannerSet, opt => opt.Ignore());


                Mapper.CreateMap<RaveReview, RaveReview>()
                 .ForMember(x => x.Company, opt => opt.Ignore());

                Mapper.CreateMap<CompanyTerritory, CompanyTerritory>()
              .ForMember(x => x.Addresses, opt => opt.Ignore())
              .ForMember(x => x.Company, opt => opt.Ignore())
              .ForMember(x => x.CompanyContacts, opt => opt.Ignore());


                Mapper.CreateMap<Address, Address>()
             .ForMember(x => x.Company, opt => opt.Ignore())
             .ForMember(x => x.CompanyContacts, opt => opt.Ignore())
             .ForMember(x => x.CompanyTerritory, opt => opt.Ignore())
             .ForMember(x => x.ShippingCompanyContacts, opt => opt.Ignore())
             .ForMember(x => x.State, opt => opt.Ignore())
             .ForMember(x => x.Country, opt => opt.Ignore());

                Mapper.CreateMap<CompanyContact, CompanyContact>()
            .ForMember(x => x.Company, opt => opt.Ignore())
            .ForMember(x => x.Address, opt => opt.Ignore())
             .ForMember(x => x.CompanyTerritory, opt => opt.Ignore())
              .ForMember(x => x.Estimates, opt => opt.Ignore())
              .ForMember(x => x.Inquiries, opt => opt.Ignore())
               .ForMember(x => x.Invoices, opt => opt.Ignore())
                .ForMember(x => x.NewsLetterSubscribers, opt => opt.Ignore())
                .ForMember(x => x.ShippingAddress, opt => opt.Ignore());


                Mapper.CreateMap<Campaign, Campaign>()
                   .ForMember(x => x.Company, opt => opt.Ignore());

                Mapper.CreateMap<PaymentGateway, PaymentGateway>()
                  .ForMember(x => x.Company, opt => opt.Ignore())
                  .ForMember(x => x.PaymentMethod, opt => opt.Ignore());

                Mapper.CreateMap<CmsSkinPageWidget, CmsSkinPageWidget>()
                  .ForMember(x => x.Company, opt => opt.Ignore())
                  .ForMember(x => x.Organisation, opt => opt.Ignore())
                  .ForMember(x => x.Widget, opt => opt.Ignore())
                  .ForMember(x => x.CmsPage, opt => opt.Ignore());

                Mapper.CreateMap<CmsSkinPageWidgetParam, CmsSkinPageWidgetParam>()
                    .ForMember(x => x.CmsSkinPageWidget, opt => opt.Ignore());

                Mapper.CreateMap<CompanyCostCentre, CompanyCostCentre>()
                  .ForMember(x => x.Company, opt => opt.Ignore())
                  .ForMember(x => x.CostCentre, opt => opt.Ignore());


                Mapper.CreateMap<CompanyCMYKColor, CompanyCMYKColor>()
                  .ForMember(x => x.Company, opt => opt.Ignore());


                Mapper.CreateMap<SmartForm, SmartForm>()
                  .ForMember(x => x.Company, opt => opt.Ignore());

                Mapper.CreateMap<SmartFormDetail, SmartFormDetail>()
                .ForMember(x => x.SmartForm, opt => opt.Ignore());

                Mapper.CreateMap<FieldVariable, FieldVariable>()
                    .ForMember(x => x.SmartFormDetails, opt => opt.Ignore())
                  .ForMember(x => x.Company, opt => opt.Ignore());


                db.Database.CommandTimeout = 1090;

                List<Company> ObjCompany = db.Companies.Include("CompanyDomains").Include("CmsSkinPageWidgets.CmsSkinPageWidgetParams").Include("CmsPages").Include("CmsOffers").Include("MediaLibraries").Include("CompanyBannerSets.CompanyBanners").Include("RaveReviews").Include("CompanyTerritories").Include("Addresses").Include("CompanyContacts").Include("Campaigns").Include("PaymentGateways").Include("CompanyCostCentres").Include("CompanyCmykColors").Include("SmartForms.SmartFormDetails").Include("FieldVariables").Where(c => c.OrganisationId == OID && c.IsCustomer == 2).ToList();


                //Include("CmsSkinPageWidgets")

                //List<CmsSkinPageWidget> widgets = db.PageWidgets.Include("CmsSkinPageWidgetParams").Where(c => c.CompanyId == CompanyId && c.PageId != null).ToList();
                //List<CmsPage> pages = db.CmsPages.Where(c => c.CompanyId == CompanyId).ToList();
                //if (widgets != null && widgets.Count > 0)
                //{
                //    ObjCompany.CmsSkinPageWidgets = widgets;
                //}
                //if (pages != null && pages.Count > 0)
                //{
                //    ObjCompany.CmsPages = pages;
                //}
                List<Company> suppliersList = new List<Company>();
                if (ObjCompany != null && ObjCompany.Count > 0)
                {
                    foreach (var comp in ObjCompany)
                    {
                        var omappedCompany = Mapper.Map<Company, Company>(comp);
                        suppliersList.Add(omappedCompany);
                    }
                }


                return suppliersList;



                //return db.Companies.Where(c => c.IsCustomer == 2 && c.OrganisationId == OID).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //public Company GetCompanyByCompanyIDforArtwork(long CompanyID)
        //{
        //    try
        //    {
        //        db.Configuration.LazyLoadingEnabled = false;
        //        return db.Companies.Where(c => c.CompanyId == CompanyID).FirstOrDefault();
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public string GetSupplierNameByID(int CID)
        {
          return  db.Companies.Where(c => c.CompanyId == CID).Select(x => x.Name).FirstOrDefault();
        }
        /// <summary>
        /// Check web access code exists
        /// </summary>
        /// <param name="subscriptionCode"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public Company isValidWebAccessCode(string WebAccessCode, long OrganisationId)
        {
            return db.Companies.Where(c => c.WebAccessCode == WebAccessCode && c.OrganisationId == OrganisationId && c.IsCustomer == (int)CustomerTypes.Corporate).SingleOrDefault();
        }

        public List<StoresListResponse> GetStoresNameByOrganisationId()
        {
            db.Configuration.LazyLoadingEnabled = false;
            
            List<Company> objCompany = db.Companies.Where(c => c.OrganisationId == OrganisationId && (c.IsCustomer == 3 || c.IsCustomer == 4)).ToList();

            List<StoresListResponse> response = new List<StoresListResponse>();
            if (objCompany != null && objCompany.Count > 0)
            {
                foreach(var obj in objCompany)
                {
                    StoresListResponse objRes = new StoresListResponse();
                    objRes.StoreID = obj.CompanyId;
                    objRes.StoreName = obj.Name;

                    response.Add(objRes);
                }
            }
            return response;

           
        }

        public void UpdateLiveStores(long organisationId, int storesCount)
        {
            db.Configuration.LazyLoadingEnabled = false;
            List<Company> LiveStores = DbSet.Where(c => c.OrganisationId == organisationId && c.isStoreLive == true).ToList();
            if (LiveStores.Count() > storesCount)
            {
                int ExtraLive = LiveStores.Count() - storesCount;
                List<Company> StoresToOffline = LiveStores.Take(ExtraLive).ToList();
                StoresToOffline.ForEach(c => c.isStoreLive = false);
                SaveChanges();
            }
        }

        public int GetLiveStoresCount(long organisationId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            return DbSet.Where(c => c.isStoreLive == true && (c.isArchived == false || c.isArchived == null) && c.OrganisationId == organisationId).Count();
        }

        public bool IsStoreLive(long storeId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var store = DbSet.Where(s => s.CompanyId == storeId && (s.isArchived == false || s.isArchived == null)).FirstOrDefault();
            return store != null && store.isStoreLive == true ? true : false;
        }

    }
}
