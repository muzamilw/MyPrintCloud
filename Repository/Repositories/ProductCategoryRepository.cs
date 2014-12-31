using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public ProductCategory GetCategoryById(int categoryId)
        {
            List<ProductCategory> LstCategories = this.GetPublicCategories(); //get all the categories
            ProductCategory objCategory = LstCategories.Find(category => category.ProductCategoryId == categoryId);
            return objCategory;
        }

        public List<ProductCategory> GetPublicCategories()
        {
            return db.ProductCategories.ToList();
        
        }

        public List<ProductCategory> GetChildCategories(int categoryId)
        {
           
                List<ProductCategory> childCategoresList =  db.ProductCategories.Where(category => category.ParentCategoryId.HasValue && category.ParentCategoryId.Value == categoryId && category.isArchived == false && category.isEnabled == true && category.isPublished == true).ToList();
                return childCategoresList;
       

        }

        public List<ProductCategory> GetAllChildCorporateCatalogByTerritory(int customerId, int ContactId, int ParentCatId)
        {
                var query = (from product in db.ProductCategories
                             join CT in db.CategoryTerritories on product.ParentCategoryId equals CT.ProductCategoryId
                             join contact in db.CompanyContacts on CT.TerritoryId equals contact.TerritoryId
                             where contact.ContactId == ContactId && product.ParentCategoryId == ParentCatId && product.isEnabled == true
                             && product.isPublished == true && (product.isArchived == false || product.isArchived == null)
                             select product).ToList();
                return query.OrderBy(i => i.DisplayOrder).ToList();
           
        }

    }
}
