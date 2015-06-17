using System.Collections.Generic;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Field Variable Repository Interface
    /// </summary>
    public interface IFieldVariableRepository : IBaseRepository<FieldVariable, long>
    {

        /// <summary>
        ///Is Field Variable Name Or Tag Already Exist
        /// </summary>
        string IsFiedlVariableNameOrTagDuplicate(string variableName, string variableTag, long companyId, long variableId);

        /// <summary>
        /// Get Field Variables By Company Id
        /// </summary>
        FieldVariableResponse GetFieldVariable(FieldVariableRequestModel request);

        /// <summary>
        /// Get Field Varibale By Company ID and Scope
        /// </summary>
        IEnumerable<FieldVariable> GetFieldVariableByCompanyIdAndScope(long companyId, int scope);

        /// <summary>
        /// Get Field Varibale By Company For Smart Form
        /// </summary>
        IEnumerable<FieldVariable> GetFieldVariablesForSmartForm(long companyId);

        /// <summary>
        /// Get System Variables
        /// </summary>
        IEnumerable<FieldVariable> GetSystemVariables();

        /// <summary>
        /// Get all system variables and company variables
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        List<FieldVariable> GetSystemAndCompanyVariables(long companyID);

        /// <summary>
        /// Get System Field Variables
        /// </summary>
        FieldVariableResponse GetSystemFieldVariable(FieldVariableRequestModel request);
    }
}
