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
    public class StockCategoryRepository : BaseRepository<StockCategory>, IStockCategoryRepository
    {
        #region Private
        private readonly Dictionary<StockCategoryByColumn, Func<StockCategory, object>> stockCategoryOrderByClause = new Dictionary<StockCategoryByColumn, Func<StockCategory, object>>
                    {
                        {StockCategoryByColumn.Code, d => d.Code},
                        {StockCategoryByColumn.Name, c => c.Name},
                        {StockCategoryByColumn.Description, d => d.Description}
                    };
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public StockCategoryRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<StockCategory> DbSet
        {
            get
            {
                return db.StockCategories;
            }
        }

        #endregion
        #region Public

        /// <summary>
        /// Get All Stock Category
        /// </summary>
        public override IEnumerable<StockCategory> GetAll()
        {
            return DbSet.Where(stockCategory => stockCategory.OrganisationId == OrganisationId || stockCategory.OrganisationId == 0).ToList();
        }
        public List<StockCategory> getDefaulStockCat()
        {
            db.Configuration.LazyLoadingEnabled = true;
            db.Configuration.ProxyCreationEnabled = true;
            return db.StockCategories.Where(c => c.OrganisationId == 0).ToList();
        }
        /// <summary>
        /// Get Stock Categories For Inventory
        /// </summary>
        public IEnumerable<StockCategory> GetStockCategoriesForInventory()
        {
            return DbSet.Where(stockCategory => (stockCategory.OrganisationId == OrganisationId || stockCategory.OrganisationId == 0)
                && stockCategory.CategoryId != 3 && stockCategory.CategoryId != 4).ToList();
        }
        public StockCategoryResponse SearchStockCategory(StockCategoryRequestModel request)
        {
            int rowCount = 0;
            IEnumerable<StockCategory> stockCategories = null;
            if(request.StockCategoryId > 0) // edit case
            {

                stockCategories = db.StockCategories.Where(c => c.CategoryId == request.StockCategoryId).ToList();

                if (stockCategories != null && stockCategories.Count() > 0)
                {
                    foreach (var sc in stockCategories)
                    {
                        sc.StockSubCategories = sc.StockSubCategories.Where(c => c.OrganisationId == OrganisationId).ToList();
                    }
                }
               
            }
            else // list case
            {
                int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;
                bool isStringSpecified = !string.IsNullOrEmpty(request.SearchString);
                bool isCategoryIdSpecified = request.StockCategoryId != 0;
                Expression<Func<StockCategory, bool>> query =
                    s =>
                        (isStringSpecified && (s.Name.Contains(request.SearchString)) || !isStringSpecified) &&
                        ((isCategoryIdSpecified && s.CategoryId.Equals(request.StockCategoryId)) || !isCategoryIdSpecified) &&
                        (s.OrganisationId == OrganisationId || s.OrganisationId == 0) && s.CategoryId != 3 && s.CategoryId != 4;

                rowCount = DbSet.Count(query);
                stockCategories = request.IsAsc
                    ? DbSet.Where(query)
                        .OrderBy(stockCategoryOrderByClause[request.StockCategoryOrderBy])
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : DbSet.Where(query)
                        .OrderByDescending(stockCategoryOrderByClause[request.StockCategoryOrderBy])
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();
            }

            return new StockCategoryResponse
                   {
                       RowCount = rowCount,
                       StockCategories = stockCategories
                   };
        }
        public List<StockCategory> GetStockCategoriesByOrganisationID(long OrganisationID)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                // List<StockCategory> stockcategories = new List<StockCategory>();

                Mapper.CreateMap<StockCategory, StockCategory>()
                .ForMember(x => x.StockItems, opt => opt.Ignore());

                Mapper.CreateMap<StockSubCategory, StockSubCategory>()
               .ForMember(x => x.StockItems, opt => opt.Ignore())
              .ForMember(x => x.StockCategory, opt => opt.Ignore());

                Mapper.CreateMap<StockItem, StockItem>()
                    .ForMember(x => x.Company, opt => opt.Ignore())
               .ForMember(x => x.ItemSections, opt => opt.Ignore())
               .ForMember(x => x.SectionCostCentreDetails, opt => opt.Ignore())
               .ForMember(x => x.StockCategory, opt => opt.Ignore())
                .ForMember(x => x.StockSubCategory, opt => opt.Ignore());


                Mapper.CreateMap<StockCostAndPrice, StockCostAndPrice>()
           .ForMember(x => x.StockItem, opt => opt.Ignore());


                List<StockCategory> StockCat = db.StockCategories.Include("StockSubCategories").Where(s => s.OrganisationId == OrganisationID).ToList();


                List<StockCategory> oOutputStockItems = new List<StockCategory>();

                if (StockCat != null && StockCat.Count > 0)
                {
                    foreach (var item in StockCat)
                    {
                        var omappedItem = Mapper.Map<StockCategory, StockCategory>(item);
                        oOutputStockItems.Add(omappedItem);
                    }
                }
                return oOutputStockItems;

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public List<StockCategory> getStockCatByOrgid()
        {
            db.Configuration.LazyLoadingEnabled = true;
            db.Configuration.ProxyCreationEnabled = true;
            return db.StockCategories.Where(c => c.OrganisationId == OrganisationId).ToList();
        }

        public void UpdateStockItemForCatDeleteion(long StockId,long CategoryId,long SubCategoryId)
        {
            try
            {
                StockItem stockItems = db.StockItems.Where(c => c.StockItemId == StockId).FirstOrDefault();
                if(stockItems != null)
                {
                    stockItems.CategoryId = CategoryId;
                    stockItems.SubCategoryId = SubCategoryId;

                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<StockSubCategory> getStockSubCategoryByCategoryId(long CatId)
        {
            return db.StockSubCategories.Where(c => c.CategoryId == CatId).ToList();
        }

        public StockCategory getStockCategoryByCategoryId(long CatId)
        {
            StockCategory category = db.StockCategories.Where(c => c.CategoryId == CatId).FirstOrDefault();

            //if(category != null)
            //{
            //    if(category.StockSubCategories != null && category.StockSubCategories.Count > 0)
            //    {
            //        category.StockSubCategories = category.StockSubCategories.Where(c => c.OrganisationId == OrganisationId).ToList();
            //    }
            //}
            return category;
        }
        #endregion
    }
}
