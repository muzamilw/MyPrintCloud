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
        #endregion
    }
}
