
using Cares.Models.DomainModels;
using Cares.Models.RequestModels;
using Cares.Models.ResponseModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// City Service Interface
    /// </summary>
    public interface ICityService
    {
        /// <summary>
        /// Load City Base Data
        /// </summary>
        CityBaseDataResponse LoadCityBaseData();

        /// <summary>
        /// Search City
        /// </summary>
        CitySearchRequestResponse SearchCity(CitySearchRequest request);

        /// <summary>
        /// Delete City by id
        /// </summary>
        void DeleteCity(long cityId);

        /// <summary>
        /// Add /Update region
        /// </summary>
        City SaveCity(City city);

    }
}
