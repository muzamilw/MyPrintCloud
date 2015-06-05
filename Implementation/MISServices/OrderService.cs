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
            ISectionCostCentreDetailRepository sectionCostCentreDetailRepository, IPipeLineProductRepository pipeLineProductRepository, IItemStockOptionRepository itemStockOptionRepository, IItemSectionRepository itemSectionRepository, IItemAddOnCostCentreRepository itemAddOnCostCentreRepository)
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
                                string sJCReportPath = ExportPDF(165, item.ItemId, ReportType.JobCard, OrderID, string.Empty);
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
                            string sOrderReportPath = ExportPDF(103, Convert.ToInt64(OrderID), ReportType.Order, OrderID, string.Empty);
                            if (System.IO.File.Exists(sOrderReportPath))
                            {
                                ZipEntry r = zip.AddFile(sOrderReportPath, "");
                                r.Comment = "Order Report by My Print Cloud";
                            }
                        }
                        // here xml comes
                        if (IncludeOrderXML)
                        {
                            string sOrderXMLReportPath = ExportOrderReportXML(OrderID, "", "0");
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

        public string ExportPDF(int iReportID, long iRecordID, ReportType type, long OrderID, string CriteriaParam)
        {
            string sFilePath = string.Empty;
            try
            {
                long OrganisationID = 0;
                Organisation org = organisationRepository.GetOrganizatiobByID();
                if (org != null)
                {
                    OrganisationID = org.OrganisationId;
                }
                Report currentReport = ReportRepository.GetReportByReportID(iReportID);
                if (currentReport.ReportId > 0)
                {
                    byte[] rptBytes = null;
                    rptBytes = System.Text.Encoding.Unicode.GetBytes(currentReport.ReportTemplate);
                    // Encoding must be done
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(rptBytes);
                    // Load it to memory stream
                    ms.Position = 0;
                    SectionReport currReport = new SectionReport();
                    string sFileName = string.Empty;
                    // FileNamesList.Add(sFileName);
                    currReport.LoadLayout(ms);
                    if (type == ReportType.JobCard)
                    {
                        sFileName = iRecordID + "JobCardReport.pdf";
                        //  FileNamesList.Add(sFileName);
                        List<usp_JobCardReport_Result> rptSource = ReportRepository.getJobCardReportResult(OrganisationID, OrderID, iRecordID);
                        currReport.DataSource = rptSource;
                    }
                    else if (type == ReportType.Order)
                    {
                        sFileName = iRecordID + "OrderReport.pdf";
                        List<usp_OrderReport_Result> rptOrderSource = ReportRepository.getOrderReportResult(OrganisationID, iRecordID);
                        currReport.DataSource = rptOrderSource;
                    }
                    else if (type == ReportType.Estimate)
                    {
                        sFileName = iRecordID + "EstimateReport.pdf";
                        List<usp_EstimateReport_Result> rptEstimateSource = ReportRepository.getEstimateReportResult(OrganisationID, iRecordID);
                        currReport.DataSource = rptEstimateSource;
                    }
                    else if (type == ReportType.Invoice)
                    {
                        sFileName = iRecordID + "InvoiceReport.pdf";
                        List<usp_InvoiceReport_Result> rptInvoiceSource = ReportRepository.getInvoiceReportResult(OrganisationID, iRecordID);
                        currReport.DataSource = rptInvoiceSource;
                    }
                    else if (type == ReportType.Internal)
                    {
                        string ReportDataSource = string.Empty;
                        string ReportTemplate = string.Empty;

                        sFileName = "OrderReport.pdf";
                        DataTable dataSourceList = ReportRepository.GetReportDataSourceByReportID(iReportID, CriteriaParam);
                        currReport.DataSource = dataSourceList;
                    }
                    if (currReport != null)
                    {
                        currReport.Run();
                        GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdf = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                        string Path = HttpContext.Current.Server.MapPath("~/" + ImagePathConstants.ReportPath + OrganisationID + "/");
                        if (!Directory.Exists(Path))
                        {
                            Directory.CreateDirectory(Path);
                        }
                        // PdfExport pdf = new PdfExport();
                        sFilePath = HttpContext.Current.Server.MapPath("~/" + ImagePathConstants.ReportPath + OrganisationID + "/") + sFileName;

                        pdf.Export(currReport.Document, sFilePath);
                        ms.Close();
                        currReport.Document.Dispose();
                        pdf.Dispose();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return sFilePath;
        }



        public string ExportOrderReportXML(long iRecordID, string OrderCode, string XMLFormat)
        {
            string sFilePath = string.Empty;
            bool isCorporate = false;
            string CurrencySymbol = string.Empty;

            try
            {

                long OrganisationID = 0;
                Organisation org = organisationRepository.GetOrganizatiobByID();
                if (org != null)
                {
                    OrganisationID = org.OrganisationId;
                }
                Estimate orderEntity = new Estimate();
                if (iRecordID > 0)
                    orderEntity = orderRepository.GetOrderByIdforXml(iRecordID);
                if (OrderCode != "")
                    orderEntity = orderRepository.GetOrderByOrderCode(OrderCode);

                //long CurrencyID = db.Organisations.Where(c => c.OrganisationId == OrganisationId).Select(c => c.CurrencyId ?? 0).FirstOrDefault();
                Currency curr = CurrencyRepository.GetCurrencySymbolByOrganisationId(OrganisationID);
                if (curr != null)
                    CurrencySymbol = curr.CurrencySymbol;
                List<PrePayment> paymentsList = prePaymentRepository.GetPrePaymentsByOrganisatioID(iRecordID);
                if (orderEntity != null)
                {

                    XmlDocument XDoc = new XmlDocument();

                    XmlNode docNode = XDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                    XDoc.AppendChild(docNode);
                    // Create root node.
                    XmlElement XElemRoot = XDoc.CreateElement("Order");
                    //Add the node to the document.
                    XDoc.AppendChild(XElemRoot);

                    XmlElement XTemp = XDoc.CreateElement("OrderDetail");


                    XmlAttribute OrderUserNotesAttr = XDoc.CreateAttribute("UserNotes");
                    if (!string.IsNullOrEmpty(orderEntity.UserNotes))
                        OrderUserNotesAttr.Value = orderEntity.UserNotes;
                    else
                        OrderUserNotesAttr.Value = string.Empty;
                    XTemp.SetAttributeNode(OrderUserNotesAttr);


                    int SID = 0;
                    if (orderEntity.SourceId != null)
                        SID = (int)orderEntity.SourceId;

                    string SourceName = pipeLineSourceRepository.GetSourceNameByID(SID);
                    XmlAttribute OrderSourceAttr = XDoc.CreateAttribute("Source");
                    if (!string.IsNullOrEmpty(SourceName))
                    {
                        OrderSourceAttr.Value = SourceName;
                    }
                    else
                    {
                        OrderSourceAttr.Value = string.Empty;
                    }
                    XTemp.SetAttributeNode(OrderSourceAttr);


                    DateTime dtCreation = new DateTime();
                    string CreationDate = string.Empty;
                    XmlAttribute OrderCreationAttr = XDoc.CreateAttribute("CreationDate");
                    if (orderEntity.CreationDate != null)
                    {
                        dtCreation = Convert.ToDateTime(orderEntity.CreationDate);
                        CreationDate = dtCreation.ToString("dd/MMM/yyyy");
                        OrderCreationAttr.Value = CreationDate;
                    }
                    else
                    {
                        OrderCreationAttr.Value = string.Empty;
                    }

                    XTemp.SetAttributeNode(OrderCreationAttr);

                    XmlAttribute OrderFooterAttr = XDoc.CreateAttribute("OrderFooter");
                    if (!string.IsNullOrEmpty(orderEntity.FootNotes))
                        OrderFooterAttr.Value = orderEntity.FootNotes;
                    else
                        OrderFooterAttr.Value = string.Empty;
                    XTemp.SetAttributeNode(OrderFooterAttr);

                    XmlAttribute OrderHeaderAttr = XDoc.CreateAttribute("OrderHeader");
                    if (!string.IsNullOrEmpty(orderEntity.HeadNotes))
                        OrderHeaderAttr.Value = orderEntity.HeadNotes;
                    else
                        OrderHeaderAttr.Value = string.Empty;
                    XTemp.SetAttributeNode(OrderHeaderAttr);

                    XmlAttribute OrderPONumbAttr = XDoc.CreateAttribute("CustomerPORef");
                    int isPO = 1;
                    string POString = string.Empty;
                    if (orderEntity.CustomerPO != null)
                    {
                        OrderPONumbAttr.Value = orderEntity.CustomerPO;
                    }
                    else
                    {
                        OrderPONumbAttr.Value = string.Empty;
                    }
                    XTemp.SetAttributeNode(OrderPONumbAttr);


                    XmlAttribute OrderCreditAppAttr = XDoc.CreateAttribute("OrderCreditApproved");
                    int isApproved = 1;
                    string ApproveString = string.Empty;
                    if (orderEntity.IsCreditApproved != null)
                    {
                        isApproved = (int)orderEntity.IsCreditApproved;
                        if (isApproved == 1)
                            ApproveString = "True";
                        else
                            ApproveString = "False";
                    }
                    OrderCreditAppAttr.Value = ApproveString;
                    XTemp.SetAttributeNode(OrderCreditAppAttr);

                    SectionFlag oFlag = sectionFlagRepository.GetSectionFlag(orderEntity.SectionFlagId);
                    XmlAttribute OrderflagAttr = XDoc.CreateAttribute("OrderFlag");
                    if (oFlag != null)
                        OrderflagAttr.Value = oFlag.FlagName;
                    else
                        OrderflagAttr.Value = string.Empty;
                    XTemp.SetAttributeNode(OrderflagAttr);

                    string OrderDateSta = string.Empty;
                    //if (orderEntity.Order_Date != null)
                    DateTime dtStart = new DateTime();
                    XmlAttribute OrderDateStartAttr = XDoc.CreateAttribute("StartDeliveryDate");
                    if (orderEntity.Order_Date != null)
                    {
                        dtStart = Convert.ToDateTime(orderEntity.StartDeliveryDate);
                        OrderDateSta = dtStart.ToString("dd/MMM/yyyy");
                        OrderDateStartAttr.Value = OrderDateSta;
                    }
                    else
                        OrderDateStartAttr.Value = string.Empty;
                    XTemp.SetAttributeNode(OrderDateStartAttr);

                    string OrderDateFin = string.Empty;
                    //if (orderEntity.Order_Date != null)
                    DateTime dtFinsih = new DateTime();
                    XmlAttribute OrderDateFinishAttr = XDoc.CreateAttribute("FinishDeliveryDate");
                    if (orderEntity.Order_Date != null)
                    {
                        dtFinsih = Convert.ToDateTime(orderEntity.FinishDeliveryDate);
                        OrderDateFin = dtFinsih.ToString("dd/MMM/yyyy");
                        OrderDateFinishAttr.Value = OrderDateFin;
                    }
                    else
                        OrderDateFinishAttr.Value = string.Empty;
                    XTemp.SetAttributeNode(OrderDateFinishAttr);


                    string OrderDate = string.Empty;
                    //if (orderEntity.Order_Date != null)
                    DateTime dt = new DateTime();
                    XmlAttribute OrderDateAtt = XDoc.CreateAttribute("OrderDate");
                    if (orderEntity.Order_Date != null)
                    {
                        dt = Convert.ToDateTime(orderEntity.Order_Date);
                        OrderDate = dt.ToString("dd/MMM/yyyy");
                        OrderDateAtt.Value = OrderDate;
                    }
                    else
                        OrderDateAtt.Value = string.Empty;
                    XTemp.SetAttributeNode(OrderDateAtt);

                    XmlAttribute OrderCod = XDoc.CreateAttribute("OrderCode");
                    OrderCod.Value = orderEntity.Order_Code; ;
                    XTemp.SetAttributeNode(OrderCod);

                    XmlAttribute status = XDoc.CreateAttribute("Status");
                    status.Value = orderEntity.Status.StatusName;
                    XTemp.SetAttributeNode(status);

                    XmlAttribute Title = XDoc.CreateAttribute("Title");
                    Title.Value = orderEntity.Estimate_Name;
                    XTemp.SetAttributeNode(Title);

                    XElemRoot.AppendChild(XTemp);

                    foreach (Item items in orderEntity.Items)
                    {
                        if (items.ItemType == 2)
                        {
                            XmlAttribute deliverycost = XDoc.CreateAttribute("DeliveryCost");
                            deliverycost.Value = Convert.ToString(items.Qty1GrossTotal ?? 0);
                            XTemp.SetAttributeNode(deliverycost);
                            XElemRoot.AppendChild(XTemp);
                        }
                    }

                    XmlElement XCompanyTemp = XDoc.CreateElement("Company");

                    XmlAttribute CredAttr = XDoc.CreateAttribute("CreditReference");
                    if (!string.IsNullOrEmpty(orderEntity.Company.CreditReference))
                        CredAttr.Value = orderEntity.Company.CreditReference;
                    else
                        CredAttr.Value = string.Empty;
                    XCompanyTemp.SetAttributeNode(CredAttr);


                    XmlAttribute CorpHomeAttr = XDoc.CreateAttribute("CorporateHomeURL");
                    if (!string.IsNullOrEmpty(orderEntity.Company.RedirectWebstoreURL))
                        CorpHomeAttr.Value = orderEntity.Company.RedirectWebstoreURL;
                    else
                        CorpHomeAttr.Value = string.Empty;
                    XCompanyTemp.SetAttributeNode(CorpHomeAttr);

                    XmlAttribute LinkAttr = XDoc.CreateAttribute("LinkedInURL");
                    if (!string.IsNullOrEmpty(orderEntity.Company.LinkedinURL))
                        LinkAttr.Value = orderEntity.Company.LinkedinURL;
                    else
                        LinkAttr.Value = string.Empty;
                    XCompanyTemp.SetAttributeNode(LinkAttr);

                    XmlAttribute TwitAttr = XDoc.CreateAttribute("TwitterURL");
                    if (!string.IsNullOrEmpty(orderEntity.Company.TwitterURL))
                        TwitAttr.Value = orderEntity.Company.TwitterURL;
                    else
                        TwitAttr.Value = string.Empty;
                    XCompanyTemp.SetAttributeNode(TwitAttr);


                    XmlAttribute FaceAttr = XDoc.CreateAttribute("FacebookURL");
                    if (!string.IsNullOrEmpty(orderEntity.Company.FacebookURL))
                        FaceAttr.Value = orderEntity.Company.FacebookURL;
                    else
                        FaceAttr.Value = string.Empty;
                    XCompanyTemp.SetAttributeNode(FaceAttr);

                    XmlAttribute WebUrlAttr = XDoc.CreateAttribute("WebURL");
                    if (!string.IsNullOrEmpty(orderEntity.Company.URL))
                        WebUrlAttr.Value = orderEntity.Company.URL;
                    else
                        WebUrlAttr.Value = string.Empty;
                    XCompanyTemp.SetAttributeNode(WebUrlAttr);




                    XmlAttribute AccBalAttr = XDoc.CreateAttribute("AccountBalance");
                    if (orderEntity.Company.AccountBalance != null)
                        AccBalAttr.Value = Convert.ToString(orderEntity.Company.AccountBalance);
                    else
                        AccBalAttr.Value = string.Empty;
                    XCompanyTemp.SetAttributeNode(AccBalAttr);


                    XmlAttribute VatRefAttr = XDoc.CreateAttribute("VATRefNumber");
                    if (!string.IsNullOrEmpty(orderEntity.Company.VATRegReference))
                        VatRefAttr.Value = orderEntity.Company.VATRegReference;
                    else
                        VatRefAttr.Value = string.Empty;
                    XCompanyTemp.SetAttributeNode(VatRefAttr);


                    XmlAttribute VatRegAttr = XDoc.CreateAttribute("VATRegNumber");
                    if (!string.IsNullOrEmpty(orderEntity.Company.VATRegNumber))
                        VatRegAttr.Value = orderEntity.Company.VATRegNumber;
                    else
                        VatRegAttr.Value = string.Empty;
                    XCompanyTemp.SetAttributeNode(VatRegAttr);


                    XmlAttribute CreditLimitAttr = XDoc.CreateAttribute("CreditLimit");
                    if (orderEntity.Company.CreditLimit != null)
                        CreditLimitAttr.Value = Convert.ToString(orderEntity.Company.CreditLimit);
                    else
                        CreditLimitAttr.Value = string.Empty;
                    XCompanyTemp.SetAttributeNode(CreditLimitAttr);

                    XmlAttribute NotesAttr = XDoc.CreateAttribute("Notes");
                    if (!string.IsNullOrEmpty(orderEntity.Company.Notes))
                        NotesAttr.Value = orderEntity.Company.Notes;
                    else
                        NotesAttr.Value = string.Empty;
                    XCompanyTemp.SetAttributeNode(NotesAttr);


                    int CustTypeS = orderEntity.Company.IsCustomer;
                    if (CustTypeS == 3)
                        isCorporate = true;

                    if (isCorporate)
                    {

                        XmlAttribute WebAccessAttr = XDoc.CreateAttribute("WebAccessCode");
                        if (!string.IsNullOrEmpty(orderEntity.Company.WebAccessCode))
                            WebAccessAttr.Value = orderEntity.Company.WebAccessCode;
                        else
                            WebAccessAttr.Value = string.Empty;
                        XCompanyTemp.SetAttributeNode(WebAccessAttr);

                    }



                    XmlAttribute AccAtNoAttr = XDoc.CreateAttribute("AccountNo");
                    if (!string.IsNullOrEmpty(orderEntity.Company.AccountNumber))
                        AccAtNoAttr.Value = orderEntity.Company.AccountNumber;
                    else
                        AccAtNoAttr.Value = string.Empty;
                    XCompanyTemp.SetAttributeNode(AccAtNoAttr);

                    XmlAttribute phneAttr = XDoc.CreateAttribute("Phone");
                    if (!string.IsNullOrEmpty(orderEntity.Company.PhoneNo))
                        phneAttr.Value = orderEntity.Company.PhoneNo;
                    else
                        phneAttr.Value = string.Empty;
                    XCompanyTemp.SetAttributeNode(phneAttr);



                    int CustType = orderEntity.Company.IsCustomer;
                    string CustomerType = string.Empty;
                    if (CustType == 1)
                        CustomerType = "Retail Customer";
                    else if (CustType == 2)
                        CustomerType = "Supplier";
                    else if (CustType == 3)
                    {
                        CustomerType = "Corporate Store";
                        isCorporate = true;
                    }
                    else if (CustType == 4)
                    {
                        CustomerType = "Retail Store";
                    }

                    XmlAttribute typeAttr = XDoc.CreateAttribute("Type");
                    typeAttr.Value = CustomerType;
                    XCompanyTemp.SetAttributeNode(typeAttr);


                    XmlAttribute comp = XDoc.CreateAttribute("Name");
                    if (orderEntity.Company != null)
                        comp.Value = orderEntity.Company.Name;
                    else
                        comp.Value = string.Empty;
                    XCompanyTemp.SetAttributeNode(comp);


                    XElemRoot.AppendChild(XCompanyTemp);

                    XmlElement XContactTemp = XDoc.CreateElement("CompanyContact");

                    XmlAttribute cSkypeAtt = XDoc.CreateAttribute("SkypeID");
                    if (!string.IsNullOrEmpty(orderEntity.CompanyContact.SkypeId))
                        cSkypeAtt.Value = orderEntity.CompanyContact.SkypeId;
                    else
                        cSkypeAtt.Value = "";
                    XContactTemp.SetAttributeNode(cSkypeAtt);

                    XmlAttribute cFaxAtt = XDoc.CreateAttribute("FAX");
                    if (!string.IsNullOrEmpty(orderEntity.CompanyContact.FAX))
                        cFaxAtt.Value = orderEntity.CompanyContact.FAX;
                    else
                        cFaxAtt.Value = "";
                    XContactTemp.SetAttributeNode(cFaxAtt);


                    XmlAttribute cDirectAtt = XDoc.CreateAttribute("DirectLine");
                    if (!string.IsNullOrEmpty(orderEntity.CompanyContact.HomeTel1))
                        cDirectAtt.Value = orderEntity.CompanyContact.HomeTel1;
                    else
                        cDirectAtt.Value = "";
                    XContactTemp.SetAttributeNode(cDirectAtt);


                    XmlAttribute cCellAtt = XDoc.CreateAttribute("CellNumbber");
                    if (!string.IsNullOrEmpty(orderEntity.CompanyContact.Mobile))
                        cCellAtt.Value = orderEntity.CompanyContact.Mobile;
                    else
                        cCellAtt.Value = "";
                    XContactTemp.SetAttributeNode(cCellAtt);


                    XmlAttribute cJobAtt = XDoc.CreateAttribute("JobTitle");
                    if (!string.IsNullOrEmpty(orderEntity.CompanyContact.JobTitle))
                        cJobAtt.Value = orderEntity.CompanyContact.JobTitle;
                    else
                        cJobAtt.Value = "";
                    XContactTemp.SetAttributeNode(cJobAtt);



                    XmlAttribute cEmailAtt = XDoc.CreateAttribute("Email");
                    cEmailAtt.Value = orderEntity.CompanyContact.Email;
                    XContactTemp.SetAttributeNode(cEmailAtt);



                    XmlAttribute contName = XDoc.CreateAttribute("Name");
                    string FullName = orderEntity.CompanyContact.FirstName + " " + orderEntity.CompanyContact.LastName;
                    contName.Value = FullName;
                    XContactTemp.SetAttributeNode(contName);


                    XElemRoot.AppendChild(XContactTemp);
                    // att area contact

                    XmlElement XAddressTemp = XDoc.CreateElement("ShippingAddress");

                    //  orderEntity.tbl_contacts.tbl_addresses.pos
                    Address contAddress = orderEntity.CompanyContact.Address;

                    XmlAttribute CountAttr = XDoc.CreateAttribute("Country");
                    if (orderEntity.CompanyContact.Address != null)
                    {
                        if (contAddress.Country != null)
                            CountAttr.Value = contAddress.Country.CountryName;
                        else
                            CountAttr.Value = string.Empty;
                    }
                    else
                    {
                        CountAttr.Value = string.Empty;
                    }
                    XAddressTemp.SetAttributeNode(CountAttr);

                    XmlAttribute StateAttr = XDoc.CreateAttribute("State");
                    if (contAddress.State != null)
                    {
                        if (!string.IsNullOrEmpty(contAddress.State.StateName))
                            StateAttr.Value = contAddress.State.StateName;
                        else
                            StateAttr.Value = string.Empty;
                    }
                    else
                    {
                        StateAttr.Value = string.Empty;
                    }
                    XAddressTemp.SetAttributeNode(StateAttr);


                    XmlAttribute CityAttr = XDoc.CreateAttribute("City");
                    if (contAddress != null)
                    {
                        if (!string.IsNullOrEmpty(contAddress.City))
                            CityAttr.Value = contAddress.City;
                        else
                            CityAttr.Value = string.Empty;
                    }
                    else
                    {
                        CityAttr.Value = string.Empty;
                    }
                    XAddressTemp.SetAttributeNode(CityAttr);

                    XmlAttribute PostAttr = XDoc.CreateAttribute("PostCode");
                    if (contAddress != null)
                    {
                        if (!string.IsNullOrEmpty(contAddress.PostCode))
                            PostAttr.Value = contAddress.PostCode;
                        else
                            PostAttr.Value = string.Empty;
                    }
                    else
                    {
                        PostAttr.Value = string.Empty;
                    }
                    XAddressTemp.SetAttributeNode(PostAttr);

                    XmlAttribute AddName = XDoc.CreateAttribute("Address");
                    if (contAddress != null)
                        AddName.Value = contAddress.Address1 + " " + contAddress.Address2;
                    XAddressTemp.SetAttributeNode(AddName);

                    XElemRoot.AppendChild(XAddressTemp);
                    // att area address

                    if (orderEntity.BillingAddressId != null)
                    {
                        XmlElement XBillingAddressTemp = XDoc.CreateElement("BillingAddress");

                        Address adress = addressRepository.GetAddressByIdforXML(orderEntity.BillingAddressId ?? 0);

                        XmlAttribute CountaAttr = XDoc.CreateAttribute("Country");
                        if (adress != null)
                        {
                            if (adress.Country != null)
                                CountaAttr.Value = adress.Country.CountryName;
                            else
                                CountaAttr.Value = string.Empty;
                        }
                        else
                        {
                            CountaAttr.Value = string.Empty;
                        }
                        XBillingAddressTemp.SetAttributeNode(CountaAttr);


                        XmlAttribute StateaAttr = XDoc.CreateAttribute("State");
                        if (adress != null)
                        {
                            if (adress.State != null)
                                StateaAttr.Value = adress.State.StateName;
                            else
                                StateaAttr.Value = string.Empty;
                        }
                        else
                        {
                            StateaAttr.Value = string.Empty;
                        }
                        XBillingAddressTemp.SetAttributeNode(StateaAttr);


                        XmlAttribute CityaAttr = XDoc.CreateAttribute("City");
                        if (adress != null)
                        {
                            if (!string.IsNullOrEmpty(adress.City))
                                CityaAttr.Value = adress.City;
                            else
                                CityaAttr.Value = string.Empty;
                        }
                        else
                        {
                            CityaAttr.Value = string.Empty;
                        }
                        XBillingAddressTemp.SetAttributeNode(CityaAttr);



                        XmlAttribute PostaAttr = XDoc.CreateAttribute("PostCode");
                        if (adress != null)
                        {
                            if (!string.IsNullOrEmpty(adress.PostCode))
                                PostaAttr.Value = adress.PostCode;
                            else
                                PostaAttr.Value = string.Empty;
                        }
                        else
                        {
                            PostaAttr.Value = string.Empty;
                        }
                        XBillingAddressTemp.SetAttributeNode(PostaAttr);

                        XmlAttribute AddaName = XDoc.CreateAttribute("Address");
                        if (adress != null)
                            AddaName.Value = adress.Address1 + " " + adress.Address2;
                        else
                            AddaName.Value = string.Empty;
                        XBillingAddressTemp.SetAttributeNode(AddaName);

                        XElemRoot.AppendChild(XBillingAddressTemp);
                    }


                    XmlElement ItemElemRoot = XDoc.CreateElement("Items");
                    XElemRoot.AppendChild(ItemElemRoot);

                    foreach (Item items in orderEntity.Items)
                    {
                        if (items.ItemType != 2)
                        {

                            //  string ItemCount = "ItemNo " + itemsCount;
                            XmlElement ItemNoElemRoot = XDoc.CreateElement("Item");

                            string qtyMarkup = string.Empty;
                            if (items.Qty1MarkUp1Value != null)
                            {
                                qtyMarkup = Convert.ToString(items.Qty1MarkUp1Value);
                                if (qtyMarkup.StartsWith("-"))
                                {
                                    XmlAttribute DiscAttr = XDoc.CreateAttribute("Discount");

                                    DiscAttr.Value = Convert.ToString(items.Qty1MarkUp1Value);
                                    ItemNoElemRoot.SetAttributeNode(DiscAttr);
                                }
                            }

                            XmlAttribute GrandAttr = XDoc.CreateAttribute("GrandTotal");
                            string grandtotal = string.Empty;
                            if (items.Qty1GrossTotal != null)
                                grandtotal = CurrencySymbol + Convert.ToString(items.Qty1GrossTotal);
                            GrandAttr.Value = grandtotal;
                            ItemNoElemRoot.SetAttributeNode(GrandAttr);

                            XmlAttribute VatAttr = XDoc.CreateAttribute("VAT");
                            string VAT = string.Empty;
                            if (items.Qty1Tax1Value != null)
                                VAT = CurrencySymbol + Convert.ToString(items.Qty1Tax1Value);
                            VatAttr.Value = VAT;
                            ItemNoElemRoot.SetAttributeNode(VatAttr);

                            XmlAttribute netAttr = XDoc.CreateAttribute("NetTotal");
                            string nettotal = string.Empty;
                            if (items.Qty1NetTotal != null)
                                nettotal = CurrencySymbol + Convert.ToString(items.Qty1NetTotal);
                            netAttr.Value = nettotal;
                            ItemNoElemRoot.SetAttributeNode(netAttr);


                            XmlAttribute qtyAttr = XDoc.CreateAttribute("Quantity");
                            string qty = string.Empty;
                            if (items.Qty1 != null)
                                qty = Convert.ToString(items.Qty1);
                            qtyAttr.Value = qty;
                            ItemNoElemRoot.SetAttributeNode(qtyAttr);


                            if (items.Status != null)
                            {
                                if (items.Status.StatusId != 17)
                                {

                                    XmlAttribute JoAttr = XDoc.CreateAttribute("JobCode");
                                    JoAttr.Value = items.JobCode;
                                    ItemNoElemRoot.SetAttributeNode(JoAttr);
                                }

                            }

                            XmlAttribute codeAttr = XDoc.CreateAttribute("ItemCode");
                            codeAttr.Value = items.ItemCode;
                            ItemNoElemRoot.SetAttributeNode(codeAttr);


                            string stat = string.Empty;
                            if (items.Status != null)
                                stat = items.Status.StatusName;
                            else
                                stat = "N/A";

                            XmlAttribute JobStatus = XDoc.CreateAttribute("Status");
                            JobStatus.Value = stat;
                            ItemNoElemRoot.SetAttributeNode(JobStatus);


                            string ProductFullName = items.ProductName;


                            XmlAttribute Prod = XDoc.CreateAttribute("ProductName");
                            Prod.Value = ProductFullName;
                            ItemNoElemRoot.SetAttributeNode(Prod);

                            // ItemXTemp.InnerText = ProductFullName;
                            ItemElemRoot.AppendChild(ItemNoElemRoot);
                            // attr area items


                            XmlElement ItemXTemp = XDoc.CreateElement("JobDescriptions1");
                            string JobDescription1 = string.Empty;
                            JobDescription1 = items.JobDescriptionTitle1 + ": " + items.JobDescription1;
                            if (JobDescription1 != ": ")
                                ItemXTemp.InnerText = JobDescription1;
                            else
                                ItemXTemp.InnerText = "N/A";
                            ItemNoElemRoot.AppendChild(ItemXTemp);

                            ItemXTemp = XDoc.CreateElement("JobDescriptions2");
                            string JobDescription2 = string.Empty;
                            JobDescription2 = items.JobDescriptionTitle2 + ": " + items.JobDescription2;
                            if (JobDescription2 != ": ")
                                ItemXTemp.InnerText = JobDescription2;
                            else
                                ItemXTemp.InnerText = "N/A";
                            ItemNoElemRoot.AppendChild(ItemXTemp);


                            ItemXTemp = XDoc.CreateElement("JobDescriptions3");
                            string JobDescription3 = string.Empty;
                            JobDescription3 = items.JobDescriptionTitle3 + ": " + items.JobDescription3;
                            if (JobDescription3 != ": ")
                                ItemXTemp.InnerText = JobDescription3;
                            else
                                ItemXTemp.InnerText = "N/A";
                            ItemNoElemRoot.AppendChild(ItemXTemp);



                            ItemXTemp = XDoc.CreateElement("JobDescriptions4");
                            string JobDescription4 = string.Empty;
                            JobDescription4 = items.JobDescriptionTitle4 + ": " + items.JobDescription4;
                            if (JobDescription4 != ": ")
                                ItemXTemp.InnerText = JobDescription4;
                            else
                                ItemXTemp.InnerText = "N/A"; ;
                            ItemNoElemRoot.AppendChild(ItemXTemp);

                            ItemXTemp = XDoc.CreateElement("JobDescriptions5");
                            string JobDescription5 = string.Empty;
                            JobDescription5 = items.JobDescriptionTitle5 + ": " + items.JobDescription5;
                            if (JobDescription5 != ": ")
                                ItemXTemp.InnerText = JobDescription5;
                            else
                                ItemXTemp.InnerText = "N/A"; ;
                            ItemNoElemRoot.AppendChild(ItemXTemp);

                            ItemXTemp = XDoc.CreateElement("JobDescriptions6");
                            string JobDescription6 = string.Empty;
                            JobDescription6 = items.JobDescriptionTitle6 + ": " + items.JobDescription6;
                            if (JobDescription6 != ": ")
                                ItemXTemp.InnerText = JobDescription6;
                            else
                                ItemXTemp.InnerText = "N/A"; ;
                            ItemNoElemRoot.AppendChild(ItemXTemp);

                            ItemXTemp = XDoc.CreateElement("JobDescriptions7");
                            string JobDescription7 = string.Empty;
                            JobDescription7 = items.JobDescriptionTitle7 + ": " + items.JobDescription7;
                            if (JobDescription7 != ": ")
                                ItemXTemp.InnerText = JobDescription7;
                            else
                                ItemXTemp.InnerText = "N/A"; ;
                            ItemNoElemRoot.AppendChild(ItemXTemp);


                            ItemXTemp = XDoc.CreateElement("InvoiceDescription");
                            string invDesc = string.Empty;
                            invDesc = items.InvoiceDescription;
                            if (!string.IsNullOrEmpty(invDesc))
                                ItemXTemp.InnerText = invDesc;
                            else
                                ItemXTemp.InnerText = "N/A";
                            ItemNoElemRoot.AppendChild(ItemXTemp);


                            if (items.ItemAttachments != null)
                            {
                                if (items.ItemAttachments.Count > 0)
                                {
                                    XmlElement ItemXTempAttachments = XDoc.CreateElement("Attachments");
                                    ItemNoElemRoot.AppendChild(ItemXTempAttachments);
                                    foreach (var v in items.ItemAttachments)
                                    {
                                        XmlElement AttachmentXTemp = XDoc.CreateElement("Attachment");
                                        XmlAttribute AttributeAtt = XDoc.CreateAttribute("Name");
                                        AttributeAtt.Value = v.FileName;
                                        AttachmentXTemp.SetAttributeNode(AttributeAtt);
                                        ItemXTempAttachments.AppendChild(AttachmentXTemp);

                                    }
                                }
                            }


                            XmlElement SectionsElemRoot = XDoc.CreateElement("ItemSections");
                            ItemNoElemRoot.AppendChild(SectionsElemRoot);

                            if (items.ItemSections != null)
                            {
                                if (items.ItemSections.Count > 0)
                                {
                                    foreach (var s in items.ItemSections)
                                    {
                                        XmlElement SectionsXTemp = XDoc.CreateElement("Section");
                                        SectionsElemRoot.AppendChild(SectionsXTemp);


                                        XmlAttribute platesAttr = XDoc.CreateAttribute("Plates");
                                        if (s.Side1PlateQty != null)
                                        {
                                            platesAttr.Value = Convert.ToString(s.Side1PlateQty);

                                        }
                                        else
                                            platesAttr.Value = "N/A";
                                        SectionsXTemp.SetAttributeNode(platesAttr);



                                        XmlAttribute incPlate = XDoc.CreateAttribute("IsPlateSupplied");
                                        if (s.IsPlateSupplied == true)
                                        {
                                            incPlate.Value = "True";

                                        }
                                        else
                                            incPlate.Value = "False";
                                        SectionsXTemp.SetAttributeNode(incPlate);



                                        XmlAttribute incGutter = XDoc.CreateAttribute("IncludeGutter");
                                        if (s.IncludeGutter == true)
                                        {
                                            incGutter.Value = "True";

                                        }
                                        else
                                            incGutter.Value = "False";
                                        SectionsXTemp.SetAttributeNode(incGutter);


                                        XmlAttribute PaperSupplied = XDoc.CreateAttribute("IsPaperSupplied");
                                        if (s.IsPaperSupplied == true)
                                        {
                                            PaperSupplied.Value = "True";

                                        }
                                        else
                                            PaperSupplied.Value = "False";
                                        SectionsXTemp.SetAttributeNode(PaperSupplied);


                                        int InkID = 0;
                                        if (s.PlateInkId != null)
                                            InkID = (int)s.PlateInkId;

                                        string InkString = MachineRepository.GetInkPlatesSidesByInkID(InkID);
                                        XmlAttribute inkAttr = XDoc.CreateAttribute("Ink");
                                        if (!string.IsNullOrEmpty(InkString))
                                            inkAttr.Value = InkString;
                                        else
                                            inkAttr.Value = "N/A";
                                        SectionsXTemp.SetAttributeNode(inkAttr);



                                        bool iscustome = false;
                                        bool isItemSizeCustom = false;

                                        XmlAttribute itemSizAttr = XDoc.CreateAttribute("ItemSizeCustom");
                                        if (s.IsItemSizeCustom == true)
                                        {
                                            itemSizAttr.Value = "True";
                                            isItemSizeCustom = true;
                                        }
                                        else
                                            itemSizAttr.Value = "False";
                                        SectionsXTemp.SetAttributeNode(itemSizAttr);


                                        XmlAttribute SecSizAttr = XDoc.CreateAttribute("SectionSizeCustom");
                                        if (s.IsSectionSizeCustom == true)
                                        {
                                            SecSizAttr.Value = "True";
                                            iscustome = true;
                                        }
                                        else
                                            SecSizAttr.Value = "False";
                                        SectionsXTemp.SetAttributeNode(SecSizAttr);


                                        if (iscustome)
                                        {

                                            XmlAttribute secHeightAttr = XDoc.CreateAttribute("SectionSizeHeight");
                                            if (s.SectionSizeHeight != null)
                                                secHeightAttr.Value = Convert.ToString(s.SectionSizeHeight);
                                            else
                                                secHeightAttr.Value = "";
                                            SectionsXTemp.SetAttributeNode(secHeightAttr);

                                            XmlAttribute secwidthAttr = XDoc.CreateAttribute("SectionSizeWidth");
                                            if (s.SectionSizeWidth != null)
                                                secwidthAttr.Value = Convert.ToString(s.SectionSizeWidth);
                                            else
                                                secwidthAttr.Value = "";
                                            SectionsXTemp.SetAttributeNode(secwidthAttr);

                                        }
                                        else
                                        {
                                            string PaperString = string.Empty;
                                            int PSSID = 0;
                                            if (s.SectionSizeId != null)
                                                PSSID = (int)s.SectionSizeId;

                                            // N Chance

                                            var data = paperSizeRepository.GetPaperSizesByID(PSSID);

                                            if (data != null)
                                            {
                                                foreach (var v in data)
                                                {
                                                    PaperString = v.Name + " " + v.Height + "x" + v.Width;
                                                }

                                            }
                                            XmlAttribute secwidthssAttr = XDoc.CreateAttribute("SectionSize");
                                            if (!string.IsNullOrEmpty(PaperString))
                                                secwidthssAttr.Value = PaperString;
                                            else
                                                secwidthssAttr.Value = "";
                                            SectionsXTemp.SetAttributeNode(secwidthssAttr);


                                        }


                                        if (isItemSizeCustom)
                                        {
                                            XmlAttribute itemSizHeightAttr = XDoc.CreateAttribute("ItemSizeHeight");
                                            if (s.ItemSizeHeight != null)
                                                itemSizHeightAttr.Value = Convert.ToString(s.ItemSizeHeight);
                                            else
                                                itemSizHeightAttr.Value = "N/A";
                                            SectionsXTemp.SetAttributeNode(itemSizHeightAttr);

                                            XmlAttribute itemSizwidthAttr = XDoc.CreateAttribute("ItemSizeWidth");
                                            if (s.ItemSizeWidth != null)
                                                itemSizwidthAttr.Value = Convert.ToString(s.ItemSizeWidth);
                                            else
                                                itemSizwidthAttr.Value = "N/A";
                                            SectionsXTemp.SetAttributeNode(itemSizwidthAttr);

                                        }
                                        else
                                        {


                                            string PaperSString = string.Empty;
                                            int PSID = 0;
                                            if (s.ItemSizeId != null)
                                                PSID = (int)s.ItemSizeId;

                                            var data2 = paperSizeRepository.GetPaperSizesByID(PSID);

                                            if (data2 != null)
                                            {
                                                foreach (var v in data2)
                                                {
                                                    PaperSString = v.Name + " " + v.Height + "x" + v.Width;
                                                }

                                            }

                                            // N chance

                                            XmlAttribute itemSizeAttr = XDoc.CreateAttribute("ItemSize");
                                            if (!string.IsNullOrEmpty(PaperSString))
                                                itemSizeAttr.Value = PaperSString;
                                            else
                                                itemSizeAttr.Value = "N/A";
                                            SectionsXTemp.SetAttributeNode(itemSizeAttr);

                                        }


                                        XmlAttribute GuiloAttr = XDoc.CreateAttribute("Guillotin");

                                        int GID = 0;
                                        if (s.GuillotineId != null)
                                            GID = (int)s.GuillotineId;
                                        string GuillotinName = MachineRepository.GetMachineByID(GID);
                                        if (!string.IsNullOrEmpty(GuillotinName))
                                            GuiloAttr.Value = GuillotinName;
                                        else
                                            GuiloAttr.Value = string.Empty;
                                        SectionsXTemp.SetAttributeNode(GuiloAttr);




                                        XmlAttribute StockAttr = XDoc.CreateAttribute("Stock");

                                        int StockID = 0;
                                        if (s.StockItemID1 != null)
                                            StockID = (int)s.StockItemID1;
                                        string StockName = stockItemRepository.GetStockName(StockID);
                                        if (!string.IsNullOrEmpty(StockName))
                                            StockAttr.Value = StockName;
                                        else
                                            StockAttr.Value = string.Empty;
                                        SectionsXTemp.SetAttributeNode(StockAttr);


                                        XmlAttribute PressAttr = XDoc.CreateAttribute("Press");
                                        int PID = 0;
                                        if (s.PressId != null)
                                            PID = (int)s.PressId;

                                        string PressName = MachineRepository.GetMachineByID(PID);
                                        if (!string.IsNullOrEmpty(PressName))
                                            PressAttr.Value = PressName;
                                        else
                                            PressAttr.Value = string.Empty;
                                        SectionsXTemp.SetAttributeNode(PressAttr);


                                        XmlAttribute AttributeSec = XDoc.CreateAttribute("Name");
                                        AttributeSec.Value = s.SectionName;
                                        SectionsXTemp.SetAttributeNode(AttributeSec);

                                        SectionsElemRoot.AppendChild(SectionsXTemp);

                                        // attr secComes here


                                        XmlElement SectXTemp = XDoc.CreateElement("Quantities");

                                        XmlAttribute AttributeQty3 = XDoc.CreateAttribute("Quantity3");
                                        if (s.Qty3 != null)
                                            AttributeQty3.Value = Convert.ToString(s.Qty3);
                                        else
                                            AttributeQty3.Value = "0";
                                        SectXTemp.SetAttributeNode(AttributeQty3);


                                        XmlAttribute AttributeQty2 = XDoc.CreateAttribute("Quantity2");
                                        if (s.Qty2 != null)
                                            AttributeQty2.Value = Convert.ToString(s.Qty2);
                                        else
                                            AttributeQty2.Value = "0";
                                        SectXTemp.SetAttributeNode(AttributeQty2);

                                        XmlAttribute AttributeQty1 = XDoc.CreateAttribute("Quantity1");
                                        if (s.Qty1 != null)
                                            AttributeQty1.Value = Convert.ToString(s.Qty1);
                                        else
                                            AttributeQty1.Value = "0";
                                        SectXTemp.SetAttributeNode(AttributeQty1);

                                        SectionsXTemp.AppendChild(SectXTemp);


                                        SectXTemp = XDoc.CreateElement("CostCenterTotals");

                                        double Qty3Total = s.SectionCostcentres.Sum(c => c.Qty3NetTotal ?? 0);
                                        XmlAttribute AttributeCS3 = XDoc.CreateAttribute("Quantity3");
                                        AttributeCS3.Value = CurrencySymbol + Convert.ToString(Qty3Total);
                                        SectXTemp.SetAttributeNode(AttributeCS3);


                                        double Qty2Total = s.SectionCostcentres.Sum(c => c.Qty2NetTotal ?? 0);
                                        XmlAttribute AttributeCS2 = XDoc.CreateAttribute("Quantity2");
                                        AttributeCS2.Value = CurrencySymbol + Convert.ToString(Qty2Total);
                                        SectXTemp.SetAttributeNode(AttributeCS2);


                                        double Qty1Total = s.SectionCostcentres.Sum(c => c.Qty1NetTotal ?? 0);
                                        XmlAttribute AttributeCS1 = XDoc.CreateAttribute("Quantity1");

                                        AttributeCS1.Value = CurrencySymbol + Convert.ToString(Qty1Total);
                                        SectXTemp.SetAttributeNode(AttributeCS1);

                                        SectionsXTemp.AppendChild(SectXTemp);

                                        SectXTemp = XDoc.CreateElement("Signatures");

                                        double Sig3 = 0;
                                        if (s.SimilarSections != null)
                                            Sig3 = (double)s.SimilarSections * Qty3Total;

                                        XmlAttribute AttributeSS3 = XDoc.CreateAttribute("Quantity3");
                                        AttributeSS3.Value = CurrencySymbol + Convert.ToString(Sig3);

                                        SectXTemp.SetAttributeNode(AttributeSS3);


                                        double Sig2 = 0;
                                        if (s.SimilarSections != null)
                                            Sig2 = (double)s.SimilarSections * Qty2Total;
                                        XmlAttribute AttributeSS2 = XDoc.CreateAttribute("Quantity2");
                                        AttributeSS2.Value = CurrencySymbol + Convert.ToString(Sig2);
                                        SectXTemp.SetAttributeNode(AttributeSS2);

                                        double Sig1 = 0;
                                        if (s.SimilarSections != null)
                                            Sig1 = (double)s.SimilarSections * Qty1Total;
                                        XmlAttribute AttributeSS1 = XDoc.CreateAttribute("Quantity1");
                                        AttributeSS1.Value = CurrencySymbol + Convert.ToString(Sig1);

                                        SectXTemp.SetAttributeNode(AttributeSS1);


                                        XmlAttribute SimilarSections = XDoc.CreateAttribute("SimilarSignature");
                                        if (s.SimilarSections != null)
                                            SimilarSections.Value = Convert.ToString(s.SimilarSections);
                                        else
                                            SimilarSections.Value = "0";
                                        SectXTemp.SetAttributeNode(SimilarSections);

                                        SectionsXTemp.AppendChild(SectXTemp);

                                        SectXTemp = XDoc.CreateElement("Markups");

                                        double Mrk1 = 0;
                                        if (s.BaseCharge1 != null)
                                        {
                                            double Add = Qty1Total + Sig1;
                                            Mrk1 = (double)s.BaseCharge1 - Add;
                                        }

                                        double Mrk2 = 0;
                                        if (s.BaseCharge2 != null)
                                        {
                                            double Add = Qty2Total + Sig2;
                                            Mrk2 = (double)s.BaseCharge2 - Add;
                                        }
                                        double Mrk3 = 0;
                                        if (s.Basecharge3 != null)
                                        {
                                            double Add = Qty3Total + Sig3;
                                            Mrk3 = (double)s.Basecharge3 - Add;
                                        }


                                        //string MarkupName3 = ObjectContext.tbl_markup.Where(m => m.MarkUpID == MarkUp3).Select(a => a.MarkUpName).FirstOrDefault();
                                        XmlAttribute AttributeMarkup3 = XDoc.CreateAttribute("Quantity3");

                                        AttributeMarkup3.Value = CurrencySymbol + Convert.ToString(Mrk3);

                                        SectXTemp.SetAttributeNode(AttributeMarkup3);


                                        XmlAttribute AttributeMarkup2 = XDoc.CreateAttribute("Quantity2");
                                        AttributeMarkup2.Value = CurrencySymbol + Convert.ToString(Mrk2);

                                        SectXTemp.SetAttributeNode(AttributeMarkup2);



                                        XmlAttribute AttributeMarkup1 = XDoc.CreateAttribute("Quantity1");
                                        AttributeMarkup1.Value = CurrencySymbol + Convert.ToString(Mrk1);

                                        SectXTemp.SetAttributeNode(AttributeMarkup1);

                                        SectionsXTemp.AppendChild(SectXTemp);

                                        SectXTemp = XDoc.CreateElement("SectionSubTotal");


                                        XmlAttribute AttributesubTotal3 = XDoc.CreateAttribute("Quantity3");
                                        if (s.Basecharge3 != null)
                                            AttributesubTotal3.Value = CurrencySymbol + Convert.ToString(s.Basecharge3);
                                        else
                                            AttributesubTotal3.Value = "0";

                                        SectXTemp.SetAttributeNode(AttributesubTotal3);

                                        XmlAttribute AttributesubTotal2 = XDoc.CreateAttribute("Quantity2");
                                        if (s.BaseCharge2 != null)
                                            AttributesubTotal2.Value = CurrencySymbol + Convert.ToString(s.BaseCharge2);
                                        else
                                            AttributesubTotal2.Value = "0";
                                        SectXTemp.SetAttributeNode(AttributesubTotal2);

                                        XmlAttribute AttributesubTotal1 = XDoc.CreateAttribute("Quantity1");
                                        if (s.BaseCharge1 != null)
                                            AttributesubTotal1.Value = CurrencySymbol + Convert.ToString(s.BaseCharge1);
                                        else
                                            AttributesubTotal1.Value = "0";

                                        SectXTemp.SetAttributeNode(AttributesubTotal1);


                                        SectionsXTemp.AppendChild(SectXTemp);



                                        if (s.SectionCostcentres != null)
                                        {
                                            List<long> CostCenterIDs = s.SectionCostcentres.Select(v => v.CostCentreId ?? 0).ToList();
                                            if (CostCenterIDs != null && CostCenterIDs.Count > 0)
                                            {
                                                var ProductData = CostCentreRepository.GetCostCentresforxml(CostCenterIDs);


                                                XmlElement SectionCostCenterElemRoot = XDoc.CreateElement("SectionCostCenters");
                                                SectionsXTemp.AppendChild(SectionCostCenterElemRoot);

                                                foreach (var cc in ProductData)
                                                {

                                                    XmlElement SCCNoXTemp = XDoc.CreateElement("CostCenter");
                                                    // SCCXTemp.InnerText = SupplierName;
                                                    SectionCostCenterElemRoot.AppendChild(SCCNoXTemp);


                                                    double nettot = s.SectionCostcentres.Where(q => q.CostCentreId == cc.CostCentreId).Select(w => w.Qty1NetTotal ?? 0).FirstOrDefault();
                                                    XmlAttribute NetAttr = XDoc.CreateAttribute("NetTotal");
                                                    NetAttr.InnerText = CurrencySymbol + Convert.ToString(nettot);
                                                    SCCNoXTemp.SetAttributeNode(NetAttr);

                                                    double markupVal = s.SectionCostcentres.Where(q => q.CostCentreId == cc.CostCentreId).Select(w => w.Qty1MarkUpValue ?? 0).FirstOrDefault();
                                                    XmlAttribute MarkUpValAttr = XDoc.CreateAttribute("MarkupValue");
                                                    MarkUpValAttr.Value = CurrencySymbol + Convert.ToString(markupVal);
                                                    SCCNoXTemp.SetAttributeNode(MarkUpValAttr);



                                                    int MID = s.SectionCostcentres.Where(q => q.CostCentreId == cc.CostCentreId).Select(w => w.Qty1MarkUpID ?? 0).FirstOrDefault();
                                                    string MarkName = _markupRepository.GetMarkupNamebyID(MID);
                                                    XmlAttribute MarkUpAttr = XDoc.CreateAttribute("Markup");
                                                    if (!string.IsNullOrEmpty(MarkName))
                                                        MarkUpAttr.Value = MarkName;
                                                    else
                                                        MarkUpAttr.Value = "N/A";
                                                    SCCNoXTemp.SetAttributeNode(MarkUpAttr);


                                                    double QtyChrge = s.SectionCostcentres.Where(q => q.CostCentreId == cc.CostCentreId).Select(w => w.Qty1Charge ?? 0).FirstOrDefault();
                                                    XmlAttribute PriceAttr = XDoc.CreateAttribute("Price");
                                                    if (QtyChrge > 0)
                                                        PriceAttr.Value = CurrencySymbol + Convert.ToString(QtyChrge);
                                                    else
                                                        PriceAttr.Value = "0";
                                                    SCCNoXTemp.SetAttributeNode(PriceAttr);

                                                    double EstimateTime = s.SectionCostcentres.Where(q => q.CostCentreId == cc.CostCentreId).Select(w => w.Qty1EstimatedTime).FirstOrDefault();
                                                    XmlAttribute EstimAttr = XDoc.CreateAttribute("EstimateProductionTime");
                                                    if (EstimateTime > 0)
                                                        EstimAttr.Value = Convert.ToString(EstimateTime);
                                                    else
                                                        EstimAttr.Value = "0";
                                                    SCCNoXTemp.SetAttributeNode(EstimAttr);

                                                    string SupplierName = companyRepository.GetSupplierNameByID(cc.PreferredSupplierId ?? 0);
                                                    XmlAttribute SuppNameAttr = XDoc.CreateAttribute("SupplierName");
                                                    if (!string.IsNullOrEmpty(SupplierName))
                                                        SuppNameAttr.Value = SupplierName;
                                                    else
                                                        SuppNameAttr.Value = "";
                                                    SCCNoXTemp.SetAttributeNode(SuppNameAttr);


                                                    XmlAttribute AttributeCostCenter = XDoc.CreateAttribute("Name");
                                                    AttributeCostCenter.Value = cc.Name;
                                                    SCCNoXTemp.SetAttributeNode(AttributeCostCenter);

                                                    SectionCostCenterElemRoot.AppendChild(SCCNoXTemp);


                                                    string WorkInstruction = s.SectionCostcentres.Where(q => q.CostCentreId == cc.CostCentreId).Select(w => w.Qty1WorkInstructions ?? "N/A").FirstOrDefault();
                                                    XmlElement SCCXTemp = XDoc.CreateElement("WorkInstruction");
                                                    SCCXTemp.InnerText = WorkInstruction;
                                                    SCCNoXTemp.AppendChild(SCCXTemp);

                                                }
                                            }
                                        }


                                    }
                                }
                            }

                        }
                    }
                    // end of att area items

                    if (paymentsList != null && paymentsList.Count > 0)
                    {

                        XmlElement ItemElemRootPayment = XDoc.CreateElement("Payments");
                        XElemRoot.AppendChild(ItemElemRootPayment);


                        foreach (var payment in paymentsList)
                        {
                            XmlElement PaymentXTemp = XDoc.CreateElement("Payment");


                            string reference = PayPalRepsoitory.GetPayPalReference(iRecordID);
                            //select top 1 transactionid from tbl_paypalResponses where orderid = tbl_estimates.estimateid
                            XmlAttribute cRef = XDoc.CreateAttribute("Reference");
                            if (payment.PaymentMethodId == 1)
                                cRef.Value = reference;
                            else
                            {
                                if (payment.ReferenceCode != null)
                                    cRef.Value = payment.ReferenceCode;
                                else
                                    cRef.Value = "";
                            }

                            PaymentXTemp.SetAttributeNode(cRef);

                            XmlAttribute cAmount = XDoc.CreateAttribute("Amount");
                            if (payment.Amount != null)
                                cAmount.Value = CurrencySymbol + Convert.ToString(payment.Amount);
                            else
                                cAmount.Value = "";


                            PaymentXTemp.SetAttributeNode(cAmount);



                            DateTime dtPayment = new DateTime();
                            string paymentDate = string.Empty;
                            XmlAttribute cPDate = XDoc.CreateAttribute("Date");
                            if (payment.PaymentDate != null)
                            {
                                dtPayment = Convert.ToDateTime(payment.PaymentDate);
                                paymentDate = dtPayment.ToString("dd/MMM/yyyy");
                                cPDate.Value = paymentDate;
                            }
                            else
                                cPDate.Value = string.Empty;
                            PaymentXTemp.SetAttributeNode(cPDate);


                            XmlAttribute cPType = XDoc.CreateAttribute("Type");
                            if (payment.PaymentMethodId == 1)
                                cPType.Value = "Paypal";
                            else
                                cPType.Value = "On Account";
                            PaymentXTemp.SetAttributeNode(cPType);

                            ItemElemRootPayment.AppendChild(PaymentXTemp);


                        }
                    }

                    // att area payment

                    string sFileName = orderEntity.Order_Code + "_" + "OrderXML.xml";
                    // FileNamesList.Add(sFileName);
                    string Path = HttpContext.Current.Server.MapPath("~/" + ImagePathConstants.ReportPath + OrganisationID);
                    if (!Directory.Exists(Path))
                    {
                        Directory.CreateDirectory(Path);
                    }
                    sFilePath = Path + "/" + sFileName;
                    XDoc.Save(sFilePath);
                    //db.Configuration.LazyLoadingEnabled = false;
                    //db.Configuration.ProxyCreationEnabled = false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return sFilePath;
        }


        public string ExportExcel(int iReportID, long iRecordID, ReportType type, long OrderID, string CriteriaParam)
        {
            string sFilePath = string.Empty;
            try
            {
                long OrganisationID = 0;
                Organisation org = organisationRepository.GetOrganizatiobByID();
                if (org != null)
                {
                    OrganisationID = org.OrganisationId;
                }
                Report currentReport = ReportRepository.GetReportByReportID(iReportID);
                if (currentReport.ReportId > 0)
                {
                    byte[] rptBytes = null;
                    rptBytes = System.Text.Encoding.Unicode.GetBytes(currentReport.ReportTemplate);
                    // Encoding must be done
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(rptBytes);
                    // Load it to memory stream
                    ms.Position = 0;
                    SectionReport currReport = new SectionReport();
                    string sFileName = iRecordID + "OrderReport.xls";
                    // FileNamesList.Add(sFileName);
                    currReport.LoadLayout(ms);
                    if (type == ReportType.JobCard)
                    {
                        sFileName = iRecordID + "JobCardReport.xls";
                        //  FileNamesList.Add(sFileName);
                        List<usp_JobCardReport_Result> rptSource = ReportRepository.getJobCardReportResult(OrganisationID, OrderID, iRecordID);
                        currReport.DataSource = rptSource;
                    }
                    else if (type == ReportType.Order)
                    {

                        List<usp_OrderReport_Result> rptOrderSource = ReportRepository.getOrderReportResult(OrganisationID, OrderID);
                        currReport.DataSource = rptOrderSource;
                    }
                    else if (type == ReportType.Internal)
                    {
                        string ReportDataSource = string.Empty;
                        string ReportTemplate = string.Empty;

                        DataTable dataSourceList = ReportRepository.GetReportDataSourceByReportID(iReportID, CriteriaParam);
                        currReport.DataSource = dataSourceList;
                    }
                    if (currReport != null)
                    {
                        currReport.Run();
                        GrapeCity.ActiveReports.Export.Excel.Section.XlsExport xls = new GrapeCity.ActiveReports.Export.Excel.Section.XlsExport();
                        string Path = HttpContext.Current.Server.MapPath("~/" + ImagePathConstants.ReportPath + OrganisationID + "/");
                        if (!Directory.Exists(Path))
                        {
                            Directory.CreateDirectory(Path);
                        }
                        // PdfExport pdf = new PdfExport();
                        sFilePath = HttpContext.Current.Server.MapPath("~/" + ImagePathConstants.ReportPath + OrganisationID + "/") + sFileName;

                        xls.Export(currReport.Document, sFilePath);
                        ms.Close();
                        currReport.Document.Dispose();
                        xls.Dispose();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return sFilePath;
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
