using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.IdentityModel.SecurityTokenService;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.Repositories;

namespace MPC.Implementation.MISServices
{
    public class CategoryService : ICategoryService
    {
        #region Private

        private readonly IProductCategoryRepository productCategoryRepository;
        private readonly ICategoryTerritoryRepository categoryTerritoryRepository;

        private void SaveProductCategoryThumbNailImage(ProductCategory productCategory)
        {
            var thumbNailFileBytes = new byte[] { };
            var imageFileBytes = new byte[] { };
            if (!string.IsNullOrEmpty(productCategory.ThumbNailBytes))
            {
                string base64 = productCategory.ThumbNailBytes.Substring(productCategory.ThumbNailBytes.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                thumbNailFileBytes = Convert.FromBase64String(base64);
            }
            if (!string.IsNullOrEmpty(productCategory.ImageBytes))
            {
                string base64Image = productCategory.ImageBytes.Substring(productCategory.ImageBytes.IndexOf(',') + 1);
                base64Image = base64Image.Trim('\0');
                imageFileBytes = Convert.FromBase64String(base64Image);
            }

            string directoryPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + productCategoryRepository.OrganisationId + "/" + productCategory.CompanyId + "/ProductCategories");
            if ((!string.IsNullOrEmpty(directoryPath)) && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string savePath;
            int indexOf;
            if (thumbNailFileBytes.Count() > 0)
            {
                savePath = directoryPath + "\\" + productCategory.ProductCategoryId + "_" + StringHelper.SimplifyString(productCategory.CategoryName) + "_Thumbnail.png";
                if ((!string.IsNullOrEmpty(productCategory.ThumbnailPath)) && File.Exists(HttpContext.Current.Server.MapPath("~/" + productCategory.ThumbnailPath)))
                {
                    File.Delete(productCategory.ThumbnailPath);
                }
                File.WriteAllBytes(savePath, thumbNailFileBytes);
                indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                productCategory.ThumbnailPath = savePath.Substring(indexOf, savePath.Length - indexOf);
            }
            if (imageFileBytes.Count() > 0)
            {

                savePath = directoryPath + "\\" + productCategory.ProductCategoryId + "_" + StringHelper.SimplifyString(productCategory.CategoryName) + "_Banner.png";
                if ((!string.IsNullOrEmpty(productCategory.ImagePath)) && File.Exists(HttpContext.Current.Server.MapPath("~/" + productCategory.ImagePath)))
                {
                    File.Delete(productCategory.ImagePath);
                }
                File.WriteAllBytes(savePath, imageFileBytes);
                indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                productCategory.ImagePath = savePath.Substring(indexOf, savePath.Length - indexOf);
            }

        }


        //#region Private Methods
        private ProductCategory Create(ProductCategory productCategory)
        {
            productCategory.OrganisationId = productCategoryRepository.OrganisationId;
            productCategoryRepository.Add(productCategory);
            AddCategoryTerritories(productCategory);
            productCategoryRepository.SaveChanges();
            SaveProductCategoryThumbNailImage(productCategory);
            productCategoryRepository.Update(productCategory);
            productCategoryRepository.SaveChanges();
            return productCategory;
        }

        private ProductCategory Update(ProductCategory productCategory)
        {
            productCategory.OrganisationId = productCategoryRepository.OrganisationId;
            UpdateCategoryTerritories(productCategory);
            SaveProductCategoryThumbNailImage(productCategory);
            productCategoryRepository.Update(productCategory);
            productCategoryRepository.SaveChanges();
            //productCategoryRepository.Update(productCategory);
            //productCategoryRepository.SaveChanges();
            return productCategory;
        }

        private void AddCategoryTerritories(ProductCategory productCategory)
        {
            if (productCategory.CategoryTerritories != null)
            {
                foreach (var categoryTerritory in productCategory.CategoryTerritories)
                {
                    categoryTerritory.CompanyId = productCategory.CompanyId;
                    categoryTerritory.OrganisationId = productCategoryRepository.OrganisationId;
                    categoryTerritory.ProductCategoryId = productCategory.ProductCategoryId;
                    categoryTerritory.TerritoryId = categoryTerritory.TerritoryId;
                    categoryTerritoryRepository.Add(categoryTerritory);
                }
            }
        }
        private void UpdateCategoryTerritories(ProductCategory productCategory)
        {
            var productCategoryDbVersion = productCategoryRepository.GetCategoryById(productCategory.ProductCategoryId);
            #region Company Cost Centers
            //Add  Company Cost Centers
            if (productCategory.CategoryTerritories != null)
            {
                List<CategoryTerritory> newlist = productCategory.CategoryTerritories.Where(
                    c => productCategoryDbVersion.CategoryTerritories.All(cc => cc.CategoryTerritoryId != c.CategoryTerritoryId)).ToList();

                foreach (var item in newlist)
                {
                    item.CompanyId = productCategory.CompanyId;
                    item.OrganisationId = productCategoryRepository.OrganisationId;
                    item.ProductCategoryId = productCategory.ProductCategoryId;
                    //productCategory.CompanyCostCentres.Add(item);
                    categoryTerritoryRepository.Add(item);
                }
            }
            if (productCategory.CategoryTerritories != null)
            {
                List<CategoryTerritory> missingItemsList = productCategoryDbVersion.CategoryTerritories.Where(
                    c => productCategory.CategoryTerritories.All(cc => cc.CategoryTerritoryId != c.CategoryTerritoryId)).ToList();
                //remove missing items
                foreach (CategoryTerritory missingCategoryTerritory in missingItemsList)
                {
                    CategoryTerritory dbVersionMissingItem = productCategoryDbVersion.CategoryTerritories.First(x => x.CategoryTerritoryId == missingCategoryTerritory.CategoryTerritoryId && x.CompanyId == missingCategoryTerritory.CompanyId);
                    categoryTerritoryRepository.Delete(dbVersionMissingItem);
                    productCategoryDbVersion.CategoryTerritories.Remove(dbVersionMissingItem);

                }
            }
            else if (productCategory.CategoryTerritories == null && productCategoryDbVersion.CategoryTerritories != null && productCategoryDbVersion.CategoryTerritories.Count > 0)
            {
                List<CategoryTerritory> lisRemoveAllItemsList = productCategoryDbVersion.CategoryTerritories.ToList();
                foreach (CategoryTerritory missingCategoryTerritory in lisRemoveAllItemsList)
                {
                    CategoryTerritory dbVersionMissingItem = productCategoryDbVersion.CategoryTerritories.First(x => x.CategoryTerritoryId == missingCategoryTerritory.CategoryTerritoryId && x.CompanyId == missingCategoryTerritory.CompanyId);
                    categoryTerritoryRepository.Delete(dbVersionMissingItem);
                    productCategoryDbVersion.CategoryTerritories.Remove(dbVersionMissingItem);
                }
            }

            #endregion

        }
        //#endregion
        #endregion

        #region Constructor

        public CategoryService(IProductCategoryRepository productCategoryRepository, ICategoryTerritoryRepository categoryTerritoryRepository)
        {
            this.productCategoryRepository = productCategoryRepository;
            this.categoryTerritoryRepository = categoryTerritoryRepository;
        }

        #endregion

        #region Public

        public List<ProductCategory> GetChildCategories(int categoryId)
        {
            var result = productCategoryRepository.GetChildCategories(categoryId);
            return result;
        }
        public List<ProductCategory> GetChildCategoriesIncludingArchive(int categoryId)
        {
            var result = productCategoryRepository.GetChildCategoriesIncludingArchive(categoryId);
            return result;
        }

        public ProductCategory GetProductCategoryById(int categoryId)
        {
            var result = productCategoryRepository.GetCategoryById(categoryId);
            //if (result.ThumbnailStreamId.HasValue)
            //{
            //    CategoryFileTableView categoryfileTableView = productCategoryFileTableViewRepository.GetByStreamId(result.ThumbnailStreamId.Value);
            //    if (categoryfileTableView != null)
            //    {
            //        result.ThumbNailFileBytes = categoryfileTableView.FileStream;
            //    }
            //}
            //if (result.ImageStreamId.HasValue)
            //{
            //    CategoryFileTableView categoryfileTableView = productCategoryFileTableViewRepository.GetByStreamId(result.ImageStreamId.Value);
            //    if (categoryfileTableView != null)
            //    {
            //        result.ImageFileBytes = categoryfileTableView.FileStream;
            //    }
            //}
            return result;
        }

        public ProductCategory Save(ProductCategory productCategory)
        {
            if (productCategory.ProductCategoryId == 0)
            {
                return Create(productCategory);
            }
            return Update(productCategory);
        }

        /// <summary>
        /// Delete Product Category
        /// </summary>
        public void DeleteCategory(int productCategoryId)
        {
            ProductCategory category = productCategoryRepository.Find(productCategoryId);
            if (category != null)
            {
                category.isArchived = true;
                productCategoryRepository.Update(category);
                productCategoryRepository.SaveChanges();
            }
        }

        #endregion
    }
}
