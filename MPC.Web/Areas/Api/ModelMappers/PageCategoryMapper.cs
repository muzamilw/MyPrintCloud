using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;


namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    ///Page Category Mapper 
    /// </summary>
    public static class PageCategoryMapper
    {
        #region Public
        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static PageCategoryDropDown CreateFromDropDown(this DomainModels.PageCategory source)
        {
            return new PageCategoryDropDown
            {

                CategoryId = source.CategoryId,
                CategoryName = source.CategoryName
            };
        }

        #endregion

    }

}