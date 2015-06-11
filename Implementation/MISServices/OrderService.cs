using System;
using System.Collections.Generic;
using System.Configuration;
using MPC.ExceptionHandling;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
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
            ISectionCostCentreDetailRepository sectionCostCentreDetailRepository, IPipeLineProductRepository pipeLineProductRepository, IItemStockOptionRepository itemStockOptionRepository, IItemSectionRepository itemSectionRepository, IItemAddOnCostCentreRepository itemAddOnCostCentreRepository, IExportReportHelper exportReportHelper)
        {
            if (estimateRepository == null)
            {
                throw new ArgumentNullException("estimateRepository");
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
        }

        #endregion
        #region Public

        /// <summary>
        /// Get All Orders
        /// </summary>
        public GetOrdersResponse GetAll(GetOrdersRequest request)
        {
            return estimateRepository.GetOrders(request);
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
            return estimateRepository.Find(orderId);
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

            // Update Order
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
            // Save Changes
            estimateRepository.SaveChanges();

            // Save Item Attachments
            SaveItemAttachments(order);

            // Save Changes
            estimateRepository.SaveChanges();

            // Load Status
            estimateRepository.LoadProperty(order, () => order.Status);

            // Return 
            return order;
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
                       LoggedInUser = organisationRepository.LoggedInUserId
                   };
        }

        /// <summary>
        /// Get base data for Estimate
        /// Difference from order is Different Section Id
        /// </summary>
        public OrderBaseResponse GetBaseDataForEstimate()
        {
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
                LoggedInUser = organisationRepository.LoggedInUserId
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

            return new ItemDetailBaseResponse
            {
                Markups = _markupRepository.GetAll(),
                PaperSizes = paperSizeRepository.GetAll(),
                InkPlateSides = inkPlateSideRepository.GetAll(),
                Inks = stockItemRepository.GetStockItemOfCategoryInk(),
                InkCoverageGroups = inkCoverageGroupRepository.GetAll(),
                CurrencySymbol = organisation != null ? (organisation.Currency != null ? organisation.Currency.CurrencySymbol : string.Empty) : string.Empty,
                SystemUsers = systemUserRepository.GetAll(),
                LengthUnit = organisation != null && organisation.LengthUnit != null ? organisation.LengthUnit.UnitName : string.Empty,
                WeightUnit = organisation != null && organisation.WeightUnit != null ? organisation.WeightUnit.UnitName : string.Empty,
                LoggedInUser = organisationRepository.LoggedInUserId,
                Machines = MachineRepository.GetAll()
            };

        }

        /// <summary>
        /// Get Base Data For Company
        /// </summary>
        public OrderBaseResponseForCompany GetBaseDataForCompany(long companyId, long storeId)
        {
            return new OrderBaseResponseForCompany
                {
                    CompanyContacts = companyContactRepository.GetCompanyContactsByCompanyId(companyId),
                    CompanyAddresses = addressRepository.GetAddressByCompanyID(companyId),
                    TaxRate = companyRepository.GetTaxRateByStoreId(storeId)
                };
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
            target.StatusId = (short)OrderStatus.PendingOrder;

            Estimate est_Source = GetById(source.EstimateId);
            est_Source.StatusId = 39;

            target = UpdateEstimeteOnCloning(est_Source, target, source);
            target.RefEstimateId = source.EstimateId;

            estimateRepository.SaveChanges();

            est_Source.RefEstimateId = target.EstimateId;
            estimateRepository.SaveChanges();

            return target;
        }

        public Estimate UpdateEstimeteOnCloning(Estimate source, Estimate target, Estimate clientSource)
        {
            // Clone Estimate
            source.Clone(target);
            target.Order_Date = clientSource.Order_Date;
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
                }
                if (item.JobSelectedQty != null && targetItem.ItemType != 2)
                {
                    if (item.JobSelectedQty == 1)
                    {
                        targetItem.Qty1 = item.Qty1;
                        targetItem.Qty2 = 0;
                        targetItem.Qty3 = 0;
                    }
                    else if (item.JobSelectedQty == 2)
                    {
                        targetItem.Qty1 = item.Qty2;
                        targetItem.Qty2 = 0;
                        targetItem.Qty3 = 0;
                    }
                    else if (item.JobSelectedQty == 3)
                    {
                        targetItem.Qty1 = item.Qty3;
                        targetItem.Qty2 = 0;
                        targetItem.Qty3 = 0;
                    }
                }

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
                    targetItemSection.Qty1 = source.Qty1;
                    targetItemSection.Qty2 = 0;
                    targetItemSection.Qty3 = 0;
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
                targetSectionCostcentre.Qty1 = source.Qty1;
                targetSectionCostcentre.Qty2 = 0;
                targetSectionCostcentre.Qty3 = 0;
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
        public string DownloadOrderArtwork(int OrderID, string sZipName)
        {
            //return orderRepository.GenerateOrderArtworkArchive(OrderID, sZipName);
            return GenerateOrderArtworkArchive(OrderID, sZipName);
            // return ExportPDF(105, 0, ReportType.Invoice, 814, string.Empty);
        }

        public string GenerateOrderArtworkArchive(int OrderID, string sZipName)
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
                OrganisationId = Organisation.OrganisationId;

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

                if (!IncludeOrderReport && !IncludeJobCardReport && !IncludeOrderXML && !MakeArtWorkProductionReady)
                {
                    IncludeOrderReport = true;
                }

                //making the artwork production ready and regenerating template PDFs
                if (MakeArtWorkProductionReady)
                {

                    ArtworkProductionReadyResult = MakeOrderArtworkProductionReady(oOrder);

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
                                string sJCReportPath = exportReportHelper.ExportPDF(165, item.ItemId, ReportType.JobCard, OrderID, string.Empty);
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
                            string sOrderReportPath = exportReportHelper.ExportPDF(103, Convert.ToInt64(OrderID), ReportType.Order, OrderID, string.Empty);
                            if (System.IO.File.Exists(sOrderReportPath))
                            {
                                ZipEntry r = zip.AddFile(sOrderReportPath, "");
                                r.Comment = "Order Report by My Print Cloud";
                            }
                        }
                        // here xml comes
                        if (IncludeOrderXML)
                        {
                            string sOrderXMLReportPath = exportReportHelper.ExportOrderReportXML(OrderID, "", "0");
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

        public bool MakeOrderArtworkProductionReady(Estimate oOrder)
        {
            try
            {
                long sOrganisationId = organisationRepository.GetOrganizatiobByID().OrganisationId;
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
                bool mutlipageMode = true;
                bool hasOverlayPdf = false;
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

                            orderRepository.regeneratePDFs(TemplateID, OrganisationId, isaddcropMark, mutlipageMode, drawBleedArea, bleedsize);
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
                                string folderPath = "MPC_Content/Attachments/" + OrganisationId;
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
                                string folderPath = "MPC_Content/Attachments/" + OrganisationId;
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

                            string folderPath = "Attachments/" + OrganisationId;// Web2Print.UI.Components.ImagePathConstants.ProductImagesPath + "Attachments/";
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

                            //foreach (var oPage in ListOfAttachments)
                            //{
                            //    string fileName = oPage.FileName;
                            //    string fileCompleteAddress = System.IO.Path.Combine(virtualFolderPth, fileName);
                            //    string sourceFileAdd = System.IO.Path.Combine(fileSourcePath, fileName);
                            //    System.IO.File.Copy(sourceFileAdd, fileCompleteAddress, true);

                            //}
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
    }
}
