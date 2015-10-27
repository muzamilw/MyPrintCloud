using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Common;
using MPC.Models.ResponseModels;
using MPC.Models.RequestModels;
using MPC.Models.Common;

namespace MPC.Interfaces.Repository
{
    public interface IListingRepository : IBaseRepository<MPC.Models.DomainModels.Listing, long>
    {

        RealEstateListViewResponse GetRealEstatePropertyCompaigns(RealEstateRequestModel request);
        List<MPC.Models.DomainModels.ListingImage> GetAllListingImages();
        List<MPC.Models.DomainModels.ListingImage> GetListingImagesByListingID(int propertyId);
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
        string DynamicQueryToGetRecord(string CriteriaFieldName, string RefTableName, string KeyField, long keyValue);

        long GetContactCompanyID(string sStoreCode, string CompanyName, long OrganisationID);

        MPC.Models.DomainModels.Listing CheckListingForUpdate(string clientListingID);
        bool UpdateListingData(ListingProperty objProperty, MPC.Models.DomainModels.Listing listing);
        bool AddListingData(ListingProperty objProperty);
        List<MPC.Models.DomainModels.Listing> GetPropertiesByContactCompanyID(long CompanyID);

        long GetContactCompanyIDByStoreCode(string sStoreCode, long OrganisationID);

        bool UpdateListingXMLData(ListingPropertyXML objProperty, MPC.Models.DomainModels.Listing listing, long OrgId);


        bool AddListingDataXML(ListingPropertyXML objProperty, long Organisationid);
       
        

        

        

        

        





    }
}
