using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
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

        public IEnumerable<vw_RealEstateProperties> GetRealEstatePropertyCompaigns()
        {
            return listingRepository.GetRealEstatePropertyCompaigns();
        }
    }
}
