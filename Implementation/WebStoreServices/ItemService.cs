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

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public ItemService(IItemRepository ItemRepository, IItemStockOptionRepository StockOptions, ISectionFlagRepository SectionFlagRepository, ICompanyRepository CompanyRepository
            , IItemStockControlRepository StockRepository, IItemAddOnCostCentreRepository AddOnRepository, IProductCategoryRepository ProductCategoryRepository
            , IItemAttachmentRepository itemAtachement)
        {
            this._ItemRepository = ItemRepository;
            this._StockOptions = StockOptions;
            this._SectionFlagRepository = SectionFlagRepository;
            this._CompanyRepository = CompanyRepository;
            this._StockRepository = StockRepository;
            this._AddOnRepository = AddOnRepository;
            this._ProductCategoryRepository = ProductCategoryRepository;
            this._itemAtachement = itemAtachement;
        }

        public List<ItemStockOption> GetStockList(long ItemId, long CompanyId)
        {
            return _StockOptions.GetStockList(ItemId, CompanyId);
        }

        public Item GetItemById(long ItemId)
        {
            return _ItemRepository.GetItemById(ItemId);
        }
        public Item CloneItem(long itemID, long RefItemID, long OrderID, long CustomerID, int TemplateID, long StockID, List<AddOnCostsCenter> SelectedAddOnsList, bool isSavedDesign, bool isCopyProduct, long objContactID)
        {
            return _ItemRepository.CloneItem(itemID, RefItemID, OrderID, CustomerID, TemplateID, StockID, SelectedAddOnsList, isSavedDesign, isCopyProduct, objContactID);
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
        
    }
}
