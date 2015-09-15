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

        #region SaveListingRealEstateCompaign

        bool AddListingData(ListingProperty objProperty);
        void AddListingVendors(int newlyAddedListing, List<ListingVendors> list);
        void AddListingConjunctionalAgents(int newlyAddedListing, List<ListingConjunctionalAgents> list);
        void AddListingAgents(int newlyAddedListing, List<ListingAgents> lstAgents, int CompanyId);
        void AddListingLinks(int newlyAddedListing, List<ListingLinks> lstListingLinks);
        void AddListingFloorPlans(int newlyAddedListing, List<ListingFloorplans> lstFloorPlans);
        void AddListingCustomCopy(int newlyAddedListing, ListingCustomCopy listingCustomCopy);
        void AddListingOFIs(int newlyAddedListing, List<ListingOFIs> listingOFIs);
        void AddListingImages(int newlyAddedListing, List<ListingImages> listingImages, string contactCompanyId);

        #endregion

        #region UpdateListingRealEstateCompaign

        bool UpdateListingData(ListingProperty objProperty, MPC.Models.DomainModels.Listing listing);
        int UpdateListing(MPC.Models.DomainModels.Listing propertyListing, MPC.Models.DomainModels.Listing tblListing);
        void UpdateListingVendors(int updatedListingID, List<ListingVendors> list);
        void UpdateListingConjunctionalAgents(int updatedListingID, List<ListingConjunctionalAgents> list);
        void UpdateListingAgents(int updatedListingID, List<ListingAgents> lstAgents);
        void UpdateListingLinks(int updatedListingID, List<ListingLinks> list);
        void UpdateListingFloorPlans(int updatedListing, List<ListingFloorplans> listingFloorPlans);
        void UpdateListingOFIs(int updatedListingID, List<ListingOFIs> listingOFIs);
        void UpdateListingImages(int updatedListing, List<ListingImages> listingImages, string ContactCompanyID);
        void UpdateListingCustomCopy(int updatedListing, ListingCustomCopy listingCustomCopy);

        #endregion

        #region Staff Member

        void ProcessStaffMember(int newlyAddedAddress, List<ListingStaffMember> lstStaffMember, string contactCompanyId, int territoryId);
        void UpdateStaffMember(int newlyAddedAddress, CompanyContact contact, ListingStaffMember lstStaffMember, string contactCompanyId, int territoryId);
        void AddStaffMember(int newlyAddedAddress, ListingStaffMember lstStaffMembers, string contactCompanyId, int territoryId);
        void AddStaffMembers(int newlyAddedAddress, List<ListingStaffMember> lstStaffMembers, string contactCompanyId);

        #endregion

        #region ListingOffice(Address)

        int ProcessOffice(ListingOffice listingOffice, string contactCompanyId, int territoryId);
        int AddOffice(ListingOffice listingOffice, string contactCompanyId, int territoryId);
        int UpdateOffice(Address address, ListingOffice listingOffice, string contactCompanyId, int territoryId);

        #endregion

        #region CommonFunction

        bool IsValidEmail(string email);
        bool IsNewEmail(string email);
        bool DownloadImageLocally(string SourceURL, string DestinationBasePath);
        CompanyContact GetContactIDForListinAgent(string memberID);
        int GetDefaultTerritoryByContactCompanyID(string contactCompanyId);
        MPC.Models.DomainModels.Listing CheckListingForUpdate(string clientListingID);
        int GetContactCompanyID(string sStoreCode);

        #endregion


    }
}
