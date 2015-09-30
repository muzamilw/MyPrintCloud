using MPC.Common;
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

        public List<MPC.Models.DomainModels.Listing> GetRealEstateProperties()
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

        public MPC.Models.DomainModels.Listing GetListingByListingId(long listingId)
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

        public List<FieldVariable> GetVariablesListWithValues(long listingId, long itemId, long ContactID, long ContactCompanyID, long FlagID, long AddressID, out List<MPC.Models.Common.TemplateVariable> lstVariableAndValue, out List<MPC.Models.Common.TemplateVariable> lstGeneralVariables, out List<MPC.Models.Common.TemplateVariable> lstListingImages, out List<VariableSection> lstSectionsName)
        {
            return _ListingRepository.GetVariablesListWithValues(listingId, itemId, ContactID, ContactCompanyID, FlagID, AddressID, out lstVariableAndValue, out lstGeneralVariables, out lstListingImages, out lstSectionsName);
        }

        public string UpdateListingData(ListingProperty objProperty)
        {
            string dataError = string.Empty;
            bool dataProcessed = false;
           // objProperty.Listing.StoreCode == null
            if (objProperty.Office.StoreCode == null)
            {
                dataError = "Store code is missing";
                return dataError;
            }
            long iContactCompanyID = GetContactCompanyID(objProperty.Office.StoreCode, objProperty.Office.CompanyName);
            //int territoryId = GetDefaultTerritoryByContactCompanyID(objProperty.Listing.StoreCode);

            if (iContactCompanyID == 0)
            {
                dataError = "Invalid Store code [" + objProperty.Office.StoreCode + "]";
                return dataError;
            }
            else
            {
                objProperty.Listing.ContactCompanyID = Convert.ToString(iContactCompanyID);
            }

            MPC.Models.DomainModels.Listing listing = CheckListingForUpdate(objProperty.Listing.ListingID);
            //here stratsss
            if (listing != null) // update
            {
              dataProcessed = UpdateListingData(objProperty, listing);
            }
            else
            {
                dataProcessed = AddListingData(objProperty);
            }

            if (dataProcessed)
                dataError = "Data processed successfully.";
            else
                dataError = "Error occurred while processing data.";

            return dataError;
        }
        private long GetContactCompanyID(string sStoreCode,string CompanyName)
        {
            try
            {
                return _ListingRepository.GetContactCompanyID(sStoreCode, CompanyName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private MPC.Models.DomainModels.Listing CheckListingForUpdate(string clientListingID)
        {
            try
            {
                return _ListingRepository.CheckListingForUpdate(clientListingID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool UpdateListingData(ListingProperty objProperty, MPC.Models.DomainModels.Listing listing)
        {
            try
            {
                bool dataAdded = false;



                dataAdded = _ListingRepository.UpdateListingData(objProperty, listing);

                return dataAdded;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private bool AddListingData(ListingProperty objProperty)
        {
            try
            {
                bool dataAdded = false;



                dataAdded = _ListingRepository.AddListingData(objProperty);

                return dataAdded;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<MPC.Models.DomainModels.Listing> GetPropertiesByContactCompanyID(long CompanyID)
        {
            return _ListingRepository.GetPropertiesByContactCompanyID(CompanyID);
        }
        public List<MPC.Models.DomainModels.ListingImage> GetAllListingImages()
        {
            return _ListingRepository.GetAllListingImages();
        }
    }
}
