using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;

    /// <summary>
    /// Stock Item Mapper
    /// </summary>
    public static class ItemMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static Item CreateFrom(this DomainModels.Item source)
        {
            return new Item
            {
                ItemId = source.ItemId,
                ItemCode = source.ItemCode,
                ProductCode = source.ProductCode,
                ProductName = source.ProductName,
                ProductSpecification = source.ProductSpecification,
                GridImage = source.GridImage
            };
        }

        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static DomainModels.Item CreateFrom(this Item source)
        {
            return new DomainModels.Item
            {
                ItemId = source.ItemId,
                ItemCode = source.ItemCode,
                ProductCode = source.ProductCode,
                ProductName = source.ProductName,
                ProductSpecification = source.ProductSpecification
            };
        }
    }
}