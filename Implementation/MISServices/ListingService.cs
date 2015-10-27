using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MPC.Models.Common;

namespace MPC.Implementation.MISServices
{
    public class ListingService : IListingService
    {
        
        private readonly IListingRepository listingRepository;
        private readonly ISystemUserRepository _SystemUserRepository;

        public ListingService(IListingRepository listingRepository,ISystemUserRepository _SystemUserRepository)
        {
            this.listingRepository = listingRepository;
            this._SystemUserRepository = _SystemUserRepository;
        }

        public RealEstateListViewResponse GetRealEstatePropertyCompaigns(RealEstateRequestModel request)
        {
            return listingRepository.GetRealEstatePropertyCompaigns(request);
        }

        public string SaveListingData()
        {
            // Read the file as one string.
            System.IO.StreamReader myFile = new System.IO.StreamReader("E:\\FTP\\Zunu\\CHAMBERS-94368-20151014092023.xml");
            string myString = myFile.ReadToEnd();

            myFile.Close();
              ListingPropertyXML objResult = new ListingPropertyXML();
             ListingPropertyXML Result = new ListingPropertyXML();

            XmlSerializer serializer = new XmlSerializer(typeof(ListingPropertyXML));
            using (TextReader reader = new StringReader(myString))
            {
                Result = (ListingPropertyXML)serializer.Deserialize(reader);
            }

            return InsertListingData(Result);

        }

        public string InsertListingData(ListingPropertyXML objProperty)
        {
            long iContactCompanyID = 0;
            string dataError = string.Empty;
            bool dataProcessed = false;
            if(objProperty != null)
            {
                if (objProperty.MpcLoginEmail == null)
                {
                    dataError = "Sorry,Invalid User";
                    //  return dataError;
                    if (objProperty.StoreCode == null)
                    {
                        dataError = "Store code is missing";
                    }
                    return dataError;
                }
                else
                {
                    long GetOrganisationID = GetOrganisationIdByEmail(objProperty.MpcLoginEmail);
                    if (GetOrganisationID > 0)
                    {
                        iContactCompanyID = GetContactCompanyID(objProperty.StoreCode, GetOrganisationID);
                    }

                    //int territoryId = GetDefaultTerritoryByContactCompanyID(objProperty.Listing.StoreCode);

                    if (iContactCompanyID == 0)
                    {
                        dataError = "Invalid Store code [" + objProperty.StoreCode + "]";
                        return dataError;
                    }
                    else
                    {
                        objProperty.Listing.CompanyId = Convert.ToString(iContactCompanyID);
                    }

                    MPC.Models.DomainModels.Listing listing = CheckListingForUpdate(objProperty.Listing.ClientListingId);
                    //here stratsss
                    if (listing != null) // update
                    {
                        dataProcessed = UpdateListingData(objProperty, listing,GetOrganisationID);
                    }
                    else
                    {
                        dataProcessed = AddListingData(objProperty, GetOrganisationID);
                    }

                    if (dataProcessed)
                        dataError = "Data processed successfully.";
                    else
                        dataError = "Error occurred while processing data.";

                }
            }
            
            return dataError;
        }

        public long GetOrganisationIdByEmail(string SystemUserEmail)
        {
            return _SystemUserRepository.OrganisationThroughSystemUserEmail(SystemUserEmail);
        }

        private long GetContactCompanyID(string sStoreCode, long OrganistaionID)
        {
            try
            {
                return listingRepository.GetContactCompanyIDByStoreCode(sStoreCode, OrganistaionID);
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
                return listingRepository.CheckListingForUpdate(clientListingID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool UpdateListingData(ListingPropertyXML objProperty, MPC.Models.DomainModels.Listing listing,long OrganisationId)
        {
            try
            {
                bool dataAdded = false;



                dataAdded = listingRepository.UpdateListingXMLData(objProperty, listing,OrganisationId);

                return dataAdded;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool AddListingData(ListingPropertyXML objProperty,long OrganisationId)
        {
            try
            {
                bool dataAdded = false;



                dataAdded = listingRepository.AddListingDataXML(objProperty, OrganisationId);

                return dataAdded;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
