using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Models.Common;


namespace MPC.Implementation.MISServices
{
    /// <summary>
    /// Inventory Service
    /// </summary>
    public class InventoryService : IInventoryService
    {
        #region Private

        /// <summary>
        /// Private members
        /// </summary>
        private readonly IStockCategoryRepository stockCategoryRepository;
        private readonly IStockSubCategoryRepository stockSubCategoryRepository;
        private readonly IStockItemRepository stockItemRepository;
        private readonly ISectionFlagRepository sectionFlagRepository;
        private readonly IWeightUnitRepository weightUnitRepository;
        private readonly ICompanyRepository companyRepository;
        private readonly IPaperSizeRepository paperSizeRepository;
        private readonly IStockCostAndPriceRepository stockCostAndPriceRepository;
        private readonly IPaperBasisAreaRepository paperBasisAreaRepository;
        private readonly ILengthUnitRepository lengthUnitRepository;
        private readonly IRegistrationQuestionRepository registrationQuestionRepository;
        private readonly IPrefixRepository prefixRepository;
        private readonly ICompanyTypeRepository companyTypeRepository;
        private readonly IMarkupRepository markupRepository;
        private readonly IChartOfAccountRepository chartOfAccountRepository;
        private readonly ISystemUserRepository systemUserRepository;
        private readonly IOrganisationRepository organisationRepository;
        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public InventoryService(IStockCategoryRepository stockCategoryRepository, IStockSubCategoryRepository stockSubCategoryRepository,
            IStockItemRepository stockItemRepository, ISectionFlagRepository sectionFlagRepository, IWeightUnitRepository weightUnitRepository,
            ICompanyRepository companyRepository, IPaperSizeRepository paperSizeRepository, IStockCostAndPriceRepository stockCostAndPriceRepository,
            IPaperBasisAreaRepository paperBasisAreaRepository, ILengthUnitRepository lengthUnitRepository, IRegistrationQuestionRepository registrationQuestionRepository,
            IPrefixRepository prefixRepository, ICompanyTypeRepository companyTypeRepository, IMarkupRepository markupRepository, IChartOfAccountRepository chartOfAccountRepository,
            ISystemUserRepository systemUserRepository, IOrganisationRepository organisationRepository
            )
        {
            this.stockCategoryRepository = stockCategoryRepository;
            this.stockSubCategoryRepository = stockSubCategoryRepository;
            this.stockItemRepository = stockItemRepository;
            this.sectionFlagRepository = sectionFlagRepository;
            this.weightUnitRepository = weightUnitRepository;
            this.companyRepository = companyRepository;
            this.paperSizeRepository = paperSizeRepository;
            this.stockCostAndPriceRepository = stockCostAndPriceRepository;
            this.paperBasisAreaRepository = paperBasisAreaRepository;
            this.lengthUnitRepository = lengthUnitRepository;
            this.registrationQuestionRepository = registrationQuestionRepository;
            this.prefixRepository = prefixRepository;
            this.markupRepository = markupRepository;
            this.chartOfAccountRepository = chartOfAccountRepository;
            this.systemUserRepository = systemUserRepository;
            this.companyTypeRepository = companyTypeRepository;
            this.organisationRepository = organisationRepository;
        }

        #endregion

        #region Public
        /// <summary>
        /// Load Inventory Base data
        /// </summary>
        public InventoryBaseResponse GetBaseData()
        {
            Organisation organisation = organisationRepository.GetOrganizatiobByID();
            IEnumerable<StockCategory> stocks = stockCategoryRepository.GetStockCategoriesForInventory();
            return new InventoryBaseResponse
            {
                StockCategories = stocks,
                StockSubCategories = stocks.SelectMany(s => s.StockSubCategories).ToList(),
                PaperSizes = paperSizeRepository.GetAll(),
                SectionFlags = sectionFlagRepository.GetSectionFlagForInventory(),
                WeightUnits = weightUnitRepository.GetAll(),
                LengthUnits = lengthUnitRepository.GetAll(),
                PaperBasisAreas = paperBasisAreaRepository.GetAll(),
                Organisation = organisation,
                WeightUnit = organisation.WeightUnit != null ? organisation.WeightUnit.UnitName : string.Empty,
                Region = organisation.GlobalLanguage.culture,
                IsImperical = organisation.IsImperical ?? false,
                LoggedInUserId = paperSizeRepository.LoggedInUserId,
                LoggedInUserIdentity = paperSizeRepository.LoggedInUserIdentity,


            };
        }

