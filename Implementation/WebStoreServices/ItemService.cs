using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;
using System.Web;
using WebSupergoo.ABCpdf8;

using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace MPC.Implementation.WebStoreServices
{
    public class ItemService : IItemService
    {

        private readonly IItemRepository _ItemRepository;
        private readonly IItemStockOptionRepository _StockOptions;
        private readonly ISectionFlagRepository _SectionFlagRepository;
        private readonly ICompanyRepository _CompanyRepository;
        private readonly IItemStockControlRepository _StockRepository;
        private readonly IItemAddOnCostCentreRepository _AddOnRepository;
        private readonly IProductCategoryRepository _ProductCategoryRepository;
        private readonly IItemAttachmentRepository _itemAtachement;
        private readonly IFavoriteDesignRepository _favoriteDesign;
        private readonly ITemplateService _templateService;
        private readonly IPaymentGatewayRepository _paymentRepository;
        private readonly IInquiryRepository _inquiryRepository;
        private readonly IInquiryAttachmentRepository _inquiryAttachmentRepository;
        private readonly IOrderService _orderService;
        private readonly ICompanyService _myCompanyService;
        private readonly ISmartFormService _smartFormService;
        private readonly IProductCategoryItemRepository _ProductCategoryItemRepository;
        private readonly IItemSectionRepository _ItemSectionRepository;
        private readonly ISectionCostCentreRepository _ItemSectionCostCentreRepository;
        private readonly ITemplateRepository _TemplateRepository;
        private readonly ITemplatePageRepository _TemplatePageRepository;
        private readonly ITemplateBackgroundImagesRepository _TemplateBackgroundImagesRepository;
        private readonly ITemplateObjectRepository _TemplateObjectRepository;
        private readonly ICostCentreRepository _CostCentreRepository;
        private readonly IOrderRepository _OrderRepository;
        private readonly IPrefixRepository _prefixRepository;
        private readonly IItemVideoRepository _videoRepository;
        private readonly IMarkupRepository _markupRepository;
        private readonly IDiscountVoucherRepository _DVRepository;
        private readonly IItemsVoucherRepository _ItemVRepository;
        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public ItemService(IItemRepository ItemRepository, IItemStockOptionRepository StockOptions, ISectionFlagRepository SectionFlagRepository, ICompanyRepository CompanyRepository
            , IItemStockControlRepository StockRepository, IItemAddOnCostCentreRepository AddOnRepository, IProductCategoryRepository ProductCategoryRepository
            , IItemAttachmentRepository itemAtachement, IFavoriteDesignRepository FavoriteDesign, ITemplateService templateService
            , IPaymentGatewayRepository paymentRepository, IInquiryRepository inquiryRepository, IInquiryAttachmentRepository inquiryAttachmentRepository,
            IOrderService orderService, ICompanyService companyService, ISmartFormService smartformService, IProductCategoryItemRepository ProductCategoryItemRepository
            , IItemSectionRepository ItemSectionRepository, ISectionCostCentreRepository ItemSectionCostCentreRepository
            , ITemplateRepository TemplateRepository, ITemplatePageRepository TemplatePageRepository, ITemplateBackgroundImagesRepository TemplateBackgroundImagesRepository
            , ITemplateObjectRepository TemplateObjectRepository, ICostCentreRepository CostCentreRepository
            , IOrderRepository OrderRepository, IPrefixRepository prefixRepository, IItemVideoRepository videoRepository
            , IMarkupRepository markupRepository, IDiscountVoucherRepository DVRepository, IItemsVoucherRepository ItemVRepository)
        {
            this._ItemRepository = ItemRepository;
            this._StockOptions = StockOptions;
            this._SectionFlagRepository = SectionFlagRepository;
            this._CompanyRepository = CompanyRepository;
            this._StockRepository = StockRepository;
            this._AddOnRepository = AddOnRepository;
            this._ProductCategoryRepository = ProductCategoryRepository;
            this._itemAtachement = itemAtachement;
            this._favoriteDesign = FavoriteDesign;
            this._templateService = templateService;
            this._paymentRepository = paymentRepository;
            this._inquiryRepository = inquiryRepository;
            this._inquiryAttachmentRepository = inquiryAttachmentRepository;
            this._orderService = orderService;
            this._myCompanyService = companyService;
            this._smartFormService = smartformService;
            this._ProductCategoryItemRepository = ProductCategoryItemRepository;
            this._ItemSectionRepository = ItemSectionRepository;
            this._ItemSectionCostCentreRepository = ItemSectionCostCentreRepository;
            this._TemplateRepository = TemplateRepository;
            this._TemplatePageRepository = TemplatePageRepository;
            this._TemplateBackgroundImagesRepository = TemplateBackgroundImagesRepository;
            this._TemplateObjectRepository = TemplateObjectRepository;
            this._CostCentreRepository = CostCentreRepository;
            this._OrderRepository = OrderRepository;
            this._prefixRepository = prefixRepository;
            this._videoRepository = videoRepository;
            this._markupRepository = markupRepository;
            this._DVRepository = DVRepository;
            this._ItemVRepository = ItemVRepository;
        }

        public List<ItemStockOption> GetStockList(long ItemId, long CompanyId)
        {
            return _StockOptions.GetStockList(ItemId, CompanyId);
        }

        public Item GetItemById(long ItemId)
        {
            return _ItemRepository.GetItemById(ItemId);
        }
        public Item GetItemByIdDesigner(long ItemId)
        {
            return _ItemRepository.GetItemByIdDesigner(ItemId);
        }
        public Item CloneItem(long itemID, long RefItemID, long OrderID, long CustomerID, long TemplateID, long StockID, List<AddOnCostsCenter> SelectedAddOnsList, bool isSavedDesign, bool isCopyProduct, long objContactID, long OrganisationID, bool isUploadDesignMode = false)
        {

            try
            {
                Template clonedTemplate = null;

                ItemSection tblItemSectionCloned = new ItemSection();

                ItemAttachment Attacments = new ItemAttachment();

                SectionCostcentre tblISectionCostCenteresCloned = new SectionCostcentre();

                Item newItem = new Item();


                Item ActualItem = _ItemRepository.GetActualItemToClone(itemID);
                //******************new item*********************
                newItem = _ItemRepository.Clone<Item>(ActualItem);

                newItem.ItemId = 0;

                newItem.IsPublished = false;

                newItem.IsEnabled = false;

                newItem.EstimateId = OrderID;

                newItem.StatusId = (short)ItemStatuses.ShoppingCart; //tblStatuses.StatusID; //shopping cart

                newItem.Qty1 = 0; //qty

                newItem.Qty1BaseCharge1 = 0; //productSelection.PriceTotal + productSelection.AddonTotal; //item price

                newItem.Qty1Tax1Value = 0; // say vat

                newItem.Qty1NetTotal = 0;

                newItem.Qty1GrossTotal = 0;

                newItem.ProductType = 0;

                newItem.InvoiceId = null;

                newItem.EstimateProductionTime = ActualItem.EstimateProductionTime;

                newItem.DefaultItemTax = ActualItem.DefaultItemTax;

                newItem.ProductType = ActualItem.ProductType;

                newItem.DesignerCategoryId = ActualItem.DesignerCategoryId;

                newItem.TemplateType = ActualItem.TemplateType;

                if (isUploadDesignMode == true)
                {
                    newItem.TemplateId = null;
                }
                if (isCopyProduct)
                {
                    newItem.IsOrderedItem = true;
                    newItem.Qty1 = ActualItem.Qty1; //qty

                    newItem.Qty1BaseCharge1 = ActualItem.Qty1BaseCharge1;
                    //productSelection.PriceTotal + productSelection.AddonTotal; //item price

                    newItem.Qty1Tax1Value = ActualItem.Qty1Tax1Value; // say vat

                    newItem.Qty1NetTotal = ActualItem.Qty1NetTotal;

                    newItem.Qty1GrossTotal = ActualItem.Qty1GrossTotal;
                    newItem.ProductType = ActualItem.ProductType;
                    newItem.ProductName = ActualItem.ProductName + "- Copy";
                }
                else
                {
                    newItem.IsOrderedItem = false;
                    if (!isSavedDesign)  // in case of save designs ref item 
                        newItem.RefItemId = (int)itemID;
                    else
                        newItem.RefItemId = ActualItem.RefItemId;
                }



                // Default Mark up rate will be always 0 ...
                // when updating clone item we are getting markups from organisation ask sir naveed to change needed here also 
                //Markup markup = (from c in db.Markups
                //                 where c.MarkUpId == 1 && c.MarkUpRate == 0
                //                 select c).FirstOrDefault();

                //if (markup.MarkUpId != null)
                //    newItem.Qty1MarkUpId1 = (int)markup.MarkUpId;  //markup id
                //newItem.Qty1MarkUp1Value = markup.MarkUpRate;
                _ItemRepository.Add(newItem);
                _ItemRepository.SaveChanges();
                // db.Items.Add(newItem); //dbcontext added

                //*****************Existing item Sections and cost Centeres*********************************
                foreach (ItemSection tblItemSection in ActualItem.ItemSections.ToList())
                {
                    tblItemSectionCloned = Clone<ItemSection>(tblItemSection);
                    tblItemSectionCloned.ItemSectionId = 0;
                    tblItemSectionCloned.ItemId = newItem.ItemId;
                    _ItemSectionRepository.Add(tblItemSectionCloned);
                    _ItemSectionRepository.SaveChanges();
                    //db.ItemSections.Add(tblItemSectionCloned); //ContextAdded

                    //*****************Section Cost Centeres*********************************
                    if (tblItemSection.SectionCostcentres.Count > 0)
                    {
                        foreach (SectionCostcentre tblSectCostCenter in tblItemSection.SectionCostcentres.ToList())
                        {
                            tblISectionCostCenteresCloned = Clone<SectionCostcentre>(tblSectCostCenter);
                            tblISectionCostCenteresCloned.SectionCostcentreId = 0;
                            tblISectionCostCenteresCloned.ItemSectionId = tblItemSectionCloned.ItemSectionId;
                            _ItemSectionCostCentreRepository.Add(tblISectionCostCenteresCloned);
                            //db.SectionCostcentres.Add(tblISectionCostCenteresCloned);
                        }
                        _ItemSectionCostCentreRepository.SaveChanges();
                    }

                }
                //Copy Template if it does exists

                if (newItem.TemplateId.HasValue && newItem.TemplateId.Value > 0)
                {
                    clonedTemplate = new Template();
                    if (newItem.TemplateType == 1 || newItem.TemplateType == 2 || isSavedDesign || isCopyProduct)
                    {
                        long result = _TemplateRepository.CloneTemplateByTemplateID(newItem.TemplateId.Value); //db.sp_cloneTemplate((int)newItem.TemplateId.Value, 0, "");

                        long? clonedTemplateID = result;
                        clonedTemplate = _TemplateRepository.Find((int)clonedTemplateID);//  db.Templates.Where(g => g.ProductId == clonedTemplateID).Single();

                        var oCutomer = _CompanyRepository.Find(CustomerID); //db.Companies.Where(i => i.CompanyId == CustomerID).FirstOrDefault();
                        clonedTemplate.ProductName = clonedTemplate.ProductName == null ? newItem.ProductName : clonedTemplate.ProductName;
                        if (oCutomer != null)
                        {
                            clonedTemplate.TempString = oCutomer.WatermarkText;
                            clonedTemplate.isWatermarkText = oCutomer.isTextWatermark;
                            if (oCutomer.isTextWatermark == false)
                            {
                                clonedTemplate.TempString = HttpContext.Current.Server.MapPath("~/" + oCutomer.WatermarkText);
                            }

                        }
                        _TemplateRepository.SaveChanges();
                        // here 

                        //  VariablesResolve(itemID, clonedTemplate.ProductId, objContactID);
                    }

                }

                //db.SaveChanges();
                if (clonedTemplate != null && (newItem.TemplateType == 1 || newItem.TemplateType == 2 || isSavedDesign || isCopyProduct))
                {
                    newItem.TemplateId = clonedTemplate.ProductId;
                    TemplateID = clonedTemplate.ProductId;

                    CopyTemplatePaths(clonedTemplate, OrganisationID);
                    _TemplateRepository.SaveChanges();
                }

                //  SaveAdditionalAddonsOrUpdateStockItemType(SelectedAddOnsList, newItem.ItemId, StockID, isCopyProduct, "");
                // additional addon required the newly inserted cloneditem
                newItem.ItemCode = "ITM-0-001-" + newItem.ItemId;
                _ItemRepository.SaveChanges();
                //else
                //    throw 

                return newItem;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public List<ItemPriceMatrix> GetPriceMatrix(List<ItemPriceMatrix> tblRefItemsPriceMatrix, bool IsRanged, bool IsUserLoggedIn, long CompanyId, long OrganisationId)
        {
            int flagId = 0;
            if (IsUserLoggedIn)
            {
                flagId = GetFlagId(CompanyId, OrganisationId);
                if (flagId == 0)
                {
                    // pass 0  to get the default flag id for price matrix
                    flagId = GetFlagId(0, OrganisationId);
                    if (IsRanged == true)
                    {
                        tblRefItemsPriceMatrix = tblRefItemsPriceMatrix.Where(c => c.QtyRangeFrom > 0 && c.QtyRangeTo > 0 && c.FlagId == flagId && c.SupplierId == null).ToList();
                    }
                    else
                    {
                        tblRefItemsPriceMatrix = tblRefItemsPriceMatrix.Where(c => c.Quantity > 0 && c.FlagId == flagId && c.SupplierId == null).ToList();
                    }
                }
                else
                {
                    if (IsRanged == true)
                    {
                        tblRefItemsPriceMatrix = tblRefItemsPriceMatrix.Where(c => c.QtyRangeFrom > 0 && c.QtyRangeTo > 0 && c.FlagId == flagId && c.SupplierId == null).ToList();
                    }
                    else
                    {
                        tblRefItemsPriceMatrix = tblRefItemsPriceMatrix.Where(c => c.Quantity > 0 && c.FlagId == flagId && c.SupplierId == null).ToList();
                    }
                    if (tblRefItemsPriceMatrix.Count == 0)
                    {
                        if (IsRanged == true)
                        {
                            tblRefItemsPriceMatrix = tblRefItemsPriceMatrix.Where(c => c.QtyRangeFrom > 0 && c.QtyRangeTo > 0 && c.FlagId == flagId && c.SupplierId == null).ToList();
                        }
                        else
                        {
                            tblRefItemsPriceMatrix = tblRefItemsPriceMatrix.Where(c => c.Quantity > 0 && c.FlagId == flagId && c.SupplierId == null).ToList();
                        }
                    }
                }
            }
            else
            {
                // pass 0  to get the default flag id for price matrix
                flagId = GetFlagId(0, OrganisationId);
                if (IsRanged == true)
                {
                    tblRefItemsPriceMatrix = tblRefItemsPriceMatrix.Where(c => c.QtyRangeFrom > 0 && c.QtyRangeTo > 0 && c.FlagId == flagId && c.SupplierId == null).ToList();
                }
                else
                {
                    tblRefItemsPriceMatrix = tblRefItemsPriceMatrix.Where(c => c.Quantity > 0 && c.FlagId == flagId && c.SupplierId == null).ToList();
                }
            }

            return tblRefItemsPriceMatrix;
        }

        #endregion

        private int GetFlagId(long companyId, long OrganisationId)
        {
            if (companyId > 0)
            {
                return _CompanyRepository.GetPriceFlagIdByCompany(companyId) == null ? 0 : Convert.ToInt32(_CompanyRepository.GetPriceFlagIdByCompany(companyId));
            }
            else
            {
                return _SectionFlagRepository.GetDefaultSectionFlagId(OrganisationId);
            }
        }

        public string specialCharactersEncoder(string value)
        {
            value = value.Replace("/", "-");
            value = value.Replace(" ", "-");
            value = value.Replace(";", "-");
            value = value.Replace("&#34;", "");
            value = value.Replace("&", "");
            value = value.Replace("+", "");
            return value;
        }

        public ProductItem GetItemAndDetailsByItemID(long itemId)
        {
            return _ItemRepository.GetItemAndDetailsByItemID(itemId);
        }
        public List<ProductMarketBriefQuestion> GetMarketingInquiryQuestionsByItemID(int itemID)
        {
            return _ItemRepository.GetMarketingInquiryQuestionsByItemID(itemID);
        }
        public List<ProductMarketBriefAnswer> GetMarketingInquiryAnswersByQID(int QID)
        {
            return _ItemRepository.GetMarketingInquiryAnswersByQID(QID);
        }
        public void CopyAttachments(long ItemID, Item NewlyClonedItem, string OrderCode, bool CopyTemplate, DateTime OrderCreationDate, long OrganisationId, long CompanyId)
        {
            try
            {

                int sideNumber = 1;
                List<ItemAttachment> attachmentsOfActualItem = _ItemRepository.GetItemAttactchments(ItemID);
                List<ItemAttachment> attachmentsOfNewlyClonedItem = new List<ItemAttachment>();
                ItemAttachment obj = null;

                foreach (ItemAttachment attachment in attachmentsOfActualItem)
                {
                    obj = new ItemAttachment();

                    obj.ApproveDate = attachment.ApproveDate;
                    obj.Comments = attachment.Comments;
                    obj.ContactId = attachment.ContactId;
                    obj.ContentType = attachment.ContentType;
                    obj.CompanyId = attachment.CompanyId;
                    obj.FileTitle = attachment.FileTitle;
                    obj.FileType = attachment.FileType;
                    obj.FolderPath = "mpc_content/Attachments/" + OrganisationId + "/" + CompanyId + "/Products/" + NewlyClonedItem.ItemId;
                    obj.IsApproved = attachment.IsApproved;
                    obj.isFromCustomer = attachment.isFromCustomer;
                    obj.Parent = attachment.Parent;
                    obj.Type = attachment.Type;
                    obj.UploadDate = attachment.UploadDate;
                    obj.Version = attachment.Version;
                    obj.ItemId = NewlyClonedItem.ItemId;
                    if (NewlyClonedItem.TemplateId > 0)
                    {
                        obj.FileName = GetTemplateAttachmentFileName(NewlyClonedItem.ProductCode, OrderCode, NewlyClonedItem.ItemCode,
                            "Side" + sideNumber.ToString(), "", "", OrderCreationDate);

                    }
                    else
                    {
                        obj.FileName = GetAttachmentFileName(NewlyClonedItem.ProductCode, OrderCode, NewlyClonedItem.ItemCode,
                            sideNumber.ToString() + "Copy", "", "", OrderCreationDate);

                    }
                    sideNumber += 1;
                    attachmentsOfNewlyClonedItem.Add(obj);

                    // Copy physical file
                    string sourceFileName = null;
                    string destFileName = null;
                    if (NewlyClonedItem.TemplateId > 0 && CopyTemplate == true)
                    {
                        sourceFileName =
                            HttpContext.Current.Server.MapPath("/" + attachment.FolderPath + "/" + attachment.FileName + attachment.FileType);
                        destFileName = HttpContext.Current.Server.MapPath("/" + obj.FolderPath + "/" + obj.FileName + attachment.FileType);
                    }
                    else
                    {
                        sourceFileName = HttpContext.Current.Server.MapPath("/" + attachment.FolderPath + "/" + attachment.FileName + attachment.FileType);
                        destFileName = HttpContext.Current.Server.MapPath("/" + obj.FolderPath + "/" + obj.FileName + obj.FileType);
                    }

                    if (File.Exists(sourceFileName))
                    {
                        string destinationFolderDir = HttpContext.Current.Server.MapPath("~/" + obj.FolderPath);
                        if (!Directory.Exists(destinationFolderDir))
                            Directory.CreateDirectory(destinationFolderDir);

                        File.Copy(sourceFileName, destFileName);

                        // Generate the thumbnail

                        byte[] fileData = File.ReadAllBytes(destFileName);

                        if (obj.FileType == ".pdf" || obj.FileType == ".TIF" || obj.FileType == ".TIFF")
                        {
                            GenerateThumbnailForPdf(fileData, destFileName, false);
                        }
                        else
                        {
                            MemoryStream ms = new MemoryStream();
                            ms.Write(fileData, 0, fileData.Length);

                            CreatAndSaveThumnail(ms, destFileName);
                        }
                    }
                    _ItemRepository.AddAttachment(obj);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool RemoveCloneItem(long itemID, out List<ArtWorkAttatchment> itemAttatchmetList, out Template clonedTemplateToRemove)
        {
            try
            {

                return _ItemRepository.RemoveCloneItem(itemID, out itemAttatchmetList, out clonedTemplateToRemove);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AddOnCostsCenter> GetStockOptionCostCentres(long itemId, long companyId)
        {
            return _AddOnRepository.AddOnsPerStockOption(itemId, companyId);
        }
        public ItemStockControl GetStockItem(long itemId)
        {
            try
            {
                return _StockRepository.GetStockOfItemById(itemId);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool UpdateCloneItemService(long clonedItemID, double orderedQuantity, double itemPrice, double addonsPrice, long stockItemID, List<AddOnCostsCenter> newlyAddedCostCenters, int Mode, long OrganisationId, double TaxRate, string ItemMode, bool isInculdeTax, long ItemstockOptionID,
            long StoreId, int CountOfUploads = 0, string QuestionQueue = "", string CostCentreQueue = "", string InputQueue = "")
        {
            // return _ItemRepository.UpdateCloneItem(clonedItemID, orderedQuantity, itemPrice, addonsPrice, stockItemID, newlyAddedCostCenters, Mode, OrganisationId, TaxRate, ItemMode, isInculdeTax, ItemstockOptionID, CountOfUploads, QuestionQueue, CostCentreQueue, InputQueue);
            bool result = false;

            ItemSection FirstItemSection = null;

            double currentTotal = 0;

            double netTotal = 0;

            double grossTotal = 0;

            double? markupRate = 0;

            
            double DiscountAmountToApply = 0;

            try
            {
                Item clonedItem = null;

                clonedItem = _ItemRepository.GetItemByItemID(clonedItemID);// db.Items.Where(i => i.ItemId == clonedItemID).FirstOrDefault();

                long? markupid = 1;


                Markup OrgMarkup = _markupRepository.GetDefaultMarkupsByOrganisationId(OrganisationId); //db.Markups.Where(m => m.OrganisationId == OrganisationId && m.IsDefault == true).FirstOrDefault();

                if (OrgMarkup != null)
                {
                    markupid = 0;//OrgMarkup.MarkUpId;
                    markupRate = 0;//(int)OrgMarkup.MarkUpRate;
                }
                else
                {
                    markupid = 0;
                    markupRate = 0;
                }

                netTotal = itemPrice + addonsPrice + markupRate ?? 0;

                //DiscountVoucher storeDiscountVoucher = _DVRepository.GetStoreDefaultDiscountRate(StoreId, OrganisationId);
                //if (storeDiscountVoucher != null)
                //{
                //    DiscountAmountToApply = GetDiscountAmountByVoucher(storeDiscountVoucher, netTotal, clonedItem.RefItemId ?? 0, orderedQuantity, clonedItem.DiscountVoucherID);
                //    clonedItem.DiscountVoucherID = storeDiscountVoucher.DiscountVoucherId;
                //}
                if (CountOfUploads > 0)
                {
                    clonedItem.ProductName = clonedItem.ProductName + " " + CountOfUploads + " file(s) uploaded";
                }

                clonedItem.Qty1 = (int)orderedQuantity;

                clonedItem.IsOrderedItem = true;



                if (isInculdeTax == true)
                {
                    if (clonedItem.DefaultItemTax != null)
                    {
                        clonedItem.Tax1 = Convert.ToInt32(clonedItem.DefaultItemTax);

                        //double TaxAppliedOnCostCentreTotal = _ItemRepository.CalculatePercentage(addonsPrice, Convert.ToDouble(clonedItem.DefaultItemTax));// ((addonsPrice * Convert.ToDouble(clonedItem.DefaultItemTax)) / 100);

                        //netTotal = itemPrice + addonsPrice + markupRate ?? 0;

                        netTotal = netTotal - DiscountAmountToApply;

                        double TaxAppliedOnItemTotal = _ItemRepository.CalculatePercentage(netTotal, Convert.ToDouble(clonedItem.DefaultItemTax));// ((itemPrice * Convert.ToDouble(clonedItem.DefaultItemTax)) / 100); 

                        grossTotal = netTotal + TaxAppliedOnItemTotal;
                        clonedItem.Qty1Tax1Value = TaxAppliedOnItemTotal;//GetTaxPercentage(netTotal, Convert.ToDouble(clonedItem.DefaultItemTax));
                    }
                    else
                    {
                        clonedItem.Tax1 = Convert.ToInt32(TaxRate);
                        
                       // double TaxAppliedOnCostCentreTotal = _ItemRepository.CalculatePercentage(addonsPrice, TaxRate); //(addonsPrice * TaxRate / 100);

                        netTotal = netTotal - DiscountAmountToApply;

                        double TaxAppliedOnItemTotal = _ItemRepository.CalculatePercentage(netTotal, TaxRate); //(itemPrice * TaxRate / 100);

                        grossTotal = netTotal + TaxAppliedOnItemTotal;//CalculatePercentage(netTotal, TaxRate);

                        clonedItem.Qty1Tax1Value = TaxAppliedOnItemTotal;//GetTaxPercentage(netTotal, TaxRate);
                    }
                }
                else
                {
                    clonedItem.Tax1 = Convert.ToInt32(TaxRate);

                    netTotal = netTotal - DiscountAmountToApply;

                    grossTotal = netTotal + _ItemRepository.CalculatePercentage(netTotal, TaxRate);

                    clonedItem.Qty1Tax1Value = _ItemRepository.CalculatePercentage(netTotal, TaxRate);
                }


                //******************Existing item update*********************
                clonedItem.Qty1MarkUp1Value = markupRate;

                clonedItem.Qty1MarkUpId1 = (int)markupid;

                clonedItem.Qty1BaseCharge1 = netTotal;

                clonedItem.Qty1NetTotal = netTotal;

                clonedItem.Qty1GrossTotal = grossTotal;

                clonedItem.Qty1CostCentreProfit = DiscountAmountToApply;



                FirstItemSection = _ItemSectionRepository.GetFirstSectionOfItem(clonedItem.ItemId);
                //clonedItem.ItemSections.Where(sec => sec.SectionNo == 1 && sec.ItemId == clonedItem.ItemId)
                //    .FirstOrDefault();

                result = SaveAdditionalAddonsOrUpdateStockItemType(newlyAddedCostCenters, stockItemID, FirstItemSection,
                    ItemMode, ItemstockOptionID); // additional addon required the newly inserted cloneditem

                FirstItemSection.Qty1 = clonedItem.Qty1;

                FirstItemSection.BaseCharge1 = clonedItem.Qty1BaseCharge1;



                FirstItemSection.Qty1MarkUpID = (int)markupid;
                FirstItemSection.QuestionQueue = QuestionQueue;
                FirstItemSection.InputQueue = InputQueue;
                FirstItemSection.CostCentreQueue = CostCentreQueue;


                bool isNewSectionCostCenter = false;


                List<SectionCostcentre> listOfSectionCostCentres = _ItemSectionCostCentreRepository.GetAllSectionCostCentres(FirstItemSection.ItemSectionId); //db.SectionCostcentres.Where(c => c.ItemSectionId == FirstItemSection.ItemSectionId).ToList();

                SectionCostcentre sectionCC = null;
                foreach (var ccItem in listOfSectionCostCentres)
                {
                    if (ccItem.CostCentre != null)
                    {
                        if (ccItem.CostCentre.Type == 29)
                        {
                            sectionCC = ccItem;
                        }
                    }
                }


                if (sectionCC == null)
                {
                    sectionCC = new SectionCostcentre();

                    sectionCC.Qty1MarkUpID = 1;
                    sectionCC.Qty1Charge = itemPrice;
                    sectionCC.Qty1NetTotal = itemPrice;

                    isNewSectionCostCenter = true;
                }

                if (isNewSectionCostCenter)
                {
                    //29 is the global type of web order cost centre
                    var oCostCentre = _CostCentreRepository.GetGlobalWebOrderCostCentre(OrganisationId); //db.CostCentres.Where(g => g.Type == 29 && g.OrganisationId == OrganisationId).SingleOrDefault();
                    if (oCostCentre != null)
                    {
                        sectionCC.Name = oCostCentre.Name;
                        sectionCC.CostCentreId = oCostCentre.CostCentreId;
                        sectionCC.ItemSectionId = FirstItemSection.ItemSectionId;

                        _ItemSectionCostCentreRepository.Add(sectionCC);

                    }
                    else
                    {
                        throw new Exception("Critcal Error, We have lost our main costcentre.", null);
                    }
                }

                _ItemRepository.SaveChanges();
                _ItemSectionRepository.SaveChanges();
                _ItemSectionCostCentreRepository.SaveChanges();

                result = true;
            }
            catch (Exception)
            {
                result = false;
                throw;
            }

            return result;
        }



        public ProductCategoriesView GetMappedCategory(string CatName, int CID)
        {

            return _ProductCategoryRepository.GetMappedCategory(CatName, CID);
        }

        public Item GetExisitingClonedItemInOrder(long OrderId, long ReferenceItemId)
        {
            return _ItemRepository.GetClonedItemByOrderId(OrderId, ReferenceItemId);
        }
        //get related items list
        public List<ProductItem> GetRelatedItemsList()
        {

            return _ItemRepository.GetRelatedItemsList();
        }

        public List<ItemAttachment> GetArtwork(long ItemId)
        {

            return _itemAtachement.GetArtworkAttachments(ItemId);
        }
        /// <summary>
        /// get an item according to usercookiemanager.orderid or itemid 
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public Item GetItemByOrderAndItemID(long ItemID, long OrderID)
        {
            return _ItemRepository.GetItemByOrderAndItemID(ItemID, OrderID);
        }

        /// <summary>
        /// to find the minimun price of specific Product by itemid
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public double FindMinimumPriceOfProduct(long itemID)
        {
            return _ItemRepository.FindMinimumPriceOfProduct(itemID);
        }
        /// <summary>
        /// get name of parent categogry by categoryID
        /// </summaery>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public string GetImmidiateParentCategory(long categoryID, out string CategoryName)
        {
            try
            {
                return _ProductCategoryRepository.GetImmidiateParentCategory(categoryID, out CategoryName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// get list of all stock options by item and company id 
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="companyID"></param>
        /// <returns></returns>
        public List<ItemStockOption> GetAllStockListByItemID(long ItemID, long companyID)
        {
            try
            {
                return _StockOptions.GetAllStockListByItemID(ItemID, companyID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<string> GetProductItemAddOnCostCentres(long StockOptionID, long CompanyID)
        {
            try
            {
                return _AddOnRepository.GetProductItemAddOnCostCentres(StockOptionID, CompanyID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// get all related items according to item id
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public List<ProductItem> GetRelatedItemsByItemID(long ItemID)
        {
            try
            {
                return _ItemRepository.GetRelatedItemsByItemID(ItemID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// get default section price flag 
        /// </summary>
        /// <returns></returns>
        public int GetDefaultSectionPriceFlag()
        {
            try
            {
                return _ItemRepository.GetDefaultSectionPriceFlag();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemImage> getItemImagesByItemID(long ItemID)
        {
            try
            {
                return _ItemRepository.getItemImagesByItemID(ItemID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FavoriteDesign GetFavContactDesign(long templateID, long contactID)
        {
            try
            {
                return _favoriteDesign.GetFavContactDesign(templateID, contactID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public long PostLoginCustomerAndCardChanges(long OrderId, long CompanyId, long ContactId, long TemporaryCompanyId, long OrganisationId)
        {
            try
            {
                List<ArtWorkAttatchment> orderAllItemsAttatchmentsListToBeRemoved = null;
                List<Template> clonedTempldateFilesList = null;

                if (OrderId > 0)
                {
                    bool isUpdateOrder = _ItemRepository.isTemporaryOrder(OrderId, CompanyId, ContactId);
                    if (isUpdateOrder)
                    {
                        long orderId = _ItemRepository.UpdateTemporaryCustomerOrderWithRealCustomer(TemporaryCompanyId, CompanyId, ContactId, OrderId, OrganisationId, out orderAllItemsAttatchmentsListToBeRemoved, out clonedTempldateFilesList);
                        if (orderId > 0)
                        {
                            RemoveItemAttacmentPhysically(orderAllItemsAttatchmentsListToBeRemoved);
                            RemoveItemTemplateFilesPhysically(clonedTempldateFilesList, OrganisationId);
                            return orderId;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        return OrderId;
                    }

                }
                else
                {
                    return OrderId;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                return 0;
            }
        }

        /// <summary>
        /// Remove Item Attachments
        /// </summary>
        /// <param name="attatchmentList"></param>
        public void RemoveItemAttacmentPhysically(List<ArtWorkAttatchment> attatchmentList)
        {
            string completePath = string.Empty;
            try
            {
                if (attatchmentList != null)
                {
                    foreach (ArtWorkAttatchment itemAtt in attatchmentList)
                    {
                        completePath = HttpContext.Current.Server.MapPath(itemAtt.FolderPath + itemAtt.FileName);
                        if (itemAtt.UploadFileType == UploadFileTypes.Artwork)
                        {
                            //delete the thumb nails as well.
                            completePath = completePath.Replace(itemAtt.FileExtention, "Thumb.png");

                            if (System.IO.File.Exists(completePath))
                            {
                                System.IO.File.Delete(completePath);
                            }
                        }
                        if (System.IO.File.Exists(completePath))
                        {
                            System.IO.File.Delete(completePath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Remove Template files
        /// </summary>
        /// <param name="clonedTempldateFiles"></param>
        private void RemoveItemTemplateFilesPhysically(List<Template> clonedTempldateFilesList, long OrganisationId)
        {
            try
            {
                if (clonedTempldateFilesList != null)
                {
                    foreach (Template templ in clonedTempldateFilesList)
                    {
                        if (templ.ProductId > 0)
                        {
                            _templateService.DeleteTemplateFiles(templ.ProductId, OrganisationId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<usp_GetRealEstateProducts_Result> GetRealEstateProductsByCompanyID(long companyId)
        {
            return _ItemRepository.GetRealEstateProductsByCompanyID(companyId);
        }
        public Item GetItemByOrderID(long OrderID)
        {
            try
            {
                return _ItemRepository.GetItemByOrderID(OrderID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GenerateThumbnailForPdf(string url, bool insertCuttingMargin, long ItemId)
        {
            using (Doc theDoc = new Doc())
            {
                theDoc.Read(url);
                theDoc.PageNumber = 1;
                theDoc.Rect.String = theDoc.CropBox.String;
                double cuttingMargin = 14.173228345;
                if (insertCuttingMargin)
                {
                    theDoc.Rect.Inset(cuttingMargin, cuttingMargin);
                }

                Stream oImgstream = new MemoryStream();

                theDoc.Rendering.DotsPerInch = 300;
                theDoc.Rendering.Save("tmp.png", oImgstream);

                theDoc.Clear();
                theDoc.Dispose();

                CreatAndSaveThumnail(oImgstream, url, ItemId + "/");

            }
        }
        public string SaveDesignAttachments(long templateID, long itemID, long customerID, string DesignName, string caller, long organisationId)
        {
            return _ItemRepository.SaveDesignAttachments(templateID, itemID, customerID, DesignName, caller, organisationId);
        }
        public bool CreatAndSaveThumnail(Stream oImgstream, string sideThumbnailPath, string itemId)
        {
            Image origImage = null;
            try
            {
                string orgPath = sideThumbnailPath;
                string baseAddress = sideThumbnailPath.Substring(0, sideThumbnailPath.LastIndexOf('\\'));
                sideThumbnailPath = Path.GetFileNameWithoutExtension(sideThumbnailPath) + "Thumb.png";


                sideThumbnailPath = baseAddress + "\\" + itemId + sideThumbnailPath;

                if (oImgstream != null)
                {
                    origImage = Image.FromStream(oImgstream);
                }
                else
                {
                    origImage = Image.FromFile(orgPath);
                }


                float WidthPer, HeightPer;

                int NewWidth, NewHeight;
                int ThumbnailSizeWidth = 400;
                int ThumbnailSizeHeight = 400;

                if (origImage.Width > origImage.Height)
                {
                    NewWidth = ThumbnailSizeWidth;
                    WidthPer = (float)ThumbnailSizeWidth / origImage.Width;
                    NewHeight = Convert.ToInt32(origImage.Height * WidthPer);
                }
                else
                {
                    NewHeight = ThumbnailSizeHeight;
                    HeightPer = (float)ThumbnailSizeHeight / origImage.Height;
                    NewWidth = Convert.ToInt32(origImage.Width * HeightPer);
                }

                Bitmap origThumbnail = new Bitmap(NewWidth, NewHeight, origImage.PixelFormat);
                Graphics oGraphic = Graphics.FromImage(origThumbnail);
                oGraphic.CompositingQuality = CompositingQuality.HighQuality;
                oGraphic.SmoothingMode = SmoothingMode.HighQuality;
                oGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Rectangle oRectangle = new Rectangle(0, 0, NewWidth, NewHeight);
                oGraphic.DrawImage(origImage, oRectangle);


                origThumbnail.Save(sideThumbnailPath, ImageFormat.Png);
                origImage.Dispose();
                oGraphic.Dispose();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                origImage.Dispose();
            }
        }
        public List<ItemAttachment> SaveArtworkAttachments(List<ItemAttachment> attachmentList)
        {
            return _itemAtachement.SaveArtworkAttachments(attachmentList);
        }
        /// <summary>
        /// gets the cloned item
        /// </summary>
        /// <param name="ItemId"></param>
        /// <returns></returns>
        public Item GetClonedItemById(long ItemId)
        {
            return _ItemRepository.GetClonedItemById(ItemId);
        }
        /// <summary>
        /// returns the active payment gateway
        /// </summary>
        /// <returns></returns>
        public PaymentGateway GetPaymentGatewayRecord(long CompanyId)
        {

            return _paymentRepository.GetPaymentGatewayRecord(CompanyId);


        }
        public long GetFirstItemIdByOrderId(long orderId)
        {

            return _ItemRepository.GetFirstItemIdByOrderId(orderId);


        }
        /// <summary>
        /// add payment
        /// </summary>
        /// <returns></returns>
        public long AddPrePayment(PrePayment prePayment)
        {

            return _paymentRepository.AddPrePayment(prePayment);


        }
        public List<Item> GetItemsByOrderID(long OrderID)
        {
            try
            {
                return _ItemRepository.GetItemsByOrderID(OrderID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Item> GetListOfDeliveryItemByOrderID(long OID)
        {
            try
            {
                return _ItemRepository.GetListOfDeliveryItemByOrderID(OID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool RemoveListOfDeliveryItemCostCenter(long OrderId)
        {
            try
            {
                return _ItemRepository.RemoveListOfDeliveryItemCostCenter(OrderId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool AddUpdateItemFordeliveryCostCenter(long orderId, long DeliveryCostCenterId, double DeliveryCost, long customerID, string DeliveryName, StoreMode Mode, bool isDeliveryTaxable, bool IstaxONService, double GetServiceTAX, double TaxRate)
        {
            try
            {
                return _ItemRepository.AddUpdateItemFordeliveryCostCenter(orderId, DeliveryCostCenterId, DeliveryCost, customerID, DeliveryName, Mode, isDeliveryTaxable, IstaxONService, GetServiceTAX, TaxRate);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Item GetItemByOrderItemID(long ItemID, long OrderID)
        {
            try
            {
                return _ItemRepository.GetItemByOrderAndItemID(ItemID, OrderID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// add in
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="DeliveryCostCenterId"></param>
        /// <param name="DeliveryCost"></param>
        /// <param name="customerID"></param>
        /// <param name="DeliveryName"></param>
        /// <param name="Mode"></param>
        /// <param name="isDeliveryTaxable"></param>
        /// <param name="IstaxONService"></param>
        /// <param name="GetServiceTAX"></param>
        /// <param name="TaxRate"></param>
        /// <returns></returns>
        public long AddInquiryAndItems(Inquiry Inquiry, List<InquiryItem> InquiryItems)
        {
            try
            {
                return _inquiryRepository.AddInquiryAndItems(Inquiry, InquiryItems);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AddInquiryAttachments(List<InquiryAttachment> InquiryAttachments)
        {
            _inquiryAttachmentRepository.SaveInquiryAttachments(InquiryAttachments);

        }

        public ItemSection GetItemFirstSectionByItemId(long ItemId)
        {
            return _ItemRepository.GetItemFirstSectionByItemId(ItemId);

        }
        public ItemSection UpdateItemFirstSectionByItemId(long ItemId, int Quantity)
        {
            return _ItemRepository.UpdateItemFirstSectionByItemId(ItemId, Quantity);
        }
        /// <summary>
        /// get id's of cost center except webstore cost cnetre 216 of first section of cloned item 
        /// </summary>
        /// <param name="StockOptionID"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public List<SectionCostcentre> GetClonedItemAddOnCostCentres(long ItemId, long OrganisationId)
        {
            try
            {
                ItemSection firstSection = _ItemSectionRepository.GetFirstSectionOfItem(ItemId);
                if (firstSection != null)
                {
                    CostCentre webOrderCC = _CostCentreRepository.GetWebOrderCostCentre(OrganisationId);
                    List<SectionCostcentre> sectionCCList = _ItemSectionCostCentreRepository.GetAllSectionCostCentres(firstSection.ItemSectionId);
                    if (webOrderCC != null)
                    {
                        sectionCCList = sectionCCList.Where(s => s.CostCentreId != webOrderCC.CostCentreId).ToList();
                        return sectionCCList;
                    }
                    else
                    {
                        return sectionCCList;
                    }
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// get cart items count 
        /// </summary>
        /// <returns></returns>
        public long GetCartItemsCount(long ContactId, long TemporaryCustomerId, long CompanyId)
        {
            try
            {
                return _ItemRepository.GetCartItemsCount(ContactId, TemporaryCustomerId, CompanyId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CmsSkinPageWidget> GetStoreWidgets()
        {
            try
            {
                return _ItemRepository.GetStoreWidgets();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SaveDesignView> GetSavedDesigns(long ContactID)
        {
            try
            {
                return _ItemRepository.GetSavedDesigns(ContactID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public void RemoveItemAttacmentPhysically(List<ArtWorkAttatchment> attatchmentList)
        //{
        //    try
        //    {
        //        _ItemRepository.RemoveItemAttacmentPhysically(attatchmentList);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<Item> GetProductsWithDisplaySettings(ProductWidget productWidgetId, long CompanyId, long OrganisationId)
        {

            List<Item> productsAllList = null;
            List<Item> filteredList = null;

            switch (productWidgetId)
            {

                case ProductWidget.FeaturedProducts:
                    productsAllList = GetDisplayProductsWithDisplaySettings(null, null, CompanyId, OrganisationId);
                    filteredList = productsAllList;//ProductManager.GetSearchedProducts((int)productWidget, productsAllList);

                    break;
                case ProductWidget.PopularProducts:

                    productsAllList = GetDisplayProductsWithDisplaySettings(true, null, CompanyId, OrganisationId); //popular

                    if (productsAllList != null & productsAllList.Count > 0)
                        filteredList = productsAllList.ToList();

                    break;
                case ProductWidget.SpecialProducts:

                    productsAllList = GetDisplayProductsWithDisplaySettings(null, true, CompanyId, OrganisationId); //promotional or special
                    if (productsAllList != null & productsAllList.Count > 0)
                    {
                        // filteredList = ProductManager.GetSearchedProducts((int)productWidget, productsAllList);

                        if (filteredList != null)
                            filteredList = filteredList.ToList();

                        break;
                    }
                    else
                    {
                        filteredList = null;
                        break;
                    }
            }

            if (filteredList != null)
                return filteredList.OrderBy(i => i.SortOrder).ToList();
            return null;

        }

        private List<Item> GetDisplayProductsWithDisplaySettings(bool? isPopularProd, bool? isPromotionProduct, long CompanyId, long OrganisationId)
        {
            //Note:All promotional or spceial will also be a featured product
            if (isPopularProd == null && isPromotionProduct == null)
            {
                return _ItemRepository.GetProductsList(CompanyId, OrganisationId).Where(i => i.IsFeatured == true).OrderBy(i => i.SortOrder).ToList();
            }
            else if (isPopularProd == true && isPromotionProduct == null)
            {
                return _ItemRepository.GetProductsList(CompanyId, OrganisationId).Where(i => i.IsPopular == true).OrderBy(i => i.SortOrder).ToList();
            }
            else
            {
                return _ItemRepository.GetProductsList(CompanyId, OrganisationId).Where(i => i.IsSpecialItem == true).OrderBy(i => i.SortOrder).ToList();
            }
        }

        //private List<Item> GetSearchedProducts(int offerTypeId, List<Item> productsAllList)
        //{
        //    Random rand = new Random();
        //    List<Int32> result = new List<Int32>();
        //    List<Item> filteredProdcuts = null;
        //    int ii = 0;
        //    if (productsAllList.Count > 0)
        //    {
        //        //Choose from the offers

        //        filteredProdcuts = productsAllList.FindAll(item => item.OfferType.HasValue && item.OfferType.Value == offerTypeId).ToList();
        //        if (filteredProdcuts.Count == 0)

        //            filteredProdcuts = productsAllList.FindAll(item => (item.OfferType.HasValue == false || item.OfferType.Value != offerTypeId)).OrderByDescending(item => item.ItemID).ToList();

        //        int seed = 1, increment = 3;
        //        int n = filteredProdcuts.Count;

        //        int x = seed;
        //        for (int i = 0; i < n; i++)
        //        {
        //            x = (x + increment) % n;
        //            result.Add(x);
        //        }
        //        //for (Int32 i = 0; i < productsAllList.Count; i++)
        //        //{
        //        //    int MAx = productsAllList.Count + 1;
        //        //    Int32 curValue = rand.Next(1, MAx);
        //        //    while (result.Contains(curValue))//result.Exists(value => value == curValue))
        //        //    {
        //        //        curValue = rand.Next(1, 11);
        //        //    }

        //        //    result.Add(curValue);
        //        //}
        //        foreach (var i in filteredProdcuts)
        //        {
        //            i.SortOrder = result[ii];
        //            ii++;
        //        }

        //    }

        //    return filteredProdcuts.OrderBy(item => item.SortOrder).ToList();

        //}


        /// <summary>
        /// get all parent categories and corresponding products of a category against a store
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="OrganisationId"></param>
        /// <returns></returns>
        public List<ProductCategory> GetStoreParentCategories(long CompanyId, long OrganisationId)
        {
            try
            {
                return _ItemRepository.GetStoreParentCategories(CompanyId, OrganisationId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public long getParentTemplateID(long itemId)
        {
            return _ItemRepository.getParentTemplateID(itemId);
        }
        // called from category page to generate template and order if skip designer mode is selected
        public string ProcessCorpOrderSkipDesignerMode(long WEBOrderId, int WEBStoreMode, long TemporaryCompanyId, long OrganisationId, long CompanyID, long ContactID, long itemID)
        {
            long ItemID = 0;
            long TemplateID = 0;
            long OrderID = 0;
            bool printCropMarks = true;
            bool printWaterMark = true;
            bool isMultiplageProduct = false;
            if (WEBOrderId == 0)
            {

                long TemporaryRetailCompanyId = 0;
                if (WEBStoreMode == (int)StoreMode.Retail)
                {
                    TemporaryRetailCompanyId = TemporaryCompanyId;
                    OrderID = _orderService.ProcessPublicUserOrder(string.Empty, OrganisationId, StoreMode.Retail, CompanyID, ContactID, ref TemporaryRetailCompanyId);
                    if (OrderID > 0)
                    {
                        WEBOrderId = OrderID;
                    }
                    if (TemporaryRetailCompanyId != 0)
                    {
                        TemporaryCompanyId = TemporaryRetailCompanyId;
                        ContactID = _myCompanyService.GetContactIdByCompanyId(TemporaryRetailCompanyId);
                    }
                    CompanyID = TemporaryRetailCompanyId;

                }
                else
                {

                    OrderID = _orderService.ProcessPublicUserOrder(string.Empty, OrganisationId, (StoreMode)WEBStoreMode, CompanyID, ContactID, ref TemporaryRetailCompanyId);
                    if (OrderID > 0)
                    {
                        WEBOrderId = OrderID;
                    }
                }

                // create new order


                Item item = CloneItem(itemID, 0, OrderID, CompanyID, 0, 0, null, false, false, ContactID, OrganisationId);

                if (item != null)
                {
                    ItemID = item.ItemId;
                    TemplateID = item.TemplateId ?? 0;
                    if (item.printCropMarks.HasValue)
                        printCropMarks = item.printCropMarks.Value;
                    if (item.drawWaterMarkTxt.HasValue)
                        printWaterMark = item.drawWaterMarkTxt.Value;
                    if (item.isMultipagePDF.HasValue)
                        isMultiplageProduct = item.isMultipagePDF.Value;
                }

            }
            else
            {
                if (TemporaryCompanyId == 0 && WEBStoreMode == (int)StoreMode.Retail && ContactID == 0)
                {
                    long TemporaryRetailCompanyId = TemporaryCompanyId;

                    // create new order

                    OrderID = _orderService.ProcessPublicUserOrder(string.Empty, OrganisationId, (StoreMode)WEBStoreMode, CompanyID, ContactID, ref TemporaryRetailCompanyId);
                    if (OrderID > 0)
                    {
                        WEBOrderId = OrderID;
                    }
                    if (TemporaryRetailCompanyId != 0)
                    {
                        TemporaryCompanyId = TemporaryRetailCompanyId;
                        ContactID = _myCompanyService.GetContactIdByCompanyId(TemporaryRetailCompanyId);
                    }
                    CompanyID = TemporaryRetailCompanyId;
                }
                else if (TemporaryCompanyId > 0 && WEBStoreMode == (int)StoreMode.Retail)
                {
                    CompanyID = TemporaryCompanyId;
                    ContactID = _myCompanyService.GetContactIdByCompanyId(CompanyID);
                }
                Item item = CloneItem(itemID, 0, WEBOrderId, CompanyID, 0, 0, null, false, false, ContactID, OrganisationId);

                if (item != null)
                {
                    ItemID = item.ItemId;
                    TemplateID = item.TemplateId ?? 0;
                    if (item.printCropMarks.HasValue)
                        printCropMarks = item.printCropMarks.Value;
                    if (item.drawWaterMarkTxt.HasValue)
                        printWaterMark = item.drawWaterMarkTxt.Value;
                    if (item.isMultipagePDF.HasValue)
                        isMultiplageProduct = item.isMultipagePDF.Value;

                }
            }
            //resolve template variables 
            _smartFormService.AutoResolveTemplateVariables(ItemID, ContactID);
            _templateService.processTemplatePDF(TemplateID, OrganisationId, printCropMarks, printWaterMark, false, isMultiplageProduct);
            return ItemID + "_" + TemplateID + "_" + WEBOrderId + "_" + TemporaryCompanyId;
            //update temporary customer id (for case of retail) and order id 
        }
        /// <summary>
        /// get category name
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public string GetCategoryNameById(long CategoryId, long ItemId)
        {
            if (CategoryId > 0)
            {
                return _ProductCategoryRepository.GetCategoryById(CategoryId).CategoryName;
            }
            else
            {
                CategoryId = _ProductCategoryItemRepository.GetCategoryId(ItemId) ?? 0;
                if (CategoryId > 0)
                {
                    return _ProductCategoryRepository.GetCategoryById(CategoryId).CategoryName;
                }
                else
                {
                    return "";
                }
            }

        }
        /// <summary>
        /// get category id
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public long GetCategoryIdByItemId(long ItemId)
        {
            return _ProductCategoryItemRepository.GetCategoryId(ItemId) ?? 0;

        }

        public static string GetAttachmentFileName(string ProductCode, string OrderCode, string ItemCode,
         string SideCode, string VirtualFolderPath, string extension, DateTime OrderCreationDate)
        {
            try
            {
                string FileName = OrderCreationDate.Year.ToString() + OrderCreationDate.ToString("MMMM") +
                             OrderCreationDate.Day.ToString() + "-" + ProductCode + "-" + OrderCode + "-" + ItemCode +
                             "-" + SideCode + extension;
                //checking whether file exists or not
                while (System.IO.File.Exists(VirtualFolderPath + FileName))
                {
                    string fileName1 = System.IO.Path.GetFileNameWithoutExtension(FileName);
                    fileName1 += "a";
                    FileName = fileName1 + extension;
                }
                return FileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static string GetTemplateAttachmentFileName(string ProductCode, string OrderCode, string ItemCode,
          string SideCode, string VirtualFolderPath, string extension, DateTime CreationDate)
        {
            try
            {
                string FileName = CreationDate.Year.ToString() + CreationDate.ToString("MMMM") + CreationDate.Day.ToString() +
                             "-" + ProductCode + "-" + OrderCode + "-" + ItemCode + "-" + SideCode + extension;

                return FileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void GenerateThumbnailForPdf(byte[] PDFFile, string sideThumbnailPath, bool insertCuttingMargin)
        {
            try
            {
                //
                using (Doc theDoc = new Doc())
                {
                    theDoc.Read(PDFFile);
                    theDoc.PageNumber = 1;
                    theDoc.Rect.String = theDoc.CropBox.String;

                    if (insertCuttingMargin)
                    {
                        theDoc.Rect.Inset((int)MPC.Models.Common.Constants.CuttingMargin,
                            (int)MPC.Models.Common.Constants.CuttingMargin);
                    }

                    Stream oImgstream = new MemoryStream();

                    theDoc.Rendering.DotsPerInch = 300;
                    theDoc.Rendering.Save("tmp.png", oImgstream);

                    theDoc.Clear();
                    theDoc.Dispose();

                    CreatAndSaveThumnail(oImgstream, sideThumbnailPath);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool CreatAndSaveThumnail(Stream oImgstream, string sideThumbnailPath)
        {
            try
            {
                string baseAddress = sideThumbnailPath.Substring(0, sideThumbnailPath.LastIndexOf('\\'));
                sideThumbnailPath = Path.GetFileNameWithoutExtension(sideThumbnailPath) + "Thumb.png";

                sideThumbnailPath = baseAddress + "\\" + sideThumbnailPath;

                Image origImage = Image.FromStream(oImgstream);

                float WidthPer, HeightPer;

                int NewWidth, NewHeight;
                int ThumbnailSizeWidth = 400;
                int ThumbnailSizeHeight = 400;

                if (origImage.Width > origImage.Height)
                {
                    NewWidth = ThumbnailSizeWidth;
                    WidthPer = (float)ThumbnailSizeWidth / origImage.Width;
                    NewHeight = Convert.ToInt32(origImage.Height * WidthPer);
                }
                else
                {
                    NewHeight = ThumbnailSizeHeight;
                    HeightPer = (float)ThumbnailSizeHeight / origImage.Height;
                    NewWidth = Convert.ToInt32(origImage.Width * HeightPer);
                }

                Bitmap origThumbnail = new Bitmap(NewWidth, NewHeight, origImage.PixelFormat);
                Graphics oGraphic = Graphics.FromImage(origThumbnail);
                oGraphic.CompositingQuality = CompositingQuality.HighQuality;
                oGraphic.SmoothingMode = SmoothingMode.HighQuality;
                oGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Rectangle oRectangle = new Rectangle(0, 0, NewWidth, NewHeight);
                oGraphic.DrawImage(origImage, oRectangle);


                origThumbnail.Save(sideThumbnailPath, ImageFormat.Png);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Load designer in case of print product 
        /// </summary>
        /// <param name="ItemId"></param>
        /// <param name="ModeOfStore"></param>
        /// <param name="OrderIdFromCookie"></param>
        /// <param name="ContactIdFromClaim"></param>
        /// <param name="CompanyIdFromClaim"></param>
        /// <param name="TemporaryRetailCompanyIdFromCookie"></param>
        /// <param name="OrganisationId"></param>
        /// <returns></returns>
        public ItemCloneResult CloneItemAndLoadDesigner(long ItemId, StoreMode ModeOfStore, long OrderIdFromCookie, long ContactIdFromClaim, long CompanyIdFromClaim, long TemporaryRetailCompanyIdFromCookie, long OrganisationId)
        {

            ItemCloneResult itemCloneObj = new ItemCloneResult();
            Item item = null;

            long ItemID = 0;
            long TemplateID = 0;
            bool isCorp = true;
            if (ModeOfStore == StoreMode.Corp)
                isCorp = true;
            else
                isCorp = false;
            int TempDesignerID = 0;
            string ProductName = string.Empty;

            long ContactID = ContactIdFromClaim;
            long CompanyID = CompanyIdFromClaim;
            itemCloneObj.OrderId = OrderIdFromCookie;
            itemCloneObj.TemporaryCustomerId = TemporaryRetailCompanyIdFromCookie;
            if (OrderIdFromCookie == 0)
            {
                long OrderID = 0;
                long TemporaryRetailCompanyId = 0;
                if (ModeOfStore == StoreMode.Retail)
                {
                    TemporaryRetailCompanyId = TemporaryRetailCompanyIdFromCookie;
                    OrderID = _orderService.ProcessPublicUserOrder(string.Empty, OrganisationId, ModeOfStore, CompanyID, ContactID, ref TemporaryRetailCompanyId);
                    if (OrderID > 0)
                    {
                        itemCloneObj.OrderId = OrderID;
                    }
                    if (TemporaryRetailCompanyId != 0)
                    {
                        itemCloneObj.TemporaryCustomerId = TemporaryRetailCompanyId;
                        ContactID = _myCompanyService.GetContactIdByCompanyId(TemporaryRetailCompanyId);
                    }
                    CompanyID = TemporaryRetailCompanyId;

                }
                else
                {
                    OrderID = _orderService.ProcessPublicUserOrder(string.Empty, OrganisationId, ModeOfStore, CompanyID, ContactID, ref TemporaryRetailCompanyId);
                    if (OrderID > 0)
                    {
                        itemCloneObj.OrderId = OrderID;
                    }
                }

                // create new order


                item = CloneItem(ItemId, 0, OrderID, CompanyID, 0, 0, null, false, false, ContactID, OrganisationId);

                if (item != null)
                {
                    ItemID = item.ItemId;
                    TemplateID = item.TemplateId ?? 0;
                    TempDesignerID = item.DesignerCategoryId ?? 0;
                    ProductName = item.ProductName;
                }

            }
            else
            {
                if (TemporaryRetailCompanyIdFromCookie == 0 && ModeOfStore == StoreMode.Retail && ContactID == 0)
                {
                    long TemporaryRetailCompanyId = TemporaryRetailCompanyIdFromCookie;

                    // create new order

                    long OrderID = _orderService.ProcessPublicUserOrder(string.Empty, OrganisationId, ModeOfStore, CompanyID, ContactID, ref TemporaryRetailCompanyId);
                    if (OrderID > 0)
                    {
                        itemCloneObj.OrderId = OrderID;
                    }
                    if (TemporaryRetailCompanyId != 0)
                    {
                        itemCloneObj.TemporaryCustomerId = TemporaryRetailCompanyId;
                        ContactID = _myCompanyService.GetContactIdByCompanyId(TemporaryRetailCompanyId);
                    }
                    CompanyID = TemporaryRetailCompanyId;
                }
                else if (TemporaryRetailCompanyIdFromCookie > 0 && ModeOfStore == StoreMode.Retail)
                {
                    CompanyID = TemporaryRetailCompanyIdFromCookie;
                    ContactID = _myCompanyService.GetContactIdByCompanyId(CompanyID);
                }
                item = CloneItem(ItemId, 0, OrderIdFromCookie, CompanyID, 0, 0, null, false, false, ContactID, OrganisationId);

                if (item != null)
                {
                    ItemID = item.ItemId;
                    TemplateID = item.TemplateId ?? 0;
                    TempDesignerID = item.DesignerCategoryId ?? 0;
                    ProductName = specialCharactersEncoder(item.ProductName);
                }
            }

            int isCalledFrom = 0;
            if (ModeOfStore == StoreMode.Corp)
                isCalledFrom = 4;
            else
                isCalledFrom = 3;

            bool isEmbedded;
            bool printWaterMark = true;
            if (ModeOfStore == StoreMode.Corp || ModeOfStore == StoreMode.Retail)
            {
                isEmbedded = true;
            }
            else
            {
                printWaterMark = false;
                isEmbedded = false;
            }

            ProductName = specialCharactersEncoder(ProductName);

            bool printCropMarks = true;

            if (item != null && item.TemplateType == 3)
            {
                itemCloneObj.RedirectUrl = "/Designer/" + ProductName + "/" + TempDesignerID + "/" + TemplateID + "/" + ItemID + "/" + CompanyID + "/" + ContactID + "/" + isCalledFrom + "/" + OrganisationId + "/" + printCropMarks + "/" + printWaterMark + "/" + isEmbedded;
            }
            else
            {
                itemCloneObj.RedirectUrl = "/Designer/" + ProductName + "/0/" + TemplateID + "/" + ItemID + "/" + CompanyID + "/" + ContactID + "/" + isCalledFrom + "/" + OrganisationId + "/" + printCropMarks + "/" + printWaterMark + "/" + isEmbedded;
            }


            return itemCloneObj;
        }
        /// <summary>
        /// delete single attachment record
        /// </summary>
        /// <param name="ItemAttachmentId"></param>
        public void DeleteItemAttachment(long ItemAttachmentId)
        {
            try
            {
                ItemAttachment oDbAttachmentRecord = _itemAtachement.GetArtworkAttachment(ItemAttachmentId);
                if (oDbAttachmentRecord != null)
                {
                    // delete attachment record
                    _itemAtachement.DeleteArtworkAttachment(oDbAttachmentRecord);
                    // delete physical file
                    string completePhysicalPathOfAttachmentThumbnail = HttpContext.Current.Server.MapPath("~/" + oDbAttachmentRecord.FolderPath + "/" + oDbAttachmentRecord.FileName + "Thumb.png");
                    string completePhysicalPathOfAttachmentFile = HttpContext.Current.Server.MapPath("~/" + oDbAttachmentRecord.FolderPath + "/" + oDbAttachmentRecord.FileName + oDbAttachmentRecord.FileType);
                    if (System.IO.File.Exists(completePhysicalPathOfAttachmentFile))
                    {
                        System.IO.File.Delete(completePhysicalPathOfAttachmentFile);
                    }
                    if (System.IO.File.Exists(completePhysicalPathOfAttachmentThumbnail))
                    {
                        System.IO.File.Delete(completePhysicalPathOfAttachmentThumbnail);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #region PrivateFunctions
        public T Clone<T>(T source)
        {
            try
            {
                //db.Configuration.LazyLoadingEnabled = false;
                object item = Activator.CreateInstance(typeof(T));
                List<PropertyInfo> itemPropertyInfoCollection = source.GetType().GetProperties().ToList<PropertyInfo>();
                foreach (PropertyInfo propInfo in itemPropertyInfoCollection)
                {
                    if (propInfo.CanRead &&
                        (propInfo.PropertyType.IsValueType || propInfo.PropertyType.FullName == "System.String"))
                    {
                        PropertyInfo newProp = item.GetType().GetProperty(propInfo.Name);
                        if (newProp != null && newProp.CanWrite)
                        {
                            object va = propInfo.GetValue(source, null);
                            newProp.SetValue(item, va, null);
                        }
                    }
                }

                return (T)item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int CopyTemplatePaths(Template clonedTemplate, long OrganisationID)
        {
            int result = 0;

            try
            {
                result = (int)clonedTemplate.ProductId;

                //  string BasePath = System.Web.HttpContext.Current.Server.MapPath("~/DesignEngine/Designer/Products/");
                string drURL =
                    System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" +
                                                                  OrganisationID.ToString() + "/Templates/");
                //result = dbContext.sp_cloneTemplate(ProductID, SubmittedBy, SubmittedByName).First().Value;

                string targetFolder = drURL + result.ToString();
                if (!System.IO.Directory.Exists(targetFolder))
                {
                    System.IO.Directory.CreateDirectory(targetFolder);
                }


                //copy the background PDF Templates
                //Templates oTemplate = db.Templates.Where(g => g.ProductID == result).Single();
                Template oTemplate = clonedTemplate;

                //copy the background of pages
                List<TemplatePage> TemplatePages = _TemplatePageRepository.GetTemplatePages(result);
                foreach (TemplatePage oTemplatePage in TemplatePages)
                {

                    if (oTemplatePage.BackGroundType == 1 || oTemplatePage.BackGroundType == 3)
                    {
                        //copy side 1
                        if (oTemplatePage.BackGroundType == 1) //additional background copy function
                        {
                            string oldproductid = oTemplatePage.BackgroundFileName.Substring(0,
                                oTemplatePage.BackgroundFileName.IndexOf("/"));
                            if (File.Exists(drURL + oldproductid + "/" + "templatImgBk" + oTemplatePage.PageNo + ".jpg"))
                            {


                                File.Copy(
                                    Path.Combine(drURL,
                                        oldproductid + "/" + "templatImgBk" + oTemplatePage.PageNo + ".jpg"),
                                    drURL + result.ToString() + "/" + "templatImgBk" + oTemplatePage.PageNo + ".jpg");

                            }
                        }
                        if (File.Exists(drURL + oTemplatePage.BackgroundFileName))
                        {
                            File.Copy(drURL + oTemplatePage.BackgroundFileName,
                                drURL + result.ToString() + "/" +
                                oTemplatePage.BackgroundFileName.Substring(oTemplatePage.BackgroundFileName.IndexOf("/"),
                                    oTemplatePage.BackgroundFileName.Length - oTemplatePage.BackgroundFileName.IndexOf("/")), true);
                            oTemplatePage.BackgroundFileName = result.ToString() + "/" +
                                                               oTemplatePage.BackgroundFileName.Substring(
                                                                   oTemplatePage.BackgroundFileName.IndexOf("/"),
                                                                   oTemplatePage.BackgroundFileName.Length -
                                                                   oTemplatePage.BackgroundFileName.IndexOf("/"));
                        }


                    }
                }


                //skip concatinating the path if its a placeholder, cuz place holder is kept in a different path and doesnt need to be copied.

                if (oTemplate.TemplateObjects != null)
                {
                    oTemplate.TemplateObjects.Where(
                        tempObject => tempObject.ObjectType == 3 && tempObject.IsQuickText != true)
                        .ToList()
                        .ForEach(item =>
                        {

                            string filepath =
                                item.ContentString.Substring(
                                    item.ContentString.IndexOf("/Designer/Organisation" + OrganisationID.ToString() +
                                                               "/Templates/") +
                                    ("/Designer/Organisation" + OrganisationID.ToString() + "/Templates/").Length,
                                    item.ContentString.Length -
                                    ((item.ContentString.IndexOf("/Designer/Organisation" + OrganisationID.ToString() +
                                                                 "/Templates/") + "/Designer/Organisation" +
                                      OrganisationID.ToString() + "/Templates/").Length));
                            item.ContentString = "Designer/Organisation" + OrganisationID.ToString() + "/Templates/" +
                                                 result.ToString() +
                                                 filepath.Substring(filepath.IndexOf("/"),
                                                     filepath.Length - filepath.IndexOf("/"));

                        });
                }

                //foreach (var item in dbContext.TemplateObjects.Where(g => g.ProductID == result && g.ObjectType == 3))
                //{
                //    string filepath = item.ContentString.Substring(item.ContentString.IndexOf("DesignEngine/Designer/Products/") + "DesignEngine/Designer/Products/".Length, item.ContentString.Length - (item.ContentString.IndexOf("DesignEngine/Designer/Products/") + "DesignEngine/Designer/Products/".Length));
                //    item.ContentString = "DesignEngine/Designer/Products/" + result.ToString() + filepath.Substring(filepath.IndexOf("/"), filepath.Length - filepath.IndexOf("/"));
                //}

                //

                //copy the background images



                //var backimgs = dbContext.TemplateBackgroundImages.Where(g => g.ProductID == result);
                if (oTemplate.TemplateBackgroundImages != null)
                {
                    oTemplate.TemplateBackgroundImages.ToList().ForEach(item =>
                    {

                        string filePath = drURL + item.ImageName;
                        string filename;

                        string ext = Path.GetExtension(item.ImageName);

                        // generate thumbnail 
                        if (!ext.Contains("svg"))
                        {
                            string[] results = item.ImageName.Split(new string[] { ext }, StringSplitOptions.None);
                            string destPath = results[0] + "_thumb" + ext;
                            string ThumbPath = drURL + destPath;
                            FileInfo oFileThumb = new FileInfo(ThumbPath);
                            if (oFileThumb.Exists)
                            {
                                string oThumbName = oFileThumb.Name;
                                oFileThumb.CopyTo(drURL + result.ToString() + "/" + oThumbName, true);
                            }
                            //  objSvc.GenerateThumbNail(sourcePath, destPath, 98);
                        }


                        FileInfo oFile = new FileInfo(filePath);

                        if (oFile.Exists)
                        {
                            filename = oFile.Name;
                            item.ImageName = result.ToString() + "/" +
                                             oFile.CopyTo(drURL + result.ToString() + "/" + filename, true).Name;
                        }


                    });
                }

                _TemplatePageRepository.SaveChanges();
                _TemplateObjectRepository.SaveChanges();
                _TemplateBackgroundImagesRepository.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception("Copy Template Paths", ex);
                //AppCommon.LogException(ex);
                //throw ex;
            }

            return result;
        }

        public long ReOrder(long ExistingOrderId, long loggedInContactID, double StatTaxVal, StoreMode mode, bool isIncludeTax, int TaxID, long OrganisationId, long StoreId)
        {
            //  return _OrderRepository.ReOrder(ExistingOrderId, loggedInContactID, StatTaxVal, mode, isIncludeTax, TaxID, OrganisationId);

            Estimate ExistingOrder = null;
            Estimate shopCartOrder = null;
            bool result = false;

            List<Item> ClonedItems = new List<Item>();
            long OrderIdOfReorderItems = 0;

            try
            {
                ExistingOrder = _OrderRepository.GetOrderByID(ExistingOrderId);

                if (ExistingOrder != null)
                {

                    shopCartOrder = _OrderRepository.GetShoppingCartOrderByContactID(loggedInContactID, OrderStatus.ShoppingCart);
                    //create a new cart
                    if (shopCartOrder == null)
                    {
                        shopCartOrder = ExistingOrder;
                        // Order status will be shopping cart
                        shopCartOrder.StatusId = (int)OrderStatus.ShoppingCart;
                        shopCartOrder.DeliveryCompletionTime = 0;
                        shopCartOrder.DeliveryCost = 0;
                        shopCartOrder.DeliveryCostCenterId = 0;
                        shopCartOrder.StartDeliveryDate = null;
                        Prefix prefix = _prefixRepository.GetDefaultPrefix();
                        if (prefix != null)
                        {
                            shopCartOrder.Order_Code = prefix.OrderPrefix + "-001-" + prefix.OrderNext.ToString();
                            prefix.OrderNext = prefix.OrderNext + 1;
                        }
                        shopCartOrder.Order_CompletionDate = null;
                        shopCartOrder.Order_ConfirmationDate = null;
                        shopCartOrder.Order_CreationDateTime = DateTime.Now;
                        shopCartOrder.CustomerPO = null;

                        _OrderRepository.Add(shopCartOrder);

                        OrderIdOfReorderItems = shopCartOrder.EstimateId;
                    }
                    else
                    {
                        OrderIdOfReorderItems = shopCartOrder.EstimateId;
                    }
                    List<Item> esxistingOrderItems = _OrderRepository.GetAllOrderItems(ExistingOrderId);
                    //Clone items related to this order

                    esxistingOrderItems = esxistingOrderItems.Where(i => i.StatusId != (int)OrderStatus.ShoppingCart && i.ItemType != Convert.ToInt32(ItemTypes.Delivery)).ToList();

                    esxistingOrderItems.ForEach(orderITem =>
                    {
                        Item item = _ItemRepository.CloneReOrderItem(OrderIdOfReorderItems, orderITem.ItemId, loggedInContactID, shopCartOrder.Order_Code, OrganisationId);
                        ClonedItems.Add(item);
                        CopyAttachments(orderITem.ItemId, item, shopCartOrder.Order_Code, false, shopCartOrder.CreationDate ?? DateTime.Now, OrganisationId, StoreId);

                    });

                    if (ExistingOrder.DiscountVoucherID.HasValue && ExistingOrder.VoucherDiscountRate > 0)
                    {
                        if (_OrderRepository.RollBackDiscountedItemsWithdbContext(ClonedItems, StatTaxVal))
                        {
                            ExistingOrder.VoucherDiscountRate = null;
                            ExistingOrder.DiscountVoucherID = null;
                            shopCartOrder.VoucherDiscountRate = null;
                            shopCartOrder.DiscountVoucherID = null;
                        }
                    }
                    else if (isIncludeTax)// apply the new state Tax Value to the cloned item 
                    {
                        _OrderRepository.ApplyCurrentTax(ClonedItems, StatTaxVal, TaxID);
                    }
                    result = true;
                    _OrderRepository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return shopCartOrder.EstimateId;

        }
        public List<ItemVideo> GetProductVideos(long ItemID)
        {
            return _videoRepository.GetProductVideos(ItemID);
        }
        public List<ItemAttachment> GetItemAttactchments(long itemID)
        {
            return _itemAtachement.GetItemAttactchments(itemID);
        }

        private bool SaveAdditionalAddonsOrUpdateStockItemType(List<AddOnCostsCenter> selectedAddonsList, long newItemID,
              long stockID, bool isCopyProduct, string updateMode, long ItemStockOptionId)
        {
            try
            {
                bool result = false;
                ItemSection SelectedtblItemSectionOne = null;

                //Create A new Item Section #1 to pass to the cost center
                SelectedtblItemSectionOne = _ItemSectionRepository.GetFirstSectionOfItem(newItemID);
                //SelectedtblItemSectionOne =
                //    db.ItemSections.Where(itemSect => itemSect.SectionNo == 1 && itemSect.ItemId == newItemID)
                //        .FirstOrDefault();

                if (isCopyProduct == true)
                {
                    result = this.SaveAdditionalAddonsOrUpdateStockItemType(selectedAddonsList,
                        Convert.ToInt64(SelectedtblItemSectionOne.StockItemID1), SelectedtblItemSectionOne, updateMode, ItemStockOptionId);
                }
                else
                {
                    result = this.SaveAdditionalAddonsOrUpdateStockItemType(selectedAddonsList, stockID,
                        SelectedtblItemSectionOne, updateMode, ItemStockOptionId);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        private bool SaveAdditionalAddonsOrUpdateStockItemType(List<AddOnCostsCenter> selectedAddonsList, long stockID,
            ItemSection SelectedtblItemSectionOne, string updateMode, long ItemstockOptionID)
        {
            try
            {
                SectionCostcentre SelectedtblISectionCostCenteres = null;

                if (SelectedtblItemSectionOne != null)
                {
                    //Set or Update the paper Type stockid in the section #1
                    if (stockID > 0)
                        this.UpdateStockItemType(SelectedtblItemSectionOne, stockID, ItemstockOptionID);

                    if (selectedAddonsList != null)
                    {
                        // Remove previous Addons
                        _ItemSectionCostCentreRepository.RemoveCostCentreOfFirstSection(SelectedtblItemSectionOne.ItemSectionId);
                        //db.SectionCostcentres.Where(
                        //    c => c.ItemSectionId == SelectedtblItemSectionOne.ItemSectionId && c.IsOptionalExtra == 1)
                        //    .ToList()
                        //    .ForEach(sc =>
                        //    {
                        //        db.SectionCostcentres.Remove(sc);

                        //    });
                        //Create Additional Addons Data
                        //Create Additional Addons Data
                        for (int i = 0; i < selectedAddonsList.Count; i++)
                        {
                            AddOnCostsCenter addonCostCenter = selectedAddonsList[i];

                            SelectedtblISectionCostCenteres = this.PopulateTblSectionCostCenteres(addonCostCenter);
                            SelectedtblISectionCostCenteres.IsOptionalExtra = 1; //1 tells that it is the Additional AddOn 

                            SelectedtblItemSectionOne.SectionCostcentres.Add(SelectedtblISectionCostCenteres);

                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void UpdateStockItemType(ItemSection itemSection, long stockID, long ItemstockOptionID)
        {
            try
            {
                itemSection.StockItemID1 = (int)stockID; //always set into the first column
                itemSection.StockItemID2 = (int)ItemstockOptionID;
                itemSection.StockItemID3 = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        private SectionCostcentre PopulateTblSectionCostCenteres(AddOnCostsCenter addOn)
        {
            try
            {
                SectionCostcentre tblISectionCostCenteres = new SectionCostcentre
                {
                    CostCentreId = addOn.CostCenterID,
                    IsOptionalExtra = 1,
                    Qty1Charge = addOn.Qty1NetTotal,
                    Qty1NetTotal = addOn.Qty1NetTotal,
                    Qty1WorkInstructions = addOn.CostCentreDescription,
                    Qty2WorkInstructions = addOn.CostCentreJsonData,
                    Name = addOn.AddOnName
                };

                return tblISectionCostCenteres;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public double GetDiscountAmountByVoucher(DiscountVoucher storeDiscountVoucher, double itemTotal, long ItemId, double OrderedQty, long? DiscountIdAlreadyApplied) 
        {
            bool isApplyDiscount = true;
            double DiscountAmountToApply = 0;
            double DiscountAmountAlreadyApplied = 0;
            if (storeDiscountVoucher != null)
            {
                if (DiscountIdAlreadyApplied != null)
                {
                    DiscountVoucher dvAlreadyApplied = _DVRepository.GetDiscountVoucherById(Convert.ToInt64(DiscountIdAlreadyApplied));
                    if (dvAlreadyApplied != null)
                    {
                        DiscountAmountAlreadyApplied = dvAlreadyApplied.DiscountRate;
                    }
                }

                if (storeDiscountVoucher.IsTimeLimit == true)
                {
                    DateTime? ValidFromDate = storeDiscountVoucher.ValidFromDate;
                    DateTime? ValidUptoDate = storeDiscountVoucher.ValidUptoDate;
                    DateTime TodayDate = DateTime.Now;
                    if (ValidFromDate != null && ValidUptoDate != null)
                    {
                        if (TodayDate >= ValidFromDate && TodayDate <= ValidUptoDate)
                        {
                            isApplyDiscount = true;
                        }
                        else
                        {
                            isApplyDiscount = false;
                        }
                    }
                    else
                    {
                        isApplyDiscount = false;
                    }
                }

                if (storeDiscountVoucher.IsQtyRequirement == true)
                {
                    if (OrderedQty >= storeDiscountVoucher.MinRequiredQty && OrderedQty <= storeDiscountVoucher.MaxRequiredQty)
                    {
                        isApplyDiscount = true;
                    }
                    else
                    {
                        isApplyDiscount = false;
                    }
                }

                if (storeDiscountVoucher.DiscountType == (int)DiscountTypes.DollaramountoffEntireorder || storeDiscountVoucher.DiscountType == (int)DiscountTypes.PercentoffEntirorder)
                {
                    if (storeDiscountVoucher.IsOrderPriceRequirement == true)
                    {

                    }
                }

            }

            if (isApplyDiscount && storeDiscountVoucher.DiscountRate > DiscountAmountAlreadyApplied)
            {
                if (storeDiscountVoucher.DiscountType == (int)DiscountTypes.DollaramountoffEntireorder)
                {
                    DiscountAmountToApply = storeDiscountVoucher.DiscountRate;
                }
                else if (storeDiscountVoucher.DiscountType == (int)DiscountTypes.PercentoffEntirorder)
                {
                    DiscountAmountToApply = _ItemRepository.CalculatePercentage(itemTotal, storeDiscountVoucher.DiscountRate);
                }
                else if (storeDiscountVoucher.DiscountType == (int)DiscountTypes.PercentoffEntirorder)
                {
                    DiscountAmountToApply = _ItemRepository.CalculatePercentage(itemTotal, storeDiscountVoucher.DiscountRate);
                }
                else
                {
                    if (_ItemVRepository.isVoucherAppliedOnThisProduct(storeDiscountVoucher.DiscountVoucherId, ItemId))
                    {
                        if (storeDiscountVoucher.DiscountType == (int)DiscountTypes.PercentoffaProduct)
                        {
                            DiscountAmountToApply = _ItemRepository.CalculatePercentage(itemTotal, storeDiscountVoucher.DiscountRate);
                        }
                        else if (storeDiscountVoucher.DiscountType == (int)DiscountTypes.DollarAmountOffProduct)
                        {
                            DiscountAmountToApply = storeDiscountVoucher.DiscountRate;
                        }
                    }
                }
            }

            return DiscountAmountToApply;
        }

        #endregion
    }
}

