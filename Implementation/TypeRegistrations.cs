using Microsoft.Practices.Unity;
using MPC.ExceptionHandling.Logger;
using MPC.Implementation.MISServices;
using MPC.Implementation.WebStoreServices;
using MPC.Interfaces.Logger;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.WebStoreServices;
using webstoreInterfaces = MPC.Interfaces.WebStoreServices;
using webstoreImplementation = MPC.Implementation.WebStoreServices;
using MISInterfaces = MPC.Interfaces.MISServices;
using MISImplementation = MPC.Implementation.MISServices;

namespace MPC.Implementation
{
    /// <summary>
    /// Type Registration for Implemention 
    /// </summary>
    public static class TypeRegistrations
    {
        /// <summary>
        /// Register Types for Implementation
        /// </summary>
        public static void RegisterType(IUnityContainer unityContainer)
        {
            Repository.TypeRegistrations.RegisterType(unityContainer);
            unityContainer.RegisterType<IMyOrganizationService, MyOrganizationService>();
            unityContainer.RegisterType<IMyCompanyDomainService, MyCompanyDomainService>();
            unityContainer.RegisterType<webstoreInterfaces.ICompanyService, webstoreImplementation.CompanyService>();
            unityContainer.RegisterType<IMPCLogger, MPCLogger>();
            unityContainer.RegisterType<IPaperSheetService, PaperSheetService>();
            unityContainer.RegisterType<IAuthorizationChecker, AuthorizationChecker>();
            unityContainer.RegisterType<ICmsSkinPageWidgetService, CmsSkinPageWidgetService>();
            unityContainer.RegisterType<ICompanyBannerService, CompanyBannerService>();
            unityContainer.RegisterType<IClaimsSecurityService, ClaimsSecurityService>();
            unityContainer.RegisterType<IInventoryService, InventoryService>();
            unityContainer.RegisterType<IStockCategoryService, StockCategoryService>();
            unityContainer.RegisterType<MISInterfaces.IItemService, MISImplementation.ItemService>();
            unityContainer.RegisterType<MISInterfaces.ICompanyService, MISImplementation.CompanyService>();
            unityContainer.RegisterType<IWebstoreClaimsSecurityService, WebstoreClaimsSecurityService>();
            unityContainer.RegisterType<IWebstoreClaimsHelperService, WebstoreClaimsHelperService>();
            unityContainer.RegisterType<Interfaces.MISServices.ICampaignService, MISServices.CampaignService>();
            unityContainer.RegisterType<IUserManagerService, UserManagerService>();
            unityContainer.RegisterType<webstoreInterfaces.IOrderService, webstoreImplementation.OrderService>();
            unityContainer.RegisterType<ICategoryService, CategoryService>();
            unityContainer.RegisterType<IPhraseLibraryService, PhraseLibraryService>();
            unityContainer.RegisterType<webstoreInterfaces.IItemService, webstoreImplementation.ItemService>();
            unityContainer.RegisterType<ITemplateService, TemplateService>();
            unityContainer.RegisterType<ITemplateColorStylesService, TemplateColorStylesService>();
            unityContainer.RegisterType<ITemplatePageService, TemplatePageService>();
            unityContainer.RegisterType<webstoreInterfaces.IPrefixService, webstoreImplementation.PrefixService>();
            unityContainer.RegisterType<MISInterfaces.IPrefixService, MISImplementation.PrefixService>();
            unityContainer.RegisterType<ITemplateObjectService, TemplateObjectService>();
            unityContainer.RegisterType<ITemplateFontsService, TemplateFontsService>();
            unityContainer.RegisterType<ITemplateBackgroundImagesService, TemplateBackgroundImagesService>();
            unityContainer.RegisterType<ICostCentreService, CostCentreService>();
            unityContainer.RegisterType<webstoreInterfaces.IListingService,webstoreImplementation.ListingService>();
            unityContainer.RegisterType<MISInterfaces.IListingService, MISImplementation.ListingService>();
            unityContainer.RegisterType<IImagePermissionsService, ImagePermissionService>();
            unityContainer.RegisterType<ICostCentersService, CostCenterService>();
            unityContainer.RegisterType<IMachineService, MachineService>();
            unityContainer.RegisterType<Interfaces.WebStoreServices.ICampaignService, WebStoreServices.CampaignService>();

            unityContainer.RegisterType<ICompanyTerritoryService, CompanyTerritoryService>();
            unityContainer.RegisterType<Interfaces.MISServices.ICampaignService, MISServices.CampaignService>();
            unityContainer.RegisterType<IAddressService, AddressService>();
            unityContainer.RegisterType<ICompanyContactService, CompanyContactService>();
            unityContainer.RegisterType<ICrmSupplierService, CrmSupplierService>();
            unityContainer.RegisterType<ICalendarService, CalendarService>();
            unityContainer.RegisterType<ICustomerService, CustomerService>();
            unityContainer.RegisterType<IDashboardService, DashboardService>();
            unityContainer.RegisterType<IOrderForCrmService, OrderForCrmService>();
            unityContainer.RegisterType<IInvoiceService, MISImplementation.InvoicesService>();
            unityContainer.RegisterType<MISInterfaces.IOrderService, MISImplementation.OrderService>();
            unityContainer.RegisterType<IStatusService, StatusService>();
            unityContainer.RegisterType<ISmartFormService, SmartFormService>();
            unityContainer.RegisterType<IPaypalPaymentRequestService, PaypalPaymentRequestService>();
            unityContainer.RegisterType<IPayPalResponseService, PayPalResponseService>();
            unityContainer.RegisterType<IPrePaymentService, PrePaymentService>();
            unityContainer.RegisterType<IPaymentGatewayService, PaymentGatewayService>();
            unityContainer.RegisterType<INABTransactionService, NABTransactionService>();
            unityContainer.RegisterType<ICompanyCostCentreService, CompanyCostCentreService>();
            unityContainer.RegisterType<ILookupMethodService, LookupMethodService>();
            unityContainer.RegisterType<IPurchaseService, PurchaseService>();
            unityContainer.RegisterType<IGoodsReceivedNoteService, GoodsReceivedNoteService>();
            unityContainer.RegisterType<IDeliveryCarriersService, DeliveryCarrierService>();
            unityContainer.RegisterType<ILengthConversionService, LengthConversionService>();
            unityContainer.RegisterType<IItemJobStatusService, ItemJobStatusService>();
            unityContainer.RegisterType<ICostCentreMatrixServices, CostCentreMatrixServices>();
            unityContainer.RegisterType<ICostCentreQuestionService, CostCentreQuestionService>();
            unityContainer.RegisterType<ISystemUserService, SystemUserService>();
            unityContainer.RegisterType<IReportService, ReportService>();
            unityContainer.RegisterType<IItemSectionService, ItemSectionService>();
            unityContainer.RegisterType<ITemplateVariableService, TemplateVariableService>();
            unityContainer.RegisterType<IInquiryService, InquiryService>();
            unityContainer.RegisterType<ILiveJobsService, LiveJobsService>();
            unityContainer.RegisterType<IDeliveryNotesService, DeliveryNotesService>();
            unityContainer.RegisterType<IExportReportHelper, ExportReportHelper>();
            unityContainer.RegisterType<ISectionService, SectionService>();
            unityContainer.RegisterType<IDiscountVoucherService, DiscountVoucherService>();
            unityContainer.RegisterType<IListingBulletPointsService, ListingBulletPointsService>();

        }
    }
}