        /// <summary>
        /// Load Supplier Base data
        /// </summary>
        public SupplierBaseResponse GetSupplierBaseData()
        {
            Organisation organisation = organisationRepository.GetOrganizatiobByID();
            return new SupplierBaseResponse
            {
                CompanyTypes = companyTypeRepository.GetAll(),
                Markups = markupRepository.GetAll(),
                //NominalCodes = chartOfAccountRepository.GetAll(),
                SystemUsers = systemUserRepository.GetAll(),
                Flags = sectionFlagRepository.GetSectionFlagBySectionId(Convert.ToInt64(SectionIds.Suppliers)),
                PriceFlags = sectionFlagRepository.GetSectionFlagBySectionId(Convert.ToInt64(SectionIds.CustomerPriceMatrix)),
                RegistrationQuestions = registrationQuestionRepository.GetAll(),
                CurrencySymbol = organisation != null ? (organisation.Currency != null ? organisation.Currency.CurrencySymbol : string.Empty) : string.Empty

            };
        }
        /// <summary>
        /// Load Stock Items, based on search filters
        /// </summary>
        public InventorySearchResponse LoadStockItems(InventorySearchRequestModel request)
        {
            IEnumerable<SectionFlag> sectionFlags = sectionFlagRepository.GetSectionFlagForInventory();
            IEnumerable<WeightUnit> weightUnits = weightUnitRepository.GetAll();
            InventorySearchResponse stockItemResponse = null;
            if (request.SubCategoryId >= 0)
            {
                stockItemResponse = stockItemRepository.GetStockItems(request);
            }
            else
            {
                stockItemResponse = stockItemRepository.GetStockItemsInOrders(request);
            }
            IEnumerable<StockItem> stockItems = stockItemResponse.StockItems;
            int totalCount = stockItemResponse.TotalCount;
            foreach (var stockItem in stockItems)
            {
                //Set selected color code
                if (stockItem.FlagID != null && stockItem.FlagID != 0 && sectionFlags != null)
                {
                    SectionFlag sectionFlag = sectionFlags.FirstOrDefault(x => x.SectionFlagId == stockItem.FlagID);
                    if (sectionFlag != null)
                        stockItem.FlagColor = sectionFlag.FlagColor;
                }
                //Set selected unit name
                if (stockItem.ItemWeightSelectedUnit != null && weightUnits != null)
                {
                    WeightUnit weightUnit = weightUnits.FirstOrDefault(x => x.Id == stockItem.ItemWeightSelectedUnit);
                    if (weightUnit != null)
                        stockItem.WeightUnitName = weightUnit.UnitName;
                }
                //Set Supplier Company Name
                //if (stockItem.SupplierId != null)
                //{
                //    long supplierId = Convert.ToInt64(stockItem.SupplierId ?? 0);
                //    if (supplierId != 0)
                //    {
                //        stockItem.SupplierCompanyName = companyRepository.Find(supplierId).Name;
                //    }
                //}
            }

            return new InventorySearchResponse { StockItems = stockItems, TotalCount = totalCount };
        }

        /// <summary>
        /// Load Stock Items, based on search filters
        /// </summary>
        public InventorySearchResponse LoadStockItemsInOrder(InventorySearchRequestModel request)
        {
            IEnumerable<SectionFlag> sectionFlags = sectionFlagRepository.GetSectionFlagForInventory();
            IEnumerable<WeightUnit> weightUnits = weightUnitRepository.GetAll();
            var stockItemResponse = stockItemRepository.GetStockItems(request);
            IEnumerable<StockItem> stockItems = stockItemResponse.StockItems;
            int totalCount = stockItemResponse.TotalCount;
            foreach (var stockItem in stockItems)
            {
                //Set selected color code
                if (stockItem.FlagID != null && stockItem.FlagID != 0 && sectionFlags != null)
                {
                    SectionFlag sectionFlag = sectionFlags.FirstOrDefault(x => x.SectionFlagId == stockItem.FlagID);
                    if (sectionFlag != null)
                        stockItem.FlagColor = sectionFlag.FlagColor;
                }
                //Set selected unit name
                if (stockItem.ItemWeightSelectedUnit != null && weightUnits != null)
                {
                    WeightUnit weightUnit = weightUnits.FirstOrDefault(x => x.Id == stockItem.ItemWeightSelectedUnit);
                    if (weightUnit != null)
                        stockItem.WeightUnitName = weightUnit.UnitName;
                }
                //Set Supplier Company Name
                if (stockItem.SupplierId != null)
                {
                    long supplierId = Convert.ToInt64(stockItem.SupplierId ?? 0);
                    if (supplierId != 0)
                    {
                        stockItem.SupplierCompanyName = companyRepository.Find(supplierId).Name;
                    }
                }
            }
            return new InventorySearchResponse { StockItems = stockItems, TotalCount = totalCount };

        }

