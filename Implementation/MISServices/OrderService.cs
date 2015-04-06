using System;
using System.Collections.Generic;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;

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
        
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public OrderService(IEstimateRepository estimateRepository, ISectionFlagRepository sectionFlagRepository, ICompanyContactRepository companyContactRepository,
            IAddressRepository addressRepository, ISystemUserRepository systemUserRepository, IPipeLineSourceRepository pipeLineSourceRepository, IMarkupRepository markupRepository,
            IPaymentMethodRepository paymentMethodRepository, IOrganisationRepository organisationRepository,IStockCategoryRepository stockCategoryRepository, IOrderRepository orderRepository, IItemRepository itemRepository, MPC.Interfaces.WebStoreServices.ITemplateService templateService,
            IChartOfAccountRepository chartOfAccountRepository, IItemSectionRepository itemsectionRepository, IPaperSizeRepository paperSizeRepository, IInkPlateSideRepository inkPlateSideRepository)
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
            this.estimateRepository = estimateRepository;
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
            this.itemRepository = itemRepository;
            this.templateService = templateService;
            this.itemsectionRepository = itemsectionRepository;
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
            throw new NotImplementedException();
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
                       Markups = _markupRepository.GetAll(),
                       Organisation = organisationRepository.Find(organisationRepository.OrganisationId),
                       StockCategories = stockCategoryRepository.GetAll(),
                       ChartOfAccounts = chartOfAccountRepository.GetAll(),
                       PaperSizes = paperSizeRepository.GetAll(),
                       InkPlateSides = inkPlateSideRepository.GetAll()
                   };
        }

        /// <summary>
        /// Get Base Data For Company
        /// </summary>
        public OrderBaseResponseForCompany GetBaseDataForCompany(long companyId)
        {
            return new OrderBaseResponseForCompany
                {
                    CompanyContacts = companyContactRepository.GetCompanyContactsByCompanyId(companyId),
                    CompanyAddresses = addressRepository.GetAddressByCompanyID(companyId)
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

        public bool DeleteCart(long CompanyID,long OrganisationID)
        {
            try
            {
                List<Estimate> cartorders = orderRepository.GetCartOrdersByCompanyID(CompanyID);

                orderRepository.DeleteCart(CompanyID);
                List<string> ImagesPath = new List<string>();
                
                if(cartorders != null && cartorders.Count > 0)
                {
                    
                    foreach(var cartOrd in cartorders)
                    {
                        DeleteItemsPhysically(cartOrd, OrganisationID);
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                throw ex;

            }
        }
        public bool DeleteOrderSP(long OrderID,long OrganisationID)
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
            catch(Exception ex)
            {
                throw ex; 
            }
        }
        #endregion

        #region Print View Plan Code
        
        

        public PtvDTO GetPTV(PTVRequestModel request)
        {
            Organisation organisation = organisationRepository.GetOrganizatiobByID();
            
            if(organisation != null)
            {
                request.ItemHeight = LengthConversionHelper.ConvertLength(request.ItemHeight, MPC.Models.Common.LengthUnit.Mm, organisation.LengthUnit);
                request.ItemWidth = LengthConversionHelper.ConvertLength(request.ItemWidth, MPC.Models.Common.LengthUnit.Mm, organisation.LengthUnit);
                request.PrintHeight = LengthConversionHelper.ConvertLength(request.PrintHeight, MPC.Models.Common.LengthUnit.Mm, organisation.LengthUnit);
                request.PrintWidth = LengthConversionHelper.ConvertLength(request.PrintWidth, MPC.Models.Common.LengthUnit.Mm, organisation.LengthUnit);
                request.ItemHorizentalGutter = LengthConversionHelper.ConvertLength(request.ItemHorizentalGutter, MPC.Models.Common.LengthUnit.Mm, organisation.LengthUnit);
                request.ItemVerticalGutter = LengthConversionHelper.ConvertLength(request.ItemVerticalGutter, MPC.Models.Common.LengthUnit.Mm, organisation.LengthUnit);              
            }
            

            return itemsectionRepository.DrawPTV((PrintViewOrientation)request.Orientation, request.ReversePtvRows, request.ReversePtvCols, request.isDoubleSided, request.isWorknTrun, request.isWorknTumble, request.ApplyPressRestrict, request.ItemHeight, request.ItemWidth, request.PrintHeight, request.PrintWidth, (GripSide)request.Grip, request.GripDepth, request.HeadDepth, request.PrintGutter, request.ItemHorizentalGutter, request.ItemVerticalGutter);
        }

        

        public PtvDTO GetPTVCalculation(PTVRequestModel request)
        {
            Organisation organisation = organisationRepository.GetOrganizatiobByID();

            if (organisation != null)
            {
                request.ItemHeight = LengthConversionHelper.ConvertLength(request.ItemHeight, MPC.Models.Common.LengthUnit.Mm, organisation.LengthUnit);
                request.ItemWidth = LengthConversionHelper.ConvertLength(request.ItemWidth, MPC.Models.Common.LengthUnit.Mm, organisation.LengthUnit);
                request.PrintHeight = LengthConversionHelper.ConvertLength(request.PrintHeight, MPC.Models.Common.LengthUnit.Mm, organisation.LengthUnit);
                request.PrintWidth = LengthConversionHelper.ConvertLength(request.PrintWidth, MPC.Models.Common.LengthUnit.Mm, organisation.LengthUnit);
                request.ItemHorizentalGutter = LengthConversionHelper.ConvertLength(request.ItemHorizentalGutter, MPC.Models.Common.LengthUnit.Mm, organisation.LengthUnit);
                request.ItemVerticalGutter = LengthConversionHelper.ConvertLength(request.ItemVerticalGutter, MPC.Models.Common.LengthUnit.Mm, organisation.LengthUnit);
            }
            return itemsectionRepository.CalculatePTV(request.ReversePtvRows, request.ReversePtvCols, request.isDoubleSided, false, request.ApplyPressRestrict, request.ItemHeight, request.ItemWidth, request.PrintHeight, request.PrintWidth, 1, request.Grip, request.GripDepth, request.HeadDepth, request.PrintGutter, request.ItemHorizentalGutter, request.ItemVerticalGutter, request.isWorknTrun, request.isWorknTumble);
        }
        
        #endregion

        #region Estimation Methods
        public BestPressResponse GetBestPresses(ItemSection currentSection)
        {
            return itemsectionRepository.GetBestPressResponse(currentSection);
        }
        #endregion


    }
}
