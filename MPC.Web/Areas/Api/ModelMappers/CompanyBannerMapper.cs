using System.IO;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;


namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Company Banner Mapper
    /// </summary>
    public static class CompanyBannerMapper
    {
        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static CompanyBanner CreateFrom(this DomainModels.CompanyBanner source)
        {
            byte[] bytes = null;
            if (source.ImageURL != null && File.Exists(source.ImageURL))
            {
                bytes = source.ImageURL != null ? File.ReadAllBytes(source.ImageURL) : null;
            }
            return new CompanyBanner
            {
                CompanyBannerId = source.CompanyBannerId,
                CompanySetId = source.CompanySetId,
                Heading = source.Heading,
                Description = source.Description,
                ImageURL = source.ImageURL,
                ButtonURL = source.ButtonURL,
                ItemURL = source.ItemURL,
                PageId = source.PageId,
                Image = bytes,
            };
        }

        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.CompanyBanner CreateFrom(this CompanyBanner source)
        {
            return new DomainModels.CompanyBanner
            {
                CompanyBannerId = source.CompanyBannerId,
                CompanySetId = source.CompanySetId,
                Heading = source.Heading,
                Description = source.Description,
                ImageURL = source.ImageURL,
                ButtonURL = source.ButtonURL,
                ItemURL = source.ItemURL,
            };
        }
        #endregion
    }
}