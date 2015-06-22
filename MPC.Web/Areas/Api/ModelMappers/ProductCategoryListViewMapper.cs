using ApiModels = MPC.MIS.Areas.Api.Models;
using DomainResponseModel = MPC.Models.ResponseModels;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class ProductCategoryListViewMapper
    {
        public static ApiModels.ProductCategoryListViewModel ListViewModelCreateFrom(this DomainModels.ProductCategory source)
        {
            return new ApiModels.ProductCategoryListViewModel
            {
                ProductCategoryId = source.ProductCategoryId,
                CategoryName = source.CategoryName,
                ContentType = source.ContentType,
                LockedBy = source.LockedBy,
                ParentCategoryId = source.ParentCategoryId,
                DisplayOrder = source.DisplayOrder,
                IsArchived = source.isArchived == true
            };
        }
    }
}