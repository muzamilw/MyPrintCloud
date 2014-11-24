using System.Collections.Generic;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;

namespace MPC.Implementation.MISServices
{
    public class StockCategoryService: IStockCategoryService
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
        public IEnumerable<StockCategory> GetAll(StockCategoryRequestModel request)
        {
            int rowCount;
            return stockCategoryRepository.SearchStockCategory(request, out rowCount);
        }

        public StockCategory Add(StockCategory stockCategory)
        {
            stockCategoryRepository.Add(stockCategory);
            stockCategoryRepository.SaveChanges();
            return stockCategory;
        }

        public StockCategory Update(StockCategory stockCategory)
        {
            stockCategoryRepository.Update(stockCategory);
            stockCategoryRepository.SaveChanges();
            return stockCategory;
        }

        public bool Delete(int stockCategoryId)
        {
            stockCategoryRepository.Delete(GetStockCategoryById(stockCategoryId));
            stockCategoryRepository.SaveChanges();
            return true;
        }

        public StockCategory GetStockCategoryById(int id)
        {
            return stockCategoryRepository.Find(id);
        }
    }
}
