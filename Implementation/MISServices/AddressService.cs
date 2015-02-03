using System.Collections.Generic;
using System.Linq;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;

namespace MPC.Implementation.MISServices
{
    public class AddressService: IAddressService
    {
        private readonly IAddressRepository addressRepository;
        private Address Create(Address address)
        {
            //check to maintain default properties of address
            CheckAddressDefault(address);
            addressRepository.Add(address);
            addressRepository.SaveChanges();
            return address;
        }
        private Address Update(Address address)
        {
            //check to maintain default properties of address
            CheckAddressDefault(address);
            addressRepository.Update(address);
            addressRepository.SaveChanges();
            return address;
        }
        private void CheckAddressDefault(Address address)
        {
            IEnumerable<Address> addressesToUpdate;
            if (address.isDefaultTerrorityBilling == true )
            {
                addressesToUpdate = addressRepository.GetAll().Where(x => x.isDefaultTerrorityBilling == true && x.CompanyId == address.CompanyId);
                foreach (var updatingAddress in addressesToUpdate)
                {
                    updatingAddress.isDefaultTerrorityBilling = false;
                    addressRepository.Update(updatingAddress);
                }
            }
            if (address.isDefaultTerrorityShipping == true)
            {
                addressesToUpdate = addressRepository.GetAll().Where(x => x.isDefaultTerrorityShipping == true && x.CompanyId == address.CompanyId);
                foreach (var updatingAddress in addressesToUpdate)
                {
                    updatingAddress.isDefaultTerrorityShipping = false;
                    addressRepository.Update(updatingAddress);
                }
            }
            if (address.IsDefaultAddress == true)
            {
                addressesToUpdate = addressRepository.GetAll().Where(x => x.IsDefaultAddress == true && x.CompanyId == address.CompanyId);
                foreach (var updatingAddress in addressesToUpdate)
                {
                    updatingAddress.IsDefaultAddress = false;
                    addressRepository.Update(updatingAddress);
                }
            }
        }
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
        public Address Save(Address address)
        {
            if (address.AddressId == 0)
            {
                return Create(address);
            }
            return Update(address);
        }
    }
}
