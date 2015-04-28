using System.IO;
using System.Web;
using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;

    /// <summary>
    /// Item Image Mapper
    /// </summary>
    public static class ItemImageMapper
    {
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static ItemImage CreateFrom(this DomainModels.ItemImage source)
        {
// ReSharper disable SuggestUseVarKeywordEvident
            ItemImage itemImage = new ItemImage
// ReSharper restore SuggestUseVarKeywordEvident
            {
                ProductImageId = source.ProductImageId,
                ItemId = source.ItemId
            };

            if (string.IsNullOrEmpty(source.ImageURL))
            {
                return itemImage;
            }

            string imageUrl = HttpContext.Current.Server.MapPath("~/" + source.ImageURL);
            if (imageUrl != null && File.Exists(imageUrl))
            {
                itemImage.ImageUrlBytes = File.ReadAllBytes(imageUrl);
            }

            return itemImage;
        }

        /// <summary>
        /// Create From WebApi Model
        /// </summary>
        public static DomainModels.ItemImage CreateFrom(this ItemImage source)
        {
            return new DomainModels.ItemImage
            {
                ProductImageId = source.ProductImageId,
                ItemId = source.ItemId,
                FileSource = source.FileSource,
                FileName = source.FileName
            };
        }

    }
}