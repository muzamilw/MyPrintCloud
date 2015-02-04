using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
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
            unityContainer.RegisterType<IAddressRepository, AddressRepository>();
            unityContainer.RegisterType<ICompanyTerritoryRepository, CompanyTerritoryRepository>();
            unityContainer.RegisterType<IItemVideoRepository, ItemVideoRepository>();
            unityContainer.RegisterType<ICompanyTypeRepository, CompanyTypeRepository>();
            unityContainer.RegisterType<ICurrencyRepository, CurrencyRepository>();
            unityContainer.RegisterType<IGlobalLanguageRepository, GlobalLanguageRepository>();
            unityContainer.RegisterType<IItemRelatedItemRepository, ItemRelatedItemRepository>();
            unityContainer.RegisterType<IColorPalleteRepository, ColorPalleteRepository>();
            unityContainer.RegisterType<ITemplateRepository, TemplateRepository>();
            unityContainer.RegisterType<ITemplatePageRepository, TemplatePageRepository>();
            unityContainer.RegisterType<ICampaignRepository, CampaignRepository>();
            unityContainer.RegisterType<IUserManagerRepository, UserManagerRepository>();
            unityContainer.RegisterType<IOrderRepository, OrderRepository>();
            unityContainer.RegisterType<ICompanyContactRoleRepository, CompanyContactRoleRepository>();
            unityContainer.RegisterType<IItemStockOptionRepository, ItemStockOptionRepository>();
            unityContainer.RegisterType<IItemAddOnCostCentreRepository, ItemAddonCostCentreRepository>();
            unityContainer.RegisterType<ICostCentreRepository, CostCentreRepository>();
            unityContainer.RegisterType<IEmailEventRepository, EmailEventRepository>();
            unityContainer.RegisterType<IMpcFileTableViewRepository, MpcFileTableViewRepository>();
            unityContainer.RegisterType<IPaymentGatewayRepository, PaymentGatewayRepository>();
            unityContainer.RegisterType<IPaymentMethodRepository, PaymentMethodRepository>();
            unityContainer.RegisterType<IWidgetRepository, WidgetRepository>();
            unityContainer.RegisterType<IOrganisationFileTableViewRepository, OrganisationFileTableViewRepository>();
            unityContainer.RegisterType<IItemPriceMatrixRepository, ItemPriceMatrixRepository>();
            unityContainer.RegisterType<IItemStateTaxRepository, ItemStateTaxRepository>();
            unityContainer.RegisterType<IStateRepository, StateRepository>();
            unityContainer.RegisterType<ICountryRepository, CountryRepository>();
            unityContainer.RegisterType<IItemStockControlRepository, ItemStockControlRepository>();
            unityContainer.RegisterType<ITemplateObjectRepository, TemplateObjectRepository>();
            unityContainer.RegisterType<ITemplateColorStylesRepository, TemplateColorStylesRepository>();
            unityContainer.RegisterType<IProductCategoryFileTableViewRepository, ProductCategoryFileTableViewRepository>();
            unityContainer.RegisterType<ITemplateFontsRepository, TemplateFontsRepository>();
            unityContainer.RegisterType<IItemProductDetailRepository, ItemProductDetailRepository>();
            unityContainer.RegisterType<ITemplateBackgroundImagesRepository, TemplateBackgroundImagesRepository>();
            unityContainer.RegisterType<IItemAttachmentRepository, ItemAttachmentRepository>();
            unityContainer.RegisterType<IFavoriteDesignRepository, FavoriteDesignRepository>();
            unityContainer.RegisterType<IListingRepository, ListingRepository>();
            unityContainer.RegisterType<IPhraseRespository, PhraseRespository>();
            unityContainer.RegisterType<IProductCategoryItemRepository, ProductCategoryItemRepository>();
            unityContainer.RegisterType<IPhraseFieldRepository, PhraseFieldRepository>();
            unityContainer.RegisterType<IImagePermissionsRepository, ImagePermissionRepository>();
            unityContainer.RegisterType<ICostCentreQuestionRepository, CostCentreQuestionRepository>();
            unityContainer.RegisterType<ICostCentreVariableRepository, CostCentreVariableRepository>();
            unityContainer.RegisterType<ICostCentreMatrixRepository, CostCentreMatrixRepository>();
            unityContainer.RegisterType<IInquiryRepository, InquiryRepository>();
            unityContainer.RegisterType<IInquiryAttachmentRepository, InquiryAttachmentRepository>();
            unityContainer.RegisterType<IMachineRepository, MachineRepository>();
            unityContainer.RegisterType<IGroupRepository, GroupRepository>();
            unityContainer.RegisterType<ICostCenterTypeRepository, CostCenterTypeRepository>();
            unityContainer.RegisterType<IItemSectionRepository, ItemSectionRepository>();
        }
    }
}