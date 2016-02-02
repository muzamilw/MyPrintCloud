using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using MPC.Common;
using MPC.ExceptionHandling;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.LoggerModels;
using MPC.Models.ModelMappers;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using Ionic.Zip;
using System.Text;
using System.Xml;
using GrapeCity.ActiveReports;
using System.Data;
using System.Web.Http;
using System.Net;
using MPC.Repository.Repositories;

namespace MPC.Implementation.MISServices
{
    /// <summary>
    /// Order Service
    /// </summary>
    public class OrderService : IOrderService
    {
        #region Private
        private readonly IStockCategoryRepository stockCategoryRepository;
        private readonly IEstimateRepository estimateRepository;
        private readonly ISectionFlagRepository sectionFlagRepository;
        private readonly ICompanyContactRepository companyContactRepository;
        private readonly IAddressRepository addressRepository;
        private readonly ISystemUserRepository systemUserRepository;
        private readonly IPipeLineSourceRepository pipeLineSourceRepository;
        private readonly IMarkupRepository _markupRepository;
        private readonly IPaymentMethodRepository paymentMethodRepository;
        private readonly IOrganisationRepository organisationRepository;
        private readonly IChartOfAccountRepository chartOfAccountRepository;
        private readonly IPaperSizeRepository paperSizeRepository;
        private readonly IInkPlateSideRepository inkPlateSideRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IItemRepository itemRepository;
        private readonly MPC.Interfaces.WebStoreServices.ITemplateService templateService;
        private readonly IItemSectionRepository itemsectionRepository;
        private readonly IStockItemRepository stockItemRepository;
        private readonly ICompanyRepository companyRepository;
        private readonly IPrefixRepository prefixRepository;
        private readonly IPrePaymentRepository prePaymentRepository;
        private readonly IItemAttachmentRepository itemAttachmentRepository;
        private readonly ISectionCostCentreRepository sectionCostCentreRepository;
        private readonly IInkCoverageGroupRepository inkCoverageGroupRepository;
        private readonly ITemplateRepository templateRepository;
        private readonly ITemplatePageRepository templatePageRepository;
        private readonly IReportRepository ReportRepository;
        private readonly ICurrencyRepository CurrencyRepository;
        private readonly IMachineRepository MachineRepository;
        private readonly IPayPalResponseRepository PayPalRepsoitory;
        private readonly ICostCentreRepository CostCentreRepository;
        private readonly ISectionInkCoverageRepository sectionInkCoverageRepository;
        private readonly IShippingInformationRepository shippingInformationRepository;
        private readonly ISectionCostCentreDetailRepository sectionCostCentreDetailRepository;
        private readonly IItemSectionRepository itemSectionRepository;
        private readonly IPipeLineProductRepository pipeLineProductRepository;
        private readonly IExportReportHelper exportReportHelper;
        private readonly IPurchaseRepository purchaseRepository;
        private readonly ICampaignRepository campaignRepository;
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IInquiryAttachmentRepository inquiryAttachmentRepository;
        /// <summary>
        /// Creates New Order and assigns new generated code
        /// </summary>
        private Estimate CreateNewOrder(bool isEstimate = false)
        {
            string orderCode = !isEstimate ? prefixRepository.GetNextOrderCodePrefix() : prefixRepository.GetNextEstimateCodePrefix();
            Estimate itemTarget = estimateRepository.Create();
            
            estimateRepository.Add(itemTarget);
            itemTarget.CreationDate = itemTarget.CreationTime = DateTime.Now;
            if (isEstimate)
            {
                itemTarget.Estimate_Code = orderCode;
                itemTarget.Order_Date = null;
            }
            else
            {
                itemTarget.Order_Code = orderCode;
               
            }
            itemTarget.OrganisationId = orderRepository.OrganisationId;
            return itemTarget;
        }

        /// <summary>
        /// Creates New Pre Payment
        /// </summary>
        private PrePayment CreateNewPrePayment()
        {
            PrePayment itemTarget = prePaymentRepository.Create();
            prePaymentRepository.Add(itemTarget);
            return itemTarget;
        }

        /// <summary>
        /// Delete Pre Payment
        /// </summary>
        private void DeletePrePayment(PrePayment prePayment)
        {
            prePaymentRepository.Delete(prePayment);
        }

        /// <summary>
        /// Creates New Delivery Schedule
        /// </summary>
        private ShippingInformation CreateNewShippingInformation()
        {
            ShippingInformation itemTarget = shippingInformationRepository.Create();
            shippingInformationRepository.Add(itemTarget);
            return itemTarget;
        }

        /// <summary>
        /// Delete Delivery Schedule
        /// </summary>
        private void DeleteShippingInformation(ShippingInformation shippingInformation)
        {
            shippingInformationRepository.Delete(shippingInformation);
        }

        /// <summary>
        /// Creates New Item and assigns new generated code
        /// </summary>
        private Item CreateItem()
        {
            Item itemTarget = itemRepository.Create();
            itemRepository.Add(itemTarget);
            string itemCode = prefixRepository.GetNextItemCodePrefix(false);
            itemTarget.ItemCode = itemCode;
            itemTarget.OrganisationId = orderRepository.OrganisationId;
            return itemTarget;
        }

        /// <summary>
        /// Delete Item
        /// </summary>
        private void DeleteItem(Item item)
        {
            itemRepository.Delete(item);
        }

        /// <summary>
        /// Creates New Item Attachment new generated code
        /// </summary>
        private ItemAttachment CreateItemAttachment()
        {
            ItemAttachment itemTarget = itemAttachmentRepository.Create();
            itemTarget.UploadDate = DateTime.Now;
            itemTarget.UploadTime = DateTime.Now;
            itemAttachmentRepository.Add(itemTarget);
            return itemTarget;
        }

        /// <summary>
        /// Delete Item Attachment
        /// </summary>
        private void DeleteItemAttachment(ItemAttachment item)
        {
            itemAttachmentRepository.Delete(item);
        }

        /// <summary>
        /// Creates New Item Section new generated code
        /// </summary>
        private ItemSection CreateItemSection()
        {
            ItemSection itemTarget = itemsectionRepository.Create();
            itemsectionRepository.Add(itemTarget);
            return itemTarget;
        }

        /// <summary>
        /// Delete Item Section
        /// </summary>
        private void DeleteItemSection(ItemSection item)
        {
            itemsectionRepository.Delete(item);
        }

        /// <summary>
        /// Creates New Section Cost Centre
        /// </summary>
        private SectionCostcentre CreateSectionCostCentre()
        {
            SectionCostcentre itemTarget = sectionCostCentreRepository.Create();
            sectionCostCentreRepository.Add(itemTarget);
            return itemTarget;
        }

        /// <summary>
        /// Delete Section Cost Centre
        /// </summary>
        private void DeleteSectionCostCentre(SectionCostcentre item)
        {
            sectionCostCentreRepository.Delete(item);
        }

        /// <summary>
        /// Creates New Section Ink Coverage
        /// </summary>
        private SectionInkCoverage CreateSectionInkCoverage()
        {
            SectionInkCoverage itemTarget = sectionInkCoverageRepository.Create();
            sectionInkCoverageRepository.Add(itemTarget);
            return itemTarget;
        }

        /// <summary>
        /// Delete Section Ink Coverage
        /// </summary>
        private void DeleteSectionInkCoverage(SectionInkCoverage item)
        {
            sectionInkCoverageRepository.Delete(item);
        }

        /// <summary>
        /// Returns Next Job Code
        /// </summary>
        private string GetJobCodeForItem()
        {
            return prefixRepository.GetNextJobCodePrefix(false);
        }

        /// <summary>
        /// Creates New Section Cost Centre Detail
        /// </summary>
        private SectionCostCentreDetail CreateSectionCostCentreDetail()
        {
            SectionCostCentreDetail itemTarget = sectionCostCentreDetailRepository.Create();
            sectionCostCentreDetailRepository.Add(itemTarget);
            return itemTarget;
        }

        /// <summary>
        /// Delete Section Cost Centre Detail
        /// </summary>
        private void DeleteSectionCostCentreDetail(SectionCostCentreDetail item)
        {
            sectionCostCentreDetailRepository.Delete(item);
        }

        /// <summary>
        /// Saves Image to File System
        /// </summary>
        /// <param name="mapPath">File System Path for Item</param>
        /// <param name="existingImage">Existing File if any</param>
        /// <param name="caption">Unique file caption e.g. ItemId + ItemProductCode + ItemProductName + "_thumbnail_"</param>
        /// <param name="fileName">Name of file being saved</param>
        /// <param name="fileSource">Base64 representation of file being saved</param>
        /// <param name="fileSourceBytes">Byte[] representation of file being saved</param>
        /// <param name="fileNameWithoutExtension">File Name without extension to be set for Item Attachemt Record</param>
        /// <returns>Path of File being saved</returns>
        private string SaveImage(string mapPath, string existingImage, string caption, string fileName,
            string fileSource, byte[] fileSourceBytes, out string fileNameWithoutExtension)
        {
            if (!string.IsNullOrEmpty(fileSource))
            {
                // Look if file already exists then replace it
                if (!string.IsNullOrEmpty(existingImage))
                {
                    if (Path.IsPathRooted(existingImage))
                    {
                        if (File.Exists(existingImage))
                        {
                            // Remove Existing File
                            File.Delete(existingImage);
                        }
                    }
                    else
                    {
                        string filePath = HttpContext.Current.Server.MapPath("~/" + existingImage);
                        if (File.Exists(filePath))
                        {
                            // Remove Existing File
                            File.Delete(filePath);
                        }
                    }

                }

                // First Time Upload
                string imageurl = mapPath + "\\" + caption + fileName;
                File.WriteAllBytes(imageurl, fileSourceBytes);
                fileNameWithoutExtension = Path.GetFileNameWithoutExtension(imageurl);
                return Path.GetExtension(imageurl);
            }

            fileNameWithoutExtension = string.Empty;
            return null;
        }

        /// <summary>
        /// Save Item Attachments
        /// </summary>
        private void SaveItemAttachments(Estimate estimate)
        {
            if(estimate.isDirectSale != true)
                return;
            ;
           
            string mpcContentPath = ConfigurationManager.AppSettings["MPC_Content"];
            HttpServerUtility server = HttpContext.Current.Server;
            string mapPath = server.MapPath(mpcContentPath + "/Attachments/" + itemRepository.OrganisationId + "/" + estimate.CompanyId + "/Products/");

            if (estimate.Items == null)
            {
                return;
            }

            foreach (Item item in estimate.Items)
            {
                string attachmentMapPath = mapPath + item.ItemId;
                DirectoryInfo directoryInfo = null;
                // Create directory if not there
                if (!Directory.Exists(attachmentMapPath))
                {
                    directoryInfo = Directory.CreateDirectory(attachmentMapPath);
                }

                if (item.ItemAttachments == null)
                {
                    continue;
                }

                foreach (ItemAttachment itemAttachment in item.ItemAttachments)
                {
                    string folderPath = directoryInfo != null ? directoryInfo.FullName : attachmentMapPath;
                    int indexOf = folderPath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                    folderPath = folderPath.Substring(indexOf, folderPath.Length - indexOf);
                    itemAttachment.FolderPath = folderPath;
                    string fileNameWithoutExtension;
                    string fileExtension = SaveImage(attachmentMapPath, itemAttachment.FolderPath, "",
                        itemAttachment.FileName,
                        itemAttachment.FileSource, itemAttachment.FileSourceBytes, out fileNameWithoutExtension);
                    if (fileExtension != null)
                    {
                        itemAttachment.FileName = fileNameWithoutExtension;
                        itemAttachment.FileType = fileExtension;
                    }
                }
            }
        }

        #region Generate Purchase Orders
        private void UpdatePurchaseOrders(Estimate dbOrder, Estimate newOrder)
        {
            //if (dbOrder.StatusId != 4 && newOrder.StatusId == 4)
            //{
            GeneratePO(newOrder.EstimateId, newOrder.ContactId ?? new long(), newOrder.CompanyId, organisationRepository.LoggedInUserId);
            //}
        }

