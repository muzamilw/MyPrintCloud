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
                objUsers = new List<SmartFormUserList>();
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
                if (contacts.Count > 0)
                {
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

            return db.SmartForms.Where(g => g.SmartFormId == smartFormId).SingleOrDefault();
        }

        public List<SmartFormDetail> GetSmartFormObjects(long smartFormId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;

            return db.SmartFormDetails.Where(g => g.SmartFormId == smartFormId).ToList();
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

        #endregion
    }
}
