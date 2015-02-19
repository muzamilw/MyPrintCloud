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
    }
}
