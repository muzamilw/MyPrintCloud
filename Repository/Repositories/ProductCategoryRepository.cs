using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    public class ProductCategoryRepository : BaseRepository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(IUnityContainer container)
            : base(container)
        {
        }

        protected override IDbSet<ProductCategory> DbSet
        {
            get
            {
                return db.ProductCategories;
            }
        }


        public List<ProductCategory> GetParentCategoriesByStoreId(long companyId)
        {

            return db.ProductCategories.Where(
                p => p.CompanyId == companyId && (p.ParentCategoryId == null || p.ParentCategoryId == 0)).ToList();
           
        }

        public List<ProductCategory> GetAllParentCorporateCatalog(int customerId)
        {
                var query = (from product in db.ProductCategories
                             where product.CompanyId == customerId && (product.ParentCategoryId == 0 || product.ParentCategoryId == null) && product.isEnabled == true && product.isPublished == true
                             && (product.isArchived == false || product.isArchived == null)
                             select product).ToList();
                return query.OrderBy(i => i.DisplayOrder).ToList();
           

        }

        public List<ProductCategory> GetAllParentCorporateCatalogByTerritory(int customerId, int ContactId)
        {
          
                var query = (from product in db.ProductCategories
                             join CT in db.CategoryTerritories on product.ProductCategoryId equals CT.ProductCategoryId
                             join contact in db.CompanyContacts on CT.TerritoryId equals contact.TerritoryId
                             where contact.ContactId == ContactId && product.CompanyId == customerId && (product.ParentCategoryId == 0 || product.ParentCategoryId == null) && product.isEnabled == true && product.isPublished == true
                             && (product.isArchived == false || product.isArchived == null)
                             select product).ToList();
                return query.OrderBy(i => i.DisplayOrder).ToList();
            
        }

        public List<ProductCategory> GetAllCategoriesByStoreId(long companyId) 
        {
            return db.ProductCategories.Where(
                p => p.CompanyId == companyId && (p.isArchived == false || p.isArchived == null) && p.isPublished == true && p.isEnabled == true ).ToList();
        }
    }
}
