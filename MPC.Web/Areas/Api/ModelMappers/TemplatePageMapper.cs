using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;

    /// <summary>
    /// Template Page Mapper
    /// </summary>
    public static class TemplatePageMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static TemplatePage CreateFrom(this DomainModels.TemplatePage source)
        {
            return new TemplatePage
            {
                ProductPageId = source.ProductPageId,
                ProductId = source.ProductId,
                Width = source.Width,
                Height = source.Height,
                PageName = source.PageName,
                PageNo = source.PageNo
            };
        }

        /// <summary>
        /// Crete From WebApi Model
        /// </summary>
        public static DomainModels.TemplatePage CreateFrom(this TemplatePage source)
        {
            return new DomainModels.TemplatePage
            {
                ProductPageId = source.ProductPageId,
                ProductId = source.ProductId,
                Width = source.Width,
                Height = source.Height,
                PageName = source.PageName,
                PageNo = source.PageNo
            };
        }

    }
}