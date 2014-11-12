using Cares.Models.ResponseModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Address Base Data Service Interface
    /// </summary>
    public interface IAddressBaseDataService
    {
        /// <summary>
        /// Load base data by Country Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AddressBaseDataResponse LoadDataByCountry(int id);
    }
}
