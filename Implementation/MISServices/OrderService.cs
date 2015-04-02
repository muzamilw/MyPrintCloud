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
        private readonly IOrderRepository orderRepository;
        private readonly IItemRepository itemRepository;
        private readonly MPC.Interfaces.WebStoreServices.ITemplateService templateService;
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public OrderService(IEstimateRepository estimateRepository, ISectionFlagRepository sectionFlagRepository, ICompanyContactRepository companyContactRepository,
            IAddressRepository addressRepository, ISystemUserRepository systemUserRepository, IPipeLineSourceRepository pipeLineSourceRepository, IMarkupRepository markupRepository,
            IPaymentMethodRepository paymentMethodRepository, IOrganisationRepository organisationRepository,IStockCategoryRepository stockCategoryRepository, IOrderRepository orderRepository, IItemRepository itemRepository, MPC.Interfaces.WebStoreServices.ITemplateService templateService,
            IChartOfAccountRepository chartOfAccountRepository)
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
            this.itemRepository = itemRepository;
            this.templateService = templateService;
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
            throw new NotImplementedException();
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
            IEnumerable<StockCategory> stocks = stockCategoryRepository.GetAll();
            return new OrderBaseResponse
                   {
                       SectionFlags = sectionFlagRepository.GetSectionFlagBySectionId((int)SectionEnum.Order),
                       SystemUsers = systemUserRepository.GetAll(),
                       PipeLineSources = pipeLineSourceRepository.GetAll(),
                       PaymentMethods = paymentMethodRepository.GetAll(),
                       Markups = _markupRepository.GetAll(),
                       Organisation = organisationRepository.Find(organisationRepository.OrganisationId),
                       StockCategories = stocks,
                       ChartOfAccounts = chartOfAccountRepository.GetAll(),
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
        public PtvDTO CalculatePTV(int ReversePTVRows, int ReversePTVCols, bool IsDoubleSided, bool ApplySwing, bool ApplyPressRestrict, double ItemHeight, double ItemWidth, double PrintHeight, double PrintWidth, int ColorBar,
                                    int Grip, double GripDepth, double HeadDepth, double PrintGutter, double ItemHorizontalGutter, double ItemVerticalGutter, bool IsWorknTurn, bool IsWorknTumble)
        {

            double vPrintAreaHeight = 0;
            double vPrintAreaWidth = 0;
            byte mCurrOrient = 0;
            int LandScapeRows = 0;
            int LandScapeCols = 0;
            int PortraitRows = 0;
            int PortraitCols = 0;
            int LandScapeSwing = 0;
            int PortraitSwing = 0;
            long LandScapeColSwing = 0;
            long PortraitColSwing = 0;
            long LandScapeRowSwing = 0;
            long PortraitRowSwing = 0;
            long LandScapeRowsRemaining = 0;
            long LandScapeColsRemaining = 0;
            long PortraitRowsRemaining = 0;
            long PortraitColsRemaining = 0;
            int LandscapePTV = 0;
            int PortraitPTV = 0;
            double LandScapeItemHeight = 0;
            double PortraitItemHeight = 0;
            double LandScapeItemWidth = 0;
            double PortraitItemWidth = 0;

            //Grip = UCase(Left(Grip, 1))
            vPrintAreaHeight = PrintHeight;
            vPrintAreaWidth = PrintWidth;

            LandScapeItemHeight = GetItemHeight(ItemHeight, ItemWidth, PrintViewOrientation.Landscape); //ItemHeight; //
            LandScapeItemWidth = GetItemWidth(ItemHeight, ItemWidth, PrintViewOrientation.Landscape); //ItemWidth;//
            //Getting area excluding Gutters and Press Restrictions and Color Bar

            SetPrintView(ref vPrintAreaHeight, ref vPrintAreaWidth, LandScapeItemHeight, LandScapeItemWidth, ApplyPressRestrict, GripSide.LongSide, Convert.ToByte(PrintViewOrientation.Landscape), ColorBar, PrintHeight, PrintWidth,
            GripDepth, HeadDepth, PrintGutter, IsWorknTurn, IsWorknTumble);

            if (ReversePTVCols > 0 && ReversePTVRows > 0)
            {
                LandScapeRows = ReversePTVRows;
                //Setting the Custom Landscape Rows if provided by user
                LandScapeCols = ReversePTVCols;
                //Setting the Custom Landscape Cols if provided by user
            }
            else
            {
                CalcRowsToFit(vPrintAreaHeight, ref LandScapeRows, ref LandScapeRowsRemaining, LandScapeItemHeight, ItemVerticalGutter, IsWorknTurn, IsWorknTumble);
                CalcColsToFit(vPrintAreaWidth, ref LandScapeCols, ref LandScapeColsRemaining, LandScapeItemWidth, ItemHorizontalGutter, IsWorknTurn, IsWorknTumble);
            }

            LandscapePTV = (LandScapeRows * LandScapeCols) + Convert.ToInt32((ApplySwing ? LandScapeSwing : 0));

            //'Check for Portrait view
            vPrintAreaHeight = PrintHeight;
            vPrintAreaWidth = PrintWidth;
            PortraitItemHeight = GetItemHeight(ItemHeight, ItemWidth, PrintViewOrientation.Portrait); //ItemHeight; //
            PortraitItemWidth = GetItemWidth(ItemHeight, ItemWidth, PrintViewOrientation.Portrait); //ItemWidth;//

            //This is the Check wheter the user has sent custom Rows and Columns
            if (ReversePTVCols > 0 && ReversePTVRows > 0)
            {
                PortraitRows = ReversePTVRows;
                PortraitCols = ReversePTVCols;
            }
            else
            {
                SetPrintView(ref vPrintAreaHeight, ref vPrintAreaWidth, PortraitItemHeight, PortraitItemWidth, ApplyPressRestrict, GripSide.ShortSide, Convert.ToByte(PrintViewOrientation.Portrait), ColorBar, PrintHeight, PrintWidth,
                GripDepth, HeadDepth, PrintGutter, IsWorknTurn, IsWorknTumble);
                CalcRowsToFit(vPrintAreaHeight, ref PortraitRows, ref PortraitRowsRemaining, PortraitItemHeight, ItemVerticalGutter, IsWorknTurn, IsWorknTumble);
                CalcColsToFit(vPrintAreaWidth, ref PortraitCols, ref PortraitColsRemaining, PortraitItemWidth, ItemHorizontalGutter, IsWorknTurn, IsWorknTumble);
            }

            PortraitPTV = (PortraitRows * PortraitCols) + Convert.ToInt32((ApplySwing ? PortraitSwing : 0));
            PtvDTO oPTVResults = new PtvDTO { LandScapeRows = LandScapeRows, LandScapeCols = LandScapeCols, PortraitRows = PortraitRows, PortraitCols = PortraitCols, LandScapeSwing = LandScapeSwing, PortraitSwing = PortraitSwing, LandscapePTV = LandscapePTV, PortraitPTV = PortraitPTV };
            return oPTVResults;
        }

        private bool CalcRowsToFit(double PrintAreaHeight, ref int Rows, ref long RowsRemaining, double ItemHeight, double ItemVertGutter, bool IsWorknTurn, bool IsWorknTumble)
        {
            if (IsWorknTurn == true)
            {
                Rows = Convert.ToInt32(Math.Floor(Convert.ToDouble(PrintAreaHeight) / (Convert.ToDouble(ItemHeight) + ItemVertGutter)));
                RowsRemaining = Convert.ToInt64(((Convert.ToDouble(PrintAreaHeight) / (Convert.ToDouble(ItemHeight) + ItemVertGutter)) - Convert.ToDouble(Rows)) * (Convert.ToDouble(ItemHeight) + ItemVertGutter));
            }
            else if (IsWorknTumble == true)
            {
                Rows = Convert.ToInt32(Math.Floor(Convert.ToDouble(PrintAreaHeight) / (Convert.ToDouble(ItemHeight) + ItemVertGutter)));
                RowsRemaining = Convert.ToInt64(((Convert.ToDouble(PrintAreaHeight) / (Convert.ToDouble(ItemHeight) + ItemVertGutter)) - Convert.ToDouble(Rows)) * (Convert.ToDouble(ItemHeight) + ItemVertGutter));

                if (Rows % 2 == 1)
                {
                    Rows -= 1;
                }
            }
            else
            {
                Rows = Convert.ToInt32(Math.Floor(Convert.ToDouble(PrintAreaHeight) / (ItemHeight + ItemVertGutter)));
                RowsRemaining = Convert.ToInt64(((Convert.ToDouble(PrintAreaHeight) / (Convert.ToDouble(ItemHeight) + ItemVertGutter)) - Convert.ToDouble(Rows)) * (Convert.ToDouble(ItemHeight) + ItemVertGutter));
            }
            return true;
        }
        private static bool CalcColsToFit(double PrintAreaWidth, ref int Cols, ref long ColsRemaining, double ItemWidth, double ItemHorzGutter, bool IsWorknTurn, bool IsWorknTumble)
        {
            if (IsWorknTurn == true)
            {
                Cols = Convert.ToInt32(Math.Floor(Convert.ToDouble(PrintAreaWidth) / (Convert.ToDouble(ItemWidth) + ItemHorzGutter)));
                ColsRemaining = Convert.ToInt64(((Convert.ToDouble(PrintAreaWidth) / (Convert.ToDouble(ItemWidth) + ItemHorzGutter)) - Convert.ToDouble(Cols)) * (Convert.ToDouble(ItemWidth) + ItemHorzGutter));
                if (Cols % 2 == 1)
                {
                    Cols -= 1;
                }
            }
            else if (IsWorknTumble == true)
            {
                Cols = Convert.ToInt32(Math.Floor(Convert.ToDouble(PrintAreaWidth) / (Convert.ToDouble(ItemWidth) + ItemHorzGutter)));
                ColsRemaining = Convert.ToInt64(((Convert.ToDouble(PrintAreaWidth) / (Convert.ToDouble(ItemWidth) + ItemHorzGutter)) - Convert.ToDouble(Cols)) * (Convert.ToDouble(ItemWidth) + ItemHorzGutter));
            }
            else
            {
                Cols = Convert.ToInt32(Math.Floor(Convert.ToDouble(PrintAreaWidth) / (Convert.ToDouble(ItemWidth) + ItemHorzGutter)));
                ColsRemaining = Convert.ToInt64(((Convert.ToDouble(PrintAreaWidth) / (Convert.ToDouble(ItemWidth) + ItemHorzGutter)) - Convert.ToDouble(Cols)) * (Convert.ToDouble(ItemWidth) + ItemHorzGutter));
            }
            return true;
        }

        public static bool SetPrintView(ref double vPrintAreaHeight, ref double vPrintAreaWidth, double ItemHeight, double ItemWidth, bool PressRestrictions, GripSide Grip, byte View, int ColorBar, double PrintHeight, double PrintWidth,
                                        double GripDepth, double HeadDepth, double PrintGutter, bool IsWorknTurn, bool IsWorknTumble)
        {

            //This Function would set the following Variables which are sent byref others are for calculation
            //printAreaHeight i.e. vPrintAreaHeight
            //printAreaWidth  i.e. vPrintAreaWidth

            //If View = 0 Then  ''Landscape
            if (View == (byte)PrintViewOrientation.Landscape)
            {
                if (PressRestrictions == true)
                {
                    GetPrintArea(ref vPrintAreaHeight, ref vPrintAreaWidth, Grip, PrintHeight, PrintWidth, PrintGutter, GripDepth, HeadDepth, ColorBar, IsWorknTurn,
                    IsWorknTumble);
                }
                else
                {
                    if (Grip == GripSide.LongSide)
                    {
                        if (PrintHeight > PrintWidth)
                        {
                            vPrintAreaHeight = PrintHeight;
                            vPrintAreaWidth = PrintWidth - ColorBar;
                        }
                        else
                        {
                            vPrintAreaHeight = PrintHeight - ColorBar;
                            vPrintAreaWidth = PrintWidth;
                        }
                    }
                    else if (Grip == GripSide.ShortSide)
                    {
                        if (PrintHeight < PrintWidth)
                        {
                            vPrintAreaHeight = PrintHeight;
                            vPrintAreaWidth = PrintWidth - ColorBar;
                        }
                        else
                        {
                            vPrintAreaHeight = PrintHeight - ColorBar;
                            vPrintAreaWidth = PrintWidth;
                        }
                    }
                }
                //ElseIf View = 1 Then ''Portrait
                //'Portrait
            }
            else if (View == (byte)PrintViewOrientation.Portrait)
            {

                if (PressRestrictions)
                {
                    GetPrintArea(ref vPrintAreaHeight, ref vPrintAreaWidth, Grip, PrintHeight, PrintWidth, PrintGutter, GripDepth, HeadDepth, ColorBar, IsWorknTurn,
                    IsWorknTumble);
                }
                else
                {
                    if (Grip == GripSide.LongSide)
                    {
                        if (PrintHeight > PrintWidth)
                        {
                            vPrintAreaHeight = PrintHeight;
                            vPrintAreaWidth = PrintWidth - ColorBar;
                        }
                        else
                        {
                            vPrintAreaHeight = PrintHeight - ColorBar;
                            vPrintAreaWidth = PrintWidth;
                        }
                    }
                    else if (Grip == GripSide.ShortSide)
                    {
                        if (PrintHeight < PrintWidth)
                        {
                            vPrintAreaHeight = PrintHeight;
                            vPrintAreaWidth = PrintWidth - ColorBar;
                        }
                        else
                        {
                            vPrintAreaHeight = PrintHeight - ColorBar;
                            vPrintAreaWidth = PrintWidth;
                        }
                    }
                }
            }
            return true;
        }

        public static bool GetPrintArea(ref double vPrintAreaHeight, ref double vPrintAreaWidth, GripSide strGrip, double PrintHeight, double PrintWidth, double PrintGutter, double GripDepth, double HeadDepth, int ColorBar, bool IsWorknTurn,
                                        bool IsWorknTumble)
        {

            if (strGrip == GripSide.LongSide)
            {
                if (PrintHeight > PrintWidth)
                {
                    vPrintAreaHeight = PrintHeight - (PrintGutter * 2);
                    vPrintAreaWidth = PrintWidth - GripDepth - HeadDepth - ColorBar;
                }
                else
                {
                    vPrintAreaHeight = PrintHeight - GripDepth - HeadDepth - ColorBar;
                    vPrintAreaWidth = PrintWidth - (PrintGutter * 2);
                }
            }
            else if (strGrip == GripSide.ShortSide)
            {
                if (PrintHeight < PrintWidth)
                {
                    vPrintAreaHeight = PrintHeight - GripDepth - HeadDepth - ColorBar;
                    vPrintAreaWidth = PrintWidth - (PrintGutter * 2);
                }
                else
                {
                    vPrintAreaHeight = PrintHeight - (PrintGutter * 2);
                    vPrintAreaWidth = PrintWidth - GripDepth - HeadDepth - ColorBar;
                }
            }
            return true;
        }


        /// <summary>
        /// This function returns the item Height according to the Orientation
        /// </summary>
        /// <param name="OrignalItemHeight"></param>
        /// <param name="OrignalItemWidth"></param>
        /// <param name="CurrentOrientation"></param>
        /// <returns></returns>
        private double GetItemHeight(double OrignalItemHeight, double OrignalItemWidth, PrintViewOrientation CurrentOrientation)
        {
            if (CurrentOrientation == PrintViewOrientation.Landscape)     //LandScape
                return OrignalItemHeight;
            else                            //Portrait
                return OrignalItemWidth;

        }


        /// <summary>
        /// 'This function returns the Item WIdht according to the Orientation
        /// </summary>
        /// <param name="OrignalItemHeight"></param>
        /// <param name="OrignalItemWidth"></param>
        /// <param name="CurrentOrientation"></param>
        /// <returns></returns>
        private double GetItemWidth(double OrignalItemHeight, double OrignalItemWidth, PrintViewOrientation CurrentOrientation)
        {
            if (CurrentOrientation == PrintViewOrientation.Landscape) //'LandScape
                return OrignalItemWidth;
            else     //'Portrait
                return OrignalItemHeight;

        }

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
            

            return DrawPTV((PrintViewOrientation)request.Orientation, request.ReversePtvRows, request.ReversePtvCols, request.isDoubleSided, request.isWorknTrun, request.isWorknTumble, request.ApplyPressRestrict, request.ItemHeight, request.ItemWidth, request.PrintHeight, request.PrintWidth, (GripSide)request.Grip, request.GripDepth, request.HeadDepth, request.PrintGutter, request.ItemHorizentalGutter, request.ItemVerticalGutter);
        }

        public PtvDTO DrawPTV(PrintViewOrientation strOrient, int ReversePTVRows, int ReversePTVCols, bool IsDoubleSided, bool IsWorknTurn, bool IsWorknTumble, bool ApplyPressRestrict, double ItemHeight, double ItemWidth, double PrintHeight, double PrintWidth, GripSide Grip, double GripDepth, double HeadDepth, double PrintGutter, double ItemHorizontalGutter, double ItemVerticalGutter)
        {
            Image imgCardL = Image.FromFile(HttpContext.Current.Server.MapPath("../Content/Images/ptv/front-up.gif"));
            Image imgCardBackL = Image.FromFile(HttpContext.Current.Server.MapPath("../Content/Images/ptv/back-up.gif"));
            Image imgCardDownL = Image.FromFile(HttpContext.Current.Server.MapPath("../Content/Images/ptv/front-down.gif"));
            Image imgCardBackDownL = Image.FromFile(HttpContext.Current.Server.MapPath("../Content/Images/ptv/back-down.gif"));
            Image imgCardP = Image.FromFile(HttpContext.Current.Server.MapPath("../Content/Images/ptv/front-upP.gif"));
            Image imgCardBackP = Image.FromFile(HttpContext.Current.Server.MapPath("../Content/Images/ptv/back-upP.gif"));
            Image imgCardDownP = Image.FromFile(HttpContext.Current.Server.MapPath("../Content/Images/ptv/front-downP.gif"));
            Image imgCardBackDownP = Image.FromFile(HttpContext.Current.Server.MapPath("../Content/Images/ptv/back-downP.gif"));
            Image imgCardBackW = Image.FromFile(HttpContext.Current.Server.MapPath("../Content/Images/ptv/back-upW.gif"));
            Image DottedImage = Image.FromFile(HttpContext.Current.Server.MapPath("../Content/Images/ptv/dotTexture.gif"));



            Bitmap bm = default(Bitmap);
            Bitmap bm2 = default(Bitmap);
            Graphics gs = default(Graphics);
            Graphics gs2 = default(Graphics);

            try
            {




                Pen oPen = new Pen(Color.Blue, 1);
                Image imgApply = default(Image);
                Image imgApplyBack = default(Image);
                SolidBrush oBrush = new SolidBrush(ColorTranslator.FromHtml("#92bbe2"));
                TextureBrush oDottedBrush = new TextureBrush(DottedImage);
                double vPageWidth = 0;
                double vPageHeight = 0;
                double vIWidth = 0;
                double vIHeight = 0;
                double dblSpace = 0;
                int x1 = 0;
                int x2 = 0;
                int x3 = 0;
                int y1 = 0;
                int y2 = 0;
                int y3 = 0;
                int i = 0;
                int j = 0;
                int n = 0;
                byte nFactor = 3;
                //'Additional gap for Page View
                int vOddSide = 0;
                //Dim blnWork As Boolean
                int intSymmetry = 0;

                int myRows = 0;
                int myCols = 0;
                int[, ,] mItemPosition = new int[2, 2, 4];
                //'Rows, Cols, (x0,y0, itemX, itemY)
                string myFlip = null;
                bool bColMode = false;
                bool bRowMode = false;
                bool IsBeforeHalfSideHorizontal = true;
                bool IsBeforeHalfSideVertical = true;

                int xFactor = 0;
                int yFactor = 0;
                int xSwing = 0;
                int ySwing = 0;
                int vRowCount = 0;
                int vColumnCount = 0;
                int vRowSwing = 0;
                int vColSwing = 0;
                double vRightPad = 0;
                double vLeftPad = 0;
                double vBottomPad = 0;
                double vTopPad = 0;
                List<DividerLine> DividerLines = new List<DividerLine>();

                //strOrient = Left(strOrient, 1)


                PtvDTO oPTVResult = CalculatePTV(ReversePTVRows, ReversePTVCols, IsDoubleSided, false, ApplyPressRestrict, ItemHeight, ItemWidth, PrintHeight, PrintWidth, 0,
                (int)Grip, GripDepth, HeadDepth, PrintGutter, ItemHorizontalGutter, ItemVerticalGutter, IsWorknTurn, IsWorknTumble);


                vIWidth = GetItemWidth(ItemHeight, ItemWidth, strOrient);
                vIHeight = GetItemHeight(ItemHeight, ItemWidth, strOrient);
                vPageWidth = PrintWidth;
                vPageHeight = PrintHeight;

                bm = new Bitmap(Convert.ToInt32(vPageWidth + nFactor), Convert.ToInt32(vPageHeight + nFactor));
                bm2 = new Bitmap(Convert.ToInt32(vPageWidth + nFactor), Convert.ToInt32(vPageHeight + nFactor));
                gs = Graphics.FromImage(bm);
                gs2 = Graphics.FromImage(bm2);
                gs.TextRenderingHint = TextRenderingHint.AntiAlias;
                gs2.TextRenderingHint = TextRenderingHint.AntiAlias;
                //'applying Background
                gs.Clear(Color.WhiteSmoke);
                gs2.Clear(Color.WhiteSmoke);
                gs.DrawRectangle(new Pen(Color.Black, 1), 1, 1, Convert.ToInt32(vPageWidth), Convert.ToInt32(vPageHeight));
                gs2.DrawRectangle(new Pen(Color.Black, 1), 1, 1, Convert.ToInt32(vPageWidth), Convert.ToInt32(vPageHeight));


                ///''''''''''''''''''''''''
                /// Drawing Gutter
                ///''''''''''''''''''''''''
                if (PrintGutter > 0)
                {
                    PrintGutter = 5;
                    if (Grip == GripSide.LongSide & PrintHeight <= PrintWidth)
                    {
                        gs.FillRectangle(new SolidBrush(Color.SeaGreen), 0, 0, Convert.ToInt32(PrintGutter), Convert.ToInt32(vPageHeight));
                        gs2.FillRectangle(new SolidBrush(Color.SeaGreen), 0, 0, Convert.ToInt32(PrintGutter), Convert.ToInt32(vPageHeight));
                        gs.FillRectangle(new SolidBrush(Color.SeaGreen), Convert.ToInt32(vPageWidth - PrintGutter), 0, Convert.ToInt32(PrintGutter), Convert.ToInt32(vPageHeight));
                        //'drawing grip on right side
                        gs2.FillRectangle(new SolidBrush(Color.SeaGreen), Convert.ToInt32(vPageWidth - PrintGutter), 0, Convert.ToInt32(PrintGutter), Convert.ToInt32(vPageHeight));
                        //'drawing grip on right side
                        vPageWidth = vPageWidth - (PrintGutter * 2);
                    }
                    else
                    {
                        gs.FillRectangle(new SolidBrush(Color.SeaGreen), 0, 0, Convert.ToInt32(vPageWidth), Convert.ToInt32(PrintGutter));
                        gs2.FillRectangle(new SolidBrush(Color.SeaGreen), 0, 0, Convert.ToInt32(vPageWidth), Convert.ToInt32(PrintGutter));
                        gs.FillRectangle(new SolidBrush(Color.SeaGreen), 0, Convert.ToInt32(vPageHeight - PrintGutter), Convert.ToInt32(vPageWidth), Convert.ToInt32(PrintGutter));
                        //'drawing grip at bottom side
                        gs2.FillRectangle(new SolidBrush(Color.SeaGreen), 0, Convert.ToInt32(vPageHeight - PrintGutter), Convert.ToInt32(vPageWidth), Convert.ToInt32(PrintGutter));
                        //'drawing grip at bottom side
                        vPageHeight = vPageHeight - (PrintGutter * 2);
                    }
                }
                ///''''''''''''''''''''''''

                ///''''''''''''''''''''''''
                /// Drawing Head
                ///''''''''''''''''''''''''
                if (HeadDepth > 0)
                {
                    HeadDepth = 5;
                    if (Grip == GripSide.LongSide & PrintHeight <= PrintWidth)
                    {
                        gs.FillRectangle(new SolidBrush(Color.Black), Convert.ToInt32(PrintGutter), 0, Convert.ToInt32(vPageWidth), Convert.ToInt32(HeadDepth));
                        gs2.FillRectangle(new SolidBrush(Color.Black), Convert.ToInt32(PrintGutter), 0, Convert.ToInt32(vPageWidth), Convert.ToInt32(HeadDepth));
                        vPageHeight = vPageHeight - HeadDepth;
                    }
                    else
                    {
                        gs.FillRectangle(new SolidBrush(Color.Black), Convert.ToInt32(vPageWidth - HeadDepth), Convert.ToInt32(PrintGutter), Convert.ToInt32(HeadDepth), Convert.ToInt32(vPageHeight));
                        gs2.FillRectangle(new SolidBrush(Color.Black), Convert.ToInt32(vPageWidth - HeadDepth), Convert.ToInt32(PrintGutter), Convert.ToInt32(HeadDepth), Convert.ToInt32(vPageHeight));
                        vPageWidth = vPageWidth - HeadDepth;
                    }
                }
                ///''''''''''''''''''''''''

                ///''''''''''''''''''''''''
                /// Drawing Grip 
                ///''''''''''''''''''''''''
                if (GripDepth > 0)
                {
                    GripDepth = 5;
                    if (Grip == GripSide.LongSide & PrintHeight <= PrintWidth)
                    {
                        gs.FillRectangle(new SolidBrush(Color.DarkSalmon), Convert.ToInt32(PrintGutter), Convert.ToInt32(HeadDepth + (vPageHeight - GripDepth)), Convert.ToInt32(vPageWidth), Convert.ToInt32(GripDepth));
                        gs2.FillRectangle(new SolidBrush(Color.DarkSalmon), Convert.ToInt32(PrintGutter), Convert.ToInt32(HeadDepth + (vPageHeight - GripDepth)), Convert.ToInt32(vPageWidth), Convert.ToInt32(GripDepth));
                        vPageHeight = vPageHeight - GripDepth;
                    }
                    else
                    {
                        gs.FillRectangle(new SolidBrush(Color.DarkSalmon), 0, Convert.ToInt32(PrintGutter), Convert.ToInt32(GripDepth), Convert.ToInt32(vPageHeight));
                        gs2.FillRectangle(new SolidBrush(Color.DarkSalmon), 0, Convert.ToInt32(PrintGutter), Convert.ToInt32(GripDepth), Convert.ToInt32(vPageHeight));
                        vPageWidth = vPageWidth - GripDepth;
                    }
                }


                ///''''''''''''''''''''''''''''''
                i = 0;
                j = 0;
                x1 = 0;
                x2 = 0;
                x3 = 0;
                y1 = 0;
                y2 = 0;
                y3 = 0;
                ///''''''''''''''''''''''''''''''
                ///Item drawing starts here...
                ///''''''''''''''''''''''''''''''



                if (strOrient == PrintViewOrientation.Portrait)
                {
                    vRowCount = oPTVResult.PortraitRows;
                    vColumnCount = oPTVResult.PortraitCols;
                }
                else
                {
                    vRowCount = oPTVResult.LandScapeRows;
                    vColumnCount = oPTVResult.LandScapeCols;
                }

                if (ItemHorizontalGutter > 0)
                {
                    if (ItemHorizontalGutter < ItemVerticalGutter)
                    {
                        vRightPad = (vIWidth * 1 / 50);
                    }
                    else
                    {
                        vRightPad = (vIWidth * 1 / 25);
                    }
                }
                if (ItemVerticalGutter > 0)
                {
                    if (ItemVerticalGutter < ItemHorizontalGutter)
                    {
                        vTopPad = (vIHeight * 1 / 50);
                    }
                    else
                    {
                        vTopPad = (vIHeight * 1 / 25);
                    }
                }

                vIWidth = Convert.ToInt32(vIWidth - vRightPad);
                vIHeight = Convert.ToInt32(vIHeight - vTopPad);

                x2 = Convert.ToInt32((vColumnCount) * (vIWidth + vRightPad));
                y2 = Convert.ToInt32((vRowCount + Convert.ToDouble((vRowSwing > 0 | vColSwing > 0 ? 0.5 : 0))) * (vIHeight + vTopPad));


                //'Start printing images
                for (i = 0; i <= vRowCount - 1; i++)
                {
                    if (i == 0)
                    {
                        yFactor = Convert.ToInt32((vPageHeight - y2) / 2);
                    }
                    else
                    {
                        yFactor = Convert.ToInt32(yFactor + vTopPad + vIHeight);
                    }

                    for (j = 0; j <= vColumnCount - 1; j++)
                    {
                        if (j == 0)
                        {
                            xFactor = Convert.ToInt32((vPageWidth - x2) / 2);
                        }
                        else
                        {
                            xFactor = Convert.ToInt32(xFactor + vRightPad + vIWidth);
                        }





                        if (IsWorknTumble == true)
                        {
                            if (vRowCount / 2 == i + 1)
                            {
                                DividerLines.Add(new DividerLine(0, Convert.ToInt32(vPageWidth), Convert.ToInt32(yFactor + vIHeight), Convert.ToInt32(yFactor + vIHeight)));
                            }


                            if (i < vRowCount / 2)
                            {
                                if (strOrient == PrintViewOrientation.Portrait)
                                {
                                    imgApply = imgCardP;
                                }
                                else
                                {
                                    imgApply = imgCardL;
                                }
                            }
                            else
                            {
                                if (strOrient == PrintViewOrientation.Portrait)
                                {
                                    imgApply = imgCardBackDownP;
                                }
                                else
                                {
                                    imgApply = imgCardBackDownL;
                                }
                            }


                            gs.DrawImage(imgApply, Convert.ToInt32(xFactor + PrintGutter), Convert.ToInt32(yFactor + HeadDepth), Convert.ToInt32(vIWidth - 1), Convert.ToInt32(vIHeight - 1));
                            //checks performed horizontal :)

                        }
                        else if (IsWorknTurn == true)
                        {
                            //draw the divider line :)
                            if (vColumnCount / 2 == j + 1)
                            {
                                DividerLines.Add(new DividerLine(Convert.ToInt32(xFactor + GripDepth + vIWidth), Convert.ToInt32(xFactor + GripDepth + vIWidth), 0, Convert.ToInt32(vPageHeight)));
                            }

                            if (j < vColumnCount / 2)
                            {
                                if (strOrient == PrintViewOrientation.Portrait)
                                {
                                    imgApply = imgCardP;
                                }
                                else
                                {
                                    imgApply = imgCardL;
                                }
                            }
                            else
                            {
                                if (strOrient == PrintViewOrientation.Portrait)
                                {
                                    imgApply = imgCardBackP;
                                }
                                else
                                {
                                    imgApply = imgCardBackL;
                                }
                            }


                            gs.DrawImage(imgApply, Convert.ToInt32(xFactor + PrintGutter), Convert.ToInt32(yFactor + HeadDepth), Convert.ToInt32(vIWidth - 1), Convert.ToInt32(vIHeight - 1));

                            //double sided case. where simple drawing is required..

                        }
                        else if (IsWorknTurn == false & IsWorknTumble == false)
                        {
                            if (strOrient == PrintViewOrientation.Portrait)
                            {
                                imgApply = imgCardP;
                                //'imgCard.RotateFlip(RotateFlipType.Rotate90FlipNone)
                                imgApplyBack = imgCardBackP;
                                //'imgCardBack.RotateFlip(RotateFlipType.Rotate90FlipNone)
                            }
                            else
                            {
                                imgApply = imgCardL;
                                imgApplyBack = imgCardBackL;
                            }

                            //Short Side                        
                            gs.DrawImage(imgApply, Convert.ToInt32(xFactor + PrintGutter), Convert.ToInt32(yFactor + HeadDepth), Convert.ToInt32(vIWidth - 1), Convert.ToInt32(vIHeight - 1));
                            gs2.DrawImage(imgApplyBack, Convert.ToInt32(xFactor + PrintGutter), Convert.ToInt32(yFactor + HeadDepth), Convert.ToInt32(vIWidth - 1), Convert.ToInt32(vIHeight - 1));

                        }

                    }


                }

                //drawing the divider lines from the divierlines list
                if (DividerLines.Count > 0)
                {
                    foreach (DividerLine oDivider in DividerLines)
                    {
                        gs.DrawLine(new Pen(oDottedBrush, 5), oDivider.X1, oDivider.Y1, oDivider.X2, oDivider.Y2);
                    }
                }

                gs.TextRenderingHint = TextRenderingHint.AntiAlias;
                gs.Flush();


                System.IO.MemoryStream stream = new System.IO.MemoryStream();
                bm.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

                System.IO.MemoryStream stream2 = new System.IO.MemoryStream();
                bm2.Save(stream2, System.Drawing.Imaging.ImageFormat.Png);


                oPTVResult.Side1Image = stream.ToArray();
                stream.Dispose();

                if (IsDoubleSided == true)
                {
                    if (IsWorknTurn == false & IsWorknTumble == false)
                    {
                        oPTVResult.Side2Image = stream2.ToArray();
                        stream2.Dispose();
                    }
                }

                return oPTVResult;
            }
            catch (Exception ex)
            {
                throw new Exception("DrawPTV", ex);
            }
            finally
            {
                if (bm != null)
                    bm.Dispose();

                if (bm2 != null)
                    bm2.Dispose();

                if (gs != null)
                    gs.Dispose();

                if (gs2 != null)
                    gs2.Dispose();
            }
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
            return CalculatePTV(request.ReversePtvRows, request.ReversePtvCols, request.isDoubleSided, false, request.ApplyPressRestrict, request.ItemHeight, request.ItemWidth, request.PrintHeight, request.PrintWidth, 1, request.Grip, request.GripDepth, request.HeadDepth, request.PrintGutter, request.ItemHorizentalGutter, request.ItemVerticalGutter, request.isWorknTrun, request.isWorknTumble);
        }
        
        #endregion
    }
}
