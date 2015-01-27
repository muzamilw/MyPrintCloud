using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
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
        private readonly IProductCategoryFileTableViewRepository productCategoryFileTableViewRepository;
        
        //#region Private Methods
        private ProductCategory Create(ProductCategory productCategory)
        {
            productCategoryRepository.Add(productCategory);
            productCategoryRepository.SaveChanges();
            return productCategory;
        }

        private ProductCategory Update(ProductCategory productCategory)
        {
            productCategoryRepository.Update(productCategory);
            productCategoryRepository.SaveChanges();
            return productCategory;
        }
        //#endregion
        #endregion

        #region Constructor

        public CategoryService(IProductCategoryRepository productCategoryRepository, IProductCategoryFileTableViewRepository productCategoryFileTableViewRepository)
        {
            this.productCategoryRepository = productCategoryRepository;
            this.productCategoryFileTableViewRepository = productCategoryFileTableViewRepository;
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
