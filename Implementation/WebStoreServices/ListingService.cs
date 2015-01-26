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

        public List<FieldVariable> GeyFieldVariablesByItemID(long itemId)
        {
            return _ListingRepository.GeyFieldVariablesByItemID(itemId);
        }

        public Listing GetListingByListingId(long listingId)
        {
            return _ListingRepository.GetListingByListingId(listingId);
        }
        public List<ListingOFI> GetListingOFIsByListingId(long listingId)
        {
            return _ListingRepository.GetListingOFIsByListingId(listingId);
        }
        public List<ListingImage> GetListingImagesByListingId(long listingId)
        {
            return _ListingRepository.GetListingImagesByListingId(listingId);
        }
        public List<ListingFloorPlan> GetListingFloorPlansByListingId(long listingId)
        {
            return _ListingRepository.GetListingFloorPlansByListingId(listingId);
        }
        public List<ListingLink> GetListingLinksByListingId(long listingId)
        {
            return _ListingRepository.GetListingLinksByListingId(listingId);
        }
        public List<ListingAgent> GetListingAgentsByListingId(long listingId)
        {
            return _ListingRepository.GetListingAgentsByListingId(listingId);
        }
        public List<ListingConjunctionAgent> GetListingConjunctionalAgentsByListingId(long listingId)
        {
            return _ListingRepository.GetListingConjunctionalAgentsByListingId(listingId);
        }
        public List<ListingVendor> GetListingVendorsByListingId(long listingId)
        {
            return _ListingRepository.GetListingVendorsByListingId(listingId);
        }
        public List<VariableSection> GetSectionsNameBySectionIDs(List<FieldVariable> fieldVariabes)
        {
            return _ListingRepository.GetSectionsNameBySectionIDs(fieldVariabes);
        }

        public List<FieldVariable> GetVariablesListWithValues(long listingId, long itemId, out List<MPC.Models.Common.TemplateVariable> lstVariableAndValue, out List<MPC.Models.Common.TemplateVariable> lstGeneralVariables, out List<string> lstListingImages, out List<VariableSection> lstSectionsName)
        {
            return _ListingRepository.GetVariablesListWithValues(listingId, itemId, out lstVariableAndValue, out lstGeneralVariables, out lstListingImages, out lstSectionsName);
        }
    }
}
