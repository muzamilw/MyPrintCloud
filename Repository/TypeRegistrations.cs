﻿using Microsoft.Practices.Unity;
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
            unityContainer.RegisterType<ISectionFlagRepository, SectionFlagRepository>();
            unityContainer.RegisterType<ISectionRepository, SectionRepository>();
            unityContainer.RegisterType<IWeightUnitRepository, WeightUnitRepository>();
        }
    }
}