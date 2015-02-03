using MPC.Models.DomainModels;
namespace MPC.Interfaces.MISServices
{
    public interface IAddressService
    {
        /// <summary>
        /// Get Address By Id
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        Address Get(long addressId);
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        bool Delete(long addressId);
    }
}
