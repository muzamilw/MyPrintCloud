using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using MPC.Common;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Repository.Repositories
{
    public class SmartFormRepository : BaseRepository<SmartForm>, ISmartFormRepository
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SmartFormRepository(IUnityContainer container)
            : base(container)
        {

        }
        protected override IDbSet<SmartForm> DbSet
        {
            get
            {
                return db.SmartForms;
            }
        }



        #endregion

        #region public

        /// <summary>
        /// Find Template
        /// </summary>
        public SmartForm Find(int id)
        {
            return DbSet.Find(id);
        }


        public List<VariableList> GetVariablesData(bool isRealestateproduct, long companyId, long organisationId)
        {
            List<VariableList> resultList = new List<VariableList>();
            if (isRealestateproduct)
            {
                var objList = from p in db.VariableSections
                              join es in db.FieldVariables on p.VariableSectionId equals es.VariableSectionId
                              where (es.IsSystem == true || (es.CompanyId == companyId && es.OrganisationId == organisationId))
                              orderby p.VariableSectionId, es.VariableType, es.SortOrder
                              select new
                              {
                                  SectionName = p.SectionName,
                                  VariableID = es.VariableId,
                                  VariableName = es.VariableName,
                                  VariableTag = es.VariableTag,
                                  VariableType = es.Scope

                              };
                var listItems = objList.ToList();
                foreach (var obj in listItems)
                {
                    VariableList objVarList = new VariableList(obj.SectionName, obj.VariableID, obj.VariableName, obj.VariableTag, obj.VariableType.Value);
                    resultList.Add(objVarList);
                }
            }
            else
            {
                var objList = from p in db.VariableSections
                              join es in db.FieldVariables on p.VariableSectionId equals es.VariableSectionId
                              where ((es.IsSystem == true || (es.CompanyId == companyId && es.OrganisationId == organisationId)) && (es.Scope != (int)FieldVariableScopeType.RealEstate || es.Scope != (int)FieldVariableScopeType.RealEstateImages))
                              orderby p.VariableSectionId, es.VariableType, es.SortOrder
                              select new
                              {
                                  SectionName = p.SectionName,
                                  VariableID = es.VariableId,
                                  VariableName = es.VariableName,
                                  VariableTag = es.VariableTag,
                                  VariableType = es.Scope

                              };
                var listItems = objList.ToList();
                foreach (var obj in listItems)
                {
                    VariableList objVarList = new VariableList(obj.SectionName, obj.VariableID, obj.VariableName, obj.VariableTag, obj.VariableType);
                    resultList.Add(objVarList);
                }
            }


            return resultList;
            // return db.FieldVariables.Take(10).ToList();
        }
        public List<TemplateVariablesObj> GetTemplateVariables(long templateId)
        {
            var objList = from p in db.TemplateVariables
                          join es in db.FieldVariables on p.VariableId equals es.VariableId
                          where (p.TemplateId == templateId)
                          orderby es.VariableType, es.SortOrder
                          select new
                          {
                              VariableTag = es.VariableTag,
                              VariableID = p.VariableId,
                              TemplateID = p.TemplateId

                          };
            List<TemplateVariablesObj> objResult = new List<TemplateVariablesObj>();
            foreach (var obj in objList.ToList())
            {
                TemplateVariablesObj objToAdd = new TemplateVariablesObj(obj.VariableTag, obj.VariableID.Value, obj.TemplateID.Value);

                objResult.Add(objToAdd);
            }
            return objResult;
        }

        public bool SaveTemplateVariables(List<TemplateVariablesObj> obj)
        {
            try
            {
                List<long> alreadyAdded = new List<long>();
                long templateID = 0;
                if (obj != null)
                {
                    if (obj.Count > 0)
                    {
                        templateID = obj[0].TemplateID;
                    }
                    //  objSettings = objSettings.DistinctBy().ToList();
                    foreach (var objToRemove in db.TemplateVariables.Where(w => w.TemplateId == templateID).ToList())
                    {
                        db.TemplateVariables.Remove(objToRemove);
                    }
                    foreach (var item in obj)
                    {
                        if (!alreadyAdded.Contains(item.VariableID))
                        {
                            alreadyAdded.Add(item.VariableID);
                            MPC.Models.DomainModels.TemplateVariable objToAdd = new MPC.Models.DomainModels.TemplateVariable();
                            objToAdd.TemplateId = item.TemplateID;
                            objToAdd.VariableId = item.VariableID;
                            db.TemplateVariables.Add(objToAdd);
                        }
                    }
                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public List<SmartFormUserList> GetUsersList(long contactId)
        {

            db.Configuration.LazyLoadingEnabled = false;
            var currentContact = db.CompanyContacts.Where(g => g.ContactId == contactId).SingleOrDefault();
            List<SmartFormUserList> objUsers = null;
            if (currentContact != null)
            {
               
                List<CompanyContact> contacts = new List<CompanyContact>();
                if (currentContact.ContactRoleId == (int)Roles.Adminstrator)
                {

                    contacts = (from c in db.CompanyContacts//.Include("tbl_ContactCompanyTerritories").Include("tbl_ContactDepartments")
                                where c.CompanyId == currentContact.CompanyId
                                select c).ToList();
                }
                else if (currentContact.ContactRoleId == (int)Roles.Manager)
                {
                    contacts = db.CompanyContacts.Where(i => i.TerritoryId == currentContact.TerritoryId).ToList();
                }
                else
                {
                 //   contacts.Add(currentContact);
                }
                if (contacts.Count > 0)
                {
                    objUsers = new List<SmartFormUserList>();
                    foreach (var contact in contacts)
                    {
                        SmartFormUserList objUser = new SmartFormUserList(contact.ContactId, (contact.FirstName + contact.LastName));
                        objUsers.Add(objUser);
                    }
                }
            }
            return objUsers;
        }
         public SmartForm GetSmartForm(long smartFormId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;

            SmartForm smartFormObj =  db.SmartForms.Where(g => g.SmartFormId == smartFormId).SingleOrDefault();
            if(smartFormObj != null)
             smartFormObj.SmartFormDetails = null;
            return smartFormObj;
        }

        public List<SmartFormDetail> GetSmartFormObjects(long smartFormId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;

            List<SmartFormDetail> objs = db.SmartFormDetails.Include("FieldVariable.VariableOptions").Where(g => g.SmartFormId == smartFormId).OrderBy(g => g.SortOrder).ToList();
            foreach (var obj in objs) { obj.SmartForm = null; };
            
            return objs;
        }

        public List<ScopeVariable> GetScopeVariables(List<SmartFormDetail> smartFormDetails, out bool hasContactVariables,long contactId)
        {
            List<ScopeVariable> result = new List<ScopeVariable>();
            hasContactVariables = false;
            var contact = db.CompanyContacts.Where(g => g.ContactId == contactId).SingleOrDefault();
            foreach(SmartFormDetail obj in smartFormDetails)
            {
                if(obj.ObjectType == (int)SmartFormDetailFieldType.VariableField)
                {
                    if(obj.FieldVariable.IsSystem.HasValue && obj.FieldVariable.IsSystem.Value == true)
                    {
                        
                        var fieldValue = "";
                        if (contact != null)
                        {


                            switch (obj.FieldVariable.RefTableName)
                            {
                                case "tbl_Listing":
                                    //   fieldValue = Convert.ToString(listing.GetType().GetProperty(item.CriteriaFieldName).GetValue(listing, null));
                                    break;
                                case "tbl_ListingImage":

                                    //if (listingImages.Count > listingImageCount)
                                    //{
                                    //    fieldValue = Convert.ToString(listingImages[listingImageCount].GetType().GetProperty(item.CriteriaFieldName).GetValue(listingImages[listingImageCount], null));
                                    //}
                                    //else
                                    //{
                                    //    listingImageCount++;
                                    //    listingOverflow = true;
                                    //    childType = "image";
                                    //    break;
                                    //}

                                    //isChildList = true;
                                    //childType = "image";
                                    //listingImageCount++;
                                    break;
                                case "tbl_ListingAgent":

                                    //if (listingAgents.Count > listingAgentCount)
                                    //{
                                    //    fieldValue = Convert.ToString(listingAgents[listingAgentCount].GetType().GetProperty(item.CriteriaFieldName).GetValue(listingAgents[listingAgentCount], null));
                                    //}
                                    //else
                                    //{
                                    //    listingAgentCount++;
                                    //    listingOverflow = true;
                                    //    break;
                                    //}

                                    //isChildList = true;
                                    //childType = "agent";
                                    //listingAgentCount++;
                                    break;
                                case "tbl_ListingOFID":

                                    //if (listingOFIDs.Count > listingOFIDCount)
                                    //{
                                    //    fieldValue = Convert.ToString(listingOFIDs[listingOFIDCount].GetType().GetProperty(item.CriteriaFieldName).GetValue(listingOFIDs[listingOFIDCount], null));
                                    //}
                                    //else
                                    //{
                                    //    listingOFIDCount++;
                                    //    listingOverflow = true;
                                    //    break;
                                    //}

                                    //isChildList = true;
                                    //childType = "ofi";
                                    //listingOFIDCount++;
                                    break;
                                case "tbl_ListingVendor":

                                    //if (listingVendors.Count > listingVendrosCount)
                                    //{
                                    //    fieldValue = Convert.ToString(listingVendors[listingVendrosCount].GetType().GetProperty(item.CriteriaFieldName).GetValue(listingVendors[listingVendrosCount], null));
                                    //}
                                    //else
                                    //{
                                    //    listingVendrosCount++;
                                    //    listingOverflow = true;
                                    //    break;
                                    //}

                                    //isChildList = true;
                                    //childType = "vendor";
                                    //listingVendrosCount++;
                                    break;
                                case "tbl_ListingLink":

                                    //if (listingLinks.Count > listingLinkCount)
                                    //{
                                    //    fieldValue = Convert.ToString(listingLinks[listingLinkCount].GetType().GetProperty(item.CriteriaFieldName).GetValue(listingLinks[listingLinkCount], null));
                                    //}
                                    //else
                                    //{
                                    //    listingLinkCount++;
                                    //    listingOverflow = true;
                                    //    break;
                                    //}

                                    //isChildList = true;
                                    //childType = "link";
                                    //listingLinkCount++;
                                    break;
                                case "tbl_ListingFloorPlan":

                                    //if (listingFloorPlans.Count > listingFloorPlansCount)
                                    //{
                                    //    fieldValue = Convert.ToString(listingFloorPlans[listingFloorPlansCount].GetType().GetProperty(item.CriteriaFieldName).GetValue(listingFloorPlans[listingFloorPlansCount], null));
                                    //}
                                    //else
                                    //{
                                    //    listingFloorPlansCount++;
                                    //    listingOverflow = true;
                                    //    break;
                                    //}

                                    //isChildList = true;
                                    //childType = "floor";
                                    //listingFloorPlansCount++;
                                    break;
                                case "tbl_ListingConjunctionAgent":

                                    //if (listingConjuctionAgents.Count > listingConAgentCount)
                                    //{
                                    //    fieldValue = Convert.ToString(listingConjuctionAgents[listingConAgentCount].GetType().GetProperty(item.CriteriaFieldName).GetValue(listingConjuctionAgents[listingConAgentCount], null));
                                    //}
                                    //else
                                    //{
                                    //    listingConAgentCount++;
                                    //    listingOverflow = true;
                                    //    break;
                                    //}

                                    //isChildList = true;
                                    //childType = "conAgent";
                                    //listingConAgentCount++;
                                    break;
                                case "CompanyContact":
                                    hasContactVariables = true;
                                    fieldValue = DynamicQueryToGetRecord(obj.FieldVariable.CriteriaFieldName, obj.FieldVariable.RefTableName, obj.FieldVariable.KeyField, contactId);
                                    break;
                                case "Company":
                                    //   keyValue = SessionParameters.ContactCompany.ContactCompanyID;
                                    fieldValue = DynamicQueryToGetRecord(obj.FieldVariable.CriteriaFieldName, obj.FieldVariable.RefTableName, obj.FieldVariable.KeyField, contact.CompanyId);
                                    break;
                                case "Address":
                                    //  keyValue = SessionParameters.CustomerContact.AddressID;
                                    fieldValue = DynamicQueryToGetRecord(obj.FieldVariable.CriteriaFieldName, obj.FieldVariable.RefTableName, obj.FieldVariable.KeyField, contact.AddressId);
                                    break;
                                default:
                                    break;
                            }
                            ScopeVariable objScopeVariable = new ScopeVariable();
                            objScopeVariable.Scope = 0;
                            objScopeVariable.VariableId = obj.FieldVariable.VariableId;
                            objScopeVariable.Value = fieldValue;
                            result.Add(objScopeVariable);
                        }
                    }
                    else
                    {
                        if (obj.FieldVariable != null && obj.FieldVariable.Scope.HasValue)
                        {
                            int scope = obj.FieldVariable.Scope.Value;
                            if (scope == (int)FieldVariableScopeType.Address)
                            {
                                // address logic will go here
                            }
                            else if (scope == (int)FieldVariableScopeType.Contact)
                            {
                                var scopeObj = db.ScopeVariables.Where(g => g.VariableId == obj.FieldVariable.VariableId && g.Id == contactId).SingleOrDefault();
                                if (scopeObj != null)
                                {
                                    result.Add(scopeObj);
                                    hasContactVariables = true;
                                }
                            }
                            else if (scope == (int)FieldVariableScopeType.RealEstate)
                            {
                                // realestate logic 
                            }
                            else if (scope == (int)FieldVariableScopeType.RealEstateImages)
                            {
                                // realestate logic 
                            }
                            else if (scope == (int)FieldVariableScopeType.Store)
                            {
                                var scopeObj = db.ScopeVariables.Where(g => g.VariableId == obj.FieldVariable.VariableId && g.Id == obj.FieldVariable.CompanyId).SingleOrDefault();
                                if (scopeObj != null)
                                {
                                    result.Add(scopeObj);
                                }
                            }
                            else if (scope == (int)FieldVariableScopeType.Territory)
                            {
                               // var contact = db.CompanyContacts.Where(g => g.ContactId == contactId).SingleOrDefault();
                                if (contact != null)
                                {
                                    var scopeObj = db.ScopeVariables.Where(g => g.VariableId == obj.FieldVariable.VariableId && g.Id == contact.TerritoryId).SingleOrDefault();
                                    if (scopeObj != null)
                                    {
                                        result.Add(scopeObj);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Get Smart Form By Company Id
        /// </summary>
        public SmartFormResponse GetSmartForms(SmartFormRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            Expression<Func<SmartForm, bool>> query =
            s =>
                (s.CompanyId == request.CompanyId);

            int rowCount = DbSet.Count(query);
            IEnumerable<SmartForm> smartForms = request.IsAsc
           ? DbSet.Where(query)
           .OrderByDescending(x => x.Name)
               .Skip(fromRow)
               .Take(toRow)
               .ToList()
           : DbSet.Where(query)
           .OrderByDescending(x => x.Name)
               .Skip(fromRow)
               .Take(toRow)
               .ToList();
            return new SmartFormResponse
            {
                RowCount = rowCount,
                SmartForms = smartForms
            };
        }
        public string DynamicQueryToGetRecord(string feildname, string tblname, string keyName, long keyValue)
        {

            string oResult = null;
            System.Data.Entity.Infrastructure.DbRawSqlQuery<string> result = db.Database.SqlQuery<string>("select top 1 cast(" + feildname + " as varchar(1000)) from " + tblname + " where " + keyName + "= " + keyValue + "", "");
            oResult = result.FirstOrDefault();
            return oResult;
        }
        public string DynamicQueryToSetRecord(string feildname, string tblname, string keyName, long keyValue,long newValue)
        {

            string oResult = null;
            System.Data.Entity.Infrastructure.DbRawSqlQuery<string> result = db.Database.SqlQuery<string>("select top 1 cast(" + feildname + " as varchar(1000)) from " + tblname + " where " + keyName + "= " + keyValue + "", "");
            oResult = result.FirstOrDefault();
            return oResult;
        }

        public Dictionary<long, List<ScopeVariable>> GetUserScopeVariables(List<SmartFormDetail> smartFormDetails,List<SmartFormUserList> contacts) {
            bool hasContactVariables = false;
            Dictionary<long, List<ScopeVariable>> UserScopeVariables = new Dictionary<long, List<ScopeVariable>>();
            foreach(var contact in contacts)
            {
                List<ScopeVariable> variables = GetScopeVariables(smartFormDetails, out hasContactVariables, contact.ContactId);
                UserScopeVariables.Add(contact.ContactId, variables);
            }
            return UserScopeVariables;
        }
        public bool SaveUserProfilesData(Dictionary<long, List<ScopeVariable>> obj)
        {
            bool result = false;
            //foreach ( var item in obj)
            //{
            //    long contactId = item.Key;
            //    List<ScopeVariable> contactVariables = item.Value;
            //    var contact = db.CompanyContacts.Where(g => g.ContactId == contactId).SingleOrDefault();
            //    foreach(var scope in contactVariables)
            //    {
            //        FieldVariable variable = db.FieldVariables.Where(g => g.VariableId == scope.VariableId).SingleOrDefault();
            //        if (variable != null)
            //        {
            //            if (variable.IsSystem.HasValue && variable.IsSystem.Value == true)
            //            {
            //                var fieldValue = "";
            //                if (contact != null)
            //                {
            //                    switch (variable.RefTableName)
            //                    {
            //                            case "tbl_Listing":
            //                                break;
            //                            case "tbl_ListingImage":
            //                                break;
            //                            case "tbl_ListingAgent":
            //                                break;
            //                            case "tbl_ListingOFID":
            //                                break;
            //                            case "tbl_ListingVendor":
            //                                break;
            //                            case "tbl_ListingLink":
            //                                break;
            //                            case "tbl_ListingFloorPlan":
            //                                break;
            //                            case "tbl_ListingConjunctionAgent":
            //                                break;
            //                            case "CompanyContact":
            //                                fieldValue = DynamicQueryToGetRecord(obj.FieldVariable.CriteriaFieldName, obj.FieldVariable.RefTableName, obj.FieldVariable.KeyField, contactId);
            //                                break;
            //                            case "Company":
            //                                //   keyValue = SessionParameters.ContactCompany.ContactCompanyID;
            //                                fieldValue = DynamicQueryToGetRecord(obj.FieldVariable.CriteriaFieldName, obj.FieldVariable.RefTableName, obj.FieldVariable.KeyField, contact.CompanyId);
            //                                break;
            //                            case "Address":
            //                                //  keyValue = SessionParameters.CustomerContact.AddressID;
            //                                fieldValue = DynamicQueryToGetRecord(obj.FieldVariable.CriteriaFieldName, obj.FieldVariable.RefTableName, obj.FieldVariable.KeyField, contact.AddressId);
            //                                break;
            //                            default:
            //                                break;
            //                        }
            //                        ScopeVariable objScopeVariable = new ScopeVariable();
            //                        objScopeVariable.Scope = 0;
            //                        objScopeVariable.VariableId = obj.FieldVariable.VariableId;
            //                        objScopeVariable.Value = fieldValue;
            //                        result.Add(objScopeVariable);
            //                    }
            //                }
            //                else
            //                {
            //                    if (obj.FieldVariable != null && obj.FieldVariable.Scope.HasValue)
            //                    {
            //                        int scope = obj.FieldVariable.Scope.Value;
            //                        if (scope == (int)FieldVariableScopeType.Address)
            //                        {
            //                            // address logic will go here
            //                        }
            //                        else if (scope == (int)FieldVariableScopeType.Contact)
            //                        {
            //                            var scopeObj = db.ScopeVariables.Where(g => g.VariableId == obj.FieldVariable.VariableId && g.Id == contactId).SingleOrDefault();
            //                            if (scopeObj != null)
            //                            {
            //                                result.Add(scopeObj);
            //                                hasContactVariables = true;
            //                            }
            //                        }
            //                        else if (scope == (int)FieldVariableScopeType.RealEstate)
            //                        {
            //                            // realestate logic 
            //                        }
            //                        else if (scope == (int)FieldVariableScopeType.RealEstateImages)
            //                        {
            //                            // realestate logic 
            //                        }
            //                        else if (scope == (int)FieldVariableScopeType.Store)
            //                        {
            //                            var scopeObj = db.ScopeVariables.Where(g => g.VariableId == obj.FieldVariable.VariableId && g.Id == obj.FieldVariable.CompanyId).SingleOrDefault();
            //                            if (scopeObj != null)
            //                            {
            //                                result.Add(scopeObj);
            //                            }
            //                        }
            //                        else if (scope == (int)FieldVariableScopeType.Territory)
            //                        {
            //                            // var contact = db.CompanyContacts.Where(g => g.ContactId == contactId).SingleOrDefault();
            //                            if (contact != null)
            //                            {
            //                                var scopeObj = db.ScopeVariables.Where(g => g.VariableId == obj.FieldVariable.VariableId && g.Id == contact.TerritoryId).SingleOrDefault();
            //                                if (scopeObj != null)
            //                                {
            //                                    result.Add(scopeObj);
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
                        
            //        }
            //    }

            //}
            return result;
        }
        #endregion
    }
}
