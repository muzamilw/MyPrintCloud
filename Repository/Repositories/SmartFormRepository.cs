using Microsoft.Practices.Unity;
using MPC.Common;
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
    class SmartFormRepository : BaseRepository<TemplateVariable>, ISmartFormRepository
    {
          #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SmartFormRepository(IUnityContainer container)
            : base(container)
        {

        }
        protected override IDbSet<TemplateVariable> DbSet
        {
            get
            {
                return db.TemplateVariables;
            }
        }



        #endregion
        #region public

        /// <summary>
        /// Find Template
        /// </summary>
        public TemplateVariable Find(int id)
        {
            return DbSet.Find(id);
        }


        public List<VariableList> GetVariablesData(bool isRealestateproduct,long storeId)
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

                return objList;
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
