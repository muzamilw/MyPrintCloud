﻿using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;

namespace MPC.Interfaces.Repository
{
    public interface IAddressRepository : IBaseRepository<Address, long>
    {
        List<Address> GetAddressesByTerritoryID(Int64 TerritoryID);
        Models.ResponseModels.AddressResponse GetAddress(Models.RequestModels.AddressRequestModel request);
    }
}
