using Microsoft.Practices.Unity;
using MPC.Common;
using MPC.Interfaces.Repository;
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


        public List<FieldVariable> GetVariablesData(bool isRealestateproduct, long storeId)
        {
            List<VariableList> objList = new List<VariableList>();
            //if (isRealestateproduct)
            //{
            //    objList = from p in db.VariableSections
            //              join es in db.FieldVariables on p.VariableSectionId equals es.VariableSectionId
            //              where (es.VariableType == (int)TemplateVariableType.Global || es.VariableType == (int)TemplateVariableType.RealState || es.VariableType == (int)TemplateVariableType.RealStateImages)
            //              orderby p.VariableSectionID, es.VariableType, es.SortOrder
            //              select new
            //              {
            //                  SectionName = p.SectionName,
            //                  VariableID = es.VariableID,
            //                  VariableName = es.VariableName,
            //                  VariableTag = es.VariableTag,
            //                  VariableType = es.VariableType

            //              };

            //}
            //else
            //{
            //    objList = from p in db.VariableSections
            //              join es in db.FieldVariables on p.VariableSectionId equals es.VariableSectionId
            //              where (es.VariableType == (int)TemplateVariableType.Global)
            //              orderby es.VariableType, es.SortOrder
            //              select new
            //              {
            //                  SectionName = p.SectionName,
            //                  VariableID = es.VariableID,
            //                  VariableName = es.VariableName,
            //                  VariableTag = es.VariableTag,
            //                  VariableType = es.VariableType

            //              };

            //}

          
          //      return objList;
            return db.FieldVariables.Take(10).ToList();
        }
        public Stream GetTemplateVariables(long templateId)
        {
            //var objList = from p in db.TemplateVariables
            //              join es in db.FieldVariables on p.VariableId equals es.VariableId
            //              where (p.TemplateId == templateId)
            //              orderby es.VariableType, es.SortOrder
            //              select new
            //              {
            //                  VariableTag = es.VariableTag,
            //                  VariableID = p.VariableID,
            //                  TemplateID = p.TemplateID

            //              };
        
            return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(true)));
        }
        public enum TemplateVariableType
        {
            Global = 1,
            RealState = 2,
            RealStateImages = 3
        }
        #endregion
    }
}