        /// <summary>
        /// Delete stock Item
        /// </summary>
        /// <param name="stockItemId"></param>
        public void DeleteInvenotry(long stockItemId)
        {
            StockItem stockItem = stockItemRepository.Find(stockItemId);
            if (stockItem != null)
            {
                stockItemRepository.Delete(stockItem);
                stockItemRepository.SaveChanges();
            }
        }

        /// <summary>
        /// Get Suppliers For Inventory
        /// </summary>
        public SupplierSearchResponseForInventory LoadSuppliers(SupplierRequestModelForInventory request)
        {
            return companyRepository.GetSuppliersForInventories(request);
        }

        /// <summary>
        /// Add/Update Stock Item
        /// </summary>
        public StockItem SaveInevntory(StockItem stockItem)
        {
            stockItem.OrganisationId = stockItemRepository.OrganisationId;
            if (stockItem.StockItemId > 0)
            {
                return UpdateStockItem(stockItem);
            }
            else
            {
                return SaveStockItem(stockItem);
            }
        }

        /// <summary>
        /// Save Stock Item
        /// </summary>
        private StockItem SaveStockItem(StockItem stockItem)
        {
            stockItem.StockCreated = DateTime.Now;
            stockItem.ItemCode = prefixRepository.GetNextStockItemCodePrefix();
            stockItem.LastModifiedDateTime = DateTime.Now;

            bool isImperical = organisationRepository.GetImpericalFlagbyOrganisationId();
            stockItem.IsImperical = isImperical;
            stockItemRepository.Add(stockItem);
            stockItemRepository.SaveChanges();
            //After save item content for list view
            return StockItemDeatilForListView(stockItem.StockItemId);
        }

        /// <summary>
        /// Update Stock Item
        /// </summary>
        private StockItem UpdateStockItem(StockItem stockItem)
        {
            StockItem stockItemDbVersion = stockItemRepository.Find(stockItem.StockItemId);
            stockItemDbVersion.ItemName = stockItem.ItemName;
            stockItemDbVersion.ItemCode = stockItem.ItemCode;
            stockItemDbVersion.SupplierId = stockItem.SupplierId;
            stockItemDbVersion.CategoryId = stockItem.CategoryId;
            stockItemDbVersion.SubCategoryId = stockItem.SubCategoryId;
           
            stockItemDbVersion.BarCode = stockItem.BarCode;
            stockItemDbVersion.ItemDescription = stockItem.ItemDescription;
            stockItemDbVersion.FlagID = stockItem.FlagID;
            stockItemDbVersion.Status = stockItem.Status;
            stockItemDbVersion.isDisabled = stockItem.isDisabled;
            stockItemDbVersion.ItemSizeSelectedUnit = stockItem.ItemSizeSelectedUnit;
            stockItemDbVersion.PerQtyQty = stockItem.PerQtyQty;
            stockItemDbVersion.ItemSizeCustom = stockItem.ItemSizeCustom;
            stockItemDbVersion.StockLocation = stockItem.StockLocation;
            stockItemDbVersion.ItemSizeId = stockItem.ItemSizeId;
            stockItemDbVersion.ItemSizeHeight = stockItem.ItemSizeHeight;
            stockItemDbVersion.ItemSizeWidth = stockItem.ItemSizeWidth;
            stockItemDbVersion.PerQtyType = stockItem.PerQtyType;
            stockItemDbVersion.PackageQty = stockItem.PackageQty;
            stockItemDbVersion.RollWidth = stockItem.RollWidth;
            stockItemDbVersion.RollLength = stockItem.RollLength;
            stockItemDbVersion.ReOrderLevel = stockItem.ReOrderLevel;
            stockItemDbVersion.ReorderQty = stockItem.ReorderQty;
            stockItemDbVersion.ItemWeight = stockItem.ItemWeight;
            stockItemDbVersion.ItemColour = stockItem.ItemColour;
            stockItemDbVersion.InkAbsorption = stockItem.InkAbsorption;
            stockItemDbVersion.ItemCoated = stockItem.ItemCoated;
            stockItemDbVersion.PaperBasicAreaId = stockItem.PaperBasicAreaId;
            stockItemDbVersion.ItemCoatedType = stockItem.ItemCoatedType;
            stockItemDbVersion.ItemWeightSelectedUnit = stockItem.ItemWeightSelectedUnit;
            stockItemDbVersion.LastModifiedDateTime = DateTime.Now;
            stockItemDbVersion.inStock = stockItem.inStock;
            stockItemDbVersion.isAllowBackOrder = stockItem.isAllowBackOrder;
            stockItemDbVersion.ThresholdLevel = stockItem.ThresholdLevel;
            stockItemDbVersion.inStock = stockItem.inStock;

            UpdateItemStockUpdateHistories(stockItem, stockItemDbVersion);
            UpdateStockCostAndPrice(stockItem, stockItemDbVersion);

            return StockItemDeatilForListView(stockItem.StockItemId);
        }

