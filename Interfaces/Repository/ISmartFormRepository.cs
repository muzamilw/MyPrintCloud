using MPC.Common;
using MPC.Models.DomainModels;
using System.Collections.Generic;
using System.IO;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.Repository
{

    /// <summary>
    /// Smart Form Repository Interface
    /// </summary>
    public interface ISmartFormRepository : IBaseRepository<SmartForm, long>
    {
        IEnumerable<SmartForm> GetAllForCompany(long companyId);
        List<VariableList> GetVariablesData(bool isRealestateproduct, long companyId, long organisationId);
        List<TemplateVariablesObj> GetTemplateVariables(long templateId);
        bool SaveTemplateVariables(List<TemplateVariablesObj> obj);
        List<SmartFormUserList> GetUsersList(long contactId);
        SmartFormWebstoreResponse GetSmartForm(long smartFormId);
        List<SmartFormDetail> GetSmartFormObjects(long smartFormId, out List<VariableOption> listVariables);

        /// <summary>
        /// Get Smart Form By Company Id
        /// </summary>
        SmartFormResponse GetSmartForms(SmartFormRequestModel request);

        List<ScopeVariable> GetScopeVariables(List<SmartFormDetail> smartFormDetails, out bool hasContactVariables, long contactId);
        Dictionary<long, List<ScopeVariable>> GetUserScopeVariables(List<SmartFormDetail> smartFormDetails, List<SmartFormUserList> contacts, long templateId);
        bool SaveUserProfilesData(Dictionary<long, List<ScopeVariable>> obj);
        string[] GetContactImageAndCompanyLogo(long contactID);
        List<ScopeVariable> GetUserTemplateVariables(long itemId, long contactID);
        List<ScopeVariable> GetTemplateScopeVariables(long templateID, long contactId);
        bool AutoResolveTemplateVariables(long itemID, long contactId);
        List<VariableExtensionWebstoreResposne> getVariableExtensions(List<ScopeVariable> listScope, long contactId);
    }
}
