using MPC.Common;
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
        MPC.Models.DomainModels.Listing GetListingByListingID(int propertyId);
        long GetOrganisationIdByEmail(string SystemUserEmail);
        List<MPC.Models.DomainModels.ListingImage> GetAllListingImages();
        List<MPC.Models.DomainModels.Listing> GetRealEstateProperties();
        string GetImageURLByListingId(long listingId);
        List<FieldVariable> GeyFieldVariablesByItemID(long itemId);
        MPC.Models.DomainModels.Listing GetListingByListingId(long listingId);
        List<ListingOFI> GetListingOFIsByListingId(long listingId);
        List<ListingImage> GetListingImagesByListingId(long listingId);
        List<ListingFloorPlan> GetListingFloorPlansByListingId(long listingId);
        List<ListingLink> GetListingLinksByListingId(long listingId);
        List<ListingAgent> GetListingAgentsByListingId(long listingId);
        List<ListingConjunctionAgent> GetListingConjunctionalAgentsByListingId(long listingId);
        List<ListingVendor> GetListingVendorsByListingId(long listingId);
        List<VariableSection> GetSectionsNameBySectionIDs(List<FieldVariable> fieldVariabes);
        List<FieldVariable> GetVariablesListWithValues(long listingId, long itemId, long ContactID, long ContactCompanyID, long FlagID, long AddressID, out List<MPC.Models.Common.TemplateVariable> lstVariableAndValue, out List<MPC.Models.Common.TemplateVariable> lstGeneralVariables, out List<MPC.Models.Common.TemplateVariable> lstListingImages, out List<VariableSection> lstSectionsName);
        string UpdateListingData(ListingProperty objProperty);
        List<MPC.Models.DomainModels.Listing> GetPropertiesByContactCompanyID(long CompanyID);
    }
}
