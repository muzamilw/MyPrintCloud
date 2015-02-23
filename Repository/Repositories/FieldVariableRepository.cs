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
            if (DbSet.Count(vf => vf.VariableName != null && vf.VariableName == variableName && vf.CompanyId == companyId && vf.VariableId != variableId) > 0)
            {
                return "Field Variable already exist with same name.";
            }
            if (DbSet.Count(vf => vf.VariableTag != null && vf.VariableTag == variableTag && vf.CompanyId == companyId && vf.VariableId != variableId) > 0)
            {
                return "Field Variable already exist with same tag.";
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
                (s.CompanyId == request.CompanyId);

            int rowCount = DbSet.Count(query);
            // ReSharper disable once ConditionalTernaryEqualBranch
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
        /// Get Field Varibale By Company ID Of Contact Type
        /// </summary>
        public IEnumerable<FieldVariable> GetFieldVariableByCompanyId(long companyId)
        {
            return DbSet.Where(vf => vf.CompanyId == companyId && vf.Scope == (int)FieldVariableScopeType.Contact).ToList();
        }
        #endregion
    }
}