        private void DeletePurchaseOrders(Estimate order)
        {
            var listPurchases = purchaseRepository.GetPurchasesList(order.EstimateId);
            long SupplierConatctId = 0;
            purchaseRepository.DeletePO(order.EstimateId);

           
              if (listPurchases != null)
              {
                  foreach (var purchase in listPurchases)
                  {
                      SupplierConatctId = purchase.Value;
                  }
              }

              Company objCompany = companyRepository.GetCompanyByCompanyID(order.CompanyId);
            campaignRepository.POEmailToSupplier(order.EstimateId, order.CompanyId, order.ContactId ?? 0, 250, SupplierConatctId, string.Empty, objCompany,true);

        }
        // ReSharper disable once InconsistentNaming
        private bool GeneratePO(long orderId, long contactId, long companyId, Guid createdBy)
        {
            string serverPath = HttpContext.Current.Request.Url.Host;

            bool isPoGenerate = purchaseRepository.GeneratePO(orderId, createdBy);
            if (isPoGenerate)
            {
                POEmail(serverPath, orderId, contactId, companyId);
            }
            return true;
        }
        // ReSharper disable once InconsistentNaming
        private void POEmail(string serverPath, long orderId, long contactId, long companyId)
        {
            // string szDirectory = WebConfigurationManager.AppSettings["VirtualDirectory"].ToString();
            List<string> attachmentsList = new List<string>();
            var listPurchases = purchaseRepository.GetPurchasesList(orderId);
            if (listPurchases != null)
            {
                foreach (var purchase in listPurchases)
                {
                    string fileName = string.Empty;
                    long reportId = ReportRepository.CheckCustomReportForPOEmail();
                    if(reportId > 0)
                        fileName = exportReportHelper.ExportPDF((int)reportId, purchase.Key, ReportType.PurchaseOrders, orderId, string.Empty);
                    else
                        fileName = exportReportHelper.ExportPDF(100, purchase.Key, ReportType.PurchaseOrders, orderId, string.Empty);

                    int itemIDs = orderRepository.GetFirstItemIDByOrderId(orderId);
                    Organisation compOrganisation = organisationRepository.GetOrganizatiobByID();
                    Company objCompany = companyRepository.GetCompanyByCompanyID(companyId);
                    SystemUser saleManager = systemUserRepository.GetUserrById(objCompany.SalesAndOrderManagerId1 ?? Guid.NewGuid());

                    string salesManagerFile = "/" + ImagePathConstants.ReportPath + compOrganisation.OrganisationId + "/" + purchase.Key + "PurchaseReport.pdf";
                    campaignRepository.POEmailToSalesManager(orderId, companyId, contactId, 250, purchase.Value, salesManagerFile, objCompany);

                    //if (objCompany.IsCustomer == (int)CustomerTypes.Corporate)
                    //{
                    //    campaignRepository.SendEmailToSalesManager((int)Events.PO_Notification_To_SalesManager, contactId, companyId, orderId, compOrganisation, compOrganisation.OrganisationId, 0, StoreMode.Corp, companyId, saleManager, itemIDs, "", "", 0);
                    //}
                    //else
                    //{
                    //    campaignRepository.SendEmailToSalesManager((int)Events.PO_Notification_To_SalesManager, contactId, companyId, orderId, compOrganisation, compOrganisation.OrganisationId, 0, StoreMode.Retail, companyId, saleManager, itemIDs, "", "", 0);
                    //}

                    string sourceFile = fileName;
                    string destinationFileSupplier = "/" + ImagePathConstants.ReportPath + compOrganisation.OrganisationId + "/" + purchase.Value + "/" + purchase.Key + "_PurchaseOrder.pdf";

                    string oDirectory = HttpContext.Current.Server.MapPath("~/" + ImagePathConstants.ReportPath + compOrganisation.OrganisationId + "/" + purchase.Value);

                    string destinationPhysicalFileSupplier = HttpContext.Current.Server.MapPath("~/" + destinationFileSupplier);

                    if (!Directory.Exists(oDirectory))
                    {
                        Directory.CreateDirectory(oDirectory);
                    }
                    
                    if (File.Exists(sourceFile))
                    {
                        File.Copy(sourceFile, destinationPhysicalFileSupplier);
                    }

                    campaignRepository.POEmailToSupplier(orderId, companyId, contactId, 250, purchase.Value, destinationFileSupplier, objCompany,false);

                    // SendEmailToSupplier(ServerPath, OrderID, ContactCompanyID, ContactID, 250, purchase.SupplierID ?? 0, DestinationFileSupplier);

                    // AttachmentsList.Add(FilePath);
                }
            }
        }
        #endregion

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public OrderService(IEstimateRepository estimateRepository, ISectionFlagRepository sectionFlagRepository, ICompanyContactRepository companyContactRepository,
            IAddressRepository addressRepository, ISystemUserRepository systemUserRepository, IPipeLineSourceRepository pipeLineSourceRepository, IMarkupRepository markupRepository,
            IPaymentMethodRepository paymentMethodRepository, IOrganisationRepository organisationRepository, IStockCategoryRepository stockCategoryRepository, IOrderRepository orderRepository, IItemRepository itemRepository, MPC.Interfaces.WebStoreServices.ITemplateService templateService,
            IChartOfAccountRepository chartOfAccountRepository, IItemSectionRepository itemsectionRepository, IPaperSizeRepository paperSizeRepository, IInkPlateSideRepository inkPlateSideRepository, IStockItemRepository stockItemRepository, IInkCoverageGroupRepository inkCoverageGroupRepository,
            ICompanyRepository companyRepository, IPrefixRepository prefixRepository, IPrePaymentRepository prePaymentRepository,
            IItemAttachmentRepository itemAttachmentRepository, ITemplateRepository templateRepository, ITemplatePageRepository templatePageRepository,
            IReportRepository ReportRepository, ICurrencyRepository CurrencyRepository, IMachineRepository MachineRepository, ICostCentreRepository CostCentreRepository,
            IPayPalResponseRepository PayPalRepsoitory, ISectionCostCentreRepository sectionCostCentreRepository,
            ISectionInkCoverageRepository sectionInkCoverageRepository, IShippingInformationRepository shippingInformationRepository,
            ISectionCostCentreDetailRepository sectionCostCentreDetailRepository, IPipeLineProductRepository pipeLineProductRepository, IItemStockOptionRepository itemStockOptionRepository, IItemSectionRepository itemSectionRepository, IItemAddOnCostCentreRepository itemAddOnCostCentreRepository, IExportReportHelper exportReportHelper
            , IPurchaseRepository purchaseRepository, ICampaignRepository campaignRepository, IInvoiceRepository invoiceRepository, IInquiryAttachmentRepository inquiryAttachmentRepository)
        {
            if (estimateRepository == null)
            {
                throw new ArgumentNullException("estimateRepository");
            }
            if (invoiceRepository == null)
            {
                throw new ArgumentNullException("invoiceRepository");
            }
            if (companyContactRepository == null)
            {
                throw new ArgumentNullException("companyContactRepository");
            }
            if (addressRepository == null)
            {
                throw new ArgumentNullException("addressRepository");
            }
            if (systemUserRepository == null)
            {
                throw new ArgumentNullException("systemUserRepository");
            }
            if (pipeLineSourceRepository == null)
            {
                throw new ArgumentNullException("pipeLineSourceRepository");
            }
            if (paymentMethodRepository == null)
            {
                throw new ArgumentNullException("paymentMethodRepository");
            }
            if (organisationRepository == null)
            {
                throw new ArgumentNullException("organisationRepository");
            }
            if (chartOfAccountRepository == null)
            {
                throw new ArgumentNullException("chartOfAccountRepository");
            }
            if (paperSizeRepository == null)
            {
                throw new ArgumentNullException("paperSizeRepository");
            }
            if (inkPlateSideRepository == null)
            {
                throw new ArgumentNullException("inkPlateSideRepository");
            }
            if (itemsectionRepository == null)
            {
                throw new ArgumentNullException("itemsectionRepository");
            }
            if (companyRepository == null)
            {
                throw new ArgumentNullException("companyRepository");
            }
            if (prefixRepository == null)
            {
                throw new ArgumentNullException("prefixRepository");
            }
            if (prePaymentRepository == null)
            {
                throw new ArgumentNullException("prePaymentRepository");
            }
            if (itemAttachmentRepository == null)
            {
                throw new ArgumentNullException("itemAttachmentRepository");
            }
            if (sectionCostCentreRepository == null)
            {
                throw new ArgumentNullException("sectionCostCentreRepository");
            }
            if (sectionInkCoverageRepository == null)
            {
                throw new ArgumentNullException("sectionInkCoverageRepository");
            }
            if (shippingInformationRepository == null)
            {
                throw new ArgumentNullException("shippingInformationRepository");
            }
            if (sectionCostCentreDetailRepository == null)
            {
                throw new ArgumentNullException("sectionCostCentreDetailRepository");
            }
            this.estimateRepository = estimateRepository;
            this.invoiceRepository = invoiceRepository;
            this.companyRepository = companyRepository;
            this.prefixRepository = prefixRepository;
            this.prePaymentRepository = prePaymentRepository;
            this.itemAttachmentRepository = itemAttachmentRepository;
            this.sectionCostCentreRepository = sectionCostCentreRepository;
            this.sectionFlagRepository = sectionFlagRepository;
            this.companyContactRepository = companyContactRepository;
            this.addressRepository = addressRepository;
            this.systemUserRepository = systemUserRepository;
            this.pipeLineSourceRepository = pipeLineSourceRepository;
            _markupRepository = markupRepository;
            this.paymentMethodRepository = paymentMethodRepository;
            this.organisationRepository = organisationRepository;
            this.orderRepository = orderRepository;
            this.stockCategoryRepository = stockCategoryRepository;
            this.chartOfAccountRepository = chartOfAccountRepository;
            this.paperSizeRepository = paperSizeRepository;
            this.inkPlateSideRepository = inkPlateSideRepository;
            this.stockItemRepository = stockItemRepository;
            this.inkCoverageGroupRepository = inkCoverageGroupRepository;
            this.itemRepository = itemRepository;
            this.templateService = templateService;
            this.itemsectionRepository = itemsectionRepository;
            this.templateRepository = templateRepository;
            this.templatePageRepository = templatePageRepository;
            this.ReportRepository = ReportRepository;
            this.CurrencyRepository = CurrencyRepository;
            this.MachineRepository = MachineRepository;
            this.CostCentreRepository = CostCentreRepository;
            this.PayPalRepsoitory = PayPalRepsoitory;
            this.sectionInkCoverageRepository = sectionInkCoverageRepository;
            this.shippingInformationRepository = shippingInformationRepository;
            this.sectionCostCentreDetailRepository = sectionCostCentreDetailRepository;
            this.pipeLineProductRepository = pipeLineProductRepository;
            this.itemSectionRepository = itemSectionRepository;
            this.sectionCostCentreDetailRepository = sectionCostCentreDetailRepository;
            this.exportReportHelper = exportReportHelper;
            this.purchaseRepository = purchaseRepository;
            this.campaignRepository = campaignRepository;
            this.inquiryAttachmentRepository = inquiryAttachmentRepository;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get All Orders
        /// </summary>
        public GetOrdersResponse GetAll(GetOrdersRequest request)
        {
            var result = estimateRepository.GetOrders(request);
            return result;
        }
        /// <summary>
        /// Get Orders For Estimates List View
        /// </summary>
        public GetOrdersResponse GetOrdersForEstimates(GetOrdersRequest request)
        {
            return estimateRepository.GetOrdersForEstimates(request);
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        public Estimate GetById(long orderId)
        {
            Estimate estimate = estimateRepository.Find(orderId);
            if (estimate != null)
            {
                Invoice invoice = invoiceRepository.GetInvoiceByEstimateId(estimate.EstimateId);
                if (invoice != null)
                {
                    estimate.InvoiceStatus = invoice.InvoiceStatus;
                }
                bool isExtra = CheckIsExtraOrder(orderId, estimate.isDirectSale ?? true);
                estimate.IsExtraOrder = isExtra;
            }
            
            return estimate;
        }

        /// <summary>
        /// Delete Order
        /// </summary>
        public void DeleteOrder(long orderId)
        {
            orderRepository.DeleteOrder(orderId);
        }

        /// <summary>
        /// Save Order
        /// </summary>
        public Estimate SaveOrder(Estimate estimate)
        {
            // Get Order if exists else create new

            Estimate order = GetById(estimate.EstimateId) ?? CreateNewOrder(estimate.isEstimate == true);
            var OldstatusId = order.StatusId;
            if (estimate.EstimateId == 0 && estimate.isEstimate == true)
            {
                var flags = sectionFlagRepository.GetSectionFlagBySectionId((int)SectionEnum.Estimate);
                if (flags != null)
                {
                    estimate.SectionFlag = flags.FirstOrDefault();
                    estimate.SectionFlagId = flags.FirstOrDefault().SectionFlagId;
                }
            }
            
            var orderStatusId = estimate.StatusId;
            try
            {
                estimate.UpdateTo(order, new OrderMapperActions
                {
                    CreatePrePayment = CreateNewPrePayment,
                    DeletePrePayment = DeletePrePayment,
                    CreateItem = CreateItem,
                    DeleteItem = DeleteItem,
                    CreateItemSection = CreateItemSection,
                    DeleteItemSection = DeleteItemSection,
                    CreateSectionCostCentre = CreateSectionCostCentre,
                    DeleteSectionCostCenter = DeleteSectionCostCentre,
                    CreateItemAttachment = CreateItemAttachment,
                    DeleteItemAttachment = DeleteItemAttachment,
                    CreateSectionInkCoverage = CreateSectionInkCoverage,
                    DeleteSectionInkCoverage = DeleteSectionInkCoverage,
                    CreateShippingInformation = CreateNewShippingInformation,
                    DeleteShippingInformation = DeleteShippingInformation,
                    GetNextJobCode = GetJobCodeForItem,
                    CreateSectionCostCenterDetail = CreateSectionCostCentreDetail,
                    DeleteSectionCostCenterDetail = DeleteSectionCostCentreDetail,
                });
            }
            catch (Exception exp)
            {
                throw new MPCException("Failed to save order. Error: " + exp.Message, estimateRepository.OrganisationId);
            }

            // Update Order
            
            // Save Changes
            estimateRepository.SaveChanges();

            // Save Item Attachments
            SaveItemAttachments(order);

            // Save Changes
            estimateRepository.SaveChanges();

            //If Data not posted to Unleashed(Xero)
            if (estimate.isEstimate == false && estimate.XeroAccessCode == null)
                PostOrderToXero(order.EstimateId);
            //Update Purchase Orders
            //Req. Whenever Its Status is inProduction Update Purchase Orders

            if ((OldstatusId == (int)OrderStatus.ConfirmedOrder || OldstatusId == (int)OrderStatus.PendingOrder) && (estimate.StatusId == (int)OrderStatus.InProduction || estimate.StatusId == (int)OrderStatus.Completed_NotShipped || estimate.StatusId == (int)OrderStatus.CompletedAndShipped_Invoiced || estimate.StatusId == (int)OrderStatus.Invoice))
            {
                try
                {
                    UpdatePurchaseOrders(order, estimate);
                }
                catch (Exception exp)
                {
                    throw new MPCException("Saved Sucessfully but failed to create Purchase Order. Error: " + exp.Message, estimateRepository.OrganisationId);
                }
            }
            

            //Delete Purchase Orders
            //Req. Whenever Its Status is Cancelled Call Delete Stored Procedure or delete sp if reversing from in production to below statuses
            //if ((estimate.StatusId == (int)OrderStatus.CancelledOrder) ||
            //    (orderStatusId == (int)OrderStatus.InProduction && (estimate.StatusId == (int)OrderStatus.PendingOrder || estimate.StatusId == (int)OrderStatus.ConfirmedOrder)))
            //{
            if (estimate.StatusId == (int)OrderStatus.CancelledOrder || estimate.StatusId == (int)OrderStatus.PendingOrder || estimate.StatusId == (int)OrderStatus.ConfirmedOrder)
            {
                try
                {
                    DeletePurchaseOrders(estimate);
                }
                catch (Exception exp)
                {
                    throw new MPCException("Saved Sucessfully but failed to delete Purchase Order(s). Error: " + exp.Message, estimateRepository.OrganisationId);
                }
            }


            //Create Invoice
            //Req. Whenever Its Status is Shipped and Invoiced 
            if (estimate.StatusId == (int)OrderStatus.Invoice)
            {
                try
                {
                    Invoice invoice = invoiceRepository.GetInvoiceByEstimateId(order.EstimateId);
                    if (invoice == null)
                    {
                        CreateInvoice(order);
                        // Save Changes
                        estimateRepository.SaveChanges();
                    }

                }
                catch (Exception exp)
                {
                    throw new MPCException("Saved Sucessfully but failed to create Invoice. Error: " + exp.Message, estimateRepository.OrganisationId);
                }
            }


            // Load Status
            estimateRepository.LoadProperty(order, () => order.Status);

            // Return 
            return order;
        }

        private void CreateInvoice(Estimate order)
        {
            Invoice itemTarget = CreateNewInvoice();

            if (order.isDirectSale ?? true)
            {
                itemTarget.InvoiceType = 0;
            }
            else
            {
                itemTarget.InvoiceType = 1;
            }

            long FlagId = invoiceRepository.GetInvoieFlag();
            if(FlagId > 0)
            {
                itemTarget.FlagID = (int)FlagId;
            }
            else
            {
                itemTarget.FlagID = 0;
            }
            itemTarget.ReportSignedBy = order.ReportSignedBy;
            order.AddInvoice(itemTarget);
        }

        /// <summary>
        /// Creates New Invoice
        /// </summary>
        private Invoice CreateNewInvoice()
        {
            Invoice itemTarget = invoiceRepository.Create();
            string invoiceCode = prefixRepository.GetNextInvoiceCodePrefix();
            itemTarget.InvoiceCode = invoiceCode;
            itemTarget.OrganisationId = estimateRepository.OrganisationId;
            itemTarget.InvoiceStatus = (int)InvoiceStatuses.Awaiting;
            invoiceRepository.Add(itemTarget);
            return itemTarget;
        }

        /// <summary>
        /// Get base data for order
        /// </summary>
        public OrderBaseResponse GetBaseData()
        {


            return new OrderBaseResponse
                   {
                       SectionFlags = sectionFlagRepository.GetSectionFlagBySectionId((int)SectionEnum.Order),
                       SystemUsers = systemUserRepository.GetAll(),
                       PipeLineSources = pipeLineSourceRepository.GetAll(),
                       PaymentMethods = paymentMethodRepository.GetAll(),
                       Organisation = organisationRepository.Find(organisationRepository.OrganisationId),
                       // ChartOfAccounts = chartOfAccountRepository.GetAll(),
                       CostCenters = CostCentreRepository.GetAllCompanyCentersForOrderItem(),
                       PipeLineProducts = pipeLineProductRepository.GetAll(),
                       LoggedInUser = organisationRepository.LoggedInUserId,
                   };
        }

        /// <summary>
        /// Get base data for Estimate
        /// Difference from order is Different Section Id
        /// </summary>
        public OrderBaseResponse GetBaseDataForEstimate()
        {
            SystemUser systemUser = systemUserRepository.GetUserrById(systemUserRepository.LoggedInUserId);
            return new OrderBaseResponse
            {
                SectionFlags = sectionFlagRepository.GetSectionFlagBySectionId((int)SectionEnum.Estimate),
                SystemUsers = systemUserRepository.GetAll(),
                PipeLineSources = pipeLineSourceRepository.GetAll(),
                PaymentMethods = paymentMethodRepository.GetAll(),
                Organisation = organisationRepository.Find(organisationRepository.OrganisationId),
                // ChartOfAccounts = chartOfAccountRepository.GetAll(),
                CostCenters = CostCentreRepository.GetAllCompanyCentersForOrderItem(),
                PipeLineProducts = pipeLineProductRepository.GetAll(),
                LoggedInUser = organisationRepository.LoggedInUserId,
                HeadNotes = systemUser != null ? systemUser.EstimateHeadNotes : string.Empty,
                FootNotes = systemUser != null ? systemUser.EstimateFootNotes : string.Empty,
            };
        }

        /// <summary>
        /// Get base data for Inquiries
        /// </summary>
        public InquiryBaseResponse GetBaseDataForInquiries()
        {
            return new InquiryBaseResponse
            {
                SectionFlags = sectionFlagRepository.GetSectionFlagBySectionId((int)SectionEnum.Inquiries)
            };
        }

        public ItemDetailBaseResponse GetBaseDataForItemDetails()
        {
            Organisation organisation = organisationRepository.GetOrganizatiobByID();
            List<Markup> markups = _markupRepository.GetAll().ToList();
            Markup defaultMarkup = markups.FirstOrDefault(x => x.IsDefault == true);
            return new ItemDetailBaseResponse
            {
                Markups = markups,
                PaperSizes = paperSizeRepository.GetAll(),
                InkPlateSides = inkPlateSideRepository.GetAll(),
                Inks = stockItemRepository.GetStockItemOfCategoryInk().Where(i => i.IsImperical == organisation.IsImperical),
                InkCoverageGroups = inkCoverageGroupRepository.GetAll(),
                CurrencySymbol = organisation != null ? (organisation.Currency != null ? organisation.Currency.CurrencySymbol : string.Empty) : string.Empty,
                SystemUsers = systemUserRepository.GetAll(),
                LengthUnit = organisation != null && organisation.LengthUnit != null ? organisation.LengthUnit.UnitName : string.Empty,
                WeightUnit = organisation != null && organisation.WeightUnit != null ? organisation.WeightUnit.UnitName : string.Empty,
                LoggedInUser = organisationRepository.LoggedInUserId,
                Machines = MachineRepository.GetAll(),
                DefaultMarkUpId = defaultMarkup != null ? defaultMarkup.MarkUpId : 0
            };

        }

        /// <summary>
        /// Get Base Data For Company
        /// </summary>
        public OrderBaseResponseForCompany GetBaseDataForCompany(long companyId, long storeId)
        {
            bool isStoreLive = companyRepository.IsStoreLive(storeId);
            var org = organisationRepository.GetOrganizatiobByID();
            if(org.isTrial ?? true)
            {
                isStoreLive = true;
            }

            //bool isMisReached = GetMonthlyOrdersReached(org, true);
            //bool isWebReached = GetMonthlyOrdersReached(org, false);

            return new OrderBaseResponseForCompany
                {
                    CompanyContacts = companyContactRepository.GetCompanyContactsByCompanyId(companyId),
                    CompanyAddresses = addressRepository.GetAddressByCompanyID(companyId),
                    TaxRate = companyRepository.GetTaxRateByStoreId(storeId),
                    JobManagerId = companyRepository.GetStoreJobManagerId(storeId),
                    IsStoreLive = isStoreLive
                };
        }

        public bool GetMonthlyOrdersReached(Organisation org, bool isMis)
        {
            bool isReached = false;
            DateTime? billingDate = org.BillingDate;
            if (billingDate != null)
            {
                List<long> orders = orderRepository.GetOrdersForBillingCycle(Convert.ToDateTime(billingDate), isMis);
                int licensedOrders = isMis ? org.MisOrdersCount ?? 0 : org.WebStoreOrdersCount ?? 0;
                if (orders.Count > licensedOrders)
                    isReached = true;
            }
            return isReached;
        }

        private bool CheckIsExtraOrder(long orderId, bool isDirectSale)
        {
            var org = organisationRepository.GetOrganizatiobByID();
            int ordersCount = isDirectSale ? org.MisOrdersCount??0 : org.WebStoreOrdersCount ?? 0;
            DateTime billingDate = org.BillingDate ?? DateTime.Now;
            bool isExtra = orderRepository.IsExtradOrderForBillingCycle(billingDate, isDirectSale, ordersCount, orderId, org.OrganisationId);
            if (org.isTrial == false)
            {
                return isExtra;
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// Get Order Statuses Count For Menu Items
        /// </summary>
        /// <returns></returns>
        public OrderMenuCount GetOrderScreenMenuItemCount()
        {
            return estimateRepository.GetOrderStatusesCountForMenuItems();
        }

        public bool DeleteCart(long CompanyID, long OrganisationID)
        {
            try
            {
                List<Estimate> cartorders = orderRepository.GetCartOrdersByCompanyID(CompanyID);

                orderRepository.DeleteCart(CompanyID);
                List<string> ImagesPath = new List<string>();

                if (cartorders != null && cartorders.Count > 0)
                {

                    foreach (var cartOrd in cartorders)
                    {
                        DeleteItemsPhysically(cartOrd, OrganisationID);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public bool DeleteOrderSP(long OrderID, long OrganisationID)
        {
            try
            {

                Estimate order = orderRepository.GetOrderByOrderID(OrderID);
                orderRepository.DeleteOrderBySP(OrderID);

                DeleteItemsPhysically(order, OrganisationID);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteItemsPhysically(Estimate order, long OrganisationID)
        {
            try
            {

                // delete item files
                List<string> ImagesPath = new List<string>();
                if (order != null)
                {
                    List<Item> items = itemRepository.GetItemsByOrderID(order.EstimateId);
                    if (items != null && items.Count > 0)
                    {
                        foreach (var itm in items)
                        {
                            if (itm.ItemAttachments != null && itm.ItemAttachments.Count > 0)
                            {
                                foreach (var itemAttach in itm.ItemAttachments)
                                {
                                    string path = itemAttach.FolderPath + itemAttach.FileName;

                                    ImagesPath.Add(path);
                                }
                            }
                            if (itm.ItemStockOptions != null && itm.ItemStockOptions.Count > 0)
                            {
                                foreach (var itemStock in itm.ItemStockOptions)
                                {
                                    string path = itemStock.ImageURL;

                                    ImagesPath.Add(path);
                                }
                            }
                            if (itm.TemplateId != null && itm.TemplateId > 0)
                            {
                                templateService.DeleteTemplateFiles(itm.ItemId, OrganisationID);
                                // delete template folder
                            }

                            // delete item files
                            string SourceDelFiles = HttpContext.Current.Server.MapPath("/MPC_Content/products/" + OrganisationID + "/" + itm.ItemId);

                            if (Directory.Exists(SourceDelFiles))
                            {
                                Directory.Delete(SourceDelFiles, true);
                            }

                            // delete itemattachments

                            string SourceDelAttachments = HttpContext.Current.Server.MapPath("/MPC_Content/Attachments/Organisation" + OrganisationID + "/" + OrganisationID + "/" + itm.ItemId);

                            if (Directory.Exists(SourceDelAttachments))
                            {
                                Directory.Delete(SourceDelAttachments, true);
                            }
                        }

                        // delete files


                        if (ImagesPath != null && ImagesPath.Count > 0)
                        {
                            foreach (var img in ImagesPath)
                            {
                                if (!string.IsNullOrEmpty(img))
                                {
                                    string filePath = HttpContext.Current.Server.MapPath("~/" + img);
                                    if (File.Exists(filePath))
                                    {
                                        File.Delete(filePath);
                                    }
                                }
                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ProgressEstimateToOrder(ProgressEstimateRequestModel requestModel)
        {
            try
            {
                var estimate = estimateRepository.Find(requestModel.EstimateId);
                var order = estimateRepository.Find(requestModel.OrderId);
                //update Estimate Reference and status
                estimate.RefEstimateId = requestModel.OrderId;
                estimate.StatusId = 39;
                //update order refence of estimate
                order.RefEstimateId = requestModel.EstimateId;

                estimateRepository.Update(estimate);
                estimateRepository.Update(order);
                estimateRepository.SaveChanges();

                return true;
            }
            catch (Exception exception)
            {
                return false;

            }
        }

        public Estimate CloneOrder(Estimate source)
        {
            Estimate target = CreateNewOrder();
            target.isEstimate = false;
            target.StatusId = (short)OrderStatus.ConfirmedOrder;

            Estimate est_Source = GetById(source.EstimateId);
            est_Source.StatusId = 39;

            target = UpdateEstimeteOnCloning(est_Source, target, source);
            target.RefEstimateId = source.EstimateId;
            target.OrderReportSignedBy = source.ReportSignedBy;

            estimateRepository.SaveChanges();

            est_Source.RefEstimateId = target.EstimateId;
            estimateRepository.SaveChanges();

            return target;
        }

        public Estimate UpdateEstimeteOnCloning(Estimate source, Estimate target, Estimate clientSource)
        {
            // Clone Estimate
            source.Clone(target);
            target.Order_Date = DateTime.Now;
            target.OrderManagerId = clientSource.OrderManagerId;
            target.IsOfficialOrder = clientSource.IsOfficialOrder;
            target.CustomerPO = clientSource.CustomerPO;
            target.ArtworkByDate = clientSource.ArtworkByDate;
            target.DataByDate = clientSource.DataByDate;
            target.PaperByDate = clientSource.PaperByDate;
            target.DataByDate = clientSource.DataByDate;
            target.TargetBindDate = clientSource.TargetPrintDate;
            target.StartDeliveryDate = clientSource.StartDeliveryDate;
            target.FinishDeliveryDate = clientSource.FinishDeliveryDate;
            target.IsCreditApproved = clientSource.IsCreditApproved;
            target.IsJobAllowedWOCreditCheck = clientSource.IsJobAllowedWOCreditCheck;

            CloneItems(source, target, clientSource);

            return target;
        }
        private void CloneItems(Estimate source, Estimate target, Estimate clientSource)
        {
            if (source.Items == null)
            {
                return;
            }
            if (target.Items == null)
            {
                target.Items = new List<Item>();
            }
            foreach (Item item in source.Items.ToList())
            {
                Item targetItem = itemRepository.Create();
                itemRepository.Add(targetItem);
                target.Items.Add(targetItem);
                item.CloneForOrder(targetItem);
                var targetItemSource = clientSource.Items.FirstOrDefault(x => x.ItemId == item.ItemId);
                if (targetItemSource != null)
                {
                    targetItem.JobSelectedQty = targetItemSource.JobSelectedQty;
                    item.JobSelectedQty = targetItemSource.JobSelectedQty;
                }
                if (targetItem.JobSelectedQty != null && targetItem.ItemType != 2)
                {
                    if (targetItem.JobSelectedQty == 1)
                    {
                        targetItem.Qty1 = item.Qty1;
                        targetItem.Qty2 = 0;
                        targetItem.Qty3 = 0;
                        targetItem.Qty1BaseCharge1 = item.Qty1BaseCharge1;
                        targetItem.Qty1MarkUp1Value = item.Qty1MarkUp1Value;
                        targetItem.Qty1MarkUpPercentageValue = item.Qty1MarkUpPercentageValue;
                        targetItem.Qty1GrossTotal = item.Qty1GrossTotal;
                        targetItem.Qty1NetTotal = item.Qty1NetTotal;
                        
                    }
                    else if (item.JobSelectedQty == 2)
                    {
                        targetItem.Qty1 = item.Qty2;
                        targetItem.Qty2 = 0;
                        targetItem.Qty3 = 0;
                        targetItem.Qty1BaseCharge1 = item.Qty2BaseCharge2;
                        targetItem.Qty1MarkUp1Value = item.Qty2MarkUp2Value;
                        targetItem.Qty1MarkUpPercentageValue = item.Qty2MarkUpPercentageValue;
                        targetItem.Qty1GrossTotal = item.Qty2GrossTotal;
                        targetItem.Qty1NetTotal = item.Qty2NetTotal;
                    }
                    else if (item.JobSelectedQty == 3)
                    {
                        targetItem.Qty1 = item.Qty3;
                        targetItem.Qty2 = 0;
                        targetItem.Qty3 = 0;
                        targetItem.Qty1BaseCharge1 = item.Qty3BaseCharge3;
                        targetItem.Qty1MarkUp1Value = item.Qty3MarkUp3Value;
                        targetItem.Qty1MarkUpPercentageValue = item.Qty3MarkUpPercentageValue;
                        targetItem.Qty1GrossTotal = item.Qty3GrossTotal;
                        targetItem.Qty1NetTotal = item.Qty3NetTotal;
                    }
                }
                target.Estimate_Total = source.Items.ToList().Sum(a => a.Qty1NetTotal);
                // Clone Item Sections
                CloneItemSections(item, targetItem);
            }
        }

        /// <summary>
        /// Copy Item Sections
        /// </summary>
        private void CloneItemSections(Item source, Item target)
        {
            if (source.ItemSections == null)
            {
                return;
            }

            // Initialize List
            if (target.ItemSections == null)
            {
                target.ItemSections = new List<ItemSection>();
            }

            foreach (ItemSection itemSection in source.ItemSections.ToList())
            {
                ItemSection targetItemSection = itemSectionRepository.Create();
                itemSectionRepository.Add(targetItemSection);
                targetItemSection.ItemId = target.ItemId;
                target.ItemSections.Add(targetItemSection);
                itemSection.CloneForOrder(targetItemSection);
                if (source.JobSelectedQty != null && target.ItemType != 2)
                {
                    if (source.JobSelectedQty == 1)
                    {
                        targetItemSection.Qty1 = source.Qty1;
                        targetItemSection.Qty2 = 0;
                        targetItemSection.Qty3 = 0;
                        targetItemSection.Qty1MarkUpID = itemSection.Qty1MarkUpID;
                        targetItemSection.BaseCharge1 = itemSection.BaseCharge1;
                        
                    }
                    else if (source.JobSelectedQty == 2)
                    {
                        targetItemSection.Qty1 = source.Qty2;
                        targetItemSection.Qty2 = 0;
                        targetItemSection.Qty3 = 0;
                        targetItemSection.Qty1MarkUpID = itemSection.Qty2MarkUpID;
                        targetItemSection.BaseCharge1 = itemSection.BaseCharge2;
                    }
                    else if (source.JobSelectedQty == 3)
                    {
                        targetItemSection.Qty1 = source.Qty3;
                        targetItemSection.Qty2 = 0;
                        targetItemSection.Qty3 = 0;
                        targetItemSection.Qty1MarkUpID = itemSection.Qty3MarkUpID;
                        targetItemSection.BaseCharge1 = itemSection.Basecharge3;
                    }
                    
                }
                CloneSectionCostCenter(itemSection, targetItemSection);
            }
        }
        private void CloneSectionCostCenter(ItemSection source, ItemSection target)
        {
            if (source.SectionCostcentres == null)
            {
                return;
            }

            // Initialize List
            if (target.SectionCostcentres == null)
            {
                target.SectionCostcentres = new List<SectionCostcentre>();
            }

            foreach (SectionCostcentre sectionCostcentre in source.SectionCostcentres.ToList())
            {
                SectionCostcentre targetSectionCostcentre = sectionCostCentreRepository.Create();
                sectionCostCentreRepository.Add(targetSectionCostcentre);
                targetSectionCostcentre.ItemSectionId = target.ItemSectionId;
                target.SectionCostcentres.Add(targetSectionCostcentre);
                sectionCostcentre.Clone(targetSectionCostcentre);
                if (source.Item.JobSelectedQty != null && target.Item.ItemType != 2)
                {
                    if (source.Item.JobSelectedQty == 1)
                    {
                        targetSectionCostcentre.Qty1 = source.Qty1;
                        targetSectionCostcentre.Qty1Charge = sectionCostcentre.Qty1Charge;
                        targetSectionCostcentre.Qty1MarkUpID = sectionCostcentre.Qty1MarkUpID;
                        targetSectionCostcentre.Qty1NetTotal = sectionCostcentre.Qty1NetTotal;

                    }
                    else if (source.Item.JobSelectedQty == 2)
                    {
                        targetSectionCostcentre.Qty1 = source.Qty2;
                        targetSectionCostcentre.Qty1Charge = sectionCostcentre.Qty2Charge;
                        targetSectionCostcentre.Qty1MarkUpID = sectionCostcentre.Qty2MarkUpID;
                        targetSectionCostcentre.Qty1NetTotal = sectionCostcentre.Qty2NetTotal;
                    }
                    else if (source.Item.JobSelectedQty == 3)
                    {
                        targetSectionCostcentre.Qty1 = source.Qty3;
                        targetSectionCostcentre.Qty1Charge = sectionCostcentre.Qty3Charge;
                        targetSectionCostcentre.Qty1MarkUpID = sectionCostcentre.Qty3MarkUpID;
                        targetSectionCostcentre.Qty1NetTotal = sectionCostcentre.Qty3NetTotal;
                    }

                }
                else
                {
                    targetSectionCostcentre.Qty1 = source.Qty1;
                    targetSectionCostcentre.Qty2 = 0;
                    targetSectionCostcentre.Qty3 = 0;
                }
               
                CloneSectionCostCenterDetail(sectionCostcentre, targetSectionCostcentre);
            }
        }
        private void CloneSectionCostCenterDetail(SectionCostcentre source, SectionCostcentre target)
        {
            if (source.SectionCostCentreDetails == null)
            {
                return;
            }

            // Initialize List
            if (target.SectionCostCentreDetails == null)
            {
                target.SectionCostCentreDetails = new List<SectionCostCentreDetail>();
            }

            foreach (SectionCostCentreDetail sectionCostCentreDetail in source.SectionCostCentreDetails.ToList())
            {
                SectionCostCentreDetail targetSectionCostCentreDetail = sectionCostCentreDetailRepository.Create();
                sectionCostCentreDetailRepository.Add(targetSectionCostCentreDetail);
                targetSectionCostCentreDetail.SectionCostCentreId = target.SectionCostcentreId;
                target.SectionCostCentreDetails.Add(targetSectionCostCentreDetail);
                sectionCostCentreDetail.Qty1 = source.Qty1;
                sectionCostCentreDetail.Qty2 = 0;
                sectionCostCentreDetail.Qty3 = 0;
                sectionCostCentreDetail.Clone(targetSectionCostCentreDetail);
            }
        }

        #endregion

        #region Download Artwork



        public string DownloadOrderArtwork(int OrderID, string sZipName, long WebStoreOrganisationId = 0)
        {
            //return orderRepository.GenerateOrderArtworkArchive(OrderID, sZipName);
            return GenerateOrderArtworkArchive(OrderID, sZipName, WebStoreOrganisationId);
            // return ExportPDF(105, 0, ReportType.Invoice, 814, string.Empty);
        }

        public string DownloadOrderXml(int orderId, long organisationId)
        {
            return exportReportHelper.ExportOrderReportXML(orderId, "", "0", organisationId);
        }

        public string GenerateOrderArtworkArchive(int OrderID, string sZipName, long WebStoreOrganisationId)
        {

            string ReturnRelativePath = string.Empty;
            string ReturnPhysicalPath = string.Empty;
            string sZipFileName = string.Empty;
            bool IncludeOrderReport = false;
            bool IncludeOrderXML = false;
            bool IncludeJobCardReport = false;
            bool MakeArtWorkProductionReady = false;

            Organisation Organisation = organisationRepository.GetOrganizatiobByID();
            long OrganisationId = 0;
            if (Organisation != null)
            {
                OrganisationId = Organisation.OrganisationId;
            }
            else
            {
                OrganisationId = WebStoreOrganisationId;
            }

            string sCreateDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Artworks/" + OrganisationId);
            bool ArtworkProductionReadyResult = false;

            //string sZipName = string.Format("{0}_{1}_{2}", ((tbl_estimates)EditRecord).Order_Code, ContactCompanyName, BrokerName);

            try
            {
                Estimate oOrder = estimateRepository.GetEstimateWithCompanyByOrderID(OrderID);
                if (sZipName == string.Empty)
                {
                    sZipFileName = GetArchiveFileName(oOrder.Order_Code, oOrder.Company.Name, oOrder.EstimateId);
                }
                else
                {
                    if (Path.HasExtension(sZipName))
                        sZipFileName = sZipName;
                    else
                        sZipFileName = sZipName + ".zip";

                }
                ReturnPhysicalPath = sCreateDirectory + "\\" + sZipFileName;
                if (File.Exists(ReturnPhysicalPath))
                {
                    ReturnPhysicalPath = "/MPC_Content/Artworks/" + OrganisationId + "/" + sZipFileName;
                    return ReturnPhysicalPath;
                }

                //filter the items which are of type delivery i.e. itemtype = 2
                List<Item> ItemsList = itemRepository.GetItemsWithAttachmentsByOrderID(OrderID);

                MakeArtWorkProductionReady = true;

                if (oOrder.Company != null)
                {
                    if (oOrder.Company.IsCustomer == 3)
                    {
                        IncludeOrderReport = oOrder.Company.includeEmailArtworkOrderReport ?? false;
                        IncludeJobCardReport = oOrder.Company.includeEmailArtworkOrderJobCard ?? false;
                        IncludeOrderXML = oOrder.Company.includeEmailArtworkOrderXML ?? false;
                    }
                    else
                    {
                        Company store = companyRepository.GetStoreById(oOrder.Company.StoreId ?? 0);
                        if (store != null)
                        {
                            IncludeOrderReport = store.includeEmailArtworkOrderReport ?? false;
                            IncludeJobCardReport = store.includeEmailArtworkOrderJobCard ?? false;
                            IncludeOrderXML = store.includeEmailArtworkOrderXML ?? false;
                        }
                    }

                }

                if (oOrder.StatusId == 4 || oOrder.StatusId == 5)
                    IncludeJobCardReport = false;
                if (!IncludeOrderReport && !IncludeJobCardReport && !IncludeOrderXML && !MakeArtWorkProductionReady)
                {
                    IncludeOrderReport = true;
                }

                //making the artwork production ready and regenerating template PDFs
                if (MakeArtWorkProductionReady)
                {

                    ArtworkProductionReadyResult = MakeOrderArtworkProductionReady(oOrder, WebStoreOrganisationId);

                }



                //ReturnRelativePath = szDirectory + "/" + PathConstants.DownloadableFilesPath + sZipFileName;

                if (File.Exists(ReturnPhysicalPath))
                {

                    return ReturnPhysicalPath;
                }
                else
                {
                    if (!Directory.Exists(sCreateDirectory))
                    {
                        Directory.CreateDirectory(sCreateDirectory);
                    }

                    string sFileFullPath = string.Empty;
                    string sFolderPath = string.Empty;
                    using (ZipFile zip = new ZipFile())
                    {

                        foreach (Item item in ItemsList)
                        {

                            sFolderPath = sCreateDirectory + "\\" + MakeValidFileName(item.ProductCode ?? "pc101" + "-" + item.ProductName);

                            string ZipfolderName = MakeValidFileName(item.ProductCode ?? "pc101" + "-" + item.ProductName);

                            Directory.CreateDirectory(sFolderPath);

                            ZipEntry d = zip.AddDirectory(sFolderPath, "");

                            // item attachments
                            foreach (var attach in item.ItemAttachments)
                            {
                                //if artwork is production ready then pick the attachments from new location.
                                if (MakeArtWorkProductionReady && ArtworkProductionReadyResult)
                                {
                                    attach.FolderPath = "MPC_Content/Artworks/" + OrganisationId + "/Production/";
                                    //  attach.FolderPath = attach.FolderPath.Replace("Attachments", "Production");
                                }

                                string path = attach.FolderPath + "\\" + attach.FileName + attach.FileType;
                                sFileFullPath = HttpContext.Current.Server.MapPath("~/" + path);
                                //sFileFullPath = sCreateDirectory + "\\" + attach.FolderPath + "\\" + attach.FileName;
                                if (System.IO.File.Exists(sFileFullPath))
                                {
                                    ZipEntry e = zip.AddFile(sFileFullPath, ZipfolderName);
                                    e.Comment = "Created by My Print Cloud";
                                }
                            }

                            //job card report
                            if (IncludeJobCardReport)
                            {
                                string sJCReportPath = exportReportHelper.ExportPDF(165, item.ItemId, ReportType.JobCard, OrderID, string.Empty, WebStoreOrganisationId);
                                if (System.IO.File.Exists(sJCReportPath))
                                {
                                    ZipEntry jcr = zip.AddFile(sJCReportPath, ZipfolderName);
                                    jcr.Comment = "Job Card Report by My Print Cloud";
                                }
                            }

                            //cleanup
                            Directory.Delete(sFolderPath);
                        }

                        //order report
                        if (IncludeOrderReport)
                        {
                            string sOrderReportPath = exportReportHelper.ExportPDF(103, Convert.ToInt64(OrderID), ReportType.Order, OrderID, string.Empty, WebStoreOrganisationId);
                            if (System.IO.File.Exists(sOrderReportPath))
                            {
                                ZipEntry r = zip.AddFile(sOrderReportPath, "");
                                r.Comment = "Order Report by My Print Cloud";
                            }
                        }
                        // here xml comes
                        if (IncludeOrderXML)
                        {
                            string sOrderXMLReportPath = exportReportHelper.ExportOrderReportXML(OrderID, "", "0", WebStoreOrganisationId);
                            if (System.IO.File.Exists(sOrderXMLReportPath))
                            {
                                ZipEntry r = zip.AddFile(sOrderXMLReportPath, "");
                                r.Comment = "Order XML Report by My Print Cloud";
                            }
                        }
                        zip.Comment = "This zip archive was created by My Print Cloud MIS";
                        if (Directory.Exists(sCreateDirectory))
                        {
                            zip.Save(sCreateDirectory + "\\" + sZipFileName);
                        }

                        // DeleteFiles();
                    }
                    ReturnRelativePath = sCreateDirectory;
                    ReturnPhysicalPath = "/MPC_Content/Artworks/" + OrganisationId + "/" + sZipFileName;

                    orderRepository.UpdateItemAttachmentPath(ItemsList);
                    //UpdateAttachmentsPath(oOrder)
                    return ReturnPhysicalPath;
                }
            }
            catch (System.Exception ex1)
            {
                throw ex1;
            }

        }

        private void UpdateAttachmentsPath(Estimate oOrder)
        {
            foreach (var item in oOrder.Items)
            {
                if (item.ItemAttachments != null)
                {
                    item.ItemAttachments.ToList().ForEach(i => i.FolderPath = i.FolderPath.Replace("Attachments", "Production"));
                }
            }
            orderRepository.SaveChanges();
        }

        public static string MakeValidFileName(string name)
        {
            var builder = new StringBuilder();
            var invalid = System.IO.Path.GetInvalidFileNameChars();
            foreach (var cur in name)
            {
                if (!invalid.Contains(cur))
                {
                    builder.Append(cur);
                }
            }
            return builder.ToString();
        }

        public bool MakeOrderArtworkProductionReady(Estimate oOrder, long WebStoreOrganisationId = 0)
        {
            try
            {
                long sOrganisationId = 0;
                if (WebStoreOrganisationId > 0)
                {
                    sOrganisationId = WebStoreOrganisationId;
                }
                else
                {
                    sOrganisationId = organisationRepository.GetOrganizatiobByID().OrganisationId;
                }

                string sOrderID = oOrder.EstimateId.ToString();
                string sProductionFolderPath = "MPC_Content/Artworks/" + sOrganisationId + "/Production";
                string sCustomerID = oOrder.CompanyId.ToString();
                return RegenerateTemplateAttachments(sOrderID, sCustomerID, sProductionFolderPath, oOrder, sOrganisationId);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public bool RegenerateTemplateAttachments(string estimateId, string customerID, string productionFolderPath, Estimate oOrder, long OrganisationId)
        {
            try
            {
                //  Web2Print.BLL.OrderManager orderManager = new Web2Print.BLL.OrderManager();
                //  Web2Print.BLL.ProductManager oProdManager = new Web2Print.BLL.ProductManager();
                long EstimateId = Convert.ToInt64(estimateId);
                long CustomerId = Convert.ToInt64(customerID);
                var Order = oOrder;// GetOrderByID(EstimateId);
                bool isaddcropMark = GetCropMark(CustomerId);

                double bleedsize = organisationRepository.GetBleedSize(OrganisationId);
                bool drawBleedArea = false;
                bool mutlipageMode = false;
                bool hasOverlayPdf = false;
                long StoreId = orderRepository.GetStoreIdByOrderId(EstimateId);
                List<Item> OrderItems = orderRepository.GetOrderItems(EstimateId);
                if (OrderItems != null)
                {
                    
                    foreach (var i in OrderItems)
                    {
                        long TemplateID = i.TemplateId ?? 0;
                        long ItemID = i.ItemId;
                        long CustomerID = Convert.ToInt64(customerID);

                        if (i.TemplateId > 0) // case of templates
                        {
                            var Item = itemRepository.GetItemWithSections(ItemID);
                            if (i.isMultipagePDF == true)
                            {
                                mutlipageMode = true;
                            }
                            if (i.drawBleedArea == true)
                            {
                                drawBleedArea = true;
                            }
                            if (i.printCropMarks == true)
                            {
                                isaddcropMark = true;
                            }

                            templateService.regeneratePDFs(TemplateID, OrganisationId, isaddcropMark, mutlipageMode, drawBleedArea, bleedsize);

                            //orderRepository.regeneratePDFs(TemplateID, OrganisationId, isaddcropMark, mutlipageMode, drawBleedArea, bleedsize);
                            //LocalTemplateDesigner.TemplateSvcSPClient oLocSvc = new LocalTemplateDesigner.TemplateSvcSPClient();b
                            //oLocSvc.regeneratePDFs(TemplateID, isaddcropMark, drawBleedArea, mutlipageMode);



                            List<TemplatePage> oPages = new List<TemplatePage>();

                            oPages = templatePageRepository.GetTemplatePages(TemplateID);
                            //List<TemplateDesignerModelTypesV2.TemplatePages> oPages = null;
                            //using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                            //{
                            //    db.ContextOptions.LazyLoadingEnabled = false;
                            //    oPages = db.TemplatePages.Where(g => g.ProductID == TemplateID).ToList();
                            //}

                            List<ArtWorkAttatchment> oLstAttachments = itemAttachmentRepository.GetItemAttactchmentsForRegenerateTemplateAttachments(ItemID, ".pdf", UploadFileTypes.Artwork);

                            // string DesignerPath = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Templates/");
                            string DesignerPath = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationId + "/Templates/");

                            if (oLstAttachments.Count == 0)  //no attachments already exist, hence a new entry in attachments is required
                            {

                                //special working for attaching the PDF
                                List<ArtWorkAttatchment> uplodedArtWorkList = new List<ArtWorkAttatchment>();
                                ArtWorkAttatchment attatcment = null;
                                string folderPath = "mpc_content/Attachments/" + OrganisationId + "/" + StoreId + "/Products/" + ItemID; //"MPC_Content/Attachments/" + OrganisationId;
                                string virtualFolderPth = System.Web.HttpContext.Current.Server.MapPath("~/" + folderPath);
                                string VirtualFolderPath2 = System.Web.HttpContext.Current.Server.MapPath("~/" + productionFolderPath);


                                if (!System.IO.Directory.Exists(virtualFolderPth))
                                    System.IO.Directory.CreateDirectory(virtualFolderPth);

                                if (!System.IO.Directory.Exists(VirtualFolderPath2))
                                    System.IO.Directory.CreateDirectory(VirtualFolderPath2);

                                if (Item.isMultipagePDF == true)
                                {
                                    string fileName = GetAttachmentFileName(i.ProductCode, Order.Order_Code, i.ItemCode, "Side1", virtualFolderPth, ".pdf", Order.CreationDate ?? DateTime.Now);
                                    string overlayName = GetAttachmentFileName(i.ProductCode, Order.Order_Code, i.ItemCode, "Side1overlay", virtualFolderPth, ".pdf", Order.CreationDate ?? DateTime.Now);

                                    string fileCompleteAddress = System.IO.Path.Combine(virtualFolderPth, fileName);
                                    string fileCompleteAddress2 = System.IO.Path.Combine(VirtualFolderPath2, fileName);

                                    string overlayCompleteAddress = System.IO.Path.Combine(virtualFolderPth, overlayName);
                                    string overlayCompleteAddress2 = System.IO.Path.Combine(VirtualFolderPath2, overlayName);

                                    //copying file from original location to attachments location

                                    System.IO.File.Copy(DesignerPath + TemplateID.ToString() + "/pages.pdf", fileCompleteAddress, true);
                                    System.IO.File.Copy(DesignerPath + TemplateID.ToString() + "/pages.pdf", fileCompleteAddress2, true);
                                    foreach (var page in oPages)
                                    {
                                        if (page.hasOverlayObjects == true)
                                            hasOverlayPdf = true;
                                    }
                                    if (hasOverlayPdf)
                                    {
                                        System.IO.File.Copy(DesignerPath + TemplateID.ToString() + "/pagesoverlay.pdf", overlayCompleteAddress, true);
                                        System.IO.File.Copy(DesignerPath + TemplateID.ToString() + "/pagesoverlay.pdf", overlayCompleteAddress2, true);
                                        attatcment = new ArtWorkAttatchment();
                                        attatcment.FileName = overlayName;
                                        attatcment.FileExtention = ".pdf";
                                        attatcment.FolderPath = folderPath;
                                        attatcment.FileTitle = "Side1overlay";
                                        uplodedArtWorkList.Add(attatcment);
                                    }

                                    //System.IO.File.WriteAllBytes(fileCompleteAddress, PDFSide1HighRes);
                                    string ThumbnailPath = fileCompleteAddress;

                                    attatcment = new ArtWorkAttatchment();
                                    attatcment.FileName = fileName;
                                    attatcment.FileExtention = ".pdf";
                                    attatcment.FolderPath = folderPath;
                                    attatcment.FileTitle = "Side1";
                                    uplodedArtWorkList.Add(attatcment);
                                }
                                else
                                {
                                    if (Item.isMultipagePDF == true)
                                    {
                                        foreach (var page in oPages)
                                        {
                                            if (page.hasOverlayObjects == true)
                                                hasOverlayPdf = true;
                                        }
                                        string fileName = GetAttachmentFileName(i.ProductCode, Order.Order_Code, i.ItemCode, "Side1", virtualFolderPth, ".pdf", Order.CreationDate ?? DateTime.Now);
                                        string overlayName = GetAttachmentFileName(i.ProductCode, Order.Order_Code, i.ItemCode, "Side1overlay", virtualFolderPth, ".pdf", Order.CreationDate ?? DateTime.Now);

                                        string fileCompleteAddress = System.IO.Path.Combine(virtualFolderPth, fileName);
                                        string fileCompleteAddress2 = System.IO.Path.Combine(VirtualFolderPath2, fileName);

                                        string overlayCompleteAddress = System.IO.Path.Combine(virtualFolderPth, overlayName);
                                        string overlayCompleteAddress2 = System.IO.Path.Combine(VirtualFolderPath2, overlayName);

                                        //copying file from original location to attachments location
                                        System.IO.File.Copy(DesignerPath + TemplateID.ToString() + "/pages.pdf", fileCompleteAddress, true);
                                        System.IO.File.Copy(DesignerPath + TemplateID.ToString() + "/pages.pdf", fileCompleteAddress2, true);

                                        if (hasOverlayPdf)
                                        {
                                            System.IO.File.Copy(DesignerPath + TemplateID.ToString() + "/pagesoverlay.pdf", overlayCompleteAddress, true);
                                            System.IO.File.Copy(DesignerPath + TemplateID.ToString() + "/pagesoverlay.pdf", overlayCompleteAddress2, true);
                                            attatcment = new ArtWorkAttatchment();
                                            attatcment.FileName = overlayName;
                                            attatcment.FileExtention = ".pdf";
                                            attatcment.FolderPath = folderPath;
                                            attatcment.FileTitle = "Side1overlay";
                                            uplodedArtWorkList.Add(attatcment);
                                        }

                                        //System.IO.File.WriteAllBytes(fileCompleteAddress, PDFSide1HighRes);
                                        string ThumbnailPath = fileCompleteAddress;

                                        attatcment = new ArtWorkAttatchment();
                                        attatcment.FileName = fileName;
                                        attatcment.FileExtention = ".pdf";
                                        attatcment.FolderPath = folderPath;
                                        attatcment.FileTitle = "Side1";
                                        uplodedArtWorkList.Add(attatcment);
                                    }
                                    else
                                    {
                                        foreach (var item in oPages)
                                        {
                                            //saving Page1  or Side 1 
                                            //string fileName = ItemID.ToString() + " Side" + item.PageNo + ".pdf";

                                            string fileName = GetAttachmentFileName(i.ProductCode, Order.Order_Code, i.ItemCode, "Side" + item.PageNo.ToString(), virtualFolderPth, ".pdf", Order.CreationDate ?? DateTime.Now);
                                            string overlayName = GetAttachmentFileName(i.ProductCode, Order.Order_Code, i.ItemCode, "Side" + item.PageNo.ToString() + "overlay", virtualFolderPth, ".pdf", Order.CreationDate ?? DateTime.Now);

                                            string fileCompleteAddress = System.IO.Path.Combine(virtualFolderPth, fileName);
                                            string fileCompleteAddress2 = System.IO.Path.Combine(VirtualFolderPath2, fileName);

                                            string overlayCompleteAddress = System.IO.Path.Combine(virtualFolderPth, overlayName);
                                            string overlayCompleteAddress2 = System.IO.Path.Combine(VirtualFolderPath2, overlayName);

                                            //copying file from original location to attachments location
                                            if (File.Exists(DesignerPath + item.ProductId.ToString() + "/p" + item.PageNo + ".pdf"))
                                            {
                                                System.IO.File.Copy(DesignerPath + item.ProductId.ToString() + "/p" + item.PageNo + ".pdf", fileCompleteAddress, true);
                                                System.IO.File.Copy(DesignerPath + item.ProductId.ToString() + "/p" + item.PageNo + ".pdf", fileCompleteAddress2, true);
                                            }


                                            if (item.hasOverlayObjects == true)
                                            {
                                                if (File.Exists(DesignerPath + item.ProductId.ToString() + "/p" + item.PageNo + "overlay.pdf"))
                                                {
                                                    System.IO.File.Copy(DesignerPath + item.ProductId.ToString() + "/p" + item.PageNo + "overlay.pdf", overlayCompleteAddress, true);
                                                    System.IO.File.Copy(DesignerPath + item.ProductId.ToString() + "/p" + item.PageNo + "overlay.pdf", overlayCompleteAddress2, true);
                                                }
                                                attatcment = new ArtWorkAttatchment();
                                                attatcment.FileName = overlayName;
                                                attatcment.FileExtention = ".pdf";
                                                attatcment.FolderPath = folderPath;
                                                attatcment.FileTitle = "Side" + item.PageNo.ToString() + "overlay";
                                                uplodedArtWorkList.Add(attatcment);
                                            }

                                            //System.IO.File.WriteAllBytes(fileCompleteAddress, PDFSide1HighRes);
                                            string ThumbnailPath = fileCompleteAddress;

                                            attatcment = new ArtWorkAttatchment();
                                            attatcment.FileName = fileName;
                                            attatcment.FileExtention = ".pdf";
                                            attatcment.FolderPath = folderPath;
                                            attatcment.FileTitle = "Side" + item.PageNo.ToString();
                                            uplodedArtWorkList.Add(attatcment);
                                            //ProductManager.GenerateThumbnailForPdf(ThumbnailPath, true);
                                        }
                                    }

                                }
                                //creating the attachment the attachment for the first time.
                                bool result = orderRepository.CreateUploadYourArtWork(ItemID, CustomerID, uplodedArtWorkList);


                                //updating the item with templateID /design
                                itemRepository.UpdateItem(ItemID, TemplateID);

                            }
                            else// attachment alredy exists hence we need to updat the existing artwork.
                            {
                                string folderPath = "mpc_content/Attachments/" + OrganisationId + "/" + StoreId + "/Products/" + ItemID; 

                                string virtualFolderPth = System.Web.HttpContext.Current.Server.MapPath("~/" + folderPath);
                                string VirtualFolderPath2 = System.Web.HttpContext.Current.Server.MapPath("~/" + productionFolderPath);


                                if (!System.IO.Directory.Exists(VirtualFolderPath2))
                                {
                                    System.IO.Directory.CreateDirectory(VirtualFolderPath2);
                                }
                                if (Item.isMultipagePDF == true)
                                {

                                    List<ArtWorkAttatchment> oPage1Attachment = oLstAttachments;
                                    foreach (var page in oPages)
                                    {
                                        if (page.hasOverlayObjects == true)
                                            hasOverlayPdf = true;
                                    }

                                    //  index = index + 1;
                                    //ArtWorkAttatchment oPage1Attachment = oLstAttachments.Where(g => g.FileTitle == oPage.PageName).Single();

                                    foreach (var attachment in oPage1Attachment)
                                    {
                                        if (attachment != null)
                                        {
                                            string fileName = attachment.FileName + attachment.FileExtention;
                                            string fileCompleteAddress = System.IO.Path.Combine(virtualFolderPth, fileName);
                                            string fileCompleteAddress2 = System.IO.Path.Combine(VirtualFolderPath2, fileName);
                                            string sourcePath = DesignerPath + TemplateID + "/pages.pdf";

                                            if (fileName.Contains("overlay"))
                                            {
                                                sourcePath = DesignerPath + TemplateID.ToString() + "/pagesoverlay.pdf";

                                            }

                                            //System.IO.File.Copy(fileCompleteAddress, fileCompleteAddress2);
                                            if (File.Exists(sourcePath))
                                            {
                                                System.IO.File.Copy(sourcePath, fileCompleteAddress, true);
                                                System.IO.File.Copy(sourcePath, fileCompleteAddress2, true);
                                            }



                                            //if (hasOverlayPdf == true)
                                            //{
                                            //    //  oPage1Attachment = oLstAttachments[0];

                                            //    if (oPage1Attachment != null)
                                            //    {
                                            //        fileName = attachment.FileName;
                                            //        fileCompleteAddress = System.IO.Path.Combine(virtualFolderPth, fileName);
                                            //        fileCompleteAddress2 = System.IO.Path.Combine(VirtualFolderPath2, fileName);
                                            //        sourcePath = DesignerPath + oPage.ProductId.ToString() + "/p" + oPage.PageNo + ".pdf";

                                            //        if (fileName.Contains("overlay"))
                                            //        {
                                            //            sourcePath = DesignerPath + oPage.ProductId.ToString() + "/p" + oPage.PageNo + "overlay.pdf";

                                            //        }

                                            //        //System.IO.File.Copy(fileCompleteAddress, fileCompleteAddress2);
                                            //        if (File.Exists(sourcePath))
                                            //        {
                                            //            System.IO.File.Copy(sourcePath, fileCompleteAddress, true);
                                            //            System.IO.File.Copy(sourcePath, fileCompleteAddress2, true);
                                            //        }
                                            //    }
                                            //}
                                            //System.IO.File.WriteAllBytes(fileCompleteAddress, PDFSide1HighRes);
                                            //string ThumbnailPath = fileCompleteAddress;
                                            //System.IO.File.WriteAllBytes( System.Web.HttpContext.Current.Server.MapPath(  System.IO.Path.Combine(Web2Print.UI.Common.Utils.GetAppBasePath() +  oPage1Attachment.FolderPath, oPage1Attachment.FileName)), PDFSide1HighRes);
                                            //ProductManager.GenerateThumbnailForPdf(ThumbnailPath, true);
                                        }
                                    }



                                }
                                else
                                {
                                    int index = 0;
                                    foreach (var oPage in oPages)
                                    {
                                        ArtWorkAttatchment oPage1Attachment = oLstAttachments[index];
                                        index = index + 1;
                                        //ArtWorkAttatchment oPage1Attachment = oLstAttachments.Where(g => g.FileTitle == oPage.PageName).Single();
                                        if (oPage1Attachment != null)
                                        {
                                            string fileName = oPage1Attachment.FileName + oPage1Attachment.FileExtention;
                                            string fileCompleteAddress = System.IO.Path.Combine(virtualFolderPth, fileName);
                                            string fileCompleteAddress2 = System.IO.Path.Combine(VirtualFolderPath2, fileName);
                                            string sourcePath = DesignerPath + oPage.ProductId.ToString() + "/p" + oPage.PageNo + ".pdf";

                                            if (fileName.Contains("overlay"))
                                            {
                                                sourcePath = DesignerPath + oPage.ProductId.ToString() + "/p" + oPage.PageNo + "overlay.pdf";

                                            }

                                            //System.IO.File.Copy(fileCompleteAddress, fileCompleteAddress2);
                                            if (File.Exists(sourcePath))
                                            {
                                                System.IO.File.Copy(sourcePath, fileCompleteAddress, true);
                                                System.IO.File.Copy(sourcePath, fileCompleteAddress2, true);
                                            }



                                            if (oPage.hasOverlayObjects == true)
                                            {
                                                oPage1Attachment = oLstAttachments[index];
                                                index = index + 1;
                                                if (oPage1Attachment != null)
                                                {
                                                    fileName = oPage1Attachment.FileName;
                                                    fileCompleteAddress = System.IO.Path.Combine(virtualFolderPth, fileName);
                                                    fileCompleteAddress2 = System.IO.Path.Combine(VirtualFolderPath2, fileName);
                                                    sourcePath = DesignerPath + oPage.ProductId.ToString() + "/p" + oPage.PageNo + ".pdf";

                                                    if (fileName.Contains("overlay"))
                                                    {
                                                        sourcePath = DesignerPath + oPage.ProductId.ToString() + "/p" + oPage.PageNo + "overlay.pdf";

                                                    }

                                                    //System.IO.File.Copy(fileCompleteAddress, fileCompleteAddress2);
                                                    if (File.Exists(sourcePath))
                                                    {
                                                        System.IO.File.Copy(sourcePath, fileCompleteAddress, true);
                                                        System.IO.File.Copy(sourcePath, fileCompleteAddress2, true);
                                                    }
                                                }
                                            }
                                            //System.IO.File.WriteAllBytes(fileCompleteAddress, PDFSide1HighRes);
                                            //string ThumbnailPath = fileCompleteAddress;
                                            //System.IO.File.WriteAllBytes( System.Web.HttpContext.Current.Server.MapPath(  System.IO.Path.Combine(Web2Print.UI.Common.Utils.GetAppBasePath() +  oPage1Attachment.FolderPath, oPage1Attachment.FileName)), PDFSide1HighRes);
                                            //ProductManager.GenerateThumbnailForPdf(ThumbnailPath, true);
                                        }

                                    }
                                }



                            }
                        }
                        else // case of uplaod images
                        {
                            List<ItemAttachment> ListOfAttachments = itemAttachmentRepository.GetItemAttactchments(ItemID);

                            string folderPath = "mpc_content/Attachments/" + OrganisationId + "/" + StoreId + "/Products/" + ItemID; // Web2Print.UI.Components.ImagePathConstants.ProductImagesPath + "Attachments/";
                            string virtualFolderPth = System.Web.HttpContext.Current.Server.MapPath("~/" + productionFolderPath);
                            string fileSourcePath = System.Web.HttpContext.Current.Server.MapPath("~/" + folderPath);

                            if (!System.IO.Directory.Exists(virtualFolderPth))
                            {
                                System.IO.Directory.CreateDirectory(virtualFolderPth);
                            }
                            if (!System.IO.Directory.Exists(fileSourcePath))
                            {
                                System.IO.Directory.CreateDirectory(fileSourcePath);
                            }

                            foreach (var oPage in ListOfAttachments)
                            {
                                string fileName = oPage.FileName;
                                string fileCompleteAddress = System.IO.Path.Combine(virtualFolderPth, fileName + oPage.FileType);
                                string sourceFileAdd = System.IO.Path.Combine(fileSourcePath, fileName + oPage.FileType);
                                System.IO.File.Copy(sourceFileAdd, fileCompleteAddress, true);

                            }
                        }
                    }
                }
                else
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                //return false;
                throw ex;
            }
        }

        public bool GetCropMark(long CustomerID)
        {


            var Rec = companyRepository.GetStoreById(CustomerID);


            if (Rec != null)
            {
                return Rec.isAddCropMarks ?? false;
            }
            else
            {
                return false;
            }

        }

        public string GetAttachmentFileName(string ProductCode, string OrderCode, string ItemCode, string SideCode, string VirtualFolderPath, string extension, DateTime OrderCreationDate)
        {
            string FileName = OrderCreationDate.Year.ToString() + OrderCreationDate.ToString("MMMM") + OrderCreationDate.Day.ToString() + "-" + ProductCode + "-" + OrderCode + "-" + ItemCode + "-" + SideCode + extension;
            //checking whether file exists or not
            while (System.IO.File.Exists(VirtualFolderPath + FileName))
            {
                string fileName1 = System.IO.Path.GetFileNameWithoutExtension(FileName);
                fileName1 += "a";
                FileName = fileName1 + extension;
            }

            return FileName;
        }

        public static string GetArchiveFileName(string OrderCode, string CustomerName, Int64 OrderID)
        {
            string FileName = DateTime.Now.Year.ToString() + "-" + OrderID + "-" + OrderCode + "-" + CustomerName.Replace("&", "").Trim() + ".zip";

            return FileName;
        }

        public List<Item> GetOrderItems(long EstimateId)
        {
            return orderRepository.GetOrderItems(EstimateId);
        } 


        #endregion


        /// <summary>
        /// Download Attachment
        /// </summary>
        public string DownloadAttachment(long id, out string fileName, out string fileTpe)
        {
            string mpcContentPath = ConfigurationManager.AppSettings["MPC_Content"];
            HttpServerUtility server = HttpContext.Current.Server;
            ItemAttachment attachment = itemAttachmentRepository.Find(id);
            fileName = attachment.FileName;
            fileTpe = attachment.FileType;

            //string mapPath1 = server.MapPath(mpcContentPath + "/Attachments/" + orderRepository.OrganisationId + "/" + attachment.CompanyId + "/Products/");
            string mapPath = server.MapPath("~/" + attachment.FolderPath + "/");
            string attachmentMapPath = mapPath + attachment.FileName + attachment.FileType;
            return attachmentMapPath;
        }

        public string DownloadInquiryAttachment(long id, out string fileName, out string fileTpe)
        {
            string mpcContentPath = ConfigurationManager.AppSettings["MPC_Content"];
            HttpServerUtility server = HttpContext.Current.Server;
            InquiryAttachment attachment = inquiryAttachmentRepository.Find(id);
            fileName = attachment.OrignalFileName;
            fileTpe = attachment.Extension;

            //string mapPath1 = server.MapPath(mpcContentPath + "/Attachments/" + orderRepository.OrganisationId + "/" + attachment.CompanyId + "/Products/");
            string mapPath = server.MapPath("~/" + attachment.AttachmentPath + "/");
            string attachmentMapPath = mapPath + attachment.OrignalFileName;
            return attachmentMapPath;
        }

        public Estimate CloneEstimate(long estimateId)
        {
            var source = GetById(estimateId);
            Estimate target = CreateNewOrder(true);
            var code = target.Estimate_Code;
            target.isEstimate = true;
            target.StatusId = source.StatusId;

            target = UpdateEstimeteOnCloning(source, target, source);
            target.Estimate_Code = code;
            target.CreationDate = DateTime.Now;
            target.Order_Date = DateTime.Now;
            target.RefEstimateId = null;
            target.StatusId = 1;
            target.Estimate_Name = target.Estimate_Name + " copy";
            estimateRepository.SaveChanges();

            Estimate estimate = GetById(target.EstimateId);
            // Load Properties
            estimateRepository.LoadProperty(estimate, () => estimate.Status);
            estimateRepository.LoadProperty(estimate, () => estimate.Company);
            return estimate;
        }

        public Estimate CloneOrder(long estimateId)
        {
            var source = GetById(estimateId);
            Estimate target = CreateNewOrder();
            var code = target.Order_Code;
            target.isEstimate = false;
            target = UpdateEstimeteOnCloning(source, target, source);
            target.Order_Code = code;
            target.CreationDate = DateTime.Now;
            target.Order_Date = DateTime.Now;
            target.RefEstimateId = null;
            target.StatusId = (int)OrderStatus.PendingOrder;
            target.Estimate_Name = target.Estimate_Name + " copy";
            estimateRepository.SaveChanges();

            Estimate estimate = GetById(target.EstimateId);
            // Load Properties
            estimateRepository.LoadProperty(estimate, () => estimate.Status);
            estimateRepository.LoadProperty(estimate, () => estimate.Company);
            return estimate;
        }

        #region Unleashed Xero Integration
        public bool PostOrderToXero(long orderID)
        {

            Organisation org = organisationRepository.GetOrganizatiobByID();
            bool key = org.isXeroIntegrationRequired ?? false;

            if (key)
            {
                string CustomerGuid = string.Empty;

                try
                {
                    Estimate order = estimateRepository.Find(orderID);
                    Company customer = order.Company;
                    List<Item> products = order.Items.ToList();

                    Item curItem = products.FirstOrDefault();
                    double totaltax = (products.Sum(c => c.Qty1GrossTotal??0) - products.Sum(c => c.Qty1NetTotal??0));
                    double taxrate = 0;
                    if (customer.IsCustomer == 3)
                        taxrate = customer.TaxRate ?? 0;
                    else
                    {
                        var store = companyRepository.GetCustomer(Convert.ToInt32(customer.StoreId ?? 0));
                        if (store != null)
                            taxrate = store.TaxRate ?? 0;
                    }
                    List<Company> suppliers = new List<Company>();

                   //// XeroAPI api = new XeroAPI();
                    string apiID = org.XeroApiId;
                    string apiKey = org.XeroApiKey;

                    foreach (var product in products)
                    {
                        var item = itemRepository.GetItemById(product.RefItemId?? 0);
                        if (item != null)
                        {
                            var supplier = companyRepository.GetCustomer(item.SupplierId ?? 0);
                            if (supplier != null)
                            {
                                suppliers.Add(supplier);
                                product.SupplierId = Convert.ToInt32(supplier.CompanyId);
                            }
                                
                        }
                        
                    }
                    
                    if (string.IsNullOrEmpty(customer.XeroAccessCode))
                    {
                        //Add Customer to Xero
                        Guid newGuid = Guid.NewGuid();

                        try
                        {
                            string PostData = CustomerXmlData(newGuid, customer);
                            PostXml("Customers", apiID, apiKey, newGuid.ToString(), PostData);
                            //api.AddCustomerXml(customer, apiID, apiKey, newGuid);
                        }
                        catch (Exception)
                        {

                            return false;
                        }
                        customer.XeroAccessCode = newGuid.ToString();
                        CustomerGuid = newGuid.ToString();
                        companyRepository.Update(customer);
                        companyRepository.SaveChanges();
                    }


                    if (suppliers.Count > 0)
                    {
                        foreach (Company supplier in suppliers)
                        {
                            if (string.IsNullOrEmpty(supplier.XeroAccessCode))
                            {
                                //Add Supplier
                                Guid newGuid = Guid.NewGuid();

                                try
                                {
                                    string PostData = CustomerXmlData(newGuid, customer);
                                    PostXml("Customers", apiID, apiKey, newGuid.ToString(), PostData);
                                    // api.AddCustomerXml(supplier, apiID, apiKey, newGuid);
                                }
                                catch (Exception)
                                {

                                    return false;
                                }
                                supplier.XeroAccessCode = newGuid.ToString();
                                companyRepository.Update(supplier);
                            }
                        }
                        companyRepository.SaveChanges();
                    }

                    //update xero code from referral item
                    foreach (Item item in products)
                    {
                        var refItem = itemRepository.Find(item.RefItemId ?? 0);
                        string xeroCode = refItem != null ? refItem.XeroAccessCode : string.Empty;
                        if (!string.IsNullOrEmpty(xeroCode))
                        {
                            item.XeroAccessCode = xeroCode;
                        }
                    }


                    foreach (Item item in products)
                    {
                        //Add product
                        if (string.IsNullOrEmpty(item.XeroAccessCode))
                        {
                            Guid newGuid = Guid.NewGuid();
                            string supplierCode = "";
                            if (item.SupplierId != null)
                            {
                                supplierCode = companyRepository.GetCustomer(item.SupplierId ?? 0).XeroAccessCode;
                                
                            }
                           

                            XeroXmlRefData refData = new XeroXmlRefData();
                            refData.createdBy = "";
                            if (item.CreatedBy.HasValue)
                            {
                                refData.createdBy = systemUserRepository.GetUserrById(order.Created_by?? new Guid()).FullName;
                            }


                            Item refItem = itemRepository.Find(item.RefItemId ?? 0);
                            
                            CostCentre costcentre = new CostCentre();
                            StockItem stockitem = new StockItem();
                            string CostCenterID = string.Empty;
                            string StockID = string.Empty;
                            if (item.ItemType == 2)
                            {
                                refData.productPurchasePrice = "1";
                                refData.productSellPrice = CalculateSalesPrice(item, order.isDirectSale.Value);
                                refData.productHeight = "0";
                                refData.productWidth = "0";
                                refData.packSize = "0";
                                refData.reOrderPoint = "0";
                               // costcentre = this.ObjectContext.tbl_costcentres.Where(c => c.Name == item.ProductName).FirstOrDefault();


                                CostCenterID = Convert.ToString(CostCentreRepository.GetCostCentreIdByName(item.ProductName));

                            }
                            else if (item.ItemType == 3)
                            {

                                refData.productPurchasePrice = CalculatePurchasePrice(item, order.isDirectSale.Value);
                                refData.productSellPrice = CalculateSalesPrice(item, order.isDirectSale.Value);
                                refData.productHeight = CalculateHeight(item);
                                refData.productWidth = CalculateWeight(item);
                                refData.packSize = CalculatePackSize(item);
                                refData.reOrderPoint = CalculateReorderLevel(item);
                                stockitem = stockItemRepository.Find(item.RefItemId ?? 0);// this.ObjectContext.tbl_stockitems.Where(s => s.StockItemID == item.RefItemID).FirstOrDefault();
                                if (stockitem != null)
                                {
                                    StockID = Convert.ToString(stockitem.StockItemId);
                                }



                            }
                            else
                            {
                                refData.productPurchasePrice = CalculatePurchasePrice(refItem, order.isDirectSale.Value);
                                refData.productSellPrice = CalculateSalesPrice(item, order.isDirectSale.Value);
                                refData.productHeight = CalculateHeight(item);
                                refData.productWidth = CalculateWeight(item);
                                refData.packSize = CalculatePackSize(item);
                                refData.reOrderPoint = CalculateReorderLevel(item);
                            }

                            try
                            {
                                string postData = ProductXmlData(newGuid, item, supplierCode, refData, CostCenterID, StockID);
                                ////api.AddProductXml(item, supplierCode, refData, apiID, apiKey, newGuid, CostCenterID, StockID);
                                PostXml("Products", apiID, apiKey, newGuid.ToString(), postData);
                            }
                            catch (Exception)
                            {

                                return false;
                            }


                            item.XeroAccessCode = newGuid.ToString();
                            if (item.ItemType == 2)
                                costcentre.XeroAccessCode = newGuid.ToString();
                            else if (item.ItemType == 3)
                                stockitem.XeroAccessCode = newGuid.ToString();
                            else
                                refItem.XeroAccessCode = newGuid.ToString();
                            
                            itemRepository.Update(item);
                        }

                        
                    }
                    itemRepository.SaveChanges();
                    // Add Order


                    if (string.IsNullOrEmpty(order.XeroAccessCode))
                    {
                        Guid orderGuid = Guid.NewGuid();
                        try
                        {
                            string postData = SaleOrderXmlData(orderGuid, order, customer, products, taxrate);
                            PostXml("SalesOrders", apiID, apiKey, orderGuid.ToString(), postData);
                        }
                        catch (Exception)
                        {

                            return false;
                        }

                        order.XeroAccessCode = orderGuid.ToString();
                        estimateRepository.Update(order);
                        estimateRepository.SaveChanges();
                    }

                    Guid invoiceGuid = Guid.NewGuid();
                    try
                    {
                        string postData = InvoiceXmlData(invoiceGuid, order, customer, taxrate, totaltax, customer.XeroAccessCode, products);
                        PostXml("SalesInvoices", apiID, apiKey, invoiceGuid.ToString(), postData);

                    }
                    catch (Exception)
                    {

                        return false;
                    }




                    return true;
                }
                catch (Exception)
                {

                    return false;
                }

            }
            else
            {
                return false;
            }

        }


        private string CustomerXmlData(Guid gid, Company companyObject)
        {
            string xml = @"<?xml version='1.0'?>
				<Customer xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns='http://api.unleashedsoftware.com/version/1'>
				<Guid>{0}</Guid>
				<CustomerCode>{1}</CustomerCode>
				<CustomerName>{2}</CustomerName>
				<CustomerType>{3}</CustomerType>
				<Email>{4}</Email>
				<EmailCC>{4}</EmailCC>
				<FaxNumber>{5}</FaxNumber>
				<GSTVATNumber>{6}</GSTVATNumber>
				<MobileNumber>{7}</MobileNumber>
				<Notes>{8}</Notes>
				<PhoneNumber>{9}</PhoneNumber>

				<Obsolete>{10}</Obsolete>

				<StopCredit>0</StopCredit>
				<Taxable>{11}</Taxable>
				

				<PrintInvoice>1</PrintInvoice>
				<PrintPackingSlipInsteadOfInvoice>0</PrintPackingSlipInsteadOfInvoice>

				<ContactFirstName>{12}</ContactFirstName>
				<ContactLastName>{13}</ContactLastName>

				<XeroContactId>1</XeroContactId>
				<XeroCostOfGoodsAccount>1</XeroCostOfGoodsAccount>
				<XeroSalesAccount>1</XeroSalesAccount>

				</Customer>";

            xml = xml.Replace("\r\n", "");
            string customerType = "";
            switch (companyObject.IsCustomer)
            {
                case 0:
                    customerType = "Prospect";
                    break;
                case 1:
                    customerType = "Retail";
                    break;
                case 2:
                    customerType = "Supplier";
                    break;
                case 3:
                    customerType = "Corporate";
                    break;
                case 4:
                    customerType = "Broker";
                    break;
            }
            string isObselete = "0";
            if (companyObject.isArchived.HasValue && companyObject.isArchived.Value == true)
            {
                isObselete = "1";
            }
            string isTaxable = "0";
            if (companyObject.isIncludeVAT.HasValue && companyObject.isIncludeVAT.Value == true)
            {
                isTaxable = "1";
            }
            string vatReference = "";
            if (!string.IsNullOrEmpty(companyObject.VATRegReference))
            {
                vatReference = companyObject.VATRegReference;
            }

            CompanyContact contact = companyObject.CompanyContacts.Where(con => con.IsDefaultContact == 1).FirstOrDefault();
            xml = string.Format(xml, gid, gid + " - " + companyObject.AccountNumber, companyObject.Name, customerType, contact.Email, contact.FAX, vatReference,
                contact.Mobile, companyObject.Notes, contact.HomeTel1, isObselete, isTaxable,
                   contact.FirstName, contact.LastName);
            xml = xml.Replace("&", "and");

            return xml;
        }
        private string ProductXmlData(Guid id, Item product, string supplierCode, XeroXmlRefData refData, string CostCenterID, string StockID)
        {
            string xml = @"<?xml version='1.0'?>
                    <Product xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns='http://api.unleashedsoftware.com/version/1'>
                    <Guid>#productID</Guid>
                    <ProductCode>#productCode</ProductCode>
                    <ProductDescription>#description</ProductDescription>  

                    <CanAutoAssemble>0</CanAutoAssemble>
                    <IsAssembledProduct>#isFinishGood</IsAssembledProduct>  
                   

                    <DefaultPurchasePrice>#purchasePrice</DefaultPurchasePrice>  
                    <DefaultSellPrice>#sellPrice</DefaultSellPrice>


                    <Height>#height</Height>
                    <Width>#width</Width>



                    <NeverDiminishing>1</NeverDiminishing>


                    <Obsolete>0</Obsolete>

                    <PackSize>#packSize</PackSize>
                    <ReOrderPoint>#reorderPoint</ReOrderPoint>


                    <Taxable>0</Taxable>
                    <TaxableSales>1</TaxableSales>

                    <XeroSalesAccount>1</XeroSalesAccount>";

            xml += "</Product>";
            xml = xml.Replace("\r\n", "");
            xml = xml.Replace("#productID", id.ToString());

            if (product.ItemType == 2)
            {
                xml = xml.Replace("#productCode", CostCenterID + "-CostCentre");

            }
            else if (product.ItemType == 3)
            {
                xml = xml.Replace("#productCode", StockID + "-StockItem");

            }
            else
            {
                xml = xml.Replace("#productCode", product.ProductCode);
            }

            if (!string.IsNullOrEmpty(product.WebDescription))
            {
                if (product.WebDescription.Length < 150)
                    xml = xml.Replace("#description", product.WebDescription);
                else
                    xml = xml.Replace("#description", product.WebDescription.Substring(0, 150));
            }
            else
            {
                xml = xml.Replace("#description", product.ProductName);
            }



            if (product.ProductType != null && product.ProductType != 1)
            {
                xml = xml.Replace("#isFinishGood", "1");
            }
            else
            {
                xml = xml.Replace("#isFinishGood", "0");
            }

            xml = xml.Replace("#purchasePrice", refData.productPurchasePrice);
            xml = xml.Replace("#sellPrice", refData.productSellPrice);

            xml = xml.Replace("#height", refData.productHeight);
            xml = xml.Replace("#width", refData.productWidth);

            xml = xml.Replace("#packSize", refData.packSize);
            xml = xml.Replace("#reorderPoint", refData.reOrderPoint);
            xml = xml.Replace("\r\n", "");
            return xml;
        }
        private string SaleOrderXmlData(Guid id, Estimate order, Company customer, List<Item> products, double TaxRate)
        {
            double corectTaxRate = TaxRate / 100;

            string xml = @"<?xml version='1.0'?>
                    <SalesOrder xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns='http://api.unleashedsoftware.com/version/1'>
 
                     <Guid>{0}</Guid>
	                    <OrderNumber>{1}</OrderNumber>
	                    <OrderStatus>Parked</OrderStatus>
	                    <Customer>
	                      <Guid>{2}</Guid>
	                    </Customer>
	
	                    <TaxRate>{3}</TaxRate>
	
	                    <SubTotal>{4}</SubTotal>
	                    <TaxTotal>{5}</TaxTotal>
	                    <Total>{6}</Total>    
	

	                    <SalesOrderLines>
	                     ";

            XeroXmlRefData refData = CalculateOrderRefData(order, customer, products);
            xml += GetSalesOrderLineXml(products, order.isDirectSale ?? false, TaxRate);
            xml += @"</SalesOrderLines>
						  </SalesOrder>";
            xml = xml.Replace("\r\n", "");
            xml = string.Format(xml, id, order.Order_Code, customer.XeroAccessCode, corectTaxRate, refData.subTotal, refData.taxTotal, refData.Total);
            return xml;
        }
        private static string InvoiceXmlData(Guid gid, Estimate order, Company customer, double TaxRate, double taxtotal, string custGuid, List<Item> products)
        {

            #region preFunctionalities for xml
            // For order date
            DateTime orderDate = new DateTime();
            orderDate = order.Order_Date ?? DateTime.Now;


            string orderDateString = orderDate.ToString("yyyy-MM-dd");
            // for requiredDate
            DateTime ReqDate = new DateTime();
            ReqDate = order.Order_DeliveryDate ?? DateTime.Now;

            string ReqDateString = ReqDate.ToString("yyyy-MM-dd");

            // for Due Date
            DateTime DueDate = new DateTime();
            DueDate = order.Order_DeliveryDate ?? DateTime.Now;

            string DueDateString = DueDate.ToString("yyyy-MM-dd"); 
            // for taxrate

            double corectTaxRate = TaxRate / 100;

            DateTime pd = new DateTime();
            if (order.PrePayments.Count > 0)
            {
                pd = order.PrePayments.Select(x => x.PaymentDate).FirstOrDefault();
            }
            else
            {
                pd = DateTime.Now;
            }

            string paymentddate = pd.ToString("yyyy-MM-dd");

            double LineTotal = 0;
            foreach (Item itm in products)
            {
                LineTotal += itm.Qty1NetTotal ?? 0;
            }
            double linetax = 0;
            foreach (Item itm in products)
            {

                linetax += itm.Qty1Tax1Value ?? 0;

            }


            double Subtotal = 0;
            Subtotal = LineTotal;
            double taxtotalOrder = 0;
            taxtotalOrder = linetax;

            double total = 0;
            total = Subtotal + taxtotalOrder;

            #endregion


            string xml = @"<?xml version='1.0'?>
                <SalesInvoice xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns='http://api.unleashedsoftware.com/version/1'>
                  <Guid>{0}</Guid>
                  <OrderNumber>{1}</OrderNumber>
                  <OrderDate>{2}</OrderDate>
                 <RequiredDate>{3}</RequiredDate>
                  <OrderStatus>{4}</OrderStatus>
                    <Customer>
                    <Guid>{5}</Guid>
                  </Customer>
  
                  <TaxRate>{6}</TaxRate>
  
                  <SubTotal>{7}</SubTotal>
                  <TaxTotal>{8}</TaxTotal>
                  <Total>{9}</Total>

                  <PaymentDueDate>{10}</PaymentDueDate>
                  <SalesOrderLines> 
                ";

            xml += GetInvoiceOrderLine(products, DueDateString, corectTaxRate);

            xml += @"</SalesOrderLines>
					 </SalesInvoice>";



            xml = string.Format(xml, gid, order.Order_Code, orderDateString, ReqDateString, "Confirmed", customer.XeroAccessCode, corectTaxRate, Subtotal, taxtotalOrder, total, paymentddate);
            // xml = string.Format(xml, gid);
            xml = xml.Replace("\r\n", "");

            return xml;
        }


        private string GetSalesOrderLineXml(List<Item> products, bool isDirect, double taxrate)
        {
            string xml = "";
            string tags = "";
            int count = 1;
            double correctTaxrate = taxrate / 100;
            foreach (Item item in products)
            {
                tags += "<SalesOrderLine>";
                tags += " <LineNumber>" + count + "</LineNumber>";
                if (item.ItemType == 2)
                {
                    long ccID = 0;
                    string costcenterID = string.Empty;
                    ccID = this.CostCentreRepository.GetCostCentreIdByName(item.ProductName);
                    
                    if (ccID > 0)
                        costcenterID = Convert.ToString(ccID);
                    tags += @"<Product>a
								  <ProductCode>" + costcenterID + @"</ProductCode>
							  </Product>";
                }
                else if (item.ItemType == 3)
                {
                    long SID = 0;
                    string StockItemID = string.Empty;
                    SID = stockItemRepository.Find(item.RefItemId ?? 0).StockItemId;
                    if (SID > 0)
                        StockItemID = Convert.ToString(SID);
                    tags += @"<Product>a
								  <ProductCode>" + StockItemID + @"</ProductCode>
							  </Product>";
                }
                else
                {
                    tags += @"<Product>a
								  <ProductCode>" + item.ProductCode + @"</ProductCode>
							  </Product>";
                }
                tags += "<OrderQuantity>" + GetQty(item, isDirect) + "</OrderQuantity>";
                tags += "<UnitPrice>" + GetUnitPrice(item, isDirect) + "</UnitPrice>";
                tags += "<LineTotal>" + GetNetTotal(item, isDirect) + "</LineTotal>";
                tags += "<UnitCost>0</UnitCost>";
                tags += "<TaxRate>" + correctTaxrate + "</TaxRate>";
                tags += "<LineTax>" + GetLineTax(item, isDirect) + "</LineTax>";
                tags += "</SalesOrderLine>";

                count++;
            }
            xml += tags;
            return xml;
        }

        private static string GetInvoiceOrderLine(List<Item> products, string DueDate, double taxrate)
        {
            string xml = "";
            string tags = "";
            int count = 1;

            foreach (Item item in products)
            {
                tags += "<SalesInvoiceLine>";
                tags += " <LineNumber>" + count + "</LineNumber>";
                tags += @"<Product>
								  <Guid>" + item.XeroAccessCode + @"</Guid>
							  </Product>";
                tags += "<DueDate>" + DueDate + "</DueDate>";
                if (item.ItemType == 2)
                    tags += "<OrderQuantity>" + 1 + "</OrderQuantity>";
                else
                    tags += "<OrderQuantity>" + item.Qty1 + "</OrderQuantity>";

                tags += "<UnitPrice>" + GetUnitPrice(item, false) + "</UnitPrice>";
                tags += "<LineTotal>" + item.Qty1NetTotal + "</LineTotal>";
                tags += "<TaxRate>" + taxrate + "</TaxRate>";
                tags += "<LineTax>" + item.Qty1Tax1Value + "</LineTax>";
                tags += "</SalesInvoiceLine>";

                count++;
            }
            xml += tags;
            return xml;
        }


        private static double GetQty(Item item, bool isDirect)
        {
            if (!isDirect)
            {
                if (item.ItemType == 2)
                {
                    return 1;
                }
                else
                {
                    return item.Qty1.Value;
                }
            }
            else
            {
                switch (item.JobSelectedQty)
                {
                    case null:
                        return item.Qty1.Value;
                    case 1:
                        return item.Qty1.Value;
                    case 2:
                        return item.Qty2.Value;
                    case 3:
                        return item.Qty3.Value;
                    default:
                        return 0;
                }
            }
        }
        private static double GetUnitPrice(Item item, bool isDirect)
        {
            if (!isDirect)
            {
                if (item.ItemType == 2)
                    return item.Qty1NetTotal.Value;
                else
                    return (item.Qty1NetTotal.Value / item.Qty1.Value);

            }
            else
            {
                switch (item.JobSelectedQty)
                {
                    case null:
                        return (item.Qty1NetTotal.Value / item.Qty1.Value);
                    case 1:
                        return (item.Qty1NetTotal.Value / item.Qty1.Value);
                    case 2:
                        return (item.Qty2NetTotal.Value / item.Qty2.Value);
                    case 3:
                        return (item.Qty3NetTotal.Value / item.Qty3.Value);
                    default:
                        return 0;
                }
            }
        }
        private static double GetNetTotal(Item item, bool isDirect)
        {
            if (!isDirect)
            {
                return item.Qty1NetTotal.Value;
            }
            else
            {
                switch (item.JobSelectedQty)
                {
                    case null:
                        return item.Qty1NetTotal.Value;
                    case 1:
                        return item.Qty1NetTotal.Value;
                    case 2:
                        return item.Qty2NetTotal.Value;
                    case 3:
                        return item.Qty3NetTotal.Value;
                    default:
                        return 0;
                }
            }
        }
        private static double GetTaxRate(Item item, bool isDirect)
        {
            if (!isDirect)
            {
                return item.Qty1Tax1Value.Value;
            }
            else
            {
                switch (item.JobSelectedQty)
                {
                    case null:
                        return item.Qty1Tax1Value.Value;
                    case 1:
                        return item.Qty1Tax1Value.Value;
                    case 2:
                        return item.Qty2Tax1Value.Value;
                    case 3:
                        return item.Qty3Tax1Value.Value;
                    default:
                        return 0;
                }
            }
        }
        private static double GetLineTax(Item item, bool isDirect)
        {
            if (!isDirect)
            {
                return item.Qty1Tax1Value.Value;
            }
            else
            {
                switch (item.JobSelectedQty)
                {
                    case null:
                        return item.Qty1Tax1Value.Value;
                    case 1:
                        return item.Qty1Tax1Value.Value;
                    case 2:
                        return item.Qty2Tax1Value.Value;
                    case 3:
                        return item.Qty3Tax1Value.Value;
                    default:
                        return 0;
                }
            }
        }
        private static XeroXmlRefData CalculateOrderRefData(Estimate order, Company customer, List<Item> products)
        {
            XeroXmlRefData data = new XeroXmlRefData();
            double sub, tax, total;
            sub = 0; tax = 0; total = 0;

            if (!order.isDirectSale.Value)
            {
                foreach (Item item in products)
                {
                    sub += item.Qty1NetTotal.Value;
                    tax += item.Qty1Tax1Value.Value;
                }
                total = sub + tax;

                data.subTotal = sub.ToString();
                data.taxTotal = tax.ToString();
                data.Total = total.ToString();
            }
            else
            {
                foreach (Item item in products)
                {
                    switch (item.JobSelectedQty)
                    {
                        case null:
                            sub += item.Qty1NetTotal.Value;
                            tax += item.Qty1Tax1Value.Value;
                            break;
                        case 1:
                            sub += item.Qty1NetTotal.Value;
                            tax += item.Qty1Tax1Value.Value;
                            break;
                        case 2:
                            sub += item.Qty2NetTotal.Value;
                            tax += item.Qty2Tax1Value.Value;
                            break;
                        case 3:
                            sub += item.Qty3NetTotal.Value;
                            tax += item.Qty3Tax1Value.Value;
                            break;
                    }
                }
                total = sub + tax;

                data.subTotal = sub.ToString();
                data.taxTotal = tax.ToString();
                data.Total = total.ToString();
            }
            return data;
        }
        private string CalculatePurchasePrice(Item item, bool isDirectOrder)
        {
            ItemSection section = item.ItemSections.FirstOrDefault();
            long? stockid = section.StockItemID1;

            int? stockSequence =
                item.ItemStockOptions.Where(o => o.StockId == stockid).Select(o => o.OptionSequence).FirstOrDefault();// this.ObjectContext.tbl_ItemStockOptions.Where(o => o.StockID == stockid).Select(o => o.OptionSequence).FirstOrDefault();

            if (item.SupplierId != null)
            {
                ItemPriceMatrix spm = new ItemPriceMatrix();
                if (item.IsQtyRanged == true)
                {
                    if (!isDirectOrder)
                    {
                        spm =
                            item.ItemPriceMatrices.Where(
                                s =>
                                    s.SupplierId == item.SupplierId &&
                                    (item.Qty1.Value >= s.QtyRangeFrom && item.Qty1 <= s.QtyRangeTo))
                                .FirstOrDefault();
                    }
                    else
                    {
                        switch (item.JobSelectedQty)
                        {
                            case null:
                                spm =
                            item.ItemPriceMatrices.Where(
                                s =>
                                    s.SupplierId == item.SupplierId &&
                                    (item.Qty1.Value >= s.QtyRangeFrom && item.Qty1 <= s.QtyRangeTo))
                                .FirstOrDefault();
                                
                                break;
                            case 1:
                                spm =
                           item.ItemPriceMatrices.Where(
                               s =>
                                   s.SupplierId == item.SupplierId &&
                                   (item.Qty1.Value >= s.QtyRangeFrom && item.Qty1 <= s.QtyRangeTo))
                               .FirstOrDefault();
                                
                                break;
                            case 2:
                                spm =
                           item.ItemPriceMatrices.Where(
                               s =>
                                   s.SupplierId == item.SupplierId &&
                                   (item.Qty2.Value >= s.QtyRangeFrom && item.Qty2 <= s.QtyRangeTo))
                               .FirstOrDefault();
                                break;
                            case 3:
                                spm =
                            item.ItemPriceMatrices.Where(
                                s =>
                                    s.SupplierId == item.SupplierId &&
                                    (item.Qty3.Value >= s.QtyRangeFrom && item.Qty3 <= s.QtyRangeTo))
                                .FirstOrDefault();
                                break;
                        }
                    }
                }
                else
                {
                    if (!isDirectOrder)
                    {
                        spm =
                            item.ItemPriceMatrices.Where(
                                s =>
                                    s.SupplierId == item.SupplierId &&
                                    (item.Qty1.Value >= s.Quantity))
                                .FirstOrDefault();
                       
                    }
                    else
                    {
                        switch (item.JobSelectedQty)
                        {
                            case null:
                                spm =
                            item.ItemPriceMatrices.Where(
                                s =>
                                    s.SupplierId == item.SupplierId &&
                                    (item.Qty1.Value >= s.Quantity))
                                .FirstOrDefault();
                                break;
                            case 1:
                                spm =
                           item.ItemPriceMatrices.Where(
                               s =>
                                   s.SupplierId == item.SupplierId &&
                                   (item.Qty1.Value >= s.Quantity))
                               .FirstOrDefault();
                                
                                break;
                            case 2:
                                spm =
                            item.ItemPriceMatrices.Where(
                                s =>
                                    s.SupplierId == item.SupplierId &&
                                    (item.Qty2.Value >= s.Quantity))
                                .FirstOrDefault();
                                break;
                            case 3:
                                spm =
                           item.ItemPriceMatrices.Where(
                               s =>
                                   s.SupplierId == item.SupplierId &&
                                   (item.Qty3.Value >= s.Quantity))
                               .FirstOrDefault();
                                break;
                        }
                    }
                }

                if (spm != null)
                {
                    switch (stockSequence)
                    {
                        case 1:
                            return Convert.ToString(spm.PricePaperType1);
                        case 2:
                            return Convert.ToString(spm.PricePaperType2);
                        case 3:
                            return Convert.ToString(spm.PricePaperType3);
                        case 4:
                            return Convert.ToString(spm.PriceStockType4);
                        case 5:
                            return Convert.ToString(spm.PriceStockType5);
                        case 6:
                            return Convert.ToString(spm.PriceStockType6);
                        case 7:
                            return Convert.ToString(spm.PriceStockType7);
                        case 8:
                            return Convert.ToString(spm.PriceStockType8);
                        case 9:
                            return Convert.ToString(spm.PriceStockType9);
                        case 10:
                            return Convert.ToString(spm.PriceStockType10);
                        case 11:
                            return Convert.ToString(spm.PriceStockType11);
                        default:
                            return "0";
                    }
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }
        }
        private string CalculateSalesPrice(Item item, bool isDirectOrder)
        {
            if (!isDirectOrder)
            {
                return Convert.ToString(item.Qty1NetTotal);
            }
            else
            {
                switch (item.JobSelectedQty)
                {
                    case null:
                        return Convert.ToString(item.Qty1NetTotal);
                    case 1:
                        return Convert.ToString(item.Qty1NetTotal);
                    case 2:
                        return Convert.ToString(item.Qty2NetTotal);
                    case 3:
                        return Convert.ToString(item.Qty3NetTotal);
                }
            }
            return "0";
        }
        private string CalculateHeight(Item item)
        {
            ItemSection section = item.ItemSections.FirstOrDefault();
            long? stockid = section != null ? section.StockItemID1 : 0;

            StockItem stockItem = stockItemRepository.Find(stockid ?? 0);
            short? isCustom = stockItem != null ? stockItem.ItemSizeCustom : 0;

            if (isCustom.HasValue && stockItem != null)
            {
                if (isCustom == 0)
                {
                    return Convert.ToString(stockItem.ItemSizeHeight ?? 0);
                }
                else
                {

                    double? height = paperSizeRepository.Find(stockItem.ItemSizeId ?? 0).Height;
                    return Convert.ToString(height??0);
                }
            }

            return "0";
        }
        private string CalculateWeight(Item item)
        {
            ItemSection section = item.ItemSections.FirstOrDefault();
            long? stockid = section != null ? section.StockItemID1 : 0;

            StockItem stockItem = stockItemRepository.Find(stockid ?? 0);
            short? isCustom = stockItem != null ? stockItem.ItemSizeCustom : 0;
            
            if (isCustom.HasValue)
            {
                if (stockItem != null)
                {
                    if (isCustom == 0)
                    {
                        if (stockItem.ItemSizeWidth.HasValue)
                        {
                            return stockItem.ItemSizeWidth.Value.ToString();
                        }
                        else
                        {
                            return "0";
                        }
                    }
                    else
                    {
                        double? width = paperSizeRepository.Find(stockItem.ItemSizeId ?? 0).Width;
                        if (width != null)
                        {
                            return Convert.ToString(width);
                        }
                        else
                        {
                            return "0";
                        }
                    }
                }
            }

            return "0";
        }
        private string CalculatePackSize(Item item)
        {
            ItemSection section = item.ItemSections.FirstOrDefault();
            long? stockid = section != null ? section.StockItemID1 : 0;

            StockItem stockItem = stockItemRepository.Find(stockid ?? 0);

            if (stockItem != null)
            {
                return Convert.ToString(stockItem.PackageQty??0);
            }
            
            return "0";
        }
        private string CalculateReorderLevel(Item item)
        {
            ItemSection section = item.ItemSections.FirstOrDefault();
            long? stockid = section !=null ? section.StockItemID1 : 0;

            StockItem stockItem = stockItemRepository.Find(stockid ?? 0);

            if (stockItem != null)
            {
                return Convert.ToString(stockItem.ReOrderLevel??0);
            }
            
            return "0";
        }
        private static string PostXml(string resource, string id, string key, string guid, string postData)
        {
            string ApiHost = "https://api.unleashedsoftware.com";
            string uri = string.Format("{0}/{1}/{2}", ApiHost, resource, guid);
            var client = new WebClient();
            string query = string.Empty;
            SetAuthenticationHeaders(client, query, RequestType.Xml, id, key);
            ServicePointManager.ServerCertificateValidationCallback += ValidateServerCertificate;


            string xml = Post(client, uri, postData);

            var xmlDocument = new XmlDocument { PreserveWhitespace = true };
            xmlDocument.LoadXml(xml);
            return xmlDocument.InnerXml;
        }
        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        private static void SetAuthenticationHeaders(WebClient client, string query, RequestType requestType, string id, string key)
		{
			string signature = GetSignature(query, key);
			client.Headers.Add("api-auth-id", id);
			client.Headers.Add("api-auth-signature", signature);

			if (requestType == RequestType.Xml)
			{
				client.Headers.Add("Accept", "application/xml");
				client.Headers.Add("Content-Type", "application/xml; charset=" + client.Encoding.WebName);
			}
			else
			{
				client.Headers.Add("Accept", "application/json");
				client.Headers.Add("Content-Type", "application/json; charset=" + client.Encoding.WebName);
			}
		}
        private static string Post(WebClient client, string uri, string postData)
        {
            string response = string.Empty;
            try
            {
               
                response = client.UploadString(uri, "POST", postData);

            }
            catch (WebException ex)
            {
                throw ex;
                
            }
            return response;
        }
        private static string GetSignature(string args, string privatekey)
        {
            var encoding = new System.Text.ASCIIEncoding();
            byte[] key = encoding.GetBytes(privatekey);
            var myhmacsha256 = new HMACSHA256(key);
            byte[] hashValue = myhmacsha256.ComputeHash(encoding.GetBytes(args));
            string hmac64 = Convert.ToBase64String(hashValue);
            myhmacsha256.Clear();
            return hmac64;
        }
        #endregion
        
    }
}