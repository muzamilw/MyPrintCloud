using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.VisualStyles;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;

namespace MPC.Implementation.MISServices
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository addressRepository;
        private readonly IScopeVariableRepository scopeVariableRepository;
        private Address Create(Address address)
        {
            //check to maintain default properties of address
            CheckAddressDefault(address);
            address.OrganisationId = addressRepository.OrganisationId;
            addressRepository.Add(address);
            addressRepository.SaveChanges();
            if (address.ScopeVariables != null)
            {
                foreach (ScopeVariable scopeVariable in address.ScopeVariables)
                {
                    scopeVariable.Id = address.AddressId;
                    scopeVariableRepository.Add(scopeVariable);
                }
                scopeVariableRepository.SaveChanges();
            }
            return address;
        }
        private Address Update(Address address)
        {
            //check to maintain default properties of address
            CheckAddressDefault(address);
            address.OrganisationId = addressRepository.OrganisationId;
            addressRepository.Update(address);
            if (address.ScopeVariables != null)
            {
                UpdateScopVariables(address);
            }
            addressRepository.SaveChanges();
            return address;
        }

        /// <summary>
        /// Update Scop Variables
        /// </summary>
        private void UpdateScopVariables(Address address)
        {
            IEnumerable<ScopeVariable> scopeVariables = scopeVariableRepository.GetContactVariableByContactId(address.AddressId, (int)FieldVariableScopeType.Address);
            foreach (ScopeVariable scopeVariable in address.ScopeVariables)
            {
                ScopeVariable scopeVariableDbItem = scopeVariables.FirstOrDefault(
                    scv => scv.ScopeVariableId == scopeVariable.ScopeVariableId);
                if (scopeVariableDbItem != null)
                {
                    scopeVariableDbItem.Value = scopeVariable.Value;
                }
            }
        }

        private void CheckAddressDefault(Address address)
        {
            IEnumerable<Address> addressesToUpdate;
            if (address.isDefaultTerrorityBilling == true)
            {
                addressesToUpdate = addressRepository.GetAll().Where(x => x.isDefaultTerrorityBilling == true && x.CompanyId == address.CompanyId && x.TerritoryId == address.TerritoryId);
                foreach (var updatingAddress in addressesToUpdate)
                {
                    updatingAddress.isDefaultTerrorityBilling = false;
                    updatingAddress.OrganisationId = addressRepository.OrganisationId;
                    addressRepository.Update(updatingAddress);
                }
            }
            if (address.isDefaultTerrorityShipping == true)
            {
                addressesToUpdate = addressRepository.GetAll().Where(x => x.isDefaultTerrorityShipping == true && x.CompanyId == address.CompanyId && x.TerritoryId == address.TerritoryId);
                foreach (var updatingAddress in addressesToUpdate)
                {
                    updatingAddress.isDefaultTerrorityShipping = false;
                    updatingAddress.OrganisationId = addressRepository.OrganisationId;
                    addressRepository.Update(updatingAddress);
                }
            }
            if (address.IsDefaultAddress == true)
            {
                addressesToUpdate = addressRepository.GetAll().Where(x => x.IsDefaultAddress == true && x.CompanyId == address.CompanyId);
                foreach (var updatingAddress in addressesToUpdate)
                {
                    updatingAddress.IsDefaultAddress = false;
                    updatingAddress.OrganisationId = addressRepository.OrganisationId;
                    addressRepository.Update(updatingAddress);
                }
            }
            if (address.IsDefaultShippingAddress == true)
            {
                addressesToUpdate = addressRepository.GetAll().Where(x => x.IsDefaultShippingAddress == true && x.CompanyId == address.CompanyId);
                foreach (var updatingAddress in addressesToUpdate)
                {
                    updatingAddress.IsDefaultShippingAddress = false;
                    updatingAddress.OrganisationId = addressRepository.OrganisationId;
                    addressRepository.Update(updatingAddress);
                }
            }
        }
        #region Constructor

        public AddressService(IAddressRepository addressRepository, IScopeVariableRepository scopeVariableRepository)
        {
            this.addressRepository = addressRepository;
            this.scopeVariableRepository = scopeVariableRepository;
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
            if (dbAddress != null && (dbAddress.CompanyContacts == null || !dbAddress.CompanyContacts.Any()))
            {
                dbAddress.isArchived = true;
                addressRepository.SaveChanges();
                return true;
            }
            return false;
        }
        public Address Save(Address address)
        {
            if (address.AddressId == 0)
            {
                Create(address);
            }
            else
            {
                Update(address);
            }
            var addressToBeReturn = addressRepository.GetAddressById(address.AddressId);
            return addressToBeReturn;
        }
    }
}
