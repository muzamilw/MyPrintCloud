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

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public ItemService(IItemRepository ItemRepository, IItemStockOptionRepository StockOptions, ISectionFlagRepository SectionFlagRepository, ICompanyRepository CompanyRepository
            , IItemStockControlRepository StockRepository, IItemAddOnCostCentreRepository AddOnRepository)
        {
            this._ItemRepository = ItemRepository;
            this._StockOptions = StockOptions;
            this._SectionFlagRepository = SectionFlagRepository;
            this._CompanyRepository = CompanyRepository;
            this._StockRepository = StockRepository;
            this._AddOnRepository = AddOnRepository;
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

        public ProductItem GetItemAndDetailsByItemID(int itemId)
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
            return _StockRepository.GetStockOfItemById(itemId);
        }
        public bool UpdateCloneItemService(long clonedItemID, double orderedQuantity, double itemPrice, double addonsPrice, long stockItemID, List<AddOnCostsCenter> newlyAddedCostCenters, int Mode, long OrganisationId, double TaxRate, int CountOfUploads = 0) 
        {
            return _ItemRepository.UpdateCloneItem(clonedItemID, orderedQuantity, itemPrice, addonsPrice, stockItemID, newlyAddedCostCenters, Mode, OrganisationId, TaxRate, CountOfUploads);
        }

        public Item GetExisitingClonedItemInOrder(long OrderId, long ReferenceItemId)
        {
            return _ItemRepository.GetClonedItemByOrderId(OrderId, ReferenceItemId);
        }
    }
}
