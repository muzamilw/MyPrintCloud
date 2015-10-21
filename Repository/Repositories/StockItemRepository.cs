using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;
using AutoMapper;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Stock Item Repository
    /// </summary>
    public class StockItemRepository : BaseRepository<StockItem>, IStockItemRepository
    {
        #region privte
        /// <summary>
        /// Company Orderby clause
        /// </summary>
        private readonly Dictionary<InventoryByColumn, Func<StockItem, object>> stockItemOrderByClause = new Dictionary<InventoryByColumn, Func<StockItem, object>>
                    {
                         {InventoryByColumn.Name, c => c.ItemName}
                    };
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public StockItemRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<StockItem> DbSet
        {
            get
            {
                return db.StockItems;
            }
        }

        #endregion

        #region public
        
        /// <summary>
        /// Get All StockI tem 
        /// </summary>
        public override IEnumerable<StockItem> GetAll()
        {
            return DbSet.ToList();
        }

        /// <summary>
        /// Will Return Stock with name A4 of type Paper
        /// </summary>
        public StockItem GetA4PaperStock()
        {

            StockItem objStockItem = new StockItem();
                
            objStockItem = DbSet.FirstOrDefault(stock => stock.ItemName.Contains("A4") && stock.OrganisationId == OrganisationId &&
                                              stock.CategoryId == (int) StockCategoryEnum.Paper);


            if(objStockItem == null)
            {
               objStockItem = db.StockItems.Where(c => c.OrganisationId == OrganisationId && c.CategoryId == (int)StockCategoryEnum.Paper).FirstOrDefault();
            }

            return objStockItem;
        }

        /// <summary>
        /// Search Company
        /// </summary>
        public InventorySearchResponse GetStockItems(InventorySearchRequestModel request)
        {
            //((string.IsNullOrEmpty(request.Region) || stockItem.Region == request.Region)

            bool isImperical = db.Organisations.Where(o => o.OrganisationId == OrganisationId).Select(c => c.IsImperical ?? false).FirstOrDefault();
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            Expression<Func<StockItem,bool>> query =
                stockItem =>
                    (string.IsNullOrEmpty(request.SearchString) || (stockItem.ItemName.Contains(request.SearchString)) ||
                     (stockItem.AlternateName.Contains(request.SearchString))) && (
                         (!request.CategoryId.HasValue || request.CategoryId == stockItem.CategoryId) &&
                         (!request.SubCategoryId.HasValue || request.SubCategoryId == stockItem.SubCategoryId)) && stockItem.OrganisationId == OrganisationId && stockItem.IsImperical == isImperical && stockItem.isDisabled != true;

            IEnumerable<StockItem> stockItems = request.IsAsc
               ? DbSet.Where(query)
                   .OrderBy(stockItemOrderByClause[request.InventoryOrderBy])
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList()
               : DbSet.Where(query)
                   .OrderByDescending(stockItemOrderByClause[request.InventoryOrderBy])
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList();
            return new InventorySearchResponse { StockItems = stockItems, TotalCount = DbSet.Count(query) };
        }

        /// <summary>
        /// Get Stock Items In orders 
        /// </summary>
        public InventorySearchResponse GetStockItemsInOrders(InventorySearchRequestModel request)
        {
            bool isImperical = db.Organisations.Where(o => o.OrganisationId == OrganisationId).Select(c => c.IsImperical ?? false).FirstOrDefault();
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            Expression<Func<StockItem, bool>> query =
                stockItem =>
                    (string.IsNullOrEmpty(request.SearchString) || (stockItem.ItemName.Contains(request.SearchString)) ||
                     (stockItem.AlternateName.Contains(request.SearchString)) || (stockItem.StockCategory.Name.Contains(request.SearchString))
                     || (stockItem.StockSubCategory.Name.Contains(request.SearchString)) || (stockItem.Company.Name.Contains(request.SearchString))) && (
                         (!request.CategoryId.HasValue || request.CategoryId == stockItem.CategoryId)) && stockItem.OrganisationId == OrganisationId && stockItem.IsImperical == isImperical && stockItem.isDisabled != true;

            IEnumerable<StockItem> stockItems = request.IsAsc
               ? DbSet.Where(query)
                   .OrderBy(stockItemOrderByClause[request.InventoryOrderBy])
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList()
               : DbSet.Where(query)
                   .OrderByDescending(stockItemOrderByClause[request.InventoryOrderBy])
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList();
            return new InventorySearchResponse { StockItems = stockItems, TotalCount = DbSet.Count(query) };   
        }
        /// <summary>
        /// Get Items For Product
        /// </summary>
        public InventorySearchResponse GetStockItemsForProduct(StockItemRequestModel request)
        {
            bool isImperical = db.Organisations.Where(o => o.OrganisationId == OrganisationId).Select(c => c.IsImperical ?? false).FirstOrDefault();
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            Expression<Func<StockItem, bool>> query =
                stockItem =>
                    (string.IsNullOrEmpty(request.SearchString) || stockItem.ItemName.Contains(request.SearchString)) &&
                    (!request.CategoryId.HasValue || request.CategoryId == stockItem.CategoryId) &&
                    stockItem.OrganisationId == OrganisationId && stockItem.IsImperical == isImperical && stockItem.isDisabled != true;

            IEnumerable<StockItem> stockItems = request.IsAsc
               ? DbSet.Where(query)
                   .OrderBy(item=> item.ItemName)
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList()
               : DbSet.Where(query)
                   .OrderBy(item => item.ItemName)
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList();

            

            return new InventorySearchResponse { StockItems = stockItems, TotalCount = DbSet.Count(query) };
        }

        public List<StockItem> GetStockItemsByOrganisationID(long OrganisationID)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;

                Mapper.CreateMap<StockItem, StockItem>()
            .ForMember(x => x.ItemSections, opt => opt.Ignore())
            .ForMember(x => x.SectionCostCentreDetails, opt => opt.Ignore())
            .ForMember(x => x.StockCategory, opt => opt.Ignore())
            .ForMember(x => x.StockCategory, opt => opt.Ignore())
             .ForMember(x => x.StockSubCategory, opt => opt.Ignore());


                Mapper.CreateMap<StockCostAndPrice, StockCostAndPrice>()
           .ForMember(x => x.StockItem, opt => opt.Ignore());

                List<StockItem> stocks = db.StockItems.Include("StockCostAndPrices").Where(o => o.OrganisationId == OrganisationID).ToList();

                List<StockItem> oOutputStockItems = new List<StockItem>();

                if (stocks != null && stocks.Count > 0)
                {
                    foreach (var item in stocks)
                    {
                        var omappedItem = Mapper.Map<StockItem, StockItem>(item);
                        oOutputStockItems.Add(omappedItem);
                    }
                }

                return oOutputStockItems;
               
            } 
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<StockItem> GetStockItemOfCategoryInk()
        {
            var stockItems = db.StockItems.Where(x => x.StockCategory.CategoryId == 2 && x.OrganisationId == OrganisationId).ToList();
            return stockItems;
        }
       
        public string GetStockName (long StockID)
        {
            return db.StockItems.Where(f => f.StockItemId == StockID).Select(i => i.ItemName).FirstOrDefault();
        }
        #endregion
    }
}
