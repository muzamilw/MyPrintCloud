using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Implementation.WebStoreServices
{
    public class ListingService : IListingService
    {
        private readonly IListingRepository _ListingRepository;


        /// <summary>
        ///  Constructor
        /// </summary>
        public ListingService(IListingRepository ListingRepository)
        {
            this._ListingRepository = ListingRepository;
        }

        public List<Listing> GetRealEstateProperties()
        {
            return _ListingRepository.GetRealEstateProperties();
        }

        public string GetImageURLByListingId(long listingId)
        {
            return _ListingRepository.GetImageURLByListingId(listingId);
        }
    }
}
