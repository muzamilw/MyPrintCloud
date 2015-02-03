using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;

namespace MPC.Implementation.MISServices
{
    public class AddressService: IAddressService
    {
        private readonly IAddressRepository addressRepository;
        #region Constructor

        public AddressService(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }
        #endregion
        /// <summary>
        /// Get addressId By Id
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public Address Get(long addressId)
        {
            if (addressId > 0)
            {
                var result = addressRepository.GetAddressByID(addressId);
                if (result != null)
                {
                    return result;
                }
                return null;
            }
            return null;
        }
        public bool Delete(long addressId)
        {
            var dbAddress = addressRepository.GetAddressByID(addressId);
            if (dbAddress != null && dbAddress.CompanyContacts.Count == 0)
            {
                addressRepository.Delete(dbAddress);
                addressRepository.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