        /// <summary>
        /// Update Item Stock Update Histories  
        /// </summary>
        private void UpdateItemStockUpdateHistories(StockItem stockItem, StockItem stockItemDbVersion)
        {
            if (stockItemDbVersion.ItemStockUpdateHistories == null)
            {
                stockItemDbVersion.ItemStockUpdateHistories = new List<ItemStockUpdateHistory>();
            }

            if (stockItem.ItemStockUpdateHistories != null)
            {
                foreach (var itemStockUpdateHistoryItem in stockItem.ItemStockUpdateHistories)
                {
                    if (itemStockUpdateHistoryItem.StockHistoryId == 0)
                    {
                        stockItemDbVersion.ItemStockUpdateHistories.Add(itemStockUpdateHistoryItem);
                    }
                    
                }
            }
        }

        /// <summary>
        /// Update Stock Cost And Price
        /// </summary>
        private void UpdateStockCostAndPrice(StockItem stockItem, StockItem stockItemDbVersion)
        {

            if (stockItem.StockCostAndPrices != null)
            {
                foreach (var item in stockItem.StockCostAndPrices)
                {
                    //In case of added new Stock cost and Price
                    if (
                        stockItemDbVersion.StockCostAndPrices.All(
                            x =>
                                x.CostPriceId != item.CostPriceId ||
                                item.CostPriceId == 0))
                    {
                        item.ItemId = stockItem.StockItemId;
                        stockCostAndPriceRepository.Add(item);
                    }
                    else
                    {
                        //In case of Stock Cost And Price Updated

                        StockCostAndPrice dbStockCostAndPriceItem =
                            stockItemDbVersion.StockCostAndPrices.First(x => x.CostPriceId == item.CostPriceId);

                        if (dbStockCostAndPriceItem != null)
                        {
                            if (dbStockCostAndPriceItem.CostPrice != item.CostPrice || dbStockCostAndPriceItem.FromDate != item.FromDate
                                || dbStockCostAndPriceItem.ToDate != item.ToDate)
                            {
                                dbStockCostAndPriceItem.CostPrice = item.CostPrice;
                                dbStockCostAndPriceItem.FromDate = item.FromDate;
                                dbStockCostAndPriceItem.ToDate = item.ToDate;
                                dbStockCostAndPriceItem.PackCostPrice = item.PackCostPrice;
                            }
                        }
                    }
                }
            }

            //find missing items
            List<StockCostAndPrice> missingStockCostAndPriceListItems = new List<StockCostAndPrice>();
            foreach (StockCostAndPrice dbversionStockCostAndPriceItem in stockItemDbVersion.StockCostAndPrices)
            {
                if (stockItem.StockCostAndPrices != null && stockItem.StockCostAndPrices.All(x => x.CostPriceId != dbversionStockCostAndPriceItem.CostPriceId))
                {
                    missingStockCostAndPriceListItems.Add(dbversionStockCostAndPriceItem);
                }
                //In case user delete all Stock Cost And Price items from client side then it delete all items from db
                if (stockItem.StockCostAndPrices == null)
                {
                    missingStockCostAndPriceListItems.Add(dbversionStockCostAndPriceItem);
                }
            }
            //remove missing items
            foreach (StockCostAndPrice missingStockCostAndPriceItem in missingStockCostAndPriceListItems)
            {
                StockCostAndPrice dbVersionMissingItem = stockItemDbVersion.StockCostAndPrices.First(x => x.CostPriceId == missingStockCostAndPriceItem.CostPriceId);
                if (dbVersionMissingItem.CostPriceId > 0)
                {
                    stockCostAndPriceRepository.Delete(dbVersionMissingItem);
                }
            }

            stockItemRepository.SaveChanges();
        }
        /// <summary>
        /// After Add/Edit return stock item detail contents for list view
        /// </summary>
        private StockItem StockItemDeatilForListView(long stockItemId)
        {
            StockItem stockItem = stockItemRepository.Find(stockItemId);
            //Set selected color code
            if (stockItem.FlagID != null && stockItem.FlagID != 0)
            {
                long flagId = Convert.ToInt64(stockItem.FlagID ?? 0);
                SectionFlag sectionFlag = sectionFlagRepository.Find(flagId);
                if (sectionFlag != null)
                    stockItem.FlagColor = sectionFlag.FlagColor;
            }
            //Set selected unit name
            if (stockItem.ItemWeightSelectedUnit != null && stockItem.ItemWeightSelectedUnit != 0)
            {
                long selectedWeightUnit = Convert.ToInt64(stockItem.ItemWeightSelectedUnit ?? 0);
                WeightUnit weightUnit = weightUnitRepository.Find(selectedWeightUnit);
                if (weightUnit != null)
                    stockItem.WeightUnitName = weightUnit.UnitName;
            }
            //Set Supplier Company Name
            if (stockItem.SupplierId != null && stockItem.SupplierId != 0)
            {
                long supplierId = Convert.ToInt64(stockItem.SupplierId ?? 0);
                if (supplierId != 0)
                {
                    stockItem.SupplierCompanyName = companyRepository.Find(supplierId).Name;
                }
            }

            // Load Stock Category
            stockItemRepository.LoadProperty(stockItem, () => stockItem.StockCategory);
            stockItemRepository.LoadProperty(stockItem, () => stockItem.StockSubCategory);

            return stockItem;
        }

