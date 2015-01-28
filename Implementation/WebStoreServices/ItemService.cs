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
        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public ItemService(IItemRepository ItemRepository, IItemStockOptionRepository StockOptions, ISectionFlagRepository SectionFlagRepository, ICompanyRepository CompanyRepository
            , IItemStockControlRepository StockRepository, IItemAddOnCostCentreRepository AddOnRepository, IProductCategoryRepository ProductCategoryRepository
            , IItemAttachmentRepository itemAtachement, IFavoriteDesignRepository FavoriteDesign, ITemplateService templateService
            ,IPaymentGatewayRepository paymentRepository)
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
        }

        public List<ItemStockOption> GetStockList(long ItemId, long CompanyId)
        {
            return _StockOptions.GetStockList(ItemId, CompanyId);
        }

        public Item GetItemById(long ItemId)
        {
            return _ItemRepository.GetItemById(ItemId);
        }
        public Item CloneItem(long itemID, long RefItemID, long OrderID, long CustomerID, long TemplateID, long StockID, List<AddOnCostsCenter> SelectedAddOnsList, bool isSavedDesign, bool isCopyProduct, long objContactID, long OrganisationID)
        {
            return _ItemRepository.CloneItem(itemID, RefItemID, OrderID, CustomerID, TemplateID, StockID, SelectedAddOnsList, isSavedDesign, isCopyProduct, objContactID,OrganisationID);
        }

        public List<ItemPriceMatrix> GetPriceMatrix(List<ItemPriceMatrix> tblRefItemsPriceMatrix, bool IsRanged, bool IsUserLoggedIn, long CompanyId)
        {
            int flagId = 0;
            if (IsUserLoggedIn)
            {
                flagId = GetFlagId(CompanyId);
                if (flagId == 0)
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
                flagId = GetFlagId(0);
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

        private int GetFlagId(long companyId)
        {
            if (companyId > 0)
            {
                return _CompanyRepository.GetPriceFlagIdByCompany(companyId) == null ? 0 : Convert.ToInt32(_CompanyRepository.GetPriceFlagIdByCompany(companyId));
            }
            else
            {
                return _SectionFlagRepository.GetDefaultSectionFlagId();
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
        public void CopyAttachments(int itemID, Item NewItem, string OrderCode, bool CopyTemplate, DateTime OrderCreationDate)
        {
            try
            {
                _ItemRepository.CopyAttachments(itemID, NewItem, OrderCode, CopyTemplate, OrderCreationDate);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public bool RemoveCloneItem(long itemID, out List<ArtWorkAttatchment> itemAttatchmetList, out Template clonedTemplateToRemove)
        {
            try
            {

                return _ItemRepository.RemoveCloneItem(itemID,out itemAttatchmetList, out clonedTemplateToRemove);
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
            catch(Exception ex)
            {
                throw ex;
            }
         
        }
        public bool UpdateCloneItemService(long clonedItemID, double orderedQuantity, double itemPrice, double addonsPrice, long stockItemID, List<AddOnCostsCenter> newlyAddedCostCenters, int Mode, long OrganisationId, double TaxRate, int CountOfUploads = 0) 
        {
            return _ItemRepository.UpdateCloneItem(clonedItemID, orderedQuantity, itemPrice, addonsPrice, stockItemID, newlyAddedCostCenters, Mode, OrganisationId, TaxRate, CountOfUploads);
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
        public string GetImmidiateParentCategory(long categoryID,out string CategoryName)
        {
            try
            {
                return _ProductCategoryRepository.GetImmidiateParentCategory(categoryID, out CategoryName);
            }
            catch(Exception ex)
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
            catch(Exception ex)
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
            catch(Exception ex)
            {
                throw ex;
            }
        }



        public long PostLoginCustomerAndCardChanges(long OrderId, long CompanyId, long ContactId, long TemporaryCompanyId, long OrganisationId)
        {
            List<ArtWorkAttatchment> orderAllItemsAttatchmentsListToBeRemoved = null;
            List<Template> clonedTempldateFilesList = null;

            if (TemporaryCompanyId > 0 && TemporaryCompanyId != CompanyId)
            {
                long orderId = _ItemRepository.UpdateTemporaryCustomerOrderWithRealCustomer(TemporaryCompanyId, CompanyId, ContactId, OrderId, out orderAllItemsAttatchmentsListToBeRemoved, out clonedTempldateFilesList);
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

        /// <summary>
        /// Remove Item Attachments
        /// </summary>
        /// <param name="attatchmentList"></param>
        private void RemoveItemAttacmentPhysically(List<ArtWorkAttatchment> attatchmentList)
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

        public  void GenerateThumbnailForPdf( string url, bool insertCuttingMargin)
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

                CreatAndSaveThumnail(oImgstream, url);

            }
        }
        public string SaveDesignAttachments(long templateID, long itemID, long customerID, string DesignName, string caller, long organisationId)
        {
            return _ItemRepository.SaveDesignAttachments(templateID, itemID, customerID, DesignName, caller, organisationId);
        }
        public bool CreatAndSaveThumnail(Stream oImgstream,string sideThumbnailPath)
        {
            try
            {
                string orgPath = sideThumbnailPath;
                string baseAddress = sideThumbnailPath.Substring(0, sideThumbnailPath.LastIndexOf('\\'));
                sideThumbnailPath = Path.GetFileNameWithoutExtension(sideThumbnailPath) + "Thumb.png";

                sideThumbnailPath = baseAddress + "\\" + sideThumbnailPath;

                Image origImage = null;
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
                return true;
            }
            catch (Exception e)
            {
                return false;
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
                return _ItemRepository.AddUpdateItemFordeliveryCostCenter(orderId,DeliveryCostCenterId,DeliveryCost,customerID,DeliveryName,Mode,isDeliveryTaxable,IstaxONService,GetServiceTAX,TaxRate);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Item GetItemByOrderItemID(long ItemID,long OrderID)
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
  

    }
}
