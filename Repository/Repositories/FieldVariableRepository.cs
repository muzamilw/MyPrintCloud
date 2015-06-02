using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Edm_EntityMappingGeneratedViews;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;
namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Field Variable Repository
    /// </summary>
    public class FieldVariableRepository : BaseRepository<FieldVariable>, IFieldVariableRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public FieldVariableRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<FieldVariable> DbSet
        {
            get
            {
                return db.FieldVariables;
            }
        }

        #endregion

        #region Public

        /// <summary>
        ///Is Fiedl Variable Name Or Tag Already Exist
        /// </summary>
        public string IsFiedlVariableNameOrTagDuplicate(string variableName, string variableTag, long companyId, long variableId)
        {
            if (DbSet.Count(vf => vf.VariableName != null && vf.VariableName.ToLower() == variableName.ToLower() && vf.CompanyId == companyId && vf.VariableId != variableId) > 0)
            {
                return "Field Variable already exist with same Name.";
            }
            if (DbSet.Count(vf => vf.VariableTag != null && vf.VariableTag.ToLower() == variableTag.ToLower() && vf.CompanyId == companyId && vf.VariableId != variableId) > 0)
            {
                return "Field Variable already exist with same Tag.";
            }
            return null;
        }


        /// <summary>
        /// Get Field Variables By Company Id
        /// </summary>
        public FieldVariableResponse GetFieldVariable(FieldVariableRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            Expression<Func<FieldVariable, bool>> query =
            s =>
                (s.CompanyId == request.CompanyId && s.OrganisationId == OrganisationId);

            int rowCount = DbSet.Count(query);
            IEnumerable<FieldVariable> fieldVariables = request.IsAsc
           ? DbSet.Where(query)
           .OrderByDescending(x => x.VariableName)
               .Skip(fromRow)
               .Take(toRow)
               .ToList()
           : DbSet.Where(query)
           .OrderByDescending(x => x.VariableName)
               .Skip(fromRow)
               .Take(toRow)
               .ToList();
            return new FieldVariableResponse
            {
                RowCount = rowCount,
                FieldVariables = fieldVariables
            };
        }

        /// <summary>
        /// Get Field Varibale By Company ID and Scope Type
        /// </summary>
        public IEnumerable<FieldVariable> GetFieldVariableByCompanyIdAndScope(long companyId, int scope)
        {
            return DbSet.Where(vf => vf.CompanyId == companyId && vf.Scope == scope).ToList();
        }

        /// <summary>
        /// Get Field Varibale By Company For Smart Form
        /// </summary>
        public IEnumerable<FieldVariable> GetFieldVariablesForSmartForm(long companyId)
        {
            return DbSet.Where(vf => vf.CompanyId == companyId && vf.OrganisationId == OrganisationId).ToList();
        }

        /// <summary>
        /// Get System Variables
        /// </summary>
        public IEnumerable<FieldVariable> GetSystemVariables()
        {
            return DbSet.Where(fv => fv.IsSystem == true && fv.CompanyId == null && fv.OrganisationId == null && (fv.Scope == (int)FieldVariableScopeType.SystemStore || fv.Scope == (int)FieldVariableScopeType.SystemContact || fv.Scope == (int)FieldVariableScopeType.SystemAddress || fv.Scope == (int)FieldVariableScopeType.SystemTerritory)).ToList();
        }

        /// <summary>
        /// Get system variables and company variables
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        public List<FieldVariable> GetSystemAndCompanyVariables(long companyID)
        {
            return db.FieldVariables.Where(g=>g.IsSystem == true || g.CompanyId == companyID).ToList();
        }
        #endregion
    }
}
