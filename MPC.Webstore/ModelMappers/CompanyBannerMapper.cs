using DomainModels = MPC.Models.DomainModels;
using ApiModels = MPC.Webstore.Models;

namespace MPC.Webstore.ModelMappers
{
    public static class CompanyBannerMapper
    {
        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ApiModels.CompanyBanner CreateFrom(this DomainModels.CompanyBanner source)
        {
            return new ApiModels.CompanyBanner
            {
                CompanyBannerId = source.CompanyBannerId,
                Heading = source.Heading,
                ButtonURL = source.ButtonURL,
                ItemURL = source.ItemURL,
                Description = source.Description,
                CompanySetId = source.CompanySetId,
                ImageURL = source.ImageURL,
            };
        }

        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.CompanyBanner CreateFrom(this ApiModels.CompanyBanner source)
        {
            return new DomainModels.CompanyBanner
            {
                CompanyBannerId = source.CompanyBannerId,
                Heading = source.Heading,
                ButtonURL = source.ButtonURL,
                ItemURL = source.ItemURL,
                Description = source.Description,
                CompanySetId = source.CompanySetId,
                ImageURL = source.ImageURL,
            };
        }

        #endregion

    }
}