        /// <summary>
        ///Find Stock Item By Id 
        /// </summary>
        public StockItem GetById(long stockItemId)
        {
            return stockItemRepository.Find(stockItemId);
        }

        /// <summary>
        /// Add New Supplier
        /// </summary>
        public Company SaveSupplier(Company company)
        {
            company.CreationDate = DateTime.Now;
            company.OrganisationId = companyRepository.OrganisationId;
            company.IsCustomer = (short)CustomerTypes.Suppliers;

            if (company.Addresses != null)
            {
                foreach (var item in company.Addresses)
                {
                    item.OrganisationId = companyRepository.OrganisationId;
                }

            }

            if (company.CompanyContacts != null)
            {
                foreach (var item in company.CompanyContacts)
                {
                    item.OrganisationId = companyRepository.OrganisationId;
                }

            }
            companyRepository.Add(company);
            companyRepository.SaveChanges();
            SaveCompanyProfileImage(company);
            return company;
        }

        /// <summary>
        /// Save image path for company logo in supplier
        /// </summary>
        public void SaveCompanyImage(string path, long supplierId)
        {
            Company company = companyRepository.Find(supplierId);
            if (company != null)
            {
                company.Image = path;
                companyRepository.SaveChanges();
            }
        }


        private void SaveCompanyProfileImage(Company company)
        {
            if (company.CompanyLogoSource != null)
            {
                string base64 = company.CompanyLogoSource.Substring(company.CompanyLogoSource.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);

                string directoryPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + company.CompanyId);

                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string savePath = directoryPath + "\\logo.png";
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);

                company.Image = savePath;
                companyRepository.SaveChanges();

            }
        }
        #endregion
    }

}
