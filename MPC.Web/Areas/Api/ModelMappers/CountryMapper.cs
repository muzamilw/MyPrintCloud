using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;

    /// <summary>
    /// Country Mapper
    /// </summary>
    public static class CountryMapper
    {
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static Country CreateFrom(this DomainModels.Country source)
        {
            return new Country
            {
                CountryId = source.CountryId,
                CountryName = source.CountryName,
                CountryCode = source.CountryCode
            };
        }
        
    }
}