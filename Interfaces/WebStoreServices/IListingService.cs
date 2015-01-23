using MPC.Models.DomainModels;
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
        List<FieldVariable> GeyFieldVariablesByItemID(long itemId);
        Listing GetListingByListingId(long listingId);
        List<ListingOFI> GetListingOFIsByListingId(long listingId);
        List<ListingImage> GetListingImagesByListingId(long listingId);
        List<ListingFloorPlan> GetListingFloorPlansByListingId(long listingId);
        List<ListingLink> GetListingLinksByListingId(long listingId);
        List<ListingAgent> GetListingAgentsByListingId(long listingId);
        List<ListingConjunctionAgent> GetListingConjunctionalAgentsByListingId(long listingId);
        List<ListingVendor> GetListingVendorsByListingId(long listingId);

    }
}
