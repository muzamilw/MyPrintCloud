﻿using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.WebStoreServices
{
    public interface IListingService
    {
        List<Listing> GetRealEstateProperties();

        string GetImageURLByListingId(long listingId);
    }
}
