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
using System.Web;

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

        /// <summary>
        /// Get All Smart Froms
        /// </summary>
        public IEnumerable<SmartForm> GetAllForCompany(long companyId)
        {
            return DbSet.Where(sm => sm.CompanyId == companyId).OrderBy(sm => sm.Name).ToList();
        }

        public List<VariableList> GetVariablesData(long itemID, long companyId, long organisationId)
        {
            bool isRealestateproduct = false;
            Item item = db.Items.Where(g => g.ItemId == itemID).SingleOrDefault();
            if(item != null)
            {
                if (item.IsRealStateProduct.HasValue)
                    isRealestateproduct = item.IsRealStateProduct.Value;
            }
            List<VariableList> resultList = new List<VariableList>();
            if (isRealestateproduct)
            {
                var objList = from p in db.VariableSections
                              join es in db.FieldVariables on p.VariableSectionId equals es.VariableSectionId
                              where ((es.IsSystem == true || (es.CompanyId == companyId && es.OrganisationId == organisationId)))
                              orderby p.VariableSectionId, es.VariableTag, es.VariableType, es.SortOrder
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
                              where ((es.IsSystem == true || (es.CompanyId == companyId && es.OrganisationId == organisationId)) && (es.Scope == (int)FieldVariableScopeType.SystemAddress || es.Scope == (int)FieldVariableScopeType.SystemContact || es.Scope == (int)FieldVariableScopeType.SystemStore || es.Scope == (int)FieldVariableScopeType.SystemTerritory))
                              orderby p.VariableSectionId,es.VariableTag, es.VariableType, es.SortOrder
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

            var customVariables = db.FieldVariables.Where(g => g.CompanyId == companyId && (g.IsSystem == null || g.IsSystem == false)).ToList();
            foreach (var customVar in customVariables)
            {
                VariableList objVarList = new VariableList("Custom CRM Fields", customVar.VariableId, customVar.VariableName, customVar.VariableTag, customVar.VariableType);
                resultList.Add(objVarList);
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
                              TemplateID = p.TemplateId,
                              VariableText = p.VariableText

                          };
            List<TemplateVariablesObj> objResult = new List<TemplateVariablesObj>();
            foreach (var obj in objList.ToList())
            {
                TemplateVariablesObj objToAdd = new TemplateVariablesObj(obj.VariableTag, obj.VariableID.Value, obj.TemplateID.Value,obj.VariableText);

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
                            objToAdd.VariableText = item.VariableText;
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
                        SmartFormUserList objUser = new SmartFormUserList(contact.ContactId, (contact.FirstName +" " +  contact.LastName));
                        objUsers.Add(objUser);
                    }
                }
            }
            return objUsers;
        }
        public SmartFormWebstoreResponse GetSmartForm(long smartFormId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;

            SmartForm smartFormObj =  db.SmartForms.Where(g => g.SmartFormId == smartFormId).SingleOrDefault();
            if(smartFormObj != null)
             smartFormObj.SmartFormDetails = null;
            if (smartFormObj != null)
                smartFormObj.Company = null;


            SmartFormWebstoreResponse res = new SmartFormWebstoreResponse();
            res.CompanyId = smartFormObj.CompanyId;
            res.Heading = smartFormObj.Heading;
            res.Name = smartFormObj.Name;
            res.OrganisationId = smartFormObj.OrganisationId;
            res.SmartFormId = smartFormObj.SmartFormId;

            return res;
        }

        public List<SmartFormDetail> GetSmartFormObjects(long smartFormId,out List<VariableOption> listVariables)
        {
            List<VariableOption> listOptions = new List<VariableOption>();
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;

            List<SmartFormDetail> objs = db.SmartFormDetails.Include("FieldVariable.VariableOptions").Where(g => g.SmartFormId == smartFormId).OrderBy(g => g.SortOrder).ToList();
            foreach (var obj in objs) { 
                obj.SmartForm = null;
                if (obj.FieldVariable != null)
                {
                    if (obj.FieldVariable.VariableOptions != null)
                    {
                        listOptions.AddRange(obj.FieldVariable.VariableOptions);
                    }
                    obj.FieldVariable.Company = null;
                }
            };
            foreach(var option in listOptions)
            {
                option.FieldVariable = null;
            }
            listVariables = listOptions;
            return objs;
        }

        public List<ScopeVariable> GetScopeVariables(List<SmartFormDetail> smartFormDetails, out bool hasContactVariables,long contactId,long templateId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            List<ScopeVariable> result = new List<ScopeVariable>();
            hasContactVariables = false;
            var contact = db.CompanyContacts.Where(g => g.ContactId == contactId).SingleOrDefault();
            long ListingId = 0;
            foreach(SmartFormDetail obj in smartFormDetails)
            {
                if(obj.ObjectType == (int)SmartFormDetailFieldType.VariableField)
                {
                    if(obj.FieldVariable.IsSystem.HasValue && obj.FieldVariable.IsSystem.Value == true)
                    {
                        
                        var fieldValue = "";
                        if (contact != null)
                        {

                            obj.FieldVariable.Company = null;
                            obj.FieldVariable.ScopeVariables = null;
                            obj.FieldVariable.SmartFormDetails = null;
                            obj.FieldVariable.TemplateVariables = null;
                            obj.FieldVariable.VariableExtensions = null;
                            switch (obj.FieldVariable.RefTableName)
                            {
                                case "Listing":
                                    fieldValue = DynamicQueryToGetRecord(obj.FieldVariable.CriteriaFieldName, obj.FieldVariable.RefTableName, obj.FieldVariable.KeyField, ListingId);
                                    break;
                                case "ListingImage":  // listing images table based on listing id and image count write a seperate service for it to return all the data 
                                    // fieldValue = GetRealEstateAgent(obj, ListingId);
                                    break;
                                case "ListingAgent": //from users table based on company id and agent count
                                    fieldValue = GetRealEstateAgent(obj.FieldVariable, ListingId);
                                    break;
                                case "ListingOFIs":
                                    fieldValue = DynamicQueryToGetRecord(obj.FieldVariable.CriteriaFieldName, obj.FieldVariable.RefTableName, obj.FieldVariable.KeyField, ListingId);
                                    break;
                                case "ListingVendor":
                                    fieldValue = DynamicQueryToGetRecord(obj.FieldVariable.CriteriaFieldName, obj.FieldVariable.RefTableName, obj.FieldVariable.KeyField, ListingId);
                                    break;
                                case "ListingLink":
                                    fieldValue = DynamicQueryToGetRecord(obj.FieldVariable.CriteriaFieldName, obj.FieldVariable.RefTableName, obj.FieldVariable.KeyField, ListingId);
                                    break;
                                case "ListingFloorPlan":
                                    fieldValue = DynamicQueryToGetRecord(obj.FieldVariable.CriteriaFieldName, obj.FieldVariable.RefTableName, obj.FieldVariable.KeyField, ListingId);
                                    break;
                                case "ListingConjunctionAgent":
                                    fieldValue = DynamicQueryToGetRecord(obj.FieldVariable.CriteriaFieldName, obj.FieldVariable.RefTableName, obj.FieldVariable.KeyField, ListingId);
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
                                    if (obj.FieldVariable.CriteriaFieldName == "State")
                                    {
                                        var address = db.Addesses.Where(g => g.AddressId == contact.AddressId).SingleOrDefault();
                                        if(address != null)
                                        {
                                            if(address.StateId.HasValue)
                                            {
                                                var state = db.States.Where(g => g.StateId == address.StateId.Value).SingleOrDefault();
                                                if(state != null)
                                                {
                                                    fieldValue = state.StateName;
                                                }
                                            }
                                                
                                        }
                                    }
                                    else if (obj.FieldVariable.CriteriaFieldName == "StateAbbr")
                                    {
                                        var address = db.Addesses.Where(g => g.AddressId == contact.AddressId).SingleOrDefault();
                                        if (address != null)
                                        {
                                            if (address.StateId.HasValue)
                                            {
                                                var state = db.States.Where(g => g.StateId == address.StateId.Value).SingleOrDefault();
                                                if (state != null)
                                                {
                                                    fieldValue = state.StateCode;
                                                }
                                            }

                                        }
                                    }
                                    else if (obj.FieldVariable.CriteriaFieldName == "Country")
                                    {
                                        var address = db.Addesses.Where(g => g.AddressId == contact.AddressId).SingleOrDefault();
                                        if (address != null)
                                        {
                                            if (address.CountryId.HasValue)
                                            {
                                                var country = db.Countries.Where(g => g.CountryId == address.CountryId.Value).SingleOrDefault();
                                                if (country != null)
                                                {
                                                    fieldValue = country.CountryName;
                                                }
                                            }

                                        }
                                    }
                                    else
                                    {
                                        fieldValue = DynamicQueryToGetRecord(obj.FieldVariable.CriteriaFieldName, obj.FieldVariable.RefTableName, obj.FieldVariable.KeyField, contact.AddressId);
                                    }
                                    break;
                                default:
                                    break;
                            }
                            ScopeVariable objScopeVariable = new ScopeVariable();
                            objScopeVariable.Scope = 0;
                            objScopeVariable.VariableId = obj.FieldVariable.VariableId;
                            objScopeVariable.Value = fieldValue;
                            obj.FieldVariable.Company = null;
                            obj.FieldVariable.ScopeVariables = null;
                            obj.FieldVariable.SmartFormDetails = null;
                            obj.FieldVariable.TemplateVariables = null;
                            obj.FieldVariable.VariableExtensions = null;
                            objScopeVariable.FieldVariable = obj.FieldVariable;
                            if(obj != null)
                                result.Add(objScopeVariable);
                        }
                    }
                    else
                    {
                        if (obj.FieldVariable != null && obj.FieldVariable.Scope.HasValue)
                        {
                             obj.FieldVariable.Company = null;
                            obj.FieldVariable.ScopeVariables = null;
                            obj.FieldVariable.SmartFormDetails = null;
                            obj.FieldVariable.TemplateVariables = null;
                            obj.FieldVariable.VariableExtensions = null;

                            int scope = obj.FieldVariable.Scope.Value;
                            if (scope == (int)FieldVariableScopeType.Address)
                            {
                                var scopeObj = db.ScopeVariables.Where(g => g.VariableId == obj.FieldVariable.VariableId && g.Id == contact.AddressId).FirstOrDefault();
                                if (scopeObj != null)
                                {
                                    if (scopeObj != null)
                                        result.Add(scopeObj);
                                }
                                else
                                {
                                    ScopeVariable objScopeVariable = new ScopeVariable();
                                    objScopeVariable.Scope = 0;
                                    objScopeVariable.VariableId = obj.FieldVariable.VariableId;
                                    objScopeVariable.Value = obj.FieldVariable.DefaultValue;
                                    objScopeVariable.Id = contact.AddressId;
                                    objScopeVariable.Scope = scope;

                                    objScopeVariable.FieldVariable = obj.FieldVariable;
                                    if (objScopeVariable != null)
                                        result.Add(objScopeVariable);
                                }
                            }
                            else if (scope == (int)FieldVariableScopeType.Contact)
                            {
                                var scopeObj = db.ScopeVariables.Where(g => g.VariableId == obj.FieldVariable.VariableId && g.Id == contactId).FirstOrDefault();
                                if (scopeObj != null)
                                {
                                    if (scopeObj != null)
                                        result.Add(scopeObj);
                                    hasContactVariables = true;
                                } else
                                {
                                    ScopeVariable objScopeVariable = new ScopeVariable();
                                    objScopeVariable.Scope = 0;
                                    objScopeVariable.VariableId = obj.FieldVariable.VariableId;
                                    objScopeVariable.Value = obj.FieldVariable.DefaultValue;
                                    objScopeVariable.Id = contactId;
                                    objScopeVariable.Scope = scope;
                                    objScopeVariable.FieldVariable = obj.FieldVariable;
                                    if (objScopeVariable != null)
                                        result.Add(objScopeVariable);
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
                                var scopeObj = db.ScopeVariables.Where(g => g.VariableId == obj.FieldVariable.VariableId && g.Id == obj.FieldVariable.CompanyId).FirstOrDefault();
                                if (scopeObj != null)
                                {
                                    result.Add(scopeObj);

                                }else
                                {
                                    ScopeVariable objScopeVariable = new ScopeVariable();
                                    objScopeVariable.Scope = 0;
                                    objScopeVariable.VariableId = obj.FieldVariable.VariableId;
                                    objScopeVariable.Value = obj.FieldVariable.DefaultValue;
                                    objScopeVariable.Id = obj.FieldVariable.CompanyId.Value;
                                    objScopeVariable.Scope = scope;
                                    objScopeVariable.FieldVariable = obj.FieldVariable;
                                    if (objScopeVariable != null)
                                        result.Add(objScopeVariable);
                                }
                            }
                            else if (scope == (int)FieldVariableScopeType.Territory)
                            {
                               // var contact = db.CompanyContacts.Where(g => g.ContactId == contactId).SingleOrDefault();
                                if (contact != null)
                                {
                                    var scopeObj = db.ScopeVariables.Where(g => g.VariableId == obj.FieldVariable.VariableId && g.Id == contact.TerritoryId).FirstOrDefault();
                                    if (scopeObj != null)
                                    {
                                        if (scopeObj != null)
                                            result.Add(scopeObj);
                                    }
                                } else
                                {
                                    ScopeVariable objScopeVariable = new ScopeVariable();
                                    objScopeVariable.Scope = 0;
                                    objScopeVariable.VariableId = obj.FieldVariable.VariableId;
                                    objScopeVariable.Value = obj.FieldVariable.DefaultValue;
                                    objScopeVariable.Id = contact.TerritoryId.Value;
                                    objScopeVariable.Scope = scope;
                                    objScopeVariable.FieldVariable = obj.FieldVariable;
                                    if (objScopeVariable != null)
                                        result.Add(objScopeVariable);
                                }
                            }
                        }
                    }
                }
                var template = db.Templates.Where(g => g.ProductId == templateId).SingleOrDefault();
                if (template != null)
                {
                    if (contactId == template.contactId)
                    {
                        List<MPC.Models.DomainModels.TemplateVariable> lstTemplateVariables = db.TemplateVariables.Where(g => g.TemplateId == template.ProductId).ToList();
                        foreach (var objTVar in lstTemplateVariables)
                        {
                            var scopeObj = result.Where(g => g.VariableId == objTVar.VariableId).SingleOrDefault();
                            if (scopeObj != null)
                            {
                                if (objTVar.VariableText != null && objTVar.VariableText != "")
                                    scopeObj.Value = objTVar.VariableText;
                            }
                        }
                    }
                }
            }
            
            return result;
        }
        public List<ScopeVariable> GetTemplateScopeVariables(long templateID, long contactId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            long ListingId = 0;
            List<ScopeVariable> result = new List<ScopeVariable>();
            var contact = db.CompanyContacts.Where(g => g.ContactId == contactId).SingleOrDefault();
            List<MPC.Models.DomainModels.TemplateVariable> lstTemplateVariables = new List<Models.DomainModels.TemplateVariable>();
            List<FieldVariable> lstVariables = new List<FieldVariable>();
            lstTemplateVariables = db.TemplateVariables.Where(g => g.TemplateId == templateID).ToList();
            foreach(var item in lstTemplateVariables)
            {
                var fieldVariable = db.FieldVariables.Where(g => g.VariableId == item.VariableId).SingleOrDefault();
                if(fieldVariable != null)
                {
                    fieldVariable.Company = null;
                    fieldVariable.SmartFormDetails = null;
                    fieldVariable.TemplateVariables = null;
                    fieldVariable.VariableExtensions = null;
                    fieldVariable.VariableOptions = null;
                   
                    lstVariables.Add(fieldVariable);
                }
            }
            foreach(var obj in lstVariables)
            {


                if (obj.IsSystem.HasValue && obj.IsSystem.Value == true)
                {

                    var fieldValue = "";
                    if (contact != null)
                    {
                        switch (obj.RefTableName)
                        {
                            case "Listing":
                                fieldValue = DynamicQueryToGetRecord(obj.CriteriaFieldName, obj.RefTableName, obj.KeyField, ListingId);
                                break;
                            case "ListingImage":  // listing images table based on listing id and image count write a seperate service for it to return all the data 
                               // fieldValue = GetRealEstateAgent(obj, ListingId);
                                break;
                            case "ListingAgent": //from users table based on company id and agent count
                                fieldValue = GetRealEstateAgent(obj, ListingId);
                                break;
                            case "ListingOFIs":
                                fieldValue = DynamicQueryToGetRecord(obj.CriteriaFieldName, obj.RefTableName, obj.KeyField, ListingId);
                                break;
                            case "ListingVendor":
                                fieldValue = DynamicQueryToGetRecord(obj.CriteriaFieldName, obj.RefTableName, obj.KeyField, ListingId);
                                break;
                            case "ListingLink":
                                fieldValue = DynamicQueryToGetRecord(obj.CriteriaFieldName, obj.RefTableName, obj.KeyField, ListingId);
                                break;
                            case "ListingFloorPlan":
                                fieldValue = DynamicQueryToGetRecord(obj.CriteriaFieldName, obj.RefTableName, obj.KeyField, ListingId);
                                break;
                            case "ListingConjunctionAgent":
                                fieldValue = DynamicQueryToGetRecord(obj.CriteriaFieldName, obj.RefTableName, obj.KeyField, ListingId);
                                break;
                            case "CompanyContact":
                                fieldValue = DynamicQueryToGetRecord(obj.CriteriaFieldName, obj.RefTableName, obj.KeyField, contactId);
                                break;
                            case "Company":
                                fieldValue = DynamicQueryToGetRecord(obj.CriteriaFieldName, obj.RefTableName, obj.KeyField, contact.CompanyId);
                                break;
                            case "Address":
                                 if (obj.CriteriaFieldName == "State")
                                    {
                                        var address = db.Addesses.Where(g => g.AddressId == contact.AddressId).SingleOrDefault();
                                        if(address != null)
                                        {
                                            if(address.StateId.HasValue)
                                            {
                                                var state = db.States.Where(g => g.StateId == address.StateId.Value).SingleOrDefault();
                                                if(state != null)
                                                {
                                                    fieldValue = state.StateName;
                                                }
                                            }
                                                
                                        }
                                    }
                                 else if (obj.CriteriaFieldName == "StateAbbr")
                                 {
                                     var address = db.Addesses.Where(g => g.AddressId == contact.AddressId).SingleOrDefault();
                                     if (address != null)
                                     {
                                         if (address.StateId.HasValue)
                                         {
                                             var state = db.States.Where(g => g.StateId == address.StateId.Value).SingleOrDefault();
                                             if (state != null)
                                             {
                                                 fieldValue = state.StateCode;
                                             }
                                         }

                                     }
                                 }
                                 else if (obj.CriteriaFieldName == "Country")
                                 {
                                     var address = db.Addesses.Where(g => g.AddressId == contact.AddressId).SingleOrDefault();
                                     if (address != null)
                                     {
                                         if (address.CountryId.HasValue)
                                         {
                                             var country = db.Countries.Where(g => g.CountryId == address.CountryId.Value).SingleOrDefault();
                                             if (country != null)
                                             {
                                                 fieldValue = country.CountryName;
                                             }
                                         }

                                     }
                                 }
                                 else
                                 {
                                     fieldValue = DynamicQueryToGetRecord(obj.CriteriaFieldName, obj.RefTableName, obj.KeyField, contact.AddressId);
                                 }
                                break;
                            default:
                                break;
                        }
                        ScopeVariable objScopeVariable = new ScopeVariable();
                        objScopeVariable.Scope = 0;
                        objScopeVariable.VariableId = obj.VariableId;
                        objScopeVariable.Value = fieldValue;
                        objScopeVariable.FieldVariable = obj; 
                        result.Add(objScopeVariable);
                    }
                }
                else
                {
                    if (obj != null && obj.Scope.HasValue)
                    {
                        int scope = obj.Scope.Value;
                        if (scope == (int)FieldVariableScopeType.Address)
                        {
                            var scopeObj = db.ScopeVariables.Where(g => g.VariableId == obj.VariableId && g.Id == contact.AddressId).FirstOrDefault();
                            if (scopeObj != null)
                            {
                                result.Add(scopeObj);
                            }
                            else
                            {
                                ScopeVariable objScopeVariable = new ScopeVariable();
                                objScopeVariable.Scope = 0;
                                objScopeVariable.VariableId = obj.VariableId;
                                objScopeVariable.Value = obj.DefaultValue;
                                objScopeVariable.Id = contact.AddressId;
                                objScopeVariable.Scope = scope;
                                objScopeVariable.FieldVariable = obj;
                                result.Add(objScopeVariable);
                            }
                        }
                        else if (scope == (int)FieldVariableScopeType.Contact)
                        {
                            var scopeObj = db.ScopeVariables.Where(g => g.VariableId == obj.VariableId && g.Id == contactId).FirstOrDefault();
                            if (scopeObj != null)
                            {
                                result.Add(scopeObj);
                            }
                            else
                            {
                                ScopeVariable objScopeVariable = new ScopeVariable();
                                objScopeVariable.Scope = 0;
                                objScopeVariable.VariableId = obj.VariableId;
                                objScopeVariable.Value = obj.DefaultValue;
                                objScopeVariable.Id = contactId;
                                objScopeVariable.Scope = scope;
                                objScopeVariable.FieldVariable = obj;
                                result.Add(objScopeVariable);
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
                            var scopeObj = db.ScopeVariables.Where(g => g.VariableId == obj.VariableId && g.Id == obj.CompanyId).FirstOrDefault();
                            if (scopeObj != null)
                            {
                                result.Add(scopeObj);

                            }
                            else
                            {
                                ScopeVariable objScopeVariable = new ScopeVariable();
                                objScopeVariable.Scope = 0;
                                objScopeVariable.VariableId = obj.VariableId;
                                objScopeVariable.Value = obj.DefaultValue;
                                objScopeVariable.Id = obj.CompanyId.Value;
                                objScopeVariable.Scope = scope;
                                objScopeVariable.FieldVariable = obj;
                                result.Add(objScopeVariable);
                            }
                        }
                        else if (scope == (int)FieldVariableScopeType.Territory)
                        {
                            // var contact = db.CompanyContacts.Where(g => g.ContactId == contactId).SingleOrDefault();
                            if (contact != null)
                            {
                                var scopeObj = db.ScopeVariables.Where(g => g.VariableId == obj.VariableId && g.Id == contact.TerritoryId).FirstOrDefault();
                                if (scopeObj != null)
                                {
                                    result.Add(scopeObj);
                                }
                            }
                            else
                            {
                                ScopeVariable objScopeVariable = new ScopeVariable();
                                objScopeVariable.Scope = 0;
                                objScopeVariable.VariableId = obj.VariableId;
                                objScopeVariable.Value = obj.DefaultValue;
                                objScopeVariable.Id = contact.TerritoryId.Value;
                                objScopeVariable.Scope = scope;
                                objScopeVariable.FieldVariable = obj;
                                result.Add(objScopeVariable);
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
        public string DynamicQueryToSetRecord(string feildname, string tblname, string keyName, long keyValue,string newValue)
        {
            var query = "UPDATE " + tblname + "  SET " + feildname + "='" + newValue + "' WHERE " + keyName + " =(SELECT top 1 " + keyName + " FROM " + tblname + "  WHERE " + keyName + "= " + keyValue + ")";
            //var query = "UPDATE " + tblname + "  SET " + feildname + "= '" + newValue + "' WHERE " + keyName + " = " + keyValue ;
            string oResult = null;
            int result = db.Database.ExecuteSqlCommand(query);

         //   System.Data.Entity.Infrastructure.DbRawSqlQuery<string> result = db.Database.SqlQuery<string>(query, "");
            db.SaveChanges();
            //oResult = result.FirstOrDefault();
            return oResult;
        }

        public Dictionary<long, List<ScopeVariable>> GetUserScopeVariables(List<SmartFormDetail> smartFormDetails, List<SmartFormUserList> contacts, long templateId, long currentTemplateId)
        {
            bool hasContactVariables = false;
            db.Configuration.LazyLoadingEnabled = false;
            Dictionary<long, List<ScopeVariable>> UserScopeVariables = new Dictionary<long, List<ScopeVariable>>();
            foreach(var contact in contacts)
            {
                List<ScopeVariable> variables = GetScopeVariables(smartFormDetails, out hasContactVariables, contact.ContactId, currentTemplateId);
                List<ScopeVariable> variablesToRemove = new List<ScopeVariable>();
              //  variablesList = variables;
                foreach (var variable in variables)
                {
                    if (variable == null)
                        variablesToRemove.Add(variable);
                    else
                        if(variable.FieldVariable != null)
                            if(variable.FieldVariable.Company != null)
                               variable.FieldVariable.Company = null;
                }
                foreach(var variable in variablesToRemove)
                {
                    variables.Remove(variable);
                }
                List<ScopeVariable> allTemplateVariables = GetTemplateScopeVariables(templateId, contact.ContactId);
                foreach (var item in allTemplateVariables)
                {
                    var sVariable = variables.Where(g => g.FieldVariable.VariableId == item.FieldVariable.VariableId).FirstOrDefault();
                    if (sVariable == null)
                    {
                        if (item.FieldVariable != null)
                            if (item.FieldVariable.Company != null)
                                item.FieldVariable.Company = null;
                        variables.Add(item);

                    }
                }
                
                UserScopeVariables.Add(contact.ContactId, variables);
            }
            return UserScopeVariables;
        }
      
        public bool SaveUserProfilesData(Dictionary<long, List<ScopeVariable>> obj)
        {
            bool result = false;
            foreach (var item in obj)
            {
                long contactId = item.Key;
                List<ScopeVariable> contactVariables = item.Value;
                var contact = db.CompanyContacts.Where(g => g.ContactId == contactId).SingleOrDefault();
                Company objCompany = null;
                if (contact != null)
                {
                    objCompany = db.Companies.Where(g => g.CompanyId == contact.CompanyId).SingleOrDefault();
                }
                foreach (var scope in contactVariables)
                {
                    FieldVariable variable = db.FieldVariables.Where(g => g.VariableId == scope.VariableId).SingleOrDefault();
                    if (variable != null)
                    {
                        if (variable.IsSystem.HasValue && variable.IsSystem.Value == true)
                        {
                            var fieldValue = "";
                            if (contact != null)
                            {
                                switch (variable.RefTableName)
                                {
                                    case "Listing":
                                        break;
                                    case "ListingImage":
                                        break;
                                    case "ListingAgent":
                                        break;
                                    case "ListingOFIs":
                                        break;
                                    case "ListingVendor":
                                        break;
                                    case "ListingLink":
                                        break;
                                    case "ListingFloorPlan":
                                        break;
                                    case "ListingConjunctionAgent":
                                        break;
                                    case "CompanyContact":
                                        fieldValue = DynamicQueryToSetRecord(variable.CriteriaFieldName, variable.RefTableName, variable.KeyField, contactId,scope.Value);
                                        break;
                                    case "Company":  // commented on request of 2020 https://trello.com/c/MTZJsEeT/965-smartform-data-updating-to-all-stores-back-end
                                   //     fieldValue = DynamicQueryToSetRecord(variable.CriteriaFieldName, variable.RefTableName, variable.KeyField, contact.CompanyId, scope.Value);
                                        break;
                                    case "Address":
                                        if (objCompany != null && objCompany.CanUserUpdateAddress.HasValue && objCompany.CanUserUpdateAddress.Value == true)
                                        {
                                            if (variable.CriteriaFieldName == "State")
                                            {
                                                var address = db.Addesses.Where(g => g.AddressId == contact.AddressId).SingleOrDefault();
                                                if (address != null)
                                                {
                                                    if (address.StateId.HasValue)
                                                    {
                                                        var state = db.States.Where(g => g.StateId == address.StateId.Value).SingleOrDefault();
                                                        if (state != null)
                                                        {
                                                            state.StateName = scope.Value;
                                                        }
                                                    }
                                                    db.SaveChanges();

                                                }
                                            }
                                            else if (variable.CriteriaFieldName == "StateAbbr")
                                            {
                                                var address = db.Addesses.Where(g => g.AddressId == contact.AddressId).SingleOrDefault();
                                                if (address != null)
                                                {
                                                    if (address.StateId.HasValue)
                                                    {
                                                        var state = db.States.Where(g => g.StateId == address.StateId.Value).SingleOrDefault();
                                                        if (state != null)
                                                        {
                                                            state.StateCode = scope.Value;
                                                        }
                                                    }
                                                    db.SaveChanges();

                                                }
                                            }
                                            else if (variable.CriteriaFieldName == "Country")
                                            {
                                                var address = db.Addesses.Where(g => g.AddressId == contact.AddressId).SingleOrDefault();
                                                if (address != null)
                                                {
                                                    if (address.CountryId.HasValue)
                                                    {
                                                        var country = db.Countries.Where(g => g.CountryId == address.CountryId.Value).SingleOrDefault();
                                                        if (country != null)
                                                        {
                                                            country.CountryName = scope.Value;
                                                        }
                                                    }
                                                    db.SaveChanges();
                                                }
                                            }
                                            else
                                            {
                                                fieldValue = DynamicQueryToSetRecord(variable.CriteriaFieldName, variable.RefTableName, variable.KeyField, contact.AddressId, scope.Value);

                                            }
                                        }
                                     break;
                                    default:
                                        break;
                                }
                            }
                        }
                        else
                        {
                            if (variable != null && variable.Scope.HasValue)
                            {
                                //int scopeId = variable.Scope.Value;
                                if (variable.Scope.Value == (int)FieldVariableScopeType.Address)
                                {
                                    if (objCompany != null && objCompany.CanUserUpdateAddress.HasValue && objCompany.CanUserUpdateAddress.Value == true)
                                    {
                                        if (contact != null)
                                        {
                                            var scopeObj = db.ScopeVariables.Where(g => g.VariableId == variable.VariableId && g.Id == contact.AddressId).FirstOrDefault();
                                            if (scopeObj != null)
                                            {
                                                scopeObj.Value = scope.Value;
                                            }
                                            else
                                            {
                                                scope.FieldVariable = null;
                                                db.ScopeVariables.Add(scope);
                                            }
                                        }
                                    }
                                }
                                else if (variable.Scope.Value == (int)FieldVariableScopeType.Contact)
                                {
                                    var scopeObj = db.ScopeVariables.Where(g => g.VariableId == variable.VariableId && g.Id == contactId).FirstOrDefault();
                                    if (scopeObj != null)
                                    {
                                        scopeObj.Value = scope.Value;
                                    } else
                                    {
                                        scope.FieldVariable = null;
                                        db.ScopeVariables.Add(scope);
                                    }
                                }
                                else if (variable.Scope.Value == (int)FieldVariableScopeType.RealEstate)
                                {
                                    // realestate logic 
                                }
                                else if (variable.Scope.Value == (int)FieldVariableScopeType.RealEstateImages)
                                {
                                    // realestate logic 
                                }
                                else if (variable.Scope.Value == (int)FieldVariableScopeType.Store)
                                {
                                    //var scopeObj = db.ScopeVariables.Where(g => g.VariableId == variable.VariableId && g.Id == variable.CompanyId).FirstOrDefault();
                                    //if (scopeObj != null)
                                    //{
                                    //    scopeObj.Value = scope.Value;
                                    //}
                                    //else
                                    //{
                                    //    scope.FieldVariable = null;   
                                    //    db.ScopeVariables.Add(scope);
                                    //}
                                }
                                else if (variable.Scope.Value == (int)FieldVariableScopeType.Territory)
                                {
                                    //if (contact != null)
                                    //{
                                    //    var scopeObj = db.ScopeVariables.Where(g => g.VariableId == variable.VariableId && g.Id == contact.TerritoryId).FirstOrDefault();
                                    //    if (scopeObj != null)
                                    //    {
                                    //        scopeObj.Value = scope.Value;
                                    //    }
                                    //    else
                                    //    {
                                    //        scope.FieldVariable = null;
                                    //        db.ScopeVariables.Add(scope);
                                    //    }
                                    //}
                                }
                            }
                        }

                    }
                }

            }
            db.SaveChanges();
            result = true;
            return result;
        }
        public string[] GetContactImageAndCompanyLogo(long contactID)
        {
            string[] array = new string[6];
            string CompanyLogo = "";
            string ContactLogo = "";
            int contactLogoHeight = 0, contactLogoWidth = 0, companyLogoHeight = 0, companyLogoWidth = 0;
            CompanyContact contact = db.CompanyContacts.Where(g => g.ContactId == contactID).SingleOrDefault();
            if(contact != null)
            {
                System.Drawing.Image objCompanyLogo = null;
                System.Drawing.Image objImage = null;
                Company company = db.Companies.Where(g => g.CompanyId == contact.CompanyId).SingleOrDefault();
                if(contact.image != null && contact.image != ""){
                    ContactLogo = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + contact.image;
                    try
                    {
                        using (objImage = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath( "~/" + contact.image)))
                        {
                            contactLogoWidth = objImage.Width;
                            contactLogoHeight = objImage.Height;
                        }
                    }
                    catch
                    {

                    }
                    finally
                    {
                        if (objImage != null)
                        {
                            objImage.Dispose();
                        }
                    }
                }
                    
                if(company!= null)
                {
                    if (company.Image != null && company.Image != "")
                    {
                        CompanyLogo = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + company.Image;
                        try
                        {
                            using (objCompanyLogo = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath( "~/" + company.Image)))
                            {
                                companyLogoWidth = objCompanyLogo.Width;
                                companyLogoHeight = objCompanyLogo.Height;
                            }

                        }
                        catch
                        {

                        }
                        finally
                        {
                            if (objCompanyLogo != null)
                            {
                                objCompanyLogo.Dispose();
                            }
                        }
                    }
                }
            
              
            }

            array[0] = CompanyLogo;
            array[1] = ContactLogo;
            array[2] = companyLogoHeight.ToString();
            array[3] = companyLogoWidth.ToString();
            array[4] = contactLogoHeight.ToString();
            array[5] = contactLogoWidth.ToString();
            return array;
        }
        public List<ScopeVariable> GetUserTemplateVariables(long itemId, long contactID)
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            List<ScopeVariable> result = new List<ScopeVariable>();
            CompanyContact contact = db.CompanyContacts.Where(g => g.ContactId == contactID).SingleOrDefault();
            Item item = db.Items.Where(g=>g.ItemId == itemId).SingleOrDefault();
            Item orignalItem = null;
            long ListingId = 0;
            if(item != null)
            {
                orignalItem = db.Items.Where(g=>g.ItemId == item.RefItemId).SingleOrDefault();
                if(orignalItem != null)
                {
                    List<MPC.Models.DomainModels.TemplateVariable> listTemplateVariables = new List<Models.DomainModels.TemplateVariable>();
                    listTemplateVariables = db.TemplateVariables.Where(g=>g.TemplateId == orignalItem.TemplateId).ToList();
                    foreach( var objTempVariable in listTemplateVariables)
                    {
                        var FieldVariable = db.FieldVariables.Where(g=>g.VariableId ==objTempVariable.VariableId).SingleOrDefault();
                        if(FieldVariable != null)
                        {
                            if (FieldVariable.IsSystem.HasValue && FieldVariable.IsSystem.Value == true)
                            {

                                var fieldValue = "";
                                if (contact != null)
                                {


                                    switch (FieldVariable.RefTableName)
                                    {
                                        case "Listing":
                                            fieldValue = DynamicQueryToGetRecord(obj.CriteriaFieldName, obj.RefTableName, obj.KeyField, ListingId);
                                            break;
                                        case "ListingImage":  // listing images table based on listing id and image count write a seperate service for it to return all the data 
                                            // fieldValue = GetRealEstateAgent(obj, ListingId);
                                            break;
                                        case "ListingAgent": //from users table based on company id and agent count
                                            fieldValue = GetRealEstateAgent(FieldVariable, ListingId);
                                            break;
                                        case "ListingOFIs":
                                            fieldValue = DynamicQueryToGetRecord(FieldVariable.CriteriaFieldName, FieldVariable.RefTableName, FieldVariable.KeyField, ListingId);
                                            break;
                                        case "ListingVendor":
                                            fieldValue = DynamicQueryToGetRecord(FieldVariable.CriteriaFieldName, FieldVariable.RefTableName, FieldVariable.KeyField, ListingId);
                                            break;
                                        case "ListingLink":
                                            fieldValue = DynamicQueryToGetRecord(FieldVariable.CriteriaFieldName, FieldVariable.RefTableName, FieldVariable.KeyField, ListingId);
                                            break;
                                        case "ListingFloorPlan":
                                            fieldValue = DynamicQueryToGetRecord(FieldVariable.CriteriaFieldName, FieldVariable.RefTableName, FieldVariable.KeyField, ListingId);
                                            break;
                                        case "ListingConjunctionAgent":
                                            fieldValue = DynamicQueryToGetRecord(FieldVariable.CriteriaFieldName, FieldVariable.RefTableName, FieldVariable.KeyField, ListingId);
                                            break;
                                        case "CompanyContact":
                                            fieldValue = DynamicQueryToGetRecord(FieldVariable.CriteriaFieldName, FieldVariable.RefTableName, FieldVariable.KeyField, contactID);
                                            break;
                                        case "Company":
                                            //   keyValue = SessionParameters.ContactCompany.ContactCompanyID;
                                            fieldValue = DynamicQueryToGetRecord(FieldVariable.CriteriaFieldName, FieldVariable.RefTableName, FieldVariable.KeyField, contact.CompanyId);
                                            break;
                                        case "Address":
                                            //  keyValue = SessionParameters.CustomerContact.AddressID;
                                            if (FieldVariable.CriteriaFieldName == "State")
                                            {
                                                var address = db.Addesses.Where(g => g.AddressId == contact.AddressId).SingleOrDefault();
                                                if (address != null)
                                                {
                                                    if (address.StateId.HasValue)
                                                    {
                                                        var state = db.States.Where(g => g.StateId == address.StateId.Value).SingleOrDefault();
                                                        if (state != null)
                                                        {
                                                            fieldValue = state.StateName;
                                                        }
                                                    }

                                                }
                                            }
                                            else if (FieldVariable.CriteriaFieldName == "Country")
                                            {
                                                var address = db.Addesses.Where(g => g.AddressId == contact.AddressId).SingleOrDefault();
                                                if (address != null)
                                                {
                                                    if (address.CountryId.HasValue)
                                                    {
                                                        var country = db.Countries.Where(g => g.CountryId == address.CountryId.Value).SingleOrDefault();
                                                        if (country != null)
                                                        {
                                                            fieldValue = country.CountryName;
                                                        }
                                                    }

                                                }
                                            }
                                            else if (FieldVariable.CriteriaFieldName == "StateAbbr")
                                            {
                                                var address = db.Addesses.Where(g => g.AddressId == contact.AddressId).SingleOrDefault();
                                                if (address != null)
                                                {
                                                    if (address.StateId.HasValue)
                                                    {
                                                        var state = db.States.Where(g => g.StateId == address.StateId.Value).SingleOrDefault();
                                                        if (state != null)
                                                        {
                                                            fieldValue = state.StateCode;
                                                        }
                                                    }

                                                }
                                            }
                                            else
                                            {
                                                fieldValue = DynamicQueryToGetRecord(FieldVariable.CriteriaFieldName, FieldVariable.RefTableName, FieldVariable.KeyField, contact.AddressId);
                                            }
                                          break;
                                        default:
                                            break;
                                    }
                                    ScopeVariable objScopeVariable = new ScopeVariable();
                                    objScopeVariable.Scope = 0;
                                    objScopeVariable.VariableId = FieldVariable.VariableId;
                                    objScopeVariable.Value = fieldValue;
                                    objScopeVariable.FieldVariable = FieldVariable;
                                    result.Add(objScopeVariable);
                                }
                            }
                            else
                            {
                                if (FieldVariable != null && FieldVariable.Scope.HasValue)
                                {
                                    int scope = FieldVariable.Scope.Value;
                                    if (scope == (int)FieldVariableScopeType.Address)
                                    {
                                        var scopeObj = db.ScopeVariables.Where(g => g.VariableId == FieldVariable.VariableId && g.Id == contact.AddressId).FirstOrDefault();
                                        if (scopeObj != null)
                                        {
                                            result.Add(scopeObj);
                                        }
                                        else
                                        {
                                            ScopeVariable objScopeVariable = new ScopeVariable();
                                            objScopeVariable.Scope = 0;
                                            objScopeVariable.VariableId = FieldVariable.VariableId;
                                            objScopeVariable.Value = FieldVariable.DefaultValue;
                                            objScopeVariable.Id = contact.AddressId;
                                            objScopeVariable.Scope = scope;
                                            objScopeVariable.FieldVariable = FieldVariable;
                                            result.Add(objScopeVariable);
                                        }
                                    }
                                    else if (scope == (int)FieldVariableScopeType.Contact)
                                    {
                                        var scopeObj = db.ScopeVariables.Where(g => g.VariableId == FieldVariable.VariableId && g.Id == contactID).FirstOrDefault();
                                        if (scopeObj != null)
                                        {
                                            result.Add(scopeObj);
                                        }
                                        else
                                        {
                                            ScopeVariable objScopeVariable = new ScopeVariable();
                                            objScopeVariable.Scope = 0;
                                            objScopeVariable.VariableId = FieldVariable.VariableId;
                                            objScopeVariable.Value = FieldVariable.DefaultValue;
                                            objScopeVariable.Id = contactID;
                                            objScopeVariable.Scope = scope;
                                            objScopeVariable.FieldVariable = FieldVariable;
                                            result.Add(objScopeVariable);
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
                                        var scopeObj = db.ScopeVariables.Where(g => g.VariableId == FieldVariable.VariableId && g.Id == FieldVariable.CompanyId).FirstOrDefault();
                                        if (scopeObj != null)
                                        {
                                            result.Add(scopeObj);

                                        }
                                        else
                                        {
                                            ScopeVariable objScopeVariable = new ScopeVariable();
                                            objScopeVariable.Scope = 0;
                                            objScopeVariable.VariableId = FieldVariable.VariableId;
                                            objScopeVariable.Value = FieldVariable.DefaultValue;
                                            objScopeVariable.Id = FieldVariable.CompanyId.Value;
                                            objScopeVariable.Scope = scope;
                                            objScopeVariable.FieldVariable = FieldVariable;
                                            result.Add(objScopeVariable);
                                        }
                                    }
                                    else if (scope == (int)FieldVariableScopeType.Territory)
                                    {
                                        // var contact = db.CompanyContacts.Where(g => g.ContactId == contactId).SingleOrDefault();
                                        if (contact != null)
                                        {
                                            var scopeObj = db.ScopeVariables.Where(g => g.VariableId == FieldVariable.VariableId && g.Id == contact.TerritoryId).FirstOrDefault();
                                            if (scopeObj != null)
                                            {
                                                result.Add(scopeObj);
                                            }
                                        }
                                        else
                                        {
                                            ScopeVariable objScopeVariable = new ScopeVariable();
                                            objScopeVariable.Scope = 0;
                                            objScopeVariable.VariableId = FieldVariable.VariableId;
                                            objScopeVariable.Value = FieldVariable.DefaultValue;
                                            objScopeVariable.Id = contact.TerritoryId.Value;
                                            objScopeVariable.Scope = scope;
                                            objScopeVariable.FieldVariable = FieldVariable;
                                            result.Add(objScopeVariable);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    var template = db.Templates.Where(g => g.ProductId == item.TemplateId).SingleOrDefault();
                    if(template != null)
                    {
                        if (contactID == template.contactId)
                        {
                            List<MPC.Models.DomainModels.TemplateVariable> lstTemplateVariables = db.TemplateVariables.Where(g => g.TemplateId == template.ProductId).ToList();
                            foreach (var objTVar in lstTemplateVariables)
                            {
                                var scopeObj = result.Where(g => g.VariableId == objTVar.VariableId).SingleOrDefault();
                                if (scopeObj != null)
                                {
                                    if (objTVar.VariableText != null && objTVar.VariableText != "")
                                        scopeObj.Value = objTVar.VariableText;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
        public bool AutoResolveTemplateVariables(long itemID, long contactId)
        {
            bool result = false;
           
            long templateID = 0;
            List<ScopeVariable> lstVariables = GetUserTemplateVariables(itemID, contactId);
            var item = db.Items.Where(g=>g.ItemId == itemID).SingleOrDefault();
            var contact = db.CompanyContacts.Where(g => g.ContactId == contactId).SingleOrDefault();
            string[] logos = GetContactImageAndCompanyLogo(contactId);
            if(item != null)
            {
                if(item.TemplateId.HasValue)
                    templateID = item.TemplateId.Value;
            }
            List<TemplateObject> lstTemplateObjects = db.TemplateObjects.Where(g => g.ProductId == templateID).ToList();
            foreach(var obj in lstTemplateObjects)
            {
                foreach(var variable in lstVariables)
                {
                    if (variable != null)
                    {

                        obj.ContentString = obj.ContentString.Replace(variable.FieldVariable.VariableTag, variable.Value);
                        if (variable.FieldVariable != null)
                        {
                            //if (variable != null && variable.Value != "" && variable.FieldVariable.VariableTag != "")
                            //{
                            //    obj.ContentString = obj.ContentString.Replace(variable.FieldVariable.VariableTag.ToUpper(), variable.Value.ToUpper());
                            //    obj.ContentString = obj.ContentString.Replace(variable.FieldVariable.VariableTag.ToLower(), variable.Value.ToLower());
                            //}
                            // replace prefix and postFixes 
                            if (variable.FieldVariable.VariableTag != null)
                            {
                                string tag = variable.FieldVariable.VariableTag.Replace("{{", "").Replace("}}", "");
                                string preFix = "{{" + tag + "_pre}}"; ;
                                string postFix = "{{" + tag + "_post}}";


                                if (contact != null)
                                {
                                    var ext = db.VariableExtensions.Where(g => g.CompanyId == contact.CompanyId && g.FieldVariableId == variable.FieldVariable.VariableId).SingleOrDefault();
                                    if (ext != null)
                                    {
                                        if (ext.VariablePrefix != null && ext.VariablePrefix != "")
                                        {
                                            obj.ContentString = obj.ContentString.Replace(preFix, ext.VariablePrefix);
                                            //obj.ContentString = obj.ContentString.Replace(preFix.ToUpper(), ext.VariablePrefix.ToUpper());
                                            //obj.ContentString = obj.ContentString.Replace(preFix.ToLower(), ext.VariablePrefix.ToLower());
                                        }
                                        if (ext.VariablePostfix != null && ext.VariablePostfix != "")
                                        {
                                            obj.ContentString = obj.ContentString.Replace(postFix, ext.VariablePostfix);
                                            //obj.ContentString = obj.ContentString.Replace(postFix.ToUpper(), ext.VariablePostfix.ToUpper());
                                            //obj.ContentString = obj.ContentString.Replace(postFix.ToLower(), ext.VariablePostfix.ToLower());
                                        }
                                    }
                                }
                            }
                        }
                    }
                   
                    
                }
                if (obj.ObjectType == 8)
                {
                    if (logos[0] != "")
                    {
                        obj.ContentString = logos[0];
                    }
                }
                else if (obj.ObjectType == 12)
                {
                    if (logos[1] != "")
                    {
                        obj.ContentString = logos[1];
                    }
                }
            }

            db.SaveChanges();
            result = true;
            return result;
        }
        public List<VariableExtensionWebstoreResposne> getVariableExtensions(List<ScopeVariable> listScope, long contactId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            List<VariableExtensionWebstoreResposne> listExtensions = new List<VariableExtensionWebstoreResposne>();
            var contact = db.CompanyContacts.Where(g=>g.ContactId == contactId).SingleOrDefault();
            if(contact != null)
            {
                var company = db.Companies.Where(g=>g.CompanyId == contact.CompanyId).SingleOrDefault();
                if(company != null)
                {
                    foreach(var variable in listScope)
                    {
                        var ext = db.VariableExtensions.Where(g => g.CompanyId == company.CompanyId && g.FieldVariableId == variable.FieldVariable.VariableId).SingleOrDefault();
                        if(ext != null)
                        {
                            VariableExtensionWebstoreResposne obj = new VariableExtensionWebstoreResposne();
                            obj.CollapsePostfix = ext.CollapsePostfix;
                            obj.CollapsePrefix = ext.CollapsePrefix;
                            obj.CompanyId = ext.CompanyId;
                            obj.FieldVariableId = ext.FieldVariableId;
                            obj.Id = ext.Id;
                            obj.VariablePostfix = ext.VariablePostfix;
                            obj.VariablePrefix = ext.VariablePrefix;

                            listExtensions.Add(obj);
                        }
                    }
                
                }
            }
            return listExtensions;
        }

        public string GetRealEstateAgent(FieldVariable obj , long propertyId)
        {
            int count = 1;

            return "";
        }
        #endregion
    }
}
