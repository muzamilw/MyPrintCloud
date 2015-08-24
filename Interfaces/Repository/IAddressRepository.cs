using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;

namespace MPC.Interfaces.Repository
{
    public interface IAddressRepository : IBaseRepository<Address, long>
    {
        
        List<State> GetAllStates();
        Country GetCountryByCountryID(long CountryID);
        List<Country> GetAllCountries();
        State GetStateByStateID(long StateID);
        List<State> GetCountryStates(long CountryId);
        bool AddressNameExist(Address address);
        List<Address> GetAdressesByContactID(long contactID);
        List<Address> GetAddressesListByContactCompanyID(long contactCompanyId);
        List<Address> GetAddressesByTerritoryID(Int64 TerritoryID);
        Models.ResponseModels.AddressResponse GetAddress(Models.RequestModels.AddressRequestModel request);

        Address GetDefaultAddressByStoreID(Int64 StoreID);
        IEnumerable<Address> GetAllDefaultAddressByStoreID(Int64 StoreID);

        IEnumerable<Address> GetAllAddressByStoreId(Int64 storeId);

        Address GetAddressByID(long AddressID);

        List<Address> GetAddressByCompanyID(long companyID);
        Address GetAddressByAddressID(long AddressID);
        

        List<Address> GetBillingAndShippingAddresses(long TerritoryID);

        List<Address> GetContactCompanyAddressesList(long customerID);

        void UpdateAddress(Address billingAddress, Address deliveryAddress, long contactCompanyID);

             /// <summary>
        /// Get addresses list billing, shipping and pickup address
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        List<Address> GetContactCompanyAddressesList(long BillingAddressId, long ShippingAddressid, long PickUpAddressId);
        List<Address> GetsearchedAddress(long CompanyId, String searchtxt);
        bool UpdateBillingShippingAdd(Address Model);
        bool AddAddBillingShippingAdd(Address Address);
        void ResetDefaultShippingAddress(Address address);
        Address GetAddressById(long addressId);

        Address GetAddressByIdforXML(int addressId);
    }
}
