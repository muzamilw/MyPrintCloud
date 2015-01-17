using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{


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

        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static CountryDropDown CreateFromDropDown(this DomainModels.Country source)
        {
            return new CountryDropDown
            {
                CountryId = source.CountryId,
                CountryName = source.CountryName,
            };
        }

    }
}