using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;

    /// <summary>
    /// Item Vdp Price Mapper
    /// </summary>
    public static class ItemVideoMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ItemVideo CreateFrom(this DomainModels.ItemVideo source)
        {
            return new ItemVideo
            {
                VideoId = source.VideoId,
                ItemId = source.ItemId,
                VideoLink = source.VideoLink,
                Caption = source.Caption
            };
        }

        /// <summary>
        /// Crete From WebApi Model
        /// </summary>
        public static DomainModels.ItemVideo CreateFrom(this ItemVideo source)
        {
            return new DomainModels.ItemVideo
            {
                VideoId = source.VideoId,
                ItemId = source.ItemId,
                VideoLink = source.VideoLink,
                Caption = source.Caption
            };
        }

    }
}