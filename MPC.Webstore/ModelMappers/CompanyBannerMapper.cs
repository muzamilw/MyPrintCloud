﻿using DomainModels = MPC.Models.DomainModels;
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
                Heading = source.Heading,
                ButtonURL = source.ButtonURL,
                ItemURL = source.ItemURL,
                ImageURL = source.ImageURL,
                Description = source.Description
            };
        }

        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.CompanyBanner CreateFrom(this ApiModels.CompanyBanner source)
        {
            return new DomainModels.CompanyBanner
            {
                Heading = source.Heading,
                ButtonURL = source.ButtonURL,
                ItemURL = source.ItemURL,
                ImageURL = source.ImageURL,
                Description = source.Description
            };
        }
     
        #endregion
       
    }
}