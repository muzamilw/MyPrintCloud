using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface IListingRepository : IBaseRepository<Listing, long>
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
        List<VariableSection> GetSectionsNameBySectionIDs(List<FieldVariable> fieldVariabes);
        List<FieldVariable> GetVariablesListWithValues(long listingId, long itemId, out List<MPC.Models.Common.TemplateVariable> lstVariableAndValue, out List<MPC.Models.Common.TemplateVariable> lstGeneralVariables, out List<string> lstListingImages, out List<VariableSection> lstSectionsName);
        string DynamicQueryToGetRecord(string CriteriaFieldName, string RefTableName, string KeyField, long keyValue);

    }
}
