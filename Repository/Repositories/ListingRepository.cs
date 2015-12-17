using Microsoft.Practices.Unity;
using MPC.Common;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace MPC.Repository.Repositories
{
    class ListingRepository : BaseRepository<MPC.Models.DomainModels.Listing>, IListingRepository
    {
        int listingImageCount = 0;
        int listingAgentCount = 0;
        int listingOFIDCount = 0;
        int listingVendrosCount = 0;
        int listingLinkCount = 0;
        int listingFloorPlansCount = 0;
        int listingConAgentCount = 0;

        bool isChildList = false;
        string childType = string.Empty;
        bool listingOverflow = false;
        int propertyId = 0;
        static string cultureKey = ConfigurationManager.AppSettings["PropertyCulture"];
        IFormatProvider culture;
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ListingRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<MPC.Models.DomainModels.Listing> DbSet
        {
            get
            {
                return db.Listings;
            }
        }

        #endregion

        #region public

        public List<MPC.Models.DomainModels.Listing> GetRealEstateProperties()
        {
            List<MPC.Models.DomainModels.Listing> lstListings = db.Listings.ToList();

            return lstListings;
        }


        public string GetImageURLByListingId(long listingId)
        {
            string imageURL = string.Empty;
            ListingImage listingImage = db.ListingImages.Where(i => i.ListingId == listingId).FirstOrDefault();

            if (listingImage != null)
            {
                imageURL = listingImage.ImageURL;
            }

            return imageURL;
        }

        public List<FieldVariable> GeyFieldVariablesByItemID(long itemId)
        {

            var tempID = (from i in db.Items
                          where i.ItemId == itemId
                          select i.TemplateId).FirstOrDefault();

            long templateID = Convert.ToInt64(tempID);

            var IDs = (from v in db.TemplateVariables
                       where v.TemplateId == templateID
                       select v.VariableId).ToList();

            List<FieldVariable> lstFieldVariables = new List<FieldVariable>();

            foreach (long item in IDs)
            {
                FieldVariable objFieldVariable = (from FV in db.FieldVariables
                                                  where FV.VariableId == item
                                                  orderby FV.VariableSectionId
                                                  select FV).FirstOrDefault();

                lstFieldVariables.Add(objFieldVariable);
            }

            List<FieldVariable> finalList = (List<FieldVariable>)lstFieldVariables.OrderBy(item => item.VariableSectionId).ToList();

            return finalList;
        }

        public MPC.Models.DomainModels.Listing GetListingByListingId(long listingId)
        {
            return (from Listing in db.Listings
                    where Listing.ListingId == listingId
                    select Listing).FirstOrDefault();
        }

        public List<ListingOFI> GetListingOFIsByListingId(long listingId)
        {
            return (from listingOFID in db.ListingOFIs
                    where listingOFID.ListingId == listingId
                    select listingOFID).ToList();
        }
        public List<ListingImage> GetListingImagesByListingId(long listingId)
        {
            return (from listingImage in db.ListingImages
                    where listingImage.ListingId == listingId
                    select listingImage).ToList();
        }
        public List<ListingFloorPlan> GetListingFloorPlansByListingId(long listingId)
        {
            return (from listingOFID in db.ListingFloorPlans
                    where listingOFID.ListingId == listingId
                    select listingOFID).ToList();
        }
        public List<ListingLink> GetListingLinksByListingId(long listingId)
        {
            return (from listingOFID in db.ListingLinks
                    where listingOFID.ListingId == listingId
                    select listingOFID).ToList();
        }
        public List<ListingAgent> GetListingAgentsByListingId(long listingId)
        {
            return (from listingAgents in db.ListingAgents
                    where listingAgents.ListingId == listingId
                    select listingAgents).ToList();
        }
        public List<ListingConjunctionAgent> GetListingConjunctionalAgentsByListingId(long listingId)
        {
            return (from listingOFID in db.ListingConjunctionAgents
                    where listingOFID.ListingId == listingId
                    select listingOFID).ToList();
        }
        public List<ListingVendor> GetListingVendorsByListingId(long listingId)
        {
            return (from listingOFID in db.ListingVendors
                    where listingOFID.ListingId == listingId
                    select listingOFID).ToList();
        }
        public List<VariableSection> GetSectionsNameBySectionIDs(List<FieldVariable> fieldVariabes)
        {
            List<VariableSection> lstSectionsName = new List<VariableSection>();


            foreach (FieldVariable item in fieldVariabes)
            {
                VariableSection objVariableSection = (from VS in db.VariableSections
                                                      where VS.VariableSectionId == item.VariableSectionId
                                                      select VS).FirstOrDefault();

                lstSectionsName.Add(objVariableSection);
            }

            List<VariableSection> finalList = (List<VariableSection>)lstSectionsName.Distinct().ToList();

            return finalList;
        }
        #endregion

        #region GetRealEstateCompaigns

        public RealEstateListViewResponse GetRealEstatePropertyCompaigns(RealEstateRequestModel request)
        {
           // return db.vw_RealEstateProperties.ToList();

            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            bool isString = !string.IsNullOrEmpty(request.SearchString);


            List<vw_RealEstateProperties> RealEstates = db.vw_RealEstateProperties.Where(c => c.CompanyId == request.CompanyId).ToList();

            return new RealEstateListViewResponse { RealEstates = RealEstates, RowCount = RealEstates.Count() };

        }

        #endregion

        

        public List<FieldVariable> GetVariablesListWithValues(long listingId, long itemId, long ContactID, long ContactCompanyID, long FlagID, long AddressID, out List<MPC.Models.Common.TemplateVariable> lstVariableAndValue, out List<MPC.Models.Common.TemplateVariable> lstGeneralVariables, out List<MPC.Models.Common.TemplateVariable> lstListingImages, out List<VariableSection> lstSectionsName)
        {
            List<FieldVariable> lstFieldVariable = new List<FieldVariable>();
            lstGeneralVariables = new List<MPC.Models.Common.TemplateVariable>();
            lstVariableAndValue = new List<MPC.Models.Common.TemplateVariable>();
            lstListingImages = new List<MPC.Models.Common.TemplateVariable>();
            lstSectionsName = new List<VariableSection>();

            try
            {
                MPC.Models.DomainModels.Listing listing = GetListingByListingId(listingId);
                List<ListingOFI> listingOFIs = GetListingOFIsByListingId(listingId);
                List<ListingImage> listingImages = GetListingImagesByListingId(listingId);
                List<ListingFloorPlan> listingFloorPlans = GetListingFloorPlansByListingId(listingId);
                List<ListingLink> listingLinks = GetListingLinksByListingId(listingId);
                List<ListingAgent> listingAgents = GetListingAgentsByListingId(listingId);
                List<ListingConjunctionAgent> listingConjuctionAgents = GetListingConjunctionalAgentsByListingId(listingId);
                List<ListingVendor> listingVendors = GetListingVendorsByListingId(listingId);

                //create controls for smart form
                if (listing != null) //meaning; me got listing against the requested item
                {
                    //field variables
                    lstFieldVariable = GeyFieldVariablesByItemID(itemId);
                    //variable sections
                    lstSectionsName = GetSectionsNameBySectionIDs(lstFieldVariable); //Sections

                    //drop controls on page
                    string currentSectionName = string.Empty;
                    string currentPannelName = string.Empty;

                    foreach (var item in lstFieldVariable)
                    {
                        if (item.VariableType != 1)
                        {
                            //add controls to current section
                            long keyValue = 0;
                            string fieldValue = string.Empty;

                            switch (item.RefTableName)
                            {
                                case "Listing":
                                    fieldValue = Convert.ToString(listing.GetType().GetProperty(item.CriteriaFieldName).GetValue(listing, null));
                                    break;
                                case "ListingImage":

                                    if (listingImages.Count > listingImageCount)
                                    {
                                        fieldValue = Convert.ToString(listingImages[listingImageCount].GetType().GetProperty(item.CriteriaFieldName).GetValue(listingImages[listingImageCount], null));
                                    }
                                    else
                                    {
                                        listingImageCount++;
                                        listingOverflow = true;
                                        childType = "image";
                                        break;
                                    }

                                    isChildList = true;
                                    childType = "image";
                                    listingImageCount++;
                                    break;
                                case "ListingAgent":

                                    if (listingAgents.Count > listingAgentCount)
                                    {
                                        fieldValue = Convert.ToString(listingAgents[listingAgentCount].GetType().GetProperty(item.CriteriaFieldName).GetValue(listingAgents[listingAgentCount], null));
                                    }
                                    else
                                    {
                                        listingAgentCount++;
                                        listingOverflow = true;
                                        break;
                                    }

                                    isChildList = true;
                                    childType = "agent";
                                    listingAgentCount++;
                                    break;
                                case "ListingOFIs":

                                    if (listingOFIs.Count > listingOFIDCount)
                                    {
                                        fieldValue = Convert.ToString(listingOFIs[listingOFIDCount].GetType().GetProperty(item.CriteriaFieldName).GetValue(listingOFIs[listingOFIDCount], null));
                                    }
                                    else
                                    {
                                        listingOFIDCount++;
                                        listingOverflow = true;
                                        break;
                                    }

                                    isChildList = true;
                                    childType = "ofi";
                                    listingOFIDCount++;
                                    break;
                                case "ListingVendor":

                                    if (listingVendors.Count > listingVendrosCount)
                                    {
                                        fieldValue = Convert.ToString(listingVendors[listingVendrosCount].GetType().GetProperty(item.CriteriaFieldName).GetValue(listingVendors[listingVendrosCount], null));
                                    }
                                    else
                                    {
                                        listingVendrosCount++;
                                        listingOverflow = true;
                                        break;
                                    }

                                    isChildList = true;
                                    childType = "vendor";
                                    listingVendrosCount++;
                                    break;
                                case "ListingLink":

                                    if (listingLinks.Count > listingLinkCount)
                                    {
                                        fieldValue = Convert.ToString(listingLinks[listingLinkCount].GetType().GetProperty(item.CriteriaFieldName).GetValue(listingLinks[listingLinkCount], null));
                                    }
                                    else
                                    {
                                        listingLinkCount++;
                                        listingOverflow = true;
                                        break;
                                    }

                                    isChildList = true;
                                    childType = "link";
                                    listingLinkCount++;
                                    break;
                                case "ListingFloorPlan":

                                    if (listingFloorPlans.Count > listingFloorPlansCount)
                                    {
                                        fieldValue = Convert.ToString(listingFloorPlans[listingFloorPlansCount].GetType().GetProperty(item.CriteriaFieldName).GetValue(listingFloorPlans[listingFloorPlansCount], null));
                                    }
                                    else
                                    {
                                        listingFloorPlansCount++;
                                        listingOverflow = true;
                                        break;
                                    }

                                    isChildList = true;
                                    childType = "floor";
                                    listingFloorPlansCount++;
                                    break;
                                case "ListingConjunctionAgent":

                                    if (listingConjuctionAgents.Count > listingConAgentCount)
                                    {
                                        fieldValue = Convert.ToString(listingConjuctionAgents[listingConAgentCount].GetType().GetProperty(item.CriteriaFieldName).GetValue(listingConjuctionAgents[listingConAgentCount], null));
                                    }
                                    else
                                    {
                                        listingConAgentCount++;
                                        listingOverflow = true;
                                        break;
                                    }

                                    isChildList = true;
                                    childType = "conAgent";
                                    listingConAgentCount++;
                                    break;
                                case "CompanyContact":
                                    keyValue = ContactID;//SessionParameters.CustomerContact.ContactID;
                                    fieldValue = DynamicQueryToGetRecord(item.CriteriaFieldName, item.RefTableName, item.KeyField, keyValue);
                                    break;
                                case "Company":
                                    keyValue = ContactCompanyID;//UserCookieManager.StoreId;
                                    fieldValue = DynamicQueryToGetRecord(item.CriteriaFieldName, item.RefTableName, item.KeyField, keyValue);
                                    break;
                                case "Address":
                                    keyValue = AddressID;//SessionParameters.CustomerContact.AddressID;
                                    fieldValue = DynamicQueryToGetRecord(item.CriteriaFieldName, item.RefTableName, item.KeyField, keyValue);
                                    break;
                                default:
                                    break;
                            }

                            if (!listingOverflow)
                            {
                                if (item.VariableType == 3) //Image
                                {
                                    //CreateImage2(item.CriteriaFieldName, fieldValue); //create image
                                    //BindImages(currentPannelName);
                                    MPC.Models.Common.TemplateVariable tVar = new MPC.Models.Common.TemplateVariable(item.VariableName, fieldValue);
                                    lstListingImages.Add(tVar);
                                }
                                else //TextBox
                                {
                                    //CreateLable(item.CriteriaFieldName, item.VariableName, currentPannelName); //create label
                                    //string txtFielName = item.VariableName.Replace(" ", "");
                                    //txtFielName.Trim();
                                    //CreateTextBox(txtFielName, fieldValue, currentPannelName); //create textox

                                    MPC.Models.Common.TemplateVariable tVar = new MPC.Models.Common.TemplateVariable(item.VariableName, fieldValue);

                                    lstVariableAndValue.Add(tVar);
                                }
                            }
                            else
                            {
                                listingOverflow = false;
                            }
                        }
                        else //General Variable
                        {
                            long keyValue = 0;
                            string fieldValue = string.Empty;

                            switch (item.RefTableName)
                            {
                                case "CompanyContact":
                                    keyValue = ContactID;//SessionParameters.CustomerContact.ContactID;
                                    fieldValue = DynamicQueryToGetRecord(item.CriteriaFieldName, item.RefTableName, item.KeyField, keyValue);
                                    break;
                                case "Company":
                                    keyValue = ContactCompanyID;//SessionParameters.ContactCompany.ContactCompanyID;
                                    fieldValue = DynamicQueryToGetRecord(item.CriteriaFieldName, item.RefTableName, item.KeyField, keyValue);
                                    break;
                                case "Address":
                                    keyValue = AddressID;//SessionParameters.CustomerContact.AddressID;
                                    fieldValue = DynamicQueryToGetRecord(item.CriteriaFieldName, item.RefTableName, item.KeyField, keyValue);
                                    break;
                                case "SectionFlag":
                                    
                                    keyValue = FlagID;// SessionParameters.ContactCompany.FlagID;
                                    fieldValue = DynamicQueryToGetRecord(item.CriteriaFieldName, item.RefTableName, item.KeyField, keyValue);
                                    break;
                                case "tbl_ContactDepartments":
                                    //keyValue = Convert.ToInt32(SessionParameters.CustomerContact.DepartmentID);
                                    //fieldValue = DynamicQueryToGetRecord(item.CriteriaFieldName, item.RefTableName, item.KeyField, keyValue);
                                    break;
                                default:
                                    break;
                            }

                            MPC.Models.Common.TemplateVariable tVar = new MPC.Models.Common.TemplateVariable(item.VariableTag, fieldValue);

                            lstGeneralVariables.Add(tVar);
                        }
                    }

                }

                return lstFieldVariable;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string DynamicQueryToGetRecord(string feildname, string tblname, string keyName, long keyValue)
        {

            string oResult = null;
            System.Data.Entity.Infrastructure.DbRawSqlQuery<string> result = db.Database.SqlQuery<string>("select top 1 cast(" + feildname + " as varchar(1000)) from " + tblname + " where " + keyName + "= " + keyValue + "", "");
            oResult = result.FirstOrDefault();
            return oResult;

        }
        public long GetContactCompanyID(string sStoreCode,string CompanyName,long OrganisationID)
        {
            long iCompanyId = 0;
            var comp = db.Companies.Where(c => c.WebAccessCode == sStoreCode && c.OrganisationId == OrganisationID).FirstOrDefault();
            if (comp != null)
                iCompanyId = comp.CompanyId;
            return iCompanyId;
        }
        public MPC.Models.DomainModels.Listing CheckListingForUpdate(string clientListingID)
        {
            try
            {
                //MPC.Models.DomainModels.Listing listing;
                //listing = (from l in db.Listings
                //               where l.ClientListingId == clientListingID
                //               select l).FirstOrDefault();

                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                return db.Listings.Where(c => c.ClientListingId == clientListingID).FirstOrDefault();
                //return listing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateListingData(ListingProperty objProperty, MPC.Models.DomainModels.Listing listing)
        {

            try
            {
                bool dataAdded = false;
                if (cultureKey == null) //not defined in web.config
                {
                    culture = new System.Globalization.CultureInfo("en-AU", true); // AU is default
                }
                else
                {
                    culture = new System.Globalization.CultureInfo(cultureKey, true);
                }

                long territoryId = GetDefaultTerritoryByContactCompanyID(objProperty.Listing.ContactCompanyID);

                long officeId = ProcessOffice(objProperty.Office, objProperty.Listing.ContactCompanyID, territoryId);

                ProcessStaffMember(officeId, objProperty.StaffMember, objProperty.Listing.ContactCompanyID, territoryId);

                long updatedListingID = UpdateListing(objProperty.Listing, listing);
                UpdateListingCustomCopy(updatedListingID, objProperty.Listing.CustomCopy);
                UpdateListingImages(updatedListingID, objProperty.ListingImages, objProperty.Listing.ContactCompanyID);
                UpdateListingOFIs(updatedListingID, objProperty.ListingOFIs);
                UpdateListingFloorPlans(updatedListingID, objProperty.ListingFloorplans);
                UpdateListingLinks(updatedListingID, objProperty.ListingLinks);
                UpdateListingAgents(updatedListingID, objProperty.ListingAgents);
                UpdateListingConjunctionalAgents(updatedListingID, objProperty.ListingConjunctionalAgents);
                UpdateListingVendors(updatedListingID, objProperty.ListingVendors);

                dataAdded = true;
                return dataAdded;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool UpdateListingXMLData(ListingPropertyXML objProperty, MPC.Models.DomainModels.Listing listing,long OrgId)
        {

            try
            {
                bool dataAdded = false;
                if (cultureKey == null) //not defined in web.config
                {
                    culture = new System.Globalization.CultureInfo("en-AU", true); // AU is default
                }
                else
                {
                    culture = new System.Globalization.CultureInfo(cultureKey, true);
                }

                long territoryId = GetDefaultTerritoryByContactCompanyID(objProperty.Listing.CompanyId);

                long officeId = ProcessOfficeXML(objProperty.Listing.Office, objProperty.Listing.CompanyId, territoryId,OrgId);

               // ProcessStaffMemberXML(officeId, objProperty.Listing.ListingAgents, objProperty.Listing.CompanyId, territoryId,OrgId);

                long updatedListingID = UpdateListingXML(objProperty.Listing, listing);
                ProcessStaffMemberXML(officeId, objProperty.Listing.ListingAgents, objProperty.Listing.CompanyId, territoryId, OrgId, updatedListingID);
               
                if(objProperty.Listing.ListingImages != null)
                    UpdateListingImagesXML(updatedListingID, objProperty.Listing.ListingImages.image, objProperty.Listing.CompanyId);
                if(objProperty.Listing.ListingFloorplans != null)
                {
                    UpdateListingFloorPlansXML(updatedListingID, objProperty.Listing.ListingFloorplans.floorplans);
                }
                
                
                dataAdded = true;
                return dataAdded;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool AddListingData(ListingProperty objProperty)
        {
            bool dataAdded = false;
            try
            {
                long territoryId = GetDefaultTerritoryByContactCompanyID(objProperty.Listing.ContactCompanyID);

                long newlyAddedAddress = ProcessOffice(objProperty.Office, objProperty.Listing.ContactCompanyID, territoryId);
                ProcessStaffMember(newlyAddedAddress, objProperty.StaffMember, objProperty.Listing.ContactCompanyID, territoryId);

                long newlyAddedListing = AddListing(objProperty.Listing); //listing added
                AddListingCustomCopy(newlyAddedListing, objProperty.Listing.CustomCopy);
                AddListingImages(newlyAddedListing, objProperty.ListingImages, objProperty.Listing.ContactCompanyID);
                AddListingOFIs(newlyAddedListing, objProperty.ListingOFIs);
                AddListingFloorPlans(newlyAddedListing, objProperty.ListingFloorplans);
                AddListingLinks(newlyAddedListing, objProperty.ListingLinks); // tbl_ListingLink is not adding in database
                AddListingAgents(newlyAddedListing, objProperty.ListingAgents);
                AddListingConjunctionalAgents(newlyAddedListing, objProperty.ListingConjunctionalAgents);
                AddListingVendors(newlyAddedListing, objProperty.ListingVendors);

                dataAdded = true;
                return dataAdded;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private long AddListing(MPC.Common.Listing listing)
        {
            long newlyAddedListing = 0;
            try
            {
                string strForParse = string.Empty;
                if (cultureKey == null) //not defined in web.config
                {
                    culture = new System.Globalization.CultureInfo("en-AU", true); // AU is default
                }
                else
                {
                    culture = new System.Globalization.CultureInfo(cultureKey, true);
                }

                MPC.Models.DomainModels.Listing tbl_listing = new MPC.Models.DomainModels.Listing();
                    tbl_listing.ClientListingId = listing.ListingID;
                    tbl_listing.WebID = listing.WebID;
                    tbl_listing.WebLink = listing.WebLink;
                    tbl_listing.AddressDisplay = listing.AddressDisplay;
                    tbl_listing.StreetAddress = listing.StreetAddress;
                    tbl_listing.LevelNumber = (String.IsNullOrEmpty(listing.LevelNum)) ? 0 : Convert.ToInt32(listing.LevelNum);
                    tbl_listing.LotNumber = (String.IsNullOrEmpty(listing.LotNum)) ? 0 : Convert.ToInt32(listing.LotNum);
                    tbl_listing.UnitNumber = (String.IsNullOrEmpty(listing.UnitNum)) ? 0 : Convert.ToInt32(listing.UnitNum);
                    tbl_listing.StreetNumber = (String.IsNullOrEmpty(listing.StreetNum)) ? 0 : Convert.ToInt32(listing.StreetNum);
                    tbl_listing.Street = listing.Street;
                    tbl_listing.Suburb = listing.Suburb;
                    tbl_listing.State = listing.State;
                    tbl_listing.PostCode = listing.Postcode;
                    tbl_listing.PropertyName = listing.PropertyName;
                    tbl_listing.PropertyType = listing.PropertyType;
                    tbl_listing.PropertyCategory = listing.PropertyCategory;

                    if (!String.IsNullOrEmpty(listing.ListingDate))
                        //tbl_listing.ListingDate = Convert.ToDateTime(listing.ListingDate, new System.Globalization.CultureInfo("en-AU"));
                        tbl_listing.ListingDate = DateTime.Parse(listing.ListingDate, culture, System.Globalization.DateTimeStyles.AssumeLocal);

                    if (!String.IsNullOrEmpty(listing.ListingExpiryDate))
                        //tbl_listing.ListingExpiryDate = Convert.ToDateTime(listing.ListingExpiryDate, new System.Globalization.CultureInfo("en-AU"));
                        tbl_listing.ListingExpiryDate = DateTime.Parse(listing.ListingExpiryDate, culture, System.Globalization.DateTimeStyles.AssumeLocal);

                    tbl_listing.ListingStatus = listing.ListingStatus;
                    tbl_listing.ListingMethod = listing.ListingMethod;
                    tbl_listing.ListingAuthority = listing.ListingAuthority;
                    tbl_listing.InspectionTypye = listing.InspectionType;

                    if (!String.IsNullOrEmpty(listing.AuctionDate))
                        //tbl_listing.AuctionDate = Convert.ToDateTime(listing.AuctionDate, new System.Globalization.CultureInfo("en-AU"));
                        tbl_listing.AuctionDate = DateTime.Parse(listing.AuctionDate, culture, System.Globalization.DateTimeStyles.AssumeLocal);

                    tbl_listing.AutionVenue = listing.AuctionVenue;

                    if (!String.IsNullOrEmpty(listing.EOIClosingDate))
                        //tbl_listing.EOIClosingDate = Convert.ToDateTime(listing.EOIClosingDate, new System.Globalization.CultureInfo("en-AU"));
                        tbl_listing.EOIClosingDate = DateTime.Parse(listing.EOIClosingDate, culture, System.Globalization.DateTimeStyles.AssumeLocal);

                    if (listing.DisplayPrice != null)
                    {
                        strForParse = listing.DisplayPrice;
                        string result = Regex.Replace(strForParse, @"[^\d]", "");
                        if (!result.Equals(string.Empty))
                            tbl_listing.DisplayPrice = Convert.ToDouble(result);
                    }

                    if (listing.SearchPrice != null)
                    {
                        strForParse = listing.SearchPrice;
                        string result = Regex.Replace(strForParse, @"[^\d]", "");
                        if (!result.Equals(string.Empty))
                            tbl_listing.SearchPrice = Convert.ToDouble(result);
                    }

                    tbl_listing.RendPeriod = (String.IsNullOrEmpty(listing.RentPeriod)) ? 0 : Convert.ToInt32(listing.RentPeriod);

                    if (!String.IsNullOrEmpty(listing.AvailableDate))
                        tbl_listing.AvailableDate = Convert.ToDateTime(listing.AvailableDate, new System.Globalization.CultureInfo("en-AU"));
                        
                        
                    if (!String.IsNullOrEmpty(listing.SoldDate))
                        //tbl_listing.SoldDate = Convert.ToDateTime(listing.SoldDate, new System.Globalization.CultureInfo("en-AU"));
                        tbl_listing.SoldDate = DateTime.Parse(listing.SoldDate, culture, System.Globalization.DateTimeStyles.AssumeLocal);

                    if (listing.SoldPrice != null)
                    {
                        strForParse = listing.SoldPrice;
                        string result = Regex.Replace(strForParse, @"[^\d]", "");
                        if (!result.Equals(string.Empty))
                            tbl_listing.SoldPrice = Convert.ToDouble(result);
                    }

                    string confid = listing.SoldPriceConfidential;
                    if (confid.Equals("Yes"))
                    {
                        tbl_listing.IsSoldPriceConfidential = true;
                    }
                    else if (confid.Equals("No"))
                    {
                        tbl_listing.IsSoldPriceConfidential = false;
                    }

                    tbl_listing.MainHeadLine = listing.MainHeadline;
                    tbl_listing.MainDescription = listing.MainDescription;
                    tbl_listing.BedRooms = (String.IsNullOrEmpty(listing.BedRooms)) ? 0 : Convert.ToInt32(listing.BedRooms);
                    tbl_listing.BathRooms = (String.IsNullOrEmpty(listing.BathRooms)) ? 0 : Convert.ToInt32(listing.BathRooms);
                    tbl_listing.LoungeRooms = (String.IsNullOrEmpty(listing.LoungeRooms)) ? 0 : Convert.ToInt32(listing.LoungeRooms);
                    tbl_listing.Toilets = (String.IsNullOrEmpty(listing.Toilets)) ? 0 : Convert.ToInt32(listing.Toilets);
                    tbl_listing.Studies = (String.IsNullOrEmpty(listing.Studies)) ? 0 : Convert.ToInt32(listing.Studies);
                    tbl_listing.Pools = (String.IsNullOrEmpty(listing.Pools)) ? 0 : Convert.ToInt32(listing.Pools);
                    tbl_listing.Garages = (String.IsNullOrEmpty(listing.Garages)) ? 0 : Convert.ToInt32(listing.Garages);
                    tbl_listing.Carports = (String.IsNullOrEmpty(listing.Carports)) ? 0 : Convert.ToInt32(listing.Carports);
                    tbl_listing.CarSpaces = (String.IsNullOrEmpty(listing.CarSpaces)) ? 0 : Convert.ToInt32(listing.CarSpaces);
                    tbl_listing.TotalParking = (String.IsNullOrEmpty(listing.TotalParking)) ? 0 : Convert.ToInt32(listing.TotalParking);

                    if (listing.LandArea != null)
                    {
                        strForParse = listing.LandArea;
                        string result = Regex.Replace(strForParse, @"[^\d]", "");
                        if (!result.Equals(string.Empty))
                            tbl_listing.LandArea = Convert.ToDouble(result);
                    }

                    tbl_listing.LandAreaUnit = listing.LandAreaUnit;
                    tbl_listing.BuildingAreaSqm = (String.IsNullOrEmpty(listing.BuildingAreaSqm)) ? 0 : Convert.ToInt32(listing.BuildingAreaSqm);
                    tbl_listing.ExternalAreaSqm = (String.IsNullOrEmpty(listing.ExternalAreaSqm)) ? 0 : Convert.ToInt32(listing.ExternalAreaSqm);
                    tbl_listing.FrontageM = (String.IsNullOrEmpty(listing.FrontageM)) ? 0 : Convert.ToInt32(listing.FrontageM);
                    tbl_listing.Aspect = listing.Aspect;
                    tbl_listing.YearBuilt = listing.YearBuilt;
                    tbl_listing.YearRenovated = listing.YearRenovated;
                    tbl_listing.Construction = listing.Construction;
                    tbl_listing.PropertyCondition = listing.PropertyCondition;

                    if (listing.EnergyRating != null)
                    {
                        strForParse = listing.EnergyRating;
                        string result = Regex.Replace(strForParse, @"[^\d]", "");
                        if (!result.Equals(string.Empty))
                            tbl_listing.EnergyRating = Convert.ToDouble(result);
                    }

                    tbl_listing.Features = listing.Features;

                    if (listing.LandTax != null)
                    {
                        strForParse = listing.LandTax;
                        string result = Regex.Replace(strForParse, @"[^\d]", "");
                        if (!result.Equals(string.Empty))
                            tbl_listing.LandTax = Convert.ToDouble(result);
                    }

                    if (listing.CounsilRates != null)
                    {
                        strForParse = listing.CounsilRates;
                        string result = Regex.Replace(strForParse, @"[^\d]", "");
                        if (!result.Equals(string.Empty))
                            tbl_listing.CounsilRates = Convert.ToDouble(result);
                    }

                    if (listing.StrataAdmin != null)
                    {
                        strForParse = listing.StrataAdmin;
                        string result = Regex.Replace(strForParse, @"[^\d]", "");
                        if (!result.Equals(string.Empty))
                            tbl_listing.StrataAdmin = Convert.ToDouble(result);
                    }

                    if (listing.StrataSinking != null)
                    {
                        strForParse = listing.StrataSinking;
                        string result = Regex.Replace(strForParse, @"[^\d]", "");
                        if (!result.Equals(string.Empty))
                            tbl_listing.StrataSinking = Convert.ToDouble(result);
                    }

                    if (listing.OtherOutgoings != null)
                    {
                        strForParse = listing.OtherOutgoings;
                        string result = Regex.Replace(strForParse, @"[^\d]", "");
                        if (!result.Equals(string.Empty))
                            tbl_listing.OtherOutgoings = Convert.ToDouble(result);
                    }

                    if (listing.TotalOutgoings != null)
                    {
                        strForParse = listing.TotalOutgoings;
                        string result = Regex.Replace(strForParse, @"[^\d]", "");
                        if (!result.Equals(string.Empty))
                            tbl_listing.TotalOutgoings = Convert.ToDouble(result);
                    }

                    tbl_listing.LegalDescription = listing.LegalDescription;
                    tbl_listing.LegalLot = listing.LegalLot;
                    tbl_listing.LegalDP = listing.LegalDP;
                    tbl_listing.LegalVol = listing.LegalVol;
                    tbl_listing.LegalFolio = listing.LegalFolio;
                    tbl_listing.Zoning = listing.Zoning;
                    tbl_listing.CompanyId = (String.IsNullOrEmpty(listing.ContactCompanyID)) ? 0 : Convert.ToInt32(listing.ContactCompanyID);

                    db.Listings.Add(tbl_listing);
                    db.SaveChanges();
                 //   if (db.SaveChanges() > 0)
                   // {
                        newlyAddedListing = tbl_listing.ListingId;
                  // }
                

                return newlyAddedListing;
            }
            catch (Exception)
            {
                newlyAddedListing = 0;
                return newlyAddedListing;
            }
        }
        private long UpdateListing(MPC.Common.Listing propertyListing, MPC.Models.DomainModels.Listing tblListing)
        {
            long updatedListing = 0;
            try
            {
                string strForParse = string.Empty;


                    var listing = db.Listings.Where(item => item.ClientListingId == tblListing.ClientListingId).FirstOrDefault();

                    if (listing != null)
                    {
                        listing.PropertyName = propertyListing.PropertyName;
                        listing.ClientListingId = propertyListing.ListingID;
                        listing.WebID = propertyListing.WebID;
                        listing.WebLink = propertyListing.WebLink;
                        listing.AddressDisplay = propertyListing.AddressDisplay;
                        listing.StreetAddress = propertyListing.StreetAddress;
                        listing.LevelNumber = (String.IsNullOrEmpty(propertyListing.LevelNum)) ? 0 : Convert.ToInt32(propertyListing.LevelNum);
                        listing.LotNumber = (String.IsNullOrEmpty(propertyListing.LotNum)) ? 0 : Convert.ToInt32(propertyListing.LotNum);
                        listing.UnitNumber = (String.IsNullOrEmpty(propertyListing.UnitNum)) ? 0 : Convert.ToInt32(propertyListing.UnitNum);
                        listing.StreetNumber = (String.IsNullOrEmpty(propertyListing.StreetNum)) ? 0 : Convert.ToInt32(propertyListing.StreetNum);
                        listing.Street = propertyListing.Street;
                        listing.Suburb = propertyListing.Suburb;
                        listing.State = propertyListing.State;
                        listing.PostCode = propertyListing.Postcode;
                        listing.PropertyName = propertyListing.PropertyName;
                        listing.PropertyType = propertyListing.PropertyType;
                        listing.PropertyCategory = propertyListing.PropertyCategory;

                        if (!String.IsNullOrEmpty(propertyListing.ListingDate))
                            //listing.ListingDate = Convert.ToDateTime(listing.ListingDate, new System.Globalization.CultureInfo("en-AU"));
                            listing.ListingDate = DateTime.Parse(propertyListing.ListingDate, culture, System.Globalization.DateTimeStyles.AssumeLocal);

                        if (!String.IsNullOrEmpty(propertyListing.ListingExpiryDate))
                            //listing.ListingExpiryDate = Convert.ToDateTime(listing.ListingExpiryDate, new System.Globalization.CultureInfo("en-AU"));
                            listing.ListingExpiryDate = DateTime.Parse(propertyListing.ListingExpiryDate, culture, System.Globalization.DateTimeStyles.AssumeLocal);

                        listing.ListingStatus = propertyListing.ListingStatus;
                        listing.ListingMethod = propertyListing.ListingMethod;
                        listing.ListingAuthority = propertyListing.ListingAuthority;
                        listing.InspectionTypye = propertyListing.InspectionType;

                        if (!String.IsNullOrEmpty(propertyListing.AuctionDate))
                            //listing.AuctionDate = Convert.ToDateTime(listing.AuctionDate, new System.Globalization.CultureInfo("en-AU"));
                            listing.AuctionDate = DateTime.Parse(propertyListing.AuctionDate, culture, System.Globalization.DateTimeStyles.AssumeLocal);

                        listing.AutionVenue = propertyListing.AuctionVenue;

                        if (!String.IsNullOrEmpty(propertyListing.EOIClosingDate))
                            //listing.EOIClosingDate = Convert.ToDateTime(listing.EOIClosingDate, new System.Globalization.CultureInfo("en-AU"));
                            listing.EOIClosingDate = DateTime.Parse(propertyListing.EOIClosingDate, culture, System.Globalization.DateTimeStyles.AssumeLocal);

                        if (propertyListing.DisplayPrice != null)
                        {
                            strForParse = propertyListing.DisplayPrice;
                            string result = Regex.Replace(strForParse, @"[^\d]", "");
                            if (!result.Equals(string.Empty))
                                listing.DisplayPrice = Convert.ToDouble(result);
                        }

                        if (propertyListing.SearchPrice != null)
                        {
                            strForParse = propertyListing.SearchPrice;
                            string result = Regex.Replace(strForParse, @"[^\d]", "");
                            if (!result.Equals(string.Empty))
                                listing.SearchPrice = Convert.ToDouble(result);
                        }

                        listing.RendPeriod = (String.IsNullOrEmpty(propertyListing.RentPeriod)) ? 0 : Convert.ToInt32(propertyListing.RentPeriod);

                        if (!String.IsNullOrEmpty(propertyListing.AvailableDate))
                            //listing.AvailableDate = Convert.ToDateTime(listing.AvailableDate, new System.Globalization.CultureInfo("en-AU"));
                            listing.AvailableDate = DateTime.Parse(propertyListing.AvailableDate);

                        if (!String.IsNullOrEmpty(propertyListing.SoldDate))
                            //listing.SoldDate = Convert.ToDateTime(listing.SoldDate, new System.Globalization.CultureInfo("en-AU"));
                            listing.SoldDate = DateTime.Parse(propertyListing.SoldDate, culture, System.Globalization.DateTimeStyles.AssumeLocal);

                        if (propertyListing.SoldPrice != null)
                        {
                            strForParse = propertyListing.SoldPrice;
                            string result = Regex.Replace(strForParse, @"[^\d]", "");
                            if (!result.Equals(string.Empty))
                                listing.SoldPrice = Convert.ToDouble(result);
                        }

                        string confid = propertyListing.SoldPriceConfidential;
                        if (confid.Equals("Yes"))
                        {
                            listing.IsSoldPriceConfidential = true;
                        }
                        else if (confid.Equals("No"))
                        {
                            listing.IsSoldPriceConfidential = false;
                        }

                        listing.MainHeadLine = propertyListing.MainHeadline;
                        listing.MainDescription = propertyListing.MainDescription;
                        listing.BedRooms = (String.IsNullOrEmpty(propertyListing.BedRooms)) ? 0 : Convert.ToInt32(propertyListing.BedRooms);
                        listing.BathRooms = (String.IsNullOrEmpty(propertyListing.BathRooms)) ? 0 : Convert.ToInt32(propertyListing.BathRooms);
                        listing.LoungeRooms = (String.IsNullOrEmpty(propertyListing.LoungeRooms)) ? 0 : Convert.ToInt32(propertyListing.LoungeRooms);
                        listing.Toilets = (String.IsNullOrEmpty(propertyListing.Toilets)) ? 0 : Convert.ToInt32(propertyListing.Toilets);
                        listing.Studies = (String.IsNullOrEmpty(propertyListing.Studies)) ? 0 : Convert.ToInt32(propertyListing.Studies);
                        listing.Pools = (String.IsNullOrEmpty(propertyListing.Pools)) ? 0 : Convert.ToInt32(propertyListing.Pools);
                        listing.Garages = (String.IsNullOrEmpty(propertyListing.Garages)) ? 0 : Convert.ToInt32(propertyListing.Garages);
                        listing.Carports = (String.IsNullOrEmpty(propertyListing.Carports)) ? 0 : Convert.ToInt32(propertyListing.Carports);
                        listing.CarSpaces = (String.IsNullOrEmpty(propertyListing.CarSpaces)) ? 0 : Convert.ToInt32(propertyListing.CarSpaces);
                        listing.TotalParking = (String.IsNullOrEmpty(propertyListing.TotalParking)) ? 0 : Convert.ToInt32(propertyListing.TotalParking);

                        if (propertyListing.LandArea != null)
                        {
                            strForParse = propertyListing.LandArea;
                            string result = Regex.Replace(strForParse, @"[^\d]", "");
                            if (!result.Equals(string.Empty))
                                listing.LandArea = Convert.ToDouble(result);
                        }

                        listing.LandAreaUnit = propertyListing.LandAreaUnit;
                        listing.BuildingAreaSqm = (String.IsNullOrEmpty(propertyListing.BuildingAreaSqm)) ? 0 : Convert.ToInt32(propertyListing.BuildingAreaSqm);
                        listing.ExternalAreaSqm = (String.IsNullOrEmpty(propertyListing.ExternalAreaSqm)) ? 0 : Convert.ToInt32(propertyListing.ExternalAreaSqm);
                        listing.FrontageM = (String.IsNullOrEmpty(propertyListing.FrontageM)) ? 0 : Convert.ToInt32(propertyListing.FrontageM);
                        listing.Aspect = propertyListing.Aspect;
                        listing.YearBuilt = propertyListing.YearBuilt;
                        listing.YearRenovated = propertyListing.YearRenovated;
                        listing.Construction = propertyListing.Construction;
                        listing.PropertyCondition = propertyListing.PropertyCondition;
                        
                        if (propertyListing.EnergyRating != null)
                        {
                            strForParse = propertyListing.EnergyRating;
                            string result = Regex.Replace(strForParse, @"[^\d]", "");
                            if (!result.Equals(string.Empty))
                                listing.EnergyRating = Convert.ToDouble(result);
                        }
                          
                        listing.Features = propertyListing.Features;

                        if (propertyListing.LandTax != null)
                        {
                            strForParse = propertyListing.LandTax;
                            string result = Regex.Replace(strForParse, @"[^\d]", "");
                            if (!result.Equals(string.Empty))
                                listing.LandTax = Convert.ToDouble(result);
                        }

                        if (propertyListing.CounsilRates != null)
                        {
                            strForParse = propertyListing.CounsilRates;
                            string result = Regex.Replace(strForParse, @"[^\d]", "");
                            if (!result.Equals(string.Empty))
                                listing.CounsilRates = Convert.ToDouble(result);
                        }

                        if (propertyListing.StrataAdmin != null)
                        {
                            strForParse = propertyListing.StrataAdmin;
                            string result = Regex.Replace(strForParse, @"[^\d]", "");
                            if (!result.Equals(string.Empty))
                                listing.StrataAdmin = Convert.ToDouble(result);
                        }

                        if (propertyListing.StrataSinking != null)
                        {
                            strForParse = propertyListing.StrataSinking;
                            string result = Regex.Replace(strForParse, @"[^\d]", "");
                            if (!result.Equals(string.Empty))
                                listing.StrataSinking = Convert.ToDouble(result);
                        }

                        if (propertyListing.OtherOutgoings != null)
                        {
                            strForParse = propertyListing.OtherOutgoings;
                            string result = Regex.Replace(strForParse, @"[^\d]", "");
                            if (!result.Equals(string.Empty))
                                listing.OtherOutgoings = Convert.ToDouble(result);
                        }

                        if (propertyListing.TotalOutgoings != null)
                        {
                            strForParse = propertyListing.TotalOutgoings;
                            string result = Regex.Replace(strForParse, @"[^\d]", "");
                            if (!result.Equals(string.Empty))
                                listing.TotalOutgoings = Convert.ToDouble(result);
                        }

                        listing.LegalDescription = propertyListing.LegalDescription;
                        listing.LegalLot = propertyListing.LegalLot;
                        listing.LegalDP = propertyListing.LegalDP;
                        listing.LegalVol = propertyListing.LegalVol;
                        listing.LegalFolio = propertyListing.LegalFolio;
                        listing.Zoning = propertyListing.Zoning;
                        listing.CompanyId = (String.IsNullOrEmpty(propertyListing.ContactCompanyID)) ? 0 : Convert.ToInt64(propertyListing.ContactCompanyID);
                        db.Listings.Attach(listing);

                        db.Entry(listing).State = EntityState.Modified;
                        db.SaveChanges();
                     //   if (db.SaveChanges() > 0)
                      //  {
                            updatedListing = listing.ListingId;
                       // }
                        //awais
                    }
                

                return updatedListing;
            }
            catch (Exception)
            {
                updatedListing = 0;
                return updatedListing;
            }
        }

        private void UpdateListingVendors(long updatedListingID, List<ListingVendors> list)
        {
            try
            {

                    List<ListingVendor> listingVendors = db.ListingVendors.Where(i => i.ListingId == updatedListingID).ToList();

                    foreach (ListingVendor item in listingVendors)
                    {
                        if (item != null)
                        {
                            db.ListingVendors.Remove(item);
                        }
                    }

                    db.SaveChanges();

                    //old OFIs deleted now add new ones
                    AddListingVendors(updatedListingID, list);

            }
            catch (Exception)
            {

                throw;
            }
        }
        private void AddListingVendors(long newlyAddedListing, List<ListingVendors> list)
        {
            try
            {
                    foreach (ListingVendors item in list)
                    {
                        if (item != null)
                        {
                            ListingVendor vendor = new ListingVendor();
                            vendor.ListingId = newlyAddedListing;
                            vendor.FirstName = item.FirstName;
                            vendor.LastName = item.LastName;
                            vendor.Solutation = item.Salutation;
                            vendor.MailingSolutation = item.MailingSalutation;
                            vendor.Company = item.Company;
                            vendor.Email = item.Email;
                            vendor.Mobile = item.Mobile;
                            vendor.Phone = item.Phone;

                            db.ListingVendors.Add(vendor);
                        }
                    }

                    db.SaveChanges();
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void UpdateListingConjunctionalAgents(long updatedListingID, List<ListingConjunctionalAgents> list)
        {
            try
            {
                List<ListingConjunctionAgent> listingAgents = db.ListingConjunctionAgents.Where(i => i.ListingId == updatedListingID).ToList();

                foreach (ListingConjunctionAgent item in listingAgents)
                {
                    if (item != null)
                    {
                        db.ListingConjunctionAgents.Remove(item);
                    }
                }

                db.SaveChanges();

                    //old OFIs deleted now add new ones
                AddListingConjunctionalAgents(updatedListingID, list);

            }
            catch (Exception)
            {

                throw;
            }
        }
        private void AddListingConjunctionalAgents(long newlyAddedListing, List<ListingConjunctionalAgents> list)
        {
            try
            {
                    foreach (ListingConjunctionalAgents item in list)
                    {
                        if (item != null)
                        {
                            ListingConjunctionAgent agent = new ListingConjunctionAgent();
                            agent.ListingId = newlyAddedListing;
                            agent.FirstName = item.FirstName;
                            agent.LastName = item.LastName;
                            agent.Company = item.Company;
                            agent.Email = item.Email;
                            agent.Mobile = item.Mobile;
                            agent.Phone = item.Phone;

                            db.ListingConjunctionAgents.Add(agent);
                        }
                    }

                    db.SaveChanges();
                
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void UpdateListingAgents(long updatedListingID, List<ListingAgents> list)
        {
            try
            {
                List<ListingAgent> listingAgents = db.ListingAgents.Where(i => i.ListingId == updatedListingID).ToList();

                    foreach (ListingAgent item in listingAgents)
                    {
                        if (item != null)
                        {
                            db.ListingAgents.Remove(item);
                        }
                    }

                    db.SaveChanges();

                    //old Agents deleted now add new ones
                    AddListingAgents(updatedListingID, list);
             
            }
            catch (Exception)
            {

                throw;
            }
        }

        private CompanyContact GetContactIDForListinAgent(string memberID)
        {
            try
            {
                CompanyContact contact;

                contact = (from i in db.CompanyContacts
                               where i.HomePostCode == memberID
                               select i).FirstOrDefault();

                    return contact;
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void AddListingAgents(long newlyAddedListing, List<ListingAgents> lstAgents)
        {
            try
            {
                    foreach (ListingAgents item in lstAgents)
                    {
                        if (item != null)
                        {
                            CompanyContact contact = GetContactIDForListinAgent(item.MemberID);

                            if (contact == null)
                                continue;

                            ListingAgent agent = new ListingAgent();
                            agent.ListingId = newlyAddedListing;
                            agent.MemberId = contact.ContactId; //memberid is contactid from tbl_contacts
                            agent.AgentOrder = item.AgentOrder;
                            agent.Mobile = contact.Mobile;
                            agent.Name = contact.FirstName;
                            agent.Phone = contact.HomeTel1;
                            agent.Phone2 = contact.HomeTel2;

                            if (contact.ContactRoleId != null && contact.ContactRoleId == 1)
                            {
                                agent.Admin = true;
                            }
                            else
                            {
                                agent.Admin = false;
                            }

                            agent.UserRef = contact.quickWebsite;
                            db.ListingAgents.Add(agent);
                        }
                    }

                    db.SaveChanges();
               
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void UpdateListingLinks(long updatedListingID, List<ListingLinks> list)
        {
            try
            {
                List<ListingLink> listingLinks = db.ListingLinks.Where(i => i.ListingId == updatedListingID).ToList();

                foreach (ListingLink item in listingLinks)
                    {
                        if (item != null)
                        {
                            db.ListingLinks.Remove(item);
                        }
                    }

                    db.SaveChanges();

                    //old Links deleted now add new ones
                    AddListingLinks(updatedListingID, list);
                
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void AddListingLinks(long newlyAddedListing, List<ListingLinks> lstListingLinks)
        {
            try
            {
                    foreach (ListingLinks item in lstListingLinks)
                    {
                        if (item != null)
                        {
                            ListingLink link = new ListingLink();
                            link.ListingId = newlyAddedListing;
                            link.LinkType = item.LinkType;
                            link.LinkTitle = item.LinkTitle;
                            link.LinkURL = item.LinkURL;

                            db.ListingLinks.Add(link);
                        }
                    }

                    db.SaveChanges();
             
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void UpdateListingFloorPlans(int updatedListing, List<ListingFloorplans> listingFloorPlans)
        {
            try
            {
                    foreach (ListingFloorplans item in listingFloorPlans)
                    {
                        if (item != null)
                        {
                            var listingFloorPlan = db.ListingFloorPlans.Where(i => i.ClientFloorplanID == item.FloorplanID).FirstOrDefault();

                            if (listingFloorPlan != null) //update
                            {
                                listingFloorPlan.ListingId = updatedListing;
                                listingFloorPlan.ClientFloorplanID = item.FloorplanID;
                                listingFloorPlan.ImageURL = item.ImageURL;
                                listingFloorPlan.PDFURL = item.PDFURL;

                                if (!String.IsNullOrEmpty(item.LastMod))
                                    //floorPlan.LastMode = Convert.ToDateTime(item.LastMod, new System.Globalization.CultureInfo("en-AU"));
                                    listingFloorPlan.LastMode = DateTime.Parse(item.LastMod, culture, System.Globalization.DateTimeStyles.AssumeLocal);

                                db.SaveChanges();
                            }
                            else //add 
                            {
                                List<ListingFloorplans> lstFloorPlanToAdd = new List<ListingFloorplans>();
                                lstFloorPlanToAdd.Add(item);

                                AddListingFloorPlans(updatedListing, lstFloorPlanToAdd);
                            }
                        }
                    }
                
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void AddListingFloorPlans(long newlyAddedListing, List<ListingFloorplans> lstFloorPlans)
        {
            try
            {
                    foreach (ListingFloorplans item in lstFloorPlans)
                    {
                        if (item != null)
                        {
                            ListingFloorPlan floorPlan = new ListingFloorPlan();
                            floorPlan.ListingId = newlyAddedListing;
                            floorPlan.ClientFloorplanID = item.FloorplanID;
                            floorPlan.ImageURL = item.ImageURL;
                            floorPlan.PDFURL = item.PDFURL;

                            if (!String.IsNullOrEmpty(item.LastMod))
                                //floorPlan.LastMode = Convert.ToDateTime(item.LastMod, new System.Globalization.CultureInfo("en-AU"));
                                floorPlan.LastMode = DateTime.Parse(item.LastMod, culture, System.Globalization.DateTimeStyles.AssumeLocal);

                            db.ListingFloorPlans.Add(floorPlan);
                        }
                    }
                    db.SaveChanges();
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void UpdateListingFloorPlansXML(long updatedListing, List<FloorPlan> listingFloorPlans)
        {
            try
            {
                foreach (FloorPlan item in listingFloorPlans)
                {
                    if (item != null)
                    {
                        var listingFloorPlan = db.ListingFloorPlans.Where(i => i.ClientFloorplanID == item.FloorplanID).FirstOrDefault();

                        if (listingFloorPlan != null) //update
                        {
                            listingFloorPlan.ListingId = updatedListing;
                            listingFloorPlan.ClientFloorplanID = item.FloorplanID;
                            listingFloorPlan.ImageURL = item.ImageURL;
                            //listingFloorPlan.PDFURL = item.PDFURL;

                            if (!String.IsNullOrEmpty(item.LastMod))
                                //floorPlan.LastMode = Convert.ToDateTime(item.LastMod, new System.Globalization.CultureInfo("en-AU"));
                                listingFloorPlan.LastMode = DateTime.Parse(item.LastMod, culture, System.Globalization.DateTimeStyles.AssumeLocal);

                            db.SaveChanges();
                        }
                        else //add 
                        {
                            List<FloorPlan> lstFloorPlanToAdd = new List<FloorPlan>();
                            lstFloorPlanToAdd.Add(item);

                            AddListingFloorPlansXML(updatedListing, lstFloorPlanToAdd);
                        }
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void AddListingFloorPlansXML(long newlyAddedListing, List<FloorPlan> lstFloorPlans)
        {
            try
            {
                foreach (FloorPlan item in lstFloorPlans)
                {
                    if (item != null)
                    {
                        ListingFloorPlan floorPlan = new ListingFloorPlan();
                        floorPlan.ListingId = newlyAddedListing;
                        floorPlan.ClientFloorplanID = item.FloorplanID;
                        floorPlan.ImageURL = item.ImageURL;
                        //floorPlan.PDFURL = item.PDFURL;

                        if (!String.IsNullOrEmpty(item.LastMod))
                            //floorPlan.LastMode = Convert.ToDateTime(item.LastMod, new System.Globalization.CultureInfo("en-AU"));
                            floorPlan.LastMode = DateTime.Parse(item.LastMod, culture, System.Globalization.DateTimeStyles.AssumeLocal);

                        db.ListingFloorPlans.Add(floorPlan);
                    }
                }
                db.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void UpdateListingOFIs(long updatedListingID, List<ListingOFIs> listingOFIs)
        {
            try
            {
                List<ListingOFI> listingOFI = db.ListingOFIs.Where(i => i.ListingId == updatedListingID).ToList();

                foreach (ListingOFI item in listingOFI)
                {
                    if (item != null)
                    {
                        db.ListingOFIs.Remove(item);
                    }
                }

                    db.SaveChanges();

                    //old OFIs deleted now add new ones
                    AddListingOFIs(updatedListingID, listingOFIs);
                
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void AddListingOFIs(long newlyAddedListing, List<ListingOFIs> listingOFIs)
        {
            try
            {
                    foreach (ListingOFIs item in listingOFIs)
                    {
                        if (item != null)
                        {
                            ListingOFI tbl_listingOFID = new ListingOFI();
                            tbl_listingOFID.ListingId = newlyAddedListing;

                            if (!String.IsNullOrEmpty(item.StartTime))
                                //tbl_listingOFID.StartTime = Convert.ToDateTime(item.StartTime, new System.Globalization.CultureInfo("en-AU"));
                                tbl_listingOFID.StartTime = DateTime.Parse(item.StartTime, culture, System.Globalization.DateTimeStyles.AssumeLocal);

                            if (!String.IsNullOrEmpty(item.EndTime))
                                //tbl_listingOFID.EndTime = Convert.ToDateTime(item.EndTime, new System.Globalization.CultureInfo("en-AU"));
                                tbl_listingOFID.EndTime = DateTime.Parse(item.EndTime, culture, System.Globalization.DateTimeStyles.AssumeLocal);

                            db.ListingOFIs.Add(tbl_listingOFID);
                        }
                    }

                    db.SaveChanges();
                
            }
            catch (Exception)
            {
                throw;
            }
        }
        private string DownloadImageLocally(string SourceURL, string DestinationBasePath)
        {
           
            //Stream stream = null;
            //MemoryStream memStream = new MemoryStream();
            //try
            //{
            //    WebRequest req = WebRequest.Create(SourceURL);
            //    WebResponse response;

            //    try
            //    {
            //        response = req.GetResponse();
            //    }
                 
            //    catch (Exception)
            //    {
            //        //No file exists
            //        return false;
            //    }

            //    if (response != null)
            //    {
            //        stream = response.GetResponseStream();
                   
            //        byte[] buffer = new byte[2048];

            //        //Get Total Size
            //        int dataLength = (int)response.ContentLength;

            //        int bytesRead;
            //        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            //        {
            //            memStream.Write(buffer, 0, bytesRead);
            //        }
                   
            //     ///   FileInfo file = new System.IO.FileInfo(DestinationBasePath);
            //      // System.IO
            //        //file.Directory.Create(); // If the directory already exists, this method does nothing.
                   
                        
                        
            //           // System.IO.File.WriteAllBytes(DestinationBasePath, memStream.ToArray());

            //    // System.IO.File.WriteAllBytes(String.Format("{0}{1}", DestinationBasePath,), memStream.ToArray());
                    
            //    }
            //    else
            //        return false;

            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}
            //finally
            //{
            //    //Clean up
            //    if (stream != null)
            //        stream.Close();

            //    if (memStream != null)
            //        memStream.Close();
            //}
            //string imageUrl = SourceURL;
            //string saveLocation = DestinationBasePath;

            //byte[] imageBytes;
            //HttpWebRequest imageRequest = (HttpWebRequest)WebRequest.Create(imageUrl);
            //WebResponse imageResponse = imageRequest.GetResponse();

            //Stream responseStream = imageResponse.GetResponseStream();

            //using (BinaryReader br = new BinaryReader(responseStream))
            //{
            //    imageBytes = br.ReadBytes(500000);
            //    br.Close();
            //}
            //responseStream.Close();
            //imageResponse.Close();

            //FileStream fs = new FileStream("~/Content/Images", FileMode.Create);
            //BinaryWriter bw = new BinaryWriter(fs);
            //try
            //{
            //    bw.Write(imageBytes);
            //}
            //finally
            //{
            //    fs.Close();
            //    bw.Close();
            //}

            System.Drawing.Image image = null;
            
                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(SourceURL);
                webRequest.AllowWriteStreamBuffering = true;
                webRequest.Timeout = 30000;

                System.Net.WebResponse webResponse = webRequest.GetResponse();
                string filename = webResponse.ResponseUri.LocalPath;

                System.IO.Stream stream = webResponse.GetResponseStream();

                //  string fileName = webResponse.Headers["Content-Disposition"].Replace("attachment; filename=", String.Empty).Replace("\"", String.Empty);

                image = System.Drawing.Image.FromStream(stream);


                string rootPath = DestinationBasePath;
                string[] tokens = filename.Split(new[] { "/" }, StringSplitOptions.None);
                string file = System.IO.Path.Combine(rootPath, tokens[1]);
                image.Save(file);
                return tokens[1];
         
         
        }
        private void UpdateListingImages(long updatedListing, List<ListingImages> listingImages, string ContactCompanyID)
        {
            try
            {
                    foreach (ListingImages item in listingImages)
                    {
                       
                        if (item != null)
                        {
                            var listingImage = db.ListingImages.Where(i => i.ClientImageId == item.ImageID).FirstOrDefault();

                            if (listingImage != null) //update
                            {
                                //string drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Stores/" + ContactCompanyID.ToString() +"/"+ updatedListing + "/" + item.ImageID);
                                string drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Stores/" + ContactCompanyID.ToString() + "/" + updatedListing);
                                //first download image locally
                                if (!System.IO.Directory.Exists((drURL)))
                                    System.IO.Directory.CreateDirectory(drURL);
                              string imgName=  DownloadImageLocally(item.ImageURL, drURL);

                                listingImage.ListingId = updatedListing;
                                listingImage.ClientImageId = item.ImageID;
                                //listingImage.ImageURL = "/MPC_Content/Stores/" + ContactCompanyID + "/" + updatedListing + "/" + item.ImageID;
                                listingImage.ImageURL = "/MPC_Content/Stores/" + ContactCompanyID + "/" + updatedListing + "/" + imgName;
                                listingImage.ImageOrder = item.ImageOrder;

                                if (!String.IsNullOrEmpty(item.LastMod))
                                    //tbl_listingImage.LastMode = Convert.ToDateTime(item.LastMode, new System.Globalization.CultureInfo("en-AU"));
                                    listingImage.LastMode = DateTime.Parse(item.LastMod, culture, System.Globalization.DateTimeStyles.AssumeLocal);
                                db.ListingImages.Attach(listingImage);

                                db.Entry(listingImage).State = EntityState.Modified;

                                db.SaveChanges();
                            }
                            else //add 
                            {
                                List<ListingImages> lstImageToAdd = new List<ListingImages>();
                                lstImageToAdd.Add(item);

                                AddListingImages(updatedListing, lstImageToAdd, ContactCompanyID);
                            }
                        }
                        
                    }
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void UpdateListingImagesXML(long updatedListing, List<PropertyImage> listingImages, string ContactCompanyID)
        {
            try
            {
                foreach (PropertyImage item in listingImages)
                {

                    if (item != null)
                    {
                        var listingImage = db.ListingImages.Where(i => i.ClientImageId == item.ImageID).FirstOrDefault();

                        if (listingImage != null) //update
                        {
                            if(!string.IsNullOrEmpty(item.ImageURL))
                            {
                                //string drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Stores/" + ContactCompanyID.ToString() +"/"+ updatedListing + "/" + item.ImageID);
                                string drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Stores/" + ContactCompanyID.ToString() + "/" + updatedListing);
                                //first download image locally
                                if (!System.IO.Directory.Exists((drURL)))
                                    System.IO.Directory.CreateDirectory(drURL);
                                string imgName = DownloadImageLocallyXML(item.ImageURL, drURL);

                                listingImage.ListingId = updatedListing;
                                listingImage.ClientImageId = item.ImageID;
                                //listingImage.ImageURL = "/MPC_Content/Stores/" + ContactCompanyID + "/" + updatedListing + "/" + item.ImageID;
                                listingImage.ImageURL = "/MPC_Content/Stores/" + ContactCompanyID + "/" + updatedListing + "/" + imgName;
                                //listingImage.ImageOrder = item.ImageOrder;

                                if (!String.IsNullOrEmpty(item.LastMod))
                                    //tbl_listingImage.LastMode = Convert.ToDateTime(item.LastMode, new System.Globalization.CultureInfo("en-AU"));
                                    listingImage.LastMode = DateTime.Parse(item.LastMod, culture, System.Globalization.DateTimeStyles.AssumeLocal);
                                db.ListingImages.Attach(listingImage);

                                db.Entry(listingImage).State = EntityState.Modified;

                                db.SaveChanges();
                            }
                           
                        }
                        else //add 
                        {
                            List<PropertyImage> lstImageToAdd = new List<PropertyImage>();
                            lstImageToAdd.Add(item);

                            AddListingImagesXML(updatedListing, lstImageToAdd, ContactCompanyID);
                        }
                    }

                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        private void AddListingImages(long newlyAddedListing, List<ListingImages> listingImages, string contactCompanyId)
        {
            try
            {
                    foreach (ListingImages item in listingImages)
                    {
                        
                        if (item != null)
                        {
                          //  string destinationPath = HostingEnvironment.MapPath("~/StoredImages/RealEstateImages/" + contactCompanyId + "\\" + newlyAddedListing + "\\" + item.ImageID);
                            string drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Stores/" + contactCompanyId.ToString() + "/" + newlyAddedListing);

                            //first download image locally
                            if (!System.IO.Directory.Exists((drURL)))
                                System.IO.Directory.CreateDirectory(drURL);
                           string ImageName= DownloadImageLocally(item.ImageURL, drURL);

                            ListingImage tbl_listingImage = new ListingImage();
                            tbl_listingImage.ListingId = newlyAddedListing;
                            tbl_listingImage.ClientImageId = item.ImageID;
                            tbl_listingImage.ImageURL = "/MPC_Content/Stores/" + contactCompanyId + "/" + newlyAddedListing + "/" + ImageName;
                            tbl_listingImage.ImageOrder = item.ImageOrder;

                            if (!String.IsNullOrEmpty(item.LastMod))
                                //tbl_listingImage.LastMode = Convert.ToDateTime(item.LastMode, new System.Globalization.CultureInfo("en-AU"));
                                tbl_listingImage.LastMode = DateTime.Parse(item.LastMod, culture, System.Globalization.DateTimeStyles.AssumeLocal);

                            db.ListingImages.Add(tbl_listingImage);
                        }
                       
                    }
                    db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void AddListingImagesXML(long newlyAddedListing, List<PropertyImage> listingImages, string contactCompanyId)
        {
            try
            {
                foreach (PropertyImage item in listingImages)
                {

                    if (item != null)
                    {
                        if(!string.IsNullOrEmpty(item.ImageURL))
                        {
                            //  string destinationPath = HostingEnvironment.MapPath("~/StoredImages/RealEstateImages/" + contactCompanyId + "\\" + newlyAddedListing + "\\" + item.ImageID);
                            string drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Stores/" + contactCompanyId.ToString() + "/" + newlyAddedListing);

                            //first download image locally
                            if (!System.IO.Directory.Exists((drURL)))
                                System.IO.Directory.CreateDirectory(drURL);
                            string ImageName = DownloadImageLocallyXML(item.ImageURL, drURL);

                            ListingImage tbl_listingImage = new ListingImage();
                            tbl_listingImage.ListingId = newlyAddedListing;
                            tbl_listingImage.ClientImageId = item.ImageID;
                            tbl_listingImage.ImageURL = "/MPC_Content/Stores/" + contactCompanyId + "/" + newlyAddedListing + "/" + ImageName;


                            if (!String.IsNullOrEmpty(item.LastMod))
                                //tbl_listingImage.LastMode = Convert.ToDateTime(item.LastMode, new System.Globalization.CultureInfo("en-AU"));
                                tbl_listingImage.LastMode = DateTime.Parse(item.LastMod, culture, System.Globalization.DateTimeStyles.AssumeLocal);

                            db.ListingImages.Add(tbl_listingImage);
                        }
                        
                    }

                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void UpdateListingCustomCopy(long updatedListing, ListingCustomCopy listingCustomCopy)
        {
            try
            {
                if (listingCustomCopy != null)
                {
                    
                        var customCopy = db.CustomCopies.Where(item => item.ListingId == updatedListing).FirstOrDefault();

                        if (customCopy != null) //update
                        {
                            customCopy.ListingId = updatedListing;
                            customCopy.SignboardHeadline = listingCustomCopy.SignboardHeadline;
                            customCopy.SignboardDescription = listingCustomCopy.SignboardDescription;
                            customCopy.BrochureHeadline = listingCustomCopy.BrochureHeadline;
                            customCopy.BrochureDescription = listingCustomCopy.BrochureDescription;
                            customCopy.BrochureFeature1 = listingCustomCopy.BrochureFeature1;
                            customCopy.BrochureFeature2 = listingCustomCopy.BrochureFeature2;
                            customCopy.BrochureFeature3 = listingCustomCopy.BrochureFeature3;
                            customCopy.BrochureFeature4 = listingCustomCopy.BrochureFeature4;
                            customCopy.BrochureLifeStyle1 = listingCustomCopy.BrochureLifeStyle1;
                            customCopy.BrochureLifeStyle2 = listingCustomCopy.BrochureLifeStyle2;
                            customCopy.BrochureLifeStyle3 = listingCustomCopy.BrochureLifeStyle3;
                            db.CustomCopies.Attach(customCopy);

                            db.Entry(customCopy).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        else //add
                        {
                            AddListingCustomCopy(updatedListing, listingCustomCopy);
                        }
                    
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void AddListingCustomCopy(long newlyAddedListing, ListingCustomCopy listingCustomCopy)
        {
            try
            {
                if (listingCustomCopy != null)
                {

                    CustomCopy tbl_customCopy = new CustomCopy();
                        tbl_customCopy.ListingId = newlyAddedListing;
                        tbl_customCopy.SignboardHeadline = listingCustomCopy.SignboardHeadline;
                        tbl_customCopy.SignboardDescription = listingCustomCopy.SignboardDescription;
                        tbl_customCopy.BrochureHeadline = listingCustomCopy.BrochureHeadline;
                        tbl_customCopy.BrochureDescription = listingCustomCopy.BrochureDescription;
                        tbl_customCopy.BrochureFeature1 = listingCustomCopy.BrochureFeature1;
                        tbl_customCopy.BrochureFeature2 = listingCustomCopy.BrochureFeature2;
                        tbl_customCopy.BrochureFeature3 = listingCustomCopy.BrochureFeature3;
                        tbl_customCopy.BrochureFeature4 = listingCustomCopy.BrochureFeature4;
                        tbl_customCopy.BrochureLifeStyle1 = listingCustomCopy.BrochureLifeStyle1;
                        tbl_customCopy.BrochureLifeStyle2 = listingCustomCopy.BrochureLifeStyle2;
                        tbl_customCopy.BrochureLifeStyle3 = listingCustomCopy.BrochureLifeStyle3;

                        db.CustomCopies.Add(tbl_customCopy);

                        db.SaveChanges();
                    
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void UpdateListingFloorPlans(long updatedListing, List<ListingFloorplans> listingFloorPlans)
        {
            try
            {
               
                    foreach (ListingFloorplans item in listingFloorPlans)
                    {
                        if (item != null)
                        {
                            var listingFloorPlan = db.ListingFloorPlans.Where(i => i.ClientFloorplanID == item.FloorplanID).FirstOrDefault();

                            if (listingFloorPlan != null) //update
                            {
                                listingFloorPlan.ListingId = updatedListing;
                                listingFloorPlan.ClientFloorplanID = item.FloorplanID;
                                listingFloorPlan.ImageURL = item.ImageURL;
                                listingFloorPlan.PDFURL = item.PDFURL;

                                if (!String.IsNullOrEmpty(item.LastMod))
                                    //floorPlan.LastMode = Convert.ToDateTime(item.LastMod, new System.Globalization.CultureInfo("en-AU"));
                                    listingFloorPlan.LastMode = DateTime.Parse(item.LastMod, culture, System.Globalization.DateTimeStyles.AssumeLocal);
                                db.ListingFloorPlans.Attach(listingFloorPlan);
                                db.Entry(listingFloorPlan).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                            else //add 
                            {
                                List<ListingFloorplans> lstFloorPlanToAdd = new List<ListingFloorplans>();
                                lstFloorPlanToAdd.Add(item);

                                AddListingFloorPlans(updatedListing, lstFloorPlanToAdd);
                            }
                        }
                    }
                
            }
            catch (Exception)
            {
                throw;
            }
        }
        private long GetDefaultTerritoryByContactCompanyID(string contactCompanyId)
        {
            try
            {
                long contCompanyId = Convert.ToInt64(contactCompanyId);
                long territoryId;
                    territoryId = (from t in db.CompanyTerritories
                                   where t.CompanyId == contCompanyId
                                   && t.isDefault == true
                                   select t.TerritoryId).FirstOrDefault();


                return territoryId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private long ProcessOffice(ListingOffice listingOffice, string contactCompanyId, long territoryId)
        {
            try
            {
                long processedOfficeId = 0;
                Address address;

                    //Reference
                address = db.Addesses.Where(item => item.Reference == listingOffice.OfficeID).FirstOrDefault();

                    if (address != null) // update
                    {
                        processedOfficeId = UpdateOffice(address, listingOffice, contactCompanyId, territoryId);
                    }
                    else //add
                    {
                        processedOfficeId = AddOffice(listingOffice, contactCompanyId, territoryId);
                    }
                

                return processedOfficeId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private long ProcessOfficeXML(ListingOfficeXML listingOffice, string contactCompanyId, long territoryId,long Organisationid)
        {
            try
            {
                long processedOfficeId = 0;
                Address address;

               

                string CState = listingOffice.State;
                string PostCode = listingOffice.PostCode;
                //Reference
                address = db.Addesses.Where(item => item.State.StateName == CState && item.PostCode == PostCode).FirstOrDefault();

                if (address != null) // update
                {
                    processedOfficeId = UpdateOfficeXML(address, listingOffice, contactCompanyId, territoryId);
                }
                else //add
                {
                    processedOfficeId = AddOfficeXML(listingOffice, contactCompanyId, territoryId, Organisationid);
                }


                return processedOfficeId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private long AddOffice(ListingOffice listingOffice, string contactCompanyId, long territoryId)
        {
            try
            {
                //State last = (from l in db.States
                //                 orderby l.StateId descending
                //                 select l).First();

                long newlyAddedAddress = 0;

                //long NewStateID =last.StateId+1;

                    Address address = new Address();
                    State NState = new State();
                    address.CompanyId = (String.IsNullOrEmpty(contactCompanyId)) ? 0 : Convert.ToInt64(contactCompanyId);
                    address.AddressName = listingOffice.OfficeName;
                    address.Address1 = listingOffice.Address + "; " + listingOffice.TradingName + "; " + listingOffice.ABN;
                    address.Address2 = listingOffice.MailAddress + "; " + listingOffice.MailSuburb;
                    address.Address3 = listingOffice.MailState + "; " + listingOffice.MailPostCode;
                    address.City = listingOffice.Suburb;
                    address.URL = listingOffice.Website;
                    address.Email = listingOffice.Email;
                    address.Tel1 = listingOffice.Phone;
                    address.Tel2 = listingOffice.Fax;
                    NState.StateName = listingOffice.State;
                    
                    address.State = NState;
                
                    db.States.Add(NState);

                    db.SaveChanges();

                    address.StateId = NState.StateId;
                    address.PostCode = listingOffice.PostCode;
                    address.TerritoryId = territoryId;
                    address.Reference = listingOffice.OfficeID;

                    address.isArchived = false;

                    db.Addesses.Add(address);

                    if (db.SaveChanges() > 0)
                    {
                        newlyAddedAddress = address.AddressId;
                    }
                

                return newlyAddedAddress;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private long UpdateOffice(Address address, ListingOffice listingOffice, string contactCompanyId, long territoryId)
        {
            try
            {
                long updatedAddress = 0;

                address.CompanyId = (String.IsNullOrEmpty(contactCompanyId)) ? 0 : Convert.ToInt64(contactCompanyId);
                address.AddressName = listingOffice.OfficeName;
                address.Address3 = listingOffice.CompanyName;
                address.Address2 = listingOffice.TradingName;
                address.URL = listingOffice.Website;
                address.Email = listingOffice.Email;
                address.Tel1 = listingOffice.Phone;
                address.Tel2 = listingOffice.Fax;
                address.Address1 = listingOffice.Address;
                address.State.StateName = listingOffice.State;
                address.PostCode = listingOffice.PostCode;
                address.TerritoryId = territoryId;
                address.Reference = listingOffice.OfficeID;
                db.Addesses.Attach(address);

                db.Entry(address).State = EntityState.Modified;

                if (db.SaveChanges() > 0)
                {
                    updatedAddress = address.AddressId;
                }

                return updatedAddress;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private long UpdateOfficeXML(Address address, ListingOfficeXML listingOffice, string contactCompanyId, long territoryId)
        {
            try
            {
                long updatedAddress = 0;

                address.CompanyId = (String.IsNullOrEmpty(contactCompanyId)) ? 0 : Convert.ToInt64(contactCompanyId);
                address.AddressName = listingOffice.StreetNumber + " " + listingOffice.Street;
                address.Address1 = listingOffice.SubNumber + ", " + listingOffice.StreetNumber + " " + listingOffice.Street;
             
                address.City = listingOffice.Suburb;
                if (address.State != null)
                    address.State.StateName = listingOffice.State;
                
                address.PostCode = listingOffice.PostCode;
                address.TerritoryId = territoryId;
               
                db.Addesses.Attach(address);

                db.Entry(address).State = EntityState.Modified;

                if (db.SaveChanges() > 0)
                {
                    updatedAddress = address.AddressId;
                }

                return updatedAddress;
            }
            catch (Exception)
            {
                throw;
            }
        }


        private long AddOfficeXML(ListingOfficeXML listingOffice, string contactCompanyId, long territoryId,long Organisationid)
        {
            try
            {
                //State last = (from l in db.States
                //                 orderby l.StateId descending
                //                 select l).First();

                long newlyAddedAddress = 0;

                //long NewStateID =last.StateId+1;

                Address address = new Address();
                State NState = new State();
                address.CompanyId = (String.IsNullOrEmpty(contactCompanyId)) ? 0 : Convert.ToInt64(contactCompanyId);
                address.AddressName = listingOffice.StreetNumber + " " + listingOffice.Street;
                address.Address1 = listingOffice.SubNumber + ", " + listingOffice.StreetNumber + " " + listingOffice.Street;

                address.City = listingOffice.Suburb;
                address.OrganisationId = Organisationid;
                address.PostCode = listingOffice.PostCode;
                address.TerritoryId = territoryId;
                NState.StateName = listingOffice.State;

                address.State = NState;

                db.States.Add(NState);

                db.SaveChanges();

                address.StateId = NState.StateId;

                address.isArchived = false;

                db.Addesses.Add(address);

                if (db.SaveChanges() > 0)
                {
                    newlyAddedAddress = address.AddressId;
                }


                return newlyAddedAddress;
            }
            catch (Exception)
            {
                throw;
            }
        }


        #region Staff Member
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void ProcessStaffMember(long newlyAddedAddress, List<ListingStaffMember> lstStaffMember, string contactCompanyId, long territoryId)
        {
            try
            {
                CompanyContact contact;
                long contCompId = Convert.ToInt64(contactCompanyId);
 
                    foreach (ListingStaffMember item in lstStaffMember)
                    {
                        bool validEmail = IsValidEmail(item.Email);
                        //bool newEmail = IsNewEmail(item.Email);

                        if (!validEmail) //|| !newEmail)
                        {
                            continue;
                        }


                        //check with email
                        contact = db.CompanyContacts.Where(i => i.Email == item.Email && i.CompanyId == contCompId).FirstOrDefault();

                        if (contact != null) // update
                        {
                            UpdateStaffMember(contact.AddressId, contact, item, contactCompanyId, territoryId);
                        }
                        else //add
                        {
                            AddStaffMember(newlyAddedAddress, item, contactCompanyId, territoryId);
                        }
                    }
        
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateStaffMember(long newlyAddedAddress, CompanyContact contact, ListingStaffMember lstStaffMember, string contactCompanyId, long territoryId)
        {
            try
            {
                //problemarea
                contact.CompanyId = (String.IsNullOrEmpty(contactCompanyId)) ? 0 : Convert.ToInt64(contactCompanyId);
                contact.AddressId = newlyAddedAddress;
                contact.FirstName = lstStaffMember.FirstName;
                contact.LastName = lstStaffMember.LastName;
                contact.Title = lstStaffMember.JobTitle;
                contact.Email = lstStaffMember.Email;
                contact.HomeTel1 = lstStaffMember.Mobile;
                contact.HomeTel2 = lstStaffMember.Phone;
                contact.URL = lstStaffMember.Image;
                contact.TerritoryId = territoryId;
                contact.HomePostCode = lstStaffMember.MemberID;
                db.CompanyContacts.Attach(contact);

                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void AddStaffMember(long newlyAddedAddress, ListingStaffMember lstStaffMembers, string contactCompanyId, long territoryId)
        {
            try
            {
                CompanyContact tbl_contact = new CompanyContact();
                tbl_contact.CompanyId = (String.IsNullOrEmpty(contactCompanyId)) ? 0 : Convert.ToInt64(contactCompanyId);
                tbl_contact.AddressId = newlyAddedAddress;
                tbl_contact.FirstName = lstStaffMembers.FirstName;
                tbl_contact.LastName = lstStaffMembers.LastName;
                tbl_contact.Title = lstStaffMembers.JobTitle;
                tbl_contact.Email = lstStaffMembers.Email;
                tbl_contact.HomeTel1 = lstStaffMembers.Mobile;
                tbl_contact.HomeTel2 = lstStaffMembers.Phone;
                tbl_contact.URL = lstStaffMembers.Image;
                tbl_contact.TerritoryId = territoryId;
                tbl_contact.HomePostCode = lstStaffMembers.MemberID;

                if (lstStaffMembers.Status.Equals("Active"))
                    tbl_contact.isWebAccess = true;
                else if (lstStaffMembers.Status.Equals("Inactive"))
                    tbl_contact.isWebAccess = false;

                tbl_contact.isArchived = false;
                tbl_contact.Password = "1234";
                tbl_contact.QuestionId = 1;
                tbl_contact.SecretAnswer = "abc";

                db.CompanyContacts.Add(tbl_contact);

                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void UpdateStaffMemberXML(long newlyAddedAddress, CompanyContact contact, ListingAgentsXML lstStaffMember, string contactCompanyId, long territoryId,long ListingId)
        {
            try
            {
                //problemarea
                contact.CompanyId = (String.IsNullOrEmpty(contactCompanyId)) ? 0 : Convert.ToInt64(contactCompanyId);
                contact.AddressId = newlyAddedAddress;
                contact.FirstName = lstStaffMember.Name;
                contact.Email = lstStaffMember.Email;
                contact.HomeTel1 = lstStaffMember.Mobile;
                contact.TerritoryId = territoryId;
                db.CompanyContacts.Attach(contact);

                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();

                UpdateListingAgentsXML(ListingId, lstStaffMember, contact.ContactId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ProcessStaffMemberXML(long newlyAddedAddress, List<ListingAgentsXML> lstStaffMember, string contactCompanyId, long territoryId,long OrgId,long listingID)
        {
            try
            {
                CompanyContact contact;
                long contCompId = Convert.ToInt64(contactCompanyId);

                foreach (ListingAgentsXML item in lstStaffMember)
                {
                    bool validEmail = IsValidEmail(item.Email);
                    //bool newEmail = IsNewEmail(item.Email);

                    if (!validEmail) //|| !newEmail)
                    {
                        continue;
                    }

                    //check with email
                    contact = db.CompanyContacts.Where(i => i.Email == item.Email && i.CompanyId == contCompId).FirstOrDefault();

                    if (contact != null) // update
                    {
                        UpdateStaffMemberXML(contact.AddressId, contact, item, contactCompanyId, territoryId, listingID);
                    }
                    else //add
                    {
                        AddStaffMemberXML(newlyAddedAddress, item, contactCompanyId, territoryId, OrgId, listingID);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void AddStaffMemberXML(long newlyAddedAddress, ListingAgentsXML lstStaffMembers, string contactCompanyId, long territoryId,long orgId,long ListingId)
        {
            try
            {
                CompanyContact tbl_contact = new CompanyContact();
                tbl_contact.CompanyId = (String.IsNullOrEmpty(contactCompanyId)) ? 0 : Convert.ToInt64(contactCompanyId);
                tbl_contact.AddressId = newlyAddedAddress;
                tbl_contact.FirstName = lstStaffMembers.Name;
                tbl_contact.Email = lstStaffMembers.Email;
                tbl_contact.HomeTel1 = lstStaffMembers.Mobile;
                tbl_contact.TerritoryId = territoryId;
                tbl_contact.OrganisationId = orgId;

              

                tbl_contact.isArchived = false;
                tbl_contact.Password = "1234";
                tbl_contact.QuestionId = 1;
                tbl_contact.SecretAnswer = "abc";

                db.CompanyContacts.Add(tbl_contact);

                db.SaveChanges();

                AddListingAgentsXML(ListingId, lstStaffMembers, tbl_contact.ContactId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private bool IsNewEmail(string email)
        {
            bool newEmail = true;


            var emailCount = (from contact in db.CompanyContacts
                                  where contact.Email == email
                                  select contact).ToList();

                if (emailCount.Count > 0)
                {
                    newEmail = false;
                }

                return newEmail;
 
        }

        private void AddStaffMembers(int newlyAddedAddress, List<ListingStaffMember> lstStaffMembers, string contactCompanyId)
        {
            try
            {
                    foreach (ListingStaffMember item in lstStaffMembers)
                    {
                        bool validEmail = IsValidEmail(item.Email);
                        bool newEmail = IsNewEmail(item.Email);

                        if (!validEmail)// || !newEmail)
                        {
                            continue;
                        }

                        CompanyContact tbl_contact = new CompanyContact();
                        tbl_contact.CompanyId = (String.IsNullOrEmpty(contactCompanyId)) ? 0 : Convert.ToInt64(contactCompanyId);
                        tbl_contact.AddressId = newlyAddedAddress;
                        tbl_contact.FirstName = item.FirstName;
                        tbl_contact.LastName = item.LastName;
                        tbl_contact.Title = item.JobTitle;
                        tbl_contact.Email = item.Email;
                        tbl_contact.URL = item.Image;
                        tbl_contact.Mobile = item.Mobile;

                        if (item.Status.Equals("Active"))
                            tbl_contact.isWebAccess = true;
                        else if (item.Status.Equals("Inactive"))
                            tbl_contact.isWebAccess = false;

                        tbl_contact.HomeTel1 = item.Phone;
                        tbl_contact.Notes = item.Description;

                        db.CompanyContacts.Add(tbl_contact);
                    }

                    db.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
        public List<MPC.Models.DomainModels.Listing> GetPropertiesByContactCompanyID(long CompanyID)
        {

            return db.Listings.Where(c => c.CompanyId == CompanyID).ToList();
            
        }
        public List<MPC.Models.DomainModels.ListingImage> GetListingImagesByListingID(int propertyId)
        {
              return (from listingImage in db.ListingImages
                        where listingImage.ListingId == propertyId
                        select listingImage).ToList();
        }
        public List<MPC.Models.DomainModels.ListingImage> GetAllListingImages()
        {
            return db.ListingImages.ToList();
        }

        public long GetContactCompanyIDByStoreCode(string sStoreCode, long OrganisationID)
        {
            long iCompanyId = 0;
            var comp = db.Companies.Where(c => c.WebAccessCode == sStoreCode && c.OrganisationId == OrganisationID).FirstOrDefault();
            if (comp != null)
                iCompanyId = comp.CompanyId;
            return iCompanyId;
        }

        private long UpdateListingXML(ListingXML propertyListing, MPC.Models.DomainModels.Listing tblListing)
        {
            long updatedListing = 0;
            try
            {
                string strForParse = string.Empty;


                var listing = db.Listings.Where(item => item.ClientListingId == tblListing.ClientListingId).FirstOrDefault();

                if (listing != null)
                {
                    listing.ClientListingId = propertyListing.ClientListingId;
                    listing.WebID = propertyListing.AgentId;
                    listing.DisplayPrice = propertyListing.Price != null ? Convert.ToDouble(propertyListing.Price): 0;
                    listing.PriceView = propertyListing.PriceView;
                    listing.WebLink = propertyListing.ExternalLink != null ? propertyListing.ExternalLink.href : "";
                    listing.PropertyCategory = propertyListing.PropertyCategory != null ? propertyListing.PropertyCategory.Name : string.Empty;
                    listing.MainHeadLine = propertyListing.MainHeadline;
                    listing.MainDescription = propertyListing.MainDescription;
                    listing.InspectionTypye = propertyListing.InspectionTimes;
                    listing.ListingAuthority = propertyListing.ListingAuthority != null ? propertyListing.ListingAuthority.value : "";
                    listing.BedRooms = (String.IsNullOrEmpty(propertyListing.features.BedRooms)) ? 0 : Convert.ToInt32(propertyListing.features.BedRooms);
                    listing.BathRooms = (String.IsNullOrEmpty(propertyListing.features.BathRooms)) ? 0 : Convert.ToInt32(propertyListing.features.BathRooms);
                    listing.Garages = (String.IsNullOrEmpty(propertyListing.features.Garages)) ? 0 : Convert.ToInt32(propertyListing.features.Garages);
                    listing.Carports = (String.IsNullOrEmpty(propertyListing.features.Carports)) ? 0 : Convert.ToInt32(propertyListing.features.Carports);
                    listing.AirConditioning = propertyListing.features.AirConditioning;
                    listing.AlarmSystem = propertyListing.features.AlarmSystem;
                    listing.Intercom = propertyListing.features.Intercom;
                    listing.OpenFirePlace = propertyListing.features.OpenFirePlace;
                    listing.TennisCourt = propertyListing.features.TennisCourt;
                    listing.Toilets = (String.IsNullOrEmpty(propertyListing.features.Toilets)) ? 0 : Convert.ToInt32(propertyListing.features.Toilets);
                    listing.RempoteGarage = propertyListing.features.RempoteGarage;
                    listing.TotalParking = (String.IsNullOrEmpty(propertyListing.features.SecureParking)) ? 0 : Convert.ToInt32(propertyListing.features.SecureParking);
                    listing.Studies = (String.IsNullOrEmpty(propertyListing.features.Study)) ? 0 : Convert.ToInt32(propertyListing.features.Study);
                    listing.DishWasher = propertyListing.features.DishWasher;
                    listing.BuiltinRaboes = propertyListing.features.BuiltinRaboes;
                    listing.Gym = propertyListing.features.Gym;
                    listing.WorkShop = propertyListing.features.WorkShop;
                    listing.RumpusRoom = propertyListing.features.RumpusRoom;
                    listing.FloorBoards = propertyListing.features.FloorBoards;
                    listing.BroadBand = propertyListing.features.BroadBand;
                    listing.PayTV = propertyListing.features.PayTV;
                    listing.DuctedHeating = propertyListing.features.DuctedHeating;
                    listing.DuctedCooling = propertyListing.features.DuctedCooling;
                    listing.SplitSystemHeating = propertyListing.features.SplitSystemHeating;
                    listing.HydronicHeating = propertyListing.features.HydronicHeating;
                    listing.SplitSystemAircon = propertyListing.features.SplitSystemAircon;
                    listing.ReverseCycleAircon = propertyListing.features.ReverseCycleAircon;
                    listing.EvaporateCooling = propertyListing.features.EvaporateCooling;
                    listing.VacuumSystem = propertyListing.features.VacuumSystem;
                    listing.PoolInGround = propertyListing.features.PoolInGround;
                    listing.PoolAboveGround = propertyListing.features.PoolAboveGround;
                    listing.Balcony = propertyListing.features.Balcony;
                    listing.Deck = propertyListing.features.Deck;
                    listing.CourtYard = propertyListing.features.CourtYard;
                    listing.OutDoorEnt = propertyListing.features.OutDoorEnt;
                    listing.Shed = propertyListing.features.Shed;
                    listing.FullyFenced = propertyListing.features.FullyFenced;
                    listing.InsideSPA = propertyListing.features.InsideSPA;
                    listing.OutSideSPA = propertyListing.features.OutSideSPA;
                    listing.PropertyType = propertyListing.PropertyType;
                    listing.PropertyName = propertyListing.Office.Street;
                    listing.CompanyId = (String.IsNullOrEmpty(propertyListing.CompanyId)) ? 0 : Convert.ToInt64(propertyListing.CompanyId);
                    db.Listings.Attach(listing);

                    db.Entry(listing).State = EntityState.Modified;
                    if (db.SaveChanges() > 0)
                    {
                        updatedListing = listing.ListingId;
                    }
                }


                return updatedListing;
            }
            catch (Exception)
            {
                updatedListing = 0;
                return updatedListing;
            }
        }



        public bool AddListingDataXML(ListingPropertyXML objProperty,long Organisationid)
        {
            bool dataAdded = false;
            try
            {


                long territoryId = GetDefaultTerritoryByContactCompanyID(objProperty.Listing.CompanyId);

                long newlyAddedAddress = ProcessOfficeXML(objProperty.Listing.Office, objProperty.Listing.CompanyId, territoryId, Organisationid);
               // ProcessStaffMemberXML(newlyAddedAddress, objProperty.Listing.ListingAgents, objProperty.Listing.CompanyId, territoryId,Organisationid);

                long newlyAddedListing = AddListingXML(objProperty.Listing); //listing added
                ProcessStaffMemberXML(newlyAddedAddress, objProperty.Listing.ListingAgents, objProperty.Listing.CompanyId, territoryId, Organisationid, newlyAddedListing);
                AddListingImagesXML(newlyAddedListing, objProperty.Listing.ListingImages.image, objProperty.Listing.CompanyId);
                AddListingFloorPlansXML(newlyAddedListing, objProperty.Listing.ListingFloorplans.floorplans);
             
                dataAdded = true;
                return dataAdded;
            }
            catch (Exception)
            {
                throw;
            }
        }


        private long AddListingXML(ListingXML propertyListing)
        {
            long newlyAddedListing = 0;
            try
            {
                string strForParse = string.Empty;
                if (cultureKey == null) //not defined in web.config
                {
                    culture = new System.Globalization.CultureInfo("en-AU", true); // AU is default
                }
                else
                {
                    culture = new System.Globalization.CultureInfo(cultureKey, true);
                }

                MPC.Models.DomainModels.Listing listing = new MPC.Models.DomainModels.Listing();
                listing.ClientListingId = propertyListing.ClientListingId;
                listing.WebID = propertyListing.AgentId;
                listing.DisplayPrice = propertyListing.Price != null ? Convert.ToDouble(propertyListing.Price) : 0;
                listing.PriceView = propertyListing.PriceView;
                listing.WebLink = propertyListing.ExternalLink != null ? propertyListing.ExternalLink.href : "";
                listing.PropertyCategory = propertyListing.PropertyCategory != null ? propertyListing.PropertyCategory.Name : string.Empty;
                listing.MainHeadLine = propertyListing.MainHeadline;
                listing.MainDescription = propertyListing.MainDescription;
                listing.InspectionTypye = propertyListing.InspectionTimes;
                listing.ListingAuthority = propertyListing.ListingAuthority != null ? propertyListing.ListingAuthority.value : "";
                listing.BedRooms = (String.IsNullOrEmpty(propertyListing.features.BedRooms)) ? 0 : Convert.ToInt32(propertyListing.features.BedRooms);
                listing.BathRooms = (String.IsNullOrEmpty(propertyListing.features.BathRooms)) ? 0 : Convert.ToInt32(propertyListing.features.BathRooms);
                listing.Garages = (String.IsNullOrEmpty(propertyListing.features.Garages)) ? 0 : Convert.ToInt32(propertyListing.features.Garages);
                listing.Carports = (String.IsNullOrEmpty(propertyListing.features.Carports)) ? 0 : Convert.ToInt32(propertyListing.features.Carports);
                listing.AirConditioning = propertyListing.features.AirConditioning;
                listing.AlarmSystem = propertyListing.features.AlarmSystem;
                listing.Intercom = propertyListing.features.Intercom;
                listing.OpenFirePlace = propertyListing.features.OpenFirePlace;
                listing.TennisCourt = propertyListing.features.TennisCourt;
                listing.Toilets = (String.IsNullOrEmpty(propertyListing.features.Toilets)) ? 0 : Convert.ToInt32(propertyListing.features.Toilets);
                listing.RempoteGarage = propertyListing.features.RempoteGarage;
                listing.TotalParking = (String.IsNullOrEmpty(propertyListing.features.SecureParking)) ? 0 : Convert.ToInt32(propertyListing.features.SecureParking);
                listing.Studies = (String.IsNullOrEmpty(propertyListing.features.Study)) ? 0 : Convert.ToInt32(propertyListing.features.Study);
                listing.DishWasher = propertyListing.features.DishWasher;
                listing.BuiltinRaboes = propertyListing.features.BuiltinRaboes;
                listing.Gym = propertyListing.features.Gym;
                listing.WorkShop = propertyListing.features.WorkShop;
                listing.RumpusRoom = propertyListing.features.RumpusRoom;
                listing.FloorBoards = propertyListing.features.FloorBoards;
                listing.BroadBand = propertyListing.features.BroadBand;
                listing.PayTV = propertyListing.features.PayTV;
                listing.DuctedHeating = propertyListing.features.DuctedHeating;
                listing.DuctedCooling = propertyListing.features.DuctedCooling;
                listing.SplitSystemHeating = propertyListing.features.SplitSystemHeating;
                listing.HydronicHeating = propertyListing.features.HydronicHeating;
                listing.SplitSystemAircon = propertyListing.features.SplitSystemAircon;
                listing.ReverseCycleAircon = propertyListing.features.ReverseCycleAircon;
                listing.EvaporateCooling = propertyListing.features.EvaporateCooling;
                listing.VacuumSystem = propertyListing.features.VacuumSystem;
                listing.PoolInGround = propertyListing.features.PoolInGround;
                listing.PoolAboveGround = propertyListing.features.PoolAboveGround;
                listing.Balcony = propertyListing.features.Balcony;
                listing.Deck = propertyListing.features.Deck;
                listing.CourtYard = propertyListing.features.CourtYard;
                listing.OutDoorEnt = propertyListing.features.OutDoorEnt;
                listing.Shed = propertyListing.features.Shed;
                listing.FullyFenced = propertyListing.features.FullyFenced;
                listing.InsideSPA = propertyListing.features.InsideSPA;
                listing.OutSideSPA = propertyListing.features.OutSideSPA;
                listing.PropertyType = propertyListing.PropertyType;
                listing.PropertyName = propertyListing.Office.Street;
                listing.CompanyId = (String.IsNullOrEmpty(propertyListing.CompanyId)) ? 0 : Convert.ToInt64(propertyListing.CompanyId);

                db.Listings.Add(listing);
                db.SaveChanges();
                //   if (db.SaveChanges() > 0)
                // {
                newlyAddedListing = listing.ListingId;
                // }


                return newlyAddedListing;
            }
            catch (Exception)
            {
                newlyAddedListing = 0;
                return newlyAddedListing;
            }
        }

        private void UpdateListingAgentsXML(long updatedListingID, ListingAgentsXML list,long ContactId)
        {
            try
            {
                List<ListingAgent> listingAgents = db.ListingAgents.Where(i => i.ListingId == updatedListingID).ToList();

                foreach (ListingAgent item in listingAgents)
                {
                    if (item != null)
                    {
                        db.ListingAgents.Remove(item);
                    }
                }

                db.SaveChanges();

                //old Agents deleted now add new ones
                AddListingAgentsXML(updatedListingID, list,ContactId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void AddListingAgentsXML(long newlyAddedListing, ListingAgentsXML lstAgents,long ContactId)
        {
            try
            {

                if (lstAgents != null)
                {
                        CompanyContact contact = db.CompanyContacts.Where(c => c.ContactId == ContactId).FirstOrDefault();

                        if (contact != null)
                        {
                            ListingAgent agent = new ListingAgent();
                            agent.ListingId = newlyAddedListing;
                            agent.MemberId = contact.ContactId; //memberid is contactid from tbl_contacts
                            // agent.AgentOrder = item.AgentOrder;
                            agent.Mobile = contact.Mobile;
                            agent.Name = contact.FirstName;
                            agent.Phone = contact.HomeTel1;
                            agent.Phone2 = contact.HomeTel2;

                            if (contact.ContactRoleId != null && contact.ContactRoleId == 1)
                            {
                                agent.Admin = true;
                            }
                            else
                            {
                                agent.Admin = false;
                            }

                            agent.UserRef = contact.quickWebsite;
                            db.ListingAgents.Add(agent);
                        }

                       
                    }
                

                db.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
        }
        private string DownloadImageLocallyXML(string SourceURL, string DestinationBasePath)
        {

          

            System.Drawing.Image image = null;

            System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(SourceURL);
            webRequest.AllowWriteStreamBuffering = true;
            webRequest.Timeout = 30000;

            System.Net.WebResponse webResponse = webRequest.GetResponse();
            string filename = webResponse.ResponseUri.LocalPath;

            System.IO.Stream stream = webResponse.GetResponseStream();

            //  string fileName = webResponse.Headers["Content-Disposition"].Replace("attachment; filename=", String.Empty).Replace("\"", String.Empty);

            image = System.Drawing.Image.FromStream(stream);


            string rootPath = DestinationBasePath;
            string[] tokens = filename.Split(new[] { "/" }, StringSplitOptions.None);
            string token = tokens[tokens.Length - 1];
            string file = System.IO.Path.Combine(rootPath, token);
            image.Save(file);
            return token;


        }

        public MPC.Models.DomainModels.Listing GetListingByListingID(int propertyId)
        {
           
                return (from Listing in db.Listings
                        where Listing.ListingId == propertyId
                        select Listing).FirstOrDefault();
            
        }

        public long UpdateListing(MPC.Models.DomainModels.Listing propertyListing, MPC.Models.DomainModels.Listing tblListing)
        {
            long updatedListing = 0;
            try
            {
                string strForParse = string.Empty;
                var listing = db.Listings.Where(item => item.ListingId == tblListing.ListingId).FirstOrDefault();

                if (listing != null)
                {
                    //  listing.ClientListingID = propertyListing.ListingID;
                    listing.WebID = propertyListing.WebID;
                    listing.WebLink = propertyListing.WebLink;
                    listing.AddressDisplay = propertyListing.AddressDisplay;
                    listing.StreetAddress = propertyListing.StreetAddress;
                    listing.ClientListingId = propertyListing.ClientListingId;
                    if (propertyListing.LevelNumber == null || propertyListing.LevelNumber.Equals(string.Empty))
                    {
                        listing.LevelNumber = 0;
                    }
                    else
                    {
                        listing.LevelNumber = propertyListing.LevelNumber;
                    }
                    if (propertyListing.LotNumber == null || propertyListing.LotNumber.Equals(string.Empty))
                    {
                        listing.LotNumber = 0;
                    }
                    else
                    {
                        listing.LotNumber = propertyListing.LotNumber;
                    }



                    if (propertyListing.UnitNumber == null || propertyListing.UnitNumber.Equals(string.Empty))
                    {
                        listing.UnitNumber = 0;
                    }

                    else
                    {
                        listing.UnitNumber = propertyListing.UnitNumber;
                    }



                    if (propertyListing.StreetNumber == null || propertyListing.StreetNumber.Equals(string.Empty))
                    {
                        propertyListing.StreetNumber = 0;
                    }
                    else
                    {

                        listing.StreetNumber = propertyListing.StreetNumber;
                    }
                    listing.Street = propertyListing.Street;
                    listing.Suburb = propertyListing.Suburb;
                    listing.State = propertyListing.State;
                    listing.PostCode = propertyListing.PostCode;
                    listing.PropertyName = propertyListing.PropertyName;
                    listing.PropertyType = propertyListing.PropertyType;
                    listing.PropertyCategory = propertyListing.PropertyCategory;

                    //if (!String.IsNullOrEmpty(propertyListing.ListingDate))
                    //    listing.ListingDate = DateTime.Parse(propertyListing.ListingDate, culture, System.Globalization.DateTimeStyles.AssumeLocal);

                    //if (!String.IsNullOrEmpty(propertyListing.ListingExpiryDate))
                    //    listing.ListingExpiryDate = DateTime.Parse(propertyListing.ListingExpiryDate, culture, System.Globalization.DateTimeStyles.AssumeLocal);

                    listing.ListingStatus = propertyListing.ListingStatus;
                    listing.ListingMethod = propertyListing.ListingMethod;
                    listing.ListingAuthority = propertyListing.ListingAuthority;
                    listing.InspectionTypye = propertyListing.InspectionTypye;
                    listing.ListingType = propertyListing.ListingType;
                    if (propertyListing.WaterRates != null && !propertyListing.WaterRates.Equals(string.Empty))
                    {
                        listing.WaterRates = propertyListing.WaterRates;
                    }
                    else
                    {
                        listing.WaterRates = 0;
                    }

                    if (propertyListing.StrataAdmin != null && !propertyListing.StrataAdmin.Equals(string.Empty))
                    {
                        listing.StrataAdmin = propertyListing.StrataAdmin;
                    }
                    else
                    {
                        listing.StrataAdmin = 0;
                    }

                    if (propertyListing.StrataSinking != null && !propertyListing.StrataSinking.Equals(string.Empty))
                    {
                        listing.StrataSinking = propertyListing.StrataSinking;
                    }
                    else
                    {
                        listing.StrataSinking = 0;
                    }

                    listing.AutionVenue = propertyListing.AutionVenue;
                     
                   // listing.AuctionDate =Convert.ToDateTime( propertyListing.AuctionDate).;
                   // DateTime myDate = new DateTime(myDate.Year, myDate.Month, myDate.Day);
                    listing.AuctionDate =propertyListing.AuctionDate;  //DateTime.ParseExact(propertyListing.AuctionDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    //    
                    //listing.AutionVenue = propertyListing.AuctionVenue;

                    //if (!String.IsNullOrEmpty(propertyListing.EOIClosingDate))
                    //    listing.EOIClosingDate = DateTime.Parse(propertyListing.EOIClosingDate, culture, System.Globalization.DateTimeStyles.AssumeLocal);

                    if (propertyListing.DisplayPrice != null)
                    {
                        strForParse = propertyListing.DisplayPrice.ToString();
                        string result = Regex.Replace(strForParse, @"[^\d]", "");
                        if (!result.Equals(string.Empty))
                            listing.DisplayPrice = Convert.ToDouble(result);
                    }
                    else
                    {
                        listing.DisplayPrice = null;
                    }
                    if (propertyListing.SearchPrice == null || propertyListing.SearchPrice.Equals(string.Empty))
                    {
                        strForParse = propertyListing.SearchPrice.ToString();
                        string result = Regex.Replace(strForParse, @"[^\d]", "");
                        if (!result.Equals(string.Empty))
                            listing.SearchPrice = Convert.ToDouble(result);
                    }
                    else
                    {
                        listing.SearchPrice = propertyListing.SearchPrice;
                    }

                    if (propertyListing.RendPeriod == null || propertyListing.RendPeriod.Equals(string.Empty))
                    {
                        listing.RendPeriod = 0;
                        
                    }
                    else
                    {
                        listing.RendPeriod = propertyListing.RendPeriod;
                    }
                 //   DateTime dtIN = DateTime.ParseExact(propertyListing.AvailableDate.ToString(), "{0:MM/dd/yyyy}", CultureInfo.InvariantCulture);
                    //listing.AvailableDate = DateTime.ParseExact(propertyListing.AvailableDate.ToString(), "yyyyMMdd ", null); //DateTime.ParseExact(propertyListing.AvailableDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    listing.AvailableDate = propertyListing.AvailableDate;
                    //propertyListing.AvailableDate.ToString("yyyy-MM-dd")
                    //listing.AvailableDate = DateTime.Parse();
                   
                    listing.InspectionTimeFrom2 = propertyListing.InspectionTimeFrom2;


                    listing.InspectionTimeFrom1 = propertyListing.InspectionTimeFrom1;


                    listing.AuctionTime = propertyListing.AuctionTime;


                    listing.AuctionEndTime = propertyListing.AuctionEndTime;

                    //if (!String.IsNullOrEmpty(propertyListing.SoldDate))
                    //    listing.SoldDate = DateTime.Parse(propertyListing.SoldDate, culture, System.Globalization.DateTimeStyles.AssumeLocal);

                    if (propertyListing.SoldPrice != null && !propertyListing.SoldPrice.Equals(string.Empty))
                    {
                        strForParse = propertyListing.SoldPrice.ToString();
                        string result = Regex.Replace(strForParse, @"[^\d]", "");
                        if (!result.Equals(string.Empty))
                            listing.SoldPrice = Convert.ToDouble(result);
                    }
                    else
                    {
                        listing.SoldPrice = null;
                    }

                    if (propertyListing.IsSoldPriceConfidential != null)
                    {
                        //string confid = propertyListing.IsSoldPriceConfidential;
                        //if (confid.Equals("Yes"))
                        //{
                        //    listing.IsSoldPriceConfidential = true;
                        //}
                        //else if (confid.Equals("No"))
                        //{
                        //    listing.IsSoldPriceConfidential = false;
                        //}
                        listing.IsSoldPriceConfidential = propertyListing.IsSoldPriceConfidential;
                    }
                    else
                    {
                        listing.IsSoldPriceConfidential = null;
                    }

                    listing.MainHeadLine = propertyListing.MainHeadLine;

                    listing.MainDescription = propertyListing.MainDescription;
                    listing.BedRooms = (String.IsNullOrEmpty(propertyListing.BedRooms.ToString())) ? 0 : Convert.ToInt32(propertyListing.BedRooms);
                    listing.BathRooms = (String.IsNullOrEmpty(propertyListing.BathRooms.ToString())) ? 0 : Convert.ToInt32(propertyListing.BathRooms);
                    listing.LoungeRooms = (String.IsNullOrEmpty(propertyListing.LoungeRooms.ToString())) ? 0 : Convert.ToInt32(propertyListing.LoungeRooms);
                    listing.Toilets = (String.IsNullOrEmpty(propertyListing.Toilets.ToString())) ? 0 : Convert.ToInt32(propertyListing.Toilets);
                    listing.Studies = (String.IsNullOrEmpty(propertyListing.Studies.ToString())) ? 0 : Convert.ToInt32(propertyListing.Studies);
                    listing.Pools = (String.IsNullOrEmpty(propertyListing.Pools.ToString())) ? 0 : Convert.ToInt32(propertyListing.Pools);
                    listing.Garages = (String.IsNullOrEmpty(propertyListing.Garages.ToString())) ? 0 : Convert.ToInt32(propertyListing.Garages);
                    listing.Carports = (String.IsNullOrEmpty(propertyListing.Carports.ToString())) ? 0 : Convert.ToInt32(propertyListing.Carports);
                    listing.CarSpaces = (String.IsNullOrEmpty(propertyListing.CarSpaces.ToString())) ? 0 : Convert.ToInt32(propertyListing.CarSpaces);
                    listing.TotalParking = (String.IsNullOrEmpty(propertyListing.TotalParking.ToString())) ? 0 : Convert.ToInt32(propertyListing.TotalParking);

                    if (propertyListing.LandArea != null)
                    {
                        strForParse = propertyListing.LandArea.ToString();
                        string result = Regex.Replace(strForParse, @"[^\d]", "");
                        if (!result.Equals(string.Empty))
                        {
                            listing.LandArea = Convert.ToDouble(result);
                        }
                        else
                        {
                            listing.LandArea = null;
                        }

                    }
                    else
                    {
                        listing.LandArea = null;
                    }
                    listing.LandAreaUnit = propertyListing.LandAreaUnit;
                    listing.BuildingAreaSqm = (String.IsNullOrEmpty(propertyListing.BuildingAreaSqm.ToString())) ? 0 : Convert.ToInt32(propertyListing.BuildingAreaSqm);
                    listing.ExternalAreaSqm = (String.IsNullOrEmpty(propertyListing.ExternalAreaSqm.ToString())) ? 0 : Convert.ToInt32(propertyListing.ExternalAreaSqm);
                    listing.FrontageM = (String.IsNullOrEmpty(propertyListing.FrontageM.ToString())) ? 0 : Convert.ToInt32(propertyListing.FrontageM);
                    listing.Aspect = propertyListing.Aspect;
                    listing.YearBuilt = propertyListing.YearBuilt;
                    listing.YearRenovated = propertyListing.YearRenovated;
                    listing.Construction = propertyListing.Construction;
                    listing.PropertyCondition = propertyListing.PropertyCondition;

                    if (propertyListing.EnergyRating != null)
                    {
                        strForParse = propertyListing.EnergyRating.ToString();
                        string result = Regex.Replace(strForParse, @"[^\d]", "");
                        if (!result.Equals(string.Empty))
                        {
                            listing.EnergyRating = Convert.ToDouble(result);
                        }
                        else
                        {
                            listing.EnergyRating = null;
                        }

                    }
                    else
                    {
                        listing.EnergyRating = null;
                    }


                    listing.Features = propertyListing.Features;

                    if (propertyListing.LandTax != null)
                    {
                        strForParse = propertyListing.LandTax.ToString();
                        string result = Regex.Replace(strForParse, @"[^\d]", "");
                        if (!result.Equals(string.Empty))
                        {
                            listing.LandTax = Convert.ToDouble(result);
                        }
                        else
                        {
                            listing.LandTax = null;
                        }

                    }
                    else
                    {
                        listing.LandTax = null;
                    }
                    if (propertyListing.CounsilRates != null)
                    {
                        strForParse = propertyListing.CounsilRates.ToString();
                        string result = Regex.Replace(strForParse, @"[^\d]", "");
                        if (!result.Equals(string.Empty))
                        {
                            listing.CounsilRates = Convert.ToDouble(result);
                        }
                        else
                        {
                            listing.CounsilRates = null;
                        }

                    }
                    else
                    {
                        listing.CounsilRates = null;
                    }

                    if (propertyListing.StrataAdmin != null)
                    {
                        strForParse = propertyListing.StrataAdmin.ToString();
                        string result = Regex.Replace(strForParse, @"[^\d]", "");
                        if (!result.Equals(string.Empty))
                        {
                            listing.StrataAdmin = Convert.ToDouble(result);
                        }
                        else
                        {
                            listing.StrataAdmin = null;
                        }
                    }
                    else
                    {
                        listing.StrataAdmin = null;
                    }

                    if (propertyListing.StrataSinking != null)
                    {
                        strForParse = propertyListing.StrataSinking.ToString();
                        string result = Regex.Replace(strForParse, @"[^\d]", "");
                        if (!result.Equals(string.Empty))
                        {
                            listing.StrataSinking = Convert.ToDouble(result);
                        }
                        else
                        {
                            listing.StrataSinking = null;
                        }

                    }
                    else
                    {
                        listing.StrataSinking = null;
                    }

                    if (propertyListing.OtherOutgoings != null)
                    {
                        strForParse = propertyListing.OtherOutgoings.ToString();
                        string result = Regex.Replace(strForParse, @"[^\d]", "");
                        if (!result.Equals(string.Empty))
                        {
                            listing.OtherOutgoings = Convert.ToDouble(result);
                        }
                        else
                        {
                            listing.OtherOutgoings = null;
                        }
                    }
                    else
                    {
                        listing.OtherOutgoings = null;
                    }

                    if (propertyListing.TotalOutgoings != null)
                    {
                        strForParse = propertyListing.TotalOutgoings.ToString();
                        string result = Regex.Replace(strForParse, @"[^\d]", "");
                        if (!result.Equals(string.Empty))
                        {
                            listing.TotalOutgoings = Convert.ToDouble(result);
                        }
                        else
                        {
                            listing.TotalOutgoings = null;
                        }
                    }
                    else
                    {
                        listing.TotalOutgoings = null;
                    }

                    listing.LegalDescription = propertyListing.LegalDescription;
                    listing.LegalLot = propertyListing.LegalLot;
                    listing.LegalDP = propertyListing.LegalDP;
                    listing.LegalVol = propertyListing.LegalVol;
                    listing.LegalFolio = propertyListing.LegalFolio;
                    listing.Zoning = propertyListing.Zoning;
                    //    listing.ContactCompanyID = (String.IsNullOrEmpty(propertyListing.ContactCompanyID)) ? 0 : Convert.ToInt32(propertyListing.ContactCompanyID);
                    listing.BrochureDescription = propertyListing.BrochureDescription;
                    listing.BrochureMainHeadLine = propertyListing.BrochureMainHeadLine;
                    listing.BrochureSummary = propertyListing.BrochureSummary;
                    listing.SignBoardDescription = propertyListing.SignBoardDescription;
                    listing.SignBoardInstallInstruction = propertyListing.SignBoardInstallInstruction;
                    listing.SignBoardMainHeadLine = propertyListing.SignBoardMainHeadLine;
                    listing.SignBoardSummary = propertyListing.SignBoardSummary;
                    listing.AdvertsDescription = propertyListing.AdvertsDescription;
                    listing.AdvertsMainHeadLine = propertyListing.AdvertsMainHeadLine;
                    listing.AdvertsSummary = propertyListing.AdvertsSummary;
                    listing.CompanyId = propertyListing.CompanyId;
                    db.Listings.Attach(listing);

                    db.Entry(listing).State = EntityState.Modified;
                  //  if (db.SaveChanges() > 0)
                  //  {
                    db.SaveChanges();
                        updatedListing = listing.ListingId;

                  //  }
                }
                return updatedListing;
            }
            catch (Exception ex)
            {
                updatedListing = 0;
                throw ex;
            }
            
        }

        public long AddNewListing(MPC.Models.DomainModels.Listing propertyListing)
        {
            long ListingId = 0;
            MPC.Models.DomainModels.Listing listing = new Models.DomainModels.Listing();
            string strForParse = string.Empty;
            listing.WebID = propertyListing.WebID;
            listing.WebLink = propertyListing.WebLink;
            listing.AddressDisplay = propertyListing.AddressDisplay;
            listing.StreetAddress = propertyListing.StreetAddress;
            listing.PropertyName = propertyListing.PropertyName;
            listing.ClientListingId = propertyListing.ClientListingId;
            if (propertyListing.LevelNumber == null || propertyListing.LevelNumber.Equals(string.Empty))
            {
                listing.LevelNumber = 0;
            }
            else
            {
                listing.LevelNumber = propertyListing.LevelNumber;
            }
            if (propertyListing.LotNumber == null || propertyListing.LotNumber.Equals(string.Empty))
            {
                listing.LotNumber = 0;
            }
            else
            {
                listing.LotNumber = propertyListing.LotNumber;
            }



            if (propertyListing.UnitNumber == null || propertyListing.UnitNumber.Equals(string.Empty))
            {
                listing.UnitNumber = 0;
            }

            else
            {
                listing.UnitNumber = propertyListing.UnitNumber;
            }



            if (propertyListing.StreetNumber == null || propertyListing.StreetNumber.Equals(string.Empty))
            {
                propertyListing.StreetNumber = 0;
            }
            else
            {

                listing.StreetNumber = propertyListing.StreetNumber;
            }
            listing.Street = propertyListing.Street;
            listing.Suburb = propertyListing.Suburb;
            listing.State = propertyListing.State;
            listing.PostCode = propertyListing.PostCode;
            listing.PropertyName = propertyListing.PropertyName;
            listing.PropertyType = propertyListing.PropertyType;
            listing.PropertyCategory = propertyListing.PropertyCategory;

            //if (!String.IsNullOrEmpty(propertyListing.ListingDate))
            //    listing.ListingDate = DateTime.Parse(propertyListing.ListingDate, culture, System.Globalization.DateTimeStyles.AssumeLocal);

            //if (!String.IsNullOrEmpty(propertyListing.ListingExpiryDate))
            //    listing.ListingExpiryDate = DateTime.Parse(propertyListing.ListingExpiryDate, culture, System.Globalization.DateTimeStyles.AssumeLocal);

            listing.ListingStatus = propertyListing.ListingStatus;
            listing.ListingMethod = propertyListing.ListingMethod;
            listing.ListingAuthority = propertyListing.ListingAuthority;
            listing.InspectionTypye = propertyListing.InspectionTypye;
            listing.ListingType = propertyListing.ListingType;
            if (propertyListing.WaterRates != null && !propertyListing.WaterRates.Equals(string.Empty))
            {
                listing.WaterRates = propertyListing.WaterRates;
            }
            else
            {
                listing.WaterRates = 0;
            }

            if (propertyListing.StrataAdmin != null && !propertyListing.StrataAdmin.Equals(string.Empty))
            {
                listing.StrataAdmin = propertyListing.StrataAdmin;
            }
            else
            {
                listing.StrataAdmin = 0;
            }

            if (propertyListing.StrataSinking != null && !propertyListing.StrataSinking.Equals(string.Empty))
            {
                listing.StrataSinking = propertyListing.StrataSinking;
            }
            else
            {
                listing.StrataSinking = 0;
            }

            listing.AutionVenue = propertyListing.AutionVenue;
            listing.AuctionDate = propertyListing.AuctionDate; //DateTime.ParseExact(propertyListing.AuctionDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            //listing.AutionVenue = propertyListing.AuctionVenue;

            //if (!String.IsNullOrEmpty(propertyListing.EOIClosingDate))
            //    listing.EOIClosingDate = DateTime.Parse(propertyListing.EOIClosingDate, culture, System.Globalization.DateTimeStyles.AssumeLocal);

            if (propertyListing.DisplayPrice != null)
            {
                strForParse = propertyListing.DisplayPrice.ToString();
                string result = Regex.Replace(strForParse, @"[^\d]", "");
                if (!result.Equals(string.Empty))
                    listing.DisplayPrice = Convert.ToDouble(result);
            }
            else
            {
                listing.DisplayPrice = null;
            }
            if (propertyListing.SearchPrice == null || propertyListing.SearchPrice.Equals(string.Empty))
            {
                strForParse = propertyListing.SearchPrice.ToString();
                string result = Regex.Replace(strForParse, @"[^\d]", "");
                if (!result.Equals(string.Empty))
                    listing.SearchPrice = Convert.ToDouble(result);
            }
            else
            {
                listing.SearchPrice = propertyListing.SearchPrice;
            }

            if (propertyListing.RendPeriod == null || propertyListing.RendPeriod.Equals(string.Empty))
            {
                listing.RendPeriod = 0;

            }
            else
            {
                listing.RendPeriod = propertyListing.RendPeriod;
            }

            listing.AvailableDate = propertyListing.AvailableDate; //DateTime.ParseExact(propertyListing.AvailableDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            listing.InspectionTimeFrom2 = propertyListing.InspectionTimeFrom2;


            listing.InspectionTimeFrom1 = propertyListing.InspectionTimeFrom1;


            listing.AuctionTime = propertyListing.AuctionTime;


            listing.AuctionEndTime = propertyListing.AuctionEndTime;

            //if (!String.IsNullOrEmpty(propertyListing.SoldDate))
            //    listing.SoldDate = DateTime.Parse(propertyListing.SoldDate, culture, System.Globalization.DateTimeStyles.AssumeLocal);

            if (propertyListing.SoldPrice != null && !propertyListing.SoldPrice.Equals(string.Empty))
            {
                strForParse = propertyListing.SoldPrice.ToString();
                string result = Regex.Replace(strForParse, @"[^\d]", "");
                if (!result.Equals(string.Empty))
                    listing.SoldPrice = Convert.ToDouble(result);
            }
            else
            {
                listing.SoldPrice = null;
            }

            if (propertyListing.IsSoldPriceConfidential != null)
            {
                //string confid = propertyListing.IsSoldPriceConfidential;
                //if (confid.Equals("Yes"))
                //{
                //    listing.IsSoldPriceConfidential = true;
                //}
                //else if (confid.Equals("No"))
                //{
                //    listing.IsSoldPriceConfidential = false;
                //}
                listing.IsSoldPriceConfidential = propertyListing.IsSoldPriceConfidential;
            }
            else
            {
                listing.IsSoldPriceConfidential = null;
            }

            listing.MainHeadLine = propertyListing.MainHeadLine;

            listing.MainDescription = propertyListing.MainDescription;
            listing.BedRooms = (String.IsNullOrEmpty(propertyListing.BedRooms.ToString())) ? 0 : Convert.ToInt32(propertyListing.BedRooms);
            listing.BathRooms = (String.IsNullOrEmpty(propertyListing.BathRooms.ToString())) ? 0 : Convert.ToInt32(propertyListing.BathRooms);
            listing.LoungeRooms = (String.IsNullOrEmpty(propertyListing.LoungeRooms.ToString())) ? 0 : Convert.ToInt32(propertyListing.LoungeRooms);
            listing.Toilets = (String.IsNullOrEmpty(propertyListing.Toilets.ToString())) ? 0 : Convert.ToInt32(propertyListing.Toilets);
            listing.Studies = (String.IsNullOrEmpty(propertyListing.Studies.ToString())) ? 0 : Convert.ToInt32(propertyListing.Studies);
            listing.Pools = (String.IsNullOrEmpty(propertyListing.Pools.ToString())) ? 0 : Convert.ToInt32(propertyListing.Pools);
            listing.Garages = (String.IsNullOrEmpty(propertyListing.Garages.ToString())) ? 0 : Convert.ToInt32(propertyListing.Garages);
            listing.Carports = (String.IsNullOrEmpty(propertyListing.Carports.ToString())) ? 0 : Convert.ToInt32(propertyListing.Carports);
            listing.CarSpaces = (String.IsNullOrEmpty(propertyListing.CarSpaces.ToString())) ? 0 : Convert.ToInt32(propertyListing.CarSpaces);
            listing.TotalParking = (String.IsNullOrEmpty(propertyListing.TotalParking.ToString())) ? 0 : Convert.ToInt32(propertyListing.TotalParking);

            if (propertyListing.LandArea != null)
            {
                strForParse = propertyListing.LandArea.ToString();
                string result = Regex.Replace(strForParse, @"[^\d]", "");
                if (!result.Equals(string.Empty))
                {
                    listing.LandArea = Convert.ToDouble(result);
                }
                else
                {
                    listing.LandArea = null;
                }

            }
            else
            {
                listing.LandArea = null;
            }
            listing.LandAreaUnit = propertyListing.LandAreaUnit;
            listing.BuildingAreaSqm = (String.IsNullOrEmpty(propertyListing.BuildingAreaSqm.ToString())) ? 0 : Convert.ToInt32(propertyListing.BuildingAreaSqm);
            listing.ExternalAreaSqm = (String.IsNullOrEmpty(propertyListing.ExternalAreaSqm.ToString())) ? 0 : Convert.ToInt32(propertyListing.ExternalAreaSqm);
            listing.FrontageM = (String.IsNullOrEmpty(propertyListing.FrontageM.ToString())) ? 0 : Convert.ToInt32(propertyListing.FrontageM);
            listing.Aspect = propertyListing.Aspect;
            listing.YearBuilt = propertyListing.YearBuilt;
            listing.YearRenovated = propertyListing.YearRenovated;
            listing.Construction = propertyListing.Construction;
            listing.PropertyCondition = propertyListing.PropertyCondition;

            if (propertyListing.EnergyRating != null)
            {
                strForParse = propertyListing.EnergyRating.ToString();
                string result = Regex.Replace(strForParse, @"[^\d]", "");
                if (!result.Equals(string.Empty))
                {
                    listing.EnergyRating = Convert.ToDouble(result);
                }
                else
                {
                    listing.EnergyRating = null;
                }

            }
            else
            {
                listing.EnergyRating = null;
            }


            listing.Features = propertyListing.Features;

            if (propertyListing.LandTax != null)
            {
                strForParse = propertyListing.LandTax.ToString();
                string result = Regex.Replace(strForParse, @"[^\d]", "");
                if (!result.Equals(string.Empty))
                {
                    listing.LandTax = Convert.ToDouble(result);
                }
                else
                {
                    listing.LandTax = null;
                }

            }
            else
            {
                listing.LandTax = null;
            }
            if (propertyListing.CounsilRates != null)
            {
                strForParse = propertyListing.CounsilRates.ToString();
                string result = Regex.Replace(strForParse, @"[^\d]", "");
                if (!result.Equals(string.Empty))
                {
                    listing.CounsilRates = Convert.ToDouble(result);
                }
                else
                {
                    listing.CounsilRates = null;
                }

            }
            else
            {
                listing.CounsilRates = null;
            }

            if (propertyListing.StrataAdmin != null)
            {
                strForParse = propertyListing.StrataAdmin.ToString();
                string result = Regex.Replace(strForParse, @"[^\d]", "");
                if (!result.Equals(string.Empty))
                {
                    listing.StrataAdmin = Convert.ToDouble(result);
                }
                else
                {
                    listing.StrataAdmin = null;
                }
            }
            else
            {
                listing.StrataAdmin = null;
            }

            if (propertyListing.StrataSinking != null)
            {
                strForParse = propertyListing.StrataSinking.ToString();
                string result = Regex.Replace(strForParse, @"[^\d]", "");
                if (!result.Equals(string.Empty))
                {
                    listing.StrataSinking = Convert.ToDouble(result);
                }
                else
                {
                    listing.StrataSinking = null;
                }

            }
            else
            {
                listing.StrataSinking = null;
            }

            if (propertyListing.OtherOutgoings != null)
            {
                strForParse = propertyListing.OtherOutgoings.ToString();
                string result = Regex.Replace(strForParse, @"[^\d]", "");
                if (!result.Equals(string.Empty))
                {
                    listing.OtherOutgoings = Convert.ToDouble(result);
                }
                else
                {
                    listing.OtherOutgoings = null;
                }
            }
            else
            {
                listing.OtherOutgoings = null;
            }

            if (propertyListing.TotalOutgoings != null)
            {
                strForParse = propertyListing.TotalOutgoings.ToString();
                string result = Regex.Replace(strForParse, @"[^\d]", "");
                if (!result.Equals(string.Empty))
                {
                    listing.TotalOutgoings = Convert.ToDouble(result);
                }
                else
                {
                    listing.TotalOutgoings = null;
                }
            }
            else
            {
                listing.TotalOutgoings = null;
            }

            listing.LegalDescription = propertyListing.LegalDescription;
            listing.LegalLot = propertyListing.LegalLot;
            listing.LegalDP = propertyListing.LegalDP;
            listing.LegalVol = propertyListing.LegalVol;
            listing.LegalFolio = propertyListing.LegalFolio;
            listing.Zoning = propertyListing.Zoning;
            //    listing.ContactCompanyID = (String.IsNullOrEmpty(propertyListing.ContactCompanyID)) ? 0 : Convert.ToInt32(propertyListing.ContactCompanyID);
            listing.BrochureDescription = propertyListing.BrochureDescription;
            listing.BrochureMainHeadLine = propertyListing.BrochureMainHeadLine;
            listing.BrochureSummary = propertyListing.BrochureSummary;
            listing.SignBoardDescription = propertyListing.SignBoardDescription;
            listing.SignBoardInstallInstruction = propertyListing.SignBoardInstallInstruction;
            listing.SignBoardMainHeadLine = propertyListing.SignBoardMainHeadLine;
            listing.SignBoardSummary = propertyListing.SignBoardSummary;
            listing.AdvertsDescription = propertyListing.AdvertsDescription;
            listing.AdvertsMainHeadLine = propertyListing.AdvertsMainHeadLine;
            listing.AdvertsSummary = propertyListing.AdvertsSummary;
            db.Listings.Add(listing);
            db.SaveChanges();
            //if (db.SaveChanges() > 0)
           // {

                ListingId = listing.ListingId;

           // }
            return ListingId;
        }

        public List<ListingImage> GetAllListingImages(long ListingID)
        {
            return db.ListingImages.Where(i => i.ListingId == ListingID).ToList();
        }

        public void ListingImage(ListingImage NewImage)
        {
            db.ListingImages.Add(NewImage);
            db.SaveChanges();
        }

        public void DeleteListingImage(long listingImageID)
        {
            ListingImage image = db.ListingImages.Where(i => i.ListingImageId == listingImageID).FirstOrDefault();
            db.ListingImages.Remove(image);
            db.SaveChanges();
            
        }

        public bool DeleteLisitngData(long ListingId)
        {
            bool result=false;
            MPC.Models.DomainModels.Listing Listing = db.Listings.Where(i => i.ListingId == ListingId).FirstOrDefault();
            List<ListingImage> ListingImages = db.ListingImages.Where(i => i.ListingId == ListingId).ToList();
            foreach (ListingImage Image in ListingImages)
            {
                db.ListingImages.Remove(Image);
            }
            List<ListingBulletPoint> BulletePoint = db.ListingBulletPoints.Where(i => i.ListingId == ListingId).ToList();

            foreach (ListingBulletPoint point in BulletePoint)
            {
                db.ListingBulletPoints.Remove(point);
            }
            //List<ListingAgent> Agents = db.ListingAgents.Where(i => i.ListingId == ListingId).ToList();

            //foreach (ListingAgent agent in Agents)
            //{
            //    db.ListingAgents.Remove(agent);
            //}
            db.Listings.Remove(Listing);
            if (db.SaveChanges() > 0)
            {
                result = true;
            }
            return result;
        }
        public void AddlistingImages(long ListingId,List<ListingImage> Images)
        {
            foreach (var item in Images)
            {
                ListingImage Image = new ListingImage();
                Image.ImageURL = item.ImageType;
                Image.ListingId = ListingId;
                db.ListingImages.Add(Image);
                db.SaveChanges();
            }
        
        }

    }
}
