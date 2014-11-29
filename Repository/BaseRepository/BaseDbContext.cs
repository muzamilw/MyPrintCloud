using System;
using System.Data.Entity;
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
        #endregion
    }
}
