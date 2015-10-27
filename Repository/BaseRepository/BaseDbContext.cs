using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using MPC.Models.DomainModels;

namespace MPC.Repository.BaseRepository
{
    /// <summary>
    /// Base Db Context. Implements Identity Db Context over Application User
    /// </summary>
    [Serializable]
    public sealed class BaseDbContext : DbContext
    {
        #region Private
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once NotAccessedField.Local
        private IUnityContainer container;
        #endregion

        #region Protected


        #endregion

        #region Constructor
        public BaseDbContext()
        {
        }
        /// <summary>
        /// Eager load property
        /// </summary>
        public void LoadProperty(object entity, string propertyName, bool isCollection = false)
        {
            if (!isCollection)
            {
                Entry(entity).Reference(propertyName).Load();
            }
            else
            {
                Entry(entity).Collection(propertyName).Load();
            }
        }
        /// <summary>
        /// Eager load property
        /// </summary>
        public void LoadProperty<T>(object entity, Expression<Func<T>> propertyExpression, bool isCollection = false)
        {
            string propertyName = PropertyReference.GetPropertyName(propertyExpression);
            LoadProperty(entity, propertyName, isCollection);
        }

        #endregion

        #region Public

        public BaseDbContext(IUnityContainer container, string connectionString)
            : base(connectionString)
        {
            this.container = container;
        }
        /// <summary>
        /// Organisation Db Set
        /// </summary>
        public DbSet<Organisation> Organisations { get; set; }

        /// <summary>
        /// Stock Items Db Set
        /// </summary>
        public DbSet<StockItem> StockItems { get; set; }

        /// <summary>
        /// Section Db Set
        /// </summary>
        public DbSet<Section> Sections { get; set; }

        /// <summary>
        /// Section Flag
        /// </summary>
        public DbSet<SectionFlag> SectionFlags { get; set; }

        /// <summary>
        /// Weight Units
        /// </summary>
        public DbSet<WeightUnit> WeightUnits { get; set; }

        /// <summary>
        /// Stock Cost And Price Db Set
        /// </summary>
        public DbSet<StockCostAndPrice> StockCostAndPrices { get; set; }
        /// <summary>
        /// MarkUp Db Set
        /// </summary>
        public DbSet<Markup> Markups { get; set; }
        /// <summary>
        /// Tax Rate Db Set
        /// </summary>
        public DbSet<TaxRate> TaxRates { get; set; }
        /// <summary>
        /// Chart Of Account Db Set
        /// </summary>
        public DbSet<ChartOfAccount> ChartOfAccounts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyType> CompanyTypes { get; set; }

        public DbSet<CompanyDomain> CompanyDomains { get; set; }

        /// <summary>
        /// Page Sizes Db Set
        /// </summary>
        public DbSet<PaperSize> PaperSizes { get; set; }
        public DbSet<StockCategory> StockCategories { get; set; }
        public DbSet<StockSubCategory> StockSubCategories { get; set; }

        public DbSet<CmsPage> CmsPages { get; set; }

        public DbSet<CmsSkinPageWidget> PageWidgets { get; set; }

        public DbSet<CmsSkinPageWidgetParam> PageWidgetParams { get; set; }
        public DbSet<Widget> Widgets { get; set; }

        public DbSet<CompanyBanner> CompanyBanners { get; set; }

