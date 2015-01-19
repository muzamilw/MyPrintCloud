﻿using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;

namespace MPC.Interfaces.Repository
{
    public interface IAddressRepository : IBaseRepository<Address, long>
    {
        List<Address> GetAddressesByTerritoryID(Int64 TerritoryID);
        Models.ResponseModels.AddressResponse GetAddress(Models.RequestModels.AddressRequestModel request);

        Address GetDefaultAddressByStoreID(Int64 StoreID);
        IEnumerable<Address> GetAllDefaultAddressByStoreID(Int64 StoreID);

        Address GetAddressByID(long AddressID);

        List<Address> GetAddressByCompanyID(long companyID);

        List<Address> GetAdressesByContactID(long contactID);

        List<Address> GetBillingAndShippingAddresses(long TerritoryID);

        List<Address> GetContactCompanyAddressesList(long customerID);

    }
}
