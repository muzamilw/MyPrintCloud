using Microsoft.Practices.Unity;
using MPC.Common;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
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


        public List<FieldVariable> GetVariablesData(bool isRealestateproduct, long companyId, long organisationId)
        {
            //List<VariableList> resultList = new List<VariableList>();
            //if (isRealestateproduct)
            //{
            //   var objList = from p in db.VariableSections
            //              join es in db.FieldVariables on p.VariableSectionId equals es.VariableSectionId
            //                 where (es.IsSystem == true || (es.CompanyId == companyId && es.OrganisationId == organisationId))
            //              orderby p.VariableSectionId, es.VariableType, es.SortOrder
            //              select new
            //              {
            //                  SectionName = p.SectionName,
            //                  VariableID = es.VariableId,
            //                  VariableName = es.VariableName,
            //                  VariableTag = es.VariableTag,
            //                  VariableType = es.VariableType

            //              };

            //}
            //else
            //{
            //    var objList = from p in db.VariableSections
            //                  join es in db.FieldVariables on p.VariableSectionId equals es.VariableSectionId
            //                  where (es.IsSystem == true || (es.CompanyId == companyId && es.OrganisationId == organisationId))
            //                  orderby p.VariableSectionId, es.VariableType, es.SortOrder
            //                  select new
            //                  {
            //                      SectionName = p.SectionName,
            //                      VariableID = es.VariableId,
            //                      VariableName = es.VariableName,
            //                      VariableTag = es.VariableTag,
            //                      VariableType = es.VariableType

            //                  };

            //}


           // return objList;
            return db.FieldVariables.Take(10).ToList();
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
            foreach(var obj in objList)
            {
                TemplateVariablesObj objToAdd = new TemplateVariablesObj(obj.VariableTag,obj.VariableID.Value,obj.TemplateID.Value);

                objResult.Add(objToAdd);
            }
            return objResult;
        }
        //public enum TemplateVariableType
        //{
        //    Global = 1,
        //    RealState = 2,
        //    RealStateImages = 3
        //}
        #endregion
    }
}
