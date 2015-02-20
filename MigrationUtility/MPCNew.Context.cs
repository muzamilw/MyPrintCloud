﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MigrationUtility
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MPCPreviewEntities : DbContext
    {
        public MPCPreviewEntities()
            : base("name=MPCPreviewEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AccessRight> AccessRights { get; set; }
        public virtual DbSet<AccountDefault> AccountDefaults { get; set; }
        public virtual DbSet<AccountType> AccountTypes { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<ActivityType> ActivityTypes { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AppVersion> AppVersions { get; set; }
        public virtual DbSet<Audit> Audits { get; set; }
        public virtual DbSet<BusinessType> BusinessTypes { get; set; }
        public virtual DbSet<Campaign> Campaigns { get; set; }
        public virtual DbSet<CampaignClickThrough> CampaignClickThroughs { get; set; }
        public virtual DbSet<CampaignEmailQueue> CampaignEmailQueues { get; set; }
        public virtual DbSet<CampaignEmailVariable> CampaignEmailVariables { get; set; }
        public virtual DbSet<CampaignGroup> CampaignGroups { get; set; }
        public virtual DbSet<CampaignImage> CampaignImages { get; set; }
        public virtual DbSet<CampaignReport> CampaignReports { get; set; }
        public virtual DbSet<CategoryTerritory> CategoryTerritories { get; set; }
        public virtual DbSet<ChartOfAccount> ChartOfAccounts { get; set; }
        public virtual DbSet<CmsOffer> CmsOffers { get; set; }
        public virtual DbSet<CmsPage> CmsPages { get; set; }
        public virtual DbSet<CmsPageTag> CmsPageTags { get; set; }
        public virtual DbSet<CmsSkinPageWidget> CmsSkinPageWidgets { get; set; }
        public virtual DbSet<CmsSkinPageWidgetParam> CmsSkinPageWidgetParams { get; set; }
        public virtual DbSet<CmsTag> CmsTags { get; set; }
        public virtual DbSet<ColorPallete> ColorPalletes { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanyBanner> CompanyBanners { get; set; }
        public virtual DbSet<CompanyBannerSet> CompanyBannerSets { get; set; }
        public virtual DbSet<CompanyCMYKColor> CompanyCMYKColors { get; set; }
        public virtual DbSet<CompanyContact> CompanyContacts { get; set; }
        public virtual DbSet<CompanyContactRole> CompanyContactRoles { get; set; }
        public virtual DbSet<CompanyCostCentre> CompanyCostCentres { get; set; }
        public virtual DbSet<CompanyDomain> CompanyDomains { get; set; }
        public virtual DbSet<CompanyTerritory> CompanyTerritories { get; set; }
        public virtual DbSet<CompanyType> CompanyTypes { get; set; }
        public virtual DbSet<Correspondence> Correspondences { get; set; }
        public virtual DbSet<CorrespondenceDetail> CorrespondenceDetails { get; set; }
        public virtual DbSet<CostCenterChoice> CostCenterChoices { get; set; }
        public virtual DbSet<CostCentre> CostCentres { get; set; }
        public virtual DbSet<CostCentreAnswer> CostCentreAnswers { get; set; }
        public virtual DbSet<CostCentreGroup> CostCentreGroups { get; set; }
        public virtual DbSet<CostcentreGroupDetail> CostcentreGroupDetails { get; set; }
        public virtual DbSet<CostcentreInstruction> CostcentreInstructions { get; set; }
        public virtual DbSet<CostCentreMatrix> CostCentreMatrices { get; set; }
        public virtual DbSet<CostCentreMatrixDetail> CostCentreMatrixDetails { get; set; }
        public virtual DbSet<CostCentreQuestion> CostCentreQuestions { get; set; }
        public virtual DbSet<CostcentreResource> CostcentreResources { get; set; }
        public virtual DbSet<CostcentreSystemType> CostcentreSystemTypes { get; set; }
        public virtual DbSet<CostCentreTemplate> CostCentreTemplates { get; set; }
        public virtual DbSet<CostCentreType> CostCentreTypes { get; set; }
        public virtual DbSet<CostCentreVariable> CostCentreVariables { get; set; }
        public virtual DbSet<CostCentreVariableType> CostCentreVariableTypes { get; set; }
        public virtual DbSet<CostcentreWorkInstructionsChoice> CostcentreWorkInstructionsChoices { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<CreditCardInformation> CreditCardInformations { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<CustomCopy> CustomCopies { get; set; }
        public virtual DbSet<CustomizedField> CustomizedFields { get; set; }
        public virtual DbSet<CustomizedFieldsData> CustomizedFieldsDatas { get; set; }
        public virtual DbSet<CustomizedFieldsValue> CustomizedFieldsValues { get; set; }
        public virtual DbSet<DeliveryNote> DeliveryNotes { get; set; }
        public virtual DbSet<DeliveryNoteDetail> DeliveryNoteDetails { get; set; }
        public virtual DbSet<DiscountVoucher> DiscountVouchers { get; set; }
        public virtual DbSet<dtproperty> dtproperties { get; set; }
        public virtual DbSet<EmailCampaignTracking> EmailCampaignTrackings { get; set; }
        public virtual DbSet<EmailEvent> EmailEvents { get; set; }
        public virtual DbSet<Estimate> Estimates { get; set; }
        public virtual DbSet<EstimateProjection> EstimateProjections { get; set; }
        public virtual DbSet<FavoriteDesign> FavoriteDesigns { get; set; }
        public virtual DbSet<FaxCampaign> FaxCampaigns { get; set; }
        public virtual DbSet<FaxCampaignsTracking> FaxCampaignsTrackings { get; set; }
        public virtual DbSet<FieldVariable> FieldVariables { get; set; }
        public virtual DbSet<FinancialYear> FinancialYears { get; set; }
        public virtual DbSet<FinancialYearDetail> FinancialYearDetails { get; set; }
        public virtual DbSet<GlobalLanguage> GlobalLanguages { get; set; }
        public virtual DbSet<GoodsReceivedNote> GoodsReceivedNotes { get; set; }
        public virtual DbSet<GoodsReceivedNoteDetail> GoodsReceivedNoteDetails { get; set; }
        public virtual DbSet<GroupDetail> GroupDetails { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<ImagePermission> ImagePermissions { get; set; }
        public virtual DbSet<InkCoverageGroup> InkCoverageGroups { get; set; }
        public virtual DbSet<InkPlateSide> InkPlateSides { get; set; }
        public virtual DbSet<Inquiry> Inquiries { get; set; }
        public virtual DbSet<InquiryAttachment> InquiryAttachments { get; set; }
        public virtual DbSet<InquiryItem> InquiryItems { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual DbSet<ItemAddonCostCentre> ItemAddonCostCentres { get; set; }
        public virtual DbSet<ItemAttachment> ItemAttachments { get; set; }
        public virtual DbSet<ItemImage> ItemImages { get; set; }
        public virtual DbSet<ItemPriceMatrix> ItemPriceMatrices { get; set; }
        public virtual DbSet<ItemProductDetail> ItemProductDetails { get; set; }
        public virtual DbSet<ItemRelatedItem> ItemRelatedItems { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemSection> ItemSections { get; set; }
        public virtual DbSet<ItemSectionCostCentreGroup> ItemSectionCostCentreGroups { get; set; }
        public virtual DbSet<ItemStateTax> ItemStateTaxes { get; set; }
        public virtual DbSet<ItemStockControl> ItemStockControls { get; set; }
        public virtual DbSet<ItemStockOption> ItemStockOptions { get; set; }
        public virtual DbSet<ItemStockUpdateHistory> ItemStockUpdateHistories { get; set; }
        public virtual DbSet<ItemType> ItemTypes { get; set; }
        public virtual DbSet<ItemVDPPrice> ItemVDPPrices { get; set; }
        public virtual DbSet<ItemVideo> ItemVideos { get; set; }
        public virtual DbSet<JobPreference> JobPreferences { get; set; }
        public virtual DbSet<JobStatu> JobStatus { get; set; }
        public virtual DbSet<LengthUnit> LengthUnits { get; set; }
        public virtual DbSet<Listing> Listings { get; set; }
        public virtual DbSet<ListingAgent> ListingAgents { get; set; }
        public virtual DbSet<ListingConjunctionAgent> ListingConjunctionAgents { get; set; }
        public virtual DbSet<ListingFloorPlan> ListingFloorPlans { get; set; }
        public virtual DbSet<ListingImage> ListingImages { get; set; }
        public virtual DbSet<ListingLink> ListingLinks { get; set; }
        public virtual DbSet<ListingOFI> ListingOFIs { get; set; }
        public virtual DbSet<ListingVendor> ListingVendors { get; set; }
        public virtual DbSet<LookupMethod> LookupMethods { get; set; }
        public virtual DbSet<Machine> Machines { get; set; }
        public virtual DbSet<MachineCategory> MachineCategories { get; set; }
        public virtual DbSet<MachineClickChargeLookup> MachineClickChargeLookups { get; set; }
        public virtual DbSet<MachineClickChargeZone> MachineClickChargeZones { get; set; }
        public virtual DbSet<MachineCostCentreGroup> MachineCostCentreGroups { get; set; }
        public virtual DbSet<MachineCylinder> MachineCylinders { get; set; }
        public virtual DbSet<MachineGuillotineCalc> MachineGuillotineCalcs { get; set; }
        public virtual DbSet<MachineGuilotinePtv> MachineGuilotinePtvs { get; set; }
        public virtual DbSet<MachineInkCoverage> MachineInkCoverages { get; set; }
        public virtual DbSet<MachineLookupMethod> MachineLookupMethods { get; set; }
        public virtual DbSet<MachineMeterPerHourLookup> MachineMeterPerHourLookups { get; set; }
        public virtual DbSet<MachinePaginationProfile> MachinePaginationProfiles { get; set; }
        public virtual DbSet<MachinePerHourLookup> MachinePerHourLookups { get; set; }
        public virtual DbSet<MachineResource> MachineResources { get; set; }
        public virtual DbSet<MachineSpeedWeightLookup> MachineSpeedWeightLookups { get; set; }
        public virtual DbSet<MachineSpoilage> MachineSpoilages { get; set; }
        public virtual DbSet<margin> margins { get; set; }
        public virtual DbSet<Markup> Markups { get; set; }
        public virtual DbSet<MediaLibrary> MediaLibraries { get; set; }
        public virtual DbSet<NewsLetterSubscriber> NewsLetterSubscribers { get; set; }
        public virtual DbSet<Organisation> Organisations { get; set; }
        public virtual DbSet<PageCategory> PageCategories { get; set; }
        public virtual DbSet<PaginationCombination> PaginationCombinations { get; set; }
        public virtual DbSet<PaginationFinishStyle> PaginationFinishStyles { get; set; }
        public virtual DbSet<PaginationProfile> PaginationProfiles { get; set; }
        public virtual DbSet<PaginationProfileCostcentreGroup> PaginationProfileCostcentreGroups { get; set; }
        public virtual DbSet<PaperBasisArea> PaperBasisAreas { get; set; }
        public virtual DbSet<PaperSize> PaperSizes { get; set; }
        public virtual DbSet<PaymentGateway> PaymentGateways { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<PaypalPaymentRequest> PaypalPaymentRequests { get; set; }
        public virtual DbSet<PayPalResponse> PayPalResponses { get; set; }
        public virtual DbSet<Phrase> Phrases { get; set; }
        public virtual DbSet<PhraseField> PhraseFields { get; set; }
        public virtual DbSet<PipelineDeduction> PipelineDeductions { get; set; }
        public virtual DbSet<PipeLineProduct> PipeLineProducts { get; set; }
        public virtual DbSet<PipeLineSource> PipeLineSources { get; set; }
        public virtual DbSet<prefix> prefixes { get; set; }
        public virtual DbSet<PrePayment> PrePayments { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductCategoryFoldLine> ProductCategoryFoldLines { get; set; }
        public virtual DbSet<ProductCategoryItem> ProductCategoryItems { get; set; }
        public virtual DbSet<ProductMarketBriefAnswer> ProductMarketBriefAnswers { get; set; }
        public virtual DbSet<ProductMarketBriefQuestion> ProductMarketBriefQuestions { get; set; }
        public virtual DbSet<ProductPaperType> ProductPaperTypes { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<ProfileCostCentreGroup> ProfileCostCentreGroups { get; set; }
        public virtual DbSet<ProfileDescriptionLabel> ProfileDescriptionLabels { get; set; }
        public virtual DbSet<ProfileDescriptionLabelsValue> ProfileDescriptionLabelsValues { get; set; }
        public virtual DbSet<ProfileType> ProfileTypes { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public virtual DbSet<RaveReview> RaveReviews { get; set; }
        public virtual DbSet<RegionalsSetting> RegionalsSettings { get; set; }
        public virtual DbSet<RegistrationQuestion> RegistrationQuestions { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<ReportCategory> ReportCategories { get; set; }
        public virtual DbSet<ReportNote> ReportNotes { get; set; }
        public virtual DbSet<Reportparam> Reportparams { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Roleright> Rolerights { get; set; }
        public virtual DbSet<RoleSection> RoleSections { get; set; }
        public virtual DbSet<RoleSite> RoleSites { get; set; }
        public virtual DbSet<SalesCommissionRangeBased> SalesCommissionRangeBaseds { get; set; }
        public virtual DbSet<SalesCommissionType> SalesCommissionTypes { get; set; }
        public virtual DbSet<SalesTargetType> SalesTargetTypes { get; set; }
        public virtual DbSet<SalesType> SalesTypes { get; set; }
        public virtual DbSet<ScheduledActivitySpliteDetail> ScheduledActivitySpliteDetails { get; set; }
        public virtual DbSet<ScheduledCostCenter> ScheduledCostCenters { get; set; }
        public virtual DbSet<ScheduledPrintJob> ScheduledPrintJobs { get; set; }
        public virtual DbSet<ScheduledTimeActivity> ScheduledTimeActivities { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<SectionCostcentre> SectionCostcentres { get; set; }
        public virtual DbSet<SectionCostCentreDetail> SectionCostCentreDetails { get; set; }
        public virtual DbSet<SectionCostCentreResource> SectionCostCentreResources { get; set; }
        public virtual DbSet<SectionFlag> SectionFlags { get; set; }
        public virtual DbSet<SectionInkCoverage> SectionInkCoverages { get; set; }
        public virtual DbSet<ShippingInformation> ShippingInformations { get; set; }
        public virtual DbSet<SoftwareUpdate> SoftwareUpdates { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<StockCategory> StockCategories { get; set; }
        public virtual DbSet<StockCostAndPrice> StockCostAndPrices { get; set; }
        public virtual DbSet<StockItem> StockItems { get; set; }
        public virtual DbSet<StockItemHistory> StockItemHistories { get; set; }
        public virtual DbSet<StockItemsColor> StockItemsColors { get; set; }
        public virtual DbSet<StockSubCategory> StockSubCategories { get; set; }
        public virtual DbSet<SubAccounType> SubAccounTypes { get; set; }
        public virtual DbSet<SuccessChance> SuccessChances { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<SystemEmail> SystemEmails { get; set; }
        public virtual DbSet<SystemEmailEmailsIDAndSectionsId> SystemEmailEmailsIDAndSectionsIds { get; set; }
        public virtual DbSet<SystemLog> SystemLogs { get; set; }
        public virtual DbSet<SystemUser> SystemUsers { get; set; }
        public virtual DbSet<SystemUserCheckin> SystemUserCheckins { get; set; }
        public virtual DbSet<SystemuserPreference> SystemuserPreferences { get; set; }
        public virtual DbSet<TargetType> TargetTypes { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<tbl_cmsSkins> tbl_cmsSkins { get; set; }
        public virtual DbSet<tbl_enquiries> tbl_enquiries { get; set; }
        public virtual DbSet<Template> Templates { get; set; }
        public virtual DbSet<TemplateBackgroundImage> TemplateBackgroundImages { get; set; }
        public virtual DbSet<TemplateColorStyle> TemplateColorStyles { get; set; }
        public virtual DbSet<TemplateFont> TemplateFonts { get; set; }
        public virtual DbSet<TemplateObject> TemplateObjects { get; set; }
        public virtual DbSet<TemplatePage> TemplatePages { get; set; }
        public virtual DbSet<TemplateVariable> TemplateVariables { get; set; }
        public virtual DbSet<TerritoryItem> TerritoryItems { get; set; }
        public virtual DbSet<UserAchievedTarget> UserAchievedTargets { get; set; }
        public virtual DbSet<UserEmailEvent> UserEmailEvents { get; set; }
        public virtual DbSet<UserPipeline> UserPipelines { get; set; }
        public virtual DbSet<UserReport> UserReports { get; set; }
        public virtual DbSet<UserSalesCommission> UserSalesCommissions { get; set; }
        public virtual DbSet<UserSalesTarget> UserSalesTargets { get; set; }
        public virtual DbSet<UserStickyNote> UserStickyNotes { get; set; }
        public virtual DbSet<UserTarget> UserTargets { get; set; }
        public virtual DbSet<VariableSection> VariableSections { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }
        public virtual DbSet<VoucherDetail> VoucherDetails { get; set; }
        public virtual DbSet<WeightUnit> WeightUnits { get; set; }
        public virtual DbSet<Widget> Widgets { get; set; }
        public virtual DbSet<WorkflowPreference> WorkflowPreferences { get; set; }
        public virtual DbSet<AuditTrail> AuditTrails { get; set; }
        public virtual DbSet<AuditTrailDetail> AuditTrailDetails { get; set; }
        public virtual DbSet<CompanyVariableIcon> CompanyVariableIcons { get; set; }
        public virtual DbSet<CustomerUsersRolespage> CustomerUsersRolespages { get; set; }
        public virtual DbSet<SectionCostCentresFeedback> SectionCostCentresFeedbacks { get; set; }
        public virtual DbSet<StockItemsIssueLog> StockItemsIssueLogs { get; set; }
      
    }
}
