using System;
using System.Collections.Generic;
using System.Linq;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.MISServices
{
    public class StockCategoryService : IStockCategoryService
    {
        #region Private

        private readonly IStockCategoryRepository stockCategoryRepository;
        private readonly IStockSubCategoryRepository stockSubCategoryRepository;

        #endregion
        #region Constructor

        public StockCategoryService(IStockCategoryRepository stockCategoryRepository, IStockSubCategoryRepository stockSubCategoryRepository)
        {
            this.stockCategoryRepository = stockCategoryRepository;
            this.stockSubCategoryRepository = stockSubCategoryRepository;
        }

        #endregion
        public StockCategoryResponse GetAll(StockCategoryRequestModel request)
        {
            return stockCategoryRepository.SearchStockCategory(request);
        }

        public StockCategory Add(StockCategory stockCategory)
        {
            //stockCategory.CompanyId = 324234;todo
            stockCategory.OrganisationId = stockCategoryRepository.OrganisationId;
            stockCategoryRepository.Add(stockCategory);
            stockCategoryRepository.SaveChanges();
            return stockCategory;
        }

        public StockCategory Update(StockCategory stockCategory)
        {
            stockCategory.OrganisationId = stockCategoryRepository.OrganisationId;
            var stockDbVersion = stockCategoryRepository.Find(stockCategory.CategoryId);
            #region Sub Stock Categories Items
            //Add  SubStockCategories 
            if (stockCategory.StockSubCategories != null)
            {
                foreach (var item in stockCategory.StockSubCategories)
                {
                    if (stockDbVersion.StockSubCategories.All(x => x.SubCategoryId != item.SubCategoryId) || item.SubCategoryId == 0)
                    {
                        item.CategoryId = stockCategory.CategoryId;
                        stockDbVersion.StockSubCategories.Add(item);
                    }
                }
            }
            //find missing items

            List<StockSubCategory> missingStockSubCategories = new List<StockSubCategory>();
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (StockSubCategory dbversionStockSubCategories in stockDbVersion.StockSubCategories)
            {
                if (stockCategory.StockSubCategories != null && stockCategory.StockSubCategories.All(x => x.SubCategoryId != dbversionStockSubCategories.SubCategoryId))
                {
                    missingStockSubCategories.Add(dbversionStockSubCategories);
                }
            }

            //remove missing items
            foreach (StockSubCategory missingStockSubCategory in missingStockSubCategories)
            {

                StockSubCategory dbVersionMissingItem = stockDbVersion.StockSubCategories.First(x => x.SubCategoryId == missingStockSubCategory.SubCategoryId);
                if (dbVersionMissingItem.SubCategoryId > 0)
                {
                    if (dbVersionMissingItem.StockItems.Count > 0)
                    {
                        throw new Exception(" It is Being used In Stock Items! "); 
                    }
                    
                    stockDbVersion.StockSubCategories.Remove(dbVersionMissingItem);
                    stockSubCategoryRepository.Delete(dbVersionMissingItem);
                }
            }
            if (stockCategory.StockSubCategories != null)
            {
                //updating stock sub categories
                foreach (var subCategoryItem in stockCategory.StockSubCategories)
                {
                    stockSubCategoryRepository.Update(subCategoryItem);
                }
            }

            #endregion
            stockCategoryRepository.Update(stockCategory);
            stockCategoryRepository.SaveChanges();
            return stockCategory;
        }

        public bool Delete(int stockCategoryId)
        {
            var stockCategoryToBeDeleted = GetStockCategoryById(stockCategoryId);
            if (stockCategoryToBeDeleted.StockItems.Where(c => c.isDisabled != true).ToList().Count > 0)
            {
                throw new Exception (" It is Being used In Stock Items! ");
            }
            stockCategoryRepository.Delete(GetStockCategoryById(stockCategoryId));
            stockCategoryRepository.SaveChanges();
            return true;
        }

        public StockCategory GetStockCategoryById(int id)
        {
            return stockCategoryRepository.Find(id);
        }

        /// <summary>
        /// Being used in Base Data as DD
        /// </summary>
        public IEnumerable<StockCategory> GetAllStockCategories()
        {
          return  stockCategoryRepository.GetAll();
        }
    }
}
