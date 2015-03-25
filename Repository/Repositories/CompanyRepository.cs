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

        public long GetStoreIdFromDomain(string domain)
        {
            try
            {
                var companyDomain = db.CompanyDomains.Where(d => d.Domain.Contains(domain)).ToList();
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
                    .Include(c => c.CompanyCMYKColors)
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
                        RaveReviews = c.RaveReviews.OrderBy(r => r.SortOrder).ToList(),
                        CmsPages = c.CmsPages.Where(page => page.isUserDefined==true).Take(5).Select(cms => new
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
                        c.CompanyCMYKColors,
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
                        Addresses = c.Addresses.Take(1).ToList(),
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
                        CompanyCMYKColors = c.CompanyCMYKColors,
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
                        cmp.CompanyId == companyId && cmp.isUserDefined==true),
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
                    (s.OrganisationId == OrganisationId && s.isArchived != true) && (s.IsCustomer == 3 || s.IsCustomer == 4);

                int rowCount = DbSet.Count(query);
                IEnumerable<Company> companies = request.IsAsc
                    ? DbSet.Where(query)
                        .OrderBy(companyOrderByClause[request.CompanyByColumn])
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : DbSet.Where(query)
                        .OrderByDescending(companyOrderByClause[request.CompanyByColumn])
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
                Expression<Func<Company, bool>> query =
                    s =>
                    ((!isStringSpecified || s.Name.Contains(request.SearchString)) && (isTypeSpecified && s.TypeId == type || !isTypeSpecified)) &&
                    (s.OrganisationId == OrganisationId && s.isArchived != true) && (s.IsCustomer == 1 || s.IsCustomer == 0);

                int rowCount = DbSet.Count(query);
                IEnumerable<Company> companies = request.IsAsc
                    ? DbSet.Where(query)
                        .OrderBy(companyOrderByClause[request.CompanyByColumn])
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : DbSet.Where(query)
                        .OrderByDescending(companyOrderByClause[request.CompanyByColumn])
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
                    ((!isStringSpecified || s.Name.Contains(request.SearchString)) ) &&
                    (s.OrganisationId == OrganisationId && s.isArchived != true) && (s.IsCustomer == 2);

                int rowCount = DbSet.Count(query);
                IEnumerable<Company> companies = request.IsAsc
                    ? DbSet.Where(query)
                        .OrderBy(companyOrderByClause[request.CompanyByColumn])
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : DbSet.Where(query)
                        .OrderByDescending(companyOrderByClause[request.CompanyByColumn])
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

                sets.ExportRetailStore1 = ExportRetailCompany1(CompanyId, sets);
                sets.ExportRetailStore3 = ExportRetailCompany3(CompanyId, sets, false);
                sets.ExportRetailStore2 = ExportRetailCompany2(CompanyId, sets, false);
                sets.ExportRetailStore4 = ExportRetailCompany4(CompanyId, sets, false);

                return sets;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ExportOrganisation ExportRetailCompany1(long CompanyId, ExportSets Sets)
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
                .ForMember(x => x.ProductCategories, opt => opt.Ignore())
                .ForMember(x => x.CmsPages, opt => opt.Ignore());

                Mapper.CreateMap<CompanyDomain, CompanyDomain>()
                .ForMember(x => x.Company, opt => opt.Ignore());

                Mapper.CreateMap<CmsOffer, CmsOffer>()
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
                   .ForMember(x => x.CmsSkinPageWidgetParams, opt => opt.Ignore())
                   .ForMember(x => x.Organisation, opt => opt.Ignore())
                   .ForMember(x => x.Widget, opt => opt.Ignore())
                   .ForMember(x => x.CmsPage, opt => opt.Ignore());

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


                 db.Database.CommandTimeout = 1080;

                 Company ObjCompany = db.Companies.Include("CompanyDomains").Include("CmsOffers").Include("MediaLibraries").Include("CompanyBannerSets.CompanyBanners").Include("RaveReviews").Include("CompanyTerritories").Include("Addresses").Include("CompanyContacts").Include("Campaigns").Include("PaymentGateways").Include("CompanyCostCentres").Include("CompanyCmykColors").Include("SmartForms.SmartFormDetails").Include("FieldVariables").Where(c => c.CompanyId == CompanyId).FirstOrDefault();


                 //Include("CmsSkinPageWidgets")

                 List<CmsSkinPageWidget> widgets = db.PageWidgets.Where(c => c.CompanyId == CompanyId && c.PageId != null).ToList();

                if(widgets != null && widgets.Count > 0)
                {
                    ObjCompany.CmsSkinPageWidgets = widgets;
                }

                 var omappedCompany = Mapper.Map<Company, Company>(ObjCompany);
                 
                 ObjExportOrg.RetailCompany = omappedCompany;

              //  ObjExportOrg.RetailCompany = db.Companies.Where(c => c.CompanyId == CompanyId).FirstOrDefault();



              //  // set Company Domain

              //  ObjExportOrg.RetailCompanyDomain = db.CompanyDomains.Where(c => c.CompanyId == CompanyId).ToList();

              //  // set cms offers

              //  ObjExportOrg.RetailCmsOffer = db.CmsOffers.Where(c => c.CompanyId == CompanyId).ToList();


              //  ObjExportOrg.RetailMediaLibrary = db.MediaLibraries.Where(c => c.CompanyId == CompanyId).ToList();


              //  List<CompanyBannerSet> bannerSets = new List<CompanyBannerSet>();
              //  bannerSets = db.CompanyBannerSets.Include("CompanyBanners").Where(c => c.CompanyId == CompanyId).ToList();

              //  List<CompanyBanner> Lstbanner = new List<CompanyBanner>();
              //  // company banners
              //  if (bannerSets != null)
              //  {
              //      List<CompanyBannerSet> CompanyBannerSet = bannerSets;
              //      ObjExportOrg.RetailCompanyBannerSet = CompanyBannerSet;
              //      if (CompanyBannerSet != null && CompanyBannerSet.Count > 0)
              //      {
              //          foreach (var banner in CompanyBannerSet)
              //          {
              //              if (banner.CompanyBanners != null)
              //              {
              //                  if (banner.CompanyBanners.Count > 0)
              //                  {
              //                      foreach (var bann in banner.CompanyBanners)
              //                      {
              //                          Lstbanner.Add(bann);
              //                      }
              //                  }
              //              }
              //          }

              //      }
              //      ObjExportOrg.RetailCompanyBanner = Lstbanner.ToList();
              //  }

              //  //// Secondary Pages

              //  //List<CmsPage> pages = db.CmsPages.Where(c => c.CompanyId == CompanyId).ToList();

              //  //pages.ToList().ForEach(s => s.CmsSkinPageWidgets = null);
              //  //pages.ToList().ForEach(s => s.PageCategory = null);

              //  //pages.ToList().ForEach(s => s.Company = null);


              //  //ObjExportOrg.RetailSecondaryPages = pages;


              //  //Rave Reviews

              //  ObjExportOrg.RetailRaveReview = db.RaveReviews.Where(r => r.CompanyId == CompanyId).ToList();



              //  //  CompanyTerritories


              //  ObjExportOrg.RetailCompanyTerritory = db.CompanyTerritories.Where(c => c.CompanyId == CompanyId).ToList();


              //  //  Addresses


              //  ObjExportOrg.RetailAddress = db.Addesses.Where(a => a.CompanyId == CompanyId).ToList();


              //  //  contacts


              //  ObjExportOrg.RetailCompanyContact = db.CompanyContacts.Where(c => c.CompanyId == CompanyId).ToList();

             

              //  List<Campaign> campaigns = db.Campaigns.Where(c => c.CompanyId == CompanyId).ToList();
              //  campaigns.ToList().ForEach(s => s.Company = null);
              //  campaigns.ToList().ForEach(s => s.CampaignImages = null);

              //  ObjExportOrg.RetailCampaigns = campaigns;

              //  //   payment gateways

              //  ObjExportOrg.RetailPaymentGateways = db.PaymentGateways.Where(c => c.CompanyId == CompanyId).ToList();

              //  // cms skin page widgets

              //  List<CmsSkinPageWidget> widgets = db.PageWidgets.Where(c => c.CompanyId == CompanyId).ToList();
              
              //  widgets.ToList().ForEach(w => w.Company = null);
              //  widgets.ToList().ForEach(w => w.Organisation = null);
              //  widgets.ToList().ForEach(w => w.Widget = null);

              //  //  company cost centre

              //  ObjExportOrg.RetailCompanyCostCentre = db.CompanyCostCentres.Where(c => c.CompanyId == CompanyId).ToList();


              //  // company cmyk colors
              //  ObjExportOrg.RetailCompanyCMYKColor = db.CompanyCmykColors.Where(c => c.CompanyId == CompanyId).ToList();


              // List<SmartForm> smartForms = db.SmartForms.Where(c => c.CompanyId == CompanyId).ToList();
              // ObjExportOrg.RetailSmartForms = smartForms;


              //  List<FieldVariable> variables = db.FieldVariables.Where(c => c.CompanyId == CompanyId).ToList();
              // // variables.ToList().ForEach(s => s.ScopeVariables = null);
              //  variables.ToList().ForEach(s => s.Company = null);
              // // variables.ToList().ForEach(s => s.SmartFormDetails = null);
              ////  variables.ToList().ForEach(s => s.VariableOptions = null);

              //  ObjExportOrg.RetailFieldVariables = variables;


                //  template color style
                List<TemplateColorStyle> lstTemplateColorStyle = db.TemplateColorStyles.Where(c => c.CustomerId == CompanyId).ToList();
                if (lstTemplateColorStyle != null && lstTemplateColorStyle.Count > 0)
                {
                    foreach (var tempStyle in lstTemplateColorStyle)
                    {
                        TemplateColorStyle.Add(tempStyle);
                    }

                }

               ObjExportOrg.RetailTemplateColorStyle = TemplateColorStyle;

               string JsonRetail = JsonConvert.SerializeObject(ObjExportOrg, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                // export json file
                string sRetailPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/RetailJson1.txt";
                System.IO.File.WriteAllText(sRetailPath, JsonRetail);
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

        public List<ProductCategory> ExportRetailCompany2(long CompanyId, ExportSets Sets,bool isCorp)
        {
            try
            {
               
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;

                List<ProductCategory> productCategories = new List<ProductCategory>();

                Mapper.CreateMap<ProductCategory, ProductCategory>()
               .ForMember(x => x.Company, opt => opt.Ignore())
               .ForMember(x => x.ProductCategoryItems, opt => opt.Ignore());
              

                List<ProductCategory> categories = db.ProductCategories.Where(s => s.isArchived != true && s.CompanyId == CompanyId).ToList();
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
                if(isCorp)
                {
                    string sRetailPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/CorporateProductCategories.txt";
                    System.IO.File.WriteAllText(sRetailPath, JsonRetail);
                }
                else
                {
                    string sRetailPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/RetailProductCategories.txt";
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
        public List<Item> ExportRetailCompany3(long CompanyId, ExportSets Sets,bool isCorp)
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
              .ForMember(x => x.Machine, opt => opt.Ignore());

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

              List<Item> items = db.Items.Include("ItemSections.SectionCostcentres.SectionCostCentreResources").Include("ItemStockOptions.ItemAddonCostCentres").Include("ProductCategoryItems").Include("ItemVdpPrices").Include("ItemPriceMatrices").Include("ItemProductDetails").Include("ItemStateTaxes").Include("ItemImages").Include("ItemRelatedItems").Include("ItemVideos").Include("Template.TemplatePages").Include("Template.TemplateObjects").Include("Template.TemplateFonts").Include("Template.TemplateFonts").Include("Template.TemplateBackgroundImages.ImagePermissions").Where(i => i.IsArchived != true && i.CompanyId == CompanyId && i.EstimateId == null).ToList();
              List<Item> oOutputItems = new List<Item>();
             
              if(items != null && items.Count > 0)
              {
                  foreach (var item in items)
                  {
                      var omappedItem = Mapper.Map<Item, Item>(item);
                      oOutputItems.Add(omappedItem);
                  }
              }
             
            

              string jsonRetail = JsonConvert.SerializeObject(oOutputItems, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            if(isCorp)
            {
                string sCorpPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/CorporateJson2.txt";
                System.IO.File.WriteAllText(sCorpPath, jsonRetail);
             
            }
            else
            {
                string sRetailPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/RetailJson2.txt";
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
        public List<CmsPage> ExportRetailCompany4(long CompanyId,ExportSets sets,bool isCorp)
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
               
                if(isCorp)
                {
                    string CorpPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/CorporateSecondaryPages.txt";
                    System.IO.File.WriteAllText(CorpPath, JsonRetail);

                }
                else
                {
                    string sRetailPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/RetailSecondaryPages.txt";
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

                sets.ExportStore1 = ExportCorporateCompany1(CompanyId, sets);
                sets.ExportStore3 = ExportRetailCompany3(CompanyId, sets, true);
                sets.ExportStore2 = ExportRetailCompany2(CompanyId, sets, true);
                sets.ExportStore4 = ExportRetailCompany4(CompanyId, sets, true);

                return sets;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ExportOrganisation ExportCorporateCompany1(long CompanyId, ExportSets Sets)
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
                .ForMember(x => x.ProductCategories, opt => opt.Ignore())
                .ForMember(x => x.CmsPages, opt => opt.Ignore());

                Mapper.CreateMap<CompanyDomain, CompanyDomain>()
                .ForMember(x => x.Company, opt => opt.Ignore());

                Mapper.CreateMap<CmsOffer, CmsOffer>()
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
                  .ForMember(x => x.CmsSkinPageWidgetParams, opt => opt.Ignore())
                  .ForMember(x => x.Organisation, opt => opt.Ignore())
                  .ForMember(x => x.Widget, opt => opt.Ignore())
                  .ForMember(x => x.CmsPage, opt => opt.Ignore());

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


                db.Database.CommandTimeout = 1080;

                Company ObjCompany = db.Companies.Include("CompanyDomains").Include("CmsOffers").Include("MediaLibraries").Include("CompanyBannerSets.CompanyBanners").Include("RaveReviews").Include("CompanyTerritories").Include("Addresses").Include("CompanyContacts").Include("Campaigns").Include("PaymentGateways").Include("CompanyCostCentres").Include("CompanyCmykColors").Include("SmartForms.SmartFormDetails").Include("FieldVariables").Where(c => c.CompanyId == CompanyId).FirstOrDefault();


                //Include("CmsSkinPageWidgets")

                List<CmsSkinPageWidget> widgets = db.PageWidgets.Where(c => c.CompanyId == CompanyId && c.PageId != null).ToList();

                if (widgets != null && widgets.Count > 0)
                {
                    ObjCompany.CmsSkinPageWidgets = widgets;
                }

                var omappedCompany = Mapper.Map<Company, Company>(ObjCompany);

                ObjExportOrg.Company = omappedCompany;

                //List<TemplateColorStyle> TemplateColorStyle = new List<TemplateColorStyle>();
                //ExportOrganisation ObjExportOrg = new ExportOrganisation();
                //db.Configuration.LazyLoadingEnabled = false;
                //db.Configuration.ProxyCreationEnabled = false;

                //ObjExportOrg.Company = db.Companies.Where(c => c.CompanyId == CompanyId).FirstOrDefault();



                //// set Company Domain

                //ObjExportOrg.CompanyDomain = db.CompanyDomains.Where(c => c.CompanyId == CompanyId).ToList();

                //// set cms offers

                //ObjExportOrg.CmsOffer = db.CmsOffers.Where(c => c.CompanyId == CompanyId).ToList();


                //ObjExportOrg.MediaLibrary = db.MediaLibraries.Where(c => c.CompanyId == CompanyId).ToList();


                //List<CompanyBannerSet> bannerSets = new List<CompanyBannerSet>();
                //bannerSets = db.CompanyBannerSets.Include("CompanyBanners").Where(c => c.CompanyId == CompanyId).ToList();

                //List<CompanyBanner> Lstbanner = new List<CompanyBanner>();
                //// company banners
                //if (bannerSets != null)
                //{
                //    List<CompanyBannerSet> CompanyBannerSet = bannerSets;
                //    ObjExportOrg.CompanyBannerSet = CompanyBannerSet;
                //    if (CompanyBannerSet != null && CompanyBannerSet.Count > 0)
                //    {
                //        foreach (var banner in CompanyBannerSet)
                //        {
                //            if (banner.CompanyBanners != null)
                //            {
                //                if (banner.CompanyBanners.Count > 0)
                //                {
                //                    foreach (var bann in banner.CompanyBanners)
                //                    {
                //                        Lstbanner.Add(bann);
                //                    }
                //                }
                //            }
                //        }

                //    }
                //    ObjExportOrg.CompanyBanner = Lstbanner.ToList();
                //}

                ////// Secondary Pages

                ////List<CmsPage> pages = db.CmsPages.Where(c => c.CompanyId == CompanyId).ToList();

                ////pages.ToList().ForEach(s => s.CmsSkinPageWidgets = null);
                ////pages.ToList().ForEach(s => s.PageCategory = null);

                ////pages.ToList().ForEach(s => s.Company = null);


                ////ObjExportOrg.RetailSecondaryPages = pages;


                ////Rave Reviews

                //ObjExportOrg.RaveReview = db.RaveReviews.Where(r => r.CompanyId == CompanyId).ToList();



                ////  CompanyTerritories


                //ObjExportOrg.CompanyTerritory = db.CompanyTerritories.Where(c => c.CompanyId == CompanyId).ToList();


                ////  Addresses


                //ObjExportOrg.Address = db.Addesses.Where(a => a.CompanyId == CompanyId).ToList();


                ////  contacts


                //ObjExportOrg.CompanyContact = db.CompanyContacts.Where(c => c.CompanyId == CompanyId).ToList();



                //List<Campaign> campaigns = db.Campaigns.Where(c => c.CompanyId == CompanyId).ToList();
                //campaigns.ToList().ForEach(s => s.Company = null);
                //campaigns.ToList().ForEach(s => s.CampaignImages = null);

                //ObjExportOrg.Campaigns = campaigns;

                ////   payment gateways

                //ObjExportOrg.PaymentGateways = db.PaymentGateways.Where(c => c.CompanyId == CompanyId).ToList();

                //// cms skin page widgets

                //List<CmsSkinPageWidget> widgets = db.PageWidgets.Where(c => c.CompanyId == CompanyId).ToList();

                //widgets.ToList().ForEach(w => w.Company = null);
                //widgets.ToList().ForEach(w => w.Organisation = null);
                //widgets.ToList().ForEach(w => w.Widget = null);

                ////  company cost centre

                //ObjExportOrg.CompanyCostCentre = db.CompanyCostCentres.Where(c => c.CompanyId == CompanyId).ToList();


                //// company cmyk colors
                //ObjExportOrg.CompanyCMYKColor = db.CompanyCmykColors.Where(c => c.CompanyId == CompanyId).ToList();


                //List<SmartForm> smartForms = db.SmartForms.Where(c => c.CompanyId == CompanyId).ToList();
                //ObjExportOrg.SmartForms = smartForms;


                //List<FieldVariable> variables = db.FieldVariables.Where(c => c.CompanyId == CompanyId).ToList();
                //// variables.ToList().ForEach(s => s.ScopeVariables = null);
                //variables.ToList().ForEach(s => s.Company = null);
                //// variables.ToList().ForEach(s => s.SmartFormDetails = null);
                ////  variables.ToList().ForEach(s => s.VariableOptions = null);

                //ObjExportOrg.FieldVariables = variables;

                //  template color style
                List<TemplateColorStyle> lstTemplateColorStyle = db.TemplateColorStyles.Where(c => c.CustomerId == CompanyId).ToList();
                if (lstTemplateColorStyle != null && lstTemplateColorStyle.Count > 0)
                {
                    foreach (var tempStyle in lstTemplateColorStyle)
                    {
                        TemplateColorStyle.Add(tempStyle);
                    }

                }

                ObjExportOrg.TemplateColorStyle = TemplateColorStyle;
                TemplateColorStyle = null;
                string JsonRetail = JsonConvert.SerializeObject(ObjExportOrg, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                // export json file
                string sRetailPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/Organisations/CorporateJson1.txt";
                System.IO.File.WriteAllText(sRetailPath, JsonRetail);
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
       

        public long GetCorporateCompanyIDbyOrganisationID(long OID)
        {
            try
            {
                return db.Companies.Where(o => o.OrganisationId == OID && o.IsCustomer == 3).Select(c => c.CompanyId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public long GetRetailCompanyIDbyOrganisationID(long OID)
        {
            try
            {
                return db.Companies.Where(o => o.OrganisationId == OID && o.IsCustomer == 4).Select(c => c.CompanyId).FirstOrDefault();
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
                return DbSet.Where(supplier => supplier.OrganisationId == OrganisationId && supplier.IsCustomer == 0).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public long CreateCustomer(string CompanyName, bool isEmailSubscriber, bool isNewsLetterSubscriber, CompanyTypes customerType, string RegWithSocialMedia, long OrganisationId, CompanyContact contact = null)
        {
            try
            {
                bool isCreateTemporaryCompany = true;
                if ((int)customerType == (int)CompanyTypes.TemporaryCustomer)
                {
                    Company ContactCompany = db.Companies.Where(c => c.TypeId == (int)customerType && c.OrganisationId == OrganisationId).FirstOrDefault();
                    if (ContactCompany != null)
                    {
                        isCreateTemporaryCompany = false;
                        return ContactCompany.CompanyId;
                    }
                    else
                    {
                        isCreateTemporaryCompany = true;
                    }

                }

                if (isCreateTemporaryCompany)
                {
                    Address Contactaddress = null;

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

                    //Create Billing Address and Delivery Address and mark them default billing and shipping
                    Contactaddress = PopulateAddressObject(0, ContactCompany.CompanyId, true, true);
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
        private Address PopulateAddressObject(long addressId, long companyId, bool isDefaulAddress, bool isDefaultShippingAddress)
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
                         !isStringSpecified) && s.IsCustomer == request.IsCustomerType && s.OrganisationId == OrganisationId;

                int rowCount = DbSet.Count(query);
                IEnumerable<Company> companies =
                    DbSet.Where(query).OrderByDescending(x => x.Name)
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
                return DbSet.Count(company => !company.isArchived.HasValue && company.OrganisationId == OrganisationId);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
