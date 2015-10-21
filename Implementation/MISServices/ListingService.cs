﻿using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Implementation.MISServices
{
    public class ListingService : IListingService
    {
        
        private readonly IListingRepository listingRepository;

        public ListingService(IListingRepository listingRepository)
        {
            this.listingRepository = listingRepository;
        }

        public RealEstateListViewResponse GetRealEstatePropertyCompaigns(RealEstateRequestModel request)
        {
            return listingRepository.GetRealEstatePropertyCompaigns(request);
        }
    }
}