        public DbSet<CompanyBannerSet> CompanyBannerSets { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<CategoryTerritory> CategoryTerritories { get; set; }

        public DbSet<PageCategory> PageCategories { get; set; }

        public DbSet<CompanyContact> CompanyContacts { get; set; }
        public DbSet<CompanyContactRole> CompanyContactRoles { get; set; }
        public DbSet<CompanyCostCentre> CompanyCostCentres { get; set; }
        public DbSet<CompanyTerritory> CompanyTerritories { get; set; }
        public DbSet<CostCentre> CostCentres { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemAddonCostCentre> ItemAddonCostCentres { get; set; }

        public DbSet<ItemAttachment> ItemAttachments { get; set; }
        public DbSet<ItemImage> ItemImages { get; set; }
        public DbSet<ItemPriceMatrix> ItemPriceMatrices { get; set; }
        public DbSet<ItemProductDetail> ItemProductDetails { get; set; }
        public DbSet<ItemRelatedItem> ItemRelatedItems { get; set; }
        public DbSet<ItemSection> ItemSections { get; set; }
        public DbSet<ItemStateTax> ItemStateTaxes { get; set; }
        public DbSet<ItemStockControl> ItemStockControls { get; set; }
        public DbSet<ItemStockOption> ItemStockOptions { get; set; }
        public DbSet<ItemVdpPrice> ItemVdpPrices { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<SectionCostcentre> SectionCostcentres { get; set; }
        public DbSet<SectionCostCentreDetail> SectionCostCentreDetails { get; set; }
        public DbSet<SectionCostCentreResource> SectionCostCentreResources { get; set; }
        public DbSet<SystemUser> SystemUsers { get; set; }
        public DbSet<EmailEvent> EmailEvents { get; set; }
        public DbSet<Estimate> Estimates { get; set; }
        public DbSet<PaymentGateway> PaymentGateways { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<PrePayment> PrePayments { get; set; }
        public DbSet<RaveReview> RaveReviews { get; set; }

        public DbSet<PaperBasisArea> PaperBasisAreas { get; set; }
        public DbSet<LengthUnit> LengthUnits { get; set; }
        public DbSet<RegistrationQuestion> RegistrationQuestions { get; set; }
        public DbSet<Inquiry> Inquiries { get; set; }
        public DbSet<InquiryAttachment> InquiryAttachments { get; set; }
        public DbSet<InquiryItem> InquiryItems { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<CampaignImage> CampaignImages { get; set; }
        public DbSet<CampaignEmailVariable> CampaignEmailVariables { get; set; }
        public DbSet<DeliveryNote> DeliveryNotes { get; set; }
        public DbSet<DeliveryNoteDetail> DeliveryNoteDetails { get; set; }
        public DbSet<DiscountVoucher> DiscountVouchers { get; set; }
        public DbSet<CompanyCMYKColor> CompanyCmykColors { get; set; }
        public DbSet<GetItemsListView> GetItemsListViews { get; set; }

        public DbSet<vw_RealEstateProperties> vw_RealEstateProperties { get; set; }

        public DbSet<vw_CompanyVariableIcons> vw_CompanyVariableIcons { get; set; }

        public DbSet<GetCategoryProduct> GetCategoryProducts { get; set; }

        public DbSet<Address> Addesses { get; set; }
        /// <summary>
        /// Prefix DbSet
        /// </summary>
        public DbSet<Prefix> Prefixes { get; set; }

        /// <summary>
        /// Item Video DbSet
        /// </summary>
        public DbSet<ItemVideo> ItemVideos { get; set; }

        /// <summary>
        /// Color Pallete DbSet
        /// </summary>
        public DbSet<ColorPallete> ColorPalletes { get; set; }

        /// <summary>
        /// Template DbSet
        /// </summary>
        public DbSet<Template> Templates { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<UserActionsLog> UserActionLogs { get; set; }
        /// <summary>
        /// Get Minimum Product Value
        /// </summary>
        public double GetMinimumProductValue(long itemId)
        {
            if (itemId <= 0)
            {
                throw new ArgumentException(LanguageResources.InvalidItem, "itemId");
            }

// ReSharper disable SuggestUseVarKeywordEvident
            ObjectParameter itemIdParameter = new ObjectParameter("ItemId", itemId);
// ReSharper restore SuggestUseVarKeywordEvident
            ObjectResult<double?> result = ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<double?>("sp_GetMinimumProductValue", itemIdParameter);

            return result.FirstOrDefault() ?? 0;
        }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<GlobalLanguage> GlobalLanguages { get; set; }

        /// <summary>
        /// Template Page DbSet
        /// </summary>
        public DbSet<TemplatePage> TemplatePages { get; set; }

        /// <summary>
        /// Campaign Email Queue DbSet
        /// </summary>
        public DbSet<CampaignEmailQueue> CampaignEmailQueues { get; set; }

        /// <summary>
        /// Cms Page Tag DbSet
        /// </summary>
        public DbSet<CmsPageTag> CmsPageTags { get; set; }

        /// <summary>
        /// Cms Tag DbSet
        /// </summary>
        public DbSet<CmsTag> CmsTags { get; set; }

        /// <summary>
        /// Cost Centre Type DbSet
        /// </summary>
        public DbSet<CostCentreType> CostCentreTypes { get; set; }

        /// <summary>
        /// Field Variable DbSet
        /// </summary>
        public DbSet<FieldVariable> FieldVariables { get; set; }

        /// <summary>
        /// Template Variable DbSet
        /// </summary>
        public DbSet<TemplateVariable> TemplateVariables { get; set; }

        /// <summary>
        /// Variable Section DbSet
        /// </summary>
        public DbSet<VariableSection> VariableSections { get; set; }

        /// <summary>
        /// Template Background Image DbSet
        /// </summary>
        public DbSet<TemplateBackgroundImage> TemplateBackgroundImages { get; set; }

        /// <summary>
        /// Template Object DbSet
        /// </summary>
        public DbSet<TemplateObject> TemplateObjects { get; set; }

        /// <summary>
        /// Product Market Brief Question DbSet
        /// </summary>
        public DbSet<ProductMarketBriefQuestion> ProductMarketBriefQuestions { get; set; }

        /// <summary>
        /// Product Market Brief Answer DbSet
        /// </summary>
        public DbSet<ProductMarketBriefAnswer> ProductMarketBriefAnswers { get; set; }

        

        /// <summary>
        /// Role DbSet
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Product Category Item DbSet
        /// </summary>
        public DbSet<ProductCategoryItem> ProductCategoryItems { get; set; }

        /// <summary>
        /// Template Color Style DbSet
        /// </summary>
        public DbSet<TemplateColorStyle> TemplateColorStyles { get; set; }

        /// <summary>
        /// Template Font DbSet
        /// </summary>
        public DbSet<TemplateFont> TemplateFonts { get; set; }

        /// <summary>
        /// Product Categories View DbSet
        /// </summary>
        public DbSet<ProductCategoriesView> ProductCategoriesViews { get; set; }

        /// <summary>
        /// Image Permission DbSet
        /// </summary>
        public DbSet<ImagePermission> ImagePermissions { get; set; }

        /// <summary>
        /// Listing DbSet
        /// </summary>
        public DbSet<Listing> Listings { get; set; }

        /// <summary>
        /// Listing Agent DbSet
        /// </summary>
        public DbSet<ListingAgent> ListingAgents { get; set; }

        /// <summary>
        /// Listing Conjunction Agent DbSet
        /// </summary>
        public DbSet<ListingConjunctionAgent> ListingConjunctionAgents { get; set; }

        /// <summary>
        /// Listing Floor Plan DbSet
        /// </summary>
        public DbSet<ListingFloorPlan> ListingFloorPlans { get; set; }

        /// <summary>
        /// Listing Image DbSet
        /// </summary>
        public DbSet<ListingImage> ListingImages { get; set; }

        /// <summary>
        /// Listing Link DbSet
        /// </summary>
        public DbSet<ListingLink> ListingLinks { get; set; }

        /// <summary>
        /// Listing OFI DbSet
        /// </summary>
        // ReSharper disable InconsistentNaming
        public DbSet<ListingOFI> ListingOFIs { get; set; }
        // ReSharper restore InconsistentNaming

        /// <summary>
        /// Listing Vendor DbSet
        /// </summary>
        public DbSet<ListingVendor> ListingVendors { get; set; }

        /// <summary>
        /// Favorite Design DbSet
        /// </summary>
        public DbSet<FavoriteDesign> FavoriteDesigns { get; set; }

        /// <summary>
        /// Company Variable Icon DbSet
        /// </summary>
        public DbSet<CompanyVariableIcon> CompanyVariableIcons { get; set; }

        /// <summary>
        /// Custom Copy DbSet
        /// </summary>
        public DbSet<CustomCopy> CustomCopies { get; set; }

        /// <summary>
        /// Phrase Field DbSet
        /// </summary>
        public DbSet<PhraseField> PhraseFields { get; set; }

        /// <summary>
        /// Phrase DbSet
        /// </summary>
        public DbSet<Phrase> Phrases { get; set; }

        /// <summary>
        /// Cost Centre Answer DbSet
        /// </summary>
        public DbSet<CostCentreAnswer> CostCentreAnswers { get; set; }

        /// <summary>
        /// Cost center Instuction DbSet
        /// </summary>
        public DbSet<CostcentreInstruction> CostcentreInstructions { get; set; }

        /// <summary>
        /// Cost Centre Matrix DbSet
        /// </summary>
        public DbSet<CostCentreMatrix> CostCentreMatrices { get; set; }

        /// <summary>
        /// Cost Centre Matrix Detail DbSet
        /// </summary>
        public DbSet<CostCentreMatrixDetail> CostCentreMatrixDetails { get; set; }

        /// <summary>
        /// Cost Centre Question DbSet
        /// </summary>
        public DbSet<CostCentreQuestion> CostCentreQuestions { get; set; }

        /// <summary>
        /// Cost Centre Answer DbSet
        /// </summary>
        public DbSet<CostcentreResource> CostcentreResources { get; set; }

        /// <summary>
        /// Cost Centre System Type DbSet
        /// </summary>
        public DbSet<CostcentreSystemType> CostcentreSystemTypes { get; set; }

        /// <summary>
        /// Cost Centre Template DbSet
        /// </summary>
        public DbSet<CostCentreTemplate> CostCentreTemplates { get; set; }

        /// <summary>
        /// Cost Centre Variable DbSet
        /// </summary>
        public DbSet<CostCentreVariable> CostCentreVariables { get; set; }

        /// <summary>
        /// Cost Centre Variable Type DbSet
        /// </summary>
        public DbSet<CostCentreVariableType> CostCentreVariableTypes { get; set; }

        /// <summary>
        /// Cost Centre Work Instruction Choice DbSet
        /// </summary>
        public DbSet<CostcentreWorkInstructionsChoice> CostcentreWorkInstructionsChoices { get; set; }

        /// <summary>
        /// Cms Offer DbSet
        /// </summary>
        public DbSet<CmsOffer> CmsOffers { get; set; }

        /// <summary>
        /// Item Stock Update History DbSet
        /// </summary>
        public DbSet<ItemStockUpdateHistory> ItemStockUpdateHistories { get; set; }

        /// <summary>
        /// Media Library DbSet
        /// </summary>
        public DbSet<MediaLibrary> MediaLibraries { get; set; }

        /// <summary>
        /// Lookup Method DbSet
        /// </summary>
        public DbSet<LookupMethod> LookupMethods { get; set; }

        /// <summary>
        /// Machine DbSet
        /// </summary>
        public DbSet<Machine> Machines { get; set; }

        /// <summary>
        /// Machine Ink Coverage DbSet
        /// </summary>
        public DbSet<MachineInkCoverage> MachineInkCoverages { get; set; }

        /// <summary>
        /// Machine Resource DbSet
        /// </summary>
        public DbSet<MachineResource> MachineResources { get; set; }

        /// <summary>
        /// Machine Category DbSet
        /// </summary>
        public DbSet<MachineCategory> MachineCategories { get; set; }

        /// <summary>
        /// Group DbSet
        /// </summary>
        public DbSet<Group> Groups { get; set; }

        /// <summary>
        /// CostCenterChoice DbSet
        /// </summary>
        public DbSet<CostCenterChoice> CostCenterChoices { get; set; }
        
        /// <summary>
        /// InkCoverageGroup DbSet
        /// </summary>
        public DbSet<InkCoverageGroup> InkCoverageGroups { get; set; }
        
        /// <summary>
        /// MachineSpoilage DbSet
        /// </summary>
        public DbSet<MachineSpoilage> MachineSpoilages { get; set; }
        
        /// <summary>
        /// Report DbSet
        /// </summary>
        public DbSet<Report> Reports { get; set; }
        
        /// <summary>
        /// ReportNote DbSet
        /// </summary>
        public DbSet<ReportNote> ReportNotes { get; set; }

        /// <summary>
        /// PipeLineProduct DbSet
        /// </summary>
        public DbSet<PipeLineProduct> PipeLineProducts { get; set; }

        /// <summary>
        /// PipeLineSource DbSet
        /// </summary>
        public DbSet<PipeLineSource> PipeLineSources { get; set; }

        /// <summary>
        /// Activity DbSet
        /// </summary>
        public DbSet<Activity> Activities { get; set; }


        /// <summary>
        /// Activity Type DbSet
        /// </summary>
        public DbSet<ActivityType> ActivityTypes { get; set; }

        /// <summary>
        /// Variable Option DbSet
        /// </summary>
        public DbSet<VariableOption> VariableOptions { get; set; }

        /// <summary>
        /// Scope Variable DbSet
        /// </summary>
        public DbSet<ScopeVariable> ScopeVariables { get; set; }

        /// <summary>
        /// Smart Form DbSet
        /// </summary>
        public DbSet<SmartForm> SmartForms { get; set; }

        /// <summary>
        /// Smart Form Detail DbSet
        /// </summary>
        public DbSet<SmartFormDetail> SmartFormDetails { get; set; }

        /// <summary>
        /// Delivery Carrier DbSet
        /// </summary>
        public DbSet<DeliveryCarrier> DeliveryCarriers { get; set; }

        /// <summary>
        /// Paypal PaymentRequest DbSet
        /// </summary>
        public DbSet<PaypalPaymentRequest> PaypalPaymentRequests { get; set; }

        /// <summary>
        /// PayPal Response DbSet
        /// </summary>
        public DbSet<PayPalResponse> PayPalResponses { get; set; }

        /// <summary>
        /// NAB Transaction DbSet
        /// </summary>
// ReSharper disable InconsistentNaming
        public DbSet<NABTransaction> NABTransactions { get; set; }
// ReSharper restore InconsistentNaming

        /// <summary>
        /// vw_SaveDesign DbSet
        /// </summary>
        public DbSet<SaveDesignView> SaveDesignViews { get; set; }

        /// <summary>
        /// NewsLetter Subscriber DbSet
        /// </summary>
        public DbSet<NewsLetterSubscriber> NewsLetterSubscribers { get; set; }

        /// <summary>
        /// MachineClickChargeLookup DbSet
        /// </summary>
        public DbSet<MachineClickChargeLookup> MachineClickChargeLookups { get; set; }

        /// <summary>
        /// MachineClickChargeZone DbSet
        /// </summary>
        public DbSet<MachineClickChargeZone> MachineClickChargeZones { get; set; }

        /// <summary>
        /// MachineGuillotineCalc DbSet
        /// </summary>
        public DbSet<MachineGuillotineCalc> MachineGuillotineCalcs { get; set; }

        /// <summary>
        /// MachineGuilotinePtv DbSet
        /// </summary>
        public DbSet<MachineGuilotinePtv> MachineGuilotinePtvs { get; set; }

        /// <summary>
        /// MachineMeterPerHourLookup DbSet
        /// </summary>
        public DbSet<MachineMeterPerHourLookup> MachineMeterPerHourLookups { get; set; }

        /// <summary>
        /// MachinePerHourLookup DbSet
        /// </summary>
        public DbSet<MachinePerHourLookup> MachinePerHourLookups { get; set; }

        /// <summary>
        /// MachineSpeedWeightLookup DbSet
        /// </summary>
        public DbSet<MachineSpeedWeightLookup> MachineSpeedWeightLookups { get; set; }

        /// <summary>
        /// Goods Received Note DbSet
        /// </summary>
        public DbSet<GoodsReceivedNote> GoodsReceivedNotes { get; set; }
        
        /// <summary>
        /// Goods Received Note Detail DbSet
        /// </summary>
        public DbSet<GoodsReceivedNoteDetail> GoodsReceivedNoteDetails { get; set; }
        
        /// <summary>
        /// Purchase DbSet
        /// </summary>
        public DbSet<Purchase> Purchases { get; set; }
        
        /// <summary>
        /// Purchase Detail DbSet
        /// </summary>
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }

        /// <summary>
        /// Job Preference DbSet
        /// </summary>
        public DbSet<JobPreference> JobPreferences { get; set; }

        /// <summary>
        /// Ink Plate Side DbSet
        /// </summary>
        public DbSet<InkPlateSide> InkPlateSides { get; set; }

        /// <summary>
        /// Section Ink Coverage DbSet
        /// </summary>
        public DbSet<SectionInkCoverage> SectionInkCoverages { get; set; }

        /// <summary>
        /// Job Card Report View DbSet
        /// </summary>
        public DbSet<JobCardReportView> JobCardReportViews { get; set; }

        /// <summary>
        /// Order Report View DbSet
        /// </summary>
        public DbSet<OrderReportView> OrderReportViews { get; set; }

        /// <summary>
        /// Report Category DbSet
        /// </summary>
        public DbSet<ReportCategory> ReportCategories { get; set; }

        /// <summary>
        /// Shipping Information DbSet
        /// </summary>
        public DbSet<ShippingInformation> ShippingInformations { get; set; }

        /// <summary>
        /// Report Param DbSet
        /// </summary>
        public DbSet<Reportparam> Reportparams { get; set; }

        /// <summary>
        /// Staging Import Company Contact Address DbSet
        /// </summary>
        public DbSet<StagingImportCompanyContactAddress> StagingImportCompanyContactAddresses { get; set; }

        /// <summary>
        /// Machine Lookup Method DbSet
        /// </summary>
        public DbSet<MachineLookupMethod> MachineLookupMethods { get; set; }

        /// <summary>
        /// Variable Extension DbSet
        /// </summary>
        public DbSet<VariableExtension> VariableExtensions { get; set; }


        /// <summary>
        /// Template Variable Extension DbSet
        /// </summary>
        public DbSet<TemplateVariableExtension> TemplateVariableExtensions { get; set; }
        /// <summary>
        /// Company Vouchers Redeem DbSet
        /// </summary>
        public DbSet<CompanyVoucherRedeem> CompanyVoucherRedeems { get; set; }
        
        /// <summary>
        /// Item Vouchers Redeem DbSet
        /// </summary>
        public DbSet<ItemsVoucher> ItemsVouchers { get; set; }
         
        /// <summary>
        /// Item Vouchers Redeem DbSet
        /// </summary>
        public DbSet<ProductCategoryVoucher> ProductCategoryVouchers { get; set; }

        public DbSet<MarketingBriefHistory> MarketingBriefHistory { get; set; }
        /// <summary>
        /// Clone Template Stored Procedure
        /// </summary>
// ReSharper disable InconsistentNaming
        public long sp_cloneTemplate(long templateId, long submittedBy, string submittedByName)
// ReSharper restore InconsistentNaming
        {
// ReSharper disable SuggestUseVarKeywordEvident
            ObjectParameter templateIdParameter = new ObjectParameter("TemplateID", templateId);

            ObjectParameter submittedByParameter = new ObjectParameter("submittedBy", submittedBy);
            ObjectParameter submittedByNameParameter = new ObjectParameter("submittedByName", submittedByName);
            // ReSharper restore SuggestUseVarKeywordEvident

            ObjectResult<long?> result = ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<long?>("BaseDbContext.sp_cloneTemplate", templateIdParameter, submittedByParameter,
                submittedByNameParameter);
            long? newTemplateId = result.FirstOrDefault();

            return newTemplateId.HasValue ? newTemplateId.Value : 0;
        }
        
        /// <summary>
        /// GetUsedFonts Updated 
        /// </summary>
        // ReSharper disable InconsistentNaming
        public IEnumerable<sp_GetUsedFontsUpdated_Result> sp_GetUsedFontsUpdated(long? templateID, long? customerID)
        // ReSharper restore InconsistentNaming
        {
            var templateIdParameter = templateID.HasValue ?
                new ObjectParameter("TemplateID", templateID) :
                new ObjectParameter("TemplateID", typeof(long));

            var customerIdParameter = customerID.HasValue ?
                new ObjectParameter("CustomerID", customerID) :
                new ObjectParameter("CustomerID", typeof(long));

            ObjectResult<sp_GetUsedFontsUpdated_Result> templateFontsUpdatedResults =
                ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetUsedFontsUpdated_Result>("BaseDbContext.sp_GetUsedFontsUpdated", templateIdParameter,
                customerIdParameter);

            return templateFontsUpdatedResults.ToList();
        }

        /// <summary>
        /// Get Real Estate Products 
        /// </summary>
// ReSharper disable InconsistentNaming
        public IEnumerable<usp_GetRealEstateProducts_Result> usp_GetRealEstateProducts(int? contactCompanyId)
// ReSharper restore InconsistentNaming
        {
            var contactCompanyIdParameter = contactCompanyId.HasValue ?
                new ObjectParameter("ContactCompanyID", contactCompanyId) :
                new ObjectParameter("ContactCompanyID", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.
                ExecuteFunction<usp_GetRealEstateProducts_Result>("BaseDbContext.usp_GetRealEstateProducts", contactCompanyIdParameter).ToList();
        }

        /// <summary>
        /// Get Template Images Result
        /// </summary>
// ReSharper disable InconsistentNaming
        public IEnumerable<sp_GetTemplateImages_Result> sp_GetTemplateImages(int? isCalledFrom, int? imageSetType, long? templateId, long? contactCompanyId,
// ReSharper restore InconsistentNaming
            long? contactId, long? territory, int? pageNumber, int? pageSize, string sortColumn, string search, ObjectParameter imageCount)
        {
            var isCalledFromParameter = isCalledFrom.HasValue ?
                new ObjectParameter("isCalledFrom", isCalledFrom) :
                new ObjectParameter("isCalledFrom", typeof(int));

            var imageSetTypeParameter = imageSetType.HasValue ?
                new ObjectParameter("imageSetType", imageSetType) :
                new ObjectParameter("imageSetType", typeof(int));

            var templateIdParameter = templateId.HasValue ?
                new ObjectParameter("templateID", templateId) :
                new ObjectParameter("templateID", typeof(long));

            var contactCompanyIdParameter = contactCompanyId.HasValue ?
                new ObjectParameter("contactCompanyID", contactCompanyId) :
                new ObjectParameter("contactCompanyID", typeof(long));

            var contactIdParameter = contactId.HasValue ?
                new ObjectParameter("contactID", contactId) :
                new ObjectParameter("contactID", typeof(long));

            var territoryParameter = territory.HasValue ?
                new ObjectParameter("territory", territory) :
                new ObjectParameter("territory", typeof(long));

            var pageNumberParameter = pageNumber.HasValue ?
                new ObjectParameter("pageNumber", pageNumber) :
                new ObjectParameter("pageNumber", typeof(int));

            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("pageSize", pageSize) :
                new ObjectParameter("pageSize", typeof(int));

            var sortColumnParameter = sortColumn != null ?
                new ObjectParameter("sortColumn", sortColumn) :
                new ObjectParameter("sortColumn", typeof(string));

            var searchParameter = search != null ?
                new ObjectParameter("search", search) :
                new ObjectParameter("search", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.
                ExecuteFunction<sp_GetTemplateImages_Result>("sp_GetTemplateImages", isCalledFromParameter, imageSetTypeParameter,
                templateIdParameter, contactCompanyIdParameter, contactIdParameter, territoryParameter, pageNumberParameter,
                pageSizeParameter, sortColumnParameter, searchParameter, imageCount).ToList();
        }

        /// <summary>
        /// Stored Procedure sp_CostCentreExecution_get_StockPriceByCalculationType
        /// </summary>
// ReSharper disable InconsistentNaming
        public double sp_CostCentreExecution_get_StockPriceByCalculationType(int? stockId, int? calculationType, ObjectParameter returnPrice, ObjectParameter perQtyQty)
// ReSharper restore InconsistentNaming
        {
            var stockIdParameter = stockId.HasValue ?
                new ObjectParameter("StockID", stockId) :
                new ObjectParameter("StockID", typeof(int));

            var calculationTypeParameter = calculationType.HasValue ?
                new ObjectParameter("CalculationType", calculationType) :
                new ObjectParameter("CalculationType", typeof(int));

            ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_CostCentreExecution_get_StockPriceByCalculationType", stockIdParameter,
                calculationTypeParameter, returnPrice, perQtyQty);

            return perQtyQty.Value != null ? (double)perQtyQty.Value : 0;
        }

// ReSharper disable InconsistentNaming
        public int usp_DeleteProduct(long? itemid)
// ReSharper restore InconsistentNaming
        {
            var itemidParameter = itemid.HasValue ?
                new ObjectParameter("itemid", itemid) :
                new ObjectParameter("itemid", typeof(long));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_DeleteProduct", itemidParameter);
        }


// ReSharper disable InconsistentNaming
        public int usp_DeleteContactCompanyByID(int? companyId)
// ReSharper restore InconsistentNaming
        {
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("CompanyID", companyId) :
                new ObjectParameter("CompanyID", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_DeleteContactCompanyByID", companyIdParameter);
        }

        /// <summary>
        /// Stored procedure to delete an organisation
        /// </summary>
// ReSharper disable InconsistentNaming
        public int usp_DeleteOrganisation(int? organisationId)
// ReSharper restore InconsistentNaming
        {
            var organisationIdParameter = organisationId.HasValue ?
                new ObjectParameter("OrganisationID", organisationId) :
                new ObjectParameter("OrganisationID", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_DeleteOrganisation", organisationIdParameter);
        }

// ReSharper disable InconsistentNaming
        public int usp_DeleteCarts(long? companyId)
// ReSharper restore InconsistentNaming
        {
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("CompanyID", companyId) :
                new ObjectParameter("CompanyID", typeof(long));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_DeleteCarts", companyIdParameter);
        }

// ReSharper disable InconsistentNaming
        public int usp_DeleteOrderByID(long? orderId)
// ReSharper restore InconsistentNaming
        {
            var orderIdParameter = orderId.HasValue ?
                new ObjectParameter("OrderID", orderId) :
                new ObjectParameter("OrderID", typeof(long));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_DeleteOrderByID", orderIdParameter);
        }

// ReSharper disable InconsistentNaming
        public ObjectResult<usp_JobCardReport_Result> usp_JobCardReport(long? organisationId, long? orderId, long? itemId)
// ReSharper restore InconsistentNaming
        {
            var organisationIdParameter = organisationId.HasValue ?
                new ObjectParameter("organisationId", organisationId) :
                new ObjectParameter("organisationId", typeof(long));

            var orderIdParameter = orderId.HasValue ?
                new ObjectParameter("OrderID", orderId) :
                new ObjectParameter("OrderID", typeof(long));

            var itemIdParameter = itemId.HasValue ?
                new ObjectParameter("ItemID", itemId) :
                new ObjectParameter("ItemID", typeof(long));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_JobCardReport_Result>("usp_JobCardReport", organisationIdParameter, orderIdParameter, itemIdParameter);
        }

// ReSharper disable InconsistentNaming
        public ObjectResult<usp_OrderReport_Result> usp_OrderReport(long? organisationId, long? orderId)
// ReSharper restore InconsistentNaming
        {
            var organisationIdParameter = organisationId.HasValue ?
                new ObjectParameter("organisationId", organisationId) :
                new ObjectParameter("organisationId", typeof(long));

            var orderIdParameter = orderId.HasValue ?
                new ObjectParameter("OrderID", orderId) :
                new ObjectParameter("OrderID", typeof(long));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_OrderReport_Result>("usp_OrderReport", organisationIdParameter, orderIdParameter);
        }

// ReSharper disable InconsistentNaming
        public ObjectResult<usp_DeliveryReport_Result> usp_DeliveryReport(long? organisationId, long? deliveryId)
// ReSharper restore InconsistentNaming
        {
            var organisationIdParameter = organisationId.HasValue ?
                new ObjectParameter("OrganisationID", organisationId) :
                new ObjectParameter("OrganisationID", typeof(long));

            var deliveryIdParameter = deliveryId.HasValue ?
                new ObjectParameter("DeliveryID", deliveryId) :
                new ObjectParameter("DeliveryID", typeof(long));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_DeliveryReport_Result>("usp_DeliveryReport", organisationIdParameter, 
                deliveryIdParameter);
        }

// ReSharper disable InconsistentNaming
        public ObjectResult<usp_EstimateReport_Result> usp_EstimateReport(long? organisationId, long? estimateId)
// ReSharper restore InconsistentNaming
        {
            var organisationIdParameter = organisationId.HasValue ?
                new ObjectParameter("OrganisationId", organisationId) :
                new ObjectParameter("OrganisationId", typeof(long));

            var estimateIdParameter = estimateId.HasValue ?
                new ObjectParameter("EstimateID", estimateId) :
                new ObjectParameter("EstimateID", typeof(long));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_EstimateReport_Result>("usp_EstimateReport", organisationIdParameter, 
                estimateIdParameter);
        }

// ReSharper disable InconsistentNaming
        public ObjectResult<usp_InvoiceReport_Result> usp_InvoiceReport(long? organisationid, long? invoiceId)
// ReSharper restore InconsistentNaming
        {
            var organisationidParameter = organisationid.HasValue ?
                new ObjectParameter("Organisationid", organisationid) :
                new ObjectParameter("Organisationid", typeof(long));

            var invoiceIdParameter = invoiceId.HasValue ?
                new ObjectParameter("InvoiceId", invoiceId) :
                new ObjectParameter("InvoiceId", typeof(long));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_InvoiceReport_Result>("usp_InvoiceReport", organisationidParameter, 
                invoiceIdParameter);
        }

// ReSharper disable InconsistentNaming
        public ObjectResult<usp_PurchaseOrderReport_Result> usp_PurchaseOrderReport(long? organisationId, long? purchaseId)
// ReSharper restore InconsistentNaming
        {
            var organisationIdParameter = organisationId.HasValue ?
                new ObjectParameter("OrganisationId", organisationId) :
                new ObjectParameter("OrganisationId", typeof(long));

            var purchaseIdParameter = purchaseId.HasValue ?
                new ObjectParameter("PurchaseID", purchaseId) :
                new ObjectParameter("PurchaseID", typeof(long));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_PurchaseOrderReport_Result>("usp_PurchaseOrderReport", organisationIdParameter, 
                purchaseIdParameter);
        }

// ReSharper disable InconsistentNaming
        public ObjectResult<usp_TotalEarnings_Result> usp_TotalEarnings(DateTime? fromdate, DateTime? todate, long? organisationid)
// ReSharper restore InconsistentNaming
        {
            var fromdateParameter = fromdate.HasValue ?
                new ObjectParameter("fromdate", fromdate) :
                new ObjectParameter("fromdate", typeof(DateTime));

            var todateParameter = todate.HasValue ?
                new ObjectParameter("todate", todate) :
                new ObjectParameter("todate", typeof(DateTime));

            var organisationidParameter = organisationid.HasValue ?
                new ObjectParameter("Organisationid", organisationid) :
                new ObjectParameter("Organisationid", typeof(long));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_TotalEarnings_Result>("usp_TotalEarnings", fromdateParameter, 
                todateParameter, organisationidParameter);
        }

// ReSharper disable InconsistentNaming
        public int usp_DeletePurchaseOrders(int? orderId)
// ReSharper restore InconsistentNaming
        {
            var orderIdParameter = orderId.HasValue ?
                new ObjectParameter("OrderID", orderId) :
                new ObjectParameter("OrderID", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_DeletePurchaseOrders", orderIdParameter);
        }

// ReSharper disable InconsistentNaming
        public int usp_GeneratePurchaseOrders(int? orderId, Guid? createdBy)
// ReSharper restore InconsistentNaming
        {
            var orderIdParameter = orderId.HasValue ?
                new ObjectParameter("OrderID", orderId) :
                new ObjectParameter("OrderID", typeof(int));

            var createdByParameter = createdBy.HasValue ?
                new ObjectParameter("CreatedBy", createdBy) :
                new ObjectParameter("CreatedBy", typeof(Guid));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_GeneratePurchaseOrders", orderIdParameter, createdByParameter);
        }

// ReSharper disable InconsistentNaming
        public int usp_importTerritoryContactAddressByStore(long? organisationId, long? storeId)
// ReSharper restore InconsistentNaming
        {
            var organisationIdParameter = organisationId.HasValue ?
                new ObjectParameter("OrganisationId", organisationId) :
                new ObjectParameter("OrganisationId", typeof(long));

            var storeIdParameter = storeId.HasValue ?
                new ObjectParameter("StoreId", storeId) :
                new ObjectParameter("StoreId", typeof(long));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_importTerritoryContactAddressByStore", organisationIdParameter, storeIdParameter);
        }

// ReSharper disable InconsistentNaming
        /// <summary>
        /// Delete CRM Company By Id
        /// </summary>
        public int usp_DeleteCRMCompanyByID(int? companyId)
// ReSharper restore InconsistentNaming
        {
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("CompanyID", companyId) :
                new ObjectParameter("CompanyID", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_DeleteCRMCompanyByID", companyIdParameter);
        }

        /// <summary>
        /// Delete Staging Import table data
        /// </summary>
// ReSharper disable InconsistentNaming
        public int usp_DeleteStagingImportCompanyContactAddress()
// ReSharper restore InconsistentNaming
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_DeleteStagingImportCompanyContactAddress");
        }

        /// <summary>
        /// Delete Template
        /// </summary>
// ReSharper disable InconsistentNaming
        public int usp_DeleteTemplate(long? templateId)
// ReSharper restore InconsistentNaming
        {
            var templateIdParameter = templateId.HasValue ?
                new ObjectParameter("TemplateID", templateId) :
                new ObjectParameter("TemplateID", typeof(long));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_DeleteTemplate", templateIdParameter);
        }

        public ObjectResult<usp_ChartRegisteredUserByStores_Result> usp_ChartRegisteredUserByStores(long? organisationid)
        // ReSharper restore InconsistentNaming
        {
            var organisationidParameter = organisationid.HasValue ?
                new ObjectParameter("OrganisationId", organisationid) :
                new ObjectParameter("OrganisationId", typeof(long));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_ChartRegisteredUserByStores_Result>("usp_ChartRegisteredUserByStores", organisationidParameter);
        }

        public ObjectResult<usp_ChartTopPerformingStores_Result> usp_ChartTopPerformingStores(long? organisationid)
        // ReSharper restore InconsistentNaming
        {
            var organisationidParameter = organisationid.HasValue ?
                new ObjectParameter("OrganisationId", organisationid) :
                new ObjectParameter("OrganisationId", typeof(long));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_ChartTopPerformingStores_Result>("usp_ChartTopPerformingStores", organisationidParameter);
        }

        public ObjectResult<usp_ChartMonthlyOrdersCount_Result> usp_ChartMonthlyOrdersCount(long? organisationid)
        // ReSharper restore InconsistentNaming
        {
            var organisationidParameter = organisationid.HasValue ?
                new ObjectParameter("OrganisationId", organisationid) :
                new ObjectParameter("OrganisationId", typeof(long));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_ChartMonthlyOrdersCount_Result>("usp_ChartMonthlyOrdersCount", organisationidParameter);
        }

        public ObjectResult<usp_ChartEstimateToOrderConversion_Result> usp_ChartEstimateToOrderConversion(long? organisationid)
        // ReSharper restore InconsistentNaming
        {
            var organisationidParameter = organisationid.HasValue ?
                new ObjectParameter("OrganisationId", organisationid) :
                new ObjectParameter("OrganisationId", typeof(long));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_ChartEstimateToOrderConversion_Result>("usp_ChartEstimateToOrderConversion", organisationidParameter);
        }

        public ObjectResult<usp_ChartEstimateToOrderConversionCount_Result> usp_ChartEstimateToOrderConversionCount(long? organisationid)
        // ReSharper restore InconsistentNaming
        {
            var organisationidParameter = organisationid.HasValue ?
                new ObjectParameter("OrganisationId", organisationid) :
                new ObjectParameter("OrganisationId", typeof(long));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_ChartEstimateToOrderConversionCount_Result>("usp_ChartEstimateToOrderConversionCount", organisationidParameter);
        }

        public ObjectResult<usp_ChartTop10PerfomingCustomers_Result> usp_ChartTop10PerfomingCustomers(long? organisationid)
        // ReSharper restore InconsistentNaming
        {
            var organisationidParameter = organisationid.HasValue ?
                new ObjectParameter("OrganisationId", organisationid) :
                new ObjectParameter("OrganisationId", typeof(long));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_ChartTop10PerfomingCustomers_Result>("usp_ChartTop10PerfomingCustomers", organisationidParameter);
        }
        public ObjectResult<usp_ChartMonthlyEarningsbyStore_Result> usp_ChartMonthlyEarningsbyStore(long? organisationid)
        // ReSharper restore InconsistentNaming
        {
            var organisationidParameter = organisationid.HasValue ?
                new ObjectParameter("OrganisationId", organisationid) :
                new ObjectParameter("OrganisationId", typeof(long));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_ChartMonthlyEarningsbyStore_Result>("usp_ChartMonthlyEarningsbyStore", organisationidParameter);
        }
        public ObjectResult<usp_DashboardROICounter_Result> usp_DashboardROICounter(long? organisationid)
        // ReSharper restore InconsistentNaming
        {
            var organisationidParameter = organisationid.HasValue ?
                new ObjectParameter("OrganisationId", organisationid) :
                new ObjectParameter("OrganisationId", typeof(long));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_DashboardROICounter_Result>("usp_DashboardROICounter", organisationidParameter);
        }

        public int usp_importCRMCompanyContacts(long? organisationId)
        // ReSharper restore InconsistentNaming
        {
            var organisationIdParameter = organisationId.HasValue ?
                new ObjectParameter("OrganisationId", organisationId) :
                new ObjectParameter("OrganisationId", typeof(long));


            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_importCRMCompanyContacts", organisationIdParameter);
        }


        public int usp_CopyStoreProducts(long? organisationId,long? NewStoreId,long? OldStoreId)
        // ReSharper restore InconsistentNaming
        {
            var organisationIdParameter = organisationId.HasValue ?
                new ObjectParameter("OrganisationId", organisationId) :
                new ObjectParameter("OrganisationId", typeof(long));

            var NewstoreIdParameter = NewStoreId.HasValue ?
              new ObjectParameter("NewStoreId", NewStoreId) :
              new ObjectParameter("NewStoreId", typeof(long));

            var OldstoreIdParameter = OldStoreId.HasValue ?
            new ObjectParameter("OldStoreId", OldStoreId) :
            new ObjectParameter("OldStoreId", typeof(long));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_CopyStoreProducts", OldstoreIdParameter, NewstoreIdParameter,organisationIdParameter);
        }

        public int usp_DeleteCostCentre(long? costcentreId)
        // ReSharper restore InconsistentNaming
        {
            var costcentreIdParameter = costcentreId.HasValue ?
                new ObjectParameter("costcentreId", costcentreId) :
                new ObjectParameter("costcentreId", typeof(long));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_DeleteCostCentre", costcentreIdParameter);
        }

        /// <summary>
        /// Delete Staging Import table data
        /// </summary>
        // ReSharper disable InconsistentNaming
        public int usp_GetLiveStores()
        // ReSharper restore InconsistentNaming
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_GetLiveStores");
        }

        #endregion
    }
}
