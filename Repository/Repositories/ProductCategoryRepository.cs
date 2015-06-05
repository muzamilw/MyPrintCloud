using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;

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


        public List<ProductCategory> GetParentCategoriesByStoreId(long companyId, long OrganisationId)
        {

            return db.ProductCategories.Where(
                p => p.CompanyId == companyId && p.OrganisationId == OrganisationId && (p.ParentCategoryId == null || p.ParentCategoryId == 0)
                && p.isEnabled == true && p.isPublished == true
                             && (p.isArchived == false || p.isArchived == null)).ToList();
           
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
                             where contact.ContactId == ContactId && contact.CompanyId == customerId && product.CompanyId == customerId 
                             && (product.ParentCategoryId == 0 || product.ParentCategoryId == null) && product.isEnabled == true && product.isPublished == true
                             && (product.isArchived == false || product.isArchived == null)
                             select product).ToList();
                return query.OrderBy(i => i.DisplayOrder).ToList();
            
        }

        public List<ProductCategory> GetAllCategoriesByStoreId(long companyId, long OrganisationId) 
        {
            return db.ProductCategories.Where(
                p => p.CompanyId == companyId && p.OrganisationId == OrganisationId && (p.isArchived == false || p.isArchived == null) && p.isPublished == true && p.isEnabled == true).ToList();
        }

        public ProductCategory GetCategoryById(long categoryId)
        {
            List<ProductCategory> LstCategories = this.GetPublicCategories(); //get all the categories
            ProductCategory objCategory = LstCategories.Find(category => category.ProductCategoryId == categoryId);
            return objCategory;
        }

        public List<ProductCategory> GetPublicCategories()
        {
            return db.ProductCategories.ToList();
        
        }

        public List<ProductCategory> GetChildCategories(long categoryId, long CompanyId)
        {
            List<ProductCategory> childCategoresList = db.ProductCategories.Where(category => category.ParentCategoryId.HasValue && category.ParentCategoryId.Value == categoryId && (category.isArchived == false || category.isArchived == null) && category.isEnabled == true && category.isPublished == true && category.CompanyId == CompanyId).ToList().OrderBy(x => x.DisplayOrder).ToList();
            return childCategoresList;
        }

        public List<ProductCategory> GetAllChildCorporateCatalogByTerritory(long customerId, long ContactId, long ParentCatId)
        {
                var query = (from product in db.ProductCategories
                             join CT in db.CategoryTerritories on product.ParentCategoryId equals CT.ProductCategoryId
                             join contact in db.CompanyContacts on CT.TerritoryId equals contact.TerritoryId
                             where contact.ContactId == ContactId && product.ParentCategoryId == ParentCatId && product.isEnabled == true
                             && product.isPublished == true && (product.isArchived == false || product.isArchived == null)
                             select product).ToList();
                return query.OrderBy(i => i.DisplayOrder).ToList();
           
        }

        public List<ProductCategoriesView> GetMappedCategoryNames(bool isClearCache, int companyID)
        {
            List<ProductCategoriesView> mappedCategories = null;
            //if (HttpContext.Current.Cache["MappedCategoryNames"] == null || isClearCache == true)
            //{
            mappedCategories = GetProductDesignerMappedCategoryNames(companyID);
            //    HttpContext.Current.Cache["MappedCategoryNames"] = mappedCategories;
            //}
            //else
            //{
            //    mappedCategories = HttpContext.Current.Cache["MappedCategoryNames"] as List<vw_ProductCategories>;
            //}

            return mappedCategories;
        }
        public List<ProductCategoriesView> GetProductDesignerMappedCategoryNames(int CompanyID)
        {
            try
            {

                return (from c in db.ProductCategoriesViews
                        where !string.IsNullOrEmpty(c.TemplateDesignerMappedCategoryName) && c.CompanyId == CompanyID
                        select c).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        public ProductCategoriesView GetMappedCategory(string CatName, int CID)
        {
            try
            {
                return (from c in GetMappedCategoryNames(false, CID).ToList()
                        where c.TemplateDesignerMappedCategoryName == CatName
                        select c).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }
        /// <summary>
        /// get name of parent categogry by ItemID
        /// </summaery>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public string GetImmidiateParentCategory(long ItemID, out string CurrentProductCategoryName)
        {
            try
            {
                long catID = db.ProductCategoryItems.Where(p => p.ItemId == ItemID).Select(s => s.CategoryId ?? 0).FirstOrDefault();
                List<ProductCategory> LstCategories = this.GetPublicCategories(); //get all the categories

                ProductCategory currCategory = LstCategories.Find(category => category.ProductCategoryId == catID); //finds itself
                CurrentProductCategoryName = currCategory.CategoryName;
                if (currCategory != null)
                {

                    if ((currCategory.ParentCategoryId ?? 0) > 0)
                        currCategory = LstCategories.Find(cat => cat.ProductCategoryId == currCategory.ParentCategoryId.Value); // finds the first parent
                }
                if (currCategory != null)
                    return currCategory.CategoryName;
                else
                    return string.Empty;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Get Parent Categories For Organisation
        /// </summary>
        public IEnumerable<ProductCategory> GetParentCategories(long? companyId)
        {
            return DbSet.Where(productCategory => !productCategory.ParentCategoryId.HasValue && productCategory.OrganisationId == OrganisationId &&
                (!companyId.HasValue || productCategory.CompanyId == companyId)).ToList();
        }

        public List<ProductCategory> GetChildCategories(long categoryId)
        {
            List<ProductCategory> childCategoresList = db.ProductCategories.Where(category => category.ParentCategoryId.HasValue && 
                category.ParentCategoryId.Value == categoryId && category.isArchived == false && category.isEnabled == true && category.isPublished == true && 
                category.OrganisationId == OrganisationId).ToList().OrderBy(x => x.DisplayOrder).ToList();
            return childCategoresList;

        }

        public List<ProductCategory> GetAllRetailPublishedCat()
        {
                return (from p in db.ProductCategories
                        where p.isPublished == true && p.isEnabled == true
                        && (p.isArchived == false || p.isArchived == null)
                        && p.CompanyId == null
                        select p).ToList();
        }

        public List<ProductCategory> GetAllCategories()
        {
            return db.ProductCategories.ToList();
        }
    }
}
