using System.Linq;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;


namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Company Banner Set Mapper
    /// </summary>
    public static class CompanyBannerSetMapper
    {
        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static CompanyBannerSet CreateFrom(this DomainModels.CompanyBannerSet source)
        {
            return new CompanyBannerSet
            {
                CompanySetId = source.CompanySetId,
                SetName = source.SetName,
                CompanyBanners = source.CompanyBanners.Select(cb => cb.CreateFrom()).ToList()
            };
        }

        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.CompanyBannerSet CreateFrom(this CompanyBannerSet source)
        {
            return new DomainModels.CompanyBannerSet
            {
                CompanySetId = source.CompanySetId,
                SetName = source.SetName,
            };
        }
        #endregion
    }
}