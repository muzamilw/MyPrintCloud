using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Repository.BaseRepository;
using MPC.Repository.Repositories;

namespace MPC.Repository
{
    /// <summary>
    /// Repository Type Registration
    /// </summary>
    public static class TypeRegistrations
    {
        /// <summary>
        /// Register Types for Repositories
        /// </summary>
        public static void RegisterType(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<BaseDbContext>(new PerRequestLifetimeManager());
            unityContainer.RegisterType<IOrganisationRepository, OrganisationRepository>();
            unityContainer.RegisterType<IMarkupRepository, MarkupRepository>();
            unityContainer.RegisterType<ITaxRateRepository, TaxRateRepository>();
            unityContainer.RegisterType<IChartOfAccountRepository, ChartOfAccountRepository>();
            unityContainer.RegisterType<ICompanyDomainRepository, CompanyDomainRepository>();
            unityContainer.RegisterType<IPaperSheetRepository, PaperSheetRepository>();
            unityContainer.RegisterType<ICompanyRepository, CompanyRepository>();
            unityContainer.RegisterType<IStockCategoryRepository, StockCategoryRepository>();
            unityContainer.RegisterType<IStockSubCategoryRepository, StockSubCategoryRepository>();
            unityContainer.RegisterType<ICmsSkinPageWidgetRepository, CmsSkinPageWidgetRepository>();
            unityContainer.RegisterType<ICompanyBannerRepository, CompanyBannerRepository>();
            unityContainer.RegisterType<IProductCategoryRepository, ProductCategoryRepository>();
            unityContainer.RegisterType<IStockItemRepository, StockItemRepository>();
            unityContainer.RegisterType<IStockCostAndPriceRepository, StockCostAndPriceRepository>();
            unityContainer.RegisterType<ICmsPageRepository, CmsPageRepository>();
            unityContainer.RegisterType<IPageCategoryRepository, PageCategoryRepository>();
            unityContainer.RegisterType<ISectionFlagRepository, SectionFlagRepository>();
            unityContainer.RegisterType<ISectionRepository, SectionRepository>();
            unityContainer.RegisterType<IWeightUnitRepository, WeightUnitRepository>();
            unityContainer.RegisterType<IPaperSizeRepository, PaperSizeRepository>();
            unityContainer.RegisterType<IItemRepository, ItemRepository>();
            unityContainer.RegisterType<ISystemUserRepository, SystemUserRepository>();
            unityContainer.RegisterType<ILengthUnitRepository, LengthUnitRepository>();
            unityContainer.RegisterType<IPaperBasisAreaRepository, PaperBasisAreaRepository>();
            unityContainer.RegisterType<IRegistrationQuestionRepository, RegistrationQuestionRepository>();
            unityContainer.RegisterType<ICompanyContactRepository, CompanyContactRepository>();
            unityContainer.RegisterType<IGetItemsListViewRepository, GetItemsListViewRepository>();
            unityContainer.RegisterType<IRaveReviewRepository, RaveReviewRepository>();
            unityContainer.RegisterType<ICompanyCMYKColorRepository, CompanyCMYKColorRepository>();
            unityContainer.RegisterType<IItemVdpPriceRepository, ItemVdpPriceRepository>();
            unityContainer.RegisterType<IPrefixRepository, PrefixRepository>();
            unityContainer.RegisterType<IItemVideoRepository, ItemVideoRepository>();
            unityContainer.RegisterType<ICompanyTypeRepository, CompanyTypeRepository>();
            unityContainer.RegisterType<IItemRelatedItemRepository, ItemRelatedItemRepository>();
            unityContainer.RegisterType<IColorPalleteRepository, ColorPalleteRepository>();
            unityContainer.RegisterType<ITemplateRepository, TemplateRepository>();
        }
    }
}