
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace MPC.Repository.Repositories
{
    public class TemplateVariableRepository : BaseRepository<TemplateVariable>, ITemplateVariableRepository
    {
           #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public TemplateVariableRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<TemplateVariable> DbSet
        {
            get
            {
                return db.TemplateVariables;
            }
        }


        #endregion

        #region public

        public List<TemplateVariable> getVariablesList(long templateID)
        {
            return db.TemplateVariables.Where(g => g.TemplateId == templateID).ToList();
        }
        public void InsertTemplateVariables(List<TemplateVariable> lstTemplateVariables, List<TemplateVariableExtension> lstVariableExtensions)
        {
            if (lstTemplateVariables != null && lstTemplateVariables.Count != 0)
            {
                db.TemplateVariables.AddRange(lstTemplateVariables);
            }
            if(lstVariableExtensions != null && lstVariableExtensions.Count!= 0)
            {
                int templateId = lstVariableExtensions[0].TemplateId.Value;
                var variableRange = db.TemplateVariableExtensions.Where(g => g.TemplateId == templateId).ToList();
                if(variableRange !=  null)
                    db.TemplateVariableExtensions.RemoveRange(variableRange);
                db.TemplateVariableExtensions.AddRange(lstVariableExtensions);
            }
            db.SaveChanges();


        }
        #endregion

    }
}
