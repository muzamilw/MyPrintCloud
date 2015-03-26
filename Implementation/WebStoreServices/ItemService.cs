﻿using MPC.Interfaces.Repository;
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
        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public ItemService(IItemRepository ItemRepository, IItemStockOptionRepository StockOptions, ISectionFlagRepository SectionFlagRepository, ICompanyRepository CompanyRepository
            , IItemStockControlRepository StockRepository, IItemAddOnCostCentreRepository AddOnRepository, IProductCategoryRepository ProductCategoryRepository
            , IItemAttachmentRepository itemAtachement, IFavoriteDesignRepository FavoriteDesign, ITemplateService templateService
            , IPaymentGatewayRepository paymentRepository, IInquiryRepository inquiryRepository, IInquiryAttachmentRepository inquiryAttachmentRepository)
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
                    // pass 0  to get the default flag id for price matrix
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
        public bool UpdateCloneItemService(long clonedItemID, double orderedQuantity, double itemPrice, double addonsPrice, long stockItemID, List<AddOnCostsCenter> newlyAddedCostCenters, int Mode, long OrganisationId, double TaxRate, string ItemMode, bool isInculdeTax, int CountOfUploads = 0, string QuestionQueue = "") 
        {
            return _ItemRepository.UpdateCloneItem(clonedItemID, orderedQuantity, itemPrice, addonsPrice, stockItemID, newlyAddedCostCenters, Mode, OrganisationId, TaxRate, ItemMode, isInculdeTax, CountOfUploads, QuestionQueue);
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
            try{
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
            }catch(Exception ex)
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
       public List<SectionCostcentre> GetClonedItemAddOnCostCentres(long ItemId)
       {
           try
           {
               return _AddOnRepository.GetClonedItemAddOnCostCentres(ItemId);
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
        public long GetCartItemsCount(long ContactId, long TemporaryCustomerId)
        {
            try
            {
                return _ItemRepository.GetCartItemsCount(ContactId, TemporaryCustomerId);
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

    }
}
