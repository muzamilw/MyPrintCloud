using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Repository.Repositories
{
    class ListingRepository : BaseRepository<Listing>, IListingRepository
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
        protected override IDbSet<Listing> DbSet
        {
            get
            {
                return db.Listings;
            }
        }

        #endregion

        #region public

        public List<Listing> GetRealEstateProperties()
        {
            List<Listing> lstListings = db.Listings.ToList();

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

        public Listing GetListingByListingId(long listingId)
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

        public List<FieldVariable> GetVariablesListWithValues(long listingId, long itemId, out List<MPC.Models.Common.TemplateVariable> lstVariableAndValue, out List<MPC.Models.Common.TemplateVariable> lstGeneralVariables, out List<string> lstListingImages, out List<VariableSection> lstSectionsName)
        {
            List<FieldVariable> lstFieldVariable = new List<FieldVariable>();
            lstGeneralVariables = new List<MPC.Models.Common.TemplateVariable>();
            lstVariableAndValue = new List<MPC.Models.Common.TemplateVariable>();
            lstListingImages = new List<string>();
            lstSectionsName = new List<VariableSection>();

            try
            {
                Listing listing = GetListingByListingId(listingId);
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
                            var keyValue = 0;
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
                                    keyValue = 0;//SessionParameters.CustomerContact.ContactID;
                                    fieldValue = DynamicQueryToGetRecord(item.CriteriaFieldName, item.RefTableName, item.KeyField, keyValue);
                                    break;
                                case "Company":
                                    keyValue = 0;//UserCookieManager.StoreId;
                                    fieldValue = DynamicQueryToGetRecord(item.CriteriaFieldName, item.RefTableName, item.KeyField, keyValue);
                                    break;
                                case "Address":
                                    keyValue = 0;//SessionParameters.CustomerContact.AddressID;
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

                                    lstListingImages.Add(fieldValue);
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
                            int keyValue = 0;
                            string fieldValue = string.Empty;

                            switch (item.RefTableName)
                            {
                                case "CompanyContact":
                                    keyValue = 0;//SessionParameters.CustomerContact.ContactID;
                                    fieldValue = DynamicQueryToGetRecord(item.CriteriaFieldName, item.RefTableName, item.KeyField, keyValue);
                                    break;
                                case "Company":
                                    keyValue = 0;//SessionParameters.ContactCompany.ContactCompanyID;
                                    fieldValue = DynamicQueryToGetRecord(item.CriteriaFieldName, item.RefTableName, item.KeyField, keyValue);
                                    break;
                                case "Address":
                                    keyValue = 0;//SessionParameters.CustomerContact.AddressID;
                                    fieldValue = DynamicQueryToGetRecord(item.CriteriaFieldName, item.RefTableName, item.KeyField, keyValue);
                                    break;
                                case "SectionFlag":
                                    //using (MPCEntities dbContext = new MPCEntities())
                                    //{
                                    //    keyValue = SessionParameters.ContactCompany.FlagID;
                                    //    fieldValue = DynamicQueryToGetRecord(item.CriteriaFieldName, item.RefTableName, item.KeyField, keyValue);
                                    //}
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
    }
}
