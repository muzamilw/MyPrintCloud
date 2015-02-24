using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
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

            string savePath = directoryPath + "\\" + productCategory.ProductCategoryId + "_" + StringHelper.SimplifyString(productCategory.CategoryName) + "_Thumbnail.png";
            if ((!string.IsNullOrEmpty(productCategory.ThumbnailPath)) && File.Exists(HttpContext.Current.Server.MapPath("~/" + productCategory.ThumbnailPath)))
            {
                File.Delete(productCategory.ThumbnailPath);
            }
            File.WriteAllBytes(savePath, thumbNailFileBytes);
            int indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
            productCategory.ThumbnailPath = savePath.Substring(indexOf, savePath.Length - indexOf);

            savePath = directoryPath + "\\" + productCategory.ProductCategoryId + "_" + StringHelper.SimplifyString(productCategory.CategoryName) + "_Banner.png";
            if ((!string.IsNullOrEmpty(productCategory.ImagePath)) && File.Exists(HttpContext.Current.Server.MapPath("~/" + productCategory.ImagePath)))
            {
                File.Delete(productCategory.ImagePath);
            }
            File.WriteAllBytes(savePath, imageFileBytes);
            indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
            productCategory.ImagePath = savePath.Substring(indexOf, savePath.Length - indexOf);
        }


        //#region Private Methods
        private ProductCategory Create(ProductCategory productCategory)
        {
            productCategoryRepository.Add(productCategory);
            productCategoryRepository.SaveChanges();
            SaveProductCategoryThumbNailImage(productCategory);
            return productCategory;
        }

        private ProductCategory Update(ProductCategory productCategory)
        {
            productCategoryRepository.Update(productCategory);
            productCategoryRepository.SaveChanges();
            SaveProductCategoryThumbNailImage(productCategory);
            return productCategory;
        }
        //#endregion
        #endregion

        #region Constructor

        public CategoryService(IProductCategoryRepository productCategoryRepository)
        {
            this.productCategoryRepository = productCategoryRepository;
        }
        #endregion

        #region Public

        public List<ProductCategory> GetChildCategories(int categoryId)
        {
            var result = productCategoryRepository.GetChildCategories(categoryId);
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

        #endregion
    }
}
