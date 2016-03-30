using Cares.Models.ResponseModels;

namespace Cares.Interfaces.IServices
{
    public interface IPhoneService
    {
        /// <summary>
        /// Get Phones By Worklocation ID
        /// </summary>
        PhonesSearchByWorkLocationIdResponse GetPhonesByWorklocationId(long workLocationId);
    }
}
