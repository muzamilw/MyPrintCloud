using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using MPC.Models.DomainModels;

namespace MPC.Repository.BaseRepository
{
    /// <summary>
    /// Base Db Context. Implements Identity Db Context over Application User
    /// </summary>
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
        public DbSet<Company> Company { get; set; }
        public DbSet<CompanyType> CompanyTypes { get; set; }

        public DbSet<CompanyDomain> CompanyDomains { get; set; }

        /// <summary>
        /// Page Sizes Db Set
        /// </summary>
        public DbSet<PaperSize> PaperSizes { get; set; }
        public DbSet<StockCategory> StockCategories { get; set; }
        public DbSet<StockSubCategory> StockSubCategories { get; set; }

        public DbSet<CmsPage> CmsPage { get; set; }

        public DbSet<CmsSkinPageWidget> PageWidgets { get; set; }

        public DbSet<CmsSkinPageWidgetParam> PageWidgetParams { get; set; }
        public DbSet<Widget> Widgets { get; set; }

        public DbSet<CompanyBanner> CompanyBanner { get; set; }

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
        public DbSet<ItemStateTax> ItemStateTaxs { get; set; }
        public DbSet<ItemStockControl> ItemStockControls { get; set; }
        public DbSet<ItemStockOption> ItemStockOptions { get; set; }
        public DbSet<ItemVdpPrice> ItemVdpPrices { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<SectionCostcentre> SectionCostcentres { get; set; }
        public DbSet<SectionCostCentreDetail> SectionCostCentre { get; set; }
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
        public DbSet<ColorPallete> ColorPalletes { get; set; }

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
        /// Get next id for a table
        /// </summary>
        public double GetMinimumProductValue(long itemId)
        {
            if (itemId <= 0)
            {
                throw new ArgumentException(LanguageResources.InvalidItem, "itemId");
            }

            ObjectParameter itemIdParameter = new ObjectParameter("ItemID", itemId);
            ObjectParameter result = new ObjectParameter("Result", typeof(int));
            ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction(
                "BaseDbContext.funGetMiniumProductValue", itemIdParameter, result);
            return (double)result.Value;
        }
        #endregion
    }
}
